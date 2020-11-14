using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class RiskAcceptation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                loadgrdRisk();
                mvRisk.SetActiveView(vwlist);
            }
        }

        public void loadgrdRisk()
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = (from u in db.Risks
                         where u.IsAcceptedByAdmin == false
                         select u).ToList();
                grdrisk.DataSource = n;
                grdrisk.DataBind();                
            }
        }

        protected void grdrisk_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int riskeid = int.Parse(e.CommandArgument.ToString());
            ViewState["RiskID"] = riskeid;

            switch (e.CommandName)
            {
                case "showdetail":
                    {
                        using (RiskManagementEntities db = new RiskManagementEntities())
                        {
                            var n = (from u in db.Stages
                                     join i in db.Acts on u.ActID equals i.ActID
                                     join p in db.Risks on u.StageID equals p.StageID
                                     where p.RiskID == riskeid
                                     select new
                                         {
                                             u.ActID,
                                             u.StageID,
                                             u.StageTitle,
                                             i.OperationID,
                                             i.ActTitle,
                                             p.RiskTitle,
                                         }).FirstOrDefault();

                            lblstage.Text = n.StageTitle;
                            lblact.Text = n.ActTitle;
                            lblrisktitle.Text = n.RiskTitle;
                            int operID = n.OperationID;

                            var t = (from u in db.Operations
                                     join i in db.OperationGroups on u.OperationGroupID equals i.OperationGroupID
                                     where u.OperationID == operID
                                     select new
                                     {
                                         u.OperationGroupID,
                                         u.OperationID,
                                         u.OperationTitle,
                                         i.OperationGroupTitle,

                                     }).FirstOrDefault();
                            lblOperation.Text = t.OperationTitle;
                            lblproject.Text = t.OperationGroupTitle;
                            mvRisk.SetActiveView(vwdetail);
                            break;
                        }
                    }
            }
        }

        protected void btnaccept_Click(object sender, EventArgs e)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                int Riskid = int.Parse(ViewState["RiskID"].ToString());

                var n = (from u in db.Risks
                         where u.RiskID == Riskid
                         select u).FirstOrDefault();
                n.IsAcceptedByAdmin = true;
                db.SaveChanges();
                loadgrdRisk();
                mvRisk.SetActiveView(vwlist);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            mvRisk.SetActiveView(vwlist);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }
     
    }
}