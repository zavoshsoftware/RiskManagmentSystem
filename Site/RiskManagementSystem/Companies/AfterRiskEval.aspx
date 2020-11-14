<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true" CodeBehind="AfterRiskEval.aspx.cs" Inherits="RiskManagementSystem.Companies.AfterRiskEval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">محاسبه شدت ریسک بعد از اقدامات کنترلی</div>
        <div class="panel-body">
            <asp:GridView ID="grdRisks" runat="server" AutoGenerateColumns="False"
                Width="100%"
                CellPadding="4" CssClass="table table-condensed"
                GridLines="None" ForeColor="#333333">
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
                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                            <a href='<%# Eval("UserRiskId","/Companies/AfterRiskEvalDetail.aspx?Id={0}") %>'>ثبت ریسک</a>
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
