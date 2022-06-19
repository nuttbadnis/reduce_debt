Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Collections.Generic

Partial Class project_nametest
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If

            Dim DT_List As DataTable
            Dim strSql As String

            Dim strRO As String
            Dim strCluster As String
            Dim strcluster_name As String

            Dim strCustomerType As String
            Dim strMonitorDate As String
            Dim strMonitorTime As String

            ShowEmployee()

            txtDocumentDate.Text = Now.Day.ToString("00") + "/" + Now.Month.ToString("00") + "/" + Now.Year.ToString("0000")

            strCustomerType = "select CustomerType_name from dbo.CustomerType where Status = '1' order by CustomerType_order "
            C.SetDropDownList(ddlCustomerType, strCustomerType, "CustomerType_name", "CustomerType_name", "(โปรดเลือก)")

            strRO = "select distinct RO from Cluster where Status = '1' order by RO "
            C.SetDropDownList(ddlArea, strRO, "RO", "RO", "(โปรดเลือก)")

            strMonitorDate = "select MonitorDate_name from dbo.MonitorDate where Status = '1' order by MonitorDate_order "
            C.SetDropDownList(ddlMonitorDate, strMonitorDate, "MonitorDate_name", "MonitorDate_name", "(โปรดเลือก)")

            strMonitorTime = "select MonitorTime_name from dbo.MonitorTime where Status = '1' order by MonitorTime_order "
            C.SetDropDownList(ddlMonitorTime, strMonitorTime, "MonitorTime_name", "MonitorTime_name", "(โปรดเลือก)")

            strSql = "select Project_Name, Project_Code, Customer_Name, Enterprise_Name, Location_Name, Customer_Type " + vbCr
            strSql += ", convert(varchar(10),Document_Date,103) 'Document_Date', convert(varchar(10),Service_Date,103) 'Service_Date' " + vbCr
            strSql += ", Customer_Assistant_Name, Customer_Assistant_ID, Area, Cluster, Cluster_name, Cluster_email " + vbCr
            strSql += ", Customer_Contact_Name, Customer_Contact_Tel, Customer_Contact_Email, Company_Service, Type_Service " + vbCr
            strSql += ", Detail_Service, SLA, isnull(SLA_File,'') 'SLA_File', Monitor_Date, Monitor_Time, Fine, isnull(Fine_File,'') 'Fine_File' " + vbCr
            strSql += "from List_ProjectName where CreateBy='" + Session("uemail") + "' and Document_No is null "
            DT_List = C.GetDataTable(strSql)
            If DT_List.Rows.Count > 0 Then
                txtProjectName.Text = DT_List.Rows(0).Item("Project_Name").ToString
                txtLocationName.Text = DT_List.Rows(0).Item("Location_Name").ToString
                txtProjectCode.Text = DT_List.Rows(0).Item("Project_Code").ToString
                txtCustomerName.Text = DT_List.Rows(0).Item("Customer_Name").ToString
                txtEnterprise.Text = DT_List.Rows(0).Item("Enterprise_Name").ToString
                ddlCustomerType.SelectedValue = DT_List.Rows(0).Item("Customer_Type").ToString
                txtDocumentDate.Text = DT_List.Rows(0).Item("Document_Date").ToString
                txtServiceDate.Text = DT_List.Rows(0).Item("Service_Date").ToString
                'txtCustomerAssistantName.Text = DT_List.Rows(0).Item("Customer_Assistant_Name").ToString
                'txtCustomerAssistantID.Text = DT_List.Rows(0).Item("Customer_Assistant_ID").ToString
                ddlArea.SelectedValue = DT_List.Rows(0).Item("Area").ToString

                strCluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
                C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster", "(โปรดเลือก)")
                ddlCluster.SelectedValue = DT_List.Rows(0).Item("Cluster").ToString

                strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
                C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
                ddlClusterName.SelectedValue = DT_List.Rows(0).Item("Cluster_email").ToString

                txtCustomerContactName.Text = DT_List.Rows(0).Item("Customer_Contact_Name").ToString
                txtCustomerContactTel.Text = DT_List.Rows(0).Item("Customer_Contact_Tel").ToString
                txtCustomerContactEmail.Text = DT_List.Rows(0).Item("Customer_Contact_Email").ToString

                If DT_List.Rows(0).Item("Company_Service").ToString = rbtTTTBB.Text Then
                    rbtTTTBB.Checked = True
                ElseIf DT_List.Rows(0).Item("Company_Service").ToString = rbtTTTi.Text Then
                    rbtTTTi.Checked = True
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

                txtDetailService.Text = DT_List.Rows(0).Item("Detail_Service").ToString
                txtSLA.Text = DT_List.Rows(0).Item("SLA").ToString
                txtFine.Text = DT_List.Rows(0).Item("Fine").ToString
                ddlMonitorDate.SelectedValue = DT_List.Rows(0).Item("Monitor_Date").ToString
                ddlMonitorTime.SelectedValue = DT_List.Rows(0).Item("Monitor_Time").ToString

                If DT_List.Rows(0).Item("SLA_File") <> "" Then
                    div_UploadSLA.Visible = False
                    div_OpenSLA.Visible = True
                    'LinkFileSLA.Visible = True
                    'chkSLA.Visible = False
                    'chkDeleteSLA.Visible = True
                    'FileUploadSLA.Visible = False
                    LinkFileSLA.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("SLA_File")
                    LinkFileSLA.Target = "_blank"
                Else
                    div_UploadSLA.Visible = True
                    div_OpenSLA.Visible = False
                    'LinkFileSLA.Visible = False
                    'chkSLA.Visible = True
                    'chkDeleteSLA.Visible = False
                End If

                If DT_List.Rows(0).Item("Fine_File") <> "" Then
                    div_UploadFine.Visible = False
                    div_OpenFine.Visible = True
                    LinkFileFine.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("Fine_File")
                    LinkFileFine.Target = "_blank"
                Else
                    div_UploadFine.Visible = True
                    div_OpenFine.Visible = False
                End If
            Else
                div_OpenSLA.Visible = False
                div_OpenFine.Visible = False

                strCluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
                C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster", "(โปรดเลือก)")

                strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster_name "
                C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
            End If
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Trim(txtProjectName.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Project Name');", True)
            txtProjectName.Focus()
        ElseIf txtProjectName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Project Name ได้"");focus();", True)
            txtProjectName.Focus()
        ElseIf txtLocationName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Ref.LocationName/Code: ได้"");focus();", True)
            txtLocationName.Focus()
        ElseIf Trim(txtCustomerName.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ชื่อลูกค้า');", True)
            txtCustomerName.Focus()
        ElseIf txtCustomerName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล ชื่อลูกค้า ได้"");focus();", True)
            txtCustomerName.Focus()
        ElseIf txtEnterprise.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล โครงการ ได้"");focus();", True)
            txtEnterprise.Focus()
        ElseIf ddlCustomerType.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ประเภทลูกค้า');focus();", True)
            ddlCustomerType.Focus()
        ElseIf txtDocumentDate.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ วันที่จัดทำ/ปรับปรุง');", True)
            txtDocumentDate.Focus()
        ElseIf txtServiceDate.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ วันที่เริ่มให้บริการ');", True)
            txtServiceDate.Focus()
        ElseIf Trim(txtCustomerAssistantName.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Customer Assistant Name');", True)
            txtCustomerAssistantName.Focus()
        ElseIf txtCustomerAssistantName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Customer Assistant Name ได้"");focus();", True)
            txtCustomerAssistantName.Focus()
        ElseIf Trim(txtCustomerAssistantID.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Customer Assistant ID');", True)
            txtCustomerAssistantID.Focus()
        ElseIf txtCustomerAssistantID.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Customer Assistant ID ได้"");focus();", True)
            txtCustomerAssistantID.Focus()
        ElseIf ddlArea.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Area');focus();", True)
            ddlArea.Focus()
        ElseIf ddlCluster.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Cluster');focus();", True)
            ddlCluster.Focus()
        ElseIf ddlClusterName.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Prepared by');focus();", True)
            ddlClusterName.Focus()

        ElseIf rbtOtherCompany.Checked = True And Trim(txtOtherCompany.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Company');", True)
            txtOtherCompany.Focus()
        ElseIf rbtOtherCompany.Checked = True And txtOtherCompany.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Company ได้"");focus();", True)
            txtOtherCompany.Focus()

        ElseIf rbtOther.Checked = True And Trim(txtOther.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Type Of Service');", True)
            txtOther.Focus()
        ElseIf rbtOther.Checked = True And txtOther.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Type Of Service ได้"");focus();", True)
            txtOther.Focus()

        ElseIf txtDetailService.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล รายละเอียดโครงการ ได้"");focus();", True)
            txtDetailService.Focus()
        ElseIf txtSLA.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล SLA ได้"");focus();", True)
            txtSLA.Focus()
        ElseIf txtFine.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล ค่าปรับ ได้"");focus();", True)
            txtFine.Focus()

        ElseIf ddlMonitorDate.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Monitor Date');focus();", True)
            ddlMonitorDate.Focus()
        ElseIf ddlMonitorTime.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ Monitor Time');focus();", True)
            ddlMonitorTime.Focus()

        ElseIf chkSLA.Checked = True And FileUploadSLA.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ไฟล์แนบ SLA');", True)
            FileUploadSLA.Focus()
        ElseIf chkSLA.Checked = True And Path.GetExtension(FileUploadSLA.FileName) <> ".pdf" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไฟล์แนบ SLA ต้องเป็น PDF เท่านั้น');", True)
            FileUploadSLA.Focus()
        ElseIf chkFine.Checked = True And FileUploadFine.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ไฟล์แนบค่าปรับ');", True)
            FileUploadFine.Focus()
        ElseIf chkFine.Checked = True And Path.GetExtension(FileUploadFine.FileName) <> ".pdf" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไฟล์แนบค่าปรับ ต้องเป็น PDF เท่านั้น');", True)
            FileUploadFine.Focus()
        Else
            SaveData()
        End If
    End Sub

    Public Sub SaveData()
        Dim FileNameSLA As String = ""
        Dim FileNameFine As String = ""
        If chkSLA.Checked = True Then
            If FileUploadSLA.HasFile = True Then
                Dim CurrentFileName As String
                Dim CurrentPath As String

                CurrentFileName = FileUploadSLA.FileName
                CurrentPath = Request.PhysicalApplicationPath
                CurrentPath += "Upload\"
                CurrentPath += CurrentFileName
                FileUploadSLA.SaveAs(CurrentPath)
                Dim file_extSLA As String
                file_extSLA = Path.GetExtension(FileUploadSLA.FileName)
                Dim TheFileSLA As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
                FileNameSLA = "SLA" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + "-" + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extSLA
                If TheFileSLA.Exists Then
                    If System.IO.File.Exists(MapPath(".") & "\Upload\" & FileNameSLA) Then
                        System.IO.File.Delete(MapPath(".") & "\Upload\" & FileNameSLA)
                    End If
                    System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\" & FileNameSLA)
                Else
                    Throw New FileNotFoundException()
                End If
            Else
                FileNameSLA = ""
            End If
            If FileNameSLA = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ไฟล์แนบ SLA');", True)
                FileUploadSLA.Focus()
                Exit Sub
            End If
        End If

        If chkFine.Checked = True Then
            If FileUploadFine.HasFile = True Then
                Dim CurrentFileName As String
                Dim CurrentPath As String

                CurrentFileName = FileUploadFine.FileName
                CurrentPath = Request.PhysicalApplicationPath
                CurrentPath += "Upload\"
                CurrentPath += CurrentFileName
                FileUploadFine.SaveAs(CurrentPath)
                Dim file_extFine As String
                file_extFine = Path.GetExtension(FileUploadFine.FileName)
                Dim TheFileFine As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
                FileNameFine = "Fine" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + "-" + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extFine
                If TheFileFine.Exists Then
                    If System.IO.File.Exists(MapPath(".") & "\Upload\" & FileNameFine) Then
                        System.IO.File.Delete(MapPath(".") & "\Upload\" & FileNameFine)
                    End If
                    System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\" & FileNameFine)
                Else
                    Throw New FileNotFoundException()
                End If
            Else
                FileNameFine = ""
            End If
            If FileNameFine = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "alert('กรุณาระบุ ไฟล์แนบค่าปรับ');", True)
                FileUploadFine.Focus()
                Exit Sub
            End If
        End If


        Dim strSql As String
        Dim DT_List As New DataTable

        strSql = "select * from List_ProjectName where CreateBy='" + Session("uemail") + "' and Document_No is null "
        DT_List = C.GetDataTable(strSql)

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

        If DT_List.Rows.Count > 0 Then
            strSql = "Update List_ProjectName set Project_Name = '" & txtProjectName.Text & "', Project_Code = '" & txtProjectCode.Text & "', Customer_Name = '" & txtCustomerName.Text & "' " + vbCr
            strSql += ", Enterprise_Name = '" & txtEnterprise.Text & "', Location_Name = '" & txtLocationName.Text & "', Customer_Type = '" & ddlCustomerType.SelectedValue & "', Document_Date = '" & C.CDateText(txtDocumentDate.Text) & "' " + vbCr
            strSql += ", Service_Date = '" & C.CDateText(txtServiceDate.Text) & "', Customer_Assistant_Name = '" & txtCustomerAssistantName.Text & "', Customer_Assistant_ID = '" & txtCustomerAssistantID.Text & "' " + vbCr
            strSql += ", Area = '" & ddlArea.SelectedValue & "', Cluster = '" & ddlCluster.SelectedValue & "', Cluster_name = '" & ddlClusterName.SelectedItem.Text & "', Cluster_email = '" & ddlClusterName.SelectedValue & "' " + vbCr
            strSql += ", Customer_Contact_Name = '" & txtCustomerContactName.Text & "', Customer_Contact_Tel = '" & txtCustomerContactTel.Text & "', Customer_Contact_Email = '" & txtCustomerContactEmail.Text & "', Company_Service = '" & Company.ToString & "' " + vbCr
            strSql += ", Type_Service = '" & TypeOfService.ToString & "', Detail_Service = '" & txtDetailService.Text & "', SLA = '" & txtSLA.Text & "', Monitor_Date = '" & ddlMonitorDate.SelectedValue & "'" + vbCr
            strSql += ", Monitor_Time = '" & ddlMonitorTime.SelectedValue & "', Fine = '" & txtFine.Text & "', UpdateBy = '" & Session("uemail") & "', UpdateDate = getdate() " + vbCr
            If chkSLA.Checked = True Then
                strSql += ", SLA_File = '" & FileNameSLA.ToString & "' "
            End If
            If chkDeleteSLA.Checked = True Then
                strSql += ", SLA_File = '' "
            End If
            If chkFine.Checked = True Then
                strSql += ", Fine_File = '" & FileNameFine.ToString & "' "
            End If
            If chkDeleteFine.Checked = True Then
                strSql += ", Fine_File = '' "
            End If
            strSql += "where CreateBy='" + Session("uemail") + "' and Document_No is null "
            C.ExecuteNonQuery(strSql)

            If chkDeleteSLA.Checked = True Then
                'strSql += ", SLA_File = '' "
                If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("SLA_File")) Then
                    System.IO.File.Delete(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("SLA_File"))
                End If
            End If
            If chkDeleteFine.Checked = True Then
                'strSql += ", Fine_File = '' "
                If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Fine_File")) Then
                    System.IO.File.Delete(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Fine_File"))
                End If
            End If
        Else
            strSql = "insert into List_ProjectName (Project_Name, Project_Code, Customer_Name, Enterprise_Name, Location_Name, Customer_Type, Document_Date, Service_Date, Customer_Assistant_Name, Customer_Assistant_ID, Area, Cluster, Cluster_name, Cluster_email, Customer_Contact_Name, Customer_Contact_Tel, Customer_Contact_Email, Company_Service, Type_Service, Detail_Service, SLA, SLA_File, Monitor_Date, Monitor_Time, Fine, Fine_File, Status, CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr
            strSql += "values('" & txtProjectName.Text & "','" & txtProjectCode.Text & "','" & txtCustomerName.Text & "','" & txtEnterprise.Text & "','" & txtLocationName.Text & "','" & ddlCustomerType.SelectedValue & "','" & C.CDateText(txtDocumentDate.Text) & "','" & C.CDateText(txtServiceDate.Text) & "','" & txtCustomerAssistantName.Text & "','" & txtCustomerAssistantID.Text & "','" & ddlArea.SelectedValue & "','" & ddlCluster.SelectedValue & "','" & ddlClusterName.SelectedItem.Text & "','" & ddlClusterName.SelectedValue & "','" & txtCustomerContactName.Text & "','" & txtCustomerContactTel.Text & "','" & txtCustomerContactEmail.Text & "','" & Company.ToString & "','" & TypeOfService.ToString & "','" & txtDetailService.Text & "','" & txtSLA.Text & "','" & FileNameSLA.ToString & "','" & ddlMonitorDate.SelectedValue & "','" & ddlMonitorTime.SelectedValue & "','" & txtFine.Text & "','" & FileNameFine.ToString & "','1','" & Session("uemail") & "',getdate(),'" & Session("uemail") & "',getdate()) "
            C.ExecuteNonQuery(strSql)
        End If
        Response.Redirect("add_service.aspx")
    End Sub

    Protected Sub rbtOther_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtOther.CheckedChanged
        CheckTypeOfService()
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

    Public Sub CheckTypeOfService()
        If rbtOther.Checked = True Then
            txtOther.Enabled = True
        Else
            txtOther.Enabled = False
        End If
    End Sub

    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        Dim strcluster As String
        Dim strcluster_name As String
        strcluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
        C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster", "(โปรดเลือก)")
        strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
        C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
    End Sub

    Protected Sub ddlCluster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCluster.SelectedIndexChanged
        Dim strcluster_name As String
        strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
        C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
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

    Protected Sub chkDeleteSLA_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeleteSLA.CheckedChanged
        'If chkEditSLA.Checked = True Then
        '    FileUploadEditSLA.Enabled = True
        '    FileUploadEditSLA.Visible = True
        'Else
        '    FileUploadEditSLA.Enabled = False
        '    FileUploadEditSLA.Visible = False
        'End If

    End Sub

    'Public Sub ShowEmployee()
    '    Dim DS As New DataSet
    '    Dim DT As New DataTable
    '    'Dim HttpWReq As HttpWebRequest
    '    'Dim httpWRes As HttpWebResponse = Nothing
    '    'Dim address As Uri
    '    Dim strData As New StringBuilder
    '    Dim Client_id As String = "SYyhqKGSyD_Feasi"
    '    Dim Client_Secret As String = "fDwVhCQMGjZwORGaFBIm"

    '    Dim json As String
    '    Dim reader As StreamReader

    '    Dim Request As HttpWebRequest
    '    Dim Response As HttpWebResponse
    '    Try
    '        Request = DirectCast(WebRequest.Create(New Uri("https://app.jasmine.com/contact-resource-api/v2/" + Session("uemail") + "@jasmine.com/")), HttpWebRequest)
    '        Request.Method = "GET"
    '        Request.ContentType = "application/x-www-form-urlencoded"
    '        Request.Headers("token") = Session("token")
    '        txtCustomerAssistantID.Text = Session("token")
    '        Request.Headers("scope") = "employee-information"
    '        Response = DirectCast(Request.GetResponse(), HttpWebResponse)
    '        reader = New StreamReader(Response.GetResponseStream())
    '        json = reader.ReadToEnd()
    '        DT = ConvertJSONToDataTable(json)
    '        If DT.Rows.Count > 0 Then
    '            Dim i As Integer
    '            For i = 0 To DT.Rows.Count - 2
    '                If (DT.Rows(i).Item("dateExpired") = "" Or DT.Rows(i).Item("dateExpired") = "null" Or C.CDateText(DT.Rows(i).Item("dateExpired")) > C.GetDateNow) And DT.Rows(i).Item("accountStatus") = "true" And DT.Rows(i).Item("email") = (Session("uemail") + "@jasmine.com") Then

    '                    txtCustomerAssistantName.Text = DT.Rows(i).Item("thaiInitialname") & DT.Rows(i).Item("thaiFirstname") & " " & DT.Rows(i).Item("thaiLastname")
    '                    txtCustomerAssistantID.Text = DT.Rows(i).Item("employeeId")

    '                    'ddlTitleName.SelectedValue = DT.Rows(i).Item("thaiInitialname")
    '                    'txtName.Text =  DT.Rows(i).Item("thaiFirstname")
    '                    'txtSurname.Text = DT.Rows(i).Item("thaiLastname")
    '                    'txtEmpID.Text = DT.Rows(i).Item("employeeId")
    '                    'txtBillet.Text = DT.Rows(i).Item("position")
    '                    'txtTel.Text = DT.Rows(i).Item("workPhone")
    '                    'Email.Enabled = False
    '                End If
    '            Next
    '            'If txtCustomerAssistantName.Text = "" Then
    '            '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
    '    End Try
    'End Sub

    Public Sub ShowEmployee()
        Dim DS As New DataSet
        Dim DT As New DataTable
        'Dim HttpWReq As HttpWebRequest
        'Dim httpWRes As HttpWebResponse = Nothing
        'Dim address As Uri
        Dim strData As New StringBuilder
        Dim Client_id As String = "SYyhqKGSyD_Feasi"
        Dim Client_Secret As String = "fDwVhCQMGjZwORGaFBIm"

        Dim json As String
        Dim reader As StreamReader

        Dim Request As HttpWebRequest
        Dim Response As HttpWebResponse
        Try
            Request = DirectCast(WebRequest.Create(New Uri("https://api.jasmine.com/authen1/me")), HttpWebRequest)
            Request.Method = "GET"
            Request.ContentType = "application/x-www-form-urlencoded"
            Request.Headers("Authorization") = "Bearer " & Session("token")
            Response = DirectCast(Request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(Response.GetResponseStream())
            json = reader.ReadToEnd()
            DT = ConvertJSONToDataTable(json)
            If DT.Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To DT.Rows.Count - 2
                    'If (DT.Rows(i).Item("dateExpired") = "" Or DT.Rows(i).Item("dateExpired") = "null" Or C.CDateText(DT.Rows(i).Item("dateExpired")) > C.GetDateNow) And DT.Rows(i).Item("accountStatus") = "true" And DT.Rows(i).Item("email") = (Session("uemail") + "@jasmine.com") Then

                    txtCustomerAssistantName.Text = "คุณ" + DT.Rows(i).Item("thai_fullname")
                    txtCustomerAssistantID.Text = DT.Rows(i).Item("employee_id")

                    'ddlTitleName.SelectedValue = DT.Rows(i).Item("thaiInitialname")
                    'txtName.Text =  DT.Rows(i).Item("thaiFirstname")
                    'txtSurname.Text = DT.Rows(i).Item("thaiLastname")
                    'txtEmpID.Text = DT.Rows(i).Item("employeeId")
                    'txtBillet.Text = DT.Rows(i).Item("position")
                    'txtTel.Text = DT.Rows(i).Item("workPhone")
                    'Email.Enabled = False
                    'End If
                Next
                'If txtCustomerAssistantName.Text = "" Then
                '    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
                'End If
            End If
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
        End Try
    End Sub

    Private Function ConvertJSONToDataTable(ByVal jsonString As String) As DataTable
        Dim dt As New DataTable
        'strip out bad characters
        Dim jsonParts As String() = jsonString.Replace("[{", "{").Replace("}]", "}").Split("},{")

        'hold column names
        Dim dtColumns As New List(Of String)

        'get columns
        For Each jp As String In jsonParts
            'only loop thru once to get column names
            Dim propData As String() = jp.Replace("{", "").Replace("}", "").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
            For Each rowData As String In propData
                Try
                    If rowData.Split(":").Length - 1 <> 0 Then
                        Dim idx As Integer = rowData.IndexOf(":")
                        Dim n As String = rowData.Substring(0, idx - 1)
                        Dim v As String = rowData.Substring(idx + 1)
                        If Not dtColumns.Contains(n) Then
                            dtColumns.Add(n.Replace("""", ""))
                        End If
                    End If
                Catch ex As Exception
                    Throw New Exception(String.Format("Error Parsing Column Name : {0}", rowData))
                End Try

            Next
            Exit For
        Next

        'build dt
        For Each c As String In dtColumns
            dt.Columns.Add(c)
        Next
        'get table data
        For Each jp As String In jsonParts
            Dim propData As String() = jp.Replace("{", "").Replace("}", "").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
            Dim nr As DataRow = dt.NewRow
            For Each rowData As String In propData
                Try
                    Dim idx As Integer = rowData.IndexOf(":")
                    Dim n As String = rowData.Substring(0, idx - 1).Replace("""", "")
                    Dim v As String = rowData.Substring(idx + 1).Replace("""", "")
                    nr(n) = v
                Catch ex As Exception
                    Continue For
                End Try

            Next
            dt.Rows.Add(nr)
        Next
        Return dt
    End Function

    Function CertificateValidationCallBack(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

End Class
