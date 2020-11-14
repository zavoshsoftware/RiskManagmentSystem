using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using System.Web.Security;
using RiskManagementSystem.Classes;

namespace KalaSanat.Contorols
{
    public partial class UCLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void cvCheckLogin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var n = (from us in DataContext.Context.Users
                     where us.Username == txtUsername.Text && us.Password == txtPassword.Text
                     select us
                     ).SingleOrDefault();
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


                int UserID = Convert.ToInt32(ViewState["UserID"].ToString());
                var n = (from u in DataContext.Context.Users
                         where u.UserID == UserID
                         select u).FirstOrDefault();
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
}