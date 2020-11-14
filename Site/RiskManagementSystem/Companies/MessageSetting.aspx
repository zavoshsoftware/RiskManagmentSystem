<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true" CodeBehind="MessageSetting.aspx.cs" Inherits="RiskManagementSystem.Companies.MessageSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvOperationGroup" runat="server" ActiveViewIndex="0">

        <asp:View ID="vwList" runat="server">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" CssClass="btn btn-warning" />
            <br />
            <asp:GridView ID="grdMessage" runat="server" AutoGenerateColumns="False"
                Width="100%"
                OnRowCommand="grdMessage_RowCommand" CellPadding="4"
                GridLines="None" ForeColor="#333333" OnRowDataBound="grdMessage_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="hfMessageId" runat="server" Value='<%# Eval("Id") %>' />
                            <asp:Label ID="lblUnRead" runat="server" Text="" Visible="false" CssClass="fa fa-envelope-o"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Subject" HeaderText="عنوان پیغام">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Username" HeaderText="کاربر دریافت کننده">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SendDate" HeaderText="زمان ارسال">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbDetails" runat="server" CommandArgument='<%# Eval("Id") %>'
                                CommandName="DoDetails" Style="text-decoration: none;">مشاهده پیغام</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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
            <asp:EntityDataSource ID="EntityDataSource1" runat="server"
                ConnectionString="name=eShopSalesDBEntities"
                DefaultContainerName="eShopSalesDBEntities" EnableFlattening="False"
                EntitySetName="Khadamats">
            </asp:EntityDataSource>
        </asp:View>

        <asp:View ID="vwEdit" runat="server">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Label ID="lblBody" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div id="butonDiv">
                &nbsp;<asp:Button ID="Button3" runat="server" CausesValidation="False"
                    Text="بازگشت" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
            </div>
        </asp:View>

    </asp:MultiView>
</asp:Content>
