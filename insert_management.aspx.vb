Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class insert_management
    Inherits System.Web.UI.Page
    Dim query As New Cls_Data
    Dim up As New Cls_UploadFile

    Private exconn As OleDbConnection
    Private dtex As DataTable = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            username.Value = Session("uemail")

            query.SetDropDownList(ddlGroup, "select distinct Main_Group from Management order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            query.SetDropDownList(ddlSubGroup, "select distinct Sub_Group from Management order by Sub_Group ", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            Table_Management()
            'searchauto.Visible = "false"
        End If

    End Sub

    Protected Sub Table_Management()
        Dim DT_List As DataTable
        Dim Sql_Management As String = "SELECT [Management_id], [Management_Code], [Management_Name], [Main_Group], [Sub_Group], [Management_Cost] "
        Sql_Management += "FROM [Management] Where 1 = 1 "
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Management += "and Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If

        If ddlSubGroup.SelectedIndex <> 0 Then
            Sql_Management += "and Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
        End If
        Sql_Management += "ORDER BY [Management_id] DESC "

        DT_List = query.GetDataTable(Sql_Management)
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        ViewState("dt") = DT_List
        GridView2.DataSource = DT_List
        GridView2.DataBind()

    End Sub

    Protected Sub insertsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles insertsubmit.Click

        Dim Management_code As String = Managementcode.Value()
        Dim Management_name As String = Managementname.Value()
        Dim main_group As String = maingroup.Value()
        Dim sub_group As String = subgroup.Value()
        Dim Management_cost As String = Managementcost.Value()

        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        Try
            If Management_code.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Management Code', function() { focus; });", True)
            ElseIf Management_name.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Management Name', function() { focus; });", True)
            ElseIf main_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf sub_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf IsNumeric(Management_cost) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ  Management Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " insert into Management(Management_Code,Management_Name,Main_Group,Sub_Group,Management_Cost,Create_By,Create_Date) "
                vSqlIn += " values('" + Management_code.Trim + "','" + Management_name.Trim + "','" + main_group.Trim + "','" + sub_group.Trim + "','" + Management_cost + "','" + user + "',GETDATE() ) "
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('บันทึกข้อมูลสำเร็จ', function() { focus;window.location.href='insert_management.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('บันทึกข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView2.PageIndex = e.NewPageIndex
        Page_Management()
    End Sub

    Protected Sub Edit(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#updateManagement').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        'Managementid_update.Visible = False

        Managementid_update.Text = row.Cells(0).Text
        Managementcode_update.Text = row.Cells(1).Text.Replace("&nbsp;", "").Trim
        Managementname_update.Text = row.Cells(2).Text.Replace("&nbsp;", "").Trim
        maingroup_update.Text = row.Cells(3).Text.Replace("&nbsp;", "").Trim
        subgroup_update.Text = row.Cells(4).Text.Replace("&nbsp;", "").Trim
        Managementcost_update.Text = row.Cells(5).Text.Replace("&nbsp;", "").Trim

    End Sub


    Protected Sub update_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Update.Click

        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            If Managementcode_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Management Code', function() { focus; });", True)
            ElseIf Managementname_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Management Name', function() { focus; });", True)
            ElseIf maingroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf subgroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf IsNumeric(Managementcost_update.Text) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Management Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " Update Management "
                vSqlIn += " Set Management_Code = '" + Managementcode_update.Text.Trim + "', Management_Name = '" + Managementname_update.Text.Trim + "', "
                vSqlIn += " Management_Cost = '" + Managementcost_update.Text.Trim + "', "
                vSqlIn += " Main_Group = '" + maingroup_update.Text.Trim + "', "
                vSqlIn += " Sub_Group = '" + subgroup_update.Text.Trim + "', "
                vSqlIn += " Update_By = '" + user + "', "
                vSqlIn += " Update_Date = GETDATE() "
                vSqlIn += " Where Management_Id =" + Managementid_update.Text + " "

                'Response.write(vSqlIn)
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('อัพเดตข้อมูลสำเร็จ', function() { focus;window.location.href='insert_management.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('อัพเดตข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub


    Protected Sub Delete_Data(ByVal sender As Object, ByVal e As EventArgs)
        Managementid_delete.Visible = False
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#deleteManagement').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        msg_delete.Text = "คุณต้องการลบ Item " & row.Cells(1).Text & " หรือไม่"
        Managementid_delete.Text = row.Cells(0).Text
    End Sub

    Protected Sub delete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click

        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            vSqlIn += " Delete From Management "
            vSqlIn += " Where Management_Id =" + Managementid_delete.Text + " "

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('ลบข้อมูลสำเร็จ');", True)
            Table_Management()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('ลบข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub searchauto_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchauto.Click
        Dim DT_Search As DataTable

        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('aa" + tbAuto.Text + "');", True)
        DT_Search = query.GetDataTable("SELECT [Management_id], [Management_Code], [Management_Name], [Main_Group], [Sub_Group], [Management_Cost] FROM [Management] Where Management_Name like '%" + tbAuto.Text + "%'  ORDER BY [Management_id] DESC")
        ViewState("dt") = DT_Search
        GridView2.DataSource = DT_Search
        GridView2.DataBind()
    End Sub

    Protected Sub GridView2_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.Sorted
        If lblSort.Text = "Ascending" Then
            lblSort.Text = "Descending"
        ElseIf lblSort.Text = "Descending" Then
            lblSort.Text = "Ascending"
        Else
            lblSort.Text = "Ascending"
        End If
    End Sub

    Protected Sub GridView2_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView2.Sorting
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
            GridView2.DataSource = dt
            GridView2.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Management()
        Dim dt As DataTable = ViewState.Item("dt")
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dropdown_Management()
        Table_Management()
    End Sub
    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Table_Management()
    End Sub
    Protected Sub Dropdown_Management()
        Dim Sql_Management As String = "select distinct Sub_Group from Management "
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Management += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If
        Sql_Management += "order by Sub_Group "
        query.SetDropDownList(ddlSubGroup, Sql_Management, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
    End Sub

    Protected Sub UploadManagementSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UploadManagementSubmit.Click
        Dim file_ext As String = ""
        Dim DT As New DataTable
        file_ext = Path.GetExtension(FileUploadManagement.FileName)
        If FileUploadManagement.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ File Upload!', function() { focus; });", True)

            FileUploadManagement.Focus()
        ElseIf file_ext <> ".xls" And file_ext <> ".xlsx" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์นามสกุลนี้ไม่สามารถ Upload ได้ ผู้ใช้งานสามารถ Upload ได้เฉพาะไฟล์ Excel เท่านั้น!', function() { focus; });", True)
            FileUploadManagement.Focus()
        Else
            Try
                Dim FileName As String
                Dim nFileUploadName As String
                FileName = up.rUploadFile(FileUploadManagement, "Management")
                nFileUploadName = Replace(Replace(FileName, ".xlsx", ""), ".xls", "")

                If CheckFileUploadPOS(FileName) Then
                    Dim strsql As String
                    strsql = "Insert into Management(Management_Code,Management_Name,Main_Group,Sub_Group,Management_Cost,File_Name,Create_By,Create_Date,Update_By,Update_Date) " + vbCr
                    strsql += "Select Item_Code, Item_Name, Main_Group, Sub_Group, Cost, File_Name, Create_By,Create_Date, Create_By 'Update_By',Create_Date 'Update_Date' " + vbCr
                    strsql += "From SettingData_UploadFile where File_Name = '" & nFileUploadName & "' "

                    query.ExecuteNonQuery(strsql)
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('Uplod File ข้อมูลสำเร็จ', function() { focus;window.location.href='insert_management.aspx?menu=insert'; });", True)
                End If
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('" & Replace(ex.Message, "'", "") & "', function() { focus; });", True)
            End Try
        End If
    End Sub

    Private Sub GetExcelSheetNames(ByVal f As String)
        Try
            'If Path.GetExtension(FileUpload1.FileName) = ".xls" Then
            ' Excel 2005
            'Me.Session("strConn") = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath("./CheckFile/") & f & ";Extended Properties=""Excel 8.0;"""
            If Path.GetExtension(FileUploadManagement.FileName) = ".csv" Then
                ' CSV
                Me.Session("strConn") = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("./Upload/SettingData/") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
            Else
                ' Excel 2007
                Me.Session("strConn") = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("./Upload/SettingData/") & f & ";Extended Properties=""Excel 12.0;"""
            End If

            exconn = New OleDbConnection(Me.Session("strConn").ToString())
            exconn.Open()
            dtex = exconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            If dtex IsNot Nothing Then
                If dtex.Rows.Count > 0 Then
                    DropDownList1.DataSource = dtex
                    DropDownList1.DataTextField = "TABLE_NAME"
                    DropDownList1.DataValueField = "TABLE_NAME"
                    DropDownList1.DataBind()
                    GridViewSheet.DataSource = dtex
                    GridViewSheet.DataBind()
                End If
            End If

        Catch ex As Exception
            'Label1.Text = Me.Session("strConn") 'ex.Message
        Finally
            If Not exconn Is Nothing Then
                exconn.Close()
                exconn.Dispose()
            End If
            If Not dtex Is Nothing Then
                dtex.Dispose()
            End If
        End Try
    End Sub
    Function getDataByWorksheet(ByVal worksSheetNames As String) As DataTable
        Dim ds As DataSet = New DataSet
        Try
            exconn = New OleDbConnection(Me.Session("strConn").ToString())
            exconn.Open()
            Dim da As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM [" + worksSheetNames + "]  ", exconn)
            da.Fill(ds)
        Catch ex As Exception
            'Me.Label1.Text = ex.Message & ""
        Finally
            If Not exconn Is Nothing Then
                exconn.Close()
                exconn.Dispose()
            End If
        End Try
        If ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function
    Protected Sub ShowData2GridView1(ByVal workSheet As String)
        GridViewDataUpload.DataSource = getDataByWorksheet(DropDownList1.SelectedValue.ToString())
        GridViewDataUpload.DataBind()
    End Sub

    Protected Sub GridViewDataUpload_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewDataUpload.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text.Trim.Length = 0 Or e.Row.Cells(0).Text.Trim = "&nbsp;" Then
                e.Row.Visible = False
            End If
        End If
    End Sub

    Function CheckFileUploadPOS(ByVal nFileUploadName As String) As Boolean
        Dim strsql As String = ""
        Dim strsql1 As String = ""
        Dim DT As New DataTable
        Dim FileName As String = Replace(Replace(nFileUploadName, ".xlsx", ""), ".xls", "").Trim
        Try
            GetExcelSheetNames(nFileUploadName)
            If Path.GetExtension(FileUploadManagement.FileName) = ".csv" Then
                ShowData2GridView1(nFileUploadName)
            Else
                ShowData2GridView1(DropDownList1.SelectedItem.Text.Trim())
            End If

            For i As Integer = 0 To GridViewDataUpload.Rows.Count - 1
                If GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "&nbsp;" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim.Length <> 0 Then
                    strsql = "select * from Management where Management_Code = '" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "' "
                    DT = query.GetDataTable(strsql)
                    If DT.Rows.Count > 0 Then
                        ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('Item " & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & " ในไฟล์บรรทัดที่ " & i + 1 & " ซ้ำกับฐานข้อมูล ไม่สามารถเพิ่มข้อมูลได้', function() { focus; });", True)
                        ClearSettingDataUpoload(FileName)
                        Return False
                        Exit Function
                    End If
                    strsql = "select * from SettingData_UploadFile where Item_Code = '" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "' and File_Name = '" & FileName & "' "
                    DT = query.GetDataTable(strsql)
                    If DT.Rows.Count > 0 Then
                        ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('พบ Item " & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & " ซ้ำกันอยู่ภายในไฟล์ ไม่สามารถเพิ่มข้อมูลได้', function() { focus; });", True)
                        ClearSettingDataUpoload(FileName)
                        Return False
                        Exit Function
                    End If
                    If IsNumeric(GridViewDataUpload.Rows(i).Cells(4).Text) = False Then
                        ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ข้อมูลราคา ในไฟล์บรรทัดที่ " & i + 1 & " ไม่ใช่ตัวเลข โปรดระบุข้อมูลให้ถูกต้อง', function() { focus; });", True)
                        ClearSettingDataUpoload(FileName)
                        Return False
                        Exit Function
                    End If

                    Try
                        strsql1 = "insert into SettingData_UploadFile values('" & FileName & "','" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(1).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(2).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(3).Text.Trim & "',''," & CDbl(GridViewDataUpload.Rows(i).Cells(4).Text.Trim) & "," & i + 1 & ",'" & username.Value() & "',getdate()) "
                        query.ExecuteNonQuery(strsql1)
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('" & Replace(ex.Message, "'", "") & "', function() { focus; });", True)
                        ClearSettingDataUpoload(FileName)
                        Return False
                        Exit Function
                    End Try
                End If
            Next
            'C.ExecuteNonQuery(strsql1)
            Return True
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('" & Replace(ex.Message, "'", "") & "', function() { focus; });", True)
            ClearSettingDataUpoload(FileName)
            Return False
        End Try
    End Function

    Public Sub ClearSettingDataUpoload(ByVal nFileName As String)
        Try
            Dim strsql As String = ""
            strsql = "delete from SettingData_UploadFile where File_Name = '" & nFileName & "'  "
            query.ExecuteNonQuery(strsql)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('" & Replace(ex.Message, "'", "") & "', function() { focus; });", True)
        End Try
    End Sub
End Class

