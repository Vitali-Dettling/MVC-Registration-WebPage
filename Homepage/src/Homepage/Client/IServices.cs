using Homepage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homepage.Client
{
    public interface IServices
    {
        Task<IActionResult> GetRegistrationsList();

        Task<IActionResult> SendRegistration(string email, string password, string confirmPassword);

        Task<IActionResult> SendLogin(string userName, string password);

    }
}
