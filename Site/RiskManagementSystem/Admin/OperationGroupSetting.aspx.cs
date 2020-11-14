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
    public partial class OperationGroupSetting : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GrdOGDataSource();
            }

        }
        public void GrdOGDataSource()
        {
            var n = from og in db.OperationGroups
                    orderby og.OperationGroupID descending
                    select og;

            grdOperationGroup.DataSource = n;
            grdOperationGroup.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["OperationGroupID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvOperationGroup.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["OperationGroupID"] = Convert.ToInt32(e.CommandArgument);
                        int OperationGroupID = Convert.ToInt32(ViewState["OperationGroupID"]);

                        var n = (from p in db.OperationGroups
                                 where p.OperationGroupID == OperationGroupID
                                 select p).FirstOrDefault();
                        lblDelete.Text = n.OperationGroupTitle;
                        ViewState["EditMode"] = "Delete";
                        mvOperationGroup.SetActiveView(vwDelete);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
          txtOpGroup.Text = string.Empty;
            
        }
        private void Delete()
        {

            int OperationGroupID = Convert.ToInt32(ViewState["OperationGroupID"]);


            (from p in db.OperationGroups
             where p.OperationGroupID == OperationGroupID
             select p).ToList().ForEach(db.DeleteObject);
            db.SaveChanges();

        }

        private void LoadForm()
        {
            int OperationGroupID = Convert.ToInt32(ViewState["OperationGroupID"]);

            var n = (from p in db.OperationGroups
                     where p.OperationGroupID == OperationGroupID
                     select p).FirstOrDefault();

            if (n != null)
            {
                txtOpGroup.Text = n.OperationGroupTitle;
                txtOperationGroupName.Text = n.OperationGroupName;
            };

        }

        private void InsertForm()
        {

            OperationGroups p = new OperationGroups()
            {
                OperationGroupTitle = txtOpGroup.Text,
                OperationGroupName=txtOperationGroupName.Text

            };
            db.OperationGroups.AddObject(p);
            db.SaveChanges();

        }

        private void UpdateForm()
        {

            int OperationGroupID = Convert.ToInt32(ViewState["OperationGroupID"]);



            var n = (from p in db.OperationGroups
                     where p.OperationGroupID == OperationGroupID
                     select p).FirstOrDefault();
            n.OperationGroupTitle = txtOpGroup.Text;
            n.OperationGroupName = txtOperationGroupName.Text;
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
                GrdOGDataSource();
                mvOperationGroup.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvOperationGroup.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdOGDataSource();
            mvOperationGroup.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvOperationGroup.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvOperationGroup.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }
    }
}