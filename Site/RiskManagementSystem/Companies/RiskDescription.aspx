<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true" CodeBehind="RiskDescription.aspx.cs" Inherits="RiskManagementSystem.Companies.RiskDescription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-primary">
    <div class="panel panel-heading">
        توضیحات ریسک
    </div>

        <div class="panel panel-body">

            <asp:Panel ID="pnlNoData" runat="server" Visible="False">
                <div class="alert alert-danger">
                    این ریسک توضیحاتی ندارد.
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                توضیحات وارد شده توسط ناظر:
                <br/>
                <asp:Label ID="lblRisDesk" runat="server" Text=""></asp:Label>
                

            </asp:Panel>
        </div>
        
    </div>
</asp:Content>
