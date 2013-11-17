Imports System.IO

Module Utils

#Region "フォルダ関連"

    ' フォルダ選択ダイアログを表示
    ' 選択したらテキストボックスにその値をセットする
    Public Function ChooseFolder(ByVal defaultPath As String) As String
        Dim fbd As New FolderBrowserDialog()
        ' 初期フォルダはデスクトップ
        fbd.RootFolder = System.Environment.SpecialFolder.Desktop
        ' 初期選択するパスを設定する
        fbd.SelectedPath = defaultPath
        ' 新しいフォルダボタンを表示する
        'FolderBrowserDialog1.ShowNewFolderButton = True
        ' ダイアログを表示
        Try
            If fbd.ShowDialog() = DialogResult.OK Then
                Return fbd.SelectedPath
            End If
        Finally
            ' ダイアログオブジェクトを破棄
            fbd.Dispose()
        End Try
        Return Nothing
    End Function

    ' フォルダ選択ダイアログを表示して結果をコントロールにセットする
    Public Function SetChoosenFolder(ByRef cntrl As Control) As Boolean
        Dim choosenFolder As String = ChooseFolder(cntrl.Text)
        If IsNothing(choosenFolder) Then
            ' 選択されなかったのでセットしない
            Return False
        End If
        cntrl.Text = choosenFolder
        Return True
    End Function

    ' フォルダの存在チェックをしてメッセージを返す
    Public Function FolderCheck(ByVal folderPath As String, ByVal msgPathIsEmpty As String, ByVal msgFormatIsNotFolder As String, ByVal msgFormatNotExists As String) As String
        ' パスが空
        If String.IsNullOrEmpty(folderPath) Then
            Return String.Format(msgPathIsEmpty, folderPath)
        End If
        ' パスがフォルダではなくファイル
        If File.Exists(folderPath) Then
            Return String.Format(msgFormatIsNotFolder, folderPath)
        End If
        ' フォルダが存在しない
        If Not Directory.Exists(folderPath) Then
            Return String.Format(msgFormatNotExists, folderPath)
        End If
        Return Nothing
    End Function

#End Region

#Region "ファイル関連"

    ' ファイルの存在チェックをしてメッセージを返す
    ' ファイルの存在チェックをしない
    Public Function FileCheck(ByVal filePath As String, ByVal msgPathIsEmpty As String, ByVal msgFormatIsNotFile As String) As String
        Return FileCheck(filePath, msgPathIsEmpty, msgFormatIsNotFile, Nothing)
    End Function

    ' ファイルの存在チェックをしてメッセージを返す
    Public Function FileCheck(ByVal filePath As String, ByVal msgPathIsEmpty As String, ByVal msgFormatIsNotFile As String, ByVal msgFormatNotExists As String) As String
        ' パスが空
        If String.IsNullOrEmpty(filePath) Then
            Return String.Format(msgPathIsEmpty, filePath)
        End If
        ' パスがファイルではなくフォルダ
        If Directory.Exists(filePath) Then
            Return String.Format(msgFormatIsNotFile, filePath)
        End If
        ' ファイルが存在しない(フォーマットがnothingの場合はチェックしない)
        If Not IsNothing(msgFormatNotExists) AndAlso Not Directory.Exists(filePath) Then
            Return String.Format(msgFormatNotExists, filePath)
        End If
        Return Nothing
    End Function

    ' 新規ファイル作成前ファイルチェック
    ' 既にファイルが有る場合は削除するかどうか表示する
    Public Function NewFileCheck(ByVal filePath As String, _
                                 ByVal msgPathIsEmpty As String, _
                                 ByVal isNotFileMsg As String, _
                                 ByVal logDirectoryNotExistsMsg As String, _
                                 ByVal logFileDeleteConfirmTitle As String, _
                                 ByVal logFileDeleteConfirmMsg As String, _
                                 ByVal logFileDeleteCanceledMsg As String) As String
        ' パスがフォルダを指してないかチェック
        Dim errorMsg As String = FileCheck(filePath, msgPathIsEmpty, isNotFileMsg)
        If Not IsNothing(errorMsg) Then
            Return errorMsg
        End If
        ' 親ディレクトリがあるか？
        Dim logFolderPath As String = Path.GetDirectoryName(filePath)
        If Not Directory.Exists(logFolderPath) Then
            ' 親ディレクトリがない場合
            Return String.Format(logDirectoryNotExistsMsg, logFolderPath)
        End If
        ' ファイルすでにあるか？
        If Not File.Exists(filePath) Then
            ' 正常終了
            Return Nothing
        End If
        ' ファイルが既にある場合は削除するかどうか確認
        If MsgBox(String.Format(logFileDeleteConfirmMsg, filePath), MsgBoxStyle.OkCancel, logFileDeleteConfirmTitle) <> MsgBoxResult.Ok Then
            ' 拒否の場合はエラー
            Return String.Format(logFileDeleteCanceledMsg, filePath)
        End If
        Return Nothing
    End Function

    ' 空のファイル作成
    ' 既にファイルが有る場合はゴミ箱に移動する
    Public Function CreateNewEmptyFile(ByVal filePath As String, ByVal logFileNotDeletedMsg As String, ByVal logFileNotCreatedMsg As String) As String
        ' 既存のファイルをゴミ箱に移動
        If File.Exists(filePath) Then
            Try
                My.Computer.FileSystem.DeleteFile(filePath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Catch ex As Exception
                ' ファイル削除失敗
                Return String.Format(logFileNotDeletedMsg, filePath)
            End Try
        End If
        ' 空のログファイルを作成
        Try
            File.Create(filePath).Close()
        Catch ex As Exception
            Return String.Format(logFileNotCreatedMsg, filePath)
        End Try
        Return Nothing
    End Function

#End Region

End Module
