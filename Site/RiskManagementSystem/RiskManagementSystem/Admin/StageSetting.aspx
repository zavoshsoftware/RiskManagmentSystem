﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="StageSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.StageSetting" %>
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
                </div> </div>  <br />
         <div class="ContentDiv">
<asp:MultiView ID="mvStages" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwList" runat="server">
            <br />
            <asp:Button ID="btnAdd" runat="server" Text="اضافه کردن" 
                onclick="btnAdd_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="بازگشت" />
               &nbsp;&nbsp;&nbsp;&nbsp;
            <input id="Button2" type="button" value="جست و جو" onclick="$('#SearchDiv').css('display','block');"  />
           
          <br />
          <div id="SearchDiv">
                 <asp:Panel ID="Panel1" runat="server" GroupingText="جست و جو بر اساس مراحل اصلی انجام کار" BorderWidth="1px" BorderStyle="Solid">
        
            <asp:TextBox ID="txtSearch" CssClass="text" runat="server" Width="300px"
                                value="نام مرحله" onblur="if(this.value == '') { this.value='نام مرحله'}" onfocus="if (this.value == 'نام مرحله') {this.value=''}" />
          &nbsp;  <asp:Button ID="btnSearch" runat="server" Text="بگرد" OnClick="btnSearch_Click" />  &nbsp;
                     <input id="Button4" type="button" value="انصراف"  onclick="$('#SearchDiv').css('display','none');" />
           <br />
                     <asp:Label ID="lblNotFound" runat="server" Text="موردی یافت نشد" Visible="False"></asp:Label>
            </asp:Panel>  </div> 
          <br />  
            <asp:GridView ID="grdStage" runat="server" AutoGenerateColumns="False" 
              Width="100%" 
                onrowcommand="GridView1_RowCommand" CellPadding="4" 
                GridLines="None" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                        
                    <asp:BoundField DataField="CodeID" HeaderText="کد" 
                       >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="StageTitle" HeaderText="مراحل اصلی انجام کار" 
                       >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                         
        
                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                              <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("StageID") %>'
                                CommandName="DoEdit" Style="text-decoration: none;">ویرایش</asp:LinkButton>
                   &nbsp;   &nbsp;   &nbsp;     <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("StageID") %>'
                                CommandName="DoDelete" Style="text-decoration: none;">حذف</asp:LinkButton>
                    &nbsp;   &nbsp;   &nbsp;    <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("StageID") %>'
                                CommandName="Risk" Style="text-decoration: none;">مدیریت ریسک ها</asp:LinkButton>
                 
                    
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
            <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
                ConnectionString="name=eShopSalesDBEntities" 
                DefaultContainerName="eShopSalesDBEntities" EnableFlattening="False" 
                EntitySetName="Khadamats">
            </asp:EntityDataSource>
  
        </asp:View>
        <asp:View ID="vwEdit" runat="server">
           
                <div class="fieldGroup">
                    <div class="right">
                         کد:</div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtCode" runat="server" Width="50px"></asp:TextBox>
             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="  کد را وارد نمایید" ForeColor="Red" 
                            ControlToValidate="txtCode" Display="Dynamic">→</asp:RequiredFieldValidator>
              
                    </div>
                </div>
                          <div class="fieldGroup">
                    <div class="right">
                          مرحله انجام کار:</div>
                    &nbsp;<div class="left">
            <asp:TextBox ID="txtStage" runat="server" Width="500px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                            ErrorMessage="  مرحله انجام کار را وارد نمایید" ForeColor="Red" 
                            ControlToValidate="txtStage" Display="Dynamic">→</asp:RequiredFieldValidator>
               
                    </div>
                </div>
                <div id="butonDiv">    &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت" 
                            onclick="btnSave_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                            Text="انصراف" onclick="btnCancel_Click" />                  </div>
         
       
            <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" />
        </asp:View>
        <asp:View ID="vwDelete" runat="server">

             آیا مطمئن به حذف &quot;<asp:Label ID="lblDelete" runat="server"></asp:Label>
             &quot; هستید؟&nbsp;
            <asp:Button ID="btnYes" runat="server" onclick="btnYes_Click" Text="بلی" 
                Width="50px" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="خیر" 
                Width="50px" />
       </asp:View>
    </asp:MultiView>
    </div></div>
</asp:Content>
