Imports System.Data
Imports System.Web.UI
Partial Class MasterPageMenu
    Inherits System.Web.UI.MasterPage
    Dim C As New Cls_Data
    Dim N As New auto_vb

    'Public MasterpageString As String = N.Get_Uemail(Request.QueryString("ue"), Session("uemail"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'If Not Page.IsPostBack Then
        Dim user As String = ""
        If Request.QueryString("ue") IsNot Nothing Then
            user = Request.QueryString("ue")
        Else
            user = Session("uemail")
        End If


        lblUser.Text = "<i class='fas fa-1x fa-user-alt'></i> " + user + " (Logout)"


        If Session("menu") IsNot Nothing
            hide_user_master.value = Session("menu")
        End If

        'If Not Page.IsPostBack Then
        Dim strSql As String
        Dim DT As New DataTable
        strSql = "select * from dbo.UserLogin where login_name = '" & user & "' "
        DT = C.GetDataTable(strSql)
        If DT.Rows.Count > 0 Then
            Session("Login_permission") = DT.Rows(0).Item("Login_permission")
            '----Link_URL_Main_Menu-----'
                Dim link_create = "project_name.aspx?menu=create"
                Dim link_upload = "project_name.aspx?menu=upload"
                Dim link_edit = "edit_list.aspx?menu=edit"
                Dim link_checkstatus = "check_status_list.aspx?menu=check"
                Dim link_approve = "approve_list.aspx?menu=approve"
                Dim link_insert = "insert_capex.aspx?menu=insert"
                'Dim link_contractfile = "project_contract.aspx?menu=contract"
                Create.HRef = link_create :Create_sm.HRef = link_create
                Upload.HRef = link_upload :Upload_sm.HRef = link_upload
                Edit.HRef = link_edit :Edit_sm.HRef = link_edit
                CheckStatus.HRef = link_checkstatus :CheckStatus_sm.HRef = link_checkstatus
                Approve.HRef = link_approve :Approve_sm.HRef = link_approve
                Insert.HRef = link_insert :Insert_sm.HRef = link_insert
                'ContractFile.HRef = link_contractfile
            '----Visible_Link_Main_Menu-----'
                Create.Visible = True :Create_sm.Visible = True
                Upload.Visible = True :Upload_sm.Visible = True
                Edit.Visible = True :Edit_sm.Visible = True
                CheckStatus.Visible = True :CheckStatus_sm.Visible = True
                'ContractFile.Visible = True
                Approve.Visible = True :Approve_sm.Visible = True
                Insert.Visible = True :Insert_sm.Visible = True
            If Session("Login_permission") = "administrator" Then
                Create.Attributes("class") = "list-group-item active"            
                Create_sm.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "inspector" Then
                Create.Visible = False :Create_sm.Visible = False
                Upload.Visible = False :Upload_sm.Visible = False
                CheckStatus.Visible = False :CheckStatus_sm.Visible = False
                'ContractFile.Visible = False
                Approve.Attributes("class") = "list-group-item active"
                Approve_sm.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "user" Then
                Approve.Visible = False :Approve_sm.Visible = False
                Insert.Visible = False :Insert_sm.Visible = False
                Create.Attributes("class") = "list-group-item active"      
                Create_sm.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "approve" Or Session("Login_permission") = "approve_ro" Or Session("Login_permission") = "approve_cluster" Then  
                Create.Visible = False :Create_sm.Visible = False
                Upload.Visible = False :Upload_sm.Visible = False
                Edit.Visible = False :Edit_sm.Visible = False
                Insert.Visible = False :Insert_sm.Visible = False 
                Approve.Attributes("class") = "list-group-item active"               
                Approve_sm.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "presale" Then
                Insert.Visible = False :Insert_sm.Visible = False
                Create.Attributes("class") = "list-group-item active"      
                Create_sm.Attributes("class") = "list-group-item active"
            ElseIf Session("Login_permission") = "account" Or Session("Login_permission") = "carrier" Then
                Create.Visible = False : Create_sm.Visible = False
                Upload.Visible = False : Upload_sm.Visible = False
                Edit.Visible = False : Edit_sm.Visible = False
                Approve.Visible = False : Approve_sm.Visible = False
                Insert.Visible = False : Insert_sm.Visible = False
                CheckStatus.Attributes("class") = "list-group-item active"
                CheckStatus_sm.Attributes("class") = "list-group-item active"
            End If
        Else
            Session.Clear()
            Response.Redirect("~/index.aspx?alert=1")
        End If
        If Session("menu") IsNot Nothing Then
            Dim Menu_Active = "list-group-item active"
            Dim Menu_NotActive = "list-group-item"

            Create.Attributes("class") = Menu_NotActive:Create_sm.Attributes("class") = Menu_NotActive
            Upload.Attributes("class") = Menu_NotActive:Upload_sm.Attributes("class") = Menu_NotActive
            Edit.Attributes("class") = Menu_NotActive:Edit_sm.Attributes("class") = Menu_NotActive
            CheckStatus.Attributes("class") = Menu_NotActive:CheckStatus_sm.Attributes("class") = Menu_NotActive
            'ContractFile.Attributes("class") = Menu_NotActive
            Approve.Attributes("class") = Menu_NotActive:Approve_sm.Attributes("class") = Menu_NotActive
            Insert.Attributes("class") = Menu_NotActive:Insert_sm.Attributes("class") = Menu_NotActive

            If Session("menu") = "create" Then
                Create.Attributes("class") = Menu_Active :Create_sm.Attributes("class") = Menu_Active
            ElseIf Session("menu") = "upload" Then
                Upload.Attributes("class")= Menu_Active :Upload_sm.Attributes("class") = Menu_Active
            ElseIf Session("menu") = "edit" Then
                Edit.Attributes("class") = Menu_Active :Edit_sm.Attributes("class") = Menu_Active
            ElseIf Session("menu") = "approve" Then
                Approve.Attributes("class") = Menu_Active :Approve_sm.Attributes("class") = Menu_Active
            ElseIf Session("menu") = "insert" Then
                Insert.Attributes("class") = Menu_Active :Insert_sm.Attributes("class") = Menu_Active
            ElseIf Session("menu") = "check" Then
                CheckStatus.Attributes("class") = Menu_Active :CheckStatus_sm.Attributes("class") = Menu_Active 
            ElseIf Session("menu") = "contract" Then
                'ContractFile.Attributes("class") = Menu_Active
            End If
        End If
        
    'End If
        'End If

        'If Session("Login_permission") <> "administrator" Then
        '    Response.Redirect("serverclose.aspx")
        'End If

    End Sub
    ' Protected Sub main_button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles main_button.Click
    '     'hide_user_master.value = "0"
    '     Session("menu_value") = 0
    ' End Sub
    ' Protected Sub sm_button_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '     hide_user_master.value = "1"
    '     'ScriptManager.RegisterStartupScript(page, page.GetType(),"AAA", "small_sidebar();", False)
    '     Session("menu_value") = 1
    ' End Sub
    Protected Sub lblUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUser.Click
        Session.Clear()
        'Response.Redirect("~/index.aspx")
        Response.Redirect("https://api.jasmine.com/authen1/jaslogout-page")
    End Sub
End Class

