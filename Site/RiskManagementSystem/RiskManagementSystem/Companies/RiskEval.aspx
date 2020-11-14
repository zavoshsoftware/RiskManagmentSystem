<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="RiskEval.aspx.cs" Inherits="RiskManagementSystem.Companies.RiskEval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="minheight">
       
        <asp:Button ID="btnRet" runat="server" Text="بازگشت" onclick="btnRet_Click" />
        <br />
       <center> <b>
            <asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label>
        </b></center>
        <div class="titleDiv">
            <div class="fieldGroup">
                <div class="right">
                    نام پروژه:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام عملیات:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlOperation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام فعالیت:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlAct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAct_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fieldGroup">
                <div class="right">
                    نام مرحله:</div>
                &nbsp;<div class="left">
                    <asp:DropDownList ID="ddlStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
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
                    <asp:TemplateField HeaderText="اقدامات کنترلی">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbRisk" runat="server" CommandArgument='<%# Eval("RiskID") %>'
                                CommandName="Control" Style="text-decoration: none;">مشاهده</asp:LinkButton>
                        </ItemTemplate><ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>




                </Columns>
            </asp:GridView>
            <asp:Button ID="btnEvalRisk" runat="server" Text="محاسبه شدت ریسک" OnClick="btnEvalRisk_Click"
                Visible="False" />&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnInsert" runat="server" Text="ثبت اطلاعات" OnClick="btnInsert_Click"
                Visible="False" />
        </div>
    </div>
</asp:Content>
