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
    public partial class RiskHistory : System.Web.UI.Page
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
                List<UserRisks> userRisks = GetUserRiskListByType(Request.QueryString["type"], user.UserID);

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
                            CompanyUserId = user.UserID,
                            Type = Request.QueryString["type"]
                        });
                    }
                }
            }
            grdOperationGroup.DataSource = riskHistory;
            grdOperationGroup.DataBind();
        }

        public List<UserRisks> GetUserRiskListByType(string type, int companyUserId)
        {
            switch (type.ToLower())
            {
                case "all":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current => current.UserID_Company == companyUserId && current.IsCheckBySup == true)
                            .OrderBy(current => current.CreationDate).ThenBy(current => current.UserRiskID).ToList();

                        return userRisks;
                    }
                case "confirm":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current =>
                                current.UserID_Company == companyUserId && current.IsCheckBySup == true &&
                                current.StatusId == 2).OrderBy(current => current.CreationDate)
                            .ThenBy(current => current.UserRiskID).ToList();

                        return userRisks;
                    }
                case "deny":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current =>
                                current.UserID_Company == companyUserId && current.IsCheckBySup == true &&
                                current.StatusId == 3).OrderBy(current => current.CreationDate)
                            .ThenBy(current => current.UserRiskID).ToList();

                        return userRisks;
                    }
                case "edit":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current =>
                                current.UserID_Company == companyUserId && current.IsCheckBySup == true &&
                                current.StatusId == 4).OrderBy(current => current.CreationDate)
                            .ThenBy(current => current.UserRiskID).ToList();

                        return userRisks;
                    }
                default: return new List<UserRisks>();
            }
        }

    }
}