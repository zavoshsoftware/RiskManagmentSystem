using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using System.Net;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Companies
{
    public partial class DlFiles : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GrdFilesDataSource();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DLCH":
                    {
                        ViewState["FileGroupID"] = Convert.ToInt32(e.CommandArgument);
                        int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);

                        var m = (from p in db.Files
                                 where p.FileGroupID == FileGroupID
                                 select p).FirstOrDefault();

                        // Response.Redirect("~/Files/Checklists/" + m.CHName);
                        if (m != null)
                        {
                            DownloadFile("~/Files/Checklists/" + m.CHName);
                        }
                        break;

                    }
                case "DLGU":
                    {
                        ViewState["FileGroupID"] = Convert.ToInt32(e.CommandArgument);
                        int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);

                        var m = (from p in db.Files
                                 where p.FileGroupID == FileGroupID
                                 select p).FirstOrDefault();


                        if (m != null)
                        {
                            DownloadFile("~/Files/guideline/" + m.GUName);
                        }
                        break;
                    }
                case "DLRG":
                    {
                        ViewState["FileGroupID"] = Convert.ToInt32(e.CommandArgument);
                        int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);

                        var m = (from p in db.Files
                                 where p.FileGroupID == FileGroupID
                                 select p).FirstOrDefault();


                        if (m != null)
                        {
                            DownloadFile("~/Files/regular/" + m.RGName);
                        }
                        break;
                    }

            }
        }
        public void DownloadFile(string FilePath)
        {

            string strURL = FilePath;
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
            byte[] data = req.DownloadData(Server.MapPath(strURL));
            response.BinaryWrite(data);
            response.End();
        }
        public void GrdFilesDataSource()
        {
            var n = from fg in db.FileGroups

                    orderby fg.FileGroupID descending
                    select fg;

            grdFiles.DataSource = n;
            grdFiles.DataBind();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Companies/default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0)
            {
                var n = (from a in db.FileGroups
                         where a.FileGroupTitle == txtSearch.Text
                         select a);
                if (n.FirstOrDefault() != null)
                {
                    grdFiles.DataSource = n;
                    grdFiles.DataBind();
                }
                else
                {
                    lblNotFound.Visible = true;

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "SearchScript",
            "$('#SearchDiv').css('display','block');", true);
            }
        }

        protected void grdFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow r in grdFiles.Rows)
            {
                HiddenField hfFileGroupID = (HiddenField)r.FindControl("hfFileID");

                LinkButton lbCH = (LinkButton)r.FindControl("lbch");

                LinkButton lbRe = (LinkButton)r.FindControl("lbRe");
                LinkButton lbGu = (LinkButton)r.FindControl("lbGu");

                int FileGroupID = Convert.ToInt32(hfFileGroupID.Value);

                using (RiskManagementEntities db = new RiskManagementEntities())
                {
                    var n = (from a in db.Files
                             where a.FileGroupID == FileGroupID
                             select a).FirstOrDefault();

                    if (n.CHName.Length <= 0)
                    {
                        lbCH.Enabled = false;
                    }
                    if (n.RGName.Length <= 0)
                    {
                        lbRe.Enabled = false;
                    }
                    if (n.GUName.Length <= 0)
                    {
                        lbGu.Enabled = false;
                    }


                }


            }
        }

    }
}