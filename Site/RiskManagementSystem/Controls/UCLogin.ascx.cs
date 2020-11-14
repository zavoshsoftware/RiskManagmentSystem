using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using System.Web.Security;
using RiskManagementSystem;
using RiskManagementSystem.Model;

namespace KalaSanat.Contorols
{
    public partial class UCLogin : System.Web.UI.UserControl
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void cvCheckLogin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string password = Helper.CreateHashPassword(txtPassword.Text);

            var n = (from us in db.Users
                     where us.Username == txtUsername.Text && us.Password == password
                     select us
                     ).FirstOrDefault();
            args.IsValid = n != null;
            if (args.IsValid)
            {
                ViewState["UserID"] = n.UserID;
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
              
                    FormsAuthentication.SetAuthCookie(ViewState["UserID"].ToString(), chkRemember.Checked);

                    SubmitInLoginLogs(Convert.ToInt32(ViewState["UserID"]));

                    int UserID = Convert.ToInt32(ViewState["UserID"].ToString());

                    Users n = (from u in db.Users
                               where u.UserID == UserID
                               select u).FirstOrDefault();

                    if (n != null)
                    {
                        if (n.RoleID == 1)
                        {
                            Response.Redirect("~/Admin/Default.aspx");
                        }
                        else if (n.RoleID == 2)
                        {
                            Response.Redirect("~/Companies/Default.aspx");
                        }
                        else if (n.RoleID == 3)
                        {
                            Response.Redirect("~/sup/Default.aspx");
                        }
                    }
                
            }
        }

        public void SubmitInLoginLogs(int userId)
        {
            Users user = db.Users.FirstOrDefault(current => current.UserID == userId);

            if (user != null)
            {
                LoginLogs loginLogs=new LoginLogs()
                {
                    LoginDate = DateTime.Now,
                    UserId = userId,
                };

                db.LoginLogs.AddObject(loginLogs);
                db.SaveChanges();
            }
        }

        protected void cvrecaptcha_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool isHuman = ExampleCaptcha.Validate(CaptchaCode.Text);
            CaptchaCode.Text = null; // clear previous user input

            if (isHuman)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }
}