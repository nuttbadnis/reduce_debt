<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="edit_management.aspx.vb" Inherits="edit_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

            <ol class="breadcrumb headingbar">
            <li class="breadcrumb-item"><a id="menu_project_name" runat="server">Project Overview</a></li>
            <li class="breadcrumb-item"><a id="menu_service" runat="server">Service</a></li>
            <li class="breadcrumb-item"><a id="menu_capex" runat="server">Capex</a></li>
            <li class="breadcrumb-item"><a id="menu_opex" runat="server">Opex</a></li>
            <li class="breadcrumb-item"><a id="menu_other" runat="server">Other</a></li>
            <li class="breadcrumb-item active"><u>Management</u></li>
            <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>
            </ol>
    
            <div class="col-md-12">
                <div class="form-inline form-group">
                    <asp:DropDownList ID="ddlGroup" runat="server" class="form-control mr-2" AutoPostBack="true" Width="15%">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control mr-2" AutoPostBack="true" Width="15%">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control mr-2" Width="50%">
                    </asp:DropDownList>
                    <div class="input-group mr-2" style="width:10%;">
                        <span class="input-group-addon mr-2"><asp:Label ID="Label1" runat="server" Text="จำนวน"></asp:Label></span>
                            <asp:TextBox ID="TextBox4" runat="server" class="form-control">1</asp:TextBox>
                        </div>
                    <asp:Button ID="Button1" runat="server" Text="เลือก" class="btn btn-success bg-mint"/>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-inline form-group">              
                    <asp:CheckBox ID="chkOther" Text=" อื่นๆ" runat="server" AutoPostBack="true" CssClass="form-check mr-1" />
                    <asp:TextBox ID="txtOther" class="form-control mr-1 mb-2" runat="server" Enabled="False"  Width="85%"></asp:TextBox>
                    <asp:Button ID="btnAddOther" runat="server" Text="+"  class="btn btn-success bg-mint mb-2"/>
                </div>
            </div>
            <div class="col-md-12">
                <!--<table class="table" style="margin-top: -2%;">
                <tr>
                    <td>-->
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table bg-mint" EnableModelValidation="True" OnRowDeleting="GridView1_RowDeleting" ShowFooter="True">
                <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" font-size="14px" />
                <Columns>
                    <asp:TemplateField HeaderText="VAS &amp; Cost (per Month)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtManagementName" runat="server" Text='<%# Bind("Management_Name")%>' CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ราคาต่อหน่วย (THB)" ItemStyle-width="15%" HeaderStyle-Width = "15%" FooterStyle-Width = "15%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCostPerUnit" runat="server" Text='<%# Bind("Cost_perUnit") %>' CssClass="form-control text-right" OnTextChanged="txtCostPerUnit_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="จำนวน" ItemStyle-width="5%" HeaderStyle-Width = "5%" FooterStyle-Width = "5%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("Number") %>' CssClass="form-control text-right" OnTextChanged="txtNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ราคารวม (THB)" ItemStyle-width="12%" HeaderStyle-Width = "12%" FooterStyle-Width = "12%">
                        <ItemTemplate>
                            <asp:Label ID="lblCost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id_List") %>' Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" ItemStyle-width="4%" HeaderStyle-Width = "4%" FooterStyle-Width = "4%">
                        <ItemStyle Width="4%" />
                        <ItemTemplate>
                            <asp:Linkbutton ID="cmdDelete" runat="server" CssClass="btn btn-danger" 
                                CommandName="Delete" OnClientClick="return confirmDelete(this,'ต้องการลบรายการนี้ใช่หรือไม่');"
                                Text="<span class='fas fa-times'></span>" />               
                        </ItemTemplate>
                    </asp:TemplateField>  
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#000FFF" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#000FFF" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#000FFF" />
                <HeaderStyle Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <%--<asp:Button ID="Button3" runat="server" Text="Next" class="btn btn-success pull-right"/>--%>
            <%--<asp:Button ID="Button4" runat="server" Text="Back" class="btn btn-success pull-right"/>--%>
            <!--</td>
            </tr>
            </table>-->
        
        </div>
        <hr style="border-color: #DDD;" />
        <asp:Button ID="btnNext" runat="server" Text="ถัดไป" class="btn btn-success float-right bg-mint" />
        <asp:Button ID="Button2" runat="server" Text="บันทึก" class="btn btn-success bg-mint float-right mr-2"/>

   <script type="text/javascript">
    </script>        
</asp:Content>    