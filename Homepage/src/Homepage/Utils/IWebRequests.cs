using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homepage.Utils
{
    public interface IWebRequests
    {
        string PostRegistrationUrl();

        string GetLoginUrl();

        string GetIsAuthenticatedUrl();
    }
}
