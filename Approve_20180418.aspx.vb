Imports System.Data
Imports Microsoft.VisualBasic
Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf

Partial Class Approve_20180418
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
                Dim DT_OPEX As DataTable
                Dim DT_OTHER As DataTable
                Dim DT_Service As DataTable
                Dim DT_InternetBWCost As DataTable
                Dim DT_Document As DataTable
                Dim DomesticCost As Double
                Dim InternationalCost As Double
                Dim Transit As Double
                Dim xDSL_FTTx As Double
                Dim r As String
                Dim total_capex As Double = 0
                Dim total_opex As Double = 0
                Dim total_other As Double = 0
                strSql = "select * from dbo.List_CAPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_CAPEX = C.GetDataTable(strSql)
                r = "<table style='width: 100%'>"
                For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
                    'r += "<tr>"
                    'r += "<td style='width: 63%'>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
                    'r += "<td align='center' style='width: 7%'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
                    'r += "<td align='right'  style='width: 15%'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
                    'r += "<td align='right'  style='width: 15%'>บาท/เดือน</td>"
                    'r += "</tr>"
                    total_capex += DT_CAPEX.Rows(i).Item("Cost")
                Next
                r += "<tr>"
                r += "<td></td>"
                r += "<td></td>"
                r += "<td align='center'><b><u>" + Format(CDbl(total_capex.ToString), "###,##0.00") + "</u>  บาท/เดือน</b></td>"
                r += "<td></td>"
                r += "</tr>"
                r += "</table>"
                CAPEX.InnerHtml = r
                lblTotalCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00") & "   บาท/เดือน"

                strSql = "select * from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_OPEX = C.GetDataTable(strSql)
                r = "<table style='width: 100%'>"
                For i As Integer = 0 To DT_OPEX.Rows.Count - 1
                    'r += "<tr>"
                    'r += "<td>" + DT_OPEX.Rows(i).Item("OPEX_Name").ToString + "</td>"
                    'r += "<td align='right'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
                    'r += "<td align='right'>บาท/เดือน</td>"
                    'r += "</tr>"
                    total_opex += DT_OPEX.Rows(i).Item("Cost")
                Next
                r += "<tr>"
                r += "<td></td>"
                r += "<td align='center'><b><u>" + Format(CDbl(total_opex.ToString), "###,##0.00") + "</u> บาท/เดือน</b></td>"
                r += "<td></td>"
                r += "</tr>"
                r += "</table>"
                OPEX.InnerHtml = r
                lblTotalOPEX.Text = Format(CDbl(total_opex.ToString), "###,##0.00") & "   บาท/เดือน"

                strSql = "select * from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' "
                DT_OTHER = C.GetDataTable(strSql)
                For i As Integer = 0 To DT_OTHER.Rows.Count - 1
                    total_other += DT_OTHER.Rows(i).Item("Cost")
                Next
                lblTotalOTHER.Text = Format(CDbl(total_other.ToString), "###,##0.00") & "   บาท/เดือน"


                strSql = "select * from dbo.List_Model where Document_No = '" + xRequest_id + "' "
                DT_Service = C.GetDataTable(strSql)
                If DT_Service.Rows.Count > 0 Then
                    lblContractYear.Text = Math.Round(CDbl(DT_Service.Rows(0).Item("Contract").ToString / 12), 0)
                    lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
                    lblMonthly.Text = Format(CDbl(DT_Service.Rows(0).Item("Monthly").ToString), "###,##0.00")
                    lblOneTimePayment.Text = Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
                    lblCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")

                    ' Define money format.
                    Dim MoneyFmt As String = "###,##0.00"
                    ' Define percentage format.
                    Dim PercentFmt As String = "#0.00"
                    Dim values(lblContract.Text) As Double

                    Dim Revenue As Double
                    Dim MKTCost As Double
                    Dim RevenueOneTime As Double
                    Dim MKTCostOneTime As Double
                    Dim CostOfInternet As Double
                    Dim CostOfNetwork As Double
                    Dim CostOfNOC As Double
                    Dim Vas As Double
                    Dim CashFlow As Double
                    Dim CashFlowOneTime As Double

                    DT_InternetBWCost = C.GetDataTable("select * from List_Model where Document_No = '" + xRequest_id + "' ")
                    Revenue = DT_Service.Rows(0).Item("Monthly")
                    RevenueOneTime = DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")
                    MKTCost = (Revenue * 3) / 100
                    MKTCostOneTime = (RevenueOneTime * 3) / 100
                    CostOfNetwork = ((DT_Service.Rows(0).Item("EthernetIPV") * (DT_InternetBWCost.Rows(0).Item("IPV_Discount") / 100)) + DT_Service.Rows(0).Item("EthernetINP")) * DT_InternetBWCost.Rows(0).Item("Network_Price")
                    CostOfNOC = (DT_Service.Rows(0).Item("TotalINLCurcuits") + DT_Service.Rows(0).Item("TotalIPVCurcuits") + DT_Service.Rows(0).Item("TotalINPCurcuits")) * DT_InternetBWCost.Rows(0).Item("NOC_Price")
                    'Vas = C.GetDataTable("select SUM(Cost) 'VasCost' from dbo.List_OPEX where Document_No = '" + xRequest_id + "' ").Rows(0).Item("VasCost")
                    If C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' ").Rows.Count > 0 Then
                        Vas = Vas + C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OPEX where Document_No = '" + xRequest_id + "' and Status = '1' ").Rows(0).Item("VasCost")
                    End If
                    If C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' ").Rows.Count > 0 Then
                        Vas = Vas + C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OTHER where Document_No = '" + xRequest_id + "' and Status = '1' ").Rows(0).Item("VasCost")
                    End If

                    'lblRevenue.Text = ((Revenue * DT_Service.Rows(0).Item("Contract")) + DT_Service.Rows(0).Item("OneTimePayment")).ToString
                    'lblMKTCost.Text = (MKTCost * DT_Service.Rows(0).Item("Contract")).ToString
                    lblRevenue.Text = Format(CDbl((RevenueOneTime + (Revenue * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")
                    lblMKTCost.Text = Format(CDbl(((MKTCostOneTime) + (MKTCost * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")

                    If DT_InternetBWCost.Rows.Count > 0 Then
                        DomesticCost = (DT_Service.Rows(0).Item("Domestic") * DT_InternetBWCost.Rows(0).Item("Domestic_Price")) * (DT_InternetBWCost.Rows(0).Item("Dom_Discount") / 100)
                        InternationalCost = ((DT_Service.Rows(0).Item("International") - DT_Service.Rows(0).Item("Transit")) * DT_InternetBWCost.Rows(0).Item("All_International_Price")) * (DT_InternetBWCost.Rows(0).Item("Inter_Discount") / 100)
                        Transit = DT_Service.Rows(0).Item("Transit") * DT_InternetBWCost.Rows(0).Item("Transit_Price")
                        xDSL_FTTx = DT_Service.Rows(0).Item("ServicePrice")
                        CostOfInternet = (DomesticCost + InternationalCost + Transit + xDSL_FTTx)
                        lblCostOfInternet.Text = Format(CDbl((CostOfInternet * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                        lblCostOfNetwork.Text = Format(CDbl((CostOfNetwork * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                        lblCostOfNOC.Text = Format(CDbl((CostOfNOC * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                    End If
                    lblVas.Text = Format(CDbl((Vas * DT_Service.Rows(0).Item("Contract")).ToString.ToString), "###,##0.00")
                    CashFlow = Revenue - MKTCost - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
                    CashFlowOneTime = RevenueOneTime - MKTCostOneTime - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
                    lblCashFlow.Text = Format(CDbl(((CashFlowOneTime - lblCAPEX.Text) + (CashFlow * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")
                    'lblCashFlow.Text = (((DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")) - (((DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")) * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas) + ((DT_Service.Rows(0).Item("Monthly") * (DT_Service.Rows(0).Item("Contract") - 1)) - (((DT_Service.Rows(0).Item("Monthly") * (DT_Service.Rows(0).Item("Contract") - 1)) * 3) / 100) - ((CostOfInternet + CostOfNetwork + CostOfNOC + Vas) * (DT_Service.Rows(0).Item("Contract") - 1)))).ToString

                    'CashFlow = DT_Service.Rows(0).Item("Monthly") - ((DT_Service.Rows(0).Item("Monthly") * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas

                    If lblOneTimePayment.Text - lblCAPEX.Text > 0 Then
                        lblPayBack.Text = "<1"
                    Else
                        lblPayBack.Text = Format(lblCAPEX.Text / CashFlow, "#0.00")
                    End If

                    For i As Integer = 0 To lblContract.Text - 1
                        If i = 0 Then
                            values(i) = CashFlowOneTime - lblCAPEX.Text
                        Else
                            values(i) = CashFlow
                        End If
                    Next

                    Dim FixedRetRate As Double = 0.05
                    ' Calculate net present value.
                    Dim NetPVal As Double = NPV(FixedRetRate / lblContract.Text, values)
                    ' Display net present value.
                    ' MsgBox("The net present value of these cash flows is " & Format(NetPVal, MoneyFmt) & ".")
                    lblNPV.Text = Format(NetPVal, MoneyFmt)
                    lblMargin.Text = Format((lblNPV.Text / lblRevenue.Text) * 100, PercentFmt).ToString & "%"

                End If

                If Not Page.IsPostBack Then
                    'strSql = "select Document_No, convert(varchar(10),Document_Date,103) 'Document_Date', convert(varchar(10),Service_Date,103) 'Service_Date' " + vbCr
                    'strSql += ", Area, Cluster, Customer_Name, Location_Name, Type_Service, Detail_Service  " + vbCr
                    'strSql += "from dbo.FeasibilityDocument where Document_No = '" + xRequest_id + "' and Status = '1' "
                    'DT_Document = C.GetDataTable(strSql)
                    'If DT_Document.Rows.Count > 0 Then
                    '    lblDocumentNo.Text = DT_Document.Rows(0).Item("Document_No")
                    '    lblDocumentDate.Text = DT_Document.Rows(0).Item("Document_Date")
                    '    lblArea.Text = DT_Document.Rows(0).Item("Area")
                    '    lblLocationName.Text = DT_Document.Rows(0).Item("Location_Name")
                    '    lblServiceDate.Text = DT_Document.Rows(0).Item("Service_Date")
                    '    lblCluster.Text = DT_Document.Rows(0).Item("Cluster")
                    '    lblCustomerName.Text = DT_Document.Rows(0).Item("Customer_Name")
                    '    'RadioButton4.Checked = False
                    '    If DT_Document.Rows(0).Item("Type_Service") = "Re-New Service" Then
                    '        RadioButton2.Checked = True
                    '    ElseIf DT_Document.Rows(0).Item("Type_Service") = "Maintenance" Then
                    '        RadioButton3.Checked = True
                    '    Else
                    '        RadioButton1.Checked = True
                    '    End If
                    '    txtDetailService.Text = DT_Document.Rows(0).Item("Detail_Service")
                    'End If


                    'Dim strRO As String
                    'Dim DTRO As DataTable
                    'Dim strCluster As String
                    'Dim DTCluster As DataTable

                    'strCluster = "select distinct Cluster, Cluster_email, Cluster_name from Cluster where Status = '1' and RO = '" + DT_Document.Rows(0).Item("Area") + "' "
                    'DTCluster = C.GetDataTable(strCluster)
                    'If DTCluster.Rows.Count > 0 Then
                    '    lblPrepare.Text = "(" & DTCluster.Rows(0).Item("Cluster_name") & ")"
                    'End If

                    strSql = "select * from List_ProjectName where Document_No = '" + xRequest_id + "' --and CreateBy='" + Session("uemail") + "' "
                    DT_ProjectName = C.GetDataTable(strSql)
                    If DT_ProjectName.Rows.Count > 0 Then
                        lblDocumentNo.Text = DT_ProjectName.Rows(0).Item("Document_No")
                        lblProjectName.Text = DT_ProjectName.Rows(0).Item("Project_Name")
                        lblDocumentDate.Text = DT_ProjectName.Rows(0).Item("Document_Date")
                        lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
                        lblLocationName.Text = DT_ProjectName.Rows(0).Item("Location_Name")
                        lblServiceDate.Text = DT_ProjectName.Rows(0).Item("Service_Date")
                        lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
                        lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Name")
                        lblTypeOfService.Text = DT_ProjectName.Rows(0).Item("Type_Service")
                        txtDetailService.Text = DT_ProjectName.Rows(0).Item("Detail_Service")
                        lblPrepare.Text = "(" & DT_ProjectName.Rows(0).Item("Cluster_name") & ")"
                        lblPrepaireEmail.Text = DT_ProjectName.Rows(0).Item("Cluster_email")
                    Else
                        lblProjectName.Text = "-"
                    End If

                    loadDetail(xRequest_id)
                End If
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "", "alert('คุณไม่มีสิทธิ์ในการ Approve');", True)
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strSql As String
        Dim DT As New DataTable
        Dim TypeService As String
        If RadioButton2.Checked = True Then
            TypeService = RadioButton2.Text
        ElseIf RadioButton3.Checked = True Then
            TypeService = RadioButton3.Text
        Else
            TypeService = RadioButton1.Text
        End If
        strSql = "select convert(varchar(5),right(isnull(max(Document_No),'FES000000-00000'),5) + 1) 'Max_Document',  left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) 'YearMonth' " + vbCr
        strSql += "from FeasibilityDocument " + vbCr
        strSql += "where substring(Document_No,4,6) = left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) "
        DT = C.GetDataTable(strSql)
        'Dim a As Integer = 200
        'Dim doc As String = CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")
        'strSql = "insert into FeasibilityDocument (Document_No,Document_Date,Service_Date,Area,Cluster,Customer_Name,Location_Name,Type_Service,Detail_Service,CreateBy,CreateDate) values ('FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "','" + C.CDateText(txtDocumentDate.Text) + "','" + C.CDateText(txtServiceDate.Text) + "','" + txtArea.Text + "','" + txtCluster.Text + "','" + txtCustomerName.Text + "','" + txtLocationName.Text + "','" + TypeService + "','" + txtDetailService.Text + "','weraphon.r',getdate()) "
        C.ExecuteNonQuery(strSql)
        strSql = "Update dbo.List_CAPEX set Document_No='FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = 'weraphon.r' and Document_No is NULL " + vbCr
        strSql += "Update dbo.List_OPEX set Document_No='FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = 'weraphon.r' and Document_No is NULL " + vbCr
        strSql += "Update dbo.List_Model set Document_No='FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000") + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = 'weraphon.r' and Document_No is NULL "
        C.ExecuteNonQuery(strSql)
    End Sub

    Sub Add_Next(ByVal Source As Object, ByVal E As EventArgs)

    End Sub

    Sub Flow_Submit(ByVal Source As Object, ByVal E As EventArgs)
        Dim strSql As String
        strSql = ""

        Dim flow_file As String = "" 'rUpFile("flow_file", Request.QueryString("request_id") + "_F")
        CF.SaveFlow(hide_uemail.Value, hide_flow_no.Value, hide_flow_sub.Value, hide_next_step.Value, hide_back_step.Value, hide_department.Value, hide_flow_status.Value, hide_flow_remark.Value, flow_file)
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

End Class
