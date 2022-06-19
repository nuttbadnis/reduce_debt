<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="add_service - Copy.aspx.vb" Inherits="add_service" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--        <link rel="stylesheet" href="App_Inc/bootstrap/css/bootstrap.css" />
	    <script type="text/javascript" src="App_Inc/bootstrap/js/bootstrap.js"></script>--%>
     <div class="col-md-10 pull-right">  

            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item"><a href="project_name.aspx">Project Name</a></li><li class="breadcrumb-item active">Add Service</li><li class="breadcrumb-item"><a href="add_capex.aspx">Add Capex</a></li><li class="breadcrumb-item"><a href="add_opex.aspx">Add Opex</a></li><li class="breadcrumb-item"><a href="add_other.aspx">Add Other</a></li><li class="breadcrumb-item"><a href="Summary.aspx">Summary</a></li></ol>
            
            <div class="container">
                <div class="form-horizontal">
                    <div class="form-group">
                         <label class="col-md-2 control-label">Contract Period :</label>  
                         <div class="col-md-2">
                             <div class="input-group">
                             <asp:TextBox ID="txtContract" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                             <span class="input-group-addon">Month</span>
                             </div>
                         </div>
                    </div>      
                  
                    <hr style="border-color: #DDD;" />
                    
                    
                        <div class="col-md-7">
                            <div class="col-md-7">
                                <div class="form-group">
                                    <label id="lblInputPrice" runat="server" class="control-label" style="text-decoration: underline;"></label>
                                </div> 
                                <div class="form-group">
                                    <div class="input-group"> 
                                        <asp:TextBox ID="txtInputPrice" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        <span class="input-group-addon">THB</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">   
                                <div class="form-group">
                                    <label class="control-label" style="text-decoration: underline;">One-Time Payment</label>
                                </div> 
                                <div class="form-group">
                                    <div class="input-group"> 
                                        <asp:TextBox ID="txtOneTimePayment" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        <span class="input-group-addon">THB</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7">   
                                <div class="form-group">
                                    <label class="control-label" style="text-decoration: underline;">ค่าของขวัญ</label>
                                </div> 
                                <div class="form-group">
                                    <div class="input-group"> 
                                        <asp:TextBox ID="txtGift" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon">THB</span>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-5">   
                                <div class="form-group">
                                    <label class="control-label" style="text-decoration: underline;">Pen. (ค่าปรับ)</label>
                                </div> 
                                <div class="form-group">
                                    <div class="input-group"> 
                                        <asp:TextBox ID="txtPenalty" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon">THB</span>
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                        <div class="col-md-5">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="control-label" style="text-decoration: underline;">Selling Price (Exclude VAT)</label>
                                </div> 
                                <div class="form-group">
                                    <label class="col-md-6 control-label">Monthly (ราคาขาย) </label> 
                                    <asp:Label ID="lblMonthlyPrice" runat="server" Text="0.00"></asp:Label>
                                    <label class="control-label" style="font-weight: 100;"> THB (exVAT)</label>
                                    
                                </div> 
                                <div class="form-group">
                                    <label class="col-md-6 control-label">Total Yearly </label> 
                                    <asp:Label ID="lblTotalYearly" runat="server" Text="0.00"></asp:Label>
                                    <label class="control-label" style="font-weight: 100;"> THB (exVAT)</label>
                                   
                                </div> 
                                <div class="form-group">
                                    <label class="col-md-6 control-label">One-Time Payment </label> 
                                    <asp:Label ID="lblOneTimePayment" runat="server" Text="0.00"></asp:Label>
                                    <label class="control-label" style="font-weight: 100;"> THB (exVAT)</label>
                                </div> 
                            </div>
                        </div>
                   
                    
                    <hr style="border-color: #DDD;width: 100%;" />
                    
                    <div class="col-md-3"> 
                        <div class="form-group">
                            <label class="control-label" style="text-decoration: underline;">Service</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">INL</label> 
                            <div class="input-group"> 
                                <asp:TextBox ID="txtINL" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Curcuit</span>
                            </div>
                        </div>
                    
                        <div class="form-group">
                            <label class="col-md-4 control-label">IPV</label>
                            <div class="input-group"> 	
                                <asp:TextBox ID="txtIPV" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Curcuit</span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">INF</label> 
                            <div class="input-group">  
                                <asp:TextBox ID="txtINF" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">Curcuit</span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">   
                        <div class="form-group">
                            <div class="col-md-12">
                                <label class="control-label" style="text-decoration: underline;">INL Bandwidth</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Domestic</label> 
                            <div class="input-group"> 
                                <asp:TextBox ID="txtDomestic" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <span class="input-group-addon">Mbps</span>
                            </div>
                        </div>  
                        <div class="form-group">
                            <label class="col-md-5 control-label">Utilization</label> 
                            <asp:Label ID="lblINLUtilization" runat="server" CssClass="control-label"></asp:Label>
                            <label class="control-label" style="font-weight: 100;">%</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-5 control-label">International</label> 
                            <div class="input-group">  
                                <asp:TextBox ID="txtInternational" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <span class="input-group-addon">Mbps</span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-5 control-label">Direct Traffic</label> 
                            <asp:Label ID="lblDirectTraffic" runat="server" CssClass="control-label"></asp:Label>
                            <label class="control-label"  style="font-weight: 100; color: white;">Mbps</label>
                        </div> 
                    </div>
                    <div class="col-md-4" style="padding-left:0px;padding-right:0px;">
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="control-label" style="text-decoration: underline;">IPV Bandwidth</label>
                            </div>
                       </div> 
                        <div class="form-group">
                            <label class="col-md-5 control-label">Ethernet</label> 
                                <div class="input-group">  
                                    <asp:TextBox ID="txtIPVEthernet" runat="server" CssClass="form-control" AutoPostBack="True" MaxLength="10"></asp:TextBox>
                                    <span class="input-group-addon">Mbps</span>
                                </div>
                        </div>
                        <div class="form-group">
                             <label class="col-md-5 control-label">Utilization</label> 
                            <asp:Label ID="lblIPVUtilization" runat="server" CssClass="control-label"></asp:Label>
                            <label class="control-label" style="font-weight: 100;">%</label>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12">
                                <label class="control-label" style="text-decoration: underline;">Network Bandwidth</label>
                            </div>
                       </div> 
                        <div class="form-group">
                            <label class="col-md-5 control-label">Ethernet</label> 
                            <asp:Label ID="lblNetworkEthernet" runat="server" CssClass="control-label"></asp:Label>
                            <label class="control-label"  style="font-weight: 100;"> Mbps</label>
                        </div>
                    </div>
                    
                    <hr style="border-color: #DDD;width: 100%;" />
                    

                 <div class="col-md-12" >
                    <asp:Button ID="Button1" runat="server" Text="บันทึก" class="btn btn-success pull-right"/>   
                 </div>             

             </div>
          </div>
     </div>
          
         
    <asp:Label ID="lblUtilization" runat="server" Text="0" Visible="false"></asp:Label> 
    <asp:Label ID="lblCustomerType" runat="server" Text="-" Visible="false"></asp:Label>  
                 
</asp:Content>  
