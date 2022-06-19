Imports System.Data
Imports System.Web.Script.Serialization
Imports System.Collections.Generic

Partial Class _json
    Inherits System.Web.UI.Page
    Dim query As New Cls_Data
    Dim CP As New Cls_Panu

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim qrs As String = ""

        If Request.QueryString("qrs") <> Nothing Then
            qrs = Request.QueryString("qrs")
        End If


        If qrs = "autocomplete_capex" Then
            autoComplete_Capex(Request.QueryString("term"), Request.QueryString("mgroup"), Request.QueryString("sgroup"))
        End If

        If qrs = "autocomplete_capexmass" Then
            autoComplete_CapexMass(Request.QueryString("term"))
        End If

        If qrs = "autocomplete_opex" Then
            autoComplete_Opex(Request.QueryString("term"), Request.QueryString("mgroup"), Request.QueryString("sgroup"))
        End If

        If qrs = "autocomplete_other" Then
            autoComplete_Other(Request.QueryString("term"), Request.QueryString("mgroup"), Request.QueryString("sgroup"))
        End If

        If qrs = "autocomplete_management" Then
            autoComplete_Management(Request.QueryString("term"), Request.QueryString("mgroup"), Request.QueryString("sgroup"))
        End If

        If qrs = "autocomplete_check_status" Then
            autoComplete_Check_Status(Request.QueryString("term"))
        End If

    End Sub

    Protected Sub autoComplete_Capex(ByVal vTerm As String, ByVal MainGroup As String, ByVal SubGroup As String)
        Dim dt As DataTable
        Dim vSqlIn As String = ""
        Try
            vSqlIn = "select Item_Code + ' :: ' + replace(replace(replace(CAPEX_Name,'""','นิ้ว'),',','&'),':',' ต่อ ') as 'CAPEX_Name' from capex " + vbCr
            vSqlIn += "where (Item_Code like '%" + vTerm + "%' or CAPEX_Name like '%" + vTerm + "%' or Main_Group like '%" + vTerm + "%') " + vbCr
            If MainGroup <> "" Then
                vSqlIn += "and Main_Group = '" & MainGroup & "' "
            End If
            If SubGroup <> "" Then
                vSqlIn += "and Sub_Group = '" & SubGroup & "' "
            End If
            dt = query.GetDataTable(vSqlIn)
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลสำเร็จ " + dt.Rows(0).Item("CAPEX_Name") + "');", True)
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลไม่สำเร็จ');", True)
        End Try
        'Response.Write(vSqlIn)
        Response.Write(CP.rJsonDBv4s(vSqlIn, "DB106"))
        ' Response.Write(CP.rGetDataTable(vSqlIn, "DB105"))

    End Sub

    Protected Sub autoComplete_CapexMass(ByVal vTerm As String)
        Dim dt As DataTable
        Dim vSqlIn As String = ""
        Try
            vSqlIn = "select Item_Code + ' :: ' + replace(replace(replace(CAPEX_Mass_Name,'""','นิ้ว'),',','&'),':',' ต่อ ') as 'CAPEX_Mass_Name' from capex_mass "
            vSqlIn += "where Item_Code like '%" + vTerm + "%' or CAPEX_Mass_Name like '%" + vTerm + "%' or Main_Group like '%" + vTerm + "%' "
            dt = query.GetDataTable(vSqlIn)
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลสำเร็จ " + dt.Rows(0).Item("CAPEX_Name") + "');", True)
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลไม่สำเร็จ');", True)
        End Try
        'Response.Write(vSqlIn)
        Response.Write(CP.rJsonDBv4s(vSqlIn, "DB106"))
        ' Response.Write(CP.rGetDataTable(vSqlIn, "DB105"))

    End Sub

    Protected Sub autoComplete_Opex(ByVal vTerm As String, ByVal MainGroup As String, ByVal SubGroup As String)
        Dim dt As DataTable
        Dim vSqlIn As String = "select * from opex "
        Try
            vSqlIn += "where (OPEX_Code like '%" + vTerm + "%' or OPEX_Name like '%" + vTerm + "%' or Main_Group like '%" + vTerm + "%') "
            If MainGroup <> "" Then
                vSqlIn += "and Main_Group = '" & MainGroup & "' "
            End If
            If SubGroup <> "" Then
                vSqlIn += "and Sub_Group = '" & SubGroup & "' "
            End If
            dt = query.GetDataTable(vSqlIn)
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลสำเร็จ " + dt.Rows(0).Item("CAPEX_Name") + "');", True)
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลไม่สำเร็จ');", True)
        End Try

        Response.Write(CP.rJsonDBv4s(vSqlIn, "DB106"))
        ' Response.Write(CP.rGetDataTable(vSqlIn, "DB105"))

    End Sub

    Protected Sub autoComplete_Other(ByVal vTerm As String, ByVal MainGroup As String, ByVal SubGroup As String)
        Dim dt As DataTable
        Dim vSqlIn As String = "select * from other "
        Try
            vSqlIn += "where (OTHER_Code like '%" + vTerm + "%' or OTHER_Name like '%" + vTerm + "%' or Main_Group like '%" + vTerm + "%') "
            If MainGroup <> "" Then
                vSqlIn += "and Main_Group = '" & MainGroup & "' "
            End If
            If SubGroup <> "" Then
                vSqlIn += "and Sub_Group = '" & SubGroup & "' "
            End If
            dt = query.GetDataTable(vSqlIn)
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลสำเร็จ " + dt.Rows(0).Item("CAPEX_Name") + "');", True)
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลไม่สำเร็จ');", True)
        End Try

        Response.Write(CP.rJsonDBv4s(vSqlIn, "DB106"))
        ' Response.Write(CP.rGetDataTable(vSqlIn, "DB105"))

    End Sub

    Protected Sub autoComplete_Management(ByVal vTerm As String, ByVal MainGroup As String, ByVal SubGroup As String)
        Dim dt As DataTable
        Dim vSqlIn As String = "select * from Management "
        Try
            vSqlIn += "where (Management_Code like '%" + vTerm + "%' or Management_Name like '%" + vTerm + "%' or Main_Group like '%" + vTerm + "%') "
            If MainGroup <> "" Then
                vSqlIn += "and Main_Group = '" & MainGroup & "' "
            End If
            If SubGroup <> "" Then
                vSqlIn += "and Sub_Group = '" & SubGroup & "' "
            End If
            dt = query.GetDataTable(vSqlIn)
            Catch ex As Exception
            'ClientScript.RegisterStartupScript(Page.GetType, "", "alert('บันทึกข้อมูลไม่สำเร็จ');", True)
        End Try

        Response.Write(CP.rJsonDBv4s(vSqlIn, "DB106"))

    End Sub

    Protected Sub autoComplete_Check_Status(ByVal vTerm As String)
        Dim dt As DataTable
        Dim vSqlIn As String
        vSqlIn = "select doc.Document_No + ' :: ' + name.Project_Name  as 'Project_Name' " + vbCr
        vSqlIn += "from FeasibilityDocument doc inner join List_ProjectName name on doc.Document_No = name.Document_No " + vbCr
        vSqlIn += "left outer join request_status re on doc.request_status   = re.status_id " + vbCr
        vSqlIn += "where doc.CreateBy = '" + Session("uemail") + "' " + vbCr
        vSqlIn += "and (name.Project_Name like '%" + vTerm + "%' or name.Customer_Name like '%" + vTerm + "%' or doc.Document_No like '%" + vTerm + "%') "

        dt = query.GetDataTable(vSqlIn)
        
        Response.Write(CP.rJsonDBv4s(vSqlIn, "DB106"))
    End Sub

End Class
