<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="RiskEval.aspx.cs" Inherits="RiskManagementSystem.Companies.RiskEval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 291px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
        <asp:Button ID="btnRet" runat="server" Text="بازگشت" OnClick="btnRet_Click" CssClass="btn-danger" CausesValidation="false" />
        <br />
        <center>
            <b>
                <asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label>
            </b>
        </center>
        <div class="titleDiv">
            <div class="fieldGroup">
                <div class="right">
                    نام پروژه:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام عملیات:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlOperation" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام فعالیت:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlAct" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlAct_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام مرحله:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlStage" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <%-- <div class="fieldGroup">
                <div class="right">
                    نام ریسک:</div>
                &nbsp;<div class="left">
              <asp:DropDownList ID="ddlRisk" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>--%>
        </div>
        <br />
        <div class="ContentDiv">
            <asp:Button ID="btnadd" runat="server" Text="اضافه کردن ریسک به این مرحله" OnClick="btnadd_Click"
                Visible="false"  CssClass="alert-success"  CausesValidation="false"/><br />
            <center>
                <asp:Panel runat="server" ID="panelmsg" Visible="false">
                    <div class="alert alert-success ">
                        <b>
                            <asp:Label runat="server" Text="" ID="lblmsg"></asp:Label></b></div>
                </asp:Panel>
            </center>
            <asp:Panel runat="server" ID="paneladd" Visible="false">
                <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
                <table class="style1">
                    <tr>
                        <td class="style2">
                            ردیف :
                        </td>
                        <td>
                            <asp:TextBox ID="txtcode" runat="server" Width="79px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="ردیف را وارد نمایید." ControlToValidate="txtcode"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            نوع فعالیت :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblnormal" runat="server">
                                <asp:ListItem Value="1">عادی</asp:ListItem>
                                <asp:ListItem Value="0">غیرعادی</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="نوع فعالیت را مشخص نمایید." ControlToValidate="rblnormal"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            عنوان ریسک :
                        </td>
                        <td>
                            <asp:TextBox ID="txtrisktitle" runat="server" CssClass="form-control-medium" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="عنوان ریسک را وارد نمایید." ControlToValidate="txtrisktitle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btninsertrisk" runat="server" Text="ثبت" OnClick="btninsertrisk_Click" CssClass="alert-success"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:GridView ID="grdRisks" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdRisks_RowDataBound"
                OnRowCommand="grdRisks_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
                <Columns>
                    <asp:BoundField DataField="CodeID" HeaderText="ردیف">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RiskTitle" HeaderText="خطرات یا حوادث احتمالی">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="نوع فعالیت">
                        <ItemTemplate>
                            <asp:Label ID="lblIsNormal" runat="server" Text='<%# (Boolean) Eval("IsNormal") != true ? "عادی" : "غیر عادی"%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="احتمال">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfRisk" runat="server" Value='<%# Eval("RiskID") %>' />
                            <asp:DropDownList ID="ddlProb" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="شدت">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlInt" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ریسک">
                        <ItemTemplate>
                            <asp:Label ID="lblRisk" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="وجود ندارد">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkNotAvailable" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="اقدامات کنترلی">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                CommandName="Control" Style="text-decoration: none;">مشاهده</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView><br />
            <asp:Button ID="btnEvalRisk" runat="server" Text="محاسبه شدت ریسک" OnClick="btnEvalRisk_Click"
                Visible="False" CssClass="alert-success"  CausesValidation="false"/>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnInsert" runat="server" Text="ثبت اطلاعات" OnClick="btnInsert_Click"
                Visible="False" CssClass="alert-success"  CausesValidation="false"/>
        </div>
    </div>
</asp:Content>
