using ManageApp.Services;
using System;
using System.Collections.Generic;
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

namespace ManageApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListStudent : Page
    {
        private IStudentService _service;
        public ListStudent()
        {
            this.InitializeComponent();
            this._service = new InmemoryStudentService();
            foreach(var st in _service.GetList())
            {
                string item = string.Format("{0} - {1} - {2} - {3} - {4} - {5}",
                    st.RollNumber, st.FullName, st.Email, st.Phone, st.Status, st.Birthday);
                lsST.Items.Add(item);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StudentForm));
        }
    }
}
