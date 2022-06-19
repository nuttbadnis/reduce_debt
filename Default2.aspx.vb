Imports System.IO
Imports System.Net
Imports System.Data
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net.IPAddress
Imports System.Threading

Partial Class Default2
    Inherits System.Web.UI.Page
    Dim N as new test_vb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'heading
        Dim lblProjectName As String = ""
    'row_1
    Dim RefNo AS string = ""
    Dim intenal_refno AS string = ""
    Dim txtDocumentDate AS string = ""
    'row_2
    Dim lblProjectCode AS string = ""
    Dim lblCustomerType AS string = ""
    Dim txtServiceDate AS string = ""
    'row_3
    Dim ProjectName AS string = ""
    Dim lblArea AS string = ""
    Dim lblCluster AS string = ""

    ' <table cellspacing='0' style='width: 100%;border-left: 1px solid black;border-right: 1px solid black;border-bottom-style: none;'>
    '     <tr class='border border-1 border-dark' valign='top'>
    '      <td colspan='5' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '         <asp:Label ID='TypeOfContact_Label' runat='server' Text='Type of Contact' Font-Bold='true'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
    '         <asp:Label ID='TypeOfService_Label' runat='server' Text='Service:' Font-Bold='True'></asp:Label>
    '         <asp:Label ID='lblTypeOfService' runat='server'></asp:Label>
    '      </td>
                                
    '                             <td colspan='4' style='text-align: left; border-right: 1px solid black;border-bottom: 1px solid black; '>
    '                                 <asp:Label ID='CompanyService_Label' runat='server' Text='Company:' Font-Bold='True'></asp:Label>
    '                                 <asp:Label ID='lblCompanyService' runat='server'></asp:Label>
    '                             </td>
    '                         </tr>
    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td style='text-align: left; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='Description_Label' runat='server' Text='Description' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td colspan='4' style='text-align: left;border-bottom: 1px solid black; border-right: 1px solid black;'>
    '                                 <asp:Label ID='txtProjectName' runat='server'></asp:Label>
    '                                 <asp:Label ID='Label11' runat='server'>ชื่อผู้ติดต่อ&nbsp;</asp:Label>
    '                                 <asp:Label ID='lblCustomerName' runat='server'></asp:Label>
    '                             </td>
    '                             <td colspan='2' rowspan='2' style='text-align: left; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='OneTimePayment_label' runat='server' Text='ค่าใช้จ่ายครั้งเดียว '></asp:Label><br/>
    '                                 <asp:Label ID='MonthlyPrice_Label' runat='server' Text='ค่าใช้จ่ายรายเดือน '></asp:Label><br/>
    '                                 <asp:Label ID='TotalCost_label' runat='server' Text='มูลค่าโครงการ '></asp:Label>
    '                             </td>
    '                             <td colspan='2' rowspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='lblOneTimePayment' runat='server'></asp:Label>
    '                                 <asp:Label ID='OneTimePayment_Unit' runat='server' Text='   บาท'></asp:Label><br/>
    '                                 <asp:Label ID='lblMonthlyPrice' runat='server'></asp:Label>
    '                                 <asp:Label ID='MonthlyPrice_Unit' runat='server' Text='   บาท/เดือน'></asp:Label><br/>
    '                                 <asp:Label ID='lblTotalCost' runat='server'></asp:Label>
    '                                 <asp:Label ID='TotalCost_Unit' runat='server' Text='   บาท(ไม่รวมภาษีมูลค่าเพิ่ม)'></asp:Label>
    '                             </td>
    '                         </tr>
    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td style='text-align: left;border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='SLA_Label' runat='server' Text='SLA' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='txtSLA' runat='server' ></asp:Label>
    '                             </td>
    '                             <td style='text-align: left;border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='MTTR_Label' runat='server' Text='MTTR' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='txtMTTR'  runat='server'></asp:Label>
    '                             </td>
    '                         </tr>
    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td style='text-align: left; border-bottom: 1px solid black;'>                    
    '                                 <asp:Label ID='MonitorDate_Label' runat='server' Text='Monitor Date ' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td colspan='2' valign='top' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='lblMonitorDate' runat='server'></asp:Label>
    '                             </td>
    '                             <td style='text-align: left;border-bottom: 1px solid black;'> 
    '                                 <asp:Label ID='MonitorTime_Label' runat='server' Text='Monitor Time ' Font-Bold='true'></asp:Label>
    '                             </td>                
    '                             <td valign='top' style=-'text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='lblMonitorTime' runat='server'></asp:Label>
    '                             </td>
    '                             <td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='ContractPeriod_Label' runat='server' Text='สัญญา '></asp:Label>
    '                                 <asp:Label ID='lblContract' runat='server'></asp:Label>
    '                                 <asp:Label ID='ContractPeriod_Unit' runat='server' Text='   months'></asp:Label>
    '                             </td>
    '                             <td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>
    '                                 <asp:Label ID='Email_Label' Text='Email ' runat='server'></asp:Label>
    '                                 <asp:Label ID='lblCustomerContactEmail' runat='server'></asp:Label>
    '                             </td>
    '                         </tr>
    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td style='text-align: left; border-right: 1px solid black;'>
    '                                 <asp:Label ID='Service_Label' runat='server' Text='Service' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td colspan='8' style='text-align: left; border-right: 1px solid black;'>
    '                                 <div id='txtDetailService' runat='server'></div>
    '                             </td>
    '                         </tr>

    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td style='text-align: left; border-right: 1px solid black; border-top: 1px solid black;'>
    '                                 <asp:Label ID='Fine_Label' runat='server' Text='ค่าปรับ' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td colspan='8' style='text-align: left; border-right: 1px solid black; border-top: 1px solid black;'>
    '                                 <div id='txtFine' runat='server'></div>                           
    '                             </td>
    '                         </tr>
    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td colspan='5' class='bg-mint text-light' style='text-align: left; border-top: 1px solid black;color:red;'>
    '                                 <asp:Label ID='Capex_Label' runat='server' Text='งบลงทุน(CAPEX)' Font-Bold='true'></asp:Label>
    '                             </td>
    '                             <td colspan='4' class='bg-mint text-light' style='text-align: left; border-top: 1px solid black;color:red;'>
    '                                 <asp:Label ID='Opex_Label' runat='server' Text='ค่าใช้จ่ายรายเดือน(OPEX)' Font-Bold='true'></asp:Label>
    '                             </td>
    '                         </tr>
    '                         <tr class='border border-1 border-dark' valign='top'>
    '                             <td colspan='5' valign='top'>
    '                                 <div id='CAPEX_Detail' runat='server'></div>
    '                             </td>
    '                             <td colspan='4' valign='top' width='46.5%'>
    '                                 <div id='OPEX_Detail' runat='server'></div>
    '                             </td>
    '                         </tr>
    '                         <tr class='text-grey border border-1 border-dark font-weight-bold' style='background-color: #FFFF99;border: 1px solid black;' valign='top'>
    '                             <td align='left' style='border-bottom: 1px solid black; border-top: 1px solid black;' colspan='4'>Total Investment Cost</td>
    '                             <td align='right' style='border-bottom: 1px solid black; border-top: 1px solid black;'>
    '                                 <asp:Label ID='lblTotalCAPEX' runat='server'></asp:Label></td>
    '                             <td align='left' colspan='3' width='10%' style='border-bottom: 1px solid black; border-top: 1px solid black;'>Total Cost</td>
    '                             <td align='right' style='border-bottom: 1px solid black; border-top: 1px solid black;'>
    '                                 <asp:Label ID='lblTotalOPEXALL' runat='server'></asp:Label></td>
    '                         </tr>
    '                     </table>
    '     <asp:Label ID='lblTotalOPEX' runat='server' Visible='false'></asp:Label>
    '     <asp:Label ID='lblTotalOTHER' runat='server' Visible='false'></asp:Label>

    '     <table width='100%' style='border-collapse:collapse;'>
    '         <tr class='font-weight-bold'>
    '             <td colspan='2' width='10%'>Revenue</td>
    '             <td align='right'><asp:Label ID='lblRevenuePercent' style='color:#6c757d;' runat='server'></asp:Label></td>
    '             <td align='right'><asp:Label ID='lblRevenue' runat='server'></asp:Label></td>
    '             <td align='right' style='color:#6c757d;' width='10%'><asp:Label ID='lblRevenue_total' runat='server'></asp:Label></td>
    '             <td colspan='3' style='background: #87CEFA;'>Cumulative Project</td>
    '             <td align='right' style='background: #87CEFA;'><asp:Label ID='lblContract_EN' runat='server'></asp:Label></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='3'>OPEX</td>
    '             <td align='right' ><asp:Label ID='lblOPEX' runat='server'></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblOPEX_total' runat='server'></asp:Label></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='3' style='color:#6c757d;'>ต้นทุนทางการตลาด(Marketing Cost)</td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblMKTCost' runat='server'></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblMKTCost_total' runat='server'></asp:Label></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='3' style='color:#6c757d;'>ต้นทุน Internet Bandwidth</td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblCostOfInternet' runat='server'></asp:Label></td>
    '             <td align='right' style='background:#6c757d;'></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='3' style='color:#6c757d;'>ต้นทุน Network Bandwidth</td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblCostOfNetwork' runat='server'></asp:Label></td>
    '             <td align='right' style='background:#6c757d;'></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='3' style='color:#6c757d;'>ต้นทุน NOC</td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblCostOfNOC' runat='server' Text=''></asp:Label></td>
    '             <td align='right' style='background:#6c757d;'></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='3' style='color:#6c757d;'>EXP. Jasmine Group</td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblJasmineGorup' runat='server' Text=''></asp:Label></td>
    '             <td align='right' style='background:#6c757d;'></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='2' style='color:#6c757d;'>EXP. Other</td>
    '             <td align='right'><asp:Label ID='lblOtherPercent' style='color:#6c757d;' runat='server'></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblOther' runat='server' Text=''></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblOther_total' runat='server' Text=''></asp:Label></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr>
    '             <td colspan='3' style='color:#6c757d;'>Penalty</td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblPenalty' runat='server' Text=''></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblPenalty_total' runat='server' Text=''></asp:Label></td>
    '             <td colspan='3'></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblUtil' runat='server'></asp:Label></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='3'>Revenue After Operation</td>
    '             <td align='right'><asp:Label ID='lblRevenue_Operation' runat='server' Text=''></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblRevenue_Operationtotal' runat='server' Text=''></asp:Label></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='2'>CAPEX</td>
    '             <td align='right'><asp:Label ID='lblCAPEXPercent' style='color:#6c757d;' runat='server'></asp:Label></td>
    '             <td align='right'><asp:Label ID='lblCAPEX_value' runat='server'></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblCAPEX_total' runat='server'></asp:Label></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='3'>Cash Flow</td>
    '             <td align='right'><asp:Label ID='lblCashFlow_value' runat='server'></asp:Label></td>
    '             <td align='right' style='color:#6c757d;'><asp:Label ID='lblCashFlow_total' runat='server'></asp:Label></td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td style='border-right: 1px solid black;border-bottom: 1px solid black;' colspan='4'></td>
    '             <td align='right'  style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'>Marginal Profit</td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='3' style='border-left: 1px solid black;border-bottom: 1px solid black;'>payback (months)
    '             </td>
    '             <td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>
    '                 <asp:Label ID='lblPayBack' runat='server' Text=''></asp:Label>
    '             </td>
    '             <td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>
    '                 <asp:Label ID='lblPayBackProfit' runat='server' Text=''></asp:Label>
    '             </td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='3'  style='border-left: 1px solid black;border-bottom: 1px solid black;'>margin
    '             </td>
    '             <td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>
    '                     <asp:Label ID='lblMargin' runat='server'></asp:Label>%
    '                </td>
    '             <td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>
    '                 <asp:Label ID='lblMarginProfit' runat='server'></asp:Label>%
    '             </td>
    '             <td colspan='4'></td>
    '         </tr>
    '         <tr class='font-weight-bold'>
    '             <td colspan='3' style='border-left: 1px solid black;border-bottom: 1px solid black;'>NPV (5% per year)
    '             </td>
    '             <td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>
    '                 <asp:Label ID='lblNPV' runat='server'></asp:Label>
    '             </td>
    '             <td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>
    '                 <asp:Label ID='lblNPVProfit' runat='server'></asp:Label>
    '             </td>
    '             <td colspan='4'></td>
    '         </tr>
    '     </table>
    '     </td>
    '     </tr>

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        N.SendMailSubmit("FES202008-013")
    End Sub

End Class
