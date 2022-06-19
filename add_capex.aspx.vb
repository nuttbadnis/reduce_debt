Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Collections.Generic
Imports System.Data.OleDb

Partial Class add_capex
    Inherits System.Web.UI.Page
    Dim C As New Cls_Data
    Dim N As New auto_vb
    Dim trow As New HtmlTableRow
    Dim tcell As New HtmlTableCell
    Dim table As New HtmlTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("code") <> "" Then
            SetOAuthSingleSignOn(Request.QueryString("code"))
            If Session("current_url") <> Nothing Then
                Response.Redirect(Session("current_url"))
            Else
                Response.Redirect("add_capex.aspx")
            End If

        ElseIf Session("token") = "" And Request.QueryString("token") = "" Then
            Response.Redirect("index.aspx")
        ElseIf Request.QueryString("token") <> "" Then
            Session("token") = Request.QueryString("token")
        End If

        ' If Session("capex_data") = "" Then
        '     Session("capex_data") = DropDownList1.SelectedItem.Text
        ' End If

        If Not Page.IsPostBack Then
            If Request.QueryString("menu") IsNot Nothing Then
                Session("menu") = Request.QueryString("menu")
            End If
            Dim DT_List As DataTable
            Dim r As String = ""
            'C.SetDropDownList(ddlGroup, "select distinct Main_Group from CAPEX order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            C.SetDropDownList(ddlGroup, "select Main_Group from CAPEX union select Main_Group from CAPEX_Mass order by Main_Group", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
            C.SetDropDownList(ddlSubGroup, "select Sub_Group from CAPEX union select Sub_Group from CAPEX_Mass order by Sub_Group", "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            'C.SetDropDownList(DropDownList1, "select CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            C.SetDropDownList(DropDownList1, "select CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX union select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code from CAPEX_Mass", "CAPEX_Name", "CAPEX_Name")
            C.ExecuteNonQuery("delete from tmpList_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            C.ExecuteNonQuery("insert into tmpList_CAPEX select CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is null and Status = '1' ")
            DT_List = C.GetDataTable("select * from tmpList_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            GridView1.DataSource = DT_List
            GridView1.DataBind()

                'C.SetDropDownList(ddlGroupMass, "select distinct Main_Group from CAPEX_Mass order by Main_Group ", "Main_Group", "Main_Group", "-- Group ทั้งหมด --")
                'C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass", "CAPEX_Mass_Name", "Item_Code")
                'C.ExecuteNonQuery("delete from tmpList_CAPEX_Mass where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
                'C.ExecuteNonQuery("insert into tmpList_CAPEX_Mass select CAPEX_Mass_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from List_CAPEX_Mass where CreateBy = '" + Session("uemail") + "' and Document_No is null and Status = '1' ")
                'DT_List = C.GetDataTable("select * from tmpList_CAPEX_Mass where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
                'GridView2.DataSource = DT_List
                'GridView2.DataBind()

            txtOther.Visible = False
            btnAddOther.Visible = False
                'txtOtherMass.Visible = False
        End If

        'Label1.Text = test.InnerText
    End Sub

    'Protected Sub GetDropDownListValues(sender As Object, e As EventArgs)
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        SelectItemToGridview()
    End Sub

    Protected Sub btnAddOther_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOther.Click
        SelectItemToGridview()
    End Sub

    Public Sub SelectItemToGridview()
        Dim DT As New DataTable
        Dim DT_List As New DataTable
        Dim strSql As String = ""
        Dim r As String = ""

        Dim sql_capex As String = "SELECT * FROM ("
        sql_capex += "select CAPEX_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group, Asset_Type, Equipment_Cost from CAPEX "
        'sql_capex += "union "
        'sql_capex += "select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group, Asset_Type, Equipment_Cost from CAPEX_Mass "
        sql_capex += ") as capex_all "

        'If IsNumeric(TextBox4.Text) = True Then
        'TextBox4.Text = Math.Round(CDbl(TextBox4.Text), 0)
        If chkOther.Checked = True Then
            If Replace(txtOther.Text, " ", "") = "" Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล อื่นๆ');focus();", True)
                txtOther.Focus()
            ElseIf txtOther.Text.Contains("'") = True Then
                ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูลอื่นๆ ได้"");focus();", True)
                txtOther.Focus()
            Else
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "',"
                    strSql += "Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "',"
                    strSql += "Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "',"
                    strSql += "Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "',"
                    strSql += "Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "',"
                    strSql += "Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "',"
                    strSql += "UpdateBy='" + Session("uemail") + "',"
                    strSql += "UpdateDate = getdate() "
                    strSql += "where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
                    C.ExecuteNonQuery(strSql)
                Next

                strSql = "insert into tmpList_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate)"
                strSql += "values('" + C.rpSpecialChars(txtOther.Text.ToString) + "','เช่าใช้','0.00','1','0.00','0.00','" + Session("uemail") + "',getdate()) "

            End If
        Else
            sql_capex += "where CAPEX_Name ='" + DropDownList1.SelectedValue + "' "
            DT = C.GetDataTable(sql_capex)
            'Response.Write(sql_capex)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "',"
                    strSql += "Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "',"
                    strSql += "Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "',"
                    strSql += "Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "',"
                    strSql += "Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "',"
                    strSql += "Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "',"
                    strSql += "UpdateBy='" + Session("uemail") + "',"
                    strSql += "UpdateDate = getdate() "
                    strSql += "where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
                    C.ExecuteNonQuery(strSql)
                Next

                strSql = "insert into tmpList_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Initial_Cost_perUnit, CreateBy, CreateDate) "
                strSql += "values('" + C.rpSpecialChars(DT.Rows(0).Item("CAPEX_Name").ToString) + "','" + DT.Rows(0).Item("Asset_Type").ToString + "','" + DT.Rows(0).Item("Equipment_Cost").ToString + "','1','" + (CDbl(DT.Rows(0).Item("Equipment_Cost")) * 1).ToString + "','" + DT.Rows(0).Item("Equipment_Cost").ToString + "','" + Session("uemail") + "',getdate()) "
            End If

        End If
        Try
            If strSql <> "" Then
                C.ExecuteNonQuery(strSql)
                txtOther.Text = ""
                DT_List = C.GetDataTable("select * from tmpList_CAPEX where CreateBy = '" + Session("uemail") + "'  and Document_No is null ")
                GridView1.DataSource = DT_List
                GridView1.DataBind()
            End If
        Catch ex As Exception
            Dim alert As String = N.alert_collapse(strSql, ex.Message)

            'Response.Write(strSql)
            ClientScript.RegisterStartupScript(Page.GetType, "Insert_Failed", "AlertError('ไม่สามารถบันทึกข้อมูลได้!<br/>" + alert + "');", True)
        End Try
        'Else
        'ClientScript.RegisterStartupScript(Page.GetType, "Alert", "AlertNotification('ระบุจำนวนเป็นตัวเลขจำนวนเต็มเท่านั้น!');", True)
        'TextBox4.Focus()
        'End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            CType(e.Row.Cells(4).FindControl("lblCost"), Label).Text = Format(CDbl(CType(e.Row.Cells(4).FindControl("lblCost"), Label).Text), "#,##0.00")
            'e.Row.Cells(2).Text = Format(CDbl(CType(e.Row.Cells(2).FindControl("lblPrice"), Label).Text), "###,##0.00")
            'C.SetDropDownList(CType(e.Row.Cells(1).FindControl("ddlAssetType"), DropDownList), "select * from AssetType where status='1' order by Asset_order", "Asset_Type", "Asset_id")
            'CType(e.Row.Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue = CType(e.Row.Cells(1).FindControl("lblID"), Label).Text
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim DT_List As DataTable
        Dim id_list As String
        Dim strSql As String

        For i As Integer = 0 To GridView1.Rows.Count - 1
            strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "', Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "', Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
            C.ExecuteNonQuery(strSql)
        Next

        If Not CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text Is Nothing Then
            id_list = CType(GridView1.Rows(e.RowIndex).Cells(1).FindControl("lblID"), Label).Text
            strSql = "delete from tmpList_CAPEX where id_List = '" + id_list + "' "
            'Response.Write(strSql)
            C.ExecuteNonQuery(strSql)
        End If

        'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
        'C.SetDropDownList(DropDownList1, "select CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX union select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code from CAPEX_Mass", "CAPEX_Name", "Item_Code")
        DT_List = C.GetDataTable("select * from tmpList_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
        GridView1.DataSource = DT_List
        GridView1.DataBind()

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        SaveData("Save")
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        SaveData("Next")
    End Sub

    Private Sub SaveData(ByVal SaveOrNext As String)
        Dim strSql As String
        Dim i As Integer

        If GridView1.Rows.Count > 0 Then
            For i = 0 To GridView1.Rows.Count - 1
                If Replace(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text, " ", "") = "" Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกข้อมูล Investment Cost');focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Focus()
                    Exit Sub
                End If
                If CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกข้อมูล Investment Cost ได้"");focus();", True)
                    CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Focus()
                    Exit Sub
                End If
                If Not IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกราคาเป็นตัวเลขเท่านั้น');focus();", True)
                    CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Focus()
                    Exit Sub
                End If
                If Not IsNumeric(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text) Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification('กรุณากรอกจำนวนเป็นตัวเลขเท่านั้น');focus();", True)
                    CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Focus()
                    Exit Sub
                End If
                If CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text.Contains("'") = True Then
                    ClientScript.RegisterStartupScript(Page.GetType, "", "AlertNotification(""ไม่สามารถใช้ตัว ' ในการกรอกหมายเหตุได้"");focus();", True)
                    CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Focus()
                    Exit Sub
                End If
            Next
            For i = 0 To GridView1.Rows.Count - 1
                strSql = "Update tmpList_CAPEX Set CAPEX_Name='" + C.rpSpecialChars(CType(GridView1.Rows(i).Cells(0).FindControl("txtCapexName"), TextBox).Text) + "', Asset_Type='" + CType(GridView1.Rows(i).Cells(1).FindControl("ddlAssetType"), DropDownList).SelectedValue + "', Cost_perUnit = '" + Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text), "#0.00") + "', Number = '" + CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text + "', Cost='" + Format(CDbl(CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text), "#0.00") + "', Remark='" + CType(GridView1.Rows(i).Cells(5).FindControl("TextBox4"), TextBox).Text + "', UpdateBy='" + Session("uemail") + "', UpdateDate = getdate() where id_List='" + CType(GridView1.Rows(i).Cells(1).FindControl("lblID"), Label).Text + "' "
                C.ExecuteNonQuery(strSql)
            Next
        End If
        C.ExecuteNonQuery("delete from List_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is null ")

        Try
            C.ExecuteNonQuery("insert into List_CAPEX (CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, Status, CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No) select CAPEX_Name, Asset_Type, Cost_perUnit, Number, Cost, Remark, Initial_Cost_perUnit, '1', CreateBy, CreateDate, UpdateBy, UpdateDate, Document_No from tmpList_CAPEX where CreateBy = '" + Session("uemail") + "' and Document_No is null ")
            If SaveOrNext = "Next" Then
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('บันทึกข้อมูลได้สำเร็จ',function(){ window.location = 'add_opex.aspx?menu=create'; });", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType, "success_insert", "AlertSuccess('บันทึกข้อมูลได้สำเร็จ');", True)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertError('ไม่สามารถบันทึกข้อมูลได้');", True)
        End Try
    End Sub

    Private Sub SetOAuthSingleSignOn(ByVal code As String)
        Dim DS As New DataSet
        Dim DT As New DataTable
        Dim HttpWReq As HttpWebRequest
        Dim httpWRes As HttpWebResponse = Nothing
        Dim address As Uri
        Dim strData As New StringBuilder
        Dim Client_id As String = "SYyhqKGSyD_Feasi"
        Dim Client_Secret As String = "fDwVhCQMGjZwORGaFBIm"
        Dim URI As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & HttpContext.Current.Request.ApplicationPath & "/Default.aspx"

        address = New Uri("https://api.jasmine.com/authen1/oauth/token?client_id=" + Client_id + "&redirect_uri=" & URI & "&grant_type=authorization_code&code=" & code)
        HttpWReq = DirectCast(WebRequest.Create(address), HttpWebRequest)
        HttpWReq.Method = "POST"
        HttpWReq.ContentType = "application/x-www-form-urlencoded"

        SetBasicAuthHeader(HttpWReq, Client_id, Client_Secret)

        httpWRes = DirectCast(HttpWReq.GetResponse(), HttpWebResponse)

        Dim reader As StreamReader = New StreamReader(httpWRes.GetResponseStream())
        Dim json As String = reader.ReadToEnd()

        Dim vHeader() As String
        Dim Token() As String
        Dim Access_Token As String = ""
        vHeader = Split(json.ToString, ",")
        If vHeader.Length > 2 Then
            Token = Split(vHeader(0).ToString, ":")
            Access_Token = Replace(Token(1).ToString, """", "").ToString.Trim
        End If

        Dim Request As HttpWebRequest
        Dim Response As HttpWebResponse
        Request = DirectCast(WebRequest.Create(New Uri("https://api.jasmine.com/authen1/me")), HttpWebRequest)
        HttpWReq.Method = "GET"
        HttpWReq.ContentType = "application/x-www-form-urlencoded"
        Request.Headers("Authorization") = "Bearer " + Access_Token

        Session("token") = Access_Token

        Response = DirectCast(Request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(Response.GetResponseStream())
        json = reader.ReadToEnd()

        DT = ConvertJSONToDataTable(json)
        Dim username As String() = DT.Rows(0).Item("username").ToString.Split("@")
        Session("uemail") = username(0)


    End Sub

    Public Sub SetBasicAuthHeader(ByVal request As WebRequest, ByVal userName As String, ByVal userPassword As String)
        Dim authInfo As String = userName + ":" + userPassword
        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo))
        request.Headers("Authorization") = "Basic " + authInfo
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

    Protected Sub chkOther_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther.CheckedChanged
        If chkOther.Checked = True Then
            ddlGroup.Enabled = False
            ddlSubGroup.Enabled = False
            DropDownList1.Enabled = False
            Button1.Enabled = False
            txtOther.Enabled = True
            txtOther.Visible = True
            btnAddOther.Visible = True
        Else
            ddlGroup.Enabled = True
            ddlSubGroup.Enabled = True
            DropDownList1.Enabled = True
            Button1.Enabled = True
            txtOther.Text = ""
            txtOther.Enabled = False
            txtOther.Visible = False
            btnAddOther.Visible = False
        End If
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dim sql_subgroup As String = "select Distinct Sub_Group from CAPEX "
        Dim sql_capex As String = "SELECT * FROM ("
        sql_capex += "select CAPEX_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX "
        sql_capex += "union "
        sql_capex += "select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX_Mass) "
        sql_capex += "as capex_all "
        If ddlGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_capex += "where capex_all.Main_Group = '" + ddlGroup.SelectedValue + "' "
            sql_subgroup += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            'sql_capex += "where Main_Group = '" + ddlGroup.SelectedValue + "'"
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            C.SetDropDownList(ddlSubGroup, sql_subgroup, "Sub_Group", "Sub_Group", "-- SubGroup ทั้งหมด --")
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        End If

    End Sub

    Protected Sub ddlSubGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Dim sql_capex As String = "SELECT * FROM ("
        sql_capex += "select CAPEX_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX "
        sql_capex += "union "
        sql_capex += "select CAPEX_Mass_Name as 'CAPEX_Name', Item_Code, Main_Group, Sub_Group from CAPEX_Mass) "
        sql_capex += "as capex_all "
        If ddlSubGroup.SelectedIndex <> 0 Then
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Name", "Item_Code")

            sql_capex += "where capex_all.Sub_Group = '" + ddlSubGroup.SelectedValue + "' "
            If ddlGroup.SelectedIndex <> 0 Then
                sql_capex += " and  capex_all.Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "AlertNotification('teststwhhere');focus();", True)
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        Else
            'C.SetDropDownList(DropDownList1, "select Item_Code + ' :: ' + CAPEX_Name as 'CAPEX_Name', Item_Code from CAPEX", "CAPEX_Name", "Item_Code")
            'ClientScript.RegisterStartupScript(Page.GetType, "subtest", "AlertNotification('teststalllll');focus();", True)
            If ddlGroup.SelectedIndex <> 0 Then
                sql_capex += "where capex_all.Main_Group = '" + ddlGroup.SelectedValue + "' "
            End If
            C.SetDropDownList(DropDownList1, sql_capex, "CAPEX_Name", "CAPEX_Name")
        End If
    End Sub

    ' Protected Sub Dropdownlist1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGroup.SelectedIndexChanged
    '     Session("dropdownlist") = DropDownList1.SelectedValue
    '     Response.write(Session("dropdownlist"))
    ' End Sub


    'Protected Sub chkOtherMass_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOtherMass.CheckedChanged
    '    If chkOtherMass.Checked = True Then
    '        ddlGroupMass.Enabled = False
    '        ddlItemMass.Enabled = False
    '        txtOtherMass.Enabled = True
    '        txtOtherMass.Visible = True
    '    Else
    '        ddlGroupMass.Enabled = True
    '        ddlItemMass.Enabled = True
    '        txtOtherMass.Text = ""
    '        txtOtherMass.Enabled = False
    '        txtOtherMass.Visible = False
    '    End If
    'End Sub

    'Protected Sub ddlGroupMass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroupMass.SelectedIndexChanged
    '    If ddlGroup.SelectedIndex <> 0 Then
    '        C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass where Main_Group = '" + ddlGroup.SelectedValue + "'", "CAPEX_Mass_Name", "Item_Code")
    '    Else
    '        C.SetDropDownList(ddlItemMass, "select CAPEX_Mass_Name as 'CAPEX_Mass_Name', Item_Code from CAPEX_Mass", "CAPEX_Mass_Name", "Item_Code")
    '    End If

    'End Sub

    'Protected Sub txtCostMass_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    For i As Integer = 0 To GridView2.Rows.Count - 1
    '        If IsNumeric(CType(GridView2.Rows(i).Cells(4).FindControl("txtCostMass"), TextBox).Text) Then
    '            GridView2.Rows(i).Cells(2).Text = Format(CDbl(CType(GridView2.Rows(i).Cells(4).FindControl("txtCostMass"), TextBox).Text) / CDbl(CType(GridView2.Rows(i).Cells(3).FindControl("lblUnitMass"), Label).Text), "#,##0.00")
    '        End If
    '    Next
    'End Sub

    Protected Sub txtCostPerUnit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If IsNumeric(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) = False Then
                CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('โปรดระบุ ราคาต่อหน่วย เป็นตัวเลขเท่านั้น');", True)
                CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Focus()
            End If
            CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text = Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) * CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text), "#,##0.00")
        Next
    End Sub

    Protected Sub txtNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If IsNumeric(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text) = False Then
                CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text = "0"
                ClientScript.RegisterStartupScript(Page.GetType, "failed_insert", "AlertNotification('โปรดระบุ จำนวน เป็นตัวเลขเท่านั้น');", True)
                CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Focus()
            End If
            CType(GridView1.Rows(i).Cells(4).FindControl("lblCost"), Label).Text = Format(CDbl(CType(GridView1.Rows(i).Cells(2).FindControl("txtCostPerUnit"), TextBox).Text) * CDbl(CType(GridView1.Rows(i).Cells(3).FindControl("txtUnit"), TextBox).Text), "#,##0.00")
        Next
    End Sub


End Class
