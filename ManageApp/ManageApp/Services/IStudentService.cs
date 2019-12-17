using ManageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageApp.Services
{
    interface IStudentService
    {
        Student Create(Student student);
        List<Student> GetList();
        Student GetDetail(string rollNumber);
        Student Update(Student student);
        bool Delete(string rollNumber);
    }
}
