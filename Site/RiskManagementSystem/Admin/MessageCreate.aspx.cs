using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class MessageCreate : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                InsertForm();
              
                Response.Redirect("~/Admin/MessageSetting.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/MessageSetting.aspx");
        }

        #region Form Data Methods
        private void ResetForm()
        {
            txtBody.Text = string.Empty;
            txtTitle.Text = string.Empty;
            ddlUser.SelectedIndex = -1;
        }

        private void DisableForm()
        {
            txtBody.Enabled = false;
            txtTitle.Enabled = false;
            btnSave.Enabled = false;
            ddlUser.Enabled = false;
            cbAllSup.Enabled = false;
            cbAllCompany.Enabled = false;
            cbAllUser.Enabled = false;

        }

        private void EnableForm()
        {
            txtBody.Enabled = true;
            txtTitle.Enabled = true;
            btnSave.Enabled = true;
            ddlUser.Enabled = true;
            cbAllSup.Enabled = true;
            cbAllCompany.Enabled = true;
            cbAllUser.Enabled = true;

        }
        private void LoadForm()
        {
            int Id = Convert.ToInt32(ViewState["Id"]);

            var n = (from p in db.Messages
                     where p.Id == Id
                     select p).FirstOrDefault();

            if (n != null)
            {
                txtTitle.Text = n.Subject;
                txtBody.Text = n.Body;
                ddlUser.SelectedValue = n.UserId.ToString();
            };

        }

        private void InsertForm()
        {
            if (cbAllUser.Checked == true)
            {
                var users = (from u in db.Users select u).ToList();
                foreach (var user in users)
                {
                    Messages p = new Messages()
                    {
                        Subject = txtTitle.Text,
                        Body = txtBody.Text,
                        SendDate = DateTime.Now,
                        IsSendByAdmin = true,
                        IsRead = false,
                        UserId = user.UserID
                        //UserId = Convert.ToInt32(ddlUser.SelectedValue)
                    };
                    db.Messages.AddObject(p);
                }
            }
            else if (cbAllCompany.Checked == true)
            {
                var users = (from u in db.Users where u.RoleID == 2 select u).ToList();
                foreach (var user in users)
                {
                    Messages p = new Messages()
                    {
                        Subject = txtTitle.Text,
                        Body = txtBody.Text,
                        SendDate = DateTime.Now,
                        IsSendByAdmin = true,
                        IsRead = false,
                        UserId = user.UserID
                        //UserId = Convert.ToInt32(ddlUser.SelectedValue)
                    };
                    db.Messages.AddObject(p);
                }
            }
            else if (cbAllSup.Checked == true)
            {
                var users = (from u in db.Users where u.RoleID == 3 select u).ToList();
                foreach (var user in users)
                {
                    Messages p = new Messages()
                    {
                        Subject = txtTitle.Text,
                        Body = txtBody.Text,
                        SendDate = DateTime.Now,
                        IsSendByAdmin = true,
                        IsRead = false,
                        UserId = user.UserID
                        //UserId = Convert.ToInt32(ddlUser.SelectedValue)
                    };
                    db.Messages.AddObject(p);
                }
            }
            else
            {
                Messages p = new Messages()
                {
                    Subject = txtTitle.Text,
                    Body = txtBody.Text,
                    SendDate = DateTime.Now,
                    IsSendByAdmin = true,
                    IsRead = false,
                    UserId = Convert.ToInt32(ddlUser.SelectedValue)
                };
                db.Messages.AddObject(p);
            }

            db.SaveChanges();
        }


        #endregion
    }
}