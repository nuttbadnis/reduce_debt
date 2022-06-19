Imports System.Data
Partial Class add_service
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    'Dim DT_Discount As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim DT As DataTable
            Dim DT_CustomerType As DataTable
            Dim DT_List As DataTable
            Dim strSql As String
            strSql = "select cus.CustomerType_name, cus.Utilization, cus.Input, cus.Status from List_ProjectName pro inner join CustomerType cus on pro.Customer_Type = cus.CustomerType_name where pro.CreateBy='" + Session("uemail") + "'  and pro.Document_No is null and cus.Status = '1' "
            DT_CustomerType = C.GetDataTable(strSql)
            If DT_CustomerType.Rows.Count > 0 Then
                lblInputPrice.InnerText = DT_CustomerType.Rows(0).Item("Input").ToString
                lblUtilization.Text = DT_CustomerType.Rows(0).Item("Utilization").ToString
                lblCustomerType.Text = DT_CustomerType.Rows(0).Item("CustomerType_name").ToString
            Else
                lblInputPrice.InnerText = "-"
            End If

            strSql = "select * from List_Service where CreateBy='" + Session("uemail") + "' and Document_No is null "
            DT_List = C.GetDataTable(strSql)
            If DT_List.Rows.Count > 0 Then
                txtContract.Text = DT_List.Rows(0).Item("Contract").ToString
                txtInputPrice.Text = DT_List.Rows(0).Item("Monthly").ToString
                txtOneTimePayment.Text = DT_List.Rows(0).Item("OneTimePayment").ToString
                txtGift.Text = DT_List.Rows(0).Item("Gift").ToString
                txtPenalty.Text = DT_List.Rows(0).Item("Penalty").ToString
                txtINL.Text = DT_List.Rows(0).Item("INL").ToString
                txtIPV.Text = DT_List.Rows(0).Item("IPV").ToString
                txtINF.Text = DT_List.Rows(0).Item("INF").ToString
                txtDomestic.Text = DT_List.Rows(0).Item("Domestic").ToString
                txtInternational.Text = DT_List.Rows(0).Item("International").ToString
                lblDirectTraffic.Text = DT_List.Rows(0).Item("DirectTraffic").ToString
                txtIPVEthernet.Text = DT_List.Rows(0).Item("EthernetIPV").ToString
                lblNetworkEthernet.Text = Format(CInt(DT_List.Rows(0).Item("Domestic").ToString) + CInt(DT_List.Rows(0).Item("EthernetIPV").ToString), "###,##0")

                If txtContract.Text < 12 Then
                    lblINLUtilization.Text = "40"
                    lblIPVUtilization.Text = "40"
                Else
                    lblINLUtilization.Text = lblUtilization.Text
                    lblIPVUtilization.Text = lblUtilization.Text
                End If

                CalculateSellingPrice()
                CalculateOneTimePayment()

                'txtDom.Text = DT_List.Rows(0).Item("Domestic").ToString
                'txtInter.Text = DT_List.Rows(0).Item("International").ToString
                'txtTransit.Text = DT_List.Rows(0).Item("Transit").ToString
                'txtTotalINL.Text = DT_List.Rows(0).Item("TotalINLCurcuits").ToString
                'txtEthernetIPV.Text = DT_List.Rows(0).Item("EthernetIPV").ToString
                'txtTotalIPV.Text = DT_List.Rows(0).Item("TotalIPVCurcuits").ToString
                'txtEthernetINP.Text = DT_List.Rows(0).Item("EthernetINP").ToString
                'txtPriceINP.Text = DT_List.Rows(0).Item("ServicePrice").ToString
                'txtTotalINP.Text = DT_List.Rows(0).Item("TotalINPCurcuits").ToString
                'txtMonthly.Text = DT_List.Rows(0).Item("Monthly").ToString
                'lblTotalYealy.Text = Format(CDbl(txtMonthly.Text) * 12, "###,##0.00") '(CInt(txtMonthly.Text) * 12).ToString
                'txtOneTime.Text = DT_List.Rows(0).Item("OneTimePayment").ToString

                'ddlDiscount.SelectedValue = DT_List.Rows(0).Item("Discount")
            Else
                txtContract.Text = "12"
                txtInputPrice.Text = "0.00"
                txtOneTimePayment.Text = "0.00"
                txtGift.Text = "0.00"
                txtPenalty.Text = "0.00"
                txtINL.Text = "0"
                txtIPV.Text = "0"
                txtINF.Text = "0"
                txtDomestic.Text = "0"
                txtInternational.Text = "0"
                lblDirectTraffic.Text = "0"
                txtIPVEthernet.Text = "0"
                lblNetworkEthernet.Text = "0"

                lblINLUtilization.Text = lblUtilization.Text
                lblIPVUtilization.Text = lblUtilization.Text


                'txtContract.Text = "12"
                'txtDom.Text = "0"
                'txtInter.Text = "0"
                'txtTransit.Text = "0"
                'txtTotalINL.Text = "0"
                'txtEthernetIPV.Text = "0"
                'txtTotalIPV.Text = "0"
                'txtEthernetINP.Text = "0"
                'txtPriceINP.Text = "0"
                'txtTotalINP.Text = "0"
                'txtMonthly.Text = "0"
                'lblTotalYealy.Text = (CInt(txtMonthly.Text) * 12).ToString
                'txtOneTime.Text = "0"
            End If

        'strSql = "select * from InternetBWCost where BWCost_Type = 'Normal' order by 1"
        'DT = C.GetDataTable(strSql)
        'If DT.Rows.Count > 0 Then
        '    lblDom.Text = DT.Rows(0).Item("Dom_Discount").ToString
        '    lblInterUtilization.Text = DT.Rows(0).Item("Inter_Discount").ToString
        '    lblIPVUtilization.Text = DT.Rows(0).Item("IPV_Discount").ToString

        '    Domestic_Price.Text = DT.Rows(0).Item("Domestic").ToString
        '    All_International_Price.Text = DT.Rows(0).Item("All_International").ToString
        '    Transit_Price.Text = DT.Rows(0).Item("Transit").ToString
        '    Network_Price.Text = DT.Rows(0).Item("Network").ToString
        '    NOC_Price.Text = DT.Rows(0).Item("NOC").ToString
        '    Dom_Discount.Text = DT.Rows(0).Item("Dom_Discount").ToString
        '    Inter_Discount.Text = DT.Rows(0).Item("Inter_Discount").ToString
        '    IPV_Discount.Text = DT.Rows(0).Item("IPV_Discount").ToString
        'End If
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

    Protected Sub txtContract_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContract.TextChanged
        If IsNumeric(txtContract.Text) Then
            If txtContract.Text < 1 Then
                txtContract.Text = "12"
                lblINLUtilization.Text = lblUtilization.Text
                lblIPVUtilization.Text = lblUtilization.Text
                ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Contract Period ให้มากกว่า 0');", True)
                txtContract.Focus()
            ElseIf txtContract.Text < 12 Then
                lblINLUtilization.Text = "40"
                lblIPVUtilization.Text = "40"
            Else
                lblINLUtilization.Text = lblUtilization.Text
                lblIPVUtilization.Text = lblUtilization.Text
            End If

            If lblCustomerType.Text = "ลูกค้าราชการ" Then
                lblMonthlyPrice.Text = Format(Math.Round(((txtInputPrice.Text * 100) / 107) / txtContract.Text, 2), "###,##0.00")
            End If

        Else
            txtContract.Text = "12"
            lblINLUtilization.Text = lblUtilization.Text
            lblIPVUtilization.Text = lblUtilization.Text
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Contract Period เป็นตัวเลขเท่านั้น!');", True)
            txtContract.Focus()
        End If
    End Sub

    Protected Sub txtDomestic_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDomestic.TextChanged
        If IsNumeric(txtDomestic.Text) Then
            txtDomestic.Text = Math.Round(CDbl(txtDomestic.Text), 0)
            lblNetworkEthernet.Text = Format(Math.Round(CDbl(txtDomestic.Text), 0) + Math.Round(CDbl(txtIPVEthernet.Text), 0), "###,##0")
        Else
            txtDomestic.Text = "0"
            lblNetworkEthernet.Text = Format(Math.Round(CDbl(txtIPVEthernet.Text), 0), "###,##0")
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Domestic เป็นตัวเลขเท่านั้น!');", True)
            txtDomestic.Focus()
        End If
    End Sub


    Protected Sub txtIPVEthernet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIPVEthernet.TextChanged
        If IsNumeric(txtIPVEthernet.Text) Then
            txtIPVEthernet.Text = Math.Round(CDbl(txtIPVEthernet.Text), 0)
            lblNetworkEthernet.Text = Format(Math.Round(CDbl(txtDomestic.Text), 0) + Math.Round(CDbl(txtIPVEthernet.Text), 0), "###,##0")
        Else
            txtIPVEthernet.Text = "0"
            lblNetworkEthernet.Text = Format(Math.Round(CDbl(txtDomestic.Text), 0), "###,##0")
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Ethernet เป็นตัวเลขเท่านั้น!');", True)
            txtIPVEthernet.Focus()
        End If
    End Sub


    Protected Sub txtInternational_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInternational.TextChanged
        If IsNumeric(txtInternational.Text) Then
            txtInternational.Text = Math.Round(CDbl(txtInternational.Text), 0)
            lblDirectTraffic.Text = Math.Ceiling(CDbl(txtInternational.Text) * (CDbl(lblUtilization.Text) / 100) * 0.7)
        Else
            txtInternational.Text = "0"
            lblDirectTraffic.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
            txtInternational.Focus()
        End If
    End Sub

    Protected Sub txtInputPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInputPrice.TextChanged
        If IsNumeric(txtInputPrice.Text) Then
            CalculateSellingPrice()
        Else
            txtInputPrice.Text = "0.00"
            lblTotalYearly.Text = "0.00"
            lblMonthlyPrice.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ " + lblInputPrice.InnerText + " เป็นตัวเลขเท่านั้น!');", True)
            txtInputPrice.Focus()
        End If
    End Sub

    Protected Sub txtOneTimePayment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOneTimePayment.TextChanged
        If IsNumeric(txtOneTimePayment.Text) Then
            CalculateOneTimePayment()
        Else
            txtOneTimePayment.Text = "0.00"
            lblOneTimePayment.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
            txtOneTimePayment.Focus()
        End If
    End Sub

    Public Sub CalculateSellingPrice()
        txtInputPrice.Text = Math.Round(CDbl(txtInputPrice.Text), 2)
        If lblCustomerType.Text = "ลูกค้าทั่วไป" Then
            lblMonthlyPrice.Text = Format(CDbl(txtInputPrice.Text), "###,##0.00")
            lblTotalYearly.Text = Format(CDbl(txtInputPrice.Text * 12), "###,##0.00")
        ElseIf lblCustomerType.Text = "ลูกค้าราชการ" Then
            lblTotalYearly.Text = Format(Math.Round((txtInputPrice.Text * 100) / 107, 2), "###,##0.00")
            lblMonthlyPrice.Text = Format(Math.Round(((txtInputPrice.Text * 100) / 107) / txtContract.Text, 2), "###,##0.00")
        Else
            lblTotalYearly.Text = "0.00"
            lblMonthlyPrice.Text = "0.00"
        End If
    End Sub

    Public Sub CalculateOneTimePayment()
        txtOneTimePayment.Text = Math.Round(CDbl(txtOneTimePayment.Text), 2)
        If lblCustomerType.Text = "ลูกค้าทั่วไป" Then
            lblOneTimePayment.Text = Format(CDbl(txtOneTimePayment.Text), "###,##0.00")
        ElseIf lblCustomerType.Text = "ลูกค้าราชการ" Then
            lblOneTimePayment.Text = Format(Math.Round((txtOneTimePayment.Text * 100) / 107, 2), "###,##0.00")
        Else
            lblOneTimePayment.Text = "0.00"
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DT As DataTable
        Dim strSql As String
        Dim r As String = ""
        If IsNumeric(txtContract.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุอายุสัญญา Contract เป็นตัวเลขจำนวนเต็มเท่านั้น!');", True)
            txtContract.Focus()
        ElseIf CInt(txtContract.Text) < 1 Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุอายุสัญญา Contract ให้มากกว่า 0');", True)
            txtContract.Focus()
        ElseIf IsNumeric(txtInputPrice.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ " + lblInputPrice.InnerText.ToString + " เป็นตัวเลขเท่านั้น!');", True)
            txtInputPrice.Focus()
        ElseIf IsNumeric(txtOneTimePayment.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
            txtOneTimePayment.Focus()
        ElseIf IsNumeric(txtGift.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ ค่าของขวัญ เป็นตัวเลขเท่านั้น!');", True)
            txtGift.Focus()
        ElseIf IsNumeric(txtPenalty.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Pen. (ค่าปรับ) เป็นตัวเลขเท่านั้น!');", True)
            txtPenalty.Focus()
        ElseIf IsNumeric(txtINL.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ INL เป็นตัวเลขเท่านั้น!');", True)
            txtINL.Focus()
        ElseIf IsNumeric(txtIPV.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ IPV เป็นตัวเลขเท่านั้น!');", True)
            txtIPV.Focus()
        ElseIf IsNumeric(txtINF.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ INF เป็นตัวเลขเท่านั้น!');", True)
            txtINF.Focus()
        ElseIf IsNumeric(txtDomestic.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Domestic เป็นตัวเลขเท่านั้น!');", True)
            txtDomestic.Focus()
        ElseIf IsNumeric(txtInternational.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ International เป็นตัวเลขเท่านั้น!');", True)
            txtInternational.Focus()
        ElseIf IsNumeric(txtIPVEthernet.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ IPV Ethernet เป็นตัวเลขเท่านั้น!');", True)
            txtIPVEthernet.Focus()
        Else
            strSql = "select * from List_Service where CreateBy='" + Session("uemail") + "' and Document_No is null "
            DT = C.GetDataTable(strSql)
            txtContract.Text = Math.Round(CDbl(txtContract.Text), 0)
            txtINL.Text = Math.Round(CDbl(txtINL.Text), 0)
            txtIPV.Text = Math.Round(CDbl(txtIPV.Text), 0)
            txtINF.Text = Math.Round(CDbl(txtINF.Text), 0)
            txtDomestic.Text = Math.Round(CDbl(txtDomestic.Text), 0)
            txtInternational.Text = Math.Round(CDbl(txtInternational.Text), 0)
            txtIPVEthernet.Text = Math.Round(CDbl(txtIPVEthernet.Text), 0)
            If DT.Rows.Count > 0 Then
                strSql = "Update List_Service set Contract='" + txtContract.Text + "', Monthly='" + Format(CDbl(txtInputPrice.Text), "#0.00") + "', OneTimePayment = '" + Format(CDbl(txtOneTimePayment.Text), "#0.00") + "', Gift = '" + Format(CDbl(txtGift.Text), "#0.00") + "', Penalty = '" + Format(CDbl(txtPenalty.Text), "#0.00") + "', INL = '" + txtINL.Text + "', IPV = '" + txtIPV.Text + "', INF = '" + txtINF.Text + "', Domestic = '" + txtDomestic.Text + "', INLUtilization = '" + lblINLUtilization.Text + "', International = '" + txtInternational.Text + "', DirectTraffic = '" + lblDirectTraffic.Text + "', EthernetIPV = '" + txtIPVEthernet.Text + "', IPVUtilization = '" + lblIPVUtilization.Text + "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() where CreateBy='" + Session("uemail") + "' and Document_No is null  "
                C.ExecuteNonQuery(strSql)
            Else
                strSql = "insert into List_Service(Contract,Monthly,OneTimePayment,Gift,Penalty,INL,IPV,INF,Domestic,INLUtilization,International,DirectTraffic,EthernetIPV,IPVUtilization,CreateBy,CreateDate) values('" + txtContract.Text + "','" + Format(CDbl(txtInputPrice.Text), "#0.00") + "','" + Format(CDbl(txtOneTimePayment.Text), "#0.00") + "','" + Format(CDbl(txtGift.Text), "#0.00") + "','" + Format(CDbl(txtPenalty.Text), "#0.00") + "','" + txtINL.Text + "','" + txtIPV.Text + "','" + txtINF.Text + "','" + txtDomestic.Text + "','" + lblINLUtilization.Text + "','" + txtInternational.Text + "','" + lblDirectTraffic.Text + "','" + txtIPVEthernet.Text + "','" + lblIPVUtilization.Text + "','" + Session("uemail") + "',getdate()) "
                C.ExecuteNonQuery(strSql)
            End If
            Response.Redirect("add_capex.aspx")
        End If

    End Sub
End Class
