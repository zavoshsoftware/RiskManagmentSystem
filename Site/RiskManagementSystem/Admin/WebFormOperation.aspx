<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormOperation.aspx.cs" Inherits="RiskManagementSystem.Admin.WebFormOperation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 




    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEdit" runat="server">
       <div id="butonDiv">
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="انصراف"
                          />
                    </div><br />
                       
                       
                           <table class="style1">
                    <tr>
                        <td>
                            نام
                        </td>
                        <td>
                            عادی:</td>
                        <td>
                            کد</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td>
                            <asp:TextBox ID="TextBox4" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                      <td>
                            <asp:TextBox ID="TextBox7" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                       <td>
                            <asp:TextBox ID="TextBox8" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                      <td>
                            <asp:TextBox ID="TextBox9" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox16" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox17" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox18" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox19" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox20" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                  </asp:View>
    </asp:MultiView>
</asp:Content>
