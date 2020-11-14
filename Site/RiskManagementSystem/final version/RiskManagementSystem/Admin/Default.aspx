<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RiskManagementSystem.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="minheight">
<div style="width:824px; margin:0 auto;">
<div class="menuBoxnom">
<div class="menuBoxImg">
  <a href="UserSetting.aspx">  <img src="../Images/UM.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="UserSetting.aspx">مدیریت کاربران</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="SupervisorSetting.aspx">  <img src="../Images/Supervisor.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="SupervisorSetting.aspx">مدیریت ناظران</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="OperationGroupSetting.aspx">  <img src="../Images/ProjectIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="OperationGroupSetting.aspx">مدیریت پروژه ها</a></div>
</div>

<div class="menuBox">
<div class="menuBoxImg">
  <a href="OperationSetting.aspx">  <img src="../Images/OperaionIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="OperationSetting.aspx">مدیریت عملیات ها</a></div>
</div>
<p class="clear"></p>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="RiskView.aspx?ID=0">  <img src="../Images/risklogo.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RiskView.aspx?ID=0">مشاهده سطح ریسک های بررسی نشده توسط ناظر</a></div>
</div>

<div class="menuBox">
<div class="menuBoxImg">
  <a href="RiskView.aspx?ID=1">  <img src="../Images/risklogo.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RiskView.aspx?ID=1">مشاهده سطح ریسک های تایید شده توسط ناظر</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="UploadFiles.aspx">  <img src="../Images/UploadIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="UploadFiles.aspx">آپلود فایل ها</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="AdvanceSearch.aspx">  <img src="../Images/SearchIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="AdvanceSearch.aspx">جست و جو</a></div>
</div>
<p class="clear"></p>

<div class="menuBox">
<div class="menuBoxImg">
  <a href="RiskAcceptation.aspx">  <img src="../Images/ConfirmIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RiskAcceptation.aspx">تایید ریسک های وارد شده توسط شرکت ها</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="RecoverPassword.aspx">  <img src="../Images/changepassIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RecoverPassword.aspx">تغییر کلمه عبور</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="../default.aspx">  <img src="../Images/LogoutIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="../default.aspx">خروج </a></div>
</div>
</div>
</div>
</asp:Content>
