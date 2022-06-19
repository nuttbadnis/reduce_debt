<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPageMenu.master" Debug="true" ValidateRequest="false" CodeFile="request_list.aspx.vb" Inherits="request_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

<style>
    .searchtable{
        width:15% !important;
        margin-bottom: 3px;
        padding-top: 13px;
        color: #fff;
        background-color: #337ab7;
        }
 </style>
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active"><u>Request List</u></li>
            <li class="breadcrumb-item"><a href="add_request.aspx">Add Request</a></li>
            </ol>

            <div class="row">
                <div class="form-group col-md-12">
                    <div class="form-inline float-left">
                    </div>
                    <div class="form-inline float-right">
                        <asp:DropDownList ID="ddlSubjectId" cssclass="form-control mr-2" AutoPostBack="true" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddlPermissionId" cssclass="form-control mr-2" AutoPostBack="true" runat="server"></asp:DropDownList>
                        <div class="input-group">   
                            <asp:TextBox ID="tbAuto" class="autocomplete form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:button  ID="searchauto" runat="server" class="btn btn-primary pt-0 pb-0" text="ค้นหา" />
                            </div>
                        </div>
                    </div> 
                </div>
            </div
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True"
                DataKeyNames="Request_Id" CssClass="table table-striped table-bordered table-hover bg-mint" EnableModelValidation="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No records has been added." OnSorting="GridView1_Sorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" >
                <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
                <Columns>
                    <asp:BoundField DataField="Request_Id" HeaderText="เลขที่คำขอ" SortExpression="Request_Id" ItemStyle-HorizontalAlign="left">
                        <ItemStyle Width="2%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="subject_name" HeaderText="ประเภทคำขอ" SortExpression="subject_name" ItemStyle-HorizontalAlign="left">
                    </asp:BoundField>
                    <asp:BoundField DataField="Login_Name" HeaderText="Login_Name" SortExpression="Login_Name" ItemStyle-HorizontalAlign="Left">  
                    </asp:BoundField>
                    <asp:BoundField DataField="Full_Name" HeaderText="Full_Name" SortExpression="Full_Name" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Create_By" HeaderText="ผู้สร้างคำขอ" SortExpression="Login_Name" ItemStyle-HorizontalAlign="Left">          
                    </asp:BoundField>
                    <asp:BoundField DataField="Create_Date" HeaderText="วันที่เริ่มเปิดคำขอ" SortExpression="Login_Name" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundField>
                    <asp:BoundField DataField="Update_Date" HeaderText="วันที่อัพเดตล่าสุด" SortExpression="Login_Name" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundField>
                    <asp:BoundField DataField="status_name" HeaderText="สถานะล่าสุด" SortExpression="Login_Name" ItemStyle-HorizontalAlign="Left">
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="Deletedata" imageUrl="http://icons.iconarchive.com/icons/icons8/windows-8/16/Household-Waste-icon.png" runat="server" data-toggle="modal" onclick="Delete_Data" text="ลบข้อมูล"/>
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                
                <EditRowStyle BackColor="#CCCCCC" ForeColor="Black"/>
                <FooterStyle BackColor="#1ABC9C" ForeColor="White"/>
                <HeaderStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Right" BackColor="#107763" ForeColor="white" CssClass="page-gridview bg-mint"/>
                <RowStyle BackColor="#E3EAEB"  ForeColor="#337AB7" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblSort" runat="server" Text="" Visible="false"></asp:Label>
            

<%--         </div>--%>

<!---=================Delete Modal============================-->

        <div class="modal fade modal-mint" id="deletecapex" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">ลบข้อมูล Capex</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                        <div class="modal-body">
                        <asp:label ID="msg_delete" runat="server" Text="คุณต้องการลบข้อมูลหรือไม่"></asp:label>
                        <asp:label ID="capexid_delete" runat="server" Visible="false"></asp:label>
                        <!-- The form is placed inside the body of modal -->
                        
                        </div><!---div class modal-body-->
                        <div class="modal-footer">
                             <asp:Button ID="cancel_delete" runat="server" Text="ไม่ลบข้อมูล" class="btn btn-warning flaot-left"/>	
                             <asp:Button ID="delete" runat="server" Text="ลบข้อมูล" class="btn btn-danger float-right"/>	
                        </div><!---div class modal-footer-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->

<!---=================Delete Modal============================-->

<%--<link rel="stylesheet" href="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.css" />
<script src="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.js"></script>--%>
            
    <script type="text/javascript">
        //$("#updatecapex").modal("show");
        //$(function () {
        var main_group;
        var sub_group;
        if(document.getElementById('<%= ddlSubjectId.ClientID%>').value == "--- เลือกประเภทคำขอ ---"){
            main_group = ""
        }else {
            main_group = document.getElementById('<%= ddlSubjectId.ClientID%>').value;
        }
        
        if(document.getElementById('<%= ddlPermissionId.ClientID%>').value == "--- เลือกสิทธิ์ในการใช้งาน ---"){
            sub_group = ""
        }else {
            sub_group = document.getElementById('<%= ddlPermissionId.ClientID%>').value;
        }

            $(".autocomplete").autocomplete({
                minLength: 3,
                source: function (request, response) {
                    console.log('defaultJson.aspx?qrs=autocomplete_capex&term=' + request.term + '&mgroup=' + main_group + '&sgroup=' + sub_group)
                    $.ajax({
                        url: 'defaultJson.aspx?qrs=autocomplete_capex&mgroup=' + main_group + '&sgroup=' + sub_group,
                        data: { term: request.term },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                    
                            response($.map(data, function (item) {
                                return {
                                    label: item.CAPEX_Name,
                                    value: item.CAPEX_Name
                                }
                            }));
                        },
                        error: function () {
                            alert('notsucccesss');
                        }
                    });
                },
                select: function (event, ui) {
                    //bootbox.alert("sentdata success", function () { window.location.reload(); });
                    var value = ui.item.value;
                    $('.autocomplete').val(value);
                   $('.searchtable').click();

                }
            
            });
           
       // });
     //   $('.tb').on('autocompleteselect', function (e, ui) {
     //       tb(ui.item.value);
       // });

       $("ul li").click(function () {
           $(this).parent().children().removeClass("active");
           $(this).addClass("active");
       });
       
       function check(f,type) {
           
            var select_id = f.id;
            var select_value = f.value;
            console.log(select_id + "," + select_value+ ","+ type);
            select_dataajax(select_id, select_value, type);
        }

   </script> 
</asp:Content>  
