Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Cls_Calculate
    Dim C As New Cls_Data

    Public Function GetProfit(ByVal User As String, ByVal Menu As String, ByVal DocNo As String, ByVal Contract As Double, ByVal MonthlyPrice As Double, ByVal OneTimePayment As Double, ByVal Marketing As Double, ByVal EntertainCustomer As Double, ByVal Penalty As Double, ByVal PenaltyLate As Double, ByVal Gift As Double) As DataTable
        Dim DT As New DataTable


        Return DT
    End Function

    Public Function GetTableService(ByVal User As String, ByVal Menu As String, ByVal DocNo As String) As DataTable
        Dim strSql As String
        Dim DT_Service As DataTable
        If Menu = "Create" Then
            strSql = "select * from dbo.List_Service where CreateBy='" + User + "' and Document_No is null "
        ElseIf Menu = "Edit" Then
            strSql = "select * from dbo.List_Service where Document_No = '" & DocNo & "' "
        ElseIf Menu = "Duplicate" Then
            'strSql = "select * from dbo.List_Service where Document_No = '" & DocNo & "' and CreateBy='" + User + "' "
            strSql = "select * from dbo.List_Service where Document_No = '" & DocNo & "' "
        Else
            strSql = "select * from dbo.List_Service where Document_No = '-'"
        End If

        DT_Service = C.GetDataTable(strSql)
        Return DT_Service
    End Function

    Public Function GetTableProjectName(ByVal User As String, ByVal Menu As String, ByVal DocNo As String) As DataTable
        Dim strSql As String
        Dim DT_ProjectName As DataTable
        If Menu = "Create" Or Menu = "CreateUpload" Then
            strSql = "select * from List_ProjectName where CreateBy='" + User + "' and Document_No is null "
        ElseIf Menu = "Duplicate" Then
            'strSql = "select * from List_ProjectName where Document_No = '" & DocNo & "' and CreateBy='" + User + "' "
            strSql = "select * from List_ProjectName where Document_No = '" & DocNo & "' "
        Else
            strSql = "select * from List_ProjectName where Document_No = '" & DocNo & "' "
        End If

        DT_ProjectName = C.GetDataTable(strSql)
        Return DT_ProjectName
    End Function

    Public Function GetTableSummary(ByVal xRequest_id As String) As DataTable
        Dim strSql As String
        Dim DT_Summary As DataTable
        strSql = "select * from List_Summary where Document_No = '" & xRequest_id & "' "
        DT_Summary = C.GetDataTable(strSql)
        Return DT_Summary
    End Function

    Public Function CalculateOverMinMax(ByVal Cost_perUnit As Double, ByVal Initial_Costper_Unit As Double, ByVal CustomerType As String) As Boolean
        Dim strSql As String
        Dim MinMax As Double = 0
        Dim DT As New DataTable
        strSql = "select * from dbo.InternetBWCost BWcost inner join dbo.CustomerType Cus on BWcost.BWCostType = Cus.BWCostType where CustomerType_name = '" & CustomerType & "' "
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

    Public Function ShowCAPEX_OPEX(ByVal User As String, ByVal Menu As String, ByVal DocNo As String, ByVal DT_Service As DataTable, ByVal CustomerType As String) As DataTable
        Dim strsql As String
        Dim DT_CAPEX As DataTable
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

        If Menu = "Create" Then
            strsql = "select * from dbo.List_CAPEX where CreateBy='" + User + "' and Document_No is null "
        Else
            strsql = "select * from dbo.List_CAPEX where Document_No = '" + DocNo + "' and Status = '1' "
        End If

        DT_CAPEX = C.GetDataTable(strsql)
        r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        'r += "<td width='65%' align='left'><b><u>" + txtProjectName.Text + "</u></b></td>"
        r += "<td width='15%' align='center'><b><u>Asset</u></b></td>"
        r += "<td width='15%' align='right'><b><u>บาท</u></b></td>"
        r += "</tr>"
        For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_CAPEX.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_CAPEX.Rows(i).Item("Initial_Cost_perUnit").ToString), CustomerType) Then
                r += "<td style='color: Red'>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
                r += "<td align='center' style='color: Red'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
                r += "<td align='right' style='color: Red'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            Else
                r += "<td>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
                r += "<td align='center'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
                r += "<td align='right'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
            End If
            r += "</tr>"
            total_capex += DT_CAPEX.Rows(i).Item("Cost")
        Next
        r += "</table>"
        'CAPEX_Detail.InnerHtml = r

        If Menu = "Create" Then
            strsql = "select * from dbo.List_OPEX where CreateBy='" + User + "' and Document_No is null "
        Else
            strsql = "select * from dbo.List_OPEX where Document_No = '" + DocNo + "' and Status = '1' "
        End If

        DT_OPEX = C.GetDataTable(strsql)
        r = "<table style='width: 100%'>"
        r += "<tr class='font-weight-bold'>"
        r += "<td width='70%' align='left'><u>JASMINE GROUP</u></td>"
        r += "<td width='15%' align='center'><u>หน่วย<u></td>"
        r += "<td width='15%' align='right'><u>บาท/เดือน<u></td>"
        r += "</tr>"
        For i As Integer = 0 To DT_OPEX.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_OPEX.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_OPEX.Rows(i).Item("Initial_Cost_perUnit").ToString), CustomerType) Then
                r += "<td align='left' style='color: Red'>" + DT_OPEX.Rows(i).Item("OPEX_Name").ToString + "</td>"
                r += "<td align='center' style='color: Red'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right' style='color: Red'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
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

        If Menu = "Create" Then
            strsql = "select * from dbo.List_OTHER where CreateBy='" + User + "' and Document_No is null "
        Else
            strsql = "select * from dbo.List_OTHER where Document_No = '" + DocNo + "' and Status = '1' "
        End If

        DT_OTHER = C.GetDataTable(strsql)
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><u>OTHER</u></td>"
        r += "<td align='center'><u>หน่วย<u></td>"
        r += "<td align='right'><u>บาท/เดือน<u></td>"
        r += "</tr>"
        For i As Integer = 0 To DT_OTHER.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_OTHER.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_OTHER.Rows(i).Item("Initial_Cost_perUnit").ToString), CustomerType) Then
                r += "<td align='left'  style='color: Red'>" + DT_OTHER.Rows(i).Item("OTHER_Name").ToString + "</td>"
                r += "<td align='center'  style='color: Red'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right'  style='color: Red'>" + Format(CDbl(DT_OTHER.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
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

        If Menu = "Create" Then
            strsql = "select * from dbo.List_Management where CreateBy='" + User + "' and Document_No is null "
        Else
            strsql = "select * from dbo.List_Management where Document_No = '" + DocNo + "' and Status = '1' "
        End If

        DT_Management = C.GetDataTable(strsql)
        r += "<tr class='font-weight-bold'>"
        r += "<td align='left'><u>Management</u></td>"
        r += "<td align='center'><u>หน่วย<u></td>"
        r += "<td align='right'><u>บาท/เดือน<u></td>"
        r += "</tr>"
        r += "<tr>"
        r += "<td align='left'>ค่าบริการ NOC</td>"
        'r += "<td align='center'>" + Format(CDbl(DT_Service.Rows(0).Item("NOC")), "###,##0") + "</td>"
        r += "<td align='right'>" + Format(CDbl(NOCTotalCost), "###,##0.00") + "</td>"
        r += "</tr>"
        total_management += CDbl(NOCTotalCost)

        For i As Integer = 0 To DT_Management.Rows.Count - 1
            r += "<tr>"
            If CalculateOverMinMax(CDbl(DT_Management.Rows(i).Item("Cost_perUnit").ToString), CDbl(DT_Management.Rows(i).Item("Initial_Cost_perUnit").ToString), CustomerType) Then
                r += "<td align='left'  style='color: Red'>" + DT_Management.Rows(i).Item("Management_Name").ToString + "</td>"
                r += "<td align='center'  style='color: Red'>" + Format(CDbl(DT_Management.Rows(i).Item("Number").ToString), "###,##0") + "</td>"
                r += "<td align='right'  style='color: Red'>" + Format(CDbl(DT_Management.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
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

        'OPEX_Detail.InnerHtml = r

        'lblNOCTotalCost.Text = total_management
        'lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")
        'lblTotalOPEX.Text = Format(CDbl(total_opex.ToString), "###,##0.00")
        'lblTotalOTHER.Text = Format(CDbl(total_other.ToString), "###,##0.00")
        'lblTotalManagement.Text = Format(CDbl(total_management.ToString), "###,##0.00")

        Dim totalopex_all = total_opex + total_other + total_management
        'lblTotalOPEXALL.Text = Format(CDbl(totalopex_all.ToString), "###,##0.00")

        'lblUtil.Text = "Util." & DT_Service.Rows(0).Item("INLUtilization").ToString & "%"

        Dim dt As New DataTable
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("TotalCapex", GetType(Double))
        dt.Columns.Add("TotalOpex", GetType(Double))
        dt.Columns.Add("TotalOther", GetType(Double))
        dt.Columns.Add("TotalManagement", GetType(Double))
        dt.Columns.Add("TotalOpexAll", GetType(Double))
        dt.Columns.Add("Utilize", GetType(String))
        Dim N As Integer = dt.Columns("ID").AutoIncrement
        If DT_Service.Rows.Count > 0 Then
            dt.Rows.Add(N, total_capex, total_opex, total_other, total_management, totalopex_all, "Util." & DT_Service.Rows(0).Item("INLUtilization").ToString & "%")
        Else
            dt.Rows.Add(N, total_capex, total_opex, total_other, total_management, totalopex_all, "")
        End If

        Return dt
    End Function

    Public Function ShowService(ByVal User As String, ByVal Menu As String, ByVal DocNo As String, ByVal DT_ProjectName As DataTable, ByVal DT_Service As DataTable, ByVal Contract As Integer, ByVal MonthlyPrice As Double, ByVal OneTimePayment As Double, ByVal Marketing As Double, ByVal EntertainCustomer As Double, ByVal Penalty As Double, ByVal PenaltyLate As Double, ByVal Gift As Double, ByVal TotalCapex As Double, ByVal TotalOpex As Double, ByVal TotalOther As Double, ByVal TotalManagement As Double) As DataTable
        Dim dt As New DataTable

        ' Define money format.
        Dim MoneyFmt As String = "###,##0.00"
        Dim MoneyRoundup As String = "###,##0"
        ' Define percentage format.
        Dim PercentFmt As String = "#0.00"

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
        Dim GiftPrice As Double
        Dim CostOfInternet As Double
        Dim DomesticCost As Double
        Dim InternationalCost As Double
        Dim Caching As Double
        Dim CostOfNetwork As Double

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

        Dim Revenue As Double
        Dim Revenue_Profit As Double
        Dim RevenuePercent As String
        Dim OPEX As Double
        Dim OPEX_Profit As Double
        Dim MarketingCost As Double
        Dim MarketingCost_Profit As Double
        Dim InternetBandwidthCost As Double
        Dim NetworkBandwidthCost As Double
        Dim NOCCost As Double
        Dim JasmineGroupCost As Double
        Dim OtherCost As Double
        Dim OtherCost_Profit As Double
        Dim OtherPercent As String
        Dim PenaltyCost As Double
        Dim PenaltyCost_Profit As Double
        Dim RevenueAfter As Double
        Dim RevenueAfter_Profit As Double
        Dim CAPEX As Double
        Dim CAPEX_Profit As Double
        Dim CAPEXPercent As String
        Dim CashFlowTotal As Double
        Dim CashFlowTotal_Profit As Double
        Dim Payback As String
        Dim Payback_Profit As String
        Dim Margin As String
        Dim Margin_Profit As String
        Dim NPVTotal As String
        Dim NPVTotal_Profit As String

        If DT_Service.Rows.Count > 0 Then

            RevenueOne = MonthlyPrice + OneTimePayment
            RevenueNotOne = MonthlyPrice

            MKTCostOne = ((Marketing / 100) * RevenueOne) + ((EntertainCustomer / 100) * RevenueOne) + Gift
            MKTCostNotOne = ((Marketing / 100) * RevenueNotOne) + ((EntertainCustomer / 100) * RevenueNotOne)

            DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("DomesticCost").ToString))
            InternationalCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * CDbl(DT_Service.Rows(0).Item("TransitCost").ToString))
            Caching = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Caching").ToString) * CDbl(DT_Service.Rows(0).Item("AllInternationalCost").ToString))
            CostOfInternet = DomesticCost + InternationalCost + Caching

            PortCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("NetWorkPort").ToString) * CDbl(DT_Service.Rows(0).Item("NetWorkPortCost").ToString))
            NetworkCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("EthernetNetwork").ToString) * (CDbl(DT_Service.Rows(0).Item("NetworkUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("NetworkCost").ToString))
            CostOfNetwork = PortCost + NetworkCost

            JasmineGroup = TotalOpex
            PenaltyOne = PenaltyLate + ((Penalty / 100) * RevenueOne)
            PenaltyNotOne = (Penalty / 100) * RevenueNotOne

            OPEXOne = MKTCostOne + CostOfInternet + CostOfNetwork + JasmineGroup + TotalOther + TotalManagement + PenaltyOne
            OPEXNotOne = MKTCostNotOne + CostOfInternet + CostOfNetwork + JasmineGroup + TotalOther + TotalManagement + PenaltyNotOne


            RevenueAfterOperationOne = RevenueOne - OPEXOne
            RevenueAfterOperationNotOne = RevenueNotOne - OPEXNotOne

            CashFlowOne = RevenueAfterOperationOne - TotalCapex
            CashFlowNotOne = RevenueAfterOperationNotOne

            Dim CashFlowPeriod(Contract) As Double
            Dim PaybackPeriod(Contract) As Double

            For i As Integer = 0 To CashFlowPeriod.Length - 1
                If i = 0 Then
                    CashFlowPeriod(i) = CashFlowOne
                Else
                    CashFlowPeriod(i) = CashFlowNotOne + CashFlowPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationOne >= TotalCapex Then
                Payback = "<=1"
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
                For i As Integer = 0 To Contract - 1
                    PaybackSum = PaybackSum + PaybackPeriod(i)
                Next
                Payback = Format(PaybackSum, MoneyFmt)
            End If



            Revenue = RevenueOne + (RevenueNotOne * (Contract - 1))
            Dim Revenue_total = RevenueOne + (RevenueNotOne * (Contract - 1))
            Revenue_Profit = Revenue_total
            RevenuePercent = "100%"


            OPEX = OPEXOne + (OPEXNotOne * (Contract - 1))

            MarketingCost = MKTCostOne + (MKTCostNotOne * (Contract - 1))
            MarketingCost_Profit = MKTCostOne + (MKTCostNotOne * (Contract - 1))
            InternetBandwidthCost = CostOfInternet * Contract
            NetworkBandwidthCost = CostOfNetwork * Contract
            NOCCost = TotalManagement * Contract
            PenaltyCost = PenaltyOne + (PenaltyNotOne * (Contract - 1))
            PenaltyCost_Profit = Format(PenaltyOne + (PenaltyNotOne * (Contract - 1)), MoneyRoundup)
            JasmineGroupCost = JasmineGroup * Contract

            OtherCost = TotalOther * Contract
            OtherCost_Profit = TotalOther * Contract
            OtherPercent = Format((OtherCost / Revenue) * 100, PercentFmt) & "%"

            Dim OPEX_total = (MKTCostOne + (MKTCostNotOne * (Contract - 1))) + OtherCost_Profit + PenaltyCost_Profit
            OPEX_Profit = OPEX_total

            Dim Revenue_Operation As Double = (RevenueOne + (RevenueNotOne * (Contract - 1))) - (OPEXOne + (OPEXNotOne * (Contract - 1)))
            RevenueAfter = Revenue_Operation
            Dim Revenue_Operationtotal As Double = Revenue_total - OPEX_total
            RevenueAfter_Profit = Revenue_Operationtotal

            CAPEX = TotalCapex
            CAPEX_Profit = TotalCapex
            CAPEXPercent = Format((CAPEX / Revenue) * 100, PercentFmt) & "%"

            CashFlow = Revenue_Operation - TotalCapex
            Dim CashFlow_total As Double = Revenue_Operationtotal - TotalCapex
            CashFlowTotal = CDbl(CashFlow.ToString)
            CashFlowTotal_Profit = CDbl(CashFlow_total.ToString)

            Dim values(Contract) As Double
            For i As Integer = 0 To Contract - 1
                If i = 0 Then
                    values(i) = CashFlowOne ' Format(CashFlowOne, MoneyRoundup)
                Else
                    values(i) = CashFlowNotOne 'Format(CashFlowNotOne, MoneyRoundup)
                End If
            Next
            Dim FixedRetRate As Double = 0.05
            ' Calculate net present value.
            Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
            NPVTotal = Format(NetPVal, MoneyFmt)
            If NetPVal <> 0 Then
                Margin = Format((CDbl(NPVTotal) / Revenue) * 100, PercentFmt).ToString
            Else
                Margin = "0"
            End If

            '/////////////  Marginal Profit ///////////////////

            Dim OPEXProfitOne As Double
            Dim OPEXProfitNotOne As Double
            Dim RevenueAfterOperationProfitOne As Double
            Dim RevenueAfterOperationProfitNotOne As Double
            Dim CashFlowProfitOne As Double
            Dim CashFlowProfitnotOne As Double
            Dim PaybackProfitSum As Double

            OPEXProfitOne = MKTCostOne + TotalOther + PenaltyOne
            OPEXProfitNotOne = MKTCostNotOne + TotalOther + PenaltyNotOne
            RevenueAfterOperationProfitOne = RevenueOne - OPEXProfitOne
            RevenueAfterOperationProfitNotOne = RevenueNotOne - OPEXProfitNotOne

            CashFlowProfitOne = RevenueAfterOperationProfitOne - TotalCapex
            CashFlowProfitnotOne = RevenueAfterOperationProfitNotOne

            Dim CashFlowProfitPeriod(Contract) As Double
            Dim PaybackProfitPeriod(Contract) As Double

            For i As Integer = 0 To CashFlowProfitPeriod.Length - 1
                If i = 0 Then
                    CashFlowProfitPeriod(i) = CashFlowProfitOne
                Else
                    CashFlowProfitPeriod(i) = CashFlowProfitnotOne + CashFlowProfitPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationProfitOne >= TotalCapex Then
                Payback_Profit = "<=1"
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
                For i As Integer = 0 To Contract - 1
                    PaybackProfitSum = PaybackProfitSum + PaybackProfitPeriod(i)
                Next
                Payback_Profit = Format(PaybackProfitSum, MoneyFmt)
            End If

            Dim valuesProfit(Contract) As Double
            For i As Integer = 0 To Contract - 1
                If i = 0 Then
                    valuesProfit(i) = CashFlowProfitOne 'Format(CashFlowProfitOne, MoneyRoundup)
                Else
                    valuesProfit(i) = CashFlowProfitnotOne 'Format(CashFlowProfitnotOne, MoneyRoundup)
                End If
            Next

            ' Calculate net present value.
            Dim NetPValProfit As Double = NPV(FixedRetRate / 12, valuesProfit)
            NPVTotal_Profit = Format(NetPValProfit, MoneyFmt)
            If NetPValProfit <> 0 Then
                Margin_Profit = Format((CDbl(NPVTotal_Profit) / Revenue) * 100, PercentFmt).ToString
            Else
                Margin_Profit = "0"
            End If

        Else
            RevenueOne = MonthlyPrice + OneTimePayment
            RevenueNotOne = MonthlyPrice

            MKTCostOne = ((Marketing / 100) * RevenueOne) + ((EntertainCustomer / 100) * RevenueOne) + Gift
            MKTCostNotOne = ((Marketing / 100) * RevenueNotOne) + ((EntertainCustomer / 100) * RevenueNotOne)

            RevenueAfterOperationOne = RevenueOne - OPEXOne
            RevenueAfterOperationNotOne = RevenueNotOne - OPEXNotOne

            CashFlowOne = RevenueAfterOperationOne - TotalCapex
            CashFlowNotOne = RevenueAfterOperationNotOne

            JasmineGroup = TotalOpex
            PenaltyOne = PenaltyLate + ((Penalty / 100) * RevenueOne)
            PenaltyNotOne = (Penalty / 100) * RevenueNotOne

            OPEXOne = MKTCostOne + CostOfInternet + CostOfNetwork + JasmineGroup + TotalOther + TotalManagement + PenaltyOne
            OPEXNotOne = MKTCostNotOne + CostOfInternet + CostOfNetwork + JasmineGroup + TotalOther + TotalManagement + PenaltyNotOne


            RevenueAfterOperationOne = RevenueOne - OPEXOne
            RevenueAfterOperationNotOne = RevenueNotOne - OPEXNotOne

            CashFlowOne = RevenueAfterOperationOne - TotalCapex
            CashFlowNotOne = RevenueAfterOperationNotOne

            Dim CashFlowPeriod(Contract) As Double
            Dim PaybackPeriod(Contract) As Double

            For i As Integer = 0 To CashFlowPeriod.Length - 1
                If i = 0 Then
                    CashFlowPeriod(i) = CashFlowOne
                Else
                    CashFlowPeriod(i) = CashFlowNotOne + CashFlowPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationOne >= TotalCapex Then
                Payback = "<=1"
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
                For i As Integer = 0 To Contract - 1
                    PaybackSum = PaybackSum + PaybackPeriod(i)
                Next
                Payback = Format(PaybackSum, MoneyFmt)
            End If



            Revenue = RevenueOne + (RevenueNotOne * (Contract - 1))
            Dim Revenue_total = RevenueOne + (RevenueNotOne * (Contract - 1))
            Revenue_Profit = Revenue_total
            RevenuePercent = "100%"


            OPEX = OPEXOne + (OPEXNotOne * (Contract - 1))

            MarketingCost = MKTCostOne + (MKTCostNotOne * (Contract - 1))
            MarketingCost_Profit = MKTCostOne + (MKTCostNotOne * (Contract - 1))
            InternetBandwidthCost = CostOfInternet * Contract
            NetworkBandwidthCost = CostOfNetwork * Contract
            NOCCost = TotalManagement * Contract
            PenaltyCost = PenaltyOne + (PenaltyNotOne * (Contract - 1))
            PenaltyCost_Profit = Format(PenaltyOne + (PenaltyNotOne * (Contract - 1)), MoneyRoundup)
            JasmineGroupCost = JasmineGroup * Contract

            OtherCost = TotalOther * Contract
            OtherCost_Profit = TotalOther * Contract
            OtherPercent = Format((OtherCost / Revenue) * 100, PercentFmt) & "%"

            Dim OPEX_total = (MKTCostOne + (MKTCostNotOne * (Contract - 1))) + OtherCost_Profit + PenaltyCost_Profit
            OPEX_Profit = OPEX_total

            Dim Revenue_Operation As Double = (RevenueOne + (RevenueNotOne * (Contract - 1))) - (OPEXOne + (OPEXNotOne * (Contract - 1)))
            RevenueAfter = Revenue_Operation
            Dim Revenue_Operationtotal As Double = Revenue_total - OPEX_total
            RevenueAfter_Profit = Revenue_Operationtotal

            CAPEX = TotalCapex
            CAPEX_Profit = TotalCapex
            CAPEXPercent = Format((CAPEX / Revenue) * 100, PercentFmt) & "%"

            CashFlow = Revenue_Operation - TotalCapex
            Dim CashFlow_total As Double = Revenue_Operationtotal - TotalCapex
            CashFlowTotal = CDbl(CashFlow.ToString)
            CashFlowTotal_Profit = CDbl(CashFlow_total.ToString)

            Dim values(Contract) As Double
            For i As Integer = 0 To Contract - 1
                If i = 0 Then
                    values(i) = CashFlowOne ' Format(CashFlowOne, MoneyRoundup)
                Else
                    values(i) = CashFlowNotOne 'Format(CashFlowNotOne, MoneyRoundup)
                End If
            Next
            Dim FixedRetRate As Double = 0.05
            ' Calculate net present value.
            Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
            NPVTotal = Format(NetPVal, MoneyFmt)
            If NetPVal <> 0 Then
                Margin = Format((CDbl(NPVTotal) / Revenue) * 100, PercentFmt).ToString
            Else
                Margin = "0"
            End If

            '/////////////  Marginal Profit ///////////////////

            Dim OPEXProfitOne As Double
            Dim OPEXProfitNotOne As Double
            Dim RevenueAfterOperationProfitOne As Double
            Dim RevenueAfterOperationProfitNotOne As Double
            Dim CashFlowProfitOne As Double
            Dim CashFlowProfitnotOne As Double
            Dim PaybackProfitSum As Double

            OPEXProfitOne = MKTCostOne + TotalOther + PenaltyOne
            OPEXProfitNotOne = MKTCostNotOne + TotalOther + PenaltyNotOne
            RevenueAfterOperationProfitOne = RevenueOne - OPEXProfitOne
            RevenueAfterOperationProfitNotOne = RevenueNotOne - OPEXProfitNotOne

            CashFlowProfitOne = RevenueAfterOperationProfitOne - TotalCapex
            CashFlowProfitnotOne = RevenueAfterOperationProfitNotOne

            Dim CashFlowProfitPeriod(Contract) As Double
            Dim PaybackProfitPeriod(Contract) As Double

            For i As Integer = 0 To CashFlowProfitPeriod.Length - 1
                If i = 0 Then
                    CashFlowProfitPeriod(i) = CashFlowProfitOne
                Else
                    CashFlowProfitPeriod(i) = CashFlowProfitnotOne + CashFlowProfitPeriod(i - 1)
                End If
            Next

            If RevenueAfterOperationProfitOne >= TotalCapex Then
                Payback_Profit = "<=1"
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
                For i As Integer = 0 To Contract - 1
                    PaybackProfitSum = PaybackProfitSum + PaybackProfitPeriod(i)
                Next
                Payback_Profit = Format(PaybackProfitSum, MoneyFmt)
            End If

            Dim valuesProfit(Contract) As Double
            For i As Integer = 0 To Contract - 1
                If i = 0 Then
                    valuesProfit(i) = CashFlowProfitOne 'Format(CashFlowProfitOne, MoneyRoundup)
                Else
                    valuesProfit(i) = CashFlowProfitnotOne 'Format(CashFlowProfitnotOne, MoneyRoundup)
                End If
            Next

            ' Calculate net present value.
            Dim NetPValProfit As Double = NPV(FixedRetRate / 12, valuesProfit)
            NPVTotal_Profit = Format(NetPValProfit, MoneyFmt)
            If NetPValProfit <> 0 Then
                Margin_Profit = Format((CDbl(NPVTotal_Profit) / Revenue) * 100, PercentFmt).ToString
            Else
                Margin_Profit = "0"
            End If
        End If

        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Revenue", GetType(Double))
        dt.Columns.Add("Revenue_Profit", GetType(Double))
        dt.Columns.Add("RevenuePercent", GetType(String))
        dt.Columns.Add("OPEX", GetType(Double))
        dt.Columns.Add("OPEX_Profit", GetType(Double))
        dt.Columns.Add("MarketingCost", GetType(Double))
        dt.Columns.Add("MarketingCost_Profit", GetType(Double))
        dt.Columns.Add("InternetCost", GetType(Double))
        dt.Columns.Add("NetworkCost", GetType(Double))
        dt.Columns.Add("NOCCost", GetType(Double))
        dt.Columns.Add("JasmineGroupCost", GetType(Double))
        dt.Columns.Add("OtherCost", GetType(Double))
        dt.Columns.Add("OtherCost_Profit", GetType(Double))
        dt.Columns.Add("OtherPercent", GetType(String))
        dt.Columns.Add("PenaltyCost", GetType(Double))
        dt.Columns.Add("PenaltyCost_Profit", GetType(Double))
        dt.Columns.Add("RevenueAfter", GetType(Double))
        dt.Columns.Add("RevenueAfter_Profit", GetType(Double))
        dt.Columns.Add("CAPEX", GetType(Double))
        dt.Columns.Add("CAPEX_Profit", GetType(Double))
        dt.Columns.Add("CAPEXPercent", GetType(String))
        dt.Columns.Add("CashFlow", GetType(Double))
        dt.Columns.Add("CashFlow_Profit", GetType(Double))
        dt.Columns.Add("Payback", GetType(String))
        dt.Columns.Add("Payback_Profit", GetType(String))
        dt.Columns.Add("Margin", GetType(String))
        dt.Columns.Add("Margin_Profit", GetType(String))
        dt.Columns.Add("NPV", GetType(String))
        dt.Columns.Add("NPV_Profit", GetType(String))
        Dim N As Integer = dt.Columns("ID").AutoIncrement
        dt.Rows.Add(N, Revenue, Revenue_Profit, RevenuePercent, OPEX, OPEX_Profit, MarketingCost, MarketingCost_Profit, InternetBandwidthCost, NetworkBandwidthCost, NOCCost, JasmineGroupCost, OtherCost, OtherCost_Profit, OtherPercent, PenaltyCost, PenaltyCost_Profit, RevenueAfter, RevenueAfter_Profit, CAPEX, CAPEX_Profit, CAPEXPercent, CashFlowTotal, CashFlowTotal_Profit, Payback, Payback_Profit, Margin, Margin_Profit, NPVTotal, NPVTotal_Profit)

        Return dt
    End Function

    Public Function DuplicateProject(ByVal User As String, ByVal Menu As String, ByVal DocNo As String) As String
        Dim strSql As String = ""
        Dim DT_ProjectName As New DataTable
        Dim DT_Service As New DataTable
        Dim DT_CAPEX As New DataTable
        Dim DT_OPEX As New DataTable
        Dim DT_OTHER As New DataTable
        Dim DT_Management As New DataTable
        Dim DT_Document_Project As New DataTable

        Try
            DT_ProjectName = GetTableProjectName(User, "Duplicate", DocNo)
            If DT_ProjectName.Rows.Count > 0 Then
                Dim DT_P As New DataTable
                DT_P = GetTableProjectName(User, "Create", "")
                If DT_P.Rows.Count > 0 Then
                    C.ExecuteNonQuery("delete from tmpList_CAsst where CreateBy = '" & User & "' and isnull(ProjectName_id, '') in ('','" & DT_P.Rows(0).Item("id_List") & "') ")
                    C.ExecuteNonQuery("delete from tmpList_CT where CreateBy = '" & User & "' and isnull(ProjectName_id, '') in ('','" & DT_P.Rows(0).Item("id_List") & "') ")
                End If

                C.ExecuteNonQuery("delete from List_ProjectName where CreateBy = '" & User & "' and Document_No is null")

                strSql = "insert into List_ProjectName " + vbCr
                strSql += "(Project_Name, Project_Code, Customer_Name, Enterprise_Name, " + vbCr
                strSql += "Location_Name, Customer_Type, Document_Date, Service_Date, " + vbCr
                strSql += "Company_Service, Type_Service, Detail_Project, Detail_Service, SLA, SLA_File,MTTR, " + vbCr
                strSql += "Monitor_Date, Monitor_Time, Fine, Fine_File, Doc_File, " + vbCr
                strSql += "Status, Special_Price, Upload_Project,  " + vbCr
                strSql += "Contract, Monthly, OneTimePayment, MonthlySummary, OneTimeSummary, TotalProject, "
                strSql += "Marketing, EntertainCustomer, Gift, Penalty, PenaltyLate,  " + vbCr
                strSql += "TotalMarketing, TotalEntertainCustomer, TotalGift, TotalPenalty, TotalPenaltyLate,  " + vbCr
                strSql += "CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr

                strSql += "Select Project_Name, Project_Code, Customer_Name, Enterprise_Name, " + vbCr
                strSql += "Location_Name, Customer_Type, convert(varchar(10),getdate(),111) 'Document_Date', convert(varchar(10),getdate(),111) 'Service_Date', " + vbCr
                strSql += "Company_Service, Type_Service, Detail_Project, Detail_Service, SLA, '', MTTR, " + vbCr
                strSql += "Monitor_Date, Monitor_Time, Fine, '', '', " + vbCr
                strSql += "Status, Special_Price, Upload_Project,  " + vbCr
                strSql += "Contract, Monthly, OneTimePayment, MonthlySummary, OneTimeSummary, TotalProject, "
                strSql += "Marketing, EntertainCustomer, Gift, Penalty, PenaltyLate,  " + vbCr
                strSql += "TotalMarketing, TotalEntertainCustomer, TotalGift, TotalPenalty, TotalPenaltyLate,  " + vbCr
                strSql += "'" & User & "', getdate(), '" & User & "', getdate() " + vbCr
                strSql += "From List_ProjectName Where Document_No = '" & DocNo & "' /*and CreateBy='" + User + "'*/ " + vbCr

                'Dim DT_casst_cluster As New DataTable
                'Dim DT_casst As New DataTable
                'Dim strSql_casst_cluster As String
                'DT_casst = C.GetDataTable("select * from tmpList_CAsst where CreateBy='" + User + "' and isnull(ProjectName_id, '') = ''")
                'If DT_casst.Rows.Count = 0 Then
                '    strSql_casst_cluster = "select  lo.Login_name, cl.RO 'Area' , cl.Cluster 'Cluster', Cluster_name 'Cluster_Name', cl.Cluster_email 'Cluster_Email' " + vbCr
                '    strSql_casst_cluster += "from dbo.UserLogin lo inner join dbo.UserBranch br on lo.Login_name = br.Login_name " + vbCr
                '    strSql_casst_cluster += "inner join dbo.Cluster cl on br.Cluster = cl.Cluster " + vbCr
                '    strSql_casst_cluster += "where lo.Login_name = '" + User + "' " + vbCr
                '    DT_casst_cluster = C.GetDataTable(strSql_casst_cluster)
                '    If DT_casst_cluster.Rows.Count > 0 Then
                '        strSql_casst_cluster = "insert into tmpList_CAsst(Customer_Assistant_ID, Customer_Assistant_Name, Customer_Assistant_Email, Area, Cluster, Cluster_Name, Cluster_Email, CreateBy, CreateDate) " + vbCr
                '        strSql_casst_cluster += "values ('" & Session("uid") & "','" & Session("ufullname") & "','" & hide_user.Value & "@jasmine.com','" & DT_casst_cluster.Rows(0).Item("Area") & "','" & DT_casst_cluster.Rows(0).Item("Cluster") & "','" & DT_casst_cluster.Rows(0).Item("Cluster_Name") & "','" & DT_casst_cluster.Rows(0).Item("Cluster_Email") & "','" + User + "',getdate())"
                '        C.ExecuteNonQuery(strSql_casst_cluster)
                '    End If
                'End If

                strSql += "insert into tmpList_CT(Customer_Contact_Name,Customer_Contact_Tel,Customer_Contact_Email,CreateBy) " + vbCr
                strSql += "select Customer_Contact_Name,Customer_Contact_Tel,Customer_Contact_Email,'" + User + "' " + vbCr
                strSql += "from tmpList_CT  where isnull(ProjectName_id, '') in ( "
                strSql += "select id_List from dbo.List_ProjectName where Document_No='" & DocNo & "') " + vbCr

            End If

            DT_Service = GetTableService(User, "Duplicate", DocNo)

            C.ExecuteNonQuery("delete from List_Service where CreateBy = '" & User & "' and Document_No is null")
            If DT_Service.Rows.Count > 0 Then
                strSql += "insert into List_Service(INL,IPV,IPVECO,INP,INF,IDC,IAD,IAP,OFC,TotalService,INL_NOC,IPV_NOC,IPVECO_NOC, " + vbCr
                strSql += "INP_NOC,INF_NOC,IDC_NOC,IAD_NOC,IAP_NOC,OFC_NOC,NOC,Domestic,INLUtilization,International,InterUtilization, " + vbCr
                strSql += "InterDirect,Caching,DirectTraffic,EthernetIPV,IPVUtilization,NetworkPort,EthernetNetwork,NetworkUtilization, " + vbCr
                strSql += "DomesticCost,AllInternationalCost,TransitCost,NetworkCost,NetworkPortCost,NOCCost,CreateBy,CreateDate) " + vbCr
                strSql += "Select INL,IPV,IPVECO,INP,INF,IDC,IAD,IAP,OFC,TotalService,INL_NOC,IPV_NOC,IPVECO_NOC, " + vbCr
                strSql += "INP_NOC,INF_NOC,IDC_NOC,IAD_NOC,IAP_NOC,OFC_NOC,NOC,Domestic,INLUtilization,International,InterUtilization, " + vbCr
                strSql += "InterDirect,Caching,DirectTraffic,EthernetIPV,IPVUtilization,NetworkPort,EthernetNetwork,NetworkUtilization, " + vbCr
                strSql += "DomesticCost,AllInternationalCost,TransitCost,NetworkCost,NetworkPortCost,NOCCost,'" & User & "',getdate() " + vbCr
                strSql += "From List_Service Where Document_No = '" & DocNo & "' /*and CreateBy='" + User + "'*/ " + vbCr

            End If

            'DT_CAPEX = C.GetDataTable("select * from dbo.List_CAPEX where CreateBy = '" & User & "' and Document_No = '" & DocNo & "' and Status='1' ")
            DT_CAPEX = C.GetDataTable("select * from dbo.List_CAPEX where Document_No = '" & DocNo & "' and Status='1' ")

            C.ExecuteNonQuery("delete from dbo.List_CAPEX where CreateBy = '" & User & "' and Document_No is null and Status='1'")
            If DT_CAPEX.Rows.Count > 0 Then
                strSql += "insert into List_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr
                strSql += "select CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, Status, '" & User & "', getdate(), '" & User & "', getdate() " + vbCr
                strSql += "from List_CAPEX where /*CreateBy = '" & User & "' and*/ Document_No = '" & DocNo & "' and Status='1' " + vbCr

            End If

            'DT_OPEX = C.GetDataTable("select * from dbo.List_OPEX where CreateBy = '" & User & "' and Document_No = '" & DocNo & "' and Status='1' ")
            DT_OPEX = C.GetDataTable("select * from dbo.List_OPEX where Document_No = '" & DocNo & "' and Status='1' ")

            C.ExecuteNonQuery("delete from dbo.List_OPEX where CreateBy = '" & User & "' and Document_No is null and Status='1'")
            If DT_OPEX.Rows.Count > 0 Then
                strSql += "insert into List_OPEX (OPEX_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr
                strSql += "select OPEX_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, '" & User & "', getdate(), '" & User & "', getdate() " + vbCr
                strSql += "from List_OPEX where /*CreateBy = '" & User & "' and*/ Document_No = '" & DocNo & "' and Status='1' " + vbCr

            End If

            'DT_OTHER = C.GetDataTable("select * from dbo.List_OTHER where CreateBy = '" & User & "' and Document_No = '" & DocNo & "' and Status='1' ")
            DT_OTHER = C.GetDataTable("select * from dbo.List_OTHER where Document_No = '" & DocNo & "' and Status='1' ")

            C.ExecuteNonQuery("delete from dbo.List_OTHER where CreateBy = '" & User & "' and Document_No is null and Status='1'")
            If DT_OTHER.Rows.Count > 0 Then
                strSql += "insert into List_OTHER (OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr
                strSql += "select OTHER_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, '" & User & "', getdate(), '" & User & "', getdate() " + vbCr
                strSql += "from List_OTHER where /*CreateBy = '" & User & "' and*/ Document_No = '" & DocNo & "' and Status='1' " + vbCr
            End If

            'DT_Management = C.GetDataTable("select * from dbo.List_Management where CreateBy = '" & User & "' and Document_No = '" & DocNo & "' and Status='1' ")
            DT_Management = C.GetDataTable("select * from dbo.List_Management where Document_No = '" & DocNo & "' and Status='1' ")

            C.ExecuteNonQuery("delete from dbo.List_Management where CreateBy = '" & User & "' and Document_No is null and Status='1'")
            If DT_Management.Rows.Count > 0 Then
                strSql += "insert into List_Management (Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr
                strSql += "select Management_Name, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, Status, '" & User & "', getdate(), '" & User & "', getdate() " + vbCr
                strSql += "from List_Management where /*CreateBy = '" & User & "' and*/ Document_No = '" & DocNo & "' and Status='1' " + vbCr
            End If

            C.ExecuteNonQuery("delete from dbo.List_Summary where CreateBy = '" & User & "' and Document_No is null and Status='1'")

            If strSql <> "" Then
                C.ExecuteNonQuery(strSql)

                Dim DS_List As New DataTable
                DS_List = C.GetDataTable("SELECT id_List as ScopeIdentity from List_ProjectName where CreateBy = '" + User + "' and Document_No is null ")
                If DS_List.Rows.Count > 0 Then
                    Dim ProjectName_Id As String = DS_List.Rows(0).Item("ScopeIdentity").ToString()
                    Dim update_cc As String = "update tmpList_CT set ProjectName_id = '" + ProjectName_Id + "' where CreateBy = '" + User + "' and ProjectName_id is null "
                    C.ExecuteNonQuery(update_cc)

                    Dim DT_ct As New DataTable
                    DT_ct = C.GetDataTable("select * from tmpList_CT where ProjectName_id = '" + ProjectName_Id + "' order by id_List  ")
                    If DT_ct.Rows.Count > 0 Then
                        C.ExecuteNonQuery("Update List_ProjectName set Customer_Contact_Name = '" & DT_ct.Rows(0).Item("Customer_Contact_Name") & "', Customer_Contact_Tel = '" & DT_ct.Rows(0).Item("Customer_Contact_Tel") & "', Customer_Contact_Email = '" & DT_ct.Rows(0).Item("Customer_Contact_Email") & "'  where id_List = '" & ProjectName_Id & "'")
                    End If
                End If
            Else
                Return "Project นี้ไม่สามารถ Duplicate ได้"
            End If

            DT_Document_Project = C.GetDataTable("select * from dbo.FeasibilityDocument where Document_No = '" & DocNo & "' and Upload_Project = '1' ")
            If DT_Document_Project.Rows.Count > 0 Then
                Return "Complete_Upload"
            Else
                Return "Complete"
            End If

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function getFlowID(ByVal area As String, ByVal presale As String, ByVal tPenaltyLate As Double, ByVal tRevenue As Double, ByVal tPayback As Double, ByVal purchase As String, ByVal tMargin As Double, ByVal specialprice As String) As String
        Dim flow_id As String
        Dim special_price As String
        If specialprice = "True" Then
            special_price = "1"
        Else
            special_price = "0"
        End If

        If area = "CS" Then
            If presale = "" Then
                If special_price <> "1" Then
                    If (tPenaltyLate / tRevenue) * 100 < 10 Then
                        If tPayback < 12 Then
                            'If purchase < 100000 Then
                            '    If tMargin > 25 Then
                            '        flow_id = "81"
                            '    ElseIf tMargin > 20 Then
                            '        flow_id = "82"
                            '    Else
                            '        flow_id = "80"
                            '    End If
                            'ElseIf purchase < 500000 Then
                            If purchase < 500000 Then
                                If tMargin > 20 Then
                                    flow_id = "82"
                                Else
                                    flow_id = "80"
                                End If
                            Else
                                flow_id = "80"
                            End If
                        Else
                            flow_id = "80"
                        End If
                    Else
                        flow_id = "80"
                    End If
                Else
                    flow_id = "80"
                End If

            Else
                If special_price <> "1" Then
                    If (tPenaltyLate / tRevenue) * 100 < 10 Then
                        If tPayback < 12 Then
                            If purchase < 500000 Then
                                If tMargin > 20 Then
                                    flow_id = "85"
                                Else
                                    flow_id = "83"
                                End If
                            Else
                                flow_id = "83"
                            End If
                        Else
                            flow_id = "83"
                        End If
                    Else
                        flow_id = "83"
                    End If
                Else
                    flow_id = "83"
                End If

            End If

        Else
            If presale = "" Then
                If special_price <> "1" Then
                    If (tPenaltyLate / tRevenue) * 100 < 10 Then
                        If tPayback < 12 Then
                            If purchase < 100000 Then
                                If tMargin > 25 Then
                                    flow_id = "91"
                                ElseIf tMargin > 20 Then
                                    flow_id = "92"
                                Else
                                    flow_id = "90"
                                End If
                            ElseIf purchase < 500000 Then
                                If tMargin > 20 Then
                                    flow_id = "92"
                                Else
                                    flow_id = "90"
                                End If
                            Else
                                flow_id = "90"
                            End If
                        Else
                            flow_id = "90"
                        End If
                    Else
                        flow_id = "90"
                    End If
                Else
                    flow_id = "90"
                End If

            Else
                If special_price <> "1" Then
                    If (tPenaltyLate / tRevenue) * 100 < 10 Then
                        If tPayback < 12 Then
                            If purchase < 100000 Then
                                If tMargin > 25 Then
                                    flow_id = "94"
                                ElseIf tMargin > 20 Then
                                    flow_id = "95"
                                Else
                                    flow_id = "93"
                                End If
                            ElseIf purchase < 500000 Then
                                If tMargin > 20 Then
                                    flow_id = "95"
                                Else
                                    flow_id = "93"
                                End If
                            Else
                                flow_id = "93"
                            End If
                        Else
                            flow_id = "93"
                        End If
                    Else
                        flow_id = "93"
                    End If
                Else
                    flow_id = "93"
                End If

            End If

        End If

        Return flow_id
    End Function

    Public Function SendMailSubmit(ByVal vRequest_id As String) As String

        Dim vDT As New DataTable
        Dim DT_ProjectName As DataTable
        Dim DT_Service As New DataTable

        '--HEADING
        Dim ProjectName As String = ""
        '--ROW_1
        Dim RefNo As String = vRequest_id
        Dim intenal_refno As String = ""
        Dim txtDocumentDate As String = ""
        '--ROW_2
        Dim lblProjectCode As String = ""
        Dim lblCustomerType As String = ""
        Dim txtServiceDate As String = ""
        '--ROW_3
        Dim lblArea As String = ""
        Dim lblCluster As String = ""
        '--ROW_4
        Dim lblTypeOfService As String = ""
        Dim lblCompanyService As String = ""
        Dim lblCustomerAssistantName As String = ""
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

        Dim Upload_Project As String
        Dim strSql As String
        Dim strSql2 As String
        'Dim DT As New DataTable
        Dim sReturn As String = ""

        DT_ProjectName = GetTableProjectName("", "edit", vRequest_id)
        DT_Service = GetTableService("", "edit", vRequest_id)


        'strSql2 = "select * from dbo.List_ProjectName where Document_No = '" & vRequest_id & "' "
        'DT = CDB.GetDataTable(strSql2)
        If DT_ProjectName.Rows.Count > 0 Then
            Upload_Project = DT_ProjectName.Rows(0).Item("Upload_Project")
            ProjectName = DT_ProjectName.Rows(0).Item("Project_Name")
            txtDocumentDate = DT_ProjectName.Rows(0).Item("Document_Date")

            If IsDBNull(DT_ProjectName.Rows(0).Item("Project_Code")) Then
                lblProjectCode = ""
            Else
                lblProjectCode = DT_ProjectName.Rows(0).Item("Project_Code")
            End If

            lblCustomerType = DT_ProjectName.Rows(0).Item("Customer_Type")
            txtServiceDate = DT_ProjectName.Rows(0).Item("Service_Date")

            lblArea = DT_ProjectName.Rows(0).Item("Area")
            lblCluster = DT_ProjectName.Rows(0).Item("Cluster")

            lblTypeOfService = DT_ProjectName.Rows(0).Item("Type_Service")
            lblCompanyService = DT_ProjectName.Rows(0).Item("Company_Service")
            lblCustomerAssistantName = DT_ProjectName.Rows(0).Item("Customer_Assistant_Name")

            lblCustomerName = DT_ProjectName.Rows(0).Item("Customer_Contact_Name")
            MonthlyPrice = CDbl(DT_ProjectName.Rows(0).Item("Monthly").ToString)
            OneTimePayment = CDbl(DT_ProjectName.Rows(0).Item("OneTimePayment").ToString)
            lblMonthlyPrice = Format(CDbl(MonthlyPrice), "###,##0.00")
            lblOneTimePayment = Format(CDbl(OneTimePayment), "###,##0.00")
            lblTotalCost = Format(CDbl(DT_ProjectName.Rows(0).Item("TotalProject").ToString), "###,##0.00")

            txtSLA = DT_ProjectName.Rows(0).Item("SLA")
            txtMTTR = DT_ProjectName.Rows(0).Item("MTTR")
            lblMonitorDate = DT_ProjectName.Rows(0).Item("Monitor_Date")
            lblMonitorTime = DT_ProjectName.Rows(0).Item("Monitor_Time")
            lblContract = DT_ProjectName.Rows(0).Item("Contract").ToString
            lblCustomerContactEmail = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
            txtDetailService = Replace(DT_ProjectName.Rows(0).Item("Detail_Service"), Environment.NewLine, "<br />")
            txtFine = Replace(DT_ProjectName.Rows(0).Item("Fine"), Environment.NewLine, "<br />")
            Dim showPenalty As String = ""
            If CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString) = "0" Then
                showPenalty += ""
                'lblPenaltyLate.Text = "0"
            Else
                showPenalty += "ประเมินค่าปรับส่งมอบล่าช้า " & Format(CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString), "###,##0.00") & " บาท<br />"
                'lblPenaltyLate.Text = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString)
            End If
            If CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) = "0" Then
                showPenalty += ""
            Else
                showPenalty += "ประเมินค่าปรับงานซ่อม " & Format(CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString), "###,##0.00") & "%<br />"
            End If
            txtFine = showPenalty + txtFine

            RevenueOne = CDbl(lblMonthlyPrice) + CDbl(lblOneTimePayment)
            RevenueNotOne = CDbl(lblMonthlyPrice)
            lblRevenue = Format(RevenueOne + (RevenueNotOne * (lblContract - 1)), MoneyRoundup)
            Revenue_total = Format(RevenueOne + (RevenueNotOne * (lblContract - 1)), MoneyRoundup)

            MKTCostOne = ((CDbl(DT_ProjectName.Rows(0).Item("Marketing").ToString) / 100) * RevenueOne) + ((CDbl(DT_ProjectName.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueOne) + CDbl(DT_ProjectName.Rows(0).Item("Gift").ToString)
            MKTCostNotOne = ((CDbl(DT_ProjectName.Rows(0).Item("Marketing").ToString) / 100) * RevenueNotOne) + ((CDbl(DT_ProjectName.Rows(0).Item("EntertainCustomer").ToString) / 100) * RevenueNotOne)

            PenaltyOne = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString) + ((CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) / 100) * RevenueOne)
            PenaltyNotOne = (CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) / 100) * RevenueNotOne

            'vCustomer_name = DT.Rows(0).Item("Customer_name")
            'vProject_name = DT.Rows(0).Item("Project_Name")
            'vSubject_name = DT.Rows(0).Item("Customer_name") & " - " & DT.Rows(0).Item("Project_Name")
            'If Upload_Project = 0 
            'strSql = "select * from dbo.List_Service where Document_No = '" & RefNo & "' "
            'DT_Service = CDB.GetDataTable(strSql)


            If DT_Service.Rows.Count > 0 Then
                NOC = CDbl(DT_Service.Rows(0).Item("NOC"))
                NOCTotalCost = CDbl(DT_Service.Rows(0).Item("NOC")) * CDbl(DT_Service.Rows(0).Item("NOCCost"))

                lblUtil = "Util." & DT_Service.Rows(0).Item("INLUtilization").ToString & "%"
                DomesticCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Domestic").ToString) * (CDbl(DT_Service.Rows(0).Item("INLUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("DomesticCost").ToString))
                InternationalCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("DirectTraffic").ToString) * CDbl(DT_Service.Rows(0).Item("TransitCost").ToString))
                Caching = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("Caching").ToString) * CDbl(DT_Service.Rows(0).Item("AllInternationalCost").ToString))
                CostOfInternet = DomesticCost + InternationalCost + Caching

                PortCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("NetWorkPort").ToString) * CDbl(DT_Service.Rows(0).Item("NetWorkPortCost").ToString))
                NetworkCost = Math.Ceiling(CDbl(DT_Service.Rows(0).Item("EthernetNetwork").ToString) * (CDbl(DT_Service.Rows(0).Item("NetworkUtilization").ToString) / 100) * CDbl(DT_Service.Rows(0).Item("NetworkCost").ToString))
                CostOfNetwork = PortCost + NetworkCost
            End If


            sReturn = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
            , txtServiceDate, lblArea, lblCluster, lblTypeOfService, lblCompanyService, lblCustomerAssistantName, lblCustomerName, lblOneTimePayment _
            , lblMonthlyPrice, lblTotalCost, txtSLA, txtMTTR, lblMonitorDate, lblMonitorTime, lblContract _
            , lblCustomerContactEmail, txtDetailService, txtFine, NOC, NOCTotalCost, lblUtil, lblRevenue, Revenue_total _
            , RevenueOne, RevenueNotOne, MKTCostOne, MKTCostNotOne, PenaltyOne, PenaltyNotOne, CostOfInternet, CostOfNetwork)
            'System.Web.HttpContext.Current.Response.Write(sReturn)
            'Else

            'End If

        End If

        Return sReturn

        ' Dim B AS String = rMailtest(vRequest_id)
        ' System.Web.HttpContext.Current.Response.Write(B)

        'Try
        '    'vMain_Point += " *" + vCase

        '    Dim vUemail As String = "" '"panupong.pa;test.t;test.t;"
        '    vUemail = "nat.m;test.a;"

        '    Dim vSplit_uemail As String() = Regex.Split(vUemail, ";")

        '    Dim mail As New MailMessage()
        '    mail.From = New MailAddress("fesibility@jasmine.com")
        '    mail.CC.Add("weraphon.r@jasmine.com")

        '    For Each sMail As String In vSplit_uemail
        '        If sMail.Trim() <> "" Then
        '            mail.To.Add(sMail + "@jasmine.com")
        '        End If
        '    Next

        '    'mail.Subject = "Follow Request " + vRequest_id + ": " + vMain_Point
        '    mail.Subject = "หัวข้อเทส"

        '    'If Upload_Project = 0 
        '    mail.Body = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
        '    , txtServiceDate, lblArea, lblCluster, lblTypeOfService, lblCompanyService, lblCustomerAssistantName, lblCustomerName, lblOneTimePayment _
        '    , lblMonthlyPrice, lblTotalCost, txtSLA, txtMTTR, lblMonitorDate, lblMonitorTime, lblContract _
        '    , lblCustomerContactEmail, txtDetailService, txtFine, NOC, NOCTotalCost, lblUtil, lblRevenue, Revenue_total _
        '    , RevenueOne, RevenueNotOne, MKTCostOne, MKTCostNotOne, PenaltyOne, PenaltyNotOne, CostOfInternet, CostOfNetwork)
        '    'Else

        '    'End If
        '    'mail.Body = rMailBody(vRequest_id, vCustomer_name, vSubject_name, vSubject_url, vProject_name, vMain_Point)
        '    ' mail.Body = rMailtest(ProjectName, RefNo, intenal_refno, txtDocumentDate, lblProjectCode, lblCustomerType _
        '    ' , txtServiceDate, lblArea, lblCluster, lblTypeOfService, lblCompanyService, lblCustomerName, lblOneTimePayment _
        '    ' , lblMonthlyPrice, lblTotalCost, txtSLA, txtMTTR, lblMonitorDate, lblMonitorTime, lblContract _
        '    ' , lblCustomerContactEmail, txtDetailService, txtFine, NOC, NOCTotalCost, lblUtil, lblRevenue, Revenue_total _
        '    ' , RevenueOne, RevenueNotOne, MKTCostOne, MKTCostNotOne, PenaltyOne, PenaltyNotOne, CostOfInternet, CostOfNetwork)

        '    mail.IsBodyHtml = True

        '    Dim smtp As New SmtpClient("smtp.jasmine.com")
        '    smtp.Credentials = New NetworkCredential("chancharas.w", "311227")

        '    smtp.Send(mail)

        'Catch ex As Exception
        '    ClientScript.RegisterStartupScript(Page.GetType, "Alert", "alert('false');", True)
        'End Try
    End Function

    Public Function rMailtest(ByVal ProjectName As String, ByVal RefNo As String, ByVal intenal_refno As String _
    , ByVal txtDocumentDate As String, ByVal lblProjectCode As String, ByVal lblCustomerType As String _
    , ByVal txtServiceDate As String, ByVal lblArea As String, ByVal lblCluster As String _
    , ByVal lblTypeOfService As String, ByVal lblCompanyService As String, ByVal lblCustomerAssistantName As String, ByVal lblCustomerName As String _
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

        Dim DT_Summary As DataTable
        DT_Summary = GetTableSummary(RefNo)

        strsql = "select * from dbo.List_CAPEX where Document_No = '" + RefNo + "' and Status = '1' "
        DT_CAPEX = C.GetDataTable(strsql)
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
        DT_OPEX = C.GetDataTable(strsql)
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
        DT_OTHER = C.GetDataTable(strsql)
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
        DT_Management = C.GetDataTable(strsql)
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

        Dim lblContract_EN = lblContract + " เดือน"
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

        Dim CashFlow As Double = Revenue_Operation - CDbl(lblTotalCAPEX)
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

        If RevenueAfterOperationOne >= CDbl(lblTotalCAPEX) Then
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

        If DT_Summary.Rows.Count > 0 Then
            lblRevenue = Format(CDbl(DT_Summary.Rows(0).Item("Revenue")), MoneyRoundup)
            Revenue_total = Format(CDbl(DT_Summary.Rows(0).Item("Revenue_Profit")), MoneyRoundup)
            lblMKTCost = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost")), MoneyRoundup)
            lblMKTCost_total = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost_Profit")), MoneyRoundup)
            lblCostOfInternet = Format(CDbl(DT_Summary.Rows(0).Item("InternetCost")), MoneyRoundup)
            lblCostOfNetwork = Format(CDbl(DT_Summary.Rows(0).Item("NetworkCost")), MoneyRoundup)
            lblCostOfNOC = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost")), MoneyRoundup)
            lblJasmineGroup = Format(CDbl(DT_Summary.Rows(0).Item("JasmineGroupCost")), MoneyRoundup)
            lblOther = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost")), MoneyRoundup)
            lblOther_total = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost_Profit")), MoneyRoundup)
            lblPenalty = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost")), MoneyRoundup)
            lblPenalty_total = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost_Profit")), MoneyRoundup)
            lblCAPEX_value = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX")), MoneyRoundup)
            lblCAPEX_total = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX_Profit")), MoneyRoundup)
            lblCashFlow_value = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow")), MoneyRoundup)
            lblCashFlow_total = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow_Profit")), MoneyRoundup)
            If DT_Summary.Rows(0).Item("Payback") = "-1" Then
                lblPayBack = "<=1"
            Else
                lblPayBack = DT_Summary.Rows(0).Item("Payback")
            End If
            If DT_Summary.Rows(0).Item("Payback_Profit") = "-1" Then
                lblPayBackProfit = "<=1"
            Else
                lblPayBackProfit = DT_Summary.Rows(0).Item("Payback_Profit")
            End If
            lblMargin = DT_Summary.Rows(0).Item("Margin")
            lblMarginProfit = DT_Summary.Rows(0).Item("Margin_Profit")
            lblNPV = Format(CDbl(DT_Summary.Rows(0).Item("NPV")), MoneyRoundup)
            lblNPVProfit = Format(CDbl(DT_Summary.Rows(0).Item("NPV_Profit")), MoneyRoundup)

            lblOPEX = Format(CDbl(DT_Summary.Rows(0).Item("OPEX")), MoneyRoundup)
            lblOPEX_total = Format(CDbl(DT_Summary.Rows(0).Item("OPEX_Profit")), MoneyRoundup)
            lblRevenue_Operation = Format(CDbl(DT_Summary.Rows(0).Item("RevenueAfter")), MoneyRoundup)
            lblRevenue_Operationtotal = Format(CDbl(DT_Summary.Rows(0).Item("RevenueAfter_Profit")), MoneyRoundup)
            'lblRevenuePercent = "100%"
            lblOtherPercent = Format((CDbl(lblOther) / CDbl(lblRevenue)) * 100, PercentFmt) & "%"
            lblCAPEXPercent = Format((CDbl(lblCAPEX_value) / CDbl(lblRevenue)) * 100, PercentFmt) & "%"
        End If

        Dim text_mail As String = ""

        text_mail += "<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>"
        text_mail += "<html lang='th'>"
        text_mail += "<head>"
        text_mail += "  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>"
        text_mail += "  <meta name='viewport' content='width=device-width, initial-scale=1'>"
        text_mail += "  <meta http-equiv='X-UA-Compatible' content='IE=edge'>"
        text_mail += "  <meta name='format-detection' content='telephone=no'>"
        text_mail += "</head>"
        text_mail += "<body style='margin:0; padding:0;' leftmargin='0' topmargin='0' marginwidth='0' marginheight='0'>"
        text_mail += "<table style='width: 100%; margin: auto;'>"
        text_mail += "<tr>"
        text_mail += "<td style='border: 1px solid black;padding:4px;'>"
        text_mail += "<table style='width: 100%;border: 1px solid black;margin: 0px 0px 0px 0px;border-bottom-style: none; font-family:tahoma; font-size: 14px;'>"
        text_mail += "<tr>"
        text_mail += "<td style='text-align: right; height: 10px;'>"
        text_mail += "</td>"
        text_mail += "</tr>"
        text_mail += "<tr align='center'>"
        text_mail += "<td style='font-weight: bolder; height: 18px;' valign='top'>"
        text_mail += "<b>แบบอนุมัติแผนงานขยายโครงข่ายและบริการ</b>"
        text_mail += "</td>"
        text_mail += "</tr>"
        text_mail += "</table>"
        text_mail += "<table cellspacing='0' style='width: 100%;border: 1px solid black;border-bottom-style: none; font-family:tahoma; font-size: 12px;'>"
        text_mail += "<tr class='border border-1 border-dark' valign='top'>"
        text_mail += "<td style='text-align: left; border-bottom: 1px solid black;'><b>CS Ref.No: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + RefNo + "</td>"
        text_mail += "<td style='text-align: left; border-bottom: 1px solid black;'><b>Internal Ref.No: </b></td> "
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + intenal_refno + "</td>"
        text_mail += "<td style='text-align: left; border-bottom: 1px solid black;'><b>Date: </b></td> "
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + txtDocumentDate + "</td>"
        text_mail += "</tr>"
        text_mail += "<tr class='border border-1 border-dark' valign='top'>"
        text_mail += "<td style='text-align: left;border-bottom: 1px solid black;'><b>Project Code: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + lblProjectCode + "</td>"
        text_mail += "<td style='text-align: left;border-bottom: 1px solid black;'><b>ประเภทลูกค้า: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + lblCustomerType + "</td>"
        text_mail += "<td style='text-align: left;border-bottom: 1px solid black;'><b>วันเปิดบริการ: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + txtServiceDate + "</td>"
        text_mail += "</tr>"
        text_mail += "<tr class='border border-1 border-dark' valign='top'>"
        text_mail += "<td style='text-align: left;border-bottom: 1px solid black;'><b>Project Name: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + ProjectName + "</td>"
        text_mail += "<td style='text-align: left;border-bottom: 1px solid black;'><b>Area: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;border-right: 1px solid black;'>" + lblArea + "</td>"
        text_mail += "<td style='text-align: left;border-bottom: 1px solid black;'><b>Cluster: </b></td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>" + lblCluster + "</td>"
        text_mail += "</tr>"
        'text_mail += "</table>"
        'text_mail += "<table cellspacing='0' style='width: 100%;border-left: 1px solid black;border-right: 1px solid black;border-bottom-style: none;  font-family:tahoma; font-size: 12px;'>"
        text_mail += "<tr valign='top'>"
        text_mail += "<td style='text-align: left; border-bottom: 1px solid black;'>"
        text_mail += "<b>Type of Contact </b>"
        text_mail += "</td>"
        text_mail += "<td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
        text_mail += "<b>Service: </b>" + lblTypeOfService + " "
        text_mail += "</td>"
        text_mail += "<td style='text-align: left; border-bottom: 1px solid black;'>"
        text_mail += "<b>Company: </b>"
        text_mail += "</td>"
        text_mail += "<td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black; '>"
        text_mail += lblCompanyService + " "
        text_mail += "</td>"
        text_mail += "<td style='text-align: left; border-bottom: 1px solid black;'>"
        text_mail += "<b>ผู้เสนอโครงการ: </b>"
        text_mail += "</td>"
        text_mail += "<td colspan='2' style='text-align: left; border-bottom: 1px solid black; '>"
        text_mail += lblCustomerAssistantName + " "
        text_mail += "</td>"
        text_mail += "</tr>"
        'text_mail +="<tr valign='top'>"
        '    text_mail +="<td align='left' style='border-bottom: 1px solid black;'>Description</td>"
        '    text_mail +="<td align='left' colspan='4' style='border-bottom: 1px solid black; border-right: 1px solid black;'>"
        '    text_mail +="" + ProjectName + " <br/> ชื่อผู้ติดต่อ  " + lblCustomerName + ""
        '    text_mail +="</td>"
        '    text_mail +="<td colspan='2' align='left' width='15%'>"
        '    text_mail +="ค่าใช้จ่ายครั้งเดียว <br/> ค่าใช้จ่ายรายเดือน <br/>"
        '    text_mail +="</td>"
        '    text_mail +="<td colspan='2' align='left' width='10%'>"
        '    text_mail +="" + lblOneTimePayment + " บาท <br/> " + lblMonthlyPrice + " บาท/เดือน <br/> "
        '    text_mail +="</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr valign='top'>"
        '    text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>SLA </td>"
        '    text_mail +="<td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
        '    text_mail +="" + txtSLA + ""
        '    text_mail +="</td>"
        '    text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>MTTR </td>"
        '    text_mail +="<td style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
        '    text_mail +="" + txtMTTR + ""
        '    text_mail +="</td>"
        '    text_mail +="<td colspan='2' align='left' style='border-bottom: 1px solid black;'>"
        '    text_mail +="มูลค่าโครงการ "
        '    text_mail +="</td>"
        '    text_mail +="<td colspan='2' align='left' style='border-bottom: 1px solid black;'>"
        '    text_mail +="" + lblTotalCost + " บาท(ไม่รวมภาษีมูลค่าเพิ่ม) <br/>"
        '    text_mail +="</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr valign='top'>"
        '    text_mail +="<td style='text-align: left; border-bottom: 1px solid black;'>Monitor Date </td>"
        '    text_mail +="<td colspan='2' valign='top' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
        '    text_mail +="" + lblMonitorDate + ""
        '    text_mail +="</td>"
        '    text_mail +="<td style='text-align: left;border-bottom: 1px solid black;'>Monitor Time </td>"
        '    text_mail +="<td valign='top' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
        '    text_mail +="" + lblMonitorTime + ""
        '    text_mail +="</td>"
        '    text_mail +="<td colspan='2' style='text-align: left; border-right: 1px solid black; border-bottom: 1px solid black;'>"
        '    text_mail +="สัญญา " + lblContract + " months"
        '    text_mail +="</td>"
        '    text_mail +="<td colspan='2' style='text-align: left; border-bottom: 1px solid black;'>"
        '    text_mail +="Email " + lblCustomerContactEmail + ""
        '    text_mail +="</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr valign='top'>"
        '    text_mail +="<td style='text-align: left;'>Service</td>"
        '    text_mail +="<td colspan='8' style='text-align: left;'>" + txtDetailService + "</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr valign='top'>"
        '    text_mail +="<td style='text-align: left; border-top: 1px solid black;'>ค่าปรับ</td>"
        '    text_mail +="<td colspan='8' style='text-align: left; border-top: 1px solid black;'>"
        '    text_mail +="" + txtFine + ""
        '    text_mail +="</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr align='left' valign='top' style='left; border-top: 1px solid black; color:white; background: #107763;'>"
        '  text_mail +="<td colspan='5' style='border-right: 1px solid black;'>"
        '  text_mail +="<b>งบลงทุน(CAPEX)</b>"
        '  text_mail +="</td>"
        '  text_mail +="<td colspan='4'>"
        '  text_mail +="<b>ค่าใช้จ่ายรายเดือน(OPEX)</b>"
        '  text_mail +="</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr align='left' valign='top'>"
        '  text_mail +="<td colspan='5'>"
        '  text_mail +="" + Left_Table + ""
        '  text_mail +="</td>"
        '  text_mail +="<td colspan='4' width='46.5%'>"
        '  text_mail +="" + Right_Table + ""
        '  text_mail +="</td>"
        'text_mail +="</tr>"
        'text_mail +="<tr style='background-color: #FFFF99;' valign='top'>"
        '   text_mail +="<td align='left' colspan='4' style='border-top: 1px solid black;border-bottom: 1px solid black;'>Total Investment Cost</td>"
        '   text_mail +="<td align='right' style='border-top: 1px solid black;border-bottom: 1px solid black;'>"
        '   text_mail +="" + Format(CDbl(total_capex.ToString), "###,##0.00") + ""
        '   text_mail +="</td>"
        '   text_mail +="<td align='left' colspan='3' width='10%' style='border-top: 1px solid black;border-bottom: 1px solid black;'>Total Cost</td>"
        '   text_mail +="<td align='right' style='border-top: 1px solid black;border-bottom: 1px solid black;'>"
        '   text_mail +="" + Format(CDbl(totalopex_all.ToString), "###,##0.00") + ""
        '   text_mail +="</td>"
        'text_mail +="</tr>"
        text_mail += "</table>"
        text_mail += "<table width='100%' style='border-collapse:collapse;  font-family:tahoma; font-size: 12px;'>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='6' align='center' valign='top' style='background: #87CEFA; height: 15px; border-left: 1px solid black; border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><b>ระยะสัญญา " + lblContract_EN + "</b></td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='3'  width='50%'></td>"
        text_mail += "<td colspan='2' align='right'><b>Marginal Profit</b></td>"
        text_mail += "<td colspan='1'></td>"
        text_mail += "<td colspan='3' width='20%'></td>"
        text_mail += "</tr>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2' width='40%'><b>Revenue</b></td>"
        text_mail += "<td align='right' width='10%'><b>" + lblRevenue + "</b></td>"
        text_mail += "<td align='right' width='10%' style='color:#6c757d;'>100%</td>"
        text_mail += "<td align='right' width='10%'>" + Revenue_total + "</td>"
        text_mail += "<td align='right' width='10%' style='color:#6c757d;'>100%</td>"
        text_mail += "<td colspan='3' width='20%'></td>"
        text_mail += "</tr>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'><b>OPEX</b></td>"
        text_mail += "<td align='right'><b>" + lblOPEX + "<b/></td>"
        text_mail += "<td colspan='1'></td>"
        text_mail += "<td align='right'>" + lblOPEX_total + "</td>"
        text_mail += "<td colspan='1'></td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุนทางการตลาด(Marketing Cost)</td>"
        text_mail += "<td align='right'>" + lblMKTCost + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblMKTCost) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right'>" + lblMKTCost_total + "</td>"
        If CDbl(Revenue_total) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblMKTCost_total) / CDbl(Revenue_total)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุน Internet Bandwidth</td>"
        text_mail += "<td align='right'>" + lblCostOfInternet + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblCostOfInternet) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right' style='background:#6c757d;'></td>"
        text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุน Network Bandwidth</td>"
        text_mail += "<td align='right'>" + lblCostOfNetwork + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblCostOfNetwork) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right' style='background:#6c757d;'></td>"
        text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;ต้นทุน NOC</td>"
        text_mail += "<td align='right'>" + lblCostOfNOC + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblCostOfNOC) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right' style='background:#6c757d;'></td>"
        text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;EXP. Jasmine Group</td>"
        text_mail += "<td align='right'>" + lblJasmineGroup + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblJasmineGroup) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right' style='background:#6c757d;'></td>"
        text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;EXP. Other</td>"
        text_mail += "<td align='right'>" + lblOther + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" + lblOtherPercent + "</td>"
        End If
        text_mail += "<td align='right'>" + lblOther_total + "</td>"
        If CDbl(Revenue_total) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblOther_total) / CDbl(Revenue_total)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr>"
        text_mail += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;Penalty</td>"
        text_mail += "<td align='right'>" + lblPenalty + "</td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblPenalty) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right'>" + lblPenalty_total + "</td>"
        If CDbl(Revenue_total) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblPenalty_total) / CDbl(Revenue_total)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr class='font-weight-bold'>"
        text_mail += "<td colspan='2'><b>Revenue After Operation</b></td>"
        text_mail += "<td align='right'><b>" + lblRevenue_Operation + "<b/></td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblRevenue_Operation) / CDbl(lblRevenue)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td align='right'>" + lblRevenue_Operationtotal + "</td>"
        If CDbl(Revenue_total) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblRevenue_Operationtotal) / CDbl(Revenue_total)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr class='font-weight-bold'>"
        text_mail += "<td colspan='2'><b>CAPEX</b></td>"
        text_mail += "<td align='right'><b>" + lblCAPEX_value + "</b></td>"
        If CDbl(lblRevenue) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" + lblCAPEXPercent + "</td>"
        End If
        text_mail += "<td align='right'>" + lblCAPEX_total + "</td>"
        If CDbl(Revenue_total) = 0 Then
            text_mail += "<td align='right' style='color:#6c757d;'>0.00%</td>"
        Else
            text_mail += "<td align='right' style='color:#6c757d;'>" & Format((CDbl(lblCAPEX_total) / CDbl(Revenue_total)) * 100, "###,##0.00") & "%</td>"
        End If
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr style='height: 10px;'>"
        text_mail += "</tr>"
        text_mail += "<tr class='font-weight-bold'>"
        text_mail += "<td colspan='2' style='border-left: 1px solid black;border-top: 1px solid black;'><b>Cash Flow</b></td>"
        text_mail += "<td align='right' style='border-top: 1px solid black;'><b>" + lblCashFlow_value + "</b></td>"
        text_mail += "<td colspan='1' style='border-top: 1px solid black; color:#6c757d;'></td>"
        text_mail += "<td align='right' style='border-top: 1px solid black;'>" + lblCashFlow_total + "</td>"
        text_mail += "<td colspan='1' style='border-top: 1px solid black; border-right: 1px solid black; color:#6c757d;'></td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr class='font-weight-bold'>"
        text_mail += "<td colspan='2' style='border-left: 1px solid black;'><b>payback (months)</b></td>"
        text_mail += "<td align='right'>"
        text_mail += "<b>" + lblPayBack + "</b>"
        text_mail += "</td>"
        text_mail += "<td colspan='1'></td>"
        text_mail += "<td align='right'>"
        text_mail += "" + lblPayBackProfit + ""
        text_mail += "</td>"
        text_mail += "<td colspan='1' style='border-right: 1px solid black;'></td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr class='font-weight-bold'>"
        text_mail += "<td colspan='2'  style='border-left: 1px solid black;'><b>margin</b></td>"
        text_mail += "<td align='right'>"
        text_mail += "<b>" + lblMargin + "%</b>"
        text_mail += "</td>"
        text_mail += "<td colspan='1'></td>"
        text_mail += "<td align='right'>"
        text_mail += "" + lblMarginProfit + "%"
        text_mail += "</td>"
        text_mail += "<td colspan='1' style='border-right: 1px solid black;'></td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "<tr class='font-weight-bold'>"
        text_mail += "<td colspan='2' style='border-left: 1px solid black; border-bottom: 1px solid black;'><b>NPV (5% per year)</b></td>"
        text_mail += "<td align='right' style='border-bottom: 1px solid black;'>"
        text_mail += "<b>" + lblNPV + "</b>"
        text_mail += "</td>"
        text_mail += "<td colspan='1' style='border-bottom: 1px solid black;'></td>"
        text_mail += "<td align='right' style='border-bottom: 1px solid black;'>"
        text_mail += "" + lblNPVProfit + ""
        text_mail += "</td>"
        text_mail += "<td colspan='1' style='border-bottom: 1px solid black;border-right: 1px solid black;'></td>"
        text_mail += "<td colspan='3'></td>"
        text_mail += "</tr>"
        text_mail += "</table>"
        text_mail += "</td>"
        text_mail += "</tr>"
        text_mail += "</table>"
        text_mail += "</body>"
        text_mail += "</html>"

        Return text_mail

    End Function

End Class
