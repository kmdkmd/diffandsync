Imports System.IO

Public Class Main

#Region "定数"

    ' 開始前確認メッセージ
    Private Const DIFF_CONFIRM_TITLE As String = "差分取得開始確認"
    Private Const DIFF_CONFIRM_MSG As String = "差分取得してもいいですか？"
    Private Const SYNC_CONFIRM_TITLE As String = "同期開始確認"
    Private Const SYNC_CONFIRM_MSG As String = "同期してもいいですか？"

    ' 共通メッセージ
    Private Const CHECK_ERROR_TITLE As String = "エラー"
    Private Const PATH_IS_EMPTY As String = """{0}"" が空です。"
    Private Const IS_NOT_FOLDER_MSG As String = """{0}"" は" & vbCrLf & "フォルダではなくファイルです。フォルダを指定して下さい。"
    Private Const IS_NOT_FILE_MSG As String = """{0}"" は" & vbCrLf & "ファイルではフォルダではなくです。ファイルを指定して下さい。"
    Private Const IS_NOT_EXISTS_MSG As String = """{0}"" は" & vbCrLf & "存在しません。"
    Private Const FILE_NOT_DELETED_MSG As String = """{0}"" を" & vbCrLf & "削除できませんでした。"
    Private Const FILE_NOT_CREATED_MSG As String = """{0}"" を" & vbCrLf & "作成できませんでした。"

    ' From関連メッセージ
    Private ReadOnly FROM_PATH_IS_EMPTY_MSG As String = String.Format(PATH_IS_EMPTY, "From")

    ' To関連メッセージ
    Private ReadOnly TO_PATH_IS_EMPTY_MSG As String = String.Format(PATH_IS_EMPTY, "To")
    Private Const FROM_TO_DUPLICATION_MSG As String = "FromとToは別のフォルダを指定して下さい。"

    ' Log関連メッセージ
    Private Const LOG_FILE_FORMAT As String = """diffandsync_""yyyyMMddHHmmss"".log"""
    Private ReadOnly LOG_PATH_IS_EMPTY_MSG As String = String.Format(PATH_IS_EMPTY, "Log")
    Private Const LOG_FILE_DELETE_CONFIRM_TITLE As String = "ログファイル削除確認"
    Private Const LOG_FILE_DELETE_CONFIRM_MSG As String = "{0}は" & vbCrLf & "すでに存在します。上書きしてもいいですか？"
    Private Const LOG_DIRECTORY_NOT_EXISTS_MSG As String = "ログファイル保存ディレクトリ" & vbCrLf & "{0}が" & vbCrLf & "存在しません。"
    Private Const LOG_FILE_NOT_CREATED_MSG As String = "ログファイルを作成できません。"

#End Region

#Region "イベント"

    Private Sub FromButton_Click(sender As Object, e As EventArgs) Handles FromButton.Click
        SetChoosenFolder(FromBox)
    End Sub

    Private Sub ToButton_Click(sender As Object, e As EventArgs) Handles ToButton.Click
        SetChoosenFolder(ToBox)
    End Sub

    Private Sub LogFolderButton_Click(sender As Object, e As EventArgs) Handles LogFolderButton.Click
        If Not SetChoosenFolder(LogBox) Then
            Exit Sub
        End If
        ' 選択したフォルダパスにファイル名を追加
        LogBox.Text = Path.Combine(LogBox.Text, DateTime.Now.ToString(LOG_FILE_FORMAT))
    End Sub

    Private Sub DiffButton_Click(sender As Object, e As EventArgs) Handles DiffButton.Click
        ' 実行前確認
        If MsgBox(DIFF_CONFIRM_MSG, MsgBoxStyle.OkCancel, DIFF_CONFIRM_TITLE) <> MsgBoxResult.Ok Then
            Exit Sub
        End If
        ' 実行前チェック
        If Not CheckBefore() Then
            Exit Sub
        End If
    End Sub

    Private Sub SyncButton_Click(sender As Object, e As EventArgs) Handles SyncButton.Click
        ' 実行前確認
        If MsgBox(SYNC_CONFIRM_MSG, MsgBoxStyle.OkCancel, SYNC_CONFIRM_TITLE) <> MsgBoxResult.Ok Then
            Exit Sub
        End If
        ' 実行前チェック
        If Not CheckBefore() Then
            Exit Sub
        End If
    End Sub

#End Region

    Private Function CheckBefore() As Boolean
        ' フォルダ・ファイルのチェック
        Dim errorMsgs As String() = CheckFoldersAndFile()
        If Not IsNothing(errorMsgs) Then
            ' エラーあり
            MsgBox(String.Join(vbCrLf & vbCrLf, errorMsgs), MsgBoxStyle.OkOnly, CHECK_ERROR_TITLE)
            Return False
        End If

        ' ログファイル初期化
        Dim errorMsg As String = CheckLogFile()
        If Not IsNothing(errorMsg) Then
            ' エラーあり
            MsgBox(errorMsg, MsgBoxStyle.OkOnly, CHECK_ERROR_TITLE)
            Return False
        End If

        Return True
    End Function

    ' 指定したフォルダをチェックする
    Private Function CheckFoldersAndFile() As String()
        ' Fromのチェック
        Dim fromErrorMsg As String = FolderCheck(FromBox.Text, FROM_PATH_IS_EMPTY_MSG, IS_NOT_FOLDER_MSG, IS_NOT_EXISTS_MSG)
        ' Toのチェック
        Dim toErrorMsg As String = FolderCheck(ToBox.Text, TO_PATH_IS_EMPTY_MSG, IS_NOT_FOLDER_MSG, IS_NOT_EXISTS_MSG)
        ' FromTo重複チェック
        Dim fromToErrorMsg As String = IIf(FromBox.Text = ToBox.Text, FROM_TO_DUPLICATION_MSG, Nothing)
        ' Logのチェック
        Dim logErrorMsg As String = FileCheck(LogBox.Text, LOG_PATH_IS_EMPTY_MSG, IS_NOT_FILE_MSG)

        ' エラーメッセージをまとめる
        Dim errorMsgs As New List(Of String)()
        If Not IsNothing(fromErrorMsg) Then
            errorMsgs.Add(fromErrorMsg)
        End If
        If Not IsNothing(toErrorMsg) Then
            errorMsgs.Add(toErrorMsg)
        End If
        If Not IsNothing(fromToErrorMsg) Then
            errorMsgs.Add(fromToErrorMsg)
        End If
        If Not IsNothing(logErrorMsg) Then
            errorMsgs.Add(logErrorMsg)
        End If

        ' エラーメッセージがない場合
        If errorMsgs.Count < 1 Then
            Return Nothing
        End If

        ' エラーメッセージを配列で返す
        Return errorMsgs.ToArray()
    End Function

    ' 指定したファイルをチェックする
    Private Function CheckLogFile() As String
        ' Logのファイルチェック
        Dim errorMsg As String = NewFileCheck(LogBox.Text, _
                                                 LOG_PATH_IS_EMPTY_MSG, _
                                                 IS_NOT_FILE_MSG, _
                                                 LOG_DIRECTORY_NOT_EXISTS_MSG, _
                                                 LOG_FILE_DELETE_CONFIRM_TITLE, _
                                                 LOG_FILE_DELETE_CONFIRM_MSG, _
                                                 LOG_FILE_NOT_CREATED_MSG)
        If Not IsNothing(errorMsg) Then
            Return errorMsg
        End If
        ' ログファイルを新規作成
        errorMsg = CreateNewEmptyFile(LogBox.Text, FILE_NOT_DELETED_MSG, FILE_NOT_CREATED_MSG)
        If Not IsNothing(errorMsg) Then
            Return errorMsg
        End If
        Return Nothing
    End Function

End Class
