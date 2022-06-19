Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class insert_other
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
            
            query.SetDropDownList(ddlGroup, "select distinct Main_Group from OTHER order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            query.SetDropDownList(ddlSubGroup, "select distinct Sub_Group from OTHER order by Sub_Group ", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            Table_Other()
            'searchauto.Visible = "false"
        End If

    End Sub

    Protected Sub Table_Other()
        Dim DT_List As DataTable
        Dim Sql_Other As String = "SELECT [OTHER_id], [OTHER_Code], [OTHER_Name], [Main_Group], [Sub_Group], [OTHER_Cost] "
        Sql_Other +="FROM [OTHER] Where 1=1 "
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Other += "and Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If

        If ddlSubGroup.SelectedIndex <> 0 Then
            Sql_Other += "and Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
        End If
        Sql_Other += "ORDER BY [OTHER_id] DESC "

        DT_List = query.GetDataTable(Sql_Other)
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        ViewState("dt") = DT_List
        GridView2.DataSource = DT_List
        GridView2.DataBind()

    End Sub

    Protected Sub insertsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles insertsubmit.Click

        Dim other_code As String = othercode.Value()
        Dim other_name As String = othername.Value()
        Dim main_group As String = maingroup.Value()
        Dim sub_group As String = subgroup.Value()
        Dim other_cost As String = othercost.Value()
        
        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        Try
            If other_code.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OTHER Code', function() { focus; });", True)
            ElseIf other_name.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OTHER Name', function() { focus; });", True)
            ElseIf main_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf sub_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf IsNumeric(other_cost) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ  OTHER Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " insert into OTHER(OTHER_Code,OTHER_Name,Main_Group,Sub_Group,OTHER_Cost,Create_By,Create_Date) "
                vSqlIn += " values('" + other_code.Trim + "','" + other_name.Trim + "','" + main_group.Trim + "','" + sub_group.Trim + "','" + other_cost + "','" + user + "',GETDATE() ) "
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('บันทึกข้อมูลสำเร็จ', function() { focus;window.location.href='insert_other.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('บันทึกข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView2.PageIndex = e.NewPageIndex
        Page_Other()
    End Sub

    Protected Sub Edit(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#updateother').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        'otherid_update.Visible = False

        otherid_update.Text = row.Cells(0).Text
        othercode_update.Text = row.Cells(1).Text.Replace("&nbsp;", "").Trim
        othername_update.Text = row.Cells(2).Text.Replace("&nbsp;", "").Trim
        maingroup_update.Text = row.Cells(3).Text.Replace("&nbsp;", "").Trim
        subgroup_update.Text = row.Cells(4).Text.Replace("&nbsp;", "").Trim
        othercost_update.Text = row.Cells(5).Text.Replace("&nbsp;", "").Trim

    End Sub


    Protected Sub update_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Update.Click

        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            If othercode_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OTHER Code', function() { focus; });", True)
            ElseIf othername_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OTHER Name', function() { focus; });", True)
            ElseIf maingroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf subgroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf IsNumeric(othercost_update.Text) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ OTHER Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " Update OTHER "
                vSqlIn += " Set OTHER_Code = '" + othercode_update.Text.Trim + "', OTHER_Name = '" + othername_update.Text.Trim + "', "
                vSqlIn += " OTHER_Cost = '" + othercost_update.Text.Trim + "', "
                vSqlIn += " Main_Group = '" + maingroup_update.Text.Trim + "', "
                vSqlIn += " Sub_Group = '" + subgroup_update.Text.Trim + "', "
                vSqlIn += " Update_By = '" + user + "', "
                vSqlIn += " Update_Date = GETDATE() "
                vSqlIn += " Where OTHER_Id =" + otherid_update.Text + " "

                'Response.write(vSqlIn)
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('อัพเดตข้อมูลสำเร็จ', function() { focus;window.location.href='insert_other.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('อัพเดตข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub


    Protected Sub Delete_Data(ByVal sender As Object, ByVal e As EventArgs)
        otherid_delete.Visible = False
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#deleteother').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        msg_delete.Text = "คุณต้องการลบ Item " & row.Cells(1).Text & " หรือไม่"
        otherid_delete.Text = row.Cells(0).Text
    End Sub

    Protected Sub delete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click

        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            vSqlIn += " Delete From OTHER "
            vSqlIn += " Where OTHER_Id =" + otherid_delete.Text + " "

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('ลบข้อมูลสำเร็จ');", True)
            Table_Other()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('ลบข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Protected Sub searchauto_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchauto.Click
        Dim DT_Search As DataTable

        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('aa" + tbAuto.Text + "');", True)
        DT_Search = query.GetDataTable("SELECT [OTHER_id], [OTHER_Code], [OTHER_Name], [Main_Group], [Sub_Group], [OTHER_Cost] FROM [OTHER] Where OTHER_Name like '%" + tbAuto.Text + "%'  ORDER BY [OTHER_id] DESC")
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

    Protected Sub Page_Other()
        Dim dt As DataTable = ViewState.Item("dt")
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dropdown_Other()
        Table_Other()
    End Sub
    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Table_Other()
    End Sub
    Protected Sub Dropdown_Other()
        Dim Sql_Other As String = "select distinct Sub_Group from OTHER "
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Other += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If
        Sql_Other += "order by Sub_Group "
        query.SetDropDownList(ddlSubGroup, Sql_Other, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
    End Sub

    Protected Sub UploadOtherSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UploadOtherSubmit.Click
        Dim file_ext As String = ""
        Dim DT As New DataTable
        file_ext = Path.GetExtension(FileUploadOther.FileName)
        If FileUploadOther.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ File Upload!', function() { focus; });", True)

            FileUploadOther.Focus()
        ElseIf file_ext <> ".xls" And file_ext <> ".xlsx" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์นามสกุลนี้ไม่สามารถ Upload ได้ ผู้ใช้งานสามารถ Upload ได้เฉพาะไฟล์ Excel เท่านั้น!', function() { focus; });", True)
            FileUploadOther.Focus()
        Else
            Try
                Dim FileName As String
                Dim nFileUploadName As String
                FileName = up.rUploadFile(FileUploadOther, "OTHER")
                nFileUploadName = Replace(Replace(FileName, ".xlsx", ""), ".xls", "")

                If CheckFileUploadPOS(FileName) Then
                    Dim strsql As String
                    strsql = "Insert into OTHER(OTHER_Code,OTHER_Name,Main_Group,Sub_Group,OTHER_Cost,File_Name,Create_By,Create_Date,Update_By,Update_Date) " + vbCr
                    strsql += "Select Item_Code, Item_Name, Main_Group, Sub_Group, Cost, File_Name, Create_By,Create_Date, Create_By 'Update_By',Create_Date 'Update_Date' " + vbCr
                    strsql += "From SettingData_UploadFile where File_Name = '" & nFileUploadName & "' "

                    query.ExecuteNonQuery(strsql)
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('Uplod File ข้อมูลสำเร็จ', function() { focus;window.location.href='insert_other.aspx?menu=insert'; });", True)
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
            If Path.GetExtension(FileUploadOther.FileName) = ".csv" Then
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
            If Path.GetExtension(FileUploadOther.FileName) = ".csv" Then
                ShowData2GridView1(nFileUploadName)
            Else
                ShowData2GridView1(DropDownList1.SelectedItem.Text.Trim())
            End If

            For i As Integer = 0 To GridViewDataUpload.Rows.Count - 1
                If GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "&nbsp;" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim.Length <> 0 Then
                    strsql = "select * from OTHER where OTHER_Code = '" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "' "
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

