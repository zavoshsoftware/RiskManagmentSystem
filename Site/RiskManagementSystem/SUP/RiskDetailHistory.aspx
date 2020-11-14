<%@ Page Title="" Language="C#" MasterPageFile="~/SUP/SupervisorMaster.Master" AutoEventWireup="true" CodeBehind="RiskDetailHistory.aspx.cs" Inherits="RiskManagementSystem.SUP.RiskDetailHistory" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            تاریخچه ریسک های 
            <asp:Label ID="lblStageTitle" runat="server" Text=""></asp:Label>
            <asp:DropDownList ID="ddlRiskDegree" runat="server" AutoPostBack="true" Width="300px" Height="33px" OnSelectedIndexChanged="ddlRiskDegree_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="درجه ریسک"></asp:ListItem>
                <asp:ListItem Value="1" Text="low"></asp:ListItem>
                <asp:ListItem Value="2" Text="medium"></asp:ListItem>
                <asp:ListItem Value="3" Text="high"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Panel ID="pnlRiskEdit" runat="server" Visible="false">
            <div class="panel panel-warning">
                <div class="panel-heading">ویرایش شدت و احتمال ریسک</div>

                <div class="panel-body">
                    <div class="fieldGroup">
                        <div class="right3">
                            احتمال ریسک قبل از اقدامات کنترلی
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="dlBeforeProb" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right3">
                            شدت ریسک قبل از اقدامات کنترلی
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="dlBeforeInt" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right3">
                            احتمال ریسک بعد از اقدامات کنترلی
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="dlAfterProb" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right3">
                            شدت ریسک بعد از اقدامات کنترلی
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="dlAfterInt" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="fieldGroup">
                        <div class="right3">
                            توضیحات:
                        </div>
                        &nbsp;<div class="left">
                            <asp:TextBox ID="txtDesc" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="butonDiv">
                        <asp:Button ID="btnInsertEdit" runat="server" Text="ثبت تغییرات"
                            CssClass="btn btn-success" OnClick="btnInsertEdit_Click" />
                    </div>

                </div>
            </div>
        </asp:Panel>
         <asp:Panel ID="pnlDesc" runat="server" Visible="false">
            <div class="panel panel-warning">
                <div class="panel-body">
                    <div class="fieldGroup">
                        <div class="right3">
                       <b>توضیحات</b>   
                        </div>
                        &nbsp;<div class="left">
                            <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    </div>
                </div>
             </asp:Panel>
        <div class="danger-lbl">
            <asp:Label ID="lblEmpty" runat="server" Text="موردی یافت نشد." Visible="false" CssClass="label label-danger"></asp:Label>
        </div>
        <div class="panel-body">
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False"
                Width="100%"
                CellPadding="4"
                GridLines="None" ForeColor="#333333" CssClass="table table-condensed" OnRowCommand="grdTable_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="RiskTitle" HeaderText="خطرات یا حوادث احتمالی">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskProbabilityTitle" HeaderText="احتمال قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskIntensityTitle" HeaderText="شدت قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskEvaluationTitle" HeaderText="ریسک محاسبه شده قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskAfterProbabilityTitle" HeaderText="احتمال بعد">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskAfterIntensityTitle" HeaderText=" شدت بعد">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskAfterEvaluationTitle" HeaderText="ریسک محاسبه شده بعد">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="StatusTitle" HeaderText="وضعیت ریسک">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SubmitDate" HeaderText="تاریخ ثبت">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbConfirm" runat="server" CommandArgument='<%# Eval("UserRiskId") %>'
                                CommandName="DoConfirm" Style="text-decoration: none;">
                                <i class="fa fa-check"></i>تایید</asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="lbDeny" runat="server" CommandArgument='<%# Eval("UserRiskId") %>'
                                CommandName="DoDeny" Style="text-decoration: none;">
                                <i class="fa fa-remove"></i>رد</asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("UserRiskId") %>'
                                CommandName="DoEdit" Style="text-decoration: none;">
                                <i class="fa fa-edit"></i>ویرایش</asp:LinkButton>
                            <br />
                            <asp:LinkButton ID="lbDesc" runat="server" CommandArgument='<%# Eval("UserRiskId") %>'
                                CommandName="ShowDesc" Style="text-decoration: none;">
                                <i class="fa fa-list"></i>توضیحات</asp:LinkButton>
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
        </div>
    </div>

</asp:Content>
