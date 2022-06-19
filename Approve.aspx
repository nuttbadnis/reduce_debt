<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="Approve.aspx.vb" Inherits="Approve" %>

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
    line-height: 15px;
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
.table_flow{
  font-family :"kanit";
}
.table_flow td{
  line-height: 20px;
}
.table_flow> thead > tr > th{
  font-weight: bolder !important;
}
</style>
<link type="text/css" rel="stylesheet" href="App_Inc/_css/pagedata.css?v=3" />       
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active">Approve</li>
            <!--<li class="breadcrumb-item"><a href="Default.aspx">Default</a></li>
            <li class="breadcrumb-item"><a href="add_opex.aspx">Add Opex</a></li>
            <li class="breadcrumb-item"><a href="add_service.aspx">Add Service</a></li>
            <li class="breadcrumb-item active">Summary</li>--></ol>
                       
    <div runat="server" class="col-md-12" id="dvMain" style="border: 1px solid black; padding-bottom: 15px; margin: 10px 0px 10px 0px;line-height: 17px;">
    <%-- <div class="col-md-12 border border-dark p-2">
            <asp:Label ID="Label2" runat="server" Text="" CssClass="float-right"></asp:Label>
            <div class="col-md-12" style="text-align : center;">
                <asp:Label ID="Label1" runat="server" Text="แบบอนุมัติ" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblProjectName" runat="server" Font-Bold="True" ></asp:Label>
            </div>
        </div> --%>
        <table cellspacing="0" class="w-100 mt-3">
            <tr class="border border-1 border-dark">
                <td colspan="9" class="text-center pt-2 pb-2 text-bold">
                    <asp:Label ID="lblProjectName" runat="server" Text="แบบอนุมัติแผนงานขยายโครงข่ายและบริการ" Font-Bold="True"></asp:Label>
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
                    <asp:Label ID="ContractPeriod_Label" runat="server" Text="สัญญา " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblContract" runat="server"></asp:Label>
                    <asp:Label ID="ContractPeriod_Unit" runat="server" Text="   เดือน"></asp:Label>
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
                    <asp:Label ID="CustomerType_Label" runat="server" Text="ประเภทลูกค้า:" Font-Bold="True"></asp:Label>
                </td>               
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="lblCustomerType" runat="server"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="Date_Label" runat="server" Text="วันที่จัดทำเอกสาร:" Font-Bold="true"></asp:Label>
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
               <!-- <td style="text-align: left;">
                    <asp:Label ID="Area_Label" runat="server" Text="Area:" Font-Bold="True"></asp:Label>
                </td>               
                <td colspan="2" style="text-align: left; border-right: 1px solid black;">
                    
                </td>-->
                <td style="text-align: left;">
                    <asp:Label ID="txtServiceDate_Label" runat="server" Text="วันเปิดบริการ:" Font-Bold="true"></asp:Label>
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
                    <asp:Label ID="CustomerAssistantName_Label" runat="server" Text="ผู้เสนอโครงการ: " Font-Bold="True"></asp:Label>
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
                    <asp:Label ID="OneTimePayment_label" runat="server" Text="ค่าใช้จ่ายครั้งเดียว " Font-Bold="true"></asp:Label><br/>
                    <asp:Label ID="MonthlyPrice_Label" runat="server" Text="ค่าบริการรายเดือน " Font-Bold="true"></asp:Label><br/>
                    <asp:Label ID="TotalCost_label" runat="server" Text="มูลค่าโครงการ (ไม่รวมภาษีมูลค่าเพิ่ม) " Font-Bold="true"></asp:Label>
                </td>
                <td colspan="1" rowspan="2" style="text-align: right;  border-top: 1px solid black;">
                    <asp:Label ID="lblOneTimePayment" runat="server"></asp:Label><br/>                
                    <asp:Label ID="lblMonthlyPrice" runat="server"></asp:Label><br/>                  
                    <asp:Label ID="lblTotalCost" runat="server"></asp:Label><br/>
                </td>
                <td colspan="1" rowspan="2" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                    &nbsp;&nbsp;<asp:Label ID="OneTimePayment_Unit" runat="server" Text="   บาท"></asp:Label><br/>
                    &nbsp;&nbsp;<asp:Label ID="MonthlyPrice_Unit" runat="server" Text="   บาท/เดือน"></asp:Label><br/>
                    &nbsp;&nbsp;<asp:Label ID="TotalCost_Unit" runat="server" Text="   บาท"></asp:Label>
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
                    <asp:Label ID="Guarantee_Label" runat="server" Text="หลักประกันสัญญา " Font-Bold="true"></asp:Label>
                </td>
                <td colspan="1" style="text-align: right; border-top: 1px solid black;">
                    <asp:Label ID="lblGuarantee" runat="server"></asp:Label>
                    <asp:Label ID="Email_Label" Text="Email " runat="server" Font-Bold="true" Visible="false"></asp:Label>
                    <asp:Label ID="lblCustomerContactEmail" runat="server" Visible="false"></asp:Label>
                </td>
                <td colspan="1" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    &nbsp;&nbsp;<asp:Label ID="Guarantee_Unit" runat="server" Text="   บาท"></asp:Label>
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
                    <asp:Label ID="txtDetailService" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left;" class="align-top">
                    <asp:Label ID="Fine_Label" runat="server" Text="ค่าปรับ" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="txtFine" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblPenaltyLate" runat="server" Text="0" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="tr_CAPEX_Header" runat="server" class="border border-1 border-dark">
                <td colspan="5" class="bg-mint text-light" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Capex_Label" runat="server" Text="งบลงทุน(CAPEX)" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" class="bg-mint text-light pl-2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Opex_Label" runat="server" Text="ค่าใช้จ่ายรายเดือน(OPEX)" Font-Bold="true"></asp:Label>
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
        
        <table width="100%" id="Layout_Summary" runat="server">
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
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุนทางการตลาด(Marketing Cost)</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMKTCost" Visible="false" runat="server" ></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMKTCost_total"  Visible="false" runat="server" ></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ค่าการตลาด</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMarketing" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblMarketing_profit" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ค่ารับรอง/ค่าของขวัญ</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertainGift" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertainGift_profit" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr id="tr_gift" runat="server">
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ค่าของขวัญ</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertain" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblEntertain_profit" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblGift" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblGift_profit" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุน Internet Bandwidth&nbsp;&nbsp;<asp:Label ID="lblInternetBW" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCostOfInternet" runat="server"></asp:Label></td>
                <td align="right" style="background:#6c757d;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุน Network Bandwidth&nbsp;&nbsp;<asp:Label ID="lblNetworkBW" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCostOfNetwork" runat="server"></asp:Label></td>
                <td align="right" style="background:#6c757d;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#6c757d;">&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุน O & M</td>
                <td align="right" style="color:#6c757d;"><asp:Label ID="lblCostOfNOC" runat="server" Text=""></asp:Label></td>
                <td id="td_CostNOC_Profit" runat="server" align="right" style="background:#6c757d;"><asp:Label ID="lblCostOfNOC_Profit" runat="server" Text=""></asp:Label></td>
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
                <td colspan="4"></td>
            </tr>
        </table>
        
    </div>
    
        <asp:Label ID="lblPrepaireEmail" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblROMail" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblNOCTotalCost" runat="server" Visible="False"></asp:Label>
          
        <asp:Button ID="test" class="btn btn-warning" runat="server" Text="Print PDF"/>
        <asp:HyperLink ID="LinkFileDoc" class="btn btn-danger text-light" runat="server"><i class="fas fa-file-alt"></i> ไฟล์เอกสารแนบ</asp:HyperLink> 
        <asp:HyperLink ID="LinkProjectFile" cssclass="btn btn-info text-white" runat="server"><i class="fas fa-file-alt"></i> Project File</asp:HyperLink>
        <asp:Button ID="btnSave" runat="server" Text="Save" Visible="False" />
        <asp:LinkButton ID="LinkButton1" runat="server" Visible="False">#</asp:LinkButton>

    <div class="card">
	    <div class="card-header bg-default p-2">Flow Step</div>
		    <div class="panel-body p-2">
			    <table id="table_flow" class="table table-striped table_flow">
			    <thead class='txt-blue'>
                    <tr>
                        <th style="width: 2%;">#</th>
                        <th style="width: 3%;">Step</th>
                        <!--<th style="width: 5%;">Next</th>-->
                        <th style="width: 10%;">ส่วนงาน</th>
                        <th style="width: 5%;">สถานะ</th>
                        <th style="width: 15%;">วัน-เวลา</th>
                        <th style="width: 15%;">อีเมล์</th>
                        <th style="width: 15%;">ผู้ดำเนินการ</th>
                        <th style="width: 30%;">หมายเหตุ</th>
                        <th style="width: 0%;"></th>
                    </tr>
				</thead>
		            <tbody runat="server" id="inn_flow"></tbody>
			    </table>
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
	                <input runat="server" id="hide_project_code" xd="hide_project_code" type="hidden" />
	                <input runat="server" id="hide_contract_file" xd="hide_contract_file" type="hidden" />
	                <input runat="server" id="hide_contract_file1"  type='file' style="display:none;" />
		    </div>
	    </div>
    

        

</asp:Content>  


 <asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server"> 

 <script type="text/javascript" src="App_Inc/_js/check_required.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/request_update.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/flow_submit.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/redebt_search_autoinput.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/redebt_operator.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/redebt_pick_refund.js?v=3"></script>

<script type="text/javascript">
$(document).ready(function() { 
	$('[data-toggle="popover"]').popover({html:true, trigger:"hover"}); 

	setDatePicker();
	checkRefund();

	count_acc_RQclose($('input[xd="txt_account_number"]').val());
	count_acc_RQprocess($('input[xd="txt_account_number"]').val());

	loadCause($('select[xd="sel_title"]').val(), $('input[xd="hide_redebt_cause"]').val());
	loadDescApprove();
	loadDescVerify();

	$('#page_loading').fadeOut();
});
</script> 
 </asp:Content> 