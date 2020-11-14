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
    public partial class SupervisorSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridViewBind();
                loadCompanies();
            }
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
                        using (RiskManagementEntities db = new RiskManagementEntities())
                        {
                            var n = (from us in db.Users
                                     where us.UserID == UserID
                                     select us).FirstOrDefault();

                            lblUserDelete.Text = n.Username;
                            mvUsers.SetActiveView(vwDelete);
                        }
                            break;
                    }
            }
        }
        #region Form Data Methods
        private void GridViewBind()
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                
                var n = from us in db.Users
                        join rl in db.Users
                        on us.SupervisorUserId equals rl.UserID into g
                        from a in g.DefaultIfEmpty()

                     where us.RoleID==3

                        select new
                        {
                            UserID = us.UserID,
                            Username = us.Username,
                            Password = us.Password,
                            UserCompanyName = a.Username,                        
                            Email = us.Email,
                            Name = us.Name,
                            Family = us.Family,

                        };
                grdUsers.DataSource = n;
                grdUsers.DataBind();
            }
        }
        public void loadCompanies()
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = from a in db.Users
                        where a.RoleID == 2
                        select new
                            {
                                a.UserID,
                                a.Username
                            };
                ddlUser.Items.Clear();
                ddlUser.Items.Add(new ListItem("پیمانکار", "-1"));
                foreach (var i in n)
                    ddlUser.Items.Add(new ListItem(i.Username, i.UserID.ToString()));
            }
        }
        private void ResetForm()
        {

            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFamily.Text = string.Empty;
            ddlUser.SelectedIndex = -1;
        }
        private void Delete()
        {

            int UserID = Convert.ToInt32(ViewState["UserID"]);

            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                (from us in db.Users
                 where us.UserID == UserID
                 select us).ToList().ForEach(db.DeleteObject);
                db.SaveChanges();
            }
        }
        private void LoadForm()
        {
            int UserID = Convert.ToInt32(ViewState["UserID"]);
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = (from us in db.Users
                         where us.UserID == UserID
                         select us).FirstOrDefault();

                if (n != null)
                {
                    if(n.SupervisorUserId!=null)
                    ddlUser.SelectedValue = n.SupervisorUserId.ToString();
                    txtUsername.Text = n.Username.ToString();


                    txtPassword.Text = n.Password.ToString();
                    txtEmail.Text = n.Email.ToString();

                    txtName.Text = n.Name.ToString();
                    txtFamily.Text = n.Family.ToString();

                };

            }
        }

        private void InsertForm()
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                Users us = new Users();
                
                    us.RoleID =3;
                    us.Username = txtUsername.Text;
                    us.Password = txtPassword.Text;

                    us.Email = txtEmail.Text;

                    us.Name = txtName.Text;
                    us.Family = txtFamily.Text;
                if(ddlUser.SelectedValue!="-1")
                    us.SupervisorUserId = Convert.ToInt32(ddlUser.SelectedValue);

                
                db.Users.AddObject(us);
                db.SaveChanges();
            }
        }

        private void UpdateForm()
        {

            int UserID = Convert.ToInt32(ViewState["UserID"]);
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = (from us in db.Users
                         where us.UserID == UserID
                         select us).FirstOrDefault();
                if (ddlUser.SelectedValue != "-1")
                    n.SupervisorUserId = Convert.ToInt32(ddlUser.SelectedValue);
                else
                    n.SupervisorUserId = null;


                n.Username = txtUsername.Text;
                n.Password = txtPassword.Text;

                n.Email = txtEmail.Text;

                n.Name = txtName.Text;
                n.Family = txtFamily.Text;

                db.SaveChanges();
            }
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