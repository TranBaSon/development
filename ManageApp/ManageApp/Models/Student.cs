using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageApp.Models
{
    class Student
    {
        public string RollNumber { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public StudentStatus Status { get; set; }
        public StudentGender Gender { get; set; }
    }
}

public enum StudentStatus
{
    ACTIVE = 1,
    DEACTIVE = 0,
};

public enum StudentGender
{
    MALE = 1,
    FEMALE = 2,
    OTHER = 0,
};