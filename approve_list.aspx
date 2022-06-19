<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master"  AutoEventWireup="false" CodeFile="approve_list.aspx.vb" Inherits="approve_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
                 
    <ol class="breadcrumb headingbar">
    <li class="breadcrumb-item active">Approve</li>
    <!--<li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
        <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
        <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>-->
    </ol>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table bg-mint display" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="เอกสาร">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Document_No") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="Link_DocumentID" runat="server" Text='<%# Bind("Document_No") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Document_Date" HeaderText="วันที่จัดทำ">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Area" HeaderText="Area">
                        <ItemStyle Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cluster" HeaderText="Cluster">
                        <ItemStyle Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Project_Name" HeaderText="Project Name">
                        <ItemStyle Width="35%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name">
                        <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateBy" HeaderText="จัดทำโดย">
                        <ItemStyle Width="25%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                        <asp:LinkButton Id="flow_data" class="text-danger" runat="server"
                            CommandName="Order" 
                            CommandArgument='<%# Eval("Document_No") %>' 
                            OnCommand="LinkButton_Command" ToolTip="รายละเอียดการอนุมัติ">
                            <i class="fas fa-lg fa-info-circle"></i>
                        </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
                <FooterStyle BackColor="#FFFFCC" ForeColor="#000FFF" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#000FFF" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#000FFF" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            
            <div style="text-align:center">
                <asp:Label ID="lblNoData" runat="server" Text="-- ไม่พบ Project ที่ค้างรอการอนุมัติ --" Width="100%" Font-Bold="true"  Visible="false"></asp:Label>
            </div>
            
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
                                <tr align="center">
                                    <th style="width: 5%;">#</th>
                                    <th style="width: 5%;">Step</th>
                                    <!--<th style="width: 5%;">Next</th>-->
                                    <th style="width: 10%;">ส่วนงาน</th>
                                    <th style="width: 10%;">สถานะ</th>
                                    <th style="width: 10%;">วัน-เวลา</th>
                                    <th style="width: 10%;">ผู้ดำเนินการ</th>
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
            
<script type="text/javascript">
    // $(function () {
    $('.display').dataTable({
        bLengthChange: false,
        "order": [[0, "desc"]],
        "columnDefs": [
           { type: "date-uk", targets: 1 }
       ],
       // lengthMenu: [[5, 10, -1], [5, 10, "All"]],
        bFilter: false,
        bSort: true,
        bPaginate: false
    });
   // });
</script>
</asp:Content>    