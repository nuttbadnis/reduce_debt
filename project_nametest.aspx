<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="project_nametest.aspx.vb" Inherits="project_nametest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
            
    <div class="col-md-10 pull-right">  

            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active">Project Name</li><li class="breadcrumb-item"><a href="add_service.aspx">Add Service</a></li><li class="breadcrumb-item"><a href="add_capex.aspx">Add Capex</a></li><li class="breadcrumb-item"><a href="add_opex.aspx">Add Opex</a></li><li class="breadcrumb-item"><a href="add_other.aspx">Add Other</a></li><li class="breadcrumb-item"><a href="Summary.aspx">Summary</a></li></ol>
      
           
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Project Name:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtProjectName" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <!--<div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Ref.No.:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtLocationName" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label11" runat="server" Text="Project Code:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtProjectCode" cssclass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>-->
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="ลูกค้า:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtCustomerName" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="Label12" runat="server" Text="โครงการ:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtEnterprise" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="Label13" runat="server" Text="ประเภทลูกค้า:" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlCustomerType" cssclass="form-control" runat="server"></asp:DropDownList>  
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Prepare:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtDocumentDate" autocomplete="off" cssclass="form-control datepicker" runat="server" data-field="date"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="On Service:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtServiceDate" autocomplete="off" cssclass="form-control datepicker" runat="server" data-field="date"></asp:TextBox>
                </div>
            </div>

            <hr style="border-color: #DDD;width: 100%;" />
            
            <div class="col-md-12">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Customer Assistant</label>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label14" runat="server" Text="Name:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtCustomerAssistantName" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="Label15" runat="server" Text="ID:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtCustomerAssistantID" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>   
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="Label6" runat="server" Text="Area:" Font-Bold="True"></asp:Label>  
                    <asp:DropDownList ID="ddlArea" cssclass="form-control" runat="server" AutoPostBack="True" ></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group"> 
                    <asp:Label ID="Label7" runat="server" Text="Cluster:" Font-Bold="True"></asp:Label>  
                    <asp:DropDownList ID="ddlCluster" cssclass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                </div>   
            </div> 
            <div class="col-md-3">
                <div class="form-group"> 
                    <asp:Label ID="Label10" runat="server" Text="Prepared by:" Font-Bold="True"></asp:Label>  
                    <asp:DropDownList ID="ddlClusterName" cssclass="form-control" runat="server"></asp:DropDownList>
                </div>   
            </div>    
            </div>
            
            <hr style="border-color: #DDD;width: 100%;" />
            
            <div class="col-md-12">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Customer Contact</label>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label16" runat="server" Text="ชื่อผู้ติดต่อ:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtCustomerContactName" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label17" runat="server" Text="โทรศัพท์:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtCustomerContactTel" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label18" runat="server" Text="Email:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtCustomerContactEmail" cssclass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            </div>
  
            <hr style="border-color: #DDD;width: 100%;" />
            
            <div class="col-md-12">
            <div class="form-group">
                <label class="control-label" style="text-decoration: underline;">Type Of Contact</label>
            </div>
            <div class="form-group">
                  <div class="form-inline">
                        <asp:Label ID="Label19" runat="server" Text="Company: " Font-Bold="True"></asp:Label>
                        &nbsp;
                        <asp:RadioButton ID="rbtTTTBB" runat="server" Text="TTTBB" GroupName="Company" AutoPostBack="True" Cssclass="radio-inline" Visible="True"/> 
                        <asp:RadioButton ID="rbtTTTi" runat="server" Checked="True" Text="TTTi" GroupName="Company" AutoPostBack="True" Cssclass="radio-inline"/> 
                        <asp:RadioButton ID="rbtOtherCompany" runat="server" Text="อื่นๆ" GroupName="Company" AutoPostBack="True" Cssclass="radio-inline"/>
                        <asp:TextBox ID="txtOtherCompany" runat="server" Enabled="False" Cssclass="form-control" Width="8%"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label8" runat="server" Text="Type Of Service: " Font-Bold="True"></asp:Label> 
                        &nbsp;
                        <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" Text="New Service" GroupName="type" AutoPostBack="True" Cssclass="radio-inline" Visible="True"/> 
                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="type" Text="Re-New Service" AutoPostBack="True" Cssclass="radio-inline"/> 
                        <asp:RadioButton ID="RadioButton3" runat="server" GroupName="type" Text="Maintenance" AutoPostBack="True" Cssclass="radio-inline"/> 
                        <asp:RadioButton ID="rbtOther" runat="server" Text="อื่นๆ" GroupName="type" AutoPostBack="True" Cssclass="radio-inline"/>
                        <asp:TextBox ID="txtOther" runat="server" Enabled="False" Cssclass="form-control" Width="8%"></asp:TextBox>
                  </div>
            </div>
            </div>
            
            <hr style="border-color: #DDD;width: 100%;" />
            
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label9" runat="server" Text="รายละเอียดโครงการ:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtDetailService" runat="server" cssclass="form-control" TextMode="MultiLine" Height="90px" MaxLength="300"></asp:TextBox>
                </div>
            </div> 
            
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label20" runat="server" Text="SLA:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtSLA" runat="server" cssclass="form-control" TextMode="MultiLine" Height="90px" MaxLength="300"></asp:TextBox>
                </div>
            </div> 
            <div class="col-md-12" style="padding: 0px;">
                <div class="col-md-6"> 
                <div class="form-group">
                    <asp:Label ID="Label21" runat="server" Text="ค่าปรับ:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtFine" runat="server" cssclass="form-control" TextMode="MultiLine" Height="100px" MaxLength="300"></asp:TextBox>
                </div>
                </div>
                <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label22" runat="server" Text="Monitor Date:" Font-Bold="True"></asp:Label>  
                    <asp:DropDownList ID="ddlMonitorDate" cssclass="form-control" runat="server"></asp:DropDownList>
                </div>
                </div>
                <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label23" runat="server" Text="Monitor Time:" Font-Bold="True"></asp:Label>  
                    <asp:DropDownList ID="ddlMonitorTime" cssclass="form-control" runat="server"></asp:DropDownList>
                </div>
                </div>
                <div class="col-md-3">
                <div class="form-group" id="div_UploadSLA" runat="server">
                    <asp:CheckBox ID="chkSLA" Text="เพิ่มไฟล์แนบ SLA (PDF เท่านั้น)" runat="server" AutoPostBack="true" />
                    <asp:FileUpload ID="FileUploadSLA" runat="server" Visible="false" />
                </div>
                <div class="form-group" id="div_OpenSLA" runat="server">
                    <asp:HyperLink ID="LinkFileSLA" runat="server">ไฟล์แนบ SLA</asp:HyperLink><br />
                    <asp:CheckBox ID="chkDeleteSLA" Text="ลบไฟล์แนบ SLA" runat="server" />
                </div>
                </div>
                <div class="col-md-3">
                <div class="form-group"  id="div_UploadFine" runat="server">
                    <asp:CheckBox ID="chkFine" Text="เพิ่มไฟล์แนบ ค่าปรับ (PDF เท่านั้น)" runat="server" AutoPostBack="true" />
                    <asp:FileUpload ID="FileUploadFine" runat="server" Visible="false" />
                </div>
                <div class="form-group" id="div_OpenFine" runat="server">
                    <asp:HyperLink ID="LinkFileFine" runat="server">ไฟล์แนบค่าปรับ</asp:HyperLink><br />
                    <asp:CheckBox ID="chkDeleteFine" Text="ลบไฟล์แนบค่าปรับ" runat="server" />
                </div>
                </div>
            </div>
         
            <div class="col-md-12">
                <asp:Button ID="btnSave" cssclass="btn btn-success" runat="server" Text="บันทึก" />
            </div>
                     
    </div>



    <script type="text/javascript">


//    $("#dtBox").DateTimePicker({
//      dateSeparator: "/",
//      dateFormat: "dd/MM/yyyy",
//      readonlyInputs: true
 //    });

        //$(function () {
            //    $("[id*=datetimepicker4]").datetimepicker({
            //        format: 'DD/MM/YYYY'
            //    });
        //});
        $(function () {
            $(".datepicker").datepicker({
                clearBtn: true,
                autoclose: true,
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                language: "tr"
            });
        });
    </script>    
   
</asp:Content>     