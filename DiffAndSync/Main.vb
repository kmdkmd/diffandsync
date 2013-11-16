Public Class Main

    Private Sub FromButton_Click(sender As Object, e As EventArgs) Handles FromButton.Click
        ChooseFolder(FromBox)
    End Sub

    Private Sub ToButton_Click(sender As Object, e As EventArgs) Handles ToButton.Click
        ChooseFolder(ToBox)
    End Sub

    Private Sub LogFolderButton_Click(sender As Object, e As EventArgs) Handles LogFolderButton.Click
        ChooseFolder(LogBox)
    End Sub


End Class
