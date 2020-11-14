using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class UserSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewBind();
        }

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["UserID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvUsers.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["UserID"] = Convert.ToInt32(e.CommandArgument);
                        int UserID = Convert.ToInt32(ViewState["UserID"]);

                        ViewState["EditMode"] = "Delete";
                        var n = (from us in DataContext.Context.Users
                                 where us.UserID == UserID
                                 select us).SingleOrDefault();

                        lblUserDelete.Text = n.Username;
                        mvUsers.SetActiveView(vwDelete);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void GridViewBind()
        {

            var n = from us in DataContext.Context.Users
                    join rl in DataContext.Context.Roles
                    on us.RoleID equals rl.RoleID
                    select new
                    {
                        UserID = us.UserID,
                        Username = us.Username,
                        Password = us.Password,

                        RoleTitle = rl.RoleTitle,
                        Email = us.Email,
                        Name = us.Name,
                        Family = us.Family,

                    };
            grdUsers.DataSource = n;
            grdUsers.DataBind();
        }
        private void ResetForm()
        {

            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFamily.Text = string.Empty;

            ddlRoles.SelectedIndex = -1;
        }
        private void Delete()
        {

            int UserID = Convert.ToInt32(ViewState["UserID"]);


            (from us in DataContext.Context.Users
             where us.UserID == UserID
             select us).ToList().ForEach(DataContext.Context.DeleteObject);
            DataContext.Context.SaveChanges();

        }
        private void LoadForm()
        {
            int UserID = Convert.ToInt32(ViewState["UserID"]);

            var n = (from us in DataContext.Context.Users
                     where us.UserID == UserID
                     select us).SingleOrDefault();

            if (n != null)
            {
                ddlRoles.SelectedValue = n.RoleID.ToString();
                txtUsername.Text = n.Username.ToString();


                txtPassword.Text = n.Password.ToString();
                txtEmail.Text = n.Email.ToString();

                txtName.Text = n.Name.ToString();
                txtFamily.Text = n.Family.ToString();

            };

        }

        private void InsertForm()
        {

            Users us = new Users()
            {
                RoleID = Convert.ToInt32(ddlRoles.SelectedValue),
                Username = txtUsername.Text,
                Password = txtPassword.Text,

                Email = txtEmail.Text,

                Name = txtName.Text,
                Family = txtFamily.Text


            };
            DataContext.Context.Users.AddObject(us);
            DataContext.Context.SaveChanges();

        }

        private void UpdateForm()
        {

            int UserID = Convert.ToInt32(ViewState["UserID"]);
            var n = (from us in DataContext.Context.Users
                     where us.UserID == UserID
                     select us).SingleOrDefault();
            n.RoleID = Convert.ToInt32(ddlRoles.SelectedValue);
            n.Username = txtUsername.Text;
            n.Password = txtPassword.Text;

            n.Email = txtEmail.Text;

            n.Name = txtName.Text;
            n.Family = txtFamily.Text;

            DataContext.Context.SaveChanges();

        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["EditMode"].ToString().Equals("Edit"))
                {
                    UpdateForm();
                }
                else
                {
                    InsertForm();
                }
                GridViewBind();
                mvUsers.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvUsers.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GridViewBind();

            mvUsers.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvUsers.SetActiveView(vwList);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }

        protected void btnUsers_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvUsers.SetActiveView(vwEdit);

        }
    }
}