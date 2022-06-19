<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPageMenu.master" CodeFile="insert_other.aspx.vb" Inherits="insert_other" %>
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
            <li class="breadcrumb-item active"><u>Other</u></li> 
            <li class="breadcrumb-item"><a href="insert_management.aspx">Management</a></li>
            <li class="breadcrumb-item"><a href="insert_bw_cost.aspx">BW Cost</a></li> 
            </ol>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-inline float-left">
                        <button type="button" class="btn btn-danger float-left mr-2" data-toggle="modal" data-target="#insertother" style="margin-bottom: 1%;">
                            <i class="fas fa-plus-circle"></i> เพิ่มข้อมูล
                        </button>
                        <button type="button" id='btnUploadOther' runat="server" class="btn btn-danger float-left" data-toggle="modal" data-target="#uploadother" style="margin-bottom: 1%;">
                            <i class="fas fa-plus-circle"></i> Upload File ข้อมูล
                        </button>
                    </div>
                    <div class="form-inline float-right">
                        <asp:DropDownList ID="ddlGroup" runat="server" class="form-control mr-2" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control mr-2" AutoPostBack="true">
                        </asp:DropDownList> 
                        <div class="input-group"> 
                            <asp:TextBox ID="tbAuto" class="autocomplete form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:button  ID="searchauto" runat="server" class="btn btn-primary pt-0 pb-0" text="ค้นหา"/>
                            </div>
                        </div>
                    </div>
                </div> 
            </div>

			<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" 
                DataKeyNames="OTHER_id" GridLines="None" EnableModelValidation="True" CssClass="table table-striped table-bordered table-hover bg-mint" AllowSorting="True"
                EmptyDataText="ไม่มีข้อมูลที่ทำการค้นหา" OnPageIndexChanging="OnPageIndexChanging">
                <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#337AB7"/>

                <Columns>
                    <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="HiddenFieldOtherId" Value='<%# Eval("OTHER_id") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField> --%>
                    <asp:BoundField DataField="OTHER_id" HeaderText="Id" SortExpression="OTHER_id" ItemStyle-CssClass="d-none" HeaderStyle-CssClass="d-none">
                    <HeaderStyle Width="50px"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="OTHER_Code" HeaderText="OTHER_Code" SortExpression="OTHER_Code" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle Width="85px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OTHER_Name" HeaderText="OTHER_Name" SortExpression="OTHER_Name" ApplyFormatInEditMode="True" ItemStyle-HorizontalAlign="Left">
                    <ControlStyle Width="100%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Main_Group" HeaderText="Main_Group" SortExpression="Main_Group" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Sub_Group" HeaderText="Sub_Group" SortExpression="Sub_Group" ItemStyle-HorizontalAlign="Left">               
                    </asp:BoundField>
                    <asp:BoundField DataField="OTHER_Cost" HeaderText="OTHER_Cost" SortExpression="OTHER_Cost" ItemStyle-HorizontalAlign="Right">
                    <HeaderStyle Width="85px" />
                    <ControlStyle Width="85px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="Editdata" imageUrl="Images/editicon.ico" runat="server" data-toggle="modal" data-target="#updatecapex" onclick="Edit" text="แก้ไขข้อมูล"/>
                            <asp:ImageButton ID="Deletedata" imageUrl="http://icons.iconarchive.com/icons/icons8/windows-8/16/Household-Waste-icon.png" runat="server" data-toggle="modal" data-target="#deletecapex" onclick="Delete_Data" text="ลบข้อมูล"/>
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                </Columns>

                <EditRowStyle BackColor="#cccccc" ForeColor="black"/>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Right" BackColor="#107763" ForeColor="white" CssClass="page-gridview bg-mint" />
                <RowStyle BackColor="#E3EAEB"  ForeColor="#337AB7" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            </asp:GridView>
			<asp:Label ID="lblSort" runat="server" Text="" Visible="false"></asp:Label>
			
			<asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
                </asp:DropDownList>
         
                <asp:GridView ID="GridViewDataUpload" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Small"
                    Width="500px" Visible="False">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                
                <asp:GridView ID="GridViewSheet" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Visible="False" Width="400px">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
 
<!---=================Insert Modal============================-->
        <div class="modal fade modal-mint" id="insertother" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">เพิ่มข้อมูล Other</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <!-- The form is placed inside the body of modal -->                     
                        <div class="form-group">
                            <label for="usr">OtherCode:</label>
					        <input runat="server" class="form-control" id="othercode" type="text" />
				        </div>
                        <div class="form-group">
                            <label for="usr">OtherName:</label>
					        <input runat="server" class="form-control" id="othername" type="text" />
				        </div>
				        <div class="form-group">
                            <label for="usr">MainGroup:</label>	
					        <input runat="server" class="form-control" id="maingroup" type="text" />
				        </div>
				        <div class="form-group">
                            <label for="usr">SubGroup:</label>	
					        <input runat="server" class="form-control" id="subgroup" type="text" />
				        </div>
				        <div class="form-group">
                            <label for="usr">OtherCost:</label>	
					        <input runat="server" class="form-control" id="othercost" type="text" />
					        <input runat="server" class="form-control" id="username" type="hidden"/>
				        </div>
						
				        <asp:Button ID="insertsubmit" runat="server" Text="บันทึก" class="btn btn-success float-right"/>	
				        <asp:button ID="resetbutton" runat="server" type="reset" Text="reset" class="btn btn-warning" OnClientClick="this.form.reset();return false;"/>
                           
                    </div><!---div class modal-body-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->
<!---=================Insert Modal============================-->

<!---=================Upload Insert Modal============================-->

        <div class="modal fade modal-mint" id="uploadother" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Uplaod File ข้อมูล Other</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    </div>

                    <div class="modal-body">
                    <!-- The form is placed inside the body of modal -->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Upload File (เฉพาะไฟล์ Excel -</label><a href="~/Upload/Ex_FileUpload/Ex_OTHER.xlsx"> ข้อมูลตาม Format นี้ </a><label>):</label>
                                    <asp:FileUpload ID="FileUploadOther" runat="server" />
                                </div>
                            </div>
                        </div>
                     </div><!---div class modal-body-->
                     <div class="modal-footer">
				        <asp:button ID="UploadOtherReset" runat="server" type="reset" Text="reset" class="btn btn-warning" 
                                OnClientClick="this.form.reset();return false;"/>
                    	<asp:Button ID="UploadOtherSubmit" runat="server" Text="อัปโหลด" class="btn btn-success"/>	
                     </div><!---div class modal-footer-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->

<!---=================Upload Insert Modal============================-->

<!---=================Update Modal============================-->
        <div class="modal fade modal-mint" id="updateother" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">อัพเดตข้อมูล Other</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                        <div class="modal-body">
                        <!-- The form is placed inside the body of modal -->
                            <div class='row'>
                                <div class='col-md-12'>
                                    <asp:Label ID="otherid_update" runat="server" Visible="false"></asp:Label>
                                    <div class="form-group">
                                        <label for="usr">OtherCode:</label>
                                        <asp:TextBox ID="othercode_update" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
                                    <div class="form-group">
                                        <label for="usr">OtherName:</label>
                                        <asp:TextBox ID="othername_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
                                    <div class="form-group">
                                        <label for="usr">MainGroup:</label>	
                                        <asp:TextBox ID="maingroup_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
				                    <div class="form-group">
                                        <label for="usr">SubGroup:</label>	
                                        <asp:TextBox ID="subgroup_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
				                    <div class="form-group">
                                        <label for="usr">OtherCost:</label>	
                                        <asp:TextBox ID="othercost_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
                                     <asp:Button ID="update" runat="server" Text="อัพเดต" class="btn btn-success float-right"/>	
                                  </div>
                              </div>
                        </div><!---div class modal-body-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->
<!---=================Update Modal============================-->
<!---=================Delete Modal============================-->
        <div class="modal fade modal-mint" id="deleteother" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">ลบข้อมูล Other</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                        <div class="modal-body">
                        <asp:label ID="msg_delete" runat="server" Text="คุณต้องการลบข้อมูลหรือไม่"></asp:label>
                        <asp:label ID="otherid_delete" runat="server" Visible="false"></asp:label>
                        <!-- The form is placed inside the body of modal -->
                        </div><!---div class modal-body-->
                        <div class="modal-footer">
                             <asp:Button ID="cancel_delete" runat="server" Text="ไม่ลบข้อมูล" class="btn btn-warning float-left"/>	
                             <asp:Button ID="delete" runat="server" Text="ลบข้อมูล" class="btn btn-danger float--right"/>	
                        </div><!---div class modal-footer-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->
<!---=================Delete Modal============================-->

<link rel="stylesheet" href="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.css" />
<script src="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.js"></script>

   <script type="text/javascript">
       var main_group;
       var sub_group;
       if(document.getElementById('<%= ddlGroup.ClientID%>').value == "-- Group ทั้งหมด --"){
            main_group = ""
       }else {
            main_group = document.getElementById('<%= ddlGroup.ClientID%>').value;
       }
        
       if(document.getElementById('<%= ddlSubGroup.ClientID%>').value == "-- SubGroup ทั้งหมด --"){
            sub_group = ""
       }else {
            sub_group = document.getElementById('<%= ddlSubGroup.ClientID%>').value;
       }
       
       $(function () {
           $(".autocomplete").autocomplete({
               minLength: 3,
               source: function (request, response) {
                   console.log('defaultJson.aspx?qrs=autocomplete_other&term=' + request.term + '&mgroup=' + main_group + '&sgroup=' + sub_group)
                   $.ajax({
                       url: 'defaultJson.aspx?qrs=autocomplete_other' + '&mgroup=' + main_group + '&sgroup=' + sub_group,
                       data: { term: request.term },
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (data) {

                           response($.map(data, function (item) {
                               return {
                                   label: item.OTHER_Name,
                                   value: item.OTHER_Name
                               }
                           }));
                       },
                       error: function () {
                          console.log('notsucccesss');
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

       });

 
   </script>
</asp:Content>  