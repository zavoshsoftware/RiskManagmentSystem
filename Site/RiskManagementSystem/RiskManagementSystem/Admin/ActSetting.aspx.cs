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
    public partial class ActSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                GrdActDataSource();

            }

        }
       
        public void LoadLabels(int OperationID )
        {
            var n = (from op in DataContext.Context.Operations
                     where op.OperationID == OperationID
                     select op).FirstOrDefault();

            lblOperationName.Text = n.OperationTitle;

            var m = (from opg in DataContext.Context.OperationGroups
                     where opg.OperationGroupID ==n.OperationGroupID
                     select opg).FirstOrDefault();

            lblProjectName.Text = m.OperationGroupTitle;

        }
        public void GrdActDataSource()
        {
  
            if (Request.QueryString["ID"] != null)
            {
                ViewState["OperationID"] = Request.QueryString["ID"];

                int OperationID = Convert.ToInt32(ViewState["OperationID"]);

                var n = from og in DataContext.Context.Acts
                        orderby og.CodeID
                        where og.OperationID == OperationID && og.IsAcceptedByAdmin==true
                        select og;

                grdAct.DataSource = n;
                grdAct.DataBind();

                LoadLabels(OperationID);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["ActID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvActs.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["ActID"] = Convert.ToInt32(e.CommandArgument);
                        int ActID = Convert.ToInt32(ViewState["ActID"]);

                        var n = (from p in DataContext.Context.Acts
                                 where p.ActID == ActID
                                 select p).SingleOrDefault();
                        lblDelete.Text = n.ActTitle;
                        ViewState["EditMode"] = "Delete";
                        mvActs.SetActiveView(vwDelete);
                        break;
                    }
                case "Stage":
                    {
                        ViewState["ActID"] = Convert.ToInt32(e.CommandArgument);
                        int ActID = Convert.ToInt32(ViewState["ActID"]);

                        Response.Redirect("~/Admin/StageSetting.aspx?ID=" + ActID);
                        break;
                    }
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
            txtAct.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtCourses.Text = string.Empty;
            txtEqp.Text = string.Empty;
        }
        private void Delete()
        {

            int ActID = Convert.ToInt32(ViewState["ActID"]);


            (from p in DataContext.Context.Acts
             where p.ActID == ActID
             select p).ToList().ForEach(DataContext.Context.DeleteObject);
            DataContext.Context.SaveChanges();

        }

        private void LoadForm()
        {
            int ActID = Convert.ToInt32(ViewState["ActID"]);

            var n = (from p in DataContext.Context.Acts
                     where p.ActID == ActID
                     select p).SingleOrDefault();

            if (n != null)
            {
                txtAct.Text = n.ActTitle;
                txtCode.Text = n.CodeID.ToString();
                 txtCourses.Text=n.Curses;
                 txtEqp.Text = n.ProtectionEQP;
            };

        }

        private void InsertForm()
        {
            int OperationID = Convert.ToInt32(ViewState["OperationID"]);


            Acts p = new Acts()
            {
                ActTitle = txtAct.Text,
                OperationID = OperationID,
                CodeID = Convert.ToInt32(txtCode.Text),
                ProtectionEQP=txtEqp.Text,
                Curses=txtCourses.Text,
                IsAcceptedByAdmin=true
            };
            DataContext.Context.Acts.AddObject(p);
            DataContext.Context.SaveChanges();

        }

        private void UpdateForm()
        {

            int ActID = Convert.ToInt32(ViewState["ActID"]);



            var n = (from p in DataContext.Context.Acts
                     where p.ActID == ActID
                     select p).SingleOrDefault();

            n.ActTitle = txtAct.Text;
            n.CodeID = Convert.ToInt32(txtCode.Text);

            n.ProtectionEQP = txtEqp.Text;
            n.Curses = txtCourses.Text;
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
                GrdActDataSource();
                mvActs.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvActs.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdActDataSource();
            mvActs.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvActs.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvActs.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/OperationSetting.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int OperationID = Convert.ToInt32(ViewState["OperationID"]);
            if (txtSearch.Text.Length != 0)
            {
                var n = (from a in DataContext.Context.Acts
                         where a.ActTitle.Contains(txtSearch.Text)&&
                         a.OperationID==OperationID
                         select a);
                if (n.FirstOrDefault() != null)
                {
                    grdAct.DataSource = n;
                    grdAct.DataBind();
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