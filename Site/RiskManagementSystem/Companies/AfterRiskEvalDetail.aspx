<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true" CodeBehind="AfterRiskEvalDetail.aspx.cs" Inherits="RiskManagementSystem.Companies.AfterRiskEvalDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
        <br />
        <div class="panel panel-primary">
            <div class="panel-heading">
                <b>
                    <asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label>
                </b>
            </div>
            <div class="panel-body">
                <div class="titleDiv">
                    <div class="fieldGroup">
                        <div class="right">
                            <b>نام پروژه:</b>
                        </div>
                        &nbsp;<div class="left">
                            <asp:Label ID="lblProject" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            <b>نام عملیات:</b>
                        </div>
                        &nbsp;<div class="left">
                            <asp:Label ID="lblOperation" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            <b>نام فعالیت:</b>
                        </div>
                        &nbsp;<div class="left">
                            <asp:Label ID="lblAct" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            <b>نام مرحله:</b>
                        </div>
                        &nbsp;<div class="left">
                            <asp:Label ID="lblStage" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="ContentDiv">
                    <div class="alert alert-success" id="successDiv">محاسبه ریسک با موفقیت در سیستم ثبت گردید</div>
                    <div class="alert alert-danger" id="dangerDiv">در محاسبه ریسک خطایی رخ داده است. لطفا مجددا تلاش کنید</div>
                    <asp:GridView ID="grdRisks" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="100%" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                        <Columns>
                            <asp:BoundField DataField="CodeID" HeaderText="ردیف">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RiskTitle" HeaderText="خطرات یا حوادث احتمالی">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="نوع فعالیت">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsNormal" runat="server" Text='<%# (Boolean) Eval("IsNormal") != true ? "عادی" : "غیر عادی"%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="احتمال قبل">
                                <ItemTemplate>
                                   <asp:Label ID="lblBProb" runat="server" Text=""></asp:Label>
                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="شدت قبل">
                                <ItemTemplate>
                                   <asp:Label ID="lblBInt" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ریسک قبل">
                                <ItemTemplate>
                                    <asp:Label ID="lblBRisk" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="احتمال">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfRisk" runat="server" Value='<%# Eval("RiskID") %>' />
                                    <asp:DropDownList ID="ddlProb" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="شدت">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlInt" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ریسک">
                                <ItemTemplate>
                                    <asp:Label ID="lblRisk" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnEvalRisk" runat="server" Text="محاسبه شدت ریسک" OnClick="btnEvalRisk_Click" CssClass="btn btn-primary"
                        Visible="False" />&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnInsert" runat="server" Text="ثبت اطلاعات" OnClick="btnInsert_Click" CssClass="btn btn-success" Visible="false" />&nbsp;&nbsp;&nbsp;
                </div>
            </div>
        </div>
    </div>
</asp:Content>
