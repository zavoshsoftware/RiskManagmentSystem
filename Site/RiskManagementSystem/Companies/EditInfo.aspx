<%@ Page Title="" Language="C#" MasterPageFile="~/Companies/CompanyMaster.Master" AutoEventWireup="true"
    CodeBehind="EditInfo.aspx.cs" Inherits="RiskManagementSystem.Companies.EditInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
        <div class="panel panel-primary">
            <div class="panel-heading">ویرایش پروفایل</div>
            <div class="panel-body">
                <div class="fieldGroup">
                    <div class="right2">
                        نام :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        نام خانوادگی :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtFamily" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        نام پیمانکار:
                    </div>
                    &nbsp;<div class="left">
                        <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        نام پروژه :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtProjectName" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        موقعیت انجام کار :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtPosition" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        آدرس :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtAddress" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        ایمیل :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div class="fieldGroup">
                    <div class="right2">
                        تلفن :
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
                    </div>
                </div>
                <div id="butonDiv">
                    <asp:Button ID="btnInsert" runat="server" Text="ثبت تغیرات"
                        OnClick="btnInsert_Click" CssClass="btn btn-success" />
                    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancele" runat="server" Text="انصراف"
            OnClick="btnCancele_Click" CssClass="btn btn-warning" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
