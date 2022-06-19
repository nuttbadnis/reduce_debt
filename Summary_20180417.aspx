<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="Summary.aspx.vb" Inherits="Summary" %>



    
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
            <a id="Create" runat="server" class="list-group-item active" href="Default.aspx?menu=create">
                Create Project</a> <a id="Edit" runat="server" class="list-group-item" href="edit_list.aspx?menu=edit">
                    Edit Project</a> <a id="Approve" runat="server" class="list-group-item" href="approve_list.aspx?menu=approve">
                        Approve Project</a> <a id="menu3" runat="server" visible="false" class="list-group-item" href="#menu3">
                            Fourth item</a>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966"
            BorderStyle="None" BorderWidth="1px" CellPadding="0" class="table" CssClass="tablesummary"
            GridLines="None" Width="0%">
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
            <RowStyle BackColor="#f5f5f5" Font-Size="4" ForeColor="#337ab7" />
            <FooterStyle BackColor="#FFFFCC" ForeColor="#fff" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#fff" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#fff" />
            <HeaderStyle BackColor="#1abc9c" ForeColor="white" HorizontalAlign="Center" />
        </asp:GridView>
        <br />
    </div>
  </nav>  
<!-----Navbar LeftSide------>

<!----Body ----------------->    
    <!--<link rel="stylesheet" type="text/css" href="App_Inc/jquery-ui-1.11.4/jquery-ui.css" />-->
    <div class="col-md-10 pull-right" >   
            
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item"><a href="project_name.aspx">Project Name</a></li>
            <li class="breadcrumb-item"><a href="add_service.aspx">Add Service</a></li>
            <li class="breadcrumb-item"><a href="add_capex.aspx">Add Capex</a></li>
            <li class="breadcrumb-item"><a href="add_opex.aspx">Add Opex</a></li>
            <li class="breadcrumb-item"><a href="add_other.aspx">Add Other</a></li>
            <li class="breadcrumb-item active">Summary</li>
            </ol>
            

            <div class="col-md-12" id="dvMain" style="border: 1px solid black; padding-bottom: 15px; margin: 10px 0px 20px 0px;"> 
                
                    <div class="col-md-12" style="border: 1px solid black; padding-bottom: 15px; margin: 15px 0px 0px 0px;"> 

                            <asp:Label ID="Label2" runat="server" Text="" CssClass="pull-right"></asp:Label>
                            <div class="col-md-12" style="text-align : center;">
                            <asp:Label ID="Label1" runat="server" Text="??ÿÿ?ÿ " Font-Bold="True"></asp:Label><asp:Label ID="lblProjectName" runat="server" Font-Bold="True" ></asp:Label>
                            </div>
                            
                    </div> 
                    
                    <div class="col-md-12" style="padding-left: 0px;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                    
                            <div class="col-md-5" style="border-right: 1px solid black;padding: 10px 0px 10px 3px;">
                            <asp:Label ID="Label3" runat="server" Text="Ref. No. :"></asp:Label>
                            <asp:Label ID="lblDocumentNo" runat="server"></asp:Label>
                            </div>
                            
                            <div class="col-md-5" style="border-right: 1px solid black;padding: 10px 0px 10px 3px;">
                                    <asp:Label ID="Label5" runat="server" Text="ÿ?ÿÿÿ?ÿÿ/ÿÿ?ÿÿ? :"></asp:Label>
                                        <asp:Label ID="txtDocumentDate" runat="server"></asp:Label>
                                        <!--<input id="txtDocumentDate1" runat="server" type="text" data-field="date"/>
                                    <div id="dtBox"></div>-->
                            </div>
                            
                             <div class="col-md-2" style="padding: 10px 0px 0px 3px;">
                             <asp:Label ID="Label7" runat="server" Text=" Area :"></asp:Label>
                             <asp:Label ID="lblArea" runat="server"></asp:Label>
                             <!--<asp:DropDownList ID="ddlArea" runat="server" Width="70px" AutoPostBack="True" Visible="false"></asp:DropDownList>-->
                             </div>
                        </div>  
                    
                    <div class="col-md-12" style="padding-left: 0px;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                            
                            <div class="col-md-5" style="padding-left: 3px;border-right: 1px solid black;">
                            <asp:Label ID="Label9" runat="server" Text="Ref.LocationName/Code"></asp:Label>
                            <asp:Label ID="txtLocationName" runat="server"></asp:Label>
                            <!--<asp:TextBox ID="txtLocationName1" runat="server"></asp:TextBox>-->
                            </div>
                            
                            <div class="col-md-5" style="padding-left: 3px;border-right: 1px solid black;">
                            <asp:Label ID="Label11" runat="server" Text="ÿ?ÿÿÿÿÿÿÿÿÿÿÿÿ?ÿÿ :"></asp:Label>
                            <asp:Label ID="txtServiceDate" runat="server"></asp:Label>
                            <!--<input id="txtServiceDate1" runat="server" type="text" data-field="date"/>
                            <div id="Div1"></div>-->
                            </div>
                            
                            <div class="col-md-2" style="padding-left: 3px;">        
                            <asp:Label ID="Label13" runat="server" Text="Cluster :"></asp:Label>
                            <asp:Label ID="lblCluster" runat="server"></asp:Label>
                            <!--<asp:DropDownList ID="ddlCluster" runat="server" Width="70px" Visible="false"></asp:DropDownList>-->
                            </div>
                            
                     </div>       
                
                    <div class="col-md-12" style="padding-left: 0px; border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                    
                            <div class="col-md-5" style="padding-left: 3px;border-right: 1px solid black;">
                            <asp:Label ID="Label15" runat="server" Text="Customer Name :"></asp:Label>
                            <asp:Label ID="txtCustomerName" runat="server"></asp:Label>
                            <!--<asp:TextBox ID="txtCustomerName1" runat="server"></asp:TextBox>-->
                            </div>
                            <div class="col-md-5" style="padding-left: 3px;">
                             <asp:Label ID="LabelType" runat="server" Text="Type Of Service :"></asp:Label>
                            <asp:Label ID="lblTypeOfService" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                            </div>
                    </div>
               
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

            
            <div class="col-md-12" style="padding-left: 0px;padding-right: 0px;border-left: 1px solid black;border-right: 1px solid black;border-bottom: 1px solid black;"> 
                    <div class="col-md-12">
                    <asp:Label ID="Label18" runat="server" Text="Description ÿÿÿÿ?ÿÿ?ÿ"></asp:Label>
                    </div>
                    
                    <div class="col-md-6">
                        <div class="col-md-6">
                        <asp:Label ID="Label19" runat="server" Text="ÿÿÿÿ?ÿÿ?ÿÿÿÿ? :"></asp:Label>
                        </div>
                        <div class="col-md-2">
                        <asp:Label ID="lblMonthly" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-4">
                        <asp:Label ID="Label21" runat="server" Text="THB (exVAT)"></asp:Label>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <div class="col-md-6">    
                        <asp:Label ID="Label22" runat="server" Text="ÿ?ÿÿÿ?ÿÿÿÿÿÿÿ :"></asp:Label>
                        </div>
                        <div class="col-md-2">
                        <asp:Label ID="lblOneTimePayment" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-4">
                        <asp:Label ID="Label24" runat="server" Text="THB (exVAT)"></asp:Label>
                        </div>
                    </div>    
                    
                    <div class="col-md-6">
                        <div class="col-md-6">    
                        <asp:Label ID="Label25" runat="server" Text="ÿÿÿÿÿÿÿÿ?ÿÿÿÿÿÿÿ?ÿÿ :"></asp:Label>
                        </div>
                        <div class="col-md-6">
                        <asp:Label ID="Label26" runat="server" Text="(DCN+INL)"></asp:Label>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                      <div class="col-md-6">       
                        <asp:Label ID="Label28" runat="server" Text="ÿ??ÿ? (CAPEX) :"></asp:Label>
                      </div>
                      <div class="col-md-2">      
                        <asp:Label ID="lblCAPEX" runat="server" Text=""></asp:Label> 
                      </div>
                      <div class="col-md-4">     
                        <asp:Label ID="Label30" runat="server" Text="THB (exVAT)"></asp:Label> 
                      </div>
                    </div>   
                    
                    <div class="col-md-6">
                      <div class="col-md-6"> 
                        <asp:Label ID="Label31" runat="server" Text="ÿ?ÿÿ (ÿÿ) :"></asp:Label>  
                      </div>
                      <div class="col-md-2">   
                        <asp:Label ID="lblContractYear" runat="server"></asp:Label>  
                      </div>
                      <div class="col-md-4">   
                        <asp:Label ID="Label32" runat="server" Text="ÿÿ"></asp:Label>
                      </div>       
                    </div>    

                    <div class="col-md-12">
                        <div class="col-md-3">
                        <asp:Label ID="Label27" runat="server" Text="ÿ?ÿÿÿÿ?ÿÿÿ?ÿÿÿ :"></asp:Label>    
                        </div>
                        <div class="col-md-9">
                        <asp:TextBox ID="txtDetailService" runat="server" TextMode="MultiLine" Height="100px" Width="550px" ReadOnly="true"></asp:TextBox>
                        </div> 
                    </div>  
                                       
                    <div class="col-md-12" style="background-color:#FFFF99;">
                        <div class="col-md-3" >
                       
                        </div>
                    
                        <div class="col-md-3" >
                        <asp:Label ID="Label34" runat="server" Text="Investment Cost (CAPEX)" Font-Bold="True" Font-Underline="True"></asp:Label>
                        </div>
                        
                        <div class="col-md-3">
                        <asp:Label ID="Label35" runat="server" Text="VAS Internal (per Month)" Font-Bold="True" Font-Underline="True"></asp:Label>
                        
                        </div>
                        <div class="col-md-3">
                        <asp:Label ID="Label17" runat="server" Text="VAS Other (per Month)" Font-Bold="True" Font-Underline="True"></asp:Label>
                        
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-3" >
                            <asp:Label ID="Label54" runat="server" Text="Total Investment" Font-Bold="True" Font-Underline="True"></asp:Label>
                        </div>
                        <div class="col-md-3" >
                            <asp:Label ID="lblTotalCAPEX" runat="server" Font-Bold="true"></asp:Label>
                            <!--<div id="CAPEX" runat="server"></div>-->
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblTotalOPEX" runat="server" Font-Bold="true"></asp:Label>
                            <!--<div id="OPEX" runat="server"></div>-->
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblTotalOTHER" runat="server" Font-Bold="true"></asp:Label>
                            <!--<div id="OTHER" runat="server"></div>-->
                        </div>
                    </div>

            </div>
            
             <div class="col-md-12" style="padding-left:0px">
             
                    <asp:Label ID="Label20" runat="server" Text="Cumulative"></asp:Label>
                    <asp:Label ID="lblContract" runat="server"></asp:Label>
                    <asp:Label ID="Label29" runat="server" Text="Months"></asp:Label>
                    <br />
                    
                    <div class="col-md-3" style="padding-left:0px; padding-right:10px;margin-right: 14px;">
                    <asp:Label ID="Label36" runat="server" Text="Revenue"></asp:Label>
                    </div>
                    <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="Label38" runat="server" Text="OPEX"></asp:Label>
                    
                    
                        <div style="color:#696969; margin-left: 20px;"> 
                        
                            <div class="col-md-3">
                            <asp:Label ID="Label39" runat="server" Text="MKT Cost 3%"></asp:Label>
                            </div>
                            <asp:Label ID="lblMKTCost" runat="server" Text=""></asp:Label>
                            <br />
                           
                            <div class="col-md-3">
                            <asp:Label ID="Label40" runat="server" Text="Cost of Internet Bandwidth"></asp:Label>
                            </div>
                            <asp:Label ID="lblCostOfInternet" runat="server" Text=""></asp:Label>
                            <br />
                            
                            <div class="col-md-3">
                            <asp:Label ID="Label41" runat="server" Text="Cost of Network Bandwidth"></asp:Label>
                            </div>
                            <asp:Label ID="lblCostOfNetwork" runat="server" Text=""></asp:Label>
                            <br />
                           
                            <div class="col-md-3">
                            <asp:Label ID="Label42" runat="server" Text="Cost of NOC"></asp:Label>
                            </div>
                            <asp:Label ID="lblCostOfNOC" runat="server" Text=""></asp:Label>
                            <br />
                            
                            <div class="col-md-3">
                            <asp:Label ID="Label43" runat="server" Text="VAS & COST per month"></asp:Label>
                            </div>
                            <asp:Label ID="lblVas" runat="server" Text=""></asp:Label>
                            
                        </div>
                    <div class="col-md-3" style="padding-left:0px; padding-right:10px;margin-right: 14px;">  
                    <asp:Label ID="Label44" runat="server" Text="Cash Flow"></asp:Label>
                    </div>
                    <asp:Label ID="lblCashFlow" runat="server" Text=""></asp:Label>
             </div>   

          <div class="col-md-4" style="border: 1px solid black;"> 
           
                  <div class="col-md-6" style="border-right: 1px solid black;">
                      <asp:Label ID="Label37" runat="server" Text="Payback (months)"></asp:Label>
                      <br />
                      <asp:Label ID="Label33" runat="server" Text="Margin"></asp:Label>
                      <br />
                      <asp:Label ID="Label23" runat="server" Text="NPV (5% / Year)"></asp:Label>                       
                  </div>
                  
                  <div class="col-md-6">
                       <asp:Label ID="lblPayBack" runat="server" Text=""></asp:Label>
                       <br />
                       <asp:Label ID="lblMargin" runat="server"></asp:Label>
                       <br />
                       <asp:Label ID="lblNPV" runat="server"></asp:Label>
                  </div>
           </div>
          
        
           <div class="col-md-12" style="border: 1px solid black; margin-top: 2%; margin-bottom: 2%;"> 
           
             <div class="col-md-4" style="border-right: 1px solid black;padding-left: 0px;"> 
                    
                    <div style="text-align: center;">
                    <asp:Label ID="Label45" runat="server" Text="Prepared by"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="lblPrepare" runat="server"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label51" runat="server" Text="Date"></asp:Label>
            </div>
            
            <div class="col-md-4" style="border-right: 1px solid black;"> 
            
                    <div style="text-align: center;">
                    <asp:Label ID="Label46" runat="server" Text="Verified by HRO"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label49" runat="server" Text="(ÿ?ÿ?ÿÿ  ÿÿÿÿÿ?ÿ???)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label52" runat="server" Text="Date"></asp:Label>
                    
            </div>
                    
            <div class="col-md-4"> 
                    
                    <div style="text-align: center;">
                    <asp:Label ID="Label47" runat="server" Text="Verifired by Network"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label50" runat="server" Text="(ÿ?ÿ?ÿÿ  ?ÿÿÿ?)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label53" runat="server" Text="Date"></asp:Label>
                    
            </div>
            
          </div>
          
          
          <div class="col-md-12" style="border: 1px solid black;"> 
           
             <div class="col-md-4" style="border-right: 1px solid black;padding-left: 0px;"> 
                    <div style="text-align: center;">
                    <asp:Label ID="Label4" runat="server" Text="Verified by COO2"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label6" runat="server" Text="(ÿ?ÿ?ÿÿÿ ÿÿÿ?ÿÿÿÿ)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
            </div>
            
            <div class="col-md-4" style="border-right: 1px solid black;"> 
            
                    <div style="text-align: center;">
                    <asp:Label ID="Label10" runat="server" Text="Verified by COO"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label12" runat="server" Text="(ÿ?ÿÿ??ÿÿ  ÿ?ÿÿ?ÿ)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
                    
            </div>
                    
            <div class="col-md-4"> 
                    
                    <div style="text-align: center;">
                    <asp:Label ID="Label16" runat="server" Text="Approved by"></asp:Label>
                    </div>
                    <br />
                    <div style="margin-top:50px;text-align: center;">
                    <asp:Label ID="Label117" runat="server" Text="(ÿ?ÿ?ÿÿÿ  ÿ?ÿÿÿÿ?ÿÿÿÿÿ)"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="Label48" runat="server" Text="Date"></asp:Label>
                    
            </div>
            
          </div>
   
                
      </div>

        <asp:Button ID="test" class="btn btn-warning" runat="server" Visible="False" Text="Print PDF"/>
        <asp:Button ID="btnSave" runat="server" class="btn btn-success pull-right" Text="ÿ?ÿ?" />
        <asp:Label ID="lblPrepaireEmail" runat="server" Visible="False"></asp:Label></div>
    
    
    
<script type="text/javascript">

    $("#dtBox").DateTimePicker({
    dateSeparator: "/",
    dateFormat: "dd/MM/yyyy",
     readonlyInputs: true,

   
    });
    
</script>    
</asp:Content>  
