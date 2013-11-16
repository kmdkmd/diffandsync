<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.FromLabel = New System.Windows.Forms.Label()
        Me.FromBox = New System.Windows.Forms.TextBox()
        Me.FromButton = New System.Windows.Forms.Button()
        Me.ToButton = New System.Windows.Forms.Button()
        Me.ToBox = New System.Windows.Forms.TextBox()
        Me.ToLabel = New System.Windows.Forms.Label()
        Me.DiffButton = New System.Windows.Forms.Button()
        Me.SyncButton = New System.Windows.Forms.Button()
        Me.LogBox = New System.Windows.Forms.TextBox()
        Me.LogLabel = New System.Windows.Forms.Label()
        Me.DiffConditionGroup = New System.Windows.Forms.GroupBox()
        Me.DiffMd5Check = New System.Windows.Forms.CheckBox()
        Me.DiffSizeCheck = New System.Windows.Forms.CheckBox()
        Me.DiffModifiedTimeCheck = New System.Windows.Forms.CheckBox()
        Me.DiffCreatetimeCheck = New System.Windows.Forms.CheckBox()
        Me.SyncItemGroup = New System.Windows.Forms.GroupBox()
        Me.SyncPurgeCheck = New System.Windows.Forms.CheckBox()
        Me.SyncCreatedTimeCheck = New System.Windows.Forms.CheckBox()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.LogFolderButton = New System.Windows.Forms.Button()
        Me.CheckGroup = New System.Windows.Forms.GroupBox()
        Me.CheckMd5Check = New System.Windows.Forms.CheckBox()
        Me.CheckSizeCheck = New System.Windows.Forms.CheckBox()
        Me.StatusGroup = New System.Windows.Forms.GroupBox()
        Me.DiffConditionGroup.SuspendLayout()
        Me.SyncItemGroup.SuspendLayout()
        Me.CheckGroup.SuspendLayout()
        Me.StatusGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'FromLabel
        '
        Me.FromLabel.AutoSize = True
        Me.FromLabel.Location = New System.Drawing.Point(12, 9)
        Me.FromLabel.Name = "FromLabel"
        Me.FromLabel.Size = New System.Drawing.Size(33, 12)
        Me.FromLabel.TabIndex = 0
        Me.FromLabel.Text = "From:"
        '
        'FromBox
        '
        Me.FromBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FromBox.Location = New System.Drawing.Point(51, 6)
        Me.FromBox.Name = "FromBox"
        Me.FromBox.Size = New System.Drawing.Size(280, 19)
        Me.FromBox.TabIndex = 1
        '
        'FromButton
        '
        Me.FromButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FromButton.Location = New System.Drawing.Point(337, 4)
        Me.FromButton.Name = "FromButton"
        Me.FromButton.Size = New System.Drawing.Size(75, 23)
        Me.FromButton.TabIndex = 2
        Me.FromButton.Text = "参照"
        Me.FromButton.UseVisualStyleBackColor = True
        '
        'ToButton
        '
        Me.ToButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToButton.Location = New System.Drawing.Point(337, 33)
        Me.ToButton.Name = "ToButton"
        Me.ToButton.Size = New System.Drawing.Size(75, 23)
        Me.ToButton.TabIndex = 5
        Me.ToButton.Text = "参照"
        Me.ToButton.UseVisualStyleBackColor = True
        '
        'ToBox
        '
        Me.ToBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToBox.Location = New System.Drawing.Point(51, 35)
        Me.ToBox.Name = "ToBox"
        Me.ToBox.Size = New System.Drawing.Size(280, 19)
        Me.ToBox.TabIndex = 4
        '
        'ToLabel
        '
        Me.ToLabel.AutoSize = True
        Me.ToLabel.Location = New System.Drawing.Point(12, 38)
        Me.ToLabel.Name = "ToLabel"
        Me.ToLabel.Size = New System.Drawing.Size(20, 12)
        Me.ToLabel.TabIndex = 3
        Me.ToLabel.Text = "To:"
        '
        'DiffButton
        '
        Me.DiffButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DiffButton.Location = New System.Drawing.Point(166, 297)
        Me.DiffButton.Name = "DiffButton"
        Me.DiffButton.Size = New System.Drawing.Size(120, 23)
        Me.DiffButton.TabIndex = 6
        Me.DiffButton.Text = "差分取得のみ"
        Me.DiffButton.UseVisualStyleBackColor = True
        '
        'SyncButton
        '
        Me.SyncButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SyncButton.Location = New System.Drawing.Point(292, 297)
        Me.SyncButton.Name = "SyncButton"
        Me.SyncButton.Size = New System.Drawing.Size(120, 23)
        Me.SyncButton.TabIndex = 7
        Me.SyncButton.Text = "フォルダの同期"
        Me.SyncButton.UseVisualStyleBackColor = True
        '
        'LogBox
        '
        Me.LogBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogBox.Location = New System.Drawing.Point(51, 64)
        Me.LogBox.Name = "LogBox"
        Me.LogBox.Size = New System.Drawing.Size(280, 19)
        Me.LogBox.TabIndex = 9
        '
        'LogLabel
        '
        Me.LogLabel.AutoSize = True
        Me.LogLabel.Location = New System.Drawing.Point(12, 67)
        Me.LogLabel.Name = "LogLabel"
        Me.LogLabel.Size = New System.Drawing.Size(25, 12)
        Me.LogLabel.TabIndex = 8
        Me.LogLabel.Text = "ログ:"
        '
        'DiffConditionGroup
        '
        Me.DiffConditionGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DiffConditionGroup.Controls.Add(Me.DiffMd5Check)
        Me.DiffConditionGroup.Controls.Add(Me.DiffSizeCheck)
        Me.DiffConditionGroup.Controls.Add(Me.DiffModifiedTimeCheck)
        Me.DiffConditionGroup.Controls.Add(Me.DiffCreatetimeCheck)
        Me.DiffConditionGroup.Location = New System.Drawing.Point(14, 91)
        Me.DiffConditionGroup.Name = "DiffConditionGroup"
        Me.DiffConditionGroup.Size = New System.Drawing.Size(399, 45)
        Me.DiffConditionGroup.TabIndex = 18
        Me.DiffConditionGroup.TabStop = False
        Me.DiffConditionGroup.Text = "比較条件"
        '
        'DiffMd5Check
        '
        Me.DiffMd5Check.AutoSize = True
        Me.DiffMd5Check.Location = New System.Drawing.Point(286, 18)
        Me.DiffMd5Check.Name = "DiffMd5Check"
        Me.DiffMd5Check.Size = New System.Drawing.Size(47, 16)
        Me.DiffMd5Check.TabIndex = 18
        Me.DiffMd5Check.Text = "MD5"
        Me.DiffMd5Check.UseVisualStyleBackColor = True
        '
        'DiffSizeCheck
        '
        Me.DiffSizeCheck.AutoSize = True
        Me.DiffSizeCheck.Checked = True
        Me.DiffSizeCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DiffSizeCheck.Location = New System.Drawing.Point(193, 18)
        Me.DiffSizeCheck.Name = "DiffSizeCheck"
        Me.DiffSizeCheck.Size = New System.Drawing.Size(87, 16)
        Me.DiffSizeCheck.TabIndex = 17
        Me.DiffSizeCheck.Text = "ファイルサイズ"
        Me.DiffSizeCheck.UseVisualStyleBackColor = True
        '
        'DiffModifiedTimeCheck
        '
        Me.DiffModifiedTimeCheck.AutoSize = True
        Me.DiffModifiedTimeCheck.Checked = True
        Me.DiffModifiedTimeCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DiffModifiedTimeCheck.Location = New System.Drawing.Point(115, 18)
        Me.DiffModifiedTimeCheck.Name = "DiffModifiedTimeCheck"
        Me.DiffModifiedTimeCheck.Size = New System.Drawing.Size(72, 16)
        Me.DiffModifiedTimeCheck.TabIndex = 16
        Me.DiffModifiedTimeCheck.Text = "更新日時"
        Me.DiffModifiedTimeCheck.UseVisualStyleBackColor = True
        '
        'DiffCreatetimeCheck
        '
        Me.DiffCreatetimeCheck.AutoSize = True
        Me.DiffCreatetimeCheck.Checked = True
        Me.DiffCreatetimeCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DiffCreatetimeCheck.Location = New System.Drawing.Point(37, 18)
        Me.DiffCreatetimeCheck.Name = "DiffCreatetimeCheck"
        Me.DiffCreatetimeCheck.Size = New System.Drawing.Size(72, 16)
        Me.DiffCreatetimeCheck.TabIndex = 15
        Me.DiffCreatetimeCheck.Text = "作成日時"
        Me.DiffCreatetimeCheck.UseVisualStyleBackColor = True
        '
        'SyncItemGroup
        '
        Me.SyncItemGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SyncItemGroup.Controls.Add(Me.SyncPurgeCheck)
        Me.SyncItemGroup.Controls.Add(Me.SyncCreatedTimeCheck)
        Me.SyncItemGroup.Location = New System.Drawing.Point(14, 142)
        Me.SyncItemGroup.Name = "SyncItemGroup"
        Me.SyncItemGroup.Size = New System.Drawing.Size(399, 45)
        Me.SyncItemGroup.TabIndex = 19
        Me.SyncItemGroup.TabStop = False
        Me.SyncItemGroup.Text = "同期"
        '
        'SyncPurgeCheck
        '
        Me.SyncPurgeCheck.AutoSize = True
        Me.SyncPurgeCheck.Location = New System.Drawing.Point(193, 18)
        Me.SyncPurgeCheck.Name = "SyncPurgeCheck"
        Me.SyncPurgeCheck.Size = New System.Drawing.Size(187, 16)
        Me.SyncPurgeCheck.TabIndex = 17
        Me.SyncPurgeCheck.Text = "Fromにないファイル・フォルダを削除"
        Me.SyncPurgeCheck.UseVisualStyleBackColor = True
        '
        'SyncCreatedTimeCheck
        '
        Me.SyncCreatedTimeCheck.AutoSize = True
        Me.SyncCreatedTimeCheck.Checked = True
        Me.SyncCreatedTimeCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SyncCreatedTimeCheck.Location = New System.Drawing.Point(37, 18)
        Me.SyncCreatedTimeCheck.Name = "SyncCreatedTimeCheck"
        Me.SyncCreatedTimeCheck.Size = New System.Drawing.Size(72, 16)
        Me.SyncCreatedTimeCheck.TabIndex = 15
        Me.SyncCreatedTimeCheck.Text = "作成日時"
        Me.SyncCreatedTimeCheck.UseVisualStyleBackColor = True
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = True
        Me.StatusLabel.Location = New System.Drawing.Point(34, 15)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(41, 12)
        Me.StatusLabel.TabIndex = 20
        Me.StatusLabel.Text = "停止中"
        '
        'LogFolderButton
        '
        Me.LogFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogFolderButton.Location = New System.Drawing.Point(337, 62)
        Me.LogFolderButton.Name = "LogFolderButton"
        Me.LogFolderButton.Size = New System.Drawing.Size(75, 23)
        Me.LogFolderButton.TabIndex = 21
        Me.LogFolderButton.Text = "フォルダ参照"
        Me.LogFolderButton.UseVisualStyleBackColor = True
        '
        'CheckGroup
        '
        Me.CheckGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckGroup.Controls.Add(Me.CheckMd5Check)
        Me.CheckGroup.Controls.Add(Me.CheckSizeCheck)
        Me.CheckGroup.Location = New System.Drawing.Point(13, 193)
        Me.CheckGroup.Name = "CheckGroup"
        Me.CheckGroup.Size = New System.Drawing.Size(399, 45)
        Me.CheckGroup.TabIndex = 22
        Me.CheckGroup.TabStop = False
        Me.CheckGroup.Text = "同期後チェック"
        '
        'CheckMd5Check
        '
        Me.CheckMd5Check.AutoSize = True
        Me.CheckMd5Check.Location = New System.Drawing.Point(115, 18)
        Me.CheckMd5Check.Name = "CheckMd5Check"
        Me.CheckMd5Check.Size = New System.Drawing.Size(47, 16)
        Me.CheckMd5Check.TabIndex = 16
        Me.CheckMd5Check.Text = "MD5"
        Me.CheckMd5Check.UseVisualStyleBackColor = True
        '
        'CheckSizeCheck
        '
        Me.CheckSizeCheck.AutoSize = True
        Me.CheckSizeCheck.Checked = True
        Me.CheckSizeCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckSizeCheck.Location = New System.Drawing.Point(37, 18)
        Me.CheckSizeCheck.Name = "CheckSizeCheck"
        Me.CheckSizeCheck.Size = New System.Drawing.Size(53, 16)
        Me.CheckSizeCheck.TabIndex = 15
        Me.CheckSizeCheck.Text = "サイズ"
        Me.CheckSizeCheck.UseVisualStyleBackColor = True
        '
        'StatusGroup
        '
        Me.StatusGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusGroup.Controls.Add(Me.StatusLabel)
        Me.StatusGroup.Location = New System.Drawing.Point(14, 244)
        Me.StatusGroup.Name = "StatusGroup"
        Me.StatusGroup.Size = New System.Drawing.Size(399, 35)
        Me.StatusGroup.TabIndex = 23
        Me.StatusGroup.TabStop = False
        Me.StatusGroup.Text = "状況"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 332)
        Me.Controls.Add(Me.StatusGroup)
        Me.Controls.Add(Me.CheckGroup)
        Me.Controls.Add(Me.LogFolderButton)
        Me.Controls.Add(Me.SyncItemGroup)
        Me.Controls.Add(Me.DiffConditionGroup)
        Me.Controls.Add(Me.LogBox)
        Me.Controls.Add(Me.LogLabel)
        Me.Controls.Add(Me.SyncButton)
        Me.Controls.Add(Me.DiffButton)
        Me.Controls.Add(Me.ToButton)
        Me.Controls.Add(Me.ToBox)
        Me.Controls.Add(Me.ToLabel)
        Me.Controls.Add(Me.FromButton)
        Me.Controls.Add(Me.FromBox)
        Me.Controls.Add(Me.FromLabel)
        Me.MinimumSize = New System.Drawing.Size(440, 370)
        Me.Name = "Main"
        Me.Text = "DiffAndSync"
        Me.DiffConditionGroup.ResumeLayout(False)
        Me.DiffConditionGroup.PerformLayout()
        Me.SyncItemGroup.ResumeLayout(False)
        Me.SyncItemGroup.PerformLayout()
        Me.CheckGroup.ResumeLayout(False)
        Me.CheckGroup.PerformLayout()
        Me.StatusGroup.ResumeLayout(False)
        Me.StatusGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FromLabel As System.Windows.Forms.Label
    Friend WithEvents FromBox As System.Windows.Forms.TextBox
    Friend WithEvents FromButton As System.Windows.Forms.Button
    Friend WithEvents ToButton As System.Windows.Forms.Button
    Friend WithEvents ToBox As System.Windows.Forms.TextBox
    Friend WithEvents ToLabel As System.Windows.Forms.Label
    Friend WithEvents DiffButton As System.Windows.Forms.Button
    Friend WithEvents SyncButton As System.Windows.Forms.Button
    Friend WithEvents LogBox As System.Windows.Forms.TextBox
    Friend WithEvents LogLabel As System.Windows.Forms.Label
    Friend WithEvents DiffConditionGroup As System.Windows.Forms.GroupBox
    Friend WithEvents DiffMd5Check As System.Windows.Forms.CheckBox
    Friend WithEvents DiffSizeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents DiffModifiedTimeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents DiffCreatetimeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents SyncItemGroup As System.Windows.Forms.GroupBox
    Friend WithEvents SyncPurgeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents SyncCreatedTimeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents LogFolderButton As System.Windows.Forms.Button
    Friend WithEvents CheckGroup As System.Windows.Forms.GroupBox
    Friend WithEvents CheckMd5Check As System.Windows.Forms.CheckBox
    Friend WithEvents CheckSizeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents StatusGroup As System.Windows.Forms.GroupBox

End Class
