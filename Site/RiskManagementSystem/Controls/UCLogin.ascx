<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLogin.ascx.cs" Inherits="KalaSanat.Contorols.UCLogin" %>
<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI"
    TagPrefix="BotDetect" %>

<link href="../Css/AdminStyle/app.v2.min.css" rel="stylesheet" />
<link href="../Css/LoginStyle/Style.css" rel="stylesheet" />
<style>
    body, html {
        background-color: darkgrey;
    }

    .imagelogobox {
        text-align: center;
    }

    .text-right, .form-group {
        direction: rtl;
    }

    label {
        width: 100px;
    }
    .div-captcha{
        margin-right:100px;
    }
    .form-group
    {
        padding:0 10px 0 10px;
    }
    .cb-remember{
        width:200px;
        padding-right:42px;
    }
    .cb-remember label{
        width:200px;
        padding-right:5px;
    }
    /*.btn {
        float: right;
    }*/
</style>
<asp:Panel ID="Panel1" runat="server" DefaultButton="btnInsert">
    <section id="content" class="m-t-lg wrapper-md animated fadeInUp">
        <div class="container aside-xxl">
            <a class="imagelogobox block" href="/">
                <img src="../Images/logo.png" width="300px" class="automaxheight" /></a>
            <section class="panel panel-default bg-white m-t-lg login">
                <header class="panel-heading text-center">
                    <strong>ورود</strong>
                </header>

                <asp:ValidationSummary ID="vsLogin" runat="server" Font-Names="Tahoma" ForeColor="Red" ValidationGroup="b" />

                <div class="form-group text-right">
                    <label class="control-label">نام کاربری</label>
                    <asp:TextBox ID="txtUsername" runat="server" Width="250px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                        ControlToValidate="txtUsername" Display="Dynamic"
                        ErrorMessage="لطفا نام کاربری خود را وارد نمایید" ForeColor="Red"
                        ValidationGroup="b">→</asp:RequiredFieldValidator>

                </div>
                <div class="form-group text-right">
                    <label class="control-label">پسورد</label>

                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword" Display="Dynamic"
                        ErrorMessage="لطفا کلمه عبور خود را وارد نمایید" ForeColor="Red"
                        ValidationGroup="b">→</asp:RequiredFieldValidator>
                    &nbsp;<asp:CustomValidator ID="cvCheckLogin" runat="server" Display="Dynamic"
                        ErrorMessage="نام کاربری یا کلمه عبور صحیح نمی باشد" ForeColor="Red"
                        OnServerValidate="cvCheckLogin_ServerValidate" ValidationGroup="b">→</asp:CustomValidator>
                </div>
                <div class="form-group text-right">
                    <label class="control-label">کد کنترلی</label>
                    <div class="div-captcha">
                        <BotDetect:WebFormsCaptcha ID="ExampleCaptcha" runat="server" />
                        <asp:TextBox ID="CaptchaCode" runat="server" Width="250px" />
                        <asp:CustomValidator ID="cvrecaptcha" runat="server" Display="Dynamic"
                            ErrorMessage="کد کنترلی وارد شده صحیح نمی باشد." ForeColor="Red"
                            OnServerValidate="cvrecaptcha_ServerValidate" ValidationGroup="b">→</asp:CustomValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click"
                        CssClass="btn btn-success" Text="ورود" ValidationGroup="b" />
                    &nbsp;<asp:CheckBox ID="chkRemember" CssClass="cb-remember" runat="server" Font-Names="Tahoma"
                        Text="مرا به خاطر بسپار" />
                </div>



            </section>
        </div>
    </section>
    <!-- footer -->
    <footer id="footer">
        <div class="text-center padder">
            <p>
            </p>
        </div>
    </footer>
    <!-- / footer -->
    <script src="../js/AdminScripts/app.v2.min.js"></script>
</asp:Panel>
