Imports System.Text
Imports System.IO

Public Class FolderSync

    ' ログファイル文字コード 
    Private ReadOnly LOG_FILE_ENCODING As Encoding = Encoding.UTF8

#Region "ログメッセージ"

    ' 作成
    Private Const CREATED_MSG As String = "{0} : 作成"
    ' 作成失敗
    Private Const NOT_CREATED_MSG As String = "{0} : 作成失敗"

    ' 作成完了ディレクトリ数
    Private Const CREATED_FOLDER_NUM_MSG As String = "作成完了フォルダ数 : {0}"
    ' 削除失敗フォルダ数
    Private Const NOT_CREATED_FOLDER_NUM_MSG As String = "作成失敗フォルダ数 : {0}"

    ' 属性コピー
    Private Const COPIED_ATTR_MSG As String = "{0} : 属性反映"
    ' 属性コピー失敗
    Private Const NOT_COPIED_ATTR_MSG As String = "{0} : 属性反映失敗"

    ' 属性コピー完了フォルダ数
    Private Const COPIED_ATTR_FOLDER_NUM_MSG As String = "属性コピー完了フォルダ数 : {0}"
    ' 属性コピー失敗フォルダ数
    Private Const NOT_COPIED_ATTR_FOLDER_NUM_MSG As String = "属性コピー失敗フォルダ数 : {0}"

    ' 削除失敗
    Private Const DELETED_MSG As String = "{0} : 削除"
    ' 削除失敗
    Private Const NOT_DELETED_MSG As String = "{0} : 削除失敗"

    ' 削除完了ファイル数
    Private Const DELETED_FILE_NUM_MSG As String = "削除完了ファイル数 : {0}"
    ' 削除失敗ファイル数
    Private Const NOT_DELETED_FILE_NUM_MSG As String = "削除失敗ファイル数 : {0}"

    ' 削除完了フォルダ数
    Private Const DELETED_FOLDER_NUM_MSG As String = "削除完了フォルダ数 : {0}"
    ' 削除失敗フォルダ数
    Private Const NOT_DELETED_FOLDER_NUM_MSG As String = "削除失敗フォルダ数 : {0}"

#End Region

#Region "カウンタ"

    Private sameFileNum As Integer = 0

    Private copiedFileNum As Integer = 0
    Private copyErrorFileNum As Integer = 0

    Private createdFolderNum As Integer = 0
    Private createErrorFolderNum As Integer = 0

    Private copiedFolderAttrNum As Integer = 0
    Private copyErrorFolderAttrNum As Integer = 0

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

    ' ファイル比較用ファイル情報
    Private Structure SyncFileInfo
        Dim LastWriteDate As Date
        Dim FileSize As Integer
        Dim MD5 As String
    End Structure

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

                    logWriter.WriteLine(CREATED_FOLDER_NUM_MSG, createdFolderNum)
                    logWriter.WriteLine(NOT_CREATED_FOLDER_NUM_MSG, createErrorFolderNum)
                    logWriter.WriteLine(COPIED_ATTR_FOLDER_NUM_MSG, copiedFolderAttrNum)
                    logWriter.WriteLine(NOT_COPIED_ATTR_FOLDER_NUM_MSG, copyErrorFolderAttrNum)
                    logWriter.WriteLine(DELETED_FOLDER_NUM_MSG, deletedFolderNum)
                    logWriter.WriteLine(NOT_DELETED_FOLDER_NUM_MSG, deleteErrorFolderNum)

                    logWriter.WriteLine(DELETED_FILE_NUM_MSG, deletedFileNum)
                    logWriter.WriteLine(NOT_DELETED_FILE_NUM_MSG, deleteErrorFileNum)

                    logWriter.Close()
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.OkOnly)
                End Try
            End If
        End Try
    End Sub

    ' 同期処理
    Private Sub Sync(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダがなければ作成
        CreateFolder(toFolder)

        ' ファイルの同期
        SyncFiles(fromFolder, toFolder)

        ' サブフォルダの同期
        SyncSubFolders(fromFolder, toFolder)

        ' フォルダの属性コピー
        CopyFolderAttributes(fromFolder, toFolder)
    End Sub

    ' ファイルの同期
    Private Sub SyncFiles(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダのファイル一覧取得
        Dim toFiles As List(Of String) = New List(Of String)(Directory.GetFiles(toFolder))

        ' From-Toのループ
        For Each fromFile As String In Directory.GetFiles(fromFolder)
            ' toのファイルパス取得
            Dim toFile As String = Path.Combine(toFolder, Path.GetFileName(fromFile))

            ' ファイルの比較
            Dim fromInfo As Nullable(Of SyncFileInfo) = GetFromFileInfoIfNotSame(fromFile, toFile)
            If Not IsNothing(fromInfo) Then
                ' ファイルをコピー
                ' 作成日時をセット
                ' アクセス日時をセット
                
                ' ファイルサイズを比較
                ' FromのファイルのMD5チェックサムを未取得の場合は取得
                ' ToのファイルのMD5チェックサムを未取得の場合は取得
                ' チェックサム比較
            End If

            ' 同期完了したのでToのフォルダリストから削除
            Dim deleteIndex As Integer = toFiles.IndexOf(toFile)
            If deleteIndex >= 0 Then
                toFiles.RemoveAt(deleteIndex)
            End If
        Next

        ' Toディレクトリファイル一覧から削除
        For Each toFile As String In toFiles
            Try
                My.Computer.FileSystem.DeleteFile(toFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                logWriter.WriteLine(DELETED_MSG, toFile)
                deletedFileNum += 1
            Catch ex As Exception
                logWriter.WriteLine(NOT_DELETED_MSG, toFile)
                deleteErrorFileNum += 1
            End Try
        Next
    End Sub

    ' ファイルが同じかどうか比較して同じじゃない場合はfromのファイルの比較情報を取得
    ' MD5 > ファイルサイズ > 更新日時 > 作成日時 > ファイルパス
    Private Function GetFromFileInfoIfNotSame(ByVal fromFile As String, ByVal toFile As String) As Nullable(Of SyncFileInfo)
        Dim fromInfo As SyncFileInfo = New SyncFileInfo

        ' Fromのファイル名と同じ名前のToのファイルがあるかチェック
        If Not File.Exists(toFile) Then
            Return fromInfo
        End If

        ' 更新日時比較
        fromInfo.LastWriteDate = File.GetLastWriteTime(fromFile)
        If fromInfo.LastWriteDate.Ticks <> File.GetLastWriteTime(toFile).Ticks Then
            Return fromInfo
        End If

        ' ファイルサイズ比較
        fromInfo.FileSize = New FileInfo(fromFile).Length
        If fromInfo.FileSize <> New FileInfo(toFile).Length Then
            Return fromInfo
        End If

        ' TODO: MD5
        ' FromのファイルのMD5チェックサムを取得
        ' ToのファイルのMD5チェックサムを取得
        ' チェックサム比較

        Return Nothing
    End Function

    ' Toフォルダの作成
    Private Sub CreateFolder(ByVal toFolder As String)
        If Not Directory.Exists(toFolder) Then
            Try
                Directory.CreateDirectory(toFolder)
                logWriter.WriteLine(CREATED_MSG, toFolder)
                createdFolderNum += 1
            Catch ex As Exception
                logWriter.WriteLine(NOT_CREATED_MSG, toFolder)
                createErrorFolderNum += 1
                Exit Sub
            End Try
        End If
    End Sub

    ' フォルダ属性、日付をコピー
    Private Sub CopyFolderAttributes(ByVal fromFolder As String, ByVal toFolder As String)
        Try
            ' 属性
            File.SetAttributes(toFolder, File.GetAttributes(fromFolder))
            ' 作成日時
            Directory.SetCreationTime(toFolder, Directory.GetCreationTime(fromFolder))
            ' 更新日時
            Directory.SetLastWriteTime(toFolder, Directory.GetCreationTime(fromFolder))

            logWriter.WriteLine(COPIED_ATTR_MSG, toFolder)
            copiedFolderAttrNum += 1
        Catch ex As Exception
            logWriter.WriteLine(NOT_COPIED_ATTR_MSG, toFolder)
            copyErrorFolderAttrNum += 1
        End Try
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
                logWriter.WriteLine(NOT_DELETED_MSG, toSubFolder)
                deleteErrorFolderNum += 1
            End Try
        Next
    End Sub

End Class
