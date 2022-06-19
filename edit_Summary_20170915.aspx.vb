Imports System.Data
Imports Microsoft.VisualBasic
Partial Class edit_Summary
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim xRequest_id
    Dim request_status

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        menu_capex.HRef = "edit_capex.aspx?request_id=" + xRequest_id
        menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
        menu_service.HRef = "edit_service.aspx?request_id=" + xRequest_id

        Dim dt_check As DataTable
        dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (request_status = 55 or request_status = 110) and Document_No = '" + xRequest_id + "'")
        If dt_check.Rows.Count > 0 Then
            If ((dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                request_status = dt_check.Rows(0).Item("request_status").ToString
                Dim DT_Document As DataTable
                Dim strSql As String
                Dim DT_CAPEX As DataTable
                Dim DT_OPEX As DataTable
                Dim DT_Service As DataTable
                Dim DT_InternetBWCost As DataTable
                Dim DomesticCost As Double
                Dim InternationalCost As Double
                Dim Transit As Double
                Dim xDSL_FTTx As Double
                Dim r As String
                Dim total_capex As Double = 0
                Dim total_opex As Double = 0
                strSql = "select * from dbo.List_CAPEX where CreateBy='" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
                DT_CAPEX = C.GetDataTable(strSql)
                r = "<table style='width: 100%'>"
                For i As Integer = 0 To DT_CAPEX.Rows.Count - 1
                    r += "<tr>"
                    r += "<td>" + DT_CAPEX.Rows(i).Item("CAPEX_Name").ToString + "</td>"
                    r += "<td align='center'>" + DT_CAPEX.Rows(i).Item("Asset_Type").ToString + "</td>"
                    r += "<td align='right'>" + Format(CDbl(DT_CAPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
                    r += "<td align='right'>บาท/เดือน</td>"
                    r += "</tr>"
                    total_capex += DT_CAPEX.Rows(i).Item("Cost")
                Next
                r += "<tr>"
                r += "<td><b>Total Investment</b></td>"
                r += "<td></td>"
                r += "<td align='right'><b><u>" + Format(CDbl(total_capex.ToString), "###,##0.00") + "</u></b></td>"
                r += "<td></td>"
                r += "</tr>"
                r += "</table>"
                CAPEX.InnerHtml = r

                strSql = "select * from dbo.List_OPEX where CreateBy='" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
                DT_OPEX = C.GetDataTable(strSql)
                r = "<table style='width: 100%'>"
                For i As Integer = 0 To DT_OPEX.Rows.Count - 1
                    r += "<tr>"
                    r += "<td>" + DT_OPEX.Rows(i).Item("OPEX_Name").ToString + "</td>"
                    r += "<td align='right'>" + Format(CDbl(DT_OPEX.Rows(i).Item("Cost").ToString), "###,##0.00") + "</td>"
                    r += "<td align='right'>บาท/เดือน</td>"
                    r += "</tr>"
                    total_opex += DT_OPEX.Rows(i).Item("Cost")
                Next
                r += "<tr>"
                r += "<td><b>Total Investment</b></td>"
                r += "<td align='right'><b><u>" + Format(CDbl(total_opex.ToString), "###,##0.00") + "</u></b></td>"
                r += "<td></td>"
                r += "</tr>"
                r += "</table>"
                OPEX.InnerHtml = r

                strSql = "select * from dbo.List_Model where CreateBy='" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
                DT_Service = C.GetDataTable(strSql)
                If DT_Service.Rows.Count > 0 And DT_CAPEX.Rows.Count > 0 And DT_OPEX.Rows.Count > 0 Then
                    lblContractYear.Text = Math.Round(CDbl(DT_Service.Rows(0).Item("Contract").ToString / 12), 0)
                    lblContract.Text = DT_Service.Rows(0).Item("Contract").ToString
                    lblMonthly.Text = Format(CDbl(DT_Service.Rows(0).Item("Monthly").ToString), "###,##0.00")
                    lblOneTimePayment.Text = Format(CDbl(DT_Service.Rows(0).Item("OneTimePayment").ToString), "###,##0.00")
                    lblCAPEX.Text = Format(CDbl(total_capex.ToString), "###,##0.00")

                    ' Define money format.
                    Dim MoneyFmt As String = "###,##0.00"
                    ' Define percentage format.
                    Dim PercentFmt As String = "#0.0"
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

                    DT_InternetBWCost = C.GetDataTable("select * from InternetBWCost where BWCost_Type='" + DT_Service.Rows(0).Item("Discount") + "' ")
                    Revenue = DT_Service.Rows(0).Item("Monthly")
                    RevenueOneTime = DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")
                    MKTCost = (Revenue * 3) / 100
                    MKTCostOneTime = (RevenueOneTime * 3) / 100
                    CostOfNetwork = ((DT_Service.Rows(0).Item("EthernetIPV") * (DT_InternetBWCost.Rows(0).Item("IPV_Discount") / 100)) + DT_Service.Rows(0).Item("EthernetINP")) * DT_InternetBWCost.Rows(0).Item("Network")
                    CostOfNOC = (DT_Service.Rows(0).Item("TotalINLCurcuits") + DT_Service.Rows(0).Item("TotalIPVCurcuits") + DT_Service.Rows(0).Item("TotalINPCurcuits")) * DT_InternetBWCost.Rows(0).Item("NOC")
                    Vas = C.GetDataTable("select SUM(Cost) 'VasCost' from dbo.List_OPEX where CreateBy='" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' ").Rows(0).Item("VasCost")

                    'lblRevenue.Text = ((Revenue * DT_Service.Rows(0).Item("Contract")) + DT_Service.Rows(0).Item("OneTimePayment")).ToString
                    'lblMKTCost.Text = (MKTCost * DT_Service.Rows(0).Item("Contract")).ToString
                    lblRevenue.Text = Format(CDbl((RevenueOneTime + (Revenue * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")
                    lblMKTCost.Text = Format(CDbl(((MKTCostOneTime) + (MKTCost * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")

                    If DT_InternetBWCost.Rows.Count > 0 Then
                        DomesticCost = (DT_Service.Rows(0).Item("Domestic") * DT_InternetBWCost.Rows(0).Item("Domestic")) * (DT_InternetBWCost.Rows(0).Item("Dom_Discount") / 100)
                        InternationalCost = ((DT_Service.Rows(0).Item("International") - DT_Service.Rows(0).Item("Transit")) * DT_InternetBWCost.Rows(0).Item("All_International")) * (DT_InternetBWCost.Rows(0).Item("Inter_Discount") / 100)
                        Transit = DT_Service.Rows(0).Item("Transit") * DT_InternetBWCost.Rows(0).Item("Transit")
                        xDSL_FTTx = DT_Service.Rows(0).Item("ServicePrice")
                        CostOfInternet = (DomesticCost + InternationalCost + Transit + xDSL_FTTx)
                        lblCostOfInternet.Text = Format(CDbl((CostOfInternet * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                        lblCostOfNetwork.Text = Format(CDbl((CostOfNetwork * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                        lblCostOfNOC.Text = Format(CDbl((CostOfNOC * DT_Service.Rows(0).Item("Contract")).ToString), "###,##0.00")
                    End If
                    lblVas.Text = Format(CDbl((Vas * DT_Service.Rows(0).Item("Contract")).ToString.ToString), "###,##0.00")
                    CashFlow = Revenue - MKTCost - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
                    CashFlowOneTime = RevenueOneTime - MKTCostOneTime - CostOfInternet - CostOfNetwork - CostOfNOC - Vas
                    lblCashFlow.Text = Format(CDbl((CashFlowOneTime + (CashFlow * (DT_Service.Rows(0).Item("Contract") - 1))).ToString), "###,##0.00")
                    'lblCashFlow.Text = (((DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")) - (((DT_Service.Rows(0).Item("Monthly") + DT_Service.Rows(0).Item("OneTimePayment")) * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas) + ((DT_Service.Rows(0).Item("Monthly") * (DT_Service.Rows(0).Item("Contract") - 1)) - (((DT_Service.Rows(0).Item("Monthly") * (DT_Service.Rows(0).Item("Contract") - 1)) * 3) / 100) - ((CostOfInternet + CostOfNetwork + CostOfNOC + Vas) * (DT_Service.Rows(0).Item("Contract") - 1)))).ToString

                    'CashFlow = DT_Service.Rows(0).Item("Monthly") - ((DT_Service.Rows(0).Item("Monthly") * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas

                    Dim ShowPayMonth(14) As Double
                    Dim ShowPayBack(14) As String
                    Dim per As Double = 0.3

                    If lblOneTimePayment.Text - lblCAPEX.Text > 0 Then
                        lblPayBack.Text = "<1"
                        For j As Integer = 0 To ShowPayBack.Length - 1
                            ShowPayBack(j) = "<1"
                            ShowPayMonth(j) = Revenue * per
                            per = per + 0.1
                        Next
                    Else
                        lblPayBack.Text = Format(lblCAPEX.Text / CashFlow, "#0.0")
                        For j As Integer = 0 To ShowPayBack.Length - 1
                            ShowPayBack(j) = Format(lblCAPEX.Text / ((Revenue * per) - ((Revenue * per * 3) / 100) - CostOfInternet - CostOfNetwork - CostOfNOC - Vas), "#0.0")
                            ShowPayMonth(j) = Revenue * per
                            per = per + 0.1
                        Next
                    End If
                    Dim arrList As New ArrayList()
                    For j As Integer = 0 To ShowPayBack.Length - 1
                        arrList.Add(New ListItem(Format(CDbl(ShowPayMonth(j)), "###,##0.00"), ShowPayBack(j)))
                    Next
                    GridView1.DataSource = arrList
                    GridView1.DataBind()

                    For i As Integer = 0 To lblContract.Text - 1
                        If i = 0 Then
                            values(i) = CashFlowOneTime - lblCAPEX.Text
                        Else
                            values(i) = CashFlow
                        End If
                    Next

                    Dim FixedRetRate As Double = 0.05
                    ' Calculate net present value.
                    Dim NetPVal As Double = NPV(FixedRetRate / 12, values)
                    ' Display net present value.
                    ' MsgBox("The net present value of these cash flows is " & Format(NetPVal, MoneyFmt) & ".")
                    lblNPV.Text = Format(NetPVal, MoneyFmt)
                    lblMargin.Text = Format((lblNPV.Text / lblRevenue.Text) * 100, PercentFmt).ToString & "%"

                End If
                If Not Page.IsPostBack Then

                    Dim strRO As String
                    Dim DTRO As DataTable
                    Dim strCluster As String
                    Dim DTCluster As DataTable

                    strRO = "select distinct RO from Cluster where Status = '1' "
                    C.SetDropDownList(ddlArea, strRO, "RO", "RO")

                    strCluster = "select distinct Cluster, Cluster_email, Cluster_name from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' "
                    C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster")
                    DTCluster = C.GetDataTable(strCluster)
                    If DTCluster.Rows.Count > 0 Then
                        lblPrepare.Text = "(" & DTCluster.Rows(0).Item("Cluster_name") & ")"
                        lblPrepaireEmail.Text = DTCluster.Rows(0).Item("Cluster_email")
                    End If



                    strSql = "select Document_No, convert(varchar(10),Document_Date,103) 'Document_Date', convert(varchar(10),Service_Date,103) 'Service_Date' " + vbCr
                    strSql += ", Area, Cluster, Customer_Name, Location_Name, Type_Service, Detail_Service  " + vbCr
                    strSql += "from dbo.FeasibilityDocument where Document_No = '" + xRequest_id + "' and Status = '1' "
                    DT_Document = C.GetDataTable(strSql)
                    If DT_Document.Rows.Count > 0 Then
                        lblDocumentNo.Text = DT_Document.Rows(0).Item("Document_No")
                        txtDocumentDate.Value = DT_Document.Rows(0).Item("Document_Date")
                        ddlArea.SelectedValue = DT_Document.Rows(0).Item("Area")
                        txtLocationName.Text = DT_Document.Rows(0).Item("Location_Name")
                        txtServiceDate.Value = DT_Document.Rows(0).Item("Service_Date")
                        ddlCluster.SelectedValue = DT_Document.Rows(0).Item("Cluster")
                        txtCustomerName.Text = DT_Document.Rows(0).Item("Customer_Name")
                        'RadioButton4.Checked = False
                        If DT_Document.Rows(0).Item("Type_Service") = "Re-New Service" Then
                            RadioButton2.Checked = True
                        ElseIf DT_Document.Rows(0).Item("Type_Service") = "Maintenance" Then
                            RadioButton3.Checked = True
                        Else
                            RadioButton1.Checked = True
                        End If
                        txtDetailService.Text = DT_Document.Rows(0).Item("Detail_Service")
                    End If
                End If
            Else
                btnSave.Visible = False
            End If
        Else
            btnSave.Visible = False
        End If
        Set_Menu()


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim DT_CAPEX As New DataTable
        Dim DT_OPEX As New DataTable
        Dim DT_Model As New DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim TypeService As String

        strSql = "select * from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        DT_CAPEX = C.GetDataTable(strSql)
        strSql = "select * from List_OPEX where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        DT_OPEX = C.GetDataTable(strSql)
        strSql = "select * from List_Model where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        DT_Model = C.GetDataTable(strSql)

        If DT_CAPEX.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล CAPEX');focus();window.location.href='edit_capex.aspx?request_id=" + xRequest_id + "';", True)
        ElseIf DT_OPEX.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล OPEX');focus();window.location.href='edit_opex.aspx?request_id=" + xRequest_id + "';", True)
        ElseIf DT_Model.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณากรอกข้อมูล Service');focus();window.location.href='edit_service.aspx?request_id=" + xRequest_id + "';", True)
        ElseIf txtDocumentDate.Value = "" Or txtDocumentDate.Value.Length <> 10 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ วันที่จัดทำ/ปรับปรุง');", True)
            txtDocumentDate.Focus()
        ElseIf txtServiceDate.Value = "" Or txtServiceDate.Value.Length <> 10 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ วันที่เริ่มให้บริการ');", True)
            txtServiceDate.Focus()
        Else
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
            Dim doc_no As String = "FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")
            'strSql = "insert into FeasbilityDocument (Document_No,Document_Date,Service_Date,Area,Cluster,Customer_Name,Location_Name,Type_Service,Detail_Service,CreateBy,CreateDate,Status) values ('" + doc_no + "','" + C.CDateText(txtDocumentDate.Text) + "','" + C.CDateText(txtServiceDate.Text) + "','" + txtArea.Text + "','" + txtCluster.Text + "','" + txtCustomerName.Text + "','" + txtLocationName.Text + "','" + TypeService + "','" + txtDetailService.Text + "','" + Session("uemail") + "',getdate(),'1') "
            strSql = "Update FeasibilityDocument set Document_Date = '" + C.CDateText(txtDocumentDate.Value) + "', Service_Date = '" + C.CDateText(txtServiceDate.Value) + "', Area = '" + ddlArea.SelectedValue + "', Cluster = '" + ddlCluster.SelectedValue + "', Customer_Name = '" + txtCustomerName.Text + "', Location_Name = '" + txtLocationName.Text + "', Type_Service = '" + TypeService + "', Detail_Service = '" + txtDetailService.Text + "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate(), Status = '1'  "
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
        CF.SaveFlow(hide_uemail.Value, hide_flow_no.Value, hide_flow_sub.Value, hide_next_step.Value, hide_back_step.Value, hide_department.Value, hide_flow_status.Value, hide_flow_remark.Value, flow_file)
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
