<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="edit_Summary_20200427.aspx.vb" Inherits="edit_Summary_20200427" %>

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
</style>
<!-----Navbar LeftSide------>
       <nav class="navbar navbar-default navbar-fixed-top" style="width: 13.5%;position: fixed;height: 100%;top: 51px;background: #eee;">
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
    </nav>
<!-----Navbar LeftSide------>

<!----Body ----------------->   
    <div class="col-md-10 pull-right" > 
    
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item"><a id="menu_project_name" runat="server">Edit Project Name</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
            <li class="breadcrumb-item"><a id="menu_capex" runat="server">Edit Capex</a></li>
            <li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_other" runat="server">Edit Other</a></li>
            <li class="breadcrumb-item active">Edit Summary</li></ol>
            

            <div class="col-md-12" id="dvMain" style="border: 1px solid black; padding-bottom: 15px; margin: 10px 0px 20px 0px;"> 
                
                    <div class="col-md-12" style="border: 1px solid black; padding-bottom: 15px; margin: 15px 0px 0px 0px;"> 

                            <asp:Label ID="Label2" runat="server" Text="" CssClass="pull-right"></asp:Label>
                            <div class="col-md-12" style="text-align : center;">
                            <asp:Label ID="Label1" runat="server" Text="Ẻ͹��ѵ� " Font-Bold="True"></asp:Label><asp:Label ID="lblProjectName" runat="server" Font-Bold="True" ></asp:Label>
                            </div>
                            
                    </div> 
                        <table cellspacing="0" style="width: 100%;border: 1px solid black;border-bottom-style: none;">
                           <tr>
                              <td colspan="2" style="width:50%;text-align: left;border-right: 1px solid black;">
                                  <asp:Label ID="RefNo_Label" runat="server" Text="Ref.No.:" Font-Bold="true"></asp:Label>
                                  <asp:Label ID="refno" runat="server" ></asp:Label>
                              </td>  
                              <td style="width:25%;text-align: left;border-right: 1px solid black;background: #17A98C;color: white;">
                                  <asp:Label ID="Prepare_Label" runat="server" Text="Prepare"></asp:Label>
                                  <!--<input id="txtDocumentDate1" runat="server" type="text" data-field="date"/>
                                  <div id="dtBox"></div>-->
                              </td>                
                              <td style="text-align: left;background: #17A98C;color: white;">
                                  <asp:Label ID="OnService_Label" runat="server" Text="On Service"></asp:Label>
                                  <!--<asp:DropDownList ID="ddlArea" runat="server" Width="70px" AutoPostBack="True" Visible="false"></asp:DropDownList>-->
                              </td>   
                           </tr>

<tr>
                              <td colspan="2" style="width:50%;text-align: left;border-right: 1px solid black;">
                                  <asp:Label ID="ProjectCode_Label" runat="server" Text="Project Code:" Font-Bold="true"></asp:Label>
                                  <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                                  <!--<asp:TextBox ID="txtLocationName1" runat="server"></asp:TextBox>-->
                              </td>  
                              <td style="width:25%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;">
                                  <asp:Label ID="txtDocumentDate" runat="server" ></asp:Label>
                                  <!--<input id="txtServiceDate1" runat="server" type="text" data-field="date"/>
                                  <div id="Div1"></div>-->
                              </td>                
                              <td style="text-align: left;border-top: 1px solid black;">
                                  <asp:Label ID="txtServiceDate" runat="server" ></asp:Label>
                                  <!--<asp:DropDownList ID="ddlCluster" runat="server" Width="70px" Visible="false"></asp:DropDownList>-->
                              </td>   
                           </tr>

                           <tr>
                              <td colspan="2" style="width:50%;text-align: left;border-right: 1px solid black;">
                                  <asp:Label ID="CustomerName_Label" runat="server" Text="�١���:" Font-Bold="True"></asp:Label>
                                  <asp:Label ID="txtCustomerName" runat="server"></asp:Label>
                              </td>  
                              <td style="width:25%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;background: #17A98C;color: white;">
                                  <asp:Label ID="CustomerAssistant_Label" runat="server" Text="Customer Assistant"></asp:Label>
                              </td>                
                              <td style="text-align: left;border-top: 1px solid black;background: #17A98C;color: white;">
                                  <asp:Label ID="CustomerContact_Label" runat="server" Text="Customer Contact"></asp:Label>
                              </td>   
                           </tr>

                           <tr>
                               <td colspan="2" style="width:50%;text-align: left;border-right: 1px solid black;">
                                   <asp:Label ID="EnterpriseName_Label" runat="server" Text="�ç���:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblEnterpriseName" runat="server"></asp:Label>
                               </td>  
                               <td style="width:25%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;">
                                   <asp:Label ID="CustomerAssistantName_Label" runat="server" Text="Name:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblCustomerAssistantName" runat="server"></asp:Label>   
                               </td>                
                               <td style="text-align: left;border-top: 1px solid black;">
                                   <asp:Label ID="CustomerContactName_Label" runat="server" Text="���ͼ��Դ���:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblCustomerContactName" runat="server"></asp:Label>
                               </td>   
                           </tr>
                            <tr>
                               <td colspan="2" style="width:50%;text-align: left;border-right: 1px solid black;">
                                 <asp:Label ID="CustomerType_Label" runat="server" Text="�������١���:" Font-Bold="True"></asp:Label>
                                 <asp:Label ID="lblCustomerType" runat="server"></asp:Label>  
                               </td>  
                               <td style="width:25%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;">
                                 <asp:Label ID="CustomerAssistantID_Label" runat="server" Text="ID:" Font-Bold="True"></asp:Label>
                                 <asp:Label ID="lblCustomerAssistantID" runat="server"></asp:Label>
                               </td>                
                               <td style="text-align: left;border-top: 1px solid black;">
                                 <asp:Label ID="CustomerContactTel_Label" runat="server" Text="���Ѿ��:" Font-Bold="True"></asp:Label>
                                 <asp:Label ID="lblCustomerContactTel" runat="server"></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td colspan="2" style="width:50%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;background: #17A98C;color: white;">
                                   <asp:Label ID="TypeOfContact_Label" runat="server" Text="Type of Contact"></asp:Label>
                               </td>  
                               <td style="width:25%;text-align: left;border-right: 1px solid black;">
                                   <asp:Label ID="Area_Label" runat="server" Text="RO:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblArea" runat="server"></asp:Label>  
                               </td>                
                               <td style="text-align: left;">
                                   <asp:Label ID="CustomerContactEmail_Label" runat="server" Text="e-mail:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblCustomerContactEmail" runat="server"></asp:Label>
                               </td>   
                           </tr>

                           <tr>
                               <td style="width:25%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;">
                                   <asp:Label ID="TypeOfService_Label" runat="server" Text="Service:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblTypeOfService" runat="server"></asp:Label>
                               </td>  
                               <td style="width:25%;text-align: left;border-right: 1px solid black;border-top: 1px solid black;">
                                   <asp:Label ID="CompanyService_Label" runat="server" Text="Company:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblCompanyService" runat="server"></asp:Label>
                               </td> 
                               <td style="width:25%;text-align: left;border-right: 1px solid black;">
                                   <asp:Label ID="Cluster_Label" runat="server" Text="Cluster:" Font-Bold="True"></asp:Label>
                                   <asp:Label ID="lblCluster" runat="server"></asp:Label>
                               </td>                
                               <td style="text-align: left;">

                               </td>   
                           </tr>
                        </table>    
                    


                         
                
<%--                    <div class="col-md-12" style="padding-left: 0px; border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                    
                            <div class="col-md-5" style="padding-left: 3px;border-right: 1px solid black;">

                            <!--<asp:TextBox ID="txtCustomerName1" runat="server"></asp:TextBox>-->
                            </div>
                            <div class="col-md-4" style="padding-left: 3px;">

                            </div>
                            <div class="col-md-3">
                                
                            </div>
                    </div>--%>
               
                <!--<div class="col-md-12" style="padding-left: 0px;padding-right: 0px; border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black; background-color: #1abc9c; color:White;"> 
                
                    
                    
                    <div class="col-md-10" style="padding-left: 0px;padding-right: 0px;">
                        <div class="col-md-3">
                        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" Text="New Service" GroupName="type" />
                        <br />
                        
                        </div>
                        
                        <div class="col-md-3">
                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="type" Text="Re-New Service" />
                        <br />
                        
                        </div>
                        
                        <div class="col-md-4">
                        <asp:RadioButton ID="RadioButton3" runat="server" GroupName="type" Text="Maintenance" />
                        <br />
                       
                        </div>
                        
                        <div class="col-md-2" style="padding-right: 0px;">
           
                        </div>
                    </div>  
             </div>      --> 

            <div class="col-md-12" style="padding-left: 0px;padding-right: 0px;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;border-top: 1px solid black;"> 
                <div class="col-md-12" style="border-bottom: 1px solid black;background: #17A98C;color: white;padding-left:3px;">
                    <asp:Label ID="Label15" runat="server" Text="Description"></asp:Label>
                </div>
                     <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                <asp:Label ID="Label58" runat="server" Text="Service"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="Label62" runat="server" Text="Contract Period"></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:Label ID="lblContract" runat="server"></asp:Label><asp:Label ID="Label70" runat="server" Text="   months"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                <asp:Label ID="Label65" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtDetailService" runat="server" TextMode="MultiLine" Height="100px" Width="750px" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                <asp:Label ID="Label66" runat="server" Text="SLA"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtSLA" runat="server" TextMode="MultiLine" Height="100px" Width="750px" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                <asp:Label ID="Label82" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="Label79" runat="server" Text="Monitor Date"></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:Label ID="lblMonitorDate" runat="server" ></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-3">
                                <asp:Label ID="Label87" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="Label83" runat="server" Text="Monitor Time"></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:Label ID="lblMonitorTime" runat="server" ></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-2">
                            <asp:Label ID="Label89" runat="server" Text="��һ�Ѻ"></asp:Label>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="Label90" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtFine" runat="server" TextMode="MultiLine" Height="100px" Width="750px" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
            </div>

            <div class="col-md-12" style="padding-left: 0px;padding-right: 0px;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                    <div class="col-md-12" style="background-color:#FFFF99;">
                        <div class="col-md-4" >
                            <asp:Label ID="Label100" runat="server" Text="Investment Cost" Font-Bold="True" ></asp:Label>
                        </div>
                        <div class="col-md-4" >
                            <asp:Label ID="Label92" runat="server" Text="Corp." Font-Bold="True" Font-Underline="True"></asp:Label>
                        </div>
                        
                        <div class="col-md-4">
                        <asp:Label ID="Label93" runat="server" Text="Mass" Font-Bold="True" Font-Underline="True"></asp:Label>
                        
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4" >
                            <asp:Label ID="Label94" runat="server" Text="Total Investment" Font-Underline="True"></asp:Label>
                        </div>
                        <div class="col-md-4" >
                            <asp:Label ID="lblTotalCAPEX" runat="server" ></asp:Label>
                            <!--<div id="CAPEX1" runat="server"></div>-->
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblTotalCAPEXMass" runat="server" ></asp:Label>
                           
                        </div>
                    </div>
            </div>
            
            <div class="col-md-12" style="padding-left: 0px;padding-right: 0px;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                    <div class="col-md-12" style="background-color:#FFFF99;">
                        <div class="col-md-4" >
                            <asp:Label ID="Label99" runat="server" Text="�������������͹" Font-Bold="True" ></asp:Label>
                        </div>
                        <div class="col-md-4" >
                            <asp:Label ID="Label95" runat="server" Text="Jasmine Group" Font-Bold="True" Font-Underline="True"></asp:Label>
                        </div>
                        
                        <div class="col-md-4">
                        <asp:Label ID="Label97" runat="server" Text="Other" Font-Bold="True" Font-Underline="True"></asp:Label>
                        
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4" >
                            <asp:Label ID="Label98" runat="server" Text="Total" Font-Underline="True"></asp:Label>
                        </div>
                        <div class="col-md-4" >
                              <asp:Label ID="lblTotalOPEX" runat="server" ></asp:Label>
                            <!--<div id="OPEX1" runat="server"></div>-->
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblTotalOTHER" runat="server" ></asp:Label>
                            <!--<div id="OTHER1" runat="server"></div>-->
                        </div>
                    </div>
            </div>
            
             <div class="col-md-12" style="padding-left:0px">
                <div class="col-md-6" style="padding-left:0px">
                    <br />
                    <div class="col-md-3" style="padding-left:0px; padding-right:10px;margin-right: 14px;">
                        <asp:Label ID="Label36" runat="server" Text="Revenue"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="Label24" runat="server" Text="THB"></asp:Label>
                    <br />
                    <div class="col-md-3" style="padding-left:0px; padding-right:10px;margin-right: 14px;">
                        <asp:Label ID="Label17" runat="server" Text="Net Revenue"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <asp:Label ID="lblNetRevenue" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="Label25" runat="server" Text=" THB"></asp:Label>
                    <br />
                    <div class="col-md-3" style="padding-left:0px; padding-right:10px;margin-right: 14px;">
                        <asp:Label ID="Label18" runat="server" Text="CAPEX"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <asp:Label ID="lblCAPEX" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="Label26" runat="server" Text=" THB"></asp:Label>
                    <br />
                    <div style="color:#696969; margin-left: 20px;">
                        <div class="col-md-3" style="margin-right: 4px;">
                            <asp:Label ID="Label19" runat="server" Text="Corp."></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblCAPEXCorp" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label27" runat="server" Text=" THB"></asp:Label>
                        <br />
                        <div class="col-md-3" style="margin-right: 4px;">
                            <asp:Label ID="Label21" runat="server" Text="Mass"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblCAPEXMass" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label28" runat="server" Text=" THB"></asp:Label>
                    </div>
                    <div class="col-md-3" style="padding-left:0px; padding-right:10px;margin-right: 14px;">
                        <asp:Label ID="Label22" runat="server" Text="Cash Flow"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <asp:Label ID="lblCashFlow" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="Label30" runat="server" Text=" THB"></asp:Label>
                    <br />
                    <div class="col-md-8" style="padding-left:0px; background-color:#d8bfd8;"> 
                      <div class="col-md-4" style="padding-left:1px;">
                          <asp:Label ID="Label37" runat="server" Text="Payback"></asp:Label>
                      </div>  
                      <div class="col-md-6" style="text-align:right;">
                            <asp:Label ID="lblPayBack" runat="server" Text=""></asp:Label>
                       </div>
                       <asp:Label ID="Label32" runat="server" Text=" Months"></asp:Label>  
                       <br />
                       <div class="col-md-4" style="padding-left:1px;">
                          <asp:Label ID="Label33" runat="server" Text="Margin"></asp:Label>
                       </div>
                       <div class="col-md-6" style="text-align:right;">
                          <asp:Label ID="lblMargin" runat="server"></asp:Label><asp:Label ID="Label31" runat="server" Text=" %"></asp:Label>
                       </div>
                       <br />
                       <div class="col-md-4" style="padding-left:1px;">
                          <asp:Label ID="Label23" runat="server" Text="NPV 5%"></asp:Label>
                       </div>
                       <div class="col-md-6" style="text-align:right;">
                          <asp:Label ID="lblNPV" runat="server"></asp:Label>
                       </div>  
                       <asp:Label ID="Label34" runat="server" Text=" THB"></asp:Label>                      
                      
                      
                      <div class="col-md-6">
                         
                           
                      </div>
                    </div>
                </div>
                <div class="col-md-6" style="padding-left:0px">
                    
                    <br />
                    <div class="col-md-4" style="padding-left:0px; padding-right:10px;margin-right: 12px;">
                        <asp:Label ID="Label38" runat="server" Text="OPEX"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <asp:Label ID="lblOPEX" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="Label54" runat="server" Text="THB"></asp:Label>
                    <br />
                    <div style="color:#696969; margin-left: 20px;"> 
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label39" runat="server" Text="MKT Cost"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblMKTCost" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label74" runat="server" Text=" THB"></asp:Label>
                        <br />
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label35" runat="server" Text="Internet Bandwidth"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblCostOfInternet" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label96" runat="server" Text=" THB"></asp:Label>
                        <br />
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label40" runat="server" Text="Network Bandwidth"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblCostOfNetwork" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label102" runat="server" Text=" THB"></asp:Label>   
                        <br />
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label41" runat="server" Text="NOC"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblCostOfNOC" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label104" runat="server" Text=" THB"></asp:Label>
                        <br />
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label42" runat="server" Text="Pen. (��һ�Ѻ)"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblPenalty" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label105" runat="server" Text=" THB"></asp:Label>
                        <br />
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label49" runat="server" Text="Exp. Jasmine Group"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblJasmineGorup" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label107" runat="server" Text=" THB"></asp:Label>
                        <br />
                        <div class="col-md-4" style="margin-right: 4px;">
                            <asp:Label ID="Label43" runat="server" Text="Exp. Other"></asp:Label>
                        </div>
                        <div class="col-md-3" style="text-align:right;">
                            <asp:Label ID="lblOther" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:Label ID="Label108" runat="server" Text=" THB"></asp:Label>    
                        <br />       
                    </div>
                </div>
             
                    
             </div>   

          
          
        
           <div class="col-md-12" style="border: 1px solid black; margin-top: 2%; margin-bottom: 2%;"> 
           
             <div class="col-md-3" style="border-right: 1px solid black;padding-left: 0px;"> 
                    <div style="text-align: center;">
                    <asp:Label ID="Label101" runat="server" Text="����ʹ��ç���"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="lblCreateProject" runat="server"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label103" runat="server" Text="Date"></asp:Label>
                    
            </div>
            
            <div class="col-md-3" style="border-right: 1px solid black;"> 
            
                    <div style="text-align: center;">
                    <asp:Label ID="Label45" runat="server" Text="Verified by Cluster"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="lblPrepare" runat="server"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label51" runat="server" Text="Date"></asp:Label>
                    
            </div>
            
            <div class="col-md-3" style="border-right: 1px solid black;"> 
            
                    <div style="text-align: center;">
                    <asp:Label ID="Label46" runat="server" Text="Verified by HRO"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="lblHRO" runat="server" Text=""></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label52" runat="server" Text="Date"></asp:Label>
                    
            </div>
                    
            <div class="col-md-3" style="border-bottom-color: White; border-top-color: White;" > 
                    <div style="text-align: center;">
                    <asp:Label ID="Label47" runat="server" Text="Verifired by Network"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label50" runat="server" Text="(�س�ѧ��  ǹ���ɰ)"></asp:Label>
                    </div>
                   
                    <asp:Label ID="Label53" runat="server" Text="Date"></asp:Label>
                  
            </div>
            
          </div>
          
          
          <div class="col-md-12" style="border: 1px solid black;">
             
           
             <div class="col-md-3" style="border-right: 1px solid black;padding-left: 0px;"> 
                    <div style="text-align: center;">
                    <asp:Label ID="Label4" runat="server" Text="Verified by COO"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label6" runat="server" Text="(�س�ʹ��� ���Ǹ����)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                    
            </div>
            
            <div class="col-md-3" style="border-right: 1px solid black;"> 
            
                    <div style="text-align: center;">
                    <asp:Label ID="Label10" runat="server" Text="Verified by Vice President"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label12" runat="server" Text="(�س�Է�Ҡ������Ѫ�Է¡Ԩ)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
                    
            </div>
            
            <div class="col-md-3" style="border-right: 1px solid black;"> 
                    <div style="text-align: center;">
                    <asp:Label ID="Label16" runat="server" Text="Approved by President"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label117" runat="server" Text="(�س�ؾ���  �ѭ����Է�����)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label48" runat="server" Text="Date"></asp:Label>
                    
            </div>
                    
            <div class="col-md-3"> 
                    
                        
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
      <asp:Button ID="btnSave" runat="server" class="btn btn-success pull-right" Text="�ѹ�֡" Visible="false" />
      <asp:Label ID="lblPrepaireEmail" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblROMail" runat="server" Visible="False"></asp:Label>
 
    </div>
    
    
</asp:Content>  

<%-- <asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">

<script type="text/javascript" src="App_Inc/_js/check_required.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/request_update.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/flow_submit.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/redebt_search_autoinput.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/redebt_operator.js?v=3"></script>
<script type="text/javascript" src="App_Inc/_js/redebt_pick_refund.js?v=3"></script>

<script type="text/javascript">

    //$("#dtBox").DateTimePicker({
    //dateSeparator: "/",
    //dateFormat: "dd/MM/yyyy",
    //readonlyInputs: true,
    
    //});
    
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
</asp:Content> --%>