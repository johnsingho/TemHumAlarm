<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IP_Match_Location
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IP_Match_Location))
        Me.TextBox_Location = New System.Windows.Forms.TextBox()
        Me.TextBox_PI1 = New System.Windows.Forms.TextBox()
        Me.TextBox_PI4 = New System.Windows.Forms.TextBox()
        Me.TextBox_PI3 = New System.Windows.Forms.TextBox()
        Me.TextBox_PI2 = New System.Windows.Forms.TextBox()
        Me.Label1_Location = New System.Windows.Forms.Label()
        Me.Label1_IP_Address = New System.Windows.Forms.Label()
        Me.Label1_num = New System.Windows.Forms.Label()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Label3_IP_Point = New System.Windows.Forms.Label()
        Me.Label2_IP_Point = New System.Windows.Forms.Label()
        Me.Label1_IP_Point = New System.Windows.Forms.Label()
        Me.ErecordcontrolDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TextBox1_num = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TemStandard = New System.Windows.Forms.ComboBox()
        Me.HumidityStandard = New System.Windows.Forms.ComboBox()
        Me.MachineID = New System.Windows.Forms.TextBox()
        Me.Deadline = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DownloadButton = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.MAC = New System.Windows.Forms.Label()
        Me.MacText = New System.Windows.Forms.TextBox()
        CType(Me.ErecordcontrolDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox_Location
        '
        Me.TextBox_Location.Location = New System.Drawing.Point(169, 163)
        Me.TextBox_Location.Name = "TextBox_Location"
        Me.TextBox_Location.Size = New System.Drawing.Size(239, 20)
        Me.TextBox_Location.TabIndex = 56
        '
        'TextBox_PI1
        '
        Me.TextBox_PI1.Location = New System.Drawing.Point(169, 116)
        Me.TextBox_PI1.Name = "TextBox_PI1"
        Me.TextBox_PI1.Size = New System.Drawing.Size(42, 20)
        Me.TextBox_PI1.TabIndex = 52
        '
        'TextBox_PI4
        '
        Me.TextBox_PI4.Location = New System.Drawing.Point(366, 116)
        Me.TextBox_PI4.Name = "TextBox_PI4"
        Me.TextBox_PI4.Size = New System.Drawing.Size(42, 20)
        Me.TextBox_PI4.TabIndex = 55
        '
        'TextBox_PI3
        '
        Me.TextBox_PI3.Location = New System.Drawing.Point(300, 116)
        Me.TextBox_PI3.Name = "TextBox_PI3"
        Me.TextBox_PI3.Size = New System.Drawing.Size(42, 20)
        Me.TextBox_PI3.TabIndex = 54
        '
        'TextBox_PI2
        '
        Me.TextBox_PI2.Location = New System.Drawing.Point(234, 116)
        Me.TextBox_PI2.Name = "TextBox_PI2"
        Me.TextBox_PI2.Size = New System.Drawing.Size(42, 20)
        Me.TextBox_PI2.TabIndex = 53
        '
        'Label1_Location
        '
        Me.Label1_Location.AutoSize = True
        Me.Label1_Location.BackColor = System.Drawing.Color.Transparent
        Me.Label1_Location.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1_Location.Location = New System.Drawing.Point(77, 168)
        Me.Label1_Location.Name = "Label1_Location"
        Me.Label1_Location.Size = New System.Drawing.Size(53, 12)
        Me.Label1_Location.TabIndex = 63
        Me.Label1_Location.Text = "设备位置"
        '
        'Label1_IP_Address
        '
        Me.Label1_IP_Address.AutoSize = True
        Me.Label1_IP_Address.BackColor = System.Drawing.Color.Transparent
        Me.Label1_IP_Address.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1_IP_Address.Location = New System.Drawing.Point(77, 121)
        Me.Label1_IP_Address.Name = "Label1_IP_Address"
        Me.Label1_IP_Address.Size = New System.Drawing.Size(65, 12)
        Me.Label1_IP_Address.TabIndex = 62
        Me.Label1_IP_Address.Text = "IP Address"
        '
        'Label1_num
        '
        Me.Label1_num.AutoSize = True
        Me.Label1_num.BackColor = System.Drawing.Color.Transparent
        Me.Label1_num.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1_num.Location = New System.Drawing.Point(77, 69)
        Me.Label1_num.Name = "Label1_num"
        Me.Label1_num.Size = New System.Drawing.Size(29, 12)
        Me.Label1_num.TabIndex = 61
        Me.Label1_num.Text = "编码"
        '
        'Button_Save
        '
        Me.Button_Save.Location = New System.Drawing.Point(366, 234)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(75, 25)
        Me.Button_Save.TabIndex = 60
        Me.Button_Save.Text = "保存"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Label3_IP_Point
        '
        Me.Label3_IP_Point.AutoSize = True
        Me.Label3_IP_Point.BackColor = System.Drawing.Color.Transparent
        Me.Label3_IP_Point.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3_IP_Point.Location = New System.Drawing.Point(348, 126)
        Me.Label3_IP_Point.Name = "Label3_IP_Point"
        Me.Label3_IP_Point.Size = New System.Drawing.Size(12, 12)
        Me.Label3_IP_Point.TabIndex = 59
        Me.Label3_IP_Point.Text = "."
        '
        'Label2_IP_Point
        '
        Me.Label2_IP_Point.AutoSize = True
        Me.Label2_IP_Point.BackColor = System.Drawing.Color.Transparent
        Me.Label2_IP_Point.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2_IP_Point.Location = New System.Drawing.Point(282, 126)
        Me.Label2_IP_Point.Name = "Label2_IP_Point"
        Me.Label2_IP_Point.Size = New System.Drawing.Size(12, 12)
        Me.Label2_IP_Point.TabIndex = 58
        Me.Label2_IP_Point.Text = "."
        '
        'Label1_IP_Point
        '
        Me.Label1_IP_Point.AutoSize = True
        Me.Label1_IP_Point.BackColor = System.Drawing.Color.Transparent
        Me.Label1_IP_Point.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1_IP_Point.Location = New System.Drawing.Point(217, 126)
        Me.Label1_IP_Point.Name = "Label1_IP_Point"
        Me.Label1_IP_Point.Size = New System.Drawing.Size(12, 12)
        Me.Label1_IP_Point.TabIndex = 57
        Me.Label1_IP_Point.Text = "."
        '
        'TextBox1_num
        '
        Me.TextBox1_num.Location = New System.Drawing.Point(169, 64)
        Me.TextBox1_num.Name = "TextBox1_num"
        Me.TextBox1_num.Size = New System.Drawing.Size(239, 20)
        Me.TextBox1_num.TabIndex = 67
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(77, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 13)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "请在如下设置对应的编码-IP 地址-物理地址"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(90, 246)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 13)
        Me.Label2.TabIndex = 69
        Me.Label2.Text = "请在如下页面中修改设置"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(456, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "标准"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(456, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 12)
        Me.Label4.TabIndex = 71
        Me.Label4.Text = "设备ID"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(78, 207)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 72
        Me.Label5.Text = "到期日期"
        '
        'TemStandard
        '
        Me.TemStandard.FormattingEnabled = True
        Me.TemStandard.Items.AddRange(New Object() {"温度标准", "5℃~35℃", "20℃~27℃", "19℃~25℃", ""})
        Me.TemStandard.Location = New System.Drawing.Point(526, 64)
        Me.TemStandard.Name = "TemStandard"
        Me.TemStandard.Size = New System.Drawing.Size(114, 21)
        Me.TemStandard.TabIndex = 73
        '
        'HumidityStandard
        '
        Me.HumidityStandard.FormattingEnabled = True
        Me.HumidityStandard.Items.AddRange(New Object() {"湿度标准", "30%~60%", "30%~75%"})
        Me.HumidityStandard.Location = New System.Drawing.Point(665, 64)
        Me.HumidityStandard.Name = "HumidityStandard"
        Me.HumidityStandard.Size = New System.Drawing.Size(114, 21)
        Me.HumidityStandard.TabIndex = 74
        '
        'MachineID
        '
        Me.MachineID.Location = New System.Drawing.Point(526, 116)
        Me.MachineID.Name = "MachineID"
        Me.MachineID.Size = New System.Drawing.Size(253, 20)
        Me.MachineID.TabIndex = 75
        '
        'Deadline
        '
        Me.Deadline.Cursor = System.Windows.Forms.Cursors.Default
        Me.Deadline.CustomFormat = "yyy/mmm/dd"
        Me.Deadline.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Deadline.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Deadline.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Deadline.Location = New System.Drawing.Point(171, 201)
        Me.Deadline.Name = "Deadline"
        Me.Deadline.Size = New System.Drawing.Size(200, 21)
        Me.Deadline.TabIndex = 76
        Me.Deadline.Value = New Date(2016, 2, 1, 10, 33, 13, 0)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(93, 266)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(485, 13)
        Me.Label6.TabIndex = 77
        Me.Label6.Text = "* 点击保存按钮才能修改内容；可以点击各个列表进行排序显示;各个列可以拉宽和缩小显示"
        '
        'DownloadButton
        '
        Me.DownloadButton.Location = New System.Drawing.Point(468, 234)
        Me.DownloadButton.Name = "DownloadButton"
        Me.DownloadButton.Size = New System.Drawing.Size(75, 25)
        Me.DownloadButton.TabIndex = 78
        Me.DownloadButton.Text = "数据下载"
        Me.DownloadButton.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(5, 286)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(894, 369)
        Me.DataGridView1.TabIndex = 79
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(786, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 80
        Me.Label7.Text = "*支持手写输入"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(377, 207)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 13)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "*此选项仅供参考"
        '
        'MAC
        '
        Me.MAC.AutoSize = True
        Me.MAC.Location = New System.Drawing.Point(458, 169)
        Me.MAC.Name = "MAC"
        Me.MAC.Size = New System.Drawing.Size(30, 13)
        Me.MAC.TabIndex = 82
        Me.MAC.Text = "MAC"
        '
        'MacText
        '
        Me.MacText.Location = New System.Drawing.Point(526, 169)
        Me.MacText.Name = "MacText"
        Me.MacText.Size = New System.Drawing.Size(253, 20)
        Me.MacText.TabIndex = 83
        '
        'IP_Match_Location
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightBlue
        Me.ClientSize = New System.Drawing.Size(902, 667)
        Me.Controls.Add(Me.MacText)
        Me.Controls.Add(Me.MAC)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.DownloadButton)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Deadline)
        Me.Controls.Add(Me.MachineID)
        Me.Controls.Add(Me.HumidityStandard)
        Me.Controls.Add(Me.TemStandard)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1_num)
        Me.Controls.Add(Me.TextBox_PI4)
        Me.Controls.Add(Me.TextBox_PI3)
        Me.Controls.Add(Me.Label1_num)
        Me.Controls.Add(Me.TextBox_PI2)
        Me.Controls.Add(Me.TextBox_PI1)
        Me.Controls.Add(Me.Label1_Location)
        Me.Controls.Add(Me.TextBox_Location)
        Me.Controls.Add(Me.Label1_IP_Address)
        Me.Controls.Add(Me.Label1_IP_Point)
        Me.Controls.Add(Me.Label2_IP_Point)
        Me.Controls.Add(Me.Button_Save)
        Me.Controls.Add(Me.Label3_IP_Point)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "IP_Match_Location"
        Me.Text = "PCBA B11 温湿度信息采集系统"
        CType(Me.ErecordcontrolDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_Location As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_PI1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1_IP_Point As System.Windows.Forms.Label
    Friend WithEvents Label3_IP_Point As System.Windows.Forms.Label
    Friend WithEvents Label2_IP_Point As System.Windows.Forms.Label
    Friend WithEvents Label1_Location As System.Windows.Forms.Label
    Friend WithEvents Label1_IP_Address As System.Windows.Forms.Label
    Friend WithEvents Label1_num As System.Windows.Forms.Label
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents TextBox_PI4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_PI3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_PI2 As System.Windows.Forms.TextBox
    Friend WithEvents ErecordcontrolDataSetBindingSource As System.Windows.Forms.BindingSource
    '  Friend WithEvents ErecordcontrolDataSet As Temperature_Tool.erecordcontrolDataSet
    Friend WithEvents TextBox1_num As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TemStandard As System.Windows.Forms.ComboBox
    Friend WithEvents HumidityStandard As System.Windows.Forms.ComboBox
    Friend WithEvents MachineID As System.Windows.Forms.TextBox
    Friend WithEvents Deadline As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DownloadButton As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents MAC As Label
    Friend WithEvents MacText As TextBox
End Class
