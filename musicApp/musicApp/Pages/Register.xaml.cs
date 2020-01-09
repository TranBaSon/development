using musicApp.Models;
using musicApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class Register : Page
    {
        private IMemberService _service;
        private int _choosedGender;
        private string _birthday;
        public Register()
        {
            this.InitializeComponent();
            _service = new ApiMemberService();
        }

        private async void Button_Register(object sender, RoutedEventArgs e)
        {
            var member = new Member()
            {
                firstName = TxtFirstName.Text,
                lastName = TxtLastName.Text,
                password = PwdPassword.Password,
                address = TxtAddress.Text,
                phone = TxtPhone.Text,
                avatar = TxtAvatar.Text,
                gender = _choosedGender,
                email = TxtEmail.Text,
                birthday = _birthday,
            };

            var errors = member.CheckValidate(CfPassword.Password);
            if(errors.Count > 0)
            {
                ErrFirstName.Text = errors.ContainsKey("firstName") ? errors["firstName"] : "";
                ErrLastName.Text = errors.ContainsKey("lastName") ? errors["lastName"] : "";
                ErrAddress.Text = errors.ContainsKey("address") ? errors["address"] : "";
                ErrPassword.Text = errors.ContainsKey("password") ? errors["password"] : "";
                ErrEmail.Text = errors.ContainsKey("email") ? errors["email"] : "";
                ErrAvatar.Text = errors.ContainsKey("avatar") ? errors["avatar"] : "";
                ErrBirtDay.Text = errors.ContainsKey("birthday") ? errors["birthday"] : "";
                ErrPhone.Text = errors.ContainsKey("phone") ? errors["phone"] : "";
                ErrCfPassword.Text = errors.ContainsKey("confirmPassword") ? errors["confirmPassword"] : "";
                ErrGender.Text = errors.ContainsKey("gender") ? errors["gender"] : "";

            }
            else
            {
                bool status = await this._service.Register(member);
                if (status)
                {
                    Navigatior.GetCurrent().SetSelectedNavigationItem(0);
                }
            }
            

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Navigatior.GetCurrent().SetSelectedNavigationItem(2);
        }

        private void Gender_Choose(object sender, RoutedEventArgs e)
        {
            var choosedRadioButton = sender as RadioButton;
            if (choosedRadioButton == null)
            {
                return;
            }
            switch (choosedRadioButton.Tag)
            {
                case "Male":
                    _choosedGender = 1;
                    break;
                case "Female":
                    _choosedGender = 0;
                    break;
                case "Other":
                    _choosedGender = 2;
                    break;
            }
        }

        private void birthday_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date.HasValue)
            {
                _birthday = sender.Date.Value.Date.ToString("yyyy-MM-dd");
                
            }
        }
    }
}
