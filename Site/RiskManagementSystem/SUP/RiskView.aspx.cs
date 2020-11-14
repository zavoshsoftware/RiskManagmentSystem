using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;

namespace RiskManagementSystem.SUP
{
    public partial class RiskView : System.Web.UI.Page
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

            List<SupRiskHistoryViewModel> riskHistory = new List<SupRiskHistoryViewModel>();

            List<Users> users = db.Users.Where(current => current.SupervisorUserId == userId).ToList();

            foreach (Users user in users)
            {
                List<UserRisks> userRisks = db.UserRisks.Where(current => current.UserID_Company == user.UserID && current.IsCheckBySup == false)
                    .OrderBy(current => current.CreationDate).ThenBy(current => current.UserRiskID).ToList();

                foreach (UserRisks userRisk in userRisks)
                {
                    if (riskHistory.All(current => current.StageId != userRisk.Risks.StageID || current.CompanyUserId != user.UserID))
                    {
                        riskHistory.Add(new SupRiskHistoryViewModel()
                        {
                            StageTitle = userRisk.Risks.Stages.StageTitle,
                            StageId = userRisk.Risks.StageID,
                            ActTitle = userRisk.Risks.Stages.Acts.ActTitle,
                            OperationTitle = userRisk.Risks.Stages.Acts.Operations.OperationTitle,
                            OperationGroupTitle = userRisk.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupTitle,
                            CompanyTitle = user.CompanyName,
                            CompanyUserId = user.UserID
                        });
                    }
                }
            }
            grdOperationGroup.DataSource = riskHistory;
            grdOperationGroup.DataBind();
        }

    }
}