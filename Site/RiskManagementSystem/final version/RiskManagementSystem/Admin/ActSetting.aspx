<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="ActSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.ActSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
        <div class="titleDiv">
            <div class="fieldGroup">
                <div class="right">
                    نام پروژه:</div>
                &nbsp;<div class="left">
                    <asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام عملیات:</div>
                &nbsp;<div class="left">
                    <asp:Label ID="lblOperationName" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="ContentDiv">
            <asp:MultiView ID="mvActs" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwList" runat="server">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="اضافه کردن" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="جست و جو" onclick="$('#SearchDiv').css('display','block');" />
                    <br />
                    <div id="SearchDiv">
                        <asp:Panel ID="Panel1" runat="server" GroupingText="جست و جو بر اساس نام عملیات"
                            BorderWidth="1px" BorderStyle="Solid">
                            <asp:TextBox ID="txtSearch" CssClass="text" runat="server" Width="300px" value="نام عملیات"
                                onblur="if(this.value == '') { this.value='نام عملیات'}" onfocus="if (this.value == 'نام عملیات') {this.value=''}" />
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="بگرد" OnClick="btnSearch_Click" />
                            &nbsp;
                            <input id="Button4" type="button" value="انصراف" onclick="$('#SearchDiv').css('display','none');" />
                            <br />
                            <asp:Label ID="lblNotFound" runat="server" Text="موردی یافت نشد" Visible="False"></asp:Label>
                        </asp:Panel>
                    </div>
                    <br />
                    <asp:GridView ID="grdAct" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" CellPadding="4" GridLines="None" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="CodeID" HeaderText="ردیف">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ActTitle" HeaderText="عنوان فعالیت">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProtectionEQP" HeaderText="وسایل حفاظت فردی">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Curses" HeaderText="دوره های آموزشی">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="توضیحات">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ActID") %>'
                                        CommandName="DoEdit" Style="text-decoration: none;">ویرایش</asp:LinkButton>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("ActID") %>'
                                        CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ActID") %>'
                                        CommandName="Stage" Style="text-decoration: none;">مراحل انجام</asp:LinkButton>
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
                            ردیف:</div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtCode" runat="server" Width="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="  ردیف فعالیت را وارد نمایید"
                                ForeColor="Red" ControlToValidate="txtCode" Display="Dynamic">→</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right2">
                            عنوان فعالیت:</div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtAct" runat="server" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="  عنوان فعالیت را وارد نمایید"
                                ForeColor="Red" ControlToValidate="txtAct" Display="Dynamic">→</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right2">
                            وسایل حفاضت فردی:</div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtEqp" runat="server" Width="500px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right2">
                            دوره های آموزشی:</div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtCourses" runat="server" Width="500px"></asp:TextBox>
                        </div>
                    </div>
                    <div id="butonDiv">
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="انصراف"
                            OnClick="btnCancel_Click" />
                    </div>
                    <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" />
                </asp:View>
                <asp:View ID="vwDelete" runat="server">
                    آیا مطمئن به حذف &quot;<asp:Label ID="lblDelete" runat="server"></asp:Label>
                    &quot; هستید؟&nbsp;
                    <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="بلی" Width="50px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="خیر" Width="50px" />
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
