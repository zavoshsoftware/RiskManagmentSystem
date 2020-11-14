using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Classes;

namespace RiskManagementSystem.Companies
{
    public partial class EditInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadInfo();
            }
        }
        public void LoadInfo()
        {
            int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            var n = (from u in DataContext.Context.Users
                     where u.UserID == UserID
                     select u).FirstOrDefault();

            if (n != null)
            {
                txtAddress.Text = n.Address;
                txtCompanyName.Text = n.CompanyName;
                txtEmail.Text = n.Email;
                txtFamily.Text = n.Family;
                txtName.Text = n.Name;
                txtPhone.Text = n.Phone;
                txtPosition.Text = n.CompanyPosition;
                txtProjectName.Text = n.CompanyProjectName;
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            var n = (from u in DataContext.Context.Users
                     where u.UserID == UserID
                     select u).FirstOrDefault();


            n.Address = txtAddress.Text;
            n.CompanyName = txtCompanyName.Text;
            n.Email = txtEmail.Text;
            n.Family = txtFamily.Text;
            n.Name = txtName.Text;
            n.Phone = txtPhone.Text;
            n.CompanyPosition = txtPosition.Text;
            n.CompanyProjectName = txtProjectName.Text;
            DataContext.Context.SaveChanges();
            Response.Redirect("~/Companies/Default.aspx");
        }

        protected void btnCancele_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Companies/Default.aspx");
        }
    }
}