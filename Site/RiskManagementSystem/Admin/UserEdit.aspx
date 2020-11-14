<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="RiskManagementSystem.Admin.UserEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                OnClick="btnSave_OnClick" CssClass="btn btn-success" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                        </div>
                        <asp:ValidationSummary ID="vsRegister" runat="server" ForeColor="Red" />
</asp:Content>
