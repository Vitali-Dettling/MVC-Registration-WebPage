using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Homepage.ViewModels;
using System.Text;
using System.Net;
using Homepage.Controllers.Web;
using Homepage.Utils;

namespace Homepage.Client
{
    public class RegistrationManager : Controller, IServices
    {
        private IWebRequests requests;

        public RegistrationManager(IWebRequests webRequests)
        {
            requests = webRequests;
        }


        public async Task<HttpResponseMessage> SendRegistration(string email, string newPassword, string confirmPassword)
        {
            using (var httpClient = new HttpClient())
            {
                var check = Newtonsoft.Json.JsonConvert.SerializeObject(
                    new { Email = email, Password = newPassword, ConfirmPassword = confirmPassword }
                    );
                HttpContent contentPost = new StringContent(check, Encoding.UTF8, "application/json");

                return await httpClient.PostAsync(requests.PostRegistrationUrl(), contentPost);
            }
        }

        public async Task<HttpResponseMessage> SendLogin(string email, string password)
        {
            using (var httpClient = new HttpClient())
            {
                var check = Newtonsoft.Json.JsonConvert.SerializeObject(
                   new { Email = email, Password = password }
                   );
                HttpContent contentPost = new StringContent(check, Encoding.UTF8, "application/json");

                return await httpClient.PostAsync(requests.GetLoginUrl(), contentPost);
            }
        }
    }
}
