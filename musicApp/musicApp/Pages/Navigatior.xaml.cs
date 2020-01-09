using musicApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace musicApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Navigatior : Page
    {
        public Navigatior()
        {
            this.InitializeComponent();
            CheckToken();
        }

        private void nvSample_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            switch (((NavigationViewItem)args.SelectedItem).Tag.ToString())
            {
                case "login":
                    contentFrame.Navigate(typeof(Pages.Login));
                    SANav.Header = "Login";
                    break;
                case "register":
                    contentFrame.Navigate(typeof(Pages.Register));
                    SANav.Header = "Register";
                    break;
                case "listSong":
                    if (App.token.Length > 0)
                    {
                        contentFrame.Navigate(typeof(Pages.ListSong));
                        SANav.Header = "List Song";
                    }
                    else
                    {
                        goToLogin();
                    }
                    break;
                case "mySong":
                    if (App.token.Length > 0)
                    {
                        contentFrame.Navigate(typeof(Pages.MySong));
                        SANav.Header = "My Song";
                    }
                    else
                    {
                        goToLogin();
                    }
                    break;
                case "registerSong":
                    if (App.token.Length > 0)
                    {
                        contentFrame.Navigate(typeof(Pages.RegisterSong));
                        SANav.Header = "Register Song";
                    }
                    else
                    {
                        goToLogin();
                    }
                    break;
                case "handlerFile":
                    contentFrame.Navigate(typeof(Pages.testFile));
                    break;
                case "infor":
                    if (App.token.Length > 0)
                    {
                        contentFrame.Navigate(typeof(Pages.MemberInformation));
                        SANav.Header = "Information";
                    }
                    else
                    {
                        goToLogin();
                    }
                    break;
            }

        }

        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (App.token.Length > 0)
            {
                var write = Task.Run(async () => await HandlerFileService.WriteFile("token.txt", "")).Result;
                App.token = "";
            }
            contentFrame.Navigate(typeof(Pages.Login));
        }

        public void SetSelectedNavigationItem(int index)
        {
            SANav.SelectedItem = SANav.MenuItems[index];
        }
        public static Navigatior GetCurrent()
        {
            Frame appFrame = Window.Current.Content as Frame;
            return appFrame.Content as Navigatior;
        }

        private void CheckToken()
        {
            if (App.token.Length > 0)
            {
                contentFrame.Navigate(typeof(Pages.ListSong));
                SANav.Header = "List Song";

            }
            else
            {
                contentFrame.Navigate(typeof(Pages.Login));
                SANav.Header = "Login";
            }
        }

        private void goToLogin()
        {
            contentFrame.Navigate(typeof(Pages.Login));
            SANav.Header = "Login";
        }
        

    }
}
