using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskManagementSystem.Admin
{
    public partial class RiskControl : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GridViewDataSource();
        }

        public void GridViewDataSource()
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            List<RiskHistoryViewModel> riskHistory = new List<RiskHistoryViewModel>();

            List<UserRisks> userRisks = db.UserRisks.Where(current=>current.IsCheckByAdmin==null).ToList();

            foreach (UserRisks userRisk in userRisks)
            {

                //if (riskHistory.All(current => current.StageId != userRisk.Risks.StageID))
                //{
                    riskHistory.Add(new RiskHistoryViewModel()
                    {
                        StageTitle = userRisk.Risks.Stages.StageTitle,
                        UserRiskId = userRisk.UserRiskID,
                        ActTitle = userRisk.Risks.Stages.Acts.ActTitle,
                        OperationTitle = userRisk.Risks.Stages.Acts.Operations.OperationTitle,
                        OperationGroupTitle = userRisk.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupTitle,
                        StageId = userRisk.Risks.StageID,
                       RiskTitle=userRisk.Risks.RiskTitle
                    });
                //}
            }

            grdRisks.DataSource = riskHistory;
            grdRisks.DataBind();
        }

        protected void grdRisks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DoConfirm")
            {
                int userRiskId = Convert.ToInt32(e.CommandArgument);

                UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();

                if (userRisk != null)
                {
                    userRisk.IsCheckByAdmin = true;
                    userRisk.LastModifationDate = DateTime.Now;
                    db.SaveChanges();

                    GridViewDataSource();
                    //Checking();
                }
            }

            if (e.CommandName == "DoDeny")
            {
                int userRiskId = Convert.ToInt32(e.CommandArgument);

                UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();

                if (userRisk != null)
                {
                    userRisk.IsCheckByAdmin = false;
                    userRisk.LastModifationDate = DateTime.Now;
                    db.SaveChanges();
                    //Checking();
                    GridViewDataSource();
                }
            }
        }
    }
}