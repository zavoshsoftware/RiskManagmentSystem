using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskManagementSystem.Companies
{
    public partial class AfterRiskEval : System.Web.UI.Page
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
            List<UserRisks> userRisks = db.UserRisks.Where(current => current.UserID_Company == userId && (current.RiskIntensityID_AfterCO == null || current.RiskProbabilityID_AfterCO == null))
               .OrderBy(current => current.CreationDate).ThenBy(current => current.UserRiskID).ToList();
            foreach (UserRisks userRisk in userRisks)
            {
                if (riskHistory.All(current => current.StageId != userRisk.Risks.StageID))
                {
                    riskHistory.Add(new RiskHistoryViewModel()
                    {
                        StageTitle = userRisk.Risks.Stages.StageTitle,
                        UserRiskId = userRisk.UserRiskID,
                        ActTitle = userRisk.Risks.Stages.Acts.ActTitle,
                        OperationTitle = userRisk.Risks.Stages.Acts.Operations.OperationTitle,
                        OperationGroupTitle = userRisk.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupTitle,
                        StageId = userRisk.Risks.StageID
                    });
                }
            }
            grdRisks.DataSource = riskHistory;
            grdRisks.DataBind();
        }
    }
}