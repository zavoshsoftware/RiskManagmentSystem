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
    public partial class WebFormStage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["EditMode"] = "Insert";
                mvStages.SetActiveView(vwEdit);

                GrdStageDataSource();

            }

        }

        public void LoadLabels(int ActID)
        {
            var o = (from op in DataContext.Context.Acts
                     where op.ActID == ActID
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
                ViewState["ActID"] = Request.QueryString["ID"];

                int ActID = Convert.ToInt32(ViewState["ActID"]);

                var n = from og in DataContext.Context.Stages
                        orderby og.CodeID
                        where og.ActID == ActID && og.IsAcceptedByAdmin == true
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

                        var n = (from p in DataContext.Context.Stages
                                 where p.StageID == StageID
                                 select p).SingleOrDefault();
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
            }
        }
        #region Form Data Methods
        private void ResetForm()
        {
            //txtStage.Text = string.Empty;
            //txtCode.Text = string.Empty;

        }
        private void Delete()
        {

            int StageID = Convert.ToInt32(ViewState["StageID"]);


            (from p in DataContext.Context.Stages
             where p.StageID == StageID
             select p).ToList().ForEach(DataContext.Context.DeleteObject);
            DataContext.Context.SaveChanges();

        }

        private void LoadForm()
        {
            int StageID = Convert.ToInt32(ViewState["StageID"]);

            var n = (from p in DataContext.Context.Stages
                     where p.StageID == StageID
                     select p).SingleOrDefault();

            if (n != null)
            {
                //txtStage.Text = n.StageTitle;
                //txtCode.Text = n.CodeID.ToString();

            };

        }

        private void InsertForm()
        {
            int ActID = Convert.ToInt32(ViewState["ActID"]);

            //Stages p = new Stages()
            //{
            //    StageTitle = txtStage.Text,
            //    ActID = ActID,
            //    CodeID = Convert.ToInt32(txtCode.Text),
            //    IsAcceptedByAdmin = true
            //};
            //DataContext.Context.Stages.AddObject(p);
            //DataContext.Context.SaveChanges();
            int a = 0;
            if (TextBox1.Text.Length > 0) a = a + 1;
            if (TextBox2.Text.Length > 0) a = a + 1;
            if (TextBox3.Text.Length > 0) a = a + 1;
            if (TextBox4.Text.Length > 0) a = a + 1;
            if (TextBox5.Text.Length > 0) a = a + 1;
            if (TextBox6.Text.Length > 0) a = a + 1;
            if (TextBox7.Text.Length > 0) a = a + 1;
            if (TextBox8.Text.Length > 0) a = a + 1;
            if (TextBox9.Text.Length > 0) a = a + 1;
            if (TextBox10.Text.Length > 0) a = a + 1;
            if (TextBox11.Text.Length > 0) a = a + 1;
            if (TextBox12.Text.Length > 0) a = a + 1;
            if (TextBox13.Text.Length > 0) a = a + 1;
            if (TextBox14.Text.Length > 0) a = a + 1;
            if (TextBox15.Text.Length > 0) a = a + 1;
            if (TextBox16.Text.Length > 0) a = a + 1;
            if (TextBox17.Text.Length > 0) a = a + 1;
            if (TextBox18.Text.Length > 0) a = a + 1;
            if (TextBox19.Text.Length > 0) a = a + 1;
            if (TextBox20.Text.Length > 0) a = a + 1;
            if (a > 0)
            {
                for (int i = 1; i <= a; i++)
                {
                    TextBox risktitle = (TextBox)vwEdit.FindControl("TextBox" + i.ToString());

                    string aa = risktitle.Text;


                    Stages p = new Stages()
                    {
                        StageTitle = aa,
                        ActID=ActID,
                        CodeID = i,
                        IsAcceptedByAdmin = true
                    };
                    DataContext.Context.Stages.AddObject(p);
                    DataContext.Context.SaveChanges();



                    Stages p2 = new Stages()
                    {
                        StageTitle = aa,
                        ActID = ActID+1,
                        CodeID = i,
                        IsAcceptedByAdmin = true
                    };
                    DataContext.Context.Stages.AddObject(p2);
                    DataContext.Context.SaveChanges();
                
                
                
                
                }

            }
            int newriskid = ActID + 2;
            Response.Redirect("~/Admin/WebFormStage.aspx?ID=" + newriskid.ToString());

        }

        private void UpdateForm()
        {

            //int StageID = Convert.ToInt32(ViewState["StageID"]);



            //var n = (from p in DataContext.Context.Stages
            //         where p.StageID == StageID
            //         select p).SingleOrDefault();

            //n.StageTitle = txtStage.Text;
            //n.CodeID = Convert.ToInt32(txtCode.Text);


            //DataContext.Context.SaveChanges();

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

            var n = (from a in DataContext.Context.Acts
                     where a.ActID == ActID
                     select a).FirstOrDefault();
            int aaa = n.OperationID;
            Response.Redirect("~/Admin/ActSetting.aspx?ID=" + n.OperationID);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0)
            {
                var n = (from a in DataContext.Context.Stages
                         where a.StageTitle == txtSearch.Text
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