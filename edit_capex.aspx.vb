Imports System.Data
Partial Class edit_capex
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim trow As New HtmlTableRow
    Dim tcell As New HtmlTableCell
    Dim table As New HtmlTable
    Dim xRequest_id
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        If Not Page.IsPostBack Then
            menu_project_name.HRef = "edit_project_name.aspx?request_id=" + xRequest_id
            menu_service.HRef = "edit_service.aspx?request_id=" + xRequest_id
            menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
            menu_other.HRef = "edit_other.aspx?request_id=" + xRequest_id
            menu_management.HRef = "edit_management.aspx?request_id=" + xRequest_id
            menu_summary.HRef = "edit_Summary.aspx?request_id=" + xRequest_id
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If

            Dim alert As String
            Dim dt_check As DataTable
            dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (/*request_status = 0 or request_status = 55 or*/ request_status = 110) and Document_No = '" + xRequest_id + "'")
            If dt_check.Rows.Count > 0 Then
                'If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "0" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                'If dt_check.Rows(0).Item("CreateBy") <> Session("uemail") Then
                '    Button1.Visible = False
                '    Button2.Visible = False
                'End If
                If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                    'Dim DT_List As DataTable
                    'Dim r As String = ""
                    'C.SetDropDownList(ddlGroup, "select distinct Main_Group from CAPEX order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
                    ''C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
                    'C.SetDropDownList(DropDownList1, "select CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
                    'C.ExecuteNonQuery("delete from tmpList_CAPEX where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
                    'C.ExecuteNonQuery("insert into tmpList_CAPEX select CAPEX_Name, Asset_Type, Cost, Remark, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy = '" + Session("uemail") + "' ")
                    'DT_List = C.GetDataTable("select * from tmpList_CAPEX where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
                    'GridView1.DataSource = DT_List
                    'GridView1.DataBind()


                    Dim DT_List As DataTable
                    Dim r As String = ""
                    C.SetDropDownList(ddlGroup, "select Main_Group from CAPEX union select Main_Group from CAPEX_Mass order by Main_Group", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
                    C.SetDropDownList(ddlSubGroup, "select Sub_Group from CAPEX union select Sub_Group from CAPEX_Mass order by Sub_Group", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
                    'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
                    C.SetDropDownList(DropDownList1, "select CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX union select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code from CAPEX_Mass", "CAPEX_Name", "CAPEX_Name")
                    C.ExecuteNonQuery("delete from tmpList_CAPEX where Document_No = '" + xRequest_id + "' ")
                    C.ExecuteNonQuery("insert into tmpList_CAPEX select CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' ")
                    DT_List = C.GetDataTable("select * from tmpList_CAPEX where Document_No = '" + xRequest_id + "' ")
                    GridView1.DataSource = DT_List
                    GridView1.DataBind()

                    'C.SetDropDownList(ddlGroupMass, "select distinct Main_Group from CAPEX_Mass order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
                    'C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass", "CAPEX_Mass_Name", "Item_Code")
                    'C.ExecuteNonQuery("delete from tmpList_CAPEX_Mass where Document_No = '" + xRequest_id + "' ")
                    'C.ExecuteNonQuery("insert into tmpList_CAPEX_Mass select CAPEX_Mass_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_CAPEX_Mass where Document_No = '" + xRequest_id + "' and Status = '1' ")
                    'DT_List = C.GetDataTable("select * from tmpList_CAPEX_Mass where Document_No = '" + xRequest_id + "' ")
                    'GridView2.DataSource = DT_List
                    'GridView2.DataBind()

                    txtOther.Visible = False
                    'txtOtherMass.Visible = False
                Else
                    Button1.Visible = False
                    Button2.Visible = False
                    alert = "AlertNotification('User ไม่มีสิทธิ์ ในการแก้ไขข้อมูลได้!',"
                    alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                    alert += "});"
                    ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
                End If

            Else
                Button1.Visible = False
                Button2.Visible = False
                alert = "AlertNotification('โปรเจค " & xRequest_id & " ไม่อยู่ในสถานะ ที่สามารถแก้ไขข้อมูลได้!',"
                alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                alert += "});"
                ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
            End If
            txtOther.Visible = False
            btnAddOther.Visible = False
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        SelectItemToGridview()
    End Sub

    Protected Sub btnAddOther_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOther.Click
        SelectItemToGridview()
    End Sub

    Public Sub SelectItemToGridview()
        Dim DT As DataTable
        Dim DT_List As DataTable
        Dim strSql As String
        Dim r As String = ""

        Dim sql_capex As String = "SELECT * FROM ("
        sql_capex += "select CAPEX_Name as 'CAPEX_Name', Item_Code, Main_Group, Asset_Type, Equipment_Cost from CAPEX "
        'sql_capex += "union "
        'sql_capex += "select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code, Main_Group, Asset_Type, Equipment_Cost from CAPEX_Mass "
        sql_capex += ") as capex_all "

        'If IsNumeric(TextBox4.Text) = True Then
        'TextBox4.Text = Math.Round(CDbl(TextBox4.Text), 0)
        If chkOther.Checked = True Then
            If Replace(txtOther.Text, " ", "") = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล อื่นๆ');focus();", True)
                txtOther.Focus()
            ElseIf txtOther.Text.Contains("'") = True Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูลอื่นๆ ได้"");focus();", True)
                txtOther.Focus()
            Else
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "',"
                    strSql += "Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "',"
                    strSql += "Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "',"
                    strSql += "Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "',"
                    strSql += "Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "',"
                    strSql += "Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "',"
                    strSql += "UpdateBy='" + Session("uemail") + "',"
                    strSql += "UpdateDate = getdate() "
                    strSql += "where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
                    C.ExecuteNonQuery(strSql)
                Next

                strSql = "insert into tmpList_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Cost, Initial_Cost_perUnit, CreateBy, CreateDate, Document_No)"
                strSql += "values('" + C.rpSpecialChars(txtOther.Text.ToString) + "','เช่าใช้','0.00','0.00','0.00','" + Session("uemail") + "',getdate(),'" + xRequest_id + "') "
                C.ExecuteNonQuery(strSql)
                DT_List = C.GetDataTable("select * from tmpList_CAPEX where Document_No = '" + xRequest_id + "' ")
                GridView1.DataSource = DT_List
                GridView1.DataBind()
                txtOther.Text = ""
            End If
        Else
            sql_capex += "where capex_all.CAPEX_Name='" + DropDownList1.SelectedValue + "' "
            'Response.Write(sql_capex)
            DT = C.GetDataTable(sql_capex)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "',"
                    strSql += "Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "',"
                    strSql += "Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "',"
                    strSql += "Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "',"
                    strSql += "Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "',"
                    strSql += "Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "',"
                    strSql += "UpdateBy='" + Session("uemail") + "',"
                    strSql += "UpdateDate = getdate() "
                    strSql += "where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
                    'Response.Write(strSql)
                    C.ExecuteNonQuery(strSql)
                Next

                strSql = "insert into tmpList_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Cost, Initial_Cost_perUnit, CreateBy, CreateDate, Document_No)"
                strSql += "values('" + C.rpSpecialChars(DT.Rows(0).Item("CAPEX_Name").ToString) + "',"
                strSql += "'" + DT.Rows(0).Item("Asset_Type").ToString + "',"
                strSql += "'" + DT.Rows(0).Item("Equipment_Cost").ToString + "',"
                strSql += "'" + (CDbl(DT.Rows(0).Item("Equipment_Cost")) * 1).ToString + "',"
                strSql += "'" + DT.Rows(0).Item("Equipment_Cost").ToString + "',"
                strSql += "'" + Session("uemail") + "',getdate(),'" + xRequest_id + "') "
                'Response.Write(strSql)
                C.ExecuteNonQuery(strSql)
                DT_List = C.GetDataTable("select * from tmpList_CAPEX where Document_No = '" + xRequest_id + "' ")

                GridView1.DataSource = DT_List
                GridView1.DataBind()
            End If

        End If

    End Sub

    'Protected Sub btnSelectMass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectMass.Click
    '    Dim DT As DataTable
    '    Dim DT_List As DataTable
    '    Dim strSql As String
    '    Dim r As String = ""

    '    If IsNumeric(txtNumberMass.Text) = True Then
    '        txtNumberMass.Text = Math.Round(CDbl(txtNumberMass.Text), 0)
    '        If chkOtherMass.Checked = True Then
    '            If Replace(txtOtherMass.Text, " ", "") = "" Then
    '                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล อื่นๆ');focus();", True)
    '                txtOtherMass.Focus()
    '            ElseIf txtOtherMass.Text.Contains("'") = True Then
    '                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูลอื่นๆ ได้"");focus();", True)
    '                txtOtherMass.Focus()
    '            Else
    '                For i As Integer = 0 To GridView2.Rows.Count - 1
    '                    strSql = "Update tmpList_CAPEX_Mass Set CAPEX_Mass_Name='" + GridView2.Rows(i).Cells(0).Text + "', Asset_Type='" + CType(GridView2.Rows(i).Cells(1).FindControl("ddlAssetTypeMass"), DropDownList).SelectedValue + "', Cost='" + Format(CDbl(CType(GridView2.Rows(i).Cells(4).FindControl("txtCostMass"), TextBox).Text), "#0.00") + "', Remark='" + CType(GridView2.Rows(i).Cells(5).FindControl("txtRemarkMass"), TextBox).Text + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView2.Rows(i).Cells(1).FindControl("lblIDMass"), Label).Text + "' "
    '                    C.ExecuteNonQuery(strSql)
    '                Next

    '                strSql = "insert into tmpList_CAPEX_Mass (CAPEX_Mass_Name, Asset_Type, Cost_perUnit, Number, Cost, CreateBy, CreateDate, Document_No) values('" + txtOtherMass.Text.ToString + "','เช่าใช้','0.00','" + txtNumberMass.Text + "','0.00','" + Session("uemail") + "',getdate(),'" + xRequest_id + "') "
    '                C.ExecuteNonQuery(strSql)
    '                DT_List = C.GetDataTable("select * from tmpList_CAPEX_Mass where Document_No = '" + xRequest_id + "' ")
    '                GridView2.DataSource = DT_List
    '                GridView2.DataBind()
    '                txtOtherMass.Text = ""
    '            End If
    '        Else
    '            DT = C.GetDataTable("select * from CAPEX_Mass where Item_Code='" + ddlItemMass.SelectedValue + "'")

    '            If DT.Rows.Count > 0 Then
    '                For i As Integer = 0 To GridView2.Rows.Count - 1
    '                    strSql = "Update tmpList_CAPEX_Mass Set CAPEX_Mass_Name='" + GridView2.Rows(i).Cells(0).Text + "', Asset_Type='" + CType(GridView2.Rows(i).Cells(1).FindControl("ddlAssetTypeMass"), DropDownList).SelectedValue + "', Cost='" + Format(CDbl(CType(GridView2.Rows(i).Cells(4).FindControl("txtCostMass"), TextBox).Text), "#0.00") + "', Remark='" + CType(GridView2.Rows(i).Cells(5).FindControl("txtRemarkMass"), TextBox).Text + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView2.Rows(i).Cells(1).FindControl("lblIDMass"), Label).Text + "' "
    '                    C.ExecuteNonQuery(strSql)
    '                Next

    '                strSql = "insert into tmpList_CAPEX_Mass (CAPEX_Mass_Name, Asset_Type, Cost_perUnit, Number, Cost, CreateBy, CreateDate, Document_No) values('" + DT.Rows(0).Item("CAPEX_Mass_Name").ToString + "','" + DT.Rows(0).Item("Asset_Type").ToString + "','" + DT.Rows(0).Item("Equipment_Cost").ToString + "','" + txtNumberMass.Text + "','" + (CDbl(DT.Rows(0).Item("Equipment_Cost")) * txtNumberMass.Text).ToString + "','" + Session("uemail") + "',getdate(),'" + xRequest_id + "') "
    '                C.ExecuteNonQuery(strSql)
    '                DT_List = C.GetDataTable("select * from tmpList_CAPEX_Mass where Document_No = '" + xRequest_id + "' ")
    '                GridView2.DataSource = DT_List
    '                GridView2.DataBind()
    '            End If

    '        End If
    '    Else
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุจำนวนเป็นตัวเลขจำนวนเต็มเท่านั้น!');", True)
    '        txtNumberMass.Focus()
    '    End If
    'End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            CType(e.Row.Cells(4).FindControl("lblCost"), Label).Text = Format(CDbl(CType(e.Row.Cells(4).FindControl("lblCost"), Label).Text), "#,##0.00")
            If Button2.Visible = False Then
                CType(e.Row.Cells(4).FindControl("cmdDelete"), LinkButton).Visible = False
            End If
        End If
    End Sub

    'Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
    '    End If
    'End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim DT_List As DataTable
        Dim id_list As String
        Dim strSql As String

        If Not CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text Is Nothing Then
            id_list = CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text
            strSql = "delete from tmpList_CAPEX where id_List = '" + id_list + "' "
            'Response.Write(strSql)
            C.ExecuteNonQuery(strSql)
        End If

        C.SetDropDownList(DropDownList1, "select CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX union select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code from CAPEX_Mass", "CAPEX_Name", "CAPEX_Name")
        DT_List = C.GetDataTable("select * from tmpList_CAPEX where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
        GridView1.DataSource = DT_List
        GridView1.DataBind()

    End Sub

    'Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
    '    Dim id_list As String
    '    Dim strSql As String

    '    For i As Integer = 0 To GridView2.Rows.Count - 1
    '        strSql = "Update tmpList_CAPEX_Mass Set CAPEX_Mass_Name='" + GridView2.Rows(i).Cells(0).Text + "', Asset_Type='" + CType(GridView2.Rows(i).Cells(1).FindControl("ddlAssetTypeMass"), DropDownList).SelectedValue + "', Cost='" + Format(CDbl(CType(GridView2.Rows(i).Cells(4).FindControl("txtCostMass"), TextBox).Text), "#0.00") + "', Remark='" + CType(GridView2.Rows(i).Cells(5).FindControl("txtRemarkMass"), TextBox).Text + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView2.Rows(i).Cells(1).FindControl("lblIDMass"), Label).Text + "' "
    '        C.ExecuteNonQuery(strSql)
    '    Next

    '    id_list = CType(GridView2.Rows(e.RowIndex).Cells(1).FindControl("lblIDMass"), Label).Text
    '    strSql = "delete from tmpList_CAPEX_Mass where id_List = '" + id_list + "' "
    '    C.ExecuteNonQuery(strSql)


    '    Dim DT_List As DataTable
    '    Dim r As String = ""
    '    C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass", "CAPEX_Mass_Name", "Item_Code")
    '    DT_List = C.GetDataTable("select * from tmpList_CAPEX_Mass where Document_No = '" + xRequest_id + "' ")
    '    GridView2.DataSource = DT_List
    '    GridView2.DataBind()

    'End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    Dim strSql As String
    '    Dim i As Integer
    '    If GridView1.Rows.Count > 0 Then
    '        For i = 0 To GridView1.Rows.Count - 1
    '            If Not IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("TextBox3"), TextBox).Text) Then
    '                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกราคาเป็นตัวเลขเท่านั้น');focus();", True)
    '                CType(GridView1.Rows(i).Cells(2).FindControl("TextBox3"), TextBox).Focus()
    '                Exit Sub
    '            End If
    '            If CType(GridView1.Rows(i).Cells(3).FindControl("TextBox4"), TextBox).Text.Contains("'") = True Then
    '                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกหมายเหตุได้"");focus();", True)
    '                CType(GridView1.Rows(i).Cells(3).FindControl("TextBox4"), TextBox).Focus()
    '                Exit Sub
    '            End If
    '        Next
    '        For i = 0 To GridView1.Rows.Count - 1
    '            strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + GridView1.Rows(i).Cells(0).Text + "', Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("TextBox3"), TextBox).Text), "#0.00") + "', Remark='" + CType(GridView1.Rows(i).Cells(3).FindControl("TextBox4"), TextBox).Text + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
    '            C.ExecuteNonQuery(strSql)
    '        Next
    '    End If
    '    C.ExecuteNonQuery("Update List_CAPEX set Status = 'A', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate()  where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy = '" + Session("uemail") + "' ")
    '    C.ExecuteNonQuery("insert into List_CAPEX (CAPEX_Name, Asset_Type, Cost, Remark, Status, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No) select CAPEX_Name, Asset_Type, Cost, Remark, '1', '" + Session("uemail") + "', getdate(), '" + Session("uemail") + "', getdate(), Document_No from tmpList_CAPEX where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
    '    strSql = "Update FeasibilityDocument set UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' "
    '    C.ExecuteNonQuery(strSql)
    '    Response.Redirect("edit_opex.aspx?request_id=" + Request.QueryString("request_id"))
    'End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        SaveData("Save")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        SaveData("Next")
    End Sub

    Private Sub SaveData(ByVal SaveOrNext As String)
        Dim strSql As String
        Dim i As Integer

        If GridView1.Rows.Count > 0 Then
            For i = 0 To GridView1.Rows.Count - 1
                If Replace(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text, " ", "") = "" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล Investment Cost');focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Focus()
                    Exit Sub
                End If
                If CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Investment Cost ได้"");focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Focus()
                    Exit Sub
                End If
                If Not IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกราคาเป็นตัวเลขเท่านั้น');focus();", True)
                    CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Focus()
                    Exit Sub
                End If
                If Not IsNumeric(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกจำนวนเป็นตัวเลขเท่านั้น');focus();", True)
                    CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Focus()
                    Exit Sub
                End If
                If CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกหมายเหตุได้"");focus();", True)
                    CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Focus()
                    Exit Sub
                End If
            Next
            For i = 0 To GridView1.Rows.Count - 1
                strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "', Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "', Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
                C.ExecuteNonQuery(strSql)
            Next
        End If
        Try
            C.ExecuteNonQuery("Update List_CAPEX set Status = 'A', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate()  where Document_No = '" + xRequest_id + "' and Status = '1' ")
            C.ExecuteNonQuery("insert into List_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No) select CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, '1', CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from tmpList_CAPEX where Document_No = '" + xRequest_id + "' ")

            strSql = "Update FeasibilityDocument set UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' "
            C.ExecuteNonQuery(strSql)
            If SaveOrNext = "Next" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'edit_opex.aspx?request_id=" & Request.QueryString("request_id") & "'; });", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ');", True)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('ไม่สามารถอัพเดทข้อมูลได้');", True)
        End Try

    End Sub

    Protected Sub chkOther_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther.CheckedChanged
        If chkOther.Checked = True Then
            ddlGroup.Enabled = False
            ddlSubGroup.Enabled = False
            DropDownList1.Enabled = False
            Button1.Enabled = False
            txtOther.Enabled = True
            txtOther.Visible = True
            btnAddOther.Visible = True
        Else
            ddlGroup.Enabled = True
            ddlSubGroup.Enabled = True
            DropDownList1.Enabled = True
            Button1.Enabled = True
            txtOther.Text = ""
            txtOther.Enabled = False
            txtOther.Visible = False
            btnAddOther.Visible = False
        End If
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dim sql_subgroup As String = "select Distinct Sub_Group from CAPEX "
        Dim sql_capex As String = "SELECT * FROM ("
        sql_capex += "select CAPEX_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX "
        sql_capex += "union "
        sql_capex += "select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX_Mass) "
        sql_capex += "as capex_all "
        If ddlGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_capex += "where capex_all.Main_Group = '" + ddlGroup.SelectedValue + "' "
            sql_subgroup += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            'sql_capex += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        End If

    End Sub

    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Dim sql_capex As String = "SELECT * FROM ("
        sql_capex += "select CAPEX_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX "
        sql_capex += "union "
        sql_capex += "select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX_Mass) "
        sql_capex += "as capex_all "
        If ddlSubGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_capex += "where capex_all.Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
            If ddlGroup.SelectedIndex <> 0 Then
                sql_capex += " and  capex_all.Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "AlertNotification('teststwhhere');focus();", True)
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "AlertNotification('teststalllll');focus();", True)
            If ddlGroup.SelectedIndex <> 0 Then
                sql_capex += "where capex_all.Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        End If
    End Sub

    'Protected Sub chkOtherMass_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOtherMass.CheckedChanged
    '    If chkOtherMass.Checked = True Then
    '        ddlGroupMass.Enabled = False
    '        ddlItemMass.Enabled = False
    '        txtOtherMass.Enabled = True
    '        txtOtherMass.Visible = True
    '    Else
    '        ddlGroupMass.Enabled = True
    '        ddlItemMass.Enabled = True
    '        txtOtherMass.Text = ""
    '        txtOtherMass.Enabled = False
    '        txtOtherMass.Visible = False
    '    End If
    'End Sub

    'Protected Sub ddlGroupMass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroupMass.SelectedIndexChanged
    '    If ddlGroup.SelectedIndex <> 0 Then
    '        C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Mass_Name", "Item_Code")
    '    Else
    '        C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass", "CAPEX_Mass_Name", "Item_Code")
    '    End If

    'End Sub

    Protected Sub txtCostPerUnit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) = False Then
                CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('โปรดระบุ ราคาต่อหน่วย เป็นตัวเลขเท่านั้น');", True)
                CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Focus()
            End If
            CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text = Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) * CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text), "#,##0.00")
        Next
    End Sub


    Protected Sub txtNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If IsNumeric(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text) = False Then
                CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('โปรดระบุ จำนวน เป็นตัวเลขเท่านั้น');", True)
                CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Focus()
            End If
            CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text = Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) * CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text), "#,##0.00")
        Next
    End Sub
End Class
