using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.SUP
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
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            var n = from m in db.Messages
                    join aa in db.Users
                    on m.UserId equals aa.UserID
                    orderby m.SendDate descending
                    where m.UserId == userId
                    select new
                    {
                        m.Id,
                        m.Subject,
                        m.Body,
                        m.SendDate,
                        aa.Username
                    };

            grdMessage.DataSource = n;
            grdMessage.DataBind();
            mvMessages.SetActiveView(vwList);
        }
        protected void grdMessage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoDetails":
                    {
                        ViewState["Id"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvMessages.SetActiveView(vwEdit);
                        
                        break;
                    }

            }
        }
        #region Form Data Methods



        private void LoadForm()
        {
            int Id = Convert.ToInt32(ViewState["Id"]);

            var n = (from p in db.Messages
                     where p.Id == Id
                     select p).FirstOrDefault();

            if (n != null)
            {
                lblTitle.Text = n.Subject;
                lblBody.Text = n.Body;

                n.IsRead = true;
                db.SaveChanges();
            };

        }


        #endregion
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GrdOGDataSource();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }

        protected void grdMessage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblUnRead = (Label)e.Row.FindControl("lblUnRead");
                HiddenField hfMessageId = (HiddenField)e.Row.FindControl("hfMessageId");

                int messageId = int.Parse(hfMessageId.Value.ToString());
                var message = (from m in db.Messages where m.Id == messageId select m).FirstOrDefault();
                if (message.IsRead == true)
                {
                    lblUnRead.Visible = false;
                }
                else
                {
                    lblUnRead.Visible = true;
                }

            }
        }
    }
}