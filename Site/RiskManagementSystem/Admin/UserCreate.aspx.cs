using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class UserCreate : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                ResetForm();
        }
        private void ResetForm()
        {
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFamily.Text = string.Empty;

            ddlRoles.SelectedIndex = -1;
            ddlSupervisor.SelectedIndex = -1;
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Users us = new Users()
            {
                RoleID = Convert.ToInt32(ddlRoles.SelectedValue),
                Username = txtUsername.Text,
                Password = Helper.CreateHashPassword(txtPassword.Text),
                Email = txtEmail.Text,
                Name = txtName.Text,
                Family = txtFamily.Text,
                IsChangedPass = false,
                SupervisorUserId = FindSupervisorUserId(),
            };
            db.Users.AddObject(us);
            db.SaveChanges();

            Response.Redirect("~/Admin/UserSetting.aspx");

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