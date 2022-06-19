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

Public Class test_vb
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
        Dim DT_Service As New DataTable
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
        Dim lblCompanyService As String = ""
        '--ROW_5
        Dim lblCustomerName As String = ""
        Dim OneTimePayment As Double = 0
        Dim MonthlyPrice As Double = 0
        Dim lblOneTimePayment As String = ""
        Dim lblMonthlyPrice As String = ""
        Dim lblTotalCost As String = ""
        '--ROW_6
        Dim txtSLA As String = ""
        Dim txtMTTR As String = ""
        Dim lblMonitorDate As String = ""
        Dim lblMonitorTime As String = ""
        Dim lblContract As String = ""
        Dim lblCustomerContactEmail As String = ""
        Dim txtDetailService As String = ""
        Dim txtFine As String = ""
        Dim NOC As Double = 0
        Dim NOCTotalCost As Double = 0
        Dim lblUtil As String = ""
        '--ROW-Bottom
        Dim MoneyRoundup As String = "###,##0"
        Dim lblRevenue As Double
        Dim Revenue_total As Double
        Dim RevenueOne As Double
        Dim RevenueNotOne As Double
        Dim MKTCostOne As Double
        Dim MKTCostNotOne As Double
        Dim PenaltyOne As Double
        Dim PenaltyNotOne As Double
        Dim DomesticCost As Double
        Dim InternationalCost As Double
        Dim Caching As Double
        Dim PortCost As Double
        Dim CostOfInternet As Double
        Dim CostOfNetwork As Double
        Dim NetworkCost As Double

        Dim Upload_Project as string
        Dim strSql As String
        Dim strSql2 As String
        Dim DT As New DataTable
        strSql2 = "select * from dbo.List_ProjectName where Document_No = '" & vRequest_id & "' "
        DT = CDB.GetDataTable(strSql2)
        If DT.Rows.Count > 0 Then
            Upload_Project = DT.Rows(0).Item("Upload_Project")            
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

            lblCustomerName = DT.Rows(0).Item("Customer_Contact_Name")
            MonthlyPrice = CDbl(DT.Rows(0).Item("Monthly").ToString)
            OneTimePayment = CDbl(DT.Rows(0).Item("OneTimePayment").ToString)
            lblMonthlyPrice = Format(CDbl(MonthlyPrice), "###,##0.00")
            lblOneTimePayment = Format(CDbl(OneTimePayment), "###,##0.00")
            lblTotalCost = Format(CDbl(DT.Rows(0).Item("TotalProject").ToString), "###,##0.00")

            txtSLA = DT.Rows(0).Item("SLA")
            txtMTTR = DT.Rows(0).Item("MTTR")
            lblMonitorDate = DT.Rows(0).Item("Monitor_Date")
            lblMonitorTime = DT.Rows(0).Item("Monitor_Time")
            lblContract = DT.Rows(0).Item("Contract").ToString
            lblCustomerContactEmail = DT.Rows(0).Item("Customer_Contact_Email")
            txtDetailService = Replace(DT.Rows(0).Item("Detail_Service"), Environment.NewLine, "<br />")
            txtFine = Replace(DT.Rows(0).Item("Fine"), Environment.NewLine, "<br />")
            Dim showPenalty As String = ""
            If CDbl(DT.Rows(0).Item("PenaltyLate").ToString) = "0" Then
                showPenalty += ""
                'lblPenaltyLate.Text = "0"
            Else
                showPenalty += "ประเมินค่าปรับส่งมอบล่าช้า " & Format(CDbl(DT.Rows(0).Item("PenaltyLate").ToString), "###,##0.00") & " บาท<br />"
                'lblPenaltyLate.Text = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString)
            End If
            If CDbl(DT.Rows(0).Item("Penalty").ToString) = "0" Then
                showPenalty += ""
            Else
                showPenalty += "ประเมินค่าปรับงานซ่อม " & Format(CDbl(DT.Rows(0).Item("Penalty").ToString), "###,##0.00") & "%<br />"
            End If
            txtFine = showPenalty + txtFine
            
            RevenueOne = CDbl(lblMonthlyPrice) + CDbl(lblOneTimePayment)
            RevenueNotOne = CDbl(lblMonthlyPrice)        
            lblRevenue = Format(RevenueOne + (RevenueNotOne * (lblContract - 1)), MoneyRoundup)
            Revenue_total = Format(RevenueOne + (RevenueNotOne * (lblContract - 1)), MoneyRoundup)

            MKTCostOne = ((CDbl(DT.Rows(0).Item("Marketing").ToString) / 100) * RevenueOne) + ((CDbl(DT.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueOne) + CDbl(DT.Rows(0).Item("Gift").ToString)
            MKTCostNotOne = ((CDbl(DT.Rows(0).Item("Marketing").ToString) / 100) * RevenueNotOne) + ((CDbl(DT.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueNotOne)

            PenaltyOne = CDbl(DT.Rows(0).Item("PenaltyLate").ToString) + ((CDbl(DT.Rows(0).Item("Penalty").ToString) / 100) * RevenueOne)
            PenaltyNotOne = (CDbl(DT.Rows(0).Item("Penalty").ToString) / 100) * RevenueNotOne

            'vCustomer_name = DT.Rows(0).Item("Customer_name")
            'vProject_name = DT.Rows(0).Item("Project_Name")
            'vSubject_name = DT.Rows(0).Item("Customer_name") & " - " & DT.Rows(0).Item("Project_Name")
            'If Upload_Project = 0 
                strSql = "select * from dbo.List_Service where Document_No = '" & RefNo & "' "
                DT_Service = CDB.GetDataTable(strSql)
                If DT_Service.Rows.Count > 0 Then
                    NOC = CDbl(DT_Service.Rows(0).Item("NOC"))
                    NOCTotalCost = CDbl(DT_Service.Rows(0).Item("NOC")) * CDbl(DT_Service.Rows(0).Item("NOCCost"))
                End If
                
                lblUtil = "Util." & DT_Service.Rows(0).Item("INLUtilization").ToString & "%"
                DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("DomesticCost").ToString))
                InternationalCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * CDbl(DT_Service.Rows(0).Item("TransitCost").ToString))
                Caching = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Caching").ToString) * CDbl(DT_Service.Rows(0).Item("AllInternationalCost").ToString))
                CostOfInternet = DomesticCost + InternationalCost + Caching

                PortCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("NetWorkPort").ToString) * CDbl(DT_Service.Rows(0).Item("NetWorkPortCost").ToString))
                NetworkCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("EthernetNetwork").ToString) * (CDbl(DT_Service.Rows(0).Item("NetworkUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("NetworkCost").ToString))
                CostOfNetwork = PortCost + NetworkCost

                DIM A AS String = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
                , txtServiceDate, lblArea, lblCluster, lblTypeOfService, lblCompanyService, lblCustomerName, lblOneTimePayment _
                , lblMonthlyPrice, lblTotalCost, txtSLA, txtMTTR, lblMonitorDate, lblMonitorTime, lblContract _
                , lblCustomerContactEmail, txtDetailService, txtFine, NOC, NOCTotalCost, lblUtil, lblRevenue, Revenue_total _
                , RevenueOne, RevenueNotOne, MKTCostOne, MKTCostNotOne, PenaltyOne, PenaltyNotOne, CostOfInternet, CostOfNetwork)
                System.Web.HttpContext.Current.Response.Write(A)
            'Else
            
            'End If

        End If
    

        
        ' Dim B AS String = rMailtest(vRequest_id)
        ' System.Web.HttpContext.Current.Response.Write(B)

        Try
            'vMain_Point += " *" + vCase
            
            Dim vUemail As String = "" '"panupong.pa;test.t;test.t;"
            vUemail = "nat.m;test.a;"
        
            Dim vSplit_uemail As String() = Regex.Split(vUemail, ";")

            Dim mail As New MailMessage()
            mail.From = New MailAddress("fesibility@jasmine.com")
            mail.CC.Add("weraphon.r@jasmine.com")

            For Each sMail As String In vSplit_uemail
                If sMail.Trim() <> "" Then
                    mail.To.Add(sMail + "@jasmine.com")
                End If
            Next

            'mail.Subject = "Follow Request " + vRequest_id + ": " + vMain_Point
            mail.Subject = "หัวข้อเทส"

            'If Upload_Project = 0 
                mail.Body = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
                , txtServiceDate, lblArea, lblCluster, lblTypeOfService, lblCompanyService, lblCustomerName, lblOneTimePayment _
                , lblMonthlyPrice, lblTotalCost, txtSLA, txtMTTR, lblMonitorDate, lblMonitorTime, lblContract _
                , lblCustomerContactEmail, txtDetailService, txtFine, NOC, NOCTotalCost, lblUtil, lblRevenue, Revenue_total _
                , RevenueOne, RevenueNotOne, MKTCostOne, MKTCostNotOne, PenaltyOne, PenaltyNotOne, CostOfInternet, CostOfNetwork)
            'Else

            'End If
            'mail.Body = rMailBody(vRequest_id, vCustomer_name, vSubject_name, vSubject_url, vProject_name, vMain_Point)
            ' mail.Body = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
            ' , txtServiceDate, lblArea, lblCluster, lblTypeOfService, lblCompanyService, lblCustomerName, lblOneTimePayment _
            ' , lblMonthlyPrice, lblTotalCost, txtSLA, txtMTTR, lblMonitorDate, lblMonitorTime, lblContract _
            ' , lblCustomerContactEmail, txtDetailService, txtFine, NOC, NOCTotalCost, lblUtil, lblRevenue, Revenue_total _
            ' , RevenueOne, RevenueNotOne, MKTCostOne, MKTCostNotOne, PenaltyOne, PenaltyNotOne, CostOfInternet, CostOfNetwork)

            mail.IsBodyHtml = True

            Dim smtp As New SmtpClient("smtp.jasmine.com")
            smtp.Credentials = New NetworkCredential("chancharas.w", "311227")

            smtp.Send(mail)

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "alert('false');", True)
        End Try
    End Sub

    sub check_mail(ByVal vRequest_id as string)
        Dim vDT As New DataTable
        Dim DT_Service As New DataTable
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
        Dim lblCompanyService As String = ""
        '--ROW_5
        Dim lblCustomerName As String = ""
        Dim OneTimePayment As Double
        Dim MonthlyPrice As Double
        Dim lblOneTimePayment As String = ""
        Dim lblMonthlyPrice As String = ""
        Dim lblTotalCost As String = ""
        '--ROW_6
        Dim txtSLA As String = ""
        Dim txtMTTR As String = ""
        Dim lblMonitorDate As String = ""
        Dim lblMonitorTime As String = ""
        Dim lblContract As String = ""
        Dim lblCustomerContactEmail As String = ""
        Dim txtDetailService As String = ""
        Dim txtFine As String = ""
        Dim NOC As Double = 0
        Dim NOCTotalCost As Double = 0
        Dim lblUtil As String = ""
        '--ROW-Bottom
        Dim MoneyRoundup As String = "###,##0"
        Dim lblRevenue As Double
        Dim Revenue_total As Double
        Dim RevenueOne As Double
        Dim RevenueNotOne As Double
        Dim MKTCostOne As Double
        Dim MKTCostNotOne As Double
        Dim PenaltyOne As Double
        Dim PenaltyNotOne As Double
        Dim DomesticCost As Double
        Dim InternationalCost As Double
        Dim Caching As Double
        Dim PortCost As Double
        Dim CostOfInternet As Double
        Dim CostOfNetwork As Double
        Dim NetworkCost As Double

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

            lblCustomerName = DT.Rows(0).Item("Customer_Contact_Name")
            MonthlyPrice = CDbl(DT.Rows(0).Item("Monthly").ToString)
            OneTimePayment = CDbl(DT.Rows(0).Item("OneTimePayment").ToString)
            lblMonthlyPrice = Format(CDbl(MonthlyPrice), "###,##0.00")
            lblOneTimePayment = Format(CDbl(OneTimePayment), "###,##0.00")
            lblTotalCost = Format(CDbl(DT.Rows(0).Item("TotalProject").ToString), "###,##0.00")

            txtSLA = DT.Rows(0).Item("SLA")
            txtMTTR = DT.Rows(0).Item("MTTR")
            lblMonitorDate = DT.Rows(0).Item("Monitor_Date")
            lblMonitorTime = DT.Rows(0).Item("Monitor_Time")
            lblContract = DT.Rows(0).Item("Contract").ToString
            lblCustomerContactEmail = DT.Rows(0).Item("Customer_Contact_Email")
            txtDetailService = Replace(DT.Rows(0).Item("Detail_Service"), Environment.NewLine, "<br />")
            txtFine = Replace(DT.Rows(0).Item("Fine"), Environment.NewLine, "<br />")
            Dim showPenalty As String = ""
            If CDbl(DT.Rows(0).Item("PenaltyLate").ToString) = "0" Then
                showPenalty += ""
                'lblPenaltyLate.Text = "0"
            Else
                showPenalty += "ประเมินค่าปรับส่งมอบล่าช้า " & Format(CDbl(DT.Rows(0).Item("PenaltyLate").ToString), "###,##0.00") & " บาท<br />"
                'lblPenaltyLate.Text = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString)
            End If
            If CDbl(DT.Rows(0).Item("Penalty").ToString) = "0" Then
                showPenalty += ""
            Else
                showPenalty += "ประเมินค่าปรับงานซ่อม " & Format(CDbl(DT.Rows(0).Item("Penalty").ToString), "###,##0.00") & "%<br />"
            End If
            txtFine = showPenalty + txtFine
            
            RevenueOne = CDbl(lblMonthlyPrice) + CDbl(lblOneTimePayment)
            RevenueNotOne = CDbl(lblMonthlyPrice)        
            lblRevenue = Format(RevenueOne + (RevenueNotOne * (lblContract - 1)), MoneyRoundup)
            Revenue_total = Format(RevenueOne + (RevenueNotOne * (lblContract - 1)), MoneyRoundup)

            MKTCostOne = ((CDbl(DT.Rows(0).Item("Marketing").ToString) / 100) * RevenueOne) + ((CDbl(DT.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueOne) + CDbl(DT.Rows(0).Item("Gift").ToString)
            MKTCostNotOne = ((CDbl(DT.Rows(0).Item("Marketing").ToString) / 100) * RevenueNotOne) + ((CDbl(DT.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueNotOne)

            PenaltyOne = CDbl(DT.Rows(0).Item("PenaltyLate").ToString) + ((CDbl(DT.Rows(0).Item("Penalty").ToString) / 100) * RevenueOne)
            PenaltyNotOne = (CDbl(DT.Rows(0).Item("Penalty").ToString) / 100) * RevenueNotOne

            'vCustomer_name = DT.Rows(0).Item("Customer_name")
            'vProject_name = DT.Rows(0).Item("Project_Name")
            'vSubject_name = DT.Rows(0).Item("Customer_name") & " - " & DT.Rows(0).Item("Project_Name")
        End If

        Dim strSql = "select * from dbo.List_Service where Document_No = '" & RefNo & "' "
        DT_Service = CDB.GetDataTable(strSql)
        If DT_Service.Rows.Count > 0 Then
            NOC = CDbl(DT_Service.Rows(0).Item("NOC"))
            NOCTotalCost = CDbl(DT_Service.Rows(0).Item("NOC")) * CDbl(DT_Service.Rows(0).Item("NOCCost"))
        End If
        
        lblUtil = "Util." & DT_Service.Rows(0).Item("INLUtilization").ToString & "%"
        DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("DomesticCost").ToString))
        InternationalCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * CDbl(DT_Service.Rows(0).Item("TransitCost").ToString))
        Caching = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Caching").ToString) * CDbl(DT_Service.Rows(0).Item("AllInternationalCost").ToString))
        CostOfInternet = DomesticCost + InternationalCost + Caching

        PortCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("NetWorkPort").ToString) * CDbl(DT_Service.Rows(0).Item("NetWorkPortCost").ToString))
        NetworkCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("EthernetNetwork").ToString) * (CDbl(DT_Service.Rows(0).Item("NetworkUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("NetworkCost").ToString))
        CostOfNetwork = PortCost + NetworkCost

    End Sub

    Public Function rMailtest(ByVal ProjectName As String, ByVal RefNo As String, ByVal intenal_refno As String _
    , ByVal txtDocumentDate As String, ByVal lblProjectCode As String, ByVal lblCustomerType As String _
    , ByVal txtServiceDate As String, ByVal lblArea As String, ByVal lblCluster As String _
    , ByVal lblTypeOfService As String, ByVal lblCompanyService As String, ByVal lblCustomerName As String _
    , ByVal lblOneTimePayment As String, ByVal lblMonthlyPrice As String, ByVal lblTotalCost As String _
    , ByVal txtSLA As String, ByVal txtMTTR As String, ByVal lblMonitorDate As String _
    , ByVal lblMonitorTime As String, ByVal lblContract As String, ByVal lblCustomerContactEmail As String _
    , ByVal txtDetailService As String, ByVal txtFine As String, ByVal NOC As String _
    , ByVal NOCTotalCost As String, ByVal lblUtil As String _
    , ByVal lblRevenue As String, ByVal Revenue_total As String _
    , ByVal RevenueOne As String, ByVal RevenueNotOne As String _
    , ByVal MKTCostOne As String, ByVal MKTCostNotOne As String _
    , ByVal PenaltyOne As String, ByVal PenaltyNotOne As String _
    , ByVal CostOfInternet As String, ByVal CostOfNetwork As String) As String

        Dim strsql As String
        Dim Left_Table As String
        Dim Right_Table As String
        Dim DT_Service As DataTable
        Dim DT_CAPEX As DataTable
        Dim DT_OPEX As DataTable
        Dim DT_OTHER As DataTable
        Dim DT_Management As DataTable

        Dim total_capex As Double = 0
        Dim total_opex As Double = 0
        Dim total_other As Double = 0
        Dim total_management As Double = 0
        Dim MoneyFmt As String = "###,##0.00"
        Dim MoneyRoundup As String = "###,##0"
        Dim PercentFmt As String = "#0.00"

        strsql = "select * from dbo.List_CAPEX where Document_No = '" + RefNo + "' and Status = '1' "
        DT_CAPEX = CDB.GetDataTable(strsql)
        Left_Table = "<table style='width: 100%'>"
        Left_Table += "<tr>"
        Left_Table += "<td width='75%' align='left'><b><u>" + ProjectName + "</u></b></td>"
        Left_Table += "<td width='10%' align='center'><b><u>Asset</u></b></td>"
        Left_Table += "<td width='15%' align='right'><b><u>บาท</u></b></td>"
        Left_Table += "</tr>"
        For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
            Left_Table += "<tr>"
            Left_Table += "<td>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
            Left_Table += "<td align='center'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
            Left_Table += "<td align='right'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Left_Table += "</tr>"
            total_capex += DT_CAPEX.Rows(i).Item("Cost")
        Next
        Left_Table += "</table>"

        strsql = "select * from dbo.List_OPEX where Document_No = '" + RefNo + "' and Status = '1' "
        DT_OPEX = CDB.GetDataTable(strsql)
        Right_Table = "<table style='width: 100%'>"
        Right_Table += "<tr>"
        Right_Table += "<td width='70%' align='left'><b><u>JASMINE GROUP</u></b></td>"
        Right_Table += "<td width='10%' align='center'><b><u>หน่วย</u></b></td>"
        Right_Table += "<td width='20%' align='right'><b><u>บาท/เดือน</u></b></td>"
        Right_Table += "</tr>"
        For i As Integer = 0 To DT_OPEX.Rows.Count - 1
            Right_Table += "<tr>"
            Right_Table += "<td align='left'>" + DT_OPEX.Rows(i).Item("OPEX_Name").ToString + "</td>"
            Right_Table += "<td align='center'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
            Right_Table += "<td align='right'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Right_Table += "</tr>"
            total_opex += DT_OPEX.Rows(i).Item("Cost")
        Next

        Right_Table += "<tr>"
        Right_Table += "<td align='left'><b><u>Total Cost</u></b></td>"
        Right_Table += "<td></td>"
        Right_Table += "<td align='right'><b><u>" + Format(CDbl(total_opex.ToString), "###,##0.00") + "</u></b></td>"
        Right_Table += "</tr>"
        Right_Table += "<tr Style = 'height: 15px'><td></td><td></td><td></td></tr>"

        strsql = "select * from dbo.List_OTHER where Document_No = '" + RefNo + "' and Status = '1' "
        DT_OTHER = CDB.GetDataTable(strsql)
        Right_Table += "<tr>"
        Right_Table += "<td align='left'><b><u>OTHER</u></b></td>"
        Right_Table += "<td align='center'><b><u>หน่วย</u></b></td>"
        Right_Table += "<td align='right'><b><u>บาท/เดือน</u></b></td>"
        Right_Table += "</tr>"
        For i As Integer = 0 To DT_OTHER.Rows.Count - 1
            Right_Table += "<tr>"
            Right_Table += "<td align='left'>" + DT_OTHER.Rows(i).Item("OTHER_Name").ToString + "</td>"
            Right_Table += "<td align='center'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
            Right_Table += "<td align='right'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Right_Table += "</tr>"
            total_other += DT_OTHER.Rows(i).Item("Cost")
        Next
        Right_Table += "<tr>"
        Right_Table += "<td align='left'><b><u>Total Cost</u></b></td>"
        Right_Table += "<td></td>"
        Right_Table += "<td align='right'><b><u>" + Format(CDbl(total_other.ToString), "###,##0.00") + "</u></b></td>"
        Right_Table += "</tr>"

        Right_Table += "<tr Style = 'height: 15px'><td></td><td></td><td></td></tr>"

        strsql = "select * from dbo.List_Management where Document_No = '" + RefNo + "' and Status = '1' "
        DT_Management = CDB.GetDataTable(strsql)
        Right_Table += "<tr>"
        Right_Table += "<td align='left'><b><u>Management</u></b></td>"
        Right_Table += "<td align='center'><b><u>หน่วย</u></b></td>"
        Right_Table += "<td align='right'><b><u>บาท/เดือน</u></b></td>"
        Right_Table += "</tr>"
        Right_Table += "<tr>"
        Right_Table += "<td align='left'>ค่าบริการ NOC</td>"
        Right_Table += "<td align='center'>" + Format(CDbl(NOC), "###,##0") + "</td>"
        Right_Table += "<td align='right'>" + Format(CDbl(NOCTotalCost), "###,##0.00") + "</td>"
        Right_Table += "</tr>"
        total_management += CDbl(NOCTotalCost)
        For i As Integer = 0 To DT_Management.Rows.Count - 1
            Right_Table += "<tr>"
            Right_Table += "<td align='left'>" + DT_Management.Rows(i).Item("Management_Name").ToString + "</td>"
            Right_Table += "<td align='center'>" + Format(CDbl(DT_Management.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
            Right_Table += "<td align='right'>" + Format(CDbl(DT_Management.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Right_Table += "</tr>"
            total_management += DT_Management.Rows(i).Item("Cost")
        Next
        Right_Table += "<tr>"
        Right_Table += "<td align='left'><b><u>Total Cost</u></b></td>"
        Right_Table += "<td></td>"
        Right_Table += "<td align='right'><b><u>" + Format(CDbl(total_management.ToString), "###,##0.00") + "</u></b></td>"
        Right_Table += "</tr>"
        Right_Table += "</table>"

        Dim lblTotalCAPEX = Format(CDbl(total_capex.ToString), "###,##0.00")
        Dim lblTotalOPEX = Format(CDbl(total_opex.ToString), "###,##0.00")
        Dim lblTotalOTHER = Format(CDbl(total_other.ToString), "###,##0.00")
        Dim lblTotalManagement = Format(CDbl(total_management.ToString), "###,##0.00")

        Dim lblContract_EN = lblContract + " Month"
        Dim totalopex_all = total_opex + total_other + total_management
        Dim JasmineGroup = CDbl(total_opex)

        Dim OPEXOne = CDbl(MKTCostOne) + CDbl(CostOfInternet) + CostOfNetwork + JasmineGroup + lblTotalOTHER + lblTotalManagement + PenaltyOne
        Dim OPEXNotOne = CDbl(MKTCostNotOne) + CDbl(CostOfInternet) + CostOfNetwork + JasmineGroup + CDbl(lblTotalOTHER) + CDbl(lblTotalManagement) + PenaltyNotOne
        Dim lblOPEX = Format(OPEXOne + (OPEXNotOne * (lblContract - 1)), MoneyRoundup)

        Dim lblMKTCost = Format(MKTCostOne + (MKTCostNotOne * (lblContract - 1)), MoneyRoundup)
        Dim lblMKTCost_total = Format(MKTCostOne + (MKTCostNotOne * (lblContract - 1)), MoneyRoundup)
        Dim lblCostOfInternet = Format((CostOfInternet * lblContract), MoneyRoundup)
        Dim lblCostOfNetwork = Format((CostOfNetwork * lblContract), MoneyRoundup)
        Dim lblCostOfNOC = Format(CDbl((total_management) * lblContract), MoneyRoundup)
        Dim lblPenalty = Format(PenaltyOne + (PenaltyNotOne * (lblContract - 1)), MoneyRoundup)
        Dim lblPenalty_total = Format(PenaltyOne + (PenaltyNotOne * (lblContract - 1)), MoneyRoundup)
        Dim lblJasmineGroup = Format((JasmineGroup * lblContract), MoneyRoundup)

        Dim lblOther = Format((CDbl(lblTotalOTHER) * lblContract), MoneyRoundup)
        Dim lblOther_total = Format((CDbl(lblTotalOTHER) * lblContract), MoneyRoundup)
        Dim lblOtherPercent = Format((CDbl(lblOther) / CDbl(lblRevenue)) * 100, PercentFmt) & "%"

        Dim OPEX_total = (MKTCostOne + (MKTCostNotOne * (lblContract - 1))) + lblOther_total + lblPenalty_total
        Dim lblOPEX_total = Format(OPEX_total, MoneyRoundup)

        Dim Revenue_Operation As Double = (RevenueOne + (RevenueNotOne * (lblContract - 1))) - (OPEXOne + (OPEXNotOne * (lblContract - 1)))
        Dim lblRevenue_Operation = Format(Revenue_Operation, MoneyRoundup)
        Dim Revenue_Operationtotal As Double = Revenue_total - OPEX_total
        Dim lblRevenue_Operationtotal = Format(Revenue_Operationtotal, MoneyRoundup)

        Dim lblCAPEX_value = Format(CDbl(lblTotalCAPEX), MoneyRoundup)
        Dim lblCAPEX_total = Format(CDbl(lblTotalCAPEX), MoneyRoundup)
        Dim lblCAPEXPercent = Format((CDbl(lblCAPEX_value) / CDbl(lblRevenue)) * 100, PercentFmt) & "%"

        Dim CashFlow as Double = Revenue_Operation - CDbl(lblTotalCAPEX)
        Dim CashFlow_total As Double = Revenue_Operationtotal - CDbl(lblTotalCAPEX)
        Dim lblCashFlow_value = Format(CDbl(CashFlow.ToString), MoneyRoundup)
        Dim lblCashFlow_total = Format(CDbl(CashFlow_total.ToString), MoneyRoundup)
        'totalopex_all = Format(CDbl(totalopex_all.ToString), "###,##0.00")
    
        Dim RevenueAfterOperationOne As Double
        Dim RevenueAfterOperationNotOne As Double
        Dim CashFlowOne As Double
        Dim CashFlowNotOne As Double
        Dim CashFlowPeriod(lblContract) As Double
        Dim PaybackPeriod(lblContract) As Double 
        Dim lblPayBack As String
        Dim PaybackSum As Double

        RevenueAfterOperationOne = RevenueOne - OPEXOne
        RevenueAfterOperationNotOne = RevenueNotOne - OPEXNotOne

        CashFlowOne = RevenueAfterOperationOne - CDbl(lblTotalCAPEX)
        CashFlowNotOne = RevenueAfterOperationNotOne

            For i As Integer = 0 To CashFlowPeriod.Length - 1
                If i = 0 Then
                    CashFlowPeriod(i) = CashFlowOne
                Else
                    CashFlowPeriod(i) = CashFlowNotOne + CashFlowPeriod(i - 1)
                End If
            Next

            If (RevenueAfterOperationOne + CDbl(lblOneTimePayment)) >= CDbl(lblTotalCAPEX) Then
                lblPayBack = "<=1"
            Else
                For i As Integer = 0 To PaybackPeriod.Length - 1
                    If i = 0 Then
                        PaybackPeriod(i) = 1
                    Else
                        If CashFlowPeriod(i) < 0 Then
                            PaybackPeriod(i) = 1
                        Else
                            If CashFlowPeriod(i - 1) < 0 Then
                                PaybackPeriod(i) = (CashFlowPeriod(i - 1) * (-1)) / CashFlowNotOne
                            Else
                                PaybackPeriod(i) = 0
                            End If
                        End If
                    End If
                Next
                PaybackSum = 0
                For i As Integer = 0 To lblContract - 1
                    PaybackSum = PaybackSum + PaybackPeriod(i)
                Next
                lblPayBack = Format(PaybackSum, MoneyFmt)
            End If

        Dim lblMargin As String = ""
        Dim lblMarginProfit As String = ""
       
        Dim values(lblContract) As Double
            For i As Integer = 0 To lblContract - 1
                If i = 0 Then
                    values(i) = CashFlowOne ' Format(CashFlowOne, MoneyRoundup)
                Else
                    values(i) = CashFlowNotOne 'Format(CashFlowNotOne, MoneyRoundup)
                End If
            Next
        Dim FixedRetRate As Double = 0.05
            ' Calculate net present value.
        Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
        Dim lblNPV = Format(NetPVal, MoneyFmt)
            If NetPVal <> 0 Then
                lblMargin = Format((lblNPV / lblRevenue) * 100, PercentFmt).ToString
            Else
                lblMargin = "0"
            End If

        '/////////////  Marginal Profit ///////////////////

        Dim OPEXProfitOne As Double
        Dim OPEXProfitNotOne As Double
        Dim RevenueAfterOperationProfitOne As Double
        Dim RevenueAfterOperationProfitNotOne As Double
        Dim CashFlowProfitOne As Double
        Dim CashFlowProfitnotOne As Double
        Dim PaybackProfitSum As Double

            OPEXProfitOne = MKTCostOne + CDbl(lblTotalOTHER) + PenaltyOne
            OPEXProfitNotOne = MKTCostNotOne + CDbl(lblTotalOTHER) + PenaltyNotOne
            RevenueAfterOperationProfitOne = RevenueOne - OPEXProfitOne
            RevenueAfterOperationProfitNotOne = RevenueNotOne - OPEXProfitNotOne

            CashFlowProfitOne = RevenueAfterOperationProfitOne - CDbl(lblTotalCAPEX)
            CashFlowProfitnotOne = RevenueAfterOperationProfitNotOne

            Dim CashFlowProfitPeriod(lblContract) As Double
            Dim PaybackProfitPeriod(lblContract) As Double
            Dim lblPayBackProfit As String
            Dim lblNPVProfit As String

            For i As Integer = 0 To CashFlowProfitPeriod.Length - 1
                If i = 0 Then
                    CashFlowProfitPeriod(i) = CashFlowProfitOne
                Else
                    CashFlowProfitPeriod(i) = CashFlowProfitnotOne + CashFlowProfitPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationProfitOne >= CDbl(lblTotalCAPEX) Then
                lblPayBackProfit = "<=1"
            Else
                For i As Integer = 0 To PaybackProfitPeriod.Length - 1
                    If i = 0 Then
                        PaybackProfitPeriod(i) = 1
                    Else
                        If CashFlowProfitPeriod(i) < 0 Then
                            PaybackProfitPeriod(i) = 1
                        Else
                            If CashFlowProfitPeriod(i - 1) < 0 Then
                                PaybackProfitPeriod(i) = (CashFlowProfitPeriod(i - 1) * (-1)) / CashFlowProfitnotOne
                            Else
                                PaybackProfitPeriod(i) = 0
                            End If
                        End If
                    End If
                Next
                PaybackProfitSum = 0
                For i As Integer = 0 To lblContract - 1
                    PaybackProfitSum = PaybackProfitSum + PaybackProfitPeriod(i)
                Next
                lblPayBackProfit = Format(PaybackProfitSum, MoneyFmt)
            End If

            Dim valuesProfit(lblContract) As Double
            For i As Integer = 0 To lblContract - 1
                If i = 0 Then
                    valuesProfit(i) = CashFlowProfitOne 'Format(CashFlowProfitOne, MoneyRoundup)
                Else
                    valuesProfit(i) = CashFlowProfitnotOne 'Format(CashFlowProfitnotOne, MoneyRoundup)
                End If
            Next

            ' Calculate net present value.
            Dim NetPValProfit As Double = NPV(FixedRetRate / 12, valuesProfit)
            lblNPVProfit = Format(NetPValProfit, MoneyFmt)
            If NetPValProfit <> 0 Then
                lblMarginProfit = Format((lblNPVProfit / lblRevenue) * 100, PercentFmt).ToString
            Else
                lblMarginProfit = "0"
            End If

        Dim text_mail as String = ""

text_mail +="<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>"
text_mail +="<html lang='th'>"
text_mail +="<head>"
text_mail +="  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>"
text_mail +="  <meta name='viewport' content='width=device-width, initial-scale=1'>"
text_mail +="  <meta http-equiv='X-UA-Compatible' content='IE=edge'>"
text_mail +="  <meta name='format-detection' content='telephone=no'>"
text_mail +="</head>"
text_mail +="<body style='margin:0; padding:0;' leftmargin='0' topmargin='0' marginwidth='0' marginheight='0'>"
text_mail +="<table style='width: 100%;margin: auto;font-family:'tahoma';font-size:16px;'>"
text_mail +="<tr>"
text_mail +="<td style='border: 1px solid black;padding:4px;'>"
    text_mail +="<table style='width: 100%;border: 1px solid black;margin: 0px 0px 0px 0px;border-bottom-style: none;'>"
        text_mail +="<tr>"
            text_mail +="<td style='text-align: right; height: 10px;'>"
            text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr align='center'>"
            text_mail +="<td style='font-weight: bolder; height: 30px;' valign='top'>"
            text_mail +="แบบอนุมัติแผนงานขยายโครงข่ายและบริการ"
            text_mail +="</td>"
        text_mail +="</tr>"
    text_mail +="</table>"
    text_mail +="<table cellspacing='0' style='width: 100%;border: 1px solid black;border-bottom-style: none;'>"
        text_mail +="<tr class='border border-1 border-dark' valign='top'>"
        text_mail +="<td style='text-align: left; border-bottom: 1px solid black;'>CS Ref.No: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + RefNo + "</td>"
        text_mail +="<td style='text-align: left; border-bottom: 1px solid black;'>Internal Ref.No: </td> "
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + intenal_refno + "</td>"
        text_mail +="<td style='text-align: left; border-bottom: 1px solid black;'>Date: </td> "
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + txtDocumentDate + "</td>"
        text_mail +="</tr>"
        text_mail +="<tr class='border border-1 border-dark' valign='top'>"
        text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>Project Code: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + lblProjectCode + "</td>"
        text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>ประเภทลูกค้า: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + lblCustomerType + "</td>"
        text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>วันเปิดบริการ: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + txtServiceDate + "</td>"
        text_mail +="</tr>"
        text_mail +="<tr class='border border-1 border-dark' valign='top'>"
        text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>Project Name: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + ProjectName + "</td>"
        text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>Area: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + lblArea + "</td>"
        text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>Cluster: </td>"
        text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + lblCluster + "</td>"
        text_mail +="</tr>"
    text_mail +="</table>"
    text_mail +="<table cellspacing='0' style='width: 100%;border-left: 1px solid black;border-right: 1px solid black;border-bottom-style: none;'>"
        text_mail +="<tr valign='top'>"
            text_mail +="<td colspan='5' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
                text_mail +="Type of Contact Service: " + lblTypeOfService + " "
            text_mail +="</td>"
            text_mail +="<td colspan='4' style='text-align: left; border-bottom: 1px solid black; '>"
            text_mail +="Company: " + lblCompanyService + " "
            text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr valign='top'>"
            text_mail +="<td align='left' style='border-bottom: 1px solid black;'>Description</td>"
            text_mail +="<td align='left' colspan='4' style='border-bottom: 1px solid black; border-right: 1px solid black;'>"
            text_mail +="" + ProjectName + " <br/> ชื่อผู้ติดต่อ  " + lblCustomerName + ""
            text_mail +="</td>"
            text_mail +="<td colspan='2' align='left' width='15%'>"
            text_mail +="ค่าใช้จ่ายครั้งเดียว <br/> ค่าใช้จ่ายรายเดือน <br/>"
            text_mail +="</td>"
            text_mail +="<td colspan='2' align='left' width='10%'>"
            text_mail +="" + lblOneTimePayment + " บาท <br/> " + lblMonthlyPrice + " บาท/เดือน <br/> "
            text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr valign='top'>"
            text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>SLA </td>"
            text_mail +="<td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
            text_mail +="" + txtSLA + ""
            text_mail +="</td>"
            text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>MTTR </td>"
            text_mail +="<td style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
            text_mail +="" + txtMTTR + ""
            text_mail +="</td>"
            text_mail +="<td colspan='2' align='left' style='border-bottom: 1px solid black;'>"
            text_mail +="มูลค่าโครงการ "
            text_mail +="</td>"
            text_mail +="<td colspan='2' align='left' style='border-bottom: 1px solid black;'>"
            text_mail +="" + lblTotalCost + " บาท(ไม่รวมภาษีมูลค่าเพิ่ม) <br/>"
            text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr valign='top'>"
            text_mail +="<td style='text-align: left; border-bottom: 1px solid black;'>Monitor Date </td>"
            text_mail +="<td colspan='2' valign='top' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
            text_mail +="" + lblMonitorDate + ""
            text_mail +="</td>"
            text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>Monitor Time </td>"
            text_mail +="<td valign='top' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
            text_mail +="" + lblMonitorTime + ""
            text_mail +="</td>"
            text_mail +="<td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
            text_mail +="สัญญา " + lblContract + " months"
            text_mail +="</td>"
            text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>"
            text_mail +="Email " + lblCustomerContactEmail + ""
            text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr valign='top'>"
            text_mail +="<td style='text-align: left;'>Service</td>"
            text_mail +="<td colspan='8' style='text-align: left;'>" + txtDetailService + "</td>"
        text_mail +="</tr>"
        text_mail +="<tr valign='top'>"
            text_mail +="<td style='text-align: left; border-top: 1px solid black;'>ค่าปรับ</td>"
            text_mail +="<td colspan='8' style='text-align: left; border-top: 1px solid black;'>"
            text_mail +="" + txtFine + ""
            text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr align='left' valign='top' style='left; border-top: 1px solid black; color:white; background: #107763;'>"
          text_mail +="<td colspan='5' style='border-right: 1px solid black;'>"
          text_mail +="<b>งบลงทุน(CAPEX)</b>"
          text_mail +="</td>"
          text_mail +="<td colspan='4'>"
          text_mail +="<b>ค่าใช้จ่ายรายเดือน(OPEX)</b>"
          text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr align='left' valign='top'>"
          text_mail +="<td colspan='5'>"
          text_mail +="" + Left_Table + ""
          text_mail +="</td>"
          text_mail +="<td colspan='4' width='46.5%'>"
          text_mail +="" + Right_Table + ""
          text_mail +="</td>"
        text_mail +="</tr>"
        text_mail +="<tr style='background-color: #FFFF99;' valign='top'>"
           text_mail +="<td align='left' colspan='4' style='border-top: 1px solid black;border-bottom: 1px solid black;'>Total Investment Cost</td>"
           text_mail +="<td align='right' style='border-top: 1px solid black;border-bottom: 1px solid black;'>"
           text_mail +="" + Format(CDbl(total_capex.ToString), "###,##0.00") + ""
           text_mail +="</td>"
           text_mail +="<td align='left' colspan='3' width='10%' style='border-top: 1px solid black;border-bottom: 1px solid black;'>Total Cost</td>"
           text_mail +="<td align='right' style='border-top: 1px solid black;border-bottom: 1px solid black;'>"
           text_mail +="" + Format(CDbl(totalopex_all.ToString), "###,##0.00") + ""
           text_mail +="</td>"
        text_mail +="</tr>"
    text_mail +="</table>"
    text_mail +="<table width='100%' style='border-collapse:collapse;'>"
        text_mail +="<tr>"
            text_mail +="<td colspan='2' width='10%'>Revenue</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>100%</td>"
            text_mail +="<td align='right'>" + lblRevenue + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;' width='10%'>" + Revenue_total + "</td>"
            text_mail +="<td colspan='3' style='background: #87CEFA;'>Cumulative Project</td>"
            text_mail +="<td align='right' style='background: #87CEFA;'>" + lblContract_EN + "</td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3'>OPEX</td>"
            text_mail +="<td align='right'>" + lblOPEX + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblOPEX_total + "</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3' style='color:#6c757d;'>ต้นทุนทางการตลาด(Marketing Cost)</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblMKTCost + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblMKTCost_total + "</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3' style='color:#6c757d;'>ต้นทุน Internet Bandwidth</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblCostOfInternet + "</td>"
            text_mail +="<td align='right' style='background:#6c757d;'></td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3' style='color:#6c757d;'>ต้นทุน Network Bandwidth</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblCostOfNetwork + "</td>"
            text_mail +="<td align='right' style='background:#6c757d;'></td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3' style='color:#6c757d;'>ต้นทุน NOC</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblCostOfNOC + "</td>"
            text_mail +="<td align='right' style='background:#6c757d;'></td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3' style='color:#6c757d;'>EXP. Jasmine Group</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblJasmineGroup + "</td>"
            text_mail +="<td align='right' style='background:#6c757d;'></td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='2' style='color:#6c757d;'>EXP. Other</td>"
            text_mail +="<td align='right' style='color:#6c757d;>" + lblOtherPercent + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblOther + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblOther_total + "</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr>"
            text_mail +="<td colspan='3' style='color:#6c757d;'>Penalty</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblPenalty + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblPenalty_total + "</td>"
            text_mail +="<td colspan='3'></td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblUtil + "</td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td colspan='3'>Revenue After Operation</td>"
            text_mail +="<td align='right'>" + lblRevenue_Operation + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblRevenue_Operationtotal + "</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td colspan='2'>CAPEX</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblCAPEXPercent + "</td>"
            text_mail +="<td align='right'>" + lblCAPEX_value + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblCAPEX_total + "</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td colspan='3'>Cash Flow</td>"
            text_mail +="<td align='right'>" + lblCashFlow_value + "</td>"
            text_mail +="<td align='right' style='color:#6c757d;'>" + lblCashFlow_total + "</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td style='border-right: 1px solid black;border-bottom: 1px solid black;' colspan='4'></td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'>Marginal Profit</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td colspan='3' style='border-left: 1px solid black;border-bottom: 1px solid black;'>payback (months)</td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>"
                text_mail +="" + lblPayBack + ""
            text_mail +="</td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>"
                text_mail +="" + lblPayBackProfit + ""
            text_mail +="</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td colspan='3'  style='border-left: 1px solid black;border-bottom: 1px solid black;'>margin</td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>"
                text_mail +="" + lblMargin + "%"
            text_mail +="</td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>"
                text_mail +="" + lblMarginProfit + "%"
            text_mail +="</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
        text_mail +="<tr class='font-weight-bold'>"
            text_mail +="<td colspan='3' style='border-left: 1px solid black;border-bottom: 1px solid black;'>NPV (5% per year)</td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>"
                text_mail +="" + lblNPV + ""
            text_mail +="</td>"
            text_mail +="<td align='right' style='border-right: 1px solid black;border-bottom: 1px solid black;'>"
                text_mail +="" + lblNPVProfit + ""
            text_mail +="</td>"
            text_mail +="<td colspan='4'></td>"
        text_mail +="</tr>"
    text_mail +="</table>"
    text_mail +="</td>"
    text_mail +="</tr>"
text_mail +="</table>"
text_mail +="</html>"

    return text_mail   

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

