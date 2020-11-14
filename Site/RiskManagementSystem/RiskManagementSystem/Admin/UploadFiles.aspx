<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="UploadFiles.aspx.cs" Inherits="RiskManagementSystem.Admin.UploadFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="minheight">
     
        <div class="ContentDiv">
            <asp:MultiView ID="mvFiles" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwList" runat="server">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="اضافه کردن" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="جست و جو" onclick="$('#SearchDiv').css('display','block');" />
                    <br />
                    <div id="SearchDiv">
                        <asp:Panel ID="Panel1" runat="server" GroupingText="جست و جو بر اساس نام "
                            BorderWidth="1px" BorderStyle="Solid">
                            <asp:TextBox ID="txtSearch" CssClass="text" runat="server" Width="300px" value="نام "
                                onblur="if(this.value == '') { this.value='نام '}" onfocus="if (this.value == 'نام ') {this.value=''}" />
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="بگرد" OnClick="btnSearch_Click" />
                            &nbsp;
                            <input id="Button4" type="button" value="انصراف" onclick="$('#SearchDiv').css('display','none');" />
                            <br />
                            <asp:Label ID="lblNotFound" runat="server" Text="موردی یافت نشد" Visible="False"></asp:Label>
                        </asp:Panel>
                    </div>
                    <br />
                    <asp:GridView ID="grdFiles" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" CellPadding="4" GridLines="None" 
                        ForeColor="#333333" onrowdatabound="grdFiles_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="FileGroupTitle" HeaderText="عنوان">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="توضیحات">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("FileGroupID") %>'
                                        CommandName="DoEdit" Style="text-decoration: none;">ویرایش</asp:LinkButton>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("FileGroupID") %>'
                                        CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>
 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="دانلود">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfFileID" runat="server" Value='<%# Eval("FileGroupID") %>' />
                                    <asp:LinkButton ID="lbch" runat="server" CommandArgument='<%# Eval("FileGroupID") %>'
                                        CommandName="DLCH" Style="text-decoration: none;">چک لیست</asp:LinkButton>
                      &nbsp;        <asp:LinkButton ID="lbRe" runat="server" CommandArgument='<%# Eval("FileGroupID") %>'
                                        CommandName="DLGU" Style="text-decoration: none;">راهنما</asp:LinkButton>
                      &nbsp;        <asp:LinkButton ID="lbGu" runat="server" CommandArgument='<%# Eval("FileGroupID") %>'
                                        CommandName="DLRG" Style="text-decoration: none;">مقررات</asp:LinkButton>
                      
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
                  <br />
                    <div class="fieldGroup">
                        <div class="right2">
                            عنوان :</div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtFileGroupName" runat="server" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="  عنوان فعالیت را وارد نمایید"
                                ForeColor="Red" ControlToValidate="txtFileGroupName" Display="Dynamic">→</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right2">
                            آپلود چک لیست:</div>
                        &nbsp;<div class="left">
                            <asp:FileUpload ID="fuChk" runat="server" />
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right2">
                            آپلود مقررات:</div>
                        &nbsp;<div class="left">
                             <asp:FileUpload ID="fuRg" runat="server" />
                        </div>
                    </div>
                       <div class="fieldGroup">
                        <div class="right2">
                            آپلود راهنما:</div>
                        &nbsp;<div class="left">
                                <asp:FileUpload ID="fuGu" runat="server" />
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
