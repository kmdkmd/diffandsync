Module Utils

    ' フォルダ選択ダイアログを表示
    ' 選択したらテキストボックスにその値をセットする
    Public Function ChooseFolder(ByRef ResultTextBox As TextBox) As Boolean
        Dim Fbd As New FolderBrowserDialog()

        ' 初期フォルダはデスクトップ
        Fbd.RootFolder = System.Environment.SpecialFolder.Desktop

        ' 初期選択するパスを設定する
        Fbd.SelectedPath = ResultTextBox.Text

        ' [新しいフォルダ] ボタンを表示する
        'FolderBrowserDialog1.ShowNewFolderButton = True

        Dim IsChoosen As Boolean = False

        ' ダイアログを表示し、戻り値が [OK] の場合は、選択したディレクトリを表示する
        If Fbd.ShowDialog() = DialogResult.OK Then
            IsChoosen = True
            ResultTextBox.Text = Fbd.SelectedPath
        End If

        ' 不要になった時点で破棄する
        Fbd.Dispose()

        Return IsChoosen
    End Function

End Module
