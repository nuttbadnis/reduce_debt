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
            <li class="breadcrumb-item"><a id="menu_project_name" runat="server">Edit Project Name</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
            <li class="breadcrumb-item"><a id="menu_capex" runat="server">Edit Capex</a></li>
            <li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_other" runat="server">Edit Other</a></li>
            <li class="breadcrumb-item active">Edit Summary</li></ol>
            

            <div class="col-md-12" id="dvMain" style="border: 1px solid black; padding-bottom: 15px; margin: 10px 0px 20px 0px;">
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
                    <asp:Label ID="lblProjectName" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="RefNo_Label" runat="server" Text="CS Ref.No. " Font-Bold="true"></asp:Label>
                    <asp:Label ID="refno" runat="server"></asp:Label>
                </td>
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="Prepare_Label" runat="server" Text="Internal Ref.No. " Font-Bold="true"></asp:Label>
                    <!--<input id="txtDocumentDate1" runat="server" type="text" data-field="date"/>
                                  <div id="dtBox"></div>-->
                </td>
                <td colspan="3" style="text-align: left;">
                    <asp:Label ID="Date_Label" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    <asp:Label ID="txtDocumentDate" runat="server"></asp:Label>
                </td>
            </tr>

            <tr class="border border-1 border-dark">
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="ProjectCode_Label" runat="server" Text="Project Code:" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                    <!--<asp:TextBox ID="txtLocationName1" runat="server"></asp:TextBox>-->
                </td>
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="CustomerType_Label" runat="server" Text="ประเภทลูกค้า:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblCustomerType" runat="server"></asp:Label>
                </td>
                <td colspan="3" style="text-align: left; border-top: 1px solid black;">
                    <asp:Label ID="txtServiceDate_Label" runat="server" Text="วันเปิดบริการ:" Font-Bold="true"></asp:Label>
                    <asp:Label ID="txtServiceDate" runat="server"></asp:Label>
                    <!--<asp:DropDownList ID="ddlCluster" runat="server" Width="70px" Visible="false"></asp:DropDownList>-->
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="ProjectName_Label" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="Area_Label" runat="server" Text="Area:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblArea" runat="server"></asp:Label>
                </td>
                <td colspan="3" style="text-align: left; border-right: 1px solid black;">
                    <asp:Label ID="Cluster_Label" runat="server" Text="Cluster:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblCluster" runat="server"></asp:Label>
                </td>
            </tr>
            <%-- <tr class="border border-1 border-dark">
                               <td colspan="5" style="text-align: left;border-right: 1px solid black;border-top: 1px solid black;background: #17A98C;color: white;">
                                   <asp:Label ID="TypeOfContact_Label" runat="server" Text="Type of Contact"></asp:Label>
                               </td>
                            </tr> --%>
            <tr class="border border-1 border-dark">
                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="TypeOfContact_Label_copy" runat="server" Text="Type of Contact" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="TypeOfService_Label" runat="server" Text="Service:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTypeOfService" runat="server"></asp:Label>
                </td>
                <td colspan="4" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="CompanyService_Label" runat="server" Text="Company:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblCompanyService" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td rowspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Description_Label" runat="server" Text="Description" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" rowspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="txtProjectName" runat="server"></asp:Label>
                    <asp:Label ID="Label1" runat="server">ชื่อผู้ติดต่อ&nbsp;</asp:Label><asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="OneTimePayment_label" runat="server" Text="ค่าใช้จ่ายครั้งเดียว "></asp:Label>
                    <asp:Label ID="lblOneTimePayment" runat="server"></asp:Label>
                    <asp:Label ID="OneTimePayment_Unit" runat="server" Text="   บาท"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="ContractPeriod_Label" runat="server" Text="สัญญา "></asp:Label>
                    <asp:Label ID="lblContract" runat="server"></asp:Label>
                    <asp:Label ID="ContractPeriod_Unit" runat="server" Text="   months"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="MonthlyPrice_Label" runat="server" Text="ค่าใช้จ่ายรายเดือน "></asp:Label>
                    <asp:Label ID="lblMonthlyPrice" runat="server"></asp:Label>
                    <asp:Label ID="MonthlyPrice_Unit" runat="server" Text="   บาท/เดือน"></asp:Label>
                </td>
                <td colspan="2" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Email_Label" Text="Email " runat="server"></asp:Label><asp:Label ID="lblCustomerContactEmail" runat="server"></asp:Label>
                </td>
            </tr>

            <tr class="border border-1 border-dark">
                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Service_Label" runat="server" Text="Service" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:TextBox ID="txtDetailService" runat="server" TextMode="MultiLine" Height="80px" Width="416px" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="4" valign="top" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="TotalCost_label" runat="server" Text="มูลค่าโครงการ "></asp:Label>
                    <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
                    <asp:Label ID="TotalCost_Unit" runat="server" Text="   บาท(รวมภาษีมูลค่าเพิ่ม)"></asp:Label>
                </td>
            </tr>

            <tr class="border border-1 border-dark">
                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="SLA_Label" runat="server" Text="SLA" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:TextBox ID="txtSLA" runat="server" TextMode="MultiLine" Height="80px" Width="416px" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="2" valign="top" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="MonitorDate_Label" runat="server" Text="Monitor Date " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMonitorDate" runat="server"></asp:Label>
                </td>
                <td colspan="2" valign="top" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="MonitorTime_Label" runat="server" Text="Monitor Time " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblMonitorTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="MTTR_Label" runat="server" Text="MTTR" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:TextBox ID="txtMTTR" runat="server" TextMode="MultiLine" Height="100px" Width="840px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Fine_Label" runat="server" Text="ค่าปรับ" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="8" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:TextBox ID="txtFine" runat="server" TextMode="MultiLine" Height="100px" Width="840px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td colspan="5" class="bg-mint text-light" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Capex_Label" runat="server" Text="งบลงทุน(CAPEX)" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="4" class="bg-mint text-light" style="text-align: left; border-right: 1px solid black; border-top: 1px solid black;">
                    <asp:Label ID="Opex_Label" runat="server" Text="ค่าใช้จ่ายรายเดือน(OPEX)" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr class="border border-1 border-dark">
                <td colspan="5" valign="top">
                    <div id="CAPEX_Detail" runat="server"></div>
                </td>
                <td colspan="4" valign="top">
                    <div id="OPEX_Detail" runat="server"></div>
                </td>
            </tr>
            <tr class="text-grey border border-1 border-dark" style="background-color: #FFFF99;">
                <td align="left" colspan="4">Total Investment Cost</td>
                <td align="right">
                    <asp:Label ID="lblTotalCAPEX" runat="server"></asp:Label></td>
                <td align="left" colspan="3">Total Cost</td>
                <td align="right">
                    <asp:Label ID="lblTotalOPEXALL" runat="server"></asp:Label></td>
            </tr>
        </table>
        <asp:Label ID="lblTotalOPEX" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTotalOTHER" runat="server" Visible="false"></asp:Label>
        
        <table width="100%">
            <tr class="font-weight-bold">
                <td colspan="3">Revenue</td>
                <td align="right"><asp:Label ID="lblRevenue" runat="server"></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblRevenue_total" runat="server"></asp:Label></td>
                <td></td>
                <td colspan="2" style="background: #87CEFA;">Cumulative Project</td>
                <td style="background: #87CEFA;"><asp:Label ID="lblContract_EN" runat="server"></asp:Label></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">OPEX</td>
                <td align="right" ><asp:Label ID="lblOPEX" runat="server"></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblOPEX_total" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">ต้นทุนทางการตลาด(Marketing Cost)</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblMKTCost" runat="server"></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblMKTCost_total" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">ต้นทุน Internet Bandwidth</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblCostOfInternet" runat="server"></asp:Label></td>
                <td align="right" style="background:#d9d9d9;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">ต้นทุน Network Bandwidth</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblCostOfNetwork" runat="server"></asp:Label></td>
                <td align="right" style="background:#d9d9d9;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">ต้นทุน NOC</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblCostOfNOC" runat="server" Text=""></asp:Label></td>
                <td align="right" style="background:#d9d9d9;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">EXP. Jasmine Group</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblJasmineGorup" runat="server" Text=""></asp:Label></td>
                <td align="right" style="background:#d9d9d9;"></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">EXP. Other</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblOther" runat="server" Text=""></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblOther_total" runat="server" Text=""></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="3" style="color:#d9d9d9;">Penelty</td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblPenalty" runat="server" Text=""></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblPenalty_total" runat="server" Text=""></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">Revenue After Operation</td>
                <td align="right"><asp:Label ID="lblRevenue_Operation" runat="server" Text=""></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblRevenue_Operationtotal" runat="server" Text=""></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">CAPEX</td>
                <td align="right"><asp:Label ID="lblCAPEX_value" runat="server"></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblCAPEX_total" runat="server"></asp:Label></td>
                <td colspan="4"></td>
            </tr>
            <tr class="font-weight-bold">
                <td colspan="3">Case Flow</td>
                <td align="right"><asp:Label ID="lblCashFlow_value" runat="server"></asp:Label></td>
                <td align="right" style="color:#d9d9d9;"><asp:Label ID="lblCashFlow_total" runat="server"></asp:Label></td>
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
      <asp:Button ID="btnSave" runat="server" class="btn btn-success pull-right" Text="บันทึก" Visible="false" />
      <asp:Label ID="lblPrepaireEmail" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblROMail" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblNOCTotalCost" runat="server" Visible="False"></asp:Label>
   <!-- </div>-->
    
    
</asp:Content>  
