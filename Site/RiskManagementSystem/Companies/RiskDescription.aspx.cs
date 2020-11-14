using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Companies
{
    public partial class RiskDescription : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int userRiskId = Convert.ToInt32(Request.QueryString["id"]);

                    UserRiskSupervisorChange userRiskSupervisorChange = db.UserRiskSupervisorChange.Where(current => current.UserRiskId == userRiskId)
                        .FirstOrDefault();

                    if (userRiskSupervisorChange != null)
                    {
                        if (userRiskSupervisorChange.SupervisorDescription != null)
                        {
                            pnlNoData.Visible = false;
                            pnlData.Visible = true;
                            lblRisDesk.Text = userRiskSupervisorChange.SupervisorDescription;
                        }
                        else
                        {
                            pnlNoData.Visible = true;
                            pnlData.Visible = false;
                        }
                    }
                    else
                    {
                        pnlNoData.Visible = true;
                        pnlData.Visible = false;
                    }
                }
                else
                {
                    pnlNoData.Visible = true;
                    pnlData.Visible = false;
                }
            }
        }
    }
}