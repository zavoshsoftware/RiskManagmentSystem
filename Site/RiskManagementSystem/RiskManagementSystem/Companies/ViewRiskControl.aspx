<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ViewRiskControl.aspx.cs" Inherits="RiskManagementSystem.Companies.ViewRiskControl" %>
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
                         CellPadding="4" GridLines="None" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="CodeID" HeaderText="ردیف">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ControlTitle" HeaderText="اقدامات پیشنهادی کنترلی -  پیشگیرانه">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                          
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
            
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
