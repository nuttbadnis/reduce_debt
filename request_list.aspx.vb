Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlButton
Imports System.Data.OleDb


Partial Class request_list
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
            'username.Value = Session("uemail")
            Dim strSubjectId As String
            Dim strPermissionId As String
            strSubjectId = "select subject_id, subject_name from dbo.Subject where flow_id= 99 and disable = '0' order by subject_id "
            query.SetDropDownList(ddlSubjectId, strSubjectId, "subject_name", "subject_id", "--- เลือกประเภทคำขอ ---")
            strPermissionId = "select Permission_type, Permission_name from dbo.permission where status = '0' "
            query.SetDropDownList(ddlPermissionId, strPermissionId, "Permission_name", "Permission_type","--- เลือกสิทธิ์ในการใช้งาน ---")
            Table_Capex()
            'searchauto.Visible = "false"
        End If
        'Response.Redirect("insertcapexjson.aspx?qrs=updateStockShopNote")
    End Sub

    Protected Sub Table_Capex()
        Dim DT_List As DataTable
        Dim Sql_Capex As String = "SELECT R.[Request_Id], R.[Login_Name], R.[Full_Name], S.[subject_name] "
        Sql_Capex +=", R.[Create_By], R.[Create_Date], R.[Update_Date], RS.[status_name] "
        Sql_Capex +="FROM [Request] R "
        Sql_Capex +="Left join [Subject] S on S.subject_id = R.subject_id "
        Sql_Capex +="Left join [Request_Status] RS on RS.status_id = R.request_status "
        Sql_Capex +="Where 1=1 "
        'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('alldata');", True)
        If ddlSubjectId.SelectedIndex <> 0 Then
            Sql_Capex += "and R.subject_id = '" + ddlSubjectId.SelectedValue + "' "
        End If

        If ddlPermissionId.SelectedIndex <> 0 Then
            Sql_Capex += "and R.login_permission = '" + ddlPermissionId.SelectedValue + "' "
        End If
        Sql_Capex += "ORDER BY R.create_date DESC "
        'Response.write(Sql_Capex)
        DT_List = query.GetDataTable(Sql_Capex)
        ViewState("dt") = DT_List
        GridView1.DataSource = DT_List
        GridView1.DataBind()
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

End Class
