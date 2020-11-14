using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class AdvanceSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtSearch.Text = string.Empty;
                ddlSearchType.SelectedIndex = -1;
            }
        }

        protected void btnDearch_Click(object sender, EventArgs e)
        {
            string SearchType = GetDropDownData();
            using (RiskManagementEntities db = new RiskManagementEntities())
            {
                if (SearchType == "Operation")
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        var n = from a in db.Operations
                                where a.OperationTitle.Contains(txtSearch.Text)
                                select new
                                {
                                    Tittle = a.OperationTitle,
                                    ID = a.OperationID,
                                    GroupTitle="عملیات"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                    else
                    {
                        var n = from a in db.Operations 
                                select new
                                {
                                    Tittle = a.OperationTitle,
                                    ID = a.OperationID,
                                    GroupTitle = "عملیات"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                }

                else   if (SearchType == "Act")
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        var n = from a in db.Acts
                                where a.ActTitle.Contains(txtSearch.Text)
                                select new
                                {
                                    Tittle = a.ActTitle,
                                    ID = a.ActID,
                                    GroupTitle = "فعالیت"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                    else
                    {
                        var n = from a in db.Acts
                                select new
                                {
                                    Tittle = a.ActTitle,
                                    ID = a.ActID,
                                    GroupTitle = "فعالیت"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                }




                else if (SearchType == "Stage")
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        var n = from a in db.Stages
                                where a.StageTitle.Contains(txtSearch.Text)
                                select new
                                {
                                    Tittle = a.StageTitle,
                                    ID = a.StageID,
                                    GroupTitle = "مرحله"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                    else
                    {
                        var n = from a in db.Stages
                                select new
                                {
                                    Tittle = a.StageTitle,
                                    ID = a.StageID,
                                    GroupTitle = "مرحله"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                }







                else if (SearchType == "Risk")
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        var n = from a in db.Risks
                                where a.RiskTitle.Contains(txtSearch.Text)
                                select new
                                {
                                    Tittle = a.RiskTitle,
                                    ID = a.RiskID,
                                    GroupTitle = "ریسک"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                    else
                    {
                        var n = from a in db.Risks
                                select new
                                {
                                    Tittle = a.RiskTitle,
                                    ID = a.RiskID,
                                    GroupTitle = "ریسک"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                }
                else if (SearchType == "Control")
                {
                    if (txtSearch.Text.Length > 0)
                    {
                        var n = from a in db.ControlingWorks
                                where a.ControlTitle.Contains(txtSearch.Text)
                                select new
                                {
                                    Tittle = a.ControlTitle,
                                    ID = a.ControlID,
                                    GroupTitle = "اقدامات کنترلی"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                    else
                    {
                        var n = from a in db.ControlingWorks
                                select new
                                {
                                    Tittle = a.ControlTitle,
                                    ID = a.ControlID,
                                    GroupTitle = "اقدامات کنترلی"
                                };
                        grdAdvanceSearch.DataSource = n;
                        grdAdvanceSearch.DataBind();
                    }
                }
            }
        }
        public string GetDropDownData()
        {
            string Searchtype = ddlSearchType.SelectedValue;

            if (Searchtype == "1")
                return "Operation";
            if (Searchtype == "2")
                return "Act";
            if (Searchtype == "3")
                return "Stage";
            if (Searchtype == "4")
                return "Risk";
            if (Searchtype == "5")
                return "Control";

            else
                return "0";
        }

        protected void grdAdvanceSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowAll")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                  string SearchType = GetDropDownData();
                 
                      if (SearchType == "Operation")
                      {
                          Response.Redirect("~/Admin/ActSetting.aspx?ID=" + ID);
                      }
                      else if (SearchType == "Act")
                      {
                          Response.Redirect("~/Admin/StageSetting.aspx?ID=" + ID);
                      }
                      else if (SearchType == "Stage")
                      {
                          Response.Redirect("~/Admin/RiskSetting.aspx?ID=" + ID);
                      }
                      else if (SearchType == "Risk")
                      {
                          Response.Redirect("~/Admin/ControllSetting.aspx?ID=" + ID);
                      }
                      else if (SearchType == "Control")
                      {
                          using (RiskManagementEntities db = new RiskManagementEntities())
                          {
                              var n = (from a in db.ControlingWorks
                                       where a.ControlID == ID
                                       select a).FirstOrDefault();


                              Response.Redirect("~/Admin/ControllSetting.aspx?ID=" + n.RiskID);
                          }
                      }
               //   }
            }
        }

        protected void btnRet_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/default.aspx");
        }
    }
}
