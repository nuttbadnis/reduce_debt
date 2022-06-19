<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="attach_contract_file.aspx.vb" Inherits="attach_contract_file" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <div class="col-md-10 pull-right">
    <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active"><asp:Label ID="lblDocNo" runat="server" Text="-" Font-Bold="True"></asp:Label></li><!--<li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
            <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>--></ol>
             <div class="col-md-12">
                <div class="form-group">
                      
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Upload ไฟล์สัญญา (PDF เท่านั้น)" Font-Bold="true"></asp:Label> 
                     <asp:FileUpload ID="FileUploadContract" runat="server" />
                    
                </div>
            </div>
            <div class="col-md-12">
                <asp:Button ID="btnUpload" cssclass="btn btn-success" runat="server" Text="Upload" />
            </div>
    </div>
</asp:Content>