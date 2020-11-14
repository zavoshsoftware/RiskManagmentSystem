using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;

namespace RiskManagementSystem.SUP
{
    public partial class SupervisorMaster : System.Web.UI.MasterPage
    {
        private RiskManagementEntities db = new RiskManagementEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int totalNotification = GetTotalNotification();
                int totalMsg = GetTotalMessageCount();

                lblTotalNotification.Text = (totalNotification+ totalMsg).ToString();
                lblTotalNoti.Text = (totalNotification + totalMsg).ToString();
                lblNotiNotCheck.Text = totalNotification.ToString();
                lblTotalMsg.Text = totalMsg.ToString();
            }
        }

        public int GetTotalNotification()
        {
            int total = 0;
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            List<Users> users = db.Users.Where(current => current.SupervisorUserId == userId).ToList();

            foreach (Users user in users)
            {
                total = total + db.UserRisks.Count(current => current.UserID_Company == user.UserID && current.IsCheckBySup == false);
            }
         
            return total;
        }

        public int GetTotalMessageCount()
        {
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int totalMsg = db.Messages.Count(current => current.UserId == userId && current.IsRead == false);
            return totalMsg;
        }
    }
}