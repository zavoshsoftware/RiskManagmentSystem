<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="EditInfo.aspx.cs" Inherits="RiskManagementSystem.Companies.EditInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="minheight">
     
    <div class="fieldGroup">
        <div class="right2">
            نام :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            نام خانوادگی :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            نام شرکت:</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            نام پروژه :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            موقعیت انجام کار :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            آدرس :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            ایمیل :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
     <div class="fieldGroup">
        <div class="right2">
            تلفن :</div>
        &nbsp;<div class="left">
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control-xm"></asp:TextBox>
        </div>
    </div>
    <div id="butonDiv">
        <asp:Button ID="btnInsert" runat="server" Text="ثبت تغیرات" 
            onclick="btnInsert_Click" CssClass="btn-success" />
    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancele" runat="server" Text="انصراف" 
            onclick="btnCancele_Click" CssClass="btn-danger" />
    </div></div>
</asp:Content>
