<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="RiskManagementSystem.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        div#loginBoxDiv {
            margin: 0 auto;
            width: 400px;
            border-style: solid;
            border-color: #FFFFFF;
            border-width: 2px;
            margin-top: 10px;
            height: 400px;
            padding-right: 5px;
            padding-top: 5px;
            margin-top: 5px;
            direction: rtl;
        }

        div#loginBoxDiv a {
            text-decoration: none;
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:Panel ID="Panel1" runat="server" DefaultButton="btnInsert">

    <div id="loginBoxDiv">
        <table class="style1">
            <tr>
                <td>کلمه عبور پیشین:</td>
                <td>
                    <asp:TextBox ID="txtoldPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                        ControlToValidate="txtoldPassword" Display="Dynamic"
                        ErrorMessage="لطفا کلمه عبور پیشین خود را وارد نمایید" ForeColor="Red"
                        ValidationGroup="b">→</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvOldPassword" runat="server" OnServerValidate="cvOldPassword_OnServerValidate" Display="Dynamic"
                                         ValidationGroup="b" ErrorMessage="کلمه عبور قدیمی صحیح نمی باشد" ForeColor="Red"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>کلمه عبور جدید:</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" Display="Dynamic"
                        ErrorMessage="لطفا کلمه عبور جدید خود را وارد نمایید" ForeColor="Red"
                        ValidationGroup="b">→</asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td>تکرار کلمه عبور جدید:</td>
                <td>
                    <asp:TextBox ID="txtRepeatPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtPassword" Display="Dynamic"
                        ErrorMessage="لطفا تکرار کلمه عبور جدید خود را وارد نمایید" ForeColor="Red"
                        ValidationGroup="b">→</asp:RequiredFieldValidator>

                    <asp:CompareValidator ID="cvPassword" runat="server" ValidationGroup="b" ForeColor="Red" Display="Dynamic" ErrorMessage="تکرار کلمه عبور صحیح نمی باشد" ControlToCompare="txtRepeatPassword" ControlToValidate="txtPassword"></asp:CompareValidator>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:ValidationSummary ID="vsLogin" runat="server" Font-Names="Tahoma"
                        ForeColor="Red" ValidationGroup="b" />
                    <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_OnClick"
                        Text="ورود" ValidationGroup="b" />

                </td>
            </tr>
        </table>
        <br />


    </div>

</asp:Panel>
</asp:Content>
