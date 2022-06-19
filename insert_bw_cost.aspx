<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPageMenu.master" CodeFile="insert_bw_cost.aspx.vb" Inherits="insert_bw_cost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<Style>
    .searchtable{
        width:15% !important;
        margin-bottom: 3px;
        padding-top: 13px;
        color: #fff;
        background-color: #337ab7;
        }
 </style>
    <ol class="breadcrumb headingbar">
        <li class="breadcrumb-item"><a href="insert_capex.aspx">Capex(Corp.)</a></li>
        <li class="breadcrumb-item"><a href="insert_opex.aspx">Opex</a></li>
        <li class="breadcrumb-item"><a href="insert_other.aspx">Other</a></li>
        <li class="breadcrumb-item"><a href="insert_management.aspx">Management</a></li> 
        <li class="breadcrumb-item active"><u>BW Cost</u></li>  
    </ol>
    
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label13" runat="server" Text="ประเภทลูกค้า:" Font-Bold="True"></asp:Label>
            <asp:DropDownList ID="ddlCustomerType" cssclass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>  
        </div>
    </div>
    <div id="RowCost" runat="server" class="row">
    <hr style="border-color: #DDD;width: 100%;" />
        <div class="col-md-4">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Model</label>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Utilization</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtUtilization" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">%</span>
                    </div>
                </div>
            </div>    
            <div class="form-inline form-group form-row">
                <p class="col-5">Direct Traffic</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtDirectTraffic" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">%</span>
                    </div>
                </div>
            </div> 
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Difference Cost</label>
            </div>
            <div class="form-inline form-group form-row">
                <p class="col-5">Min-Max</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtMinMaxDiffCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">%</span>
                    </div>
                </div>
            </div>              
        </div> 
        <div class="col-md-4">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Internet BW Cost Structure (/Mbps)</label>
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
            <div class="form-inline form-group form-row">
                <p class="col-5">Network</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtNetworkCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div> 
            <div class="form-inline form-group form-row">
                <p class="col-5">Network-Port</p> 
                <div class="input-group col-7"> 
                    <asp:TextBox ID="txtNetworkPortCost" runat="server" CssClass="form-control text-right"></asp:TextBox>
                    <div class="input-group-append">
                        <span class="input-group-text">THB</span>
                    </div>
                </div>
            </div> 
            <div class="form-inline form-group form-row">
                <p class="col-5">NOC</p> 
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
            <asp:Button ID="Button1" runat="server" Text="บันทึก" class="btn btn-success bg-mint float-left"/> 
            <input runat="server" class="form-control" id="username" type="hidden"/>  
        </div>
    </div>            
</asp:Content>