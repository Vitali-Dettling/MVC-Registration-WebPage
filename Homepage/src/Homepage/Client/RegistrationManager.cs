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

        public async Task<HttpStatusCode> GetRegistrationsList()
        {
            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync(requests.GetAllRegistrationsUrl());

                if (response.StatusCode != HttpStatusCode.BadRequest)
                {
                    return HttpStatusCode.OK;
                }
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> SendRegistration(string email, string newPassword, string confirmPassword)
        {
            using (var httpClient = new HttpClient())
            {
                var check = Newtonsoft.Json.JsonConvert.SerializeObject(
                    new { Email = email, Password = newPassword, ConfirmPassword = confirmPassword }
                    );
                HttpContent contentPost = new StringContent(check, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requests.PostRegistrationUrl(), contentPost);
                
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return HttpStatusCode.Created;
                }
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> SendLogin(string email, string password)
        {
            using (var httpClient = new HttpClient())
            {
                var check = Newtonsoft.Json.JsonConvert.SerializeObject(
                   new { Email = email, Password = password }
                   );
                HttpContent contentPost = new StringContent(check, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requests.GetLoginUrl(), contentPost);
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
