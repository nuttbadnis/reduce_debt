Imports System.Data
Imports Microsoft.VisualBasic
Partial Class edit_Summary_20200427
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim xRequest_id
    Dim request_status

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        menu_project_name.HRef = "edit_project_name.aspx?request_id=" + xRequest_id
        menu_service.HRef = "edit_service.aspx?request_id=" + xRequest_id
        menu_capex.HRef = "edit_capex.aspx?request_id=" + xRequest_id
        menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
        menu_other.HRef = "edit_other.aspx?request_id=" + xRequest_id

        Dim dt_check As DataTable
        dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (request_status = 0 or request_status = 55 or request_status = 110) and Document_No = '" + xRequest_id + "'")
        If dt_check.Rows.Count > 0 Then
            'If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "0" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
            If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                request_status = dt_check.Rows(0).Item("request_status").ToString
                Dim DT_Document As DataTable
                Dim strSql As String
                Dim DT_ProjectName As DataTable
                Dim DT_CAPEX As DataTable
                Dim DT_CAPEX_Mass As DataTable
                Dim DT_OPEX As DataTable
                Dim DT_OTHER As DataTable
                Dim DT_Service As DataTable
                Dim r As String
                Dim total_capex As Double = 0
                Dim total_capex_mass As Double = 0
                Dim total_opex As Double = 0
                Dim total_other As Double = 0

                strSql = "select * from List_ProjectName where Document_No = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "' "
                DT_ProjectName = C.GetDataTable(strSql)
                If DT_ProjectName.Rows.Count > 0 Then
                    refno.Text = xRequest_id
                    lblProjectName.Text = DT_ProjectName.Rows(0).Item("Project_Name")
                    lblProjectCode.Text = DT_ProjectName.Rows(0).Item("Project_Code")
                    txtCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Name")
                    lblEnterpriseName.Text = DT_ProjectName.Rows(0).Item("Enterprise_Name")
                    'txtLocationName.Text = DT_ProjectName.Rows(0).Item("Location_Name")
                    lblCustomerType.Text = DT_ProjectName.Rows(0).Item("Customer_Type")
                    txtDocumentDate.Text = DT_ProjectName.Rows(0).Item("Document_Date")
                    txtServiceDate.Text = DT_ProjectName.Rows(0).Item("Service_Date")
                    lblCustomerAssistantName.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_Name")
                    lblCustomerAssistantID.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_ID")
                    lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
                    lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
                    lblCustomerContactName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name")
                    lblCustomerContactTel.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
                    lblCustomerContactEmail.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
                    lblCompanyService.Text = DT_ProjectName.Rows(0).Item("Company_Service")
                    lblTypeOfService.Text = DT_ProjectName.Rows(0).Item("Type_Service")
                    txtDetailService.Text = DT_ProjectName.Rows(0).Item("Detail_Service")
                    txtSLA.Text = DT_ProjectName.Rows(0).Item("SLA")
                    lblMonitorDate.Text = DT_ProjectName.Rows(0).Item("Monitor_Date")
                    lblMonitorTime.Text = DT_ProjectName.Rows(0).Item("Monitor_Time")
                    txtFine.Text = DT_ProjectName.Rows(0).Item("Fine")

                    Dim DT_RO_Director As DataTable
                    DT_RO_Director = C.GetDataTable("select * from RO_Director where RO = '" + DT_ProjectName.Rows(0).Item("Area") + "'")
                    If DT_RO_Director.Rows.Count > 0 Then
                        lblHRO.Text = "(" & DT_RO_Director.Rows(0).Item("RO_name") & ")"
                        lblROMail.Text = DT_RO_Director.Rows(0).Item("RO_email")
                    End If
                    lblCreateProject.Text = "(" & DT_ProjectName.Rows(0).Item("Customer_Assistant_Name") & ")"

                    lblPrepare.Text = "(" & DT_ProjectName.Rows(0).Item("Cluster_name") & ")"
                    lblPrepaireEmail.Text = DT_ProjectName.Rows(0).Item("Cluster_email")
                Else
                    lblProjectName.Text = "-"
                End If

                strSql = "select * from dbo.List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' "
                DT_CAPEX = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
                    total_capex += DT_CAPEX.Rows(i).Item("Cost")
                Next
                'lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00") & "   THB"


                strSql = "select * from dbo.List_CAPEX_Mass where Document_No = '" + xRequest_id + "' and Status = '1'  "
                DT_CAPEX_Mass = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_CAPEX_Mass.Rows.Count - 1
                    total_capex_mass += DT_CAPEX_Mass.Rows(i).Item("Cost")
                Next
                lblTotalCAPEXMass.Text = Format(CDbl(total_capex_mass.ToString), "###,##0.00") & "   THB"



                strSql = "select * from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' "
                DT_OPEX = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_OPEX.Rows.Count - 1
                    total_opex += DT_OPEX.Rows(i).Item("Cost")
                Next
                lblTotalOPEX.Text = Format(CDbl(total_opex.ToString), "###,##0.00") & "   THB"


                strSql = "select * from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' "
                DT_OTHER = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_OTHER.Rows.Count - 1
                    total_other += DT_OTHER.Rows(i).Item("Cost")
                Next
                lblTotalOTHER.Text = Format(CDbl(total_other.ToString), "###,##0.00") & "   THB"


                strSql = "select * from dbo.List_Service where Document_No = '" + xRequest_id + "' "
                DT_Service = C.GetDataTable(strSql)
                If DT_Service.Rows.Count > 0 Then 'And DT_CAPEX.Rows.Count > 0 And DT_OPEX.Rows.Count > 0 Then

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


                    Dim OneTimPayment As Double
                    Dim MonthlyPrice As Double
                    Dim RevenueOne As Double
                    Dim RevenueNotOne As Double
                    Dim NetRevenueOne As Double
                    Dim NetRevenueNotOne As Double
                    Dim JasmineGroup As Double
                    Dim Other As Double
                    Dim CAPEXCorp As Double
                    Dim CAPEXMass As Double
                    Dim MKTCostOne As Double
                    Dim MKTCostNotOne As Double
                    Dim Budget1per As Double
                    Dim GiftPrice As Double
                    Dim CostOfInternet As Double
                    Dim DomesticCost As Double
                    Dim InternationalCost As Double
                    Dim Caching As Double
                    Dim CostOfNetwork As Double
                    Dim CostOfNOC As Double
                    Dim Penalty As Double
                    Dim OPEXOne As Double
                    Dim OPEXNotOne As Double
                    Dim RevenueAfterOperationOne As Double
                    Dim RevenueAfterOperationNotOne As Double
                    Dim CashFlowOne As Double
                    Dim CashFlowNotOne As Double
                    Dim PaybackSum As Double
                    Dim SparePart As Double
                    Dim SparePartPercent As Double = 0.01

                    GiftPrice = CDbl(DT_Service.Rows(0).Item("Gift").ToString)
                    Penalty = CDbl(DT_Service.Rows(0).Item("Penalty").ToString)

                    lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
                    If lblCustomerType.Text = "ลูกค้าทั่วไป" Then
                        MonthlyPrice = CDbl(DT_Service.Rows(0).Item("Monthly").ToString)
                        OneTimPayment = CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString)  'Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
                        SparePart = MonthlyPrice * 12 * SparePartPercent
                    ElseIf lblCustomerType.Text = "ลูกค้าราชการ" Then
                        MonthlyPrice = Math.Round(((CDbl(DT_Service.Rows(0).Item("Monthly").ToString) * 100) / 107) / DT_Service.Rows(0).Item("Contract").ToString, 2)
                        OneTimPayment = Math.Round((CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString) * 100) / 107, 2) 'Format(Math.Round((CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString) * 100) / 107, 2), "###,##0.00")
                        SparePart = (CDbl(DT_Service.Rows(0).Item("Monthly").ToString) * 100 / 107) * SparePartPercent
                    End If
                    lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString) + SparePart, "###,##0.00") & "   THB"

                    JasmineGroup = total_opex
                    Other = total_other
                    CAPEXCorp = total_capex + SparePart
                    CAPEXMass = total_capex_mass

                    RevenueOne = MonthlyPrice + OneTimPayment
                    NetRevenueOne = RevenueOne - JasmineGroup - Other - CAPEXMass
                    RevenueNotOne = MonthlyPrice
                    NetRevenueNotOne = RevenueNotOne - JasmineGroup - Other

                    If OneTimPayment = 0 Then
                        Budget1per = RevenueOne * 0.01
                    Else
                        Budget1per = (OneTimPayment / DT_Service.Rows(0).Item("Contract").ToString) * 0.01
                    End If
                    MKTCostOne = Budget1per + Budget1per + GiftPrice
                    MKTCostNotOne = Budget1per + Budget1per

                    DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * 50)
                    InternationalCost = CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * 500
                    Caching = (Math.Ceiling(CDbl(DT_Service.Rows(0).Item("International").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100)) - CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString)) * 200
                    CostOfInternet = DomesticCost + InternationalCost + Caching

                    CostOfNetwork = (CDbl(DT_Service.Rows(0).Item("Domestic").ToString) + CDbl(DT_Service.Rows(0).Item("EthernetIPV").ToString)) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * 60
                    CostOfNOC = (CDbl(DT_Service.Rows(0).Item("INL").ToString) + CDbl(DT_Service.Rows(0).Item("IPV").ToString) + CDbl(DT_Service.Rows(0).Item("INF").ToString)) * 250

                    OPEXOne = MKTCostOne + CostOfInternet + CostOfNetwork + CostOfNOC + Penalty
                    OPEXNotOne = MKTCostNotOne + CostOfInternet + CostOfNetwork + CostOfNOC

                    RevenueAfterOperationOne = NetRevenueOne - OPEXOne
                    RevenueAfterOperationNotOne = NetRevenueNotOne - OPEXNotOne

                    CashFlowOne = RevenueAfterOperationOne - CAPEXCorp
                    CashFlowNotOne = RevenueAfterOperationNotOne

                    Dim CashFlowPeriod(DT_Service.Rows(0).Item("Contract").ToString) As Double
                    Dim PaybackPeriod(DT_Service.Rows(0).Item("Contract").ToString) As Double

                    For i As Integer = 0 To CashFlowPeriod.Length - 1
                        If i = 0 Then
                            CashFlowPeriod(i) = CashFlowOne
                        Else
                            CashFlowPeriod(i) = CashFlowNotOne + CashFlowPeriod(i - 1)
                        End If
                    Next

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

                    For i As Integer = 0 To DT_Service.Rows(0).Item("Contract").ToString - 1
                        PaybackSum = PaybackSum + PaybackPeriod(i)
                    Next

                    lblRevenue.Text = Format(RevenueOne + (RevenueNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                    lblNetRevenue.Text = Format(NetRevenueOne + (NetRevenueNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                    lblCAPEX.Text = Format(CAPEXCorp + CAPEXMass, MoneyRoundup)
                    lblCAPEXCorp.Text = Format(CAPEXCorp, MoneyRoundup)
                    lblCAPEXMass.Text = Format(CAPEXMass, MoneyRoundup)
                    lblCashFlow.Text = Format(CashFlowOne + (CashFlowNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                    lblPayBack.Text = Format(PaybackSum, MoneyFmt)

                    lblOPEX.Text = Format(OPEXOne + (OPEXNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                    lblMKTCost.Text = Format(MKTCostOne + (MKTCostNotOne * (DT_Service.Rows(0).Item("Contract").ToString - 1)), MoneyRoundup)
                    lblCostOfInternet.Text = Format(CostOfInternet * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                    lblCostOfNetwork.Text = Format(CostOfNetwork * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                    lblCostOfNOC.Text = Format(CostOfNOC * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                    lblPenalty.Text = Format(Penalty, MoneyRoundup)
                    lblJasmineGorup.Text = Format(JasmineGroup * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)
                    lblOther.Text = Format(Other * DT_Service.Rows(0).Item("Contract").ToString, MoneyRoundup)

                    Dim values(lblContract.Text) As Double
                    For i As Integer = 0 To 0 'lblContract.Text - 1
                        If i = 0 Then
                            values(i) = lblCashFlow.Text 'Format(CashFlowOne, MoneyRoundup)
                        Else
                            values(i) = Format(CashFlowNotOne, MoneyRoundup)
                        End If
                    Next
                    Dim FixedRetRate As Double = 0.05
                    ' Calculate net present value.
                    Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
                    lblNPV.Text = Format(NetPVal, MoneyFmt)

                    If lblNetRevenue.Text <> 0 Then
                        lblMargin.Text = Format((lblNPV.Text / lblNetRevenue.Text) * 100, MoneyFmt)
                    Else
                        lblMargin.Text = "0"
                    End If

                    Dim ShowPayMonth(14) As Double
                    Dim ShowPayBack(14) As String
                    Dim per As Double = 0.3

                    'If lblOneTimePayment.Text - lblCAPEX.Text > 0 Then
                    '    lblPayBack.Text = "<1"
                    '    For j As Integer = 0 To ShowPayBack.Length - 1
                    '        ShowPayBack(j) = "<1"
                    '        ShowPayMonth(j) = Revenue * per
                    '        per = per + 0.1
                    '    Next
                    'Else
                    '    lblPayBack.Text = Format(lblCAPEX.Text / CashFlow, "#0.00")
                    '    For j As Integer = 0 To ShowPayBack.Length - 1
                    '        ShowPayBack(j) = Format(lblCAPEX.Text / ((Revenue * per) - ((Revenue * per * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas), "#0.0")
                    '        ShowPayMonth(j) = Revenue * per
                    '        per = per + 0.1
                    '    Next
                    'End If
                    'Dim arrList As New ArrayList()
                    'For j As Integer = 0 To ShowPayBack.Length - 1
                    '    arrList.Add(New ListItem(Format(CDbl(ShowPayMonth(j)), "###,##0.00"), ShowPayBack(j)))
                    'Next
                    'GridView1.DataSource = arrList
                    'GridView1.DataBind()

                    'For i As Integer = 0 To lblContract.Text - 1
                    '    If i = 0 Then
                    '        values(i) = CashFlowOneTime - lblCAPEX.Text
                    '    Else
                    '        values(i) = CashFlow
                    '    End If
                    'Next

                    'Dim FixedRetRate As Double = 0.05
                    '' Calculate net present value.
                    'Dim NetPVal As Double = NPV(FixedRetRate / lblContract.Text, values)
                    '' Display net present value.
                    '' MsgBox("The net present value of these cash flows is " & Format(NetPVal, MoneyFmt) & ".")
                    'lblNPV.Text = Format(NetPVal, MoneyFmt)
                    'lblMargin.Text = Format((lblNPV.Text / lblRevenue.Text) * 100, PercentFmt).ToString & "%"


                    btnSave.Visible = True
                    test.Visible = True
                End If


                'strSql = "select * from dbo.List_Model where Document_No = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "' "
                'DT_Service = C.GetDataTable(strSql)
                'If DT_Service.Rows.Count > 0 Then 'And DT_CAPEX.Rows.Count > 0 And DT_OPEX.Rows.Count > 0 Then
                '    lblContractYear.Text = Math.Round(CDbl(DT_Service.Rows(0).Item("Contract").ToString / 12), 0)
                '    lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
                '    lblMonthly.Text = Format(CDbl(DT_Service.Rows(0).Item("Monthly").ToString), "###,##0.00")
                '    lblOneTimePayment.Text = Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
                '    lblCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")

                '    ' Define money format.
                '    Dim MoneyFmt As String = "###,##0.00"
                '    ' Define percentage format.
                '    Dim PercentFmt As String = "#0.00"
                '    Dim values(lblContract.Text) As Double

                '    Dim Revenue As Double
                '    Dim MKTCost As Double
                '    Dim RevenueOneTime As Double
                '    Dim MKTCostOneTime As Double
                '    Dim CostOfInternet As Double
                '    Dim CostOfNetwork As Double
                '    Dim CostOfNOC As Double
                '    Dim Vas As Double = 0
                '    Dim CashFlow As Double
                '    Dim CashFlowOneTime As Double

                '    DT_InternetBWCost = C.GetDataTable("select * from List_Model where Document_No = '" + xRequest_id + "' ")
                '    Revenue = DT_Service.Rows(0).Item("Monthly")
                '    RevenueOneTime = (DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment"))
                '    MKTCost = (Revenue * 3) / 100
                '    MKTCostOneTime = (RevenueOneTime * 3) / 100
                '    CostOfNetwork = ((DT_Service.Rows(0).Item("EthernetIPV") * (DT_InternetBWCost.Rows(0).Item("IPV_Discount") / 100)) + DT_Service.Rows(0).Item("EthernetINP")) * DT_InternetBWCost.Rows(0).Item("Network_Price")
                '    CostOfNOC = (DT_Service.Rows(0).Item("TotalINLCurcuits") + DT_Service.Rows(0).Item("TotalIPVCurcuits") + DT_Service.Rows(0).Item("TotalINPCurcuits")) * DT_InternetBWCost.Rows(0).Item("NOC_Price")
                '    'Vas = C.GetDataTable("select SUM(Cost) 'VasCost' from dbo.List_OPEX where CreateBy='" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' ").Rows(0).Item("VasCost")
                '    If C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' ").Rows.Count > 0 Then
                '        Vas = Vas + C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' ").Rows(0).Item("VasCost")
                '    End If
                '    If C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' ").Rows.Count > 0 Then
                '        Vas = Vas + C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' -- and CreateBy='" + Session("uemail") + "' ").Rows(0).Item("VasCost")
                '    End If

                '    'lblRevenue.Text = ((Revenue * DT_Service.Rows(0).Item("Contract")) + DT_Service.Rows(0).Item("OneTimePayment")).ToString
                '    'lblMKTCost.Text = (MKTCost * DT_Service.Rows(0).Item("Contract")).ToString
                '    lblRevenue.Text = Format(CDbl((RevenueOneTime + (Revenue * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")
                '    lblMKTCost.Text = Format(CDbl(((MKTCostOneTime) + (MKTCost * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")

                '    If DT_InternetBWCost.Rows.Count > 0 Then
                '        DomesticCost = (DT_Service.Rows(0).Item("Domestic") * DT_InternetBWCost.Rows(0).Item("Domestic_Price")) * (DT_InternetBWCost.Rows(0).Item("Dom_Discount") / 100)
                '        InternationalCost = ((DT_Service.Rows(0).Item("International") - DT_Service.Rows(0).Item("Transit")) * DT_InternetBWCost.Rows(0).Item("All_International_Price")) * (DT_InternetBWCost.Rows(0).Item("Inter_Discount") / 100)
                '        Transit = DT_Service.Rows(0).Item("Transit") * DT_InternetBWCost.Rows(0).Item("Transit_Price")
                '        xDSL_FTTx = DT_Service.Rows(0).Item("ServicePrice")
                '        CostOfInternet = (DomesticCost + InternationalCost + Transit + xDSL_FTTx)
                '        lblCostOfInternet.Text = Format(CDbl((CostOfInternet * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                '        lblCostOfNetwork.Text = Format(CDbl((CostOfNetwork * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                '        lblCostOfNOC.Text = Format(CDbl((CostOfNOC * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                '    End If
                '    lblVas.Text = Format(CDbl((Vas * DT_Service.Rows(0).Item("Contract")).ToString.ToString), "###,##0.00")
                '    CashFlow = Revenue - MKTCost - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
                '    CashFlowOneTime = RevenueOneTime - MKTCostOneTime - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
                '    lblCashFlow.Text = Format(CDbl(((CashFlowOneTime - lblCAPEX.Text).ToString) + (CashFlow * (DT_Service.Rows(0).Item("Contract") - 1))), "###,##0.00")
                '    'lblCashFlow.Text = (((DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")) - (((DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")) * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas) + ((DT_Service.Rows(0).Item("Monthly") * (DT_Service.Rows(0).Item("Contract") - 1)) - (((DT_Service.Rows(0).Item("Monthly") * (DT_Service.Rows(0).Item("Contract") - 1)) * 3) / 100) - ((CostOfInternet + CostOfNetwork + CostOfNOC + Vas) * (DT_Service.Rows(0).Item("Contract") - 1)))).ToString

                '    'CashFlow = DT_Service.Rows(0).Item("Monthly") - ((DT_Service.Rows(0).Item("Monthly") * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas

                '    Dim ShowPayMonth(14) As Double
                '    Dim ShowPayBack(14) As String
                '    Dim per As Double = 0.3

                '    If lblOneTimePayment.Text - lblCAPEX.Text > 0 Then
                '        lblPayBack.Text = "<1"
                '        For j As Integer = 0 To ShowPayBack.Length - 1
                '            ShowPayBack(j) = "<1"
                '            ShowPayMonth(j) = Revenue * per
                '            per = per + 0.1
                '        Next
                '    Else
                '        lblPayBack.Text = Format(lblCAPEX.Text / CashFlow, "#0.00")
                '        For j As Integer = 0 To ShowPayBack.Length - 1
                '            ShowPayBack(j) = Format(lblCAPEX.Text / ((Revenue * per) - ((Revenue * per * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas), "#0.0")
                '            ShowPayMonth(j) = Revenue * per
                '            per = per + 0.1
                '        Next
                '    End If
                '    Dim arrList As New ArrayList()
                '    For j As Integer = 0 To ShowPayBack.Length - 1
                '        arrList.Add(New ListItem(Format(CDbl(ShowPayMonth(j)), "###,##0.00"), ShowPayBack(j)))
                '    Next
                '    GridView1.DataSource = arrList
                '    GridView1.DataBind()

                '    For i As Integer = 0 To lblContract.Text - 1
                '        If i = 0 Then
                '            values(i) = CashFlowOneTime - lblCAPEX.Text
                '        Else
                '            values(i) = CashFlow
                '        End If
                '    Next

                '    Dim FixedRetRate As Double = 0.05
                '    ' Calculate net present value.
                '    Dim NetPVal As Double = NPV(FixedRetRate / lblContract.Text, values)
                '    ' Display net present value.
                '    ' MsgBox("The net present value of these cash flows is " & Format(NetPVal, MoneyFmt) & ".")
                '    lblNPV.Text = Format(NetPVal, MoneyFmt)
                '    lblMargin.Text = Format((lblNPV.Text / lblRevenue.Text) * 100, PercentFmt).ToString & "%"

                'End If
                'If Not Page.IsPostBack Then

                '    'Dim strRO As String
                '    'Dim DTRO As DataTable
                '    'Dim strCluster As String
                '    'Dim DTCluster As DataTable

                '    'strRO = "select distinct RO from Cluster where Status = '1' "
                '    'C.SetDropDownList(ddlArea, strRO, "RO", "RO")

                '    'strCluster = "select distinct Cluster, Cluster_email, Cluster_name from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' "
                '    'C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster")
                '    'DTCluster = C.GetDataTable(strCluster)
                '    'If DTCluster.Rows.Count > 0 Then
                '    '    lblPrepare.Text = "(" & DTCluster.Rows(0).Item("Cluster_name") & ")"
                '    '    lblPrepaireEmail.Text = DTCluster.Rows(0).Item("Cluster_email")
                '    'End If



                '    strSql = "select Document_No, convert(varchar(10),Document_Date,103) 'Document_Date', convert(varchar(10),Service_Date,103) 'Service_Date' " + vbCr
                '    strSql += ", Area, Cluster, Customer_Name, Location_Name, Type_Service, Detail_Service  " + vbCr
                '    strSql += "from dbo.FeasibilityDocument where Document_No = '" + xRequest_id + "' and Status = '1' "
                '    DT_Document = C.GetDataTable(strSql)
                '    If DT_Document.Rows.Count > 0 Then
                '        lblDocumentNo.Text = DT_Document.Rows(0).Item("Document_No")
                '        txtDocumentDate.Text = DT_Document.Rows(0).Item("Document_Date")
                '        ddlArea.SelectedValue = DT_Document.Rows(0).Item("Area")
                '        txtLocationName.Text = DT_Document.Rows(0).Item("Location_Name")
                '        txtServiceDate.Text = DT_Document.Rows(0).Item("Service_Date")
                '        ddlCluster.SelectedValue = DT_Document.Rows(0).Item("Cluster")
                '        txtCustomerName.Text = DT_Document.Rows(0).Item("Customer_Name")
                '        'RadioButton4.Checked = False
                '        If DT_Document.Rows(0).Item("Type_Service") = "Re-New Service" Then
                '            RadioButton2.Checked = True
                '        ElseIf DT_Document.Rows(0).Item("Type_Service") = "Maintenance" Then
                '            RadioButton3.Checked = True
                '        Else
                '            RadioButton1.Checked = True
                '        End If
                '        txtDetailService.Text = DT_Document.Rows(0).Item("Detail_Service")
                '    End If
                'End If
            Else
                btnSave.Visible = False
                test.Visible = False
            End If
        Else
            btnSave.Visible = False
            test.Visible = False
        End If
        Set_Menu()


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim DT_CAPEX As New DataTable
        Dim DT_CAPEX_Mass As New DataTable
        Dim DT_OPEX As New DataTable
        Dim DT_OTHER As New DataTable
        Dim DT_Service As New DataTable
        Dim DT_ProjectName As New DataTable
        Dim DT_ProjectName_File As New DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim TypeService As String

        strSql = "select * from List_ProjectName where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        DT_ProjectName = C.GetDataTable(strSql)
        strSql = "select * from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_CAPEX = C.GetDataTable(strSql)
        strSql = "select * from List_CAPEX_Mass where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_CAPEX_Mass = C.GetDataTable(strSql)
        strSql = "select * from List_OPEX where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OPEX = C.GetDataTable(strSql)
        strSql = "select * from List_OTHER where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        DT_OTHER = C.GetDataTable(strSql)
        strSql = "select * from List_Service where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        DT_Service = C.GetDataTable(strSql)

        If DT_ProjectName.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล Project Name');focus();window.location.href='edit_project_name.aspx?request_id=" + xRequest_id + "';", True)
            'ElseIf DT_CAPEX.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล CAPEX');focus();window.location.href='edit_capex.aspx?request_id=" + xRequest_id + "';", True)
            'ElseIf DT_OPEX.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล OPEX');focus();window.location.href='edit_opex.aspx?request_id=" + xRequest_id + "';", True)
        ElseIf DT_Service.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล Service');focus();window.location.href='edit_service.aspx?request_id=" + xRequest_id + "';", True)
            'ElseIf txtDocumentDate.Text = "" Or txtDocumentDate.Text.Length <> 10 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ วันที่จัดทำ/ปรับปรุง');", True)
            '    txtDocumentDate.Focus()
            'ElseIf txtServiceDate.Text = "" Or txtServiceDate.Text.Length <> 10 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ วันที่เริ่มให้บริการ');", True)
            '    txtServiceDate.Focus()
        Else
            'If RadioButton2.Checked = True Then
            '    TypeService = RadioButton2.Text
            'ElseIf RadioButton3.Checked = True Then
            '    TypeService = RadioButton3.Text
            'Else
            '    TypeService = RadioButton1.Text
            'End If
            strSql = "select convert(varchar(5),right(isnull(max(Document_No),'FES000000-00000'),5) + 1) 'Max_Document',  left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) 'YearMonth' " + vbCr
            strSql += "from FeasibilityDocument " + vbCr
            strSql += "where substring(Document_No,4,6) = left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) "
            DT = C.GetDataTable(strSql)
            'Dim a As Integer = 200
            'Dim doc As String = CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")
            Dim doc_no As String = "FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")
            'strSql = "insert into FeasbilityDocument (Document_No,Document_Date,Service_Date,Area,Cluster,Customer_Name,Location_Name,Type_Service,Detail_Service,CreateBy,CreateDate,Status) values ('" + doc_no + "','" + C.CDateText(txtDocumentDate.Text) + "','" + C.CDateText(txtServiceDate.Text) + "','" + txtArea.Text + "','" + txtCluster.Text + "','" + txtCustomerName.Text + "','" + txtLocationName.Text + "','" + TypeService + "','" + txtDetailService.Text + "','" + Session("uemail") + "',getdate(),'1') "
            strSql = "Update FeasibilityDocument set Document_Date = '" + C.CDateText(txtDocumentDate.Text) + "', Service_Date = '" + C.CDateText(txtServiceDate.Text) + "', Area = '" + ddlArea.SelectedValue + "', Cluster = '" + ddlCluster.SelectedValue + "', Customer_Name = '" + txtCustomerName.Text + "', Type_Service = '" + lblTypeOfService.Text + "', Detail_Service = '" + txtDetailService.Text + "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate(), Status = '1'  "
            If request_status = "110" Then
                strSql += ", request_Status='0' " + vbCr
            End If
            strSql += "where Document_No = '" + xRequest_id + "' "
            C.ExecuteNonQuery(strSql)
            'strSql = "Update dbo.List_CAPEX set Document_No='" + doc_no + "', UpdateBy='weraphon.r', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            'strSql += "Update dbo.List_OPEX set Document_No='" + doc_no + "', UpdateBy='weraphon.r', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            'strSql += "Update dbo.List_Model set Document_No='" + doc_no + "', UpdateBy='weraphon.r', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
            'C.ExecuteNonQuery(strSql)

            'Dim vSqlIn As String = ""
            'Dim flow_id As String = "555"
            'vSqlIn += "INSERT INTO request_flow ( "
            'vSqlIn += "request_id, flow_id, depart_id, flow_step, next_step, "
            'vSqlIn += "send_uemail, uemail, approval, require_remark, require_file, add_next) "
            'vSqlIn += "select '" + doc_no + "', fp.flow_id, fp.depart_id, fp.flow_step, fp.next_step, "
            'vSqlIn += "dp.uemail, dp.uemail, fp.approval, fp.require_remark, fp.require_file, dp.add_next "
            'vSqlIn += "from flow_pattern fp "
            'vSqlIn += "join ( "
            'vSqlIn += " SELECT "
            'vSqlIn += "      dm.depart_id "
            'vSqlIn += "    , dm.depart_name "
            'vSqlIn += "    , dm.add_next "
            'vSqlIn += "    , uemail = STUFF(( "
            'vSqlIn += "          SELECT ';' + du.uemail "
            'vSqlIn += "          FROM depart_user du "
            'vSqlIn += "          WHERE dm.depart_id = du.depart_id "
            'vSqlIn += "          and start_date <= getdate() "
            'vSqlIn += "          and (expired_date is null or expired_date >= getdate()) "
            'vSqlIn += "          FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') "
            'vSqlIn += "     FROM department dm "
            'vSqlIn += ") dp on dp.depart_id = fp.depart_id "
            'vSqlIn += "where flow_id = " + flow_id + " "
            'vSqlIn += "order by fp.flow_step "
            'C.ExecuteNonQuery(vSqlIn)

            'Dim PreparedMail As String = "sophida.t"
            'vSqlIn = "Update request_flow set send_uemail = '" + PreparedMail + "', uemail = '" + PreparedMail + "' where request_id = '" + doc_no + "' and depart_id='2'"
            'C.ExecuteNonQuery(vSqlIn)

            'CF.InsertRequestFile("", "", doc_no, "")
            If request_status = "110" Then
                CF.UpdateRequest(xRequest_id, "", "", "", "", Session("uemail"), Session("uemail"), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
            End If
            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('บันทึกข้อมูลสำเร็จ');", True)
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลสำเร็จ');focus();window.location.href='edit_Summary.aspx?request_id=" + xRequest_id + "';", True)
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

    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        Dim strcluster As String
        Dim DTCluster As DataTable
        strcluster = "select distinct Cluster, Cluster_email, Cluster_name from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' "
        C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster")
        DTCluster = C.GetDataTable(strcluster)
        If DTCluster.Rows.Count > 0 Then
            lblPrepare.Text = "(" & DTCluster.Rows(0).Item("Cluster_name") & ")"
            lblPrepaireEmail.Text = DTCluster.Rows(0).Item("Cluster_email")
        End If
    End Sub

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

End Class
