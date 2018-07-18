<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimeDataPage
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.MoniPanel = New System.Windows.Forms.Panel()
        Me.focustext = New System.Windows.Forms.TextBox()
        Me.GetDataTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel3.SuspendLayout()
        Me.MoniPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Location = New System.Drawing.Point(1, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1213, 60)
        Me.Panel3.TabIndex = 44
        '
        'Label44
        '
        Me.Label44.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Microsoft YaHei", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.Label44.Location = New System.Drawing.Point(3, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(230, 46)
        Me.Label44.TabIndex = 40
        Me.Label44.Text = "设备实时数据"
        '
        'MoniPanel
        '
        Me.MoniPanel.BackColor = System.Drawing.Color.Transparent
        Me.MoniPanel.Controls.Add(Me.focustext)
        Me.MoniPanel.ForeColor = System.Drawing.Color.Black
        Me.MoniPanel.Location = New System.Drawing.Point(1, 68)
        Me.MoniPanel.Name = "MoniPanel"
        Me.MoniPanel.Size = New System.Drawing.Size(1213, 835)
        Me.MoniPanel.TabIndex = 45
        '
        'focustext
        '
        Me.focustext.Location = New System.Drawing.Point(3, 634)
        Me.focustext.Name = "focustext"
        Me.focustext.Size = New System.Drawing.Size(100, 20)
        Me.focustext.TabIndex = 0
        '
        'GetDataTimer
        '
        Me.GetDataTimer.Interval = 900000000
        '
        'TimeDataPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(1213, 577)
        Me.Controls.Add(Me.MoniPanel)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "TimeDataPage"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.MoniPanel.ResumeLayout(False)
        Me.MoniPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents MoniPanel As System.Windows.Forms.Panel
    Friend WithEvents GetDataTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents focustext As System.Windows.Forms.TextBox
End Class
