using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RiskManagementSystem.Model;
using System.Drawing;
using System.IO;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RiskManagementSystem.Companies
{

    public partial class RiskEval : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblPageTitle.Text = "محاسبه شدت ریسک قبل از اقدامات کنترلی";
                
                if (Request.QueryString["Id"] != null)
                {
                    ViewState["RiskID"] = Request.QueryString["Id"];
                    int RiskID = Convert.ToInt32(ViewState["RiskID"]);
                    var n = (from a in db.Risks
                             where a.RiskID == RiskID
                             select a).FirstOrDefault();
                    ReturnDropDowns(n.StageID);
                    LoadRiskgrd(n.StageID);
                    lblProtectionEQP.Visible = true;
                    lblCurses.Visible = true;
                }
                else
                {
                    lblProtectionEQP.Visible = false;
                    lblCurses.Visible = false;
                    LoadProjectDDL();
                }
                //string QS = Request.QueryString["ID"].ToString();
                //if (QS == "Before")
                //{
                //    lblPageTitle.Text = "محاسبه شدت ریسک قبل از اقدامات کنترلی";
                //}
                //else if (QS == "After")
                //{
                //    lblPageTitle.Text = "محاسبه شدت ریسک بعد از اقدامات کنترلی";
                //}
            }

        }
        public void LoadRiskgrd(int StageID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                List<Risks> n = db.Risks
                    .Where(current => current.StageID == StageID && current.IsAcceptedByAdmin == true).ToList();


                grdRisks.DataSource = n;
                grdRisks.DataBind();

                foreach (GridViewRow r in grdRisks.Rows)
                {
                    HiddenField hfRiskId = (HiddenField)r.FindControl("hfRisk");
                    int riskId = Convert.ToInt32(hfRiskId.Value);
                    DropDownList ddlProb = (DropDownList)r.FindControl("ddlProb");
                    if (ddlProb != null)
                    {
                        LoadProbDDL(ddlProb,riskId);
                    }
                    DropDownList ddlInt = (DropDownList)r.FindControl("ddlInt");
                    if (ddlInt != null)
                    {
                        LoadIntDDL(ddlInt, riskId);
                    }
                }
                if (n.FirstOrDefault() != null)
                {
                    btnEvalRisk.Visible = true;
                    //btnExportToExcel.Visible = true;
                }
            }
        }

        #region LoadReturnDropDown
        public void ReturnDropDowns(int stageId)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var stage = (from s in db.Stages
                             where s.StageID == stageId
                             select s).FirstOrDefault();
                ddlStage.Items.Clear();
                ddlStage.Items.Add(new ListItem(stage.StageTitle, stage.StageID.ToString()));

                var act = (from op in db.Acts
                           where op.ActID == stage.ActID
                           select op).FirstOrDefault();
                ddlAct.Items.Clear();
                ddlAct.Items.Add(new ListItem(act.ActTitle, act.ActID.ToString()));
                lblProtectionEQP.Text = "وسایل حفاظت فردی: " + act.ProtectionEQP;
                lblProtectionEQP.Visible = true;

                lblCurses.Text = "دوره های آموزشی: " + act.Curses;
                lblCurses.Visible = true;

                var operation = (from op in db.Operations
                                 where op.OperationID == act.OperationID
                                 select op).FirstOrDefault();
                ddlOperation.Items.Clear();
                ddlOperation.Items.Add(new ListItem(operation.OperationTitle, operation.OperationID.ToString()));

                var operationGroup = (from u in db.OperationGroups
                                      where u.OperationGroupID == operation.OperationGroupID
                                      select u).FirstOrDefault();
                var operationGroupList = from op in db.OperationGroups
                        select op;
                ddlProject.Items.Clear();
                ddlProject.Items.Add(new ListItem(operationGroup.OperationGroupTitle, operationGroup.OperationGroupID.ToString()));
                foreach (var i in operationGroupList)
                    ddlProject.Items.Add(new ListItem(i.OperationGroupTitle, i.OperationGroupID.ToString()));
            }
        }
        //public void ReturnOperationDDL(int ProjectID)
        //{
        //    using (RiskManagementEntities db = new RiskManagementEntities())
        //    {
        //        var m = from op in db.Operations
        //                where op.OperationGroupID == ProjectID
        //                select op;
        //        ddlOperation.Items.Clear();
        //        ddlOperation.Items.Add(new ListItem("عملیات ", "-1"));
        //        foreach (var i in m)
        //            ddlOperation.Items.Add(new ListItem(i.OperationTitle, i.OperationID.ToString()));
        //    }
        //}
        //public void ReturnActDDL(int OperationID)
        //{
        //    using (RiskManagementEntities db = new RiskManagementEntities())
        //    {
        //        var m = from op in db.Acts
        //                where op.OperationID == OperationID
        //                select op;
        //        ddlAct.Items.Clear();
        //        ddlAct.Items.Add(new ListItem("فعالیت ", "-1"));
        //        foreach (var i in m)
        //            ddlAct.Items.Add(new ListItem(i.ActTitle, i.ActID.ToString()));
        //    }
        //}
        //public void ReturnStageDDL(int ActID)
        //{
        //    using (RiskManagementEntities db = new RiskManagementEntities())
        //    {
        //        var m = from op in db.Stages
        //                where op.ActID == ActID
        //                select op;
        //        ddlStage.Items.Clear();
        //        ddlStage.Items.Add(new ListItem("مرحله ", "-1"));
        //        foreach (var i in m)
        //            ddlStage.Items.Add(new ListItem(i.StageTitle, i.StageID.ToString()));

        //    }
        //}
        #endregion
        #region LoadDropDowns
        public void LoadProjectDDL()
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var m = from op in db.OperationGroups
                        select op;
                ddlProject.Items.Clear();
                ddlProject.Items.Add(new ListItem("پروژه ", "-1"));
                foreach (var i in m)
                    ddlProject.Items.Add(new ListItem(i.OperationGroupTitle, i.OperationGroupID.ToString()));
            }
        }
        public void LoadOperationDDL(int ProjectID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var m = from op in db.Operations
                        where op.OperationGroupID == ProjectID
                        select op;
                ddlOperation.Items.Clear();
                ddlOperation.Items.Add(new ListItem("عملیات ", "-1"));
                foreach (var i in m)
                    ddlOperation.Items.Add(new ListItem(i.OperationTitle, i.OperationID.ToString()));
            }
        }
        public void LoadActDDL(int OperationID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var m = from op in db.Acts
                        where op.OperationID == OperationID
                        select op;
                ddlAct.Items.Clear();
                ddlAct.Items.Add(new ListItem("فعالیت ", "-1"));
                foreach (var i in m)
                    ddlAct.Items.Add(new ListItem(i.ActTitle, i.ActID.ToString()));
            }
        }
        public void LoadStageDDL(int ActID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var m = from op in db.Stages
                        where op.ActID == ActID
                        select op;
                ddlStage.Items.Clear();
                ddlStage.Items.Add(new ListItem("مرحله ", "-1"));
                foreach (var i in m)
                    ddlStage.Items.Add(new ListItem(i.StageTitle, i.StageID.ToString()));

            }
        }
        public void LoadProbDDL(DropDownList ddlProb,int riskId)
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

                int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var userRisk = (from ru in db.UserRisks
                                where ru.UserID_Company == UserID && ru.RiskID == riskId
                                select ru).FirstOrDefault();
                if(userRisk!=null)
                {
                    ddlProb.SelectedValue = userRisk.RiskProbabilityID.ToString();
                }
            }
        }
        public void LoadIntDDL(DropDownList ddlInt,int riskId)
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

                int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                var userRisk = (from ru in db.UserRisks
                                where ru.UserID_Company == UserID && ru.RiskID == riskId
                                select ru).FirstOrDefault();
                if (userRisk != null)
                {
                    ddlInt.SelectedValue = userRisk.RiskIntensityID.ToString();
                }
            }
        }
        #endregion
        #region DropDown_SelectedIndexChanged
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProjectID = Convert.ToInt32(ddlProject.SelectedValue);
            LoadOperationDDL(ProjectID);
        }
        protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int OperationID = Convert.ToInt32(ddlOperation.SelectedValue);
            LoadActDDL(OperationID);
        }
        protected void ddlAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ActID = Convert.ToInt32(ddlAct.SelectedValue);
            LoadStageDDL(ActID);
            Acts act = db.Acts.Where(current => current.ActID == ActID).FirstOrDefault();
            lblProtectionEQP.Text = "<b style='color:#428bca;'>وسایل حفاظت فردی:</b> " + act.ProtectionEQP;
            lblProtectionEQP.Visible = true;

            lblCurses.Text = "<b style='color:#428bca;'>دوره های آموزشی:</b> " + act.Curses;
            lblCurses.Visible = true;

        }
        protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StageID = Convert.ToInt32(ddlStage.SelectedValue);
            //btnshowEducation.Visible = true;
            btnAddRisk.Visible = true;
            ViewState["stageId"] = StageID;
            LoadRiskgrd(StageID);
        }
        #endregion
        #region Buttons_Click
        protected void btnRet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Companies/Default.aspx");
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
                //string QS = Request.QueryString["ID"].ToString();
                int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    foreach (GridViewRow r in grdRisks.Rows)
                    {
                        HiddenField hfRisk = (HiddenField)(r.FindControl("hfRisk"));
                        int RiskID = Convert.ToInt32(hfRisk.Value);

                        DropDownList ddlProb = (DropDownList)r.FindControl("ddlProb");

                        //   Label lblRisk = (Label)(r.FindControl("lblRisk"));

                        DropDownList ddlInt = (DropDownList)r.FindControl("ddlInt");

                        int RiskIntensityID = Convert.ToInt32(ddlInt.SelectedValue);
                        int RiskProbabilityID = Convert.ToInt32(ddlProb.SelectedValue);


                        var n = (from ru in db.UserRisks
                                 where ru.UserID_Company == UserID && ru.RiskID == RiskID
                                 select ru).FirstOrDefault();

                        //if (QS == "Before")
                        //{
                        if (n == null)
                        {
                            UserRisks u = new UserRisks()
                            {
                                UserID_Company = UserID,
                                RiskID = RiskID,
                                RiskIntensityID = RiskIntensityID,
                                RiskProbabilityID = RiskProbabilityID,
                                IsCheckBySup = false,
                                CreationDate = DateTime.Now,
                                StatusId = 1,
                            };
                            db.UserRisks.AddObject(u);
                        }
                        else
                        {
                            if (n.StatusId == 1)
                            {
                                n.RiskIntensityID = RiskIntensityID;
                                n.RiskProbabilityID = RiskProbabilityID;
                                n.LastModifationDate = DateTime.Now;


                            }
                            else
                            {
                                script =
                                    "alert('ریسک های این مرحله پیش از این توسط ناظر بررسی شده است و امکان تغییر وجود ندارد');";

                                break;
                            }
                        }
                        //}

                        //else
                        //{
                        //    if (n == null)
                        //    {
                        //        UserRisks u = new UserRisks()
                        //        {
                        //            UserID_Company = UserID,
                        //            RiskID = RiskID,
                        //            RiskIntensityID_AfterCO = RiskIntensityID,
                        //            RiskProbabilityID_AfterCO = RiskProbabilityID,
                        //            IsCheckByAdmin = false,
                        //            IsCheckBySup = false,
                        //            CreationDate = DateTime.Now,
                        //            StatusId = 1,
                        //        };
                        //        db.UserRisks.AddObject(u);
                        //    }
                        //    else
                        //    {
                        //        if (n.StatusId == 1)
                        //        {
                        //            n.RiskIntensityID_AfterCO = RiskIntensityID;
                        //            n.RiskProbabilityID_AfterCO = RiskProbabilityID;
                        //            n.LastModifationDate = DateTime.Now;
                        //        }
                        //        else
                        //        {
                        //            script =
                        //                "alert('ریسک های این مرحله پیش از این توسط ناظر بررسی شده است و امکان تغییر وجود ندارد');";

                        //            break;
                        //        }
                        //    }
                        //}

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
        //protected void btnshowEducation_Click(object sender, EventArgs e)
        //{
        //    grdEducationBind();
        //}
        protected void btnBack_Click(object sender, EventArgs e)
        {
            mvRisk.SetActiveView(vwList);
        }
        //protected void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    ExportToExcel();
        //}
        protected void btnReturnEducation_Click(object sender, EventArgs e)
        {
            mvRisk.SetActiveView(vwEducation);
        }
        protected void btnAddRisk_Click(object sender, EventArgs e)
        {
            txtRiskTitle.Text = string.Empty;
            cbIsNormal.Checked = false;
            mvRisk.SetActiveView(vwInsertRisk);
        }
        protected void btnInsertRisk_Click(object sender, EventArgs e)
        {
            var stageId = int.Parse(ViewState["stageId"].ToString());
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                Risks risk = new Risks()
                {
                    StageID = stageId,
                    RiskTitle = txtRiskTitle.Text,
                    IsNormal = cbIsNormal.Checked,
                    IsAcceptedByAdmin = false,
                    UniqueId = GenerateUniqueId(),
                    CodeID = 0
                };
                db.Risks.AddObject(risk);
                db.SaveChanges();
            }
            mvRisk.SetActiveView(vwList);
        }
        #endregion
        protected void grdRisks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Control")
            {
                int RiskID = Convert.ToInt32(e.CommandArgument);

                Response.Redirect("~/Companies/ViewRiskControl.aspx?ID=" + RiskID);
            }
        }
        //public void grdEducationBind()
        //{
        //    var stageId = int.Parse(ddlStage.SelectedValue);
        //    using (RiskManagementEntities db = new RiskManagementEntities())
        //    {
        //        var educations = (from e in db.Education
        //                          join s in db.Stages on e.StageId equals s.StageID
        //                          where e.IsDelete == false && e.StageId == stageId
        //                          select new
        //                          {
        //                              e.Id,
        //                              e.StageId,
        //                              e.Title,
        //                              s.StageTitle
        //                          }).ToList();
        //        grdEducation.DataSource = educations;
        //        grdEducation.DataBind();
        //    }
        //    mvRisk.SetActiveView(vwEducation);
        //}
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
        //        this.LoadExcelRiskgrd(stageId);

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
        //        string style = @"<style> .textmode {text-align:right;direction:rtl; } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}
        protected void grdEducation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int educationId = Convert.ToInt32(e.CommandArgument);
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var education = db.Education.Where(current => current.Id == educationId).FirstOrDefault();
                lblTitle.Text = education.Title;
                lblBody.Text = education.Body;
            }
            mvRisk.SetActiveView(vwEducationDetail);
        }
        private int GenerateUniqueId()
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                int uniqueId = 1000;
                var risk = (from r in db.Risks
                            orderby r.UniqueId descending
                            select r).FirstOrDefault();
                if (risk.UniqueId >= 1000)
                {
                    uniqueId = risk.UniqueId + 1;
                }
                return uniqueId;
            }
        }
    }
}