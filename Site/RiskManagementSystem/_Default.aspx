﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="_Default.aspx.cs" Inherits="RiskManagementSystem.Default" %>
<%@ Register src="Controls/UCLogin.ascx" tagname="UCLogin" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="minheight">
    <uc1:UCLogin ID="UCLogin1" runat="server" /></div>
</asp:Content>