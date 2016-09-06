using Homepage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Homepage.Client
{
    public interface IServices
    {
        Task<HttpStatusCode> GetRegistrationsList();

        Task<HttpStatusCode> SendRegistration(string email, string password, string confirmPassword);

        Task<HttpStatusCode> SendLogin(string userName, string password);

    }
}
