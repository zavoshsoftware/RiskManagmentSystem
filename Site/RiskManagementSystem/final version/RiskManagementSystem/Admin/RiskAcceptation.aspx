<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="RiskAcceptation.aspx.cs" Inherits="RiskManagementSystem.Admin.RiskAcceptation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 129px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
        <asp:MultiView runat="server" ID="mvRisk">
            <asp:View runat="server" ID="vwdetail">
                <div class="fieldGroup">
                    <center>
                        <b>
                            <asp:Label runat="server" Text="" ID="lblrisktitle"></asp:Label>
                        </b>
                    </center>
                    <table class="style1">
                        <tr>
                            <td class="style2">
                                نام پروژه:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblproject" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                نام عملیات :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblOperation" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                نام فعالیت :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblact" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                نام مرحله :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblstage" Text=""></asp:Label>
                            </td>
                        </tr>
                   
                        <tr>
                            <td class="style2">
                            </td>
                            <td>
                                <asp:Button runat="server" Text="تایید" ID="btnaccept" OnClick="btnaccept_Click" CssClass="alert-success" />
                                <asp:Button runat="server" Text="انصراف" ID="btncancel" OnClick="btncancel_Click" CssClass="btn-danger" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
            <asp:View runat="server" ID="vwlist">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" CssClass="btn-danger" />
                <asp:GridView ID="grdrisk" Width="100%" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdrisk_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="RiskTitle" HeaderText="عنوان ریسک" />
                        <asp:TemplateField HeaderText="نوع فعالیت">
                            <ItemTemplate>
                                <asp:Label ID="lblIsNormal" runat="server" Text='<%# (Boolean) Eval("IsNormal") != true ? "عادی" : "غیر عادی"%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="توضیحات">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbdetail" CommandName="showdetail" CommandArgument='<%# Eval("RiskID") %>'>مشاهده جزئیات</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
