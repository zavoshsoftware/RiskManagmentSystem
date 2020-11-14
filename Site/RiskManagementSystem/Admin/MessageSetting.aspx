<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MessageSetting.aspx.cs" Inherits="RiskManagementSystem.Admin.MessageSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">

        <asp:MultiView ID="mvOperationGroup" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwList" runat="server">
                <a href="MessageCreate.aspx" class="btn btn-success">پیغام جدید</a>
              
                &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="بازگشت" CssClass="btn btn-warning" />
                <br />
                <asp:GridView ID="grdOperationGroup" runat="server" AutoGenerateColumns="False"
                    Width="100%"
                    OnRowCommand="GridView1_RowCommand" CellPadding="4"
                    GridLines="None" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>


                        <asp:BoundField DataField="Subject" HeaderText="عنوان پیغام">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Username" HeaderText="کاربر دریافت کننده">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="SendDate" HeaderText="زمان ارسال">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="توضیحات">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CommandName="DoDetails" Style="text-decoration: none;">مشاهده پیغام</asp:LinkButton>

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
                       CssClass="btn btn-success" />
                    &nbsp;<asp:Button ID="Button3" runat="server" CausesValidation="False"
                        Text="انصراف" OnClick="Button3_OnClick"  CssClass="btn btn-danger" />
                </div>

                <asp:ValidationSummary ID="vsSummary" runat="server" ForeColor="Red" />
            </asp:View>

        </asp:MultiView>
    </div>
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

