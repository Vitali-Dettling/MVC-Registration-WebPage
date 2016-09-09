using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Homepage.ViewModels;
using System.Net;
using Homepage.Client;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace Homepage.Controllers
{
    public class AuthController : Controller
    {
        private IServices services;

        public AuthController(IServices registrationManagerController)
        {
            services = registrationManagerController;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(Login login)
        {  
            //Additional check can be made here, eg.: model.Email.Contains("aol.com")
            if (ModelState.IsValid)
            {
                var result = services.SendLogin(login.Email, login.Password).Result;

                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Redirect("Logout");
                    case HttpStatusCode.NotFound:
                        //TODO Feedback how to react if the credentials not match.
                        return Redirect("Register");
                    default:
                        //TODO Feedback registration did not work.
                        return View();
                }
            }
            //TODO Feedback BadRequest("Wrong Credentials! Please try again");
            return View(login);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        /// <summary>
        /// Show the page register if called
        /// </summary>
        /// <returns>
        /// View of the naked page
        /// </returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            //Additional check can be made here, eg.: model.Email.Contains("aol.com")
            if (ModelState.IsValid)
            {
                var result = services.SendRegistration(register.Email, register.Password, register.ConfirmPassword).Result;

                switch (result.StatusCode)
                {
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Redirect:
                        //TODO Feedback that user is already locked in.
                        return RedirectToAction("Index", "App");
                    case HttpStatusCode.Forbidden:
                        //TODO Feedback for forbidden
                        return View();
                }
            }
            //TODO BadRequest("Registration did not work! (Internal server error)");
            //TODO Feedback return HttpStatusCode.BadRequest;
            return View("Bad Request: 400");
        }
    }
}
