﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="UserSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.UserSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
        <asp:MultiView ID="mvUsers" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwList" runat="server">
                <asp:Button ID="btnUsers" runat="server" Text="اضافه کردن کاربر جدید" OnClick="btnUsers_Click"
                    CssClass="btn-info" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="بازگشت"
                    Width="150px" CssClass="btn-danger" />
                <br />
                <div class="gridHight">
                    <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="grdUsers_RowCommand" CellPadding="4" GridLines="None" ForeColor="#333333">
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
                            <asp:BoundField DataField="Password" HeaderText="کلمه عبور">
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
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("UserID") %>'
                                        CommandName="DoEdit" Style="text-decoration: none;">ویرایش</asp:LinkButton>
                                    &#160;&#160;&#160;&#160;
                                    <asp:LinkButton ID="lbDoDelete" runat="server" CommandArgument='<%# Eval("UserID") %>'
                                        CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>
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
                        نقش کاربر:</div>
                    &nbsp;<div class="left">
                        <asp:DropDownList ID="ddlRoles" runat="server" DataSourceID="edsRoles" DataTextField="RoleTitle"
                            DataValueField="RoleID" Width="130px">
                        </asp:DropDownList>
                        <asp:EntityDataSource ID="edsRoles" runat="server" ConnectionString="name=RiskManagementEntities"
                            DefaultContainerName="RiskManagementEntities" EnableFlattening="False" EntitySetName="Roles">
                        </asp:EntityDataSource>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        نام پروژه و شرکت:</div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control-xm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                            Display="Dynamic" ErrorMessage="نام را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        نام طرح:</div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control-xm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFamily" runat="server" ControlToValidate="txtFamily"
                            Display="Dynamic" ErrorMessage="نام خانوادگی را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        نام کاربری:</div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control-xm"> </asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                            Display="Dynamic" ErrorMessage="نام کابری را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        کلمه عبور:</div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control-xm"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" ErrorMessage="کلمه عبور را وارد کنید" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        ایمیل:</div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-xm"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="فرمت وارد شده درست نمی باشد" ForeColor="Red"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                </div>
                <div id="butonDiv">
                    &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click"
                        CssClass="alert-success" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="انصراف"
                        OnClick="btnCancel_Click" CssClass="btn-danger"/>
                </div>
                <asp:ValidationSummary ID="vsRegister" runat="server" ForeColor="Red" />
            </asp:View>
            <asp:View ID="vwDelete" runat="server">
                آیا مطمئن به حذف &nbsp;<b>
                    <asp:Label ID="lblUserDelete" runat="server" Text=""></asp:Label></b>&nbsp;
                هستید؟
                <%--  <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>--%>&nbsp;
                <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="بلی" CssClass="alert-success" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="خیر" CssClass="alert-success" />
            </asp:View>
        </asp:MultiView></div>
</asp:Content>
