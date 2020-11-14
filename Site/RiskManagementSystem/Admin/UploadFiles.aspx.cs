using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;
using RiskManagementSystem.Model;
using System.IO;
using System.Net;

namespace RiskManagementSystem.Admin
{
    public partial class UploadFiles : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                GrdFilesDataSource();

            }

        }


        public void GrdFilesDataSource()
        {



            var n = from fg in db.FileGroups

                    orderby fg.FileGroupID descending
                    select fg;

            grdFiles.DataSource = n;
            grdFiles.DataBind();


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
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["FileGroupID"] = Convert.ToInt32(e.CommandArgument);
                        ViewState["EditMode"] = "Edit";
                        LoadForm();
                        mvFiles.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        ViewState["FileGroupID"] = Convert.ToInt32(e.CommandArgument);
                        int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);

                        var n = (from p in db.FileGroups
                                 where p.FileGroupID == FileGroupID
                                 select p).FirstOrDefault();
                        lblDelete.Text = n.FileGroupTitle;
                        ViewState["EditMode"] = "Delete";
                        mvFiles.SetActiveView(vwDelete);
                        break;
                    }
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
        #region Form Data Methods
        private void ResetForm()
        {
            txtFileGroupName.Text = string.Empty;

        }
        private void Delete()
        {

            int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);


            (from p in db.FileGroups
             where p.FileGroupID == FileGroupID
             select p).ToList().ForEach(db.DeleteObject);
            db.SaveChanges();

        }

        private void LoadForm()
        {
            int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);


            var m = (from fg in db.FileGroups
                     where fg.FileGroupID == FileGroupID
                     select fg).FirstOrDefault();

            var n = (from p in db.Files
                     where p.FileGroupID == m.FileGroupID
                     select p).FirstOrDefault();
           
            if (m != null)
            {
                txtFileGroupName.Text = m.FileGroupTitle;
                ViewState["CH"] = n.CHName;
                ViewState["GU"] = n.GUName;
                ViewState["RG"] = n.RGName;

            }

        }

        private void InsertForm()
        {
            string new_filenameCH = string.Empty;
            string new_filenameRG = string.Empty;
            string new_filenameGU = string.Empty;

            if (fuChk.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuChk.PostedFile.FileName);

                new_filenameCH =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Files/checklists/" + new_filenameCH);
                fuChk.PostedFile.SaveAs(new_filepath);
                ViewState["CH"] = new_filenameCH;
            }

            if (fuRg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuRg.PostedFile.FileName);

                new_filenameRG =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Files/regular/" + new_filenameRG);
                fuRg.PostedFile.SaveAs(new_filepath);
                ViewState["RG"] = new_filenameRG;
            }

            if (fuGu.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuGu.PostedFile.FileName);

                new_filenameGU =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Files/guideline/" + new_filenameGU);
                fuGu.PostedFile.SaveAs(new_filepath);
                ViewState["GU"] = new_filenameGU;
            }
            FileGroups fg = new FileGroups()
            {
                FileGroupTitle = txtFileGroupName.Text
            };
            db.FileGroups.AddObject(fg);
            db.SaveChanges();
            Files p = new Files()
            {
                CHName=new_filenameCH,
                GUName = new_filenameGU,
                RGName = new_filenameRG,
                FileGroupID = fg.FileGroupID,

            };
            db.Files.AddObject(p);
            db.SaveChanges();

        }

        private void UpdateForm()
        {
            int FileGroupID = Convert.ToInt32(ViewState["FileGroupID"]);


            var m = (from fg in db.FileGroups
                     where fg.FileGroupID == FileGroupID
                     select fg).FirstOrDefault();

            var n = (from p in db.Files
                     where p.FileGroupID == m.FileGroupID
                     select p).FirstOrDefault();

            if (fuChk.PostedFile.ContentLength != 0)
            {
                if (n.CHName != null)
                {
                    string old_filename = Server.MapPath("~/Files/checklists/" + ViewState["CH"].ToString());
                    if (File.Exists(old_filename))
                    {
                        File.Delete(old_filename);
                    }
                }
                string original_filename = Path.GetFileName(fuChk.PostedFile.FileName);

                string new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Files/checklists/" + new_filename);
                fuChk.PostedFile.SaveAs(new_filepath);
                ViewState["CH"] = new_filename;
            }

            if (fuRg.PostedFile.ContentLength != 0)
            {
                if (n.RGName != null)
                {
                    string old_filename = Server.MapPath("~/Files/regular/" + ViewState["RG"].ToString());
                    if (File.Exists(old_filename))
                    {
                        File.Delete(old_filename);
                    }
                }
                string original_filename = Path.GetFileName(fuRg.PostedFile.FileName);

                string new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Files/regular/" + new_filename);
                fuRg.PostedFile.SaveAs(new_filepath);
                ViewState["RG"] = new_filename;
            }

            if (fuGu.PostedFile.ContentLength != 0)
            {
                if (n.GUName != null)
                {
                    string old_filename = Server.MapPath("~/Files/guideline/" + ViewState["GU"].ToString());
                    if (File.Exists(old_filename))
                    {
                        File.Delete(old_filename);
                    }
                }
                string original_filename = Path.GetFileName(fuGu.PostedFile.FileName);

                string new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Files/guideline/" + new_filename);
                fuGu.PostedFile.SaveAs(new_filepath);
                ViewState["GU"] = new_filename;
            }




            m.FileGroupTitle = txtFileGroupName.Text;

            n.CHName = ViewState["CH"].ToString();
            n.GUName = ViewState["GU"].ToString();
            n.RGName = ViewState["RG"].ToString();

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
                GrdFilesDataSource();
                mvFiles.SetActiveView(vwList);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvFiles.SetActiveView(vwList);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Delete();
            GrdFilesDataSource();
            mvFiles.SetActiveView(vwList);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvFiles.SetActiveView(vwList);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["EditMode"] = "Insert";
            ResetForm();
            mvFiles.SetActiveView(vwEdit);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/default.aspx");
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

                int FileGroupID=Convert.ToInt32(hfFileGroupID.Value);

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