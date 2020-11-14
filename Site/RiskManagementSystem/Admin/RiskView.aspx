<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="RiskView.aspx.cs" Inherits="RiskManagementSystem.Admin.RiskView"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">
            <b>تاریخچه ریسک های وارد شده
            </b>
        </div>
        <br />
        <div class="panel panel-info">
            <div class="panel-heading">
                <b>فیلتر
                </b>
            </div>
            <div class="panel-body">

                <div class="titleDiv">
                    <div class="fieldGroup">
                        <div class="right">
                            نام پروژه:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            نام عملیات:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlOperation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            نام فعالیت:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlAct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAct_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fieldGroup">
                        <div class="right">
                            نام مرحله:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="fieldGroup">
                        <div class="right">
                            شرکت پیمانکار:
                        </div>
                        &nbsp;<div class="left">
                            <asp:DropDownList ID="ddlCompany" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="panel-body">
            <div class="danger-lbl">
                <asp:Label ID="lblEmpty" runat="server" Text="موردی یافت نشد." Visible="false" CssClass="label label-danger"></asp:Label>
            </div>
            <asp:Button ID="btnExportToExcel" runat="server" Text="خروجی اکسل" OnClick="btnExportToExcel_Click" CssClass="btn btn-primary" />
            <asp:DropDownList ID="ddlRiskDegree" runat="server" AutoPostBack="true" Width="300px" Height="33px" OnSelectedIndexChanged="ddlRiskDegree_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="درجه ریسک"></asp:ListItem>
                <asp:ListItem Value="1" Text="low"></asp:ListItem>
                <asp:ListItem Value="2" Text="medium"></asp:ListItem>
                <asp:ListItem Value="3" Text="high"></asp:ListItem>
            </asp:DropDownList>

            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False"
                Width="100%"
                CellPadding="4"
                GridLines="None" ForeColor="#333333" CssClass="table table-condensed" OnRowDataBound="grdTable_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="CompanyName" HeaderText="پیمانکار">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RiskTitle" HeaderText="خطرات یا حوادث احتمالی">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskProbabilityTitle" HeaderText="احتمال قبل">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="RiskIntensityTitle" HeaderText=" شدت قبل">
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
                     <asp:TemplateField>
                        <ItemTemplate>
                              <asp:HiddenField ID="hfUserRiskID" runat="server" Value='<%# Eval("UserRiskID") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%-- <asp:BoundField DataField="SubmitDate" HeaderText="تاریخ ثبت">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
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
    <%--<div class="minheight">
      <center>
                    <b>لیست شرکت هایی که اقدام به وارد کردن شدت ریسک نموده اند.
                    و توسط ناظر تایید 
                        <asp:Label ID="LblSup" runat="server" Text=""></asp:Label>
                    
                    </b></center>
                <br />


        <asp:MultiView ID="mvRisk" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwCompany" runat="server">
              
                <asp:Button ID="btnReturn" runat="server" Text="بازگشت" 
                    onclick="btnReturn_Click" />
                <asp:GridView ID="grdCompany" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdCompany_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="نام شرکت">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="توضیحات">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Risk" CommandArgument='<%# Eval("UserID") %>'>مشاهده ریسک</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
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
                </asp:GridView>
            </asp:View>
            <asp:View ID="vwRiskView" runat="server">
                <asp:Button ID="btnRetCompanyList" runat="server" Text="بازگشت" 
                    onclick="btnRetCompanyList_Click" />
                <asp:Panel ID="pnlRiskDetails" runat="server" Visible="False">
                    <div class="titleDiv">
                     <div class="titleRightDiv">
                        <div class="fieldGroup">
                            <div class="right">
                                نام پروژه:</div>
                            &nbsp;<div class="left">
                                <asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right">
                                نام عملیات:</div>
                            &nbsp;<div class="left">
                                <asp:Label ID="lblOperationName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right">
                                نام فعالیت:</div>
                            &nbsp;<div class="left">
                                <asp:Label ID="lblActName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right">
                                نام مرحله:</div>
                            &nbsp;<div class="left">
                                <asp:Label ID="lblStageName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="fieldGroup">
                            <div class="right">
                                نام ریسک:</div>
                            &nbsp;<div class="left">
                                <asp:Label ID="lblRiskName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                     </div>
                        <div class="titleNameDiv">
                        JHA/<asp:Label ID="lblProName" runat="server" Text=""></asp:Label>
                       /<asp:Label ID="lblOpCode" runat="server" Text=""></asp:Label>
                       /<asp:Label ID="lblActCode" runat="server" Text=""></asp:Label>
                       
                        </div>
                        <p class="clear"></p>   </div>
                </asp:Panel>
                  <asp:Panel ID="pnlRiskEdit" runat="server" Visible="false">
                 <div class="fieldGroup">
                            <div class="right3">
                                شدت ریسک:</div>
                            &nbsp;<div class="left">
                                <asp:DropDownList ID="dlBeforeInt" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="fieldGroup">
                            <div class="right3">
                                احتمال ریسک:</div>
                            &nbsp;<div class="left">
                                <asp:DropDownList ID="dlBeforeProb" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="fieldGroup">
                            <div class="right3">
                                شدت ریسک بعد از کنترل:</div>
                            &nbsp;<div class="left">
               <asp:DropDownList ID="dlAfterInt" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="fieldGroup">
                            <div class="right3">
                                احتمال ریسک بعد از کنترل:</div>
                            &nbsp;<div class="left">
                                 <asp:DropDownList ID="dlAfterProb" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="butonDiv">
                            <asp:Button ID="btnInsertEdit" runat="server" Text="ثبت تغییرات" 
                                onclick="btnInsertEdit_Click" />
                        </div>
                </asp:Panel>
                <asp:GridView ID="grdRisk" runat="server" Width="100%" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdRisk_RowDataBound1"
                    OnRowCommand="grdRisk_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="RiskTitle" HeaderText="نام ریسک">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                       
                          
                        <asp:BoundField DataField="RiskProbabilityTitle" HeaderText="احتمال ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="RiskProbabilityLevel" HeaderText="سطح">
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="RiskIntensityTitle" HeaderText="شدت ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="RiskIntensityLevel" HeaderText="طبقه">
                         <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ریسک">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfUserRisk" runat="server" Value='<%# Eval("UserRiskID") %>' />
                                <asp:Label ID="lblRisk" runat="server" Text="Label"></asp:Label>
                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RiskProbabilityTitle_after" HeaderText="احتمال ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="RiskProbabilityLevel_AfterCO" HeaderText="سطح">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                     
                        <asp:BoundField DataField="RiskIntensityTitle_after" HeaderText="شدت ریسک">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                            <asp:BoundField DataField="RiskIntensityLevel_AfterCO" HeaderText="طبقه">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ریسک بعد از کنترل">
                            <ItemTemplate>
                                <asp:Label ID="lblRiskAfter" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="جزییات">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("UserRiskID") %>'
                                    CommandName="Detail">مشاهده جزییات</asp:LinkButton>    <br />
                                <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("UserRiskID") %>'
                                    CommandName="DoEdit">ویرایش</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
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
                </asp:GridView>
            </asp:View>
        </asp:MultiView></div>--%>
</asp:Content>
