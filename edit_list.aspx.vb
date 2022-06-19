Imports System.Data
Partial Class edit_list
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            Dim DT_List As New DataTable
            If Session("Login_permission") = "user" Or Session("Login_permission") = "presale" Then
                'DT_List = C.GetDataTable("select Document_No, CONVERT(varchar(10),Document_Date,103) Document_Date, Area, Cluster, Customer_Name from FeasibilityDocument where request_status = 110 and CreateBy = '" + Session("uemail") + "' ")
                DT_List = C.GetDataTable("select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date, name.Area, name.Cluster, name.Project_Name, name.Customer_Name, doc.CreateBy  from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No where doc.request_status = 110 and doc.CreateBy = '" + Session("uemail") + "' order by doc.Document_No desc ")
                GridView1.DataSource = DT_List
                GridView1.DataBind()
            ElseIf Session("Login_permission") = "inspector" Then
                DT_List = C.GetDataTable("select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date, name.Area, name.Cluster, name.Project_Name, name.Customer_Name, doc.CreateBy  from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No where doc.request_status in (55) order by doc.Document_No desc ")
                GridView1.DataSource = DT_List
                GridView1.DataBind()
            ElseIf Session("Login_permission") = "administrator" Then
                DT_List = C.GetDataTable("select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date, name.Area, name.Cluster, name.Project_Name, name.Customer_Name, doc.CreateBy  from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No where doc.request_status in (110) order by doc.Document_No desc")
                GridView1.DataSource = DT_List
                GridView1.DataBind()
            End If
            If DT_List.Rows.Count > 0 Then
                lblNoData.Visible = False
                GridView1.UseAccessibleHeader = True
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                lblNoData.Visible = True
            End If

        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).NavigateUrl = "./edit_project_name.aspx?request_id=" + CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Text
            'CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Target = ""
        End If
    End Sub


End Class
