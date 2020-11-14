<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RiskControl.aspx.cs" Inherits="RiskManagementSystem.Admin.RiskControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">لیست ریسک های بررسی نشده</div>
        <div class="panel-body">

            <asp:GridView ID="grdRisks" runat="server" AutoGenerateColumns="False"
                Width="100%"
                CellPadding="4" CssClass="table table-condensed"
                GridLines="None" ForeColor="#333333" OnRowCommand="grdRisks_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>


                    <asp:BoundField DataField="OperationGroupTitle" HeaderText="پروژه">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="OperationTitle" HeaderText="عملیات">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ActTitle" HeaderText="فعالیت">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="StageTitle" HeaderText="مرحله">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField DataField="RiskTitle" HeaderText="ریسک">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                             <asp:LinkButton ID="lbConfirm" runat="server" CommandArgument='<%# Eval("UserRiskId") %>'
                                CommandName="DoConfirm" Style="text-decoration: none;">
                                <i class="fa fa-check"></i>تایید</asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="lbDeny" runat="server" CommandArgument='<%# Eval("UserRiskId") %>'
                                CommandName="DoDeny" Style="text-decoration: none;">
                                <i class="fa fa-remove"></i>رد</asp:LinkButton>
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
        </div>
    </div>
</asp:Content>
