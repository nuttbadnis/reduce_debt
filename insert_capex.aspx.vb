Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlButton
Imports System.Data.OleDb


Partial Class insert_capex
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

            query.SetDropDownList(ddlGroup, "select distinct Main_Group from CAPEX order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            query.SetDropDownList(ddlSubGroup, "select distinct Sub_Group from CAPEX order by Sub_Group ", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            Table_Capex()
            'searchauto.Visible = "false"
        End If
        'Response.Redirect("insertcapexjson.aspx?qrs=updateStockShopNote")
    End Sub

    Protected Sub Table_Capex()
        Dim DT_List As DataTable
        Dim Sql_Capex As String = "SELECT [CAPEX_id], [Item_Code], [CAPEX_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] "
        Sql_Capex +="FROM [CAPEX] Where 1=1 "
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Capex += "and Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If

        If ddlSubGroup.SelectedIndex <> 0 Then
            Sql_Capex += "and Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
        End If
        Sql_Capex += "ORDER BY [CAPEX_id] DESC "
        'Response.write(Sql_Capex)
        DT_List = query.GetDataTable(Sql_Capex)
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
        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        Try
            If item_code.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Item Code', function() { focus; });", True)
            ElseIf capex_name.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ CAPEX Name', function() { focus; });", True)
            ElseIf main_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf sub_group.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf asset_type.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Asset Type', function() { focus; });", True)
            ElseIf IsNumeric(equipment_cost) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Equipment Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " insert into CAPEX(Item_Code,CAPEX_Name,Main_Group,Sub_Group,Asset_Type,Equipment_Cost,Labor_Cost,Create_By,Create_Date,Update_By,Update_Date) "
                vSqlIn += " values('" + item_code.Trim + "','" + capex_name.Trim + "','" + main_group.Trim + "','" + sub_group.Trim + "','" + asset_type.Trim + "','" + equipment_cost + "','0','" + user + "',GETDATE(),'" + user + "',GETDATE()) "
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('บันทึกข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex.aspx?menu=insert'; });", True)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('บันทึกข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
    End Sub

    Protected Sub searchauto_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchauto.Click

        Dim DT_Search As DataTable

        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('aa" + tbAuto.Text + "');", True)
        If tbAuto.Text.Contains(" :: ") Then
            Dim vText() As String
            vText = Split(tbAuto.Text, " :: ")
            DT_Search = query.GetDataTable("SELECT [CAPEX_id], [Item_Code], [CAPEX_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] FROM [CAPEX] Where Item_Code like '%" + vText(0) + "%' and CAPEX_Name like '%" + vText(1) + "%'  ORDER BY [CAPEX_id] DESC")
        Else
            DT_Search = query.GetDataTable("SELECT [CAPEX_id], [Item_Code], [CAPEX_Name], [Main_Group], [Sub_Group], [Asset_Type], [Equipment_Cost], [Labor_Cost] FROM [CAPEX] Where Item_Code like '%" + tbAuto.Text + "%' or CAPEX_Name like '%" + tbAuto.Text + "%' or Main_Group like '%" + tbAuto.Text + "%' ORDER BY [CAPEX_id] DESC")
        End If

        ViewState("dt") = DT_Search
        GridView1.DataSource = DT_Search
        GridView1.DataBind()

    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Page_Capex()
    End Sub


    Protected Sub Edit(ByVal sender As Object, ByVal e As EventArgs)
        ClientScript.RegisterStartupScript(Page.GetType, "update_modal", "$('#updatecapex').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        capexid_update.Visible = False
        capexid_update.Text = row.Cells(0).Text
        itemcode_update.Text = row.Cells(1).Text.Replace("&nbsp;", "").Trim
        capexname_update.Text = row.Cells(2).Text.Replace("&nbsp;", "").Trim
        maingroup_update.Text = row.Cells(3).Text.Replace("&nbsp;", "").Trim
        subgroup_update.Text = row.Cells(4).Text.Replace("&nbsp;", "").Trim
        assettype_update.Text = row.Cells(5).Text.Replace("&nbsp;", "").Trim
        equipmentcost_update.Text = row.Cells(6).Text.Replace("&nbsp;", "").Trim
    End Sub


    Protected Sub update_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update.Click

        Dim user As String = username.Value()
        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            If itemcode_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Item Code', function() { focus; });", True)
            ElseIf capexname_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ CAPEX Name', function() { focus; });", True)
            ElseIf maingroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Main Group', function() { focus; });", True)
            ElseIf subgroup_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Sub Group', function() { focus; });", True)
            ElseIf assettype_update.Text.Trim = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Asset Type', function() { focus; });", True)
            ElseIf IsNumeric(equipmentcost_update.Text) = False Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('โปรดระบุ Equipment Cost เป็นตัวเลข', function() { focus; });", True)
            ElseIf user = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณา Login ระบบใหม่อีกครั้ง', function() { focus;window.location.href='index.aspx'; });", True)
            Else
                vSqlIn += " Update CAPEX "
                vSqlIn += " Set Item_Code = '" + itemcode_update.Text.Trim + "', CAPEX_Name = '" + capexname_update.Text.Trim + "', "
                vSqlIn += " Main_Group = '" + maingroup_update.Text.Trim + "', "
                vSqlIn += " Sub_Group = '" + subgroup_update.Text.Trim + "', "
                vSqlIn += " Asset_Type = '" + assettype_update.Text.Trim + "', "
                vSqlIn += " Equipment_Cost = '" + equipmentcost_update.Text + "', "
                vSqlIn += " Labor_Cost = '0', "
                vSqlIn += " Update_By = '" + user + "', "
                vSqlIn += " Update_Date = GETDATE() "
                vSqlIn += " Where CAPEX_Id =" + capexid_update.Text + " "

                'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
                query.ExecuteNonQuery(vSqlIn)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('อัพเดตข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex.aspx?menu=insert'; });", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('อัพเดตข้อมูลไม่สำเร็จ', function() { focus; });", True)
        End Try
        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub


    Protected Sub Delete_Data(ByVal sender As Object, ByVal e As EventArgs)
        capexid_delete.Visible = False
        ClientScript.RegisterStartupScript(Page.GetType, "", "$('#deletecapex').modal();", True)

        Dim row As GridViewRow = CType(CType(sender, ImageButton).Parent.Parent, GridViewRow)
        msg_delete.Text = "คุณต้องการลบ Item " & row.Cells(1).Text & " หรือไม่"
        capexid_delete.Text = row.Cells(0).Text
    End Sub

    Protected Sub delete_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click

        Dim vSqlIn As String = " "
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('DataKey: " & CAPEX_Id & "');", True)
        Try
            vSqlIn += " Delete From CAPEX "
            vSqlIn += " Where CAPEX_Id =" + capexid_delete.Text + " "

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + vSqlIn + "');", True)
            query.ExecuteNonQuery(vSqlIn)
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('ลบข้อมูลสำเร็จ');", True)
            Table_Capex()

            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ลบข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex.aspx?menu=insert'; });", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('ลบข้อมูลไม่สำเร็จ', function() { focus; });", True)
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
        Dropdown_Capex()
        Table_Capex()
    End Sub
    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Table_Capex()
    End Sub

    Protected Sub Dropdown_Capex()
        Dim Sql_Opex As String = "select distinct Sub_Group from CAPEX "
        If ddlGroup.SelectedIndex <> 0 Then
            Sql_Opex += "where Main_Group = '" + ddlGroup.SelectedValue + "' "
        End If
        Sql_Opex += "order by Sub_Group "
        query.SetDropDownList(ddlSubGroup, Sql_Opex, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
    End Sub

    Protected Sub UploadCapexSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UploadCapexSubmit.Click
        Dim file_ext As String = ""
        Dim DT As New DataTable
        file_ext = Path.GetExtension(FileUploadCapex.FileName)
        If FileUploadCapex.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ File Upload!', function() { focus; });", True)

            FileUploadCapex.Focus()
        ElseIf file_ext <> ".xls" And file_ext <> ".xlsx" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์นามสกุลนี้ไม่สามารถ Upload ได้ ผู้ใช้งานสามารถ Upload ได้เฉพาะไฟล์ Excel เท่านั้น!', function() { focus; });", True)
            FileUploadCapex.Focus()
        Else
            Try
                Dim FileName As String
                Dim nFileUploadName As String
                FileName = up.rUploadFile(FileUploadCapex, "CAPEX")
                nFileUploadName = Replace(Replace(FileName, ".xlsx", ""), ".xls", "")

                If CheckFileUploadPOS(FileName) Then
                    Dim strsql As String
                    strsql = "Insert into CAPEX(Item_Code,CAPEX_Name,Main_Group,Sub_Group,Asset_Type,Equipment_Cost,Labor_Cost, File_Name,Create_By,Create_Date,Update_By,Update_Date) " + vbCr
                    strsql += "Select Item_Code, Item_Name, Main_Group, Sub_Group, Asset_Type, Cost, '0', File_Name, Create_By,Create_Date, Create_By 'Update_By',Create_Date 'Update_Date' " + vbCr
                    strsql += "From SettingData_UploadFile where File_Name = '" & nFileUploadName & "' "

                    query.ExecuteNonQuery(strsql)
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('Uplod File ข้อมูลสำเร็จ', function() { focus;window.location.href='insert_capex.aspx?menu=insert'; });", True)
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
            If Path.GetExtension(FileUploadCapex.FileName) = ".csv" Then
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
            If Path.GetExtension(FileUploadCapex.FileName) = ".csv" Then
                ShowData2GridView1(nFileUploadName)
            Else
                ShowData2GridView1(DropDownList1.SelectedItem.Text.Trim())
            End If

            For i As Integer = 0 To GridViewDataUpload.Rows.Count - 1
                If GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim <> "&nbsp;" And GridViewDataUpload.Rows(i).Cells(0).Text.Trim.Length <> 0 Then
                    strsql = "select * from CAPEX where Item_Code = '" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "' "
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
                    If IsNumeric(GridViewDataUpload.Rows(i).Cells(5).Text) = False Then
                        ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ข้อมูลราคา ในไฟล์บรรทัดที่ " & i + 1 & " ไม่ใช่ตัวเลข โปรดระบุข้อมูลให้ถูกต้อง', function() { focus; });", True)
                        ClearSettingDataUpoload(FileName)
                        Return False
                        Exit Function
                    End If

                    Try
                        strsql1 = "insert into SettingData_UploadFile values('" & FileName & "','" & GridViewDataUpload.Rows(i).Cells(0).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(1).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(2).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(3).Text.Trim & "','" & GridViewDataUpload.Rows(i).Cells(4).Text.Trim & "'," & CDbl(GridViewDataUpload.Rows(i).Cells(5).Text.Trim) & "," & i + 1 & ",'" & username.Value() & "',getdate()) "
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
