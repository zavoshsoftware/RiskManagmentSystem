<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RiskManagementSystem.Companies.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="minheight">
<div class="menuBox">
<div class="menuBoxImg">
  <a href="RiskEval.aspx?ID=Before">  <img src="../Images/risklogo.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RiskEval.aspx?ID=Before">محاسبه ریسک</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="RiskEval.aspx?ID=After">  <img src="../Images/risklogo.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RiskEval.aspx?ID=After">محاسبه ریسک بعد از اقدامات کنترلی</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="EditInfo.aspx">  <img src="../Images/CFS.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="EditInfo.aspx">ویرایش اطلاعات کاربری</a></div>
</div><div class="menuBox">
<div class="menuBoxImg">
  <a href="RecoverPassword.aspx">  <img src="../Images/changepassIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="RecoverPassword.aspx">تغییر کلمه عبور</a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="DlFiles.aspx">  <img src="../Images/UploadIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="DlFiles.aspx">دانلود فایل </a></div>
</div>
<div class="menuBox">
<div class="menuBoxImg">
  <a href="../default.aspx">  <img src="../Images/LogoutIcon.png" width="70px" height="70px" /></a></div>
<div class="menuText"><a href="../default.aspx">خروج </a></div>
</div>

</div>
</asp:Content>
