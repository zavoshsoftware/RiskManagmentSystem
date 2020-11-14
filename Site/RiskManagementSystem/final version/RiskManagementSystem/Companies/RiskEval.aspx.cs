using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Companies
{
    public partial class RiskEval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadProjectDDL();
                string QS = Request.QueryString["ID"].ToString();
                if (QS == "Before")
                {
                    lblPageTitle.Text = "محاسبه شدت ریسک قبل از اقدامات کنترلی";
                }
                else if (QS == "After")
                {
                    lblPageTitle.Text = "محاسبه شدت ریسک بعد از اقدامات کنترلی";
                }
            }

        }

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
        public void LoadRiskgrd(int StageID)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = from og in db.Risks
                        where og.StageID == StageID && og.IsAcceptedByAdmin==true
                        select og;

                grdRisks.DataSource = n;
                grdRisks.DataBind();

                foreach (GridViewRow r in grdRisks.Rows)
                {
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
                }
                if (n.FirstOrDefault() != null)
                {
                    btnEvalRisk.Visible = true;
                }
            }
        }
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
        }

        protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StageID = Convert.ToInt32(ddlStage.SelectedValue);
            btnadd.Visible = true;
            LoadRiskgrd(StageID);
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
            string QS = Request.QueryString["ID"].ToString();
            int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            foreach (GridViewRow r in grdRisks.Rows)
            {
                HiddenField hfRisk = (HiddenField)(r.FindControl("hfRisk"));
                int RiskID = Convert.ToInt32(hfRisk.Value);

                DropDownList ddlProb = (DropDownList)r.FindControl("ddlProb");

                CheckBox chkNotAvailable = (CheckBox)r.FindControl("chkNotAvailable");

                //   Label lblRisk = (Label)(r.FindControl("lblRisk"));

                DropDownList ddlInt = (DropDownList)r.FindControl("ddlInt");

                int RiskIntensityID = Convert.ToInt32(ddlInt.SelectedValue);
                int RiskProbabilityID = Convert.ToInt32(ddlProb.SelectedValue);

                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    var n = (from ru in db.UserRisks
                             where ru.UserID_Company == UserID && ru.RiskID == RiskID
                             select ru).FirstOrDefault();
                    if (QS == "Before")
                    {
                        if (n == null)
                        {
                            UserRisks u = new UserRisks()
                            {
                                UserID_Company = UserID,
                                RiskID = RiskID,
                                RiskIntensityID = RiskIntensityID,
                                RiskProbabilityID = RiskProbabilityID,
                                IsCheckByAdmin = false,
                                IsCheckBySup = false,
                                IsNotAvailable=chkNotAvailable.Checked
                            };
                            db.UserRisks.AddObject(u);
                            db.SaveChanges();
                        }
                        else
                        {
                            n.RiskIntensityID = RiskIntensityID;
                            n.RiskProbabilityID = RiskProbabilityID;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
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
                                IsNotAvailable = chkNotAvailable.Checked
                            };
                            db.UserRisks.AddObject(u);
                            db.SaveChanges();
                        }
                        else
                        {
                            n.RiskIntensityID_AfterCO = RiskIntensityID;
                            n.RiskProbabilityID_AfterCO = RiskProbabilityID;
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        protected void grdRisks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
 
        }

        public void LoadProbDDL(DropDownList ddlProb)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                var n = from p in db.RiskProbabilities
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
                        select p;

                ddlInt.DataSource = n;
                ddlInt.DataValueField = "RiskIntensityID";
                ddlInt.DataTextField = "RiskIntensityTitle";
                ddlInt.DataBind();
            }
        }

        protected void grdRisks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Control")
            {
                int RiskID = Convert.ToInt32(e.CommandArgument);

                Response.Redirect("~/Companies/ViewRiskControl.aspx?ID=" + RiskID);
            }
        }

        protected void btnRet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Companies/Default.aspx");
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            paneladd.Visible = true;
            panelmsg.Visible = false;
           
        }

        protected void btninsertrisk_Click(object sender, EventArgs e)
        {
            using (RiskManagementEntities db = new RiskManagementEntities())
           {
               int stageid = Convert.ToInt32(ddlStage.SelectedValue);
               int normalvalue = Convert.ToInt32(rblnormal.SelectedValue);
                 Boolean normal=true;
                 if (normalvalue == 1)
                     normal = false;
                 //else if (normalvalue == 0)
                    // normal = false;
               Risks r = new Risks()
               {
                   StageID = stageid,
                    RiskTitle=txtrisktitle.Text,
                   IsNormal = normal,
                    CodeID=int.Parse(txtcode.Text),
                    IsAcceptedByAdmin=false
               };

               db.Risks.AddObject(r);
               db.SaveChanges();
               hidepaneladd();
               //LoadRiskgrd(stageid);
               lblmsg.Text = "ریسک مورد نظر شما وارد شد,پس از تایید ادمین در لیست اضافه می شود.";
               panelmsg.Visible = true;
           }
        }

        public void hidepaneladd()
        {
            txtcode.Text = string.Empty;
            txtrisktitle.Text = string.Empty;
            paneladd.Visible = false;
        }
    }
}