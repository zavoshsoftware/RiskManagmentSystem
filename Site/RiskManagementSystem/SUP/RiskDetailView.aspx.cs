using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;
using System.Drawing;
using System.IO;

namespace RiskManagementSystem.SUP
{
    public partial class RiskDetailView : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<UserRisks> userRisks = new List<UserRisks>();
                GridViewDataSource(userRisks);
                ddlProjectBind();
            }

        }

        public void GridViewDataSource(List<UserRisks> userRiskList)
        {

            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string riskAfterProbabilityTitle = string.Empty;
            string riskAfterIntensityTitle = string.Empty;
            string riskAfterEvaluationTitle = string.Empty;
            List<UserRisks> userRisks = new List<UserRisks>();

            if (userRiskList.Count() > 0)
                userRisks = userRiskList;
            else
                userRisks = db.UserRisks.Where(Current => Current.Users.SupervisorUserId == userId && Current.StatusId == 1).ToList();

            //int stageId = Convert.ToInt32(Request.QueryString["stageId"]);
            //int companyUserId = Convert.ToInt32(Request.QueryString["companyId"]);

            //lblStageTitle.Text = db.Stages.FirstOrDefault(current => current.StageID == stageId)?.StageTitle;
            //  List<Users> users = db.Users.Where(current => current.SupervisorUserId == userId).ToList();

            List<SupRiskDetailViewModel> riskDetails = new List<SupRiskDetailViewModel>();

            //  foreach (Users user in users)
            //  {
            //List<UserRisks> userRisks = db.UserRisks
            //    .Where(current => current.UserID_Company == companyUserId && current.Risks.StageID == stageId &&
            //                      current.IsCheckBySup == false).ToList();

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
                //  }
            }
            grdTable.DataSource = riskDetails;
            grdTable.DataBind();
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

        protected void grdTable_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DoConfirm")
            {
                int userRiskId = Convert.ToInt32(e.CommandArgument);

                UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();

                if (userRisk != null)
                {
                    userRisk.IsCheckBySup = true;
                    userRisk.StatusId = 2;
                    userRisk.LastModifationDate = DateTime.Now;
                    db.SaveChanges();

                    //GridViewDataSource();
                    Checking();
                }
            }

            if (e.CommandName == "DoDeny")
            {
                int userRiskId = Convert.ToInt32(e.CommandArgument);

                UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();

                if (userRisk != null)
                {
                    userRisk.IsCheckBySup = true;
                    userRisk.StatusId = 3;
                    userRisk.LastModifationDate = DateTime.Now;
                    db.SaveChanges();
                    Checking();
                    //GridViewDataSource();
                }
            }
            if (e.CommandName == "DoEdit")
            {
                int userRiskId = Convert.ToInt32(e.CommandArgument);
                ViewState["UserRiskID"] = userRiskId;
                UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();

                if (userRisk != null)
                {
                    pnlRiskEdit.Visible = true;

                    LoadProbDDL(dlAfterProb);
                    LoadProbDDL(dlBeforeProb);
                    LoadIntDDL(dlBeforeInt);
                    LoadIntDDL(dlAfterInt);
                    LoadDropDownData(userRisk);
                    Checking();
                    //GridViewDataSource();
                }
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
        protected void btnInsertEdit_Click(object sender, EventArgs e)
        {
            int userRiskId = Convert.ToInt32(ViewState["UserRiskID"]);

            UserRisks userRisk = db.UserRisks.FirstOrDefault(current => current.UserRiskID == userRiskId);

            SubmitOldUserRisk(userRisk);
            ChangeCurrentUserRisk(userRisk);
            db.SaveChanges();

            //GridViewDataSource();
            Checking();
            pnlRiskEdit.Visible = false;
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


        private void UpdateGrdRisks(int? projectId, int? operationId, int? actId, int? stageId)
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            if (stageId == null)
            {
                if (actId == null)
                {
                    if (operationId == null)
                    {
                        ViewState["projectId"] = projectId;
                        List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                         current.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupID == projectId && current.StatusId == 1).ToList();
                        //List<UserRisks> userRisks = (from u in db.UserRisks
                        //                             join r in db.Risks on u.RiskID equals r.RiskID
                        //                             join s in db.Stages on r.StageID equals s.StageID
                        //                             join a in db.Acts on s.ActID equals a.ActID
                        //                             join p in db.Operations on a.OperationID equals p.OperationID
                        //                             where p.OperationGroupID == projectId
                        //                             select u).ToList();
                        if (userRisks.Count() > 0)
                        {
                            GridViewDataSource(userRisks);
                            grdTable.Visible = true;
                            lblEmpty.Visible = false;
                            btnExportToExcel.Visible = true;
                        }

                        else
                        {
                            lblEmpty.Visible = true;
                            grdTable.Visible = false;
                            btnExportToExcel.Visible = false;
                        }

                    }
                    else
                    {
                        ViewState["operationId"] = operationId;
                        List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                          current.Risks.Stages.Acts.OperationID == operationId && current.StatusId == 1).ToList();
                        //List<UserRisks> userRisks = (from u in db.UserRisks
                        //                             join r in db.Risks on u.RiskID equals r.RiskID
                        //                             join s in db.Stages on r.StageID equals s.StageID
                        //                             join a in db.Acts on s.ActID equals a.ActID
                        //                             where a.OperationID == operationId
                        //                             select u).ToList();

                        if (userRisks.Count() > 0)
                        {
                            GridViewDataSource(userRisks);
                            grdTable.Visible = true;
                            lblEmpty.Visible = false;
                            btnExportToExcel.Visible = true;
                        }

                        else
                        {
                            lblEmpty.Visible = true;
                            grdTable.Visible = false;
                            btnExportToExcel.Visible = false;
                        }
                    }
                }
                else
                {
                    ViewState["actId"] = actId;
                    List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                         current.Risks.Stages.ActID == actId && current.StatusId == 1).ToList();
                    //List<UserRisks> userRisks = (from u in db.UserRisks
                    //                             join r in db.Risks on u.RiskID equals r.RiskID
                    //                             join s in db.Stages on r.StageID equals s.StageID
                    //                             where s.ActID == actId
                    //                             select u).ToList();

                    if (userRisks.Count() > 0)
                    {
                        GridViewDataSource(userRisks);
                        grdTable.Visible = true;
                        lblEmpty.Visible = false;
                        btnExportToExcel.Visible = true;

                    }

                    else
                    {
                        lblEmpty.Visible = true;
                        grdTable.Visible = false;
                        btnExportToExcel.Visible = false;
                    }
                }

            }
            else
            {
                ViewState["stageId"] = stageId;
                List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                       current.Risks.StageID == stageId && current.StatusId == 1).ToList();
                //List<UserRisks> userRisks = (from u in db.UserRisks
                //                             join r in db.Risks on u.RiskID equals r.RiskID
                //                             where r.StageID == stageId
                //                             select u).ToList();

                if (userRisks.Count() > 0)
                {
                    GridViewDataSource(userRisks);
                    grdTable.Visible = true;
                    lblEmpty.Visible = false;
                    btnExportToExcel.Visible = true;
                }

                else
                {
                    lblEmpty.Visible = true;
                    grdTable.Visible = false;
                    btnExportToExcel.Visible = false;
                }
            }
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(ddlProject.SelectedValue);
            ddlOperationBind(projectId);
            UpdateGrdRisks(projectId, null, null, null);
        }
        protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int operationId = Convert.ToInt32(ddlOperation.SelectedValue);
            ddlActBind(operationId);
            UpdateGrdRisks(null, operationId, null, null);
        }
        protected void ddlAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int actId = Convert.ToInt32(ddlAct.SelectedValue);
            ddlStageBind(actId);
            UpdateGrdRisks(null, null, actId, null);
        }
        protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stageId = Convert.ToInt32(ddlStage.SelectedValue);
            UpdateGrdRisks(null, null, null, stageId);
        }
        private void ddlProjectBind()
        {
            List<OperationGroups> list = db.OperationGroups.ToList();
            ddlProject.Items.Clear();
            ddlProject.Items.Add(new ListItem("پروژه ", "-1"));
            foreach (var i in list)
                ddlProject.Items.Add(new ListItem(i.OperationGroupTitle, i.OperationGroupID.ToString()));
        }
        private void ddlOperationBind(int projectId)
        {
            List<Operations> list = db.Operations.Where(current => current.OperationGroupID == projectId).ToList();
            ddlOperation.Items.Clear();
            ddlOperation.Items.Add(new ListItem("عملیات ", "-1"));
            foreach (var i in list)
                ddlOperation.Items.Add(new ListItem(i.OperationTitle, i.OperationID.ToString()));
        }

        private void ddlActBind(int operationId)
        {
            List<Acts> list = db.Acts.Where(current => current.OperationID == operationId).ToList();
            ddlAct.Items.Clear();
            ddlAct.Items.Add(new ListItem("فعالیت ", "-1"));
            foreach (var i in list)
                ddlAct.Items.Add(new ListItem(i.ActTitle, i.ActID.ToString()));
        }
        private void ddlStageBind(int actId)
        {
            List<Stages> list = db.Stages.Where(current => current.ActID == actId).ToList();
            ddlStage.Items.Clear();
            ddlStage.Items.Add(new ListItem("فعالیت ", "-1"));
            foreach (var i in list)
                ddlStage.Items.Add(new ListItem(i.StageTitle, i.StageID.ToString()));
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void Checking()
        {
            if (ViewState["stageId"] != null)
            {
                var stageId = int.Parse(ViewState["stageId"].ToString());
                ExcelGrdRisks(null, null, null, stageId);
            }
            else if (ViewState["actId"] != null)
            {
                var actId = int.Parse(ViewState["actId"].ToString());
                ExcelGrdRisks(null, null, actId, null);
            }
            else if (ViewState["operationId"] != null)
            {
                var operationId = int.Parse(ViewState["operationId"].ToString());
                ExcelGrdRisks(null, operationId, null, null);
            }
            else if (ViewState["projectId"] != null)
            {
                var projectId = int.Parse(ViewState["projectId"].ToString());
                ExcelGrdRisks(projectId, null, null, null);
            }
            else
            {
                List<UserRisks> userRisks = new List<UserRisks>();
                ExcelGrdRisks(null, null, null, null);
                //GridViewDataSource(userRisks);
            }
        }

        private void ExcelGrdRisks(int? projectId, int? operationId, int? actId, int? stageId)
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            if (projectId == null && operationId == null && actId == null && stageId == null)
            {
                List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                        current.StatusId == 1
                        && (current.RiskProbabilityID != 7 || current.RiskProbabilityID_AfterCO != 7
                         || current.RiskIntensityID != 5 || current.RiskIntensityID_AfterCO != 5)).ToList();
                GridViewDataSource(userRisks);
            }
            else
            {
                if (stageId == null)
                {
                    if (actId == null)
                    {
                        if (operationId == null)
                        {
                            ViewState["projectId"] = projectId;
                            List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                             current.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupID == projectId && current.StatusId == 1
                             && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1)).ToList();
                            //List<UserRisks> userRisks = (from u in db.UserRisks
                            //                             join r in db.Risks on u.RiskID equals r.RiskID
                            //                             join s in db.Stages on r.StageID equals s.StageID
                            //                             join a in db.Acts on s.ActID equals a.ActID
                            //                             join p in db.Operations on a.OperationID equals p.OperationID
                            //                             where p.OperationGroupID == projectId
                            //                             select u).ToList();
                            if (userRisks.Count() > 0)
                            {
                                GridViewDataSource(userRisks);
                                grdTable.Visible = true;
                                lblEmpty.Visible = false;
                                btnExportToExcel.Visible = true;
                            }

                            else
                            {
                                lblEmpty.Visible = true;
                                grdTable.Visible = false;
                                btnExportToExcel.Visible = false;
                            }

                        }
                        else
                        {
                            ViewState["operationId"] = operationId;
                            List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                              current.Risks.Stages.Acts.OperationID == operationId && current.StatusId == 1
                               && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1)).ToList();
                            //List<UserRisks> userRisks = (from u in db.UserRisks
                            //                             join r in db.Risks on u.RiskID equals r.RiskID
                            //                             join s in db.Stages on r.StageID equals s.StageID
                            //                             join a in db.Acts on s.ActID equals a.ActID
                            //                             where a.OperationID == operationId
                            //                             select u).ToList();

                            if (userRisks.Count() > 0)
                            {
                                GridViewDataSource(userRisks);
                                grdTable.Visible = true;
                                lblEmpty.Visible = false;
                                btnExportToExcel.Visible = true;
                            }

                            else
                            {
                                lblEmpty.Visible = true;
                                grdTable.Visible = false;
                                btnExportToExcel.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        ViewState["actId"] = actId;
                        List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                             current.Risks.Stages.ActID == actId && current.StatusId == 1
                              && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1)).ToList();
                        //List<UserRisks> userRisks = (from u in db.UserRisks
                        //                             join r in db.Risks on u.RiskID equals r.RiskID
                        //                             join s in db.Stages on r.StageID equals s.StageID
                        //                             where s.ActID == actId
                        //                             select u).ToList();

                        if (userRisks.Count() > 0)
                        {
                            GridViewDataSource(userRisks);
                            grdTable.Visible = true;
                            lblEmpty.Visible = false;
                            btnExportToExcel.Visible = true;

                        }

                        else
                        {
                            lblEmpty.Visible = true;
                            grdTable.Visible = false;
                            btnExportToExcel.Visible = false;
                        }
                    }

                }
                else
                {
                    ViewState["stageId"] = stageId;
                    List<UserRisks> userRisks = db.UserRisks.Where(current => current.Users.SupervisorUserId == userId &&
                           current.Risks.StageID == stageId && current.StatusId == 1
                            && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1)).ToList();
                    //List<UserRisks> userRisks = (from u in db.UserRisks
                    //                             join r in db.Risks on u.RiskID equals r.RiskID
                    //                             where r.StageID == stageId
                    //                             select u).ToList();

                    if (userRisks.Count() > 0)
                    {
                        GridViewDataSource(userRisks);
                        grdTable.Visible = true;
                        lblEmpty.Visible = false;
                        btnExportToExcel.Visible = true;
                    }

                    else
                    {
                        lblEmpty.Visible = true;
                        grdTable.Visible = false;
                        btnExportToExcel.Visible = false;
                    }
                }
            }
        }
        protected void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdTable.AllowPaging = false;

                Checking();
                //this.LoadRiskgrd(stageId);

                grdTable.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdTable.HeaderRow.Cells)
                {
                    cell.BackColor = grdTable.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdTable.Rows)
                {
                    LinkButton lbConfirm = (LinkButton)row.FindControl("lbConfirm");
                    lbConfirm.Visible = false;
                    LinkButton lbDeny = (LinkButton)row.FindControl("lbDeny");
                    lbDeny.Visible = false;
                    LinkButton lbEdit = (LinkButton)row.FindControl("lbEdit");
                    lbEdit.Visible = false;

                    Label lblControl = (Label)row.FindControl("lblControl");
                    lblControl.Visible = true;
                    HiddenField hfUserRiskID = (HiddenField)row.FindControl("hfUserRiskID");
                    int userRiskId = Convert.ToInt32(hfUserRiskID.Value);
                    var riskk = (from u in db.UserRisks
                                 join r in db.Risks
                                 on u.RiskID equals r.RiskID
                                 where u.UserRiskID == userRiskId
                                 select r).FirstOrDefault();
                    List<Risks> risks = db.Risks.Where(current => current.StageID == riskk.StageID).ToList();
                    int i = 0;
                    string controlText = string.Empty;
                    foreach (var risk in risks)
                    {
                        var n = (from og in db.ControlingWorks
                                 orderby og.CodeID
                                 where og.RiskID == risk.RiskID
                                 select og).ToList();
                        foreach (var item in n)
                        {
                            i = i + 1;
                            controlText = controlText + "</br>" + (i) + ") " + item.ControlTitle;
                            lblControl.Text = controlText;
                        }

                    }


                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {

                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdTable.AlternatingRowStyle.BackColor;
                            
                        }
                        else
                        {
                            cell.BackColor = grdTable.RowStyle.BackColor;
                        }

                        cell.CssClass = "textmode";

                    }
                }

                //grdTable.Columns[9].Visible = false;
                grdTable.Columns[9].HeaderText = "اقدامات کنترلی";
                grdTable.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode {direction:rtl;}.table,.table > tr, .table > thead > tr > th, .table > tbody > tr > td{float:right;direction:rtl;} </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlRiskDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            int degreeId = Convert.ToInt32(ddlRiskDegree.SelectedValue);
            RiskDegreeBind(degreeId);
        }

        private void RiskDegreeBind(int degreeId)
        {
           
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            List<UserRisks> userRisks = db.UserRisks.Where(Current => Current.Users.SupervisorUserId == userId && Current.StatusId == 1).ToList();
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
    }
}