using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsert_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        string userId = HttpContext.Current.User.Identity.Name;

                        int id = Convert.ToInt32(userId);

                        using (RiskManagementEntities db = new RiskManagementEntities())
                        {
                            string password = Helper.CreateHashPassword(txtPassword.Text);

                            Users user = db.Users
                                .FirstOrDefault(current => current.UserID == id);

                            user.Password = password;
                            user.IsChangedPass = true;

                            db.SaveChanges();

                            RedirectToPage(user.RoleID);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        protected void cvOldPassword_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string userId = HttpContext.Current.User.Identity.Name;

                int id = Convert.ToInt32(userId);

                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    string password = Helper.CreateHashPassword(txtoldPassword.Text);

                    Users user = db.Users
                        .FirstOrDefault(current => current.UserID == id && current.Password == password);

                    args.IsValid = user != null;
                }
            }
        }

        public void RedirectToPage(int roleId)
        {
            if (roleId ==1)
                Response.Redirect("~/admin/default.aspx");
            if (roleId == 2)
                Response.Redirect("~/Companies/default.aspx");
            if (roleId == 3)
                Response.Redirect("~/sup/default.aspx");
        }
    }
}