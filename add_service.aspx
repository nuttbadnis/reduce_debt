<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" MaintainScrollPositionOnPostBack="true" CodeFile="add_service.aspx.vb" Inherits="add_service" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--        <link rel="stylesheet" href="App_Inc/bootstrap/css/bootstrap.css" />
	    <script type="text/javascript" src="App_Inc/bootstrap/js/bootstrap.js"></script>--%>

    
    <ol class="breadcrumb headingbar">
        <li class="breadcrumb-item"><a href="project_name.aspx?menu=create">Project Overview</a></li>
        <li class="breadcrumb-item active"><u>Service</u></li>
        <li class="breadcrumb-item"><a href="add_capex.aspx?menu=create">Capex</a></li>
        <li class="breadcrumb-item"><a href="add_opex.aspx?menu=create">Opex</a></li>
        <li class="breadcrumb-item"><a href="add_other.aspx?menu=create">Other</a></li>
        <li class="breadcrumb-item"><a href="add_management.aspx?menu=create">Management</a></li>
        <li class="breadcrumb-item"><a href="Summary.aspx?menu=creates">Summary</a></li>
    </ol>
    
    <div class="row">              
        <div class="col-md-4"> 
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Service</label>
            </div>
            <div class="form-inline">
                <label class="control-label col-2"></label>
                <div class="control-label col-4"> �ӹǹǧ��</div>
                <div class="control-label col-4">  �Դ NOC</div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">INL</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINL" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINL_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">IPV</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIPV" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIPV_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">INL(ECO)</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINLECO" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINLECO_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">IPV(ECO)</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIPVECO" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIPVECO_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">INP</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINP" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINP_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">INL/IPVoNet</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINLIPVoNet" runat="server" CssClass="form-control text-right" AutoPostBack="true" ></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINLIPVoNet_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">INF</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINF" runat="server" CssClass="form-control text-right" AutoPostBack="true" ></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtINF_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">IDC</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIDC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIDC_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">IAD</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIAD" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4">
                    <asp:TextBox ID="txtIAD_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>   
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">IAP</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtIAP" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4">
                    <asp:TextBox ID="txtIAP_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>  
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">OFC</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtOFC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4">
                    <asp:TextBox ID="txtOFC_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>  
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">SIT</label>
                <div class="input-group col-4"> 
                    <asp:TextBox ID="txtSIT" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <!--<div class="input-group-append">
                        <span class="input-group-text">Curcuit</span>
                    </div>-->
                </div>
                <div class="input-group col-4">
                    <asp:TextBox ID="txtSIT_NOC" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                </div>  
            </div>
            <div class="form-inline form-group">
                <label class="control-label col-2">NOC</label> 
                <div class="col-4 text-right">          
                    <asp:Label ID="lblTotalService" runat="server"></asp:Label>&nbsp;Curcuit
                </div>
                <div class="col-4 text-right">         
                    <asp:Label ID="lblNOC" runat="server"></asp:Label>&nbsp;Curcuit
                </div>
            </div>
        </div>
        <div class="col-md-4">   
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">INL Bandwidth</label>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Domestic</p>
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtDomestic" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">Mbps</span>
                    </div>
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Utilization</p> 
                <div class="col-7 text-right">          
                    <asp:Label ID="lblINLUtilization" runat="server">
                    </asp:Label>&nbsp;%
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">International</p>
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtInternational" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">Mbps</span>
                    </div>
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Inter. Utilization</p>
                <div class="col-7 text-right">  
                    <asp:Label ID="lblInterUtilization" runat="server">
                    </asp:Label>&nbsp;%
                </div>
            </div> 
            <div class="form-inline form-group form-row">
                <p class="col-5">Inter. Direct</p>
                <div class="col-7 text-right">  
                    <asp:Label ID="lblInterDirect" runat="server">
                    </asp:Label>&nbsp;%
                </div>
            </div> 
            <div class="form-inline form-group form-row">
                <p class="col-5">Direct Traffic</p>
                <div class="col-7 text-right">
                    <asp:Label ID="lblDirectTraffic" runat="server">
                    </asp:Label>&nbsp;Mbps
                    <asp:Label ID="lblDirectTrafficValue" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Caching</p>
                <div class="col-7 text-right">  
                    <asp:Label ID="lblCaching" runat="server"></asp:Label>
                    &nbsp;Mbps
                    <asp:Label ID="lblCachingValue" runat="server" Visible="false"></asp:Label>
                </div>
            </div>  
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">IPV Bandwidth</label>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Ethernet</p> 
                <div class="input-group col-6">  
                    <asp:TextBox ID="txtIPVEthernet" runat="server" CssClass="form-control text-right" AutoPostBack="True" MaxLength="10"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">Mbps</span>
                    </div>
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Utilization</p>
                <div class="col-6 text-right"> 
                    <asp:Label ID="lblIPVUtilization" runat="server">
                    </asp:Label>&nbsp;%
                </div>
            </div>
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Network Bandwidth</label>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Network Port</p> 
                <div class="col-6 text-right">
                    <asp:Label ID="lblNetworkPort" runat="server">
                    </asp:Label>&nbsp;Port
                </div>
            </div>  
            <div class="form-inline form-group form-row">
                <p class="col-5">Ethernet</p> 
                <div class="col-6 text-right">
                    <asp:Label ID="lblNetworkEthernet" runat="server">
                    </asp:Label>&nbsp;Mbps
                </div>
            </div>    
            <div class="form-inline form-group form-row">
                <p class="col-5">Utilization</p>
                <div class="col-6 text-right"> 
                    <asp:Label ID="lblNetworkUtilization" runat="server">
                    </asp:Label>&nbsp;%
                </div>
            </div>
        </div>
    </div>     
    
    <div id="RowCost" runat="server" class="row">
    <hr style="border-color: #DDD;width: 100%;" />
        <div class="col-md-4">
            <div class="form-inline form-group form-row">
                <p class="col-5">Utilization</p> 
                <div class="input-group col-6">
                    <asp:TextBox ID="txtUtilization" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">%</span>
                    </div>
                </div>
            </div>    
            <div class="form-inline form-group form-row">
                <p class="col-5">Direct Traffic</p> 
                <div class="input-group col-6"> 
                    <asp:TextBox ID="txtDirectTraffic" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">%</span>
                    </div>
                </div>
            </div>            
        </div> 
        <div class="col-md-4">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Internet BW Cost</label>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Domestic</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtDomCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div> 
            <div class="form-inline form-group form-row">
                <p class="col-5">All International</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtAllInterCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Transit</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtTransitCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div>  
        </div> 
        <div class="col-md-4">
            <div class="form-inline form-group form-row">
                <p class="col-4">Network</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtNetworkCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div> 
            <div class="form-inline form-group form-row">
                <p class="col-4">Network-Port</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtNetworkPortCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-4">NOC</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtNOCCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div> 
        </div>
    </div>       
    <hr style="border-color: #DDD;width: 100%;" />
    <div class="row">    
        <div class="col-md-12"> 
            <asp:Button ID="btnClear" cssclass="btn btn-warning float-left" OnClientClick="return confirmDelete(this,'�س��ͧ�����ҧ���������� �������');" runat="server" Text="��ҧ����������"  />      
            <asp:Button ID="btnNext" runat="server" Text="�Ѵ�" class="btn btn-success float-right bg-mint" />           
            <asp:Button ID="Button1" runat="server" Text="�ѹ�֡" class="btn btn-success bg-mint float-right mr-2"/>   
        </div>
    </div>             

      
    <asp:Label ID="lblCustomerType" runat="server" Text="-" Visible="false"></asp:Label>  
                 
</asp:Content>  
