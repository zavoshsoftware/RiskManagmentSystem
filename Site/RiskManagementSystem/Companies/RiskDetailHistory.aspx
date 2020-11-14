<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true" CodeBehind="RiskDetailHistory.aspx.cs" Inherits="RiskManagementSystem.Companies.RiskDetailHistory" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <b>تاریخچه ریسک های وارد شده
            </b>
             <asp:DropDownList ID="ddlRiskDegree" runat="server" AutoPostBack="true" Width="300px" Height="33px" OnSelectedIndexChanged="ddlRiskDegree_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="درجه ریسک"></asp:ListItem>
                <asp:ListItem Value="1" Text="low"></asp:ListItem>
                <asp:ListItem Value="2" Text="medium"></asp:ListItem>
                <asp:ListItem Value="3" Text="high"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <br />
        <div class="panel panel-info">
            <div class="panel-heading">
                <b>فیلتر
                </b>
            </div>
            <div class="panel-body">

                <div class="titleDiv">
                    <div class="fieldGroup">
                        <div class="right">
                            نام پروژه:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            نام عملیات:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlOperation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            نام فعالیت:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlAct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAct_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            نام مرحله:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>

            </div>
        </div>
        <div class="panel-body">
            <div class="danger-lbl">
                <asp:Label ID="lblEmpty" runat="server" Text="موردی یافت نشد." Visible="false" CssClass="label label-danger"></asp:Label>
            </div>
            <asp:Button ID="btnExportToExcel" runat="server" Text="خروجی اکسل" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />

            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False"
                Width="100%"
                CellPadding="4"
                GridLines="None" ForeColor="#333333" CssClass="table table-condensed" OnRowDataBound="grdTable_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="UniqueId" HeaderText="کد یکتا">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RiskTitle" HeaderText="خطرات یا حوادث احتمالی">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskProbabilityTitle" HeaderText="احتمال قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskIntensityTitle" HeaderText=" شدت قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskEvaluationTitle" HeaderText="ریسک محاسبه شده قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskAfterProbabilityTitle" HeaderText="احتمال بعد">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskAfterIntensityTitle" HeaderText=" شدت بعد">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskAfterEvaluationTitle" HeaderText="ریسک محاسبه شده بعد">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="StatusTitle" HeaderText="وضعیت ریسک">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="اقدامات کنترلی">
                        <ItemTemplate>
                            <asp:Label ID="lblControl" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hfUserRiskID" runat="server" Value='<%# Eval("UserRiskID") %>' />
                    
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                            <a href='<%# Eval("UserRiskID","/Companies/RiskDescription.aspx?id={0}") %>' >مشاهده توضیحات</a> 
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <%-- <asp:BoundField DataField="SubmitDate" HeaderText="تاریخ ثبت">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
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
