Imports System.Data
Partial Class approve_list
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            Dim DT_List As DataTable
            If Session("Login_permission") = "inspector" Or Session("Login_permission") = "administrator" Or Session("Login_permission") = "approve" Or Session("Login_permission") = "approve_ro" Or Session("Login_permission") = "approve_cluster" Or Session("Login_permission") = "presale" Then
                DT_List = C.GetDataTable("select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date, name.Area, name.Cluster, name.Project_Name, name.Customer_Name, doc.CreateBy  from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No inner join request_flow rf on doc.Document_No = rf.request_id and doc.request_step = rf.flow_step where doc.request_status in ('0','50','55') and rf.disable = '0' and rf.send_uemail like '%" & Session("uemail") & "%' order by doc.Document_No desc ")
                If DT_List.Rows.Count > 0 Then
                    lblNoData.Visible = False
                    GridView1.DataSource = DT_List
                    GridView1.DataBind()
                    GridView1.UseAccessibleHeader = True
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
                Else
                    lblNoData.Visible = True
                End If
            End If
        Else
            If GridView1.Rows.Count > 0 Then
                GridView1.UseAccessibleHeader = True
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
            End If
        End If
    End Sub

        Protected Sub LinkButton_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        dim test as string 
        test = "You chose: " & e.CommandName & " Item " + e.CommandArgument
        loadDetail(e.CommandArgument)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "alert('ไม่สามารถบันทึกข้อมูลได้"+test+"');", True)
    End Sub

    Sub loadDetail(ByVal vRequest_id As String)
        Dim request_status As String
        Dim request_step As String
        Dim create_uemail As String
        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from FeasibilityDocument " + vbCr
        strSql += "where Document_No = '" + vRequest_id + "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            create_uemail = DT.Rows(0).Item("CreateBy")
            request_status = DT.Rows(0).Item("request_status")
            request_step = DT.Rows(0).Item("request_step")
            loadFlow(vRequest_id, request_status, request_step,create_uemail)
        End If
        strSql = "select * from dbo.List_ProjectName where Document_No='" + vRequest_id + "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            TextHeaderDetail.InnerHtml = vRequest_id & " " & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ผู้เสนอโครงการ: " & DT.Rows(0).Item("Customer_Assistant_Name")
        End If
    End Sub

    Sub loadFlow(ByVal vRequest_id As String, ByVal vRequest_status As String, ByVal vRequest_step As String, ByVal create_uemail As String)
        Dim vRequest_permiss As Integer = CF.rViewDetail(vRequest_id, create_uemail)
        inn_flow.InnerHtml = CF.rLoadFlowBody_Detail(vRequest_id, vRequest_status, vRequest_step, vRequest_permiss, 1)
        ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "$('#myModal').modal('show');", True)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).NavigateUrl = "~/Approve.aspx?request_id=" + CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Text
            'CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Target = ""
        End If
    End Sub

End Class
