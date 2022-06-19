<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPageMenu.master" Debug="true" ValidateRequest="false" CodeFile="insert_capex_mass.aspx.vb" Inherits="insert_capex_mass" %>
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
            <li class="breadcrumb-item"><a href="insert_capex.aspx">Insert Capex(Corp.)</a></li>
            <li class="breadcrumb-item active">Insert Capex(Mass)</li>
            <li class="breadcrumb-item"><a href="insert_opex.aspx">Insert Opex</a></li>
            <li class="breadcrumb-item"><a href="insert_other.aspx">Insert Other</a></li>
            </ol>

        <div class="col-md-12">
            <button type="button" class="btn btn-danger float-left" data-toggle="modal" data-target="#insertcapex" style="margin-bottom: 1%;cursor:pointer;">
            <i class="fas fa-plus-circle"></i> เพิ่มข้อมูล</button> 
                        
            <div class="form-inline float-right">
                <asp:DropDownList ID="ddlGroup" cssclass="form-control mr-2" AutoPostBack="true" runat="server"></asp:DropDownList>
                <div class="input-group">
                <asp:TextBox ID="tbAuto" class="autocomplete form-control" runat="server"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:button ID="searchauto" runat="server" class="btn btn-primary pt-0 pb-0" text="ค้นหา"/>
                    </div>
                </div>
            </div> 
               
  

            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                DataKeyNames="CAPEX_Mass_id" CssClass="table table-striped table-bordered table-hover bg-mint" EnableModelValidation="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="No records has been added." OnSorting="GridView1_Sorting" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" >
                <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
                <Columns>
                    <asp:BoundField DataField="CAPEX_Mass_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="CAPEX_Mass_id">
                        <ItemStyle Width="2%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Item_Code" HeaderText="Item_Code" SortExpression="Item_Code">
               
                    </asp:BoundField>
                    <asp:BoundField DataField="CAPEX_Mass_Name" HeaderText="CAPEX_Mass_Name" SortExpression="CAPEX_Mass_Name">
               
                    </asp:BoundField>
                    <asp:BoundField DataField="Main_Group" HeaderText="Main_Group" SortExpression="Main_Group">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Sub_Group" HeaderText="Sub_Group" SortExpression="Sub_Group">
                       
                    </asp:BoundField>
                    <asp:BoundField DataField="Asset_Type" HeaderText="Asset_Type" SortExpression="Asset_Type">
             
                    </asp:BoundField>
                    <asp:BoundField DataField="Equipment_Cost" HeaderText="Equipment_Cost" SortExpression="Equipment_Cost">
               
                    </asp:BoundField>
                    <asp:BoundField DataField="Labor_Cost" HeaderText="Labor_Cost" SortExpression="Labor_Cost">
                       
                    </asp:BoundField> 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="Editdata" imageUrl="Images/editicon.ico" runat="server" data-toggle="modal" onclick="Edit" text="แก้ไขข้อมูล"/>
                            <asp:ImageButton ID="Deletedata" imageUrl="http://icons.iconarchive.com/icons/icons8/windows-8/16/Household-Waste-icon.png" runat="server" data-toggle="modal" onclick="Delete_Data" text="ลบข้อมูล"/>
                        </ItemTemplate>
                        <ItemStyle Width="5%"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                
                <EditRowStyle BackColor="#CCCCCC" ForeColor="Black"/>
                <FooterStyle BackColor="#1ABC9C" ForeColor="White"/>
                <HeaderStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Right" BackColor="#1ABC9C" ForeColor="Black" CssClass="pagination-ys"/>
                <RowStyle BackColor="#E3EAEB"  ForeColor="#337AB7" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblSort" runat="server" Text="" Visible="false"></asp:Label>
         </div>

<!---=================Insert Modal============================-->

        <div class="modal fade modal-mint" id="insertcapex" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">เพิ่มข้อมูล Capex(Mass)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    </div>
                        <div class="modal-body">
                        <!-- The form is placed inside the body of modal -->                     
                            <div class="form-group">
                                <label for="usr">ItemCode:</label>
					            <input runat="server" class="form-control" id="itemcode" type="text" />
				            </div>
                            <div class="form-group">
                                <label for="usr">CapexMassName:</label>
					            <input runat="server" class="form-control" id="capexname" type="text" />
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
                                <label for="usr">AssetType:</label>
					            <input runat="server" class="form-control" id="assettype" type="text" />
				            </div>
				            <div class="form-group">
                                <label for="usr">EquipmentCost:</label>	
					            <input runat="server" class="form-control" id="equipmentcost" type="text" />
				            </div>
				            <div class="form-group">
                                <label for="usr">LaborCost:</label>	
					            <input runat="server" class="form-control" id="laborcost" type="text" />
					            <input runat="server" class="form-control" id="username" type="hidden"/>
				            </div>	
				            <asp:Button ID="insertsubmit" runat="server" Text="บันทึก" class="btn btn-success pull-right"/>	
				            <asp:button ID="resetbutton" runat="server" type="reset" Text="reset" class="btn btn-warning" 
                                OnClientClick="this.form.reset();return false;"/>
                       
                     </div><!---div class modal-body-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->
            
<!---=================Insert Modal============================-->
<!---=================Update Modal============================-->

        <div class="modal fade modal-mint" id="updatecapex" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">แก้ไขข้อมูล Capex(Mass)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    </div>
                        <div class="modal-body">
                        <!-- The form is placed inside the body of modal -->
                            <div class='row'>
                                <div class='col-md-12'>
                                    <asp:label ID="capexid_update" runat="server"></asp:label>
                                    <div class="form-group">
                                        <label for="usr">ItemCode:</label>
                                        <asp:TextBox ID="itemcode_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
                                    <div class="form-group">
                                        <label for="usr">CapexMassName:</label>
                                        <asp:TextBox ID="capexname_update" runat="server" CssClass="form-control"></asp:TextBox>
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
                                        <label for="usr">AssetType:</label>
                                        <asp:TextBox ID="assettype_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
				                    <div class="form-group">
                                        <label for="usr">EquipmentCost:</label>	
                                        <asp:TextBox ID="equipmentcost_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>
				                    <div class="form-group">
                                        <label for="usr">LaborCost:</label>	
                                        <asp:TextBox ID="laborcost_update" runat="server" CssClass="form-control"></asp:TextBox>
				                    </div>	
                                     <asp:Button ID="update" runat="server" Text="อัพเดต" class="btn btn-success pull-right"/>	
                                  </div>
                              </div>
                        </div><!---div class modal-body-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->

<!---=================Update Modal============================-->
<!---=================Delete Modal============================-->

        <div class="modal fade modal-mint" id="deletecapex" tabindex="-1" role="dialog" aria-labelledby="Login" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">ลบข้อมูล Capex(Mass)</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    </div>
                        <div class="modal-body">
                        <asp:label ID="capexid_delete" runat="server"></asp:label>
                        <!-- The form is placed inside the body of modal -->
                        คุณต้องการลบข้อมูลหรือไม่
                        </div><!---div class modal-body-->
                        <div class="modal-footer">
                             <asp:Button ID="cancel_delete" runat="server" Text="ไม่ลบข้อมูล" class="btn btn-warning pull-left"/>	
                             <asp:Button ID="delete" runat="server" Text="ลบข้อมูล" class="btn btn-danger pull-right"/>	
                        </div><!---div class modal-footer-->
                  </div><!---div class modal-content-->
                </div><!---div class modal-dialog-->
            </div> <!---div class model fade-->

<!---=================Delete Modal============================-->
</div>
<link rel="stylesheet" href="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.css" />
<script src="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.js"></script>
            
    <script>
        $(function () {
            $(".autocomplete").autocomplete({
                minLength: 3,
                source: function (request, response) {
                    console.log('defaultJson.aspx?qrs=autocomplete_capexmass&term=' + request.term)
                    $.ajax({
                        url: 'defaultJson.aspx?qrs=autocomplete_capexmass',
                        data: { term: request.term },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                    
                            response($.map(data, function (item) {
                                return {
                                    label: item.CAPEX_Mass_Name,
                                    value: item.CAPEX_Mass_Name
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
           
        });
     //   $('.tb').on('autocompleteselect', function (e, ui) {
     //       tb(ui.item.value);
       // });

       $("ul li").click(function () {
           $(this).parent().children().removeClass("active");
           $(this).addClass("active");
       });

   </script> 
</asp:Content>  
