Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic
Partial Class Summary
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim Cal As New Cls_Calculate
    Dim PrepareEmail As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim strSql As String

        Dim DT_ProjectName As DataTable
        Dim DT_Service As New DataTable
        

        'Dim DT_InternetBWCost As DataTable
        'Dim Transit As Double
        'Dim xDSL_FTTx As Double


        DT_ProjectName = GetTableProjectName()
        DT_Service = GetTableService()

        'Session("uemail") = "natnapich.t"

        If DT_ProjectName.Rows.Count > 0 And DT_Service.Rows.Count > 0 Then
            ShowProjectName(DT_ProjectName)
            ShowCAPEX_OPEX(DT_Service)
            ShowService(DT_ProjectName, DT_Service)
            btnSave.Visible = True
            btnSaveToEditProject.Visible = True
        Else
            btnSave.Visible = False
            btnSaveToEditProject.Visible = False
            LinkFileDoc.Visible = False
        End If

        'strSql = "select * from dbo.List_Model where CreateBy='" + Session("uemail") + "' and Document_No is null "
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

        '    DT_InternetBWCost = C.GetDataTable("select * from InternetBWCost where BWCost_Type='" + DT_Service.Rows(0).Item("Discount") + "' ")
        '    Revenue = DT_Service.Rows(0).Item("Monthly")
        '    RevenueOneTime = DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")
        '    MKTCost = (Revenue * 3) / 100
        '    MKTCostOneTime = (RevenueOneTime * 3) / 100
        '    CostOfNetwork = ((DT_Service.Rows(0).Item("EthernetIPV") * (DT_InternetBWCost.Rows(0).Item("IPV_Discount") / 100)) + DT_Service.Rows(0).Item("EthernetINP")) * DT_InternetBWCost.Rows(0).Item("Network")
        '    CostOfNOC = (DT_Service.Rows(0).Item("TotalINLCurcuits") + DT_Service.Rows(0).Item("TotalIPVCurcuits") + DT_Service.Rows(0).Item("TotalINPCurcuits")) * DT_InternetBWCost.Rows(0).Item("NOC")
        '    If C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OPEX where CreateBy='" + Session("uemail") + "' and Document_No is null ").Rows.Count > 0 Then
        '        Vas = Vas + C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OPEX where CreateBy='" + Session("uemail") + "' and Document_No is null ").Rows(0).Item("VasCost")
        '    End If
        '    If C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OTHER where CreateBy='" + Session("uemail") + "' and Document_No is null ").Rows.Count > 0 Then
        '        Vas = Vas + C.GetDataTable("select isnull(SUM(Cost),0) 'VasCost' from dbo.List_OTHER where CreateBy='" + Session("uemail") + "' and Document_No is null ").Rows(0).Item("VasCost")
        '    End If

        '    lblRevenue.Text = Format(CDbl((RevenueOneTime + (Revenue * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")
        '    lblMKTCost.Text = Format(CDbl(((MKTCostOneTime) + (MKTCost * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")

        '    If DT_InternetBWCost.Rows.Count > 0 Then
        '        DomesticCost = (DT_Service.Rows(0).Item("Domestic") * DT_InternetBWCost.Rows(0).Item("Domestic")) * (DT_InternetBWCost.Rows(0).Item("Dom_Discount") / 100)
        '        InternationalCost = ((DT_Service.Rows(0).Item("International") - DT_Service.Rows(0).Item("Transit")) * DT_InternetBWCost.Rows(0).Item("All_International")) * (DT_InternetBWCost.Rows(0).Item("Inter_Discount") / 100)
        '        Transit = DT_Service.Rows(0).Item("Transit") * DT_InternetBWCost.Rows(0).Item("Transit")
        '        xDSL_FTTx = DT_Service.Rows(0).Item("ServicePrice")
        '        CostOfInternet = (DomesticCost + InternationalCost + Transit + xDSL_FTTx)
        '        lblCostOfInternet.Text = Format(CDbl((CostOfInternet * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
        '        lblCostOfNetwork.Text = Format(CDbl((CostOfNetwork * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
        '        lblCostOfNOC.Text = Format(CDbl((CostOfNOC * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
        '    End If
        '    lblVas.Text = Format(CDbl((Vas * DT_Service.Rows(0).Item("Contract")).ToString.ToString), "###,##0.00")
        '    CashFlow = Revenue - MKTCost - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
        '    CashFlowOneTime = RevenueOneTime - MKTCostOneTime - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
        '    lblCashFlow.Text = Format(CDbl(((CashFlowOneTime - lblCAPEX.Text) + (CashFlow * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")

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
        'Dim strRO As String
        'Dim DTRO As DataTable
        'Dim strCluster As String
        'Dim DTCluster As DataTable

        'strCluster = "select distinct Cluster, Cluster_email, Cluster_name from Cluster where Status = '1' and RO = '" + lblArea.Text + "' "
        'C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster")
        'DTCluster = C.GetDataTable(strCluster)
        'If DTCluster.Rows.Count > 0 Then
        '    lblPrepare.Text = "(" & DTCluster.Rows(0).Item("Cluster_name") & ")"
        '    lblPrepaireEmail.Text = DTCluster.Rows(0).Item("Cluster_email")
        'End If

        'End If

        'Set_Menu()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        InsertData("SubmitApprove")
    End Sub

    Protected Sub btnSaveToEditProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveToEditProject.Click
        InsertData("SaveProject")
    End Sub

    Public Sub InsertData(ByVal Command As String)
        Dim DT_CAPEX As New DataTable
        'Dim DT_CAPEX_Mass As New DataTable
        Dim DT_OPEX As New DataTable
        Dim DT_OTHER As New DataTable
        Dim DT_Service As New DataTable
        Dim DT_ProjectName As New DataTable
        Dim DT_ProjectName_File As New DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim TypeService As String

        strSql = "select * from List_ProjectName where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        DT_ProjectName = C.GetDataTable(strSql)
        strSql = "select * from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        DT_CAPEX = C.GetDataTable(strSql)
        ' strSql = "select * from List_CAPEX_Mass where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        ' DT_CAPEX_Mass = C.GetDataTable(strSql)
        strSql = "select * from List_OPEX where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        DT_OPEX = C.GetDataTable(strSql)
        strSql = "select * from List_OTHER where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        DT_OTHER = C.GetDataTable(strSql)
        strSql = "select * from List_Service where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        DT_Service = C.GetDataTable(strSql)

        If DT_ProjectName.Rows.Count <= 0 Then
            Dim pn_alert As String = "AlertNotification('กรุณากรอกข้อมูล Project Name',function(){ window.location = 'project_name.aspx?menu=create'; });"
            ClientScript.RegisterStartupScript(Page.GetType, "alert_projectname", pn_alert, True)

        ElseIf DT_Service.Rows.Count <= 0 Then
            Dim sv_alert As String = "AlertNotification('กรุณากรอกข้อมูล Service',function(){ window.location = 'add_service.aspx?menu=create'; });"
            ClientScript.RegisterStartupScript(Page.GetType, "alert_servicename", sv_alert, True)

        Else
            'If RadioButton2.Checked = True Then
            '    TypeService = RadioButton2.Text
            'ElseIf RadioButton3.Checked = True Then
            '    TypeService = RadioButton3.Text
            'Else
            '    TypeService = RadioButton1.Text
            'End If
            strSql = "select convert(varchar(3),right(isnull(max(Document_No),'FES000000-00000'),3) + 1) 'Max_Document',  left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) 'YearMonth' " + vbCr
            strSql += "from FeasibilityDocument " + vbCr
            strSql += "where substring(Document_No,4,6) = left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) "
            DT = C.GetDataTable(strSql)
            'Dim a As Integer = 200
            'Dim doc As String = CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")
            Dim doc_no As String = "FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("000")

            strSql = "Insert Into FeasibilityDocument (Document_No,Document_Date,Service_Date,Area,Cluster,Customer_Name,"
            strSql += "Type_Service,Detail_Service,CreateBy,CreateDate,Status,Upload_Project)"
            strSql += "values ('" + doc_no + "','" + C.CDateText(txtDocumentDate.Text) + "','" + C.CDateText(txtServiceDate.Text) + "',"
            strSql += "'" + lblArea.Text + "','" + lblCluster.Text + "','" & lblCustomerName.Text & "','" + lblTypeOfService.Text + "',"
            strSql += "'" + txtDetailService.Text + "','" + Session("uemail") + "',getdate(),'1','0') "
            C.ExecuteNonQuery(strSql)

            strSql = "Insert Into List_Summary (Document_No,Revenue,Revenue_Profit,OPEX,OPEX_Profit, " + vbCr
            strSql += "MarketingPrice,MarketingPrice_Profit,EntertainPrice,EntertainPrice_Profit, " + vbCr
            strSql += "GiftPrice,GiftPrice_Profit,InternetBW,NetworkBW, " + vbCr
            strSql += "MarketingCost,MarketingCost_Profit,InternetCost,NetworkCost,NOCCost, " + vbCr
            strSql += "JasmineGroupCost,OtherCost,OtherCost_Profit,PenaltyCost,PenaltyCost_Profit, " + vbCr
            strSql += "RevenueAfter,RevenueAfter_Profit,CAPEX,CAPEX_Profit,CashFlow,CashFlow_Profit, " + vbCr
            strSql += "Payback,Payback_Profit, " + vbCr
            strSql += "Margin,Margin_Profit,NPV,NPV_Profit,Status,CreateBy,CreateDate)" + vbCr
            strSql += "values ('" & doc_no & "','" & CDbl(lblRevenue.Text) & "','" & CDbl(lblRevenue_total.Text) & "','" & CDbl(lblOPEX.Text) & "','" & CDbl(lblOPEX_total.Text) & "', " + vbCr
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


            strSql = ""
            If DT_ProjectName.Rows.Count > 0 Then
                strSql += "Update dbo.List_ProjectName set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            End If
            If DT_CAPEX.Rows.Count > 0 Then
                strSql += "Update dbo.List_CAPEX set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            End If
            ' If DT_CAPEX_Mass.Rows.Count > 0 Then
            '     strSql += "Update dbo.List_CAPEX_Mass set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            ' End If
            If DT_OPEX.Rows.Count > 0 Then
                strSql += "Update dbo.List_OPEX set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            End If
            If DT_OTHER.Rows.Count > 0 Then
                strSql += "Update dbo.List_OTHER set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            End If
            If DT_Service.Rows.Count > 0 Then
                strSql += "Update dbo.List_Service set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
            End If
            C.ExecuteNonQuery(strSql)

            'Dim doc_no_SLA As String = "SLA_" + doc_no + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + ".pdf"
            'Dim doc_no_Fine As String = "Fine_" + doc_no + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + ".pdf"


            Dim vTempPath As String = Server.MapPath("Upload\")
            Dim vUploadPath As String
            DT_ProjectName_File = C.GetDataTable("select isnull(Doc_File,'') 'Doc_File' from List_ProjectName where Document_No='" + doc_no + "' ")

            'vUploadPath = "SLA"
            'vUploadPath &= "\" & DateTime.Now.ToString("yyyy")
            'vUploadPath &= "\" & DateTime.Now.ToString("MM")
            'If Not Directory.Exists(vTempPath & vUploadPath) Then
            '    Directory.CreateDirectory(vTempPath & vUploadPath)
            'End If
            'vUploadPath &= "\"
            'DT_ProjectName_File = C.GetDataTable("select isnull(SLA_File,'') 'SLA_File', isnull(Fine_File,'') 'Fine_File' from List_ProjectName where Document_No='" + doc_no + "' ")
            'If DT_ProjectName_File.Rows(0).Item("SLA_File") <> "" Then
            '    If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("SLA_File")) Then
            '        'System.IO.File.Move(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("SLA_File"), MapPath(".") & "\Upload\SLA\" & doc_no_SLA)
            '        System.IO.File.Move(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("SLA_File"), vTempPath & vUploadPath & doc_no_SLA)
            '        C.ExecuteNonQuery("Update List_ProjectName set SLA_File = '" + vUploadPath & doc_no_SLA + "' where Document_No='" + doc_no + "' and Status='1' ")
            '    End If
            'End If
            'vUploadPath = "Fine"
            'vUploadPath &= "\" & DateTime.Now.ToString("yyyy")
            'vUploadPath &= "\" & DateTime.Now.ToString("MM")
            'If Not Directory.Exists(vTempPath & vUploadPath) Then
            '    Directory.CreateDirectory(vTempPath & vUploadPath)
            'End If
            'vUploadPath &= "\"
            'If DT_ProjectName_File.Rows(0).Item("Fine_File") <> "" Then
            '    If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("Fine_File")) Then
            '        System.IO.File.Move(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("Fine_File"), vTempPath & vUploadPath & doc_no_Fine)
            '        C.ExecuteNonQuery("Update List_ProjectName set Fine_File = '" + vUploadPath & doc_no_Fine + "' where Document_No='" + doc_no + "' and Status='1' ")
            '    End If
            'End If

            If DT_ProjectName_File.Rows(0).Item("Doc_File") <> "" Then
                vUploadPath = "Doc"
                vUploadPath &= "\" & DateTime.Now.ToString("yyyy")
                vUploadPath &= "\" & DateTime.Now.ToString("MM")
                If Not Directory.Exists(vTempPath & vUploadPath) Then
                    Directory.CreateDirectory(vTempPath & vUploadPath)
                End If
                vUploadPath &= "\"

                Dim doc_no_Doc As String = "DOC_" + doc_no + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + Right(DT_ProjectName_File.Rows(0).Item("Doc_File"), 4)
                If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("Doc_File")) Then
                    System.IO.File.Move(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("Doc_File"), vTempPath & vUploadPath & doc_no_Doc)
                    C.ExecuteNonQuery("Update List_ProjectName set Doc_File = '" + vUploadPath & doc_no_Doc + "' where Document_No='" + doc_no + "' and Status='1' ")
                End If
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


            vSqlIn += "INSERT INTO request_flow ( "
            vSqlIn += "request_id, flow_id, depart_id, flow_step, next_step, "
            vSqlIn += "send_uemail, uemail, approval, require_remark, require_file, add_next, back_step) "
            vSqlIn += "select '" + doc_no + "', fp.flow_id, fp.depart_id, fp.flow_step, fp.next_step, "
            vSqlIn += "dp.uemail, dp.uemail, fp.approval, fp.require_remark, fp.require_file, dp.add_next, fp.back_step "
            vSqlIn += "from flow_pattern fp "
            vSqlIn += "join ( "
            vSqlIn += " SELECT "
            vSqlIn += "      dm.depart_id "
            vSqlIn += "    , dm.depart_name "
            vSqlIn += "    , dm.add_next "
            vSqlIn += "    , uemail = STUFF(( "
            vSqlIn += "          SELECT ';' + du.uemail "
            vSqlIn += "          FROM depart_user du "
            vSqlIn += "          WHERE dm.depart_id = du.depart_id "
            vSqlIn += "          and start_date <= getdate() "
            vSqlIn += "          and (expired_date is null or expired_date >= getdate()) "
            vSqlIn += "          FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') "
            vSqlIn += "     FROM department dm "
            vSqlIn += ") dp on dp.depart_id = fp.depart_id "
            vSqlIn += "where flow_id = " + flow_id + " "
            vSqlIn += "order by fp.flow_step "
            C.ExecuteNonQuery(vSqlIn)


            vSqlIn = CF.rSqlUpdateRequestFlow(lblArea.Text, lblROMail.Text, lblPrepaireEmail.Text, lblPresaleMail.Text, Session("uemail"), doc_no)
            C.ExecuteNonQuery(vSqlIn)

            If Command = "SubmitApprove" Then
                CF.InsertRequestFile("", "", doc_no, "")

                'Response.Write("<script>")
                'Response.Write("window.open('summarypdf.aspx?Doc=" + doc_no + "','_blank')")
                'Response.Write("</script>")

                Dim sm_alert As String = "AlertSuccess('บันทึกข้อมูลและส่งอนุมัติ สำเร็จ',function(){ window.location = 'check_status_list.aspx?menu=check'; });"
                ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)

            Else
                Dim dt_flow As New DataTable
                dt_flow = C.GetDataTable("select * from dbo.request_flow where request_id = '" & doc_no & "' and disable='0' and flow_step = '1' ")
                If dt_flow.Rows.Count > 0 Then
                    vSqlIn = "Update request_flow set flow_status = '110' where request_id = '" & doc_no & "' and disable = '0' and flow_step = '1' " + vbCr
                    vSqlIn += "update FeasibilityDocument set "
                    vSqlIn += "request_status = 110 "
                    vSqlIn += ", last_update = getdate(), last_depart = '" & dt_flow.Rows(0).Item("depart_id") & "' "
                    vSqlIn += ", next_depart = '0' " '***** ขอเพิ่มข้อมูล ให้ลำดับถัดไปเป็น คนสร้างคำขอ
                    vSqlIn += "where Document_No = '" & doc_no & "' "
                    C.ExecuteNonQuery(vSqlIn)
                End If

                Dim sm_alert As String = "AlertSuccess('บันทึกโครงการ สำเร็จ',function(){ window.location = 'edit_list.aspx?menu=edit'; });"
                ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)
            End If



        End If
    End Sub

    Protected Sub test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles test.Click
        'Session("refno") = lblDocumentNo.Text
        'Session("startdate") = txtDocumentDate.Text
        'Session("area") = ddlArea.SelectedValue
        'Session("ref") = txtLocationName.Text
        'Session("enddate") = txtServiceDate.Text
        'Session("cluster") = ddlCluster.SelectedValue
        'Session("customer") = txtCustomerName.Text

        'If RadioButton2.Checked = True Then
        '    Session("radio") = RadioButton2.Text
        'ElseIf RadioButton3.Checked = True Then
        '    Session("radio") = RadioButton3.Text
        'Else
        '    Session("radio") = RadioButton1.Text
        'End If

        'Session("costmonth") = lblMonthly.Text
        'Session("costonetime") = lblOneTimePayment.Text
        'Session("textarea") = txtDetailService.Text
        'Session("capex") = lblCAPEX.Text
        'Session("contactyear") = lblContractYear.Text
        'Session("tablecapex") = CAPEX.InnerHtml
        'Session("tableopex") = OPEX.InnerHtml
        'Session("contact") = lblContract.Text
        'Session("revenue") = lblRevenue.Text
        'Session("marketcost") = lblMKTCost.Text
        'Session("internetcost") = lblCostOfInternet.Text
        'Session("networkcost") = lblCostOfNetwork.Text
        'Session("noccost") = lblCostOfNOC.Text
        'Session("vas") = lblVas.Text
        'Session("cashflow") = lblCashFlow.Text
        'Session("payback") = lblPayBack.Text
        'Session("margin") = lblMargin.Text
        'Session("npv") = lblNPV.Text
        'Session("prepare") = lblPrepare.Text
        'Session("vhro") = lblHRO.Text
        'Session("vNetwork") = Label50.Text
        'Session("vcoo") = Label6.Text
        'Session("vcoo2") = Label12.Text
        'Session("Approved") = Label117.Text
        'Response.Write(ddlArea.SelectedValue)

        'Response.Redirect("summarypdf.aspx")
        'Response.Write("<script>")
        'Response.Write("window.open('summarypdf.aspx','_blank')")
        'Response.Write("</script>")


    End Sub

    Public Sub ShowProjectName(ByVal DT_ProjectName As DataTable)
        Dim strSql As String
        Dim OneTimePayment As Double
        Dim MonthlyPrice As Double

        'Dim DT_ProjectName As DataTable
        'strSql = "select * from List_ProjectName where CreateBy='" + Session("uemail") + "' and Document_No is null "
        'DT_ProjectName = C.GetDataTable(strSql)
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
            If IsDBNull(DT_ProjectName.Rows(0).Item("Customer_Contact_Name")) Then
                lblCustomerName.Text = ""
            Else
                If IsDBNull(DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")) Or DT_ProjectName.Rows(0).Item("Customer_Contact_Tel") = "" Then
                    lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; เบอร์โทร. -"
                Else
                    lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; เบอร์โทร. " + DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
                End If
            End If
            'lblEnterpriseName.Text = DT_ProjectName.Rows(0).Item("Enterprise_Name")
            'txtLocationName.Text = DT_ProjectName.Rows(0).Item("Location_Name")
            lblCustomerType.Text = DT_ProjectName.Rows(0).Item("Customer_Type")
            txtDocumentDate.Text = DT_ProjectName.Rows(0).Item("Document_Date")
            txtServiceDate.Text = DT_ProjectName.Rows(0).Item("Service_Date")
            lblCustomerAssistantName.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_Name")
            'lblCustomerAssistantID.Text = DT_ProjectName.Rows(0).Item("Customer_Assistant_ID")
            If IsDBNull(DT_ProjectName.Rows(0).Item("Area")) Then
                lblArea.Text = ""
            Else
                lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
            End If
            'lblArea.Text = DT_ProjectName.Rows(0).Item("Area")
            If IsDBNull(DT_ProjectName.Rows(0).Item("Cluster")) Then
                lblCluster.Text = ""
            Else
                lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
            End If
            'lblCluster.Text = DT_ProjectName.Rows(0).Item("Cluster")
            'lblCustomerContactName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name")
            'lblCustomerContactTel.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
            If IsDBNull(DT_ProjectName.Rows(0).Item("Customer_Contact_Email")) Then
                lblCustomerContactEmail.Text = ""
            Else
                lblCustomerContactEmail.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
            End If
            'lblCustomerContactEmail.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Email")
            lblCompanyService.Text = DT_ProjectName.Rows(0).Item("Company_Service")
            lblTypeOfService.Text = DT_ProjectName.Rows(0).Item("Type_Service")
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
                'lblHRO.Text = "(" & DT_RO_Director.Rows(0).Item("RO_name") & ")"
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

            'lblCreateProject.Text = "(" & DT_ProjectName.Rows(0).Item("Customer_Assistant_Name") & ")"

            'lblPrepare.Text = "(" & DT_ProjectName.Rows(0).Item("Cluster_name") & ")"
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

        strsql = "select * from dbo.List_CAPEX where CreateBy='" + Session("uemail") + "' and Document_No is null "
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

        strsql = "select * from dbo.List_OPEX where CreateBy='" + Session("uemail") + "' and Document_No is null "
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

        strsql = "select * from dbo.List_OTHER where CreateBy='" + Session("uemail") + "' and Document_No is null "
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

        strsql = "select * from dbo.List_Management where CreateBy='" + Session("uemail") + "' and Document_No is null "
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

            'If (RevenueAfterOperationOne + CDbl(lblOneTimePayment.Text)) >= CDbl(lblTotalCAPEX.Text) Then
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

            'Dim ShowPayMonth(14) As Double
            'Dim ShowPayBack(14) As String
            'Dim per As Double = 0.3


        End If
    End Sub

    Public Function GetTableService() As DataTable
        Dim strSql As String
        Dim DT_Service As DataTable

        strSql = "select * from dbo.List_Service where CreateBy='" + Session("uemail") + "' and Document_No is null "
        DT_Service = C.GetDataTable(strSql)
        Return DT_Service
    End Function

    Public Function GetTableProjectName() As DataTable
        Dim strSql As String
        Dim DT_ProjectName As DataTable
        strSql = "select * from List_ProjectName where CreateBy='" + Session("uemail") + "' and Document_No is null "
        DT_ProjectName = C.GetDataTable(strSql)
        Return DT_ProjectName
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


End Class
