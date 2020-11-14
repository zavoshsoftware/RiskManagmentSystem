﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DlFiles.aspx.cs" Inherits="RiskManagementSystem.Companies.DlFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="minheight">
     
        <div class="ContentDiv">
           
                    <br />
                   
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت"  CssClass="btn-danger"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="جست و جو" onclick="$('#SearchDiv').css('display','block');" class="btn-warning" />
                    <br />
                    <div id="SearchDiv">
                        <asp:Panel ID="Panel1" runat="server" GroupingText="جست و جو بر اساس نام "
                            BorderWidth="1px" BorderStyle="Solid">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control-xm" value="نام "
                                onblur="if(this.value == '') { this.value='نام '}" onfocus="if (this.value == 'نام ') {this.value=''}" />
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="بگرد" OnClick="btnSearch_Click"  CssClass="btn-success"/>
                            &nbsp;
                            <input id="Button4" type="button" value="انصراف" onclick="$('#SearchDiv').css('display','none');"  class="btn-danger"/>
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
              
        </div>
    </div>
</asp:Content>