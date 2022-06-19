<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="check_status_list_20210402.aspx.vb" Inherits="check_status_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">            
    <style type="text/css">
    .searchtable{
        width:6% !important;
        margin-bottom: 3px;
        padding-top: 13px;
        color: #fff;
        background-color: #337ab7;
        }
    .ui-autocomplete {
        max-height: 200px;
        overflow-y: auto;
        /* prevent horizontal scrollbar */
        overflow-x: hidden;
        }
    .datepicker{
        margin-top: 55px;   
        }
    .pagination
    {
      line-height: 26px;
    }

    .pagination span
    {
      padding: 5px;
      border: solid 1px #477B0E;
      text-decoration: none;
      white-space: nowrap;
      background: #547B2A;
    }

    .pagination a, 
    .pagination a:visited
    {
      text-decoration: none;
      padding: 6px;
      white-space: nowrap;
    }
    .pagination a:hover, 
    .pagination a:active
    {
      padding: 5px;
      border: solid 1px #9ECDE7;
      text-decoration: none;
      white-space: nowrap;
      background: #486694;
    }
    .loader
    {
        width: 3rem; 
        height: 3rem;
        position: fixed;   
        top: 50%;
        z-index: 9999;
    }
    </style>        
    <div class="text-center">
    <div class="spinner-border text-primary loader invisible" role="status">
        <span class="sr-only">Loading...</span>
    </div>
    </div>
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active">Check Status</li><!--<li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
            <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>--></ol>

            <div class="form-inline form-group">
                    <div class="form-group col-md-2 p-0">
                        <asp:Label ID="Label2" runat="server" Text="วันที่จัดทำ: " Font-Bold="true" cssclass="mr-2"></asp:Label>
                        <asp:TextBox ID="txtDocumentDate" autocomplete="off" cssclass="form-control dateselect" runat="server" Width="60%" data-field="date" data-format="dd-MMM-yyyy"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-2 p-0">
                        <asp:Label ID="Label6" runat="server" Text="Area: " Font-Bold="True" cssclass="mr-2"></asp:Label>  
                        <asp:DropDownList ID="ddlArea" cssclass="form-control" runat="server" AutoPostBack="True" Width="65%" ></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-2 p-0">
                        <asp:Label ID="Label7" runat="server" Text="Cluster: " Font-Bold="True" cssclass="mr-2"></asp:Label>  
                        <asp:DropDownList ID="ddlCluster" cssclass="form-control" runat="server" AutoPostBack="True" Width="65%"></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-3 p-0">
                        <asp:Label ID="Label1" runat="server" Text="Status: " Font-Bold="true" cssclass="mr-2"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" Width="60%" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-3 p-0">
                        <asp:TextBox ID="tbAuto" cssclass="autocomplete form-control mr-2" runat="server" width="65%"></asp:TextBox>       
                        <asp:button  ID="searchauto" runat="server" cssclass="btn btn-primary mr-2" text="ค้นหา" />
                    </div>
            </div>
<%--         <asp:ScriptManager ID="ScriptManager_test" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel_test" runat="server">
        <ContentTemplate>--%>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table bg-mint display compact" Width="100%" Font-Size="13px">
                <Columns>
                    <asp:TemplateField HeaderText="เอกสาร">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Document_No") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="Link_DocumentID" runat="server" Text='<%# Bind("Document_No") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="14%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Document_Date" HeaderText="วันที่จัดทำ">
                        <ItemStyle Width="9%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Area" HeaderText="Area">
                        <ItemStyle Width="3%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cluster" HeaderText="Cluster">
                        <ItemStyle Width="3%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Project_Name" HeaderText="Project Name">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateBy" HeaderText="จัดทำโดย">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status_name" HeaderText="Status">
                        <ItemStyle Width="8%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Step" >
                        <ItemTemplate>
                            <asp:Label Text='<%# GetStep_Flow(Eval("Document_No").ToString())%>'
                                runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="2%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Code" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                        <asp:Label ID="lblRefNo" Text="<%# Bind('Project_Code') %>" runat="server" />
                        <asp:Label ID="lblGetJob" runat="server" Text="<%# Bind('Get_Job') %>" Visible="false"></asp:Label>
                        <asp:LinkButton Id="btn_EditProjectcode" class="text-warning" runat="server"
                            CommandName='<%# Eval("Project_Code") %>'
                            CommandArgument='<%# Eval("Document_No") %>' 
                            OnCommand="LinkButton_EditProjectcode"  ToolTip="แก้ไข Project Code">
                            <i class="fas fa-pencil-alt"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="13%" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                        <asp:LinkButton Id="flow_data" class="text-danger" runat="server"
                            CommandName="Order" 
                            CommandArgument='<%# Eval("Document_No") %>' 
                            OnCommand="LinkButton_Command"  ToolTip="รายละเอียดการอนุมัติ">
                            <i class="fas fa-lg fa-info-circle"></i>
                        </asp:LinkButton>
                        <asp:LinkButton Id="btn_Duplicate" class="text-info" runat="server"
                            CommandName="Duplicate" 
                            CommandArgument='<%# Eval("Document_No") %>' 
                            OnClientClick="return confirmDelete(this,'คุณต้องการ คัดลอกโปรเจค หรือไม่');"
                            OnCommand="LinkButton_Duplicate"  ToolTip="คัดลอกโปรเจค">
                            <i class="far fa-lg fa-copy"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="7%" />
                        <ItemStyle Width="7%" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
                <%--<FooterStyle BackColor="#FFFFCC" ForeColor="#000FFF" />--%>
<%--                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" CssClass="pagination" />
                <Pagersettings Mode="Numeric" Position="Bottom" PageButtonCount="10"/>--%>
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#000FFF" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />

            </asp:GridView>
            
            <div style="text-align:center">
                <asp:Label ID="lblNoData" runat="server" Text="-- ไม่พบ Project ที่มีสิทธิ์ในการดูข้อมูลได้ --" Width="100%" Font-Bold="true"  Visible="false"></asp:Label>
            </div>
            
            <asp:Button ID="btnLoadMore" cssclass="test_test btn btn-success" runat="server" Text="Load More Data" style="display: none;" />
<%--            </ContentTemplate>   
        </asp:UpdatePanel>--%>

            <div class="modal modal-mint" id="myModal" tabindex="-1">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                    <div class="modal-header">
                        <h5 id="TextHeaderDetail" class="modal-title" runat="server">Flow Step</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table id="table_flow" class="table table-striped table_flow">
                            <thead class='txt-blue'>
                                <tr  align="center">
                                    <th style="width: 5%;">#</th>
                                    <th style="width: 5%;">Step</th>
                                    <!--<th style="width: 5%;">Next</th>-->
                                    <th style="width: 10%;">ส่วนงาน</th>
                                    <th style="width: 10%;">สถานะ</th>
                                    <th style="width: 15%;">วัน-เวลา</th>
                                    <th style="width: 15%;">ผู้ดำเนินการ</th>
                                    <th style="width: 15%;">อีเมล์</th>
                                    <th style="width: 15%;">หมายเหตุ</th>
                                    <!--<th style="width: 15%;">เอกสารประกอบ</th>-->
                                </tr>
                            </thead>
                            <tbody runat="server" id="inn_flow"></tbody>
                        </table>
                    </div>
                    </div>
                </div>
            </div>
            
            <div class="modal modal-mint" id="divEditProjectCode" tabindex="-1">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Project Code</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="Project Code:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtProjectCode" cssclass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-check form-check-inline">
                                        <asp:RadioButton ID="rbtWork" Text="ได้งาน" cssclass="form-check-input" runat="server" GroupName="WorkOrNot" />
                                        <asp:RadioButton ID="rbtNotWork" Text="ไม่ได้งาน" cssclass="form-check-input" runat="server" GroupName="WorkOrNot" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="ข้อมูลเจ้าของโครงการ:" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtProjectOwner" TextMode="MultiLine" Height="90px" cssclass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Date_Label" runat="server" Text="วันที่เปิดบริการ (dd/MM/yyyy):" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtServiceDate" autocomplete="off" cssclass="form-control dateselect" runat="server" data-field="date"></asp:TextBox> 
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancelProjectCode" class="btn btn-danger" runat="server" Text="ยกเลิก" />
                            <asp:Button ID="btnSaveProjectCode" class="btn btn-success" runat="server" Text="บันทึก" OnClientClick="return confirmDelete(this,'คุณต้องการแก้ไข Project Code หรือไม่');" />
                            <asp:Label ID="lblDocNo" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        
                    </div>
                </div>
            </div>

<%--    <link rel="stylesheet" href="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.css" />
    <script type="text/javascript" src="App_Inc/jquery-ui-1.12.1/jquery-ui-1.12.1.js"></script>--%>
        
    <script type="text/javascript">
        $(document).ready(function () {
            $( ".loader" ).addClass( "invisible" );
            loader_data();
            console.log("window top : " + $(window).scrollTop());
            console.log("window height : " + $(window).height());
            console.log("document height : " + $(document).height());
            <%-- $(document).scroll(function (e) { //bind scroll event

                var intBottomMargin = 0; //Pixels from bottom when script should trigger

                //if less than intBottomMargin px from bottom
                if ($(window).scrollTop() >= $(document).height() - $(window).height() - intBottomMargin) {
                    //var clickButton = document.getElementById("<%= btnLoadMore.ClientID %>");
                    $('.test_test').click();
                    //clickButton.click();//trigger click
                }

            }); --%>

        });
        function loader_data(){
            $(window).scroll(function () {
                if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                    alert("End Of The Page");
                    $( ".loader" ).removeClass( "invisible" );
                    $( ".loader" ).addClass( "visible" );
                    $('.test_test').click();
                }
            });
        }
        <%-- $('.display').dataTable({
            bLengthChange: false,
            "order": [[0, "desc"]],
            "columnDefs": [
                { type: "date-uk", targets: 1 }
            ],
            "pageLength": 10,
            // lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: false,
            bSort: true,
            bPaginate: false,
            info: false
            }); --%>
        // DateTimePicker
        callDatePicker();
       
        // autocomplete
        $(function () {
            $(".autocomplete").autocomplete({
                minLength: 3,
                source: function (request, response) {
                    console.log('defaultJson.aspx?qrs=autocomplete_check_status&term=' + request.term)
                    $.ajax({
                        url: 'defaultJson.aspx?qrs=autocomplete_check_status',
                        data: { term: request.term },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                    
                            response($.map(data, function (item) {
                                return {
                                    label: item.Project_Name,
                                    value: item.Project_Name
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
       $("ul li").click(function () {
           $(this).parent().children().removeClass("active");
           $(this).addClass("active");
       });
    </script> 
    
</asp:Content>    