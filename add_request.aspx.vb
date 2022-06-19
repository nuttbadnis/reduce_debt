Imports System.Data
Partial Class add_request
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            Dim user_value as string
            user_value = Session("uemail")
            If Session("uemail") = "" Then
                user_value = Request.QueryString("ue")
            End If
            hide_user_value.value = user_value

            Dim strSubjectId As String
            Dim strPermissionId As String
            Dim strRo As String
            Dim strCluster As String

            strSubjectId = "select subject_id, subject_name from dbo.Subject where flow_id= 99 and disable = '0' order by subject_id "
            C.SetDropDownList(ddlSubjectId, strSubjectId, "subject_name", "subject_id")
            strPermissionId = "select Permission_type, Permission_name from dbo.permission where status = '0' "
            C.SetDropDownList(ddlPermissionId, strPermissionId, "Permission_name", "Permission_type")
            strRo = "select Ro from dbo.cluster group by Ro "
            C.SetDropDownList(ddlRo, strRo, "Ro", "Ro")
            strCluster = "select Cluster from dbo.cluster group by Cluster "
            C.SetDropDownList(ddlCluster, strCluster, "Cluster", "Cluster")

            ddlSubjectId.Items.Insert(0, New ListItem("--- ���͡�������Ӣ� ---", "0"))
            ddlRo.Items.Insert(0, New ListItem("--- ���͡ RO ---", "0"))
            ddlCluster.Items.Insert(0, New ListItem("--- ���͡ Cluster ---", "0"))
            ddlPermissionId.Items.Insert(0, New ListItem("--- ���͡�Է���㹡����ҹ ---", "0"))
            
        End If
    End Sub

    Protected Sub ddlSubjectId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlSubjectId.SelectedItem.Value = 992001 or ddlSubjectId.SelectedItem.Value = 993001 Then
            edit_user_form.Attributes.CssStyle.Add("display", "block")
            txtFullName.Attributes.Add("readonly", "readonly")
            txtEmail.Attributes.Add("readonly", "readonly")
            'ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�������Ӣ� = "+ddlSubjectId.SelectedItem.Value+"');", True)
        Else
            edit_user_form.Attributes.CssStyle.Add("display", "none")
            txtFullName.Attributes.Remove("readonly")
            txtEmail.Attributes.Remove("readonly")
            'ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�������Ӣ� > 0');", True)
        End If
    End Sub

    Protected Sub ddlPermissionId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If ddlPermissionId.SelectedItem.Value = "approve_ro" Then
            add_form_cluster.Attributes.CssStyle.Add("display", "none")
            'ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�������Ӣ� = "+ddlSubjectId.SelectedItem.Value+"');", True)
        Else
            add_form_cluster.Attributes.CssStyle.Add("display", "block")
            'ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('�������Ӣ� > 0');", True)
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strSql As String

        If ddlSubjectId.SelectedValue = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('��س����͡ <b style=\'font-weight:bold;\'>�������Ӣ�</b> �͡���!');", True)
            ddlSubjectId.Focus()       
        ElseIf txtFullName.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('��سҡ�͡ <b style=\'font-weight:bold;\'>���ͨ�ԧ</b> �����ҹ!');", True)
            txtFullName.Focus()
        ElseIf txtEmail.Text = "" Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('��سҡ�͡ <b style=\'font-weight:bold;\'>Login Name</b> �����ҹ!');", True)
            txtEmail.Focus()
        ElseIf ddlRo.SelectedValue = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('��س����͡ <b style=\'font-weight:bold;\'>Ro</b> �͡���!');", True)
            ddlRo.Focus()
        ' ElseIf ddlCluster.SelectedValue = "0" Then
        '     ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('��س����͡ <b style=\'font-weight:bold;\'>Cluster</b> �͡���!');", True)
        '     ddlCluster.Focus()
        ElseIf ddlPermissionId.SelectedValue = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('��س����͡ <b style=\'font-weight:bold;\'>�Է���㹡����ҹ</b> �͡���!');", True)
            ddlPermissionId.Focus()                  
        Else
            Try
                Dim DT As DataTable
                    strSql = "DECLARE @request_runid varchar(12) "
                    strSql += "SET @request_runid  = (SELECT 'R'+subject_prefix from subject where subject_id = '"+ddlSubjectId.SelectedValue+"') + (RIGHT(LEFT(CONVERT(varchar, GETDATE(),112),6),4)) "
                    strSql += "SET @request_runid = @request_runid + (SELECT RIGHT('00000' + CAST(count(1)+1 AS VARCHAR(5)), 5) from request where subject_id = '"+ddlSubjectId.SelectedValue+"') "
                    strSql += "Insert Into Request (Request_id, Subject_id, login_name, full_name, ro, cluster, "
                    strSql += "login_permission, request_status, create_date, create_by) "
                    strSql += "VALUES (@request_runid "
                    strSql += ",'" + ddlSubjectId.SelectedValue + "' "   
                    strSql += ",'" + txtEmail.Text + "' "
                    strSql += ",'" + txtFullName.Text + "' "
                    strSql += ",'" + ddlRo.SelectedValue + "' "
                    strSql += ",'" + ddlCluster.SelectedValue + "' "
                    strSql += ",'" + ddlPermissionId.SelectedValue + "' " 
                    strSql += ",'0',GETDATE(), '"+hide_user_value.value +"') "                      

                'Response.write(strSql)
                ' If ddlPermissionId.selectedValue = "approve_cluster" Then
                '     strSql += "Insert Into Cluster (RO, Cluster, Cluster_email, Cluster_name, Status) "
                '     strSql += "VALUES ('" + ddlRo.SelectedValue + "' "
                '     strSql += ",'" + ddlCluster.SelectedValue + "' "
                '     strSql += ",'" + txtEmail.Text + "' "
                '     strSql += ",'" + txtFullName.Text + "' "
                '     strSql += ",'1') "
                ' End If

                ' If ddlPermissionId.selectedValue = "presale" Then
                '     strSql += "Insert Into Ro_Director (RO, Ro_email, Ro_name, Status, Permission_Type) "
                '     strSql += "VALUES ('" + ddlRo.SelectedValue + "' "
                '     strSql += ",'" + txtEmail.Text + "' "
                '     strSql += ",'" + txtFullName.Text + "' "
                '     strSql += ",'1' "
                '     strSql += ",'Presale') "
                ' Else If ddlPermissionId.selectedValue = "approve_ro" Then
                '     strSql += "Insert Into Ro_Director (RO, Ro_email, Ro_name, Status, Permission_Type) "
                '     strSql += "VALUES ('" + ddlRo.SelectedValue + "' "
                '     strSql += ",'" + txtEmail.Text + "' "
                '     strSql += ",'" + txtFullName.Text + "' "
                '     strSql += ",'1' "
                '     strSql += ",'RO') "
                ' End If

                C.ExecuteNonQuery(strSql)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertSuccess('���ҧ�Ӣ������', function() { focus;window.location.href='add_request.aspx?ue=nat.m'; });", True)
            Catch ex As Exception
                Response.write(strSql)
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertError('���ҧ�Ӣ���������', function() { focus; });", True)
            End Try
        End If
    End Sub
End Class
