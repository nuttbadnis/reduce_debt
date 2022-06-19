Imports System.Data
Partial Class edit_other
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim xRequest_id

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        If Not Page.IsPostBack Then
            menu_project_name.HRef = "edit_project_name.aspx?request_id=" + xRequest_id
            menu_service.HRef = "edit_service.aspx?request_id=" + xRequest_id
            menu_capex.HRef = "edit_capex.aspx?request_id=" + xRequest_id
            menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
            menu_management.HRef = "edit_management.aspx?request_id=" + xRequest_id
            menu_summary.HRef = "edit_Summary.aspx?request_id=" + xRequest_id

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
                    Dim DT_List As DataTable
                    C.SetDropDownList(ddlGroup, "select Distinct Main_Group from OTHER order by Main_Group", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
                    C.SetDropDownList(ddlSubGroup, "select Distinct Sub_Group from OTHER order by Sub_Group", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
                    C.SetDropDownList(DropDownList1, "select OTHER_Name as 'OTHER_Name', OTHER_Code from OTHER", "OTHER_Name", "OTHER_Name")
                    C.ExecuteNonQuery("delete from tmpList_OTHER where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
                    C.ExecuteNonQuery("insert into tmpList_OTHER select OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy = '" + Session("uemail") + "'  ")
                    DT_List = C.GetDataTable("select * from tmpList_OTHER where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
                    GridView1.DataSource = DT_List
                    GridView1.DataBind()
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
        If IsNumeric(TextBox4.Text) = True Then

            TextBox4.Text = Math.Round(CDbl(TextBox4.Text), 0)
            If chkOther.Checked = True Then
                If Replace(txtOther.Text, " ", "") = "" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล อื่นๆ');focus();", True)
                    txtOther.Focus()
                ElseIf txtOther.Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูลอื่นๆ ได้"");focus();", True)
                    txtOther.Focus()
                Else
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        strSql = "Update tmpList_OTHER Set OTHER_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
                        C.ExecuteNonQuery(strSql)
                    Next

                    strSql = "insert into tmpList_OTHER (OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate, Document_No) values('" + C.rpSpecialChars(txtOther.Text.ToString) + "','0.00','" + TextBox4.Text + "','0.00','0.00','" + Session("uemail") + "',getdate(),'" + xRequest_id + "') "
                    C.ExecuteNonQuery(strSql)
                    DT_List = C.GetDataTable("select * from tmpList_OTHER where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
                    GridView1.DataSource = DT_List
                    GridView1.DataBind()
                    txtOther.Text = ""
                End If
            Else
                DT = C.GetDataTable("select * from OTHER where OTHER_Name='" + DropDownList1.SelectedItem.Text + "'")
                If DT.Rows.Count > 0 Then
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        strSql = "Update tmpList_OTHER Set OTHER_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
                        C.ExecuteNonQuery(strSql)
                    Next

                    strSql = "insert into tmpList_OTHER (OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate, Document_No) values('" + C.rpSpecialChars(DT.Rows(0).Item("OTHER_Name").ToString) + "','" + DT.Rows(0).Item("OTHER_Cost").ToString + "','" + TextBox4.Text + "','" + (CDbl(DT.Rows(0).Item("OTHER_Cost")) * TextBox4.Text).ToString + "','" + DT.Rows(0).Item("OTHER_Cost").ToString + "','" + Session("uemail") + "',getdate(),'" + xRequest_id + "') "
                    C.ExecuteNonQuery(strSql)
                    DT_List = C.GetDataTable("select * from tmpList_OTHER where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
                    GridView1.DataSource = DT_List
                    GridView1.DataBind()

                End If
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุจำนวนเป็นตัวเลขจำนวนเต็มเท่านั้น!');", True)
            TextBox4.Focus()
        End If

    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim id_list As String
        Dim strSql As String

        For i As Integer = 0 To GridView1.Rows.Count - 1
            strSql = "Update tmpList_OTHER Set OTHER_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
            C.ExecuteNonQuery(strSql)
        Next

        id_list = CType(GridView1.Rows(e.RowIndex).Cells(2).FindControl("lblID"), Label).Text
        strSql = "delete from tmpList_OTHER where id_List = '" + id_list + "' "
        C.ExecuteNonQuery(strSql)

        Dim DT_List As DataTable
        Dim r As String = ""
        'C.SetDropDownList(DropDownList1, "select OTHER_Name as 'OTHER_Name', OTHER_Code from OTHER", "OTHER_Name", "OTHER_Code")
        DT_List = C.GetDataTable("select * from tmpList_OTHER where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
        GridView1.DataSource = DT_List
        GridView1.DataBind()
    End Sub

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
                If Replace(CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Text, " ", "") = "" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล VAS & Cost');focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Focus()
                    Exit Sub
                End If
                If CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล VAS & Cost ได้"");focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Focus()
                    Exit Sub
                End If
                If Not IsNumeric(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกราคาเป็นตัวเลขเท่านั้น');focus();", True)
                    CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Focus()
                    Exit Sub
                End If
                If Not IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกจำนวนเป็นตัวเลขเท่านั้น');focus();", True)
                    CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Focus()
                    Exit Sub
                End If
            Next
            For i = 0 To GridView1.Rows.Count - 1
                strSql = "Update tmpList_OTHER Set OTHER_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtOtherName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', CreateBy='" + Session("uemail") + "', CreateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
                C.ExecuteNonQuery(strSql)
            Next
        End If
        Try
            'C.ExecuteNonQuery("delete from List_OTHER where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' ")
            C.ExecuteNonQuery("Update List_OTHER set Status = 'A', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy = '" + Session("uemail") + "' ")
            C.ExecuteNonQuery("insert into List_OTHER (OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No) select OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, '1', '" + Session("uemail") + "', getdate(), '" + Session("uemail") + "', getdate(), Document_No from tmpList_OTHER where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' ")
            strSql = "Update FeasibilityDocument set UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' "
            C.ExecuteNonQuery(strSql)
            If SaveOrNext = "Next" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'edit_management.aspx?request_id=" & Request.QueryString("request_id") & "'; });", True)
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
            'DropDownList1.Visible = False
        Else
            ddlGroup.Enabled = True
            ddlSubGroup.Enabled = True
            DropDownList1.Enabled = True
            Button1.Enabled = True
            txtOther.Text = ""
            txtOther.Enabled = False
            txtOther.Visible = False
            btnAddOther.Visible = False
            'DropDownList1.Visible = True
        End If
    End Sub

        Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dim sql_subgroup As String = "select Distinct Sub_Group from OTHER "
        Dim sql_other As String = "SELECT OTHER_Name as 'OTHER_Name', OTHER_Code from OTHER "
        If ddlGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_other += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
            sql_subgroup += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            'sql_capex += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_other, "OTHER_Name", "OTHER_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_other, "OTHER_Name", "OTHER_Name")
        End If

    End Sub

    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Dim sql_other As String = "SELECT OTHER_Name as 'OTHER_Name', OTHER_Code from OTHER "
        If ddlSubGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_other += "where Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
            If ddlGroup.SelectedIndex <> 0 Then
                sql_other += " and Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "bootbox.alert('teststwhhere');focus();", True)
            C.SetDropDownList(DropDownList1, sql_other, "OTHER_Name", "OTHER_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "bootbox.alert('teststalllll');focus();", True)
            If ddlGroup.SelectedIndex <> 0 Then
                sql_other += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            C.SetDropDownList(DropDownList1, sql_other, "OTHER_Name", "OTHER_Name")
        End If
    End Sub

    Protected Sub txtCostPerUnit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If IsNumeric(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text) = False Then
                CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('โปรดระบุ ราคาต่อหน่วย เป็นตัวเลขเท่านั้น');", True)
                CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Focus()
            End If
            CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text = Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text) * CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text), "#,##0.00")
        Next
    End Sub

    Protected Sub txtNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text) = False Then
                CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('โปรดระบุ จำนวน เป็นตัวเลขเท่านั้น');", True)
                CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Focus()
            End If
            CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text = Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text) * CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text), "#,##0.00")
        Next
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            CType(e.Row.Cells(3).FindControl("lblCost"), Label).Text = Format(CDbl(CType(e.Row.Cells(3).FindControl("lblCost"), Label).Text), "#,##0.00")
            If Button2.Visible = False Then
                CType(e.Row.Cells(4).FindControl("cmdDelete"), LinkButton).Visible = False
            End If
        End If
    End Sub
End Class
