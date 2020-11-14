using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Companies
{
    public partial class ViewRiskControl : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GrdStageDataSource();
            }

        }

        public void LoadLabels(int RiskID)
        {
            var rr = (from r in db.Risks
                      where r.RiskID == RiskID
                      select r).FirstOrDefault();

            lblRiskName.Text = rr.RiskTitle;

            var ss = (from s in db.Stages
                      where s.StageID == rr.StageID
                      select s).FirstOrDefault();
            lblStageName.Text = ss.StageTitle;

            var o = (from op in db.Acts
                     where op.ActID == ss.ActID
                     select op).FirstOrDefault();

            lblActName.Text = o.ActTitle;

            var n = (from op in db.Operations
                     where op.OperationID == o.OperationID
                     select op).FirstOrDefault();

            lblOperationName.Text = n.OperationTitle;

            var m = (from opg in db.OperationGroups
                     where opg.OperationGroupID == n.OperationGroupID
                     select opg).FirstOrDefault();

            lblProjectName.Text = m.OperationGroupTitle;

        }
        public void GrdStageDataSource()
        {

            if (Request.QueryString["ID"] != null)
            {
                ViewState["RiskID"] = Request.QueryString["ID"];

                int RiskID = Convert.ToInt32(ViewState["RiskID"]);

                var n = from og in db.ControlingWorks
                        orderby og.CodeID
                        where og.RiskID == RiskID
                        select og;

                grdControl.DataSource = n;
                grdControl.DataBind();

                LoadLabels(RiskID);
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            int RiskID = Convert.ToInt32(ViewState["RiskID"]);

            //var n = (from a in db.Risks
            //         where a.RiskID == RiskID
            //         select a).FirstOrDefault();
            Response.Redirect("~/Companies/RiskEval.aspx?Id="+ RiskID);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0)
            {
                var n = (from a in db.ControlingWorks
                         where a.ControlTitle == txtSearch.Text
                         select a);
                if (n.FirstOrDefault() != null)
                {
                    grdControl.DataSource = n;
                    grdControl.DataBind();
                }
                else
                {
                    lblNotFound.Visible = true;

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "SearchScript",
            "$('#SearchDiv').css('display','block');", true);
            }
        }
    }
}