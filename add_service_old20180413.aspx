<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="add_service_old20180413.aspx.vb" Inherits="add_service" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="col-md-10 pull-right">  

            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item"><a href="project_name.aspx">Project Name</a></li><li class="breadcrumb-item active">Add Service</li><li class="breadcrumb-item"><a href="add_capex.aspx">Add Capex</a></li><li class="breadcrumb-item"><a href="add_opex.aspx">Add Opex</a></li><li class="breadcrumb-item"><a href="add_other.aspx">Add Other</a></li><li class="breadcrumb-item"><a href="Summary.aspx">Summary</a></li></ol>
            
            <div class="container">
                <v class="form-horizontal">
                    <div class="form-group">
                         <label class="col-md-2 control-label">อายุสัญญา Contract :</label>  
                         <div class="col-md-2">
                             <div class="input-group">
                             <asp:TextBox ID="txtContract" runat="server" CssClass="form-control"></asp:TextBox>
                             <span class="input-group-addon">Month</span>
                             </div>
                         </div>
                    </div>      
                    <!--<div class="form-group">
                        <label class="col-md-2 control-label" style="text-decoration: underline;">Discount</label> 
                        <div class="col-md-2">
                        <asp:DropDownList ID="ddlDiscount" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>-->
                    
                    <hr style="border-color: #DDD;" />
                 <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label" style="text-decoration: underline;">INL Service</label>
                        </div>   
                    <div class="col-md-4"> 
                        <div class="form-group">
                            <label class="col-md-7 control-label">Domestic</label> 
                                <div class="input-group">
                                <asp:TextBox ID="txtDom" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Mbps</span>
                                </div>
                        </div>               
                        <div class="form-group">
                            <label class="col-md-7 control-label">Dom.Utilization</label>	
                            <asp:Label ID="lblDom" runat="server" CssClass="control-label"></asp:Label>
                            <label class="control-label">%</label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="col-md-5 control-label">International</label> 
                                <div class="input-group">              
                                <asp:TextBox ID="txtInter" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <span class="input-group-addon">Mbps</span>
                                </div>
                        </div>       
                        <div class="form-group">
                            <label class="col-md-5 control-label">Inter.Utilization</label>	
                            <asp:Label ID="lblInterUtilization" runat="server" CssClass="control-label"></asp:Label>
                            <label class="control-label">%</label>
                        </div>
                    </div>
                    <div class="col-md-4" style="padding-left:0px;padding-right:0px;">
                       <div class="form-group">
                            <label class="col-md-6 control-label">Total INL Curcuits</label> 
                                <div class="input-group">  
                                <asp:TextBox ID="txtTotalINL" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Curcuits</span>
                                </div>
                        </div>   
                        <div class="form-group">
                            <label class="col-md-6 control-label">Transit</label> 
                                <!--<asp:TextBox ID="txtTransit1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>-->
                                <asp:Label ID="txtTransit" runat="server" Text="Label" class="control-label"></asp:Label>
                                <label class="control-label" style="font-weight: lighter"> Mbps</label>
                        </div>
                    </div>
                 </div>

                    <hr style="border-color: #DDD;width: 100%;" />

                     <div class="col-md-4"> 
                             <div class="form-group">
                            <label class="control-label" style="text-decoration: underline;">IPV Service</label>
                     </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Ethernet</label> 
                                <div class="input-group"> 
                                <asp:TextBox ID="txtEthernetIPV" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Mbps</span>
                                </div>
                            </div>
                    
                        <div class="form-group">
                            <label class="col-md-5 control-label">IPV.Utilization</label>	
                            <asp:Label ID="lblIPVUtilization" runat="server" class="control-label"></asp:Label>
                            <label class="control-label">%</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Total IPV Curcuits</label> 
                                <div class="input-group">  
                                <asp:TextBox ID="txtTotalIPV" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Curcuits</span>
                                </div>
                        </div>
                    </div>
                    <div class="col-md-4">   
                        <div class="form-group">
                            <div class="col-md-12">
                            <label class="control-label" style="text-decoration: underline;">INP Service</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Ethernet</label> 
                                <div class="input-group"> 
                                <asp:TextBox ID="txtEthernetINP" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Mbps</span>
                                </div>
                        </div>  
                        <div class="form-group">
                            <label class="col-md-5 control-label"></label> 
                                <div class="input-group"> 
                                <asp:TextBox ID="txtPriceINP" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">THB (exVAT)</span>
                                </div>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-5 control-label">Total INP Curcuits</label> 
                                <div class="input-group">  
                                <asp:TextBox ID="txtTotalINP" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Curcuits</span>
                                </div>
                        </div>
                    </div>
                    <div class="col-md-4" style="padding-left:0px;padding-right:0px;">
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="control-label" style="text-decoration: underline;">Selling Price (Ex.VAT)</label>
                            </div>
                       </div> 
                        <div class="form-group">
                            <label class="col-md-5 control-label">Monthly (รายได้)</label> 
                                <div class="input-group">  
                                    <asp:TextBox ID="txtMonthly" runat="server" CssClass="form-control" AutoPostBack="True" MaxLength="10"></asp:TextBox>
                                    <span class="input-group-addon">THB (exVAT)</span>
                                </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Total Yearly</label>	
                            <asp:Label ID="lblTotalYealy" runat="server" Text="Label" class="control-label"></asp:Label>
                    	    <label class="control-label" style="font-weight: 100;"> THB (exVAT)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">One-Time Payment</label> 
                                <div class="input-group">  
                                    <asp:TextBox ID="txtOneTime" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">THB (exVAT)</span>
                                </div>
                        </div>
                    </div>
                 
                    <hr style="border-color: #DDD;width: 100%;" />
                 <div class="col-md-12" >
                 
           
                            <asp:Button ID="Button1" runat="server" Text="บันทึก" class="btn btn-success pull-right"/>   
       
               
                 </div>             

                    
                    <!--<div class="col-md-12 hidden" >
            
                        <div class="panel panel-info">
                            <div class="panel-body">
                            Revenue : <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                            <div class="clearfix"></div>
                            One-Time Payment : <asp:Label ID="lblOneTime" runat="server"></asp:Label>
                            <div class="clearfix"></div>
                            Monthly Payment : <asp:Label ID="lblMonthly" runat="server"></asp:Label>
                            <div class="clearfix"></div>
                            OPEX
                            <div class="clearfix"></div>
                                <div class="col-md-12">
                                MKT Cost 3% <asp:Label ID="lblMKTCost" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                Cost of Internet Bandwidth : <asp:Label ID="lblCostOfInternet" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                Domestic Cost : <asp:Label ID="lblDomesticCost" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                Internatonal Cost : <asp:Label ID="lblInternationalCost" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                Transit : <asp:Label ID="lblTransitCost" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                xDSL, FTTx : <asp:Label ID="lblServiceCost" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                Cost of Network Bandwidth : <asp:Label ID="lblCostOfNetwork" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                Cost of NOC : <asp:Label ID="lblCostOfNOC" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                VAS & COST per month : <asp:Label ID="lblVas" runat="server"></asp:Label>
                                <div class="clearfix"></div>
                                </div>
                            Revenue after Operation : <asp:Label ID="lblRevenueAfter" runat="server"></asp:Label>
                            <div class="clearfix"></div>    
                            CAPEX (Include Spare Part) : <asp:Label ID="lblCAPEX" runat="server"></asp:Label>
                            <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>-->
                 
             </div>
             
          </div>
          
         
       </div>
          
    <asp:Label ID="Domestic_Price" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="All_International_Price" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="Transit_Price" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="Network_Price" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="NOC_Price" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="Dom_Discount" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="Inter_Discount" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="IPV_Discount" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="Type_Discount" runat="server" Text="Normal" Visible="false"></asp:Label>

        
</asp:Content>  
