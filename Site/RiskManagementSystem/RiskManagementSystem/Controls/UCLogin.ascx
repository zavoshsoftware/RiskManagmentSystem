<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLogin.ascx.cs" Inherits="KalaSanat.Contorols.UCLogin" %>
<style type="text/css">


div#loginBoxDiv
{
    margin: 0 auto;
    width: 400px;
    border-style: solid;
    border-color: #FFFFFF;
    border-width: 2px;
    margin-top: 10px;
    height: 400px;
    padding-right: 5px;
    padding-top: 5px;
    margin-top:5px;
    direction:rtl;
}

div#loginBoxDiv a
{
    text-decoration: none;
    color: #000000;
}
 
</style>
<asp:Panel ID="Panel1" runat="server" DefaultButton="btnInsert">

<div id="loginBoxDiv"><table class="style1">
        <tr>
            <td>
                نام کاربری:</td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server" Width="250px"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                    ControlToValidate="txtUsername" Display="Dynamic" 
                    ErrorMessage="لطفا نام کاربری خود را وارد نمایید" ForeColor="Red" 
                    ValidationGroup="b">→</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                کلمه عبور:</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                    ControlToValidate="txtPassword" Display="Dynamic" 
                    ErrorMessage="لطفا کلمه عبور خود را وارد نمایید" ForeColor="Red" 
                    ValidationGroup="b">→</asp:RequiredFieldValidator>
            &nbsp;<asp:CustomValidator ID="cvCheckLogin" runat="server" Display="Dynamic" 
                    ErrorMessage="نام کاربری یا کلمه عبور صحیح نمی باشد" ForeColor="Red" 
                    onservervalidate="cvCheckLogin_ServerValidate" ValidationGroup="b">→</asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:ValidationSummary ID="vsLogin" runat="server" Font-Names="Tahoma" 
                    ForeColor="Red" ValidationGroup="b" />
                <asp:Button ID="btnInsert" runat="server" onclick="btnInsert_Click" 
                    Text="ورود" ValidationGroup="b" />
&nbsp;<asp:CheckBox ID="chkRemember" runat="server" Font-Names="Tahoma" 
                    Text="مرا به خاطر بسپار" />
            </td>
        </tr>
    </table>
    <br />
  
    
    </div>    </asp:Panel>