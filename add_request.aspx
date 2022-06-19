<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPageMenu.master" CodeFile="add_request.aspx.vb" Inherits="add_request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<%-- <link rel="stylesheet" type="text/css" href="App_Inc/Semantic/semantic.min.css">
<script src="App_Inc/Semantic/semantic.min.js"></script> --%>
    <input id="hide_user_value" type="hidden" runat="server" value="" />
    <input id="hide_subject_prefix" type="hidden" runat="server" value="" />  
    <ol class="breadcrumb headingbar">
        <li class="breadcrumb-item active">สร้างคำขอใหม่</li>  
    </ol>
    
    <div class="form-inline">
        <asp:Label ID="Label13" runat="server" Text="ประเภทคำขอ:" Font-Bold="True" cssclass="col-form-label ml-2"></asp:Label>
        <div class="col-md-5">
            <asp:DropDownList ID="ddlSubjectId" cssclass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSubjectId_SelectedIndexChanged"></asp:DropDownList>
        </div>  
    </div>
        <hr style="border-color: #DDD;width: 100%;" />
            <div id="edit_user_form" runat="server" style="display:none;">
                <div class="col-md-6">
                    <div class="form-group form-row">
                        <p>ค้นหารายชื่อพนักงาน : </p> 
                        <div class="input-group col-9"> 
                            <input ID="SearchtxtFullName" Class="form-control" placeholder="ค้นหาด้วย Email, ชื่อ-นามสกุล" />
                            <input runat="server" id="hide_SearchtxtFullName" xd="hide_SearchtxtFullName" type="hidden">
                        </div>
                    </div>
                </div> 
            </div> 
            <div class="col-md-6">
                <div class="form-group form-row">
                    <p class="col-3">ชื่อผู้ใช้งาน : </p> 
                    <div class="input-group col-9"> 
                        <asp:TextBox ID="txtFullName" xd="txtFullName" runat="server" CssClass="form-control" placeholder="นายทดสอบ ลองดู"></asp:TextBox>
                    </div>
                </div>    
                <div class="form-inline form-group form-row">
                    <p class="col-3">Login Name : </p> 
                    <div class="input-group col-9"> 
                        <asp:TextBox ID="txtEmail" xd="txtEmail" runat="server" CssClass="form-control" placeholder="test.a"></asp:TextBox>
                        <div class="input-group-append">
                            <span class="input-group-text">@jasmine.com</span>
                        </div>
                    </div>
                </div>
                <div class="form-inline form-group form-row">
                    <p class="col-3">สิทธิ์ในการใช้งาน</p> 
                    <div class="input-group col-9"> 
                        <asp:DropDownList ID="ddlPermissionId" xd="ddlPermissionId" cssclass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPermissionId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>  
                <div class="form-inline form-group form-row">
                    <p class="col-3">RO</p> 
                    <div class="input-group col-9"> 
                        <asp:DropDownList ID="ddlRo" xd="ddlRo" cssclass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>                </div>
                </div>
                <div id="add_form_cluster" xd="add_form_cluster" runat="server">              
                    <div class="form-inline form-group form-row">
                        <p class="col-3">Cluster</p> 
                        <div class="input-group col-9"> 
                            <asp:DropDownList ID="ddlCluster" xd="ddlCluster" cssclass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>                </div>
                    </div>
                </div> 
            </div>  
        <hr style="border-color: #DDD;width: 100%;" />
    <div class="row">    
        <div class="col-md-6">
            <asp:LinkButton ID="Button1" runat="server" class="btn btn-success bg-mint float-right">
            <i class="fas fa-save"></i> บันทึก
            </asp:LinkButton>                   
            <input runat="server" class="form-control" id="username" type="hidden"/>  
        </div>
    </div>
<%-- <select name="states" class="ui selection dropdown" multiple="" id="multi-select">
  <option value="">States</option>
  <option value="AL">Alabama</option>
  <option value="AK">Alaska</option>
  <option value="AZ">Arizona</option>
  <option value="AR">Arkansas</option>
  <option value="CA">California</option>
  <option value="OH">Ohio</option>
  <option value="OK">Oklahoma</option>
  <option value="OR">Oregon</option>
  <option value="PA">Pennsylvania</option>
  <option value="RI">Rhode Island</option>
  <option value="SC">South Carolina</option>
  <option value="SD">South Dakota</option>
  <option value="TN">Tennessee</option>
  <option value="TX">Texas</option>
  <option value="UT">Utah</option>
  <option value="VT">Vermont</option>
  <option value="VA">Virginia</option>
  <option value="WA">Washington</option>
  <option value="WV">West Virginia</option>
  <option value="WI">Wisconsin</option>
  <option value="WY">Wyoming</option>
</select> --%>
<script type="text/javascript">
<%-- $('#multi-select').dropdown({
    maxSelections: 3
}); --%>
    $(document).ready(function() { 
        $("#SearchtxtFullName").autocomplete().autocomplete({
	        minLength: 2,
	        focus: function(event, ui) {
		        event.preventDefault();
		        $("#SearchtxtFullName").val(ui.item.label);
                $('input[xd="txtFullName"]').val(ui.item.fullname);
                $('input[xd="txtEmail"]').val(ui.item.value);
                $('select[xd="ddlPermissionId"]').val(ui.item.permission);

                if(ui.item.permission == "approve_cluster"){
                    $('select[xd="ddlRo"]').val(ui.item.c_ro);
                    $('select[xd="ddlCluster"]').val(ui.item.c_cluster);
                    $('div[xd="add_form_cluster"]').css("display", "block");
                }else if(ui.item.permission == "approve_ro"){
                    $('select[xd="ddlRo"]').val(ui.item.dr_ro);
                    $('select[xd="ddlCluster"]').val(ui.item.ub_cluster);
                    $('div[xd="add_form_cluster"]').css("display", "none");
                }else{
                    $('select[xd="ddlRo"]').val(ui.item.ub_ro);
                    $('select[xd="ddlCluster"]').val(ui.item.ub_cluster);
                    $('div[xd="add_form_cluster"]').css("display", "block");
                }
	        },
	        source: function( request, response ) {
            var url = "json_query.aspx?qrs=search_user&v=" + request.term;
            console.log(url)

                $.ajax({
                    url: url,
                    cache: false,
                    dataType: "json",
                    success: function( data ) {
                        response( $.map( data, function( item ) {
                            return {
                                fullname: item.Full_Name,
                                ub_ro: item.UB_RO,
                                ub_cluster: item.UB_Cluster,
                                dr_ro: item.DR_RO,
                                c_ro: item.C_RO,
                                c_cluster: item.C_Cluster,
                                permission: item.Login_permission,
                                label: item.Login_name + " / " + item.Full_Name,
                                value: item.Login_name
                            }
                        }));
                    },
                    error: function() {
                        console.log("autocomplete fail!!");
                        $('#page_loading').fadeOut();
                    }
                });
	        }
        });

        $('#SearchtxtFullName').click(function(){
            $('#SearchtxtFullName').val("");
        });

        $('#SearchtxtFullName').focusout(function() {
            $('#SearchtxtFullName').val("");
            
            if($('input[xd="hide_SearchtxtFullName"]').val().trim().length > 0){
                $('#SearchtxtFullName').val($('input[xd="hide_SearchtxtFullName"]').val());
            }
        });
    });
</script>           
</asp:Content>