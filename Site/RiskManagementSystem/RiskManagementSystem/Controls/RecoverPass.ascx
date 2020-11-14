<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecoverPass.ascx.cs" Inherits="AzAmooz.RecoverPass" %>
<style type="text/css">
   body
   {
       direction:rtl;
   }
    .style1
    {
        width: 100%;
    }
</style>
<asp:Label ID="lblSucces" runat="server" Text=""></asp:Label>
<table class="style1">
    <tr>
        <td>
            نام کاربری:</td>
        <td>
            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            کلمه عبور پیشین:</td>
        <td>
            <asp:TextBox ID="txtOldPass" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvOldPass" runat="server" 
                            ControlToValidate="txtOldPass" Display="Dynamic" 
                            ErrorMessage="کلمه عبور پیشین را وارد کنید" ForeColor="Red">→</asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cvOldPas" runat="server" 
                ErrorMessage="کلمه عبور پیشین صحیح نمی باشد" 
                onservervalidate="cvOldPas_ServerValidate">→</asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td>
            کلمه عبور:</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                            ControlToValidate="txtPassword" Display="Dynamic" 
                            ErrorMessage="کلمه عبور را وارد کنید" ForeColor="Red">→</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            تکرار کلمه عبور:</td>
        <td>
            <asp:TextBox ID="txtRpPassword" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvRePassword" runat="server" 
                            ControlToValidate="txtRpPassword" Display="Dynamic" 
                            ErrorMessage="کلمه عبور را مجدادا وارد کنید" ForeColor="Red">→</asp:RequiredFieldValidator>
         <asp:CompareValidator ID="cvPassword" runat="server" 
                    ControlToCompare="txtPassword" ControlToValidate="txtRpPassword" 
                    Display="Dynamic" ErrorMessage="تکرار کلمه عبور صحیح نمی باشد" 
                ForeColor="Red">→</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" Text="ثبت تغیرات" 
                onclick="btnSubmit_Click" />
&nbsp;&nbsp;&nbsp;
         
        </td>
    </tr>
</table>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
