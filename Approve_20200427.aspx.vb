Imports System.Data
Imports Microsoft.VisualBasic
Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf

Partial Class Approve
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from dbo.UserLogin where login_name = '" & Session("uemail") & "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            If DT.Rows(0).Item("Login_permission") = "approve" Or DT.Rows(0).Item("Login_permission") = "inspector" Or DT.Rows(0).Item("Login_permission") = "administrator" Or DT.Rows(0).Item("Login_permission") = "user" Then
                Dim xRequest_id = Request.QueryString("request_id") '"FES201705-00004"
                If Request.QueryString("menu") IsNot Nothing Then
                    Session("menu") = Request.QueryString("menu")
                End If
                'Dim strSql As String

                Dim DT_ProjectName As DataTable
                Dim DT_CAPEX As DataTable
                Dim DT_CAPEX_Mass As DataTable
                Dim DT_OPEX As DataTable
                Dim DT_OTHER As DataTable
                Dim DT_Service As DataTable
                Dim DT_InternetBWCost As DataTable
                Dim Transit As Double
                Dim xDSL_FTTx As Double
                Dim r As String
                Dim total_capex As Double = 0
                Dim total_capex_mass As Double = 0
                Dim total_opex As Double = 0
                Dim total_other As Double = 0

                strSql = "select * from List_ProjectName where Document_No = '" + xRequest_id + "'  "
                refno.Text = xRequest_id
                DT_ProjectName = C.GetDataTable(strSql)
                If DT_ProjectName.Rows.Count > 0 Then
                    lblProjectName.Text = DT_ProjectName.Rows(0).Item("Project_Name")
                    If IsDBNull(DT_ProjectName.Rows(0).Item("Project_Code")) Then
                        lblProjectCode.Text = ""
                    Else
                        lblProjectCode.Text = DT_ProjectName.Rows(0).Item("Project_Code")
                    End If

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

                If txtDocumentDate.Text = "" Then
                    txtDocumentDate.Text = "&nbsp;"
                End If

                strSql = "select * from dbo.List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_CAPEX = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
                    total_capex += DT_CAPEX.Rows(i).Item("Cost")
                Next
                'lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00") & "   THB"


                strSql = "select * from dbo.List_CAPEX_Mass where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_CAPEX_Mass = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_CAPEX_Mass.Rows.Count - 1
                    total_capex_mass += DT_CAPEX_Mass.Rows(i).Item("Cost")
                Next
                lblTotalCAPEXMass.Text = Format(CDbl(total_capex_mass.ToString), "###,##0.00") & "   THB"


                strSql = "select * from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_OPEX = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_OPEX.Rows.Count - 1
                    total_opex += DT_OPEX.Rows(i).Item("Cost")
                Next
                lblTotalOPEX.Text = Format(CDbl(total_opex.ToString), "###,##0.00") & "   THB"

                strSql = "select * from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_OTHER = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_OTHER.Rows.Count - 1
                    total_other += DT_OTHER.Rows(i).Item("Cost")
                Next
                lblTotalOTHER.Text = Format(CDbl(total_other.ToString), "###,##0.00") & "   THB"


                strSql = "select * from dbo.List_Service where Document_No = '" + xRequest_id + "' "
                DT_Service = C.GetDataTable(strSql)
                If DT_Service.Rows.Count > 0 Then 'And DT_CAPEX.Rows.Count > 0 And DT_OPEX.Rows.Count > 0 Then
                    'lblContractYear.Text = Math.Round(CDbl(DT_Service.Rows(0).Item("Contract").ToString / 12), 0)
                    'lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
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

                End If

                If Not Page.IsPostBack Then
                    loadDetail(xRequest_id)
                End If
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "", "alert('คุณไม่มีสิทธิ์ในการ Approve');", True)
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

    Sub Add_Next(ByVal Source As Object, ByVal E As EventArgs)

    End Sub

    Sub Flow_Submit(ByVal Source As Object, ByVal E As EventArgs)
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
        Response.Redirect("Approve.aspx?request_id=" + Request.QueryString("request_id"))
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
                'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่สามารถ Upload ไฟล์์ได้ เนื่องจากเคย Upload แล้ว');", True)
            Else
                System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\Contract\" & FileNameContract)
            End If
        Else
            Throw New FileNotFoundException()
        End If
    End Sub
    
End Class
