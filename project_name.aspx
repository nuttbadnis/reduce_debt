<%@ Page MaintainScrollPositionOnPostback="true" Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="true" CodeFile="project_name.aspx.vb" Inherits="project_name" EnableEventValidation="false" %>
<%@ MasterType TypeName="MasterPageMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
  .ui-autocomplete {
    max-height: 200px;
    overflow-y: auto;
    /* prevent horizontal scrollbar */
    overflow-x: hidden;
  }
  .datepicker{
    margin-top: 55px;   
  }
  
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
    <input id="hide_user" type="hidden" runat="server" /> 
    <input id="hide_doc_no" type="hidden" runat="server" />  
    <input id="hide_user_permission" type="hidden" runat="server" />         
    <ol id="menu_create" runat="server" class="breadcrumb headingbar">
        <li class="breadcrumb-item active"><u>Project Overview</u></li>
        <li class="breadcrumb-item"><a href="add_service.aspx?menu=create">Service</a></li>
        <li class="breadcrumb-item"><a href="add_capex.aspx?menu=create">Capex</a></li>
        <li class="breadcrumb-item"><a href="add_opex.aspx?menu=create">Opex</a></li>
        <li class="breadcrumb-item"><a href="add_other.aspx?menu=create">Other</a></li>
        <li class="breadcrumb-item"><a href="add_management.aspx?menu=create">Management</a></li>
        <li class="breadcrumb-item"><a href="Summary.aspx?menu=create">Summary</a></li>
    </ol>
    <ol id="menu_upload" runat="server" class="breadcrumb headingbar">
        <li class="breadcrumb-item active"><u>Project Overview (Upload)</u></li>
        <li class="breadcrumb-item"><a href="Summary_Upload.aspx?menu=upload">Summary (Upload)</a></li>
    </ol>
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
                        <asp:TextBox ID="txtDocumentDate" autocomplete="off" cssclass="form-control dateselect" runat="server" data-field="date" data-format="dd-MMM-yyyy"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="On Service:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtServiceDate" autocomplete="off" cssclass="form-control dateselect" runat="server" data-field="date"></asp:TextBox>
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
                <div class="col-md-4" id="searchCustomerAssistant" runat="server">
                    <div class="form-group">
                        <asp:Label ID="assistant_name" runat="server" Text="ค้นหาพนักงาน :" Font-Bold="True" ></asp:Label> 
                        <input id="assistant_search" class="form-control"/>
                    </div>
                </div>
            </div> 
            <div class="row" id="selectCustomerAssistant" runat="server">
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="Label_id" runat="server" Text="ID:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerAssistantID" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>  
                <div class="col-md-2" style="padding-left:0;">
                    <div class="form-group">
                        <asp:Label ID="Label_name" runat="server" Text="Name:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerAssistantName" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="padding-left:0;">
                    <div class="form-group">
                        <asp:Label ID="Label_email" runat="server" Text="Email:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerAssistantEmail" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>           
                <div class="col-md-1" style="padding-left:0;">
                    <div class="form-group">
                        <asp:Label ID="Label_area" runat="server" Text="Area:" Font-Bold="True"></asp:Label>             
                        <br />
                        <asp:DropDownList ID="ddlArea" cssclass="form-control" runat="server" onchange="check(this,'cluster')">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-1" style="padding-left:0;">
                    <div class="form-group"> 
                        <asp:Label ID="Label_cluster" runat="server" Text="Cluster:" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlCluster" class="form-control" runat="server" onchange="check(this,'cluster_name')">
                        </asp:DropDownList>
                    </div>   
                </div> 
                <div class="col-md-2" style="padding-left:0;">
                    <div class="form-group"> 
                        <asp:Label ID="Label_prepared" runat="server" Text="Cluster Approved by:" Font-Bold="True"></asp:Label>  
                        <asp:DropDownList ID="ddlClusterName" cssclass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>   
                </div>
                <div style="margin-top:30px;">
                    <%--<asp:Button runat="server" class="btn btn-success" ID="Button1" Text="บันทึก" AutoPostback = false />--%>
                    <button class="btn btn-success bg-mint" id="button1">
                    <i class="fas fa-user-plus"></i> เพิ่มชื่อ</button>
                </div>
            </div>
            <div class="row">         
                <div class="col-md-12">

                    <div id="data_tabletmpcasst" runat="server"></div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="None" 
                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table" EnableModelValidation="True" OnRowDeleting="GridView1_RowDeleting">
                        <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" font-size="14px"/>
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="1%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("id_List") %>' Visible="false"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Customer_Assistant_ID" HeaderText="ID" >
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer_Assistant_Name" HeaderText="Name" >
                                <ItemStyle Width="16%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer_Assistant_Email" HeaderText="Email" >
                                <ItemStyle Width="17%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Area" HeaderText="Area" >
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cluster" HeaderText="Cluster" >
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cluster_Name" HeaderText="Cluster Approved by" >
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
<%--                            <asp:TemplateField ShowHeader="False">
                                <ItemStyle Width="2%" />
                                <ItemTemplate>
                                    <asp:Linkbutton runat="server" 
                                        CommandName="Delete" OnClientClick="return confirm('ต้องการลบรายชื่อรายนี้ใช่หรือไม่');"
                                        Text="<span class='fas fa-times'></span>" style="color:red;"/>               
                                </ItemTemplate>
                            </asp:TemplateField>  --%>
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <PagerStyle BackColor="#1ABC9C" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <HeaderStyle BackColor="#1ABC9C" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>    
                </div>
            </div>
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
                        <asp:Label ID="Label16" runat="server" Text="Contact Name" class="control-label"  Font-Underline ="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerContactName" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="Label17" runat="server" Text="Tel:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerContactTel" cssclass="form-control number" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:Label ID="Label18" runat="server" Text="Email:" Font-Bold="True"></asp:Label>  
                        <asp:TextBox ID="txtCustomerContactEmail" cssclass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div style="margin-top:30px;">
<%--                <Button runat="server" class="btn btn-success bg-mint" ID="Button2" onserverclick="Button2_Click">
                        <i class="fas fa-user-plus"></i> บันทึก
                    </button>--%>
                    <button class="btn btn-success bg-mint" id="button2">
                    <i class="fas fa-user-plus"></i> เพิ่มชื่อ</button>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div id="data_tabletmpct" runat="server"></div>            
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="None" width="72%"
                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table" EnableModelValidation="True" OnRowDeleting="GridView2_RowDeleting">
                        <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" font-size="14px"/>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("id_List") %>' Visible="false"></asp:Label>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Customer_Contact_Name" HeaderText="ชื่อผู้ติดต่อ" >
                                <ItemStyle Width="38%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer_Contact_Tel" HeaderText="โทรศัพท์" >
                                <ItemStyle Width="24%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Customer_Contact_Email" HeaderText="Email" >
                                <ItemStyle Width="38%" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Linkbutton ID="btn_delete" Enabled="false" runat="server" 
                                        CommandName="Delete" OnClientClick="return confirm('ต้องการลบรายชื่อรายนี้ใช่หรือไม่');"
                                        Text="<span class='fas fa-times'></span>" style="color:red;"/>               
                                </ItemTemplate>
                            </asp:TemplateField>  
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <PagerStyle BackColor="#1ABC9C" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <HeaderStyle BackColor="#1ABC9C" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>    
                </div>
            </div>
            <hr style="border-color: #DDD;width: 100%;" />

            <div class="row">            
                <div class="col-md-12">
                    <label class="control-label" style="text-decoration: underline;">Type Of Contact</label>      
                </div>

                <div class="col-sm-5">
                    <asp:Label ID="Label19" runat="server" Text="Company: " Font-Bold="True"></asp:Label>
                    <div class="form-inline row">
                        <div class="form-group">
                            <asp:RadioButton ID="rbtTTTBB" runat="server" Text="TTTBB" GroupName="Company" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline" />
                            <asp:RadioButton ID="rbtTTTi" runat="server" Checked="True" Text="TTTi" GroupName="Company" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/>

                            <asp:RadioButton ID="rbtOtherCompany" runat="server" Text="อื่นๆ" GroupName="Company" AutoPostBack="True" Cssclass="custom-control custom-radio custom-control-inline pl-0"/>
                            <asp:TextBox ID="txtOtherCompany" runat="server" Enabled="False" Cssclass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-7 row">
                    <asp:Label ID="Label8" runat="server" Text="Type Of Service: " Font-Bold="True"></asp:Label> 
                    <div class="form-inline row">
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
                        <asp:CheckBox ID="chkSpecialApprove" Text=". ขออนุมัติต้นทุนเป็นกรณีพิเศษ" runat="server" CssClass="form-check ml-1"/>
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
                    <div class="form-group" id="div_UploadSLA" runat="server">
                        <asp:CheckBox ID="chkSLA" Text="เพิ่มไฟล์แนบ SLA (PDF เท่านั้น)" runat="server" AutoPostBack="true" CssClass="custom-control custom-checkbox" />
                        <asp:FileUpload ID="FileUploadSLA" runat="server" Visible="false" accept=".pdf" />
                    </div>
                    <div class="form-group" id="div_OpenSLA" runat="server">
                        <asp:HyperLink ID="LinkFileSLA" runat="server">ไฟล์แนบ SLA</asp:HyperLink><br />
                        <asp:CheckBox ID="chkDeleteSLA" Text="ลบไฟล์แนบ SLA" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group"  id="div_UploadFine" runat="server">
                        <asp:CheckBox ID="chkFine" Text="เพิ่มไฟล์แนบ ค่าปรับ (PDF เท่านั้น)" runat="server" AutoPostBack="true" />
                        <asp:FileUpload ID="FileUploadFine" runat="server" Visible="false" accept=".pdf" />
                    </div>
                    <div class="form-group" id="div_OpenFine" runat="server">
                        <asp:HyperLink ID="LinkFileFine" runat="server">ไฟล์แนบค่าปรับ</asp:HyperLink><br />
                        <asp:CheckBox ID="chkDeleteFine" Text="ลบไฟล์แนบค่าปรับ" runat="server" />
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
                        <asp:CheckBox ID="chkDoc" Text="เพิ่มไฟล์เอกสารแนบ (pdf, zip หรือ rar เท่านั้น)" runat="server" AutoPostBack="true" />
                        <asp:FileUpload ID="FileUploadDoc" runat="server" Visible="false" />
                    </div>
                    <div class="form-group" id="div_OpenDoc" runat="server">
                        <asp:HyperLink ID="LinkFileDoc" runat="server">ไฟล์เอกสารแนบ</asp:HyperLink><br />
                        <asp:CheckBox ID="chkDeleteDoc" Text="ลบไฟล์เอกสารแนบ" runat="server" AutoPostBack="true" />
                    </div>
                </div>
            </div>
            
            <hr style="border-color: #DDD;width: 100%;" />
            <div class="row">    
                <div class="col-md-12">
                    <asp:Button ID="btnClear" cssclass="btn btn-warning float-left" OnClientClick="return confirmDelete(this,'คุณต้องการล้างข้อมูลใหม่ หรือไม่');" runat="server" Text="ล้างข้อมูลใหม่"  />
                    <asp:Button ID="btnNext" cssclass="btn btn-success float-right bg-mint" runat="server" Text="ถัดไป"  />
                    <asp:Button ID="btnSave" cssclass="btn btn-success float-right bg-mint mr-2" runat="server" Text="บันทึก"  />
                </div>
            </div>
            

  
    <script type="text/javascript">

        var user = document.getElementById('<%= hide_user.ClientID%>').value;
        var project_id = document.getElementById('<%= hide_doc_no.ClientID%>').value;
        var user_permission = document.getElementById('<%= hide_user_permission.ClientID%>').value;
        datatable_casst();
        datatable_ct();
        add_typenumber();
        callDatePicker();
        //ajax_new();
        function add_typenumber(){
            $('.number').each(function(){ $(this).attr('type','number'); });
        }

        function datatable_casst() {
            var url = "json_query.aspx?qrs=select_tmpcasst&menu=create&u=" + user + "&p_id=" + project_id;
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
                "ordering": false,
                //length data
                "bLengthChange": false,

                "columnDefs": [
                { "width": "12%", "className": "dt-text-left", "targets": 0 },
                { "width": "20%", "className": "dt-text-left", "targets": 1 },
                { "width": "13%", "className": "dt-text-left", "targets": 2 },
                { "width": "5%", "className": "dt-text-left", "targets": 3 },
                { "width": "5%", "className": "dt-text-left", "targets": 4 },
                { "width": "22%", "className": "dt-text-left", "targets": 5 }
                ]
            });
            table.columns.adjust().draw();
        }

        function datatable_ct() {
            var url = "json_query.aspx?qrs=select_tmpct&menu=create&u=" + user + "&p_id=" + project_id;
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
                "ordering": false,
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

        function check(f,type) {
           
            var select_id = f.id;
            var select_value = f.value;
            console.log(select_id + "," + select_value+ ","+ type);
            select_dataajax(select_id, select_value, type);
        }
        function select_dataajax(id, value, type) {
            if(type == "cluster"){
                var url = "json_query.aspx?qrs=select_cluster&r=" + value
            } else if(type == "cluster_name") {
                var value_area = document.getElementById('<%= ddlArea.ClientID%>').value;
                var url = "json_query.aspx?qrs=select_clustername&r=" + value_area + "&c=" + value 
            }

            console.log(url);
            var option_data = "";
            $.ajax({
                url: url,
                cache: false,
                dataType: "json",
                success: function (data) {
                    console.log('function select ' + type + ' success');
                    if (type == "cluster") {
                        var DropDownList = document.getElementById('<%= ddlCluster.ClientID%>');
                    } else if (type == "cluster_name") {
                        var DropDownList = document.getElementById('<%= ddlClusterName.ClientID%>');
                    }

                    for (i = DropDownList.length - 1; i >= 0; i--) {
                        DropDownList.remove(i);
                    }
                    var option = document.createElement("option");
                    option.text = "(โปรดเลือก)";
                    option.value = "(โปรดเลือก)";
                    DropDownList.options.add(option);
                    //alert(DropDownList.length);
                    if (data.length > 0) {
                        console.log('function select '+ type +' success data > 0');
                        if (type == "cluster") {
                            $.each(data, function (i, item) {
                                //console.log(data[i].Cluster);
                                var option = document.createElement("option");
                                option.text = data[i].Cluster;
                                option.value = data[i].Cluster;
                                DropDownList.options.add(option);
                            });
                        } else if (type == "cluster_name") {
                            $.each(data, function (i, item) {
                                //console.log(data[i].Cluster);
                                var option = document.createElement("option");
                                option.text = data[i].Cluster_name;
                                option.value = data[i].Cluster_email;
                                DropDownList.options.add(option);
                            });
                        }
                    } else {
                        console.log('function select '+ type +' success data = 0');
                    }
                },///success

                error: function () {
                    AlertError("select option error");
                    console.log('function select data error');
                }///error

            });////ajax
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
                        var re_url = "json_query.aspx?qrs="+ re_qrs +"&menu=create&u=" + user + "&p_id=" + project_id;
                        //var re_id = "table_tmpcasst";
                        ajax_new(url,re_url,re_id);
                    }else{
  
                    }

                }
            });
          }
        }

        function reload_datatable(id,url){
            $('#'+id+'').DataTable().ajax.url(url).load();
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
                var url = "json_query.aspx?qrs=insert_tmpct";
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
                            $('#table_tmpct').DataTable().ajax.url('json_query.aspx?qrs=select_tmpct&menu=create&u=' + user + '&p_id=' + project_id).load();
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

        $('#button1').click(function(e) {
            e.preventDefault();

            var customer_id = document.getElementById('<%= txtCustomerAssistantID.ClientID%>').value;
            var customer_name = document.getElementById('<%= txtCustomerAssistantName.ClientID%>').value;
            var customer_email = document.getElementById('<%= txtCustomerAssistantEmail.ClientID%>').value;
            var area = document.getElementById('<%= ddlArea.ClientID%>').value;
            var cluster = document.getElementById('<%= ddlCluster.ClientID%>').value;
            var e = document.getElementById('<%= ddlClusterName.ClientID%>');
            var cluster_name = e.options[e.selectedIndex].text;
            var cluster_email = document.getElementById('<%= ddlClusterName.ClientID%>').value;

            var check_data = customer_id != ""
              && customer_name != ""
              && customer_email != ""
              && area != ""
              && cluster != ""
              && cluster_name != ""
              && cluster_email != ""
              && user != ""
            if(check_data){
                var url = "json_query.aspx?qrs=insert_tmpcasst";
                url += "&cm_id=" + customer_id + "&cm_n=" + customer_name + "&cm_em=" + customer_email + "&a=" + area;
                url += "&ct=" + cluster + "&ct_n=" + cluster_name + "&ct_em=" + cluster_email + "&u=" + user;
                console.log(url);
                $.ajax({
                    url: url,
                    cache: false,
                    dataType: "json",
                    success: function (data) {
                        console.log("function insert data success");
                        console.log(data);
                        //if (data["code"] == "insert success") {
                        AlertSuccess(data["Message"], function () {
                            $('#table_tmpcasst').DataTable().ajax.url('json_query.aspx?qrs=select_tmpcasst&menu=create&u=' + user + '&p_id=' + project_id).load();
                        });
                        //}
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
        //$(function () {

            //$("#txtDocumentDate").datepicker({
            //    clearBtn: true,
            //    autoclose: true,
            //    changeMonth: true,
            //    changeYear: true,
            //    Format: "dd/mm/yyyy",
            //    //language: "tr"
            //});
            //$("#txtDate").datepicker({
            //    clearBtn: true,
            //    autoclose: true,
            //    changeMonth: true,
            //    changeYear: true,
            //    Format: "dd/mm/yyyy",
            //    //language: "tr"
            //});
            //$(".datepicker").datepicker({
            //    clearBtn: true,
            //    autoclose: true,
            //    changeMonth: true,
            //    changeYear: true,
            //    Format: "dd/mm/yyyy",
            //    //language: "tr"
            //});
       // });
        //search_autocomplete("assistant_search", "user");


        function select_data(select, qrs ,value) {
            var url = 'json_query.aspx?qrs=' + qrs;
            console.log(url);
            var option_data = "";
            $.ajax({
                url: url,
                cache: false,
                dataType: "json",
                success: function (data) {
                    console.log('function select data success');
                    option_data += "<select name='" + select + "' class='form-control'>";
                    option_data += "<option selected='selected' value='(โปรดเลือก)'>(โปรดเลือก)</option>";
                    $.each(data, function (i, item) {
                        option_data += "<option value='" + item.RO + "'>" + item.RO + "</option>";
                    });
                    option_data += "</select>";
                    //return option_data;
                    $("#assistant_list").append(option_data);
                },///success

                error: function () {
                    AlertError("select option error");
                    console.log('function select data error');
                }///error

            });////ajax
            //return option_data;
        }

        function search_autocomplete(select, url) {
            $("#" + select + "").autocomplete({
                minLength: 2,
                select: function (event, ui) {

                    selectemployee(ui.item.value);
                    $(this).val("");
                    event.preventDefault();
                },
                source: function (request, response) {
                    var data = $("#" + select + "").val();
                    ///encode data to utf-8 before send
                    console.log(data);
                    var ajax_url = ''
                    if (url == 'user') {
                        ajax_url = "json_query.aspx?qrs=select_user&value=" + data;
                    } else {

                    }
                    console.log(ajax_url);

                    $.ajax({

                        url: ajax_url,
                        cache: false,
                        dataType: "json",
                        success: function (data) {
                            console.log(this.url);
                            console.log('autocomplete user success');
                            console.log(data);
                            if (url == 'user') {
                                response($.map(data, function (item) {
                                    return {
                                        label: "T26-" + item.Login_id + " : " + item.Login_name,
                                        value: "T26-"+item.Login_id + "_" + item.Login_name + "_" + item.Login_name,
                                    }

                                }));///respone

                            } else {

                            }

                        },///success

                        error: function (xmlhttprequest, textstatus, message) {
                            console.log('message : ' + message + '');
                            console.log('textstatus : ' + textstatus + '');
                            console.log('xmlhttprequest : ' + xmlhttprequest + '');
                            console.log('autocomplete employee error');
                        },///error

                    });////ajax
                }/////source
            }).focus(function () {
                $(this).autocomplete("search");
            });
        }

        function fnCheckSelection(){
            $.ajax({
                cache: false,
                url: "/select_area",
                type: "POST",
                success: function (result) {
                    AlertSuccess(result);
                },
                error: function (msg) {
                    AlertError("false");
                }
            });
        }

        function selectemployee(data, select) {
            console.log(data);
            var split_data = data.split("_");
            var employee_code = split_data[0];
            var employee_name = split_data[1];
            var employee_email = split_data[2];

            document.getElementById("<%= txtCustomerAssistantID.ClientID%>").value = employee_code;
            document.getElementById("<%= txtCustomerAssistantName.ClientID%>").value = employee_name;
            document.getElementById("<%= txtCustomerAssistantEmail.ClientID%>").value = employee_email + "@jasmine.com";
            //var employee_data = "";
            //employee_data += "<div class='row'>";
            //employee_data += "<div class='col-md-1' style='width: 12% !important;'>";
            //employee_data += "<div class='form-group'>";
            //employee_data += "<span style='font-weight:bold;'>ID:</span>";
            //employee_data += "<input name='assitant_empname' class='form-control' type='text' value='" + employee_code + "' />";
            //employee_data += "</div></div>";
            //employee_data += "<div class='col-md-2' style='width: 20% !important;'>";
            //employee_data += "<div class='form-group'>";
            //employee_data += "<span style='font-weight:bold;'>Name:</span>";
            //employee_data += "<input name='assitant_empcode' class='form-control' type='text' value='" + employee_name + "' />";
            //employee_data += "</div></div>";
            //employee_data += "<div class='col-md-2' style='width: 20% !important;'>";
            //employee_data += "<div class='form-group'>";
            //employee_data += "<span style='font-weight:bold;'>Email:</span>";
            //employee_data += "<input name='assitant_empemail' class='form-control' type='text' value='" + employee_email + "@jasmine.com' />";
            //employee_data += "</div></div>";


            //$("#assistant_list").append(employee_data);
            //employee_data += "<div class='col-md-1' style='width: 12% !important;'>";
            //employee_data += "<div class='form-group'>";
            //employee_data += "<span style='font-weight:bold;'>Area:</span>";
            //select_data("assitant_area", "select_area");
            //employee_data += "</div></div>";
            //employee_data += "</div>";
        }
        
        function callme(oFile)
            {
                var fileUpload = document.getElementById("<%=FileUploadProject.ClientID %>");
                var lblText = document.getElementById("<%=lblFileUploadName.ClientID %>");
                document.getElementById('<%=lblFileUploadName.ClientID %>').innerHTML = fileUpload.files.item(0).name;
            }

    </script>    
 
</asp:Content>     