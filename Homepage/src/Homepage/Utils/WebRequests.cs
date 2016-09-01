using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Homepage.Utils;
using System.ComponentModel;
using System.Reflection;

namespace Homepage.Controllers.Web
{
    public class WebRequests : IWebRequests
    {
        private IHostingEnvironment env;

        public WebRequests(IHostingEnvironment environment)
        {
            env = environment;
        }
        
        public string GetAllRegistrationsUrl()
        {
            return URL(GetUrlQuery(UrlQuery.registrations));
        }

        public string PostRegistrationUrl()
        {
            return URL(GetUrlQuery(UrlQuery.registration));
        }

        public string GetLoginUrl()
        {
            return URL(GetUrlQuery(UrlQuery.login));
        }

        private string URL(string path)
        {
            var url = new UriBuilder();

            if (env.IsDevelopment())
            {
                //TODO Automate URL builder via VS 2015 settings.
                url.Host = "localhost";
                url.Port = 60647;
            }
            else
            {
                url.Host = "registrationmanager-01.azurewebsites.net";
            }

            url.Path = path;
            return url.ToString();
        }

        private enum UrlQuery
        {
            [Description("/api/registrations")]
            registrations,
            [Description("/api/registration")]
            registration,
            [Description("/api/login")]
            login,
        }

        private static string GetUrlQuery(Enum UrlQuery)
        {
            FieldInfo fi = UrlQuery.GetType().GetField(UrlQuery.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return UrlQuery.ToString();
        }
    }
}
