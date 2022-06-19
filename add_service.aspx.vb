Imports System.Data
Partial Class add_service
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    'Dim DT_Discount As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        RowCost.Visible = False
        If Not Page.IsPostBack Then
            Dim DT As DataTable
            Dim DT_CustomerType As DataTable
            Dim DT_List As DataTable
            Dim DT_Utilization As New DataTable
            Dim strSql As String
            strSql = "select cus.CustomerType_name, cus.Utilization, cus.Input, cus.Status, cus.BWCostType from List_ProjectName pro inner join CustomerType cus on pro.Customer_Type = cus.CustomerType_name where pro.CreateBy='" + Session("uemail") + "'  and pro.Document_No is null and cus.Status = '1' "
            DT_CustomerType = C.GetDataTable(strSql)
            If DT_CustomerType.Rows.Count > 0 Then
                'lblInputPrice.InnerText = DT_CustomerType.Rows(0).Item("Input").ToString

                lblCustomerType.Text = DT_CustomerType.Rows(0).Item("BWCostType").ToString
            Else
                'lblInputPrice.InnerText = "-"
                lblCustomerType.Text = "Normal"
            End If

            'DT_Utilization = C.GetDataTable("Select * from InternetBWCost where BWCostType = '" & DT_CustomerType.Rows(0).Item("BWCostType") & "'")
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
            Else
                txtUtilization.Text = "0"
                txtDirectTraffic.Text = "0"
                txtDomCost.Text = "0"
                txtAllInterCost.Text = "0"
                txtTransitCost.Text = "0"
                txtNetworkCost.Text = "0"
                txtNetworkPortCost.Text = "0"
                txtNOCCost.Text = "0"
            End If


            strSql = "select * from List_Service where CreateBy='" + Session("uemail") + "' and Document_No is null "
            DT_List = C.GetDataTable(strSql)
            If DT_List.Rows.Count > 0 Then
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

                txtDomestic.Text = DT_List.Rows(0).Item("Domestic").ToString
                txtInternational.Text = DT_List.Rows(0).Item("International").ToString
                lblDirectTraffic.Text = DT_List.Rows(0).Item("DirectTraffic").ToString
                txtIPVEthernet.Text = DT_List.Rows(0).Item("EthernetIPV").ToString
                'lblNetworkEthernet.Text = Format(CInt(DT_List.Rows(0).Item("Domestic").ToString) + CInt(DT_List.Rows(0).Item("EthernetIPV").ToString), "###,##0")
                lblNetworkEthernet.Text = Format(CInt(DT_List.Rows(0).Item("EthernetNetwork").ToString), "###,##0")
                lblNetworkPort.Text = Format(CInt(DT_List.Rows(0).Item("NetworkPort").ToString), "###,##0")
                'If txtContract.Text < 12 Then
                '    lblINLUtilization.Text = "40"
                '    lblIPVUtilization.Text = "40"
                'Else
                '    lblINLUtilization.Text = lblUtilization.Text
                '    lblIPVUtilization.Text = lblUtilization.Text
                'End If

                'lblCaching.Text = DT_List.Rows(0).Item("Caching").ToString
                'lblINLUtilization.Text = DT_List.Rows(0).Item("INLUtilization").ToString
                'lblInterUtilization.Text = DT_List.Rows(0).Item("InterUtilization").ToString
                'lblInterDirect.Text = DT_List.Rows(0).Item("InterDirect").ToString
                'lblIPVUtilization.Text = DT_List.Rows(0).Item("IPVUtilization").ToString
                'lblNetworkUtilization.Text = DT_List.Rows(0).Item("NetworkUtilization").ToString

                'CalculateOneTimePayment()
            Else

                'txtUtilization.Text = "0"
                'txtDirectTraffic.Text = "0"
                'txtDomCost.Text = "0"
                'txtAllInterCost.Text = "0"
                'txtTransitCost.Text = "0"
                'txtNetworkCost.Text = "0"
                'txtNOCCost.Text = "0"

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
                txtDomestic.Text = "0"
                txtInternational.Text = "0"
                lblDirectTraffic.Text = "0"
                txtIPVEthernet.Text = "0"
                lblNetworkEthernet.Text = "0"
                lblNetworkPort.Text = "0"
                lblCaching.Text = "0"

                'lblINLUtilization.Text = txtUtilization.Text
                'lblInterUtilization.Text = txtUtilization.Text
                'lblInterDirect.Text = txtDirectTraffic.Text
                'lblIPVUtilization.Text = txtUtilization.Text
                'lblNetworkUtilization.Text = txtUtilization.Text
            End If

            lblINLUtilization.Text = txtUtilization.Text
            lblInterUtilization.Text = txtUtilization.Text
            lblInterDirect.Text = txtDirectTraffic.Text
            lblIPVUtilization.Text = txtUtilization.Text
            lblNetworkUtilization.Text = txtUtilization.Text
            CalculateBandwidth()

        End If
    End Sub

    'Protected Sub ddlDiscount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDiscount.SelectedIndexChanged
    '    Dim DT As DataTable
    '    Dim strSql As String
    '    strSql = "select * from InternetBWCost where BWCost_Type = '" + ddlDiscount.SelectedValue + "' order by 1"
    '    DT = C.GetDataTable(strSql)
    '    If DT.Rows.Count > 0 Then
    '        lblDom.Text = DT.Rows(0).Item("Dom_Discount").ToString
    '        lblInterUtilization.Text = DT.Rows(0).Item("Inter_Discount").ToString
    '        lblIPVUtilization.Text = DT.Rows(0).Item("IPV_Discount").ToString
    '    End If
    'End Sub

    'Protected Sub txtMonthly_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonthly.TextChanged
    '    If IsNumeric(txtMonthly.Text) Then
    '        lblTotalYealy.Text = Format(CDbl(txtMonthly.Text) * 12, "###,##0.00")
    '        lblMonthly.Text = txtMonthly.Text
    '    Else
    '        'txtMonthly.Text = "0"
    '        lblTotalYealy.Text = "0"
    '        lblMonthly.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Monthly (รายได้)เป็นตัวเลขเท่านั้น!');", True)
    '        txtMonthly.Focus()
    '    End If

    'End Sub

    'Protected Sub txtInter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInter.TextChanged
    '    If IsNumeric(txtInter.Text) Then
    '        txtTransit.Text = Math.Round(CDbl(txtInter.Text) * 0.2, 0)
    '        'lblMonthly.Text = txtInter.Text
    '    Else
    '        txtTransit.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
    '        txtInter.Focus()
    '    End If

    'End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim DT As DataTable
    '    Dim DT_List As DataTable
    '    Dim strSql As String
    '    Dim r As String = ""
    '    If IsNumeric(txtContract.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุอายุสัญญา Contract เป็นตัวเลขจำนวนเต็มเท่านั้น!');", True)
    '        txtContract.Focus()
    '    ElseIf CInt(txtContract.Text) < 0 Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุอายุสัญญา Contract ให้มากกว่า 0');", True)
    '        txtContract.Focus()
    '    ElseIf IsNumeric(txtDom.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Domestic เป็นตัวเลขเท่านั้น!');", True)
    '        txtDom.Focus()
    '    ElseIf IsNumeric(txtInter.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
    '        txtInter.Focus()
    '    ElseIf IsNumeric(txtTransit.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Transit เป็นตัวเลขเท่านั้น!');", True)
    '        txtTransit.Focus()
    '    ElseIf IsNumeric(txtTotalINL.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Total INL Curcuits เป็นตัวเลขเท่านั้น!');", True)
    '        txtTotalINL.Focus()
    '    ElseIf IsNumeric(txtEthernetIPV.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Ethernet เป็นตัวเลขเท่านั้น!');", True)
    '        txtEthernetIPV.Focus()
    '    ElseIf IsNumeric(txtTotalIPV.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Total IPV Curcuits เป็นตัวเลขเท่านั้น!');", True)
    '        txtTotalIPV.Focus()
    '    ElseIf IsNumeric(txtEthernetINP.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Ethernet เป็นตัวเลขเท่านั้น!');", True)
    '        txtEthernetINP.Focus()
    '    ElseIf IsNumeric(txtPriceINP.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุราคา INP Service เป็นตัวเลขเท่านั้น!');", True)
    '        txtPriceINP.Focus()
    '    ElseIf IsNumeric(txtTotalINP.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Total INP Curcuits เป็นตัวเลขเท่านั้น!');", True)
    '        txtTotalINP.Focus()
    '    ElseIf IsNumeric(txtMonthly.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Monthly (รายได้) เป็นตัวเลขเท่านั้น!');", True)
    '        txtMonthly.Focus()
    '    ElseIf IsNumeric(txtOneTime.Text) = False Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
    '        txtOneTime.Focus()
    '    Else
    '        strSql = "select * from List_Model where CreateBy='" + Session("uemail") + "' and Document_No is null "
    '        DT = C.GetDataTable(strSql)
    '        txtContract.Text = Math.Round(CDbl(txtContract.Text), 0)
    '        txtDom.Text = Math.Round(CDbl(txtDom.Text), 0)
    '        txtInter.Text = Math.Round(CDbl(txtInter.Text), 0)
    '        txtTransit.Text = Math.Round(CDbl(txtTransit.Text), 0)
    '        txtTotalINL.Text = Math.Round(CDbl(txtTotalINL.Text), 0)
    '        txtEthernetIPV.Text = Math.Round(CDbl(txtEthernetIPV.Text), 0)
    '        txtTotalIPV.Text = Math.Round(CDbl(txtTotalIPV.Text), 0)
    '        txtEthernetINP.Text = Math.Round(CDbl(txtEthernetINP.Text), 0)
    '        txtTotalINP.Text = Math.Round(CDbl(txtTotalINP.Text), 0)
    '        If DT.Rows.Count > 0 Then
    '            strSql = "Update List_Model set Contract='" + txtContract.Text + "', Discount='" + Type_Discount.Text + "', Domestic = '" + txtDom.Text + "', DomUtilization = '" + lblDom.Text + "', International = '" + txtInter.Text + "', InterUtilization = '" + lblInterUtilization.Text + "', Transit = '" + txtTransit.Text + "', TotalINLCurcuits = '" + txtTotalINL.Text + "', EthernetIPV = '" + txtEthernetIPV.Text + "', IPVUtilization = '" + lblIPVUtilization.Text + "', TotalIPVCurcuits = '" + txtTotalIPV.Text + "', EthernetINP = '" + txtEthernetINP.Text + "', ServicePrice = '" + Format(CDbl(txtPriceINP.Text), "#0.00") + "', TotalINPCurcuits = '" + txtTotalINP.Text + "', Monthly = '" + Format(CDbl(txtMonthly.Text), "#0.00") + "', OneTimePayment = '" + Format(CDbl(txtOneTime.Text), "#0.00") + "' where CreateBy='" + Session("uemail") + "' and Document_No is null  "
    '            C.ExecuteNonQuery(strSql)
    '        Else
    '            strSql = "insert into List_Model(Contract,Discount,Domestic,DomUtilization,International,InterUtilization,Transit,TotalINLCurcuits,EthernetIPV,IPVUtilization,TotalIPVCurcuits,EthernetINP,ServicePrice,TotalINPCurcuits,Monthly,OneTimePayment,Domestic_Price,All_International_Price,Transit_Price,Network_Price,NOC_Price,Dom_Discount,Inter_Discount,IPV_Discount,CreateBy,CreateDate) values('" + txtContract.Text + "','" + Type_Discount.Text + "','" + txtDom.Text + "','" + lblDom.Text + "','" + txtInter.Text + "','" + lblInterUtilization.Text + "','" + txtTransit.Text + "','" + txtTotalINL.Text + "','" + txtEthernetIPV.Text + "','" + lblIPVUtilization.Text + "','" + txtTotalIPV.Text + "','" + txtEthernetINP.Text + "','" + Format(CDbl(txtPriceINP.Text), "#0.00") + "','" + txtTotalINP.Text + "','" + Format(CDbl(txtMonthly.Text), "#0.00") + "','" + Format(CDbl(txtOneTime.Text), "#0.00") + "','" + Domestic_Price.Text + "','" + All_International_Price.Text + "','" + Transit_Price.Text + "','" + Network_Price.Text + "','" + NOC_Price.Text + "','" + Dom_Discount.Text + "','" + Inter_Discount.Text + "','" + IPV_Discount.Text + "','" + Session("uemail") + "',getdate()) "
    '            C.ExecuteNonQuery(strSql)
    '        End If
    '        Response.Redirect("add_capex.aspx")
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
    '    '    ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุจำนวนเป็นตัวเลขเท่านั้น!');", True)
    '    '    TextBox4.Focus()
    '    'End If

    'End Sub

    'Protected Sub txtContract_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContract.TextChanged
    '    If IsNumeric(txtContract.Text) Then
    '        If txtContract.Text < 1 Then
    '            txtContract.Text = "12"
    '            lblINLUtilization.Text = lblUtilization.Text
    '            lblIPVUtilization.Text = lblUtilization.Text
    '            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Contract Period ให้มากกว่า 0');", True)
    '            txtContract.Focus()
    '        ElseIf txtContract.Text < 12 Then
    '            lblINLUtilization.Text = "40"
    '            lblIPVUtilization.Text = "40"
    '        Else
    '            lblINLUtilization.Text = lblUtilization.Text
    '            lblIPVUtilization.Text = lblUtilization.Text
    '        End If

    '        If lblCustomerType.Text = "ลูกค้าราชการ" Then
    '            lblMonthlyPrice.Text = Format(Math.Round(((txtInputPrice.Text * 100) / 107) / txtContract.Text, 2), "###,##0.00")
    '        End If

    '    Else
    '        txtContract.Text = "12"
    '        lblINLUtilization.Text = lblUtilization.Text
    '        lblIPVUtilization.Text = lblUtilization.Text
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Contract Period เป็นตัวเลขเท่านั้น!');", True)
    '        txtContract.Focus()
    '    End If
    'End Sub

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

    Public Sub CalculateBandwidth()
        lblDirectTraffic.Text = Math.Round(CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100), 0, MidpointRounding.AwayFromZero)
        lblDirectTrafficValue.Text = CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100)
        lblCaching.Text = Math.Round((CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100)) - (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100)), 0, MidpointRounding.AwayFromZero)
        lblCachingValue.Text = (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100)) - (CDbl(txtInternational.Text) * (CDbl(lblInterUtilization.Text) / 100) * (CDbl(lblInterDirect.Text) / 100))
        lblNetworkEthernet.Text = Format(Math.Round(Math.Max(CDbl(txtDomestic.Text), CDbl(txtInternational.Text)) + CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero), "###,##0")
    End Sub

    'Public Sub CalculateOneTimePayment()
    '    txtOneTimePayment.Text = Math.Round(CDbl(txtOneTimePayment.Text), 2)
    '    If lblCustomerType.Text = "ลูกค้าทั่วไป" Then
    '        lblOneTimePayment.Text = Format(CDbl(txtOneTimePayment.Text), "###,##0.00")
    '    ElseIf lblCustomerType.Text = "ลูกค้าราชการ" Then
    '        lblOneTimePayment.Text = Format(Math.Round((txtOneTimePayment.Text * 100) / 107, 2), "###,##0.00")
    '    Else
    '        lblOneTimePayment.Text = "0.00"
    '    End If
    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveData("Save")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        SaveData("Next")
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearData()
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
        Else
            strSql = "select * from List_Service where CreateBy='" + Session("uemail") + "' and Document_No is null "
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
            txtINP_NOC.Text = Math.Round(CDbl(txtINP_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtINF_NOC.Text = Math.Round(CDbl(txtINF_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtIDC_NOC.Text = Math.Round(CDbl(txtIDC_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtIAD_NOC.Text = Math.Round(CDbl(txtIAD_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtIAP_NOC.Text = Math.Round(CDbl(txtIAP_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtOFC_NOC.Text = Math.Round(CDbl(txtOFC_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtSIT_NOC.Text = Math.Round(CDbl(txtSIT_NOC.Text), 0, MidpointRounding.AwayFromZero)
            txtDomestic.Text = Math.Round(CDbl(txtDomestic.Text), 0, MidpointRounding.AwayFromZero)
            txtInternational.Text = Math.Round(CDbl(txtInternational.Text), 0, MidpointRounding.AwayFromZero)
            txtIPVEthernet.Text = Math.Round(CDbl(txtIPVEthernet.Text), 0, MidpointRounding.AwayFromZero)
            Try
                If DT.Rows.Count > 0 Then
                    strSql = "Update List_Service set INL = '" & CDbl(txtINL.Text) & "', IPV = '" & CDbl(txtIPV.Text) & "', INLECO = '" & CDbl(txtINLECO.Text) & "', IPVECO = '" & CDbl(txtIPVECO.Text) & "', INP = '" & CDbl(txtINP.Text) & "', INLIPVoNet = '" & CDbl(txtINLIPVoNet.Text) & "', INF = '" & CDbl(txtINF.Text) & "', IDC = '" & CDbl(txtIDC.Text) & "', IAD = '" & CDbl(txtIAD.Text) & "', IAP = '" & CDbl(txtIAP.Text) & "', OFC = '" & CDbl(txtOFC.Text) & "', SIT = '" & CDbl(txtSIT.Text) & "', TotalService = '" & CDbl(lblTotalService.Text) & "', INL_NOC = '" & CDbl(txtINL_NOC.Text) & "', IPV_NOC = '" & CDbl(txtIPV_NOC.Text) & "', INLECO_NOC = '" & CDbl(txtINLECO_NOC.Text) & "', IPVECO_NOC = '" & CDbl(txtIPVECO_NOC.Text) & "', INP_NOC = '" & CDbl(txtINP_NOC.Text) & "', INLIPVoNet_NOC = '" & CDbl(txtINLIPVoNet_NOC.Text) & "', INF_NOC = '" & CDbl(txtINF_NOC.Text) & "', IDC_NOC = '" & CDbl(txtIDC_NOC.Text) & "', IAD_NOC = '" & CDbl(txtIAD_NOC.Text) & "', IAP_NOC = '" & CDbl(txtIAP_NOC.Text) & "', OFC_NOC = '" & CDbl(txtOFC_NOC.Text) & "', SIT_NOC = '" & CDbl(txtSIT_NOC.Text) & "', NOC = '" & CDbl(lblNOC.Text) & "', Domestic = '" & CDbl(txtDomestic.Text) & "', INLUtilization = '" & CDbl(lblINLUtilization.Text) & "', International = '" & CDbl(txtInternational.Text) & "', InterUtilization = '" & CDbl(lblInterUtilization.Text) & "', InterDirect = '" & CDbl(lblInterDirect.Text) & "', Caching = '" & CDbl(lblCachingValue.Text) & "', DirectTraffic = '" & CDbl(lblDirectTrafficValue.Text) & "', EthernetIPV = '" & CDbl(txtIPVEthernet.Text) & "', IPVUtilization = '" & CDbl(lblIPVUtilization.Text) & "', NetworkPort = '" & CDbl(lblNetworkPort.Text) & "', EthernetNetwork = '" & CDbl(lblNetworkEthernet.Text) & "', NetworkUtilization = '" & CDbl(lblNetworkUtilization.Text) & "', DomesticCost = '" & CDbl(txtDomCost.Text) & "', AllInternationalCost = '" & CDbl(txtAllInterCost.Text) & "', TransitCost = '" & CDbl(txtTransitCost.Text) & "', NetworkCost = '" & CDbl(txtNetworkCost.Text) & "', NetworkPortCost = '" & CDbl(txtNetworkPortCost.Text) & "', NOCCost = '" & CDbl(txtNOCCost.Text) & "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() where CreateBy='" + Session("uemail") + "' and Document_No is null  "
                    C.ExecuteNonQuery(strSql)
                Else
                    strSql = "insert into List_Service(INL,IPV,INLECO,IPVECO,INP,INLIPVoNet,INF,IDC,IAD,IAP,OFC,SIT,TotalService,INL_NOC,IPV_NOC,INLECO_NOC,IPVECO_NOC,INP_NOC,INLIPVoNet_NOC,INF_NOC,IDC_NOC,IAD_NOC,IAP_NOC,OFC_NOC,SIT_NOC,NOC,Domestic,INLUtilization,International,InterUtilization,InterDirect,Caching,DirectTraffic,EthernetIPV,IPVUtilization,NetworkPort,EthernetNetwork,NetworkUtilization,DomesticCost,AllInternationalCost,TransitCost,NetworkCost,NetworkPortCost,NOCCost,CreateBy,CreateDate) values('" & CDbl(txtINL.Text) & "','" & CDbl(txtIPV.Text) & "','" & CDbl(txtINLECO.Text) & "','" & CDbl(txtIPVECO.Text) & "','" & CDbl(txtINP.Text) & "','" & CDbl(txtINLIPVoNet.Text) & "','" & CDbl(txtINF.Text) & "','" & CDbl(txtIDC.Text) & "','" & CDbl(txtIAD.Text) & "','" & CDbl(txtIAP.Text) & "','" & CDbl(txtOFC.Text) & "','" & CDbl(txtSIT.Text) & "','" & CDbl(lblTotalService.Text) & "','" & CDbl(txtINL_NOC.Text) & "','" & CDbl(txtIPV_NOC.Text) & "','" & CDbl(txtINLECO_NOC.Text) & "','" & CDbl(txtIPVECO_NOC.Text) & "','" & CDbl(txtINP_NOC.Text) & "','" & CDbl(txtINLIPVoNet_NOC.Text) & "','" & CDbl(txtINF_NOC.Text) & "','" & CDbl(txtIDC_NOC.Text) & "','" & txtIAD_NOC.Text & "','" & CDbl(txtIAP_NOC.Text) & "','" & CDbl(txtOFC_NOC.Text) & "','" & CDbl(txtSIT_NOC.Text) & "','" & CDbl(lblNOC.Text) & "','" & CDbl(txtDomestic.Text) & "','" & CDbl(lblINLUtilization.Text) & "','" & CDbl(txtInternational.Text) & "','" & CDbl(lblInterUtilization.Text) & "','" & CDbl(lblInterDirect.Text) & "','" & CDbl(lblCachingValue.Text) & "','" & CDbl(lblDirectTrafficValue.Text) & "','" & CDbl(txtIPVEthernet.Text) & "','" & CDbl(lblIPVUtilization.Text) & "','" & CDbl(lblNetworkPort.Text) & "','" & CDbl(lblNetworkEthernet.Text) & "','" & CDbl(lblNetworkUtilization.Text) & "','" & CDbl(txtDomCost.Text) & "','" & CDbl(txtAllInterCost.Text) & "','" & CDbl(txtTransitCost.Text) & "','" & CDbl(txtNetworkCost.Text) & "','" & CDbl(txtNetworkPortCost.Text) & "','" & CDbl(txtNOCCost.Text) & "','" + Session("uemail") + "',getdate()) "
                    C.ExecuteNonQuery(strSql)
                End If
                If SaveOrNext = "Next" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('บันทึกข้อมูลได้สำเร็จ',function(){ window.location = 'add_capex.aspx?menu=create'; });", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('บันทึกข้อมูลได้สำเร็จ');", True)
                End If
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('" & Replace(ex.Message, "'", "") & "');", True)
            End Try
        End If
    End Sub

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

    Public Sub ClearData()
        C.ExecuteNonQuery("delete from List_Service where CreateBy = '" & Session("uemail") & "' and Document_No is null")
        ClientScript.RegisterStartupScript(Page.GetType, "success_clear", "window.location = 'add_service.aspx?menu=create';", True)
    End Sub

End Class
