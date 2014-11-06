using Instagram.DAL.DataSource;
using Instagram.DAL.Model;
using Instagram.DAL.Storage;
using Instagram.DAL.Storage.Model;
using Instagram.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Instagram.UI
{
    public sealed partial class FeedPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public FeedPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.PageState != null)
            {
                this.Feed = e.PageState["Feed"] as Feed;
                this.defaultViewModel["Feed"] = this.Feed.Items;

                this.User = e.PageState["User"] as User;
                this.defaultViewModel["User"] = this.User;

                UpdateControls();

                return;
            }

            LoadFeed(true);
            GetUser();
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            e.PageState["Feed"] = this.Feed;
            e.PageState["User"] = this.User;
        }

        #region Регистрация NavigationHelper

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private Feed Feed;
        private User User;

        private async void GetUser()
        {
            this.User = await UserSource.GetUserAsync();

            this.defaultViewModel["User"] = User;
        }

        private async void LoadFeed(bool refresh)
        {
            //ring.IsActive = true;

            progressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;

            try
            {
                this.Feed = await FeedSource.GetFeedAsync(refresh);
                
                this.defaultViewModel["Feed"] = Feed.Items;

                if (!Feed.Pagination.HasMorePages)
                {
                    moreButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
                else
                {
                    moreButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
            catch (Exception e)
            {
                ShowDialog(e.Message);
            }
            finally
            {
                //ring.IsActive = false;

                progressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private async void ShowDialog(string message)
        {
            await new MessageDialog(message,"FeedPage").ShowAsync(); 
        }

        private void UpdateControls()
        {
            if (Feed.Pagination.HasMorePages)
            {
                moreButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                moreButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as FeedItem;
            
            this.Frame.Navigate(typeof(ItemViewPage), e.ClickedItem);
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            LoadFeed(false);
        }

        private async void UserInfo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PopupMenu menu = new PopupMenu();

            menu.Commands.Add(new UICommand("View", new UICommandInvokedHandler(this.OpenUsersPage)));
            menu.Commands.Add(new UICommand("Settings", new UICommandInvokedHandler(this.OpenSettings)));
            menu.Commands.Add(new UICommand("Sign out", new UICommandInvokedHandler(this.SignOut)));

            var pointTransform = ((GridViewItem)sender).TransformToVisual(Window.Current.Content);
            var screenCoords = pointTransform.TransformPoint(new Point(200, 125));

            await menu.ShowAsync(screenCoords);
        }

        private async void OpenUsersPage(IUICommand command)
        {
            await new MessageDialog(command.Label).ShowAsync();
        }

        private async void OpenSettings(IUICommand command)
        {
            await new MessageDialog(command.Label).ShowAsync();
        }

        private async void SignOut(IUICommand command)
        {
            try
            {
                Storage storage = new Storage();

                var account = await storage.GetActiveAccountAsync();

                account.IsAutoSignIn = false;

                await storage.UpdateItem<Account>(account);

                this.Frame.Navigate(typeof(WelcomePage));
            }
            catch (Exception e)
            {
                ShowDialog(e.Message);
            }
        }
    }
}
