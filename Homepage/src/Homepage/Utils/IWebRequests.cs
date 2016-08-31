using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homepage.Utils
{
    public interface IWebRequests
    {
        string GetAllRegistrationsUrl();

        string PostRegistrationUrl();

        string GetLoginUrl();
    }
}
