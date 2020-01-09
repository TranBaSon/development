using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace musicApp.Models
{
    class Song
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string singer { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string link { get; set; }
        public long memberId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int status { get; set; }

        public Dictionary<string, string> CheckValidate()
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(this.name))
            {
                errors.Add("Name", "Song name is required!");
            }

            if (string.IsNullOrEmpty(this.author))
            {
                errors.Add("Author", "Author name is required!");
            }

            if (string.IsNullOrEmpty(this.singer))
            {
                errors.Add("Singer", "Singer name is required!");
            }

            if (string.IsNullOrEmpty(this.thumbnail))
            {
                errors.Add("Thumbnail", "Thumbnail is required!");
            }
            else
            {
                Regex regex = new Regex(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)");
                bool isValid = regex.IsMatch(this.thumbnail);
                if (!isValid)
                {
                    errors.Add("Thumbnail", "Invalid thumbnail!");
                }
            }

            if (string.IsNullOrEmpty(this.link))
            {
                errors.Add("Link", "Link is required!");
            }
            else
            {
                Regex regex = new Regex(@"(http(s?):)([/|.|\w|\s|-])*\.(mp3)");
                bool isValid = regex.IsMatch(this.link);
                if (!isValid)
                {
                    errors.Add("Link", "Invalid link!");
                }
            }


            return errors;
        }
    }
   
}

