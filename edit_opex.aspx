<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="edit_opex.aspx.vb" Inherits="edit_opex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

    <ol class="breadcrumb headingbar">
        <li class="breadcrumb-item"><a id="menu_project_name" runat="server">Project Overview</a></li>
        <li class="breadcrumb-item"><a id="menu_service" runat="server">Service</a></li>
        <li class="breadcrumb-item"><a id="menu_capex" runat="server">Capex</a></li>
        <li class="breadcrumb-item active"><u>Opex</u></li>
        <li class="breadcrumb-item"><a id="menu_other" runat="server">Other</a></li>
        <li class="breadcrumb-item"><a id="menu_management" runat="server">Management</a></li>
        <li class="breadcrumb-item"><a id="menu_summary" runat="server">Summary</a></li>
    </ol>
     
        <div class="col-md-12">
            <div class="form-inline form-group">
                <asp:DropDownList ID="ddlGroup" runat="server" class="form-control mr-2" AutoPostBack="true" Width="15%">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control mr-2" AutoPostBack="true" Width="15%">
                </asp:DropDownList>           
                <asp:DropDownList ID="DropDownList1" runat="server" cssclass="form-control mr-2" Width="50%">
                </asp:DropDownList>
                <div class="input-group mr-2" style="width:10%;">
                    <span class="input-group-addon mr-2"><asp:Label ID="Label1" runat="server" Text="จำนวน"></asp:Label></span>
                    <asp:TextBox ID="TextBox4" runat="server" class="form-control">1</asp:TextBox>
                 </div>  
                <asp:Button ID="Button1" runat="server" Text="เลือก" cssclass="btn btn-success bg-mint"/>
            </div>      
        </div>
        <div class="col-md-12">
            <div class="form-inline form-group"> 
                <asp:CheckBox ID="chkOther" Text=" อื่นๆ" runat="server" AutoPostBack="true" cssclass="form-check mr-1"/>
                <asp:TextBox ID="txtOther" class="form-control mr-1 mb-2"  runat="server" Enabled="False" Width="85%"></asp:TextBox>
                <asp:Button ID="btnAddOther" runat="server" Text="+"  class="btn btn-success bg-mint mb-2"/>
            </div>
        </div>
        <!--<table class="table" style="margin-top: -2%;">
            <tr>
            <td>-->
        <div class="col-md-12">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table bg-mint"
                EnableModelValidation="True" OnRowDeleting="GridView1_RowDeleting">
            <RowStyle BackColor="#f5f5f5" ForeColor="#337ab7" />
            <Columns>
                <%-- <asp:BoundField DataField="Cost_perUnit" HeaderText="ราคาต่อหน่วย (THB)" ItemStyle-Width="15%" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Number" HeaderText="จำนวน" ItemStyle-HorizontalAlign="Right" /> --%>
                <asp:TemplateField HeaderText="VAS &amp; Cost (per Month)">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOpexName" runat="server" Text='<%# Bind("OPEX_Name")%>' CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ราคาต่อหน่วย (THB)" ItemStyle-width="15%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCostPerUnit" runat="server" Text='<%# Bind("Cost_perUnit") %>' CssClass="form-control text-right" OnTextChanged="txtCostPerUnit_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="จำนวน" ItemStyle-width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("Number") %>' CssClass="form-control text-right" OnTextChanged="txtNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="ราคารวม (THB)" ItemStyle-width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblCost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id_List") %>' Visible="False"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="right" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                        <ItemStyle Width="2%" />
                        <ItemTemplate>
                            <asp:LinkButton runat="server"  ID="cmdDelete" CssClass="btn btn-danger"
                                CommandName="Delete" OnClientClick="return confirmDelete(this,'ต้องการลบข้อมูลอุปกรณ์นี้ใช่หรือไม่');"
                                Text="<span class='fas fa-times'></span>" />
                        </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#fff" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#fff" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#fff" />
                    <HeaderStyle  Font-Bold="True" ForeColor="white" />
            </asp:GridView>
        </div>
                <hr style="border-color: #DDD;" />
                <asp:Button ID="btnNext" runat="server" Text="ถัดไป" class="btn btn-success float-right bg-mint" />
                <asp:Button ID="Button2" runat="server" Text="บันทึก" class="btn btn-success bg-mint float-right mr-2"/>

                <%--<asp:Button ID="Button3" runat="server" Text="Next" class="btn btn-success pull-right"/>--%>
                <%--<asp:Button ID="Button4" runat="server" Text="Back" class="btn btn-success pull-right"/>--%>
                <!--</td>
            </tr>
        </table>-->
    <script type="text/javascript">
    </script>
</asp:Content>    

