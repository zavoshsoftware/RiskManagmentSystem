<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelTest.aspx.cs" Inherits="RiskManagementSystem.ExcelTest"  EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="direction:rtl;">
            <asp:Button ID="btnExcel" runat="server" Text="خروجی اکسل" OnClick="btnExcel_Click" />
           <asp:GridView ID="grdRisks" runat="server" AutoGenerateColumns="False"
                Width="100%"
                CellPadding="4" CssClass="table table-condensed"
                GridLines="None" ForeColor="#333333" OnRowDataBound="grdRisks_RowDataBound">
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
                    <asp:TemplateField HeaderText="اقدامات کنترلی">
                        <ItemTemplate>
                            <%--<a href='<%# Eval("UserRiskId","/Companies/AfterRiskEvalDetail.aspx?Id={0}") %>'>ثبت ریسک</a>--%>
                            <asp:HiddenField ID="hfStageId" runat="server" Value='<%# Eval("StageId") %>' />
                            <asp:Label ID="lblControl" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
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
    </form>
</body>
</html>
