Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic
Partial Class Summary_Upload
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim Cal As New Cls_Calculate
    Dim PrepareEmail As String
    Dim xRequest_id

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim strSql As String
        xRequest_id = Request.QueryString("request_id")
        If Not Page.IsPostBack Then
            tr_gift.Visible = False
            Dim dt_check As DataTable
            dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where Document_No = '" + xRequest_id + "'")
            If dt_check.Rows.Count > 0 Then
                lblCreateOrEdit.Text = "edit"
                menu_create.Visible = False
                test.Visible = True
                menu_project_upload.HRef = "edit_project_name.aspx?request_id=" + xRequest_id
                LoadPageUpdate()
            Else
                lblCreateOrEdit.Text = "create"
                menu_edit.Visible = False
                test.Visible = False
                LoadPageCreate()
            End If

        End If
    End Sub

    Public Sub LoadPageCreate()
        Dim MoneyRoundup As String = "#####0"

        Dim DT_ProjectName As DataTable
        Dim DT_Service As New DataTable
        Dim DT_Summary As New DataTable

        DT_ProjectName = GetTableProjectName()
        DT_Service = GetTableService()
        DT_Summary = GetTableSummary()

        'Session("uemail") = "natnapich.t"

        If DT_ProjectName.Rows.Count > 0 Then
            If DT_ProjectName.Rows(0).Item("project_File") <> "" Then
                ShowProjectName(DT_ProjectName)

                btnSave.Visible = True
                btnSaveToEditProject.Visible = True
                btnSaveDraft.Visible = True
                btnCancelProject.Visible = True
                LinkFileDoc.Visible = True
                LinkProjectFile.Visible = True
            Else
                btnSave.Visible = False
                btnSaveToEditProject.Visible = False
                btnSaveDraft.Visible = False
                btnCancelProject.Visible = False
                LinkFileDoc.Visible = False
                LinkProjectFile.Visible = False
                ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡������ �˹�� Project Overview ��͹', function(){ window.location = 'project_name.aspx?menu=upload'; }); ", True)
            End If
        Else
            btnSave.Visible = False
            btnSaveToEditProject.Visible = False
            btnSaveDraft.Visible = False
            btnCancelProject.Visible = False
            LinkFileDoc.Visible = False
            LinkProjectFile.Visible = False
            ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡������ �˹�� Project Overview ��͹', function(){ window.location = 'project_name.aspx?menu=upload'; });", True)
        End If

        If DT_Summary.Rows.Count > 0 Then
            txtRevenue.Text = Format(CDbl(DT_Summary.Rows(0).Item("Revenue")), MoneyRoundup)
            lblRevenue_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("Revenue_Profit")), MoneyRoundup)
            txtMarketingCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost")), MoneyRoundup)
            lblMarketingCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost_Profit")), MoneyRoundup)
            txtMarketing.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingPrice")), MoneyRoundup)
            lblMarketing_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingPrice_Profit")), MoneyRoundup)
            txtEntertain.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice")), MoneyRoundup)
            lblEntertain_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice_Profit")), MoneyRoundup)
            'txtGift.Text = Format(CDbl(DT_Summary.Rows(0).Item("GiftPrice")), MoneyRoundup)
            'lblGift_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("GiftPrice_Profit")), MoneyRoundup)
            txtGift.Text = "0"
            lblGift_profit.Text = "0"
            txtInternetCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("InternetCost")), MoneyRoundup)
            txtNetworkCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("NetworkCost")), MoneyRoundup)
            txtNOCCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost")), MoneyRoundup)
            txtNOCCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost_Profit")), MoneyRoundup)
            txtJasmineGroupCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("JasmineGroupCost")), MoneyRoundup)
            txtOtherCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost")), MoneyRoundup)
            lblOtherCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost_Profit")), MoneyRoundup)
            txtPenaltyCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost")), MoneyRoundup)
            lblPenaltyCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost_Profit")), MoneyRoundup)
            txtCAPEX.Text = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX")), MoneyRoundup)
            lblCAPEX_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX_Profit")), MoneyRoundup)
            lblCashFlow.Text = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow")), MoneyRoundup)
            lblCashFlow_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow_Profit")), MoneyRoundup)
            If DT_Summary.Rows(0).Item("Payback") = "-1" Then
                txtPayBack.Text = "<=1"
            Else
                txtPayBack.Text = DT_Summary.Rows(0).Item("Payback")
            End If
            If DT_Summary.Rows(0).Item("Payback_Profit") = "-1" Then
                txtPayBack_Profit.Text = "<=1"
            Else
                txtPayBack_Profit.Text = DT_Summary.Rows(0).Item("Payback_Profit")
            End If
            txtMargin.Text = DT_Summary.Rows(0).Item("Margin")
            txtMargin_Profit.Text = DT_Summary.Rows(0).Item("Margin_Profit")
            txtNPV.Text = Format(CDbl(DT_Summary.Rows(0).Item("NPV")), MoneyRoundup)
            txtNPV_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("NPV_Profit")), MoneyRoundup)
        Else
            txtRevenue.Text = "0"
            lblRevenue_Profit.Text = "0"
            txtMarketingCost.Text = "0"
            lblMarketingCost_Profit.Text = "0"
            txtMarketing.Text = "0"
            lblMarketing_profit.Text = "0"
            txtEntertain.Text = "0"
            lblEntertain_profit.Text = "0"
            txtGift.Text = "0"
            lblGift_profit.Text = "0"
            txtInternetCost.Text = "0"
            txtNetworkCost.Text = "0"
            txtNOCCost.Text = "0"
            txtNOCCost_Profit.Text = "0"
            txtJasmineGroupCost.Text = "0"
            txtOtherCost.Text = "0"
            lblOtherCost_Profit.Text = "0"
            txtPenaltyCost.Text = "0"
            lblPenaltyCost_Profit.Text = "0"
            txtCAPEX.Text = "0"
            lblCAPEX_Profit.Text = "0"
            lblCashFlow.Text = "0"
            lblCashFlow_Profit.Text = "0"
            txtPayBack.Text = "0"
            txtPayBack_Profit.Text = "0"
            txtMargin.Text = "0"
            txtMargin_Profit.Text = "0"
            txtNPV.Text = "0"
            txtNPV_Profit.Text = "0"
        End If
        CalculatePrice()
    End Sub

    Public Sub LoadPageUpdate()
        Dim alert As String
        Dim dt_check As DataTable
        dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (/*request_status = 0 or request_status = 55 or*/ request_status = 110) and Document_No = '" + xRequest_id + "'")
        'btnSaveToEditProject.Visible = False
        If dt_check.Rows.Count > 0 Then
            'If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "0" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
            If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                lblRequestStatus.Text = dt_check.Rows(0).Item("request_status").ToString
                refno.Text = xRequest_id
                Dim MoneyRoundup As String = "#####0"

                Dim DT_ProjectName As DataTable
                Dim DT_Service As New DataTable
                Dim DT_Summary As New DataTable

                DT_ProjectName = GetTableEditProjectName()
                DT_Service = GetTableEditService()
                DT_Summary = GetTableEditSummary()

                If DT_ProjectName.Rows.Count > 0 Then
                    If DT_ProjectName.Rows(0).Item("project_File") <> "" Then
                        ShowProjectName(DT_ProjectName)

                        btnSave.Visible = True
                        btnSaveDraft.Visible = True
                        btnSaveToEditProject.Visible = True
                    Else
                        btnSave.Visible = False
                        btnSaveDraft.Visible = False
                        btnSaveToEditProject.Visible = False
                        ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡�������˹�� Edit Project Overview ��͹', function(){ window.location = 'edit_project_name.aspx?request_id=" & xRequest_id & "'; });", True)
                    End If
                Else
                    btnSave.Visible = False
                    btnSaveDraft.Visible = False
                    btnSaveToEditProject.Visible = False
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡�������˹�� Edit Project Overview ��͹', function(){ window.location = 'edit_project_name.aspx?request_id=" & xRequest_id & "'; });", True)
                End If

                If DT_Summary.Rows.Count > 0 Then
                    txtRevenue.Text = Format(CDbl(DT_Summary.Rows(0).Item("Revenue")), MoneyRoundup)
                    lblRevenue_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("Revenue_Profit")), MoneyRoundup)
                    txtMarketingCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost")), MoneyRoundup)
                    lblMarketingCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingCost_Profit")), MoneyRoundup)
                    txtMarketing.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingPrice")), MoneyRoundup)
                    lblMarketing_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("MarketingPrice_Profit")), MoneyRoundup)
                    txtEntertain.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice")), MoneyRoundup)
                    lblEntertain_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("EntertainPrice_Profit")), MoneyRoundup)
                    'txtGift.Text = Format(CDbl(DT_Summary.Rows(0).Item("GiftPrice")), MoneyRoundup)
                    'lblGift_profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("GiftPrice_Profit")), MoneyRoundup)
                    txtGift.Text = "0"
                    lblGift_profit.Text = "0"
                    txtInternetCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("InternetCost")), MoneyRoundup)
                    txtNetworkCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("NetworkCost")), MoneyRoundup)
                    txtNOCCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost")), MoneyRoundup)
                    txtNOCCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("NOCCost_Profit")), MoneyRoundup)
                    txtJasmineGroupCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("JasmineGroupCost")), MoneyRoundup)
                    txtOtherCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost")), MoneyRoundup)
                    lblOtherCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("OtherCost_Profit")), MoneyRoundup)
                    txtPenaltyCost.Text = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost")), MoneyRoundup)
                    lblPenaltyCost_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("PenaltyCost_Profit")), MoneyRoundup)
                    txtCAPEX.Text = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX")), MoneyRoundup)
                    lblCAPEX_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("CAPEX_Profit")), MoneyRoundup)
                    lblCashFlow.Text = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow")), MoneyRoundup)
                    lblCashFlow_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("CashFlow_Profit")), MoneyRoundup)
                    If DT_Summary.Rows(0).Item("Payback") = "-1" Then
                        txtPayBack.Text = "<=1"
                    Else
                        txtPayBack.Text = DT_Summary.Rows(0).Item("Payback")
                    End If
                    If DT_Summary.Rows(0).Item("Payback_Profit") = "-1" Then
                        txtPayBack_Profit.Text = "<=1"
                    Else
                        txtPayBack_Profit.Text = DT_Summary.Rows(0).Item("Payback_Profit")
                    End If
                    txtMargin.Text = DT_Summary.Rows(0).Item("Margin")
                    txtMargin_Profit.Text = DT_Summary.Rows(0).Item("Margin_Profit")
                    txtNPV.Text = Format(CDbl(DT_Summary.Rows(0).Item("NPV")), MoneyRoundup)
                    txtNPV_Profit.Text = Format(CDbl(DT_Summary.Rows(0).Item("NPV_Profit")), MoneyRoundup)
                Else
                    txtRevenue.Text = "0"
                    lblRevenue_Profit.Text = "0"
                    txtMarketingCost.Text = "0"
                    lblMarketingCost_Profit.Text = "0"
                    txtMarketing.Text = "0"
                    lblMarketing_profit.Text = "0"
                    txtEntertain.Text = "0"
                    lblEntertain_profit.Text = "0"
                    txtGift.Text = "0"
                    lblGift_profit.Text = "0"
                    txtInternetCost.Text = "0"
                    txtNetworkCost.Text = "0"
                    txtNOCCost.Text = "0"
                    txtJasmineGroupCost.Text = "0"
                    txtOtherCost.Text = "0"
                    lblOtherCost_Profit.Text = "0"
                    txtPenaltyCost.Text = "0"
                    lblPenaltyCost_Profit.Text = "0"
                    txtCAPEX.Text = "0"
                    lblCAPEX_Profit.Text = "0"
                    lblCashFlow.Text = "0"
                    lblCashFlow_Profit.Text = "0"
                    txtPayBack.Text = "0"
                    txtPayBack_Profit.Text = "0"
                    txtMargin.Text = "0"
                    txtMargin_Profit.Text = "0"
                    txtNPV.Text = "0"
                    txtNPV_Profit.Text = "0"
                End If
                CalculatePrice()

            Else
                btnSave.Visible = False
                btnSaveDraft.Visible = False
                btnSaveToEditProject.Visible = False
                test.Visible = False
                alert = "AlertNotification('User ������Է��� 㹡����䢢�������!',"
                alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                alert += "});"
                ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
            End If
        Else
            btnSave.Visible = False
            btnSaveDraft.Visible = False
            btnSaveToEditProject.Visible = False
            test.Visible = False
            alert = "AlertNotification('��ਤ " & xRequest_id & " ��������ʶҹ� �������ö��䢢�������!',"
            alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
            alert += "});"
            ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If lblCreateOrEdit.Text = "create" Then
            SaveCreateSummary("SubmitApprove")
        ElseIf lblCreateOrEdit.Text = "edit" Then
            SaveEditSummary("SubmitApprove")
        End If
    End Sub

    Protected Sub btnSaveToEditProject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveToEditProject.Click
        If lblCreateOrEdit.Text = "create" Then
            SaveCreateSummary("SaveProject")
        ElseIf lblCreateOrEdit.Text = "edit" Then
            SaveEditSummary("SaveProject")
        End If
    End Sub

    Public Sub SaveCreateSummary(ByVal Command As String)
        'Dim DT_CAPEX As New DataTable
        'Dim DT_OPEX As New DataTable
        'Dim DT_OTHER As New DataTable
        'Dim DT_Service As New DataTable
        Dim DT_ProjectName As New DataTable
        Dim DT_ProjectName_File As New DataTable
        Dim DT_Summary As New DataTable
        Dim strSql As String
        Dim DT As New DataTable
        'Dim TypeService As String

        DT_ProjectName = GetTableProjectName()
        DT_Summary = GetTableSummary()

        'strSql = "select * from List_ProjectName where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        'DT_ProjectName = C.GetDataTable(strSql)
        'strSql = "select * from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        'DT_CAPEX = C.GetDataTable(strSql)
        'strSql = "select * from List_OPEX where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        'DT_OPEX = C.GetDataTable(strSql)
        'strSql = "select * from List_OTHER where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        'DT_OTHER = C.GetDataTable(strSql)
        'strSql = "select * from List_Service where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
        'DT_Service = C.GetDataTable(strSql)

        If DT_ProjectName.Rows.Count <= 0 Then
            Dim pn_alert As String = "AlertNotification('��سҡ�͡������ Project Name',"
            pn_alert += "function(){ focus();window.location.href='project_name.aspx?menu=upload';"
            pn_alert += "});"

            ClientScript.RegisterStartupScript(Page.GetType, "alert_projectname", pn_alert, True)
            'ElseIf DT_Service.Rows.Count <= 0 Then
            '    Dim sv_alert as string = "AlertNotification('��سҡ�͡������ Service',"
            '    sv_alert += "function(){ focus();window.location.href='add_service.aspx?menu=create';" 
            '    sv_alert += "});"

            '    ClientScript.RegisterStartupScript(Page.GetType, "alert_servicename", sv_alert, True)
        Else
            If DT_ProjectName.Rows(0).Item("project_File") = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡������ �˹�� Project Overview ��͹', function(){ window.location = 'project_name.aspx?menu=upload'; });", True)
                Exit Sub
            End If
            strSql = "select convert(varchar(3),right(isnull(max(Document_No),'FES000000-00000'),3) + 1) 'Max_Document',  left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) 'YearMonth' " + vbCr
            strSql += "from FeasibilityDocument " + vbCr
            strSql += "where substring(Document_No,4,6) = left(CONVERT(varchar(10),getdate(),111),4) + substring(CONVERT(varchar(10),getdate(),111),6,2) "
            DT = C.GetDataTable(strSql)

            Dim doc_no As String = "FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("000")

            strSql = "Insert Into FeasibilityDocument (Document_No,Document_Date,Service_Date,Area,Cluster,Customer_Name,"
            strSql += "Type_Service,Detail_Service,CreateBy,CreateDate,Status,Upload_Project)"
            strSql += "values ('" + doc_no + "','" + C.CDateText(txtDocumentDate.Text) + "','" + C.CDateText(txtServiceDate.Text) + "',"
            strSql += "'" + lblArea.Text + "','" + lblCluster.Text + "','" & lblCustomerName.Text & "','" + lblTypeOfService.Text + "',"
            strSql += "'" + txtDetailService.Text + "','" + Session("uemail") + "',getdate(),'1','1') "
            C.ExecuteNonQuery(strSql)


            If DT_Summary.Rows.Count > 0 Then
                strSql = "Update List_Summary set Revenue = '" & CDbl(txtRevenue.Text) & "', Revenue_Profit = '" & CDbl(lblRevenue_Profit.Text) & "', " + vbCr
                strSql += "OPEX = '" & CDbl(lblOPEX.Text) & "', OPEX_Profit = '" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                strSql += "MarketingCost = '" & CDbl(txtMarketingCost.Text) & "', MarketingCost_Profit = '" & CDbl(lblMarketingCost_Profit.Text) & "', " + vbCr
                strSql += "MarketingPrice = '" & CDbl(txtMarketing.Text) & "',MarketingPrice_Profit = '" & CDbl(lblMarketing_profit.Text) & "', " + vbCr
                strSql += "EntertainPrice = '" & CDbl(txtEntertain.Text) & "', EntertainPrice_Profit = '" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                strSql += "GiftPrice = '" & CDbl(txtGift.Text) & "',GiftPrice_Profit = '" & CDbl(lblGift_profit.Text) & "', " + vbCr
                strSql += "InternetCost = '" & CDbl(txtInternetCost.Text) & "', NetworkCost = '" & CDbl(txtNetworkCost.Text) & "', " + vbCr
                strSql += "NOCCost = '" & CDbl(txtNOCCost.Text) & "', NOCCost_Profit = '" & CDbl(txtNOCCost_Profit.Text) & "', JasmineGroupCost = '" & CDbl(txtJasmineGroupCost.Text) & "', " + vbCr
                strSql += "OtherCost = '" & CDbl(txtOtherCost.Text) & "', OtherCost_Profit = '" & CDbl(lblOtherCost_Profit.Text) & "', " + vbCr
                strSql += "PenaltyCost = '" & CDbl(txtPenaltyCost.Text) & "', PenaltyCost_Profit = '" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                strSql += "RevenueAfter = '" & CDbl(lblRevenue_Operation.Text) & "', RevenueAfter_Profit = '" & CDbl(lblRevenue_Operationtotal.Text) & "', " + vbCr
                strSql += "CAPEX = '" & CDbl(txtCAPEX.Text) & "', CAPEX_Profit = '" & CDbl(lblCAPEX_Profit.Text) & "', " + vbCr
                strSql += "CashFlow = '" & CDbl(lblCashFlow.Text) & "', CashFlow_Profit = '" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                If txtPayBack.Text = "<=1" Then
                    strSql += "Payback = '-1', " + vbCr
                Else
                    strSql += "Payback = '" & CDbl(txtPayBack.Text) & "', "
                End If
                If txtPayBack_Profit.Text = "<=1" Then
                    strSql += "Payback_Profit = '-1', " + vbCr
                Else
                    strSql += "Payback_Profit = '" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                End If
                strSql += "Margin = '" & CDbl(txtMargin.Text) & "', Margin_Profit = '" & CDbl(txtMargin_Profit.Text) & "', " + vbCr
                strSql += "NPV = '" & CDbl(txtNPV.Text) & "', NPV_Profit = '" & CDbl(txtNPV_Profit.Text) & "', " + vbCr
                strSql += "Status = '1', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate(), Document_No = '" & doc_no & "'  " + vbCr
                strSql += "where CreateBy='" + Session("uemail") + "' and Document_No is null "
                C.ExecuteNonQuery(strSql)
            Else
                strSql = "Insert Into List_Summary (Document_No,Revenue,Revenue_Profit,OPEX,OPEX_Profit, " + vbCr
                strSql += "MarketingPrice,MarketingPrice_Profit,EntertainPrice,EntertainPrice_Profit, " + vbCr
                strSql += "GiftPrice,GiftPrice_Profit, " + vbCr
                strSql += "MarketingCost,MarketingCost_Profit,InternetCost,NetworkCost,NOCCost,NOCCost_Profit, " + vbCr
                strSql += "JasmineGroupCost,OtherCost,OtherCost_Profit,PenaltyCost,PenaltyCost_Profit, " + vbCr
                strSql += "RevenueAfter,RevenueAfter_Profit,CAPEX,CAPEX_Profit,CashFlow,CashFlow_Profit, " + vbCr
                strSql += "Payback,Payback_Profit, " + vbCr
                strSql += "Margin,Margin_Profit,NPV,NPV_Profit,Status,CreateBy,CreateDate)" + vbCr
                strSql += "values ('" & doc_no & "','" & CDbl(txtRevenue.Text) & "','" & CDbl(lblRevenue_Profit.Text) & "','" & CDbl(lblOPEX.Text) & "','" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtMarketing.Text) & "','" & CDbl(lblMarketing_profit.Text) & "','" & CDbl(txtEntertain.Text) & "','" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtGift.Text) & "','" & CDbl(lblGift_profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtMarketingCost.Text) & "','" & CDbl(lblMarketingCost_Profit.Text) & "','" & CDbl(txtInternetCost.Text) & "','" & CDbl(txtNetworkCost.Text) & "','" & CDbl(txtNOCCost.Text) & "','" & CDbl(txtNOCCost_Profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtJasmineGroupCost.Text) & "','" & CDbl(txtOtherCost.Text) & "','" & CDbl(lblOtherCost_Profit.Text) & "','" & CDbl(txtPenaltyCost.Text) & "','" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(lblRevenue_Operation.Text) & "','" & CDbl(lblRevenue_Operationtotal.Text) & "','" & CDbl(txtCAPEX.Text) & "','" & CDbl(lblCAPEX_Profit.Text) & "','" & CDbl(lblCashFlow.Text) & "','" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                If txtPayBack.Text = "<=1" Then
                    strSql += "'-1', "
                Else
                    strSql += "'" & CDbl(txtPayBack.Text) & "', "
                End If
                If txtPayBack_Profit.Text = "<=1" Then
                    strSql += "'-1', " + vbCr
                Else
                    strSql += "'" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                End If
                strSql += "'" & CDbl(txtMargin.Text) & "','" & CDbl(txtMargin_Profit.Text) & "','" & CDbl(txtNPV.Text) & "','" & CDbl(txtNPV_Profit.Text) & "','1','" + Session("uemail") + "',getdate()) "
                C.ExecuteNonQuery(strSql)
            End If


            strSql = ""
            If DT_ProjectName.Rows.Count > 0 Then
                strSql += "Update dbo.List_ProjectName set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            End If
            'If DT_CAPEX.Rows.Count > 0 Then
            '    strSql += "Update dbo.List_CAPEX set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            'End If
            'If DT_OPEX.Rows.Count > 0 Then
            '    strSql += "Update dbo.List_OPEX set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            'End If
            'If DT_OTHER.Rows.Count > 0 Then
            '    strSql += "Update dbo.List_OTHER set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL " + vbCr
            'End If
            'If DT_Service.Rows.Count > 0 Then
            '    strSql += "Update dbo.List_Service set Document_No='" + doc_no + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where CreateBy = '" + Session("uemail") + "' and Document_No is NULL "
            'End If
            C.ExecuteNonQuery(strSql)

            Dim vTempPath As String = Server.MapPath("Upload\")
            Dim vUploadPath As String
            DT_ProjectName_File = C.GetDataTable("select isnull(Doc_File,'') 'Doc_File', isnull(Project_File,'') 'Project_File' from List_ProjectName where Document_No='" + doc_no + "' ")

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

            If DT_ProjectName_File.Rows(0).Item("Project_File") <> "" Then
                vUploadPath = "ProjectFile"
                vUploadPath &= "\" & DateTime.Now.ToString("yyyy")
                vUploadPath &= "\" & DateTime.Now.ToString("MM")
                If Not Directory.Exists(vTempPath & vUploadPath) Then
                    Directory.CreateDirectory(vTempPath & vUploadPath)
                End If
                vUploadPath &= "\"

                Dim Project_exe As String
                If Right(DT_ProjectName_File.Rows(0).Item("Project_File"), 4) = "xlsx" Then
                    Project_exe = ".xlsx"
                Else
                    Project_exe = ".xls"
                End If

                Dim doc_no_Doc As String = "Project_" + doc_no + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + Project_exe
                If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("Project_File")) Then
                    System.IO.File.Move(MapPath(".") & "\Upload\" & DT_ProjectName_File.Rows(0).Item("Project_File"), vTempPath & vUploadPath & doc_no_Doc)
                    C.ExecuteNonQuery("Update List_ProjectName set Project_File = '" + vUploadPath & doc_no_Doc + "' where Document_No='" + doc_no + "' and Status='1' ")
                End If
            End If

            Dim vSqlIn As String = ""
            Dim flow_id As String
            Dim purchase As Double = 0
            Dim tMargin As Double = 0
            Dim tPayback As Double = 0
            Dim tPenaltyLate As Double = 0
            Dim tRevenue As Double = 0

            If IsNumeric(Replace(txtCAPEX.Text, "   THB", "")) = True Then
                purchase = CDbl(Replace(txtCAPEX.Text, "   THB", ""))
            End If
            If IsNumeric(txtMargin.Text) = True Then
                tMargin = CDbl(txtMargin.Text)
            End If
            If IsNumeric(txtPayBack.Text) = True Then
                tPayback = CDbl(txtPayBack.Text)
            End If
            If IsNumeric(lblPenaltyLate.Text) = True Then
                tPenaltyLate = CDbl(lblPenaltyLate.Text)
            End If
            If IsNumeric(txtRevenue.Text) = True Then
                If CDbl(txtRevenue.Text) <= 0 Then
                    tRevenue = 1
                Else
                    tRevenue = CDbl(txtRevenue.Text)
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

                Dim sm_alert As String = "AlertSuccess('�ѹ�֡�����������͹��ѵ� �����',"
                sm_alert += "function(){ focus();window.location.href='check_status_list.aspx?menu=check';"
                sm_alert += "});"

                ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)
            Else
                Dim dt_flow As New DataTable
                dt_flow = C.GetDataTable("select * from dbo.request_flow where request_id = '" & doc_no & "' and disable='0' and flow_step = '1' ")
                If dt_flow.Rows.Count > 0 Then
                    vSqlIn = "Update request_flow set flow_status = '110' where request_id = '" & doc_no & "' and disable = '0' and flow_step = '1' " + vbCr
                    vSqlIn += "update FeasibilityDocument set "
                    vSqlIn += "request_status = 110 "
                    vSqlIn += ", last_update = getdate(), last_depart = '" & dt_flow.Rows(0).Item("depart_id") & "' "
                    vSqlIn += ", next_depart = '0' " '***** ������������ ����ӴѺ�Ѵ��� �����ҧ�Ӣ�
                    vSqlIn += "where Document_No = '" & doc_no & "' "
                    C.ExecuteNonQuery(vSqlIn)
                End If

                Dim sm_alert As String = "AlertSuccess('�ѹ�֡�ç��� �����',function(){ window.location = 'edit_list.aspx?menu=edit'; });"
                ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)
            End If

        End If
    End Sub

    Public Sub SaveEditSummary(ByVal Command As String)
        'Dim DT_CAPEX As New DataTable
        'Dim DT_CAPEX_Mass As New DataTable
        'Dim DT_OPEX As New DataTable
        'Dim DT_OTHER As New DataTable
        'Dim DT_Service As New DataTable
        Dim DT_ProjectName As New DataTable
        Dim DT_ProjectName_File As New DataTable
        Dim DT_Summary As New DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim TypeService As String
        Dim DT_flow_id As New DataTable

        DT_ProjectName = GetTableEditProjectName()
        DT_Summary = GetTableEditSummary()

        'strSql = "select * from List_ProjectName where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        'DT_ProjectName = C.GetDataTable(strSql)
        'strSql = "select * from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        'DT_CAPEX = C.GetDataTable(strSql)
        'strSql = "select * from List_CAPEX_Mass where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        'DT_CAPEX_Mass = C.GetDataTable(strSql)
        'strSql = "select * from List_OPEX where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        'DT_OPEX = C.GetDataTable(strSql)
        'strSql = "select * from List_OTHER where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' and Status = '1' "
        'DT_OTHER = C.GetDataTable(strSql)
        'strSql = "select * from List_Service where CreateBy = '" + Session("uemail") + "' and Document_No = '" + xRequest_id + "' "
        'DT_Service = C.GetDataTable(strSql)

        If DT_ProjectName.Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('��سҡ�͡������ Edit Project Name',function(){ focus();window.location.href='edit_project_name.aspx?request_id=" + xRequest_id + "'; });", True)
            'ElseIf DT_CAPEX.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('��سҡ�͡������ CAPEX');focus();window.location.href='edit_capex.aspx?request_id=" + xRequest_id + "';", True)
            'ElseIf DT_OPEX.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('��سҡ�͡������ OPEX');focus();window.location.href='edit_opex.aspx?request_id=" + xRequest_id + "';", True)
            'ElseIf DT_Service.Rows.Count <= 0 Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('��سҡ�͡������ Service');focus();window.location.href='edit_service.aspx?request_id=" + xRequest_id + "';", True)
            '    'ElseIf txtDocumentDate.Text = "" Or txtDocumentDate.Text.Length <> 10 Then
            '    '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('��س��к� �ѹ���Ѵ��/��Ѻ��ا');", True)
            '    '    txtDocumentDate.Focus()
            '    'ElseIf txtServiceDate.Text = "" Or txtServiceDate.Text.Length <> 10 Then
            '    '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('��س��к� �ѹ������������ԡ��');", True)
            '    '    txtServiceDate.Focus()
        Else
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
            'Dim doc_no As String = "FES" + DT.Rows(0).Item("YearMonth").ToString + "-" + CInt(DT.Rows(0).Item("Max_Document")).ToString("00000")

            If DT_ProjectName.Rows(0).Item("project_File") = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡�������˹�� Edit Project Overview ��͹', function(){ window.location = 'edit_project_name.aspx?request_id=" & xRequest_id & "'; });", True)
                Exit Sub
            End If
            


            If DT_Summary.Rows.Count > 0 Then
                strSql = "Update List_Summary set Revenue = '" & CDbl(txtRevenue.Text) & "', Revenue_Profit = '" & CDbl(lblRevenue_Profit.Text) & "', " + vbCr
                strSql += "OPEX = '" & CDbl(lblOPEX.Text) & "', OPEX_Profit = '" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                strSql += "MarketingPrice = '" & CDbl(txtMarketing.Text) & "',MarketingPrice_Profit = '" & CDbl(lblMarketing_profit.Text) & "', " + vbCr
                strSql += "EntertainPrice = '" & CDbl(txtEntertain.Text) & "', EntertainPrice_Profit = '" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                strSql += "GiftPrice = '" & CDbl(txtGift.Text) & "',GiftPrice_Profit = '" & CDbl(lblGift_profit.Text) & "', " + vbCr
                strSql += "MarketingCost = '" & CDbl(txtMarketingCost.Text) & "', MarketingCost_Profit = '" & CDbl(lblMarketingCost_Profit.Text) & "', " + vbCr
                strSql += "InternetCost = '" & CDbl(txtInternetCost.Text) & "', NetworkCost = '" & CDbl(txtNetworkCost.Text) & "', " + vbCr
                strSql += "NOCCost = '" & CDbl(txtNOCCost.Text) & "', NOCCost_Profit = '" & CDbl(txtNOCCost_Profit.Text) & "', JasmineGroupCost = '" & CDbl(txtJasmineGroupCost.Text) & "', " + vbCr
                strSql += "OtherCost = '" & CDbl(txtOtherCost.Text) & "', OtherCost_Profit = '" & CDbl(lblOtherCost_Profit.Text) & "', " + vbCr
                strSql += "PenaltyCost = '" & CDbl(txtPenaltyCost.Text) & "', PenaltyCost_Profit = '" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                strSql += "RevenueAfter = '" & CDbl(lblRevenue_Operation.Text) & "', RevenueAfter_Profit = '" & CDbl(lblRevenue_Operationtotal.Text) & "', " + vbCr
                strSql += "CAPEX = '" & CDbl(txtCAPEX.Text) & "', CAPEX_Profit = '" & CDbl(lblCAPEX_Profit.Text) & "', " + vbCr
                strSql += "CashFlow = '" & CDbl(lblCashFlow.Text) & "', CashFlow_Profit = '" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                If txtPayBack.Text = "<=1" Then
                    strSql += "Payback = '-1', " + vbCr
                Else
                    strSql += "Payback = '" & CDbl(txtPayBack.Text) & "', "
                End If
                If txtPayBack_Profit.Text = "<=1" Then
                    strSql += "Payback_Profit = '-1', " + vbCr
                Else
                    strSql += "Payback_Profit = '" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                End If
                strSql += "Margin = '" & CDbl(txtMargin.Text) & "', Margin_Profit = '" & CDbl(txtMargin_Profit.Text) & "', " + vbCr
                strSql += "NPV = '" & CDbl(txtNPV.Text) & "', NPV_Profit = '" & CDbl(txtNPV_Profit.Text) & "', " + vbCr
                strSql += "Status = '1', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate()  " + vbCr
                strSql += "where Document_No = '" + xRequest_id + "' "
                C.ExecuteNonQuery(strSql)
            Else
                strSql = "Insert Into List_Summary (Document_No,Revenue,Revenue_Profit,OPEX,OPEX_Profit, " + vbCr
                strSql += "MarketingPrice,MarketingPrice_Profit,EntertainPrice,EntertainPrice_Profit, " + vbCr
                strSql += "GiftPrice,GiftPrice_Profit, " + vbCr
                strSql += "MarketingCost,MarketingCost_Profit,InternetCost,NetworkCost,NOCCost,NOCCost_Profit, " + vbCr
                strSql += "JasmineGroupCost,OtherCost,OtherCost_Profit,PenaltyCost,PenaltyCost_Profit, " + vbCr
                strSql += "RevenueAfter,RevenueAfter_Profit,CAPEX,CAPEX_Profit,CashFlow,CashFlow_Profit, " + vbCr
                strSql += "Payback,Payback_Profit, " + vbCr
                strSql += "Margin,Margin_Profit,NPV,NPV_Profit,Status,CreateBy,CreateDate)" + vbCr
                strSql += "values ('" & xRequest_id & "','" & CDbl(txtRevenue.Text) & "','" & CDbl(lblRevenue_Profit.Text) & "','" & CDbl(lblOPEX.Text) & "','" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtMarketing.Text) & "','" & CDbl(lblMarketing_profit.Text) & "','" & CDbl(txtEntertain.Text) & "','" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtGift.Text) & "','" & CDbl(lblGift_profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtMarketingCost.Text) & "','" & CDbl(lblMarketingCost_Profit.Text) & "','" & CDbl(txtInternetCost.Text) & "','" & CDbl(txtNetworkCost.Text) & "','" & CDbl(txtNOCCost.Text) & "','" & CDbl(txtNOCCost_Profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(txtJasmineGroupCost.Text) & "','" & CDbl(txtOtherCost.Text) & "','" & CDbl(lblOtherCost_Profit.Text) & "','" & CDbl(txtPenaltyCost.Text) & "','" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                strSql += "'" & CDbl(lblRevenue_Operation.Text) & "','" & CDbl(lblRevenue_Operationtotal.Text) & "','" & CDbl(txtCAPEX.Text) & "','" & CDbl(lblCAPEX_Profit.Text) & "','" & CDbl(lblCashFlow.Text) & "','" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                If txtPayBack.Text = "<=1" Then
                    strSql += "'-1', "
                Else
                    strSql += "'" & CDbl(txtPayBack.Text) & "', "
                End If
                If txtPayBack_Profit.Text = "<=1" Then
                    strSql += "'-1', " + vbCr
                Else
                    strSql += "'" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                End If
                strSql += "'" & CDbl(txtMargin.Text) & "','" & CDbl(txtMargin_Profit.Text) & "','" & CDbl(txtNPV.Text) & "','" & CDbl(txtNPV_Profit.Text) & "','1','" + Session("uemail") + "',getdate()) "
                C.ExecuteNonQuery(strSql)
            End If


            Dim vSqlIn As String = ""
            Dim flow_id As String
            Dim purchase As Double = 0
            Dim tMargin As Double = 0
            Dim tPayback As Double = 0
            Dim tPenaltyLate As Double = 0
            Dim tRevenue As Double = 0

            If IsNumeric(Replace(txtCAPEX.Text, "   THB", "")) = True Then
                purchase = CDbl(Replace(txtCAPEX.Text, "   THB", ""))
            End If
            If IsNumeric(txtMargin.Text) = True Then
                tMargin = CDbl(txtMargin.Text)
            End If
            If IsNumeric(txtPayBack.Text) = True Then
                tPayback = CDbl(txtPayBack.Text)
            End If
            If IsNumeric(lblPenaltyLate.Text) = True Then
                tPenaltyLate = CDbl(lblPenaltyLate.Text)
            End If
            If IsNumeric(txtRevenue.Text) = True Then
                If CDbl(txtRevenue.Text) <= 0 Then
                    tRevenue = 1
                Else
                    tRevenue = CDbl(txtRevenue.Text)
                End If
            End If

            flow_id = Cal.getFlowID(lblArea.Text, lblPresaleMail.Text, tPenaltyLate, tRevenue, tPayback, purchase, tMargin, lblSpecialPrice.Text, lblSpecialApprove.Text)

            DT_flow_id = C.GetDataTable("select * from request_flow where request_id = '" & xRequest_id & "' and disable = '0' and flow_id = '" & flow_id & "'")
            'If DT_flow_id.Rows.Count <= 0 Then
            Dim vDTF As New DataTable
            vDTF = CF.rLoadRequestFlow(xRequest_id, "1")
            vSqlIn += "DECLARE @newid varchar(50) "
            vSqlIn += "SET @newid = '" + xRequest_id + "' "
            vSqlIn += CF.rSqlDisableRequestFlow(xRequest_id, vDTF.Rows(0).Item("flow_id"), "1")
            vSqlIn += CF.rSqlInsertRequestFlow(flow_id, "1")
            'vSqlIn += CF.rSqlSetDepartRequestFlow( _
            '    vDTF.Rows(0).Item("uemail_verify"), "", _
            '    vDTF.Rows(0).Item("createBy"), vDTF.Rows(0).Item("uemail_HRO"), "", "")

            C.ExecuteNonQuery(vSqlIn)
            'End If


            vSqlIn = CF.rSqlUpdateRequestFlow(lblArea.Text, lblROMail.Text, lblPrepaireEmail.Text, lblPresaleMail.Text, Session("uemail"), xRequest_id)
            C.ExecuteNonQuery(vSqlIn)

            If Command = "SubmitApprove" Then
                strSql = "Update FeasibilityDocument set Document_Date = '" + C.CDateText(txtDocumentDate.Text) + "', Service_Date = '" + C.CDateText(txtServiceDate.Text) + "', Area = '" + lblArea.Text + "', Cluster = '" + lblCluster.Text + "', Customer_Name = '" + lblCustomerName.Text + "', Type_Service = '" + lblTypeOfService.Text + "', Detail_Service = '" + txtDetailService.Text + "', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate(), Status = '1'  "
                If lblRequestStatus.Text = "110" Then
                    strSql += ", request_Status='0' " + vbCr
                End If
                strSql += "where Document_No = '" + xRequest_id + "' "
                C.ExecuteNonQuery(strSql)

                If lblRequestStatus.Text = "110" Then
                    CF.UpdateRequest(xRequest_id, "", "", "", "", Session("uemail"), Session("uemail"), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                End If

                'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('�ѹ�֡�����������');", True)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('�ѹ�֡�����������͹��ѵ� �����',function(){ focus();window.location.href='check_status_list.aspx?menu=check'; });", True)

            Else
                Dim dt_flow As New DataTable
                dt_flow = C.GetDataTable("select * from dbo.request_flow where request_id = '" & xRequest_id & "' and disable='0' and flow_step = '1' ")
                If dt_flow.Rows.Count > 0 Then
                    vSqlIn = "Update request_flow set flow_status = '110' where request_id = '" & xRequest_id & "' and disable = '0' and flow_step = '1' " + vbCr
                    vSqlIn += "update FeasibilityDocument set "
                    vSqlIn += "request_status = 110 "
                    vSqlIn += ", last_update = getdate(), last_depart = '" & dt_flow.Rows(0).Item("depart_id") & "' "
                    vSqlIn += ", next_depart = '0' " '***** ������������ ����ӴѺ�Ѵ��� �����ҧ�Ӣ�
                    vSqlIn += "where Document_No = '" & xRequest_id & "' "
                    C.ExecuteNonQuery(vSqlIn)
                End If

                Dim sm_alert As String = "AlertSuccess('�ѹ�֡�ç��� �����',function(){ window.location = 'edit_list.aspx?menu=edit'; });"
                ClientScript.RegisterStartupScript(Page.GetType, "alert_summary", sm_alert, True)
            End If

        End If
    End Sub

    Protected Sub btnSaveDraft_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveDraft.Click
        If lblCreateOrEdit.Text = "create" Then
            SaveDraftCreateSummary()
        ElseIf lblCreateOrEdit.Text = "edit" Then
            SaveDraftEditSummary()
        End If
    End Sub

    Public Sub SaveDraftCreateSummary()
        Dim DT_ProjectName As New DataTable
        Dim DT_Summary As New DataTable
        Dim strSql As String
        Dim DT As New DataTable

        DT_ProjectName = GetTableProjectName()
        DT_Summary = GetTableSummary()

        If DT_ProjectName.Rows.Count <= 0 Then
            Dim pn_alert As String = "AlertNotification('��سҡ�͡������ Project Name',"
            pn_alert += "function(){ focus();window.location.href='project_name.aspx?menu=upload';"
            pn_alert += "});"

            ClientScript.RegisterStartupScript(Page.GetType, "alert_projectname", pn_alert, True)
        Else
            If DT_ProjectName.Rows(0).Item("project_File") = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡������ �˹�� Project Overview ��͹', function(){ window.location = 'project_name.aspx?menu=upload'; });", True)
                Exit Sub
            End If

            Try
                If DT_Summary.Rows.Count > 0 Then
                    strSql = "Update List_Summary set Revenue = '" & CDbl(txtRevenue.Text) & "', Revenue_Profit = '" & CDbl(lblRevenue_Profit.Text) & "', " + vbCr
                    strSql += "OPEX = '" & CDbl(lblOPEX.Text) & "', OPEX_Profit = '" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                    strSql += "MarketingPrice = '" & CDbl(txtMarketing.Text) & "',MarketingPrice_Profit = '" & CDbl(lblMarketing_profit.Text) & "', " + vbCr
                    strSql += "EntertainPrice = '" & CDbl(txtEntertain.Text) & "', EntertainPrice_Profit = '" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                    strSql += "GiftPrice = '" & CDbl(txtGift.Text) & "',GiftPrice_Profit = '" & CDbl(lblGift_profit.Text) & "', " + vbCr
                    strSql += "MarketingCost = '" & CDbl(txtMarketingCost.Text) & "', MarketingCost_Profit = '" & CDbl(lblMarketingCost_Profit.Text) & "', " + vbCr
                    strSql += "InternetCost = '" & CDbl(txtInternetCost.Text) & "', NetworkCost = '" & CDbl(txtNetworkCost.Text) & "', " + vbCr
                    strSql += "NOCCost = '" & CDbl(txtNOCCost.Text) & "', NOCCost_Profit = '" & CDbl(txtNOCCost_Profit.Text) & "', JasmineGroupCost = '" & CDbl(txtJasmineGroupCost.Text) & "', " + vbCr
                    strSql += "OtherCost = '" & CDbl(txtOtherCost.Text) & "', OtherCost_Profit = '" & CDbl(lblOtherCost_Profit.Text) & "', " + vbCr
                    strSql += "PenaltyCost = '" & CDbl(txtPenaltyCost.Text) & "', PenaltyCost_Profit = '" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                    strSql += "RevenueAfter = '" & CDbl(lblRevenue_Operation.Text) & "', RevenueAfter_Profit = '" & CDbl(lblRevenue_Operationtotal.Text) & "', " + vbCr
                    strSql += "CAPEX = '" & CDbl(txtCAPEX.Text) & "', CAPEX_Profit = '" & CDbl(lblCAPEX_Profit.Text) & "', " + vbCr
                    strSql += "CashFlow = '" & CDbl(lblCashFlow.Text) & "', CashFlow_Profit = '" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                    If txtPayBack.Text = "<=1" Then
                        strSql += "Payback = '-1', " + vbCr
                    Else
                        strSql += "Payback = '" & CDbl(txtPayBack.Text) & "', "
                    End If
                    If txtPayBack_Profit.Text = "<=1" Then
                        strSql += "Payback_Profit = '-1', " + vbCr
                    Else
                        strSql += "Payback_Profit = '" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                    End If
                    strSql += "Margin = '" & CDbl(txtMargin.Text) & "', Margin_Profit = '" & CDbl(txtMargin_Profit.Text) & "', " + vbCr
                    strSql += "NPV = '" & CDbl(txtNPV.Text) & "', NPV_Profit = '" & CDbl(txtNPV_Profit.Text) & "', " + vbCr
                    strSql += "Status = '1', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() " + vbCr
                    strSql += "where CreateBy='" + Session("uemail") + "' and Document_No is null "
                    C.ExecuteNonQuery(strSql)

                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('�ѹ�֡����������ش�����', function(){ window.location = 'Summary_Upload.aspx?menu=upload'; });", True)
                Else
                    strSql = "Insert Into List_Summary (Revenue,Revenue_Profit,OPEX,OPEX_Profit, " + vbCr
                    strSql += "MarketingPrice,MarketingPrice_Profit,EntertainPrice,EntertainPrice_Profit, " + vbCr
                    strSql += "GiftPrice,GiftPrice_Profit, " + vbCr
                    strSql += "MarketingCost,MarketingCost_Profit,InternetCost,NetworkCost,NOCCost,NOCCost_Profit, " + vbCr
                    strSql += "JasmineGroupCost,OtherCost,OtherCost_Profit,PenaltyCost,PenaltyCost_Profit, " + vbCr
                    strSql += "RevenueAfter,RevenueAfter_Profit,CAPEX,CAPEX_Profit,CashFlow,CashFlow_Profit, " + vbCr
                    strSql += "Payback,Payback_Profit, " + vbCr
                    strSql += "Margin,Margin_Profit,NPV,NPV_Profit,Status,CreateBy,CreateDate)" + vbCr
                    strSql += "values ('" & CDbl(txtRevenue.Text) & "','" & CDbl(lblRevenue_Profit.Text) & "','" & CDbl(lblOPEX.Text) & "','" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtMarketing.Text) & "','" & CDbl(lblMarketing_profit.Text) & "','" & CDbl(txtEntertain.Text) & "','" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtGift.Text) & "','" & CDbl(lblGift_profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtMarketingCost.Text) & "','" & CDbl(lblMarketingCost_Profit.Text) & "','" & CDbl(txtInternetCost.Text) & "','" & CDbl(txtNetworkCost.Text) & "','" & CDbl(txtNOCCost.Text) & "','" & CDbl(txtNOCCost_Profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtJasmineGroupCost.Text) & "','" & CDbl(txtOtherCost.Text) & "','" & CDbl(lblOtherCost_Profit.Text) & "','" & CDbl(txtPenaltyCost.Text) & "','" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(lblRevenue_Operation.Text) & "','" & CDbl(lblRevenue_Operationtotal.Text) & "','" & CDbl(txtCAPEX.Text) & "','" & CDbl(lblCAPEX_Profit.Text) & "','" & CDbl(lblCashFlow.Text) & "','" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                    If txtPayBack.Text = "<=1" Then
                        strSql += "'-1', "
                    Else
                        strSql += "'" & CDbl(txtPayBack.Text) & "', "
                    End If
                    If txtPayBack_Profit.Text = "<=1" Then
                        strSql += "'-1', " + vbCr
                    Else
                        strSql += "'" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                    End If
                    strSql += "'" & CDbl(txtMargin.Text) & "','" & CDbl(txtMargin_Profit.Text) & "','" & CDbl(txtNPV.Text) & "','" & CDbl(txtNPV_Profit.Text) & "','1','" + Session("uemail") + "',getdate()) "
                    C.ExecuteNonQuery(strSql)

                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('�ѹ�֡�����������', function(){ window.location = 'Summary_Upload.aspx?menu=upload'; });", True)
                End If
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "failed_update", "AlertError('�������ö�ѹ�֡��������');", True)
            End Try

        End If
    End Sub

    Public Sub SaveDraftEditSummary()
        Dim DT_ProjectName As New DataTable
        Dim DT_Summary As New DataTable
        Dim strSql As String
        Dim DT As New DataTable

        DT_ProjectName = GetTableEditProjectName()
        DT_Summary = GetTableEditSummary()

        If DT_ProjectName.Rows.Count <= 0 Then
            Dim pn_alert As String = "AlertNotification('��سҡ�͡������ Project Name',"
            pn_alert += "function(){ focus();window.location.href='edit_project_name.aspx?request_id=" & xRequest_id & "';"
            pn_alert += "});"

            ClientScript.RegisterStartupScript(Page.GetType, "alert_projectname", pn_alert, True)
        Else
            If DT_ProjectName.Rows(0).Item("project_File") = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertNotification('�ôṺ Project File ��кѹ�֡������ �˹�� Project Overview ��͹', function(){ window.location = 'edit_project_name.aspx?request_id=" & xRequest_id & "'; });", True)
                Exit Sub
            End If

            Try
                If DT_Summary.Rows.Count > 0 Then
                    strSql = "Update List_Summary set Revenue = '" & CDbl(txtRevenue.Text) & "', Revenue_Profit = '" & CDbl(lblRevenue_Profit.Text) & "', " + vbCr
                    strSql += "OPEX = '" & CDbl(lblOPEX.Text) & "', OPEX_Profit = '" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                    strSql += "MarketingPrice = '" & CDbl(txtMarketing.Text) & "',MarketingPrice_Profit = '" & CDbl(lblMarketing_profit.Text) & "', " + vbCr
                    strSql += "EntertainPrice = '" & CDbl(txtEntertain.Text) & "', EntertainPrice_Profit = '" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                    strSql += "GiftPrice = '" & CDbl(txtGift.Text) & "',GiftPrice_Profit = '" & CDbl(lblGift_profit.Text) & "', " + vbCr
                    strSql += "MarketingCost = '" & CDbl(txtMarketingCost.Text) & "', MarketingCost_Profit = '" & CDbl(lblMarketingCost_Profit.Text) & "', " + vbCr
                    strSql += "InternetCost = '" & CDbl(txtInternetCost.Text) & "', NetworkCost = '" & CDbl(txtNetworkCost.Text) & "', " + vbCr
                    strSql += "NOCCost = '" & CDbl(txtNOCCost.Text) & "', NOCCost_Profit = '" & CDbl(txtNOCCost_Profit.Text) & "', JasmineGroupCost = '" & CDbl(txtJasmineGroupCost.Text) & "', " + vbCr
                    strSql += "OtherCost = '" & CDbl(txtOtherCost.Text) & "', OtherCost_Profit = '" & CDbl(lblOtherCost_Profit.Text) & "', " + vbCr
                    strSql += "PenaltyCost = '" & CDbl(txtPenaltyCost.Text) & "', PenaltyCost_Profit = '" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                    strSql += "RevenueAfter = '" & CDbl(lblRevenue_Operation.Text) & "', RevenueAfter_Profit = '" & CDbl(lblRevenue_Operationtotal.Text) & "', " + vbCr
                    strSql += "CAPEX = '" & CDbl(txtCAPEX.Text) & "', CAPEX_Profit = '" & CDbl(lblCAPEX_Profit.Text) & "', " + vbCr
                    strSql += "CashFlow = '" & CDbl(lblCashFlow.Text) & "', CashFlow_Profit = '" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                    If txtPayBack.Text = "<=1" Then
                        strSql += "Payback = '-1', " + vbCr
                    Else
                        strSql += "Payback = '" & CDbl(txtPayBack.Text) & "', "
                    End If
                    If txtPayBack_Profit.Text = "<=1" Then
                        strSql += "Payback_Profit = '-1', " + vbCr
                    Else
                        strSql += "Payback_Profit = '" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                    End If
                    strSql += "Margin = '" & CDbl(txtMargin.Text) & "', Margin_Profit = '" & CDbl(txtMargin_Profit.Text) & "', " + vbCr
                    strSql += "NPV = '" & CDbl(txtNPV.Text) & "', NPV_Profit = '" & CDbl(txtNPV_Profit.Text) & "', " + vbCr
                    strSql += "Status = '1', UpdateBy = '" + Session("uemail") + "', UpdateDate = getdate() " + vbCr
                    strSql += "where Document_No = '" & xRequest_id & "' "
                    C.ExecuteNonQuery(strSql)

                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('�ѹ�֡����������ش�����', function(){ window.location = 'Summary_Upload.aspx?request_id=" & xRequest_id & "'; });", True)
                Else
                    strSql = "Insert Into List_Summary (Document_No,Revenue,Revenue_Profit,OPEX,OPEX_Profit, " + vbCr
                    strSql += "MarketingPrice,MarketingPrice_Profit,EntertainPrice,EntertainPrice_Profit, " + vbCr
                    strSql += "GiftPrice,GiftPrice_Profit, " + vbCr
                    strSql += "MarketingCost,MarketingCost_Profit,InternetCost,NetworkCost,NOCCost,NOCCost_Profit, " + vbCr
                    strSql += "JasmineGroupCost,OtherCost,OtherCost_Profit,PenaltyCost,PenaltyCost_Profit, " + vbCr
                    strSql += "RevenueAfter,RevenueAfter_Profit,CAPEX,CAPEX_Profit,CashFlow,CashFlow_Profit, " + vbCr
                    strSql += "Payback,Payback_Profit, " + vbCr
                    strSql += "Margin,Margin_Profit,NPV,NPV_Profit,Status,CreateBy,CreateDate)" + vbCr
                    strSql += "values ('" & xRequest_id & "','" & CDbl(txtRevenue.Text) & "','" & CDbl(lblRevenue_Profit.Text) & "','" & CDbl(lblOPEX.Text) & "','" & CDbl(lblOPEX_total.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtMarketing.Text) & "','" & CDbl(lblMarketing_profit.Text) & "','" & CDbl(txtEntertain.Text) & "','" & CDbl(lblEntertain_profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtGift.Text) & "','" & CDbl(lblGift_profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtMarketingCost.Text) & "','" & CDbl(lblMarketingCost_Profit.Text) & "','" & CDbl(txtInternetCost.Text) & "','" & CDbl(txtNetworkCost.Text) & "','" & CDbl(txtNOCCost.Text) & "','" & CDbl(txtNOCCost_Profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(txtJasmineGroupCost.Text) & "','" & CDbl(txtOtherCost.Text) & "','" & CDbl(lblOtherCost_Profit.Text) & "','" & CDbl(txtPenaltyCost.Text) & "','" & CDbl(lblPenaltyCost_Profit.Text) & "', " + vbCr
                    strSql += "'" & CDbl(lblRevenue_Operation.Text) & "','" & CDbl(lblRevenue_Operationtotal.Text) & "','" & CDbl(txtCAPEX.Text) & "','" & CDbl(lblCAPEX_Profit.Text) & "','" & CDbl(lblCashFlow.Text) & "','" & CDbl(lblCashFlow_Profit.Text) & "', " + vbCr
                    If txtPayBack.Text = "<=1" Then
                        strSql += "'-1', "
                    Else
                        strSql += "'" & CDbl(txtPayBack.Text) & "', "
                    End If
                    If txtPayBack_Profit.Text = "<=1" Then
                        strSql += "'-1', " + vbCr
                    Else
                        strSql += "'" & CDbl(txtPayBack_Profit.Text) & "', " + vbCr
                    End If
                    strSql += "'" & CDbl(txtMargin.Text) & "','" & CDbl(txtMargin_Profit.Text) & "','" & CDbl(txtNPV.Text) & "','" & CDbl(txtNPV_Profit.Text) & "','1','" + Session("uemail") + "',getdate()) "
                    C.ExecuteNonQuery(strSql)

                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('�ѹ�֡�����������', function(){ window.location = 'Summary_Upload.aspx?request_id=" & xRequest_id & "'; });", True)
                End If
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "failed_update", "AlertError('�������ö�ѹ�֡��������');", True)
            End Try

        End If
    End Sub

    Protected Sub test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles test.Click
        Response.Write("<script>")
        Response.Write("window.open('summarypdf.aspx?Doc=" + xRequest_id + "','_blank')")
        Response.Write("</script>")
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
                    lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; ������. -"
                Else
                    lblCustomerName.Text = DT_ProjectName.Rows(0).Item("Customer_Contact_Name") + "&nbsp;&nbsp; ������. " + DT_ProjectName.Rows(0).Item("Customer_Contact_Tel")
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
                showPenalty += "�����Թ��һ�Ѻ���ͺ��Ҫ�� " & Format(CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString), "###,##0.00") & " �ҷ<br />"
                lblPenaltyLate.Text = CDbl(DT_ProjectName.Rows(0).Item("PenaltyLate").ToString)
            End If
            If CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString) = "0" Then
                showPenalty += ""
            Else
                showPenalty += "�����Թ��һ�Ѻ�ҹ���� " & Format(CDbl(DT_ProjectName.Rows(0).Item("Penalty").ToString), "###,##0.00") & "%<br />"
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

    Public Function GetTableSummary() As DataTable
        Dim strSql As String
        Dim DT_Summary As DataTable
        strSql = "select * from List_Summary where CreateBy='" + Session("uemail") + "' and Document_No is null "
        DT_Summary = C.GetDataTable(strSql)
        Return DT_Summary
    End Function

    Public Function GetTableEditService() As DataTable
        Dim strSql As String
        Dim DT_Service As DataTable

        strSql = "select * from dbo.List_Service where Document_No = '" & xRequest_id & "' "
        DT_Service = C.GetDataTable(strSql)
        Return DT_Service
    End Function

    Public Function GetTableEditProjectName() As DataTable
        Dim strSql As String
        Dim DT_ProjectName As DataTable
        strSql = "select * from List_ProjectName where Document_No = '" & xRequest_id & "' "
        DT_ProjectName = C.GetDataTable(strSql)
        Return DT_ProjectName
    End Function

    Public Function GetTableEditSummary() As DataTable
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

    Protected Sub txtRevenue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRevenue.TextChanged
        If IsNumeric(txtRevenue.Text) Then

        Else
            txtRevenue.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Revenue �繵���Ţ��ҹ��!');", True)
            txtRevenue.Focus()
        End If
        CalculatePrice()
    End Sub

    'Protected Sub txtRevenue_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRevenue_Profit.TextChanged
    '    If IsNumeric(txtRevenue_Profit.Text) Then
    '        CalculatePrice()
    '    Else
    '        txtRevenue_Profit.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Revenue Profit �繵���Ţ��ҹ��!');", True)
    '        txtRevenue_Profit.Focus()
    '    End If
    'End Sub

    Protected Sub txtMarketingCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarketingCost.TextChanged
        If IsNumeric(txtMarketingCost.Text) Then

        Else
            txtMarketingCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� �鹷ع�ҧ��õ�Ҵ(Marketing Cost) �繵���Ţ��ҹ��!');", True)
            txtMarketingCost.Focus()
        End If
        CalculatePrice()
    End Sub

    'Protected Sub txtMarketingCost_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarketingCost_Profit.TextChanged
    '    If IsNumeric(txtMarketingCost_Profit.Text) Then
    '        CalculatePrice()
    '    Else
    '        txtMarketingCost_Profit.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� �鹷ع�ҧ��õ�Ҵ(Marketing Cost) Profit �繵���Ţ��ҹ��!');", True)
    '        txtMarketingCost_Profit.Focus()
    '    End If
    'End Sub

    Protected Sub txtMarketing_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarketing.TextChanged
        If IsNumeric(txtMarketing.Text) Then

        Else
            txtMarketing.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� ��ҡ�õ�Ҵ �繵���Ţ��ҹ��!');", True)
            txtMarketing.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtEntertain_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEntertain.TextChanged
        If IsNumeric(txtEntertain.Text) Then

        Else
            txtEntertain.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к�  �������§�Ѻ�ͧ�١��� �繵���Ţ��ҹ��!');", True)
            txtEntertain.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtGift_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGift.TextChanged
        If IsNumeric(txtGift.Text) Then

        Else
            txtGift.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к�  ��Ңͧ��ѭ �繵���Ţ��ҹ��!');", True)
            txtGift.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtInternetCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInternetCost.TextChanged
        If IsNumeric(txtInternetCost.Text) Then

        Else
            txtInternetCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� �鹷ع Internet Bandwidth �繵���Ţ��ҹ��!');", True)
            txtInternetCost.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtNetworkCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNetworkCost.TextChanged
        If IsNumeric(txtNetworkCost.Text) Then

        Else
            txtNetworkCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� �鹷ع Network Bandwidth �繵���Ţ��ҹ��!');", True)
            txtNetworkCost.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtNOCCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNOCCost.TextChanged
        If IsNumeric(txtNOCCost.Text) Then

        Else
            txtNOCCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�кص鹷ع O & M �繵���Ţ��ҹ��!');", True)
            txtNOCCost.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtNOCCost_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNOCCost_Profit.TextChanged
        If IsNumeric(txtNOCCost_Profit.Text) Then

        Else
            txtNOCCost_Profit.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�кص鹷ع O & M Profit �繵���Ţ��ҹ��!');", True)
            txtNOCCost_Profit.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtJasmineGroupCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJasmineGroupCost.TextChanged
        If IsNumeric(txtJasmineGroupCost.Text) Then

        Else
            txtJasmineGroupCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� EXP. Jasmine Group �繵���Ţ��ҹ��!');", True)
            txtJasmineGroupCost.Focus()
        End If
        CalculatePrice()
    End Sub

    Protected Sub txtOtherCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOtherCost.TextChanged
        If IsNumeric(txtOtherCost.Text) Then

        Else
            txtOtherCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� EXP. Other �繵���Ţ��ҹ��!');", True)
            txtOtherCost.Focus()
        End If
        CalculatePrice()
    End Sub

    'Protected Sub txtOtherCost_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOtherCost_Profit.TextChanged
    '    If IsNumeric(txtOtherCost_Profit.Text) Then
    '        CalculatePrice()
    '    Else
    '        txtOtherCost_Profit.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� EXP. Other Profit �繵���Ţ��ҹ��!');", True)
    '        txtOtherCost_Profit.Focus()
    '    End If
    'End Sub

    Protected Sub txtPenaltyCost_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPenaltyCost.TextChanged
        If IsNumeric(txtPenaltyCost.Text) Then

        Else
            txtPenaltyCost.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Penalty �繵���Ţ��ҹ��!');", True)
            txtPenaltyCost.Focus()
        End If
        CalculatePrice()
    End Sub

    'Protected Sub txtPenaltyCost_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPenaltyCost_Profit.TextChanged
    '    If IsNumeric(txtPenaltyCost_Profit.Text) Then
    '        CalculatePrice()
    '    Else
    '        txtPenaltyCost_Profit.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Penalty Profit �繵���Ţ��ҹ��!');", True)
    '        txtPenaltyCost_Profit.Focus()
    '    End If
    'End Sub

    Protected Sub txtCAPEX_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCAPEX.TextChanged
        If IsNumeric(txtCAPEX.Text) Then

        Else
            txtCAPEX.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� CAPEX �繵���Ţ��ҹ��!');", True)
            txtCAPEX.Focus()
        End If
        lblCAPEX_Profit.Text = txtCAPEX.Text
        CalculatePrice()
    End Sub

    Protected Sub lblCAPEX_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCAPEX_Profit.TextChanged
        If IsNumeric(lblCAPEX_Profit.Text) Then

        Else
            lblCAPEX_Profit.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� CAPEX Profit �繵���Ţ��ҹ��!');", True)
            lblCAPEX_Profit.Focus()
        End If
        CalculatePrice()
    End Sub

    'Protected Sub txtCashFlow_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashFlow.TextChanged
    '    If IsNumeric(txtCashFlow.Text) Then

    '    Else
    '        txtCashFlow.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Cash Flow �繵���Ţ��ҹ��!');", True)
    '        txtCashFlow.Focus()
    '    End If
    'End Sub

    'Protected Sub txtCashFlow_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCashFlow_Profit.TextChanged
    '    If IsNumeric(txtCashFlow_Profit.Text) Then

    '    Else
    '        txtCashFlow_Profit.Text = "0"
    '        ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Cash Flow Profit �繵���Ţ��ҹ��!');", True)
    '        txtCashFlow_Profit.Focus()
    '    End If
    'End Sub

    Protected Sub txtPayBack_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayBack.TextChanged
        If IsNumeric(txtPayBack.Text) Then

        Else
            If txtPayBack.Text <> "<=1" Then
                txtPayBack.Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Payback �繵���Ţ ���� <=1 ��ҹ��!');", True)
                txtPayBack.Focus()
            End If
        End If
    End Sub

    Protected Sub txtPayBack_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPayBack_Profit.TextChanged
        If IsNumeric(txtPayBack_Profit.Text) Then

        Else
            If txtPayBack_Profit.Text <> "<=1" Then
                txtPayBack_Profit.Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Payback Profit �繵���Ţ ���� <=1 ��ҹ��!');", True)
                txtPayBack_Profit.Focus()
            End If
        End If
    End Sub

    Protected Sub txtMargin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMargin.TextChanged
        If IsNumeric(txtMargin.Text) Then

        Else
            txtMargin.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Margin �繵���Ţ��ҹ��!');", True)
            txtMargin.Focus()
        End If
    End Sub

    Protected Sub txtMargin_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMargin_Profit.TextChanged
        If IsNumeric(txtMargin_Profit.Text) Then

        Else
            txtMargin_Profit.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� Margin Profit �繵���Ţ��ҹ��!');", True)
            txtMargin_Profit.Focus()
        End If
    End Sub

    Protected Sub txtNPV_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNPV.TextChanged
        If IsNumeric(txtNPV.Text) Then

        Else
            txtNPV.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� NPV �繵���Ţ��ҹ��!');", True)
            txtNPV.Focus()
        End If
    End Sub

    Protected Sub txtNPV_Profit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNPV_Profit.TextChanged
        If IsNumeric(txtNPV_Profit.Text) Then

        Else
            txtNPV_Profit.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�к� NPV Profit �繵���Ţ��ҹ��!');", True)
            txtNPV_Profit.Focus()
        End If
    End Sub

    Public Sub CalculatePrice()
        Dim MoneyFmt As String = "###,##0.00"
        Dim MoneyRoundup As String = "###,##0"
        Dim PercentFmt As String = "#0.00"

        lblRevenue_Profit.Text = Format(CDbl(txtRevenue.Text), MoneyRoundup)
        txtMarketingCost.Text = CDbl(txtMarketing.Text) + CDbl(txtEntertain.Text) + CDbl(txtGift.Text)
        lblMarketingCost_Profit.Text = Format(CDbl(txtMarketingCost.Text), MoneyRoundup)
        lblMarketing_profit.Text = Format(CDbl(txtMarketing.Text), MoneyRoundup)
        lblEntertain_profit.Text = Format(CDbl(txtEntertain.Text), MoneyRoundup)
        lblGift_profit.Text = Format(CDbl(txtGift.Text), MoneyRoundup)
        lblOtherCost_Profit.Text = Format(CDbl(txtOtherCost.Text), MoneyRoundup)
        lblPenaltyCost_Profit.Text = Format(CDbl(txtPenaltyCost.Text), MoneyRoundup)
        'lblCAPEX_Profit.Text = Format(CDbl(txtCAPEX.Text), MoneyRoundup)

        lblOPEX.Text = Format(Math.Ceiling(CDbl(txtMarketingCost.Text) + CDbl(txtInternetCost.Text) + CDbl(txtNetworkCost.Text) + CDbl(txtNOCCost.Text) + CDbl(txtJasmineGroupCost.Text) + CDbl(txtOtherCost.Text) + CDbl(txtPenaltyCost.Text)), MoneyRoundup)
        lblOPEX_total.Text = Format(Math.Ceiling(CDbl(lblMarketingCost_Profit.Text) + CDbl(txtNOCCost_Profit.Text) + CDbl(lblOtherCost_Profit.Text) + CDbl(lblPenaltyCost_Profit.Text)), MoneyRoundup)
        lblRevenue_Operation.Text = Format(Math.Ceiling(CDbl(txtRevenue.Text) - CDbl(lblOPEX.Text)), MoneyRoundup)
        lblRevenue_Operationtotal.Text = Format(Math.Ceiling(CDbl(lblRevenue_Profit.Text) - CDbl(lblOPEX_total.Text)), MoneyRoundup)
        lblCashFlow.Text = Format(Math.Ceiling(CDbl(lblRevenue_Operation.Text) - CDbl(txtCAPEX.Text)), MoneyRoundup)
        lblCashFlow_Profit.Text = Format(Math.Ceiling(CDbl(lblRevenue_Operationtotal.Text) - CDbl(lblCAPEX_Profit.Text)), MoneyRoundup)

        If CDbl(txtRevenue.Text) = 0 Then
            lblRevenuePercent.Text = ""
            lblOtherPercent.Text = ""
            lblCAPEXPercent.Text = ""
        Else
            lblRevenuePercent.Text = "100%"
            If CDbl(txtOtherCost.Text) / CDbl(txtRevenue.Text) > 1 Then
                lblOtherPercent.Text = ""
            Else
                lblOtherPercent.Text = Format((CDbl(txtOtherCost.Text) / CDbl(txtRevenue.Text)) * 100, PercentFmt) & "%"
            End If

            If CDbl(txtCAPEX.Text) / CDbl(txtRevenue.Text) > 1 Then
                lblCAPEXPercent.Text = ""
            Else
                lblCAPEXPercent.Text = Format((CDbl(txtCAPEX.Text) / CDbl(txtRevenue.Text)) * 100, PercentFmt) & "%"
            End If

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

    Protected Sub btnCancelProject_Command(sender As Object, e As CommandEventArgs) Handles btnCancelProject.Command
        show_CancelModal()
    End Sub

    Sub show_CancelModal()
        Dim myModal As String
        myModal = "$('#divCancelProject').modal('show');" + vbCr
        myModal += "$('.modal-header').on('mousedown', function (mousedownEvt) {" + vbCr
        myModal += "        var $draggable = $(this);" + vbCr
        myModal += "        var x = mousedownEvt.pageX - $draggable.offset().left," + vbCr
        myModal += "                y = mousedownEvt.pageY - $draggable.offset().top;" + vbCr
        myModal += "        $('body').on('mousemove.draggable', function (mousemoveEvt) {" + vbCr
        myModal += "           $draggable.closest('.modal-dialog').offset({" + vbCr
        myModal += "                'left': mousemoveEvt.pageX - x," + vbCr
        myModal += "                'top': mousemoveEvt.pageY - y" + vbCr
        myModal += "            });" + vbCr
        myModal += "        });" + vbCr
        myModal += "        $('body').one('mouseup', function () {" + vbCr
        myModal += "            $('body').off('mousemove.draggable');" + vbCr
        myModal += "        });" + vbCr
        myModal += "        $draggable.closest('#divCancelProject').one('bs.modal.hide', function () {" + vbCr
        myModal += "            $('body').off('mousemove.draggable');" + vbCr
        myModal += "        });" + vbCr
        myModal += "    });" + vbCr
        ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", myModal, True)
    End Sub

    Protected Sub btnSaveCancelProject_Click(sender As Object, e As EventArgs) Handles btnSaveCancelProject.Click
        If Trim(txtRemarkCancelProject.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('��س��к����˵ط��¡��ԡ�ç���');", True)
            show_CancelModal()
        ElseIf txtRemarkCancelProject.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""�������ö���� ' 㹡���к����˵ط��¡��ԡ�ç��� ��"");", True)
            show_CancelModal()
        Else
            Dim result As String
            result = CF.CancelProject(Session("uemail"), xRequest_id, txtRemarkCancelProject.Text)
            If result = "Complete" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_cancel", "AlertSuccess('¡��ԡ�ç��� " & xRequest_id & " ���º����', function(){ window.location = 'check_status_list.aspx?menu=check'; });", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('" & C.rpQuoted(result) & "');focus();", True)
            End If
        End If

    End Sub

End Class
