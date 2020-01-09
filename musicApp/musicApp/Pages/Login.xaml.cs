using Aspose.Pdf.Facades;
using musicApp.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class Login : Page
    {
        private bool status = false;
        private IMemberService _service;
        public Login()
        {
            this.InitializeComponent();
            txtEmail.AddHandler(TappedEvent, new TappedEventHandler(textBox_Tapped), true);
            txtPassword.AddHandler(TappedEvent, new TappedEventHandler(textBox_Tapped), true);
            _service = new ApiMemberService();
        }

        private async void Button_Login(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            var errors = validateLogin(email, password);

            if (errors.Count > 0)
            {
                Erremail.Text = errors.ContainsKey("email") ? errors["email"] : "";
                Errpassword.Text = errors.ContainsKey("password") ? errors["password"] : "";

            }
            else
            {
                string token = await _service.Login(email, password);
                if (!token.Equals("error"))
                {
                    var write = Task.Run(async () => await HandlerFileService.WriteFile("token.txt", token)).Result;
                    App.token = Task.Run(async () => await HandlerFileService.ReadFile("token.txt")).Result;
                    Navigatior.GetCurrent().SetSelectedNavigationItem(2);
                    
                }
                else
                {
                    this.status = true;
                    statusLogin.Text = "Invalid Infomation";
                    Erremail.Text = "";
                    Errpassword.Text = "";
                }
            }

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Navigatior.GetCurrent().SetSelectedNavigationItem(2);
        }

        private Dictionary<string, string> validateLogin(string email, string password)
        {
            var errors = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(email))
            {
                Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                bool isValid = regex.IsMatch(email);
                if (!isValid)
                {
                    errors.Add("email", "Invalid email!");
                }
            }
            else
            {
                errors.Add("email", "email is required!");
            }

            if (string.IsNullOrEmpty(password))
            {
                errors.Add("password", "Password is required!");
            }

            return errors;
        }

        private void textBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.status)
            {
                txtEmail.Text = "";
                txtPassword.Password = "";
                statusLogin.Text = "";
                this.status = false;
            }
            
        }
    }
}
