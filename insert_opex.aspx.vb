Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class insert_opex
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
            query.SetDropDownList(ddlGroup, "select distinct Main_Group from OPEX order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            query.SetDropDownList(ddlSubGroup, "select distinct Sub_Group from OPEX order by Sub_Group ", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            Table_Opex()
            'searchauto.Visible = "false"
        End If

    End Sub

    Protected Sub Table_Opex()
        Dim DT_List As DataTable
        Dim Sql_Opex As String = "SELECT [OPEX_id], [OPEX_Code], [OPEX_Name], [OPEX_Cost], [Main_Group], [Sub_Group] "
        Sql_Opex +="FROM [OPEX] Where 1=1 "
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Opex += "and Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If

        If ddlSubGroup.SelectedIndex <> 0 Then
            Sql_Opex += "and Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
        End If
        Sql_Opex += "ORDER BY [OPEX_id] DESC "
        DT_List = query.GetDataTable(Sql_Opex)
        'Response.write(Sql_Opex)
        ViewState("dt") = DT_List
        GridView2.DataSource = DT_List
        GridView2.DataBind()

    End Sub

    Protected Sub insertsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles insertsubmit.Click
        Dim opex_code As String = opexcode.Value()
        Dim opex_name As String = opexname.Value()
        Dim main_group As String = maingroup.Value()
        Dim sub_group As String = subgroup.Value()
        Dim opex_cost As String = opexcost.Value()
        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        Try
            If opex_code.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OPEX Code', function() { focus; });", True)
            ElseIf opex_name.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OPEX Name', function() { focus; });", True)
            ElseIf main_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf sub_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf IsNumeric(opex_cost) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ       OPEX Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " insert into OPEX(OPEX_Code,OPEX_Name,Main_Group,Sub_Group,OPEX_Cost,Create_By,Create_Date,Update_By,Update_Date) "
                vSqlIn += " values('" + opex_code.Trim + "','" + opex_name.Trim + "','" + main_group.Trim + "','" + sub_group.Trim + "','" + opex_cost + "','" + user + "',GETDATE(),'" + user + "',GETDATE() ) "
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('บันทึกข้อมูลสำเร็จ', function() { focus;window.location.href='insert_opex.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('บันทึกข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView2.PageIndex = e.NewPageIndex
        Page_Opex()
    End Sub

    Protected Sub Edit(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#updateopex').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        'opexid_update.Visible = False
        opexid_update.Text = row.Cells(0).Text
        opexcode_update.Text = row.Cells(1).Text.Replace("&nbsp;", "").Trim
        opexname_update.Text = row.Cells(2).Text.Replace("&nbsp;", "").Trim
        maingroup_update.Text = row.Cells(3).Text.Replace("&nbsp;", "").Trim
        subgroup_update.Text = row.Cells(4).Text.Replace("&nbsp;", "").Trim
        opexcost_update.Text = row.Cells(5).Text.Replace("&nbsp;", "").Trim

    End Sub


    Protected Sub update_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Update.Click

        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            If opexcode_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OPEX Code', function() { focus; });", True)
            ElseIf opexname_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OPEX Name', function() { focus; });", True)
            ElseIf maingroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf subgroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf IsNumeric(opexcost_update.Text) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OPEX Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " Update OPEX "
                vSqlIn += " Set OPEX_Code = '" + opexcode_update.Text.Trim + "', "
                vSqlIn += " OPEX_Name = '" + opexname_update.Text.Trim + "', "
                vSqlIn += " Main_Group = '" + maingroup_update.Text.Trim + "', "
                vSqlIn += " Sub_Group = '" + subgroup_update.Text.Trim + "', "
                vSqlIn += " OPEX_Cost = '" + opexcost_update.Text.Trim + "', "
                vSqlIn += " Update_By = '" + user + "', "
                vSqlIn += " Update_Date = GETDATE() "
                vSqlIn += " Where OPEX_Id =" + opexid_update.Text + " "

                'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('อัพเดตข้อมูลสำเร็จ', function() { focus;window.location.href='insert_opex.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('อัพเดตข้อมูลไม่สำเร็จ" + vSqlIn + "', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub


    Protected Sub Delete_Data(ByVal sender As Object, ByVal e As EventArgs)
        opexid_delete.Visible = False
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#deleteopex').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        msg_delete.Text = "คุณต้องการลบ Item " & row.Cells(1).Text & " หรือไม่"
        opexid_delete.Text = row.Cells(0).Text

    End Sub

    Protected Sub delete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click

        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            vSqlIn += " Delete From OPEX "
            vSqlIn += " Where OPEX_Id =" + opexid_delete.Text + " "

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('ลบข้อมูลสำเร็จ');", True)
            Table_Opex()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('ลบข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub searchauto_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchauto.Click

        Dim DT_Search As DataTable
        Dim strSql As String
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('aa" + tbAuto.Text + "');", True)
        strSql = "SELECT [OPEX_id], [OPEX_Code], [OPEX_Name], [Main_Group], [Sub_Group], [OPEX_Cost] FROM [OPEX] Where OPEX_Name like '%" + tbAuto.Text + "%' " + vbCr
        If ddlGroup.SelectedIndex <> 0 Then
            strSql += "and Main_Group = '" & ddlGroup.SelectedValue & "' " + vbCr
        End If
        If ddlSubGroup.SelectedIndex <> 0 Then
            strSql += "and Sub_Group = '" & ddlSubGroup.SelectedValue & "' " + vbCr
        End If
        strSql += "ORDER BY [OPEX_id] DESC "
        DT_Search = query.GetDataTable(strSql)
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

    Protected Sub Page_Opex()
        Dim dt As DataTable = ViewState.Item("dt")
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dropdown_Opex()
        Table_Opex()
    End Sub
    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Table_Opex()
    End Sub

    Protected Sub Dropdown_Opex()
        Dim Sql_Opex As String = "select distinct Sub_Group from OPEX "
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Opex += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If
        Sql_Opex += "order by Sub_Group "
        query.SetDropDownList(ddlSubGroup, Sql_Opex, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
    End Sub

    Protected Sub UploadOpexSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UploadOpexSubmit.Click
        Dim file_ext As String = ""
        Dim DT As New DataTable
        file_ext = Path.GetExtension(FileUploadOpex.FileName)
        If FileUploadOpex.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ File Upload!', function() { focus; });", True)

            FileUploadOpex.Focus()
        ElseIf file_ext <> ".xls" And file_ext <> ".xlsx" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์นามสกุลนี้ไม่สามารถ Upload ได้ ผู้ใช้งานสามารถ Upload ได้เฉพาะไฟล์ Excel เท่านั้น!', function() { focus; });", True)
            FileUploadOpex.Focus()
        Else
            Try
                Dim FileName As String
                Dim nFileUploadName As String
                FileName = up.rUploadFile(FileUploadOpex, "OPEX")
                nFileUploadName = Replace(Replace(FileName, ".xlsx", ""), ".xls", "")

                If CheckFileUploadPOS(FileName) Then
                    Dim strsql As String
                    strsql = "Insert into OPEX(OPEX_Code,OPEX_Name,Main_Group,Sub_Group,OPEX_Cost,File_Name,Create_By,Create_Date,Update_By,Update_Date) " + vbCr
                    strsql += "Select Item_Code, Item_Name, Main_Group, Sub_Group, Cost, File_Name, Create_By,Create_Date, Create_By 'Update_By',Create_Date 'Update_Date' " + vbCr
                    strsql += "From SettingData_UploadFile where File_Name = '" & nFileUploadName & "' "

                    query.ExecuteNonQuery(strsql)
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('Uplod File ข้อมูลสำเร็จ', function() { focus;window.location.href='insert_opex.aspx?menu=insert'; });", True)
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
            If Path.GetExtension(FileUploadOpex.FileName) = ".csv" Then
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
            If Path.GetExtension(FileUploadOpex.FileName) = ".csv" Then
                ShowData2GridView1(nFileUploadName)
            Else
                ShowData2GridView1(DropDownList1.SelectedItem.Text.Trim())
            End If

            For i As Integer = 0 To GridViewDataUpload.Rows.Count - 1
                If GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "&nbsp;" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim.Length <> 0 Then
                    strsql = "select * from OPEX where OPEX_Code = '" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "' "
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
                        ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('" & Replace(ex.Message, "'", "") & "', function() { focus; });", True)
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
