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
    public partial class OperationSetting : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                LoadProjectDS();
               ddlProject.SelectedIndex = 0;
                GrdOPerationDataSource();
             
            }

        }
        public void LoadProjectDS()
        {
            var n = from og in DataContext.Context.OperationGroups
                    select og;

            ddlProject.DataSource = n;
            ddlProject.DataValueField = "OperationGroupID";
            ddlProject.DataTextField = "OperationGroupTitle";
            ddlProject.DataBind();
        }
        public void GrdOPerationDataSource()
        {
            ViewState["OperationGroupID"] = ddlProject.SelectedValue;

            int OperationGroupID = Convert.ToInt32(ViewState["OperationGroupID"]);

            var n = from og in DataContext.Context.Operations
                    orderby og.CodeID
                    where og.OperationGroupID == OperationGroupID && og.IsAcceptedByAdmin==true
                    select og;

            grdOperation.DataSource = n;
            grdOperation.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["OperationID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvOperation.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["OperationID"] = Convert.ToInt32(e.CommandArgument);
                        int OperationID = Convert.ToInt32(ViewState["OperationID"]);

                        var n = (from p in DataContext.Context.Operations
                                 where p.OperationID == OperationID
                                 select p).FirstOrDefault();
                        lblDelete.Text = n.OperationTitle;
                        ViewState["EditMode"] = "Delete";
                        mvOperation.SetActiveView(vwDelete);
                        break;
                    }
                case "Acting":
                    {
                        ViewState["OperationID"] = Convert.ToInt32(e.CommandArgument);
                        int OperationID = Convert.ToInt32(ViewState["OperationID"]);
                             

                               Response.Redirect("~/Admin/ActSetting.aspx?ID=" + OperationID);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
            txtOp.Text = string.Empty;
            txtCode.Text = string.Empty;

        }
        private void Delete()
        {

            int OperationID = Convert.ToInt32(ViewState["OperationID"]);


            (from p in DataContext.Context.Operations
             where p.OperationID == OperationID
             select p).ToList().ForEach(DataContext.Context.DeleteObject);
            DataContext.Context.SaveChanges();

        }

        private void LoadForm()
        {
            int OperationID = Convert.ToInt32(ViewState["OperationID"]);

            var n = (from p in DataContext.Context.Operations
                     where p.OperationID == OperationID
                     select p).FirstOrDefault();

            if (n != null)
            {
                txtOp.Text = n.OperationTitle;
                txtCode.Text = n.CodeID.ToString();
            };

        }

        private void InsertForm()
        {
            int OperationGroupID = Convert.ToInt32(ViewState["OperationGroupID"]);


            Operations p = new Operations()
            {
                OperationTitle = txtOp.Text,
                OperationGroupID=OperationGroupID,
                CodeID=Convert.ToInt32(txtCode.Text),
                IsAcceptedByAdmin=true
            };
            DataContext.Context.Operations.AddObject(p);
            DataContext.Context.SaveChanges();

        }

        private void UpdateForm()
        {

            int OperationID = Convert.ToInt32(ViewState["OperationID"]);



            var n = (from p in DataContext.Context.Operations
                     where p.OperationID == OperationID
                     select p).FirstOrDefault();

                      n.OperationTitle = txtOp.Text;
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
                GrdOPerationDataSource();
                mvOperation.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvOperation.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdOPerationDataSource();
            mvOperation.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvOperation.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvOperation.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Default.aspx");
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrdOPerationDataSource();
            mvOperation.SetActiveView(vwList);
        }
    }
}