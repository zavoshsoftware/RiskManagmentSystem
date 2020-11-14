using RiskManagementSystem.Model;
using RiskManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskManagementSystem
{
    public partial class ExcelTest : System.Web.UI.Page
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
            List<RiskHistoryViewModel> riskHistory = new List<RiskHistoryViewModel>();
            List<UserRisks> userRisks = db.UserRisks.Where(current => current.UserID_Company == userId && (current.RiskIntensityID_AfterCO == null || current.RiskProbabilityID_AfterCO == null))
               .OrderBy(current => current.CreationDate).ThenBy(current => current.UserRiskID).ToList();
            foreach (UserRisks userRisk in userRisks)
            {
                if (riskHistory.All(current => current.StageId != userRisk.Risks.StageID))
                {
                    riskHistory.Add(new RiskHistoryViewModel()
                    {
                        StageTitle = userRisk.Risks.Stages.StageTitle,
                        UserRiskId = userRisk.UserRiskID,
                        ActTitle = userRisk.Risks.Stages.Acts.ActTitle,
                        OperationTitle = userRisk.Risks.Stages.Acts.Operations.OperationTitle,
                        OperationGroupTitle = userRisk.Risks.Stages.Acts.Operations.OperationGroups.OperationGroupTitle,
                        StageId = userRisk.Risks.StageID
                    });
                }
            }
            grdRisks.DataSource = riskHistory;
            grdRisks.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void ExportToExcel()
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.ContentType = "application/excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdRisks.AllowPaging = false;

                this.GridViewDataSource();

                grdRisks.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdRisks.HeaderRow.Cells)
                {
                    cell.BackColor = grdRisks.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdRisks.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdRisks.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdRisks.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdRisks.RenderControl(hw);



                //style to format numbers to string

                string style = @"<style> .textmode {} </style>";
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


        protected void grdRisks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStageId = (HiddenField)e.Row.FindControl("hfStageId");
                int stageId = int.Parse(hfStageId.Value.ToString());
                //UserRisks userRisk = db.UserRisks.Where(current => current.UserRiskID == userRiskID).FirstOrDefault();
                List<Risks> risks = db.Risks.Where(current => current.StageID == stageId).ToList();
                Label lblControl = (Label)e.Row.FindControl("lblControl");
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
                        i = i+1;
                        controlText = controlText + "</br>" +(i)+") "+ item.ControlTitle;
                        lblControl.Text = controlText;
                    }
                    
                }

               
            }
        }
    }
}