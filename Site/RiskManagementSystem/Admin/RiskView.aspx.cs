using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using System.Drawing;
using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;
using System.IO;

namespace RiskManagementSystem.Admin
{
    public partial class RiskView : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<UserRisks> userRisks = new List<UserRisks>();
                GridViewDataSource(userRisks);
                ddlProjectBind();
                ViewState["IsVisited"] = Request.QueryString["ID"];
                ddlCompanyBind();
                ViewState["companyId"] = "-1";
            }

        }

        public void GridViewDataSource(List<UserRisks> userRiskList)
        {
            int IsVisited = Convert.ToInt32(Request.QueryString["ID"]);
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string riskAfterProbabilityTitle = string.Empty;
            string riskAfterIntensityTitle = string.Empty;
            string riskAfterEvaluationTitle = string.Empty;
            List<UserRisks> userRisks = new List<UserRisks>();

            if (userRiskList.Count() > 0)
                userRisks = userRiskList;
            else
            {
                if (IsVisited == 0)
                    userRisks = db.UserRisks
                                .Where(current => current.IsCheckBySup == false).ToList();
                else
                    userRisks = db.UserRisks
                                .Where(current => current.IsCheckBySup == true).ToList();

            }

            //int stageId = Convert.ToInt32(Request.QueryString["stageId"]);

            //lblStageTitle.Text = db.Stages.FirstOrDefault(current => current.StageID == stageId)?.StageTitle;

            List<RiskDetailViewModel> riskDetails = new List<RiskDetailViewModel>();

            //List<UserRisks> userRisks = db.UserRisks
            //    .Where(current => current.UserID_Company == userId && current.Risks.StageID == stageId).ToList();

            //List<UserRisks> userRisks = db.UserRisks
            //    .Where(current => current.UserID_Company == userId).ToList();


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

                riskDetails.Add(new RiskDetailViewModel()
                {
                    RiskTitle = userRisk.Risks.RiskTitle,
                    RiskIntensityTitle = GetRiskIntensity(userRisk.RiskIntensityID),
                    RiskProbabilityTitle = GetRiskProbability(userRisk.RiskProbabilityID),
                    RiskEvaluationTitle = GetRiskEvaluationTitle(userRisk.RiskIntensityID, userRisk.RiskProbabilityID),
                    StatusTitle = userRisk.Status?.Title,
                    SubmitDate = userRisk.CreationDate.Value,

                    CompanyName = userRisk.Users.CompanyName,
                    UserRiskID = userRisk.UserRiskID,
                    RiskAfterProbabilityTitle = riskAfterProbabilityTitle,
                    RiskAfterIntensityTitle = riskAfterIntensityTitle,
                    RiskAfterEvaluationTitle = riskAfterEvaluationTitle,


                });
            }
            if (riskDetails.Count() > 0)
            {
                grdTable.Visible = true;
                lblEmpty.Visible = false;
                btnExportToExcel.Visible = true;
                ddlRiskDegree.Visible = true;
            }
            else
            {
                lblEmpty.Visible = true;
                grdTable.Visible = false;
                btnExportToExcel.Visible = false;
                ddlRiskDegree.Visible = false;
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


        private void UpdateGrdRisks(int? projectId, int? operationId, int? actId, int? stageId, int? companyId)
        {
            List<UserRisks> userRisks = new List<UserRisks>();
            int IsVisited = Convert.ToInt32(ViewState["IsVisited"]);
            bool visited;
            if (IsVisited == 0)
                visited = false;
            else
                visited = true;
            if (companyId == null)
            {
                if (stageId == null)
                {
                    if (actId == null)
                    {
                        if (operationId == null)
                        {
                            ViewState["projectId"] = projectId;
                            userRisks = (from u in db.UserRisks
                                         join r in db.Risks on u.RiskID equals r.RiskID
                                         join s in db.Stages on r.StageID equals s.StageID
                                         join a in db.Acts on s.ActID equals a.ActID
                                         join p in db.Operations on a.OperationID equals p.OperationID
                                         where p.OperationGroupID == projectId && u.IsCheckBySup == visited
                                         select u).ToList();


                        }
                        else
                        {
                            ViewState["operationId"] = operationId;
                            userRisks = (from u in db.UserRisks
                                         join r in db.Risks on u.RiskID equals r.RiskID
                                         join s in db.Stages on r.StageID equals s.StageID
                                         join a in db.Acts on s.ActID equals a.ActID
                                         where a.OperationID == operationId && u.IsCheckBySup == visited
                                         select u).ToList();


                        }
                    }
                    else
                    {
                        ViewState["actId"] = actId;
                        userRisks = (from u in db.UserRisks
                                     join r in db.Risks on u.RiskID equals r.RiskID
                                     join s in db.Stages on r.StageID equals s.StageID
                                     where s.ActID == actId && u.IsCheckBySup == visited
                                     select u).ToList();


                    }

                }
                else
                {
                    ViewState["stageId"] = stageId;
                    userRisks = (from u in db.UserRisks
                                 join r in db.Risks on u.RiskID equals r.RiskID
                                 where r.StageID == stageId && u.IsCheckBySup == visited
                                 select u).ToList();


                }
            }
            else
            {
                ViewState["companyId"] = companyId;

                if (stageId == null)
                {
                    if (actId == null)
                    {
                        if (operationId == null)
                        {
                            if (projectId == null)
                            {
                                userRisks = (from u in db.UserRisks
                                             join r in db.Risks on u.RiskID equals r.RiskID
                                             where u.UserID_Company == companyId && u.IsCheckBySup == visited
                                             select u).ToList();
                            }
                            else
                            {
                                ViewState["projectId"] = projectId;
                                userRisks = (from u in db.UserRisks
                                             join r in db.Risks on u.RiskID equals r.RiskID
                                             join s in db.Stages on r.StageID equals s.StageID
                                             join a in db.Acts on s.ActID equals a.ActID
                                             join p in db.Operations on a.OperationID equals p.OperationID
                                             where p.OperationGroupID == projectId && u.IsCheckBySup == visited && u.UserID_Company == companyId
                                             select u).ToList();


                            }

                        }
                        else
                        {
                            ViewState["operationId"] = operationId;
                            userRisks = (from u in db.UserRisks
                                         join r in db.Risks on u.RiskID equals r.RiskID
                                         join s in db.Stages on r.StageID equals s.StageID
                                         join a in db.Acts on s.ActID equals a.ActID
                                         where a.OperationID == operationId && u.IsCheckBySup == visited && u.UserID_Company == companyId
                                         select u).ToList();


                        }
                    }
                    else
                    {
                        ViewState["actId"] = actId;
                        userRisks = (from u in db.UserRisks
                                     join r in db.Risks on u.RiskID equals r.RiskID
                                     join s in db.Stages on r.StageID equals s.StageID
                                     where s.ActID == actId && u.IsCheckBySup == visited && u.UserID_Company == companyId
                                     select u).ToList();


                    }

                }
                else
                {
                    ViewState["stageId"] = stageId;
                    userRisks = (from u in db.UserRisks
                                 join r in db.Risks on u.RiskID equals r.RiskID
                                 where r.StageID == stageId && u.IsCheckBySup == visited && u.UserID_Company == companyId
                                 select u).ToList();


                }
                //userRisks = (from u in db.UserRisks
                //             join r in db.Risks on u.RiskID equals r.RiskID
                //             where u.UserID_Company == companyId && u.IsCheckBySup == visited
                //             select u).ToList();



            }
            if (userRisks.Count() > 0)
            {
                GridViewDataSource(userRisks);
                grdTable.Visible = true;
                lblEmpty.Visible = false;
                btnExportToExcel.Visible = true;
                ddlRiskDegree.Visible = true;
            }

            else
            {
                lblEmpty.Visible = true;
                grdTable.Visible = false;
                btnExportToExcel.Visible = false;
                ddlRiskDegree.Visible = false;
            }
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(ddlProject.SelectedValue);
            ddlOperationBind(projectId);
            if (ViewState["companyId"].ToString() != "-1")
            {
                int companyId = Convert.ToInt32(ViewState["companyId"]);
                UpdateGrdRisks(projectId, null, null, null, companyId);

            }
            else
                UpdateGrdRisks(projectId, null, null, null, null);
        }
        protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int operationId = Convert.ToInt32(ddlOperation.SelectedValue);
            ddlActBind(operationId);
            if (ViewState["companyId"].ToString() != "-1")
            {
                int companyId = Convert.ToInt32(ViewState["companyId"]);
                UpdateGrdRisks(null, operationId, null, null, companyId);
            }
            else
                UpdateGrdRisks(null, operationId, null, null, null);
        }
        protected void ddlAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int actId = Convert.ToInt32(ddlAct.SelectedValue);
            ddlStageBind(actId);
            if (ViewState["companyId"].ToString() != "-1")
            {
                int companyId = Convert.ToInt32(ViewState["companyId"]);
                UpdateGrdRisks(null, null, actId, null, companyId);
            }
            else
                UpdateGrdRisks(null, null, actId, null, null);
        }
        protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stageId = Convert.ToInt32(ddlStage.SelectedValue);
            //ddlCompanyBind(stageId);
            if (ViewState["companyId"].ToString() != "-1")
            {
                int companyId = Convert.ToInt32(ViewState["companyId"]);
                UpdateGrdRisks(null, null, null, stageId, companyId);
            }
            else
                UpdateGrdRisks(null, null, null, stageId, null);
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyId = Convert.ToInt32(ddlCompany.SelectedValue);
            ViewState["companyId"] = companyId;
            if (companyId != -1)
            {
                //UpdateGrdRisks(null, null, null, null, companyId);
                if (ddlStage.SelectedValue != "" && ddlStage.SelectedValue != "-1")
                {
                    int stageId = Convert.ToInt32(ddlStage.SelectedValue);
                    UpdateGrdRisks(null, null, null, stageId, companyId);
                }
                else if (ddlAct.SelectedValue != "" && ddlAct.SelectedValue != "-1")
                {
                    int actId = Convert.ToInt32(ddlAct.SelectedValue);
                    UpdateGrdRisks(null, null, actId, null, companyId);
                }
                else if (ddlOperation.SelectedValue != "" && ddlOperation.SelectedValue != "-1")
                {
                    int operationId = Convert.ToInt32(ddlOperation.SelectedValue);
                    UpdateGrdRisks(null, operationId, null, null, companyId);
                }
                else if (ddlProject.SelectedValue != "" && ddlProject.SelectedValue != "-1")
                {
                    int projectId = Convert.ToInt32(ddlProject.SelectedValue);
                    UpdateGrdRisks(projectId, null, null, null, companyId);
                }
                else
                {
                    UpdateGrdRisks(null, null, null, null, companyId);
                }
            }
            else
            {
                if (ddlStage.SelectedValue != "" && ddlStage.SelectedValue != "-1")
                {
                    int stageId = Convert.ToInt32(ddlStage.SelectedValue);
                    UpdateGrdRisks(null, null, null, stageId, null);
                }
                else if (ddlAct.SelectedValue != "" && ddlAct.SelectedValue != "-1")
                {
                    int actId = Convert.ToInt32(ddlAct.SelectedValue);
                    UpdateGrdRisks(null, null, actId, null, null);
                }
                else if (ddlOperation.SelectedValue != "" && ddlOperation.SelectedValue != "-1")
                {
                    int operationId = Convert.ToInt32(ddlOperation.SelectedValue);
                    UpdateGrdRisks(null, operationId, null, null, null);
                }
                else if (ddlProject.SelectedValue != "" && ddlProject.SelectedValue != "-1")
                {
                    int projectId = Convert.ToInt32(ddlProject.SelectedValue);
                    UpdateGrdRisks(projectId, null, null, null, null);
                }
                else
                {
                    List<UserRisks> userRisks = new List<UserRisks>();
                    GridViewDataSource(userRisks);
                }

            }

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
            ddlStage.Items.Add(new ListItem("مرحله ", "-1"));
            foreach (var i in list)
                ddlStage.Items.Add(new ListItem(i.StageTitle, i.StageID.ToString()));
        }

        private void ddlCompanyBind()
        {
            var users = (from u in db.Users where u.RoleID == 2 select u).ToList();
            //join ur in db.UserRisks on u.UserID equals ur.UserID_Company
            //join r in db.Risks on ur.RiskID equals r.RiskID
            //where r.StageID == stageId
            // group u by u.UserID into g

            //select new { UserID =g.Key,users=g.FirstOrDefault()});
            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem("پیمانکار ", "-1"));
            foreach (var i in users)
                ddlCompany.Items.Add(new ListItem(i.CompanyName, i.UserID.ToString()));
            //List<UserRisks> list = db.UserRisks.Where(current => current.Risks.StageID == stageId).ToList();
            //ddlCompany.Items.Clear();
            //ddlCompany.Items.Add(new ListItem("پیمانکار ", "-1"));
            //foreach (var i in list)
            //    ddlCompany.Items.Add(new ListItem(i.Users.CompanyName, i.UserID_Company.ToString()));
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
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

                grdTable.RenderControl(hw);

                //style to format numbers to string
                string style = @"";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        private void Checking()
        {
            if (ViewState["companyId"] != null)
            {
                var companyId = int.Parse(ViewState["companyId"].ToString());
                //this.ExcelGrdRisks(null, null, null, null, companyId);

                if (ViewState["stageId"] != null)
                {
                    var stageId = int.Parse(ViewState["stageId"].ToString());
                    ExcelGrdRisks(null, null, null, stageId, companyId);
                }
                else if (ViewState["actId"] != null)
                {
                    var actId = int.Parse(ViewState["actId"].ToString());
                    this.ExcelGrdRisks(null, null, actId, null, companyId);
                }
                else if (ViewState["operationId"] != null)
                {
                    var operationId = int.Parse(ViewState["operationId"].ToString());
                    this.ExcelGrdRisks(null, operationId, null, null, companyId);
                }
                else if (ViewState["projectId"] != null)
                {
                    var projectId = int.Parse(ViewState["projectId"].ToString());
                    this.ExcelGrdRisks(projectId, null, null, null, companyId);
                }
                //else if (ViewState["companyId"] != null)
                //{
                //    var companyId = int.Parse(ViewState["companyId"].ToString());
                //    this.ExcelGrdRisks(null, null, null, null, companyId);
                //}
                else
                {

                    this.ExcelGrdRisks(null, null, null, null, companyId);
                    //ExcelGrdRisks(null, null, null, null, null);
                }
            }
        }
        private void ExcelGrdRisks(int? projectId, int? operationId, int? actId, int? stageId, int? companyId)
        {
            int IsVisited = Convert.ToInt32(Request.QueryString["ID"]);
            List<UserRisks> userRisks = new List<UserRisks>();
            bool visited;
            if (IsVisited == 0)
                visited = false;
            else
                visited = true;
            if (projectId == null && operationId == null && actId == null && stageId == null && companyId == null)
            {

                userRisks = db.UserRisks.Where(current => (current.RiskProbabilityID != 7 || current.RiskProbabilityID_AfterCO != 7
                     || current.RiskIntensityID != 5 || current.RiskIntensityID_AfterCO != 5) && current.IsCheckBySup == visited).ToList();

            }
            else
            {

                if (companyId == -1)
                {
                    if (stageId == null)
                    {
                        if (actId == null)
                        {
                            if (operationId == null)
                            {
                                if (projectId == null || projectId == -1)
                                {
                                    userRisks = db.UserRisks.Where(current => (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                                || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited).ToList();

                                }
                                else
                                {
                                    ViewState["projectId"] = projectId;


                                    userRisks = db.UserRisks.Where(current => current.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupID == projectId
                                 && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                                 || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited).ToList();

                                }
                            }
                            else
                            {
                                ViewState["operationId"] = operationId;

                                userRisks = db.UserRisks.Where(current => current.Risks.Stages.Acts.OperationID == operationId
                               && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited).ToList();

                            }
                        }
                        else
                        {
                            ViewState["actId"] = actId;

                            userRisks = db.UserRisks.Where(current => current.Risks.Stages.ActID == actId
                              && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited).ToList();


                        }

                    }

                    else
                    {
                        ViewState["stageId"] = stageId;

                        userRisks = db.UserRisks.Where(current => current.Risks.StageID == stageId
                               && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                                || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited).ToList();


                    }
                }
                else
                {
                    ViewState["companyId"] = companyId;
                    if (stageId == null)
                    {
                        if (actId == null)
                        {
                            if (operationId == null)
                            {
                                if (projectId == null || projectId == -1)
                                {
                                    userRisks = db.UserRisks.Where(current => current.UserID_Company == companyId
                               && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                                || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited && current.UserID_Company == companyId).ToList();

                                }
                                else
                                {
                                    ViewState["projectId"] = projectId;
                                    userRisks = db.UserRisks.Where(current => current.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupID == projectId
                                 && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                                 || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited && current.UserID_Company == companyId).ToList();

                                }

                            }
                            else
                            {
                                ViewState["operationId"] = operationId;

                                userRisks = db.UserRisks.Where(current => current.Risks.Stages.Acts.OperationID == operationId
                               && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited && current.UserID_Company == companyId).ToList();

                            }
                        }
                        else
                        {
                            ViewState["actId"] = actId;

                            userRisks = db.UserRisks.Where(current => current.Risks.Stages.ActID == actId
                              && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                             || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited && current.UserID_Company == companyId).ToList();


                        }

                    }

                    else
                    {
                        ViewState["stageId"] = stageId;

                        userRisks = db.UserRisks.Where(current => current.Risks.StageID == stageId
                               && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                                || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited && current.UserID_Company == companyId).ToList();


                    }


                    //userRisks = db.UserRisks.Where(current => current.UserID_Company == companyId
                    //       //&&current.Risks.StageID == stageId
                    //       && (current.RiskProbabilities.RiskProbabilityLevel != 1 || current.RiskProbabilities1.RiskProbabilityLevel != 1
                    //        || current.RiskIntensities.RiskIntensityLevel != 1 || current.RiskIntensities1.RiskIntensityLevel != 1) && current.IsCheckBySup == visited && current.UserID_Company == companyId).ToList();

                }
            }
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void grdTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfUserRiskID = (HiddenField)e.Row.FindControl("hfUserRiskID");

                int userRiskID = int.Parse(hfUserRiskID.Value.ToString());
                UserRisks userRisk = db.UserRisks.Where(u => u.UserRiskID == userRiskID).FirstOrDefault();
                if (userRisk.StatusId == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }
                else if (userRisk.StatusId == 2)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
                else if (userRisk.StatusId == 3)
                {
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }
                else if (userRisk.StatusId == 4)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        protected void ddlRiskDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            int degreeId = Convert.ToInt32(ddlRiskDegree.SelectedValue);
            RiskDegreeBind(degreeId);
        }

        private void RiskDegreeBind(int degreeId)
        {
            int IsVisited = Convert.ToInt32(Request.QueryString["ID"]);
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            List<UserRisks> userRisks = new List<UserRisks>();
            if (IsVisited == 0)
                userRisks = db.UserRisks
                            .Where(current => current.IsCheckBySup == false).ToList();
            else
                userRisks = db.UserRisks
                            .Where(current => current.IsCheckBySup == true).ToList();

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




        //private RiskManagementEntities db = new RiskManagementEntities();
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        ViewState["IsVisited"] = Request.QueryString["ID"];
        //        LoadCompanies();
        //    }
        //}
        //public void LoadCompanies()
        //{
        //    int IsVisited = Convert.ToInt32(ViewState["IsVisited"]);
        //    if (IsVisited == 0)
        //    {
        //        var n = from ur in db.UserRisks
        //                join u in db.Users
        //                on ur.UserID_Company equals u.UserID
        //                where ur.IsCheckBySup == false
        //                group ur by new { u.Name, u.UserID } into g
        //                select new
        //               {
        //                   Name = g.Key.Name,
        //                   UserID = g.Key.UserID,
        //                   UserRiskID = g.FirstOrDefault().UserRiskID
        //               };

        //        grdCompany.DataSource = n;
        //        grdCompany.DataBind();
        //        LblSup.Text = "نشده است";
        //    }
        //    else if (IsVisited == 1)
        //    {
        //        var n = from ur in db.UserRisks
        //                join u in db.Users
        //                on ur.UserID_Company equals u.UserID
        //                where ur.IsCheckBySup == true
        //                group ur by new { u.Name, u.UserID } into g
        //                select new
        //                {
        //                    Name = g.Key.Name,
        //                    UserID = g.Key.UserID,
        //                    UserRiskID = g.FirstOrDefault().UserRiskID
        //                };

        //        grdCompany.DataSource = n;
        //        grdCompany.DataBind();
        //        LblSup.Text = "شده است";
        //    }

        //}
        //protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Risk")
        //    {
        //        int UserID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["UserID"] = UserID;

        //        mvRisk.SetActiveView(vwRiskView);
        //        LoadRiskGRD(UserID);

        //    }
        //}
        //public void LoadRiskGRD(int UserID)
        //{

        //    int IsVisited = Convert.ToInt32(ViewState["IsVisited"]);
        //    if (IsVisited == 0)
        //    {
        //        var n = from ur in db.UserRisks
        //                join r in db.Risks
        //                on ur.RiskID equals r.RiskID
        //                join rp in db.RiskProbabilities
        //                on ur.RiskProbabilityID equals rp.RiskProbabilityID
        //                join rp2 in db.RiskProbabilities
        //                on ur.RiskProbabilityID_AfterCO equals rp2.RiskProbabilityID
        //                join ri in db.RiskIntensities
        //                on ur.RiskIntensityID equals ri.RiskIntensityID
        //                join ri2 in db.RiskIntensities
        //                on ur.RiskIntensityID_AfterCO equals ri2.RiskIntensityID
        //                where ur.UserID_Company == UserID && ur.IsCheckBySup == false
        //                select new
        //                {
        //                    UserRiskID = ur.UserRiskID,
        //                    RiskTitle = r.RiskTitle,
        //                    RiskProbabilityTitle = rp.RiskProbabilityTitle,
        //                    RiskProbabilityTitle_after = rp2.RiskProbabilityTitle,
        //                    RiskIntensityTitle = ri.RiskIntensityTitle,
        //                    RiskIntensityTitle_after = ri2.RiskIntensityTitle,
        //                    IsCheckBySup = ur.IsCheckBySup,

        //                    RiskIntensityLevel = ri.RiskIntensityLevel,

        //                    RiskProbabilityLevel = rp.RiskProbabilityLevel,
        //                    RiskIntensityLevel_AfterCO = ri2.RiskIntensityLevel,
        //                    RiskProbabilityLevel_AfterCO = rp2.RiskProbabilityLevel
        //                };

        //        grdRisk.DataSource = n;
        //        grdRisk.DataBind();
        //    }
        //    else if (IsVisited == 1)
        //    {
        //        var n = from ur in db.UserRisks
        //                join r in db.Risks
        //                on ur.RiskID equals r.RiskID
        //                join rp in db.RiskProbabilities
        //                on ur.RiskProbabilityID equals rp.RiskProbabilityID
        //                join rp2 in db.RiskProbabilities
        //                on ur.RiskProbabilityID_AfterCO equals rp2.RiskProbabilityID
        //                join ri in db.RiskIntensities
        //                on ur.RiskIntensityID equals ri.RiskIntensityID
        //                join ri2 in db.RiskIntensities
        //                on ur.RiskIntensityID equals ri2.RiskIntensityID
        //                where ur.UserID_Company == UserID && ur.IsCheckBySup == true
        //                select new
        //                {
        //                    UserRiskID = ur.UserRiskID,
        //                    RiskTitle = r.RiskTitle,
        //                    RiskProbabilityTitle = rp.RiskProbabilityTitle,
        //                    RiskProbabilityTitle_after = rp2.RiskProbabilityTitle,
        //                    RiskIntensityTitle = ri.RiskIntensityTitle,
        //                    RiskIntensityTitle_after = ri2.RiskIntensityTitle,
        //                    IsCheckBySup = ur.IsCheckBySup,

        //                    RiskIntensityLevel = ri.RiskIntensityLevel,

        //                    RiskProbabilityLevel = rp.RiskProbabilityLevel,
        //                    RiskIntensityLevel_AfterCO = ri2.RiskIntensityLevel,
        //                    RiskProbabilityLevel_AfterCO = rp2.RiskProbabilityLevel
        //                };

        //        grdRisk.DataSource = n;
        //        grdRisk.DataBind();
        //    }
        //}
        //protected void grdRisk_RowDataBound1(object sender, GridViewRowEventArgs e)
        //{
        //    foreach (GridViewRow r in grdRisk.Rows)
        //    {
        //        HiddenField hfUserRisk = (HiddenField)r.FindControl("hfUserRisk");
        //        Label lblRisk = (Label)r.FindControl("lblRisk");
        //        Label lblRiskAfter = (Label)r.FindControl("lblRiskAfter");

        //        int UserRiskID = Convert.ToInt32(hfUserRisk.Value);

        //        var n = (from ur in db.UserRisks
        //                 where ur.UserRiskID == UserRiskID
        //                 select ur).FirstOrDefault();

        //        if (n != null)
        //        {
        //            var m = (from re in db.RiskEvaluations
        //                     where (re.RiskProbabilityID == n.RiskProbabilityID
        //                     && re.RiskIntensityID == n.RiskIntensityID)
        //                     select re).FirstOrDefault();

        //           // lblRisk.Text = m.RiskEvaluationNumber.ToString();

        //            if (m.RiskEvaluationNumber >= 1 &&
        //                     m.RiskEvaluationNumber <= 3)
        //            {
        //                lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
        //                    "قابل قبول بدون نیاز به بازنگری";
        //            }
        //            else if (m.RiskEvaluationNumber >= 4 &&
        //              m.RiskEvaluationNumber <= 11)
        //            {
        //                lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
        //                      "قابل قبول با نیاز به بازنگری";
        //            }
        //            else if (m.RiskEvaluationNumber >= 12 &&
        //               m.RiskEvaluationNumber <= 15)
        //            {
        //                lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
        //                    "نامطلوب ، نیاز به تصمیم گیری";
        //            }
        //            else if (m.RiskEvaluationNumber >= 16 &&
        //               m.RiskEvaluationNumber <= 20)
        //            {
        //                lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
        //                    "غیر قابل قبول";
        //            }


        //            var m2 = (from re in db.RiskEvaluations
        //                      where (re.RiskProbabilityID == n.RiskProbabilityID_AfterCO
        //                      && re.RiskIntensityID == n.RiskIntensityID_AfterCO)
        //                      select re).FirstOrDefault();

        //            if (m2.RiskEvaluationNumber >= 1 &&
        //                      m2.RiskEvaluationNumber <= 3)
        //            {
        //                lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
        //                    "قابل قبول بدون نیاز به بازنگری";
        //            }
        //            else if (m2.RiskEvaluationNumber >= 4 &&
        //              m2.RiskEvaluationNumber <= 11)
        //            {
        //                lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
        //                      "قابل قبول با نیاز به بازنگری";
        //            }
        //            else if (m2.RiskEvaluationNumber >= 12 &&
        //               m2.RiskEvaluationNumber <= 15)
        //            {
        //                lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
        //                    "نامطلوب ، نیاز به تصمیم گیری";
        //            }
        //            else if (m2.RiskEvaluationNumber >= 16 &&
        //               m2.RiskEvaluationNumber <= 20)
        //            {
        //                lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
        //                    "غیر قابل قبول";
        //            }

        //        }

        //    }

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HiddenField hfUserRisk = (HiddenField)e.Row.FindControl("hfUserRisk");

        //        int UserRiskID = Convert.ToInt32(hfUserRisk.Value);
        //        int UserRiskIDselected = Convert.ToInt32(ViewState["UserRiskID"]);

        //        if (UserRiskIDselected != 0)
        //        {

        //            if (UserRiskIDselected == UserRiskID)
        //            {
        //                e.Row.BackColor = Color.Khaki;

        //            }

        //        }
        //    }
        //}

        //protected void grdRisk_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Detail")
        //    {
        //        ViewState["UserRiskID"] = e.CommandArgument;
        //        int UserRiskID = Convert.ToInt32(e.CommandArgument);
        //        pnlRiskDetails.Visible = true;
        //        pnlRiskEdit.Visible = false;
        //        LoadDetail(UserRiskID);


        //        int UserID = Convert.ToInt32(ViewState["UserID"]);

        //        LoadRiskGRD(UserID);
        //    }
        //    else if (e.CommandName == "DoEdit")
        //    {
        //        ViewState["UserRiskID"] = e.CommandArgument;
        //        int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);

        //        pnlRiskEdit.Visible = true;
        //        pnlRiskDetails.Visible = false;
        //        LoadProbDDL(dlAfterProb);
        //        LoadProbDDL(dlBeforeProb);
        //        LoadIntDDL(dlBeforeInt);
        //        LoadIntDDL(dlAfterInt);
        //        LoadDropDownData();

        //        int UserID = Convert.ToInt32(ViewState["UserID"]);

        //        LoadRiskGRD(UserID);

        //    }
        //}

        //public void LoadDetail(int UserRiskID)
        //{
        //    var n = (from ru in db.UserRisks
        //             where ru.UserRiskID == UserRiskID
        //             select ru).FirstOrDefault();

        //    int RiskID = n.RiskID;


        //    var rr = (from r in db.Risks
        //              where r.RiskID == RiskID
        //              select r).FirstOrDefault();

        //    lblRiskName.Text = rr.RiskTitle;

        //    var ss = (from s in db.Stages
        //              where s.StageID == rr.StageID
        //              select s).FirstOrDefault();
        //    lblStageName.Text = ss.StageTitle;

        //    var o = (from op in db.Acts
        //             where op.ActID == ss.ActID
        //             select op).FirstOrDefault();

        //    lblActName.Text = o.ActTitle;

        //    var n2 = (from op in db.Operations
        //              where op.OperationID == o.OperationID
        //              select op).FirstOrDefault();

        //    lblOperationName.Text = n2.OperationTitle;

        //    var m = (from opg in db.OperationGroups
        //             where opg.OperationGroupID == n2.OperationGroupID
        //             select opg).FirstOrDefault();

        //    lblProjectName.Text = m.OperationGroupTitle;
        //    lblProName.Text = m.OperationGroupName;

        //    lblOpCode.Text = n2.CodeID.ToString();
        //    lblActCode.Text = o.CodeID.ToString();
        //}

        //public void LoadProbDDL(DropDownList ddlProb)
        //{
        //    var n = from p in db.RiskProbabilities
        //            select p;

        //    ddlProb.DataSource = n;
        //    ddlProb.DataValueField = "RiskProbabilityID";
        //    ddlProb.DataTextField = "RiskProbabilityTitle";
        //    ddlProb.DataBind();
        //}

        //public void LoadIntDDL(DropDownList ddlInt)
        //{
        //    var n = from p in db.RiskIntensities
        //            select p;

        //    ddlInt.DataSource = n;
        //    ddlInt.DataValueField = "RiskIntensityID";
        //    ddlInt.DataTextField = "RiskIntensityTitle";
        //    ddlInt.DataBind();
        //}

        //public void LoadDropDownData()
        //{
        //    int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);

        //    var n = (from ur in db.UserRisks
        //             where ur.UserRiskID == UserRiskID
        //             select ur).FirstOrDefault();

        //    dlBeforeProb.SelectedValue = n.RiskProbabilityID.ToString();
        //    dlAfterProb.SelectedValue = n.RiskProbabilityID_AfterCO.ToString();

        //    dlBeforeInt.SelectedValue = n.RiskIntensityID.ToString();
        //    dlAfterInt.SelectedValue = n.RiskIntensityID_AfterCO.ToString();
        //}

        //protected void btnInsertEdit_Click(object sender, EventArgs e)
        //{
        //    int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);

        //    var n = (from ur in db.UserRisks
        //             where ur.UserRiskID == UserRiskID
        //             select ur).FirstOrDefault();

        //    n.RiskIntensityID = Convert.ToInt32(dlBeforeInt.SelectedValue);
        //    n.RiskIntensityID_AfterCO = Convert.ToInt32(dlAfterInt.SelectedValue);
        //    n.RiskProbabilityID = Convert.ToInt32(dlBeforeProb.SelectedValue);
        //    n.RiskProbabilityID_AfterCO = Convert.ToInt32(dlAfterProb.SelectedValue);
        //    db.SaveChanges();

        //    int UserID = Convert.ToInt32(ViewState["UserID"]);

        //    LoadRiskGRD(UserID);

        //    pnlRiskEdit.Visible = false;
        //}

        //protected void btnRetCompanyList_Click(object sender, EventArgs e)
        //{
        //    mvRisk.SetActiveView(vwCompany);
        //}

        //protected void btnReturn_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Admin/Default.aspx");
        //}


    }
}