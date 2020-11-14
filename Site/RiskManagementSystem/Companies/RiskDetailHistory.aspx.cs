using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;
using System.IO;
using System.Drawing;

namespace RiskManagementSystem.Companies
{
    public partial class RiskDetailHistory : System.Web.UI.Page
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
        #region GridView
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
                userRisks = db.UserRisks
                .Where(current => current.UserID_Company == userId).ToList();

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
                    UniqueId = userRisk.Risks.UniqueId,
                    UserRiskID = userRisk.UserRiskID,
                    RiskAfterProbabilityTitle = riskAfterProbabilityTitle,
                    RiskAfterIntensityTitle = riskAfterIntensityTitle,
                    RiskAfterEvaluationTitle = riskAfterEvaluationTitle

                });
            }

            grdTable.DataSource = riskDetails;
            grdTable.DataBind();
        }
        private void UpdateGrdRisks(int? projectId, int? operationId, int? actId, int? stageId)
        {
            if (stageId == null)
            {
                if (actId == null)
                {
                    if (operationId == null)
                    {
                        ViewState["projectId"] = projectId;
                        List<UserRisks> userRisks = (from u in db.UserRisks
                                                     join r in db.Risks on u.RiskID equals r.RiskID
                                                     join s in db.Stages on r.StageID equals s.StageID
                                                     join a in db.Acts on s.ActID equals a.ActID
                                                     join p in db.Operations on a.OperationID equals p.OperationID
                                                     where p.OperationGroupID == projectId
                                                     select u).ToList();
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
                        List<UserRisks> userRisks = (from u in db.UserRisks
                                                     join r in db.Risks on u.RiskID equals r.RiskID
                                                     join s in db.Stages on r.StageID equals s.StageID
                                                     join a in db.Acts on s.ActID equals a.ActID
                                                     where a.OperationID == operationId
                                                     select u).ToList();

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
                    List<UserRisks> userRisks = (from u in db.UserRisks
                                                 join r in db.Risks on u.RiskID equals r.RiskID
                                                 join s in db.Stages on r.StageID equals s.StageID
                                                 where s.ActID == actId
                                                 select u).ToList();

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
                List<UserRisks> userRisks = (from u in db.UserRisks
                                             join r in db.Risks on u.RiskID equals r.RiskID
                                             where r.StageID == stageId
                                             select u).ToList();

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

        private void ExcelGridRisks(int stageId)
        {
            ViewState["stageId"] = stageId;
            List<UserRisks> userRisks = (from u in db.UserRisks
                                         join r in db.Risks on u.RiskID equals r.RiskID
                                         where r.StageID == stageId && u.RiskProbabilities.RiskProbabilityLevel != 1
                                         && u.RiskIntensities.RiskIntensityLevel != 1 && u.RiskProbabilities1.RiskProbabilityLevel != 1 && u.RiskIntensities1.RiskIntensityLevel != 1
                                         select u).ToList();

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
        #endregion
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
        #region DropDown_SelectedIndexChanged
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
        #endregion
        #region DropDownBind
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
        #endregion
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

                if (ViewState["stageId"] != null)
                {
                    var stageId = int.Parse(ViewState["stageId"].ToString());
                    this.ExcelGridRisks(stageId);
                }
                else if (ViewState["actId"] != null)
                {
                    var actId = int.Parse(ViewState["actId"].ToString());
                    this.UpdateGrdRisks(null, null, actId, null);
                }
                else if (ViewState["operationId"] != null)
                {
                    var operationId = int.Parse(ViewState["operationId"].ToString());
                    this.UpdateGrdRisks(null, operationId, null, null);
                }
                else if (ViewState["projectId"] != null)
                {
                    var projectId = int.Parse(ViewState["projectId"].ToString());
                    this.UpdateGrdRisks(projectId, null, null, null);
                }
                else
                {
                    List<UserRisks> userRisks = new List<UserRisks>();
                    GridViewDataSource(userRisks);
                }
                //this.LoadRiskgrd(stageId);

                grdTable.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdTable.HeaderRow.Cells)
                {
                    cell.BackColor = grdTable.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdTable.Rows)
                {
                    Label lblControl = (Label)row.FindControl("lblControl");
                    if (lblControl != null)
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
                            if (lblControl != null)
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
                grdTable.Columns[9].HeaderText = "اقدامات کنترلی";
                grdTable.RenderControl(hw);



                //style to format numbers to string

                string style = @"<style> .textmode {direction:rtl;}.table,.table > tr, .table > thead > tr > th, .table > tbody > tr > td{float:right;direction:rtl;}</style>";
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
            List<UserRisks> userRisks = userRisks = db.UserRisks
                .Where(current => current.UserID_Company == userId).ToList();
            //if (degreeId == 0)
            //{
            //    grdTable.Visible = true;
            //    GridViewDataSource(userRisks);
            //}
            //else
            //{
            if (ViewState["stageId"] != null)
            {
                var stageId = int.Parse(ViewState["stageId"].ToString());
                userRisks = (from u in db.UserRisks
                             join r in db.Risks on u.RiskID equals r.RiskID
                             where r.StageID == stageId
                             select u).ToList();
            }
            else if (ViewState["actId"] != null)
            {
                var actId = int.Parse(ViewState["actId"].ToString());
                userRisks = (from u in db.UserRisks
                             join r in db.Risks on u.RiskID equals r.RiskID
                             join s in db.Stages on r.StageID equals s.StageID
                             where s.ActID == actId
                             select u).ToList();
            }
            else if (ViewState["operationId"] != null)
            {
                var operationId = int.Parse(ViewState["operationId"].ToString());
                userRisks = (from u in db.UserRisks
                             join r in db.Risks on u.RiskID equals r.RiskID
                             join s in db.Stages on r.StageID equals s.StageID
                             join a in db.Acts on s.ActID equals a.ActID
                             where a.OperationID == operationId
                             select u).ToList();
            }
            else if (ViewState["projectId"] != null)
            {
                var projectId = int.Parse(ViewState["projectId"].ToString());
                userRisks = (from u in db.UserRisks
                             join r in db.Risks on u.RiskID equals r.RiskID
                             join s in db.Stages on r.StageID equals s.StageID
                             join a in db.Acts on s.ActID equals a.ActID
                             join p in db.Operations on a.OperationID equals p.OperationID
                             where p.OperationGroupID == projectId
                             select u).ToList();
            }

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