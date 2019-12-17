using ManageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageApp.Services
{
    class InmemoryStudentService : IStudentService
    {
        private static List<Student> _students = new List<Student>();
        public Student Create(Student student)
        {
            _students.Add(student);
            return student;
        }

        public bool Delete(string rollNumber)
        {
            foreach(var st in _students)
            {
                if (st.RollNumber.Equals(rollNumber))
                {
                    _students.Remove(st);
                    return true;
                }
            }
            return false;
        }

        public Student GetDetail(string rollNumber)
        {
            foreach (var st in _students)
            {
                if (st.RollNumber.Equals(rollNumber))
                {
                    return st;
                }
            }
            return null;
        }

        public List<Student> GetList()
        {
            return _students;
        }

        public Student Update(Student student)
        {
            for(int i = 0; i < _students.Count; i++)
            {
                if (_students[i].RollNumber.Equals(student.RollNumber))
                {
                    _students[i] = student;
                    return _students[i];
                }
            }
            return null;
        }
    }
}
