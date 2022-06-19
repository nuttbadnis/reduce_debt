
Partial Class index
    Inherits System.Web.UI.Page

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("https://api.jasmine.com/authen1/oauth/authorize?response_type=code&client_id=SYyhqKGSyD_Feasi&redirect_uri=" & Replace(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), "https", "http") & HttpContext.Current.Request.ApplicationPath & "/Default.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Request.QueryString("alert") = "1" Then
            ClientScript.RegisterStartupScript(Page.GetType, "no_permission", "bootbox.alert('Login นี้ไม่มีสิทธิ์เข้าใช้งานระบบ');", True)
        Else
            ImageButton1_Click(Nothing, Nothing) '(autoclick imagebutton jasmine key)
        End If
    End Sub
End Class
