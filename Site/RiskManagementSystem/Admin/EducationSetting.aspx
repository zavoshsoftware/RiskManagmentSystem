<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EducationSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.EducationSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">مدیریت آموزش ها</div>
        <div class="panel-body">
            <div class="minheight">
                <asp:MultiView ID="mvEducation" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vwList" runat="server">


                        <asp:Button ID="btnAddEdu" runat="server" Text="اضافه کردن آموزش جدید"
                            OnClick="btnAddEdu_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click"
                Text="بازگشت" Width="150px" />
                        <br />
                        <div class="gridHight">
                            <asp:GridView ID="grdEducation" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowCommand="grdUsers_RowCommand" CellPadding="4"
                                GridLines="None" ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Title" HeaderText="عنوان">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StageTitle" HeaderText="مرحله انجام کار">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="توضیحات">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" runat="server"
                                                CommandArgument='<%# Eval("Id") %>' CommandName="DoEdit"
                                                Style="text-decoration: none;">ویرایش</asp:LinkButton>
                                            &#160;&#160;&#160;&#160;
                                <asp:LinkButton ID="lbDoDelete" runat="server"
                                    CommandArgument='<%# Eval("Id") %>' CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
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

                    </asp:View>
                    <asp:View ID="vwEdit" runat="server">
                        <div class="fieldGroup">
                            <div class="right">
                                مرحله انجام کار:
                            </div>
                             &nbsp;<div class="left">
                                <asp:TextBox ID="txtStage" runat="server" Enabled="false"></asp:TextBox>
                                <%-- &nbsp;<div class="left">
                                <asp:DropDownList ID="ddlStages" runat="server" AutoPostBack="true"
                                    DataTextField="StageTitle" DataValueField="StageID" Width="130px">
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right">
                                عنوان:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                    ControlToValidate="txtTitle" Display="Dynamic"
                                    ErrorMessage="عنوان را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </div>
                        </div>
                        
                        <div class="fieldGroup">
                            <div class="right">
                                توضیحات:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtBody" Display="Dynamic"
                                    ErrorMessage="توضیحات را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>

                            </div>
                        </div>

                        
         


            <div id="butonDiv">
                &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت"
                    OnClick="btnSave_Click" Style="height: 26px" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                    Text="انصراف" OnClick="btnCancel_Click" />
            </div>

            <asp:ValidationSummary ID="vsRegister" runat="server" ForeColor="Red" />

            </asp:View>
                    <asp:View ID="vwDelete" runat="server">
                        آیا مطمئن به حذف &nbsp;<b>
                            <asp:Label ID="lblEduDelete" runat="server" Text=""></asp:Label></b>&nbsp; هستید؟ <%--  <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>--%>&nbsp;
            <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="بلی"
                Width="50px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="خیر"
                Width="50px" />
                    </asp:View>


            </asp:MultiView>
        </div>
    </div>
    </div>
</asp:Content>
