<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="RiskSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.RiskSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">مدیریت ریسک ها</div>
        <div class="panel-body">
            <div class="titleDiv">
                <div class="fieldGroup">
                    <div class="right">
                        نام پروژه:
                    </div>
                    &nbsp;<div class="left">
                        <asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        نام عملیات:
                    </div>
                    &nbsp;<div class="left">
                        <asp:Label ID="lblOperationName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        نام فعالیت:
                    </div>
                    &nbsp;<div class="left">
                        <asp:Label ID="lblActName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right">
                        نام مرحله:
                    </div>
                    &nbsp;<div class="left">
                        <asp:Label ID="lblStageName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <br />
            <div class="ContentDiv">
                <asp:MultiView ID="mvRisk" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vwList" runat="server">
                        <br />
                        <asp:Button ID="btnAdd" runat="server" Text="اضافه کردن" OnClick="btnAdd_Click" CssClass="btn btn-success" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" CssClass="btn btn-warning" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="جست و جو" class="btn btn-success" onclick="$('#SearchDiv').css('display', 'block');" />
                        <br />
                        <div id="SearchDiv">
                            <asp:Panel ID="Panel1" runat="server" GroupingText="جست و جو بر اساس خطرات یا حوادث احتمالی"
                                BorderWidth="1px" BorderStyle="Solid">
                                <asp:TextBox ID="txtSearch" CssClass="text" runat="server" Width="300px" value="نام خطر"
                                    onblur="if(this.value == '') { this.value='نام خطر'}" onfocus="if (this.value == 'نام خطر') {this.value=''}" />
                                &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="بگرد" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                                &nbsp;
                            <input id="Button4" type="button" value="انصراف" class="btn btn-danger" onclick="$('#SearchDiv').css('display', 'none');" />
                                <br />
                                <asp:Label ID="lblNotFound" runat="server" Text="موردی یافت نشد" Visible="False"></asp:Label>
                            </asp:Panel>
                        </div>
                        <br />
                        <asp:GridView ID="grdRisk" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnRowCommand="GridView1_RowCommand" CellPadding="4" GridLines="None" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="CodeID" HeaderText="کد">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RiskTitle" HeaderText="خطرات یا حوادث احتمالی">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="نوع فعالیت">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsNormal" runat="server" Text='<%# (Boolean) Eval("IsNormal") != true ? "غیر عادی" : " عادی"%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="توضیحات">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                            CommandName="DoEdit" Style="text-decoration: none;">ویرایش</asp:LinkButton>
                                        &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                        CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>
                                        &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                        CommandName="Control" Style="text-decoration: none;">مدیریت اقدامات کنترلی</asp:LinkButton>
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
                        <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=eShopSalesDBEntities"
                            DefaultContainerName="eShopSalesDBEntities" EnableFlattening="False" EntitySetName="Khadamats">
                        </asp:EntityDataSource>
                    </asp:View>
                    <asp:View ID="vwEdit" runat="server">
                        <div class="fieldGroup">
                            <div class="right2">
                                کد یکتا:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtUniqueId" runat="server" Width="50px" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right2">
                                کد:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtCode" runat="server" Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="  کد را وارد نمایید"
                                    ForeColor="Red" ControlToValidate="txtCode" Display="Dynamic">→</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right2">
                                خطر یا حادثه احتمالی:
                            </div>
                            &nbsp;<div class="left">
                                <asp:TextBox ID="txtRisk" runat="server" Width="500px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="  خطر یا حادثه احتمالی را وارد نمایید"
                                    ForeColor="Red" ControlToValidate="txtRisk" Display="Dynamic">→</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right2">
                                نوع فعالیت:
                            </div>
                            &nbsp;<div class="left">
                                <asp:RadioButtonList ID="rblIsNormal" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">عادی</asp:ListItem>
                                    <asp:ListItem Value="2">غیر عادی</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div id="butonDiv">
                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" CssClass="btn btn-success" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="انصراف"
                                OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                        </div>
                        <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" />
                    </asp:View>
                    <asp:View ID="vwDelete" runat="server">
                        آیا مطمئن به حذف &quot;<asp:Label ID="lblDelete" runat="server"></asp:Label>
                        &quot; هستید؟&nbsp;
                    <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="بلی" Width="50px" CssClass="btn btn-success" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="خیر" Width="50px" CssClass="btn btn-danger" />
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</asp:Content>
