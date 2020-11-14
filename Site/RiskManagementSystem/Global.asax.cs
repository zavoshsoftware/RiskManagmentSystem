using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using GSD.Globalization;
using System.Threading;
using System.Web.Configuration;

namespace RiskManagementSystem
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture();
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Url.OriginalString.ToLower().Contains("changepassword"))
            {
                if (Request.IsAuthenticated)
                {
                    Model.RiskManagementEntities db = new Model.RiskManagementEntities();

                    int id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                    Model.Users user = db.Users.FirstOrDefault(current => current.UserID == id);

                    if (user?.IsChangedPass == false)
                        Response.Redirect("~/ChangePassword.aspx");
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}