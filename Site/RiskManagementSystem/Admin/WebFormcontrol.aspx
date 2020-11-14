<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="WebFormcontrol.aspx.cs" Inherits="RiskManagementSystem.Admin.WebFormcontrol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <div class="fieldGroup">
                <div class="right">
                    نام فعالیت:</div>
                &nbsp;<div class="left">
                    <asp:Label ID="lblActName" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام مرحله:</div>
                &nbsp;<div class="left">
                    <asp:Label ID="lblStageName" runat="server" Text=""></asp:Label>
                </div>
            </div>
             <div class="fieldGroup">
                <div class="right">
                    نام ریسک:</div>
                &nbsp;<div class="left">
                    <asp:Label ID="lblRiskName" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="ContentDiv">
            <asp:MultiView ID="mvControl" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwList" runat="server">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="اضافه کردن" OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="جست و جو" onclick="$('#SearchDiv').css('display','block');" />
                    <br />
                    <div id="SearchDiv">
                        <asp:Panel ID="Panel1" runat="server" GroupingText="جست و جو بر اساس اقدامات پیشنهادی کنترلی -  پیشگیرانه
"
                            BorderWidth="1px" BorderStyle="Solid">
                            <asp:TextBox ID="txtSearch" CssClass="text" runat="server" Width="300px" value="نام اقدام کنترلی"
                                onblur="if(this.value == '') { this.value='نام اقدام کنترلی'}" onfocus="if (this.value == 'نام اقدام کنترلی') {this.value=''}" />
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="بگرد" OnClick="btnSearch_Click" />
                            &nbsp;
                            <input id="Button4" type="button" value="انصراف" onclick="$('#SearchDiv').css('display','none');" />
                            <br />
                            <asp:Label ID="lblNotFound" runat="server" Text="موردی یافت نشد" Visible="False"></asp:Label>
                        </asp:Panel>
                    </div>
                    <br />
                    <asp:GridView ID="grdControl" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" CellPadding="4" GridLines="None" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="CodeID" HeaderText="کد">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ControlTitle" HeaderText="اقدامات پیشنهادی کنترلی -  پیشگیرانه">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                           
                            <asp:TemplateField HeaderText="توضیحات">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ControlID") %>'
                                        CommandName="DoEdit" Style="text-decoration: none;">ویرایش</asp:LinkButton>
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("ControlID") %>'
                                        CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>
                                  <%--  &nbsp; &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                        CommandName="Control" Style="text-decoration: none;">مدیریت اقدامات کنترلی</asp:LinkButton>--%>
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
                         <div id="butonDiv">
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="انصراف"
                            OnClick="btnCancel_Click" />
                    </div><br />
                       
                       
                           <table class="style1">
                    <tr>
                        <td>
                            نام
                        </td>
                        <td>
                            عادی:</td>
                        <td>
                            کد</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                         <td>
                            <asp:TextBox ID="TextBox4" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                      <td>
                            <asp:TextBox ID="TextBox7" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                       <td>
                            <asp:TextBox ID="TextBox8" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                      <td>
                            <asp:TextBox ID="TextBox9" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox16" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox17" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox18" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox19" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                     <tr>
                        <td>
                            <asp:TextBox ID="TextBox20" runat="server" Width="250px"></asp:TextBox></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>

                   
                  
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
