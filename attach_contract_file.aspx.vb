Imports System.Data
Imports System.IO
Imports System.Net
Partial Class attach_contract_file
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim xRequest_id

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        Dim dt_check As DataTable
        dt_check = C.GetDataTable("select * from FeasibilityDocument where Document_No='" + xRequest_id + "' and request_status = '100' and CreateBy = '" + Session("uemail") + "' ")
        If dt_check.Rows.Count > 0 Then
            lblDocNo.Text = "เลขที่เอกสาร " + dt_check.Rows(0).Item("Document_No")
        Else
            btnUpload.Visible = False
        End If
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If FileUploadContract.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ไฟล์สัญญา');", True)
            FileUploadContract.Focus()
        ElseIf Path.GetExtension(FileUploadContract.FileName) <> ".pdf" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไฟล์สัญญา ต้องเป็น PDF เท่านั้น');", True)
            FileUploadContract.Focus()
        Else
            SaveData()
        End If


    End Sub

    Public Sub SaveData()
        Dim FileNameContract As String = ""

        If FileUploadContract.HasFile = True Then
            Dim CurrentFileName As String
            Dim CurrentPath As String

            CurrentFileName = FileUploadContract.FileName
            CurrentPath = Request.PhysicalApplicationPath
            CurrentPath += "Upload\"
            CurrentPath += CurrentFileName
            FileUploadContract.SaveAs(CurrentPath)
            Dim file_extContract As String
            file_extContract = Path.GetExtension(FileUploadContract.FileName)
            Dim TheFileContract As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
            FileNameContract = "Contract-" + xRequest_id + "-" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extContract
            If TheFileContract.Exists Then
                If System.IO.File.Exists(MapPath(".") & "\Upload\Contract\" & FileNameContract) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่สามารถ Upload ไฟล์์ได้ เนื่องจากเคย Upload แล้ว');", True)
                Else
                    System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\Contract\" & FileNameContract)
                End If
            Else
                Throw New FileNotFoundException()
            End If
        Else
            FileNameContract = ""
        End If
        If FileNameContract = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ไฟล์สัญญา');", True)
            FileUploadContract.Focus()
            Exit Sub
        End If

        Dim strSql As String
        Dim DT_List As New DataTable

        strSql = "Update FeasibilityDocument set Contract_File = '" + FileNameContract.ToString + "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() where Document_No='" + xRequest_id + "' "
        C.ExecuteNonQuery(strSql)
        Response.Redirect("project_contract.aspx")
    End Sub
End Class
