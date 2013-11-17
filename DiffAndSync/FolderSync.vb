Imports System.Text
Imports System.IO

Public Class FolderSync

    ' ファイル比較用ファイル情報
    Private Structure SyncFileInfo
        Dim LastWriteDate As Date
        Dim FileSize As Integer
        Dim MD5 As String
    End Structure

    ' ログファイル文字コード 
    Private ReadOnly LOG_FILE_ENCODING As Encoding = Encoding.UTF8

    Private ReadOnly doSync As Boolean

#Region "ログメッセージ"

#Region "個別ログ(共通)"

    ' 属性コピー失敗
    Private Const MSG_COMMON_FAILED_TO_COPY_ATTR As String = "{0} : 属性反映失敗"

    ' 削除失敗
    Private Const MSG_COMMON_DELETED As String = "{0} : 削除"
    ' 削除失敗
    Private Const MSG_COMMON_FAILED_TO_DELETE As String = "{0} : 削除失敗"

#End Region

#Region "個別ログ(フォルダ)"

    ' 作成
    Private Const MSG_FOLDER_CREATED As String = "{0} : 作成"
    ' 作成失敗
    Private Const MSG_FOLDER_FAILED_TO_CREATE As String = "{0} : 作成失敗"

#End Region

#Region "個別ログ(ファイル)"

    ' 同一
    Private Const MSG_FILE_SAME As String = "{0} : 同一ファイル"

    ' コピー
    Private Const MSG_FILE_COPIED As String = "{0} : コピー"
    ' コピー失敗
    Private Const MSG_FILE_FAILED_TO_COPY As String = "{0} : コピー失敗"
    ' コピー不整合
    Private Const MSG_FILE_COPIED_INVALID As String = "{0} : コピー不整合"

#End Region

#Region "終了ログ(フォルダ)"

    ' 作成完了数
    Private Const MSG_FOLDER_NUM_CREATED As String = "作成フォルダ数 : {0}"
    ' 作成失敗数
    Private Const MSG_FOLDER_NUM_FAILED_TO_CREATED As String = "作成失敗フォルダ数 : {0}"

    ' 属性コピー失敗フォルダ数
    Private Const MSG_FOLDER_NUM_FAILED_TO_COPY_ATTR As String = "属性反映失敗フォルダ数 : {0}"

    ' 削除完了フォルダ数
    Private Const MSG_FOLDER_NUM_DELETED As String = "削除フォルダ数 : {0}"
    ' 削除失敗フォルダ数
    Private Const MSG_FOLDER_NUM_FAILED_TO_DELETE As String = "削除失敗フォルダ数 : {0}"

#End Region

#Region "終了ログ(ファイル)"

    ' 同一ファイル数
    Private Const MSG_FILE_NUM_SAME As String = "同一ファイル数 : {0}"

    ' コピー完了数
    Private Const MSG_FILE_NUM_COPIED As String = "コピーファイル数 : {0}"
    ' コピー不整合数
    Private Const MSG_FILE_NUM_COPIED_INVALID As String = "コピーファイル不整合数 : {0}"

    ' 属性コピー失敗ファイル数
    Private Const MSG_FILE_NUM_FAILED_TO_COPY_ATTR As String = "属性反映失敗ファイル数 : {0}"

    ' 削除完了ファイル数
    Private Const MSG_FILE_NUM_DELETE As String = "削除ファイル数 : {0}"
    ' 削除失敗ファイル数
    Private Const MSG_FILE_NUM_FAILED_TO_DELETE As String = "削除失敗ファイル数 : {0}"

#End Region

#Region "カウンタ(フォルダ)"

    Private numFolderCreated As Integer = 0
    Private numFolderFailedToCreate As Integer = 0

    Private numFolderFailedToCopyAttr As Integer = 0

    Private numFolderDeleted As Integer = 0
    Private numFolderFailedToDelete As Integer = 0

#End Region

#Region "カウンタ(ファイル)"

    Private numFileSame As Integer = 0

    Private numFileCopied As Integer = 0
    Private numFileFailedToCopy As Integer = 0
    Private numFileCopyInvalid As Integer = 0

    Private numFileFailedToCopyAttr As Integer = 0

    Private numFileDeleted As Integer = 0
    Private numFileFailedToDelete As Integer = 0

#End Region

#Region "ログ関連"

    Private ReadOnly logFilePath As String

    Private logWriter As StreamWriter

#End Region

#End Region

    Public Sub New(ByVal logFilePath As String, ByVal doSync As Boolean)
        Me.logFilePath = logFilePath
        Me.doSync = doSync
    End Sub

    Public Function Execute(ByVal fromFolderPath As String, ByVal toFolderPath As String) As String
        Try
            ' ログファイルを開く
            logWriter = New StreamWriter(logFilePath, True, LOG_FILE_ENCODING)

            If doSync Then
                logWriter.WriteLine("同期開始")
            Else
                logWriter.WriteLine("差分取得開始")
            End If

            ' 同期開始
            Sync(fromFolderPath, toFolderPath)

            Dim sb As New StringBuilder()
            If doSync Then
                logWriter.WriteLine("同期終了")

                sb.AppendFormat(MSG_FOLDER_NUM_CREATED, numFolderCreated)
                sb.AppendLine()
                sb.AppendFormat(MSG_FOLDER_NUM_FAILED_TO_CREATED, numFolderFailedToCreate)
                sb.AppendLine()
                sb.AppendFormat(MSG_FOLDER_NUM_FAILED_TO_COPY_ATTR, numFolderFailedToCopyAttr)
                sb.AppendLine()
                sb.AppendFormat(MSG_FOLDER_NUM_DELETED, numFolderDeleted)
                sb.AppendLine()
                sb.AppendFormat(MSG_FOLDER_NUM_FAILED_TO_DELETE, numFolderFailedToDelete)
                sb.AppendLine()

                sb.AppendFormat(MSG_FILE_NUM_SAME, numFileSame)
                sb.AppendLine()
                sb.AppendFormat(MSG_FILE_NUM_COPIED, numFileCopied)
                sb.AppendLine()
                sb.AppendFormat(MSG_FILE_NUM_FAILED_TO_COPY_ATTR, numFileFailedToCopyAttr)
                sb.AppendLine()
                sb.AppendFormat(MSG_FILE_FAILED_TO_COPY, numFileFailedToCopy)
                sb.AppendLine()
                sb.AppendFormat(MSG_FILE_NUM_COPIED_INVALID, numFileCopyInvalid)
                sb.AppendLine()
                sb.AppendFormat(MSG_FILE_NUM_DELETE, numFileDeleted)
                sb.AppendLine()
                sb.AppendFormat(MSG_FILE_NUM_FAILED_TO_DELETE, numFileFailedToDelete)
                sb.AppendLine()
            Else
                logWriter.WriteLine("差分取得終了")
            End If

            Dim resultMsg As String = sb.ToString()
            logWriter.Write(resultMsg)
            Return resultMsg
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.OkOnly)
        Finally
            ' ログファイルを閉じる
            If Not IsNothing(logWriter) Then
                Try
                    logWriter.Close()
                Catch ex As Exception
                    MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.OkOnly)
                End Try
            End If
        End Try
        Return Nothing
    End Function

    ' 同期処理
    Private Sub Sync(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダがなければ作成
        CreateFolder(toFolder)

        ' 不要ファイルの削除
        DeleteFiles(fromFolder, toFolder)

        ' 不要フォルダの削除
        DeleteFolders(fromFolder, toFolder)

        ' ファイルの同期
        SyncFiles(fromFolder, toFolder)

        ' サブフォルダの同期
        SyncSubFolders(fromFolder, toFolder)

        ' フォルダの属性コピー
        CopyFolderAttributes(fromFolder, toFolder)
    End Sub

#Region "ファイル処理"

    ' Toからファイルを削除
    Private Sub DeleteFiles(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダのファイルでループ
        Dim toFiles As String() = Directory.GetFiles(toFolder)
        For Each toFile As String In toFiles
            ' fromに同名のファイルがあるか
            If File.Exists(Path.Combine(fromFolder, Path.GetFileName(toFile))) Then
                Continue For
            End If
            ' ない場合はtoのファイルを削除
            Try
                My.Computer.FileSystem.DeleteFile(toFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                logWriter.WriteLine(MSG_COMMON_DELETED, toFile)
                numFileDeleted += 1
            Catch ex As Exception
                logWriter.WriteLine(MSG_COMMON_FAILED_TO_DELETE, toFile)
                numFileFailedToDelete += 1
            End Try
        Next
    End Sub

    ' ファイルの同期
    Private Sub SyncFiles(ByVal fromFolder As String, ByVal toFolder As String)
        ' From-Toのループ
        For Each fromFile As String In Directory.GetFiles(fromFolder)
            ' toのファイルパス取得
            Dim toFile As String = Path.Combine(toFolder, Path.GetFileName(fromFile))

            ' ファイルの比較
            Dim fromInfo As Nullable(Of SyncFileInfo) = GetFromFileInfoIfNotSame(fromFile, toFile)
            If IsNothing(fromInfo) Then
                ' 同じファイル
                logWriter.WriteLine(MSG_FILE_SAME, toFile)
                numFileSame += 1
            Else
                ' ファイルをコピー
                If CopyFile(fromFile, toFile) Then
                    ' コピーしたファイルを比較
                    If IsSameFile(fromFile, fromInfo, toFile) Then
                        ' 成功
                        logWriter.WriteLine(MSG_FILE_COPIED, toFile)
                        numFileCopied += 1
                    Else
                        ' 不整合
                        logWriter.WriteLine(MSG_FILE_COPIED_INVALID, toFile)
                        numFileCopyInvalid += 1
                    End If
                End If
            End If
        Next
    End Sub

    ' ファイルをコピー
    Private Function CopyFile(ByVal fromFile As String, ByVal toFile As String) As Boolean
        ' ファイルをコピー
        Try
            File.Copy(fromFile, toFile, True)
        Catch ex As Exception
            ' ファイルコピー失敗
            logWriter.WriteLine(MSG_FILE_FAILED_TO_COPY, toFile)
            numFileFailedToCopy += 1
            Return False
        End Try

        Try
            ' 属性
            File.SetAttributes(toFile, File.GetAttributes(fromFile))
            ' 作成日時
            File.SetCreationTime(toFile, File.GetCreationTime(fromFile))
        Catch ex As Exception
            logWriter.WriteLine(MSG_COMMON_FAILED_TO_COPY_ATTR, toFile)
            numFileFailedToCopyAttr += 1
            Return False
        End Try

        Return True
    End Function

    ' ファイルを比較
    Private Function IsSameFile(ByVal fromFile As String, ByVal fromInfo As SyncFileInfo, ByVal toFile As String) As Boolean
        ' 更新日時比較
        If fromInfo.LastWriteDate.Ticks <> File.GetLastWriteTime(toFile).Ticks Then
            Return False
        End If

        ' ファイルサイズ比較
        If fromInfo.FileSize <> New FileInfo(toFile).Length Then
            Return False
        End If

        ' TODO: MD5
        ' FromのファイルのMD5チェックサムを取得
        ' ToのファイルのMD5チェックサムを取得
        ' チェックサム比較

        Return True
    End Function

    ' ファイルが同じかどうか比較して同じじゃない場合はfromのファイルの比較情報を取得
    ' MD5 > ファイルサイズ > 更新日時 > 作成日時 > ファイルパス
    Private Function GetFromFileInfoIfNotSame(ByVal fromFile As String, ByVal toFile As String) As Nullable(Of SyncFileInfo)
        Dim fromInfo As SyncFileInfo = New SyncFileInfo
        fromInfo.LastWriteDate = File.GetLastWriteTime(fromFile)
        fromInfo.FileSize = New FileInfo(fromFile).Length

        ' Fromのファイル名と同じ名前のToのファイルがあるかチェック
        If Not File.Exists(toFile) Then
            Return fromInfo
        End If

        ' 更新日時比較
        If fromInfo.LastWriteDate.Ticks <> File.GetLastWriteTime(toFile).Ticks Then
            Return fromInfo
        End If

        ' ファイルサイズ比較
        If fromInfo.FileSize <> New FileInfo(toFile).Length Then
            Return fromInfo
        End If

        ' TODO: MD5
        ' FromのファイルのMD5チェックサムを取得
        ' ToのファイルのMD5チェックサムを取得
        ' チェックサム比較

        Return Nothing
    End Function

#End Region

#Region "フォルダ処理"

    ' Toからフォルダを削除
    Private Sub DeleteFolders(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダのファイルでループ
        Dim toSubFolders As String() = Directory.GetDirectories(toFolder)
        For Each toSubFolder As String In toSubFolders
            ' fromに同名のフォルダがあるか
            If Directory.Exists(Path.Combine(fromFolder, Path.GetFileName(toSubFolder))) Then
                Continue For
            End If
            ' ない場合はtoのファイルを削除
            Try
                My.Computer.FileSystem.DeleteDirectory(toSubFolder, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                logWriter.WriteLine(MSG_COMMON_DELETED, toSubFolder)
                numFolderDeleted += 1
            Catch ex As Exception
                logWriter.WriteLine(MSG_COMMON_FAILED_TO_DELETE, toSubFolder)
                numFolderFailedToDelete += 1
            End Try
        Next
    End Sub

    ' Toフォルダの作成
    Private Sub CreateFolder(ByVal toFolder As String)
        If Directory.Exists(toFolder) Then
            Exit Sub
        End If
        Try
            Directory.CreateDirectory(toFolder)
            logWriter.WriteLine(MSG_FOLDER_CREATED, toFolder)
            numFolderCreated += 1
        Catch ex As Exception
            logWriter.WriteLine(MSG_FOLDER_FAILED_TO_CREATE, toFolder)
            numFolderFailedToCreate += 1
            Exit Sub
        End Try
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
        Catch ex As Exception
            logWriter.WriteLine(MSG_COMMON_FAILED_TO_COPY_ATTR, toFolder)
            numFolderFailedToCopyAttr += 1
        End Try
    End Sub

    ' サブフォルダの同期
    Private Sub SyncSubFolders(ByVal fromFolder As String, ByVal toFolder As String)
        ' Fromフォルダのフォルダ一覧取得
        For Each fromSubFolder As String In Directory.GetDirectories(fromFolder)
            ' 再帰
            Sync(fromSubFolder, Path.Combine(toFolder, Path.GetFileName(fromSubFolder)))
        Next
    End Sub

#End Region

End Class
