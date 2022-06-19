Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient


Partial Class insert_capex_mass
    Inherits System.Web.UI.Page
    Dim query As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            username.Value = Session("uemail")

            query.SetDropDownList(ddlGroup, "select distinct Main_Group from CAPEX_Mass order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            Table_Capex()
            'searchauto.Visible = "false"
        End If
        'Response.Redirect("insertcapexjson.aspx?qrs=updateStockShopNote")
    End Sub

    Protected Sub Table_Capex()
        Dim DT_List As DataTable

        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        If ddlGroup.SelectedIndex <> 0 Then
            DT_List = query.GetDataTable("SELECT [CAPEX_Mass_id], [Item_Code], [CAPEX_Mass_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] FROM [CAPEX_Mass] WHERE Main_Group = '" + ddlGroup.SelectedValue + "' ORDER BY [CAPEX_Mass_id] DESC")
        Else
            DT_List = query.GetDataTable("SELECT [CAPEX_Mass_id], [Item_Code], [CAPEX_Mass_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] FROM [CAPEX_Mass] ORDER BY [CAPEX_Mass_id] DESC")
        End If

        ViewState("dt") = DT_List
        GridView1.DataSource = DT_List
        GridView1.DataBind()
    End Sub

    Protected Sub insertsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles insertsubmit.Click
        Dim item_code As String = itemcode.Value()
        Dim capex_name As String = capexname.Value()
        Dim main_group As String = maingroup.Value()
        Dim sub_group As String = subgroup.Value()
        Dim asset_type As String = assettype.Value()
        Dim equipment_cost As String = equipmentcost.Value()
        Dim labor_cost As String = laborcost.Value()
        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        Try
            vSqlIn += " insert into CAPEX_Mass(Item_Code,CAPEX_Mass_Name,Main_Group,Sub_Group,Asset_Type,Equipment_Cost,Labor_Cost,Create_By,Create_Date) "
            vSqlIn += " values('" + item_code + "','" + capex_name + "','" + main_group + "','" + sub_group + "','" + asset_type + "','" + equipment_cost + "','" + labor_cost + "','" + user + "',GETDATE() ) "
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('บันทึกข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex_mass.aspx?menu=insert'; });", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('บันทึกข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
    End Sub

    Protected Sub searchauto_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchauto.Click

        Dim DT_Search As DataTable

        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('aa" + tbAuto.Text + "');", True)
        If tbAuto.Text.Contains(" :: ") Then
            Dim vText() As String
            vText = Split(tbAuto.Text, " :: ")
            DT_Search = query.GetDataTable("SELECT [CAPEX_Mass_id], [Item_Code], [CAPEX_Mass_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] FROM [CAPEX_Mass] Where Item_Code like '%" + vText(0) + "%' and CAPEX_Mass_Name like '%" + vText(1) + "%'  ORDER BY [CAPEX_Mass_id] DESC")
        Else
            DT_Search = query.GetDataTable("SELECT [CAPEX_Mass_id], [Item_Code], [CAPEX_Mass_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] FROM [CAPEX_Mass] Where Item_Code like '%" + tbAuto.Text + "%' or CAPEX_Mass_Name like '%" + tbAuto.Text + "%' or Main_Group like '%" + tbAuto.Text + "%' ORDER BY [CAPEX_Mass_id] DESC")
        End If

        ViewState("dt") = DT_Search
        GridView1.DataSource = DT_Search
        GridView1.DataBind()

    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Page_Capex()
    End Sub


    Protected Sub Edit(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#updatecapex').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        capexid_update.Visible = False
        capexid_update.Text = row.Cells(0).Text
        itemcode_update.Text = row.Cells(1).Text.Replace("&nbsp;", "")
        capexname_update.Text = row.Cells(2).Text.Replace("&nbsp;", "")
        maingroup_update.Text = row.Cells(3).Text.Replace("&nbsp;", "")
        subgroup_update.Text = row.Cells(4).Text.Replace("&nbsp;", "")
        assettype_update.Text = row.Cells(5).Text.Replace("&nbsp;", "")
        equipmentcost_update.Text = row.Cells(6).Text.Replace("&nbsp;", "")
        laborcost_update.Text = row.Cells(7).Text.Replace("&nbsp;", "")

    End Sub


    Protected Sub update_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update.Click

        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            vSqlIn += " Update CAPEX_Mass "
            vSqlIn += " Set Item_Code = '" + itemcode_update.Text + "', CAPEX_Mass_Name = '" + capexname_update.Text + "', "
            vSqlIn += " Main_Group = '" + maingroup_update.Text + "', "
            vSqlIn += " Sub_Group = '" + subgroup_update.Text + "', "
            vSqlIn += " Asset_Type = '" + assettype_update.Text + "', "
            vSqlIn += " Equipment_Cost = '" + equipmentcost_update.Text + "', "
            vSqlIn += " Labor_Cost = '" + laborcost_update.Text + "', "
            vSqlIn += " Update_By = '" + user + "', "
            vSqlIn += " Update_Date = GETDATE() "
            vSqlIn += " Where CAPEX_Mass_Id =" + capexid_update.Text + " "

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('อัพเดตข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex_mass.aspx?menu=insert'; });", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('อัพเดตข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub


    Protected Sub Delete_Data(ByVal sender As Object, ByVal e As EventArgs)

        capexid_delete.Visible = False
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#deletecapex').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)

        capexid_delete.Text = row.Cells(0).Text
    End Sub

    Protected Sub delete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click


        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            vSqlIn += " Delete From CAPEX_Mass "
            vSqlIn += " Where CAPEX_Mass_Id =" + capexid_delete.Text + " "

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ลบข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex_mass.aspx?menu=insert'; });", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ลบข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub GridView1_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Sorted

        If lblSort.Text = "Ascending" Then
            lblSort.Text = "Descending"
        ElseIf lblSort.Text = "Descending" Then
            lblSort.Text = "Ascending"
        Else
            lblSort.Text = "Ascending"
        End If

    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim dt As DataTable = ViewState.Item("dt")
        Dim dv As DataView = dt.DefaultView
        Dim sd As String = ""

        If Not dt Is Nothing Or Not dt Is "" Then
            If lblSort.Text = "Ascending" Then
                sd = "desc"
            ElseIf lblSort.Text = "Descending" Then
                sd = "asc"
            Else
                sd = "asc"
            End If
        End If

        Try
            dv.Sort = e.SortExpression + " " + sd
            dt = dv.ToTable
            ViewState("dt") = dt
            GridView1.DataSource = dt
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Capex()
        Dim dt As DataTable = ViewState.Item("dt")
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Table_Capex()
    End Sub
End Class
