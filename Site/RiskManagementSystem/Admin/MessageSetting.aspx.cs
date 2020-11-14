using RiskManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskManagementSystem.Admin
{
    public partial class MessageSetting : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GrdOGDataSource();
            }

        }
        public void GrdOGDataSource()
        {
            var n = from m in db.Messages
                    join aa in db.Users
                    on m.UserId equals aa.UserID
                    orderby m.SendDate descending
                    select new
                    {
                        m.Id,
                        m.Subject,
                        m.Body,
                        m.SendDate,
                        aa.Username
                    };

            grdOperationGroup.DataSource = n;
            grdOperationGroup.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoDetails":
                    {
                        ViewState["Id"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvOperationGroup.SetActiveView(vwEdit);
                        DisableForm();
                        break;
                    }

            }
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



        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    ViewState["EditMode"] = "Insert";
        //    ResetForm();
        //    EnableForm();
        //    mvOperationGroup.SetActiveView(vwEdit);
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }


        protected void Button3_OnClick(object sender, EventArgs e)
        {
            mvOperationGroup.SetActiveView(vwList);
        }
    }
}