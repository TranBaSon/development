using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace musicApp.Models
{
    class Member
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int gender { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Dictionary<string, string> CheckValidate(string confirmPassword)
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(this.firstName))
            {
                errors.Add("firstName", "First name is required!");
            }
            if (string.IsNullOrEmpty(this.address))
            {
                errors.Add("address", "Address is required!");
            }
            if (string.IsNullOrEmpty(this.lastName))
            {
                errors.Add("lastName", "Last name is required!");
            }
            if (string.IsNullOrEmpty(this.password))
            {
                errors.Add("password", "Password is required!");
            }
            if (string.IsNullOrEmpty(confirmPassword))
            {
                errors.Add("confirmPassword", "Confirm password is required!");
            }
            else
            {
                if (confirmPassword.CompareTo(this.password) != 0)
                {
                    errors.Add("confirmPassword", "Password don't match!");
                }
            }
            
            if (!string.IsNullOrEmpty(this.email))
            {
                Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                bool isValid = regex.IsMatch(this.email);
                if (!isValid)
                {
                    errors.Add("email", "Invalid email!");
                }
            }
            else
            {
                errors.Add("email", "email is required!");
            }
            if (!string.IsNullOrEmpty(this.phone))
            {
                Regex regex = new Regex(@"((09|03|07|08|05)+([0-9]{8})\b)");
                bool isValid = regex.IsMatch(this.phone);
                if (!isValid)
                {
                    errors.Add("phone", "Invalid phone!");
                }
            }
            else
            {
                errors.Add("phone", "phone is required!");
            }
            if (!string.IsNullOrEmpty(this.avatar))
            {
                Regex regex = new Regex(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)");
                bool isValid = regex.IsMatch(this.avatar);
                if (!isValid)
                {
                    errors.Add("avatar", "Invalid avatar!");
                }
            }
            else
            {
                errors.Add("avatar", "avatar is required!");
            }
            if (!string.IsNullOrEmpty(this.birthday))
            {
                Regex regex = new Regex(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))");
                bool isValid = regex.IsMatch(this.birthday);
                if (!isValid)
                {
                    errors.Add("birthday", "Invalid birthday!");
                }
            }
            else
            {
                errors.Add("birthday", "Birthday is required!");
            }
            

            return errors;
        }

        
    }
}




