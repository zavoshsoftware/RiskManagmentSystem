﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;
using System.Drawing;
using System.IO;

namespace RiskManagementSystem.Companies
{
    public partial class RiskHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                LoadRiskGRD(UserID);
            }
        }
        public void LoadRiskGRD(int UserID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {

                var n = from ur in db.UserRisks
                        join r in db.Risks
                        on ur.RiskID equals r.RiskID
                        join rp in db.RiskProbabilities
                        on ur.RiskProbabilityID equals rp.RiskProbabilityID
                        join rp2 in db.RiskProbabilities
                        on ur.RiskProbabilityID_AfterCO equals rp2.RiskProbabilityID
                        join ri in db.RiskIntensities
                        on ur.RiskIntensityID equals ri.RiskIntensityID
                        join ri2 in db.RiskIntensities
                        on ur.RiskIntensityID_AfterCO equals ri2.RiskIntensityID
                        where ur.UserID_Company == UserID 
                        select new
                        {
                            UserRiskID = ur.UserRiskID,
                            RiskTitle = r.RiskTitle,
                            RiskProbabilityTitle = rp.RiskProbabilityTitle,
                            RiskProbabilityTitle_after = rp2.RiskProbabilityTitle,
                            RiskIntensityTitle = ri.RiskIntensityTitle,
                            RiskIntensityTitle_after = ri2.RiskIntensityTitle,
                            IsCheckBySup = ur.IsCheckBySup,
                            RiskIntensityLevel = ri.RiskIntensityLevel,
                            RiskProbabilityLevel = rp.RiskProbabilityLevel,
                            RiskIntensityLevel_AfterCO = ri2.RiskIntensityLevel,
                            RiskProbabilityLevel_AfterCO = rp2.RiskProbabilityLevel,
                            IsNotAvailable = ur.IsNotAvailable
                        };

                grdRisk.DataSource = n;
                grdRisk.DataBind();

            }
        }
        protected void grdRisk_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                ViewState["UserRiskID"] = e.CommandArgument;
                int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);
                pnlRiskDetails.Visible = true;
              
                LoadDetail(UserRiskID);

                int UserID = Convert.ToInt32(ViewState["UserID"]);

                LoadRiskGRD(UserID);
            }
            
        }

        public void LoadDetail(int UserRiskID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = (from ru in db.UserRisks
                         where ru.UserRiskID == UserRiskID
                         select ru).FirstOrDefault();

                int RiskID = n.RiskID;


                var rr = (from r in db.Risks
                          where r.RiskID == RiskID
                          select r).FirstOrDefault();

                lblRiskName.Text = rr.RiskTitle;

                var ss = (from s in db.Stages
                          where s.StageID == rr.StageID
                          select s).FirstOrDefault();
                lblStageName.Text = ss.StageTitle;

                var o = (from op in db.Acts
                         where op.ActID == ss.ActID
                         select op).FirstOrDefault();

                lblActName.Text = o.ActTitle;

                var n2 = (from op in db.Operations
                          where op.OperationID == o.OperationID
                          select op).FirstOrDefault();

                lblOperationName.Text = n2.OperationTitle;

                var m = (from opg in db.OperationGroups
                         where opg.OperationGroupID == n2.OperationGroupID
                         select opg).FirstOrDefault();

                lblProjectName.Text = m.OperationGroupTitle;
                lblProName.Text = m.OperationGroupName;

                lblOpCode.Text = n2.CodeID.ToString();
                lblActCode.Text = o.CodeID.ToString();
            }
        }
        protected void grdRisk_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow r in grdRisk.Rows)
            {
                HiddenField hfUserRisk = (HiddenField)r.FindControl("hfUserRisk");
                Label lblRisk = (Label)r.FindControl("lblRisk");
                Label lblRiskAfter = (Label)r.FindControl("lblRiskAfter");

                int UserRiskID = Convert.ToInt32(hfUserRisk.Value);
                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    var n = (from ur in db.UserRisks
                             where ur.UserRiskID == UserRiskID
                             select ur).FirstOrDefault();

                    if (n != null)
                    {
                        var m = (from re in db.RiskEvaluations
                                 where (re.RiskProbabilityID == n.RiskProbabilityID
                                 && re.RiskIntensityID == n.RiskIntensityID)
                                 select re).FirstOrDefault();

                        // lblRisk.Text = m.RiskEvaluationNumber.ToString();

                        if (m.RiskEvaluationNumber >= 1 &&
                                 m.RiskEvaluationNumber <= 3)
                        {
                            lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
                                "قابل قبول بدون نیاز به بازنگری";
                        }
                        else if (m.RiskEvaluationNumber >= 4 &&
                          m.RiskEvaluationNumber <= 11)
                        {
                            lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
                                  "قابل قبول با نیاز به بازنگری";
                        }
                        else if (m.RiskEvaluationNumber >= 12 &&
                           m.RiskEvaluationNumber <= 15)
                        {
                            lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
                                "نامطلوب ، نیاز به تصمیم گیری";
                        }
                        else if (m.RiskEvaluationNumber >= 16 &&
                           m.RiskEvaluationNumber <= 20)
                        {
                            lblRisk.Text = m.RiskEvaluationNumber.ToString() + "-" +
                                "غیر قابل قبول";
                        }


                        var m2 = (from re in db.RiskEvaluations
                                  where (re.RiskProbabilityID == n.RiskProbabilityID_AfterCO
                                  && re.RiskIntensityID == n.RiskIntensityID_AfterCO)
                                  select re).FirstOrDefault();

                        if (m2.RiskEvaluationNumber >= 1 &&
                                  m2.RiskEvaluationNumber <= 3)
                        {
                            lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
                                "قابل قبول بدون نیاز به بازنگری";
                        }
                        else if (m2.RiskEvaluationNumber >= 4 &&
                          m2.RiskEvaluationNumber <= 11)
                        {
                            lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
                                  "قابل قبول با نیاز به بازنگری";
                        }
                        else if (m2.RiskEvaluationNumber >= 12 &&
                           m2.RiskEvaluationNumber <= 15)
                        {
                            lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
                                "نامطلوب ، نیاز به تصمیم گیری";
                        }
                        else if (m2.RiskEvaluationNumber >= 16 &&
                           m2.RiskEvaluationNumber <= 20)
                        {
                            lblRiskAfter.Text = m2.RiskEvaluationNumber.ToString() + "-" +
                                "غیر قابل قبول";
                        }

                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfUserRisk = (HiddenField)e.Row.FindControl("hfUserRisk");

                int UserRiskID = Convert.ToInt32(hfUserRisk.Value);
                int UserRiskIDselected = Convert.ToInt32(ViewState["UserRiskID"]);

                if (UserRiskIDselected != 0)
                {

                    if (UserRiskIDselected == UserRiskID)
                    {
                        e.Row.BackColor = Color.Khaki;

                    }

                }
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "fdgh" + ".xls");
            Response.ContentType = "Files/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            this.EnableViewState = false;
            StringWriter sr = new StringWriter();
            HtmlTextWriter hr = new HtmlTextWriter(sr);
            grdRisk.RenderControl(hr);
            Response.Write(sr.ToString());
            sr.Dispose();
            hr.Dispose();
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void btnRet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Companies/default.aspx");
        }
    }
}