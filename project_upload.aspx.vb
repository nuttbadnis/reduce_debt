Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebControl
Imports System.Web.UI.HtmlControls.HtmlControl
Imports System.Web.Script.Serialization

Partial Class project_upload
    Inherits System.Web.UI.Page

    Dim C As New Cls_Data
    Dim N As New auto_vb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim user As String = ""
        If Request.QueryString("ue") IsNot Nothing Then
            user = Request.QueryString("ue")
        Else
            user = Session("uemail")
        End If

        Page.MaintainScrollPositionOnPostBack = True
        If Not Page.IsPostBack Then
            'Response.Write("test------------" + Master.MasterpageString)
            hide_user.Value = user
            hide_user_permission.Value = Session("Login_permission")
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


            '-----Clear CustomerAssistant-----'
            C.ExecuteNonQuery("delete from tmpList_CAsst where CreateBy = '" + user + "' and ProjectName_id is null ")
            '-----Clear CustomerContact-----'

            C.ExecuteNonQuery("delete from tmpList_CT where CreateBy = '" + user + "' and ProjectName_id is null ")

            txtDocumentDate.Text = Now.Day.ToString("00") + "/" + Now.Month.ToString("00") + "/" + Now.Year.ToString("0000")
            txtServiceDate.Text = Now.Day.ToString("00") + "/" + Now.Month.ToString("00") + "/" + Now.Year.ToString("0000")

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
            strSql += ", Detail_Service, SLA, isnull(SLA_File,'') 'SLA_File', isnull(Doc_File,'') 'Doc_File', MTTR, Monitor_Date, Monitor_Time, Fine, isnull(Fine_File,'') 'Fine_File', id_List " + vbCr
            strSql += ", Special_Price, Upload_Project, isnull(Project_File,'') 'Project_File' " + vbCr
            strSql += "from List_ProjectName where CreateBy='" + hide_user.Value + "' and Document_No is null "
            DT_List = C.GetDataTable(strSql)
            If DT_List.Rows.Count > 0 Then
                hide_doc_no.Value = DT_List.Rows(0).Item("id_List")
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
                'ddlArea.SelectedValue = DT_List.Rows(0).Item("Area").ToString

                'strCluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
                'C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster", "(โปรดเลือก)")
                'ddlCluster.SelectedValue = DT_List.Rows(0).Item("Cluster").ToString

                'strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
                'C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
                'ddlClusterName.SelectedValue = DT_List.Rows(0).Item("Cluster_email").ToString

                txtCustomerContactName.Text = DT_List.Rows(0).Item("Customer_Contact_Name").ToString
                txtCustomerContactTel.Text = DT_List.Rows(0).Item("Customer_Contact_Tel").ToString
                txtCustomerContactEmail.Text = DT_List.Rows(0).Item("Customer_Contact_Email").ToString

                If DT_List.Rows(0).Item("Company_Service").ToString = rbtTTTBB.Text Then
                    rbtTTTi.Checked = False
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

                If DT_List.Rows(0).Item("Special_Price") Then
                    rbtSpecialYes.Checked = True
                Else
                    rbtSpecialNo.Checked = True
                End If

                txtDetailService.Text = DT_List.Rows(0).Item("Detail_Service").ToString
                txtSLA.Text = DT_List.Rows(0).Item("SLA").ToString
                txtMTTR.Text = DT_List.Rows(0).Item("MTTR").ToString
                txtFine.Text = DT_List.Rows(0).Item("Fine").ToString
                ddlMonitorDate.SelectedValue = DT_List.Rows(0).Item("Monitor_Date").ToString
                ddlMonitorTime.SelectedValue = DT_List.Rows(0).Item("Monitor_Time").ToString

                'If DT_List.Rows(0).Item("SLA_File") <> "" Then
                '    div_UploadSLA.Visible = False
                '    div_OpenSLA.Visible = True
                '    'LinkFileSLA.Visible = True
                '    'chkSLA.Visible = False
                '    'chkDeleteSLA.Visible = True
                '    'FileUploadSLA.Visible = False
                '    LinkFileSLA.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("SLA_File")
                '    LinkFileSLA.Target = "_blank"
                'Else
                '    div_UploadSLA.Visible = True
                '    div_OpenSLA.Visible = False
                '    'LinkFileSLA.Visible = False
                '    'chkSLA.Visible = True
                '    'chkDeleteSLA.Visible = False
                'End If

                'If DT_List.Rows(0).Item("Fine_File") <> "" Then
                '    div_UploadFine.Visible = False
                '    div_OpenFine.Visible = True
                '    LinkFileFine.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("Fine_File")
                '    LinkFileFine.Target = "_blank"
                'Else
                '    div_UploadFine.Visible = True
                '    div_OpenFine.Visible = False
                'End If

                If DT_List.Rows(0).Item("Doc_File") <> "" Then
                    Dim sp_Doc() As String
                    sp_Doc = Split(DT_List.Rows(0).Item("Doc_File"), "\")
                    If sp_Doc.Length > 0 Then
                        LinkFileDoc.Text = sp_Doc(sp_Doc.Length - 1)
                    End If
                    div_UploadDoc.Visible = False
                    div_OpenDoc.Visible = True
                    LinkFileDoc.NavigateUrl = "~/Upload/" & DT_List.Rows(0).Item("Doc_File")
                    LinkFileDoc.Target = "_blank"
                Else
                    div_UploadDoc.Visible = True
                    div_OpenDoc.Visible = False
                End If

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
            Else
                div_OpenSLA.Visible = False
                div_OpenFine.Visible = False
                div_OpenDoc.Visible = False
                div_OpenProject.Visible = False

                strCluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
                C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster", "(โปรดเลือก)")

                strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster_name "
                C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")


                Dim DT_casst_cluster As New DataTable
                Dim DT_casst As New DataTable
                Dim strSql_casst_cluster As String
                DT_casst = C.GetDataTable("select * from tmpList_CAsst where CreateBy='" & hide_user.Value & "' and isnull(ProjectName_id, '') = ''")
                If DT_casst.Rows.Count = 0 Then
                    strSql_casst_cluster = "select  lo.Login_name, cl.RO 'Area' , cl.Cluster 'Cluster', Cluster_name 'Cluster_Name', cl.Cluster_email 'Cluster_Email' " + vbCr
                    strSql_casst_cluster += "from dbo.UserLogin lo inner join dbo.UserBranch br on lo.Login_name = br.Login_name " + vbCr
                    strSql_casst_cluster += "inner join dbo.Cluster cl on br.Cluster = cl.Cluster " + vbCr
                    strSql_casst_cluster += "where lo.Login_name = '" & hide_user.Value & "' " + vbCr
                    DT_casst_cluster = C.GetDataTable(strSql_casst_cluster)
                    If DT_casst_cluster.Rows.Count > 0 Then
                        strSql_casst_cluster = "insert into tmpList_CAsst(Customer_Assistant_ID, Customer_Assistant_Name, Customer_Assistant_Email, Area, Cluster, Cluster_Name, Cluster_Email, CreateBy, CreateDate) " + vbCr
                        strSql_casst_cluster += "values ('" & Session("uid") & "','" & Session("ufullname") & "','" & hide_user.Value & "@jasmine.com','" & DT_casst_cluster.Rows(0).Item("Area") & "','" & DT_casst_cluster.Rows(0).Item("Cluster") & "','" & DT_casst_cluster.Rows(0).Item("Cluster_Name") & "','" & DT_casst_cluster.Rows(0).Item("Cluster_Email") & "','" & hide_user.Value & "',getdate())"
                        C.ExecuteNonQuery(strSql_casst_cluster)
                    End If
                End If
            End If

            '////// List Service
            strSql = "select * from List_Service where CreateBy='" + Session("uemail") + "' and Document_No is null "
            DT_List = C.GetDataTable(strSql)
            If DT_List.Rows.Count > 0 Then
                txtContract.Text = DT_List.Rows(0).Item("Contract").ToString
                txtInputPrice.Text = DT_List.Rows(0).Item("Monthly").ToString
                txtOneTimePayment.Text = DT_List.Rows(0).Item("OneTimePayment").ToString
                txtMarketing.Text = DT_List.Rows(0).Item("Marketing").ToString
                txtEntertainCustomer.Text = DT_List.Rows(0).Item("EntertainCustomer").ToString
                txtGift.Text = DT_List.Rows(0).Item("Gift").ToString
                txtPenalty.Text = DT_List.Rows(0).Item("Penalty").ToString
                txtPenaltyLate.Text = DT_List.Rows(0).Item("PenaltyLate").ToString

                CalculateSellingPrice()
            Else
                txtContract.Text = "12"
                txtInputPrice.Text = "0"
                txtOneTimePayment.Text = "0"
                txtMarketing.Text = "0"
                txtEntertainCustomer.Text = "0"
                txtGift.Text = "0"
                txtPenalty.Text = "0"
                txtPenaltyLate.Text = "0"
            End If

            'DT_List = C.GetDataTable("select * from tmpList_CAsst where CreateBy = '" + hide_user.Value + "' and ProjectName_id is null ")
            'GridView1.DataSource = DT_List
            'GridView1.DataBind()

            If Session("Login_permission") <> "administrator1" Then
                searchCustomerAssistant.Visible = False
                selectCustomerAssistant.Visible = False
            Else
                searchCustomerAssistant.Visible = True
                selectCustomerAssistant.Visible = True
            End If


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
            table_tmpcasst += "<th></th>"
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

        Else

        End If

    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    'ClientScript.RegisterStartupScript(Page.GetType, "test", "bootbox.alert('กรุณาระบุ Project Name');", True)

    '    If ddlArea.SelectedValue = "" Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ พื้นที่');", True)
    '        ddlArea.Focus()
    '    ElseIf ddlCluster.SelectedValue = "" Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Cluster');", True)
    '        ddlCluster.Focus()
    '    ElseIf ddlClusterName.SelectedValue = "" Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ รายชื่อ Cluster');", True)
    '        ddlClusterName.Focus()
    '    Else
    '        If N.validateEmail(txtCustomerAssistantEmail.Text.Trim) <> True Then
    '            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Format Email ให้ถูกต้อง');", True)
    '            txtCustomerAssistantEmail.Focus()
    '            txtCustomerAssistantEmail.Style.Add("border-color", "red")
    '        Else
    '            Dim DT_List As DataTable
    '            Dim strSql As String
    '            Dim r As String = ""

    '            strSql = "insert into tmpList_CAsst (Customer_Assistant_ID, Customer_Assistant_Name, Customer_Assistant_Email, Area, Cluster, Cluster_Name, Cluster_Email,"
    '            strSql += "CreateBy, CreateDate) Values"
    '            'For i As Integer = 0 To GridView1.Rows.Count - 1
    '            strSql += "('" + txtCustomerAssistantID.Text + "','" + txtCustomerAssistantName.Text + "','" + txtCustomerAssistantEmail.Text + "',"
    '            strSql += "'" + ddlArea.SelectedValue + "',"
    '            strSql += "'" + ddlCluster.SelectedValue + "',"
    '            strSql += "'" + ddlClusterName.SelectedItem.Text + "',"
    '            strSql += "'" + ddlClusterName.SelectedValue + "',"
    '            strSql += "'" + hide_user.Value + "',getdate())"
    '            'Next

    '            C.ExecuteNonQuery(strSql)
    '            DT_List = C.GetDataTable("select * from tmpList_CAsst where CreateBy = '" + hide_user.Value + "'  and ProjectName_id is null ")
    '            GridView1.DataSource = DT_List
    '            GridView1.DataBind()

    '            txtCustomerAssistantEmail.Style.Add("border-color", "rgba(204,204,204)")
    '        End If

    '    End If
    '    'Else

    '    'End If
    'End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim DT_List As DataTable
        Dim id_list As String
        Dim strSql As String

        If Not CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text Is Nothing Then
            id_list = CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text
            strSql = "delete from tmpList_CAsst where id_List = " + id_list + " "
            'Response.Write(strSql)
            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + Format(CDbl(e.RowIndex)) + "_" + id_list + "');", True)
            C.ExecuteNonQuery(strSql)
        End If
        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + hide_doc_no.Value + "');", True)
        DT_List = C.GetDataTable("select * from tmpList_CAsst where CreateBy = '" + hide_user.Value + "'  and (ProjectName_id is null  or ProjectName_id = '" & hide_doc_no.Value & "')")
        GridView1.DataSource = DT_List
        GridView1.DataBind()

    End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'If Not Page.IsPostBack Then
    '    '    ClientScript.RegisterStartupScript(Page.GetType, "test", "bootbox.alert('กรุณาระบุ Project Name');", True)
    '    'End If
    '    If txtCustomerContactName.Text = "" Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ชื่อผู้ติดต่อ');", True)
    '        txtCustomerContactName.Focus()
    '    ElseIf txtCustomerContactTel.Text = "" Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ โทรศัพท์');", True)
    '        txtCustomerContactTel.Focus()
    '    ElseIf txtCustomerContactEmail.Text = "" Then
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Email');", True)
    '        txtCustomerContactEmail.Focus()
    '    Else

    '        If N.validateEmail(txtCustomerContactEmail.Text) <> True Then
    '            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Format Email ให้ถูกต้อง');", True)
    '            txtCustomerContactEmail.Focus()
    '            txtCustomerContactEmail.Style.Add("border-color", "red")
    '        Else
    '            Dim DT_List As DataTable
    '            Dim strSql As String
    '            Dim r As String = ""

    '            strSql = "insert into tmpList_CT (Customer_Contact_Name, Customer_Contact_Tel, Customer_Contact_Email,"
    '            strSql += "CreateBy, CreateDate) Values"
    '            strSql += "('" + txtCustomerContactName.Text + "','" + txtCustomerContactTel.Text + "','" + txtCustomerContactEmail.Text + "',"
    '            strSql += "'" + hide_user.Value + "',getdate())"

    '            C.ExecuteNonQuery(strSql)
    '            DT_List = C.GetDataTable("select * from tmpList_CT where CreateBy = '" + hide_user.Value + "'  and ProjectName_id is null ")
    '            GridView2.DataSource = DT_List
    '            GridView2.DataBind()

    '            txtCustomerContactEmail.Style.Add("border-color", "rgba(204,204,204)")
    '        End If

    '    End If

    'End Sub

    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim DT_List As DataTable
        Dim id_list As String
        Dim strSql As String

        If Not CType(GridView2.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text Is Nothing Then
            id_list = CType(GridView2.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text
            strSql = "delete from tmpList_CT where id_List = " + id_list + " "
            'Response.Write(strSql)
            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" + Format(CDbl(e.RowIndex)) + "_" + id_list + "');", True)
            C.ExecuteNonQuery(strSql)
        End If

        DT_List = C.GetDataTable("select * from tmpList_CT where CreateBy = '" + hide_user.Value + "'  and ProjectName_id is null ")
        GridView2.DataSource = DT_List
        GridView2.DataBind()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CheckData("Save")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('" & Path.GetExtension(FileUploadProject.FileName) & "');", True)
        'CheckData("Next")
    End Sub

    Private Sub CheckData(ByVal SaveOrNext As String)
        If FileUploadProject.FileName <> "" Then
            If Path.GetExtension(FileUploadProject.FileName).ToLower <> ".xls" And Path.GetExtension(FileUploadProject.FileName).ToLower <> ".xlsx" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('Project File ต้องเป็น Excel เท่านั้น');", True)
                FileUploadProject.Focus()
                Exit Sub
            ElseIf FileUploadProject.PostedFile.ContentLength > 20971520 Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('Project File ขนาดต้องไม่เกิน 20 MB');", True)
                FileUploadProject.Focus()
                Exit Sub
            End If
        End If
        If Trim(txtProjectName.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Project Name');", True)
            txtProjectName.Focus()
        ElseIf txtProjectName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Project Name ได้"");focus();", True)
            txtProjectName.Focus()
        ElseIf txtLocationName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Ref.LocationName/Code: ได้"");focus();", True)
            txtLocationName.Focus()
        ElseIf Trim(txtCustomerName.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ชื่อลูกค้า');", True)
            txtCustomerName.Focus()
        ElseIf txtCustomerName.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล ชื่อลูกค้า ได้"");focus();", True)
            txtCustomerName.Focus()
        ElseIf txtProjectCode.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Project Code ได้"");focus();", True)
            txtProjectCode.Focus()
        ElseIf ddlCustomerType.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ประเภทลูกค้า');focus();", True)
            ddlCustomerType.Focus()
        ElseIf txtDocumentDate.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ วันที่จัดทำ/ปรับปรุง');", True)
            txtDocumentDate.Focus()
        ElseIf txtServiceDate.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ วันที่เริ่มให้บริการ');", True)
            txtServiceDate.Focus()
            'ElseIf Trim(txtCustomerAssistantName.Text) = "" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Customer Assistant Name');", True)
            '    txtCustomerAssistantName.Focus()
            'ElseIf txtCustomerAssistantName.Text.Contains("'") = True Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Customer Assistant Name ได้"");focus();", True)
            '    txtCustomerAssistantName.Focus()
            'ElseIf Trim(txtCustomerAssistantID.Text) = "" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Customer Assistant ID');", True)
            '    txtCustomerAssistantID.Focus()
            'ElseIf txtCustomerAssistantID.Text.Contains("'") = True Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Customer Assistant ID ได้"");focus();", True)
            '    txtCustomerAssistantID.Focus()
            '    'ElseIf ddlArea.SelectedIndex = 0 Then
            '    '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Area');focus();", True)
            '    '    ddlArea.Focus()
            '    'ElseIf ddlCluster.SelectedIndex = 0 Then
            '    '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Cluster');focus();", True)
            '    '    ddlCluster.Focus()
            '    'ElseIf ddlClusterName.SelectedIndex = 0 Then
            '    '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Prepared by');focus();", True)
            '    '    ddlClusterName.Focus()
        ElseIf getTable_List_CAsst().Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุข้อมูล Customer Assistant');", True)
            txtCustomerAssistantName.Focus()
        ElseIf getTable_List_CT().Rows.Count <= 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุข้อมูล Customer Contact');", True)
            txtCustomerContactName.Focus()
        ElseIf rbtOtherCompany.Checked = True And Trim(txtOtherCompany.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Company');", True)
            txtOtherCompany.Focus()
        ElseIf rbtOtherCompany.Checked = True And txtOtherCompany.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Company ได้"");focus();", True)
            txtOtherCompany.Focus()

        ElseIf rbtOther.Checked = True And Trim(txtOther.Text) = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Type Of Service');", True)
            txtOther.Focus()
        ElseIf rbtOther.Checked = True And txtOther.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Type Of Service ได้"");focus();", True)
            txtOther.Focus()

        ElseIf txtDetailService.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล รายละเอียดโครงการ ได้"");focus();", True)
            txtDetailService.Focus()
        ElseIf txtSLA.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล SLA ได้"");focus();", True)
            txtSLA.Focus()
        ElseIf txtMTTR.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล MTTR ได้"");focus();", True)
            txtMTTR.Focus()
        ElseIf txtFine.Text.Contains("'") = True Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล ค่าปรับ ได้"");focus();", True)
            txtFine.Focus()

        ElseIf ddlMonitorDate.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Monitor Date');focus();", True)
            ddlMonitorDate.Focus()
        ElseIf ddlMonitorTime.SelectedIndex = 0 Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ Monitor Time');focus();", True)
            ddlMonitorTime.Focus()

            'ElseIf chkSLA.Checked = True And FileUploadSLA.FileName = "" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ไฟล์แนบ SLA');", True)
            '    FileUploadSLA.Focus()
            'ElseIf chkSLA.Checked = True And Path.GetExtension(FileUploadSLA.FileName) <> ".pdf" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไฟล์แนบ SLA ต้องเป็น PDF เท่านั้น');", True)
            '    FileUploadSLA.Focus()
            'ElseIf chkFine.Checked = True And FileUploadFine.FileName = "" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ไฟล์แนบค่าปรับ');", True)
            '    FileUploadFine.Focus()
            'ElseIf chkFine.Checked = True And Path.GetExtension(FileUploadFine.FileName) <> ".pdf" Then
            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไฟล์แนบค่าปรับ ต้องเป็น PDF เท่านั้น');", True)
            '    FileUploadFine.Focus()

        ElseIf chkDoc.Checked = True And FileUploadDoc.FileName = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ไฟล์เอกสารแนบ');", True)
            FileUploadDoc.Focus()
        ElseIf chkDoc.Checked = True And (Path.GetExtension(FileUploadDoc.FileName) <> ".pdf" And Path.GetExtension(FileUploadDoc.FileName) <> ".zip" And Path.GetExtension(FileUploadDoc.FileName) <> ".rar") Then
            ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไฟล์เอกสารแนบ ต้องเป็น pdf, zip หรือ rar เท่านั้น');", True)
            FileUploadDoc.Focus()
        Else
            If chkDoc.Checked = True Then
                If FileUploadDoc.PostedFile.ContentLength > 20971520 Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "alert('ไฟล์เอกสารแนบ ขนาดต้องไม่เกิน 20 MB');", True)
                    FileUploadDoc.Focus()
                    Exit Sub
                End If
            End If
            SaveData(SaveOrNext)
        End If
    End Sub

    Public Sub SaveData(ByVal SaveOrNext As String)
        Dim FileNameSLA As String = ""
        Dim FileNameFine As String = ""
        Dim FileNameDoc As String = ""
        'If chkSLA.Checked = True Then
        '    If FileUploadSLA.HasFile = True Then
        '        Dim CurrentFileName As String
        '        Dim CurrentPath As String

        '        CurrentFileName = FileUploadSLA.FileName
        '        CurrentPath = Request.PhysicalApplicationPath
        '        CurrentPath += "Upload\"
        '        CurrentPath += CurrentFileName
        '        FileUploadSLA.SaveAs(CurrentPath)
        '        Dim file_extSLA As String
        '        file_extSLA = Path.GetExtension(FileUploadSLA.FileName)
        '        Dim TheFileSLA As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
        '        FileNameSLA = "SLA" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + "-" + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extSLA
        '        If TheFileSLA.Exists Then
        '            If System.IO.File.Exists(MapPath(".") & "\Upload\" & FileNameSLA) Then
        '                System.IO.File.Delete(MapPath(".") & "\Upload\" & FileNameSLA)
        '            End If
        '            System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\" & FileNameSLA)
        '        Else
        '            Throw New FileNotFoundException()
        '        End If
        '    Else
        '        FileNameSLA = ""
        '    End If
        '    If FileNameSLA = "" Then
        '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ไฟล์แนบ SLA');", True)
        '        FileUploadSLA.Focus()
        '        Exit Sub
        '    End If
        'End If

        'If chkFine.Checked = True Then
        '    If FileUploadFine.HasFile = True Then
        '        Dim CurrentFileName As String
        '        Dim CurrentPath As String

        '        CurrentFileName = FileUploadFine.FileName
        '        CurrentPath = Request.PhysicalApplicationPath
        '        CurrentPath += "Upload\"
        '        CurrentPath += CurrentFileName
        '        FileUploadFine.SaveAs(CurrentPath)
        '        Dim file_extFine As String
        '        file_extFine = Path.GetExtension(FileUploadFine.FileName)
        '        Dim TheFileFine As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
        '        FileNameFine = "Fine" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + "-" + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extFine
        '        If TheFileFine.Exists Then
        '            If System.IO.File.Exists(MapPath(".") & "\Upload\" & FileNameFine) Then
        '                System.IO.File.Delete(MapPath(".") & "\Upload\" & FileNameFine)
        '            End If
        '            System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\" & FileNameFine)
        '        Else
        '            Throw New FileNotFoundException()
        '        End If
        '    Else
        '        FileNameFine = ""
        '    End If
        '    If FileNameFine = "" Then
        '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ไฟล์แนบค่าปรับ');", True)
        '        FileUploadFine.Focus()
        '        Exit Sub
        '    End If
        'End If

        If chkDoc.Checked = True Then
            If FileUploadDoc.HasFile = True Then
                Dim CurrentFileName As String
                Dim CurrentPath As String

                CurrentFileName = FileUploadDoc.FileName
                CurrentPath = Request.PhysicalApplicationPath
                CurrentPath += "Upload\"
                CurrentPath += CurrentFileName
                FileUploadDoc.SaveAs(CurrentPath)
                Dim file_extdoc As String
                file_extdoc = Path.GetExtension(FileUploadDoc.FileName)
                Dim TheFiledoc As FileInfo = New FileInfo(MapPath(".") & "\Upload\" & CurrentFileName)
                FileNameDoc = "Doc\DOC_" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + "_" + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00") + file_extdoc
                If TheFiledoc.Exists Then
                    If System.IO.File.Exists(MapPath(".") & "\Upload\" & FileNameDoc) Then
                        System.IO.File.Delete(MapPath(".") & "\Upload\" & FileNameDoc)
                    End If
                    System.IO.File.Move(MapPath(".") & "\Upload\" & CurrentFileName, MapPath(".") & "\Upload\" & FileNameDoc)
                Else
                    Throw New FileNotFoundException()
                End If
            Else
                FileNameDoc = ""
            End If
            If FileNameDoc = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('กรุณาระบุ ไฟล์เอกสารแนบ');", True)
                FileUploadDoc.Focus()
                Exit Sub
            End If
        End If


        Dim strSql As String
        Dim DT_List As New DataTable
        Dim slSql As String
        Dim DS_List As New DataTable

        strSql = "select * from List_ProjectName where CreateBy='" + hide_user.Value + "' and Document_No is null "
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

        Dim SpecialPrice As Integer
        If rbtSpecialYes.Checked = True Then
            SpecialPrice = 1
        Else
            SpecialPrice = 0
        End If

        If DT_List.Rows.Count > 0 Then
            strSql = "Update List_ProjectName set Project_Name = '" & txtProjectName.Text & "', Project_Code = '" & txtProjectCode.Text & "', Customer_Name = '" & txtCustomerName.Text & "' " + vbCr
            strSql += ", Enterprise_Name = '', Location_Name = '" & txtLocationName.Text & "', Customer_Type = '" & ddlCustomerType.SelectedValue & "', Document_Date = '" & C.CDateText(txtDocumentDate.Text) & "' " + vbCr
            strSql += ", Service_Date = '" & C.CDateText(txtServiceDate.Text) & "', Customer_Assistant_Name = '" & txtCustomerAssistantName.Text & "', Customer_Assistant_ID = '" & txtCustomerAssistantID.Text & "' " + vbCr
            'strSql += ", Area = '" & ddlArea.SelectedValue & "', Cluster = '" & ddlCluster.SelectedValue & "', Cluster_name = '" & ddlClusterName.SelectedItem.Text & "', Cluster_email = '" & ddlClusterName.SelectedValue & "' " + vbCr
            'strSql += ", Customer_Contact_Name = '" & txtCustomerContactName.Text & "', Customer_Contact_Tel = '" & txtCustomerContactTel.Text & "', Customer_Contact_Email = '" & txtCustomerContactEmail.Text & "', 
            strSql += ", Company_Service = '" & Company.ToString & "', Special_Price = '" & SpecialPrice & "' " + vbCr
            strSql += ", Type_Service = '" & TypeOfService.ToString & "', Detail_Service = '" & txtDetailService.Text & "', SLA = '" & txtSLA.Text & "', MTTR = '" & txtMTTR.Text & "', Monitor_Date = '" & ddlMonitorDate.SelectedValue & "'" + vbCr
            strSql += ", Monitor_Time = '" & ddlMonitorTime.SelectedValue & "', Fine = '" & txtFine.Text & "', UpdateBy = '" & hide_user.Value & "', UpdateDate = getdate() " + vbCr
            'If chkSLA.Checked = True Then
            '    strSql += ", SLA_File = '" & FileNameSLA.ToString & "' "
            'End If
            'If chkDeleteSLA.Checked = True Then
            '    strSql += ", SLA_File = '' "
            'End If
            'If chkFine.Checked = True Then
            '    strSql += ", Fine_File = '" & FileNameFine.ToString & "' "
            'End If
            'If chkDeleteFine.Checked = True Then
            '    strSql += ", Fine_File = '' "
            'End If
            If chkDoc.Checked = True Then
                strSql += ", Doc_File = '" & FileNameDoc.ToString & "' "
            End If
            If chkDeleteDoc.Checked = True Then
                strSql += ", Doc_File = '' "
            End If
            strSql += "where CreateBy='" + hide_user.Value + "' and Document_No is null "
            'Response.Write(strSql)
            Try
                C.ExecuteNonQuery(strSql)
                slSql = "SELECT id_List as ScopeIdentity from List_ProjectName where CreateBy = '" + hide_user.Value + "' and Document_No is null "
                DS_List = C.GetDataTable(slSql)
                If DS_List.Rows.Count > 0 Then
                    Dim ProjectName_Id As String = DS_List.Rows(0).Item("ScopeIdentity").ToString()

                    '-----Update CustomerAssistant-----'
                    Dim update_casst As String = "update tmpList_CAsst set ProjectName_id = '" + ProjectName_Id + "' where CreateBy = '" + hide_user.Value + "' and ProjectName_id is null "
                    'Response.Write(update_casst)
                    C.ExecuteNonQuery(update_casst)

                    '-----Update CustomerContact-----'
                    Dim update_cc As String = "update tmpList_CT set ProjectName_id = '" + ProjectName_Id + "' where CreateBy = '" + hide_user.Value + "' and ProjectName_id is null "
                    'Response.Write(update_cc)
                    C.ExecuteNonQuery(update_cc)

                    Dim DT_casst As New DataTable
                    Dim DT_ct As New DataTable
                    DT_casst = C.GetDataTable("select * from tmpList_CAsst where ProjectName_id = '" + ProjectName_Id + "' order by id_List ")
                    DT_ct = C.GetDataTable("select * from tmpList_CT where ProjectName_id = '" + ProjectName_Id + "' order by id_List  ")
                    If DT_casst.Rows.Count > 0 Then
                        C.ExecuteNonQuery("Update List_ProjectName set Area = '" & DT_casst.Rows(0).Item("Area") & "', Cluster = '" & DT_casst.Rows(0).Item("Cluster") & "', Cluster_Name = '" & DT_casst.Rows(0).Item("Cluster_Name") & "', Cluster_Email = '" & DT_casst.Rows(0).Item("Cluster_Email") & "'  where id_List = '" & ProjectName_Id & "'")
                    End If
                    If DT_ct.Rows.Count > 0 Then
                        C.ExecuteNonQuery("Update List_ProjectName set Customer_Contact_Name = '" & DT_ct.Rows(0).Item("Customer_Contact_Name") & "', Customer_Contact_Tel = '" & DT_ct.Rows(0).Item("Customer_Contact_Tel") & "', Customer_Contact_Email = '" & DT_ct.Rows(0).Item("Customer_Contact_Email") & "'  where id_List = '" & ProjectName_Id & "'")
                    End If
                End If

                'If chkDeleteSLA.Checked = True Then
                '    'strSql += ", SLA_File = '' "
                '    If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("SLA_File")) Then
                '        System.IO.File.Delete(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("SLA_File"))
                '    End If
                'End If
                'If chkDeleteFine.Checked = True Then
                '    'strSql += ", Fine_File = '' "
                '    If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Fine_File")) Then
                '        System.IO.File.Delete(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Fine_File"))
                '    End If
                'End If
                If chkDeleteDoc.Checked = True Then
                    'strSql += ", Fine_File = '' "
                    If System.IO.File.Exists(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Doc_File")) Then
                        System.IO.File.Delete(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Doc_File"))
                        'System.IO.File.Move(MapPath(".") & "\Upload\" & DT_List.Rows(0).Item("Doc_File"), MapPath(".") & "\Upload\DeleteFile\" & DT_List.Rows(0).Item("Doc_File"))
                    End If
                End If
                addService()
                If SaveOrNext = "Next" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "bootbox.alert('บันทึกข้อมูลล่าสุดสำเร็จ', function(){ window.location = 'add_service.aspx?menu=create'; });", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType, "success_update", "bootbox.alert('บันทึกข้อมูลล่าสุดสำเร็จ', function(){ window.location = 'project_upload.aspx?menu=upload'; });", True)
                End If

            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "failed_update", "bootbox.alert('ไม่สามารถบันทึกข้อมูลได้');", True)
            End Try

        Else
            strSql = "insert into List_ProjectName "
            strSql += "(Project_Name, Project_Code, Customer_Name, Enterprise_Name, "
            strSql += "Location_Name, Customer_Type, Document_Date, Service_Date, "
            strSql += "Company_Service, Type_Service, Detail_Service, SLA, SLA_File,MTTR, "
            strSql += "Monitor_Date, Monitor_Time, Fine, Fine_File, Doc_File, "
            strSql += "Status, Special_Price, CreateBy, CreateDate, UpdateBy, UpdateDate) " + vbCr
            strSql += "values('" & txtProjectName.Text & "','" & txtProjectCode.Text & "','" & txtCustomerName.Text & "','',"
            strSql += "'" & txtLocationName.Text & "','" & ddlCustomerType.SelectedValue & "',"
            strSql += "Convert(Date,'" & txtDocumentDate.Text & "',103),Convert(Date,'" & txtServiceDate.Text & "',103),"

            strSql += "'" & Company.ToString & "','" & TypeOfService.ToString & "','" & txtDetailService.Text & "','" & txtSLA.Text & "','" & FileNameSLA.ToString & "','" & txtMTTR.Text & "',"
            strSql += "'" & ddlMonitorDate.SelectedValue & "','" & ddlMonitorTime.SelectedValue & "','" & txtFine.Text & "','" & FileNameFine.ToString & "','" & FileNameDoc.ToString & "',"
            strSql += "'1','" & SpecialPrice & "','" & hide_user.Value & "',getdate(),'" & hide_user.Value & "',getdate()) "
            Try
                'Response.Write(strSql)
                C.ExecuteNonQuery(strSql)
                slSql = "SELECT IDENT_CURRENT('List_ProjectName') as ScopeIdentity"
                DS_List = C.GetDataTable(slSql)
                If DS_List.Rows.Count > 0 Then
                    Dim ProjectName_Id As String = DS_List.Rows(0).Item("ScopeIdentity").ToString()

                    '-----Update CustomerAssistant-----'
                    Dim update_casst As String = "update tmpList_CAsst set ProjectName_id = '" + ProjectName_Id + "' where CreateBy = '" + hide_user.Value + "' and ProjectName_id is null "
                    'Response.Write(update_casst)
                    C.ExecuteNonQuery(update_casst)

                    '-----Update CustomerContact-----'
                    Dim update_cc As String = "update tmpList_CT set ProjectName_id = '" + ProjectName_Id + "' where CreateBy = '" + hide_user.Value + "' and ProjectName_id is null "
                    'Response.Write(update_cc)
                    C.ExecuteNonQuery(update_cc)

                    Dim DT_casst As New DataTable
                    Dim DT_ct As New DataTable
                    DT_casst = C.GetDataTable("select * from tmpList_CAsst where ProjectName_id = '" + ProjectName_Id + "' order by id_List ")
                    DT_ct = C.GetDataTable("select * from tmpList_CT where ProjectName_id = '" + ProjectName_Id + "' order by id_List  ")
                    If DT_casst.Rows.Count > 0 Then
                        C.ExecuteNonQuery("Update List_ProjectName set Area = '" & DT_casst.Rows(0).Item("Area") & "', Cluster = '" & DT_casst.Rows(0).Item("Cluster") & "', Cluster_Name = '" & DT_casst.Rows(0).Item("Cluster_Name") & "', Cluster_Email = '" & DT_casst.Rows(0).Item("Cluster_Email") & "'  where id_List = '" & ProjectName_Id & "'")
                    End If
                    If DT_ct.Rows.Count > 0 Then
                        C.ExecuteNonQuery("Update List_ProjectName set Customer_Contact_Name = '" & DT_ct.Rows(0).Item("Customer_Contact_Name") & "', Customer_Contact_Tel = '" & DT_ct.Rows(0).Item("Customer_Contact_Tel") & "', Customer_Contact_Email = '" & DT_ct.Rows(0).Item("Customer_Contact_Email") & "'  where id_List = '" & ProjectName_Id & "'")
                    End If
                End If
                addService()
                If SaveOrNext = "Next" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "bootbox.alert('บันทึกข้อมูลได้สำเร็จ', function(){ window.location = 'add_service.aspx?menu=create'; });", True)
                Else
                    ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "bootbox.alert('บันทึกข้อมูลได้สำเร็จ', function(){ window.location = 'project_upload.aspx?menu=upload'; });", True)
                End If

            Catch ex As Exception
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "bootbox.alert('ไม่สามารถบันทึกข้อมูลได้');", True)
            End Try

        End If


        'Response.Redirect("add_service.aspx")
    End Sub

    Public Sub addService()
        Dim strSql As String
        Dim DT_List As New DataTable
        strSql = "select * from List_Service where CreateBy='" + hide_user.Value + "' and Document_No is null "
        DT_List = C.GetDataTable(strSql)
        If DT_List.Rows.Count > 0 Then
            strSql = "Update List_Service set Contract = '" & CDbl(txtContract.Text) & "', Monthly='" + Format(CDbl(txtInputPrice.Text), "#0.00") + "', OneTimePayment = '" + Format(CDbl(txtOneTimePayment.Text), "#0.00") + "', MonthlySummary = '" + Format(CDbl(lblMonthlyPrice.Text), "#0.00") + "', OneTimeSummary = '" + Format(CDbl(lblOneTimePayment.Text), "#0.00") + "', TotalProject = '" + Format(CDbl(lblTotalYearly.Text), "#0.00") + "', " + vbCr
            strSql += "Marketing = '" + Format(CDbl(txtMarketing.Text), "#0.00") + "', EntertainCustomer = '" + Format(CDbl(txtEntertainCustomer.Text), "#0.00") + "', Gift = '" + Format(CDbl(txtGift.Text), "#0.00") + "', Penalty = '" + Format(CDbl(txtPenalty.Text), "#0.00") + "', PenaltyLate = '" + Format(CDbl(txtPenaltyLate.Text), "#0.00") + "' " + vbCr
            strSql += "where CreateBy='" + hide_user.Value + "' and Document_No is null "
            C.ExecuteNonQuery(strSql)
        Else
            strSql = "insert into List_Service "
            strSql += "(Contract, Monthly, OneTimePayment, MonthlySummary, OneTimeSummary, TotalProject, "
            strSql += "Marketing, EntertainCustomer, Gift, Penalty, PenaltyLate, CreateBy, CreateDate) " + vbCr
            strSql += "values ('" & CDbl(txtContract.Text) & "','" + Format(CDbl(txtInputPrice.Text), "#0.00") + "','" + Format(CDbl(txtOneTimePayment.Text), "#0.00") + "','" + Format(CDbl(lblMonthlyPrice.Text), "#0.00") + "','" + Format(CDbl(lblOneTimePayment.Text), "#0.00") + "','" + Format(CDbl(lblTotalYearly.Text), "#0.00") + "','" + Format(CDbl(txtMarketing.Text), "#0.00") + "','" + Format(CDbl(txtEntertainCustomer.Text), "#0.00") + "','" + Format(CDbl(txtGift.Text), "#0.00") + "','" + Format(CDbl(txtPenalty.Text), "#0.00") + "','" + Format(CDbl(txtPenaltyLate.Text), "#0.00") + "','" + Session("uemail") + "',getdate()) "
            C.ExecuteNonQuery(strSql)
        End If
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

    Protected Sub ddlSPUNo_Change(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Dim message As String = ddlArea.SelectedItem.Text & " - " & ddlArea.SelectedItem.Value
            ClientScript.RegisterStartupScript(Page.GetType, "alert", "alert('" & message & "');", True)
        Else
            Dim strcluster As String

            strcluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
            'C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster", "(โปรดเลือก)")
            'strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
            'C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
            'ClientScript.RegisterStartupScript(Page.GetType, "focus", "document.getElementById('" +  + "').focus()", True)
        End If

    End Sub

    'Protected Sub ItemChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If ddlCluster.SelectedValue <> 0 Then
    '        Session("ddlcluster") = ddlCluster.SelectedValue
    '    Else
    '        ClientScript.RegisterStartupScript(Page.GetType, "focus_text", "bootbox.alert('value cluster null');", True)
    '    End If
    'End Sub

    'Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim aa As String = ddlArea.ID
        Dim message As String = ddlArea.SelectedItem.Text & " - " & ddlArea.SelectedItem.Value
        'Dim strcluster As String
        'Dim strcluster_name As String
        'strcluster = "select distinct Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by Cluster "
        'C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster", "(โปรดเลือก)")
        'strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
        'C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")

    End Sub

    Protected Sub ddlCluster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim strcluster_name As String
        'strcluster_name = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' and Cluster = '" + ddlCluster.SelectedValue + "' order by Cluster_name "
        'C.SetDropDownList(ddlClusterName, strcluster_name, "Cluster_name", "Cluster_email", "(โปรดเลือก)")
        'ClientScript.RegisterStartupScript(Page.GetType, "focus_text", "bootbox.alert('value cluster null');", True)
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
    '        Request = DirectCast(WebRequest.Create(New Uri("https://app.jasmine.com/contact-resource-api/v2/" + session("Uemail") + "@jasmine.com/")), HttpWebRequest)
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
    '                If (DT.Rows(i).Item("dateExpired") = "" Or DT.Rows(i).Item("dateExpired") = "null" Or C.CDateText(DT.Rows(i).Item("dateExpired")) > C.GetDateNow) And DT.Rows(i).Item("accountStatus") = "true" And DT.Rows(i).Item("email") = (user + "@jasmine.com") Then

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
    '            '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
    '            'End If
    '        End If
    '    Catch ex As Exception
    '        ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
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
                    'If (DT.Rows(i).Item("dateExpired") = "" Or DT.Rows(i).Item("dateExpired") = "null" Or C.CDateText(DT.Rows(i).Item("dateExpired")) > C.GetDateNow) And DT.Rows(i).Item("accountStatus") = "true" And DT.Rows(i).Item("email") = (user + "@jasmine.com") Then

                    txtCustomerAssistantName.Text = "คุณ" + DT.Rows(i).Item("thai_fullname")
                    txtCustomerAssistantID.Text = DT.Rows(i).Item("employee_id")
                    txtCustomerAssistantEmail.Text = DT.Rows(i).Item("email")

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
                '    ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
                'End If
            End If
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "bootbox.alert('ไม่พบข้อมูลพนักงานนี้ โปรดตรวจสอบอีกครั้งและระบุ Email ใหม่ให้ถูกต้อง');", True)
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

    Function getTable_List_CAsst() As DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim DT_List As New DataTable
        strSql = "select * from List_ProjectName where CreateBy='" + hide_user.Value + "' and Document_No is null "
        DT_List = C.GetDataTable(strSql)
        If DT_List.Rows.Count > 0 Then
            strSql = "select * from tmpList_CAsst where CreateBy='" + hide_user.Value + "' and (ProjectName_id = '" & DT_List.Rows(0).Item("id_List") & "' or isnull(ProjectName_id, '') = '') "
            DT = C.GetDataTable(strSql)
        Else
            strSql = "select * from tmpList_CAsst where CreateBy='" + hide_user.Value + "' and  isnull(ProjectName_id, '') = '' "
            DT = C.GetDataTable(strSql)
        End If
        'Else
        'strSql = "select * from tmpList_CAsst where CreateBy='" + user + "' and isnull(ProjectName_id, '') = '' "
        'DT = C.GetDataTable(strSql)
        'End If

        Return DT
    End Function

    Function getTable_List_CT() As DataTable
        Dim strSql As String
        Dim DT As New DataTable
        Dim DT_List As New DataTable
        strSql = "select * from List_ProjectName where CreateBy='" + hide_user.Value + "' and Document_No is null "
        DT_List = C.GetDataTable(strSql)
        If DT_List.Rows.Count > 0 Then
            strSql = "select * from tmpList_CT where CreateBy='" + hide_user.Value + "' and (ProjectName_id = '" & DT_List.Rows(0).Item("id_List") & "' or isnull(ProjectName_id, '') = '') "
            DT = C.GetDataTable(strSql)
        Else
            strSql = "select * from tmpList_CT where CreateBy='" + hide_user.Value + "' and  isnull(ProjectName_id, '') = '' "
            DT = C.GetDataTable(strSql)
        End If

        'Else
        'strSql = "select * from tmpList_CT where CreateBy='" + hide_user.Value + "' and isnull(ProjectName_id, '') = '' "
        'DT = C.GetDataTable(strSql)
        'End If

        Return DT
    End Function

    Protected Sub delete_Command(ByVal sender As Object, ByVal e As CommandEventArgs)

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
    End Sub

    Protected Sub txtContract_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContract.TextChanged
        If IsNumeric(txtContract.Text) Then
            txtContract.Text = CInt(txtContract.Text)
            If txtContract.Text < 0 Then
                txtContract.Text = txtContract.Text * (-1)
            End If
        Else
            txtContract.Text = "0"
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Contract Period เป็นตัวเลขเท่านั้น!');", True)
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
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ Monthly (ราคาขาย) เป็นตัวเลขเท่านั้น!');", True)
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
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "bootbox.alert('ระบุ One-Time Payment เป็นตัวเลขเท่านั้น!');", True)
            txtOneTimePayment.Focus()
        End If
        CalculateSellingPrice()
    End Sub
    Protected Sub txtMarketing_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMarketing.TextChanged
        If IsNumeric(txtMarketing.Text) Then

        Else
            txtMarketing.Text = "0"
            lblTotalMarketing.Text = "0.00"
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtEntertainCustomer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEntertainCustomer.TextChanged
        If IsNumeric(txtEntertainCustomer.Text) Then

        Else
            txtEntertainCustomer.Text = "0"
            lblTotalEntertainCustomer.Text = "0.00"
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtGift_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGift.TextChanged
        If IsNumeric(txtGift.Text) Then

        Else
            txtGift.Text = "0"
            lblTotalGift.Text = "0.00"
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtPenaltyLate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPenaltyLate.TextChanged
        If IsNumeric(txtPenaltyLate.Text) Then

        Else
            txtPenaltyLate.Text = "0"
            lblTotalPenaltyLate.Text = "0.00"
        End If
        CalculateSellingPrice()
    End Sub

    Protected Sub txtPenalty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPenalty.TextChanged
        If IsNumeric(txtPenalty.Text) Then

        Else
            txtPenalty.Text = "0"
            lblTotalPenalty.Text = "0.00"
        End If
        CalculateSellingPrice()
    End Sub
End Class



