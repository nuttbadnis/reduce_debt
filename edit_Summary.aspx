<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="edit_Summary.aspx.vb" Inherits="edit_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style>
.tablesummary
{
    margin: -10px;
    margin-top: 60%;
}
th
{
	text-align:center;
	font-weight: normal;
	font-size: 12px;
}
td
{
	font-size: 13px;
    color:black;
}
input
{
	padding: 4px;
    margin: 4px;
    border-radius: 4px;
    border: 1px solid #ccc;
}
select
{
	padding: 4px;
    margin: 4px;
    border-radius: 4px;
    border: 1px solid #ccc;
}
textarea
{
	padding: 4px;
    margin: 4px;
    border-radius: 4px;
    border: 1px solid #ccc;
}
table{
  border: 0;
  border-spacing: 0;
  border-collapse: collapse;
  table-layout: fixed;
}
</style>

<!-----Navbar LeftSide------>
       <!--<nav class="navbar navbar-default navbar-fixed-top" style="width: 13.5%;position: fixed;height: 100%;top: 51px;background: #eee;">
        <div class="col-md-12">
            <div class="list-group" style="margin-top: 10%">
            <a id="Create" runat="server" class="list-group-item" href="Default.aspx?menu=create">Create Project</a> 
            <a id="Edit" runat="server" class="list-group-item active" href="edit_list.aspx?menu=edit">Edit Project</a> 
            <a id="Approve" runat="server" class="list-group-item" href="approve_list.aspx?menu=approve">Approve Project</a> 
            <a id="menu3" runat="server" visible="false" class="list-group-item" href="#menu3">Fourth item</a>
            </div>
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966" 
                BorderStyle="None" BorderWidth="1px" CellPadding="0" class="table" GridLines="None" Width="0%" CssClass="tablesummary">
                <Columns>
                    <asp:BoundField DataField="Text" HeaderText="Monthly Payment">
                        <HeaderStyle HorizontalAlign="Center" Width="130px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Value" HeaderText="Payback">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#f5f5f5" ForeColor="#337ab7" Font-Size="4" />
                <FooterStyle BackColor="#FFFFCC" ForeColor="#fff" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#fff" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" ForeColor="#fff" />
                <HeaderStyle BackColor="#1abc9c" ForeColor="white" HorizontalAlign="Center"/>
            </asp:GridView>
            <br />
        </div>
    </nav>-->
<!-----Navbar LeftSide------>

<!----Body ----------------->   
    <!--<div class="col-md-10 pull-right" > -->
    
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item"><a id="menu_project_name" runat="server">Project Overview</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Service</a></li>
            <li class="breadcrumb-item"><a id="menu_capex" runat="server">Capex</a></li>
            <li class="breadcrumb-item"><a id="menu_opex" runat="server">Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_other" runat="server">Other</a></li>
            <li class="breadcrumb-item"><a id="menu_management" runat="server">Management</a></li>
            <li class="breadcrumb-item active"><u>Summary</u></li></ol>
            

    <div class="col-md-12" id="dvMain" style="border: 1px solid black; padding-bottom: 15px; margin: 10px 0px 20px 0px;line-height: 17px;">
        <%-- <div class="col-md-12 border border-dark p-2">
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="float-right"></asp:Label>
                    <div class="col-md-12" style="text-align : center;">
                        <asp:Label ID="Label1" runat="server" Text="Ẻ͹��ѵ�" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblProjectName" runat="server" Font-Bold="True" ></asp:Label>
                    </div>
                </div> --%>
        <table cellspacing="0" class="w-100 mt-3">
            <tr class="border border-1 border-dark">
                <td colspan="9" class="text-center pt-2 pb-2 text-bold">
                    <asp:Label ID="lblProjectName" runat="server" Text="Ẻ͹��ѵ�Ἱ�ҹ�����ç������к�ԡ��" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;">
                    <asp:Label ID="RefNo_Label" runat="server" Text="CS Ref.No. " Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="refno" runat="server"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Prepare_Label" runat="server" Text="Internal Ref.No. " Font-Bold="true">
                    </asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="intenal_refno" runat="server"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="Cluster_Label" runat="server" Text="Area/Cluster:" Font-Bold="True"></asp:Label>
                </td>                
                <td colspan="1" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="lblArea" runat="server"></asp:Label>/<asp:Label ID="lblCluster" runat="server"></asp:Label>
                </td>
                <td rowspan="3" style="text-align: center; vertical-align: middle;" >
                    <asp:Label ID="ContractPeriod_Label" runat="server" Text="�ѭ�� " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblContract" runat="server"></asp:Label>
                    <asp:Label ID="ContractPeriod_Unit" runat="server" Text="   ��͹"></asp:Label>
                </td>
            </tr>

            <tr class="border border-1 border-dark">
                <td style="text-align: left;">
                    <asp:Label ID="ProjectCode_Label" runat="server" Text="Project Code:" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="CustomerType_Label" runat="server" Text="�������١���:" Font-Bold="True"></asp:Label>
                </td>               
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="lblCustomerType" runat="server"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="Date_Label" runat="server" Text="�ѹ���Ѵ���͡���:" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="1" style="text-align: left">
                    <asp:Label ID="txtDocumentDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;">
                    <asp:Label ID="ProjectName_Label" runat="server" Text="Project Name:" Font-Bold="true"></asp:Label>
                </td>          
                <td colspan="5" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="ProjectName" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <!--<td style="text-align: left;">
                    <asp:Label ID="Area_Label" runat="server" Text="Area:" Font-Bold="True"></asp:Label>
                </td>               
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    
                </td>-->
                <td style="text-align: left;">
                    <asp:Label ID="txtServiceDate_Label" runat="server" Text="�ѹ�Դ��ԡ��:" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="1" style="text-align: left; border-top: 1px solid black;">
                    <asp:Label ID="txtServiceDate" runat="server"></asp:Label>
                </td>  
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;">
                    <asp:Label ID="TypeOfContact_Label" runat="server" Text="Type of Contract" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="TypeOfService_Label" runat="server" Text="" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTypeOfService" runat="server"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="CompanyService_Label" runat="server" Text="Company:" Font-Bold="True"></asp:Label>
                </td>
                <td colspan="1" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="lblCompanyService" runat="server"></asp:Label>
                </td>
                <td colspan="4" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="CustomerAssistantName_Label" runat="server" Text="����ʹ��ç���: " Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblCustomerAssistantName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;">
                    <asp:Label ID="Description_Label" runat="server" Text="Customer" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="Contact_Label" runat="server" Text="Contact" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="txtProjectName" runat="server"></asp:Label>
                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                </td>
                <td colspan="2" rowspan="2" style="text-align: left; border-top: 1px solid black;">
                    <asp:Label ID="OneTimePayment_label" runat="server" Text="�������¤������� " Font-Bold="true"></asp:Label><br/>
                    <asp:Label ID="MonthlyPrice_Label" runat="server" Text="��Һ�ԡ�������͹ " Font-Bold="true"></asp:Label><br/>
                    <asp:Label ID="TotalCost_label" runat="server" Text="��Ť���ç��� (������������Ť������) " Font-Bold="true"></asp:Label>
                </td>
                <td colspan="1" rowspan="2" style="text-align: right;  border-top: 1px solid black;">
                    <asp:Label ID="lblOneTimePayment" runat="server"></asp:Label><br/>                
                    <asp:Label ID="lblMonthlyPrice" runat="server"></asp:Label><br/>                  
                    <asp:Label ID="lblTotalCost" runat="server"></asp:Label><br/>
                </td>
                <td colspan="1" rowspan="2" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                    &nbsp;&nbsp;<asp:Label ID="OneTimePayment_Unit" runat="server" Text="   �ҷ"></asp:Label><br/>
                    &nbsp;&nbsp;<asp:Label ID="MonthlyPrice_Unit" runat="server" Text="   �ҷ/��͹"></asp:Label><br/>
                    &nbsp;&nbsp;<asp:Label ID="TotalCost_Unit" runat="server" Text="   �ҷ"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;" class="align-top">
                    <asp:Label ID="SLA_Label" runat="server" Text="SLA" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="txtSLA" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: left;" class="align-top">
                    <asp:Label ID="MonitorDate_Label" runat="server" Text="Monitor Date " Font-Bold="true"></asp:Label>
                </td>
                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="lblMonitorDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;">                    
                    <asp:Label ID="MTTR_Label" runat="server" Text="MTTR" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2" valign="top" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="txtMTTR" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: left;"> 
                    <asp:Label ID="MonitorTime_Label" runat="server" Text="Monitor Time " Font-Bold="true"></asp:Label>
                </td>                
                <td valign="top" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="lblMonitorTime" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-top: 1px solid black;">
                    <asp:Label ID="Guarantee_Label" runat="server" Text="��ѡ��Сѹ�ѭ�� " Font-Bold="true"></asp:Label>
                </td>
                <td colspan="1" style="text-align: right; border-top: 1px solid black;">
                    <asp:Label ID="lblGuarantee" runat="server"></asp:Label>
                    <asp:Label ID="Email_Label" Text="Email " runat="server" Font-Bold="true" Visible="false"></asp:Label>
                    <asp:Label ID="lblCustomerContactEmail" runat="server" Visible="false"></asp:Label>
                </td>
                <td colspan="1" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    &nbsp;&nbsp;<asp:Label ID="Guarantee_Unit" runat="server" Text="   �ҷ"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark" id="Layout_Project_Detail" runat="server" style="height:30px">
                <td style="text-align: left;" class="align-top">
                    <asp:Label ID="Project_Label" runat="server" Text="Approval Info." Font-Bold="true"></asp:Label>
                </td>
                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="txtDetailProject" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark" id="Layout_Service_Detail" runat="server" style="height:30px">
                <td style="text-align: left;" class="align-top">
                    <asp:Label ID="Service_Label" runat="server" Text="Project Details" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <!--<asp:TextBox ID="txtDetailService1" runat="server" TextMode="MultiLine" Height="80px" Width="840px" ReadOnly="true"></asp:TextBox>-->
                    <asp:Label ID="txtDetailService" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;" class="align-top">
                    <asp:Label ID="Fine_Label" runat="server" Text="��һ�Ѻ" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <!--<asp:TextBox ID="txtFine1" runat="server" TextMode="MultiLine" Height="100px" Width="840px" ReadOnly="true"></asp:TextBox>-->
                    <asp:Label ID="txtFine" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblPenaltyLate" runat="server" Text="0" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="tr_CAPEX_Header" runat="server" class="border border-1 border-dark">
                <td colspan="5" class="bg-mint text-light" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Capex_Label" runat="server" Text="��ŧ�ع(CAPEX)" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" class="bg-mint text-light pl-2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Opex_Label" runat="server" Text="�������������͹(OPEX)" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr id="tr_CAPEX_Detail" runat="server" class="border border-1 border-dark">
                <td colspan="5" valign="top">
                    <div id="CAPEX_Detail" runat="server"></div>
                </td>
                <td colspan="4" valign="top">
                    <div id="OPEX_Detail" runat="server" class="pl-2"></div>
                </td>
            </tr>
            <tr id="tr_CAPEX_Total" runat="server" class="text-grey border border-1 border-dark font-weight-bold" style="background-color: #FFFF99;">
                <td align="left" colspan="4">Total Investment Cost</td>
                <td align="right">
                    <asp:Label ID="lblTotalCAPEX" runat="server"></asp:Label>&nbsp;THB
                </td>
                <td align="left" colspan="3" class="pl-2">Total Cost</td>
                <td align="right">
                    <asp:Label ID="lblTotalOPEXALL" runat="server"></asp:Label>&nbsp;THB
                </td>
            </tr>
        </table>
        <asp:Label ID="lblTotalOPEX" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTotalOTHER" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTotalManagement" runat="server" Visible="false"></asp:Label>
        
        <table cellspacing="0" width="100%" id="Layout_Summary" runat="server">
            <tr class="font-weight-bold">
                <td colspan="2">Revenue</td>
                <td align="right"><asp:Label ID="lblRevenuePercent" style="color:#6c757d;" runat="server"></asp:Label></td>
                <td align="right"><asp:Label ID="lblRevenue" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblRevenue_total" runat="server"></asp:Label></td>
                <td class="pl-2" colspan="3" style="background: #87CEFA;">Cumulative Project</td>
                <td align="right" style="background: #87CEFA;"><asp:Label ID="lblContract_EN" runat="server"></asp:Label></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">OPEX</td>
                <td align="right" ><asp:Label ID="lblOPEX" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblOPEX_total" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;�鹷ع�ҧ��õ�Ҵ(Marketing Cost)</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMKTCost" Visible="false" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMKTCost_total" Visible="false" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��ҡ�õ�Ҵ</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMarketing" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMarketing_profit" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����Ѻ�ͧ/��Ңͧ��ѭ</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertainGift" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertainGift_profit" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr id="tr_gift" runat="server">
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��Ңͧ��ѭ</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertain" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertain_profit" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblGift" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblGift_profit" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;�鹷ع Internet Bandwidth&nbsp;&nbsp;<asp:Label ID="lblInternetBW" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCostOfInternet" runat="server"></asp:Label></td>
                <td align="right" style="background:#6c757d;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;�鹷ع Network Bandwidth&nbsp;&nbsp;<asp:Label ID="lblNetworkBW" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCostOfNetwork" runat="server"></asp:Label></td>
                <td align="right" style="background:#6c757d;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;�鹷ع O & M</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCostOfNOC" runat="server" Text=""></asp:Label></td>
                <td align="right" style="background:#6c757d;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;EXP. Jasmine Group</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblJasmineGorup" runat="server" Text=""></asp:Label></td>
                <td align="right" style="background:#6c757d;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="2" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;EXP. Other</td>
                <td align="right"><asp:Label ID="lblOtherPercent" style="color:#6c757d;" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblOther" runat="server" Text=""></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblOther_total" runat="server" Text=""></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;Penalty</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblPenalty" runat="server" Text=""></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblPenalty_total" runat="server" Text=""></asp:Label></td>
                <td colspan="3"></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblUtil" runat="server"></asp:Label></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">Revenue After Operation</td>
                <td align="right"><asp:Label ID="lblRevenue_Operation" runat="server" Text=""></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblRevenue_Operationtotal" runat="server" Text=""></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="2">CAPEX</td>
                <td align="right"><asp:Label ID="lblCAPEXPercent" style="color:#6c757d;" runat="server"></asp:Label></td>
                <td align="right"><asp:Label ID="lblCAPEX_value" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCAPEX_total" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">Cash Flow</td>
                <td align="right"><asp:Label ID="lblCashFlow_value" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCashFlow_total" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>

            <tr class="font-weight-bold">
                <td colspan="4"></td>
                <td align="center" class="border border-1 border-dark">Marginal Profit</td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="4" class="border border-1 border-dark">payback (months)
                    <span class="float-right"><asp:Label ID="lblPayBack" runat="server" Text="">
                        </asp:Label>
                    </span>
                </td>
                <td align="right" class="border border-1 border-dark">
                    <asp:Label ID="lblPayBackProfit" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="4" class="border border-1 border-dark">margin
                    <span class="float-right">
                        <asp:Label ID="lblMargin" runat="server"></asp:Label>%
                    </span>
                </td>
                <td align="right" class="border border-1 border-dark">
                    <asp:Label ID="lblMarginProfit" runat="server"></asp:Label>%
                </td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="4" class="border border-1 border-dark">NPV (5% per year)
                    <span class="float-right">
                        <asp:Label ID="lblNPV" runat="server"></asp:Label>
                    </span>
                </td>
                <td align="right" class="border border-1 border-dark">
                    <asp:Label ID="lblNPVProfit" runat="server"></asp:Label>
                </td>
                <td colspan="3"></td>
            </tr>
        </table>
    </div>

            <div class="modal modal-mint" id="divCancelProject" tabindex="-1">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">���˵ط��¡��ԡ�ç���</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="�к����˵ط��¡��ԡ�ç���:"></asp:Label>
                                        <asp:TextBox ID="txtRemarkCancelProject" cssclass="form-control" runat="server" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancelCancelProject" class="btn btn-danger" runat="server" Text="¡��ԡ" />
                            <asp:Button ID="btnSaveCancelProject" class="btn btn-success" runat="server" Text="�ѹ�֡" OnClientClick="return confirmDelete(this,'�ç��ù��ʶҹШж١¡��ԡ�͡�ҡ�к��ѹ�� <br />����������ö��������䢢��������ա <br />�س��ͧ���¡��ԡ�ç��� �������');" />
                            <asp:Label ID="lblDocNo" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        
                    </div>
                </div>
            </div>

                    <input runat="server" id="hide_flow_no" xd="hide_flow_no" type="hidden" />
			        <input runat="server" id="hide_flow_sub" xd="hide_flow_sub" type="hidden" />
					<input runat="server" id="hide_next_step" xd="hide_next_step" type="hidden" />
					<input runat="server" id="hide_back_step" xd="hide_back_step" type="hidden" />
					<input runat="server" id="hide_department" xd="hide_department" type="hidden" />
					<input runat="server" id="hide_flow_status" xd="hide_flow_status" type="hidden" />
					<input runat="server" id="hide_flow_remark" xd="hide_flow_remark" type="hidden" />
					<input runat="server" id="btn_add_next_hidden" xd="btn_add_next_hidden" OnServerClick="Add_Next" type="submit" style="display:none;" value="Submit Query" />
					<input runat="server" id="hide_depart_id" xd="hide_depart_id" type="hidden" />
					<input runat="server" id="btn_flow_hidden" xd="btn_flow_hidden" OnServerClick="Flow_Submit" type="submit" style="display:none;" value="Submit Query" />
					
					<input runat="server" id="hide_token" xd="hide_token" type="hidden" />
	                <input runat="server" id="hide_uemail" xd="hide_uemail" type="hidden" />
	                <input runat="server" id="hide_uemail_create" xd="hide_uemail_create" type="hidden" />
	                <input runat="server" id="hide_redebt_cause" xd="hide_redebt_cause" type="hidden" />
      
      <asp:Button ID="test" class="btn btn-warning" runat="server" Text="Print PDF"/>
      <asp:HyperLink ID="LinkFileDoc" class="btn btn-danger" runat="server"><i class="fas fa-file-alt"></i> ����͡���Ṻ</asp:HyperLink>
      <asp:Button ID="btnSave" runat="server" OnClientClick="return confirmDelete(this,'��ѧ�ҡ��͹��ѵ����� ��ҹ���������ö��䢢��������ա <br />��ͧ�����͹��ѵ� �������');" class="btn btn-success float-right" Text="��͹��ѵ�" Visible="false" />
      <asp:Button ID="btnSaveToEditProject" runat="server" Text="�ѹ�֡��ҧ�ç���" OnClientClick="return confirmDelete(this,'��úѹ�֡�ç��� �繡�����ҧ�͡���������ա���� Mail ͹��ѵ� �����䢢������������� Edit Project <br />��ͧ��úѹ�֡�ç��� �������');" class="btn btn-success float-right bg-mint mr-2"/>
      <asp:Button ID="btnCancelProject" runat="server" Text="¡��ԡ�ç���" class="btn btn-danger float-right mr-2"/>
      <asp:Label ID="lblPrepaireEmail" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblROMail" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblPresaleMail" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblNOCTotalCost" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblSpecialPrice" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblSpecialApprove" runat="server" Visible="False"></asp:Label>
   <!-- </div>-->
    
</asp:Content>  
