using musicApp.Models;
using musicApp.Services;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace musicApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MemberInformation : Page
    {
        private IMemberService _service;
        public MemberInformation()
        {
            _service = new ApiMemberService();
            Member memberInfo = getInfo();
            this.InitializeComponent();
          
            if (memberInfo != null)
            {
                avatar.Source = new BitmapImage(new Uri(memberInfo.avatar));
                firstName.Text = "First Name: " + memberInfo.firstName;
                lastName.Text = "Last Name: " + memberInfo.lastName;
                email.Text = "Email: " +  memberInfo.email;
                phone.Text = "Phone Number: " + memberInfo.phone;
                address.Text = "Address: " + memberInfo.address;
                birthday.Text = "Birthday: " + memberInfo.birthday;

                switch (memberInfo.gender)
                {
                    case 0: gender.Text = "Gender: Female";
                        break;
                    case 1: gender.Text = "Gender: Male";
                        break;
                    case 2: gender.Text = "Gender: Other";
                        break;
                }
            }
            
        }

        private Member getInfo()
        {
            Task<string> tokenTask = Task.Run(async () => await HandlerFileService.ReadFile("token.txt"));
            string token = tokenTask.Result;
            if (token.Length > 0)
            {
                Task<Member> member = Task.Run(async () => await _service.GetMemberInformation(token));
                Member memberResult = member.Result;
                return memberResult;
            }
            else
            {
                Navigatior.GetCurrent().SetSelectedNavigationItem(2);
            }

            return null;
        }

    }
}
