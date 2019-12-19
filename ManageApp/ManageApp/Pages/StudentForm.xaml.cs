using ManageApp.Models;
using ManageApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public sealed partial class StudentForm : Page
    {
        private IStudentService _service;
        private StudentGender _choosedGender;
        private StudentStatus _choosedStatus;
        private DateTime _birthday;

        public StudentForm()
        {
            this.InitializeComponent();
            this._service = new InmemoryStudentService();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ListStudent));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student st = new Student();
            st.RollNumber = rollNumber.Text;
            st.FullName = fullName.Text;
            st.Email = email.Text;
            st.Phone = phone.Text;
            st.Gender = _choosedGender;
            st.Status = _choosedStatus;
            st.Birthday = _birthday;
            st.CreatedAt = DateTime.Now;
            Debug.WriteLine(JsonConvert.SerializeObject(st));
        
            _service.Create(st);
            HttpClient client = new HttpClient();
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
                    _choosedGender = StudentGender.MALE;
                    break;
                case "Female":
                    _choosedGender = StudentGender.FEMALE;
                    break;
                case "Other":
                    _choosedGender = StudentGender.OTHER;
                    break;
            }
        }

        private void Status_Choose(object sender, RoutedEventArgs e)
        {
            var choosedRadioButton = sender as RadioButton;
            if (choosedRadioButton == null)
            {
                return;
            }
            switch (choosedRadioButton.Tag)
            {
                case "Active":
                    _choosedStatus = StudentStatus.ACTIVE;
                    break;
                case "Deactive":
                    _choosedStatus = StudentStatus.DEACTIVE;
                    break;
            }
        }

        private void Birthday_OnDateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date.HasValue)
            {
                _birthday = sender.Date.Value.Date;
            }
        }
    }
}
