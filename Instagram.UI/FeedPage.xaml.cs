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
                
            LoadPage();
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

        private async void LoadPage()
        {
            ring.IsActive = true;

            try
            {
                this.Feed = await FeedSource.GetFeedAsync(false);
                this.User = await UserSource.GetUserAsync();

                this.defaultViewModel["User"] = User;
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
                ring.IsActive = false;
            }
        }

        private async void ShowDialog(string message)
        {
            await new MessageDialog(message).ShowAsync(); 
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
            LoadPage();
        }
    }
}
