Imports System.Data

Partial Class add_management
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim DT_List As DataTable
            C.SetDropDownList(ddlGroup, "select Distinct Main_Group from Management order by Main_Group", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            C.SetDropDownList(ddlSubGroup, "select Distinct Sub_Group from Management order by Sub_Group", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, "select Management_Name as 'Management_Name', Management_Code from Management", "Management_Name", "Management_Name")
            C.ExecuteNonQuery("delete from tmpList_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            C.ExecuteNonQuery("insert into tmpList_Management select Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            DT_List = C.GetDataTable("select * from tmpList_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            If DT_List.Rows.Count = 0 Then
                AddEmptyGridview()
            Else
                GridView1.DataSource = DT_List
                GridView1.DataBind()
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
                        strSql = "Update tmpList_Management Set Management_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
                        C.ExecuteNonQuery(strSql)
                    Next

                    strSql = "insert into tmpList_Management (Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate) values('" + C.rpSpecialChars(txtOther.Text.ToString) + "','0.00','" + TextBox4.Text + "','0.00','0.00','" + Session("uemail") + "',getdate()) "
                    C.ExecuteNonQuery(strSql)
                    DT_List = C.GetDataTable("select * from tmpList_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
                    GridView1.DataSource = DT_List
                    GridView1.DataBind()
                    txtOther.Text = ""
                End If
            Else
                DT = C.GetDataTable("select * from Management where Management_Name='" + DropDownList1.SelectedItem.Text + "'")
                If DT.Rows.Count > 0 Then
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        strSql = "Update tmpList_Management Set Management_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
                        C.ExecuteNonQuery(strSql)
                    Next

                    strSql = "insert into tmpList_Management (Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate) values('" + C.rpSpecialChars(DT.Rows(0).Item("Management_Name").ToString) + "','" + DT.Rows(0).Item("Management_Cost").ToString + "','" + TextBox4.Text + "','" + (CDbl(DT.Rows(0).Item("Management_Cost")) * TextBox4.Text).ToString + "','" + DT.Rows(0).Item("Management_Cost").ToString + "','" + Session("uemail") + "',getdate()) "
                    C.ExecuteNonQuery(strSql)
                    DT_List = C.GetDataTable("select * from tmpList_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
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
        Dim DT_List As DataTable
        Dim id_list As String
        Dim strSql As String

        For i As Integer = 0 To GridView1.Rows.Count - 1
            strSql = "Update tmpList_Management Set Management_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
            C.ExecuteNonQuery(strSql)
        Next

        If Not CType(GridView1.Rows(e.RowIndex).Cells(3).FindControl("lblID"), Label).Text Is Nothing Then
            id_list = CType(GridView1.Rows(e.RowIndex).Cells(3).FindControl("lblID"), Label).Text
            strSql = "delete from tmpList_Management where id_List = '" + id_list + "' "
            C.ExecuteNonQuery(strSql)
        End If

        C.SetDropDownList(DropDownList1, "select Management_Name as 'Management_Name', Management_Code from Management", "Management_Name", "Management_Name")
        DT_List = C.GetDataTable("select * from tmpList_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
        If DT_List.Rows.Count = 0 Then
            AddEmptyGridview()
        Else
            GridView1.DataSource = DT_List
            GridView1.DataBind()
        End If
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
                If Replace(CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Text, " ", "") = "" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล VAS & Cost');focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Focus()
                    Exit Sub
                End If
                If CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล VAS & Cost ได้"");focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Focus()
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
                strSql = "Update tmpList_Management Set Management_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtManagementName"), TextBox).Text) + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(1).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number='" + CType(GridView1.Rows(i).Cells(2).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("lblCost"), Label).Text), "#0.00") + "', CreateBy='" + Session("uemail") + "', CreateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(3).FindControl("lblID"), Label).Text + "' "
                C.ExecuteNonQuery(strSql)
            Next
        End If
        C.ExecuteNonQuery("delete from List_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
        Try
            C.ExecuteNonQuery("insert into List_Management (Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No) select Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, '1', CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from tmpList_Management where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            If SaveOrNext = "Next" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('บันทึกข้อมูลได้สำเร็จ',function(){ window.location = 'Summary.aspx?menu=create'; });", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('บันทึกข้อมูลได้สำเร็จ');", True)
            End If
            'Response.Redirect("Summary.aspx")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('ไม่สามารถบันทึกข้อมูลได้');", True)
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
        Dim sql_subgroup As String = "select Distinct Sub_Group from Management "
        Dim sql_Management As String = "SELECT Management_Name as 'Management_Name', Management_Code from Management "
        If ddlGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_Management += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
            sql_subgroup += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            'sql_capex += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_Management, "Management_Name", "Management_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_Management, "Management_Name", "Management_Name")
        End If

    End Sub

    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Dim sql_Management As String = "SELECT Management_Name as 'Management_Name', Management_Code from Management "
        If ddlSubGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_Management += "where Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
            If ddlGroup.SelectedIndex <> 0 Then
                sql_Management += " and Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "AlertNotification('teststwhhere');focus();", True)
            C.SetDropDownList(DropDownList1, sql_Management, "Management_Name", "Management_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "AlertNotification('teststalllll');focus();", True)
            If ddlGroup.SelectedIndex <> 0 Then
                sql_Management += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            C.SetDropDownList(DropDownList1, sql_Management, "Management_Name", "Management_Name")
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
            If (Not e.Row.DataItem Is Nothing) AndAlso (CInt(e.Row.DataItem("id_List")) = -1) Then
                e.Row.Visible = False
            Else
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                CType(e.Row.Cells(3).FindControl("lblCost"), Label).Text = Format(CDbl(CType(e.Row.Cells(3).FindControl("lblCost"), Label).Text), "#,##0.00")
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            Dim strsql As String
            Dim DT_Service As New DataTable
            Dim NOCCost As Double
            Dim NOC As Double
            Dim NOCTotalCost As Double
            strSql = "select * from List_Service where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
            DT_Service = C.GetDataTable(strsql)
            If DT_Service.Rows.Count > 0 Then
                NOCCost = CDbl(DT_Service.Rows(0).Item("NOCCost"))
                NOC = CDbl(DT_Service.Rows(0).Item("NOC"))
                NOCTotalCost = CDbl(DT_Service.Rows(0).Item("NOC")) * CDbl(DT_Service.Rows(0).Item("NOCCost"))
            Else
                NOCCost = 0
                NOC = 0
                NOCTotalCost = 0
            End If
            e.Row.Cells(0).Text = "ค่าบริการ NOC"
            e.Row.Cells(1).Text = Format(NOCCost, "###,##0.00")
            e.Row.Cells(2).Text = Format(NOC, "###,##0")
            e.Row.Cells(3).Text = Format(NOCTotalCost, "###,##0.00")
            e.Row.Cells(4).Text = "   "

            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Private Sub AddEmptyGridview()
        Dim dt As New DataTable()
        dt.Columns.Add("Management_Name")
        dt.Columns.Add("Cost_perUnit")
        dt.Columns.Add("Number")
        dt.Columns.Add("Cost")
        dt.Columns.Add("id_List")

        Dim dr As DataRow = dt.NewRow()
        dr(0) = "0"
        dr(1) = "0"
        dr(2) = "0"
        dr(3) = "0"
        dr(4) = "-1"
        dt.Rows.Add(dr)

        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

End Class
