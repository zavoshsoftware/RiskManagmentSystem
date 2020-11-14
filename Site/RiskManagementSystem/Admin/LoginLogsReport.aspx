<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="LoginLogsReport.aspx.cs" Inherits="RiskManagementSystem.Admin.LoginLogsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="فیلتر براساس نام کاربری"></asp:Label>
    <asp:DropDownList ID="ddlUsers" runat="server" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" AutoPostBack="true" Width="300px"></asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False"
        Width="100%" CellPadding="4"
        GridLines="None" ForeColor="#333333"
        AllowPaging="true"
        OnPageIndexChanging="grdTable_PageIndexChanging" PageSize="25">
        <AlternatingRowStyle BackColor="White" />
        <Columns>

            <asp:BoundField DataField="Username" HeaderText="نام کاربری">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="RoleTitle" HeaderText="نقش کاربر">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginDate" HeaderText="زمان ورود">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>

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
</asp:Content>
