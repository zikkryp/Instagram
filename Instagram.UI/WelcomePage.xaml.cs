using Instagram.DAL.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                await System.Threading.Tasks.Task.Delay(1);

                var account = await new Storage().GetActiveAccountAsync();
                
                this.Frame.Navigate(typeof(FeedPage));
            }
            catch (Exception e)
            {
                ShowDialog(e.Message);
            }
        }

        private async void ShowDialog(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }

        private async void Authorize()
        {
            Authentication authentication = new Authentication();
            await authentication.AuthenticateAsync();

            this.Frame.Navigate(typeof(FeedPage));
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            Authorize();
        }
    }
}
