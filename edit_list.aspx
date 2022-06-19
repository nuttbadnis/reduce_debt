<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="edit_list.aspx.vb" Inherits="edit_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">            
            
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active">Edit</li><!--<li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
            <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>--></ol>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="None"
                 BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table bg-mint display compact" Width="100%">
                 <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
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
                    <asp:BoundField DataField="Document_Date" HeaderText="วันที่จัดทำ" >
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Area" HeaderText="Area">
                        <ItemStyle Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cluster" HeaderText="Cluster">
                        <ItemStyle Width="3%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Project_Name" HeaderText="Project Name">
                        <ItemStyle Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name">
                        <ItemStyle Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateBy" HeaderText="จัดทำโดย">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <PagerStyle BackColor="#1ABC9C" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            
            <div style="text-align:center">
                <asp:Label ID="lblNoData" runat="server" Text="-- ไม่พบ Project ที่อยู่ในสถานะแก้ไขข้อมูลได้ --" Width="100%" Font-Bold="true"  Visible="false"></asp:Label>
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
