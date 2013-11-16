Imports System.Text
Imports System.IO

Public Class FolderSync

    ' ログファイル文字コード 
    Private ReadOnly LOG_FILE_ENCODING As Encoding = Encoding.UTF8

#Region "カウンタ"

    Private sameFileNum As Integer = 0

    Private copiedFileNum As Integer = 0
    Private copyErrorFileNum As Integer = 0

    Private copiedFolderNum As Integer = 0
    Private copyErrorFolderNum As Integer = 0

    Private deletedFileNum As Integer = 0
    Private deleteErrorFileNum As Integer = 0

    Private deletedFolderNum As Integer = 0
    Private deleteErrorFolderNum As Integer = 0

#End Region

    Private ReadOnly logFilePath As String

    Public Sub New(ByVal logFilePath As String)
        Me.logFilePath = logFilePath
    End Sub

    Public Sub Execute(ByVal fromFolderPath As String, ByVal toFolderPath As String)
        Dim logWriter As StreamWriter = Nothing
        Try
            ' ログファイルを開く
            logWriter = New System.IO.StreamWriter(logFilePath, True, LOG_FILE_ENCODING)
            logWriter.WriteLine("同期開始")

            ' 同期開始
            Sync(fromFolderPath, toFolderPath)
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.OkOnly)
        Finally
            ' ログファイルを閉じる
            If Not IsNothing(logWriter) Then
                Try
                    logWriter.WriteLine("同期終了")
                    logWriter.Close()
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.OkOnly)
                End Try
            End If
        End Try
    End Sub

    Private Sub Sync(ByVal fromFolderPath As String, ByVal toFolderPath As String)
        ' Fromフォルダの存在確認


        ' Fromフォルダのファイル一覧取得
        ' Toフォルダのファイル一覧取得

        ' From-Toのループ

        ' Fromのファイル名取得
        ' Fromのファイル名と同じ名前のToのファイルがあるかチェック

        ' Fromのファイルの作成日時取得
        ' Toのファイルの作成日時取得
        ' 作成日時比較

        ' Fromのファイルの更新日時取得
        ' Toのファイルの更新日時取得
        ' 更新日時比較

        ' Fromのファイルサイズ取得
        ' Toのファイルサイズ取得
        ' ファイルサイズ比較

        ' FromのファイルのMD5チェックサムを取得
        ' ToのファイルのMD5チェックサムを取得
        ' チェックサム比較

        ' ファイルをコピー
        ' 作成日時をセット
        ' アクセス日時をセット

        ' ファイルサイズを比較
        ' FromのファイルのMD5チェックサムを未取得の場合は取得
        ' ToのファイルのMD5チェックサムを未取得の場合は取得
        ' チェックサム比較

        ' コピー結果をログに書き出す

        ' Toディレクトリファイル一覧から削除


        ' To削除のループ

        ' Toのファイル一覧でループ開始
        ' ファイル削除


        ' フォルダのループ
        ' Fromフォルダのフォルダ一覧取得
        ' Toフォルダのフォルダ一覧取得

        ' From-Toのループ

        ' Fromのフォルダ名取得
        ' Fromのフォルダ名と同じ名前Toのファイルがあるかチェック

        ' Fromのフォルダ名と同じ名前Toのフォルダがあるかチェック

        ' フォルダがない場合は作成

        ' 再帰

        ' 作成日時をセット
        ' 更新日時をセット
        ' アクセス日時をセット
    End Sub

End Class
