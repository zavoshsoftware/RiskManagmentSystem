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
        private RiskManagementEntities db = new RiskManagementEntities();

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
                    pnlPassword.Visible = false;
                    mvUsers.SetActiveView(vwEdit);

                    break;
                }

                case "DoDelete":
                {
                    ViewState["UserID"] = Convert.ToInt32(e.CommandArgument);
                    int UserID = Convert.ToInt32(ViewState["UserID"]);

                    ViewState["EditMode"] = "Delete";
                    var n = (from us in db.Users
                        where us.UserID == UserID
                        select us).FirstOrDefault();

                    lblUserDelete.Text = n.Username;
                    mvUsers.SetActiveView(vwDelete);
                    break;
                }

                case "DoReset":
                {
                    ViewState["UserID"] = Convert.ToInt32(e.CommandArgument);
                    int userId = Convert.ToInt32(ViewState["UserID"]);

                    Users user = db.Users.FirstOrDefault(current => current.UserID == userId);

                    if (user != null)
                    {
                        user.Password = Helper.CreateHashPassword("111111");
                        user.IsChangedPass = false;
                        db.SaveChanges();
                        mvUsers.SetActiveView(vwResetPassword);
                    }
                    break;
                }
            }
        }

        #region Form Data Methods

        private void GridViewBind()
        {

            var n = from us in db.Users
                join rl in db.Roles
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
            ddlSupervisor.SelectedIndex = -1;
        }

        private void Delete()
        {

            int UserID = Convert.ToInt32(ViewState["UserID"]);


            (from us in db.Users
                where us.UserID == UserID
                select us).ToList().ForEach(db.DeleteObject);
            db.SaveChanges();

        }

        private void LoadForm()
        {
            int UserID = Convert.ToInt32(ViewState["UserID"]);

            var n = (from us in db.Users
                where us.UserID == UserID
                select us).FirstOrDefault();

            if (n != null)
            {
                ddlRoles.SelectedValue = n.RoleID.ToString();
                txtUsername.Text = n.Username.ToString();
                txtPassword.Text = n.Password.ToString();
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
            ;
        }

        private void InsertForm()
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

        }

        public int? FindSupervisorUserId()
        {
            if (ddlRoles.SelectedValue == "2")
                return Convert.ToInt32(ddlSupervisor.SelectedValue);
            else
                return null;
        }

        private void UpdateForm()
        {
            int UserID = Convert.ToInt32(ViewState["UserID"]);
            var n = (from us in db.Users
                where us.UserID == UserID
                select us).FirstOrDefault();

            n.RoleID = Convert.ToInt32(ddlRoles.SelectedValue);
            n.Username = txtUsername.Text;
            n.Email = txtEmail.Text;
            n.Name = txtName.Text;
            n.Family = txtFamily.Text;
            n.SupervisorUserId = FindSupervisorUserId();
            db.SaveChanges();
        }

        #endregion

        #region Buttons

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
        //protected void btnUsers_Click(object sender, EventArgs e)
        //{
        //    //ViewState["EditMode"] = "Insert";
        //    //ResetForm();
        //    mvUsers.SetActiveView(vwEdit);

        //}
        protected void btnReturnToList_OnClick(object sender, EventArgs e)
        {
            mvUsers.SetActiveView(vwList);
        }

        #endregion

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string userName = txtSearch.Text;

            var n = from us in db.Users
                join rl in db.Roles
                    on us.RoleID equals rl.RoleID
                where us.Username.Contains(userName)
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
            mvUsers.SetActiveView(vwList);
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            GridViewBind();
        }

        protected void btnAddUser_OnClick(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvUsers.SetActiveView(vwEdit);
        }
    }
}