<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AdvanceSearch.aspx.cs" Inherits="RiskManagementSystem.Admin.AdvanceSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">


       <table class="style1">
           <tr>
               <td>
                   کلمه مورد جست و جو:</td>
               <td>
                   <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
               </td>
           </tr>
           <tr>
               <td>
                   محل مورد جست و جو</td>
               <td>
                   <asp:DropDownList ID="ddlSearchType" runat="server" CssClass="form-control">
                       <asp:ListItem Value="1">عملیات ها</asp:ListItem>
                       <asp:ListItem Value="2">فعالیت ها</asp:ListItem>
                       <asp:ListItem Value="3">مراحل</asp:ListItem>
                       <asp:ListItem Value="4">ریسک ها</asp:ListItem>
                       <asp:ListItem Value="5">اقدامات کنترلی</asp:ListItem>
                   </asp:DropDownList>
               </td>
           </tr>
           <tr>
               <td>
                   &nbsp;</td>
               <td>
                   <asp:Button ID="btnDearch" runat="server" onclick="btnDearch_Click" 
                       Text="جست و جو"  CssClass="btn-info"/>
               &nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnRet" runat="server" Text="بازگشت" onclick="btnRet_Click" CssClass="btn-danger" />
               </td>
           </tr>
       </table>


       <asp:GridView ID="grdAdvanceSearch" runat="server" AutoGenerateColumns="False" Width="100%"
                        CellPadding="4" GridLines="None" ForeColor="#333333" 
           onrowcommand="grdAdvanceSearch_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                         
                            <asp:BoundField DataField="Tittle" HeaderText="عنوان">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                           <asp:BoundField DataField="GroupTitle" HeaderText="گروه">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="توضیحات">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ID") %>'
                                        CommandName="ShowAll" Style="text-decoration: none;">مشاهده</asp:LinkButton>
             
                                         </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
  
  </div>
</asp:Content>
