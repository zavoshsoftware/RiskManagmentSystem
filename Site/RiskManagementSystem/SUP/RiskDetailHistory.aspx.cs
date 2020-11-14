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
    public partial class RiskDetailHistory : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridViewDataSource(GetUserRiskListByType());
            }

        }

        public void GridViewDataSource(List<UserRisks> userRisks)
        {

            //int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            //List<UserRisks> userRisks = GetUserRiskListByType(Request.QueryString["type"], userId);
            string riskAfterProbabilityTitle = string.Empty;
            string riskAfterIntensityTitle = string.Empty;
            string riskAfterEvaluationTitle = string.Empty;
            //int stageId = Convert.ToInt32(Request.QueryString["stageId"]);
            //int companyUserId = Convert.ToInt32(Request.QueryString["companyId"]);

            //lblStageTitle.Text = db.Stages.FirstOrDefault(current => current.StageID == stageId)?.StageTitle;
            //  List<Users> users = db.Users.Where(current => current.SupervisorUserId == userId).ToList();

            List<SupRiskDetailViewModel> riskDetails = new List<SupRiskDetailViewModel>();

            //List<UserRisks> userRisks = db.UserRisks
            //    .Where(current => current.Users.SupervisorUserId == userId && current.Users.RoleID == 2 &&
            //                      current.IsCheckBySup == true).ToList();
            //List<UserRisks> userRisks = db.UserRisks
            //    .Where(current => current.UserID_Company == companyUserId && current.Risks.StageID == stageId &&
            //                      current.IsCheckBySup == true).ToList();

            foreach (UserRisks userRisk in userRisks)
            {
                if (userRisk.RiskProbabilityID_AfterCO != null)
                {
                    riskAfterProbabilityTitle = GetRiskProbability(Convert.ToInt32(userRisk.RiskProbabilityID_AfterCO));
                }
                if (userRisk.RiskIntensityID_AfterCO != null)
                {
                    riskAfterIntensityTitle = GetRiskIntensity(Convert.ToInt32(userRisk.RiskIntensityID_AfterCO));
                }
                if (userRisk.RiskProbabilityID_AfterCO != null && userRisk.RiskIntensityID_AfterCO != null)
                {
                    riskAfterEvaluationTitle = GetRiskEvaluationTitle(Convert.ToInt32(userRisk.RiskIntensityID_AfterCO), Convert.ToInt32(userRisk.RiskProbabilityID_AfterCO));
                }
                riskDetails.Add(new SupRiskDetailViewModel()
                {
                    RiskTitle = userRisk.Risks.RiskTitle,
                    RiskIntensityTitle = GetRiskIntensity(userRisk.RiskIntensityID),
                    RiskProbabilityTitle = GetRiskProbability(userRisk.RiskProbabilityID),
                    RiskEvaluationTitle =
                        GetRiskEvaluationTitle(userRisk.RiskIntensityID, userRisk.RiskProbabilityID),
                    StatusTitle = userRisk.Status?.Title,
                    SubmitDate = userRisk.CreationDate.Value,
                    UserRiskId = userRisk.UserRiskID,

                    RiskAfterProbabilityTitle = riskAfterProbabilityTitle,
                    RiskAfterIntensityTitle = riskAfterIntensityTitle,
                    RiskAfterEvaluationTitle = riskAfterEvaluationTitle
                });
            }
            grdTable.DataSource = riskDetails;
            grdTable.DataBind();
        }
        public List<UserRisks> GetUserRiskListByType()
        {
            string type = Request.QueryString["type"].ToString();
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            switch (type.ToLower())
            {
                case "all":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current => current.Users.SupervisorUserId == userId && current.Users.RoleID == 2 && current.IsCheckBySup == true)
                            .OrderBy(current => current.CreationDate).ThenBy(current => current.UserRiskID).ToList();
                        lblStageTitle.Text = "همه";
                        if (userRisks.Count() == 0)
                            lblEmpty.Visible = true;
                        return userRisks;
                    }
                case "confirm":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current => current.Users.SupervisorUserId == userId && current.Users.RoleID == 2 && current.IsCheckBySup == true &&
                                current.StatusId == 2).OrderBy(current => current.CreationDate)
                            .ThenBy(current => current.UserRiskID).ToList();
                        lblStageTitle.Text = "تایید شده";
                        if (userRisks.Count() == 0)
                            lblEmpty.Visible = true;
                        else
                            lblEmpty.Visible = false;
                        return userRisks;
                    }
                case "deny":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current => current.Users.SupervisorUserId == userId && current.Users.RoleID == 2 && current.IsCheckBySup == true &&
                                current.StatusId == 3).OrderBy(current => current.CreationDate)
                            .ThenBy(current => current.UserRiskID).ToList();
                        lblStageTitle.Text = "رد شده";
                        if (userRisks.Count() == 0)
                            lblEmpty.Visible = true;
                        else
                            lblEmpty.Visible = false;
                        return userRisks;
                    }
                case "edit":
                    {
                        List<UserRisks> userRisks = db.UserRisks
                            .Where(current => current.Users.SupervisorUserId == userId && current.Users.RoleID == 2 && current.IsCheckBySup == true &&
                                current.StatusId == 4).OrderBy(current => current.CreationDate)
                            .ThenBy(current => current.UserRiskID).ToList();
                        lblStageTitle.Text = "ویرایش شده";
                        if (userRisks.Count() == 0)
                            lblEmpty.Visible = true;
                        else
                            lblEmpty.Visible = false;
                        return userRisks;
                    }
                default: return new List<UserRisks>();
            }
        }
        public string GetRiskIntensity(int riskIntensityId)
        {
            return db.RiskIntensities.FirstOrDefault(current => current.RiskIntensityID == riskIntensityId)
                ?.RiskIntensityTitle;
        }

        public string GetRiskProbability(int riskProbabilityId)
        {
            return db.RiskProbabilities.FirstOrDefault(current => current.RiskProbabilityID == riskProbabilityId)
                ?.RiskProbabilityTitle;
        }

        public string GetRiskEvaluationTitle(int riskIntensityId, int riskProbabilityId)
        {
            var n = (from re in db.RiskEvaluations
                     where re.RiskIntensityID == riskIntensityId &&
                           re.RiskProbabilityID == riskProbabilityId
                     select re).FirstOrDefault();


            if (n != null)
            {
                if (n.RiskEvaluationNumber >= 1 &&
                    n.RiskEvaluationNumber <= 3)
                {
                    return n.RiskEvaluationNumber.ToString() + "-" +
                           "قابل قبول بدون نیاز به بازنگری";
                }
                else if (n.RiskEvaluationNumber >= 4 &&
                         n.RiskEvaluationNumber <= 11)
                {
                    return n.RiskEvaluationNumber.ToString() + "-" +
                           "قابل قبول با نیاز به بازنگری";
                }
                else if (n.RiskEvaluationNumber >= 12 &&
                         n.RiskEvaluationNumber <= 15)
                {
                    return n.RiskEvaluationNumber.ToString() + "-" +
                           "نامطلوب ، نیاز به تصمیم گیری";
                }
                else if (n.RiskEvaluationNumber >= 16 &&
                         n.RiskEvaluationNumber <= 20)
                {
                    return n.RiskEvaluationNumber.ToString() + "-" +
                           "غیر قابل قبول";
                }
            }
            return "خطا";
        }

        protected void ddlRiskDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            int degreeId = Convert.ToInt32(ddlRiskDegree.SelectedValue);
            RiskDegreeBind(degreeId);
        }

        private void RiskDegreeBind(int degreeId)
        {

            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            List<UserRisks> userRisks = GetUserRiskListByType();
            if (degreeId == 0)
            {
                grdTable.Visible = true;
                GridViewDataSource(userRisks);
            }
            else
            {
                List<UserRisks> temp = new List<UserRisks>();
                foreach (var risk in userRisks)
                {

                    RiskEvaluations riskEvaluation = db.RiskEvaluations.Where(current => current.RiskIntensityID == risk.RiskIntensityID && current.RiskProbabilityID == risk.RiskProbabilityID).FirstOrDefault();
                    if (riskEvaluation != null)
                    {

                        if (degreeId == 1)
                        {
                            if (1 <= riskEvaluation.RiskEvaluationNumber && riskEvaluation.RiskEvaluationNumber <= 3)
                                temp.Add(risk);
                        }
                        else if (degreeId == 2)
                        {
                            if (4 <= riskEvaluation.RiskEvaluationNumber && riskEvaluation.RiskEvaluationNumber <= 15)
                                temp.Add(risk);
                        }
                        else if (degreeId == 3)
                        {
                            if (16 <= riskEvaluation.RiskEvaluationNumber && riskEvaluation.RiskEvaluationNumber <= 20)
                                temp.Add(risk);
                        }
                    }
                }

                if (temp.Count() == 0)
                    grdTable.Visible = false;
                else
                {
                    grdTable.Visible = true;
                    GridViewDataSource(temp);
                }
            }
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userRiskId = Convert.ToInt32(e.CommandArgument);
            ViewState["UserRiskID"] = userRiskId;
            UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();
            string commandName = e.CommandName.ToString();

            if (commandName == "DoConfirm")
            {
                userRisk.StatusId = 2;
                db.SaveChanges();
                GridViewDataSource(GetUserRiskListByType());
            }
            else if (commandName == "DoDeny")
            {
                userRisk.StatusId = 3;
                db.SaveChanges();
                GridViewDataSource(GetUserRiskListByType());
            }
            else if (commandName == "DoEdit")
            {

                if (userRisk != null)
                {
                    pnlRiskEdit.Visible = true;

                    LoadProbDDL(dlAfterProb);
                    LoadProbDDL(dlBeforeProb);
                    LoadIntDDL(dlBeforeInt);
                    LoadIntDDL(dlAfterInt);
                    LoadDropDownData(userRisk);
                    UserRiskSupervisorChange userRiskSupervisorChang = db.UserRiskSupervisorChange.Where(current => current.UserRiskId == userRiskId).OrderByDescending(t => t.ChangeDate).FirstOrDefault();

                    if (userRiskSupervisorChang != null)
                        txtDesc.Text = userRiskSupervisorChang.SupervisorDescription;
                    //Checking();
                    //GridViewDataSource();
                }
            }
            else if (commandName == "ShowDesc")
            {
                pnlDesc.Visible = true;
                UserRiskSupervisorChange userRiskSupervisorChang = db.UserRiskSupervisorChange.Where(current => current.UserRiskId == userRiskId).OrderByDescending(t => t.ChangeDate).FirstOrDefault();
                if (userRiskSupervisorChang != null)
                    lblDesc.Text = userRiskSupervisorChang.SupervisorDescription;
            }
        }
        protected void btnInsertEdit_Click(object sender, EventArgs e)
        {
            int userRiskId = Convert.ToInt32(ViewState["UserRiskID"]);

            UserRisks userRisk = db.UserRisks.FirstOrDefault(current => current.UserRiskID == userRiskId);

            SubmitOldUserRisk(userRisk);
            ChangeCurrentUserRisk(userRisk);
            db.SaveChanges();

            //GridViewDataSource();
            //Checking();
            pnlRiskEdit.Visible = false;
            GridViewDataSource(GetUserRiskListByType());
        }
        public void ChangeCurrentUserRisk(UserRisks userRisk)
        {
            userRisk.RiskIntensityID = Convert.ToInt32(dlBeforeInt.SelectedValue);
            userRisk.RiskIntensityID_AfterCO = Convert.ToInt32(dlAfterInt.SelectedValue);
            userRisk.RiskProbabilityID = Convert.ToInt32(dlBeforeProb.SelectedValue);
            userRisk.RiskProbabilityID_AfterCO = Convert.ToInt32(dlAfterProb.SelectedValue);

            userRisk.IsCheckBySup = true;
            userRisk.StatusId = 4;
        }
        public void SubmitOldUserRisk(UserRisks userRisk)
        {
            UserRiskSupervisorChange userRiskSupervisorChange = new UserRiskSupervisorChange()
            {
                RiskIntensityID = userRisk.RiskIntensityID,
                RiskIntensityID_AfterCO = userRisk.RiskIntensityID_AfterCO,
                RiskProbabilityID = userRisk.RiskProbabilityID,
                RiskProbabilityID_AfterCO = userRisk.RiskProbabilityID_AfterCO,
                SupervisorDescription = txtDesc.Text,
                ChangeDate = DateTime.Now,
                UserRiskId = userRisk.UserRiskID
            };

            db.UserRiskSupervisorChange.AddObject(userRiskSupervisorChange);
        }
        public void LoadProbDDL(DropDownList ddlProb)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = from p in db.RiskProbabilities
                        orderby p.RiskProbabilityLevel ascending
                        select p;

                ddlProb.DataSource = n;
                ddlProb.DataValueField = "RiskProbabilityID";
                ddlProb.DataTextField = "RiskProbabilityTitle";
                ddlProb.DataBind();
            }
        }

        public void LoadIntDDL(DropDownList ddlInt)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = from p in db.RiskIntensities
                        orderby p.RiskIntensityLevel ascending
                        select p;

                ddlInt.DataSource = n;
                ddlInt.DataValueField = "RiskIntensityID";
                ddlInt.DataTextField = "RiskIntensityTitle";
                ddlInt.DataBind();
            }
        }
        public void LoadDropDownData(UserRisks userRisk)
        {
            //  if (userRisk.RiskProbabilityID != null)
            dlBeforeProb.SelectedValue = userRisk.RiskProbabilityID.ToString();

            if (userRisk.RiskProbabilityID_AfterCO != null)
                dlAfterProb.SelectedValue = userRisk.RiskProbabilityID_AfterCO.ToString();

            // if (userRisk.RiskIntensityID != null)
            dlBeforeInt.SelectedValue = userRisk.RiskIntensityID.ToString();

            if (userRisk.RiskIntensityID_AfterCO != null)
                dlAfterInt.SelectedValue = userRisk.RiskIntensityID_AfterCO.ToString();
        }
    }
}