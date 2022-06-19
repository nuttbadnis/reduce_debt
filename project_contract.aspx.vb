Imports System.Data
Partial Class project_contract
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            Dim DT_List As DataTable
            Dim strsql As String
            If Session("Login_permission") = "inspector" Or Session("Login_permission") = "administrator" Or Session("Login_permission") = "user" Then
                strsql = "select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date, name.Area " + vbCr
                strsql += ", name.Cluster, name.Project_Name, isnull(name.Project_Code,'') 'Project_Code', name.Customer_Name, doc.CreateBy, isnull(doc.Contract_File,'') 'Contract_File' " + vbCr
                strsql += "from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No " + vbCr
                strsql += "where doc.request_status = '100' and doc.CreateBy = '" + Session("uemail") + "' " + vbCr
                strsql += "order by doc.Document_No desc "
                DT_List = C.GetDataTable(strsql)
                GridView1.DataSource = DT_List
                GridView1.DataBind()
            End If

        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).NavigateUrl = "~/Approve.aspx?request_id=" + CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Text
            If CType(e.Row.Cells(6).FindControl("lblContractFile"), Label).Text = "" Then
                CType(e.Row.Cells(6).FindControl("Link_ContractFile"), HyperLink).Text = "¬—ß‰¡Ë·π∫‰ø≈Ï"
                CType(e.Row.Cells(6).FindControl("Link_ContractFile"), HyperLink).NavigateUrl = "~/attach_contract_file.aspx?request_id=" + CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Text
            Else
                CType(e.Row.Cells(6).FindControl("Link_ContractFile"), HyperLink).Text = "Open File"
                CType(e.Row.Cells(6).FindControl("Link_ContractFile"), HyperLink).Target = "_Blank"
                CType(e.Row.Cells(6).FindControl("Link_ContractFile"), HyperLink).NavigateUrl = "~/Upload/Contract/" + CType(e.Row.Cells(0).FindControl("lblContractFile"), Label).Text
            End If
        End If
    End Sub
End Class
