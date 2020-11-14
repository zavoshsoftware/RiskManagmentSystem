using RiskManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskManagementSystem.Admin
{
    public partial class EducationSetting : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewBind();
        }
        private void GridViewBind()
        {
            if (Request.QueryString["ID"] != null)
            {
                ViewState["StageID"] = Request.QueryString["ID"];

                int Id = Convert.ToInt32(ViewState["StageID"]);
                var n = from us in db.Education
                        join rl in db.Stages
                        on us.StageId equals rl.StageID
                        where us.IsDelete==false && us.StageId==Id
                        select new
                        {
                            Title = us.Title,
                            StageTitle = rl.StageTitle,
                            Id=us.Id

                        };
                grdEducation.DataSource = n;
                grdEducation.DataBind();
            }
        }
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["EducationID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvEducation.SetActiveView(vwEdit);

                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["EducationID"] = Convert.ToInt32(e.CommandArgument);
                        int EducationID = Convert.ToInt32(ViewState["EducationID"]);

                        ViewState["EditMode"] = "Delete";
                        var n = (from us in db.Education
                                 where us.Id == EducationID
                                 select us).FirstOrDefault();

                        lblEduDelete.Text = n.Title;
                        mvEducation.SetActiveView(vwDelete);
                        break;
                    }

                
            }
        }
        #region Form Data Methods
      
        private void ResetForm()
        {
            txtBody.Text = string.Empty;
            txtTitle.Text = string.Empty;
            int Id = Convert.ToInt32(ViewState["StageID"]);
            var stage = (from s in db.Stages where s.StageID == Id select s).FirstOrDefault();
            txtStage.Text = stage.StageTitle;
            //ddlStages.SelectedIndex = -1;
        }
        private void Delete()
        {

            int EducationId = Convert.ToInt32(ViewState["EducationID"]);


            (from us in db.Education
             where us.Id == EducationId
             select us).ToList().ForEach(db.DeleteObject);
            db.SaveChanges();

        }
        private void LoadForm()
        {
            int EducationId = Convert.ToInt32(ViewState["EducationID"]);

            var n = (from us in db.Education
                     join s in db.Stages
                     on us.StageId equals s.StageID
                     where us.Id == EducationId && us.IsDelete==false
                     select new
                     {
                         us.Id,
                         us.StageId,
                         us.Title,
                         us.Body,
                         s.StageTitle
                     }).FirstOrDefault();

            if (n != null)
            {
                int Id = Convert.ToInt32(ViewState["StageID"]);
                var stage = (from s in db.Stages where s.StageID == Id select s).FirstOrDefault();
                txtStage.Text = stage.StageTitle;
                txtTitle.Text = n.Title.ToString();
                txtBody.Text = n.Body.ToString();
                
               
            };
        }

        private void InsertForm()
        {
            if(Request.QueryString["ID"] != null)
            {
                ViewState["StageId"] = Request.QueryString["ID"];
                int Id = int.Parse(Request.QueryString["ID"].ToString());
                Education education = new Education()
                {
                    StageId = Id,
                    Title = txtTitle.Text,
                    Body = txtBody.Text,
                    IsDelete = false
                };
                db.Education.AddObject(education);
                db.SaveChanges();
            }

        }

      
        private void UpdateForm()
        {
            int EducationId = Convert.ToInt32(ViewState["EducationID"]);
            var n = (from us in db.Education
                     where us.Id == EducationId
                     select us).FirstOrDefault();

            //n.StageId = Convert.ToInt32(ddlStages.SelectedValue);
            n.Title = txtTitle.Text;
            n.Body = txtBody.Text;
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
                GridViewBind();
                mvEducation.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvEducation.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GridViewBind();

            mvEducation.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvEducation.SetActiveView(vwList);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            //int EducationId = Convert.ToInt32(ViewState["EducationID"]);
            //var n = (from us in db.Education
            //         join s in db.Stages on us.StageId equals s.StageID
            //         where us.Id == EducationId
            //         select s).FirstOrDefault();
            int Id = Convert.ToInt32(ViewState["StageID"]);
            var stage = (from s in db.Stages
                     where s.StageID == Id
                     select s).FirstOrDefault();
            Response.Redirect("~/Admin/StageSetting.aspx?ID=" + stage.ActID);
        }

        protected void btnAddEdu_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvEducation.SetActiveView(vwEdit);

        }

        protected void btnReturnToList_OnClick(object sender, EventArgs e)
        {
            mvEducation.SetActiveView(vwList);
        }
    }
}