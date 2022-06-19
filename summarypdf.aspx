<%@ Page Language="VB" AutoEventWireup="false" CodeFile="summarypdf.aspx.vb" Inherits="Default2" ValidateRequest="false" %>

<!DOCTYPE html>
<meta content='text/html'; charset='utf-8'/>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>print pdf</title>
          <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblNOCTotalCost" runat="server" Visible="False"></asp:Label>
    <div id="Grid" runat="server">
        <table style="width: 100%;border: 1px solid black;margin: auto;font-family:'TH Sarabun New';">
                 <tr>
                     <td >
                       <table style="width: 100%;border: 1px solid black;margin: 0px 0px 0px 0px;border-bottom-style: none;">
                           <tr>
                              <td style="text-align: right; height: 10px;">
                                    
                              </td>
                           </tr>
                           <tr>
                             <td style="font-weight: bolder;text-align: center;font-family:'TH Sarabun New'; height: 30px;" valign="top">    
                                 <asp:Label ID="lblProjectName" runat="server" Font-Bold="True" Font-Size="Large" Text="Ẻ͹��ѵ�Ἱ�ҹ�����ç������к�ԡ��" ></asp:Label>
                             </td>  
                           </tr>
                        </table>
                        <table cellspacing="0" style="width: 100%;border: 1px solid black;border-bottom-style: none;">
                           <tr class="border border-1 border-dark" valign="top">
                                <td style="text-align: left; border-bottom: 1px solid black;width: 10%;">
                                    <asp:Label ID="RefNo_Label" runat="server" Text="CS Ref.No. " Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;">
                                    <asp:Label ID="refno" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="Prepare_Label" runat="server" Text="Internal Ref.No. " Font-Bold="true">
                                    </asp:Label>
                                </td>
                                <td colspan="2" style="text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;">
                                    <asp:Label ID="intenal_refno" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="Cluster_Label" runat="server" Text="Area/Cluster:" Font-Bold="True"></asp:Label>
                                </td>                
                                <td colspan="1" style="text-align: left; border-right: 1px solid black;border-bottom: 1px solid black;">
                                    <asp:Label ID="lblArea" runat="server"></asp:Label>/<asp:Label ID="lblCluster" runat="server"></asp:Label>
                                </td>
                                <td rowspan="3" style="text-align: center; vertical-align: middle; border-bottom: 1px solid black;" >
                                    <asp:Label ID="ContractPeriod_Label" runat="server" Text="�ѭ�� "></asp:Label>
                                    <asp:Label ID="lblContract" runat="server"></asp:Label>
                                    <asp:Label ID="ContractPeriod_Unit" runat="server" Text="   ��͹"></asp:Label>
                                </td>
                           </tr>
                           <tr class="border border-1 border-dark" valign="top">
                                <td style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="ProjectCode_Label" runat="server" Text="Project Code:" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;">
                                    <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="CustomerType_Label" runat="server" Text="�������١���:" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;">
                                    <asp:Label ID="lblCustomerType" runat="server"></asp:Label>
                                </td>
                               <td style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="Date_Label" runat="server" Text="�ѹ���Ѵ���͡���:" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="1" style="text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;">
                                    <asp:Label ID="txtDocumentDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="border border-1 border-dark" valign="top">
                                <td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="ProjectName_Label" runat="server" Text="Project Name:" Font-Bold="true"></asp:Label>
                                </td>          
                                <td colspan="5" style="text-align: left; border-right: 1px solid black;border-bottom: 1px solid black;">
                                    <asp:Label ID="ProjectName" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <!--<td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="Area_Label" runat="server" Text="Area:" Font-Bold="True"></asp:Label>
                                </td>               
                                <td colspan="2" style="text-align: left; border-right: 1px solid black;border-bottom: 1px solid black;">
                                    
                                </td>-->
                                <td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="txtServiceDate_Label" runat="server" Text="�ѹ�Դ��ԡ��:" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="1" style="text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;">
                                    <asp:Label ID="txtServiceDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                            <table cellspacing="0" style="width: 100%;border-left: 1px solid black;border-right: 1px solid black;border-bottom-style: none;">

                            <tr class="border border-1 border-dark" valign="top">
                                <td colspan="3" width="32%" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="TypeOfContact_Label" runat="server" Text="Type of Contract" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="TypeOfService_Label" runat="server" Text="" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblTypeOfService" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="CompanyService_Label" runat="server" Text="Company:" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="1" width="10%"  style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="lblCompanyService" runat="server"></asp:Label>
                                </td>
                                <td colspan="4" style="text-align: left; border-right: 1px solid black;border-bottom: 1px solid black; ">
                                    <asp:Label ID="CustomerAssistantName_Label" runat="server" Text="����ʹ��ç���: " Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblCustomerAssistantName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="border border-1 border-dark" valign="top">
                                <td width="12%" style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="Description_Label" runat="server" Text="Customer" Font-Bold="true"></asp:Label><br />
                                    <asp:Label ID="Contact_Label" runat="server" Text="Contact" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="4" style="text-align: left;border-bottom: 1px solid black; border-right: 1px solid black;">
                                    <asp:Label ID="txtProjectName" runat="server"></asp:Label>
                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                </td>
                                <td width="22%" colspan="2" rowspan="2" style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="OneTimePayment_label" runat="server" Text="�������¤������� "></asp:Label><br/>
                                    <asp:Label ID="MonthlyPrice_Label" runat="server" Text="��Һ�ԡ�������͹ "></asp:Label><br/>
                                    <asp:Label ID="TotalCost_label" runat="server" Text="��Ť���ç��� (������������Ť������)"></asp:Label>
                                </td>
                                <td colspan="1" rowspan="2" style="text-align: right; border-bottom: 1px solid black;">
                                    <asp:Label ID="lblOneTimePayment" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblMonthlyPrice" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblTotalCost" runat="server"></asp:Label><br />
                                </td>
                                <td width="9%" colspan="1" rowspan="2" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;&nbsp;<asp:Label ID="OneTimePayment_Unit" runat="server" Text="   �ҷ"></asp:Label><br/>
                                    &nbsp;&nbsp;<asp:Label ID="MonthlyPrice_Unit" runat="server" Text="   �ҷ/��͹"></asp:Label><br/>
                                    &nbsp;&nbsp;<asp:Label ID="TotalCost_Unit" runat="server" Text="   �ҷ"></asp:Label>
                                </td>
                            </tr>
                            <tr class="border border-1 border-dark" valign="top">
                                <td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="SLA_Label" runat="server" Text="SLA" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="txtSLA" runat="server" ></asp:Label>
                                </td>
                                <td style="text-align: left;border-bottom: 1px solid black;">
                                    <asp:Label ID="MonitorDate_Label" runat="server" Text="Monitor Date " Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="lblMonitorDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="border border-1 border-dark" valign="top">
                                <td style="text-align: left; border-bottom: 1px solid black;"> 
                                    <asp:Label ID="MTTR_Label" runat="server" Text="MTTR" Font-Bold="true"></asp:Label>                   
                                </td>
                                <td colspan="2" valign="top" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="txtMTTR"  runat="server"></asp:Label>
                                </td>
                                <td style="text-align: left;border-bottom: 1px solid black;"> 
                                    <asp:Label ID="MonitorTime_Label" runat="server" Text="Monitor Time " Font-Bold="true"></asp:Label>
                                </td>                
                                <td valign="top" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="lblMonitorTime" runat="server"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: left; border-bottom: 1px solid black;">
                                    <asp:Label ID="Guarantee_Label" runat="server" Text="��ѡ��Сѹ�ѭ�� " Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="1" style="text-align: right; border-bottom: 1px solid black;">
                                    <asp:Label ID="lblGuarantee" runat="server"></asp:Label>
                                    <asp:Label ID="Email_Label" Text="Email " runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCustomerContactEmail" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td colspan="1" style="text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;">
                                    &nbsp;&nbsp;<asp:Label ID="Guarantee_Unit" runat="server" Text="   �ҷ"></asp:Label>
                                </td>
                            </tr>
                            <tr class="border border-1 border-dark" id="Layout_Project_Detail" runat="server" style="height:30px" valign="top">
                                <td style="text-align: left; border-right: 1px solid black;">
                                    <asp:Label ID="Project_Label" runat="server" Text="Approval Info." Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="8" style="text-align: left; border-right: 1px solid black;">
                                    <asp:Label ID="txtDetailProject" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr class="border border-1 border-dark" id="Layout_Service_Detail" runat="server" style="height:30px" valign="top">
                                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black; ">
                                    <asp:Label ID="Service_Label" runat="server" Text="Project Details" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black; ">
                                    <asp:Label id="txtDetailService" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr class="border border-1 border-dark" valign="top">
                                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label ID="Fine_Label" runat="server" Text="��һ�Ѻ" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;">
                                    <asp:Label id="txtFine" runat="server"></asp:Label>                          
                                </td>
                            </tr>
                            <tr id="tr_CAPEX_Header" runat="server" class="border border-1 border-dark" valign="top">
                                <td colspan="5" class="bg-mint text-light" style="text-align: left; border-top: 1px solid black;color:red;">
                                    <asp:Label ID="Capex_Label" runat="server" Text="��ŧ�ع(CAPEX)" Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="4" class="bg-mint text-light" style="text-align: left; border-top: 1px solid black;color:red;">
                                    <asp:Label ID="Opex_Label" runat="server" Text="�������������͹(OPEX)" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr id="tr_CAPEX_Detail" runat="server" class="border border-1 border-dark" valign="top">
                                <td colspan="5" valign="top">
                                    <div id="CAPEX_Detail" runat="server"></div>
                                </td>
                                <td colspan="4" valign="top" width="46.5%">
                                    <div id="OPEX_Detail" runat="server"></div>
                                </td>
                            </tr>
                            <tr id="tr_CAPEX_Total" runat="server" class="text-grey border border-1 border-dark font-weight-bold" style="background-color: #FFFF99;border: 1px solid black;" valign="top">
                                <td align="left" style="border-bottom: 1px solid black; border-top: 1px solid black;" colspan="4">Total Investment Cost</td>
                                <td align="right" style="border-bottom: 1px solid black; border-top: 1px solid black;">
                                    <asp:Label ID="lblTotalCAPEX" runat="server"></asp:Label>&nbsp;THB</td>
                                <td align="left" colspan="3" width="10%" style="border-bottom: 1px solid black; border-top: 1px solid black;">Total Cost</td>
                                <td align="right" style="border-bottom: 1px solid black; border-top: 1px solid black;">
                                    <asp:Label ID="lblTotalOPEXALL" runat="server"></asp:Label>&nbsp;THB</td>
                            </tr>
                        </table>
        <asp:Label ID="lblTotalOPEX" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTotalOTHER" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTotalManagement" runat="server" Visible="false"></asp:Label>

        <table width="100%" style="border-collapse:collapse;" id="Layout_Summary" runat="server">
            <tr class="font-weight-bold">
                <td colspan="2" width="10%">Revenue</td>
                <td align="right"><asp:Label ID="lblRevenuePercent" style="color:#6c757d;" runat="server"></asp:Label></td>
                <td align="right"><asp:Label ID="lblRevenue" runat="server"></asp:Label></td>
                <td align="right" style="color:#6c757d;" width="10%"><asp:Label ID="lblRevenue_total" runat="server"></asp:Label></td>
                <td colspan="3" style="background: #87CEFA;">Cumulative Project</td>
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
                <td style="border-right: 1px solid black;border-bottom: 1px solid black;" colspan="4"></td>
                <td align="right"  style="border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;">Marginal Profit</td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3" style="border-left: 1px solid black;border-bottom: 1px solid black;">payback (months)
                </td>
                <td align="right" style="border-right: 1px solid black;border-bottom: 1px solid black;">
                    <asp:Label ID="lblPayBack" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" style="border-right: 1px solid black;border-bottom: 1px solid black;">
                    <asp:Label ID="lblPayBackProfit" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3"  style="border-left: 1px solid black;border-bottom: 1px solid black;">margin
                </td>
                <td align="right" style="border-right: 1px solid black;border-bottom: 1px solid black;">
                        <asp:Label ID="lblMargin" runat="server"></asp:Label>%
                   </td>
                <td align="right" style="border-right: 1px solid black;border-bottom: 1px solid black;">
                    <asp:Label ID="lblMarginProfit" runat="server"></asp:Label>%
                </td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3" style="border-left: 1px solid black;border-bottom: 1px solid black;">NPV (5% per year)
                </td>
                <td align="right" style="border-right: 1px solid black;border-bottom: 1px solid black;">
                    <asp:Label ID="lblNPV" runat="server"></asp:Label>
                </td>
                <td align="right" style="border-right: 1px solid black;border-bottom: 1px solid black;">
                    <asp:Label ID="lblNPVProfit" runat="server"></asp:Label>
                </td>
                <td colspan="4"></td>
            </tr>
        </table>
	    
        </td>
        </tr>
    </table>
    
    <table id="table_flow" class="border border-1 border-dark" cellspacing="0" style="width: 100%;border:1px solid black;margin-top:10px; font-family:'TH Sarabun New';" >
            <tr>
                <!--<th style="width: 5%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>#</th>-->
                <th style="width: 5%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>�ӴѺ</th>
                <!--<th style="width: 5%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>Next</th>-->
                <th style="width: 15%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>��ǹ�ҹ</th>
                <th style="width: 15%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>ʶҹ�</th>
                <th style="width: 15%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>�ѹ����</th>
                <th style="width: 23%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>�����Թ���</th>
                <!--<th style="width: 20%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>������</th>-->
                <th style="width: 27%;border-bottom:1px solid black;border-right:1px solid black;" align='center'>�����˵�</th>
                <!--<th style="width: 15%;border-bottom:1px solid black;">�͡��û�Сͺ</th>-->
            </tr>
		    <tbody runat="server" id="inn_flow"></tbody>
		</table>

    </div>
    <asp:Label ID="lblPrepaireEmail" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblROMail" runat="server" Visible="False"></asp:Label>


    <asp:HiddenField ID="hfGridHtml" runat="server" />
    <%-- <asp:HiddenField ID="hfService" runat="server" /> --%>
        
    <asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClick="exportpdf" style="display:none;"/>
        
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("btnExport").click();
        };
        $(function () {
            $("[id*=btnExport]").click(function () {
                $("[id*=hfGridHtml]").val($("#Grid").html());
            });
        });

    </script>           
    </form>
  
</body>
</html>



