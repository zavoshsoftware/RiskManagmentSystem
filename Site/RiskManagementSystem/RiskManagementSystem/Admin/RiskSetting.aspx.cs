using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class RiskSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GrdStageDataSource();
            }

        }

        public void LoadLabels(int StageID)
        {
            var ss = (from s in DataContext.Context.Stages
                      where s.StageID == StageID
                      select s).FirstOrDefault();
            lblStageName.Text = ss.StageTitle;

            var o = (from op in DataContext.Context.Acts
                     where op.ActID == ss.ActID
                     select op).FirstOrDefault();

            lblActName.Text = o.ActTitle;

            var n = (from op in DataContext.Context.Operations
                     where op.OperationID == o.OperationID
                     select op).FirstOrDefault();

            lblOperationName.Text = n.OperationTitle;

            var m = (from opg in DataContext.Context.OperationGroups
                     where opg.OperationGroupID == n.OperationGroupID
                     select opg).FirstOrDefault();

            lblProjectName.Text = m.OperationGroupTitle;

        }
        public void GrdStageDataSource()
        {

            if (Request.QueryString["ID"] != null)
            {
                ViewState["StageID"] = Request.QueryString["ID"];

                int StageID = Convert.ToInt32(ViewState["StageID"]);

                var n = from og in DataContext.Context.Risks
                        orderby og.CodeID
                        where og.StageID == StageID && og.IsAcceptedByAdmin==true
                        select og;

                grdRisk.DataSource = n;
                grdRisk.DataBind();

                LoadLabels(StageID);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["RiskID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvRisk.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["RiskID"] = Convert.ToInt32(e.CommandArgument);
                        int RiskID = Convert.ToInt32(ViewState["RiskID"]);

                        var n = (from p in DataContext.Context.Risks
                                 where p.RiskID == RiskID
                                 select p).SingleOrDefault();
                        lblDelete.Text = n.RiskTitle;
                        ViewState["EditMode"] = "Delete";
                        mvRisk.SetActiveView(vwDelete);
                        break;
                    }
                case "Control":
                    {
                        ViewState["RiskID"] = Convert.ToInt32(e.CommandArgument);
                        int RiskID = Convert.ToInt32(ViewState["RiskID"]);

                        Response.Redirect("~/Admin/ControllSetting.aspx?ID=" + RiskID);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
            txtRisk.Text = string.Empty;
            txtCode.Text = string.Empty;
            rblIsNormal.SelectedValue = "1";
        }
        private void Delete()
        {

            int RiskID = Convert.ToInt32(ViewState["RiskID"]);


            (from p in DataContext.Context.Risks
             where p.RiskID == RiskID
             select p).ToList().ForEach(DataContext.Context.DeleteObject);
            DataContext.Context.SaveChanges();

        }

        private void LoadForm()
        {
            int RiskID = Convert.ToInt32(ViewState["RiskID"]);

            var n = (from p in DataContext.Context.Risks
                     where p.RiskID == RiskID
                     select p).SingleOrDefault();

            if (n != null)
            {
                txtRisk.Text = n.RiskTitle;
                txtCode.Text = n.CodeID.ToString();
                if (n.IsNormal == true)
                    rblIsNormal.SelectedValue = "1";
                else
                    rblIsNormal.SelectedValue = "2";
            };

        }

        private void InsertForm()
        {
            int StageID = Convert.ToInt32(ViewState["StageID"]);

            Boolean IsNormal = false;
            if (rblIsNormal.SelectedValue == "1")
            {
                IsNormal = true;
            }
            else
            {
                IsNormal = false;
            }


            Risks p = new Risks()
            {
                RiskTitle = txtRisk.Text,
                StageID = StageID,
                CodeID = Convert.ToInt32(txtCode.Text),
                IsNormal = IsNormal,
                IsAcceptedByAdmin=true
            };
            DataContext.Context.Risks.AddObject(p);
            DataContext.Context.SaveChanges();

        }

        private void UpdateForm()
        {

            int RiskID = Convert.ToInt32(ViewState["RiskID"]);

            Boolean IsNormal = false;
            if (rblIsNormal.SelectedValue == "1")
            {
                IsNormal = true;
            }
            else
            {
                IsNormal = false;
            }


            var n = (from p in DataContext.Context.Risks
                     where p.RiskID == RiskID
                     select p).SingleOrDefault();

            n.RiskTitle = txtRisk.Text;
            n.CodeID = Convert.ToInt32(txtCode.Text);
            n.IsNormal = IsNormal;

            DataContext.Context.SaveChanges();

        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["EditMode"].ToString().Equals("Edit"))
                {
                    UpdateForm();
                }
                else
                {
                    InsertForm();
                }
                GrdStageDataSource();
                mvRisk.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvRisk.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdStageDataSource();
            mvRisk.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvRisk.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvRisk.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int StageID = Convert.ToInt32(ViewState["StageID"]);

            var n = (from a in DataContext.Context.Stages
                     where a.StageID == StageID
                     select a).FirstOrDefault();
            Response.Redirect("~/Admin/StageSetting.aspx?ID=" + n.ActID);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0)
            { int StageID = Convert.ToInt32(ViewState["StageID"]);

                var n = (from a in DataContext.Context.Risks
                         where a.RiskTitle.Contains(txtSearch.Text)&&
                         a.StageID == StageID
                         select a);
                if (n.FirstOrDefault() != null)
                {
                    grdRisk.DataSource = n;
                    grdRisk.DataBind();
                }
                else
                {
                    lblNotFound.Visible = true;

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "SearchScript",
            "$('#SearchDiv').css('display','block');", true);
            }
        }
    }
}