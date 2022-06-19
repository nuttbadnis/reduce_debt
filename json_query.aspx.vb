Imports System.IO
Imports System.Data
Imports System.Net
Imports System.Net.Mail
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json.Converters.DateTimeConverterBase
Imports Newtonsoft.Json.Converters.JavaScriptDateTimeConverter

Partial Class json_query
    Inherits System.Web.UI.Page
    Dim DB105possup As New Cls_Data
    Dim CP As New Cls_Panu
    Dim C As New Cls_Data
    Dim N As New auto_vb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim qrs As String = ""
        If Request.QueryString("qrs") <> Nothing Then
            qrs = Request.QueryString("qrs")
        End If
        If qrs = "select_user" Then
            select_user()
        ElseIf qrs = "select_area" Then
            select_area()
        ElseIf qrs = "select_cluster" Then
            select_cluster()
        ElseIf qrs = "select_clustername" Then
            select_clustername()

        ElseIf qrs = "select_tmpcasst" Then
            select_tmpcasst()
        ElseIf qrs = "insert_tmpcasst" Then
            insert_tmpcasst()
        ElseIf qrs = "delete_tmpcasst" Then
            delete_tmpcasst()

        ElseIf qrs = "select_tmpct" Then
            select_tmpct()
        ElseIf qrs = "insert_tmpct" Then
            insert_tmpct()
        ElseIf qrs = "delete_tmpct" Then
            delete_tmpct()
        ElseIf qrs = "search_user" Then
            search_user()
        End If

    End Sub

    Protected Sub search_user()
        Dim value As String = Request.QueryString("v")
        Dim query As String = "select L.* "
        query += ", ISNULL(B.RO, 0) AS UB_RO, ISNULL(B.Cluster, 0) AS UB_Cluster "
        query += ", ISNULL(R.RO, 0) AS DR_RO "
        query += ", ISNULL(C.RO, 0) AS C_RO, ISNULL(C.Cluster, 0) AS C_Cluster "
        query += "from UserLogin L "
        query += "left join UserBranch B on L.Login_name = B.Login_name "
        query += "left join Cluster C on L.Login_name = C.Cluster_email "
        query += "left join RO_Director R on L.Login_name = R.RO_email "
        If (Not value = "") Then
            query += "where L.Login_name like '%" + value + "%' "
        End If
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub select_user()
        Dim value As String = Request.QueryString("v")
        Dim query As String = "select * from UserLogin "
        If (Not value = "") Then
            query += "where Login_name like '%" + value + "%' "
        End If
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub select_area()
        Dim query As String = "select distinct RO from Cluster where Status = '1' order by RO "
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub select_cluster()
        Dim value_ro As String = Request.QueryString("r")
        Dim query As String = "select distinct Cluster from Cluster where Status = '1' "
        If (Not value_ro = "") Then
            query += "and RO = '" + value_ro + "' "
        End If
        query += "order by Cluster"
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub select_clustername()
        Dim value_ro As String = Request.QueryString("r")
        Dim value_cluster As String = Request.QueryString("c")
        Dim query As String = "select distinct Cluster_name, Cluster_email from Cluster where Status = '1' "
        If (Not value_ro = "") Then
            query += "and RO = '" + value_ro + "' "
        End If
        If (Not value_cluster = "") Then
            query += "and Cluster = '" + value_cluster + "' "
        End If
        query += "order by Cluster_name"
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub select_tmpcasst()
        Dim user As String = Request.QueryString("u")
        Dim project_id As String = Request.QueryString("p_id")
        Dim menu As String = Request.QueryString("menu")
        Dim query As String = "select * from tmpList_CAsst "
        If menu <> "edit" Then
            If (Not user = "") Then
                query += "where CreateBy = '" + user + "' "
            Else
                query += "where CreateBy = '' "
            End If
        Else
            query += "where '1' = '1' "
        End If

        If (Not project_id = "") Then
            If menu = "edit" Then
                query += "and ProjectName_id = '" + project_id + "' "
            Else
                query += "and (ProjectName_id = '" + project_id + "' or isnull(ProjectName_id,'') = '')  "
            End If

        Else
            query += "and ProjectName_id is null "
        End If
        query += "order by Cluster_Name"
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub insert_tmpcasst()
        Dim user As String = Request.QueryString("u")
        Dim customer_id As String = Request.QueryString("cm_id")
        Dim customer_name As String = Request.QueryString("cm_n")
        Dim customer_email As String = Request.QueryString("cm_em")
        Dim area As String = Request.QueryString("a")
        Dim cluster As String = Request.QueryString("ct")
        Dim cluster_name As String = Request.QueryString("ct_n")
        Dim cluster_email As String = Request.QueryString("ct_em")

        Dim sql_employee As String = "insert into tmpList_CAsst(Customer_Assistant_ID,Customer_Assistant_Name,Customer_Assistant_Email,Area,Cluster,Cluster_Name,Cluster_Email,CreateBy) "
        sql_employee += "values('" + customer_id + "','" + customer_name + "','" + customer_email + "','" + area + "','" + cluster + "','" + cluster_name + "','" + cluster_email + "','" + user + "')"
        Dim return_data As String = ""
        Try
            C.ExecuteNonQuery(sql_employee)
            return_data = "{Code:'insert success',Message:'บันทึกข้อมูลสำเร็จ'}"

        Catch ex As Exception
            return_data = "{Code:'insert failed',Message:'บันทึกข้อมูลไม่สำเร็จ',Error_vb:'" & ex.Message & "'}"

        End Try
        Dim return_json As JObject = JObject.Parse(return_data)
        Response.Write(return_json.ToString())
    End Sub

    Protected Sub delete_tmpcasst()
        Dim id_list As String = Request.QueryString("e_id")

        Dim sql_employee As String = "delete from tmpList_CAsst "
        sql_employee += "where id_List=" + id_list + " "
        Dim return_data As String = ""
        Try
            C.ExecuteNonQuery(sql_employee)
            return_data = "{Code:'delete success',Message:'ลบข้อมูลสำเร็จ'}"

        Catch ex As Exception
            return_data = "{Code:'delete failed',Message:'ลบข้อมูลไม่สำเร็จ',Error_vb:'" & ex.Message & "'}"

        End Try
        Dim return_json As JObject = JObject.Parse(return_data)
        Response.Write(return_json.ToString())
    End Sub

    Protected Sub select_tmpct()
        Dim user As String = Request.QueryString("u")
        Dim project_id As String = Request.QueryString("p_id")
        Dim menu As String = Request.QueryString("menu")
        Dim query As String = "select * from tmpList_CT "
        If menu <> "edit" Then
            If (Not user = "") Then
                query += "where CreateBy = '" + user + "' "
            Else
                query += "where CreateBy = '' "
            End If
        Else
            query += "where '1' = '1' "
        End If
        If (Not project_id = "") Then
            If menu = "edit" Then
                query += "and ProjectName_id = '" + project_id + "' "
            Else
                query += "and (ProjectName_id = '" + project_id + "' or isnull(ProjectName_id,'') = '')  "
            End If
        Else
            query += "and ProjectName_id is null "
        End If
        query += "order by CreateDate"
        'Response.Write(query)
        Dim query_sql As New DataTable
        query_sql = N.GetDataTable_Feasibility(query)
        Response.Write(N.rDataTableToJson(query_sql))
    End Sub

    Protected Sub insert_tmpct()
        Dim user As String = Request.QueryString("u")
        Dim project_id As String = Request.QueryString("p_id")
        Dim menu As String = Request.QueryString("menu")
        Dim contact_name As String = Request.QueryString("c_id")
        Dim contact_tel As String = Request.QueryString("c_te")
        Dim contact_email As String = Request.QueryString("c_em")

        Dim sql_employee As String
        If menu <> "edit" Then
            sql_employee = "insert into tmpList_CT(Customer_Contact_Name,Customer_Contact_Tel,Customer_Contact_Email,CreateBy) "
            sql_employee += "values('" + contact_name + "','" + contact_tel + "','" + contact_email + "','" + user + "') "
        Else
            sql_employee = "insert into tmpList_CT(Customer_Contact_Name,Customer_Contact_Tel,Customer_Contact_Email,CreateBy,ProjectName_id) "
            sql_employee += "values('" + contact_name + "','" + contact_tel + "','" + contact_email + "','" + user + "','" & project_id & "') "
        End If

        Dim return_data As String = ""
        Try
            C.ExecuteNonQuery(sql_employee)
            If menu = "edit" Then
                Dim DT_ct As New DataTable
                DT_ct = C.GetDataTable("select * from tmpList_CT where ProjectName_id = '" + project_id + "' order by id_List  ")
                If DT_ct.Rows.Count > 0 Then
                    C.ExecuteNonQuery("Update List_ProjectName set Customer_Contact_Name = '" & DT_ct.Rows(0).Item("Customer_Contact_Name") & "', Customer_Contact_Tel = '" & DT_ct.Rows(0).Item("Customer_Contact_Tel") & "', Customer_Contact_Email = '" & DT_ct.Rows(0).Item("Customer_Contact_Email") & "'  where id_List = '" & project_id & "'")
                End If
            End If
            return_data = "{Code:'insert success',Message:'บันทึกข้อมูลสำเร็จ'}"

        Catch ex As Exception
            return_data = "{Code:'insert failed',Message:'บันทึกข้อมูลไม่สำเร็จ',Error_vb:'" & ex.Message & "'}"
        End Try
        Dim return_json As JObject = JObject.Parse(return_data)
        Response.Write(return_json.ToString())
    End Sub

    Protected Sub delete_tmpct()
        Dim id_list As String = Request.QueryString("e_id")

        Dim sql_employee As String = "delete from tmpList_CT "
        sql_employee += "where id_List=" + id_list + " "
        Dim return_data As String = ""
        Try
            C.ExecuteNonQuery(sql_employee)
            return_data = "{Code:'delete success',Message:'ลบข้อมูลสำเร็จ'}"

        Catch ex As Exception
            return_data = "{Code:'delete failed',Message:'ลบข้อมูลไม่สำเร็จ',Error_vb:'" & ex.Message & "'}"

        End Try
        Dim return_json As JObject = JObject.Parse(return_data)
        Response.Write(return_json.ToString())
    End Sub

End Class