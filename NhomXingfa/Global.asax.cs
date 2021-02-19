using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace NhomXingfa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    string[] roles = authTicket.UserData.Split(new Char[] { ',' });
                    GenericPrincipal userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                    Context.User = userPrincipal;
                }
            }
            catch (CryptographicException)
            {
                FormsAuthentication.SignOut();
            }
            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var cryptoEx = error as CryptographicException;
            if (cryptoEx != null)
            {
                FederatedAuthentication.WSFederationAuthenticationModule.SignOut();
                Server.ClearError();
            }
        }
    }
}
