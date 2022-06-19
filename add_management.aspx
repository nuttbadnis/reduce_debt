<%@ Page Language="VB" MasterPageFile="~/MasterPageMenu.master" AutoEventWireup="false" CodeFile="add_management.aspx.vb" Inherits="add_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

            <ol class="breadcrumb headingbar">
                <li class="breadcrumb-item"><a href="project_name.aspx?menu=create">Project Overview</a></li>
                <li class="breadcrumb-item"><a href="add_service.aspx?menu=create">Service</a></li>
                <li class="breadcrumb-item"><a href="add_capex.aspx?menu=create">Capex</a></li>
                <li class="breadcrumb-item"><a href="add_opex.aspx?menu=create">Opex</a></li>
                <li class="breadcrumb-item"><a href="add_other.aspx?menu=create">Other</a></li>
                <li class="breadcrumb-item active"><u>Management</u></li>
                <li class="breadcrumb-item"><a href="Summary.aspx?menu=create">Summary</a></li>
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
                <div class="form-inline">       
                    <asp:CheckBox ID="chkOther" Text=" อื่นๆ" runat="server" AutoPostBack="true" cssclass="form-check mr-1" />
                    <asp:TextBox ID="txtOther" class="form-control mr-1 mb-2"  runat="server" Enabled="False"  Width="85%"></asp:TextBox>
                    <asp:Button ID="btnAddOther" runat="server" Text="+"  class="btn btn-success bg-mint mb-2"/>
                </div>
            </div>

            <div class="col-md-12">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" class="table bg-mint" EnableModelValidation="True" OnRowDeleting="GridView1_RowDeleting" ShowFooter="True">
                <RowStyle BackColor="WhiteSmoke" ForeColor="#337AB7" font-size="14px" />
                <Columns>
                    <asp:TemplateField HeaderText="VAS &amp; Cost (per Month)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtManagementName" runat="server" Text='<%# Bind("Management_Name")%>' CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ราคาต่อหน่วย (THB)" HeaderStyle-HorizontalAlign = "Right" ItemStyle-width="15%" HeaderStyle-Width = "15%" FooterStyle-Width = "15%">
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
                            <asp:Linkbutton runat="server" CssClass="btn btn-danger" 
                                CommandName="Delete" OnClientClick="return confirmDelete(this,'ต้องการลบข้อมูลอุปกรณ์นี้ใช่หรือไม่');"
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
        <div class="col-md-12">
            <hr style="border-color: #DDD;" />
            <asp:Button ID="btnNext" runat="server" Text="ถัดไป" class="btn btn-success float-right bg-mint" />
            <asp:Button ID="Button2" runat="server" Text="บันทึก" class="btn btn-success bg-mint float-right mr-2"/>    
        </div> 
            
</asp:Content>    