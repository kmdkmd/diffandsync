Imports System.IO

Public Class Main

#Region "定数"

    Private Const LOG_FILE_FORMAT = """diffandsync_""yyyyMMddHHmmss"".log"""

#End Region

#Region "イベント"

    Private Sub FromButton_Click(sender As Object, e As EventArgs) Handles FromButton.Click
        ChooseFolder(FromBox)
    End Sub

    Private Sub ToButton_Click(sender As Object, e As EventArgs) Handles ToButton.Click
        ChooseFolder(ToBox)
    End Sub

    Private Sub LogFolderButton_Click(sender As Object, e As EventArgs) Handles LogFolderButton.Click
        If ChooseFolder(LogBox) Then
            ' フォルダを選択した場合
            ' パスにファイル名を追加
            LogBox.Text = Path.Combine(LogBox.Text, DateTime.Now.ToString(LOG_FILE_FORMAT))
        End If
    End Sub

    Private Sub DiffButton_Click(sender As Object, e As EventArgs) Handles DiffButton.Click

    End Sub

    Private Sub SyncButton_Click(sender As Object, e As EventArgs) Handles SyncButton.Click

    End Sub

#End Region

    ' 指定したフォルダやファイルをチェックする
    Private Sub CheckFolders()
        ' Fromのチェック
        ' Toのチェック
        ' Toのファイルチェック
        ' ファイルが既にある場合は上書きするかどうか確認
        ' 上書きとされた場合はファイルを削除
    End Sub


End Class
