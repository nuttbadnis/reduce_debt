Imports System.IO
Imports System.Net
Imports System.Data
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net.IPAddress
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json.Converters.DateTimeConverterBase
Imports Newtonsoft.Json.Converters.JavaScriptDateTimeConverter

Imports System.Data.SqlClient

Public Class auto_vb
    Inherits System.Web.UI.Page
    Dim CDB As New Cls_Data

#Region "Connect_SQL"

    Dim DB105feasibility As String = "Data Source=10.11.5.105;Initial Catalog=Feasibility;persist Security Info=True;Uid=posweb;Pwd=p0sweb;"
    Public Function GetDataTable_Feasibility(ByVal QryStr As String, Optional ByVal TableName As String = "DataTalble1") As DataTable
        Dim objDA As New SqlDataAdapter(QryStr, DB105feasibility)
        Dim objDT As New DataTable(TableName)
        Try
            objDA.Fill(objDT)
        Catch ex As Exception
            'Console.WriteLine(ex.Message)
            Err.Raise(Err.Number, , ex.Message)

        End Try
        Return objDT
    End Function
#End Region


#Region "Function"
    Public Function Get_Uemail(ByVal query_data As String, ByVal session_data As String) As String
        Dim uemail As String = ""
        If query_data IsNot Nothing Then
            uemail = query_data
        Else
            uemail = session_data
        End If
        Return uemail
    End Function
    Public Function alert_collapse(ByVal sql_error As String, ByVal text_error As String) As String
        Dim modal_collapse As String = ""
        modal_collapse += "<button class='btn btn-link p-0' data-toggle='collapse' data-target='#collapseOne'>"
        modal_collapse += "Error Detail <i class='fas fa-angle-double-down'></i>"
        modal_collapse += "</button>"
        modal_collapse += "<div id='collapseOne' class='collapse'>"
        modal_collapse += "<div class='card border border-primary text-primary'>"
        modal_collapse += "<div class='card-body p-2'>"
        modal_collapse += "SQL Query Error : </br>"
        modal_collapse += sql_error
        modal_collapse += "</div>"
        modal_collapse += "<div class='card-body p-2'>"
        modal_collapse += "Exception Error : </br>"
        modal_collapse += text_error
        modal_collapse += "</div>"
        modal_collapse += "</div>"
        modal_collapse += "</div>"
        modal_collapse = modal_collapse.Replace("'", "\'")
        Return modal_collapse
    End Function
    Public Function datethai(ByVal value_datetime As String) As String
        'Response.Write(value_datetime)
        Dim fulldate As String = ""
        Dim split_date() As String = value_datetime.Split("/")
        'Response.Write(split_date.Length)
        If (split_date.Length = 3) Then
            Dim thaiDate As String = split_date(0)
            Dim thaimonth As String = split_date(1)
            If (thaimonth = "01") Then
                thaimonth = "มกราคม"
            ElseIf (thaimonth = "02") Then
                thaimonth = "กุมภาพันธ์"
            ElseIf (thaimonth = "03") Then
                thaimonth = "มีนาคม"
            ElseIf (thaimonth = "04") Then
                thaimonth = "เมษายน"
            ElseIf (thaimonth = "05") Then
                thaimonth = "พฤษภาคม"
            ElseIf (thaimonth = "06") Then
                thaimonth = "มิถุนายน"
            ElseIf (thaimonth = "07") Then
                thaimonth = "กรกฎาคม"
            ElseIf (thaimonth = "08") Then
                thaimonth = "สิงหาคม"
            ElseIf (thaimonth = "09") Then
                thaimonth = "กันยายน"
            ElseIf (thaimonth = "10") Then
                thaimonth = "ตุลาคม"
            ElseIf (thaimonth = "11") Then
                thaimonth = "พฤศจิกายน"
            ElseIf (thaimonth = "12") Then
                thaimonth = "ธันวาคม"
            End If
            Dim thaiyear As String = split_date(2) + 543
            fulldate = thaiDate + " " + thaimonth + " " + thaiyear
            'Response.Write(thaiDate + " " + thaimonth + " " + thaiyear)
        End If
        Return fulldate
    End Function
    Public Function change_datethai(ByVal value_datetime As String) As String
        'Response.Write(value_datetime)
        Dim fulldate As String = ""
        Dim split_date() As String = value_datetime.Split(" ")
        'Response.Write(split_date.Length)
        If (split_date.Length = 3) Then
            Dim thaiDate As String = split_date(0)
            Dim thaimonth As String = split_date(1)
            If (thaimonth = "01") Then
                thaimonth = "มกราคม"
            ElseIf (thaimonth = "02") Then
                thaimonth = "กุมภาพันธ์"
            ElseIf (thaimonth = "03") Then
                thaimonth = "มีนาคม"
            ElseIf (thaimonth = "04") Then
                thaimonth = "เมษายน"
            ElseIf (thaimonth = "05") Then
                thaimonth = "พฤษภาคม"
            ElseIf (thaimonth = "06") Then
                thaimonth = "มิถุนายน"
            ElseIf (thaimonth = "07") Then
                thaimonth = "กรกฎาคม"
            ElseIf (thaimonth = "08") Then
                thaimonth = "สิงหาคม"
            ElseIf (thaimonth = "09") Then
                thaimonth = "กันยายน"
            ElseIf (thaimonth = "10") Then
                thaimonth = "ตุลาคม"
            ElseIf (thaimonth = "11") Then
                thaimonth = "พฤศจิกายน"
            ElseIf (thaimonth = "12") Then
                thaimonth = "ธันวาคม"
            End If
            Dim thaiyear As String = split_date(2) - 543
            fulldate = thaiyear + "-" + thaimonth + "-" + thaiDate

            'Response.Write(thaiDate + " " + thaimonth + " " + thaiyear)
        End If
        Return fulldate
    End Function
    Public Function short_datethai(ByVal value_datetime As String) As String
        'Response.Write(value_datetime)
        Dim shortdate As String = ""
        Dim split_date() As String = value_datetime.Split("/")
        'Response.Write(split_date.Length)
        If (split_date.Length = 3) Then
            Dim thaiDate As String = split_date(0)
            Dim thaimonth As String = split_date(1)
            If (thaimonth = "01") Then
                thaimonth = "ม.ค."
            ElseIf (thaimonth = "02") Then
                thaimonth = "ก.พ."
            ElseIf (thaimonth = "03") Then
                thaimonth = "มี.ค."
            ElseIf (thaimonth = "04") Then
                thaimonth = "เม.ย."
            ElseIf (thaimonth = "05") Then
                thaimonth = "พ.ค."
            ElseIf (thaimonth = "06") Then
                thaimonth = "มิ.ย."
            ElseIf (thaimonth = "07") Then
                thaimonth = "ก.ค."
            ElseIf (thaimonth = "08") Then
                thaimonth = "ส.ค."
            ElseIf (thaimonth = "09") Then
                thaimonth = "ก.ย."
            ElseIf (thaimonth = "10") Then
                thaimonth = "ต.ค."
            ElseIf (thaimonth = "11") Then
                thaimonth = "พ.ย."
            ElseIf (thaimonth = "12") Then
                thaimonth = "ธ.ค."
            End If
            Dim thaiyear As String = split_date(2) + 543
            thaiyear = thaiyear.Substring(2, 2)
            shortdate = thaiDate + "-" + thaimonth + "-" + thaiyear

            'Response.Write(thaiDate + " " + thaimonth + " " + thaiyear)
        End If
        Return shortdate
    End Function
    Public Function validateEmail(ByVal emailAddress As String) As Boolean
        Dim regex As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
        Dim isValid As Boolean = regex.IsMatch(emailAddress.Trim)
        Return isValid
    End Function

#End Region

#Region "sendmail"

    Public Sub SendMailSubmit(ByVal vRequest_id As String)
        Dim vDT As New DataTable
        '--HEADING
        Dim ProjectName AS string = ""
        '--ROW_1
        Dim RefNo AS string = vRequest_id
        Dim intenal_refno AS string = ""
        Dim txtDocumentDate AS string = ""
        '--ROW_2
        Dim lblProjectCode AS string = ""
        Dim lblCustomerType AS string = ""
        Dim txtServiceDate AS string = ""
        '--ROW_3
        Dim lblArea AS string = ""
        Dim lblCluster AS string = ""
        '--ROW_4
        Dim lblTypeOfService AS string = ""
        Dim lblCompanyService AS string = ""
        Dim vUemail As String = "" '"panupong.pa;test.t;test.t;"
        vUemail = "nat.m;test.a;"
        
        Dim strSql2 As String
        Dim DT As New DataTable
        strSql2 = "select * from dbo.List_ProjectName where Document_No = '" & vRequest_id & "' "
        DT = CDB.GetDataTable(strSql2)
        If DT.Rows.Count > 0 Then
            ProjectName = DT.Rows(0).Item("Project_Name")
            txtDocumentDate = DT.Rows(0).Item("Document_Date")

            If IsDBNull(DT.Rows(0).Item("Project_Code")) Then
                lblProjectCode = ""
            Else
                lblProjectCode = DT.Rows(0).Item("Project_Code")
            End If

            lblCustomerType = DT.Rows(0).Item("Customer_Type")
            txtServiceDate = DT.Rows(0).Item("Service_Date")

            lblArea = DT.Rows(0).Item("Area")
            lblCluster = DT.Rows(0).Item("Cluster")

            lblTypeOfService = DT.Rows(0).Item("Type_Service")
            lblCompanyService = DT.Rows(0).Item("Company_Service")
            
            'vCustomer_name = DT.Rows(0).Item("Customer_name")
            'vProject_name = DT.Rows(0).Item("Project_Name")
            'vSubject_name = DT.Rows(0).Item("Customer_name") & " - " & DT.Rows(0).Item("Project_Name")
        End If

        Try
            'vMain_Point += " *" + vCase

            Dim vSplit_uemail As String() = Regex.Split(vUemail, ";")

            Dim mail As New MailMessage()
            mail.From = New MailAddress("fesibility@jasmine.com")
            'mail.CC.Add("weraphon.r@jasmine.com")

            For Each sMail As String In vSplit_uemail
                If sMail.Trim() <> "" Then
                    mail.To.Add(sMail + "@jasmine.com")
                End If
            Next

            'mail.Subject = "Follow Request " + vRequest_id + ": " + vMain_Point
            mail.Subject = "หัวข้อเทส"

            'mail.Body = rMailBody(vRequest_id, vCustomer_name, vSubject_name, vSubject_url, vProject_name, vMain_Point)
            mail.Body = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
            ,txtServiceDate, lblArea, lblCluster)

            mail.IsBodyHtml = True

            Dim smtp As New SmtpClient("smtp.jasmine.com")
            smtp.Credentials = New NetworkCredential("chancharas.w", "311227")

            smtp.Send(mail)

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "alert('false');", True)
        End Try
    End Sub

    Public Function rMailtest(ByVal ProjectName As String, ByVal RefNo As String, ByVal intenal_refno As String _
    , ByVal txtDocumentDate As String, ByVal lblProjectCode As String, ByVal lblCustomerType As String _
    , ByVal txtServiceDate As String, ByVal lblArea As String, ByVal lblCluster As String _
    ) As String

        Return _
        "<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>" & _
        "<html lang='th'>" & _
        "<head>" & _
        "  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>" & _
        "  <meta name='viewport' content='width=device-width, initial-scale=1'>" & _
        "  <meta http-equiv='X-UA-Compatible' content='IE=edge'>" & _
        "  <meta name='format-detection' content='telephone=no'>" & _
        "</head>" & _
        "<body style='margin:0; padding:0;' leftmargin='0' topmargin='0' marginwidth='0' marginheight='0'>" & _
          "<table style='width: 100%;border: 1px solid black;margin: auto;font-family:'TH Sarabun New';'>" & _
        "<tr>" & _
        "<td >" & _
        "<table style='width: 100%;border: 1px solid black;margin: 0px 0px 0px 0px;border-bottom-style: none;'>" & _
        "<tr>" & _
            "<td style='text-align: right; height: 10px;'>" & _
            "</td>" & _
        "</tr>" & _
        "<tr align='center'>" & _
            "<td style='font-weight: bolder;font-family:'TH Sarabun New'; height: 30px;' valign='top'>" & _
            "แบบอนุมัติ "+ProjectName+" " & _ 
            "</td>" & _  
        "</tr>" & _
        "</table>" & _
            "<table width='1500' cellspacing='0' style='width: 100%;border: 1px solid black;border-bottom-style: none;'>" & _
            "<tr class='border border-1 border-dark' valign='top'>" & _
             "<td style='text-align: left; border-bottom: 1px solid black;'>CS Ref.No: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + RefNo + "</td>" & _
             "<td style='text-align: left; border-bottom: 1px solid black;'>Internal Ref.No: </td> " & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + intenal_refno + "</td>" & _
             "<td style='text-align: left; border-bottom: 1px solid black;'>Date: </td> " & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + txtDocumentDate + "</td>" & _
            "</tr>" & _
            "<tr class='border border-1 border-dark' valign='top'>" & _
             "<td style='text-align: left;border-bottom: 1px solid black;'>Project Code: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>"+lblProjectCode+"</td>" & _
             "<td style='text-align: left;border-bottom: 1px solid black;'>ประเภทลูกค้า: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>"+lblCustomerType+"</td>" & _
             "<td style='text-align: left;border-bottom: 1px solid black;'>วันเปิดบริการ: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>"+txtServiceDate+"</td>" & _
            "</tr>" & _
            "<tr class='border border-1 border-dark' valign='top'>" & _
             "<td style='text-align: left;border-bottom: 1px solid black;'>Project Name: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>"+ProjectName+"</td>" & _
             "<td style='text-align: left;border-bottom: 1px solid black;'>Area: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>"+lblArea+"</td>" & _
             "<td style='text-align: left;border-bottom: 1px solid black;'>Cluster: </td>" & _
             "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>"+lblCluster+"</td>" & _
            "</tr>" & _
            "</table>" & _
        "</table>" & _
        "</html>"
    End Function
    
#End Region


#Region "Analytics"

    Public Function rGetPageName(Optional ByVal vRecursive As String = "") As String
        Dim vRes As String = HttpContext.Current.Request.Url.AbsoluteUri()

        If vRecursive <> "" Then
            vRes = vRecursive
        End If

        Dim vTemp As String = vRes

        Try
            vRes = vRes.Substring(vRes.LastIndexOf("/") + 1)
            vRes = vRes.Substring(0, vRes.IndexOf("."))

            Return vRes
        Catch ex As Exception
            vRes = vTemp
            vRes = vRes.Substring(0, vRes.LastIndexOf("/"))

            Return rGetPageName(vRes)
        End Try

    End Function

    Public Function rGetClientComName() As String
        Try
            Return System.Net.Dns.GetHostByAddress(HttpContext.Current.Request.UserHostAddress()).HostName
        Catch ex As Exception
            Return "unknown"
        End Try
    End Function

    Public Function rGetBrowserName() As String
        Dim userAgent As String = HttpContext.Current.Request.UserAgent
        If userAgent.Contains("Firefox") Then
            Return userAgent.Substring(userAgent.IndexOf("Firefox"))
        ElseIf userAgent.Contains("Chrome") Then
            Dim agentPart As String = userAgent.Substring(userAgent.IndexOf("Chrome"))
            Return agentPart.Substring(0, agentPart.IndexOf("Safari") - 1)
        End If

        Return HttpContext.Current.Request.Browser.Browser & "/" & HttpContext.Current.Request.Browser.Version
    End Function

    Public Function rGetOS() As String
        Dim vClientAgent As String = HttpContext.Current.Request.UserAgent().ToLower()

        If vClientAgent.IndexOf("windows nt 10.0") >= 0 Then
            Return "Windows 10"
        ElseIf vClientAgent.IndexOf("windows nt 6.3") >= 0 Then
            Return "Windows 8.1"
        ElseIf vClientAgent.IndexOf("windows nt 6.2") >= 0 Then
            Return "Windows 8"
        ElseIf vClientAgent.IndexOf("windows nt 6.1") >= 0 Then
            Return "Windows 7"
        ElseIf vClientAgent.IndexOf("windows nt 6.0") >= 0 Then
            Return "Windows Vista"
        ElseIf vClientAgent.IndexOf("windows nt 5.2") >= 0 Then
            Return "Windows Server 2003"
        ElseIf vClientAgent.IndexOf("windows nt 5.1") >= 0 Then
            Return "Windows XP"
        ElseIf vClientAgent.IndexOf("windows nt 5.01") >= 0 Then
            Return "Windows 2000 (SP1)"
        ElseIf vClientAgent.IndexOf("windows nt 5.0") >= 0 Then
            Return "Windows 2000"
        ElseIf vClientAgent.IndexOf("windows nt 4.5") >= 0 Then
            Return "Windows NT 4.5"
        ElseIf vClientAgent.IndexOf("windows nt 4.0") >= 0 Then
            Return "Windows NT 4.0"
        ElseIf vClientAgent.IndexOf("win 9x 4.90") >= 0 Then
            Return "Windows ME"
        ElseIf vClientAgent.IndexOf("windows 98") >= 0 Then
            Return "Windows 98"
        ElseIf vClientAgent.IndexOf("windows 95") >= 0 Then
            Return "Windows 95"
        ElseIf vClientAgent.IndexOf("windows ce") >= 0 Then
            Return "Windows CE"
        ElseIf (vClientAgent.Contains("ipad")) Then
            Return String.Format("iPad OS {0}", GetMobileVersion(vClientAgent, "OS"))
        ElseIf (vClientAgent.Contains("iphone")) Then
            Return String.Format("iPhone OS {0}", GetMobileVersion(vClientAgent, "OS"))
        ElseIf (vClientAgent.Contains("linux") AndAlso vClientAgent.Contains("kfapwi")) Then
            Return "Kindle Fire"
        ElseIf (vClientAgent.Contains("rim tablet") OrElse (vClientAgent.Contains("bb") AndAlso vClientAgent.Contains("mobile"))) Then
            Return "Black Berry"
        ElseIf (vClientAgent.Contains("Windows phone")) Then
            Return String.Format("Windows Phone {0}", GetMobileVersion(vClientAgent, "Windows Phone"))
        ElseIf (vClientAgent.Contains("mac os")) Then
            Return "Mac OS"
        ElseIf vClientAgent.IndexOf("android") >= 0 Then
            Return String.Format("Android {0}", GetMobileVersion(vClientAgent, "ANDROID"))
        Else
            Return "OS is unknown."
        End If
    End Function

    Private Function GetMobileVersion(userAgent As String, device As String) As String
        Dim ReturnValue As String = String.Empty
        Dim RawVersion As String = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart()

        For Each character As Char In RawVersion
            If IsNumeric(character) Then
                ReturnValue &= character
            ElseIf (character = "." OrElse character = "_") Then
                ReturnValue &= "."
            Else
                Exit For
            End If
        Next

        Return ReturnValue
    End Function
#End Region

#Region "Json Data"

    Public Function rDataTableToJson(ByVal dt As DataTable) As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object) = Nothing

        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)()
            For Each dc As DataColumn In dt.Columns
                row.Add(dc.ColumnName.Trim(), dr(dc))
            Next

            rows.Add(row)
        Next

        Return serializer.Serialize(rows)
    End Function

#End Region

#Region "OAuth"
    Public Function SetOAuthSingleSignOn(ByVal code As String, ByVal Client_id As String, ByVal Client_Secret As String, ByVal redirect_uri As String)
        Dim DS As New DataSet
        Dim DT As New DataTable
        Dim HttpWReq As HttpWebRequest
        Dim httpWRes As HttpWebResponse = Nothing
        Dim address As Uri
        Dim strData As New StringBuilder
        Dim vUri As String = "https://api.jasmine.com/authen1/oauth/token?client_id=" + Client_id + "&redirect_uri=" + redirect_uri + "&grant_type=authorization_code&code=" + code

        address = New Uri(vUri)
        HttpWReq = DirectCast(WebRequest.Create(address), HttpWebRequest)
        HttpWReq.Method = "POST"
        HttpWReq.ContentType = "application/x-www-form-urlencoded"

        SetBasicAuthHeader(HttpWReq, Client_id, Client_Secret)

        httpWRes = DirectCast(HttpWReq.GetResponse(), HttpWebResponse)

        Dim reader As StreamReader = New StreamReader(httpWRes.GetResponseStream())
        Dim json As String = reader.ReadToEnd()

        Dim vHeader() As String
        Dim Token() As String
        Dim Access_Token As String = ""
        vHeader = Split(json.ToString, ",")
        If vHeader.Length > 2 Then
            Token = Split(vHeader(0).ToString, ":")
            Access_Token = Replace(Token(1).ToString, """", "").ToString.Trim
        End If

        Dim Request As HttpWebRequest
        Dim Response As HttpWebResponse
        Request = DirectCast(WebRequest.Create(New Uri("https://api.jasmine.com/authen1/me")), HttpWebRequest)
        HttpWReq.Method = "GET"
        HttpWReq.ContentType = "application/x-www-form-urlencoded"
        Request.Headers("Authorization") = "Bearer " + Access_Token

        Response = DirectCast(Request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(Response.GetResponseStream())
        json = reader.ReadToEnd()

        DT = ConvertJSONToDataTable(json)
        Dim username As String() = DT.Rows(0).Item("username").ToString.Split("@")
        'Session("Uemail") = username(0)

        Return username(0)
    End Function
    Public Function DerializeDataTable(ByVal strJSON As String) As DataTable
        Dim dt As DataTable
        dt = JsonConvert.DeserializeObject(Of DataTable)(strJSON)
        Return dt
    End Function

    Public Function rGetDataOAuthjson(ByVal vSearch As String, ByVal vToken As String, ByVal vDepartment As String, ByVal vEmail As String, ByVal vCode As String, ByVal vType As String) As String
        Dim DT, DT2 As New DataTable
        Dim json As String

        Dim reader As StreamReader
        Dim Request As HttpWebRequest
        Dim Response As HttpWebResponse
        Dim start, totalTime As Double
        start = Microsoft.VisualBasic.DateAndTime.Timer
        Try
            'Request = DirectCast(WebRequest.Create(New Uri("https://app.jasmine.com/contact-resource-api/v2/" + vSearch + "/")), HttpWebRequest)
            Request = DirectCast(WebRequest.Create(New Uri("https://app.jasmine.com/jpmapi/employee/contact/v1/search?keyword=" + vSearch + "")), HttpWebRequest)
            Request.Method = "GET"
            Request.ContentType = "application/x-www-form-urlencoded"

            Request.Headers("token") = vToken
            'Request.Headers("scope") = "employee-information"
            Response = DirectCast(Request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(Response.GetResponseStream())
            json = reader.ReadToEnd()
            'DT = ConvertJSONToDataTable(json)

        Catch ex As Exception
            'json = "test"
            json = "{Message : '" + (ex.Message).ToString + "'}"
            Dim return_json As JObject = JObject.Parse(json)
            json = return_json.ToString()
        End Try

        Return json
    End Function

    Private Sub SetBasicAuthHeader(ByVal request As WebRequest, ByVal userName As String, ByVal userPassword As String)
        Dim authInfo As String = userName + ":" + userPassword
        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo))
        request.Headers("Authorization") = "Basic " + authInfo
    End Sub

    Private Function ConvertJSONToDataTable(ByVal jsonString As String) As DataTable
        Dim dt As New DataTable
        'strip out bad characters
        Dim jsonParts As String() = jsonString.Replace("[{", "{").Replace("}]", "}").Split("},{")

        'hold column names
        Dim dtColumns As New List(Of String)

        'get columns
        For Each jp As String In jsonParts
            'only loop thru once to get column names
            Dim propData As String() = jp.Replace("{", "").Replace("}", "").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
            For Each rowData As String In propData
                Try
                    If rowData.Split(":").Length - 1 <> 0 Then
                        Dim idx As Integer = rowData.IndexOf(":")
                        Dim n As String = rowData.Substring(0, idx - 1)
                        Dim v As String = rowData.Substring(idx + 1)
                        If Not dtColumns.Contains(n) Then
                            dtColumns.Add(n.Replace("""", ""))
                        End If
                    End If
                Catch ex As Exception
                    Throw New Exception(String.Format("Error Parsing Column Name : {0}", rowData))
                End Try

            Next
            Exit For
        Next

        'build dt
        For Each c As String In dtColumns
            dt.Columns.Add(c)
        Next
        'get table data
        For Each jp As String In jsonParts
            Dim propData As String() = jp.Replace("{", "").Replace("}", "").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
            Dim nr As DataRow = dt.NewRow
            For Each rowData As String In propData
                Try
                    Dim idx As Integer = rowData.IndexOf(":")
                    Dim n As String = rowData.Substring(0, idx - 1).Replace("""", "")
                    Dim v As String = rowData.Substring(idx + 1).Replace("""", "")
                    nr(n) = v
                Catch ex As Exception
                    Continue For
                End Try

            Next
            dt.Rows.Add(nr)
        Next
        Return dt
    End Function

    Function rConvertDataTableToJSONv1(ByVal vRes As DataTable) As String
        Dim vJson As String = ""
        Dim vTemp As String = ""

        Dim vFields = New ArrayList
        For c As Integer = 0 To vRes.Columns.Count - 1
            vFields.Add(vRes.Columns(c).ColumnName)
        Next

        For i As Integer = 0 To vRes.Rows().Count() - 1
            vJson += "{"
            For Each vf As String In vFields
                vTemp = "Null"
                If Not IsDBNull(vRes.Rows(i).Item(vf)) Then
                    vTemp = vRes.Rows(i).Item(vf)
                End If
                vJson += " """ + vf + """:""" + CStr(vTemp) + """ ,"
            Next
            vJson = vJson.Remove(vJson.Length - 1, 1)
            vJson += "},"
        Next

        If vJson <> Nothing Then
            vJson = "[" + vJson
            vJson = vJson.Remove(vJson.Length - 1, 1)
            vJson += "]"
        Else
            vJson = "[]"
        End If

        Return vJson
    End Function
#End Region

#Region "OTP"
    Function rGenOTP(ByVal vBranch As String, ByVal vObject As Integer, Optional ByVal vAddOn As String = "") As String
        'Function rGenOTP(ByVal vBranch As String, Optional ByVal vAddOn As String = "") As String
        Dim vDegit1, vDegit2, vDegit3, vDegit4, vDegit5 As Integer
        Dim vDegit1_2 As String

        Dim vNow As String = DateTime.Now.ToString("dd/MM/yy HH:mm:ss")
        Dim vDate As String = vNow.Substring(0, 2)
        Dim vMonth As String = vNow.Substring(3, 2)
        Dim vHour As String = vNow.Substring(9, 2)

        vDegit1_2 = rShiftHour(vHour, vAddOn)

        vDegit1 = vDegit1_2.Substring(1, 1)
        vDegit2 = vDegit1_2.Substring(0, 1)
        vDegit3 = rAlphabet(vBranch.Substring(1, 1))
        vDegit4 = rAlphabet(vBranch.Substring(4, 1))
        vDegit5 = rDegit5(vDate, vMonth)

        vDegit5 = vDegit5 + vObject
        vDegit5 = vDegit5 Mod 10

        'return vDegit1 & vDegit2 & vDegit3 & vDegit4 & vDegit5
        Return rNewOTP(vDegit1, vDegit2, vDegit3, vDegit4, vDegit5)
    End Function

    Function rTestOTP(ByVal vBranch As String, ByVal vObject As Integer, ByVal vTime As String, ByVal vNow As String, Optional ByVal vAddOn As String = "")
        Dim vDegit1, vDegit2, vDegit3, vDegit4, vDegit5 As Integer
        Dim vDegit1_2 As String

        Dim vDate As String = vNow.Substring(0, 2)
        Dim vMonth As String = vNow.Substring(3, 2)
        Dim vHour As String = vTime

        'rEcho("vNow:" & vNow & " > vDate:" & vDate & " > vMonth:" & vMonth & " > vHour:" & vHour)
        vDegit1_2 = rShiftHour(vHour, vAddOn)

        vDegit1 = vDegit1_2.Substring(1, 1)
        vDegit2 = vDegit1_2.Substring(0, 1)
        vDegit3 = rAlphabet(vBranch.Substring(1, 1))
        vDegit4 = rAlphabet(vBranch.Substring(4, 1))
        vDegit5 = rDegit5(vDate, vMonth)

        vDegit5 = vDegit5 + vObject
        vDegit5 = vDegit5 Mod 10

        rEcho(vDegit1 & vDegit2 & vDegit3 & vDegit4 & vDegit5 & " > ")
        Dim vRes As String = rNewOTP(vDegit1, vDegit2, vDegit3, vDegit4, vDegit5)
        rEcho(vRes)
    End Function

    Private Function rShiftHour(ByVal vHour As String, Optional ByVal vAddOn As String = "") As String
        If vAddOn = "+1" Then
            If vHour = "24" Then
                vHour = "00"
            Else
                vHour = vHour + 1
            End If
        ElseIf vAddOn = "-1" Then
            If vHour = "01" Then
                vHour = "24"
            Else
                vHour = vHour - 1
            End If
        End If

        If vHour.Length = 1 Then
            vHour = "0" & vHour
        End If

        Return vHour
    End Function

    Function rNewOTP(ByVal vDegit1 As String, ByVal vDegit2 As String, ByVal vDegit3 As String, ByVal vDegit4 As String, ByVal vDegit5 As String)
        Dim vRes As Integer = rNonZero(vDegit1) * rNonZero(vDegit2) * rNonZero(vDegit3) * rNonZero(vDegit4) * rNonZero(vDegit5)
        vRes = vRes Mod 1000
        vRes = vRes & vDegit5

        Return rAppendZero(vRes)
    End Function

    Private Function rDegit5(ByVal vDate As String, ByVal vMonth As String) As Integer
        Dim vR1 As Integer = vDate.Substring(0, 1)
        Dim vR2 As Integer = vDate.Substring(1, 1)
        Dim vR3 As Integer = vMonth.Substring(0, 1)
        Dim vR4 As Integer = vMonth.Substring(1, 1)

        Return (vR1 + vR2 + vR3 + vR4) Mod 10
    End Function

    Private Function rNonZero(ByVal vNum As String) As String
        If vNum = 0 Then
            Return 1
        End If

        Return vNum
    End Function

    Private Function rAppendZero(ByVal vDegit As String) As String
        While vDegit.Length < 4
            vDegit = "0" & vDegit
        End While

        Return vDegit
    End Function
#End Region

#Region "NoCat"
    Function rEcho(ByVal vStr As String)
        HttpContext.Current.Response.Write(vStr)
    End Function

    Function rAlphabet(ByVal vABC As String) As String
        Dim Alphabet As String = " ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        If Regex.IsMatch(vABC, "^[0-9 ]+$") Then
            Return vABC
        Else
            Dim vR As String = vABC.ToUpper()
            vR = Alphabet.IndexOf(vR, 0)
            vR = vR Mod 10

            Return vR
        End If
    End Function

    Function rGetIpHost() As String
        Dim vHostName As String = System.Net.Dns.GetHostName()
        Dim vIPAddress As String = System.Net.Dns.GetHostByName(vHostName).AddressList(0).ToString()

        Return vIPAddress
    End Function

    Function rReplaceQuote(ByVal vValue As String) As String
        If vValue <> Nothing Then
            Return vValue.Replace("'", "''")
        Else
            Return vValue
        End If
    End Function

#End Region

End Class

