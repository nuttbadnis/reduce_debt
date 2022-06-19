<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="project_contract.aspx.vb" Inherits="project_contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <div class="col-md-10 pull-right">    
            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item active">Project Contract</li><!--<li class="breadcrumb-item"><a id="menu_opex" runat="server">Edit Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Edit Service</a></li>
            <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>--></ol>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="เอกสาร">
                        <ItemTemplate>
                            <asp:HyperLink ID="Link_DocumentID" runat="server" Text='<%# Bind("Document_No") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Document_Date" HeaderText="วันที่จัดทำ">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Project_Name" HeaderText="Project Name">
                        <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Project_Code" HeaderText="Project Code">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name">
                        <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateBy" HeaderText="จัดทำโดย">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ไฟล์สัญญาแนบ">
                        <ItemTemplate>
                            <asp:HyperLink ID="Link_ContractFile" runat="server" Text=""></asp:HyperLink>
                            <asp:Label ID="lblContractFile" runat="server" Text='<%# Bind("Contract_File") %>' Visible="false"></asp:Label>
                            
                        </ItemTemplate>
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
                <FooterStyle BackColor="#FFFFCC" ForeColor="#000FFF" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#000FFF" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#000FFF" />
                <HeaderStyle BackColor="#1ABC9C" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            
        </div>
</asp:Content> 
