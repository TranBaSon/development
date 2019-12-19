﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicApp.Models
{
    class Member
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string introduction { get; set; }
        public int gender { get; set; }
        public DateTime birtthday { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int status { get; set; }
    }
}

public enum MemberGender
{
    MALE = 0,
    FEMALE = 1,
    OTHER = 2,
};


