Imports System.Data
Imports Microsoft.VisualBasic
Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports System.Net
Imports System.Net.Mail

Partial Class Approve
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim xRequest_id

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
         
        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from dbo.UserLogin u inner join Permission p on u.Login_permission = p.permission_type where login_name = '" & Session("uemail") & "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            xRequest_id = Request.QueryString("request_id") '"FES201705-00004"
            'If DT.Rows(0).Item("Login_permission") = "approve" Or DT.Rows(0).Item("Login_permission") = "approve_ro" Or DT.Rows(0).Item("Login_permission") = "approve_cluster" Or DT.Rows(0).Item("Login_permission") = "inspector" Or DT.Rows(0).Item("Login_permission") = "administrator" Or DT.Rows(0).Item("Login_permission") = "user" Then
            If Check_Permission() Then
                refno.Text = xRequest_id
                If Request.QueryString("menu") IsNot Nothing Then
                    Session("menu") = Request.QueryString("menu")
                End If

                Dim DT_ProjectName As DataTable
                Dim DT_Service As New DataTable
                Dim DT_Summary As New DataTable
                Dim DT_FeasibilityUpload As New DataTable

                strSql = "select * from FeasibilityDocument where Document_No = '" & xRequest_id & "' and Upload_Project = '1' "
                DT_FeasibilityUpload = C.GetDataTable(strSql)

                DT_ProjectName = GetTableProjectName()
                DT_Service = GetTableService()
                DT_Summary = GetTableSummary()


                If DT_Summary.Rows.Count > 0 Then
                    If DT_FeasibilityUpload.Rows.Count > 0 Then
                        tr_CAPEX_Header.Visible = False
                        tr_CAPEX_Detail.Visible = False
                        tr_CAPEX_Total.Visible = False
                        If DT_ProjectName.Rows.Count > 0 Then
                            ShowProjectName(DT_ProjectName)
                            ShowSummary(DT_Summary)
                        Else
                            'btnSave.Visible = False
                            LinkFileDoc.Enabled = False
                            LinkProjectFile.Visible = False
                            test.Enabled = False
                        End If
                    Else
                        If DT_ProjectName.Rows.Count > 0 And DT_Service.Rows.Count > 0 Then
                            ShowProjectName(DT_ProjectName)
                            ShowCAPEX_OPEX(DT_Service)
                            ShowSummary(DT_Summary)
                            'btnSave.Visible = True
                        Else
                            'btnSave.Visible = False
                            LinkFileDoc.Enabled = False
                            test.Enabled = False
                        End If
                        LinkProjectFile.Visible = False
                    End If
                Else
                    If DT_ProjectName.Rows.Count > 0 And DT_Service.Rows.Count > 0 Then
                        ShowProjectName(DT_ProjectName)
                        ShowCAPEX_OPEX(DT_Service)
                        ShowService(DT_ProjectName, DT_Service)
                        'btnSave.Visible = True
                    Else
                        'btnSave.Visible = False
                        LinkFileDoc.Enabled = False
                        test.Enabled = False
                    End If
                    LinkProjectFile.Visible = False
                End If

                If lblCostOfNOC_Profit.Text = "0" Then
                    td_CostNOC_Profit.Style("background") = "#6c757d;"
                    lblCostOfNOC_Profit.Visible = False
                Else
                    td_CostNOC_Profit.Style("color") = "#6c757d;"
                    td_CostNOC_Profit.Style("background") = ""
                    lblCostOfNOC_Profit.Visible = True
                End If

                If DT.Rows(0).Item("Layout") = "Account" Then
                    Layout_Project_Detail.Visible = False
                    Layout_Summary.Visible = True
                ElseIf DT.Rows(0).Item("Layout") = "Technical" Then
                    Layout_Project_Detail.Visible = False
                    Layout_Summary.Visible = False
                ElseIf DT.Rows(0).Item("Layout") = "All" Then
                    Layout_Project_Detail.Visible = True
                    Layout_Summary.Visible = True
                End If

                'Dim DT_ProjectName As DataTable
                'Dim DT_CAPEX As DataTable
                'Dim DT_CAPEX_Mass As DataTable
                'Dim DT_OPEX As DataTable
                'Dim DT_OTHER As DataTable
                'Dim DT_Service As DataTable
                'Dim DT_InternetBWCost As DataTable
                'Dim Transit As Double
                'Dim xDSL_FTTx As Double
                'Dim r As String
                'Dim total_capex As Double = 0
                'Dim total_capex_mass As Double = 0
                'Dim total_opex As Double = 0
                'Dim total_other As Double = 0

                'strSql = "select * from List_ProjectName where Document_No = '" + xRequest_id + "'  "
                'refno.Text = xRequest_id
                'DT_ProjectName = C.GetDataTable(strSql)
                'If DT_ProjectName.Rows.Count > 0 Then
                '    lblProjectName.Text = DT_ProjectName.Rows(0).Item("Project_Name")
                '    If IsDBNull(DT_ProjectName.Rows(0).Item("Project_Code")) Then
                '        lblProjectCode.Text = ""
                '    Else
                '        lblProjectCode.Text = DT_ProjectName.Rows(0).Item("Project_Code")
                '    End If

                '    'txtCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Name")
                '    'lblEnterpriseName.Text = DT_ProjectName.Rows(0).Item("Enterprise_Name")
                '    'txtLocationName.Text = DT_ProjectName.Rows(0).Item("Location_Name")
                '    lblCustomerType.Text = DT_ProjectName.Rows(0).Item("Customer_Type")
                '    txtDocumentDate.Text = DT_ProjectName.Rows(0).Item("Document_Date")
                '    txtServiceDate.Text = DT_ProjectName.Rows(0).Item("Service_Date")
                '    'lblCustomerAssistantName.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_Name")
                '    'lblCustomerAssistantID.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_ID")
                '    lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
                '    lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
                '    'lblCustomerContactName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name")
                '    'lblCustomerContactTel.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
                '    lblCustomerContactEmail.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
                '    lblCompanyService.Text = DT_ProjectName.Rows(0).Item("Company_Service")
                '    lblTypeOfService.Text = DT_ProjectName.Rows(0).Item("Type_Service")
                '    txtDetailService.Text = DT_ProjectName.Rows(0).Item("Detail_Service")
                '    txtSLA.Text = DT_ProjectName.Rows(0).Item("SLA")
                '    lblMonitorDate.Text = DT_ProjectName.Rows(0).Item("Monitor_Date")
                '    lblMonitorTime.Text = DT_ProjectName.Rows(0).Item("Monitor_Time")
                '    txtFine.Text = DT_ProjectName.Rows(0).Item("Fine")

                '    Dim DT_RO_Director As DataTable
                '    DT_RO_Director = C.GetDataTable("select * from RO_Director where RO = '" + DT_ProjectName.Rows(0).Item("Area") + "'")
                '    If DT_RO_Director.Rows.Count > 0 Then
                '        'lblHRO.Text = "(" & DT_RO_Director.Rows(0).Item("RO_name") & ")"
                '        lblROMail.Text = DT_RO_Director.Rows(0).Item("RO_email")
                '    End If
                '    'lblCreateProject.Text = "(" & DT_ProjectName.Rows(0).Item("Customer_Assistant_Name") & ")"

                '    'lblPrepare.Text = "(" & DT_ProjectName.Rows(0).Item("Cluster_name") & ")"
                '    lblPrepaireEmail.Text = DT_ProjectName.Rows(0).Item("Cluster_email")
                'Else
                '    lblProjectName.Text = "-"
                'End If

                'If txtDocumentDate.Text = "" Then
                '    txtDocumentDate.Text = "&nbsp;"
                'End If

                'strSql = "select * from dbo.List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
                'DT_CAPEX = C.GetDataTable(strSql)
                'For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
                '    total_capex += DT_CAPEX.Rows(i).Item("Cost")
                'Next
                ''lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00") & "   THB"


                'strSql = "select * from dbo.List_CAPEX_Mass where Document_No = '" + xRequest_id + "' and Status = '1' "
                'DT_CAPEX_Mass = C.GetDataTable(strSql)
                'For i As Integer = 0 To DT_CAPEX_Mass.Rows.Count - 1
                '    total_capex_mass += DT_CAPEX_Mass.Rows(i).Item("Cost")
                'Next
                ''lblTotalCAPEXMass.Text = Format(CDbl(total_capex_mass.ToString), "###,##0.00") & "   THB"


                'strSql = "select * from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
                'DT_OPEX = C.GetDataTable(strSql)
                'For i As Integer = 0 To DT_OPEX.Rows.Count - 1
                '    total_opex += DT_OPEX.Rows(i).Item("Cost")
                'Next
                'lblTotalOPEX.Text = Format(CDbl(total_opex.ToString), "###,##0.00") & "   THB"

                'strSql = "select * from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' "
                'DT_OTHER = C.GetDataTable(strSql)
                'For i As Integer = 0 To DT_OTHER.Rows.Count - 1
                '    total_other += DT_OTHER.Rows(i).Item("Cost")
                'Next
                'lblTotalOTHER.Text = Format(CDbl(total_other.ToString), "###,##0.00") & "   THB"


                'strSql = "select * from dbo.List_Service where Document_No = '" + xRequest_id + "' "
                'DT_Service = C.GetDataTable(strSql)
                'If DT_Service.Rows.Count > 0 Then 'And DT_CAPEX.Rows.Count > 0 And DT_OPEX.Rows.Count > 0 Then
                '    'lblContractYear.Text = Math.Round(CDbl(DT_Service.Rows(0).Item("Contract").ToString / 12), 0)
                '    'lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
                '    'lblMonthly.Text = Format(CDbl(DT_Service.Rows(0).Item("Monthly").ToString), "###,##0.00")
                '    'lblOneTimePayment.Text = Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
                '    'lblCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")

                '    ' Define money format.
                '    Dim MoneyFmt As String = "###,##0.00"
                '    Dim MoneyRoundup As String = "###,##0"
                '    ' Define percentage format.
                '    Dim PercentFmt As String = "#0.00"
                '    'Dim values(lblContract.Text) As Double

                '    Dim Revenue As Double
                '    Dim MKTCost As Double
                '    Dim RevenueOneTime As Double
                '    Dim MKTCostOneTime As Double
                '    Dim Vas As Double = 0
                '    Dim CashFlow As Double
                '    Dim CashFlowOneTime As Double


                '    Dim OneTimPayment As Double
                '    Dim MonthlyPrice As Double
                '    Dim RevenueOne As Double
                '    Dim RevenueNotOne As Double
                '    Dim NetRevenueOne As Double
                '    Dim NetRevenueNotOne As Double
                '    Dim JasmineGroup As Double
                '    Dim Other As Double
                '    Dim CAPEXCorp As Double
                '    Dim CAPEXMass As Double
                '    Dim MKTCostOne As Double
                '    Dim MKTCostNotOne As Double
                '    Dim Budget1per As Double
                '    Dim GiftPrice As Double
                '    Dim CostOfInternet As Double
                '    Dim DomesticCost As Double
                '    Dim InternationalCost As Double
                '    Dim Caching As Double
                '    Dim CostOfNetwork As Double
                '    Dim CostOfNOC As Double
                '    Dim Penalty As Double
                '    Dim OPEXOne As Double
                '    Dim OPEXNotOne As Double
                '    Dim RevenueAfterOperationOne As Double
                '    Dim RevenueAfterOperationNotOne As Double
                '    Dim CashFlowOne As Double
                '    Dim CashFlowNotOne As Double
                '    Dim PaybackSum As Double
                '    Dim SparePart As Double
                '    Dim SparePartPercent As Double = 0.01

                '    GiftPrice = CDbl(DT_Service.Rows(0).Item("Gift").ToString)
                '    Penalty = CDbl(DT_Service.Rows(0).Item("Penalty").ToString)

                '    lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
                '    If lblCustomerType.Text = "ลูฅค้าทั่วไป" Then
                '        MonthlyPrice = CDbl(DT_Service.Rows(0).Item("Monthly").ToString)
                '        OneTimPayment = CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString)  'Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
                '        SparePart = MonthlyPrice * 12 * SparePartPercent
                '    ElseIf lblCustomerType.Text = "ลูฅค้าราชฅาร" Then
                '        MonthlyPrice = Math.Round(((CDbl(DT_Service.Rows(0).Item("Monthly").ToString) * 100) / 107) / DT_Service.Rows(0).Item("Contract").ToString, 2)
                '        OneTimPayment = Math.Round((CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString) * 100) / 107, 2) 'Format(Math.Round((CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString) * 100) / 107, 2), "###,##0.00")
                '        SparePart = (CDbl(DT_Service.Rows(0).Item("Monthly").ToString) * 100 / 107) * SparePartPercent
                '    End If
                '    lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString) + SparePart, "###,##0.00") & "   THB"

                '    JasmineGroup = total_opex
                '    Other = total_other
                '    CAPEXCorp = total_capex + SparePart
                '    CAPEXMass = total_capex_mass

                '    RevenueOne = MonthlyPrice + OneTimPayment
                '    NetRevenueOne = RevenueOne - JasmineGroup - Other - CAPEXMass
                '    RevenueNotOne = MonthlyPrice
                '    NetRevenueNotOne = RevenueNotOne - JasmineGroup - Other

                '    If OneTimPayment = 0 Then
                '        Budget1per = RevenueOne * 0.01
                '    Else
                '        Budget1per = (OneTimPayment / DT_Service.Rows(0).Item("Contract").ToString) * 0.01
                '    End If
                '    MKTCostOne = Budget1per + Budget1per + GiftPrice
                '    MKTCostNotOne = Budget1per + Budget1per

                '    DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * 50)
                '    InternationalCost = CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * 500
                '    Caching = (Math.Ceiling(CDbl(DT_Service.Rows(0).Item("International").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100)) - CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString)) * 200
                '    CostOfInternet = DomesticCost + InternationalCost + Caching

                '    CostOfNetwork = (CDbl(DT_Service.Rows(0).Item("Domestic").ToString) + CDbl(DT_Service.Rows(0).Item("EthernetIPV").ToString)) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * 60
                '    CostOfNOC = (CDbl(DT_Service.Rows(0).Item("INL").ToString) + CDbl(DT_Service.Rows(0).Item("IPV").ToString) + CDbl(DT_Service.Rows(0).Item("INF").ToString)) * 250

                '    OPEXOne = MKTCostOne + CostOfInternet + CostOfNetwork + CostOfNOC + Penalty
                '    OPEXNotOne = MKTCostNotOne + CostOfInternet + CostOfNetwork + CostOfNOC

                '    RevenueAfterOperationOne = NetRevenueOne - OPEXOne
                '    RevenueAfterOperationNotOne = NetRevenueNotOne - OPEXNotOne

                '    CashFlowOne = RevenueAfterOperationOne - CAPEXCorp
                '    CashFlowNotOne = RevenueAfterOperationNotOne

                '    Dim CashFlowPeriod(DT_Service.Rows(0).Item("Contract").ToString) As Double
                '    Dim PaybackPeriod(DT_Service.Rows(0).Item("Contract").ToString) As Double

                '    For i As Integer = 0 To CashFlowPeriod.Length - 1
                '        If i = 0 Then
                '            CashFlowPeriod(i) = CashFlowOne
                '        Else
                '            CashFlowPeriod(i) = CashFlowNotOne + CashFlowPeriod(i - 1)
                '        End If
                '    Next

                '    For i As Integer = 0 To PaybackPeriod.Length - 1
                '        If i = 0 Then
                '            PaybackPeriod(i) = 1
                '        Else
                '            If CashFlowPeriod(i) < 0 Then
                '                PaybackPeriod(i) = 1
                '            Else
                '                If CashFlowPeriod(i - 1) < 0 Then
                '                    PaybackPeriod(i) = (CashFlowPeriod(i - 1) * (-1)) / CashFlowNotOne
                '                Else
                '                    PaybackPeriod(i) = 0
                '                End If
                '            End If
                '        End If
                '    Next

                '    For i As Integer = 0 To DT_Service.Rows(0).Item("Contract").ToString - 1
                '        PaybackSum = PaybackSum + PaybackPeriod(i)
                '    Next

                '    lblRevenue.Text = Format(RevenueOne + (RevenueNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                '    'lblNetRevenue.Text = Format(NetRevenueOne + (NetRevenueNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                '    'lblCAPEX.Text = Format(CAPEXCorp + CAPEXMass, MoneyRoundup)
                '    'lblCAPEXCorp.Text = Format(CAPEXCorp, MoneyRoundup)
                '    'lblCAPEXMass.Text = Format(CAPEXMass, MoneyRoundup)
                '    'lblCashFlow.Text = Format(CashFlowOne + (CashFlowNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                '    lblPayBack.Text = Format(PaybackSum, MoneyFmt)

                '    lblOPEX.Text = Format(OPEXOne + (OPEXNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                '    lblMKTCost.Text = Format(MKTCostOne + (MKTCostNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                '    lblCostOfInternet.Text = Format(CostOfInternet * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                '    lblCostOfNetwork.Text = Format(CostOfNetwork * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                '    lblCostOfNOC.Text = Format(CostOfNOC * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                '    lblPenalty.Text = Format(Penalty, MoneyRoundup)
                '    lblJasmineGorup.Text = Format(JasmineGroup * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                '    lblOther.Text = Format(Other * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)

                '    Dim values(lblContract.Text) As Double
                '    For i As Integer = 0 To 0 'lblContract.Text - 1
                '        If i = 0 Then
                '            'values(i) = lblCashFlow.Text 'Format(CashFlowOne, MoneyRoundup)
                '        Else
                '            values(i) = Format(CashFlowNotOne, MoneyRoundup)
                '        End If
                '    Next
                '    Dim FixedRetRate As Double = 0.05
                '    ' Calculate net present value.
                '    Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
                '    lblNPV.Text = Format(NetPVal, MoneyFmt)

                '    'If lblNetRevenue.Text <> 0 Then
                '    '    lblMargin.Text = Format((lblNPV.Text / lblNetRevenue.Text) * 100, MoneyFmt)
                '    'Else
                '    '    lblMargin.Text = "0"
                '    'End If

                'End If

                If Not Page.IsPostBack Then
                    loadDetail(xRequest_id)
                End If
            Else
                LinkFileDoc.Enabled = False
                test.Enabled = False
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('คุณไม่มีสิทธิ์ในการ Approve');", True)
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Dim strSql As String
        'Dim DT As New DataTable
        'Dim TypeService As String
        'If RadioButton2.Checked = True Then
        '    TypeService = RadioButton2.Text
        'ElseIf RadioButton3.Checked = True Then
        '    TypeService = RadioButton3.Text
        'Else
        '    TypeService = RadioButton1.Text
        'End If
        'strSql = "select convert(varchar(5),right(isnull(max(Document_No),'FES000000-00000'),5) + 1) 'Max_Document',  left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) 'YearMonth' " + vbCr
        'strSql += "from FeasibilityDocument " + vbCr
        'strSql += "where substring(Document_No,4,6) = left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) "
        'DT = C.GetDataTable(strSql)
        ''Dim a As Integer = 200
        ''Dim doc As String = CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")
        ''strSql = "insert into FeasibilityDocument (Document_No,Document_Date,Service_Date,Area,Cluster,Customer_Name,Location_Name,Type_Service,Detail_Service,CreateBy,CreateDate) values ('FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "','" + C.CDateText(txtDocumentDate.Text) + "','" + C.CDateText(txtServiceDate.Text) + "','" + txtArea.Text + "','" + txtCluster.Text + "','" + txtCustomerName.Text + "','" + txtLocationName.Text + "','" + TypeService + "','" + txtDetailService.Text + "','weraphon.r',getdate()) "
        'C.ExecuteNonQuery(strSql)
        'strSql = "Update dbo.List_CAPEX set Document_No='FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = 'weraphon.r' and Document_No is NULL " + vbCr
        'strSql += "Update dbo.List_OPEX set Document_No='FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = 'weraphon.r' and Document_No is NULL " + vbCr
        'strSql += "Update dbo.List_Model set Document_No='FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = 'weraphon.r' and Document_No is NULL "
        'C.ExecuteNonQuery(strSql)
    End Sub

    Public Sub ShowProjectName(ByVal DT_ProjectName As DataTable)
        'Dim strSql As String
        'Dim DT_ProjectName As DataTable
        'strSql = "select * from List_ProjectName where CreateBy='" + Session("uemail") + "' and Document_No is null "
        'DT_ProjectName = C.GetDataTable(strSql)
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

            'lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Name")
            If IsDBNull(DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")) Or DT_ProjectName.Rows(0).Item("Customer_Contact_Tel") = "" Then
                lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; เบอร์โทร. -"
            Else
                lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; เบอร์โทร. " + DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
            End If

            'lblEnterpriseName.Text = DT_ProjectName.Rows(0).Item("Enterprise_Name")
            intenal_refno.Text = DT_ProjectName.Rows(0).Item("Location_Name")
            lblCustomerType.Text = DT_ProjectName.Rows(0).Item("Customer_Type")
            txtDocumentDate.Text = DT_ProjectName.Rows(0).Item("Document_Date")
            txtServiceDate.Text = DT_ProjectName.Rows(0).Item("Service_Date")
            lblCustomerAssistantName.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_Name")
            'lblCustomerAssistantID.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_ID")
            lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
            lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
            'lblCustomerContactName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name")
            'lblCustomerContactTel.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
            lblCustomerContactEmail.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
            lblCompanyService.Text = DT_ProjectName.Rows(0).Item("Company_Service")
            lblTypeOfService.Text = DT_ProjectName.Rows(0).Item("Type_Service")
            txtDetailProject.Text = Replace(DT_ProjectName.Rows(0).Item("Detail_Project"), Environment.NewLine, "<br />")
            txtDetailService.Text = Replace(DT_ProjectName.Rows(0).Item("Detail_Service"), Environment.NewLine, "<br />")
            txtSLA.Text = DT_ProjectName.Rows(0).Item("SLA")
            txtMTTR.Text = DT_ProjectName.Rows(0).Item("MTTR")
            lblMonitorDate.Text = DT_ProjectName.Rows(0).Item("Monitor_Date")
            lblMonitorTime.Text = DT_ProjectName.Rows(0).Item("Monitor_Time")
            txtFine.Text = Replace(DT_ProjectName.Rows(0).Item("Fine"), Environment.NewLine, "<br />")

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
            DT_RO_Director = C.GetDataTable("select * from RO_Director where RO = '" + DT_ProjectName.Rows(0).Item("Area") + "'")
            If DT_RO_Director.Rows.Count > 0 Then
                'lblHRO.Text = "(" & DT_RO_Director.Rows(0).Item("RO_name") & ")"
                lblROMail.Text = DT_RO_Director.Rows(0).Item("RO_email")
            End If
            'lblCreateProject.Text = "(" & DT_ProjectName.Rows(0).Item("Customer_Assistant_Name") & ")"

            'lblPrepare.Text = "(" & DT_ProjectName.Rows(0).Item("Cluster_name") & ")"
            'lblPrepaireEmail.Text = DT_ProjectName.Rows(0).Item("Cluster_email")

            If DT_ProjectName.Rows(0).Item("Doc_File") <> "" Then
                LinkFileDoc.NavigateUrl = "~/Upload/" & DT_ProjectName.Rows(0).Item("Doc_File")
                LinkFileDoc.Target = "_blank"
            Else
                LinkFileDoc.Visible = False
            End If

            If DT_ProjectName.Rows(0).Item("Project_File") <> "" Then
                LinkProjectFile.NavigateUrl = "~/Upload/" & DT_ProjectName.Rows(0).Item("Project_File")
                LinkProjectFile.Target = "_blank"
            Else
                LinkProjectFile.Visible = False
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
        Dim DT_CAPEX_Mass As DataTable
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
        ' r += "<tr>"
        ' r += "<td>Total Investment Cost</td>"
        ' r += "<td></td>"
        ' r += "<td align='right'><b><u>" + Format(CDbl(total_capex.ToString), "###,##0.00") + "</u></b></td>"
        ' r += "</tr>"
        r += "</table>"
        CAPEX_Detail.InnerHtml = r


        ' strSql = "select * from dbo.List_CAPEX_Mass where CreateBy='" + Session("uemail") + "' and Document_No is null "
        ' DT_CAPEX_Mass = C.GetDataTable(strSql)
        ' For i As Integer = 0 To DT_CAPEX_Mass.Rows.Count - 1
        '     total_capex_mass += DT_CAPEX_Mass.Rows(i).Item("Cost")
        ' Next
        ' lblTotalCAPEXMass.Text = Format(CDbl(total_capex_mass.ToString), "###,##0.00") & "   THB"

        strsql = "select * from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OPEX = C.GetDataTable(strsql)
        r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        r += "<td width='70%' align='left'><u>JASMINE GROUP</u></td>"
        ' r += "<th>บาท/หน่วย</th>"
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

        r += "<tr>"
        r += "<td align='left'><b><u>Total Cost</u></b></td>"
        r += "<td></td>"
        r += "<td align='right'><b><u>" + Format(CDbl(total_opex.ToString), "###,##0.00") + "</u></b></td>"
        r += "</tr>"


        r += "<tr Style = 'height: 15px'><td></td><td></td><td></td></tr>"

        strsql = "select * from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OTHER = C.GetDataTable(strsql)
        'r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><u>OTHER</u></td>"
        ' r += "<th>บาท/หน่วย</th>"
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
        r += "<tr>"
        r += "<td align='left'><b><u>Total Cost</u></b></td>"
        r += "<td></td>"
        r += "<td align='right'><b><u>" + Format(CDbl(total_other.ToString), "###,##0.00") + "</u></b></td>"
        r += "</tr>"

        r += "<tr Style = 'height: 15px'><td></td><td></td><td></td></tr>"

        strsql = "select * from dbo.List_Management where Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_Management = C.GetDataTable(strsql)
        'r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><u>Management</u></td>"
        ' r += "<th>บาท/หน่วย</th>"
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
        r += "<tr>"
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

        If DT_Service.Rows.Count > 0 Then 'And DT_CAPEX.Rows.Count > 0 And DT_OPEX.Rows.Count > 0 Then
            'lblContractYear.Text = Math.Round(CDbl(DT_Service.Rows(0).Item("Contract").ToString / 12), 0)

            'lblMonthly.Text = Format(CDbl(DT_Service.Rows(0).Item("Monthly").ToString), "###,##0.00")
            'lblOneTimePayment.Text = Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
            'lblCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")

            ' Define money format.
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



            'If lblCustomerType.Text = "ลูกค้าทั่วไป" Then

            '    SparePart = MonthlyPrice * 12 * SparePartPercent
            'ElseIf lblCustomerType.Text = "ลูกค้าราชการ" Then
            '    MonthlyPrice = Math.Round(((CDbl(DT_Service.Rows(0).Item("Monthly").ToString) * 100) / 107) / DT_Service.Rows(0).Item("Contract").ToString, 2)
            '    OneTimePayment = Math.Round((CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString) * 100) / 107, 2) 'Format(Math.Round((CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString) * 100) / 107, 2), "###,##0.00")
            '    SparePart = (CDbl(DT_Service.Rows(0).Item("Monthly").ToString) * 100 / 107) * SparePartPercent
            'End If


            'lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString) + SparePart, "###,##0.00") & "   THB"


            'JasmineGroup = total_opex
            'Other = total_other
            'CAPEXCorp = total_capex + SparePart
            'CAPEXMass = total_capex_mass


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
            lblCostOfNOC_Profit.Text = "0"
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
            If NetPVal <> 0 Then
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
            If NetPValProfit <> 0 Then
                lblMarginProfit.Text = Format((lblNPVProfit.Text / lblRevenue.Text) * 100, PercentFmt).ToString
            Else
                lblMarginProfit.Text = "0"
            End If

            'Dim ShowPayMonth(14) As Double
            'Dim ShowPayBack(14) As String
            'Dim per As Double = 0.3


        End If
    End Sub

    Public Sub ShowSummary(ByVal DT_Summary As DataTable)
        ' Define money format.
        Dim MoneyFmt As String = "###,##0.00"
        Dim MoneyRoundup As String = "###,##0"
        ' Define percentage format.
        Dim PercentFmt As String = "#0.00"

        lblRevenue.Text = Format(CDbl(DT_Summary.Rows(0).Item("Revenue")), MoneyRoundup)
        lblRevenue_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("Revenue_Profit")), MoneyRoundup)
        lblMKTCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost")), MoneyRoundup)
        lblMKTCost_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost_Profit")), MoneyRoundup)
        lblMarketing.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingPrice")), MoneyRoundup)
        lblMarketing_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingPrice_Profit")), MoneyRoundup)
        lblEntertain.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice")), MoneyRoundup)
        lblEntertain_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice_Profit")), MoneyRoundup)
        lblGift.Text = Format(CDbl(DT_Summary.Rows(0).Item("GiftPrice")), MoneyRoundup)
        lblGift_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("GiftPrice_Profit")), MoneyRoundup)

        lblEntertainGift.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice")) + CDbl(DT_Summary.Rows(0).Item("GiftPrice")), MoneyRoundup)
        lblEntertainGift_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice_Profit")) + CDbl(DT_Summary.Rows(0).Item("GiftPrice_Profit")), MoneyRoundup)
        tr_gift.Visible = False

        lblInternetBW.Text = DT_Summary.Rows(0).Item("InternetBW")
        lblNetworkBW.Text = DT_Summary.Rows(0).Item("NetworkBW")
        lblCostOfInternet.Text = Format(CDbl(DT_Summary.Rows(0).Item("InternetCost")), MoneyRoundup)
        lblCostOfNetwork.Text = Format(CDbl(DT_Summary.Rows(0).Item("NetworkCost")), MoneyRoundup)
        lblCostOfNOC.Text = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost")), MoneyRoundup)
        lblCostOfNOC_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost_Profit")), MoneyRoundup)
        lblJasmineGorup.Text = Format(CDbl(DT_Summary.Rows(0).Item("JasmineGroupCost")), MoneyRoundup)
        lblOther.Text = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost")), MoneyRoundup)
        lblOther_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost_Profit")), MoneyRoundup)
        lblPenalty.Text = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost")), MoneyRoundup)
        lblPenalty_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost_Profit")), MoneyRoundup)
        lblCAPEX_value.Text = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX")), MoneyRoundup)
        lblCAPEX_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX_Profit")), MoneyRoundup)
        lblCashFlow_value.Text = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow")), MoneyRoundup)
        lblCashFlow_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow_Profit")), MoneyRoundup)
        If DT_Summary.Rows(0).Item("Payback") = "-1" Then
            lblPayBack.Text = "<=1"
        Else
            lblPayBack.Text = DT_Summary.Rows(0).Item("Payback")
        End If
        If DT_Summary.Rows(0).Item("Payback_Profit") = "-1" Then
            lblPayBackProfit.Text = "<=1"
        Else
            lblPayBackProfit.Text = DT_Summary.Rows(0).Item("Payback_Profit")
        End If
        lblMargin.Text = DT_Summary.Rows(0).Item("Margin")
        lblMarginProfit.Text = DT_Summary.Rows(0).Item("Margin_Profit")
        lblNPV.Text = Format(CDbl(DT_Summary.Rows(0).Item("NPV")), MoneyRoundup)
        lblNPVProfit.Text = Format(CDbl(DT_Summary.Rows(0).Item("NPV_Profit")), MoneyRoundup)

        lblOPEX.Text = Format(CDbl(DT_Summary.Rows(0).Item("OPEX")), MoneyRoundup)
        lblOPEX_total.Text = Format(CDbl(DT_Summary.Rows(0).Item("OPEX_Profit")), MoneyRoundup)
        lblRevenue_Operation.Text = Format(CDbl(DT_Summary.Rows(0).Item("RevenueAfter")), MoneyRoundup)
        lblRevenue_Operationtotal.Text = Format(CDbl(DT_Summary.Rows(0).Item("RevenueAfter_Profit")), MoneyRoundup)
        lblRevenuePercent.Text = "100%"
        lblOtherPercent.Text = Format((CDbl(lblOther.Text) / CDbl(lblRevenue.Text)) * 100, PercentFmt) & "%"
        lblCAPEXPercent.Text = Format((CDbl(lblCAPEX_value.Text) / CDbl(lblRevenue.Text)) * 100, PercentFmt) & "%"
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

    Sub Add_Next(ByVal Source As Object, ByVal E As EventArgs)

    End Sub

    Sub Flow_Submit(ByVal Source As Object, ByVal E As EventArgs)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('Test');", True)
        Try
            Dim strSql As String
            strSql = ""
            Dim flow_file As String = hide_contract_file.Value
            'hide_contract_file1.Value = hide_contract_file.Value
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('" + hide_contract_file.Value + "');", True)

            'If hide_contract_file1.PostedFile.FileName <> "" Then
            '    flow_file = hide_contract_file1.PostedFile.FileName 'rUpFile("flow_file", Request.QueryString("request_id") + "_F")
            '    SaveContractFile()
            'End If

            'CF.SaveFlow(hide_uemail.Value, hide_flow_no.Value, hide_flow_sub.Value, hide_next_step.Value, hide_back_step.Value, hide_department.Value, hide_flow_status.Value, hide_flow_remark.Value, flow_file)
            CF.SaveFlow(Session("uemail"), hide_flow_no.Value, hide_flow_sub.Value, hide_next_step.Value, hide_back_step.Value, hide_department.Value, hide_flow_status.Value, hide_flow_remark.Value, flow_file, hide_project_code.Value)
            'Response.Redirect("Approve.aspx?request_id=" + Request.QueryString("request_id"))
            Dim sm_alert As String = "AlertSuccess('บันทึกข้อมูลสำเร็จ',"
            sm_alert += "function(){ focus();window.location.href='approve_list.aspx?menu=approve';"
            sm_alert += "});"

            'ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)
            ClientScript.RegisterStartupScript(Page.GetType, "alert_approve", sm_alert, True)

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('" & Replace(ex.Message, "'", "") & "');", True)
        End Try

    End Sub

    Sub loadDetail(ByVal vRequest_id As String)
        Dim request_status As String
        Dim request_step As String
        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from FeasibilityDocument " + vbCr
        strSql += "where Document_No = '" + vRequest_id + "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            request_status = DT.Rows(0).Item("request_status")
            request_step = DT.Rows(0).Item("request_step")
            loadFlow(vRequest_id, request_status, request_step)
        End If


    End Sub

    Sub loadFlow(ByVal vRequest_id As String, ByVal vRequest_status As String, ByVal vRequest_step As String)
        Dim vRequest_permiss As Integer = CF.rViewDetail(vRequest_id, hide_uemail_create.Value())

        inn_flow.InnerHtml = CF.rLoadFlowBody(vRequest_id, vRequest_status, vRequest_step, vRequest_permiss)
    End Sub

    Protected Sub test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles test.Click
        Response.Write("<script>")
        Response.Write("window.open('summarypdf.aspx?Doc=" + Request.QueryString("request_id") + "','_blank')")
        Response.Write("</script>")
    End Sub

    Public Sub SaveContractFile()
        Dim CurrentFileName As String
        Dim CurrentPath As String
        Dim FileNameContract As String = ""

        CurrentFileName = hide_contract_file1.PostedFile.FileName
        CurrentPath = Request.PhysicalApplicationPath
        CurrentPath += "Upload\"
        CurrentPath += CurrentFileName
        hide_contract_file1.PostedFile.SaveAs(CurrentPath)
        Dim file_extContract As String
        file_extContract = Path.GetExtension(hide_contract_file1.PostedFile.FileName)
        Dim TheFileContract As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
        FileNameContract = "Contract_" + Request.QueryString("request_id") + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extContract
        If TheFileContract.Exists Then
            If System.IO.File.Exists(MapPath(".") & "\Upload\Contract\" & FileNameContract) Then
                'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่สามารถ Upload ไฟล์์ได้ เนื่องจาฅเคย Upload แล้ว');", True)
            Else
                System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\Contract\" & FileNameContract)
            End If
        Else
            Throw New FileNotFoundException()
        End If
    End Sub

    Public Function Check_Permission() As Boolean
        Dim DT_ProjectName As New DataTable
        DT_ProjectName = GetTableProjectName()

        Dim DT_List As New DataTable
        DT_List = C.GetDataTable("select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date, name.Area, name.Cluster, name.Project_Name, name.Customer_Name, doc.CreateBy  from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No inner join request_flow rf on doc.Document_No = rf.request_id and doc.request_step = rf.flow_step where doc.request_status <> 105 and doc.request_status <> 100 and rf.disable = '0' and rf.send_uemail = '" & Session("uemail") & "' and doc.Document_No = '" & xRequest_id & "' ")

        If Session("Login_permission") = "administrator" Or Session("Login_permission") = "approve" Or Session("Login_permission") = "account" Or Session("Login_permission") = "inspector" Then
            Return True
        ElseIf Session("Login_permission") = "approve_ro" Or Session("Login_permission") = "presale" Then
            If DT_ProjectName.Rows.Count > 0 Then
                If DT_ProjectName.Rows(0).Item("Area") = C.RO_User(Session("uemail")) Or DT_List.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        ElseIf Session("Login_permission") = "approve_cluster" Then
            If DT_ProjectName.Rows.Count > 0 Then
                If DT_ProjectName.Rows(0).Item("Cluster") = C.Cluster_User(Session("uemail")) Or DT_List.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        ElseIf Session("Login_permission") = "user" Then
            If DT_ProjectName.Rows.Count > 0 Then
                If DT_ProjectName.Rows(0).Item("CreateBy") = Session("uemail") Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        ElseIf Session("Login_permission") = "carrier" Then
            If DT_ProjectName.Rows.Count > 0 Then
                If C.Branch_User(Session("uemail")) = "ALL" Or DT_ProjectName.Rows(0).Item("Area") = C.Branch_User(Session("uemail")) Or DT_ProjectName.Rows(0).Item("Cluster") = C.Branch_User(Session("uemail")) Or DT_List.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Dim sm As New test_vb
        'Dim mail As New MailMessage()
        'mail.From = New MailAddress("fesibility@jasmine.com")
        'mail.To.Add("weraphon.r@jasmine.com")
        'mail.Subject = "Test"
        'mail.IsBodyHtml = True


        'Dim sw As New StringWriter()
        'Dim w As New HtmlTextWriter(sw)
        'dvMain.RenderControl(w)
        'Dim s As String
        's = sw.GetStringBuilder.ToString

        sm.SendMailSubmit(xRequest_id)

        'Dim smtp As New SmtpClient("smtp.jasmine.com")
        'smtp.Credentials = New NetworkCredential("chancharas.w", "311227")

        'smtp.Send(mail)
    End Sub
End Class
