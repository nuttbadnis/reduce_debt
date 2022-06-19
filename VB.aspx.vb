
Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim message As String = ddlFruits.SelectedItem.Text & " - " & ddlFruits.SelectedItem.Value
        ScriptManager.RegisterStartupScript(CType(sender, Control), Me.GetType(), "alert", "alert('" & message & "');", True)
    End Sub
End Class
