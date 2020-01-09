using musicApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace musicApp.Services
{
    class ApiMemberService : IMemberService
    {
        private static string REGISTER_API_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        private static string LOGIN_API_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        private static string GETINFO_API_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        private static string CONTENT_TYPE = "application/json";


        public async Task<bool> Register(Member member)
        {
            var memberJson = JsonConvert.SerializeObject(member);
            HttpContent contentToSend = new StringContent(memberJson, Encoding.UTF8, CONTENT_TYPE);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(REGISTER_API_URL, contentToSend);
            var stringContent = await response.Content.ReadAsStringAsync();
            string status = JObject.Parse(stringContent)["status"].ToString();
            if (status.Equals("1"))
            {
                return true;
            }
            return false;
        }

       

        public async Task<string> Login(string email, string password)
        {
            JObject content = new JObject();
            content["email"] = email;
            content["password"] = password;
            
            HttpContent loginContent = new StringContent(
                content.ToString(),
                Encoding.UTF8,
                CONTENT_TYPE);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(LOGIN_API_URL, loginContent);
            var stringContent = await response.Content.ReadAsStringAsync();
            string status = JObject.Parse(stringContent)["status"].ToString();
            if (!status.Equals("1"))
            {
                return "error";
            }
            var token = JObject.Parse(stringContent)["token"];
            return token.ToString();
        }

        public async Task<Member> GetMemberInformation(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var response = await httpClient.GetAsync(GETINFO_API_URL);

            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Member>(stringContent);

        }
    }
}
