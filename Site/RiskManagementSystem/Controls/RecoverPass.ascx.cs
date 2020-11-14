using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RiskManagementSystem.Classes;
using System.Web.Security;
using RiskManagementSystem.Model;

namespace AzAmooz
{
    public partial class RecoverPass : System.Web.UI.UserControl
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            var n = (from u in db.Users
                     where u.UserID==UserID
                     select u).FirstOrDefault();
            lblUsername.Text = n.Username;

          
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var n = (from u in db.Users
                         where u.UserID == UserID
                         select u).FirstOrDefault();
                n.Password = txtPassword.Text;
                db.SaveChanges();

                lblSucces.Text = "کلمه عبور با موفقیت تغییر یافت.";
            }
        }

        protected void cvOldPas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            var n = (from u in db.Users
                     where u.UserID == UserID
                     select u).FirstOrDefault();

            args.IsValid = n.Password == txtOldPass.Text;

        }
      
    }
}