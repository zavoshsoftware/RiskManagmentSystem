using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using System.Drawing;

namespace RiskManagementSystem.Admin
{
    public partial class RiskView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["IsVisited"] = Request.QueryString["ID"];
                LoadCompanies();
            }
        }
        public void LoadCompanies()
        {
            int IsVisited = Convert.ToInt32(ViewState["IsVisited"]);
            if (IsVisited == 0)
            {
                var n = from ur in DataContext.Context.UserRisks
                        join u in DataContext.Context.Users
                        on ur.UserID_Company equals u.UserID
                        where ur.IsCheckBySup == false
                        group ur by new { u.Name, u.UserID } into g
                        select new
                       {
                           Name = g.Key.Name,
                           UserID = g.Key.UserID,
                           UserRiskID = g.FirstOrDefault().UserRiskID
                       };

                grdCompany.DataSource = n;
                grdCompany.DataBind();
                LblSup.Text = "نشده است";
            }
            else if (IsVisited == 1)
            {
                var n = from ur in DataContext.Context.UserRisks
                        join u in DataContext.Context.Users
                        on ur.UserID_Company equals u.UserID
                        where ur.IsCheckBySup == true
                        group ur by new { u.Name, u.UserID } into g
                        select new
                        {
                            Name = g.Key.Name,
                            UserID = g.Key.UserID,
                            UserRiskID = g.FirstOrDefault().UserRiskID
                        };

                grdCompany.DataSource = n;
                grdCompany.DataBind();
                LblSup.Text = "شده است";
            }

        }
        protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Risk")
            {
                int UserID = Convert.ToInt32(e.CommandArgument);
                ViewState["UserID"] = UserID;

                mvRisk.SetActiveView(vwRiskView);
                LoadRiskGRD(UserID);

            }
        }
        public void LoadRiskGRD(int UserID)
        {

            int IsVisited = Convert.ToInt32(ViewState["IsVisited"]);
            if (IsVisited == 0)
            {
                var n = from ur in DataContext.Context.UserRisks
                        join r in DataContext.Context.Risks
                        on ur.RiskID equals r.RiskID
                        join rp in DataContext.Context.RiskProbabilities
                        on ur.RiskProbabilityID equals rp.RiskProbabilityID
                        join rp2 in DataContext.Context.RiskProbabilities
                        on ur.RiskProbabilityID_AfterCO equals rp2.RiskProbabilityID
                        join ri in DataContext.Context.RiskIntensities
                        on ur.RiskIntensityID equals ri.RiskIntensityID
                        join ri2 in DataContext.Context.RiskIntensities
                        on ur.RiskIntensityID_AfterCO equals ri2.RiskIntensityID
                        where ur.UserID_Company == UserID && ur.IsCheckBySup == false
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
                            RiskProbabilityLevel_AfterCO = rp2.RiskProbabilityLevel
                        };

                grdRisk.DataSource = n;
                grdRisk.DataBind();
            }
            else if (IsVisited == 1)
            {
                var n = from ur in DataContext.Context.UserRisks
                        join r in DataContext.Context.Risks
                        on ur.RiskID equals r.RiskID
                        join rp in DataContext.Context.RiskProbabilities
                        on ur.RiskProbabilityID equals rp.RiskProbabilityID
                        join rp2 in DataContext.Context.RiskProbabilities
                        on ur.RiskProbabilityID_AfterCO equals rp2.RiskProbabilityID
                        join ri in DataContext.Context.RiskIntensities
                        on ur.RiskIntensityID equals ri.RiskIntensityID
                        join ri2 in DataContext.Context.RiskIntensities
                        on ur.RiskIntensityID equals ri2.RiskIntensityID
                        where ur.UserID_Company == UserID && ur.IsCheckBySup == true
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
                            RiskProbabilityLevel_AfterCO = rp2.RiskProbabilityLevel
                        };

                grdRisk.DataSource = n;
                grdRisk.DataBind();
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

                var n = (from ur in DataContext.Context.UserRisks
                         where ur.UserRiskID == UserRiskID
                         select ur).FirstOrDefault();

                if (n != null)
                {
                    var m = (from re in DataContext.Context.RiskEvaluations
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


                    var m2 = (from re in DataContext.Context.RiskEvaluations
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

        protected void grdRisk_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                ViewState["UserRiskID"] = e.CommandArgument;
                int UserRiskID = Convert.ToInt32(e.CommandArgument);
                pnlRiskDetails.Visible = true;
                pnlRiskEdit.Visible = false;
                LoadDetail(UserRiskID);


                int UserID = Convert.ToInt32(ViewState["UserID"]);

                LoadRiskGRD(UserID);
            }
            else if (e.CommandName == "DoEdit")
            {
                ViewState["UserRiskID"] = e.CommandArgument;
                int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);

                pnlRiskEdit.Visible = true;
                pnlRiskDetails.Visible = false;
                LoadProbDDL(dlAfterProb);
                LoadProbDDL(dlBeforeProb);
                LoadIntDDL(dlBeforeInt);
                LoadIntDDL(dlAfterInt);
                LoadDropDownData();

                int UserID = Convert.ToInt32(ViewState["UserID"]);

                LoadRiskGRD(UserID);

            }
        }

        public void LoadDetail(int UserRiskID)
        {
            var n = (from ru in DataContext.Context.UserRisks
                     where ru.UserRiskID == UserRiskID
                     select ru).FirstOrDefault();

            int RiskID = n.RiskID;


            var rr = (from r in DataContext.Context.Risks
                      where r.RiskID == RiskID
                      select r).FirstOrDefault();

            lblRiskName.Text = rr.RiskTitle;

            var ss = (from s in DataContext.Context.Stages
                      where s.StageID == rr.StageID
                      select s).FirstOrDefault();
            lblStageName.Text = ss.StageTitle;

            var o = (from op in DataContext.Context.Acts
                     where op.ActID == ss.ActID
                     select op).FirstOrDefault();

            lblActName.Text = o.ActTitle;

            var n2 = (from op in DataContext.Context.Operations
                      where op.OperationID == o.OperationID
                      select op).FirstOrDefault();

            lblOperationName.Text = n2.OperationTitle;

            var m = (from opg in DataContext.Context.OperationGroups
                     where opg.OperationGroupID == n2.OperationGroupID
                     select opg).FirstOrDefault();

            lblProjectName.Text = m.OperationGroupTitle;
            lblProName.Text = m.OperationGroupName;

            lblOpCode.Text = n2.CodeID.ToString();
            lblActCode.Text = o.CodeID.ToString();
        }
      
        public void LoadProbDDL(DropDownList ddlProb)
        {
            var n = from p in DataContext.Context.RiskProbabilities
                    select p;

            ddlProb.DataSource = n;
            ddlProb.DataValueField = "RiskProbabilityID";
            ddlProb.DataTextField = "RiskProbabilityTitle";
            ddlProb.DataBind();
        }

        public void LoadIntDDL(DropDownList ddlInt)
        {
            var n = from p in DataContext.Context.RiskIntensities
                    select p;

            ddlInt.DataSource = n;
            ddlInt.DataValueField = "RiskIntensityID";
            ddlInt.DataTextField = "RiskIntensityTitle";
            ddlInt.DataBind();
        }

        public void LoadDropDownData()
        {
            int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);

            var n = (from ur in DataContext.Context.UserRisks
                     where ur.UserRiskID == UserRiskID
                     select ur).FirstOrDefault();

            dlBeforeProb.SelectedValue = n.RiskProbabilityID.ToString();
            dlAfterProb.SelectedValue = n.RiskProbabilityID_AfterCO.ToString();

            dlBeforeInt.SelectedValue = n.RiskIntensityID.ToString();
            dlAfterInt.SelectedValue = n.RiskIntensityID_AfterCO.ToString();
        }

        protected void btnInsertEdit_Click(object sender, EventArgs e)
        {
            int UserRiskID = Convert.ToInt32(ViewState["UserRiskID"]);

            var n = (from ur in DataContext.Context.UserRisks
                     where ur.UserRiskID == UserRiskID
                     select ur).FirstOrDefault();

            n.RiskIntensityID = Convert.ToInt32(dlBeforeInt.SelectedValue);
            n.RiskIntensityID_AfterCO = Convert.ToInt32(dlAfterInt.SelectedValue);
            n.RiskProbabilityID = Convert.ToInt32(dlBeforeProb.SelectedValue);
            n.RiskProbabilityID_AfterCO = Convert.ToInt32(dlAfterProb.SelectedValue);
            DataContext.Context.SaveChanges();

            int UserID = Convert.ToInt32(ViewState["UserID"]);

            LoadRiskGRD(UserID);

            pnlRiskEdit.Visible = false;
        }

        protected void btnRetCompanyList_Click(object sender, EventArgs e)
        {
            mvRisk.SetActiveView(vwCompany);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }


    }
}