Imports System.IO
Imports System.Data.OleDb
Imports System.Data

Public Class Cls_UploadFile
    Inherits System.Web.UI.Page
    Private exconn As OleDbConnection
    Private dt As DataTable = Nothing


    Public Function rUploadFile(ByVal vFile As FileUpload, ByVal vPrefix As String) As String
        Dim vFileUpload As FileUpload = vFile 'HttpPostedFile = HttpContext.Current.Request.Files(vInput)
        Dim vFilePath As String = ""
        Dim vFileType As String
        Dim vFileName As String
        Dim vTempFile As String = ""

        If vFileUpload.FileName <> "" Then
            vFileName = System.IO.Path.GetFileName(vFileUpload.FileName)

            'If vFileName.Length >= 1 Then
            vTempFile = vFileName    ' FileName
            vFileType = System.IO.Path.GetExtension(vFileName)
            'End If

            vFilePath = vPrefix
            vFilePath += DateTime.Now.ToString("_yyMMdd_HHmmss")
            vFilePath += vFileType

            vFileUpload.SaveAs(Server.MapPath("Upload/SettingData/" & vTempFile))

            Dim TheFile As FileInfo = New FileInfo(Server.MapPath("Upload/SettingData/" & vTempFile))

            If TheFile.Exists Then
                System.IO.File.Move(Server.MapPath("Upload/SettingData/" & vTempFile), Server.MapPath("Upload/SettingData/" & vFilePath))  'Move File (à»ÅÕèÂ¹ª×èÍä¿Åì)
            Else
                vFilePath = ""

                Throw New FileNotFoundException()
            End If
        End If

        Return vFilePath
    End Function

    Public Function rUploadFileDriveF(ByVal vInput As String, ByVal vPrefix As String) As String
        Dim vFileUpload As HttpPostedFile = HttpContext.Current.Request.Files(vInput)
        Dim Rename AS String
        Dim vFileType As String
        Dim vFileName AS String
        Dim vTempFile As String = ""
        Dim vTempPath As String = Server.MapPath("Upload\")

        'Dim vFilePath As String = "F:\FollowRequestFile\upload" 
        Dim vUploadPath As String '= "F:\"
        'vUploadPath &= rGetPathParent() & "File\upload" 
        vUploadPath = "Contract"
        vUploadPath &= "\" & DateTime.Now.ToString("yyyy")
        vUploadPath &= "\" & DateTime.Now.ToString("MM")

        'Dim FileNameContract As String = ""
        'FileNameContract = "Contract_" + vPrefix + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00")

        IF Not Directory.Exists(vTempPath) then
            Directory.CreateDirectory(vTempPath)
        End IF

        If vFileUpload.FileName <> "" Then
            If Not Directory.Exists(vTempPath & vUploadPath) Then
                Directory.CreateDirectory(vTempPath & vUploadPath)
            End If

            vUploadPath &= "\"

            vFileName = System.IO.Path.GetFileName(vFileUpload.FileName)

            If vFileName.Length >= 1 Then
                vTempFile = vFileName    'FileName
                vFileType = System.IO.Path.GetExtension(vFileName)
            End If

            Rename = vUploadPath & "Contract_" & vPrefix
            Rename &= DateTime.Now.ToString("_yyyyMMddHHmmss")
            Rename &= vFileType

            vFileUpload.SaveAs(vTempPath & vTempFile)

            Dim TheFile As FileInfo = New FileInfo(vTempPath & vTempFile)
            If TheFile.Exists Then
                System.IO.File.Move(vTempPath & vTempFile, vTempPath & Rename)  'Move File
            Else
                Rename = ""

                Throw New FileNotFoundException()
            End If
        Else
            Rename = ""
        End If
  
        Return Rename
    End Function

    Public Function rUploadFileMutivalue(ByVal vInput As String, ByVal vPrefix As String) As String()
        Dim vFileUpload As HttpPostedFile = HttpContext.Current.Request.Files(vInput)
        Dim Rename AS String = ""
        Dim vFileType As String
        Dim vFileName AS String
        Dim vTempFile As String = ""

        'Dim vFilePath As String = "FollowRequestFile\upload" 
        Dim vFilePath As String = rGetPathParent() & "File\upload" 
        vFilePath &= "\" & DateTime.Now.ToString("yyyy")
        vFilePath &= "\" & DateTime.Now.ToString("MM")

        Dim vUploadPath As String = "F:\" & vFilePath

        IF Not Directory.Exists(vUploadPath) then
            Directory.CreateDirectory(vUploadPath)
        End IF

        vUploadPath &= "\"

        If vFileUpload.FileName <> "" Then
            vFileName = System.IO.Path.GetFileName(vFileUpload.FileName)

            If vFileName.Length >= 1 Then
                vTempFile = vFileName    ' FileName
                vFileType = System.IO.Path.GetExtension(vFileName) 
            End If

            Rename = vPrefix 
            Rename &= DateTime.Now.ToString("_yyMMdd_HHmmss")
            Rename &= vFileType

            vFileUpload.SaveAs(Server.MapPath("temp/" & vTempFile))

            'Dim TheFile As FileInfo = New FileInfo(MapPath(".") & "\" & "temp\" & vTempFile)
            Dim TheFile As FileInfo = New FileInfo(Server.MapPath("temp/" & vTempFile))
            If TheFile.Exists Then
                'System.IO.File.Move(MapPath(".") & "\" & "temp\" & vTempFile, vUploadPath & Rename)  'Move File
                System.IO.File.Move(Server.MapPath("temp/" & vTempFile), vUploadPath & Rename)  'Move File
            Else
                Rename = ""

                Throw New FileNotFoundException()
            End If
        End If

        'vFilePath = "http://posbcs.triplet.co.th/" & vFilePath.Replace("\","/") & "/"
        vFilePath = rGetHost & "/" & vFilePath.Replace("\","/") & "/"

        Return New String() {vUploadPath, vFilePath, Rename}
    End Function

    Public Function rGetPathParent() As String
        'example url = http://posbcs.triplet.co.th/FlowDemo/testupload.aspx
        'Request.Url.AbsolutePath = /FlowDemo/testupload.aspx
        'vPath(1) = FlowDemo

        Dim vPath As Array = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
        Return vPath(1)
    End Function

    Public Function rGetHost() As String
        Return HttpContext.Current.Request.Url.Host
    End Function

    'Function getDataByWorksheet(ByVal worksSheetNames As String) As DataTable
    '    Dim ds As DataSet = New DataSet
    '    Try
    '        exconn = New OleDbConnection(Me.Session("strConn").ToString())
    '        exconn.Open()
    '        Dim da As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM [" + worksSheetNames + "]  ", exconn)
    '        da.Fill(ds)
    '    Catch ex As Exception
    '        'Me.Label1.Text = ex.Message & ""
    '    Finally
    '        If Not exconn Is Nothing Then
    '            exconn.Close()
    '            exconn.Dispose()
    '        End If
    '    End Try
    '    If ds.Tables.Count > 0 Then
    '        Return ds.Tables(0)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Private Function GetExcelSheetNames(ByVal f As String) As String
    '    Dim SheetNames As String = ""
    '    Try

    '        'If Path.GetExtension(FileUpload1.FileName) = ".xls" Then
    '        ' Excel 2005
    '        'Me.Session("strConn") = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath("./CheckFile/") & f & ";Extended Properties=""Excel 8.0;"""
    '        If f = ".csv" Then
    '            ' CSV
    '            Me.Session("strConn") = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("./Upload/SettingData/") & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"""
    '        Else
    '            ' Excel 2007
    '            Me.Session("strConn") = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("./Upload/SettingData/") & f & ";Extended Properties=""Excel 12.0;"""
    '        End If

    '        exconn = New OleDbConnection(Me.Session("strConn").ToString())
    '        exconn.Open()
    '        dt = exconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                'DropDownList1.DataSource = dt
    '                'DropDownList1.DataTextField = "TABLE_NAME"
    '                'DropDownList1.DataValueField = "TABLE_NAME"
    '                'DropDownList1.DataBind()
    '                'GridView2.DataSource = dt
    '                'GridView2.DataBind()
    '                SheetNames = dt.Rows(0).Item(0)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        'Label1.Text = Me.Session("strConn") 'ex.Message
    '    Finally
    '        If Not exconn Is Nothing Then
    '            exconn.Close()
    '            exconn.Dispose()
    '        End If
    '        If Not dt Is Nothing Then
    '            dt.Dispose()
    '        End If
    '    End Try
    '    Return SheetNames
    'End Function

    'Function CheckFileUpload(ByVal nFileUploadName As String) As DataTable
    '    'Dim File As String
    '    Dim strsql1 As String = ""
    '    Dim SheetNames As String
    '    Dim DT As New DataTable
    '    'File = vFile.FileName
    '    SheetNames = GetExcelSheetNames(nFileUploadName)
    '    'ShowData2GridView1(DropDownList1.SelectedItem.Text.Trim())
    '    DT = getDataByWorksheet(SheetNames)
    '    Return DT
    'End Function

End Class

