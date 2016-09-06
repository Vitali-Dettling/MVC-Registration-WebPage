﻿using Homepage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homepage.Client
{
    public interface IServices
    {
        Task<HttpResponseMessage> SendRegistration(string email, string password, string confirmPassword);

        Task<HttpResponseMessage> SendLogin(string userName, string password);

    }
}
