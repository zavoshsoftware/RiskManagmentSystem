﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class ControllSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GrdStageDataSource();
            }

        }

        public void LoadLabels(int RiskID)
        {
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
                ViewState["RiskID"] = Request.QueryString["ID"];

                int RiskID = Convert.ToInt32(ViewState["RiskID"]);

                var n = from og in DataContext.Context.ControlingWorks
                        orderby og.CodeID
                        where og.RiskID == RiskID&&og.IsAcceptedByAdmin==true
                        select og;

                grdControl.DataSource = n;
                grdControl.DataBind();

                LoadLabels(RiskID);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["ControlID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvControl.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["ControlID"] = Convert.ToInt32(e.CommandArgument);
                        int ControlID = Convert.ToInt32(ViewState["ControlID"]);

                        var n = (from p in DataContext.Context.ControlingWorks
                                 where p.ControlID == ControlID
                                 select p).SingleOrDefault();
                        lblDelete.Text = n.ControlTitle;
                        ViewState["EditMode"] = "Delete";
                        mvControl.SetActiveView(vwDelete);
                        break;
                    }
                case "Control":
                    {
                        ViewState["ControlID"] = Convert.ToInt32(e.CommandArgument);
                        int ControlID = Convert.ToInt32(ViewState["ControlID"]);

                        //Response.Redirect("~/Admin/ControllSetting.aspx?ID=" + ControlID);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
            txtControl.Text = string.Empty;
            txtCode.Text = string.Empty;
           
        }
        private void Delete()
        {

            int ControlID = Convert.ToInt32(ViewState["ControlID"]);


            (from p in DataContext.Context.ControlingWorks
             where p.ControlID == ControlID
             select p).ToList().ForEach(DataContext.Context.DeleteObject);
            DataContext.Context.SaveChanges();

        }

        private void LoadForm()
        {
            int ControlID = Convert.ToInt32(ViewState["ControlID"]);

            var n = (from p in DataContext.Context.ControlingWorks
                     where p.ControlID == ControlID
                     select p).FirstOrDefault();

            if (n != null)
            {
                txtControl.Text = n.ControlTitle;
                txtCode.Text = n.CodeID.ToString();
               
            };

        }

        private void InsertForm()
        {
            int RiskID = Convert.ToInt32(ViewState["RiskID"]);

            ControlingWorks p = new ControlingWorks()
            {
                ControlTitle = txtControl.Text,
                RiskID = RiskID,
                CodeID = Convert.ToInt32(txtCode.Text),
                IsAcceptedByAdmin=true
            };
            DataContext.Context.ControlingWorks.AddObject(p);
            DataContext.Context.SaveChanges();

        }

        private void UpdateForm()
        {

            int ControlID = Convert.ToInt32(ViewState["ControlID"]);
 
            var n = (from p in DataContext.Context.ControlingWorks
                     where p.ControlID == ControlID
                     select p).FirstOrDefault();

            n.ControlTitle = txtControl.Text;
            n.CodeID = Convert.ToInt32(txtCode.Text);
            
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
                mvControl.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvControl.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdStageDataSource();
            mvControl.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvControl.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvControl.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int RiskID = Convert.ToInt32(ViewState["RiskID"]);

            var n = (from a in DataContext.Context.Risks
                     where a.RiskID == RiskID
                     select a).FirstOrDefault();
            Response.Redirect("~/Admin/RiskSetting.aspx?ID=" + n.StageID);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0)
            {int RiskID = Convert.ToInt32(ViewState["RiskID"]);

                var n = (from a in DataContext.Context.ControlingWorks
                         where a.ControlTitle.Contains(txtSearch.Text)
                         &&a.RiskID==RiskID
                         select a);
                if (n.FirstOrDefault() != null)
                {
                    grdControl.DataSource = n;
                    grdControl.DataBind();
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