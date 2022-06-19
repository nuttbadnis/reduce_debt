<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="add_capex.aspx.vb" Inherits="add_capex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ol class="breadcrumb headingbar">
        <li class="breadcrumb-item"><a href="project_name.aspx?menu=create">Project Overview</a></li>
        <li class="breadcrumb-item"><a href="add_service.aspx?menu=create">Service</a></li>
        <li class="breadcrumb-item active"><u>Capex</u></li>
        <li class="breadcrumb-item"><a href="add_opex.aspx?menu=create">Opex</a></li>
        <li class="breadcrumb-item"><a href="add_other.aspx?menu=create">Other</a></li>
        <li class="breadcrumb-item"><a href="add_management.aspx?menu=create">Management</a></li>
        <li class="breadcrumb-item"><a href="Summary.aspx?menu=create">Summary</a></li>
    </ol>
    <div class="col-md-12">
        <div class="form-inline form-group">
            <asp:Label ID="Label3" runat="server" Text="Corp." Font-Bold="true" Font-Underline="true" CssClass="mr-2"></asp:Label>
            <asp:DropDownList ID="ddlGroup" runat="server" class="form-control mr-2" AutoPostBack="true" Width="15%">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSubGroup" runat="server" class="form-control mr-2" AutoPostBack="true" Width="15%">
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control mr-2" Width="57%">
            </asp:DropDownList>
            <%--    <div class="input-group" style="width:15%;">
                    <span class="input-group-addon"><asp:Label ID="Label1" runat="server" Text="จำนวน"></asp:Label></span>
                    <asp:TextBox ID="TextBox4" Width="90%" runat="server" class="form-control">1</asp:TextBox>
                    </div>--%>
            <asp:Button ID="Button1" runat="server" Text="เลือก" class="btn btn-success bg-mint"/>
        </div>
    </div>
    <div class="col-md-12">
        <div class="form-inline">
            <asp:CheckBox ID="chkOther" Text=" อื่นๆ" runat="server" AutoPostBack="true" CssClass="form-check mr-1" />
            <asp:TextBox ID="txtOther" class="form-control mr-1 mb-2" runat="server" Enabled="False" Width="85%"></asp:TextBox>
            <asp:Button ID="btnAddOther" runat="server" Text="+"  class="btn btn-success bg-mint mb-2"/>
        </div>
    </div>
    <div class="col-md-12">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="None"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" class="table table-sm bg-mint" EnableModelValidation="True" OnRowDeleting="GridView1_RowDeleting">
            <RowStyle BackColor="WhiteSmoke" Font-Size="14px" />
            <Columns>
                <asp:TemplateField HeaderText="Investment Cost (Corp.)">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCapexName" runat="server" Text='<%# Bind("CAPEX_Name")%>' CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Width="32%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asset Type">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlAssetType" runat="server" CssClass="form-control" SelectedValue='<%# Bind("Asset_Type") %>'>
                            <asp:ListItem>เช่าใช้</asp:ListItem>
                            <asp:ListItem>เช่าซื้อ</asp:ListItem>
                            <asp:ListItem>แถม</asp:ListItem>
                            <asp:ListItem>ส่งมอบ</asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id_List") %>' Visible="False"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="11%" />
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
                <asp:TemplateField HeaderText="ราคารวม (THB)"  ItemStyle-width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblCost" runat="server" Text='<%# Bind("Cost") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Remark") %>' CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemStyle Width="2%" />
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CssClass="btn btn-danger"
                            CommandName="Delete" OnClientClick="return confirmDelete(this,'ต้องการลบข้อมูลอุปกรณ์นี้ใช่หรือไม่');"
                            Text="<span class='fas fa-times'></span>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#1ABC9C" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <hr style="border-color: #DDD;" />
    <div class="col-md-12">
        <asp:Button ID="btnNext" runat="server" Text="ถัดไป" class="btn btn-success float-right bg-mint" />
        <asp:Button ID="Button2" runat="server" Text="บันทึก" class="btn btn-success float-right bg-mint mr-2" />
    </div>
    <script type="text/javascript">
      
    </script>
    <%--                <div class="col-md-12">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Mass" Font-Bold="true" Font-Underline="true"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlGroupMass" runat="server" class="form-control" AutoPostBack="true" Width="20%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlItemMass" runat="server" class="form-control" Width="50%">
                            </asp:DropDownList>
                            <div class="input-group" style="width:15%;">
                                <span class="input-group-addon"><asp:Label ID="Label2" runat="server" Text="จำนวน"></asp:Label></span>
                                <asp:TextBox ID="txtNumberMass" Width="90%" runat="server" class="form-control">1</asp:TextBox>
                                
                            </div>
                            <asp:Button ID="btnSelectMass" runat="server" Text="เลือก" class="btn btn-success" />
                        </div>
                    </div>
                </div>

                <div class="col-md-11">
                      <div class="form-inline">
                            <asp:CheckBox ID="chkOtherMass" Text=" อื่นๆ" runat="server" AutoPostBack="true"/>
                            <asp:TextBox ID="txtOtherMass" class="form-control" runat="server" Enabled="False" Width="91.5%"></asp:TextBox>
                     </div>
               </div>--%>

    <%-- <div class="col-md-11">
             
                <!--<table class="table" style="margin-top: -5%;"> --> 
                            
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" GridLines="None"
                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table">
                                <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" />
                                <Columns>
                                    <asp:BoundField DataField="CAPEX_Mass_Name" HeaderText="Investment Cost (Mass)" >
                                        <ItemStyle Width="32%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Asset Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAssetTypeMass" runat="server" CssClass="form-control" SelectedValue='<%# Bind("Asset_Type") %>'>
                                                <asp:ListItem>เช่าใช้</asp:ListItem>
                                                <asp:ListItem>เช่าซื้อ</asp:ListItem>
                                                <asp:ListItem>แถม</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                            <asp:Label ID="lblIDMass" runat="server" Text='<%# Bind("id_List") %>' Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="11%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Cost_perUnit" HeaderText="ราคาต่อหน่วย (THB)" DataFormatString="{0:###,##0.00}" >
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitMass" runat="server" Text='<%# Bind("Number") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ราคารวม (THB)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCostMass" runat="server" Text='<%# Bind("Cost") %>' CssClass="form-control" Width="100px" AutoPostBack="true" OnTextChanged="txtCostMass_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarkMass" runat="server" Text='<%# Bind("Remark") %>' CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True"/>
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <PagerStyle BackColor="#1ABC9C" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <HeaderStyle BackColor="#1ABC9C" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            

                            <!--</table>-->
            </div>
  
            

       </div>--%>
</asp:Content>
