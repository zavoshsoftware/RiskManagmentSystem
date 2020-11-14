using RiskManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskManagementSystem.Companies
{
    public partial class AfterRiskEvalDetail : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblPageTitle.Text = "محاسبه شدت ریسک بعد از اقدامات کنترلی";
                int userRiskId = int.Parse(Request.QueryString["Id"].ToString());
                LoadLabels(userRiskId);
            }
        }
        private void LoadLabels(int userRiskId)
        {
            UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskId).FirstOrDefault();
            Risks risk = db.Risks.Where(current => current.RiskID == userRisk.RiskID && current.IsAcceptedByAdmin == true).FirstOrDefault();
            Stages stage = db.Stages.Where(current => current.StageID == risk.StageID).FirstOrDefault();
            Acts act = db.Acts.Where(current => current.ActID == stage.ActID).FirstOrDefault();
            Operations operation = db.Operations.Where(current => current.OperationID == act.OperationID).FirstOrDefault();
            OperationGroups operationGroup = db.OperationGroups.Where(current => current.OperationGroupID == operation.OperationGroupID).FirstOrDefault();
            lblProject.Text = operationGroup.OperationGroupTitle;
            lblOperation.Text = operation.OperationTitle;
            lblAct.Text = act.ActTitle;
            lblStage.Text = stage.StageTitle;
            ViewState["stageId"] = stage.StageID;
            LoadRiskgrd(stage.StageID);
        }
        public void LoadRiskgrd(int StageID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = from og in db.Risks
                        where og.StageID == StageID && og.IsAcceptedByAdmin == true
                        select og;
                grdRisks.DataSource = n;
                grdRisks.DataBind();

                foreach (GridViewRow r in grdRisks.Rows)
                {
                    HiddenField hfRiskId = (HiddenField)r.FindControl("hfRisk");
                    int riskId = Convert.ToInt32(hfRiskId.Value);
                    DropDownList ddlProb = (DropDownList)r.FindControl("ddlProb");
                    if (ddlProb != null)
                    {
                        LoadProbDDL(ddlProb);
                    }
                    DropDownList ddlInt = (DropDownList)r.FindControl("ddlInt");
                    if (ddlInt != null)
                    {
                        LoadIntDDL(ddlInt);
                    }

                    int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    var userRisk = (from ru in db.UserRisks
                                    where ru.UserID_Company == UserID && ru.RiskID == riskId
                                    select ru).FirstOrDefault();
                    Label lblProb = (Label)r.FindControl("lblBProb");
                    Label lblInt = (Label)r.FindControl("lblBInt");
                    Label lblRisk = (Label)r.FindControl("lblBRisk");
                    if (userRisk != null)
                    {
                        var riskProbability = db.RiskProbabilities.Where(current => current.RiskProbabilityID == userRisk.RiskProbabilityID).FirstOrDefault();
                        if (riskProbability != null)
                            lblProb.Text = riskProbability.RiskProbabilityTitle;

                        var riskIntensity = db.RiskIntensities.Where(current => current.RiskIntensityID == userRisk.RiskIntensityID).FirstOrDefault();
                        if (riskIntensity != null)
                            lblInt.Text = riskIntensity.RiskIntensityTitle;

                        var evaluation = (from re in db.RiskEvaluations
                                          where re.RiskIntensityID == userRisk.RiskIntensityID &&
                                                re.RiskProbabilityID == userRisk.RiskProbabilityID
                                          select re).FirstOrDefault();
                        if (evaluation != null)
                        {
                            if (evaluation.RiskEvaluationNumber >= 1 &&
                                evaluation.RiskEvaluationNumber <= 3)
                            {
                                lblRisk.Text = evaluation.RiskEvaluationNumber.ToString() + "-" +
                                    "قابل قبول بدون نیاز به بازنگری";
                            }
                            else if (evaluation.RiskEvaluationNumber >= 4 &&
                               evaluation.RiskEvaluationNumber <= 11)
                            {
                                lblRisk.Text = evaluation.RiskEvaluationNumber.ToString() + "-" +
                                      "قابل قبول با نیاز به بازنگری";
                            }
                            else if (evaluation.RiskEvaluationNumber >= 12 &&
                               evaluation.RiskEvaluationNumber <= 15)
                            {
                                lblRisk.Text = evaluation.RiskEvaluationNumber.ToString() + "-" +
                                    "نامطلوب ، نیاز به تصمیم گیری";
                            }
                            else if (evaluation.RiskEvaluationNumber >= 16 &&
                               evaluation.RiskEvaluationNumber <= 20)
                            {
                                lblRisk.Text = evaluation.RiskEvaluationNumber.ToString() + "-" +
                                    "غیر قابل قبول";
                            }
                        }
                    }
                    else
                    {
                        var riskProbability = db.RiskProbabilities.Where(current => current.RiskProbabilityLevel==1).FirstOrDefault();
                        if (riskProbability != null)
                            lblProb.Text = riskProbability.RiskProbabilityTitle;

                        var riskIntensity = db.RiskIntensities.Where(current => current.RiskIntensityLevel==1).FirstOrDefault();
                        if (riskIntensity != null)
                            lblInt.Text = riskIntensity.RiskIntensityTitle;
                    }
                }
                if (n.FirstOrDefault() != null)
                {
                    btnEvalRisk.Visible = true;
                    //btnExportToExcel.Visible = true;
                }
            }
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
        protected void btnEvalRisk_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow r in grdRisks.Rows)
            {
                HiddenField hfRisk = (HiddenField)(r.FindControl("hfRisk"));
                DropDownList ddlProb = (DropDownList)r.FindControl("ddlProb");
                Label lblRisk = (Label)(r.FindControl("lblRisk"));
                DropDownList ddlInt = (DropDownList)r.FindControl("ddlInt");
                int RiskIntensityID = Convert.ToInt32(ddlInt.SelectedValue);
                int RiskProbabilityID = Convert.ToInt32(ddlProb.SelectedValue);

                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    var n = (from re in db.RiskEvaluations
                             where re.RiskIntensityID == RiskIntensityID &&
                                   re.RiskProbabilityID == RiskProbabilityID
                             select re).FirstOrDefault();


                    if (n != null)
                    {
                        if (n.RiskEvaluationNumber >= 1 &&
                            n.RiskEvaluationNumber <= 3)
                        {
                            lblRisk.Text = n.RiskEvaluationNumber.ToString() + "-" +
                                "قابل قبول بدون نیاز به بازنگری";
                        }
                        else if (n.RiskEvaluationNumber >= 4 &&
                           n.RiskEvaluationNumber <= 11)
                        {
                            lblRisk.Text = n.RiskEvaluationNumber.ToString() + "-" +
                                  "قابل قبول با نیاز به بازنگری";
                        }
                        else if (n.RiskEvaluationNumber >= 12 &&
                           n.RiskEvaluationNumber <= 15)
                        {
                            lblRisk.Text = n.RiskEvaluationNumber.ToString() + "-" +
                                "نامطلوب ، نیاز به تصمیم گیری";
                        }
                        else if (n.RiskEvaluationNumber >= 16 &&
                           n.RiskEvaluationNumber <= 20)
                        {
                            lblRisk.Text = n.RiskEvaluationNumber.ToString() + "-" +
                                "غیر قابل قبول";
                        }
                    }


                }
            }
            btnInsert.Visible = true;
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string script = "alert('خطا')";
            try
            {
                //string QS = Request.QueryString["Id"].ToString();
                int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    foreach (GridViewRow r in grdRisks.Rows)
                    {
                        HiddenField hfRisk = (HiddenField)(r.FindControl("hfRisk"));
                        int RiskID = Convert.ToInt32(hfRisk.Value);
                        DropDownList ddlProb = (DropDownList)r.FindControl("ddlProb");
                        DropDownList ddlInt = (DropDownList)r.FindControl("ddlInt");
                        int RiskIntensityID = Convert.ToInt32(ddlInt.SelectedValue);
                        int RiskProbabilityID = Convert.ToInt32(ddlProb.SelectedValue);

                        var n = (from ru in db.UserRisks
                                 where ru.RiskID == RiskID
                                 select ru).FirstOrDefault();


                        if (n == null)
                        {
                            UserRisks u = new UserRisks()
                            {
                                UserID_Company = UserID,
                                RiskID = RiskID,
                                RiskIntensityID_AfterCO = RiskIntensityID,
                                RiskProbabilityID_AfterCO = RiskProbabilityID,
                                IsCheckByAdmin = false,
                                IsCheckBySup = false,
                                CreationDate = DateTime.Now,
                                StatusId = 1,
                            };
                            db.UserRisks.AddObject(u);
                        }
                        else
                        {
                            //if (n.StatusId == 1)
                            //{
                            n.RiskIntensityID_AfterCO = RiskIntensityID;
                            n.RiskProbabilityID_AfterCO = RiskProbabilityID;
                            n.LastModifationDate = DateTime.Now;
                            //}
                            //else
                            //{
                            //    script =
                            //        "alert('ریسک های این مرحله پیش از این توسط ناظر بررسی شده است و امکان تغییر وجود ندارد');";

                            //    break;
                            //}
                        }


                        script = "alert('ثبت اطلاعات با موفقیت انجام شد');";
                    }

                    db.SaveChanges();
                }
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Test5", script, true);

            }
            catch (Exception exception)
            {
                script = "alert('در فرآیند ثبت اطلاعات خطایی رخ داده است. لطفا مجددا اطلاعات را وارد نمایید.');";

                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Test", script, true);
            }
        }

        //protected void ExportToExcel()
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //        //To Export all pages
        //        grdRisks.AllowPaging = false;
        //        var stageId = int.Parse(ViewState["stageId"].ToString());
        //        this.LoadRiskgrd(stageId);

        //        grdRisks.HeaderRow.BackColor = Color.White;
        //        foreach (TableCell cell in grdRisks.HeaderRow.Cells)
        //        {
        //            cell.BackColor = grdRisks.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in grdRisks.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = grdRisks.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = grdRisks.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        grdRisks.RenderControl(hw);

        //        //style to format numbers to string
        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
    }
}