Imports System.Text
Imports System.IO

Public Class FolderSync

    ' ログファイル文字コード 
    Private ReadOnly LOG_FILE_ENCODING As Encoding = Encoding.UTF8

#Region "ログメッセージ"

    ' 削除失敗
    Private Const DELETED_MSG As String = "{0} : 削除"
    ' 削除失敗
    Private Const NOT_DELETED_MSG As String = "{0} : 削除失敗"

    ' 削除完了フォルダ数
    Private Const DELETED_FOLDER_NUM_MSG As String = "削除完了フォルダ数 : {0}"
    ' 削除失敗フォルダ数
    Private Const NOT_DELETED_FOLDER_NUM_MSG As String = "削除失敗フォルダ数 : {0}"

#End Region

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

#Region "ログ関連"

    Private ReadOnly logFilePath As String

    Private logWriter As StreamWriter

#End Region

    Private ReadOnly doSync As Boolean

    Public Sub New(ByVal logFilePath As String, ByVal doSync As Boolean)
        Me.logFilePath = logFilePath
        Me.doSync = doSync
    End Sub

    Public Sub Execute(ByVal fromFolderPath As String, ByVal toFolderPath As String)
        Try
            ' ログファイルを開く
            logWriter = New StreamWriter(logFilePath, True, LOG_FILE_ENCODING)
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
                    logWriter.WriteLine(DELETED_FOLDER_NUM_MSG, deletedFolderNum)
                    logWriter.WriteLine(NOT_DELETED_FOLDER_NUM_MSG, deleteErrorFolderNum)
                    logWriter.Close()
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.OkOnly)
                End Try
            End If
        End Try
    End Sub

    Private Sub Sync(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダの存在確認

        ' 作成日時をセット
        ' 更新日時をセット
        ' アクセス日時をセット

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

        ' サブフォルダの同期
        SyncSubFolders(fromFolder, toFolder)
    End Sub

    ' サブフォルダの同期
    Private Sub SyncSubFolders(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダのフォルダ一覧取得
        Dim toSubFolders As List(Of String) = New List(Of String)(Directory.GetDirectories(toFolder))
        ' Fromフォルダのフォルダ一覧取得
        For Each fromSubFolder As String In Directory.GetDirectories(fromFolder)
            ' Fromのフォルダ名取得
            Dim toSubFolder As String = Path.Combine(toFolder, Path.GetFileName(fromSubFolder))
            ' 再帰
            Sync(fromSubFolder, toSubFolder)
            ' 同期完了したのでToのフォルダリストから削除
            Dim deleteIndex As Integer = toSubFolders.IndexOf(toSubFolder)
            If deleteIndex >= 0 Then
                toSubFolders.RemoveAt(deleteIndex)
            End If
        Next
        ' Toにしかないフォルダを削除
        For Each toSubFolder As String In toSubFolders
            Try
                My.Computer.FileSystem.DeleteDirectory(toSubFolder, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                logWriter.WriteLine(DELETED_MSG, toSubFolder)
                deletedFolderNum += 1
            Catch ex As Exception
                ' フォルダ削除失敗
                logWriter.WriteLine(NOT_DELETED_MSG, toSubFolder)
                deleteErrorFolderNum += 1
            End Try
        Next
    End Sub

End Class
