using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homepage.ViewModels;
using Homepage.Client;
using System.Net;
using System.Net.Http;

namespace Homepage.Controllers.Web
{
    public class AppController : Controller
    {
        private IServices services;

        public AppController(IServices registrationManager)
        {
            services = registrationManager;
        }

        /// <summary>
        /// Show the page start page (Index) if called
        /// </summary>
        /// <returns>
        /// View of the naked page
        /// </returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout(Login login)
        {
            if (login.Email != null &&
                login.Password != null)
            {
                return View(login);
            }
        
            return Redirect("Index"); 
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            //Additional check can be made here, eg.: model.Email.Contains("aol.com")
            if (ModelState.IsValid)
            {
                Task<HttpStatusCode> result = services.SendLogin(login.Email, login.Password);

                if (result.Result == HttpStatusCode.OK)
                {
                    return RedirectToAction("Logout", login);
                }  
            }
            //TODO Commend BadRequest("Wrong Credentials! Please try again");
            return View(login);
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
                
                if (result == HttpStatusCode.Created)
                {
                    //TODO  Created($"api/registration/{email}", email);

                    //Required the same model (@model Homepage.ViewModels.Register) from the site needs to be returned.
                    //TODO ????
                    //var newRegistration = new Register()
                    //{
                    //    Email = register.Email,
                    //    Password = register.Password,
                    //    ConfirmPassword = register.ConfirmPassword
                    //};
                    return View(register);
                }
                
            }
            //TODO BadRequest("Registration did not work! (Internal server error)");
            return View();
        }

        /// <summary>
        /// Show the page about if called
        /// </summary>
        /// <returns>
        /// View of the naked page
        /// </returns>
        [HttpGet]
        public IActionResult About()
        { 
            return View();
        }

        [HttpGet("registrations")]
        public IActionResult Get()
        {
            //TODO 
            var result = services.GetRegistrationsList();
            //TODO  BadRequest("Internal server error");
            return View(result);
        }
    }
}
