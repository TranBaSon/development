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
        Task<string> Login(string email, string password);
        Task<bool> Register(Member member);
        Task<Member> GetMemberInformation(string token);
    }
}
