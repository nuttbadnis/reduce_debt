
Partial Class SpacialPayin_serverclose
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
        Label2.Text = HttpContext.Current.Request.ApplicationPath
    End Sub
End Class
