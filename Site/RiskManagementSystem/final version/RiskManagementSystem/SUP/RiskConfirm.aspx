<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="RiskConfirm.aspx.cs" Inherits="RiskManagementSystem.SUP.RiskConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="minheight">
       <asp:Button ID="btnRet" runat="server" Text="بازگشت" onclick="btnRet_Click" />
        <asp:MultiView ID="mvRisk" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwCompany" runat="server">
                <center>
                    <b>لیست شرکت هایی که اقدام به وارد کردن شدت ریسک نموده اند.</b></center>
                <br />
                           
                <asp:GridView ID="grdCompany" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdCompany_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="نام شرکت">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="توضیحات">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Risk" CommandArgument='<%# Eval("UserID") %>'>مشاهده ریسک</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
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
                </asp:GridView>
            </asp:View>
            <asp:View ID="vwRiskView" runat="server">
             <asp:Button ID="btnExportExcel" runat="server" Text="خروجی اکسل" 
                onclick="btnExportExcel_Click" />
                <asp:Panel ID="pnlRiskDetails" runat="server" Visible="False">
                    <div class="titleDiv">
                     <div class="titleRightDiv">
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
                          <div class="fieldGroup">
                            <div class="right">
                               </div>
                            &nbsp;<div class="left">
                                <asp:Button ID="btnConfirm" runat="server" onclick="btnConfirm_Click" Text="مورد تایید ناظر می باشد." />
        </div>
                        </div>
                        </div>

                        <div class="titleNameDiv">
                        JHA/<asp:Label ID="lblProName" runat="server" Text=""></asp:Label>
                       /<asp:Label ID="lblOpCode" runat="server" Text=""></asp:Label>
                       /<asp:Label ID="lblActCode" runat="server" Text=""></asp:Label>
                       
                        </div>
                        <p class="clear"></p>
                        </div>
                </asp:Panel>
                <asp:Panel ID="pnlRiskEdit" runat="server" Visible="false">
                 <div class="fieldGroup">
                            <div class="right3">
                                شدت ریسک:</div>
                            &nbsp;<div class="left">
                                <asp:DropDownList ID="dlBeforeInt" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="fieldGroup">
                            <div class="right3">
                                احتمال ریسک:</div>
                            &nbsp;<div class="left">
                                <asp:DropDownList ID="dlBeforeProb" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="fieldGroup">
                            <div class="right3">
                                شدت ریسک بعد از کنترل:</div>
                            &nbsp;<div class="left">
               <asp:DropDownList ID="dlAfterInt" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="fieldGroup">
                            <div class="right3">
                                احتمال ریسک بعد از کنترل:</div>
                            &nbsp;<div class="left">
                                 <asp:DropDownList ID="dlAfterProb" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="butonDiv">
                            <asp:Button ID="btnInsertEdit" runat="server" Text="ثبت تغییرات" 
                                onclick="btnInsertEdit_Click" />
                        </div>
                </asp:Panel>
                               <asp:GridView ID="grdRisk" runat="server" Width="100%" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdRisk_RowDataBound1"
                    OnRowCommand="grdRisk_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="RiskTitle" HeaderText="نام ریسک">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                       
                          
                        <asp:CheckBoxField DataField="IsNotAvailable" HeaderText=" موجود نمیباشد" >
                       
                          
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:CheckBoxField>
                       
                          
                        <asp:BoundField DataField="RiskProbabilityTitle" HeaderText="احتمال ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="RiskProbabilityLevel" HeaderText="سطح">
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="RiskIntensityTitle" HeaderText="شدت ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="RiskIntensityLevel" HeaderText="طبقه">
                         <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ریسک">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfUserRisk" runat="server" Value='<%# Eval("UserRiskID") %>' />
                                <asp:Label ID="lblRisk" runat="server" Text="Label"></asp:Label>
                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RiskProbabilityTitle_after" HeaderText="احتمال ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="RiskProbabilityLevel_AfterCO" HeaderText="سطح">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="RiskIntensityTitle_after" HeaderText="شدت ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="RiskIntensityLevel_AfterCO" HeaderText="طبقه">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ریسک بعد از کنترل">
                            <ItemTemplate>
                                <asp:Label ID="lblRiskAfter" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="جزییات">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("UserRiskID") %>'
                                    CommandName="Detail">مشاهده جزییات</asp:LinkButton>    <br />
                                <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("UserRiskID") %>'
                                    CommandName="DoEdit">ویرایش</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
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
                </asp:GridView>
            </asp:View>
        </asp:MultiView></div>
</asp:Content>
