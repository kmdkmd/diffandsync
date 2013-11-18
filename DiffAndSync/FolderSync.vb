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

#Region "ログメッセージ(差分)"

#Region "個別ログ(共通)"

    ' 一致
    Private MSG_DIFF_COMMON_SAME As String = "{0} : 一致"
    ' 不一致(コピー元のみ)
    Private MSG_DIFF_COMMON_ONLY_FROM As String = "{0} : 不一致(コピー元のみ)"
    ' 不一致(コピー先のみ)
    Private MSG_DIFF_COMMON_ONLY_TO As String = "{0} : 不一致(コピー先のみ)"

#End Region

#Region "終了ログ(フォルダ)"

    ' 一致数
    Private Const MSG_DIFF_FOLDER_NUM_SAME As String = "一致フォルダ数 : {0}"
    ' 不一致数(コピー元のみ)
    Private Const MSG_DIFF_FOLDER_NUM_ONLY_FROM As String = "不一致フォルダ数(コピー元のみ) : {0}"

#End Region

#Region "終了ログ(ファイル)"

    ' 不一致数(コピー先のみ)
    Private Const MSG_DIFF_FILE_NUM_ONLY_TO As String = "不一致ファイル数(コピー先のみ) : {0}"

#End Region

#End Region

#Region "ログメッセージ(同期)"

#Region "個別ログ(共通)"

    ' 属性コピー失敗
    Private Const MSG_SYNC_COMMON_FAILED_TO_COPY_ATTR As String = "{0} : 属性反映失敗"

    ' 削除失敗
    Private Const MSG_SYNC_COMMON_DELETED As String = "{0} : 削除"
    ' 削除失敗
    Private Const MSG_SYNC_COMMON_FAILED_TO_DELETE As String = "{0} : 削除失敗"

#End Region

#Region "個別ログ(フォルダ)"

    ' 作成
    Private Const MSG_SYNC_FOLDER_CREATED As String = "{0} : 作成"
    ' 作成失敗
    Private Const MSG_SYNC_FOLDER_FAILED_TO_CREATE As String = "{0} : 作成失敗"

#End Region

#Region "個別ログ(ファイル)"

    ' 同一
    Private Const MSG_SYNC_FILE_SAME As String = "{0} : 同一ファイル"

    ' コピー
    Private Const MSG_SYNC_FILE_COPIED As String = "{0} : コピー"
    ' コピー失敗
    Private Const MSG_SYNC_FILE_FAILED_TO_COPY As String = "{0} : コピー失敗"
    ' コピー不整合
    Private Const MSG_SYNC_FILE_COPIED_INVALID As String = "{0} : コピー不整合"

#End Region

#Region "終了ログ(フォルダ)"

    ' 作成完了数
    Private Const MSG_SYNC_FOLDER_NUM_CREATED As String = "作成フォルダ数 : {0}"
    ' 作成失敗数
    Private Const MSG_SYNC_FOLDER_NUM_FAILED_TO_CREATED As String = "作成失敗フォルダ数 : {0}"

    ' 属性コピー失敗フォルダ数
    Private Const MSG_SYNC_FOLDER_NUM_FAILED_TO_COPY_ATTR As String = "属性反映失敗フォルダ数 : {0}"

    ' 削除完了フォルダ数
    Private Const MSG_SYNC_FOLDER_NUM_DELETED As String = "削除フォルダ数 : {0}"
    ' 削除失敗フォルダ数
    Private Const MSG_SYNC_FOLDER_NUM_FAILED_TO_DELETE As String = "削除失敗フォルダ数 : {0}"

#End Region

#Region "終了ログ(ファイル)"

    ' 同一ファイル数
    Private Const MSG_SYNC_FILE_NUM_SAME As String = "同一ファイル数 : {0}"

    ' コピー完了数
    Private Const MSG_SYNC_FILE_NUM_COPIED As String = "コピーファイル数 : {0}"
    ' コピー不整合数
    Private Const MSG_SYNC_FILE_NUM_COPIED_INVALID As String = "コピーファイル不整合数 : {0}"

    ' 属性コピー失敗ファイル数
    Private Const MSG_SYNC_FILE_NUM_FAILED_TO_COPY_ATTR As String = "属性反映失敗ファイル数 : {0}"

    ' 削除完了ファイル数
    Private Const MSG_SYNC_FILE_NUM_DELETE As String = "削除ファイル数 : {0}"
    ' 削除失敗ファイル数
    Private Const MSG_SYNC_FILE_NUM_FAILED_TO_DELETE As String = "削除失敗ファイル数 : {0}"

#End Region

#End Region

#Region "カウンタ(差分)"

    ' 同名フォルダ数
    Private numDiffFolderSame As Integer = 0
    ' 同名フォルダ不一致数(コピー元のみ)
    Private numDiffFolderOnlyFrom As Integer = 0

    ' 同名ファイル不一致数(コピー先のみ)
    Private numDiffFileOnlyTo As Integer = 0

#End Region

#Region "カウンタ(同期)"

#Region "カウンタ(フォルダ)"

    ' 作成数
    Private numSyncFolderCreated As Integer = 0
    ' 作成失敗数
    Private numSyncFolderFailedToCreate As Integer = 0

    ' 属性コピー失敗数
    Private numSyncFolderFailedToCopyAttr As Integer = 0

    ' 削除数
    Private numSyncFolderDeleted As Integer = 0
    ' 削除失敗数
    Private numSyncFolderFailedToDelete As Integer = 0

#End Region

#Region "カウンタ(ファイル)"

    ' 同一数
    Private numSyncFileSame As Integer = 0

    ' コピー数
    Private numSyncFileCopied As Integer = 0
    ' コピー失敗数
    Private numSyncFileFailedToCopy As Integer = 0
    ' コピーファイル不整合数
    Private numSyncFileCopyInvalid As Integer = 0

    ' 属性コピー失敗数
    Private numSyncFileFailedToCopyAttr As Integer = 0

    ' 削除数
    Private numSyncFileDeleted As Integer = 0
    ' 削除失敗数
    Private numSyncFileFailedToDelete As Integer = 0

#End Region

#End Region

#Region "ログ関連"

    Private ReadOnly logFilePath As String

    Private logWriter As StreamWriter

#End Region

#Region "パブリックメソッド"

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

            ' 同期終了ログ
            Dim resultMsg As String = IIf(doSync, LogSyncEnd(), LogDiffEnd())
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

#End Region

    ' 同期終了時のログ
    Private Function LogSyncEnd() As String
        logWriter.WriteLine("同期終了")
        logWriter.WriteLine()

        Dim sb As New StringBuilder()
        sb.AppendFormat(MSG_SYNC_FOLDER_NUM_CREATED, numSyncFolderCreated)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FOLDER_NUM_FAILED_TO_CREATED, numSyncFolderFailedToCreate)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FOLDER_NUM_FAILED_TO_COPY_ATTR, numSyncFolderFailedToCopyAttr)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FOLDER_NUM_DELETED, numSyncFolderDeleted)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FOLDER_NUM_FAILED_TO_DELETE, numSyncFolderFailedToDelete)
        sb.AppendLine()

        sb.AppendFormat(MSG_SYNC_FILE_NUM_SAME, numSyncFileSame)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FILE_NUM_COPIED, numSyncFileCopied)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FILE_NUM_FAILED_TO_COPY_ATTR, numSyncFileFailedToCopyAttr)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FILE_FAILED_TO_COPY, numSyncFileFailedToCopy)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FILE_NUM_COPIED_INVALID, numSyncFileCopyInvalid)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FILE_NUM_DELETE, numSyncFileDeleted)
        sb.AppendLine()
        sb.AppendFormat(MSG_SYNC_FILE_NUM_FAILED_TO_DELETE, numSyncFileFailedToDelete)
        sb.AppendLine()

        Return sb.ToString()
    End Function

    ' 差分取得終了時のログ
    Private Function LogDiffEnd() As String
        logWriter.WriteLine("差分取得終了")
        logWriter.WriteLine()

        Dim sb As New StringBuilder()
        sb.AppendFormat(MSG_DIFF_FOLDER_NUM_SAME, numDiffFolderSame)
        sb.AppendLine()
        sb.AppendFormat(MSG_DIFF_FOLDER_NUM_ONLY_FROM, numDiffFolderOnlyFrom)
        sb.AppendLine()

        sb.AppendFormat(MSG_DIFF_FILE_NUM_ONLY_TO, numDiffFileOnlyTo)
        sb.AppendLine()

        Return sb.ToString()
    End Function

    ' 同期処理
    Private Sub Sync(ByVal fromFolder As String, ByVal toFolder As String)
        ' Toフォルダがなければ作成
        CreateFolder(toFolder)

        ' 不要ファイルの削除
        DeleteFiles(fromFolder, toFolder)

        ' TODO: 差分未実装
        If doSync Then
            ' 不要フォルダの削除
            DeleteFolders(fromFolder, toFolder)

            ' ファイルの同期
            SyncFiles(fromFolder, toFolder)
        End If

        ' サブフォルダの同期
        SyncSubFolders(fromFolder, toFolder)

        ' フォルダの属性反映
        CopyFolderAttributes(fromFolder, toFolder)
    End Sub

#Region "ファイル処理"

    ' Toからファイルを削除
    Private Sub DeleteFiles(ByVal fromFolder As String, ByVal toFolder As String)
        ' コピー先フォルダがない場合はなにもしない
        If Not Directory.Exists(toFolder) Then
            Exit Sub
        End If
        ' Toフォルダのファイルでループ
        Dim toFiles As String() = Directory.GetFiles(toFolder)
        For Each toFile As String In toFiles
            ' fromに同名のファイルがあるか
            If File.Exists(Path.Combine(fromFolder, Path.GetFileName(toFile))) Then
                Continue For
            End If
            ' ない場合
            ' 差分の場合はカウント
            If Not doSync Then
                logWriter.WriteLine(MSG_DIFF_COMMON_ONLY_TO, toFile)
                numDiffFileOnlyTo += 1
                Continue For
            End If
            ' 同期の場合はtoのファイルを削除
            Try
                My.Computer.FileSystem.DeleteFile(toFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                logWriter.WriteLine(MSG_SYNC_COMMON_DELETED, toFile)
                numSyncFileDeleted += 1
            Catch ex As Exception
                logWriter.WriteLine(MSG_SYNC_COMMON_FAILED_TO_DELETE, toFile)
                numSyncFileFailedToDelete += 1
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
                logWriter.WriteLine(MSG_SYNC_FILE_SAME, toFile)
                numSyncFileSame += 1
            Else
                ' ファイルをコピー
                If CopyFile(fromFile, toFile) Then
                    ' コピーしたファイルを比較
                    If IsSameFile(fromFile, fromInfo, toFile) Then
                        ' 成功
                        logWriter.WriteLine(MSG_SYNC_FILE_COPIED, toFile)
                        numSyncFileCopied += 1
                    Else
                        ' 不整合
                        logWriter.WriteLine(MSG_SYNC_FILE_COPIED_INVALID, toFile)
                        numSyncFileCopyInvalid += 1
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
            logWriter.WriteLine(MSG_SYNC_FILE_FAILED_TO_COPY, toFile)
            numSyncFileFailedToCopy += 1
            Return False
        End Try

        Try
            ' 属性
            File.SetAttributes(toFile, File.GetAttributes(fromFile))
            ' 作成日時
            File.SetCreationTime(toFile, File.GetCreationTime(fromFile))
        Catch ex As Exception
            logWriter.WriteLine(MSG_SYNC_COMMON_FAILED_TO_COPY_ATTR, toFile)
            numSyncFileFailedToCopyAttr += 1
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
                logWriter.WriteLine(MSG_SYNC_COMMON_DELETED, toSubFolder)
                numSyncFolderDeleted += 1
            Catch ex As Exception
                logWriter.WriteLine(MSG_SYNC_COMMON_FAILED_TO_DELETE, toSubFolder)
                numSyncFolderFailedToDelete += 1
            End Try
        Next
    End Sub

    ' Toフォルダの作成
    Private Sub CreateFolder(ByVal toFolder As String)
        If Directory.Exists(toFolder) Then
            ' 差分用同一フォルダ有り
            If Not doSync Then
                logWriter.WriteLine(MSG_DIFF_COMMON_SAME, toFolder)
                numDiffFolderSame += 1
            End If
            Exit Sub
        End If
        ' 差分用同一フォルダ無し
        If Not doSync Then
            logWriter.WriteLine(MSG_DIFF_COMMON_ONLY_FROM, toFolder)
            numDiffFolderOnlyFrom += 1
            Exit Sub
        End If
        ' 同期フォルダ作成
        Try
            Directory.CreateDirectory(toFolder)
            logWriter.WriteLine(MSG_SYNC_FOLDER_CREATED, toFolder)
            numSyncFolderCreated += 1
        Catch ex As Exception
            logWriter.WriteLine(MSG_SYNC_FOLDER_FAILED_TO_CREATE, toFolder)
            numSyncFolderFailedToCreate += 1
            Exit Sub
        End Try
    End Sub

    ' フォルダ属性、日付をコピー
    Private Sub CopyFolderAttributes(ByVal fromFolder As String, ByVal toFolder As String)
        ' 差分取得の場合はなにもしない
        If Not doSync Then
            Exit Sub
        End If
        Try
            ' 属性
            File.SetAttributes(toFolder, File.GetAttributes(fromFolder))
            ' 作成日時
            Directory.SetCreationTime(toFolder, Directory.GetCreationTime(fromFolder))
            ' 更新日時
            Directory.SetLastWriteTime(toFolder, Directory.GetCreationTime(fromFolder))
        Catch ex As Exception
            logWriter.WriteLine(MSG_SYNC_COMMON_FAILED_TO_COPY_ATTR, toFolder)
            numSyncFolderFailedToCopyAttr += 1
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
