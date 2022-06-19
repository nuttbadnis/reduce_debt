Imports System.Data
Imports System.IO
Partial Class edit_project_name
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim Cal As New Cls_Calculate
    Dim xRequest_id

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xRequest_id = Request.QueryString("request_id")
        If Not Page.IsPostBack Then
            menu_service.HRef = "edit_service.aspx?request_id=" + xRequest_id
            menu_capex.HRef = "edit_capex.aspx?request_id=" + xRequest_id
            menu_opex.HRef = "edit_opex.aspx?request_id=" + xRequest_id
            menu_other.HRef = "edit_other.aspx?request_id=" + xRequest_id
            menu_management.HRef = "edit_management.aspx?request_id=" + xRequest_id
            menu_summary.HRef = "edit_Summary.aspx?request_id=" + xRequest_id
            menu_summary_upload.HRef = "Summary_Upload.aspx?request_id=" + xRequest_id
            Session("FileUploadProjectName") = Nothing
            Dim alert As String
            Dim dt_check As DataTable
            dt_check = C.GetDataTable("select * from dbo.FeasibilityDocument where (/*request_status = 0 or request_status = 55 or*/ request_status = 110) and Document_No = '" + xRequest_id + "'")
            If dt_check.Rows.Count > 0 Then
                'If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "0" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                'If dt_check.Rows(0).Item("CreateBy") <> Session("uemail") Then
                '    btnSave.Visible = False
                'End If
                If CheckUpload() = "1" Then
                    menu_create.Visible = False
                    menu_upload.Visible = True
                    first_line.Visible = True
                Else
                    menu_create.Visible = True
                    menu_upload.Visible = False
                    div_UploadProject.Visible = False
                    div_OpenProject.Visible = False
                    first_line.Visible = False
                End If

                If (Session("Login_permission") = "administrator" Or (dt_check.Rows(0).Item("request_status").ToString = "55" And Session("Login_permission") = "inspector") Or (dt_check.Rows(0).Item("request_status").ToString = "110" And dt_check.Rows(0).Item("CreateBy").ToString = Session("uemail"))) Then
                    Dim DT_List As DataTable
                    Dim strSql As String
                    strSql = "select Project_Name, Project_Code, Customer_Name, Enterprise_Name, Location_Name, Customer_Type " + vbCr
                    strSql += ", convert(varchar(10),Document_Date,103) 'Document_Date', convert(varchar(10),Service_Date,103) 'Service_Date' " + vbCr
                    strSql += ", Customer_Assistant_Name, Customer_Assistant_ID, Area, Cluster, Cluster_name, Cluster_email " + vbCr
                    strSql += ", Customer_Contact_Name, Customer_Contact_Tel, Customer_Contact_Email, Company_Service, Type_Service " + vbCr
                    strSql += ", Detail_Project, Detail_Service, SLA, isnull(SLA_File,'') 'SLA_File', MTTR, Monitor_Date, Monitor_Time, Fine, isnull(Fine_File,'') 'Fine_File', isnull(Doc_File,'') 'Doc_File', id_List " + vbCr

                    strSql += ", Special_Price, Upload_Project, isnull(Project_File,'') 'Project_File' " + vbCr
                    strSql += ", Contract, Monthly, OneTimePayment, MonthlySummary, OneTimeSummary, TotalProject "
                    strSql += ", Marketing, EntertainCustomer, Gift, Penalty, PenaltyLate  " + vbCr
                    strSql += ", TotalMarketing, TotalEntertainCustomer, TotalGift, TotalPenalty, TotalPenaltyLate  " + vbCr

                    strSql += ", ContractGuarantee, SpecialApprove  " + vbCr
                    strSql += "from List_ProjectName where Document_No = '" + xRequest_id + "' -- and CreateBy = '" + Session("uemail") + "' "
                    DT_List = C.GetDataTable(strSql)
                    If DT_List.Rows.Count > 0 Then

                        hide_user.Value = Session("uemail")
                        hide_user_permission.Value = Session("Login_permission")
                        hide_doc_no.Value = DT_List.Rows(0).Item("id_List")
                        Dim table_tmpcasst As String = ""
                        table_tmpcasst = "<table id='table_tmpcasst' class='table table-sm dataTable w-100 d-block d-md-table'>"
                        table_tmpcasst += "<thead class='bg-mint text-left text-white'>"
                        table_tmpcasst += "<tr>"
                        table_tmpcasst += "<th>ID</th>"
                        table_tmpcasst += "<th>Name</th>"
                        table_tmpcasst += "<th>Email</th>"
                        table_tmpcasst += "<th>Area</th>"
                        table_tmpcasst += "<th>Cluster</th>"
                        table_tmpcasst += "<th>Cluster Approved by</th>"
                        table_tmpcasst += "</tr>"
                        table_tmpcasst += "</thead>"
                        table_tmpcasst += "</table>"
                        data_tabletmpcasst.InnerHtml = table_tmpcasst

                        Dim table_tmpct As String = ""
                        table_tmpct += "<table id='table_tmpct' class='table table-sm dataTable w-100 d-block d-md-table'>"
                        table_tmpct += "<thead class='bg-mint text-left text-white'>"
                        table_tmpct += "<tr>"
                        table_tmpct += "<th>Name</th>"
                        table_tmpct += "<th>Tel</th>"
                        table_tmpct += "<th>Email</th>"
                        table_tmpct += "<th></th>"
                        table_tmpct += "</tr>"
                        table_tmpct += "</thead>"
                        table_tmpct += "</table>"
                        data_tabletmpct.InnerHtml = table_tmpct

                        Dim strRO As String
                        Dim strCluster As String
                        Dim strcluster_name As String

                        Dim strCustomerType As String
                        Dim strMonitorDate As String
                        Dim strMonitorTime As String

                        strCustomerType = "select CustomerType_name from dbo.CustomerType where Status = '1' order by CustomerType_order "
                        C.SetDropDownList(ddlCustomerType, strCustomerType, "CustomerType_name", "CustomerType_name", "(โปรดเลือก)")

                        'strRO = "select distinct RO from Cluster where Status = '1' order by RO "
                        'C.SetDropDownList(ddlArea, strRO, "RO", "RO", "(โปรดเลือก)")

                        strMonitorDate = "select MonitorDate_name from dbo.MonitorDate where Status = '1' order by MonitorDate_order "
                        C.SetDropDownList(ddlMonitorDate, strMonitorDate, "MonitorDate_name", "MonitorDate_name", "(โปรดเลือก)")

                        strMonitorTime = "select MonitorTime_name from dbo.MonitorTime where Status = '1' order by MonitorTime_order "
                        C.SetDropDownList(ddlMonitorTime, strMonitorTime, "MonitorTime_name", "MonitorTime_name", "(โปรดเลือก)")

                        txtProjectName.Text = DT_List.Rows(0).Item("Project_Name").ToString
                        txtLocationName.Text = DT_List.Rows(0).Item("Location_Name").ToString
                        txtProjectCode.Text = DT_List.Rows(0).Item("Project_Code").ToString
                        txtCustomerName.Text = DT_List.Rows(0).Item("Customer_Name").ToString
                        'txtEnterprise.Text = DT_List.Rows(0).Item("Enterprise_Name").ToString
                        ddlCustomerType.SelectedValue = DT_List.Rows(0).Item("Customer_Type").ToString
                        txtDocumentDate.Text = DT_List.Rows(0).Item("Document_Date").ToString
                        txtServiceDate.Text = DT_List.Rows(0).Item("Service_Date").ToString
                        'txtCustomerAssistantName.Text = DT_List.Rows(0).Item("Customer_Assistant_Name").ToString
                        'txtCustomerAssistantID.Text = DT_List.Rows(0).Item("Customer_Assistant_ID").ToString
                        'ddlArea.SelectedValue = DT_List.Rows(0).Item("Area")

                        'strCluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
                        'C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster", "(โปรดเลือก)")
                        'ddlCluster.SelectedValue = DT_List.Rows(0).Item("Cluster")

                        'strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
                        'C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
                        'ddlClusterName.SelectedValue = DT_List.Rows(0).Item("Cluster_email")

                        'txtCustomerContactName.Text = DT_List.Rows(0).Item("Customer_Contact_Name").ToString
                        'txtCustomerContactTel.Text = DT_List.Rows(0).Item("Customer_Contact_Tel").ToString
                        'txtCustomerContactEmail.Text = DT_List.Rows(0).Item("Customer_Contact_Email").ToString

                        If DT_List.Rows(0).Item("Company_Service").ToString = rbtTTTi.Text Then
                            rbtTTTi.Checked = True
                        ElseIf DT_List.Rows(0).Item("Company_Service").ToString = rbtTTTBB.Text Then
                            rbtTTTi.Checked = False
                            rbtTTTBB.Checked = True
                        Else
                            rbtOtherCompany.Checked = True
                            txtOtherCompany.Enabled = True
                            txtOtherCompany.Text = DT_List.Rows(0).Item("Company_Service").ToString
                        End If

                        If DT_List.Rows(0).Item("Type_Service").ToString = RadioButton1.Text Then
                            RadioButton1.Checked = True
                        ElseIf DT_List.Rows(0).Item("Type_Service").ToString = RadioButton2.Text Then
                            RadioButton2.Checked = True
                        ElseIf DT_List.Rows(0).Item("Type_Service").ToString = RadioButton3.Text Then
                            RadioButton3.Checked = True
                        Else
                            rbtOther.Checked = True
                            txtOther.Enabled = True
                            txtOther.Text = DT_List.Rows(0).Item("Type_Service").ToString
                        End If

                        If DT_List.Rows(0).Item("Special_Price") Then
                            rbtSpecialYes.Checked = True
                        Else
                            rbtSpecialNo.Checked = True
                        End If

                        If DT_List.Rows(0).Item("SpecialApprove") Then
                            chkSpecialApprove.Checked = True
                        Else
                            chkSpecialApprove.Checked = False
                        End If

                        txtDetailProject.Text = DT_List.Rows(0).Item("Detail_Project").ToString
                        txtDetailService.Text = DT_List.Rows(0).Item("Detail_Service").ToString
                        txtSLA.Text = DT_List.Rows(0).Item("SLA").ToString
                        txtMTTR.Text = DT_List.Rows(0).Item("MTTR").ToString
                        txtFine.Text = DT_List.Rows(0).Item("Fine").ToString
                        ddlMonitorDate.SelectedValue = DT_List.Rows(0).Item("Monitor_Date").ToString
                        ddlMonitorTime.SelectedValue = DT_List.Rows(0).Item("Monitor_Time").ToString

                        txtContract.Text = DT_List.Rows(0).Item("Contract").ToString
                        txtInputPrice.Text = DT_List.Rows(0).Item("Monthly").ToString
                        txtOneTimePayment.Text = DT_List.Rows(0).Item("OneTimePayment").ToString
                        txtMarketing.Text = DT_List.Rows(0).Item("Marketing").ToString
                        txtEntertainCustomer.Text = DT_List.Rows(0).Item("EntertainCustomer").ToString
                        txtGift.Text = DT_List.Rows(0).Item("Gift").ToString
                        txtPenalty.Text = DT_List.Rows(0).Item("Penalty").ToString
                        txtPenaltyLate.Text = DT_List.Rows(0).Item("PenaltyLate").ToString

                        txtContractGuarantee.Text = DT_List.Rows(0).Item("ContractGuarantee").ToString

                        CalculateSellingPrice()

                        'If DT_List.Rows(0).Item("SLA_File") <> "" Then
                        '    div_UploadSLA.Visible = True
                        '    div_OpenSLA.Visible = True
                        '    LinkFileSLA.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("SLA_File")
                        '    LinkFileSLA.Target = "_blank"
                        '    chkSLA.Text = "แก้ไขไฟล์แนบ SLA (PDF เท่านั้น)"
                        'Else
                        '    div_UploadSLA.Visible = True
                        '    div_OpenSLA.Visible = False
                        '    chkSLA.Text = "เพิ่มไฟล์แนบ SLA (PDF เท่านั้น)"
                        'End If

                        'If DT_List.Rows(0).Item("Fine_File") <> "" Then
                        '    div_UploadFine.Visible = True
                        '    div_OpenFine.Visible = True
                        '    LinkFileFine.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("Fine_File")
                        '    LinkFileFine.Target = "_blank"
                        '    chkFine.Text = "แก้ไขไฟล์แนบค่าปรับ (PDF เท่านั้น)"
                        'Else
                        '    div_UploadFine.Visible = True
                        '    div_OpenFine.Visible = False
                        '    chkFine.Text = "เพิ่มไฟล์แนบค่าปรับ (PDF เท่านั้น)"
                        'End If

                        If DT_List.Rows(0).Item("Doc_File") <> "" Then
                            Dim sp_Doc() As String
                            sp_Doc = Split(DT_List.Rows(0).Item("Doc_File"), "\")
                            If sp_Doc.Length > 0 Then
                                LinkFileDoc.Text = "<i class='fas fa-file-alt'></i> " + sp_Doc(sp_Doc.Length - 1)
                            End If
                            div_UploadDoc.Visible = True
                            div_OpenDoc.Visible = True
                            LinkFileDoc.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("Doc_File")
                            LinkFileDoc.Target = "_blank"
                            chkDoc.Text = "แก้ไขไฟล์เอกสารแนบ (pdf, zip หรือ rar เท่านั้น)"
                        Else
                            div_UploadDoc.Visible = True
                            div_OpenDoc.Visible = False
                            chkDoc.Text = "เพิ่มไฟล์เอกสารแนบ (pdf, zip หรือ rar เท่านั้น)"
                        End If

                        If dt_check.Rows(0).Item("Upload_Project") = "1" Then
                            If DT_List.Rows(0).Item("Project_File") <> "" Then
                                Dim sp_Doc() As String
                                sp_Doc = Split(DT_List.Rows(0).Item("Project_File"), "\")
                                If sp_Doc.Length > 0 Then
                                    LinkFileDoc.Text = sp_Doc(sp_Doc.Length - 1)
                                End If
                                div_UploadProject.Visible = False
                                div_OpenProject.Visible = True
                                LinkFileProject.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("Project_File")
                                LinkFileProject.Target = "_blank"
                            Else
                                div_UploadProject.Visible = True
                                div_OpenProject.Visible = False
                            End If
                        End If

                    End If

                Else
                    btnSave.Visible = False
                    menu_upload.Visible = False
                    alert = "AlertNotification('User ไม่มีสิทธิ์ ในการแก้ไขข้อมูลได้!',"
                    alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                    alert += "});"
                    ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
                End If
            Else
                btnSave.Visible = False
                menu_upload.Visible = False
                alert = "AlertNotification('โปรเจค " & xRequest_id & " ไม่อยู่ในสถานะ ที่สามารถแก้ไขข้อมูลได้!',"
                alert += "function(){ focus();window.location.href='edit_list.aspx?menu=edit';"
                alert += "});"
                ClientScript.RegisterStartupScript(Page.GetType, "alert", alert, True)
            End If
        Else
            SetUploadFile()
        End If
    End Sub

    ' Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
    '     Dim strcluster As String
    '     Dim strcluster_name As String
    '     strcluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
    '     C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster", "(โปรดเลือก)")
    '     strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
    '     C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
    ' End Sub

    Public Sub CheckTypeOfService()
        If rbtOther.Checked = True Then
            txtOther.Enabled = True
        Else
            txtOther.Enabled = False
        End If
    End Sub

    Protected Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        CheckTypeOfService()
    End Sub

    Protected Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        CheckTypeOfService()
    End Sub

    Protected Sub RadioButton3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        CheckTypeOfService()
    End Sub

    Protected Sub rbtOther_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtOther.CheckedChanged
        CheckTypeOfService()
    End Sub

    Public Sub CheckCompany()
        If rbtOtherCompany.Checked = True Then
            txtOtherCompany.Enabled = True
        Else
            txtOtherCompany.Enabled = False
        End If
    End Sub

    Protected Sub rbtTTTBB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTTTBB.CheckedChanged
        CheckCompany()
    End Sub

    Protected Sub rbtTTTi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTTTi.CheckedChanged
        CheckCompany()
    End Sub

    Protected Sub rbtOtherCompany_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtOtherCompany.CheckedChanged
        CheckCompany()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CheckData("Save")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        CheckData("Next")
    End Sub

    Private Sub CheckData(ByVal SaveOrNext As String)

        If CheckUpload() = "1" And div_UploadProject.Visible = True Then
            If FileUploadProject.FileName <> "" Then
                If Path.GetExtension(FileUploadProject.FileName) <> ".xls" And Path.GetExtension(FileUploadProject.FileName) <> ".xlsx" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('Project File ต้องเป็น Excel เท่านั้น');", True)
                    FileUploadProject.Focus()
                    Exit Sub
                ElseIf FileUploadProject.PostedFile.ContentLength > 20971520 Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('Project File ขนาดต้องไม่เกิน 20 MB');", True)
                    FileUploadProject.Focus()
                    Exit Sub
                End If
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุแนบ Project File');", True)
                FileUploadProject.Focus()
                Exit Sub
            End If
        End If

        If Trim(txtProjectName.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Project Name');", True)
            txtProjectName.Focus()
        ElseIf txtProjectName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Project Name ได้"");focus();", True)
            txtProjectName.Focus()
        ElseIf txtLocationName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Ref.LocationName/Code: ได้"");focus();", True)
            txtLocationName.Focus()
            ' ElseIf Trim(txtCustomerName.Text) = "" Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ชื่อลูกค้า');", True)
            '     txtCustomerName.Focus()
            ' ElseIf txtCustomerName.Text.Contains("'") = True Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล ชื่อลูกค้า ได้"");focus();", True)
            '     txtCustomerName.Focus()
        ElseIf txtProjectCode.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Project Code ได้"");focus();", True)
            txtProjectCode.Focus()
        ElseIf ddlCustomerType.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ ประเภทลูกค้า');focus();", True)
            ddlCustomerType.Focus()
        ElseIf txtDocumentDate.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ วันที่จัดทำ/ปรับปรุง');", True)
            txtDocumentDate.Focus()
        ElseIf txtServiceDate.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ วันที่เริ่มให้บริการ');", True)
            txtServiceDate.Focus()
            ' ElseIf Trim(txtCustomerAssistantName.Text) = "" Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Customer Assistant Name');", True)
            '     txtCustomerAssistantName.Focus()
            ' ElseIf txtCustomerAssistantName.Text.Contains("'") = True Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Customer Assistant Name ได้"");focus();", True)
            '     txtCustomerAssistantName.Focus()
            ' ElseIf Trim(txtCustomerAssistantID.Text) = "" Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Customer Assistant ID');", True)
            '     txtCustomerAssistantID.Focus()
            ' ElseIf txtCustomerAssistantID.Text.Contains("'") = True Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Customer Assistant ID ได้"");focus();", True)
            '     txtCustomerAssistantID.Focus()
            ' ElseIf ddlArea.SelectedIndex = 0 Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Area');focus();", True)
            '     ddlArea.Focus()
            ' ElseIf ddlCluster.SelectedIndex = 0 Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Cluster');focus();", True)
            '     ddlCluster.Focus()
            ' ElseIf ddlClusterName.SelectedIndex = 0 Then
            '     ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Prepared by');focus();", True)
            '     ddlClusterName.Focus()
        ElseIf getTable_List_CT().Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Customer Contact');", True)
            txtCustomerContactName.Focus()
        ElseIf rbtOtherCompany.Checked = True And Trim(txtOtherCompany.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Company');", True)
            txtOtherCompany.Focus()
        ElseIf rbtOtherCompany.Checked = True And txtOtherCompany.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Company ได้"");focus();", True)
            txtOtherCompany.Focus()

        ElseIf rbtOther.Checked = True And Trim(txtOther.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Type Of Service');", True)
            txtOther.Focus()
        ElseIf rbtOther.Checked = True And txtOther.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Type Of Service ได้"");focus();", True)
            txtOther.Focus()

        ElseIf IsNumeric(txtContract.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Contract Period เป็นตัวเลขเท่านั้น!');", True)
            txtContract.Focus()
        ElseIf IsNumeric(txtInputPrice.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Monthly (ราคาขาย) เป็นตัวเลขเท่านั้น!');", True)
            txtInputPrice.Focus()
        ElseIf IsNumeric(txtOneTimePayment.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
            txtOneTimePayment.Focus()
        ElseIf IsNumeric(txtContractGuarantee.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ หลักประกันสัญญา เป็นตัวเลขเท่านั้น!');", True)
            txtContractGuarantee.Focus()
        ElseIf IsNumeric(txtMarketing.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าการตลาด เป็นตัวเลขเท่านั้น!');", True)
            txtMarketing.Focus()
        ElseIf IsNumeric(txtEntertainCustomer.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าเลี้ยงรับรองลูกค้า เป็นตัวเลขเท่านั้น!');", True)
            txtEntertainCustomer.Focus()
        ElseIf IsNumeric(txtPenalty.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าปรับงานซ่อม เป็นตัวเลขเท่านั้น!');", True)
            txtPenalty.Focus()
        ElseIf IsNumeric(txtPenaltyLate.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าปรับส่งมอบล่าช้า เป็นตัวเลขเท่านั้น!');", True)
            txtPenaltyLate.Focus()
        ElseIf IsNumeric(txtGift.Text) = False Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าของขวัญ เป็นตัวเลขเท่านั้น!');", True)
            txtGift.Focus()

        ElseIf txtDetailService.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล รายละเอียดโครงการ ได้"");focus();", True)
            txtDetailService.Focus()
        ElseIf txtSLA.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล SLA ได้"");focus();", True)
            txtSLA.Focus()
        ElseIf txtFine.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล ค่าปรับ ได้"");focus();", True)
            txtFine.Focus()

        ElseIf ddlMonitorDate.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Monitor Date');focus();", True)
            ddlMonitorDate.Focus()
        ElseIf ddlMonitorTime.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ Monitor Time');focus();", True)
            ddlMonitorTime.Focus()

            'ElseIf chkSLA.Checked = True And FileUploadSLA.FileName = "" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ ไฟล์แนบ SLA');", True)
            '    FileUploadSLA.Focus()
            'ElseIf chkSLA.Checked = True And Path.GetExtension(FileUploadSLA.FileName) <> ".pdf" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์แนบ SLA ต้องเป็น PDF เท่านั้น');", True)
            '    FileUploadSLA.Focus()
            'ElseIf chkFine.Checked = True And FileUploadFine.FileName = "" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ ไฟล์แนบค่าปรับ');", True)
            '    FileUploadFine.Focus()
            'ElseIf chkFine.Checked = True And Path.GetExtension(FileUploadFine.FileName) <> ".pdf" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์แนบค่าปรับ ต้องเป็น PDF เท่านั้น');", True)
            '    FileUploadFine.Focus()

        ElseIf chkDoc.Checked = True And FileUploadDoc.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ ไฟล์เอกสารแนบ');", True)
            FileUploadDoc.Focus()
        ElseIf chkDoc.Checked = True And Path.GetExtension(FileUploadDoc.FileName) <> ".pdf" And Path.GetExtension(FileUploadDoc.FileName) <> ".zip" And Path.GetExtension(FileUploadDoc.FileName) <> ".rar" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์เอกสารแนบ ต้องเป็น pdf, zip หรือ rar เท่านั้น');", True)
            FileUploadDoc.Focus()
        Else
            If chkDoc.Checked = True Then
                If FileUploadDoc.PostedFile.ContentLength > 20971520 Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('ไฟล์เอกสารแนบ ขนาดต้องไม่เกิน 20 MB');", True)
                    FileUploadDoc.Focus()
                    Exit Sub
                End If
            End If
            SaveData(SaveOrNext)
        End If
    End Sub

    Public Sub SaveData(ByVal SaveOrNext As String)
       
        Dim FileNameProject As String = ""
        Dim FileNameDoc As String = ""
        Dim SpecialPrice As Integer
        Dim SpecialApprove As Integer
        Dim UploadProject As Integer
        Dim doc_no_Doc As String = "DOC_" + xRequest_id + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00")
        Dim doc_no_Project As String = "Project_" + xRequest_id + "_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00")
        Dim vTempPath As String = Server.MapPath("Upload\")
        Dim vUploadPathDoc As String = ""
        Dim vUploadPathProject As String = ""

        If CheckUpload() = "1" Then
            UploadProject = "1"
            If div_UploadProject.Visible = True Then
                vUploadPathProject = "ProjectFile"
                vUploadPathProject &= "\" & DateTime.Now.ToString("yyyy")
                vUploadPathProject &= "\" & DateTime.Now.ToString("MM")
                If Not Directory.Exists(vTempPath & vUploadPathProject) Then
                    Directory.CreateDirectory(vTempPath & vUploadPathProject)
                End If
                vUploadPathProject &= "\"
                If FileUploadProject.HasFile = True Then
                    Dim CurrentFileName As String
                    Dim CurrentPath As String

                    CurrentFileName = FileUploadProject.FileName
                    CurrentPath = Request.PhysicalApplicationPath
                    CurrentPath += "Upload\"
                    CurrentPath += CurrentFileName
                    FileUploadProject.SaveAs(CurrentPath)
                    Dim file_extProject As String
                    file_extProject = Path.GetExtension(FileUploadProject.FileName)
                    Dim TheFileProject As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
                    FileNameProject = doc_no_Project + file_extProject
                    If TheFileProject.Exists Then
                        If System.IO.File.Exists(vTempPath & vUploadPathProject & FileNameProject) Then
                            System.IO.File.Move(vTempPath & vUploadPathProject & FileNameProject, vTempPath & vUploadPathProject & doc_no_Project + "_old" + file_extProject)
                        End If
                        System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, vTempPath & vUploadPathProject & FileNameProject)
                    Else
                        Throw New FileNotFoundException()
                    End If
                Else
                    FileNameProject = ""
                End If
                If FileNameProject = "" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุแนบ Project File');", True)
                    FileUploadProject.Focus()
                    Exit Sub
                End If
            End If
        Else
            UploadProject = "0"
        End If

        If chkDoc.Checked = True Then
            vUploadPathDoc = "Doc"
            vUploadPathDoc &= "\" & DateTime.Now.ToString("yyyy")
            vUploadPathDoc &= "\" & DateTime.Now.ToString("MM")
            If Not Directory.Exists(vTempPath & vUploadPathDoc) Then
                Directory.CreateDirectory(vTempPath & vUploadPathDoc)
            End If
            vUploadPathDoc &= "\"

            If FileUploadDoc.HasFile = True Then
                Dim CurrentFileName As String
                Dim CurrentPath As String

                CurrentFileName = FileUploadDoc.FileName
                CurrentPath = Request.PhysicalApplicationPath
                CurrentPath += "Upload\"
                CurrentPath += CurrentFileName
                FileUploadDoc.SaveAs(CurrentPath)
                Dim file_extDoc As String
                file_extDoc = Path.GetExtension(FileUploadDoc.FileName)
                Dim TheFileDoc As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
                FileNameDoc = doc_no_Doc + file_extDoc
                If TheFileDoc.Exists Then
                    If System.IO.File.Exists(vTempPath & vUploadPathDoc & FileNameDoc) Then
                        System.IO.File.Move(vTempPath & vUploadPathDoc & FileNameDoc, vTempPath & vUploadPathDoc & doc_no_Doc + "_old" + file_extDoc)
                    End If
                    System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, vTempPath & vUploadPathDoc & FileNameDoc)
                Else
                    Throw New FileNotFoundException()
                End If
            Else
                FileNameDoc = ""
            End If
            If FileNameDoc = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณาระบุ ไฟล์เอกสารแนบ');", True)
                FileUploadDoc.Focus()
                Exit Sub
            End If
        End If



        Dim strSql As String

        Dim TypeOfService As String
        If RadioButton1.Checked = True Then
            TypeOfService = RadioButton1.Text
        ElseIf RadioButton2.Checked = True Then
            TypeOfService = RadioButton2.Text
        ElseIf RadioButton3.Checked = True Then
            TypeOfService = RadioButton3.Text
        Else
            TypeOfService = txtOther.Text
        End If

        Dim Company As String
        If rbtTTTBB.Checked = True Then
            Company = rbtTTTBB.Text
        ElseIf rbtTTTi.Checked = True Then
            Company = rbtTTTi.Text
        Else
            Company = txtOtherCompany.Text
        End If

        If rbtSpecialYes.Checked = True Then
            SpecialPrice = 1
        Else
            SpecialPrice = 0
        End If

        If chkSpecialApprove.Checked = True Then
            SpecialApprove = 1
        Else
            SpecialApprove = 0
        End If

        Try
            strSql = "Update List_ProjectName set Project_Name = '" & txtProjectName.Text & "', Project_Code = '" & txtProjectCode.Text & "', Customer_Name = '" & txtCustomerName.Text & "' " + vbCr
            strSql += ", Enterprise_Name = '', Location_Name = '" & txtLocationName.Text & "', Customer_Type = '" & ddlCustomerType.SelectedValue & "', Document_Date = '" & C.CDateText(txtDocumentDate.Text) & "' " + vbCr
            strSql += ", Service_Date = '" & C.CDateText(txtServiceDate.Text) & "' "
            'strSql += ", Customer_Assistant_Name = '" & txtCustomerAssistantName.Text & "', Customer_Assistant_ID = '" & txtCustomerAssistantID.Text & "' " + vbCr
            'strSql += ", Area = '" & ddlArea.SelectedValue & "', Cluster = '" & ddlCluster.SelectedValue & "', Cluster_name = '" & ddlClusterName.SelectedItem.Text & "', Cluster_email = '" & ddlClusterName.SelectedValue & "' " + vbCr
            'strSql += ", Customer_Contact_Name = '" & txtCustomerContactName.Text & "', Customer_Contact_Tel = '" & txtCustomerContactTel.Text & "', Customer_Contact_Email = '" & txtCustomerContactEmail.Text & "' " + vbCr
            strSql += ", Company_Service = '" & Company.ToString & "', Special_Price = '" & SpecialPrice & "', Upload_Project = '" & UploadProject & "' " + vbCr
            strSql += ", Type_Service = '" & TypeOfService.ToString & "', Detail_Project = '" & txtDetailProject.Text & "', Detail_Service = '" & txtDetailService.Text & "', SLA = '" & txtSLA.Text & "', MTTR = '" & txtMTTR.Text & "', Monitor_Date = '" & ddlMonitorDate.SelectedValue & "' " + vbCr
            strSql += ", Monitor_Time = '" & ddlMonitorTime.SelectedValue & "', Fine = '" & txtFine.Text & "', SpecialApprove = '" & SpecialApprove & "', UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() " + vbCr
            'If chkSLA.Checked = True Then
            '    strSql += ", SLA_File = '" & vUploadPathSLA & FileNameSLA.ToString & "' "
            'End If
            'If chkDeleteSLA.Checked = True Then
            '    strSql += ", SLA_File = '' "
            'End If
            'If chkFine.Checked = True Then
            '    strSql += ", Fine_File = '" & vUploadPathFine & FileNameFine.ToString & "' "
            'End If
            'If chkDeleteFine.Checked = True Then
            '    strSql += ", Fine_File = '' "
            'End If

            If CheckUpload() = "1" Then
                If div_UploadProject.Visible = True Then
                    strSql += ", Project_File = '" & vUploadPathProject & FileNameProject.ToString & "' " + vbCr
                ElseIf chkDeleteProject.Checked = True Then
                    strSql += ", Project_File = '' " + vbCr
                End If
            End If

            If chkDoc.Checked = True Then
                strSql += ", Doc_File = '" & vUploadPathDoc & FileNameDoc.ToString & "' "
            End If
            If chkDeleteDoc.Checked = True Then
                strSql += ", Doc_File = '' "
            End If

            strSql += ", Contract = '" & CDbl(txtContract.Text) & "', Monthly='" + Format(CDbl(txtInputPrice.Text), "#0.00") + "', OneTimePayment = '" + Format(CDbl(txtOneTimePayment.Text), "#0.00") + "', MonthlySummary = '" + Format(CDbl(lblMonthlyPrice.Text), "#0.00") + "', OneTimeSummary = '" + Format(CDbl(lblOneTimePayment.Text), "#0.00") + "', TotalProject = '" + Format(CDbl(lblTotalYearly.Text), "#0.00") + "' " + vbCr
            strSql += ", Marketing = '" + Format(CDbl(txtMarketing.Text), "#0.00") + "', EntertainCustomer = '" + Format(CDbl(txtEntertainCustomer.Text), "#0.00") + "', Gift = '" + Format(CDbl(txtGift.Text), "#0.00") + "', Penalty = '" + Format(CDbl(txtPenalty.Text), "#0.00") + "', PenaltyLate = '" + Format(CDbl(txtPenaltyLate.Text), "#0.00") + "' " + vbCr
            strSql += ", TotalMarketing = '" + Format(CDbl(lblTotalMarketing.Text), "#0.00") + "', TotalEntertainCustomer = '" + Format(CDbl(lblTotalEntertainCustomer.Text), "#0.00") + "', TotalGift = '" + Format(CDbl(lblTotalGift.Text), "#0.00") + "', TotalPenalty = '" + Format(CDbl(lblTotalPenalty.Text), "#0.00") + "', TotalPenaltyLate = '" + Format(CDbl(lblTotalPenaltyLate.Text), "#0.00") + "' " + vbCr
            strSql += ", ContractGuarantee = '" & Format(CDbl(txtContractGuarantee.Text), "#0.00") & "' " + vbCr
            strSql += "where Document_No = '" + xRequest_id + "' -- and CreateBy='" + Session("uemail") + "' "
            C.ExecuteNonQuery(strSql)

            'Dim DT_ct As New DataTable
            'DT_ct = C.GetDataTable("select * from tmpList_CT where ProjectName_id = '" + hide_doc_no.Value + "' order by id_List  ")
            'If DT_ct.Rows.Count > 0 Then
            '    C.ExecuteNonQuery("Update List_ProjectName set Customer_Contact_Name = '" & DT_ct.Rows(0).Item("Customer_Contact_Name") & "', Customer_Contact_Tel = '" & DT_ct.Rows(0).Item("Customer_Contact_Tel") & "', Customer_Contact_Email = '" & DT_ct.Rows(0).Item("Customer_Contact_Email") & "'  where id_List = '" & hide_doc_no.Value & "'")
            'End If

            strSql = "Update FeasibilityDocument set UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() where Document_No = '" + xRequest_id + "' "
            C.ExecuteNonQuery(strSql)
            If SaveOrNext = "Next" Then
                If CheckUpload() = "1" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'Summary_Upload.aspx?request_id=" & xRequest_id & "'; });", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'edit_service.aspx?request_id=" & xRequest_id & "'; });", True)
                End If
            Else
                If CheckUpload() = "1" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'edit_project_name.aspx?request_id=" & xRequest_id & "'; });", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('อัพเดทข้อมูลได้สำเร็จ', function(){ window.location = 'edit_project_name.aspx?request_id=" & xRequest_id & "'; });", True)
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('ไม่สามารถอัพเดทข้อมูลได้');", True)
        End Try
        
    End Sub

    Function getTable_List_CT() As DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim DT_List As New DataTable
        
        strSql = "select * from tmpList_CT where /*CreateBy='" + hide_user.Value + "' and*/  isnull(ProjectName_id, '') = '" & hide_doc_no.Value & "' "
        DT = C.GetDataTable(strSql)
        
        Return DT
    End Function

    ' Protected Sub ddlCluster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCluster.SelectedIndexChanged
    '     Dim strcluster_name As String
    '     strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
    '     C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email")
    ' End Sub

    Protected Sub chkSLA_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSLA.CheckedChanged
        If chkSLA.Checked = True Then
            FileUploadSLA.Visible = True
        Else
            FileUploadSLA.Visible = False
        End If
    End Sub

    Protected Sub chkFine_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFine.CheckedChanged
        If chkFine.Checked = True Then
            FileUploadFine.Visible = True
        Else
            FileUploadFine.Visible = False
        End If
    End Sub

    Protected Sub chkDoc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDoc.CheckedChanged
        If chkDoc.Checked = True Then
            FileUploadDoc.Visible = True
            chkDeleteDoc.Checked = False
        Else
            FileUploadDoc.Visible = False
        End If
    End Sub

    Protected Sub chkDeleteDoc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeleteDoc.CheckedChanged
        If chkDeleteDoc.Checked = True Then
            chkDoc.Checked = False
            FileUploadDoc.Visible = False
        End If
    End Sub

    Public Sub CalculateSellingPrice()
        txtInputPrice.Text = Format(Math.Round(CDbl(txtInputPrice.Text), 2), "#0.00")
        txtOneTimePayment.Text = Format(Math.Round(CDbl(txtOneTimePayment.Text), 2), "#0.00")
        lblMonthlyPrice.Text = Format(CDbl(txtInputPrice.Text), "###,##0.00")
        lblOneTimePayment.Text = Format(CDbl(txtOneTimePayment.Text), "###,##0.00")
        lblTotalYearly.Text = Format(CDbl(txtInputPrice.Text * txtContract.Text) + CDbl(txtOneTimePayment.Text), "###,##0.00")

        Dim RevenueOne As Double
        Dim RevenueNotOne As Double
        RevenueOne = CDbl(txtInputPrice.Text) + CDbl(txtOneTimePayment.Text)
        RevenueNotOne = CDbl(txtInputPrice.Text)

        lblTotalMarketing.Text = Format(((CDbl(txtMarketing.Text) / 100) * RevenueOne) + ((CDbl(txtMarketing.Text) / 100) * RevenueNotOne * (CInt(txtContract.Text) - 1)), "###,##0.00")
        lblTotalEntertainCustomer.Text = Format(((CDbl(txtEntertainCustomer.Text) / 100) * RevenueOne) + ((CDbl(txtEntertainCustomer.Text) / 100) * RevenueNotOne * (CInt(txtContract.Text) - 1)), "###,##0.00")
        txtGift.Text = Format(Math.Round(CDbl(txtGift.Text), 2), "#0.00")
        lblTotalGift.Text = Format(CDbl(txtGift.Text), "###,##0.00")
        txtPenaltyLate.Text = Format(Math.Round(CDbl(txtPenaltyLate.Text), 2), "#0.00")
        lblTotalPenaltyLate.Text = Format(CDbl(txtPenaltyLate.Text), "###,##0.00")
        lblTotalPenalty.Text = Format(((CDbl(txtPenalty.Text) / 100) * RevenueOne) + ((CDbl(txtPenalty.Text) / 100) * RevenueNotOne * (CInt(txtContract.Text) - 1)), "###,##0.00")

        lblTotalContractGuarantee.Text = Format(CDbl(lblTotalYearly.Text) * (CDbl(txtContractGuarantee.Text) / 100), "###,##0.00")

        Dim DT_GetTableProjectName As New DataTable
        Dim DT_GetTableService As New DataTable
        Dim DT_CAPEX As New DataTable
        Dim DT_Service As New DataTable
        Dim sMenu As String

        If CheckUpload() = "1" Then
            sMenu = "EditUpload"
        Else
            sMenu = "Edit"
        End If
        DT_GetTableProjectName = Cal.GetTableService(Session("uemail"), sMenu, xRequest_id)
        DT_GetTableService = Cal.GetTableService(Session("uemail"), sMenu, xRequest_id)

        DT_CAPEX = Cal.ShowCAPEX_OPEX(Session("uemail"), sMenu, xRequest_id, DT_GetTableService, ddlCustomerType.SelectedValue)
        If DT_CAPEX.Rows.Count > 0 Then
            DT_Service = Cal.ShowService(Session("uemail"), sMenu, xRequest_id, DT_GetTableProjectName, DT_GetTableService, CInt(txtContract.Text), CDbl(txtInputPrice.Text), CDbl(txtOneTimePayment.Text), CDbl(txtMarketing.Text), CDbl(txtEntertainCustomer.Text), CDbl(txtPenalty.Text), CDbl(txtPenaltyLate.Text), CDbl(txtGift.Text), DT_CAPEX.Rows(0).Item("TotalCapex"), DT_CAPEX.Rows(0).Item("TotalOpex"), DT_CAPEX.Rows(0).Item("TotalOther"), DT_CAPEX.Rows(0).Item("TotalManagement"))
        End If
        If DT_Service.Rows.Count > 0 Then
            lblPayback.Text = DT_Service.Rows(0).Item("Payback")
            lblMargin.Text = DT_Service.Rows(0).Item("Margin")
            lblNPV.Text = DT_Service.Rows(0).Item("NPV")
        End If
    End Sub

    Protected Sub txtContract_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContract.TextChanged
        If IsNumeric(txtContract.Text) Then
            txtContract.Text = CInt(txtContract.Text)
            If txtContract.Text < 0 Then
                txtContract.Text = txtContract.Text * (-1)
            End If
        Else
            txtContract.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Contract Period เป็นตัวเลขเท่านั้น!');", True)
            txtContract.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtInputPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInputPrice.TextChanged
        If IsNumeric(txtInputPrice.Text) Then
            'CalculateSellingPrice()
        Else
            txtInputPrice.Text = "0"
            lblTotalYearly.Text = "0.00"
            lblMonthlyPrice.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ Monthly (ราคาขาย) เป็นตัวเลขเท่านั้น!');", True)
            txtInputPrice.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtOneTimePayment_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOneTimePayment.TextChanged
        If IsNumeric(txtOneTimePayment.Text) Then
            'CalculateOneTimePayment()
            'CalculateSellingPrice()
        Else
            txtOneTimePayment.Text = "0"
            lblOneTimePayment.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
            txtOneTimePayment.Focus()
        End If
        CalculateSellingPrice()
    End Sub
    Protected Sub txtMarketing_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarketing.TextChanged
        If IsNumeric(txtMarketing.Text) Then

        Else
            txtMarketing.Text = "0"
            lblTotalMarketing.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าการตลาด เป็นตัวเลขเท่านั้น!');", True)
            txtMarketing.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtEntertainCustomer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEntertainCustomer.TextChanged
        If IsNumeric(txtEntertainCustomer.Text) Then

        Else
            txtEntertainCustomer.Text = "0"
            lblTotalEntertainCustomer.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าเลี้ยงรับรองลูกค้า เป็นตัวเลขเท่านั้น!');", True)
            txtEntertainCustomer.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtGift_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGift.TextChanged
        If IsNumeric(txtGift.Text) Then

        Else
            txtGift.Text = "0"
            lblTotalGift.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าของขวัญ เป็นตัวเลขเท่านั้น!');", True)
            txtGift.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtPenaltyLate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPenaltyLate.TextChanged
        If IsNumeric(txtPenaltyLate.Text) Then

        Else
            txtPenaltyLate.Text = "0"
            lblTotalPenaltyLate.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าปรับส่งมอบล่าช้า เป็นตัวเลขเท่านั้น!');", True)
            txtPenaltyLate.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtPenalty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPenalty.TextChanged
        If IsNumeric(txtPenalty.Text) Then

        Else
            txtPenalty.Text = "0"
            lblTotalPenalty.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ ค่าปรับงานซ่อม เป็นตัวเลขเท่านั้น!');", True)
            txtPenalty.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtContractGuarantee_TextChanged(sender As Object, e As EventArgs) Handles txtContractGuarantee.TextChanged
        If IsNumeric(txtContractGuarantee.Text) Then

        Else
            txtContractGuarantee.Text = "0"
            lblTotalContractGuarantee.Text = "0.00"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุ หลักประกันสัญญา เป็นตัวเลขเท่านั้น!');", True)
            txtContractGuarantee.Focus()
        End If
        CalculateSellingPrice()
    End Sub

    Public Function CheckUpload() As String
        Dim dt_check_upload As DataTable
        dt_check_upload = C.GetDataTable("select * from dbo.FeasibilityDocument where (request_status = 110) and Upload_Project = '1' and Document_No = '" + xRequest_id + "'")

        If dt_check_upload.Rows.Count > 0 Then
            Return "1"
        Else
            Return "0"
        End If
    End Function

    Public Sub SetUploadFile()
        If Session("FileUploadProjectName") Is Nothing AndAlso FileUploadProject.HasFile Then
            Session("FileUploadProjectName") = FileUploadProject
            lblFileUploadName.Text = FileUploadProject.FileName
            ' Case 2: On Next PostBack Session has value but FileUpload control is
            ' Blank due to PostBack then return the values from session to FileUpload as:
        ElseIf Session("FileUploadProjectName") IsNot Nothing AndAlso (Not FileUploadProject.HasFile) Then
            FileUploadProject = DirectCast(Session("FileUploadProjectName"), FileUpload)
            lblFileUploadName.Text = FileUploadProject.FileName
            ' Case 3: When there is value in Session but user want to change the file then
            ' In this case we need to change the file in session object also as:
        ElseIf FileUploadProject.HasFile Then
            Session("FileUploadProjectName") = FileUploadProject
            lblFileUploadName.Text = FileUploadProject.FileName
        End If
    End Sub

End Class
