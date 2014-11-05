using Instagram.DAL.Storage;
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
        public WelcomePage()
        {
            this.InitializeComponent();

            GetActiveAccount();
        }

        private async void GetActiveAccount()
        {
            try
            {
                var account = await new Storage().GetActiveAccountAsync();

                if (account == null)
                {
                    //if (await new Authentication().AuthenticateAsync() != null)
                    //{
                    //    this.Frame.Navigate(typeof(FeedPage));
                    //}

                    return;
                }
                
                this.Frame.Navigate(typeof(FeedPage));
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
                this.Frame.Navigate(typeof(FeedPage));
            }
        }
    }
}
