using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homepage.ViewModels;
using Homepage.Client;

namespace Homepage.Controllers.Web
{
    public class AppController : Controller
    {
        private IServices services;

        public AppController()
        {
            services = new RegistrationManager();
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
        public IActionResult Index(Login login)
        {
            //Additional check can be made here, eg.: model.Email.Contains("aol.com")
            if (ModelState.IsValid)
            {
                var result = services.SendLogin(login.Email, login.Password);

                //Required the same model (@model Homepage.ViewModels.Login) from the site needs to be returned.
                if (!result.IsFaulted)
                {
                    var newLogin = new Login()
                    {
                        Email = login.Email,
                        Password = login.Password,
                    };
                    return View(newLogin);
                }   
            }
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
                var result = services.SendRegistration(register.Email, register.Password, register.ConfirmPassword);
                
                if (!result.IsFaulted)
                {
                    //Required the same model (@model Homepage.ViewModels.Register) from the site needs to be returned.
                    var newRegistration = new Register()
                    {
                        Email = register.Email,
                        Password = register.Password,
                        ConfirmPassword = register.ConfirmPassword
                    };
                    return View(newRegistration);
                }
                
            }
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
            var result = services.GetRegistrationsList();
            return View(result);
        }
    }
}
