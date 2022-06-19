Imports System.Data
Partial Class edit_service
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim xRequest_id As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        If Not Page.IsPostBack Then
            menu_project_name.HRef = "edit_project_name.aspx?request_id=" + xRequest_id
            menu_capex.HRef = "edit_capex.aspx?request_id=" + xRequest_id
            menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
            menu_other.HRef = "edit_other.aspx?request_id=" + xRequest_id
            menu_management.HRef = "edit_management.aspx?request_id=" + xRequest_id
            menu_summary.HRef = "edit_Summary.aspx?request_id=" + xRequest_id

            Dim alert As String
            Dim dt_check As DataTable
            dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (/*request_status = 0 or request_status = 55 or*/ request_status = 110) and Document_No = '" + xRequest_id + "'")
            If dt_check.Rows.Count > 0 Then
                'If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "0" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                'If dt_check.Rows(0).Item("CreateBy") <> Session("uemail") Then
                '    Button1.Visible = False
                '    LinkButton1.Visible = False
                'End If
                If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                    'If Session("Login_permission") = "inspector" Or Session("Login_permission") = "administrator" Then
                    '    dropdown_discount.Visible = True
                    'End If

                    'Dim DT As DataTable
                    'Dim DT_List As DataTable
                    'Dim strSql As String

                    'C.SetDropDownList(ddlDiscount, "select distinct BWCost_Type from InternetBWCost order by 1 desc", "BWCost_Type", "BWCost_Type")

                    'strSql = "select * from dbo.List_Model where Document_No = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "' "
                    'DT_List = C.GetDataTable(strSql)
                    'If DT_List.Rows.Count > 0 Then
                    '    txtContract.Text = DT_List.Rows(0).Item("Contract").ToString
                    '    txtDom.Text = DT_List.Rows(0).Item("Domestic").ToString
                    '    txtInter.Text = DT_List.Rows(0).Item("International").ToString
                    '    txtTransit.Text = DT_List.Rows(0).Item("Transit").ToString
                    '    txtTotalINL.Text = DT_List.Rows(0).Item("TotalINLCurcuits").ToString
                    '    txtEthernetIPV.Text = DT_List.Rows(0).Item("EthernetIPV").ToString
                    '    txtTotalIPV.Text = DT_List.Rows(0).Item("TotalIPVCurcuits").ToString
                    '    txtEthernetINP.Text = DT_List.Rows(0).Item("EthernetINP").ToString
                    '    txtPriceINP.Text = DT_List.Rows(0).Item("ServicePrice").ToString
                    '    txtTotalINP.Text = DT_List.Rows(0).Item("TotalINPCurcuits").ToString
                    '    txtMonthly.Text = DT_List.Rows(0).Item("Monthly").ToString
                    '    lblTotalYealy.Text = Format(CDbl(txtMonthly.Text) * 12, "###,##0.00") '(CInt(txtMonthly.Text) * 12).ToString
                    '    txtOneTime.Text = DT_List.Rows(0).Item("OneTimePayment").ToString

                    '    ddlDiscount.SelectedValue = DT_List.Rows(0).Item("Discount")

                    '    lblDom.Text = DT_List.Rows(0).Item("Dom_Discount").ToString
                    '    lblInterUtilization.Text = DT_List.Rows(0).Item("Inter_Discount").ToString
                    '    lblIPVUtilization.Text = DT_List.Rows(0).Item("IPV_Discount").ToString

                    '    txtDomestic_Price.Text = DT_List.Rows(0).Item("Domestic_Price").ToString
                    '    txtAll_International_Price.Text = DT_List.Rows(0).Item("All_International_Price").ToString
                    '    txtTransit_Price.Text = DT_List.Rows(0).Item("Transit_Price").ToString
                    '    txtNetwork_Price.Text = DT_List.Rows(0).Item("Network_Price").ToString
                    '    txtNOC_Price.Text = DT_List.Rows(0).Item("NOC_Price").ToString
                    '    txtDom_Discount.Text = DT_List.Rows(0).Item("Dom_Discount").ToString
                    '    txtInter_Discount.Text = DT_List.Rows(0).Item("Inter_Discount").ToString
                    '    txtIPV_Discount.Text = DT_List.Rows(0).Item("IPV_Discount").ToString

                    '    If DT_List.Rows(0).Item("Discount").ToString = "Custom" And (Session("Login_permission") = "inspector" Or Session("Login_permission") = "administrator") Then
                    '        edit_discount.Visible = True
                    '    End If

                    'Else
                    '    txtContract.Text = "12"
                    '    txtDom.Text = "0"
                    '    txtInter.Text = "0"
                    '    txtTransit.Text = "4"
                    '    txtTotalINL.Text = "0"
                    '    txtEthernetIPV.Text = "0"
                    '    txtTotalIPV.Text = "0"
                    '    txtEthernetINP.Text = "0"
                    '    txtPriceINP.Text = "0"
                    '    txtTotalINP.Text = "0"
                    '    txtMonthly.Text = "0"
                    '    lblTotalYealy.Text = (CInt(txtMonthly.Text) * 12).ToString
                    '    txtOneTime.Text = "0"
                    'End If

                    'strSql = "select * from InternetBWCost where BWCost_Type = '" + ddlDiscount.SelectedValue + "' order by 1"
                    'DT = C.GetDataTable(strSql)
                    'If DT.Rows.Count > 0 Then
                    '    'lblDom.Text = DT.Rows(0).Item("Dom_Discount").ToString
                    '    'lblInterUtilization.Text = DT.Rows(0).Item("Inter_Discount").ToString
                    '    'lblIPVUtilization.Text = DT.Rows(0).Item("IPV_Discount").ToString
                    'End If


                    Dim DT_CustomerType As DataTable
                    Dim DT_List As DataTable
                    Dim DT_Utilization As New DataTable
                    Dim strSql As String
                    strSql = "select cus.CustomerType_name, cus.Utilization, cus.Input, cus.Status, cus.BWCostType from List_ProjectName pro inner join CustomerType cus on pro.Customer_Type = cus.CustomerType_name where pro.CreateBy='" + Session("uemail") + "'  and pro.Document_No = '" + xRequest_id + "' and cus.Status = '1' "
                    DT_CustomerType = C.GetDataTable(strSql)
                    If DT_CustomerType.Rows.Count > 0 Then
                        'lblInputPrice.InnerText = DT_CustomerType.Rows(0).Item("Input").ToString
                        'lblUtilization.Text = DT_CustomerType.Rows(0).Item("Utilization").ToString
                        lblCustomerType.Text = DT_CustomerType.Rows(0).Item("BWCostType").ToString
                    Else
                        'lblInputPrice.InnerText = "-"
                        lblCustomerType.Text = "Normal"
                    End If

                    'DT_Utilization = C.GetDataTable("Select * from InternetBWCost where BWCostType = '" & lblCustomerType.Text & "'")
                    'If DT_Utilization.Rows.Count > 0 Then
                    '    txtUtilization.Text = DT_Utilization.Rows(0).Item("Utilization")
                    '    txtDirectTraffic.Text = DT_Utilization.Rows(0).Item("InterDirect")
                    '    txtDomCost.Text = DT_Utilization.Rows(0).Item("DomesticCost")
                    '    txtAllInterCost.Text = DT_Utilization.Rows(0).Item("AllInternationalCost")
                    '    txtTransitCost.Text = DT_Utilization.Rows(0).Item("TransitCost")
                    '    txtNetworkCost.Text = DT_Utilization.Rows(0).Item("NetworkCost")
                    '    txtNOCCost.Text = DT_Utilization.Rows(0).Item("NOCCost")
                    'Else
                    '    txtUtilization.Text = "0"
                    '    txtDirectTraffic.Text = "0"
                    '    txtDomCost.Text = "0"
                    '    txtAllInterCost.Text = "0"
                    '    txtTransitCost.Text = "0"
                    '    txtNetworkCost.Text = "0"
                    '    txtNOCCost.Text = "0"
                    'End If

                    strSql = "select * from List_Service where Document_No = '" + xRequest_id + "' "
                    DT_List = C.GetDataTable(strSql)
                    If DT_List.Rows.Count > 0 Then
                        '<!--------Service--------------->
                        txtINL.Text = DT_List.Rows(0).Item("INL").ToString
                        txtIPV.Text = DT_List.Rows(0).Item("IPV").ToString
                        txtINLECO.Text = DT_List.Rows(0).Item("INLECO").ToString
                        txtIPVECO.Text = DT_List.Rows(0).Item("IPVECO").ToString
                        txtINP.Text = DT_List.Rows(0).Item("INP").ToString
                        txtINLIPVoNet.Text = DT_List.Rows(0).Item("INLIPVoNet").ToString
                        txtINF.Text = DT_List.Rows(0).Item("INF").ToString
                        txtIDC.Text = DT_List.Rows(0).Item("IDC").ToString
                        txtIAD.Text = DT_List.Rows(0).Item("IAD").ToString
                        txtIAP.Text = DT_List.Rows(0).Item("IAP").ToString
                        txtOFC.Text = DT_List.Rows(0).Item("OFC").ToString
                        txtSIT.Text = DT_List.Rows(0).Item("SIT").ToString
                        lblTotalService.Text = DT_List.Rows(0).Item("TotalService").ToString
                        txtINL_NOC.Text = DT_List.Rows(0).Item("INL_NOC").ToString
                        txtIPV_NOC.Text = DT_List.Rows(0).Item("IPV_NOC").ToString
                        txtINLECO_NOC.Text = DT_List.Rows(0).Item("INLECO_NOC").ToString
                        txtIPVECO_NOC.Text = DT_List.Rows(0).Item("IPVECO_NOC").ToString
                        txtINP_NOC.Text = DT_List.Rows(0).Item("INP_NOC").ToString
                        txtINLIPVoNet_NOC.Text = DT_List.Rows(0).Item("INLIPVoNet_NOC").ToString
                        txtINF_NOC.Text = DT_List.Rows(0).Item("INF_NOC").ToString
                        txtIDC_NOC.Text = DT_List.Rows(0).Item("IDC_NOC").ToString
                        txtIAD_NOC.Text = DT_List.Rows(0).Item("IAD_NOC").ToString
                        txtIAP_NOC.Text = DT_List.Rows(0).Item("IAP_NOC").ToString
                        txtOFC_NOC.Text = DT_List.Rows(0).Item("OFC_NOC").ToString
                        txtSIT_NOC.Text = DT_List.Rows(0).Item("SIT_NOC").ToString
                        lblNOC.Text = DT_List.Rows(0).Item("NOC").ToString

                        '<!--------INL Bandwidth--------------->
                        txtDomestic.Text = DT_List.Rows(0).Item("Domestic").ToString
                        lblINLUtilization.Text = DT_List.Rows(0).Item("INLUtilization").ToString
                        txtInternational.Text = DT_List.Rows(0).Item("International").ToString
                        lblInterUtilization.Text = DT_List.Rows(0).Item("InterUtilization").ToString
                        lblInterDirect.Text = DT_List.Rows(0).Item("InterDirect").ToString
                        lblDirectTraffic.Text = Math.Round(CDbl(DT_List.Rows(0).Item("DirectTraffic").ToString), 0, MidpointRounding.AwayFromZero)
                        lblDirectTrafficValue.Text = DT_List.Rows(0).Item("DirectTraffic").ToString
                        lblCaching.Text = Math.Round(CDbl(DT_List.Rows(0).Item("Caching").ToString), 0, MidpointRounding.AwayFromZero)
                        lblCachingValue.Text = DT_List.Rows(0).Item("Caching").ToString

                        '<!--------IPV Bandwidth--------------->
                        txtIPVEthernet.Text = DT_List.Rows(0).Item("EthernetIPV").ToString
                        lblIPVUtilization.Text = DT_List.Rows(0).Item("IPVUtilization").ToString

                        '<!--------Network Bandwidth--------------->
                        lblNetworkEthernet.Text = Format(CInt(DT_List.Rows(0).Item("EthernetNetwork").ToString), "###,##0")
                        lblNetworkPort.Text = Format(CInt(DT_List.Rows(0).Item("NetworkPort").ToString), "###,##0")
                        lblNetworkUtilization.Text = DT_List.Rows(0).Item("NetworkUtilization").ToString


                        '<!--------Service Price--------------->
                        txtUtilization.Text = DT_List.Rows(0).Item("INLUtilization")
                        txtDirectTraffic.Text = DT_List.Rows(0).Item("InterDirect").ToString
                        txtDomCost.Text = DT_List.Rows(0).Item("DomesticCost").ToString
                        txtAllInterCost.Text = DT_List.Rows(0).Item("AllInternationalCost").ToString
                        txtTransitCost.Text = DT_List.Rows(0).Item("TransitCost").ToString
                        txtNetworkCost.Text = DT_List.Rows(0).Item("NetworkCost").ToString
                        txtNetworkPortCost.Text = DT_List.Rows(0).Item("NetworkPortCost")
                        txtNOCCost.Text = DT_List.Rows(0).Item("NOCCost").ToString
                        'CalculateSellingPrice()
                        'CalculateOneTimePayment()
                    Else 
                        '<!--------Service--------------->
                        txtINL.Text = "0"
                        txtIPV.Text = "0"
                        txtINLECO.Text = "0"
                        txtIPVECO.Text = "0"
                        txtINP.Text = "0"
                        txtINLIPVoNet.Text = "0"
                        txtINF.Text = "0"
                        txtIDC.Text = "0"
                        txtIAD.Text = "0"
                        txtIAP.Text = "0"
                        txtOFC.Text = "0"
                        txtSIT.Text = "0"
                        lblTotalService.Text = "0"
                        txtINL_NOC.Text = "0"
                        txtIPV_NOC.Text = "0"
                        txtINLECO_NOC.Text = "0"
                        txtIPVECO_NOC.Text = "0"
                        txtINP_NOC.Text = "0"
                        txtINLIPVoNet_NOC.Text = "0"
                        txtINF_NOC.Text = "0"
                        txtIDC_NOC.Text = "0"
                        txtIAD_NOC.Text = "0"
                        txtIAP_NOC.Text = "0"
                        txtOFC_NOC.Text = "0"
                        txtSIT_NOC.Text = "0"
                        lblNOC.Text = "0"

                        '<!--------INL Bandwidth--------------->
                        txtDomestic.Text = "0"
                        lblINLUtilization.Text = "0"
                        txtInternational.Text = "0"
                        lblInterUtilization.Text = "0"
                        lblInterDirect.Text = "0"
                        lblDirectTraffic.Text = "0"
                        lblDirectTrafficValue.Text = "0"
                        lblCaching.Text = "0"
                        lblCachingValue.Text = "0"

                        '<!--------IPV Bandwidth--------------->
                        txtIPVEthernet.Text = "0"
                        lblIPVUtilization.Text = "0"

                        '<!--------Network Bandwidth--------------->
                        lblNetworkEthernet.Text = "0"
                        lblNetworkPort.Text = "0"
                        lblNetworkUtilization.Text = "0"


                        '<!--------Service Price--------------->
                        txtUtilization.Text = "0"
                        txtDirectTraffic.Text = "0"
                        txtDomCost.Text = "0"
                        txtAllInterCost.Text = "0"
                        txtTransitCost.Text = "0"
                        txtNetworkCost.Text = "0"
                        txtNetworkPortCost.Text = "0"
                        txtNOCCost.Text = "0"

                    End If
                    If Session("Login_permission") = "administrator" Then
                        RowCost.Visible = True
                    Else
                        RowCost.Visible = False
                    End If
                Else
                    Button1.Visible = False
                    LinkButton1.Visible = False
                    alert = "AlertNotification('User ไม่มีสิทธิ์ ในการแก้ไขข้อมูลได้!',"
                    alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                    alert += "});"
                    ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
                End If
            Else
                Button1.Visible = False
                LinkButton1.Visible = False
                alert = "AlertNotification('โปรเจค " & xRequest_id & " ไม่อยู่ในสถานะ ที่สามารถแก้ไขข้อมูลได้!',"
                alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                alert += "});"
                ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
            End If

        End If
    End Sub

    Public Sub CalculateBandwidth()
        lblDirectTraffic.Text = Math.Round(CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100), 0, MidpointRounding.AwayFromZero)
        lblDirectTrafficValue.Text = CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100)
        lblCaching.Text = Math.Round((CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100)) - (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100)), 0, MidpointRounding.AwayFromZero)
        lblCachingValue.Text = (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100)) - (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100))
        lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
    End Sub

    Protected Sub txtDomestic_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDomestic.TextChanged
        If IsNumeric(txtDomestic.Text) Then
            txtDomestic.Text = Math.Round(CDbl(txtDomestic.Text), 0, MidpointRounding.AwayFromZero)
            lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
            txtInternational.Focus()
        Else
            txtDomestic.Text = "0"
            lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Domestic เป็นตัวเลขเท่านั้น!');", True)
            txtDomestic.Focus()
        End If
    End Sub


    Protected Sub txtIPVEthernet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPVEthernet.TextChanged
        If IsNumeric(txtIPVEthernet.Text) Then
            txtIPVEthernet.Text = Math.Round(CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero)
            lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
        Else
            txtIPVEthernet.Text = "0"
            lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Ethernet เป็นตัวเลขเท่านั้น!');", True)
            txtIPVEthernet.Focus()
        End If
    End Sub


    Protected Sub txtInternational_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInternational.TextChanged
        If IsNumeric(txtInternational.Text) Then
            txtInternational.Text = Math.Round(CDbl(txtInternational.Text), 0, MidpointRounding.AwayFromZero)
            'lblDirectTraffic.Text = Math.Round(CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100), 0, MidpointRounding.AwayFromZero)
            'lblCaching.Text = Math.Round((CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100)) - (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100)), 0, MidpointRounding.AwayFromZero)
            'lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
            CalculateBandwidth()
            txtIPVEthernet.Focus()
        Else
            txtInternational.Text = "0"
            'lblDirectTraffic.Text = Math.Round(CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100), 0, MidpointRounding.AwayFromZero)
            'lblCaching.Text = Math.Round((CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100)) - (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100)), 0, MidpointRounding.AwayFromZero)
            'lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
            CalculateBandwidth()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
            txtInternational.Focus()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveData("Save")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        SaveData("Next")
    End Sub

    Private Sub SaveData(ByVal SaveOrNext As String)
        Dim DT As DataTable
        Dim strSql As String
        Dim r As String = ""
        If IsNumeric(txtINL.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL เป็นตัวเลขเท่านั้น!');", True)
            txtINL.Focus()
        ElseIf IsNumeric(txtIPV.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV เป็นตัวเลขเท่านั้น!');", True)
            txtIPV.Focus()
        ElseIf IsNumeric(txtINLECO.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtINLECO.Focus()
        ElseIf IsNumeric(txtIPVECO.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtIPVECO.Focus()
        ElseIf IsNumeric(txtINP.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INP เป็นตัวเลขเท่านั้น!');", True)
            txtINP.Focus()
        ElseIf IsNumeric(txtINLIPVoNet.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL/IPVoNet เป็นตัวเลขเท่านั้น!');", True)
            txtINLIPVoNet.Focus()
        ElseIf IsNumeric(txtINF.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INF เป็นตัวเลขเท่านั้น!');", True)
            txtINF.Focus()
        ElseIf IsNumeric(txtIDC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IDC เป็นตัวเลขเท่านั้น!');", True)
            txtIDC.Focus()
        ElseIf IsNumeric(txtIAD.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAD เป็นตัวเลขเท่านั้น!');", True)
            txtIAD.Focus()
        ElseIf IsNumeric(txtIAP.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAP เป็นตัวเลขเท่านั้น!');", True)
            txtIAP.Focus()
        ElseIf IsNumeric(txtOFC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ OFC เป็นตัวเลขเท่านั้น!');", True)
            txtOFC.Focus()
        ElseIf IsNumeric(txtSIT.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ SIT เป็นตัวเลขเท่านั้น!');", True)
            txtSIT.Focus()
        ElseIf IsNumeric(txtINL_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL เป็นตัวเลขเท่านั้น!');", True)
            txtINL_NOC.Focus()
        ElseIf IsNumeric(txtIPV_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV เป็นตัวเลขเท่านั้น!');", True)
            txtIPV_NOC.Focus()
        ElseIf IsNumeric(txtINLECO_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtINLECO_NOC.Focus()
        ElseIf IsNumeric(txtIPVECO_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtIPVECO_NOC.Focus()
        ElseIf IsNumeric(txtINP_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INP เป็นตัวเลขเท่านั้น!');", True)
            txtINP_NOC.Focus()
        ElseIf IsNumeric(txtINLIPVoNet_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL/IPVoNet เป็นตัวเลขเท่านั้น!');", True)
            txtINLIPVoNet_NOC.Focus()
        ElseIf IsNumeric(txtINF_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INF เป็นตัวเลขเท่านั้น!');", True)
            txtINF_NOC.Focus()
        ElseIf IsNumeric(txtIDC_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IDC เป็นตัวเลขเท่านั้น!');", True)
            txtIDC_NOC.Focus()
        ElseIf IsNumeric(txtIAD_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAD เป็นตัวเลขเท่านั้น!');", True)
            txtIAD_NOC.Focus()
        ElseIf IsNumeric(txtIAP_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAP เป็นตัวเลขเท่านั้น!');", True)
            txtIAP_NOC.Focus()
        ElseIf IsNumeric(txtOFC_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ OFC เป็นตัวเลขเท่านั้น!');", True)
            txtOFC_NOC.Focus()
        ElseIf IsNumeric(txtSIT_NOC.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ SIT เป็นตัวเลขเท่านั้น!');", True)
            txtSIT_NOC.Focus()
        ElseIf IsNumeric(txtDomestic.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Domestic เป็นตัวเลขเท่านั้น!');", True)
            txtDomestic.Focus()
        ElseIf IsNumeric(txtInternational.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
            txtInternational.Focus()
        ElseIf IsNumeric(txtIPVEthernet.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV Ethernet เป็นตัวเลขเท่านั้น!');", True)
            txtIPVEthernet.Focus()
        ElseIf IsNumeric(txtUtilization.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Utilization เป็นตัวเลขเท่านั้น!');", True)
            txtUtilization.Focus()
        ElseIf IsNumeric(txtDirectTraffic.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Direct Traffic เป็นตัวเลขเท่านั้น!');", True)
            txtDirectTraffic.Focus()
        ElseIf IsNumeric(txtDomCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Domestic Cost เป็นตัวเลขเท่านั้น!');", True)
            txtDomCost.Focus()
        ElseIf IsNumeric(txtAllInterCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ All International Cost เป็นตัวเลขเท่านั้น!');", True)
            txtAllInterCost.Focus()
        ElseIf IsNumeric(txtTransitCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Transit Cost เป็นตัวเลขเท่านั้น!');", True)
            txtTransitCost.Focus()
        ElseIf IsNumeric(txtNetworkCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Network Cost เป็นตัวเลขเท่านั้น!');", True)
            txtNetworkCost.Focus()
        ElseIf IsNumeric(txtNetworkPortCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Network-Port Cost เป็นตัวเลขเท่านั้น!');", True)
            txtNetworkPortCost.Focus()
        ElseIf IsNumeric(txtNOCCost.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ NOC เป็นตัวเลขเท่านั้น!');", True)
            txtNOCCost.Focus()
        Else
            Try
                strSql = "select * from List_Service where Document_No = '" + xRequest_id + "' "
                DT = C.GetDataTable(strSql)
                txtINL.Text = Math.Round(CDbl(txtINL.Text), 0, MidpointRounding.AwayFromZero)
                txtIPV.Text = Math.Round(CDbl(txtIPV.Text), 0, MidpointRounding.AwayFromZero)
                txtINLECO.Text = Math.Round(CDbl(txtINLECO.Text), 0, MidpointRounding.AwayFromZero)
                txtIPVECO.Text = Math.Round(CDbl(txtIPVECO.Text), 0, MidpointRounding.AwayFromZero)
                txtINP.Text = Math.Round(CDbl(txtINP.Text), 0, MidpointRounding.AwayFromZero)
                txtINLIPVoNet.Text = Math.Round(CDbl(txtINLIPVoNet.Text), 0, MidpointRounding.AwayFromZero)
                txtINF.Text = Math.Round(CDbl(txtINF.Text), 0, MidpointRounding.AwayFromZero)
                txtIDC.Text = Math.Round(CDbl(txtIDC.Text), 0, MidpointRounding.AwayFromZero)
                txtIAD.Text = Math.Round(CDbl(txtIAD.Text), 0, MidpointRounding.AwayFromZero)
                txtIAP.Text = Math.Round(CDbl(txtIAP.Text), 0, MidpointRounding.AwayFromZero)
                txtOFC.Text = Math.Round(CDbl(txtOFC.Text), 0, MidpointRounding.AwayFromZero)
                txtSIT.Text = Math.Round(CDbl(txtSIT.Text), 0, MidpointRounding.AwayFromZero)
                txtINL_NOC.Text = Math.Round(CDbl(txtINL_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtIPV_NOC.Text = Math.Round(CDbl(txtIPV_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtIPVECO_NOC.Text = Math.Round(CDbl(txtIPVECO_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtINLECO_NOC.Text = Math.Round(CDbl(txtINLECO_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtINP_NOC.Text = Math.Round(CDbl(txtINP_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtINLIPVoNet_NOC.Text = Math.Round(CDbl(txtINLIPVoNet_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtINF_NOC.Text = Math.Round(CDbl(txtINF_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtIDC_NOC.Text = Math.Round(CDbl(txtIDC_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtIAD_NOC.Text = Math.Round(CDbl(txtIAD_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtIAP_NOC.Text = Math.Round(CDbl(txtIAP_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtOFC_NOC.Text = Math.Round(CDbl(txtOFC_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtSIT_NOC.Text = Math.Round(CDbl(txtSIT_NOC.Text), 0, MidpointRounding.AwayFromZero)
                txtDomestic.Text = Math.Round(CDbl(txtDomestic.Text), 0, MidpointRounding.AwayFromZero)
                txtInternational.Text = Math.Round(CDbl(txtInternational.Text), 0, MidpointRounding.AwayFromZero)
                txtIPVEthernet.Text = Math.Round(CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero)
                If DT.Rows.Count > 0 Then
                    strSql = "Update List_Service set INL = '" & CDbl(txtINL.Text) & "', IPV = '" & CDbl(txtIPV.Text) & "', INLECO = '" & CDbl(txtINLECO.Text) & "', IPVECO = '" & CDbl(txtIPVECO.Text) & "', INP = '" & CDbl(txtINP.Text) & "', INLIPVoNet = '" & CDbl(txtINLIPVoNet.Text) & "', INF = '" & CDbl(txtINF.Text) & "', IDC = '" & CDbl(txtIDC.Text) & "', IAD = '" & CDbl(txtIAD.Text) & "', IAP = '" & CDbl(txtIAP.Text) & "', OFC = '" & CDbl(txtOFC.Text) & "', SIT = '" & CDbl(txtSIT.Text) & "', TotalService = '" & CDbl(lblTotalService.Text) & "', INL_NOC = '" & CDbl(txtINL_NOC.Text) & "', IPV_NOC = '" & CDbl(txtIPV_NOC.Text) & "', INLECO_NOC = '" & CDbl(txtINLECO_NOC.Text) & "', IPVECO_NOC = '" & CDbl(txtIPVECO_NOC.Text) & "', INP_NOC = '" & CDbl(txtINP_NOC.Text) & "', INLIPVoNet_NOC = '" & CDbl(txtINLIPVoNet_NOC.Text) & "', INF_NOC = '" & CDbl(txtINF_NOC.Text) & "', IDC_NOC = '" & CDbl(txtIDC_NOC.Text) & "', IAD_NOC = '" & CDbl(txtIAD_NOC.Text) & "', IAP_NOC = '" & CDbl(txtIAP_NOC.Text) & "', OFC_NOC = '" & CDbl(txtOFC_NOC.Text) & "', SIT_NOC = '" & CDbl(txtSIT_NOC.Text) & "', NOC = '" & CDbl(lblNOC.Text) & "', Domestic = '" & CDbl(txtDomestic.Text) & "', INLUtilization = '" & CDbl(lblINLUtilization.Text) & "', International = '" & CDbl(txtInternational.Text) & "', InterUtilization = '" & CDbl(lblInterUtilization.Text) & "', InterDirect = '" & CDbl(lblInterDirect.Text) & "', Caching = '" & CDbl(lblCachingValue.Text) & "', DirectTraffic = '" & CDbl(lblDirectTrafficValue.Text) & "', EthernetIPV = '" & CDbl(txtIPVEthernet.Text) & "', IPVUtilization = '" & CDbl(lblIPVUtilization.Text) & "', NetworkPort = '" & CDbl(lblNetworkPort.Text) & "', EthernetNetwork = '" & CDbl(lblNetworkEthernet.Text) & "', NetworkUtilization = '" & CDbl(lblNetworkUtilization.Text) & "', DomesticCost = '" & CDbl(txtDomCost.Text) & "', AllInternationalCost = '" & CDbl(txtAllInterCost.Text) & "', TransitCost = '" & CDbl(txtTransitCost.Text) & "', NetworkCost = '" & CDbl(txtNetworkCost.Text) & "', NetworkPortCost = '" & CDbl(txtNetworkPortCost.Text) & "', NOCCost = '" & CDbl(txtNOCCost.Text) & "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "'  "
                    C.ExecuteNonQuery(strSql)

                    strSql = "Update FeasibilityDocument set UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' "
                    C.ExecuteNonQuery(strSql)
                End If
                If SaveOrNext = "Next" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'edit_capex.aspx?request_id=" & xRequest_id & "'; });", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ');", True)
                End If
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('" & Replace(ex.Message, "'", "") & "');", True)
            End Try
        End If
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim DT As DataTable
    '    Dim DT_List As DataTable
    '    Dim strSql As String
    '    Dim r As String = ""
    '    If IsNumeric(txtContract.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุอายุสัญญา Contract เป็นตัวเลขเท่านั้น!');", True)
    '        txtContract.Focus()
    '    ElseIf CInt(txtContract.Text) < 0 Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุอายุสัญญา Contract ให้มากกว่า 0');", True)
    '        txtContract.Focus()
    '    ElseIf IsNumeric(txtDom.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Domestic เป็นตัวเลขเท่านั้น!');", True)
    '        txtDom.Focus()
    '    ElseIf IsNumeric(txtInter.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
    '        txtInter.Focus()
    '    ElseIf IsNumeric(txtTransit.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Transit เป็นตัวเลขเท่านั้น!');", True)
    '        txtTransit.Focus()
    '    ElseIf IsNumeric(txtTotalINL.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Total INL Curcuits เป็นตัวเลขเท่านั้น!');", True)
    '        txtTotalINL.Focus()
    '    ElseIf IsNumeric(txtEthernetIPV.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Ethernet เป็นตัวเลขเท่านั้น!');", True)
    '        txtEthernetIPV.Focus()
    '    ElseIf IsNumeric(txtTotalIPV.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Total IPV Curcuits เป็นตัวเลขเท่านั้น!');", True)
    '        txtTotalIPV.Focus()
    '    ElseIf IsNumeric(txtEthernetINP.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Ethernet เป็นตัวเลขเท่านั้น!');", True)
    '        txtEthernetINP.Focus()
    '    ElseIf IsNumeric(txtTotalINP.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Total INP Curcuits เป็นตัวเลขเท่านั้น!');", True)
    '        txtTotalINP.Focus()
    '    ElseIf IsNumeric(txtMonthly.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Monthly (รายได้) เป็นตัวเลขเท่านั้น!');", True)
    '        txtMonthly.Focus()
    '    ElseIf IsNumeric(txtOneTime.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
    '        txtOneTime.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtDomestic_Price.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Domestic Price เป็นตัวเลขเท่านั้น!');", True)
    '        txtDomestic_Price.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtAll_International_Price.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ All International Price เป็นตัวเลขเท่านั้น!');", True)
    '        txtAll_International_Price.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtTransit_Price.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Transit Price เป็นตัวเลขเท่านั้น!');", True)
    '        txtTransit_Price.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtNetwork_Price.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Network Price เป็นตัวเลขเท่านั้น!');", True)
    '        txtNetwork_Price.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtNOC_Price.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ NOC Price เป็นตัวเลขเท่านั้น!');", True)
    '        txtNOC_Price.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtDom_Discount.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Dom.Utilization เป็นตัวเลขเท่านั้น!');", True)
    '        txtDom_Discount.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtInter_Discount.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Inter.Utilization เป็นตัวเลขเท่านั้น!');", True)
    '        txtInter_Discount.Focus()
    '    ElseIf ddlDiscount.SelectedValue = "Custom" And IsNumeric(txtIPV_Discount.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV.Utilization เป็นตัวเลขเท่านั้น!');", True)
    '        txtIPV_Discount.Focus()
    '    Else
    '        Try
    '            strSql = "select * from List_Model where Document_No = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "' "
    '            DT = C.GetDataTable(strSql)
    '            txtContract.Text = Math.Round(CDbl(txtContract.Text), 0)
    '            txtDom.Text = Math.Round(CDbl(txtDom.Text), 0)
    '            txtInter.Text = Math.Round(CDbl(txtInter.Text), 0)
    '            txtTransit.Text = Math.Round(CDbl(txtTransit.Text), 0)
    '            txtTotalINL.Text = Math.Round(CDbl(txtTotalINL.Text), 0)
    '            txtEthernetIPV.Text = Math.Round(CDbl(txtEthernetIPV.Text), 0)
    '            txtTotalIPV.Text = Math.Round(CDbl(txtTotalIPV.Text), 0)
    '            txtEthernetINP.Text = Math.Round(CDbl(txtEthernetINP.Text), 0)
    '            txtTotalINP.Text = Math.Round(CDbl(txtTotalINP.Text), 0)
    '            If DT.Rows.Count > 0 Then
    '                If ddlDiscount.SelectedValue = "Custom" Then
    '                    txtDom_Discount.Text = Math.Round(CDbl(txtDom_Discount.Text), 0)
    '                    txtInter_Discount.Text = Math.Round(CDbl(txtInter_Discount.Text), 0)
    '                    txtIPV_Discount.Text = Math.Round(CDbl(txtIPV_Discount.Text), 0)

    '                    strSql = "Update List_Model set Contract='" + txtContract.Text + "', Discount='" + ddlDiscount.SelectedValue + "', Domestic = '" + txtDom.Text + "', DomUtilization = '" + lblDom.Text + "', International = '" + txtInter.Text + "', InterUtilization = '" + lblInterUtilization.Text + "', Transit = '" + txtTransit.Text + "', TotalINLCurcuits = '" + txtTotalINL.Text + "', EthernetIPV = '" + txtEthernetIPV.Text + "', IPVUtilization = '" + lblIPVUtilization.Text + "', TotalIPVCurcuits = '" + txtTotalIPV.Text + "', EthernetINP = '" + txtEthernetINP.Text + "', ServicePrice = '" + Format(CDbl(txtPriceINP.Text), "#0.00") + "', TotalINPCurcuits = '" + txtTotalINP.Text + "', Monthly = '" + Format(CDbl(txtMonthly.Text), "#0.00") + "', OneTimePayment = '" + Format(CDbl(txtOneTime.Text), "#0.00") + "', Domestic_Price = '" + Format(CDbl(txtDomestic_Price.Text), "#0.00") + "', All_International_Price = '" + Format(CDbl(txtAll_International_Price.Text), "#0.00") + "', Transit_Price = '" + Format(CDbl(txtTransit_Price.Text), "#0.00") + "', Network_Price = '" + Format(CDbl(txtNetwork_Price.Text), "#0.00") + "', NOC_Price = '" + Format(CDbl(txtNOC_Price.Text), "#0.00") + "', Dom_Discount = '" + txtDom_Discount.Text + "', Inter_Discount = '" + txtInter_Discount.Text + "', IPV_Discount = '" + txtIPV_Discount.Text + "' where Document_No  = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "'  "
    '                    'ClientScript.RegisterStartupScript(Page.GetType, "Alert", "alert(""" + strSql + """);", True)
    '                    C.ExecuteNonQuery(strSql)
    '                Else
    '                    strSql = "Update List_Model set Contract='" + txtContract.Text + "', Discount='" + ddlDiscount.SelectedValue + "', Domestic = '" + txtDom.Text + "', DomUtilization = '" + lblDom.Text + "', International = '" + txtInter.Text + "', InterUtilization = '" + lblInterUtilization.Text + "', Transit = '" + txtTransit.Text + "', TotalINLCurcuits = '" + txtTotalINL.Text + "', EthernetIPV = '" + txtEthernetIPV.Text + "', IPVUtilization = '" + lblIPVUtilization.Text + "', TotalIPVCurcuits = '" + txtTotalIPV.Text + "', EthernetINP = '" + txtEthernetINP.Text + "', ServicePrice = '" + Format(CDbl(txtPriceINP.Text), "#0.00") + "', TotalINPCurcuits = '" + txtTotalINP.Text + "', Monthly = '" + Format(CDbl(txtMonthly.Text), "#0.00") + "', OneTimePayment = '" + Format(CDbl(txtOneTime.Text), "#0.00") + "', Domestic_Price = '" + Format(CDbl(Domestic_Price.Text), "#0.00") + "', All_International_Price = '" + Format(CDbl(All_International_Price.Text), "#0.00") + "', Transit_Price = '" + Format(CDbl(Transit_Price.Text), "#0.00") + "', Network_Price = '" + Format(CDbl(Network_Price.Text), "#0.00") + "', NOC_Price = '" + Format(CDbl(NOC_Price.Text), "#0.00") + "', Dom_Discount = '" + Dom_Discount.Text + "', Inter_Discount = '" + Inter_Discount.Text + "', IPV_Discount = '" + IPV_Discount.Text + "' where Document_No  = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "'  "

    '                    C.ExecuteNonQuery(strSql)
    '                End If
    '                strSql = "Update FeasibilityDocument set UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' "
    '                C.ExecuteNonQuery(strSql)
    '            End If
    '        Catch ex As Exception

    '        End Try

    '    End If
    '    'If IsNumeric(TextBox4.Text) = True Then
    '    '    DT = C.GetDataTable("select * from OPEX where OPEX_id='" + DropDownList1.SelectedValue + "'")

    '    '    If DT.Rows.Count > 0 Then
    '    '        strSql = "insert into List_OPEX (OPEX_Name, Number, Cost, CreateBy, CreateDate) values('" + DT.Rows(0).Item("OPEX_Name").ToString + "','" + TextBox4.Text + "','" + (CInt(DT.Rows(0).Item("OPEX_Cost")) * CInt(TextBox4.Text)).ToString + "','weraphon.r',getdate()) "
    '    '        C.ExecuteNonQuery(strSql)
    '    '        DT_List = C.GetDataTable("select * from List_OPEX where CreateBy = 'weraphon.r'")
    '    '        GridView1.DataSource = DT_List
    '    '        GridView1.DataBind()

    '    '    End If
    '    'Else
    '    '    ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุจำนวนเป็นตัวเลขเท่านั้น!');", True)
    '    '    TextBox4.Focus()
    '    'End If
    '    Response.Redirect("edit_capex.aspx?request_id=" + xRequest_id)
    'End Sub


    Protected Sub txtINL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINL.TextChanged
        If IsNumeric(txtINL.Text) Then
            txtINL_NOC.Text = txtINL.Text
            CalculateNOC()
        Else
            txtINL.Text = "0"
            txtINL_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL เป็นตัวเลขเท่านั้น!');", True)
            txtINL.Focus()
        End If

    End Sub

    Protected Sub txtIPV_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPV.TextChanged
        If IsNumeric(txtIPV.Text) Then
            txtIPV_NOC.Text = txtIPV.Text
            CalculateNOC()
        Else
            txtIPV.Text = "0"
            txtIPV_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV เป็นตัวเลขเท่านั้น!');", True)
            txtIPV.Focus()
        End If

    End Sub
    Protected Sub txtINLECO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINLECO.TextChanged
        If IsNumeric(txtINLECO.Text) Then
            txtINLECO_NOC.Text = txtINLECO.Text
            CalculateNOC()
        Else
            txtINLECO.Text = "0"
            txtINLECO_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtINLECO.Focus()
        End If

    End Sub
    Protected Sub txtIPVECO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPVECO.TextChanged
        If IsNumeric(txtIPVECO.Text) Then
            txtIPVECO_NOC.Text = txtIPVECO.Text
            CalculateNOC()
        Else
            txtIPVECO.Text = "0"
            txtIPVECO_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtIPVECO.Focus()
        End If

    End Sub

    Protected Sub txtINP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINP.TextChanged
        If IsNumeric(txtINP.Text) Then
            txtINP_NOC.Text = txtINP.Text
            CalculateNOC()
        Else
            txtINP.Text = "0"
            txtINP_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INP เป็นตัวเลขเท่านั้น!');", True)
            txtINP.Focus()
        End If

    End Sub
    Protected Sub txtINLIPVoNet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINLIPVoNet.TextChanged
        If IsNumeric(txtINLIPVoNet.Text) Then
            txtINLIPVoNet_NOC.Text = txtINLIPVoNet.Text
            CalculateNOC()
        Else
            txtINLIPVoNet.Text = "0"
            txtINLIPVoNet_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL/IPVoNet เป็นตัวเลขเท่านั้น!');", True)
            txtINLIPVoNet.Focus()
        End If
    End Sub
    Protected Sub txtINF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINF.TextChanged
        If IsNumeric(txtINF.Text) Then
            txtINF_NOC.Text = txtINF.Text
            CalculateNOC()
        Else
            txtINF.Text = "0"
            txtINF_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INF เป็นตัวเลขเท่านั้น!');", True)
            txtINF.Focus()
        End If
    End Sub

    Protected Sub txtIDC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIDC.TextChanged
        If IsNumeric(txtIDC.Text) Then
            txtIDC_NOC.Text = txtIDC.Text
            CalculateNOC()
        Else
            txtIDC.Text = "0"
            txtIDC_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IDC เป็นตัวเลขเท่านั้น!');", True)
            txtIDC.Focus()
        End If
    End Sub

    Protected Sub txtIAD_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIAD.TextChanged
        If IsNumeric(txtIAD.Text) Then
            txtIAD_NOC.Text = txtIAD.Text
            CalculateNOC()
        Else
            txtIAD.Text = "0"
            txtIAD_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAD เป็นตัวเลขเท่านั้น!');", True)
            txtIAD.Focus()
        End If
    End Sub

    Protected Sub txtIAP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIAP.TextChanged
        If IsNumeric(txtIAP.Text) Then
            txtIAP_NOC.Text = txtIAP.Text
            CalculateNOC()
        Else
            txtIAP.Text = "0"
            txtIAP_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAP เป็นตัวเลขเท่านั้น!');", True)
            txtIAP.Focus()
        End If
    End Sub

    Protected Sub txtOFC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOFC.TextChanged
        If IsNumeric(txtOFC.Text) Then
            txtOFC_NOC.Text = txtOFC.Text
            CalculateNOC()
        Else
            txtOFC.Text = "0"
            txtOFC_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ OFC เป็นตัวเลขเท่านั้น!');", True)
            txtOFC.Focus()
        End If
    End Sub

    Protected Sub txtSIT_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSIT.TextChanged
        If IsNumeric(txtSIT.Text) Then
            txtSIT_NOC.Text = txtSIT.Text
            CalculateNOC()
        Else
            txtSIT.Text = "0"
            txtSIT_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ SIT เป็นตัวเลขเท่านั้น!');", True)
            txtSIT.Focus()
        End If
    End Sub

    Protected Sub txtINL_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINL_NOC.TextChanged
        If IsNumeric(txtINL_NOC.Text) Then
            CalculateNOC()
        Else
            txtINL_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL เป็นตัวเลขเท่านั้น!');", True)
            txtINL_NOC.Focus()
        End If

    End Sub

    Protected Sub txtIPV_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPV_NOC.TextChanged
        If IsNumeric(txtIPV_NOC.Text) Then
            CalculateNOC()
        Else
            txtIPV_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV เป็นตัวเลขเท่านั้น!');", True)
            txtIPV_NOC.Focus()
        End If

    End Sub
    Protected Sub txtINLECO_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINLECO_NOC.TextChanged
        If IsNumeric(txtINLECO_NOC.Text) Then
            CalculateNOC()
        Else
            txtINLECO_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtINLECO_NOC.Focus()
        End If

    End Sub
    Protected Sub txtIPVECO_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPVECO_NOC.TextChanged
        If IsNumeric(txtIPVECO_NOC.Text) Then
            CalculateNOC()
        Else
            txtIPVECO_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IPV(ECO) เป็นตัวเลขเท่านั้น!');", True)
            txtIPVECO_NOC.Focus()
        End If

    End Sub

    Protected Sub txtINP_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINP_NOC.TextChanged
        If IsNumeric(txtINP_NOC.Text) Then
            CalculateNOC()
        Else
            txtINP_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INP เป็นตัวเลขเท่านั้น!');", True)
            txtINP_NOC.Focus()
        End If

    End Sub
    Protected Sub txtINLIPVoNet_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINLIPVoNet_NOC.TextChanged
        If IsNumeric(txtINLIPVoNet_NOC.Text) Then
            CalculateNOC()
        Else
            txtINLIPVoNet_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INL/IPVoNet เป็นตัวเลขเท่านั้น!');", True)
            txtINLIPVoNet_NOC.Focus()
        End If

    End Sub
    Protected Sub txtINF_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINF_NOC.TextChanged
        If IsNumeric(txtINF_NOC.Text) Then
            CalculateNOC()
        Else
            txtINF_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ INF เป็นตัวเลขเท่านั้น!');", True)
            txtINF_NOC.Focus()
        End If

    End Sub

    Protected Sub txtIDC_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIDC_NOC.TextChanged
        If IsNumeric(txtIDC_NOC.Text) Then
            CalculateNOC()
        Else
            txtIDC_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IDC เป็นตัวเลขเท่านั้น!');", True)
            txtIDC_NOC.Focus()
        End If

    End Sub

    Protected Sub txtIAD_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIAD_NOC.TextChanged
        If IsNumeric(txtIAD_NOC.Text) Then
            CalculateNOC()
        Else
            txtIAD_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAD เป็นตัวเลขเท่านั้น!');", True)
            txtIAD_NOC.Focus()
        End If

    End Sub

    Protected Sub txtIAP_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIAP_NOC.TextChanged
        If IsNumeric(txtIAP_NOC.Text) Then
            CalculateNOC()
        Else
            txtIAP_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ IAP เป็นตัวเลขเท่านั้น!');", True)
            txtIAP_NOC.Focus()
        End If

    End Sub

    Protected Sub txtOFC_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOFC_NOC.TextChanged
        If IsNumeric(txtOFC_NOC.Text) Then
            CalculateNOC()
        Else
            txtOFC_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ OFC เป็นตัวเลขเท่านั้น!');", True)
            txtOFC_NOC.Focus()
        End If

    End Sub

    Protected Sub txtSIT_NOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSIT_NOC.TextChanged
        If IsNumeric(txtSIT_NOC.Text) Then
            CalculateNOC()
        Else
            txtSIT_NOC.Text = "0"
            CalculateNOC()
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ SIT เป็นตัวเลขเท่านั้น!');", True)
            txtSIT_NOC.Focus()
        End If

    End Sub

    Public Sub CalculateNOC()
        lblTotalService.Text = CInt(txtINL.Text) + CInt(txtIPV.Text) + CInt(txtINLECO.Text) + CInt(txtIPVECO.Text) + CInt(txtINP.Text) + CInt(txtINLIPVoNet.Text) + CInt(txtINF.Text) + CInt(txtIDC.Text) + CInt(txtIAD.Text) + CInt(txtIAP.Text) + CInt(txtOFC.Text) + CInt(txtSIT.Text)
        lblNOC.Text = CInt(txtINL_NOC.Text) + CInt(txtIPV_NOC.Text) + CInt(txtINLECO_NOC.Text) + CInt(txtIPVECO_NOC.Text) + CInt(txtINP_NOC.Text) + CInt(txtINLIPVoNet_NOC.Text) + CInt(txtINF_NOC.Text) + CInt(txtIDC_NOC.Text) + CInt(txtIAD_NOC.Text) + CInt(txtIAP_NOC.Text) + CInt(txtOFC_NOC.Text) + CInt(txtSIT_NOC.Text)
        lblNetworkPort.Text = CInt(txtINL.Text) + CInt(txtIPV.Text)
    End Sub

    Protected Sub txtUtilization_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUtilization.TextChanged
        If IsNumeric(txtUtilization.Text) Then
            txtUtilization.Text = Math.Round(CDbl(txtUtilization.Text), 0)

            lblINLUtilization.Text = txtUtilization.Text
            lblInterUtilization.Text = txtUtilization.Text
            lblIPVUtilization.Text = txtUtilization.Text
            lblNetworkUtilization.Text = txtUtilization.Text
            CalculateBandwidth()
            txtUtilization.Focus()
        Else
            'lblDom.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Utilization เป็นตัวเลขเท่านั้น!');", True)
            txtUtilization.Focus()
        End If
    End Sub

    Protected Sub txtDirectTraffic_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDirectTraffic.TextChanged
        If IsNumeric(txtDirectTraffic.Text) Then
            txtDirectTraffic.Text = Math.Round(CDbl(txtDirectTraffic.Text), 0)

            lblInterDirect.Text = txtDirectTraffic.Text
            CalculateBandwidth()
            txtDirectTraffic.Focus()
        Else
            'lblDom.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Direct Traffic เป็นตัวเลขเท่านั้น!');", True)
            txtDirectTraffic.Focus()
        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Dim DT_Utilization As New DataTable
        DT_Utilization = C.GetDataTable("Select * from InternetBWCost where BWCostType = '" & lblCustomerType.Text & "'")
        If DT_Utilization.Rows.Count > 0 Then
            txtUtilization.Text = DT_Utilization.Rows(0).Item("Utilization")
            txtDirectTraffic.Text = DT_Utilization.Rows(0).Item("InterDirect")
            txtDomCost.Text = DT_Utilization.Rows(0).Item("DomesticCost")
            txtAllInterCost.Text = DT_Utilization.Rows(0).Item("AllInternationalCost")
            txtTransitCost.Text = DT_Utilization.Rows(0).Item("TransitCost")
            txtNetworkCost.Text = DT_Utilization.Rows(0).Item("NetworkCost")
            txtNetworkPortCost.Text = DT_Utilization.Rows(0).Item("NetworkPortCost")
            txtNOCCost.Text = DT_Utilization.Rows(0).Item("NOCCost")
        End If

        txtUtilization.Text = Math.Round(CDbl(txtUtilization.Text), 0)
        lblINLUtilization.Text = txtUtilization.Text
        lblInterUtilization.Text = txtUtilization.Text
        lblIPVUtilization.Text = txtUtilization.Text
        lblNetworkUtilization.Text = txtUtilization.Text

        txtDirectTraffic.Text = Math.Round(CDbl(txtDirectTraffic.Text), 0)
        lblInterDirect.Text = txtDirectTraffic.Text

        CalculateBandwidth()
        LinkButton1.Focus()
    End Sub
End Class
