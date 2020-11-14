using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class UserEdit : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["Id"]);


                    var n = (from us in db.Users
                        where us.UserID == id
                             select us).FirstOrDefault();

                    if (n != null)
                    {
                        ddlRoles.SelectedValue = n.RoleID.ToString();
                        txtUsername.Text = n.Username.ToString();
                      
                        txtEmail.Text = n.Email.ToString();
                        txtName.Text = n.Name.ToString();
                        txtFamily.Text = n.Family.ToString();
                        if (n.RoleID == 2)
                        {
                            ddlSupervisor.SelectedValue = n.SupervisorUserId.ToString();
                            pnlSupervisors.Visible = true;
                        }
                        else
                            pnlSupervisors.Visible = false;
                    }
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                var n = (from us in db.Users
                    where us.UserID == id
                         select us).FirstOrDefault();

                n.RoleID = Convert.ToInt32(ddlRoles.SelectedValue);
                n.Username = txtUsername.Text;
                n.Email = txtEmail.Text;
                n.Name = txtName.Text;
                n.Family = txtFamily.Text;
                n.SupervisorUserId = FindSupervisorUserId();
                db.SaveChanges();


                Response.Redirect("~/Admin/UserSetting.aspx");

            }
        }

        public int? FindSupervisorUserId()
        {
            if (ddlRoles.SelectedValue == "2")
                return Convert.ToInt32(ddlSupervisor.SelectedValue);
            else
                return null;
        }
        protected void ddlRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoles.SelectedValue == "2")
            {
                List<Users> users = db.Users.Where(current => current.RoleID == 3).ToList();

                ddlSupervisor.DataSource = users;
                ddlSupervisor.DataValueField = "UserID";
                ddlSupervisor.DataTextField = "Username";
                ddlSupervisor.DataBind();

                pnlSupervisors.Visible = true;
            }
            else
                pnlSupervisors.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/UserSetting.aspx");
        }
    }
}