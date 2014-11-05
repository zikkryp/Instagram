using Instagram.DAL.DataSource;
using Instagram.DAL.Model;
using Instagram.DAL.Storage;
using Instagram.DAL.Storage.Model;
using Instagram.UI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class WelcomePage : Page
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


        public WelcomePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            GetActiveAccount();
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
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

        private Account account;

        private async void GetActiveAccount()
        {
            try
            {
                this.account = await new Storage().GetActiveAccountAsync();

                if (this.account == null)
                {
                    //if (await new Authentication().AuthenticateAsync() != null)
                    //{
                    //    this.Frame.Navigate(typeof(FeedPage));
                    //}

                    return;
                }

                gridViewItem.Visibility = Windows.UI.Xaml.Visibility.Visible;

                await GetUserInfo();

                if (this.account.IsAutoSignIn)
                {
                    this.Frame.Navigate(typeof(FeedPage));
                }
            }
            catch (Exception e)
            {
                ShowDialog(e.Message);
            }
        }

        private async void ShowDialog(string message)
        {
            await new MessageDialog(message, "WelcomePage").ShowAsync();
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (await new Authentication().AuthenticateAsync() != null)
            {
                GetActiveAccount();
                //this.Frame.Navigate(typeof(FeedPage));
            }
        }

        private async Task GetUserInfo()
        {
            ring.IsActive = true;
            
            userGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            try
            {
                var user = await UserSource.GetUserAsync();
                this.account.Username = user.Username;
                this.account.ProfilePicture = user.ProfilePicture;
                this.account.FullName = user.FullName;
                
                this.defaultViewModel["User"] = this.account;

                userGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;

                UpdateUserStorage();
            }
            finally
            {
                ring.IsActive = false;
            }
        }

        private async void UpdateUserStorage()
        {
            Storage storage = new Storage();

            //this.account = await storage.GetActiveAccountAsync();

            //this.account.Username = user.Username;
            //this.account.ProfilePicture = user.ProfilePicture;

            await storage.UpdateItem<Account>(account);
        }

        private void SignIn_User(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FeedPage));
        }

        private async void Exit_Click(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();

            try
            {
                if (await storage.DeleteItem<Account>(this.account.Id))
                {
                    this.gridViewItem.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
            catch(Exception ex)
            {
                ShowDialog(ex.Message);
            }
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();

            //this.account = await storage.GetActiveAccountAsync();

            this.account.IsAutoSignIn = true;

            await storage.UpdateItem<Account>(this.account);
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();

            //this.account = await storage.GetActiveAccountAsync();

            this.account.IsAutoSignIn = false;

            await storage.UpdateItem<Account>(this.account);
        }
    }
}
