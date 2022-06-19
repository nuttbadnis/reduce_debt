Imports System.Data
Partial Class check_status_list
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim CF As New Cls_RequestFlow
    Dim Cal As New Cls_Calculate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            'Dim DT_List As DataTable
            Dim strSql As String
            Dim strRO As String
            Dim strCluster As String

            'strSql = "select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date " + vbCr
            'strSql += ", name.Area, name.Cluster, name.Project_Name, name.Customer_Name " + vbCr
            'strSql += ", case doc.request_status when 100 then 'ดำเนินการเรียบร้อย' when 105 then 'ไม่อนุมัติ' else re.status_name end 'status_name' " + vbCr
            'strSql += "from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No " + vbCr
            'strSql += "left outer join request_status re on doc.request_status   = re.status_id " + vbCr
            'strSql += "where doc.CreateBy = '" + Session("uemail") + "' "
            'If Session("Login_permission") = "user" Or Session("Login_permission") = "administrator" Then
            '    DT_List = C.GetDataTable(strSql)
            '    GridView1.DataSource = DT_List
            '    GridView1.DataBind()
            'End If
            'txtDocumentDate.Text = Now.Day.ToString("00") + "/" + Now.Month.ToString("00") + "/" + Now.Year.ToString("0000")

            strSql = "select distinct case doc.request_status when 100 then 'อนุมัติ' when 105 then 'ไม่อนุมัติ' when 50 then 'รอพิจารณา' when 55 then 'รอพิจารณา' else re.status_name end 'status_name' " + vbCr
            strSql += ", case  doc.request_status when 55 then 50 when 0 then 50 else doc.request_status end as 'request_status' " + vbCr
            strSql += "from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No " + vbCr
            strSql += "left outer join request_status re on doc.request_status   = re.status_id " + vbCr
            'strSql += "where doc.CreateBy = '" + Session("uemail") + "' " + vbCr
            strSql += "order by 2 "
            C.SetDropDownList(ddlStatus, strSql, "status_name", "request_status", "-- โครงการปัจจุบัน --")
            ddlStatus.Items.Add(New ListItem("-- โครงการทั้งหมด --", "99"))

            strRO = "select distinct RO from Cluster where Status = '1' order by RO "
            C.SetDropDownList(ddlArea, strRO, "RO", "RO", "-- ทั้งหมด --")

            strCluster = "select distinct RO, Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by RO, Cluster "
            C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster", "-- ทั้งหมด --")

            ShowData()
        Else
            If GridView1.Rows.Count > 0 Then
                GridView1.UseAccessibleHeader = True
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
            End If
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).NavigateUrl = "~/Approve.aspx?request_id=" + CType(e.Row.Cells(0).FindControl("Link_DocumentID"), HyperLink).Text
            If (Session("Login_permission") = "user" Or Session("Login_permission") = "administrator" Or Session("Login_permission") = "presale") And C.Check_Branch_User(Session("uemail")) = "1" Then
                CType(e.Row.Cells(0).FindControl("btn_Duplicate"), LinkButton).Visible = True
            Else
                CType(e.Row.Cells(0).FindControl("btn_Duplicate"), LinkButton).Visible = False
            End If

            If CType(e.Row.FindControl("lblGetJob"), Label).Text = "1" Then
                CType(e.Row.FindControl("lblRefNo"), Label).ForeColor = Drawing.Color.Red
                CType(e.Row.FindControl("lblRefNo"), Label).ToolTip = "ไม่ได้งาน"
            End If

            If e.Row.Cells(6).Text <> Session("uemail") Then
                CType(e.Row.FindControl("btn_EditProjectcode"), LinkButton).Visible = False
            End If
        End If
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        ShowData()
        GridView1.DataBind()
    End Sub

    Public Sub ShowData()
        Dim DT_List As DataTable
        Dim strSql As String
        strSql = "select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date " + vbCr
        strSql += ", name.Area, name.Cluster, name.Project_Name, name.Customer_Name, name.Project_Code, isnull(name.Get_Job,'0') 'Get_Job' " + vbCr
        strSql += ", case doc.request_status when 100 then 'อนุมัติ' when 105 then 'ไม่อนุมัติ' when 50 then 'รอพิจารณา' when 55 then 'รอพิจารณา' else re.status_name end 'status_name', doc.CreateBy " + vbCr
        strSql += "from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No " + vbCr
        strSql += "left outer join request_status re on doc.request_status   = re.status_id " + vbCr
        strSql += "Where '1' = '1' "
        If Session("Login_permission") = "user" Then
            strSql += "and doc.CreateBy = '" + Session("uemail") + "' " + vbCr
        ElseIf Session("Login_permission") = "approve_ro" Or Session("Login_permission") = "presale" Then
            strSql += "and name.Area = '" & C.RO_User(Session("uemail")) & "' "
        ElseIf Session("Login_permission") = "approve_cluster" Then
            Dim DT_C As New DataTable
            DT_C = C.GetDataTable("select * from Cluster where Cluster_email = '" & Session("uemail") & "' and Status='1' ")
            If DT_C.Rows.Count > 0 Then
                strSql += "and name.Cluster in ( "
                For i As Integer = 0 To DT_C.Rows.Count - 1
                    If i = 0 Then
                        strSql += "'" & DT_C.Rows(i).Item("Cluster") & "'"
                    Else
                        strSql += ",'" & DT_C.Rows(i).Item("Cluster") & "'"
                    End If
                Next
                strSql += ") " + vbCr
            Else
                strSql += "and '1' = '0' "
            End If
            'strSql += "and name.Cluster = '" & C.Cluster_User(Session("uemail")) & "' "
        ElseIf Session("Login_permission") = "carrier" Then
            Dim DT_C As New DataTable
            DT_C = C.GetDataTable("select * from UserBranch where Login_name = '" & Session("uemail") & "' ")
            If DT_C.Rows.Count > 0 Then
                If DT_C.Rows(0).Item("RO") <> "ALL" Then
                    If DT_C.Rows(0).Item("Cluster") <> "ALL" Then
                        strSql += "and name.Cluster = '" & DT_C.Rows(0).Item("Cluster") & "' "
                    Else
                        strSql += "and name.Area = '" & DT_C.Rows(0).Item("RO") & "' "
                    End If
                End If
            Else
                strSql += "and '1' = '0' "
            End If
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            If ddlStatus.SelectedValue <> "99" Then
                strSql += "and case doc.request_status when 100 then 'อนุมัติ' when 105 then 'ไม่อนุมัติ' when 50 then 'รอพิจารณา' when 55 then 'รอพิจารณา' else re.status_name end = '" + ddlStatus.SelectedItem.Text + "' "
            End If
        Else
            strSql += "and doc.request_status <> '90' " + vbCr
        End If

        If ddlArea.SelectedIndex <> 0 Then
            strSql += "and name.Area = '" + ddlArea.SelectedValue + "' "
        End If

        If ddlCluster.SelectedIndex <> 0 Then
            strSql += "and name.Cluster = '" + ddlCluster.SelectedValue + "' "
        End If

        If txtDocumentDate.Text <> "" Then
            strSql += "and CONVERT(varchar(10),name.Document_Date,103) = '" + txtDocumentDate.Text + "' "
        End If

        If tbAuto.Text <> "" Then
            If tbAuto.Text.Contains(" :: ") Then
                Dim vText() As String
                vText = Split(tbAuto.Text, " :: ")
                strSql += "and (doc.Document_No like '%" + vText(0) + "%' and name.Project_Name like '%" + vText(1) + "%') "
            Else
                strSql += "and (name.Project_Name like '%" + tbAuto.Text + "%' or name.Customer_Name like '%" + tbAuto.Text + "%' or doc.Document_No like '%" + tbAuto.Text + "%') "
            End If

        End If

        strSql += " order by doc.CreateDate desc "
        'Response.Write(strSql)
        Dim DT As New DataTable
        DT = C.GetDataTable("select * from Permission where Permission_Type = '" & Session("Login_permission") & "'")
        'If Session("Login_permission") = "user" Or Session("Login_permission") = "administrator" Or Session("Login_permission") = "approve" Or Session("Login_permission") = "approve_ro" Or Session("Login_permission") = "approve_cluster" Or Session("Login_permission") = "presale" Then
        If DT.Rows.Count > 0 Then
            DT_List = C.GetDataTable(strSql)
            GridView1.DataSource = DT_List
            GridView1.DataBind()

            If DT_List.Rows.Count > 0 Then
                lblNoData.Visible = False
                GridView1.UseAccessibleHeader = True
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                lblNoData.Visible = True
            End If
        End If

    End Sub

    Protected Function GetStep_Flow(Document_No As String) As String
        Dim step_data As String
        Dim DT_List As DataTable
        Dim strSql As String = ""
        strSql = "SELECT (CONVERT(varchar,sum(case when flow_complete = 1 then 1 else 0 end))+'/'+CONVERT(varchar,count(*))) as current_flow" + vbCr
        strSql += " from request_flow" + vbCr
        strSql += " where disable = 0 and next_step <> 'end'" + vbCr
        strSql += " and request_id = '" + Document_No + "' "
        DT_List = C.GetDataTable(strSql)
        If DT_List.Rows.Count > 0 Then
            step_data = DT_List.Rows(0).Item("current_flow")
        End If
        Return step_data
    End Function

    Protected Sub LinkButton_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim test As String
        test = "You chose: " & e.CommandName & " Item " + e.CommandArgument
        loadDetail(e.CommandArgument)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "alert('ไม่สามารถบันทึกข้อมูลได้"+test+"');", True)
    End Sub

    Protected Sub LinkButton_Duplicate(ByVal sender As Object, ByVal e As CommandEventArgs)
        DuplicateProject(e.CommandArgument)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "alert('ไม่สามารถบันทึกข้อมูลได้"+test+"');", True)
    End Sub

    Protected Sub LinkButton_EditProjectcode(ByVal sender As Object, ByVal e As CommandEventArgs)
        loadProjectCode(e.CommandArgument, e.CommandName)

    End Sub

    Sub loadDetail(ByVal vRequest_id As String)
        Dim request_status As String
        Dim request_step As String
        Dim create_uemail As String
        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from FeasibilityDocument " + vbCr
        strSql += "where Document_No = '" + vRequest_id + "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            create_uemail = DT.Rows(0).Item("CreateBy")
            request_status = DT.Rows(0).Item("request_status")
            request_step = DT.Rows(0).Item("request_step")
            loadFlow(vRequest_id, request_status, request_step, create_uemail)
        End If
        strSql = "select * from dbo.List_ProjectName where Document_No='" + vRequest_id + "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            TextHeaderDetail.InnerHtml = vRequest_id & " " & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ผู้เสนอโครงการ: " & DT.Rows(0).Item("Customer_Assistant_Name")
        End If
    End Sub

    Sub loadFlow(ByVal vRequest_id As String, ByVal vRequest_status As String, ByVal vRequest_step As String, ByVal create_uemail As String)
        Dim vRequest_permiss As Integer = CF.rViewDetail(vRequest_id, create_uemail)
        inn_flow.InnerHtml = CF.rLoadFlowBody_Detail(vRequest_id, vRequest_status, vRequest_step, vRequest_permiss, 1)
        Dim myModal As String
        myModal = "$('#myModal').modal('show');" + vbCr
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
        myModal += "        $draggable.closest('#myModal').one('bs.modal.hide', function () {" + vbCr
        myModal += "            $('body').off('mousemove.draggable');" + vbCr
        myModal += "        });" + vbCr
        myModal += "    });" + vbCr
        ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", myModal, True)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "$('#myModal').modal('show');", True)
        'ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)

    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        ShowData()
    End Sub

    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        Dim strcluster As String
        If ddlArea.SelectedIndex = 0 Then
            strcluster = "select distinct RO, Cluster from Cluster where Status = '1' order by RO, Cluster "
        Else
            strcluster = "select distinct RO, Cluster from Cluster where Status = '1' and RO = '" + ddlArea.SelectedValue + "' order by RO, Cluster "
        End If
        C.SetDropDownList(ddlCluster, strcluster, "Cluster", "Cluster", "-- ทั้งหมด --")

        ShowData()
    End Sub

    Protected Sub ddlCluster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCluster.SelectedIndexChanged
        ShowData()
    End Sub

    Protected Sub txtDocumentDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDocumentDate.TextChanged
        ShowData()
    End Sub

    Protected Sub searchauto_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles searchauto.Click

        'Dim DT_List As DataTable
        'Dim strSql As String
        'strSql = "select doc.Document_No, CONVERT(varchar(10),name.Document_Date,103) Document_Date " + vbCr
        'strSql += ", name.Area, name.Cluster, name.Project_Name, name.Customer_Name " + vbCr
        'strSql += ", case doc.request_status when 100 then 'ดำเนินการเรียบร้อย' when 105 then 'ไม่อนุมัติ' else re.status_name end 'status_name' " + vbCr
        'strSql += "from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No " + vbCr
        'strSql += "left outer join request_status re on doc.request_status   = re.status_id " + vbCr
        'strSql += "where doc.CreateBy = '" + Session("uemail") + "' " + vbCr
        ShowData()

    End Sub

    Sub loadProjectCode(ByVal vRequest_id As String, ByVal ProjectCode As String)
        Dim strSql As String
        Dim DT As New DataTable
        lblDocNo.Text = vRequest_id
        strSql = "select *, convert(varchar(10),Service_Date,103) 'Service_Date_Show' from List_ProjectName where isnull(Document_No,'') = '" & vRequest_id & "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            txtProjectCode.Text = DT.Rows(0).Item("Project_Code")
            If DT.Rows(0).Item("Get_Job") = "1" Then
                rbtWork.Checked = False
                rbtNotWork.Checked = True
            Else
                rbtNotWork.Checked = False
                rbtWork.Checked = True
            End If
            txtServiceDate.Text = DT.Rows(0).Item("Service_Date_Show")
            txtProjectOwner.Text = DT.Rows(0).Item("Detail_ProjectOwner")
            ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "$('#divEditProjectCode').modal('show');", True)
        End If
    End Sub

    Sub DuplicateProject(ByVal vRequest_id As String)
        Dim result As String
        result = Cal.DuplicateProject(Session("uemail"), "Duplicate", vRequest_id)
        If result = "Complete_Upload" Then
            ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('Duplicate Project สำเร็จ', function(){ window.location = 'project_name.aspx?menu=upload'; });", True)
        ElseIf result = "Complete" Then
            ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('Duplicate Project สำเร็จ', function(){ window.location = 'project_name.aspx?menu=create'; });", True)
        Else
            ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertError('" & C.rpQuoted(result) & "', function(){ window.location = 'check_status_list.aspx?menu=check'; });", True)
        End If
    End Sub


    Protected Sub btnSaveProjectCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveProjectCode.Click
        Dim strsql As String
        Dim getJob As String
        If rbtWork.Checked = True Then
            getJob = "0"
        Else
            getJob = "1"
        End If
        Try
            strsql = "Update List_ProjectName set Project_Code = '" & Replace(Replace(txtProjectCode.Text.Trim, " ", ""), "'", "") & "', Get_Job = '" & getJob & "', Detail_ProjectOwner = '" & Replace(txtProjectOwner.Text.Trim, "'", "") & "', Service_Date='" & C.CDateText(txtServiceDate.Text) & "' where isnull(Document_No,'') = '" & lblDocNo.Text & "' "
            C.ExecuteNonQuery(strsql)
            ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertSuccess('แก้ไข Project Code สำเร็จ', function(){ window.location = 'check_status_list.aspx?menu=check'; });", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "success_update", "AlertError('" & C.rpQuoted(ex.Message) & "', function(){ window.location = 'check_status_list.aspx?menu=check'; });", True)
        End Try

    End Sub
End Class
