using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.Admin
{
    public partial class LoginLogsReport : System.Web.UI.Page
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdTableBind();
                ddlUsersBind();
            }
        }

        public void grdTableBind()
        {
            var loginLogs = (from a in db.LoginLogs
                             join aa in db.Users
                                 on a.UserId equals aa.UserID
                             join aaa in db.Roles
                                 on aa.RoleID equals aaa.RoleID

                             select new
                             {
                                 Username = aa.Username,
                                 RoleTitle = aaa.RoleTitle,
                                 a.LoginDate
                             }).OrderByDescending(current => current.LoginDate);

            grdTable.DataSource = loginLogs;
            grdTable.DataBind();
        }

        public void ddlUsersBind()
        {
            List<Users> list = db.Users.ToList();
            ddlUsers.Items.Clear();
            //ddlUsers.Items.Add(new ListItem("نام کاربری ", "-1"));
            ddlUsers.Items.Add(new ListItem("نمایش همه ", "0"));
            foreach (var i in list)
                ddlUsers.Items.Add(new ListItem(i.Username, i.UserID.ToString()));
        }

        protected void grdTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTable.PageIndex = e.NewPageIndex;
            grdTableBind();
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(ddlUsers.SelectedValue);
            if (userId == 0)
                grdTableBind();
            else
            {
                var loginLogs = (from a in db.LoginLogs
                                 join aa in db.Users
                                     on a.UserId equals aa.UserID
                                 join aaa in db.Roles
                                     on aa.RoleID equals aaa.RoleID
                                 where aa.UserID == userId
                                 select new
                                 {
                                     Username = aa.Username,
                                     RoleTitle = aaa.RoleTitle,
                                     a.LoginDate
                                 }).OrderByDescending(current => current.LoginDate);

                grdTable.DataSource = loginLogs;
                grdTable.DataBind();
            }
           
        }
    }
}