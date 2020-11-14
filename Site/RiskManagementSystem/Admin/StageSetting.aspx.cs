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
    public partial class StageSetting : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

          
                GrdStageDataSource();

            }

        }

        public void LoadLabels(int ActID)
        {
            var o= (from op in db.Acts
                     where op.ActID == ActID
                     select op).FirstOrDefault();

            lblActName.Text = o.ActTitle;

            var n = (from op in db.Operations
                     where op.OperationID ==o.OperationID
                     select op).FirstOrDefault();

            lblOperationName.Text = n.OperationTitle;

            var m = (from opg in db.OperationGroups
                     where opg.OperationGroupID == n.OperationGroupID
                     select opg).FirstOrDefault();

            lblProjectName.Text = m.OperationGroupTitle;

        }
        public void GrdStageDataSource()
        {
       
            if (Request.QueryString["ID"] != null)
            {
                ViewState["ActID"] = Request.QueryString["ID"];

                int ActID = Convert.ToInt32(ViewState["ActID"]);

                var n = from og in db.Stages
                        orderby og.CodeID
                        where og.ActID == ActID&&og.IsAcceptedByAdmin==true
                        select og;

                grdStage.DataSource = n;
                grdStage.DataBind();

                LoadLabels(ActID);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["StageID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvStages.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["StageID"] = Convert.ToInt32(e.CommandArgument);
                        int StageID = Convert.ToInt32(ViewState["StageID"]);

                        var n = (from p in db.Stages
                                 where p.StageID == StageID
                                 select p).FirstOrDefault();
                        lblDelete.Text = n.StageTitle;
                        ViewState["EditMode"] = "Delete";
                        mvStages.SetActiveView(vwDelete);
                        break;
                    }
                case "Risk":
                    {
                        ViewState["StageID"] = Convert.ToInt32(e.CommandArgument);
                        int StageID = Convert.ToInt32(ViewState["StageID"]);

                        Response.Redirect("~/Admin/RiskSetting.aspx?ID=" + StageID);
                        break;
                    }
                case "Education":
                    {
                        ViewState["StageID"] = Convert.ToInt32(e.CommandArgument);
                        int StageID = Convert.ToInt32(ViewState["StageID"]);

                        Response.Redirect("~/Admin/EducationSetting.aspx?ID=" + StageID);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
          
            txtStage.Text = string.Empty;
            txtCode.Text = string.Empty;
           
        }
        private void Delete()
        {

            int StageID = Convert.ToInt32(ViewState["StageID"]);


            (from p in db.Stages
             where p.StageID == StageID
             select p).ToList().ForEach(db.DeleteObject);
            db.SaveChanges();

        }

        private void LoadForm()
        {
            int StageID = Convert.ToInt32(ViewState["StageID"]);

            var n = (from p in db.Stages
                     where p.StageID == StageID
                     select p).FirstOrDefault();

            if (n != null)
            {
                txtStage.Text = n.StageTitle;
                txtCode.Text = n.CodeID.ToString();
               
            };

        }

        private void InsertForm()
        {
             int ActID = Convert.ToInt32(ViewState["ActID"]);

            Stages p = new Stages()
            {
                StageTitle = txtStage.Text,
                ActID = ActID,
                CodeID = Convert.ToInt32(txtCode.Text),
                IsAcceptedByAdmin=true
            };
            db.Stages.AddObject(p);
            db.SaveChanges();

        }

        private void UpdateForm()
        {

            int StageID = Convert.ToInt32(ViewState["StageID"]);



            var n = (from p in db.Stages
                     where p.StageID == StageID
                     select p).FirstOrDefault();

            n.StageTitle = txtStage.Text;
            n.CodeID = Convert.ToInt32(txtCode.Text);

            
            db.SaveChanges();

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
                mvStages.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvStages.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdStageDataSource();
            mvStages.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvStages.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvStages.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int ActID = Convert.ToInt32(ViewState["ActID"]);

            var n = (from a in db.Acts
                     where a.ActID == ActID
                     select a).FirstOrDefault();
            int aaa = n.OperationID;
            Response.Redirect("~/Admin/ActSetting.aspx?ID=" + n.OperationID);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0)
            {
                int ActID = Convert.ToInt32(ViewState["ActID"]);
                var n = (from a in db.Stages
                         where a.StageTitle.Contains(txtSearch.Text)
                         && a.ActID == ActID
                         select a);
                if (n.FirstOrDefault() != null)
                {
                    grdStage.DataSource = n;
                    grdStage.DataBind();
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