using musicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicApp.Services
{
    interface IMemberService
    {
        bool Login(string email, string password);
        bool Register(Member member);
    }
}
