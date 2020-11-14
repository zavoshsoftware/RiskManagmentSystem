using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RiskManagementSystem.Model;
using RiskManagementSystem.Classes;

namespace RiskManagementSystem.Admin
{
    public partial class WebFormOperation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
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


                    Operations p = new Operations()
                    {
                      OperationGroupID=1,
                      OperationTitle=aa,
                        CodeID = 21+i,
                        IsAcceptedByAdmin = true
                    };
                    DataContext.Context.Operations.AddObject(p);
                    DataContext.Context.SaveChanges();


                    Operations p2 = new Operations()
                    {
                        OperationGroupID = 2,
                        OperationTitle = aa,
                        CodeID =18+i,
                        IsAcceptedByAdmin = true
                    };
                    DataContext.Context.Operations.AddObject(p2);
                    DataContext.Context.SaveChanges();
                }

            }
            //int newriskid = RiskID + 1;
            //Response.Redirect("~/Admin/WebFormcontrol.aspx?ID=" + newriskid.ToString());

        }
    }
}