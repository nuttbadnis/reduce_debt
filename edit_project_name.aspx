<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="edit_project_name.aspx.vb" Inherits="edit_project_name" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<style type="text/css">
  .file-upload {
    cursor: pointer;      
  }

  .file-upload input {
    top: 0;
    left: 0;
    margin: 0;
    /* Loses tab index in webkit if width is set to 0 */
    opacity: 0;
    filter: alpha(opacity=0);
  }
</style>
            <ol id="menu_create" runat="server" class="breadcrumb headingbar">
                <li class="breadcrumb-item active"><u>Project Overview</u></li>
                <li class="breadcrumb-item"><a id="menu_service" runat="server">Service</a></li>
                <li class="breadcrumb-item"><a id="menu_capex" runat="server">Capex</a></li>
                <li class="breadcrumb-item"><a id="menu_opex" runat="server">Opex</a></li>
                <li class="breadcrumb-item"><a id="menu_other" runat="server">Other</a></li>
                <li class="breadcrumb-item"><a id="menu_management" runat="server">Management</a></li>
                <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>
            </ol>
            <ol id="menu_upload" runat="server" class="breadcrumb headingbar">
                <li class="breadcrumb-item active"><u>Project Overview (Upload)</u></li>
                <li class="breadcrumb-item"><a id="menu_summary_upload" runat="server">Summary (Upload)</a></li>
            </ol>
            <input id="hide_user" type="hidden" runat="server" />  
            <input id="hide_doc_no" type="hidden" runat="server" /> 
            <input id="hide_user_permission" type="hidden" runat="server" />       
            <div class="row" id="div_UploadProject" runat="server">
                 <div class="col-md-12">
                    <div class="form-inline form-group">
                        <asp:Label ID="Label14" runat="server" Text="Upload Project File:" Font-Bold="True" class="col-2" ></asp:Label> 
                        <div class="input-group">
                             <label class="file-upload">
                                <img alt="" src="Images/BrowseButton.png" />
                                <asp:FileUpload ID="FileUploadProject" runat="server" onchange="callme(this)" Width="1px" />
                             </label>
                        </div>
                        <div class="input-group col-3">
                            <asp:Label ID="lblFileUploadName"  runat="server" Text="ไม่ได้เลือกไฟล์ใด"></asp:Label>
                            
                        </div>
                        <div class="input-group col-4">
                            <asp:Label ID="Label15"  runat="server" Text="(เฉพาะไฟล์ Excel เท่านั้น)"></asp:Label>
                        </div>
                    </div>
                 </div>
            </div>
            <div class="row" id="div_OpenProject" runat="server">
                 <div class="col-md-12">
                    <div class="form-inline form-group">
                        <asp:Label ID="Label25" runat="server" Text="Upload Project File:" Font-Bold="True" class="col-2"></asp:Label> 
                        <div class="input-group col-2">
                            <asp:HyperLink ID="LinkFileProject" cssclass="btn btn-info text-white" runat="server"><i class="fas fa-file-alt"></i> Project File</asp:HyperLink>
                        </div>
                        <div class="input-group col-5">
                            <asp:CheckBox ID="chkDeleteProject" Text=". ลบ Project File" runat="server" class="custom-control custom-control-inline  pl-0" AutoPostBack="true" />
                        </div>
                    </div>
                 </div>
            </div>
            
            <hr id="first_line" runat="server" style="border-color: #DDD;width: 100%;" />
            
            <div class="row">           
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
                        <asp:TextBox ID="txtProjectCode1" cssclass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
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
                        <asp:Label ID="Label12" runat="server" Text="Project Code:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtProjectCode" cssclass="form-control" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="txtDocumentDate" autocomplete="off" cssclass="form-control datepicker" runat="server" data-field="date"></asp:TextBox><div id="dtBox"></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="On Service:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtServiceDate" autocomplete="off" cssclass="form-control datepicker" runat="server" data-field="date"></asp:TextBox>
                    </div>
                </div>
            </div>     
            <hr style="border-color: #DDD;width: 100%;" />
                        
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label class="control-label" style="text-decoration: underline;">Customer Assistant</label>
                    </div>
                </div>
                <div class="col-md-12">
                    <div id="data_tabletmpcasst" runat="server"></div>
                </div>
                <%-- <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label14" runat="server" Text="Name:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerAssistantName" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div> --%>
            </div>
            <%-- <div class="row">
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
            </div> --%>
            
            <hr style="border-color: #DDD;width: 100%;" />

            <!--<div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                    <label class="control-label" style="text-decoration: underline;">Customer Contact</label>
                    </div>
                </div>
            </div>-->
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label ID="Label16" runat="server" Text="Contact Name" class="control-label" Font-Underline="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerContactName" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="Label17" runat="server" Text="Tel:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerContactTel" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label ID="Label18" runat="server" Text="Email:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerContactEmail" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div style="margin-top:30px;">
                    <button class="btn btn-success bg-mint" id="button2">
                    <i class="fas fa-user-plus"></i> เพิ่มชื่อ</button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div id="data_tabletmpct" runat="server"></div>
                </div>
            </div>
  
            <hr style="border-color: #DDD;width: 100%;" />
            
            <div class="row">            
                <div class="col-md-12">
                    <label class="control-label" style="text-decoration: underline;">Type Of Contact</label>
                </div>
                <div class="col-sm-5">
                    <asp:Label ID="Label19" runat="server" Text="Company: " Font-Bold="True"></asp:Label>
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:RadioButton ID="rbtTTTBB" runat="server" Text="TTTBB" GroupName="Company" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline"/> 
                            <asp:RadioButton ID="rbtTTTi" runat="server" Checked="True" Text="TTTi" GroupName="Company" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/> 
                            <asp:RadioButton ID="rbtOtherCompany" runat="server" Text="อื่นๆ" GroupName="Company" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/>
                            <asp:TextBox ID="txtOtherCompany" runat="server" Enabled="False" Cssclass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-7">
                    <asp:Label ID="Label8" runat="server" Text="Type Of Service: " Font-Bold="True"></asp:Label>
                    <div class="form-inline">
                         <div class="form-group"> 
                            <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" Text="New Service" GroupName="type" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline" Visible="True"/> 
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="type" Text="Re-New Service" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/> 
                            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="type" Text="Maintenance" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/> 
                            <asp:RadioButton ID="rbtOther" runat="server" Text="อื่นๆ" GroupName="type" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/>
                            <asp:TextBox ID="txtOther" runat="server" Enabled="False" Cssclass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            
            <hr style="border-color: #DDD;width: 100%;" />
            <div class="row">
                <div class="col-md-4">
                    <div class="form-inline form-row">
                        <asp:Label ID="Label26" runat="server" Text="." ForeColor="White"></asp:Label> 
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-5">Contract Period :</p>
                        <div class="input-group col-5">
                            <asp:TextBox ID="txtContract" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">Month</span>
                            </div>
                        </div>
                    </div> 
                    <div class="form-inline form-row">
                        <p class="col-5">Monthly (ราคาขาย)</p>
                        <div class="input-group col-6">
                            <asp:TextBox ID="txtInputPrice" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">THB</span>
                            </div>
                        </div>
                    </div> 
                    <div class="form-inline form-row">
                        <p class="col-5">One-Time Payment</p>
                        <div class="input-group col-6">
                            <asp:TextBox ID="txtOneTimePayment" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">THB</span>
                            </div>
                        </div>
                    </div>
                    <div class="form-inline form-group form-row">
                    </div>
                    <div class="form-inline form-group form-row">
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-5">ค่าใช้จ่ายอื่นๆ</p>
                        <asp:RadioButton ID="rbtSpecialNo" Text=". ไม่มี"  runat="server" Checked="true" GroupName="Special" Cssclass="custom-control custom-radio custom-control-inline" /><asp:RadioButton ID="rbtSpecialYes" Text=". มี" runat="server" GroupName="Special" Cssclass="custom-control custom-radio custom-control-inline" />
                    </div> 
                    <div class="form-inline form-row">
                        <asp:CheckBox ID="chkSpecialApprove" Text=". ขออนุมัติต้นทุนเป็นกรณีพิเศษ" runat="server" Cssclass="form-check ml-1" />
                    </div> 
                </div>
                <div class="col-md-5">
                    <div class="form-inline form-row">
                        <p class="col-4"></p>
                        <div class="input-group col-8">
                            <div class="col-12 text-right">   
                                <u>ยอดตลอดสัญญา</u>
                            </div>
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-4">หลักประกันสัญญา</p>
                        <div class="input-group col-8">
                            <asp:TextBox ID="txtContractGuarantee" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">%</span>
                            </div>
                            &nbsp;&nbsp;&nbsp;รวม
                            <div class="col-5 text-right">   
                                <asp:Label ID="lblTotalContractGuarantee" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                            </div>
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-4">ค่าการตลาด</p>
                        <div class="input-group col-8">
                            <asp:TextBox ID="txtMarketing" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">%</span>
                            </div>
                            &nbsp;&nbsp;&nbsp;รวม
                            <div class="col-5 text-right">   
                                <asp:Label ID="lblTotalMarketing" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                            </div>
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-4">ค่ารับรอง/ค่าของขวัญ</p>
                        <div class="input-group col-8">
                            <asp:TextBox ID="txtEntertainCustomer" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">%</span>
                            </div>
                            &nbsp;&nbsp;&nbsp;รวม
                            <div class="col-5 text-right">   
                                <asp:Label ID="lblTotalEntertainCustomer" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                            </div>
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-4"></p>
                        <div class="input-group col-8">
                            <asp:TextBox ID="txtGift" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">THB</span>
                            </div>
                            &nbsp;&nbsp;&nbsp;รวม
                            <div class="col-5 text-right">   
                                <asp:Label ID="lblTotalGift" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                            </div>
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-4">ค่าปรับงานซ่อม</p>
                        <div class="input-group col-8">
                            <asp:TextBox ID="txtPenalty" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">%</span>
                            </div>
                            &nbsp;&nbsp;&nbsp;รวม
                            <div class="col-5 text-right">   
                                <asp:Label ID="lblTotalPenalty" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-inline form-row">
                        <p class="col-4">ค่าปรับส่งมอบล่าช้า</p>
                        <div class="input-group col-8">
                            <asp:TextBox ID="txtPenaltyLate" runat="server" CssClass="form-control text-right" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <span class="input-group-text">THB</span>
                            </div>
                            &nbsp;&nbsp;&nbsp;รวม
                            <div class="col-5 text-right">   
                                <asp:Label ID="lblTotalPenaltyLate" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                            </div>
                        </div>
                    </div>    
                </div>
                <div id="div_selling" runat="server" class="col-md-3" visible="false">
                    <label class="control-label" style="text-decoration: underline;">Selling Price (Exclude VAT)</label>
                    <div class="form-inline form-row">
                        <p class="col-7">Monthly (ราคาขาย) </p>
                        <div class="col-5 text-right"> 
                            <asp:Label ID="lblMonthlyPrice" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-7">One-Time Payment </p>
                        <div class="col-5 text-right">  
                            <asp:Label ID="lblOneTimePayment" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                        </div>
                    </div> 
                    <div class="form-inline form-row">
                        <p class="col-7">Total Project </p>
                        <div class="col-5 text-right">   
                            <asp:Label ID="lblTotalYearly" runat="server" Text="0.00"></asp:Label>&nbsp;THB
                        </div>
                    </div>
                </div>
                
                <div class="col-md-3" style="border:solid;">
                    <div class="form-inline form-row">
                        <p class="col-7">Payback</p>
                        <div class="col-5 text-right"> 
                            <asp:Label ID="lblPayback" runat="server" Text="0.00"></asp:Label>&nbsp;Months
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-7">Margin</p>
                        <div class="col-5 text-right"> 
                            <asp:Label ID="lblMargin" runat="server" Text="0.00"></asp:Label>&nbsp;%
                        </div>
                    </div>
                    <div class="form-inline form-row">
                        <p class="col-7">NPV (5% per year)</p>
                        <div class="col-5 text-right"> 
                            <asp:Label ID="lblNPV" runat="server" Text="0.00"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            
            <hr style="border-color: #DDD;width: 100%;" />

            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="DetailProject_Label" runat="server" Text="ข้อมูลประกอบการพิจารณาอนุมัติ (ถ้ามี):" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtDetailProject" runat="server" cssclass="form-control" TextMode="MultiLine" Height="90px"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="DetailService_Label" runat="server" Text="รายละเอียดโครงการ:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtDetailService" runat="server" cssclass="form-control" TextMode="MultiLine" Height="90px"></asp:TextBox>
                </div>
            </div> 
            
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label20" runat="server" Text="SLA:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtSLA" runat="server" cssclass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label24" runat="server" Text="MTTR:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtMTTR" runat="server" cssclass="form-control"></asp:TextBox>
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
            </div>       
            <div class="row">

                <!--<div class="col-md-3">
                    <div class="form-group" id="div_OpenSLA" runat="server">
                        <asp:HyperLink ID="LinkFileSLA" runat="server">ไฟล์แนบ SLA</asp:HyperLink>
                        <asp:CheckBox ID="chkDeleteSLA" Text="ลบไฟล์แนบ SLA" runat="server" Visible="false"  />
                    </div>
                    <div class="form-group" id="div_UploadSLA" runat="server">
                        <asp:CheckBox ID="chkSLA" Text="เพิ่มไฟล์แนบ SLA (PDF เท่านั้น)" runat="server" AutoPostBack="true" />
                        <asp:FileUpload ID="FileUploadSLA" runat="server"  Visible="false"  />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group" id="div_OpenFine" runat="server">
                        <asp:HyperLink ID="LinkFileFine" runat="server">ไฟล์แนบค่าปรับ</asp:HyperLink><br />
                        <asp:CheckBox ID="chkDeleteFine" Text="ลบไฟล์แนบค่าปรับ" runat="server" Visible="false" />
                    </div>
                    <div class="form-group"  id="div_UploadFine" runat="server">
                        <asp:CheckBox ID="chkFine" Text="เพิ่มไฟล์แนบ ค่าปรับ (PDF เท่านั้น)" runat="server" AutoPostBack="true" />
                        <asp:FileUpload ID="FileUploadFine" runat="server" Visible="false" />
                    </div>           
                </div>-->
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label21" runat="server" Text="ค่าปรับ:" Font-Bold="True"></asp:Label>  
                    <asp:TextBox ID="txtFine" runat="server" cssclass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <div class="form-group" id="div_UploadDoc" runat="server">
                        <asp:CheckBox ID="chkDoc" Text="เพิ่มไฟล์เอกสารแนบ (pdf, zip และ rar เท่านั้น)" runat="server" AutoPostBack="true"  />
                        <asp:FileUpload ID="FileUploadDoc" runat="server" Visible="false" />
                    </div>
                    <div class="form-group" id="div_OpenDoc" runat="server">
                        <asp:HyperLink ID="LinkFileDoc" runat="server" class="btn btn-danger">
                            <i class="fas fa-file-alt"></i> ไฟล์เอกสารแนบ
                        </asp:HyperLink><br />
                        <asp:CheckBox ID="chkDeleteDoc" Text="ลบไฟล์เอกสารแนบ" runat="server" AutoPostBack="true" />
                    </div>
                </div>
            </div>
            <hr style="border-color: #DDD;width: 100%;" />
            <div class="row">    
                <div class="col-md-12">
                    <asp:Button ID="btnNext" cssclass="btn btn-success float-right bg-mint" runat="server" Text="ถัดไป"  />
                    <asp:Button ID="btnSave" cssclass="btn btn-success float-right bg-mint mr-2" runat="server" Text="บันทึก" />
                </div>
            </div>

    <script type="text/javascript">
        var user = document.getElementById('<%= hide_user.ClientID%>').value;
        var project_id = document.getElementById('<%= hide_doc_no.ClientID%>').value;
        var user_permission = document.getElementById('<%= hide_user_permission.ClientID%>').value;
        datatable_casst();
        datatable_ct();

        function datatable_casst() {
            var url = "json_query.aspx?qrs=select_tmpcasst&menu=edit&u=" + user + "&p_id=" + project_id ;
            console.log("running function datatable");
            console.log(url);
            
            var table = $('#table_tmpcasst').DataTable({
                "ajax": {
                    "url": "" + url + "",
                    "contentType": "application/json",
                    "type": "POST",
                    //"data": "-"
                    "dataSrc": function (json) {
                        console.log(json);
                        return json;
                    }
                },
                "columns": [
                            { 'data': 'Customer_Assistant_ID' },
                            { 'data': 'Customer_Assistant_Name' },
                            { 'data': 'Customer_Assistant_Email' },
                            { 'data': 'Area' },
                            { 'data': 'Cluster' },
                            { 'data': 'Cluster_Name' },
                            //{
                                
                            //    "mData": null,
                            //    "bSortable": false,
                            //    "mRender": function (data, type, value) {
                            //        //qrs,re_id,re_qrs
                            //        var f_data = data["id_List"]+",'delete_tmpcasst','table_tmpcasst','select_tmpcasst'";
                            //        return '<a class="btn btn-sm btn-danger text-white" onclick="delete_employee('+f_data+')"><i class="fas fa-trash-alt"></i></a>';
                            //    }
                            //},
                ],
                "oLanguage": {
                    "sEmptyTable": "ไม่มีข้อมูลพนักงาน"
                },
                ///search input
                "bFilter": false,
                "autoWidth": false,
                "paging": false,
                "info": false,
                ///sort
                //"order": [[11, 'desc']],
                //"ordering": false,
                //length data
                "bLengthChange": false,

                "columnDefs": [
                { "width": "12%", "className": "dt-text-left", "targets": 0 },
                { "width": "13%", "className": "dt-text-left", "targets": 1 },
                { "width": "13%", "className": "dt-text-left", "targets": 2 },
                { "width": "5%", "className": "dt-text-left", "targets": 3 },
                { "width": "5%", "className": "dt-text-left", "targets": 4 },
                { "width": "22%", "className": "dt-text-left", "targets": 5 }
                ]
            });
            table.columns.adjust().draw();
        }

        function datatable_ct() {
            var url = "json_query.aspx?qrs=select_tmpct&menu=edit&u=" + user + "&p_id=" + project_id;
            console.log("running function datatable");
            console.log(url);
            var table = $('#table_tmpct').DataTable({
                "ajax": {
                    "url": "" + url + "",
                    "contentType": "application/json",
                    "type": "POST",
                    //"data": "-"
                    "dataSrc": function (json) {
                        console.log(json);
                        return json;
                    }
                },
                "columns": [
                            { 'data': 'Customer_Contact_Name' },
                            { 'data': 'Customer_Contact_Tel' },
                            { 'data': 'Customer_Contact_Email' },
                            {
                                
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (data, type, value) {
                                    //qrs,re_id,re_qrs
                                    var f_data = data["id_List"]+",'delete_tmpct','table_tmpct','select_tmpct'";
                                    return '<a class="btn btn-sm btn-danger text-white" onclick="delete_employee('+f_data+')"><i class="fas fa-trash-alt"></i></a>';
                                }
                            },
                ],
                "oLanguage": {
                    "sEmptyTable": "ไม่มีข้อมูลพนักงาน"
                },
                ///search input
                "bFilter": false,
                "autoWidth": false,
                "paging": false,
                "info": false,
                ///sort
                //"order": [[11, 'desc']],
                //"ordering": false,
                //length data
                "bLengthChange": false,

                "columnDefs": [
                { "width": "12%", "className": "dt-text-left", "targets": 0 },
                { "width": "8%", "className": "dt-text-left", "targets": 1 },
                { "width": "25%", "className": "dt-text-left", "targets": 2 },
                { "width": "1%", "className": "text-center", "targets": 3 }
                ]
            });
            table.columns.adjust().draw();
        }

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
        
        function callme(oFile)
            {
                var fileUpload = document.getElementById("<%=FileUploadProject.ClientID %>");
                var lblText = document.getElementById("<%=lblFileUploadName.ClientID %>");
                document.getElementById('<%=lblFileUploadName.ClientID %>').innerHTML = fileUpload.files.item(0).name;
            }

        $('#button2').click(function(e) {
            e.preventDefault();

            var contact_name = document.getElementById('<%= txtCustomerContactName.ClientID%>').value;
            var contact_tel = document.getElementById('<%= txtCustomerContactTel.ClientID%>').value;
            var contact_email = document.getElementById('<%= txtCustomerContactEmail.ClientID%>').value;

            var check_data = contact_name != ""
              && contact_tel != ""
              && contact_email != ""
              && user != ""
            if(check_data){
                var url = "json_query.aspx?qrs=insert_tmpct&menu=edit&p_id=" + project_id;
                url += "&c_id=" + contact_name + "&c_te=" + contact_tel + "&c_em=" + contact_email + "&u=" + user;
                console.log(url);
                $.ajax({
                    url: url,
                    cache: false,
                    dataType: "json",
                    success: function (data) {
                        console.log("function insert data success");
                        console.log(data);
                        document.getElementById('<%= txtCustomerContactName.ClientID%>').value = "";
                        document.getElementById('<%= txtCustomerContactTel.ClientID%>').value = "";
                        document.getElementById('<%= txtCustomerContactEmail.ClientID%>').value = "";
                        AlertSuccess(data["Message"], function () {
                            $('#table_tmpct').DataTable().ajax.url('json_query.aspx?qrs=select_tmpct&menu=edit&u=' + user + '&p_id=' + project_id).load();
                        });
                    },///success

                    error: function () {
                        AlertError("function insert data error");
                        console.log("function insert data error");
                    }///error

                });////ajax


            } else {
                AlertNotification("กรุณากรอกข้อมูลให้ครบ ก่อนบันทึกข้อมูลพนักงาน")
            }
            //alert("test");
            return false;
        });

        function delete_employee(id_value,qrs,re_id,re_qrs){
          if((user_permission == 'administrator1' && qrs == 'delete_tmpcasst') || qrs == 'delete_tmpct') { 
              console.log(id_value+" "+qrs+" "+re_id+" "+re_qrs);
              
              bootbox.confirm({
                title: "<span style='font-size:25px;'>แจ้งเตือน</span>",
                message: "<i class='fas fa-3x fa-exclamation-circle text-warning align-middle mr-2'></i><span style='font-size: 18px;'>ต้องการลบข้อมูลพนักงานรายนี้ใช่หรือไม่ ?</span>",
                className: 'bootbox-warning',
                buttons: {
                    confirm: {
                        label: "<i class='fas fa-check'> ลบ",
                        className: 'btn-success'
                    },
                    cancel: {
                        label: "<i class='fas fa-times'> ไม่ลบ",
                        className: 'btn-danger'
                    }
                },
                  callback: function (result) {
                    console.log('This was logged in the callback: ' + result);
                    if(result == true){
                        var url = "json_query.aspx?qrs="+ qrs +"&e_id=" + id_value;
                        console.log(url);
                        var re_url = "json_query.aspx?qrs="+ re_qrs +"&menu=edit&u=" + user + "&p_id=" + project_id;
                        //var re_id = "table_tmpcasst";
                        ajax_new(url,re_url,re_id);
                    }else{
  
                    }

                }
            });
          }
        }
        function ajax_new(url,re_url,re_id){
            console.log(re_url+" "+re_id);
            fetch(url)
            .then((response) => {
                //console.log("then response");
                return response.json();
            })
            .then((data) => {
                //console.log("then data");
                console.log(data);
                //console.log(data["Message"]);
                //value= data["Message"];
                AlertSuccess(data["Message"], function () {
                    //var id = "table_tmpcasst";
                    //var url = "json_query.aspx?qrs=select_tmpcasst&u="+user;
                    reload_datatable(re_id,re_url)
                    //$('#table_tmpcasst').DataTable().ajax.url('json_query.aspx?qrs=select_tmpcasst&u='+user).load();
                });
            });
        }
        function reload_datatable(id,url){
            $('#'+id+'').DataTable().ajax.url(url).load();
        }
    </script>    
   
</asp:Content>     