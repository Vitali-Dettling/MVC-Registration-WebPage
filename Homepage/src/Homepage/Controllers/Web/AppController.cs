using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homepage.ViewModels;
using Homepage.Client;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace Homepage.Controllers.Web
{
    public class AppController : Controller
    {
        private IServices services;

        public AppController(IServices registrationManager)
        {
            services = registrationManager;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            //Currently it only redirects to the login page. It might need to be changed in the future. 
            return RedirectToAction("Login", "Auth");
        }
        
        //TODO Delete must be a better solution, because the same method is implemented in the AuthController class.
        [HttpGet]
        public IActionResult Register()
        {
            //Currently it only redirects to the login page. It might need to be changed in the future. 
            return RedirectToAction("Register", "Auth");
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
    }
}
