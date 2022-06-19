Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic
Partial Class edit_Summary
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim Cal As New Cls_Calculate
    Dim xRequest_id
    Dim request_status

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        menu_project_name.HRef = "edit_project_name.aspx?request_id=" + xRequest_id
        menu_service.HRef = "edit_service.aspx?request_id=" + xRequest_id
        menu_capex.HRef = "edit_capex.aspx?request_id=" + xRequest_id
        menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
        menu_other.HRef = "edit_other.aspx?request_id=" + xRequest_id
        menu_management.HRef = "edit_management.aspx?request_id=" + xRequest_id

        Dim alert As String
        Dim dt_check As DataTable
        dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (/*request_status = 0 or request_status = 55 or*/ request_status = 110) and Document_No = '" + xRequest_id + "'")
        If dt_check.Rows.Count > 0 Then
            'If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "0" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
            If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                request_status = dt_check.Rows(0).Item("request_status").ToString
                refno.Text = xRequest_id
                Dim DT_ProjectName As DataTable
                Dim DT_Service As New DataTable

                DT_ProjectName = GetTableProjectName()
                DT_Service = GetTableService()

                If DT_ProjectName.Rows.Count > 0 And DT_Service.Rows.Count > 0 Then
                    ShowProjectName(DT_ProjectName)
                    ShowCAPEX_OPEX(DT_Service)
                    ShowService(DT_ProjectName, DT_Service)
                    btnSave.Visible = True
                    btnSaveToEditProject.Visible = True
                    'test.Visible = True
                    test.Visible = False
                Else
                    btnSave.Visible = False
                    btnSaveToEditProject.Visible = False
                    LinkFileDoc.Visible = False
                    test.Visible = False
                End If

            Else
                btnSave.Visible = False
                btnSaveToEditProject.Visible = False
                test.Visible = False
                alert = "AlertNotification('User ไม่มีสิทธิ์ ในการแก้ไขข้อมูลได้!',"
                alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                alert += "});"
                ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
            End If
        Else
            btnSave.Visible = False
            btnSaveToEditProject.Visible = False
            test.Visible = False
            alert = "AlertNotification('โปรเจค " & xRequest_id & " ไม่อยู่ในสถานะ ที่สามารถแก้ไขข้อมูลได้!',"
            alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
            alert += "});"
            ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
        End If
        Set_Menu()


    End Sub

    Public Sub ShowProjectName(ByVal DT_ProjectName As DataTable)

        Dim OneTimePayment As Double
        Dim MonthlyPrice As Double

        If DT_ProjectName.Rows.Count > 0 Then
            'lblProjectName.Text = DT_ProjectName.Rows(0).Item("Project_Name")
            ProjectName.Text = DT_ProjectName.Rows(0).Item("Project_Name")
            txtProjectName.Text = DT_ProjectName.Rows(0).Item("Customer_Name") + "</br>"
            If IsDBNull(DT_ProjectName.Rows(0).Item("Project_Code")) Then
                lblProjectCode.Text = ""
            Else
                lblProjectCode.Text = DT_ProjectName.Rows(0).Item("Project_Code")
            End If

            If IsDBNull(DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")) Or DT_ProjectName.Rows(0).Item("Customer_Contact_Tel") = "" Then
                lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; เบอร์โทร. -"
            Else
                lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; เบอร์โทร. " + DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
            End If

            lblCustomerType.Text = DT_ProjectName.Rows(0).Item("Customer_Type")
            txtDocumentDate.Text = DT_ProjectName.Rows(0).Item("Document_Date")
            txtServiceDate.Text = DT_ProjectName.Rows(0).Item("Service_Date")
            lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
            lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
            lblCustomerContactEmail.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
            lblCompanyService.Text = DT_ProjectName.Rows(0).Item("Company_Service")
            lblTypeOfService.Text = DT_ProjectName.Rows(0).Item("Type_Service")
            lblCustomerAssistantName.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_Name")
            txtDetailProject.Text = Replace(DT_ProjectName.Rows(0).Item("Detail_Project"), Environment.NewLine, "<br />")
            txtDetailService.Text = Replace(DT_ProjectName.Rows(0).Item("Detail_Service"), Environment.NewLine, "<br />")
            txtSLA.Text = DT_ProjectName.Rows(0).Item("SLA")
            txtMTTR.Text = DT_ProjectName.Rows(0).Item("MTTR")
            lblMonitorDate.Text = DT_ProjectName.Rows(0).Item("Monitor_Date")
            lblMonitorTime.Text = DT_ProjectName.Rows(0).Item("Monitor_Time")
            txtFine.Text = Replace(DT_ProjectName.Rows(0).Item("Fine"), Environment.NewLine, "<br />")
            lblSpecialPrice.Text = DT_ProjectName.Rows(0).Item("Special_Price")
            lblSpecialApprove.Text = DT_ProjectName.Rows(0).Item("SpecialApprove")

            Dim showPenalty As String = ""
            If CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString) = "0" Then
                showPenalty += ""
                lblPenaltyLate.Text = "0"
            Else
                showPenalty += "ประเมินค่าปรับส่งมอบล่าช้า " & Format(CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString), "###,##0.00") & " บาท<br />"
                lblPenaltyLate.Text = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString)
            End If
            If CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) = "0" Then
                showPenalty += ""
            Else
                showPenalty += "ประเมินค่าปรับงานซ่อม " & Format(CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString), "###,##0.00") & "%<br />"
            End If
            txtFine.Text = showPenalty + txtFine.Text

            lblContract.Text = DT_ProjectName.Rows(0).Item("Contract").ToString
            lblContract_EN.Text = DT_ProjectName.Rows(0).Item("Contract").ToString + " Month"
            MonthlyPrice = CDbl(DT_ProjectName.Rows(0).Item("Monthly").ToString)
            OneTimePayment = CDbl(DT_ProjectName.Rows(0).Item("OneTimePayment").ToString)
            lblMonthlyPrice.Text = Format(CDbl(MonthlyPrice), "###,##0.00")
            lblOneTimePayment.Text = Format(CDbl(OneTimePayment), "###,##0.00")
            lblTotalCost.Text = Format(CDbl(DT_ProjectName.Rows(0).Item("TotalProject").ToString), "###,##0.00")
            lblGuarantee.Text = Format(CDbl(DT_ProjectName.Rows(0).Item("TotalProject").ToString) * (CDbl(DT_ProjectName.Rows(0).Item("ContractGuarantee")) / 100), "###,##0.00")

            Dim DT_RO_Director As DataTable
            DT_RO_Director = C.GetDataTable("select * from RO_Director where RO = '" + DT_ProjectName.Rows(0).Item("Area") + "' and PermissionType = 'RO' ")
            If DT_RO_Director.Rows.Count > 0 Then
                lblROMail.Text = DT_RO_Director.Rows(0).Item("RO_email")
            Else
                lblROMail.Text = ""
            End If

            Dim DT_Presale As DataTable
            DT_Presale = C.GetDataTable("select * from RO_Director where RO = '" + DT_ProjectName.Rows(0).Item("Area") + "' and PermissionType = 'Presale' ")
            If DT_Presale.Rows.Count > 0 Then
                For i As Integer = 0 To DT_Presale.Rows.Count - 1
                    If i = 0 Then
                        lblPresaleMail.Text = DT_Presale.Rows(i).Item("RO_email")
                    Else
                        lblPresaleMail.Text += ";" & DT_Presale.Rows(i).Item("RO_email")
                    End If
                Next
            Else
                lblPresaleMail.Text = ""
            End If

            lblPrepaireEmail.Text = DT_ProjectName.Rows(0).Item("Cluster_email")

            If DT_ProjectName.Rows(0).Item("Doc_File") <> "" Then
                LinkFileDoc.NavigateUrl = "~/Upload/" & DT_ProjectName.Rows(0).Item("Doc_File")
                LinkFileDoc.Target = "_blank"
            Else
                LinkFileDoc.Visible = False
            End If
        Else
            lblProjectName.Text = "-"
        End If

        If txtDocumentDate.Text = "" Then
            txtDocumentDate.Text = "&nbsp;"
        End If

    End Sub

    Public Sub ShowCAPEX_OPEX(ByVal DT_Service As DataTable)
        Dim strsql As String
        Dim DT_CAPEX As DataTable
        'Dim DT_CAPEX_Mass As DataTable
        Dim DT_OPEX As DataTable
        Dim DT_OTHER As DataTable
        Dim DT_Management As DataTable

        Dim r As String
        Dim total_capex As Double = 0
        Dim total_capex_mass As Double = 0
        Dim total_opex As Double = 0
        Dim total_other As Double = 0
        Dim total_management As Double = 0
        Dim NOC As Double
        Dim NOCTotalCost As Double

        If DT_Service.Rows.Count > 0 Then
            NOC = CDbl(DT_Service.Rows(0).Item("NOC"))
            NOCTotalCost = CDbl(DT_Service.Rows(0).Item("NOC")) * CDbl(DT_Service.Rows(0).Item("NOCCost"))
        Else
            NOC = 0
            NOCTotalCost = 0
        End If

        strsql = "select * from dbo.List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_CAPEX = C.GetDataTable(strsql)
        r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        r += "<td width='65%' align='left'><b><u>" + ProjectName.Text + "</u></b></td>"
        r += "<td width='10%' align='center'><b><u>Asset</u></b></td>"
        r += "<td width='10%' align='center'><b><u>หน่วย</u></b></td>"
        r += "<td width='15%' align='right'><b><u>บาท</u></b></td>"
        r += "</tr>"
        For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_CAPEX.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_CAPEX.Rows(i).Item("Initial_Cost_perUnit").ToString)) Then
                r += "<td style='color: Red'>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
                r += "<td align='center' style='color: Red'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
                r += "<td align='center' style='color: Red'>" + DT_CAPEX.Rows(i).Item("Number").ToString + "</td>"
                r += "<td align='right' style='color: Red'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Else
                r += "<td>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
                r += "<td align='center'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
                r += "<td align='center'>" + DT_CAPEX.Rows(i).Item("Number").ToString + "</td>"
                r += "<td align='right'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            End If
            r += "</tr>"
            total_capex += DT_CAPEX.Rows(i).Item("Cost")
        Next
        r += "</table>"
        CAPEX_Detail.InnerHtml = r


        strsql = "select * from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OPEX = C.GetDataTable(strsql)
        r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        r += "<td width='70%' align='left'><u>JASMINE GROUP</u></td>"
        r += "<td width='15%' align='center'><u>หน่วย<u></td>"
        r += "<td width='15%' align='right'><u>บาท/เดือน<u></td>"
        r += "</tr>"
        For i As Integer = 0 To DT_OPEX.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_OPEX.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_OPEX.Rows(i).Item("Initial_Cost_perUnit").ToString)) Then
                r += "<td align='left' style='color: Red;'>" + DT_OPEX.Rows(i).Item("OPEX_Name").ToString + "</td>"
                r += "<td align='center' style='color: Red;'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right' style='color: Red;'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Else
                r += "<td align='left'>" + DT_OPEX.Rows(i).Item("OPEX_Name").ToString + "</td>"
                r += "<td align='center'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            End If
            r += "</tr>"
            total_opex += DT_OPEX.Rows(i).Item("Cost")
        Next
        
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><b><u>Total Cost</u></b></td>"
        r += "<td></td>"
        r += "<td align='right'><b><u>" + Format(CDbl(total_opex.ToString), "###,##0.00") + "</u></b></td>"
        r += "</tr>"


        r += "<tr Style = 'height: 15px'><td></td><td></td><td></td></tr>"

        strsql = "select * from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OTHER = C.GetDataTable(strsql)
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><u>OTHER</u></td>"
        r += "<td align='center'><u>หน่วย<u></td>"
        r += "<td align='right'><u>บาท/เดือน<u></td>"
        r += "</tr>"
        For i As Integer = 0 To DT_OTHER.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_OTHER.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_OTHER.Rows(i).Item("Initial_Cost_perUnit").ToString)) Then
                r += "<td align='left' style='color: Red'>" + DT_OTHER.Rows(i).Item("OTHER_Name").ToString + "</td>"
                r += "<td align='center' style='color: Red'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right' style='color: Red'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Else
                r += "<td align='left'>" + DT_OTHER.Rows(i).Item("OTHER_Name").ToString + "</td>"
                r += "<td align='center'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            End If
            r += "</tr>"
            total_other += DT_OTHER.Rows(i).Item("Cost")
        Next
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><b><u>Total Cost</u></b></td>"
        r += "<td></td>"
        r += "<td align='right'><b><u>" + Format(CDbl(total_other.ToString), "###,##0.00") + "</u></b></td>"
        r += "</tr>"

        r += "<tr Style = 'height: 15px'><td></td><td></td><td></td></tr>"

        strsql = "select * from dbo.List_Management where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_Management = C.GetDataTable(strsql)
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><u>Management</u></td>"
        r += "<td align='center'><u>หน่วย<u></td>"
        r += "<td align='right'><u>บาท/เดือน<u></td>"
        r += "</tr>"
        r += "<tr>"
        r += "<td align='left'>ค่าบริการ NOC</td>"
        r += "<td align='center'>" + Format(CDbl(DT_Service.Rows(0).Item("NOC")), "###,##0") + "</td>"
        r += "<td align='right'>" + Format(CDbl(NOCTotalCost), "###,##0.00") + "</td>"
        r += "</tr>"
        total_management += CDbl(NOCTotalCost)
        For i As Integer = 0 To DT_Management.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_Management.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_Management.Rows(i).Item("Initial_Cost_perUnit").ToString)) Then
                r += "<td align='left' style='color: Red'>" + DT_Management.Rows(i).Item("Management_Name").ToString + "</td>"
                r += "<td align='center' style='color: Red'>" + Format(CDbl(DT_Management.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right' style='color: Red'>" + Format(CDbl(DT_Management.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Else
                r += "<td align='left'>" + DT_Management.Rows(i).Item("Management_Name").ToString + "</td>"
                r += "<td align='center'>" + Format(CDbl(DT_Management.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right'>" + Format(CDbl(DT_Management.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            End If
            r += "</tr>"
            total_management += DT_Management.Rows(i).Item("Cost")
        Next
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><b><u>Total Cost</u></b></td>"
        r += "<td></td>"
        r += "<td align='right'><b><u>" + Format(CDbl(total_management.ToString), "###,##0.00") + "</u></b></td>"
        r += "</tr>"

        r += "</table>"

        OPEX_Detail.InnerHtml = r

        lblNOCTotalCost.Text = total_management
        lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")
        lblTotalOPEX.Text = Format(CDbl(total_opex.ToString), "###,##0.00")
        lblTotalOTHER.Text = Format(CDbl(total_other.ToString), "###,##0.00")
        lblTotalManagement.Text = Format(CDbl(total_management.ToString), "###,##0.00")

        Dim totalopex_all = total_opex + total_other + total_management
        lblTotalOPEXALL.Text = Format(CDbl(totalopex_all.ToString), "###,##0.00")

        lblUtil.Text = "Util." & DT_Service.Rows(0).Item("INLUtilization").ToString & "%"

    End Sub

    Public Sub ShowService(ByVal DT_ProjectName As DataTable, ByVal DT_Service As DataTable)

        If DT_Service.Rows.Count > 0 Then
            Dim MoneyFmt As String = "###,##0.00"
            Dim MoneyRoundup As String = "###,##0"
            ' Define percentage format.
            Dim PercentFmt As String = "#0.00"
            'Dim values(lblContract.Text) As Double

            Dim Revenue As Double
            Dim MKTCost As Double
            Dim RevenueOneTime As Double
            Dim MKTCostOneTime As Double
            Dim Vas As Double = 0
            Dim CashFlow As Double
            Dim CashFlowOneTime As Double

            Dim RevenueOne As Double
            Dim RevenueNotOne As Double
            Dim NetRevenueOne As Double
            Dim NetRevenueNotOne As Double
            Dim JasmineGroup As Double = 0
            Dim Other As Double = 0
            Dim CAPEXCorp As Double = 0
            Dim CAPEXMass As Double = 0
            Dim MKTCostOne As Double
            Dim MKTCostNotOne As Double
            Dim Budget1per As Double
            Dim MarketingPriceOne As Double
            Dim MarketingPriceNotOne As Double
            Dim EntertainPriceOne As Double
            Dim EntertainPriceNotOne As Double
            Dim GiftPrice As Double
            Dim CostOfInternet As Double
            Dim DomesticCost As Double
            Dim InternationalCost As Double
            Dim Caching As Double
            Dim CostOfNetwork As Double
            'Dim CostOfNOC As Double
            Dim NetworkCost As Double
            Dim PortCost As Double
            Dim PenaltyOne As Double
            Dim PenaltyNotOne As Double
            Dim OPEXOne As Double
            Dim OPEXNotOne As Double
            Dim RevenueAfterOperationOne As Double
            Dim RevenueAfterOperationNotOne As Double
            Dim CashFlowOne As Double
            Dim CashFlowNotOne As Double
            Dim PaybackSum As Double
            Dim SparePart As Double
            Dim SparePartPercent As Double = 0.01

            RevenueOne = CDbl(lblMonthlyPrice.Text) + CDbl(lblOneTimePayment.Text)
            RevenueNotOne = CDbl(lblMonthlyPrice.Text)

            'NetRevenueOne = RevenueOne - JasmineGroup - Other - CAPEXMass
            'NetRevenueNotOne = RevenueNotOne - JasmineGroup - Other

            'If OneTimePayment = 0 Then
            '    Budget1per = RevenueOne * 0.01
            'Else
            '    Budget1per = (OneTimePayment / DT_Service.Rows(0).Item("Contract").ToString) * 0.01
            'End If
            MKTCostOne = ((CDbl(DT_ProjectName.Rows(0).Item("Marketing").ToString) / 100) * RevenueOne) + ((CDbl(DT_ProjectName.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueOne) + CDbl(DT_ProjectName.Rows(0).Item("Gift").ToString)
            MKTCostNotOne = ((CDbl(DT_ProjectName.Rows(0).Item("Marketing").ToString) / 100) * RevenueNotOne) + ((CDbl(DT_ProjectName.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueNotOne)

            MarketingPriceOne = ((CDbl(DT_ProjectName.Rows(0).Item("Marketing").ToString) / 100) * RevenueOne)
            MarketingPriceNotOne = ((CDbl(DT_ProjectName.Rows(0).Item("Marketing").ToString) / 100) * RevenueNotOne)

            EntertainPriceOne = ((CDbl(DT_ProjectName.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueOne)
            EntertainPriceNotOne = ((CDbl(DT_ProjectName.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueNotOne)

            GiftPrice = CDbl(DT_ProjectName.Rows(0).Item("Gift").ToString)

            DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("DomesticCost").ToString))
            InternationalCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * CDbl(DT_Service.Rows(0).Item("TransitCost").ToString))
            Caching = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Caching").ToString) * CDbl(DT_Service.Rows(0).Item("AllInternationalCost").ToString))
            CostOfInternet = DomesticCost + InternationalCost + Caching

            PortCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("NetWorkPort").ToString) * CDbl(DT_Service.Rows(0).Item("NetWorkPortCost").ToString))
            NetworkCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("EthernetNetwork").ToString) * (CDbl(DT_Service.Rows(0).Item("NetworkUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("NetworkCost").ToString))
            CostOfNetwork = PortCost + NetworkCost

            'CostOfNOC = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("NOC").ToString) * CDbl(DT_Service.Rows(0).Item("NOCCost").ToString))
            JasmineGroup = CDbl(lblTotalOPEX.Text) ' - CostOfNOC
            PenaltyOne = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString) + ((CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) / 100) * RevenueOne)
            PenaltyNotOne = (CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) / 100) * RevenueNotOne

            OPEXOne = MKTCostOne + CostOfInternet + CostOfNetwork + JasmineGroup + CDbl(lblTotalOTHER.Text) + CDbl(lblTotalManagement.Text) + PenaltyOne
            OPEXNotOne = MKTCostNotOne + CostOfInternet + CostOfNetwork + JasmineGroup + CDbl(lblTotalOTHER.Text) + CDbl(lblTotalManagement.Text) + PenaltyNotOne


            RevenueAfterOperationOne = RevenueOne - OPEXOne
            RevenueAfterOperationNotOne = RevenueNotOne - OPEXNotOne

            CashFlowOne = RevenueAfterOperationOne - CDbl(lblTotalCAPEX.Text)
            CashFlowNotOne = RevenueAfterOperationNotOne

            Dim CashFlowPeriod(lblContract.Text) As Double
            Dim PaybackPeriod(lblContract.Text) As Double

            For i As Integer = 0 To CashFlowPeriod.Length - 1
                If i = 0 Then
                    CashFlowPeriod(i) = CashFlowOne
                Else
                    CashFlowPeriod(i) = CashFlowNotOne + CashFlowPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationOne >= CDbl(lblTotalCAPEX.Text) Then
                lblPayBack.Text = "<=1"
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
                For i As Integer = 0 To lblContract.Text - 1
                    PaybackSum = PaybackSum + PaybackPeriod(i)
                Next
                lblPayBack.Text = Format(PaybackSum, MoneyFmt)
            End If

            lblRevenue.Text = Format(RevenueOne + (RevenueNotOne * (lblContract.Text - 1)), MoneyRoundup)
            Dim Revenue_total = RevenueOne + (RevenueNotOne * (lblContract.Text - 1))
            lblRevenue_total.Text = Format(Revenue_total, MoneyRoundup)
            lblRevenuePercent.Text = "100%"

            'lblNetRevenue.Text = Format(NetRevenueOne + (NetRevenueNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
            'lblCAPEX.Text = Format(CAPEXCorp + CAPEXMass, MoneyRoundup)
            'lblCAPEXCorp.Text = Format(CAPEXCorp, MoneyRoundup)
            'lblCAPEXMass.Text = Format(CAPEXMass, MoneyRoundup)
            'lblCashFlow.Text = Format(CashFlowOne + (CashFlowNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)


            lblOPEX.Text = Format(OPEXOne + (OPEXNotOne * (lblContract.Text - 1)), MoneyRoundup)

            lblMKTCost.Text = Format(MKTCostOne + (MKTCostNotOne * (lblContract.Text - 1)), MoneyRoundup)
            lblMKTCost_total.Text = Format(MKTCostOne + (MKTCostNotOne * (lblContract.Text - 1)), MoneyRoundup)
            lblMarketing.Text = Format(MarketingPriceOne + (MarketingPriceNotOne * (lblContract.Text - 1)), MoneyRoundup)
            lblMarketing_profit.Text = lblMarketing.Text
            lblEntertain.Text = Format(EntertainPriceOne + (EntertainPriceNotOne * (lblContract.Text - 1)), MoneyRoundup)
            lblEntertain_profit.Text = lblEntertain.Text
            lblGift.Text = Format(GiftPrice, MoneyRoundup)
            lblGift_profit.Text = lblGift.Text

            lblEntertainGift.Text = Format(EntertainPriceOne + (EntertainPriceNotOne * (lblContract.Text - 1)) + GiftPrice, MoneyRoundup)
            lblEntertainGift_profit.Text = lblEntertainGift.Text
            tr_gift.Visible = False
           

            lblInternetBW.Text = DT_Service.Rows(0).Item("Domestic").ToString & "M/" & DT_Service.Rows(0).Item("International").ToString & "M"
            lblNetworkBW.Text = DT_Service.Rows(0).Item("EthernetNetwork").ToString & "M"

            lblCostOfInternet.Text = Format(CostOfInternet * lblContract.Text, MoneyRoundup)
            lblCostOfNetwork.Text = Format(CostOfNetwork * lblContract.Text, MoneyRoundup)
            lblCostOfNOC.Text = Format(CDbl(lblTotalManagement.Text) * lblContract.Text, MoneyRoundup)
            lblPenalty.Text = Format(PenaltyOne + (PenaltyNotOne * (lblContract.Text - 1)), MoneyRoundup)
            lblPenalty_total.Text = Format(PenaltyOne + (PenaltyNotOne * (lblContract.Text - 1)), MoneyRoundup)
            lblJasmineGorup.Text = Format(JasmineGroup * lblContract.Text, MoneyRoundup)

            lblOther.Text = Format(CDbl(lblTotalOTHER.Text) * lblContract.Text, MoneyRoundup)
            lblOther_total.Text = Format(CDbl(lblTotalOTHER.Text) * lblContract.Text, MoneyRoundup)
            lblOtherPercent.Text = Format((CDbl(lblOther.Text) / CDbl(lblRevenue.Text)) * 100, PercentFmt) & "%"

            Dim OPEX_total = (MKTCostOne + (MKTCostNotOne * (lblContract.Text - 1))) + lblOther_total.Text + lblPenalty_total.Text
            lblOPEX_total.Text = Format(OPEX_total, MoneyRoundup)

            Dim Revenue_Operation As Double = (RevenueOne + (RevenueNotOne * (lblContract.Text - 1))) - (OPEXOne + (OPEXNotOne * (lblContract.Text - 1)))
            '(RevenueAfterOperationOne * (DT_Service.Rows(0).Item("Contract").ToString - 1), MoneyRoundup)
            lblRevenue_Operation.Text = Format(Revenue_Operation, MoneyRoundup)
            Dim Revenue_Operationtotal As Double = Revenue_total - OPEX_total
            lblRevenue_Operationtotal.Text = Format(Revenue_Operationtotal, MoneyRoundup)

            lblCAPEX_value.Text = Format(CDbl(lblTotalCAPEX.Text), MoneyRoundup)
            lblCAPEX_total.Text = Format(CDbl(lblTotalCAPEX.Text), MoneyRoundup)
            lblCAPEXPercent.Text = Format((CDbl(lblCAPEX_value.Text) / CDbl(lblRevenue.Text)) * 100, PercentFmt) & "%"

            CashFlow = Revenue_Operation - CDbl(lblTotalCAPEX.Text)
            Dim CashFlow_total As Double = Revenue_Operationtotal - CDbl(lblTotalCAPEX.Text)
            lblCashFlow_value.Text = Format(CDbl(CashFlow.ToString), MoneyRoundup)
            lblCashFlow_total.Text = Format(CDbl(CashFlow_total.ToString), MoneyRoundup)

            Dim values(lblContract.Text) As Double
            For i As Integer = 0 To lblContract.Text - 1
                If i = 0 Then
                    values(i) = CashFlowOne ' Format(CashFlowOne, MoneyRoundup)
                Else
                    values(i) = CashFlowNotOne 'Format(CashFlowNotOne, MoneyRoundup)
                End If
            Next
            Dim FixedRetRate As Double = 0.05
            ' Calculate net present value.
            Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
            lblNPV.Text = Format(NetPVal, MoneyFmt)
            If NetPVal <> 0 And lblRevenue.Text <> 0 Then
                lblMargin.Text = Format((lblNPV.Text / lblRevenue.Text) * 100, PercentFmt).ToString
            Else
                lblMargin.Text = "0"
            End If

            '/////////////  Marginal Profit ///////////////////

            Dim OPEXProfitOne As Double
            Dim OPEXProfitNotOne As Double
            Dim RevenueAfterOperationProfitOne As Double
            Dim RevenueAfterOperationProfitNotOne As Double
            Dim CashFlowProfitOne As Double
            Dim CashFlowProfitnotOne As Double
            Dim PaybackProfitSum As Double

            OPEXProfitOne = MKTCostOne + CDbl(lblTotalOTHER.Text) + PenaltyOne
            OPEXProfitNotOne = MKTCostNotOne + CDbl(lblTotalOTHER.Text) + PenaltyNotOne
            RevenueAfterOperationProfitOne = RevenueOne - OPEXProfitOne
            RevenueAfterOperationProfitNotOne = RevenueNotOne - OPEXProfitNotOne

            CashFlowProfitOne = RevenueAfterOperationProfitOne - CDbl(lblTotalCAPEX.Text)
            CashFlowProfitnotOne = RevenueAfterOperationProfitNotOne

            Dim CashFlowProfitPeriod(lblContract.Text) As Double
            Dim PaybackProfitPeriod(lblContract.Text) As Double

            For i As Integer = 0 To CashFlowProfitPeriod.Length - 1
                If i = 0 Then
                    CashFlowProfitPeriod(i) = CashFlowProfitOne
                Else
                    CashFlowProfitPeriod(i) = CashFlowProfitnotOne + CashFlowProfitPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationProfitOne >= CDbl(lblTotalCAPEX.Text) Then
                lblPayBackProfit.Text = "<=1"
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
                For i As Integer = 0 To lblContract.Text - 1
                    PaybackProfitSum = PaybackProfitSum + PaybackProfitPeriod(i)
                Next
                lblPayBackProfit.Text = Format(PaybackProfitSum, MoneyFmt)
            End If

            Dim valuesProfit(lblContract.Text) As Double
            For i As Integer = 0 To lblContract.Text - 1
                If i = 0 Then
                    valuesProfit(i) = CashFlowProfitOne 'Format(CashFlowProfitOne, MoneyRoundup)
                Else
                    valuesProfit(i) = CashFlowProfitnotOne 'Format(CashFlowProfitnotOne, MoneyRoundup)
                End If
            Next

            ' Calculate net present value.
            Dim NetPValProfit As Double = NPV(FixedRetRate / 12, valuesProfit)
            lblNPVProfit.Text = Format(NetPValProfit, MoneyFmt)
            If NetPValProfit <> 0 And lblRevenue.Text <> 0 Then
                lblMarginProfit.Text = Format((lblNPVProfit.Text / lblRevenue.Text) * 100, PercentFmt).ToString
            Else
                lblMarginProfit.Text = "0"
            End If
        End If
    End Sub

    Public Function GetTableService() As DataTable
        Dim strSql As String
        Dim DT_Service As DataTable

        strSql = "select * from dbo.List_Service where Document_No = '" & xRequest_id & "' "
        DT_Service = C.GetDataTable(strSql)
        Return DT_Service
    End Function

    Public Function GetTableProjectName() As DataTable
        Dim strSql As String
        Dim DT_ProjectName As DataTable
        strSql = "select * from List_ProjectName where Document_No = '" & xRequest_id & "' "
        DT_ProjectName = C.GetDataTable(strSql)
        Return DT_ProjectName
    End Function

    Public Function GetTableSummary() As DataTable
        Dim strSql As String
        Dim DT_Summary As DataTable
        strSql = "select * from List_Summary where Document_No = '" & xRequest_id & "' "
        DT_Summary = C.GetDataTable(strSql)
        Return DT_Summary
    End Function

    Public Function CalculateOverMinMax(ByVal Cost_perUnit As Double, ByVal Initial_Costper_Unit As Double) As Boolean
        Dim strSql As String
        Dim MinMax As Double = 0
        Dim DT As New DataTable
        strSql = "select * from dbo.InternetBWCost BWcost inner join dbo.CustomerType Cus on BWcost.BWCostType = Cus.BWCostType where CustomerType_name = '" & lblCustomerType.Text & "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            MinMax = CDbl(DT.Rows(0).Item("MinMaxDiffCost")) / 100
        End If
        If Initial_Costper_Unit = 0 Then
            Return False
        ElseIf (Cost_perUnit - Initial_Costper_Unit) / Initial_Costper_Unit > MinMax Or (Cost_perUnit - Initial_Costper_Unit) / Initial_Costper_Unit < (MinMax * (-1)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        UpdateData("SubmitApprove")
    End Sub

    Protected Sub btnSaveToEditProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveToEditProject.Click
        UpdateData("SaveProject")
    End Sub

    Public Sub UpdateData(ByVal Command As String)
        Dim DT_CAPEX As New DataTable
        Dim DT_CAPEX_Mass As New DataTable
        Dim DT_OPEX As New DataTable
        Dim DT_OTHER As New DataTable
        Dim DT_Service As New DataTable
        Dim DT_Summary As New DataTable
        Dim DT_ProjectName As New DataTable
        Dim DT_ProjectName_File As New DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim TypeService As String
        Dim DT_flow_id As New DataTable

        strSql = "select * from List_ProjectName where /*CreateBy = '" + Session("uemail") + "' and*/ Document_No = '" + xRequest_id + "' "
        DT_ProjectName = C.GetDataTable(strSql)
        strSql = "select * from List_CAPEX where /*CreateBy = '" + Session("uemail") + "' and*/ Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_CAPEX = C.GetDataTable(strSql)
        strSql = "select * from List_CAPEX_Mass where /*CreateBy = '" + Session("uemail") + "' and*/ Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_CAPEX_Mass = C.GetDataTable(strSql)
        strSql = "select * from List_OPEX where /*CreateBy = '" + Session("uemail") + "' and*/ Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OPEX = C.GetDataTable(strSql)
        strSql = "select * from List_OTHER where /*CreateBy = '" + Session("uemail") + "' and*/ Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OTHER = C.GetDataTable(strSql)
        strSql = "select * from List_Service where /*CreateBy = '" + Session("uemail") + "' and*/ Document_No = '" + xRequest_id + "' "
        DT_Service = C.GetDataTable(strSql)

        DT_Summary = GetTableSummary()

        If DT_ProjectName.Rows.Count <= 0 Then
            'ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล Project Name');focus();window.location.href='edit_project_name.aspx?request_id=" + xRequest_id + "';", True)
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล Project Name',function(){ window.location.href='edit_project_name.aspx?request_id=" + xRequest_id + "'; });", True)

            'ElseIf DT_CAPEX.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล CAPEX');focus();window.location.href='edit_capex.aspx?request_id=" + xRequest_id + "';", True)
            'ElseIf DT_OPEX.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล OPEX');focus();window.location.href='edit_opex.aspx?request_id=" + xRequest_id + "';", True)
        ElseIf DT_Service.Rows.Count <= 0 Then
            'ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล Service');focus();window.location.href='edit_service.aspx?request_id=" + xRequest_id + "';", True)
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล Service',function(){ window.location.href='edit_service.aspx?request_id=" + xRequest_id + "'; });", True)
            'ElseIf txtDocumentDate.Text = "" Or txtDocumentDate.Text.Length <> 10 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ วันที่จัดทำ/ปรับปรุง');", True)
            '    txtDocumentDate.Focus()
            'ElseIf txtServiceDate.Text = "" Or txtServiceDate.Text.Length <> 10 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ วันที่เริ่มให้บริการ');", True)
            '    txtServiceDate.Focus()
        Else
            If DT_Summary.Rows.Count > 0 Then
                strSql = "Update List_Summary set Revenue = '" & CDbl(lblRevenue.Text) & "', Revenue_Profit = '" & CDbl(lblRevenue_total.Text) & "', " + vbCr
                strSql += "OPEX = '" & CDbl(lblOPEX.Text) & "', OPEX_Profit = '" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                strSql += "MarketingPrice = '" & CDbl(lblMarketing.Text) & "',MarketingPrice_Profit = '" & CDbl(lblMarketing_profit.Text) & "', " + vbCr
                strSql += "EntertainPrice = '" & CDbl(lblEntertain.Text) & "', EntertainPrice_Profit = '" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                strSql += "GiftPrice = '" & CDbl(lblGift.Text) & "',GiftPrice_Profit = '" & CDbl(lblGift_profit.Text) & "', " + vbCr
                strSql += "InternetBW = '" & lblInternetBW.Text & "',NetworkBW = '" & lblNetworkBW.Text & "', " + vbCr
                strSql += "MarketingCost = '" & CDbl(lblMKTCost.Text) & "', MarketingCost_Profit = '" & CDbl(lblMKTCost_total.Text) & "', " + vbCr
                strSql += "InternetCost = '" & CDbl(lblCostOfInternet.Text) & "', NetworkCost = '" & CDbl(lblCostOfNetwork.Text) & "', " + vbCr
                strSql += "NOCCost = '" & CDbl(lblCostOfNOC.Text) & "', JasmineGroupCost = '" & CDbl(lblJasmineGorup.Text) & "', " + vbCr
                strSql += "OtherCost = '" & CDbl(lblOther.Text) & "', OtherCost_Profit = '" & CDbl(lblOther_total.Text) & "', " + vbCr
                strSql += "PenaltyCost = '" & CDbl(lblPenalty.Text) & "', PenaltyCost_Profit = '" & CDbl(lblPenalty_total.Text) & "', " + vbCr
                strSql += "RevenueAfter = '" & CDbl(lblRevenue_Operation.Text) & "', RevenueAfter_Profit = '" & CDbl(lblRevenue_Operationtotal.Text) & "', " + vbCr
                strSql += "CAPEX = '" & CDbl(lblCAPEX_value.Text) & "', CAPEX_Profit = '" & CDbl(lblCAPEX_total.Text) & "', " + vbCr
                strSql += "CashFlow = '" & CDbl(lblCashFlow_value.Text) & "', CashFlow_Profit = '" & CDbl(lblCashFlow_total.Text) & "', " + vbCr
                If lblPayBack.Text = "<=1" Then
                    strSql += "Payback = '-1', " + vbCr
                Else
                    strSql += "Payback = '" & CDbl(lblPayBack.Text) & "', "
                End If
                If lblPayBackProfit.Text = "<=1" Then
                    strSql += "Payback_Profit = '-1', " + vbCr
                Else
                    strSql += "Payback_Profit = '" & CDbl(lblPayBackProfit.Text) & "', " + vbCr
                End If
                strSql += "Margin = '" & CDbl(lblMargin.Text) & "', Margin_Profit = '" & CDbl(lblMarginProfit.Text) & "', " + vbCr
                strSql += "NPV = '" & CDbl(lblNPV.Text) & "', NPV_Profit = '" & CDbl(lblNPVProfit.Text) & "', " + vbCr
                strSql += "Status = '1', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() " + vbCr
                strSql += "where Document_No = '" & xRequest_id & "' "
                C.ExecuteNonQuery(strSql)
            Else
                strSql = "Insert Into List_Summary (Document_No,Revenue,Revenue_Profit,OPEX,OPEX_Profit, " + vbCr
                strSql += "MarketingPrice,MarketingPrice_Profit,EntertainPrice,EntertainPrice_Profit, " + vbCr
                strSql += "GiftPrice,GiftPrice_Profit,InternetBW,NetworkBW, " + vbCr
                strSql += "MarketingCost,MarketingCost_Profit,InternetCost,NetworkCost,NOCCost, " + vbCr
                strSql += "JasmineGroupCost,OtherCost,OtherCost_Profit,PenaltyCost,PenaltyCost_Profit, " + vbCr
                strSql += "RevenueAfter,RevenueAfter_Profit,CAPEX,CAPEX_Profit,CashFlow,CashFlow_Profit, " + vbCr
                strSql += "Payback,Payback_Profit, " + vbCr
                strSql += "Margin,Margin_Profit,NPV,NPV_Profit,Status,CreateBy,CreateDate)" + vbCr
                strSql += "values ('" & xRequest_id & "','" & CDbl(lblRevenue.Text) & "','" & CDbl(lblRevenue_total.Text) & "','" & CDbl(lblOPEX.Text) & "','" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                strSql += "'" & CDbl(lblMarketing.Text) & "','" & CDbl(lblMarketing_profit.Text) & "','" & CDbl(lblEntertain.Text) & "','" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(lblGift.Text) & "','" & CDbl(lblGift_profit.Text) & "','" & lblInternetBW.Text & "','" & lblNetworkBW.Text & "', " + vbCr
                strSql += "'" & CDbl(lblMKTCost.Text) & "','" & CDbl(lblMKTCost_total.Text) & "','" & CDbl(lblCostOfInternet.Text) & "','" & CDbl(lblCostOfNetwork.Text) & "','" & CDbl(lblCostOfNOC.Text) & "', " + vbCr
                strSql += "'" & CDbl(lblJasmineGorup.Text) & "','" & CDbl(lblOther.Text) & "','" & CDbl(lblOther_total.Text) & "','" & CDbl(lblPenalty.Text) & "','" & CDbl(lblPenalty_total.Text) & "', " + vbCr
                strSql += "'" & CDbl(lblRevenue_Operation.Text) & "','" & CDbl(lblRevenue_Operationtotal.Text) & "','" & CDbl(lblCAPEX_value.Text) & "','" & CDbl(lblCAPEX_total.Text) & "','" & CDbl(lblCashFlow_value.Text) & "','" & CDbl(lblCashFlow_total.Text) & "', " + vbCr
                If lblPayBack.Text = "<=1" Then
                    strSql += "'-1', "
                Else
                    strSql += "'" & CDbl(lblPayBack.Text) & "', "
                End If
                If lblPayBackProfit.Text = "<=1" Then
                    strSql += "'-1', " + vbCr
                Else
                    strSql += "'" & CDbl(lblPayBackProfit.Text) & "', " + vbCr
                End If
                strSql += "'" & CDbl(lblMargin.Text) & "','" & CDbl(lblMarginProfit.Text) & "','" & CDbl(lblNPV.Text) & "','" & CDbl(lblNPVProfit.Text) & "','1','" + Session("uemail") + "',getdate()) "
                C.ExecuteNonQuery(strSql)
            End If


            Dim vSqlIn As String = ""
            Dim flow_id As String
            Dim purchase As Double = 0
            Dim tMargin As Double = 0
            Dim tPayback As Double = 0
            Dim tPenaltyLate As Double = 0
            Dim tRevenue As Double = 0

            If IsNumeric(Replace(lblTotalCAPEX.Text, "   THB", "")) = True Then
                purchase = CDbl(Replace(lblTotalCAPEX.Text, "   THB", ""))
            End If
            If IsNumeric(lblMargin.Text) = True Then
                tMargin = CDbl(lblMargin.Text)
            End If
            If IsNumeric(lblPayBack.Text) = True Then
                tPayback = CDbl(lblPayBack.Text)
            End If
            If IsNumeric(lblPenaltyLate.Text) = True Then
                tPenaltyLate = CDbl(lblPenaltyLate.Text)
            End If
            If IsNumeric(lblRevenue.Text) = True Then
                If CDbl(lblRevenue.Text) <= 0 Then
                    tRevenue = 1
                Else
                    tRevenue = CDbl(lblRevenue.Text)
                End If
            End If

            flow_id = Cal.getFlowID(lblArea.Text, lblPresaleMail.Text, tPenaltyLate, tRevenue, tPayback, purchase, tMargin, lblSpecialPrice.Text, lblSpecialApprove.Text)

            DT_flow_id = C.GetDataTable("select * from request_flow where request_id = '" & xRequest_id & "' and disable = '0' and flow_id = '" & flow_id & "'")
            'If DT_flow_id.Rows.Count <= 0 Then
            Dim vDTF As New DataTable
            vDTF = CF.rLoadRequestFlow(xRequest_id, "1")
            vSqlIn += "DECLARE @newid varchar(50) "
            vSqlIn += "SET @newid = '" + xRequest_id + "' "
            vSqlIn += CF.rSqlDisableRequestFlow(xRequest_id, vDTF.Rows(0).Item("flow_id"), "1")
            vSqlIn += CF.rSqlInsertRequestFlow(flow_id, "1")
            'vSqlIn += CF.rSqlSetDepartRequestFlow( _
            '    vDTF.Rows(0).Item("uemail_verify"), "", _
            '    vDTF.Rows(0).Item("createBy"), vDTF.Rows(0).Item("uemail_HRO"), "", "")

            C.ExecuteNonQuery(vSqlIn)

            'End If

            vSqlIn = CF.rSqlUpdateRequestFlow(lblArea.Text, lblROMail.Text, lblPrepaireEmail.Text, lblPresaleMail.Text, Session("uemail"), xRequest_id)
            C.ExecuteNonQuery(vSqlIn)

            If Command = "SubmitApprove" Then
                strSql = "Update FeasibilityDocument set Document_Date = '" + C.CDateText(txtDocumentDate.Text) + "', Service_Date = '" + C.CDateText(txtServiceDate.Text) + "', Area = '" + lblArea.Text + "', Cluster = '" + lblCluster.Text + "', Customer_Name = '" + lblCustomerName.Text + "', Type_Service = '" + lblTypeOfService.Text + "', Detail_Service = '" + txtDetailService.Text + "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate(), Status = '1'  "
                If request_status = "110" Then
                    strSql += ", request_Status='0' " + vbCr
                End If
                strSql += "where Document_No = '" + xRequest_id + "' "
                C.ExecuteNonQuery(strSql)


                If request_status = "110" Then
                    CF.UpdateRequest(xRequest_id, "", "", "", "", Session("uemail"), Session("uemail"), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                End If

                'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('บันทึกข้อมูลสำเร็จ');", True)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('บันทึกข้อมูลและส่งอนุมัติ สำเร็จ',function(){ window.location = 'check_status_list.aspx?menu=check'; });", True)

            Else
                Dim dt_flow As New DataTable
                dt_flow = C.GetDataTable("select * from dbo.request_flow where request_id = '" & xRequest_id & "' and disable='0' and flow_step = '1' ")
                If dt_flow.Rows.Count > 0 Then
                    vSqlIn = "Update request_flow set flow_status = '110' where request_id = '" & xRequest_id & "' and disable = '0' and flow_step = '1' " + vbCr
                    vSqlIn += "update FeasibilityDocument set "
                    vSqlIn += "request_status = 110 "
                    vSqlIn += ", last_update = getdate(), last_depart = '" & dt_flow.Rows(0).Item("depart_id") & "' "
                    vSqlIn += ", next_depart = '0' " '***** ขอเพิ่มข้อมูล ให้ลำดับถัดไปเป็น คนสร้างคำขอ
                    vSqlIn += "where Document_No = '" & xRequest_id & "' "
                    C.ExecuteNonQuery(vSqlIn)
                End If

                Dim sm_alert As String = "AlertSuccess('บันทึกโครงการ สำเร็จ',function(){ window.location = 'edit_list.aspx?menu=edit'; });"
                ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)
            End If

        End If
    End Sub

    Sub Flow_Submit(ByVal Source As Object, ByVal E As EventArgs)
        Dim strSql As String
        strSql = ""

        Dim flow_file As String = "" 'rUpFile("flow_file", Request.QueryString("request_id") + "_F")
        CF.SaveFlow(hide_uemail.Value, hide_flow_no.Value, hide_flow_sub.Value, hide_next_step.Value, hide_back_step.Value, hide_department.Value, hide_flow_status.Value, hide_flow_remark.Value, flow_file, lblProjectCode.Text)
    End Sub

    Protected Sub Add_Next(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add_next_hidden.ServerClick

    End Sub

    'Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
    '    Dim strcluster As String
    '    Dim DTCluster As DataTable
    '    strcluster = "select distinct Cluster, Cluster_email, Cluster_name from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' "
    '    C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster")
    '    DTCluster = C.GetDataTable(strcluster)
    '    If DTCluster.Rows.Count > 0 Then
    '        lblPrepare.Text = "(" & DTCluster.Rows(0).Item("Cluster_name") & ")"
    '        lblPrepaireEmail.Text = DTCluster.Rows(0).Item("Cluster_email")
    '    End If
    'End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex = 7 Then
                e.Row.BackColor = Drawing.Color.Yellow
                e.Row.Font.Bold = True
            End If
        End If
    End Sub

    Sub Set_Menu()
        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from dbo.UserLogin where login_name = '" & Session("uemail") & "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            Session("Login_permission") = DT.Rows(0).Item("Login_permission")
            If Session("Login_permission") = "administrator" Then
                Create.Visible = True
                Edit.Visible = True
                Approve.Visible = True
                Create.HRef = "Default.aspx?menu=create"
                Edit.HRef = "edit_list.aspx?menu=edit"
                Approve.HRef = "approve_list.aspx?menu=approve"
                Create.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "inspector" Then
                'Edit.Attributes("class") = "list-group-item active"
                Create.Visible = False
                Edit.Visible = True
                Approve.Visible = True
                Edit.HRef = "edit_list.aspx?menu=edit"
                Approve.HRef = "approve_list.aspx?menu=approve"
                Edit.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "user" Then
                'Create.Attributes("class") = "list-group-item active"
                Create.Visible = True
                Edit.Visible = True
                Approve.Visible = False
                Create.HRef = "Default.aspx?menu=create"
                Edit.HRef = "edit_list.aspx?menu=edit"
                Create.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "approve" Then
                'Create.Attributes("class") = "list-group-item active"
                Create.Visible = False
                Edit.Visible = False
                Approve.Visible = True
                Approve.HRef = "approve_list.aspx?menu=approve"
                Approve.Attributes("class") = "list-group-item active"
            End If
        Else
            Session.Clear()
            Response.Redirect("~/index.aspx?alert=1")
        End If
        If Session("menu") = "create" Then
            Create.Attributes("class") = "list-group-item active"
            Edit.Attributes("class") = "list-group-item"
            Approve.Attributes("class") = "list-group-item"
        ElseIf Session("menu") = "edit" Then
            Create.Attributes("class") = "list-group-item"
            Edit.Attributes("class") = "list-group-item active"
            Approve.Attributes("class") = "list-group-item"
        ElseIf Session("menu") = "approve" Then
            Create.Attributes("class") = "list-group-item"
            Edit.Attributes("class") = "list-group-item"
            Approve.Attributes("class") = "list-group-item active"
        End If
    End Sub

    Protected Sub test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles test.Click
        Response.Write("<script>")
        Response.Write("window.open('summarypdf.aspx?Doc=" + xRequest_id + "','_blank')")
        Response.Write("</script>")
    End Sub

    Protected Sub btnCancelProject_Command(sender As Object, e As CommandEventArgs) Handles btnCancelProject.Command
        show_CancelModal()
    End Sub

    Sub show_CancelModal()
        Dim myModal As String
        myModal = "$('#divCancelProject').modal('show');" + vbCr
        myModal += "$('.modal-header').on('mousedown', function (mousedownEvt) {" + vbCr
        myModal += "        var $draggable = $(this);" + vbCr
        myModal += "        var x = mousedownEvt.pageX - $draggable.offset().left," + vbCr
        myModal += "                y = mousedownEvt.pageY - $draggable.offset().top;" + vbCr
        myModal += "        $('body').on('mousemove.draggable', function (mousemoveEvt) {" + vbCr
        myModal += "           $draggable.closest('.modal-dialog').offset({" + vbCr
        myModal += "                'left': mousemoveEvt.pageX - x," + vbCr
        myModal += "                'top': mousemoveEvt.pageY - y" + vbCr
        myModal += "            });" + vbCr
        myModal += "        });" + vbCr
        myModal += "        $('body').one('mouseup', function () {" + vbCr
        myModal += "            $('body').off('mousemove.draggable');" + vbCr
        myModal += "        });" + vbCr
        myModal += "        $draggable.closest('#divCancelProject').one('bs.modal.hide', function () {" + vbCr
        myModal += "            $('body').off('mousemove.draggable');" + vbCr
        myModal += "        });" + vbCr
        myModal += "    });" + vbCr
        ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", myModal, True)
    End Sub

    Protected Sub btnSaveCancelProject_Click(sender As Object, e As EventArgs) Handles btnSaveCancelProject.Click
        If Trim(txtRemarkCancelProject.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุสาเหตุที่ยกเลิกโครงการ');", True)
            show_CancelModal()
        ElseIf txtRemarkCancelProject.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการระบุสาเหตุที่ยกเลิกโครงการ ได้"");", True)
            show_CancelModal()
        Else
            Dim result As String
            result = CF.CancelProject(Session("uemail"), xRequest_id, txtRemarkCancelProject.Text)
            If result = "Complete" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_cancel", "AlertSuccess('ยกเลิกโครงการ " & xRequest_id & " เรียบร้อย', function(){ window.location = 'check_status_list.aspx?menu=check'; });", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('" & C.rpQuoted(result) & "');focus();", True)
            End If
        End If

    End Sub
End Class
