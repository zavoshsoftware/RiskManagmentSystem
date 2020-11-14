<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="RecoverPass.aspx.cs" Inherits="RiskManagementSystem.SUP.RecoverPass" %>
<%@ Register src="../Controls/RecoverPass.ascx" tagname="RecoverPass" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="minheight">
    <uc1:RecoverPass ID="RecoverPass1" runat="server" /></div>
</asp:Content>
