<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.UserSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">مدیریت کاربران</div>
        <div class="panel-body">
            <div class="minheight">
                <asp:MultiView ID="mvUsers" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vwList" runat="server">
                        <div class="row">
                            <div class="col-md-2">
                                <a href="/Admin/UserCreate.aspx" class="btn btn-success">افزودن کاربر جدید</a>
<%--                                <asp:Button ID="btnAddUser" OnClick="btnAddUser_OnClick" runat="server" Text="اضافه کردن کاربر جدید"  CssClass="btn btn-success" />--%>
<%--                                <asp:Button ID="btnUsers" runat="server" Text="اضافه کردن کاربر جدید" OnClick="btnUsers_Click" CssClass="btn btn-success" />--%>
                            </div>
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtSearch" Width="200px" Height="34px"></asp:TextBox>&nbsp;
                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="جستجو" OnClick="btnSearch_Click" />
                                 <asp:Button runat="server" ID="btnShowAll" CssClass="btn btn-primary" Text="نمایش همه" OnClick="btnShowAll_Click" />
                            </div>
                        </div>
                        <br />
                        <div class="gridHight">
                            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowCommand="grdUsers_RowCommand" CellPadding="4"
                                GridLines="None" ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="نام">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Family" HeaderText="نام خانوادگی">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Username" HeaderText="نام کاربری">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RoleTitle" HeaderText="نقش کاربر">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Email" HeaderText="ایمیل">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="توضیحات">
                                        <ItemTemplate>
                                            <a href='<%# Eval("UserID","/admin/UserEdit.aspx?Id={0}") %>'>ویرایش</a>
                                       
                                            &#160;&#160;&#160;&#160;
                                <asp:LinkButton ID="lbDoDelete" runat="server"
                                    CommandArgument='<%# Eval("UserID") %>' CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>

                                            &#160;&#160;&#160;&#160;
                                    <asp:LinkButton ID="lbDoReset" runat="server"
                                        CommandArgument='<%# Eval("UserID") %>' CommandName="DoReset" Style="text-decoration: none;">بازیابی رمز عبور</asp:LinkButton>
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
                                نقش کاربر:
                            </div>
                            &nbsp;<div class="left">
                                <asp:DropDownList ID="ddlRoles" runat="server" DataSourceID="edsRoles" AutoPostBack="true"
                                    DataTextField="RoleTitle" DataValueField="RoleID" Width="130px" OnSelectedIndexChanged="ddlRoles_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="edsRoles" runat="server"
                                    ConnectionString="name=RiskManagementEntities"
                                    DefaultContainerName="RiskManagementEntities" EnableFlattening="False"
                                    EntitySetName="Roles">
                                </asp:EntityDataSource>
                            </div>
                        </div>

                        <asp:Panel ID="pnlSupervisors" runat="server" Visible="False">
                            <div class="fieldGroup">
                                <div class="right">
                                    ناظر:
                                </div>
                                &nbsp;<div class="left">
                                    <asp:DropDownList ID="ddlSupervisor" runat="server" Width="130px">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </asp:Panel>

                        <div class="fieldGroup">
                            <div class="right">
                                نام:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                    ControlToValidate="txtName" Display="Dynamic"
                                    ErrorMessage="نام را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right">
                                نام خانوادگی:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtFamily" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFamily" runat="server"
                                    ControlToValidate="txtFamily" Display="Dynamic"
                                    ErrorMessage="نام خانوادگی را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="fieldGroup">
                            <div class="right">
                                نام کاربری:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                                    ControlToValidate="txtUsername" Display="Dynamic"
                                    ErrorMessage="نام کابری را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <asp:Panel ID="pnlPassword" runat="server">
                            <div class="fieldGroup">
                                <div class="right">
                                    کلمه عبور:
                                </div>
                                &nbsp;<div class="left">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                        ControlToValidate="txtPassword" Display="Dynamic"
                                        ErrorMessage="کلمه عبور را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="fieldGroup">
                            <div class="right">
                                ایمیل:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                    ControlToValidate="txtEmail" Display="Dynamic"
                                    ErrorMessage="فرمت وارد شده درست نمی باشد" ForeColor="Red"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div id="butonDiv">
                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت"
                                OnClick="btnSave_Click" CssClass="btn btn-success" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                        </div>
                        <asp:ValidationSummary ID="vsRegister" runat="server" ForeColor="Red" />
                    </asp:View>

                    <asp:View ID="vwDelete" runat="server">
                        آیا مطمئن به حذف &nbsp;<b>
                            <asp:Label ID="lblUserDelete" runat="server" Text=""></asp:Label></b>&nbsp; هستید؟ <%--  <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>--%>&nbsp;
            <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="بلی"
                Width="50px" CssClass="btn btn-success" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="خیر"
                Width="50px" CssClass="btn btn-danger" />
                    </asp:View>

                    <asp:View ID="vwResetPassword" runat="server">
                        کلمه عبور با موفقیت بازیابی شد 
                <br />
                        <asp:Button ID="btnReturnToList" runat="server" Text="بازگشت به لیست" OnClick="btnReturnToList_OnClick" CssClass="btn btn-warning" />
                    </asp:View>

                </asp:MultiView>
            </div>
        </div>
    </div>

</asp:Content>
