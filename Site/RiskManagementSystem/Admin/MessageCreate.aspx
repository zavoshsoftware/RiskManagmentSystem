<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MessageCreate.aspx.cs" Inherits="RiskManagementSystem.Admin.MessageCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="fieldGroup">
                    <div class="right2">
                        عنوان پیام:
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtTitle" runat="server" Width="500px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="  عنوان پیغام را وارد نمایید" ForeColor="Red"
                            ControlToValidate="txtTitle" Display="Dynamic">→</asp:RequiredFieldValidator>

                    </div>
                </div>

                <div class="fieldGroup multi-min-height">
                    <div class="right2">
                        متن پیام:
                    </div>
                    &nbsp;<div class="left">
                        <asp:TextBox ID="txtBody" runat="server" Width="500px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="  عنوان پیغام را وارد نمایید" ForeColor="Red"
                            ControlToValidate="txtBody" Display="Dynamic">→</asp:RequiredFieldValidator>

                    </div>
                </div>
                <br />

                <div class="fieldGroup">
                    <div class="right2">
                        کاربر دریافت کننده:
                    </div>
                    &nbsp;<div class="left">

                        <asp:DropDownList ID="ddlUser" runat="server" DataSourceID="edsUsers"
                            DataTextField="Username" DataValueField="UserID" Width="130px">
                        </asp:DropDownList>
                        <asp:EntityDataSource ID="edsUsers" runat="server"
                            ConnectionString="name=RiskManagementEntities"
                            DefaultContainerName="RiskManagementEntities" EnableFlattening="False"
                            EntitySetName="Users">
                        </asp:EntityDataSource>
                        &nbsp; &nbsp;
                    <asp:CheckBox ID="cbAllUser" runat="server" Text="همه کاربران" />
                        &nbsp;
                    <asp:CheckBox ID="cbAllCompany" runat="server" Text="همه پیمانکار ها" />
                        &nbsp;
                    <asp:CheckBox ID="cbAllSup" runat="server" Text="همه ناظران" />
                    </div>
                </div>
                <div class="clear"></div>

                <div id="butonDiv">
                    &nbsp;<asp:Button ID="btnSave" runat="server" Text="ثبت"
                        OnClick="btnSave_Click" CssClass="btn btn-success" />
                    &nbsp;<asp:Button ID="Button3" runat="server" CausesValidation="False"
                        Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                </div>

                <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" />
    
     <script type="text/javascript">
        $("#ContentPlaceHolder1_cbAllUser").change(function () {
            var allUser = document.getElementById("ContentPlaceHolder1_cbAllUser").checked;
            if (allUser == true) {
                document.getElementById("ContentPlaceHolder1_cbAllCompany").checked = true;
                document.getElementById("ContentPlaceHolder1_cbAllSup").checked = true;

                document.getElementById("ContentPlaceHolder1_cbAllCompany").disabled = true;
                document.getElementById("ContentPlaceHolder1_cbAllSup").disabled = true;
                document.getElementById("ContentPlaceHolder1_ddlUser").disabled = true;
            }
            else
            {
                document.getElementById("ContentPlaceHolder1_cbAllCompany").checked = false;
                document.getElementById("ContentPlaceHolder1_cbAllSup").checked = false;

                document.getElementById("ContentPlaceHolder1_cbAllCompany").disabled = false;
                document.getElementById("ContentPlaceHolder1_cbAllSup").disabled = false;
                document.getElementById("ContentPlaceHolder1_ddlUser").disabled = false;
            }
        });

        $("#ContentPlaceHolder1_cbAllCompany").change(function () {
            
            check();
        });
        $("#ContentPlaceHolder1_cbAllSup").change(function () {

            check();
        });
        function check()
        {
            
            var allCompany = document.getElementById("ContentPlaceHolder1_cbAllCompany").checked;
            var allSup = document.getElementById("ContentPlaceHolder1_cbAllSup").checked;
            if (allCompany == true && allSup == true)
            {
                document.getElementById("ContentPlaceHolder1_cbAllUser").checked = true;
                document.getElementById("ContentPlaceHolder1_cbAllCompany").disabled = true;
                document.getElementById("ContentPlaceHolder1_cbAllSup").disabled = true;

                document.getElementById("ContentPlaceHolder1_ddlUser").disabled = true;
            }
            if (allCompany == true || allSup == true)
            {
                document.getElementById("ContentPlaceHolder1_ddlUser").disabled = true;
            }
            else
            {
                document.getElementById("ContentPlaceHolder1_ddlUser").disabled = false;
            }
        };
</script>
</asp:Content>
