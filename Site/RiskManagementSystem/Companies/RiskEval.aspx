<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true"
    CodeBehind="RiskEval.aspx.cs" Inherits="RiskManagementSystem.Companies.RiskEval" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvRisk" runat="server" ActiveViewIndex="0">

        <asp:View runat="server" ID="vwList">
            <div class="minheight">
                <asp:Button ID="btnRet" runat="server" Text="بازگشت" OnClick="btnRet_Click" CssClass="btn btn-warning" />
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
                            <div class="fieldGroup" style="margin-bottom: 40px;">
                                <div class="right">
                                    نام فعالیت:
                                </div>
                                &nbsp;<div class="left">
                                    <asp:DropDownList ID="ddlAct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAct_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblProtectionEQP" runat="server" Text=""></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCurses" runat="server" Text=""></asp:Label>
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
                            <div class="fieldGroup">
                                <%--<asp:Button runat="server" Text="مشاهده آموزش ها" CssClass="btn btn-primary" ID="btnshowEducation" OnClick="btnshowEducation_Click" Visible="false" />--%>
                                <asp:Button runat="server" Text=" افزودن ریسک جدید" CssClass="btn btn-primary" ID="btnAddRisk" OnClick="btnAddRisk_Click" Visible="false" />
                            </div>
                        </div>
                        <br />
                        <div class="ContentDiv">
                            <div class="alert alert-success" id="successDiv">محاسبه ریسک با موفقیت در سیستم ثبت گردید</div>
                            <div class="alert alert-danger" id="dangerDiv">در محاسبه ریسک خطایی رخ داده است. لطفا مجددا تلاش کنید</div>
                            <asp:GridView ID="grdRisks" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                Width="100%" AutoGenerateColumns="False" OnRowCommand="grdRisks_RowCommand">
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
                                    <asp:BoundField DataField="UniqueId" HeaderText="کد ریسک">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
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
                                    <asp:TemplateField HeaderText="اقدامات کنترلی">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                                CommandName="Control" Style="text-decoration: none;">مشاهده</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnEvalRisk" runat="server" Text="محاسبه شدت ریسک" OnClick="btnEvalRisk_Click" CssClass="btn btn-primary"
                                Visible="False" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnInsert" runat="server" Text="ثبت اطلاعات" OnClick="btnInsert_Click" CssClass="btn btn-success"
                                Visible="False" />&nbsp;&nbsp;&nbsp;
                            <%--<asp:Button ID="btnExportToExcel" runat="server" Text="خروجی اکسل" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary"
                                Visible="False" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View runat="server" ID="vwEducation">
            <asp:Button ID="btnBack" runat="server" Text="بازگشت" OnClick="btnBack_Click" CssClass="btn btn-warning" />
            <asp:GridView ID="grdEducation" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" OnRowCommand="grdEducation_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#428bca" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="عنوان">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StageTitle" HeaderText="مرحله انجام کار">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="مشاهده آموزش">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("Id") %>'
                                CommandName="Show" Style="text-decoration: none;">مشاهده</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>

        <asp:View runat="server" ID="vwEducationDetail">
            <asp:Button ID="btnReturnEducation" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturnEducation_Click" />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                </div>
                <div class="panel-body">
                    <asp:Label ID="lblBody" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </asp:View>

        <asp:View ID="vwInsertRisk" runat="server">
            <div class="panel-body">
                <div class="titleDiv">
                    <div class="fieldGroup">
                        <div class="right">
                            عنوان ریسک:
                        </div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtRiskTitle" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            ریسک عادی؟:
                        </div>
                        &nbsp;<div class="left">
                            <asp:CheckBox ID="cbIsNormal" runat="server" />
                        </div>
                    </div>
                    <div id="butonDiv">
                        <asp:Button runat="server" Text="ثبت" CssClass="btn btn-primary" ID="btnInsertRisk" OnClick="btnInsertRisk_Click" />
                    </div>
                </div>
            </div>
        </asp:View>

    </asp:MultiView>
</asp:Content>
