<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.openbtn = New System.Windows.Forms.Button()
        Me.receivecheck = New System.Windows.Forms.CheckBox()
        Me.statuslabel = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.readbytes = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ReadDateAndWeeklyButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MACTextBox = New System.Windows.Forms.ComboBox()
        Me.ReadWeeklyTextBox = New System.Windows.Forms.TextBox()
        Me.ReadDateTimeTextBox = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SetDateTimeMACTextBox = New System.Windows.Forms.ComboBox()
        Me.ReadDateTimeButton = New System.Windows.Forms.Button()
        Me.SetDateTimeWeeklyTextBox = New System.Windows.Forms.TextBox()
        Me.SetDateTimeTextBox = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.SetDateTimeButton = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.SelectStatusMACTextBox = New System.Windows.Forms.ComboBox()
        Me.StatusTextBox = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SelectStatusButton = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ClearTemperatureAndHumidityExcursionButton = New System.Windows.Forms.Button()
        Me.SetDataTypeButton = New System.Windows.Forms.Button()
        Me.InsertDataCheckBox = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ClearTemperatureAndHumidityExcursionMACTextBox = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.ReadDataHumiHighValueTextBox = New System.Windows.Forms.TextBox()
        Me.ReadDataMACTextBox = New System.Windows.Forms.ComboBox()
        Me.ReadDataTempHighValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.ReadDataHumiLowValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.ReadDataTempLowValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.ReadDataHumiDeltaValueTextBox = New System.Windows.Forms.TextBox()
        Me.ReadDataHumiValueTextBox = New System.Windows.Forms.TextBox()
        Me.ReadDataTempDeltaValueTextBox = New System.Windows.Forms.TextBox()
        Me.ReadDataTempValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.ReadDataWeekTextBox = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.ReadDataDateTimeTextBox = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ReadDateButton = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.SetTemperatureTargetMacTextBox = New System.Windows.Forms.ComboBox()
        Me.SetTemHValueTextBox = New System.Windows.Forms.TextBox()
        Me.SetTemLValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.SetTemperatureTargetButton = New System.Windows.Forms.Button()
        Me.SetHumiHValueTextBox = New System.Windows.Forms.TextBox()
        Me.SetHumiLValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadioButton_05 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_04 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_03 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_02 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_01 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_07 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_06 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_00 = New System.Windows.Forms.RadioButton()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.SetHumidityTargetMacTextBox = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.SetHumidityTargetButton = New System.Windows.Forms.Button()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.SetTemperatureRightValueMacTextBox = New System.Windows.Forms.ComboBox()
        Me.SetTemperatureRightValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.SetTemperatureRightValueButton = New System.Windows.Forms.Button()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.SetHumidityRightValueMacTextBox = New System.Windows.Forms.ComboBox()
        Me.SetHumidityRightValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.SetHumidityRightValueButton = New System.Windows.Forms.Button()
        Me.ToSQLDataButton = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.SerialPort2 = New System.IO.Ports.SerialPort(Me.components)
        Me.AutoUpdateDataTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.修改配置文件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.重新加载ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.最大化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.最小化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.退出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigIPLocate = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.Running_Info = New System.Windows.Forms.TextBox()
        Me.DisTem = New System.Windows.Forms.Button()
        Me.TextBox3_XMLPath = New System.Windows.Forms.TextBox()
        Me.XML路径 = New System.Windows.Forms.Label()
        Me.BtnTest = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(188, 166)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "串口状态："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-6, 907)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "``"
        Me.Label4.Visible = False
        '
        'openbtn
        '
        Me.openbtn.Location = New System.Drawing.Point(345, 56)
        Me.openbtn.Name = "openbtn"
        Me.openbtn.Size = New System.Drawing.Size(70, 23)
        Me.openbtn.TabIndex = 7
        Me.openbtn.Text = "连接串口"
        Me.openbtn.UseVisualStyleBackColor = True
        Me.openbtn.Visible = False
        '
        'receivecheck
        '
        Me.receivecheck.AutoSize = True
        Me.receivecheck.Enabled = False
        Me.receivecheck.Location = New System.Drawing.Point(181, 344)
        Me.receivecheck.Name = "receivecheck"
        Me.receivecheck.Size = New System.Drawing.Size(98, 17)
        Me.receivecheck.TabIndex = 9
        Me.receivecheck.Text = "十六进制接收"
        Me.receivecheck.UseVisualStyleBackColor = True
        '
        'statuslabel
        '
        Me.statuslabel.AutoSize = True
        Me.statuslabel.Location = New System.Drawing.Point(259, 166)
        Me.statuslabel.Name = "statuslabel"
        Me.statuslabel.Size = New System.Drawing.Size(39, 13)
        Me.statuslabel.TabIndex = 14
        Me.statuslabel.Text = "Label6"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(55, 907)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "``"
        Me.Label6.Visible = False
        '
        'readbytes
        '
        Me.readbytes.AutoSize = True
        Me.readbytes.Location = New System.Drawing.Point(108, 907)
        Me.readbytes.Name = "readbytes"
        Me.readbytes.Size = New System.Drawing.Size(13, 13)
        Me.readbytes.TabIndex = 17
        Me.readbytes.Text = "``"
        Me.readbytes.Visible = False
        '
        'Timer1
        '
        '
        'ReadDateAndWeeklyButton
        '
        Me.ReadDateAndWeeklyButton.Location = New System.Drawing.Point(7, 41)
        Me.ReadDateAndWeeklyButton.Name = "ReadDateAndWeeklyButton"
        Me.ReadDateAndWeeklyButton.Size = New System.Drawing.Size(241, 23)
        Me.ReadDateAndWeeklyButton.TabIndex = 19
        Me.ReadDateAndWeeklyButton.Text = "读取仪器时间和星期"
        Me.ReadDateAndWeeklyButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MACTextBox)
        Me.GroupBox1.Controls.Add(Me.ReadWeeklyTextBox)
        Me.GroupBox1.Controls.Add(Me.ReadDateTimeTextBox)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.ReadDateAndWeeklyButton)
        Me.GroupBox1.Location = New System.Drawing.Point(648, 113)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(255, 122)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        '
        'MACTextBox
        '
        Me.MACTextBox.FormattingEnabled = True
        Me.MACTextBox.Location = New System.Drawing.Point(82, 14)
        Me.MACTextBox.Name = "MACTextBox"
        Me.MACTextBox.Size = New System.Drawing.Size(166, 21)
        Me.MACTextBox.TabIndex = 45
        '
        'ReadWeeklyTextBox
        '
        Me.ReadWeeklyTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadWeeklyTextBox.Location = New System.Drawing.Point(86, 96)
        Me.ReadWeeklyTextBox.Name = "ReadWeeklyTextBox"
        Me.ReadWeeklyTextBox.Size = New System.Drawing.Size(162, 20)
        Me.ReadWeeklyTextBox.TabIndex = 23
        Me.ReadWeeklyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ReadDateTimeTextBox
        '
        Me.ReadDateTimeTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDateTimeTextBox.Location = New System.Drawing.Point(86, 70)
        Me.ReadDateTimeTextBox.Name = "ReadDateTimeTextBox"
        Me.ReadDateTimeTextBox.Size = New System.Drawing.Size(162, 20)
        Me.ReadDateTimeTextBox.TabIndex = 22
        Me.ReadDateTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "星期"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(5, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "地址"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "日期"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SetDateTimeMACTextBox)
        Me.GroupBox2.Controls.Add(Me.ReadDateTimeButton)
        Me.GroupBox2.Controls.Add(Me.SetDateTimeWeeklyTextBox)
        Me.GroupBox2.Controls.Add(Me.SetDateTimeTextBox)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.SetDateTimeButton)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Location = New System.Drawing.Point(417, 328)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(221, 120)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        '
        'SetDateTimeMACTextBox
        '
        Me.SetDateTimeMACTextBox.FormattingEnabled = True
        Me.SetDateTimeMACTextBox.Location = New System.Drawing.Point(45, 15)
        Me.SetDateTimeMACTextBox.Name = "SetDateTimeMACTextBox"
        Me.SetDateTimeMACTextBox.Size = New System.Drawing.Size(166, 21)
        Me.SetDateTimeMACTextBox.TabIndex = 34
        '
        'ReadDateTimeButton
        '
        Me.ReadDateTimeButton.Location = New System.Drawing.Point(9, 39)
        Me.ReadDateTimeButton.Name = "ReadDateTimeButton"
        Me.ReadDateTimeButton.Size = New System.Drawing.Size(96, 23)
        Me.ReadDateTimeButton.TabIndex = 30
        Me.ReadDateTimeButton.Text = "读取电脑时间"
        Me.ReadDateTimeButton.UseVisualStyleBackColor = True
        '
        'SetDateTimeWeeklyTextBox
        '
        Me.SetDateTimeWeeklyTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetDateTimeWeeklyTextBox.Location = New System.Drawing.Point(51, 88)
        Me.SetDateTimeWeeklyTextBox.Name = "SetDateTimeWeeklyTextBox"
        Me.SetDateTimeWeeklyTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetDateTimeWeeklyTextBox.TabIndex = 29
        Me.SetDateTimeWeeklyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SetDateTimeTextBox
        '
        Me.SetDateTimeTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetDateTimeTextBox.Location = New System.Drawing.Point(51, 66)
        Me.SetDateTimeTextBox.Name = "SetDateTimeTextBox"
        Me.SetDateTimeTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetDateTimeTextBox.TabIndex = 28
        Me.SetDateTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(4, 93)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "星期"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 70)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "日期"
        '
        'SetDateTimeButton
        '
        Me.SetDateTimeButton.BackColor = System.Drawing.Color.DarkGray
        Me.SetDateTimeButton.Enabled = False
        Me.SetDateTimeButton.Location = New System.Drawing.Point(118, 39)
        Me.SetDateTimeButton.Name = "SetDateTimeButton"
        Me.SetDateTimeButton.Size = New System.Drawing.Size(94, 23)
        Me.SetDateTimeButton.TabIndex = 25
        Me.SetDateTimeButton.Text = "设置仪器时间"
        Me.SetDateTimeButton.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 13)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "地址"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.SelectStatusMACTextBox)
        Me.GroupBox3.Controls.Add(Me.StatusTextBox)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.SelectStatusButton)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Location = New System.Drawing.Point(417, 451)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(221, 116)
        Me.GroupBox3.TabIndex = 22
        Me.GroupBox3.TabStop = False
        '
        'SelectStatusMACTextBox
        '
        Me.SelectStatusMACTextBox.FormattingEnabled = True
        Me.SelectStatusMACTextBox.Location = New System.Drawing.Point(45, 14)
        Me.SelectStatusMACTextBox.Name = "SelectStatusMACTextBox"
        Me.SelectStatusMACTextBox.Size = New System.Drawing.Size(166, 21)
        Me.SelectStatusMACTextBox.TabIndex = 35
        '
        'StatusTextBox
        '
        Me.StatusTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusTextBox.Location = New System.Drawing.Point(47, 69)
        Me.StatusTextBox.Name = "StatusTextBox"
        Me.StatusTextBox.Size = New System.Drawing.Size(143, 20)
        Me.StatusTextBox.TabIndex = 33
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(4, 73)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 13)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "状态"
        '
        'SelectStatusButton
        '
        Me.SelectStatusButton.Location = New System.Drawing.Point(13, 39)
        Me.SelectStatusButton.Name = "SelectStatusButton"
        Me.SelectStatusButton.Size = New System.Drawing.Size(202, 23)
        Me.SelectStatusButton.TabIndex = 31
        Me.SelectStatusButton.Text = "查询工作状态"
        Me.SelectStatusButton.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(2, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(31, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "地址"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 120000
        Me.ToolTip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ToolTip1.ForeColor = System.Drawing.Color.Red
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        '
        'ClearTemperatureAndHumidityExcursionButton
        '
        Me.ClearTemperatureAndHumidityExcursionButton.Location = New System.Drawing.Point(13, 39)
        Me.ClearTemperatureAndHumidityExcursionButton.Name = "ClearTemperatureAndHumidityExcursionButton"
        Me.ClearTemperatureAndHumidityExcursionButton.Size = New System.Drawing.Size(202, 23)
        Me.ClearTemperatureAndHumidityExcursionButton.TabIndex = 31
        Me.ClearTemperatureAndHumidityExcursionButton.Text = "清除温湿度偏移量"
        Me.ToolTip1.SetToolTip(Me.ClearTemperatureAndHumidityExcursionButton, "注意：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    如果几台显示屏通过485连接在一起，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "在设置某一台地址时，其它显示屏要关电。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "否则所有显示屏地址都一样，这是不允许的。")
        Me.ClearTemperatureAndHumidityExcursionButton.UseVisualStyleBackColor = True
        '
        'SetDataTypeButton
        '
        Me.SetDataTypeButton.Location = New System.Drawing.Point(6, 89)
        Me.SetDataTypeButton.Name = "SetDataTypeButton"
        Me.SetDataTypeButton.Size = New System.Drawing.Size(218, 23)
        Me.SetDataTypeButton.TabIndex = 29
        Me.SetDataTypeButton.Text = "设置记录时间间隔"
        Me.ToolTip1.SetToolTip(Me.SetDataTypeButton, "选择时间时候,必须点击此按钮才生效")
        Me.SetDataTypeButton.UseVisualStyleBackColor = True
        '
        'InsertDataCheckBox
        '
        Me.InsertDataCheckBox.AutoSize = True
        Me.InsertDataCheckBox.Location = New System.Drawing.Point(337, 345)
        Me.InsertDataCheckBox.Name = "InsertDataCheckBox"
        Me.InsertDataCheckBox.Size = New System.Drawing.Size(50, 17)
        Me.InsertDataCheckBox.TabIndex = 36
        Me.InsertDataCheckBox.Text = "保存"
        Me.ToolTip1.SetToolTip(Me.InsertDataCheckBox, "当单个读取设备信息时候，点击此处表示保存记录到数据库中")
        Me.InsertDataCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ClearTemperatureAndHumidityExcursionMACTextBox)
        Me.GroupBox5.Controls.Add(Me.ClearTemperatureAndHumidityExcursionButton)
        Me.GroupBox5.Controls.Add(Me.Label16)
        Me.GroupBox5.Location = New System.Drawing.Point(419, 568)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(221, 94)
        Me.GroupBox5.TabIndex = 23
        Me.GroupBox5.TabStop = False
        '
        'ClearTemperatureAndHumidityExcursionMACTextBox
        '
        Me.ClearTemperatureAndHumidityExcursionMACTextBox.FormattingEnabled = True
        Me.ClearTemperatureAndHumidityExcursionMACTextBox.Location = New System.Drawing.Point(45, 13)
        Me.ClearTemperatureAndHumidityExcursionMACTextBox.Name = "ClearTemperatureAndHumidityExcursionMACTextBox"
        Me.ClearTemperatureAndHumidityExcursionMACTextBox.Size = New System.Drawing.Size(166, 21)
        Me.ClearTemperatureAndHumidityExcursionMACTextBox.TabIndex = 46
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(2, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 13)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "地址"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.ReadDataHumiHighValueTextBox)
        Me.GroupBox6.Controls.Add(Me.ReadDataMACTextBox)
        Me.GroupBox6.Controls.Add(Me.ReadDataTempHighValueTextBox)
        Me.GroupBox6.Controls.Add(Me.Label34)
        Me.GroupBox6.Controls.Add(Me.ReadDataHumiLowValueTextBox)
        Me.GroupBox6.Controls.Add(Me.Label31)
        Me.GroupBox6.Controls.Add(Me.Label33)
        Me.GroupBox6.Controls.Add(Me.ReadDataTempLowValueTextBox)
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.ReadDataHumiDeltaValueTextBox)
        Me.GroupBox6.Controls.Add(Me.ReadDataHumiValueTextBox)
        Me.GroupBox6.Controls.Add(Me.ReadDataTempDeltaValueTextBox)
        Me.GroupBox6.Controls.Add(Me.ReadDataTempValueTextBox)
        Me.GroupBox6.Controls.Add(Me.Label28)
        Me.GroupBox6.Controls.Add(Me.Label26)
        Me.GroupBox6.Controls.Add(Me.ReadDataWeekTextBox)
        Me.GroupBox6.Controls.Add(Me.Label27)
        Me.GroupBox6.Controls.Add(Me.Label23)
        Me.GroupBox6.Controls.Add(Me.ReadDataDateTimeTextBox)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Controls.Add(Me.ReadDateButton)
        Me.GroupBox6.Location = New System.Drawing.Point(180, 360)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(235, 302)
        Me.GroupBox6.TabIndex = 24
        Me.GroupBox6.TabStop = False
        '
        'ReadDataHumiHighValueTextBox
        '
        Me.ReadDataHumiHighValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataHumiHighValueTextBox.Location = New System.Drawing.Point(101, 271)
        Me.ReadDataHumiHighValueTextBox.Name = "ReadDataHumiHighValueTextBox"
        Me.ReadDataHumiHighValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataHumiHighValueTextBox.TabIndex = 32
        Me.ReadDataHumiHighValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ReadDataMACTextBox
        '
        Me.ReadDataMACTextBox.FormattingEnabled = True
        Me.ReadDataMACTextBox.Location = New System.Drawing.Point(63, 14)
        Me.ReadDataMACTextBox.Name = "ReadDataMACTextBox"
        Me.ReadDataMACTextBox.Size = New System.Drawing.Size(166, 21)
        Me.ReadDataMACTextBox.TabIndex = 33
        '
        'ReadDataTempHighValueTextBox
        '
        Me.ReadDataTempHighValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataTempHighValueTextBox.Location = New System.Drawing.Point(101, 225)
        Me.ReadDataTempHighValueTextBox.Name = "ReadDataTempHighValueTextBox"
        Me.ReadDataTempHighValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataTempHighValueTextBox.TabIndex = 32
        Me.ReadDataTempHighValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 267)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(94, 13)
        Me.Label34.TabIndex = 31
        Me.Label34.Text = "湿度报警上限值 "
        '
        'ReadDataHumiLowValueTextBox
        '
        Me.ReadDataHumiLowValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataHumiLowValueTextBox.Location = New System.Drawing.Point(101, 248)
        Me.ReadDataHumiLowValueTextBox.Name = "ReadDataHumiLowValueTextBox"
        Me.ReadDataHumiLowValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataHumiLowValueTextBox.TabIndex = 32
        Me.ReadDataHumiLowValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(8, 223)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(91, 13)
        Me.Label31.TabIndex = 31
        Me.Label31.Text = "温度报警上限值"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(4, 248)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(94, 13)
        Me.Label33.TabIndex = 31
        Me.Label33.Text = "湿度报警下限值 "
        '
        'ReadDataTempLowValueTextBox
        '
        Me.ReadDataTempLowValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataTempLowValueTextBox.Location = New System.Drawing.Point(101, 203)
        Me.ReadDataTempLowValueTextBox.Name = "ReadDataTempLowValueTextBox"
        Me.ReadDataTempLowValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataTempLowValueTextBox.TabIndex = 32
        Me.ReadDataTempLowValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(6, 202)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(94, 13)
        Me.Label29.TabIndex = 31
        Me.Label29.Text = "温度报警下限值 "
        '
        'ReadDataHumiDeltaValueTextBox
        '
        Me.ReadDataHumiDeltaValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataHumiDeltaValueTextBox.Location = New System.Drawing.Point(101, 180)
        Me.ReadDataHumiDeltaValueTextBox.Name = "ReadDataHumiDeltaValueTextBox"
        Me.ReadDataHumiDeltaValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataHumiDeltaValueTextBox.TabIndex = 30
        Me.ReadDataHumiDeltaValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ReadDataHumiValueTextBox
        '
        Me.ReadDataHumiValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataHumiValueTextBox.Location = New System.Drawing.Point(101, 134)
        Me.ReadDataHumiValueTextBox.Name = "ReadDataHumiValueTextBox"
        Me.ReadDataHumiValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataHumiValueTextBox.TabIndex = 30
        Me.ReadDataHumiValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ReadDataTempDeltaValueTextBox
        '
        Me.ReadDataTempDeltaValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataTempDeltaValueTextBox.Location = New System.Drawing.Point(101, 157)
        Me.ReadDataTempDeltaValueTextBox.Name = "ReadDataTempDeltaValueTextBox"
        Me.ReadDataTempDeltaValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataTempDeltaValueTextBox.TabIndex = 30
        Me.ReadDataTempDeltaValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ReadDataTempValueTextBox
        '
        Me.ReadDataTempValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataTempValueTextBox.Location = New System.Drawing.Point(101, 112)
        Me.ReadDataTempValueTextBox.Name = "ReadDataTempValueTextBox"
        Me.ReadDataTempValueTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataTempValueTextBox.TabIndex = 30
        Me.ReadDataTempValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(20, 179)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(67, 13)
        Me.Label28.TabIndex = 27
        Me.Label28.Text = "湿度偏移量"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(32, 135)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(43, 13)
        Me.Label26.TabIndex = 27
        Me.Label26.Text = "湿度值"
        '
        'ReadDataWeekTextBox
        '
        Me.ReadDataWeekTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataWeekTextBox.Location = New System.Drawing.Point(101, 89)
        Me.ReadDataWeekTextBox.Name = "ReadDataWeekTextBox"
        Me.ReadDataWeekTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataWeekTextBox.TabIndex = 30
        Me.ReadDataWeekTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(20, 157)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(67, 13)
        Me.Label27.TabIndex = 27
        Me.Label27.Text = "温度偏移量"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(32, 113)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(43, 13)
        Me.Label23.TabIndex = 27
        Me.Label23.Text = "温度值"
        '
        'ReadDataDateTimeTextBox
        '
        Me.ReadDataDateTimeTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ReadDataDateTimeTextBox.Location = New System.Drawing.Point(101, 66)
        Me.ReadDataDateTimeTextBox.Name = "ReadDataDateTimeTextBox"
        Me.ReadDataDateTimeTextBox.Size = New System.Drawing.Size(129, 20)
        Me.ReadDataDateTimeTextBox.TabIndex = 29
        Me.ReadDataDateTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(38, 91)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(31, 13)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "星期"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(31, 13)
        Me.Label19.TabIndex = 25
        Me.Label19.Text = "地址"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(38, 69)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(31, 13)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "日期"
        '
        'ReadDateButton
        '
        Me.ReadDateButton.Location = New System.Drawing.Point(6, 41)
        Me.ReadDateButton.Name = "ReadDateButton"
        Me.ReadDateButton.Size = New System.Drawing.Size(224, 23)
        Me.ReadDateButton.TabIndex = 24
        Me.ReadDateButton.Text = "读取仪器所有信息"
        Me.ReadDateButton.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.SetTemperatureTargetMacTextBox)
        Me.GroupBox7.Controls.Add(Me.SetTemHValueTextBox)
        Me.GroupBox7.Controls.Add(Me.SetTemLValueTextBox)
        Me.GroupBox7.Controls.Add(Me.Label37)
        Me.GroupBox7.Controls.Add(Me.Label38)
        Me.GroupBox7.Controls.Add(Me.Label39)
        Me.GroupBox7.Controls.Add(Me.SetTemperatureTargetButton)
        Me.GroupBox7.Location = New System.Drawing.Point(648, 235)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(255, 116)
        Me.GroupBox7.TabIndex = 27
        Me.GroupBox7.TabStop = False
        '
        'SetTemperatureTargetMacTextBox
        '
        Me.SetTemperatureTargetMacTextBox.FormattingEnabled = True
        Me.SetTemperatureTargetMacTextBox.Location = New System.Drawing.Point(83, 12)
        Me.SetTemperatureTargetMacTextBox.Name = "SetTemperatureTargetMacTextBox"
        Me.SetTemperatureTargetMacTextBox.Size = New System.Drawing.Size(166, 21)
        Me.SetTemperatureTargetMacTextBox.TabIndex = 46
        '
        'SetTemHValueTextBox
        '
        Me.SetTemHValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetTemHValueTextBox.Location = New System.Drawing.Point(108, 62)
        Me.SetTemHValueTextBox.Name = "SetTemHValueTextBox"
        Me.SetTemHValueTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetTemHValueTextBox.TabIndex = 23
        Me.SetTemHValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SetTemLValueTextBox
        '
        Me.SetTemLValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetTemLValueTextBox.Location = New System.Drawing.Point(108, 40)
        Me.SetTemLValueTextBox.Name = "SetTemLValueTextBox"
        Me.SetTemLValueTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetTemLValueTextBox.TabIndex = 22
        Me.SetTemLValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(6, 65)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(91, 13)
        Me.Label37.TabIndex = 21
        Me.Label37.Text = "温度报警上限值"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(6, 18)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(31, 13)
        Me.Label38.TabIndex = 20
        Me.Label38.Text = "地址"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(6, 40)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(91, 13)
        Me.Label39.TabIndex = 20
        Me.Label39.Text = "温度报警下限值"
        '
        'SetTemperatureTargetButton
        '
        Me.SetTemperatureTargetButton.Location = New System.Drawing.Point(6, 86)
        Me.SetTemperatureTargetButton.Name = "SetTemperatureTargetButton"
        Me.SetTemperatureTargetButton.Size = New System.Drawing.Size(243, 23)
        Me.SetTemperatureTargetButton.TabIndex = 19
        Me.SetTemperatureTargetButton.Text = "设置温度报警值"
        Me.SetTemperatureTargetButton.UseVisualStyleBackColor = True
        '
        'SetHumiHValueTextBox
        '
        Me.SetHumiHValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetHumiHValueTextBox.Location = New System.Drawing.Point(107, 61)
        Me.SetHumiHValueTextBox.Name = "SetHumiHValueTextBox"
        Me.SetHumiHValueTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetHumiHValueTextBox.TabIndex = 27
        Me.SetHumiHValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SetHumiLValueTextBox
        '
        Me.SetHumiLValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetHumiLValueTextBox.Location = New System.Drawing.Point(107, 39)
        Me.SetHumiLValueTextBox.Name = "SetHumiLValueTextBox"
        Me.SetHumiLValueTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetHumiLValueTextBox.TabIndex = 26
        Me.SetHumiLValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(6, 64)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(91, 13)
        Me.Label40.TabIndex = 25
        Me.Label40.Text = "湿度报警上限值"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(5, 39)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(91, 13)
        Me.Label41.TabIndex = 24
        Me.Label41.Text = "湿度报警下限值"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.SetDataTypeButton)
        Me.GroupBox8.Controls.Add(Me.Panel1)
        Me.GroupBox8.Location = New System.Drawing.Point(416, 207)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(226, 122)
        Me.GroupBox8.TabIndex = 28
        Me.GroupBox8.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadioButton_05)
        Me.Panel1.Controls.Add(Me.RadioButton_04)
        Me.Panel1.Controls.Add(Me.RadioButton_03)
        Me.Panel1.Controls.Add(Me.RadioButton_02)
        Me.Panel1.Controls.Add(Me.RadioButton_01)
        Me.Panel1.Controls.Add(Me.RadioButton_07)
        Me.Panel1.Controls.Add(Me.RadioButton_06)
        Me.Panel1.Controls.Add(Me.RadioButton_00)
        Me.Panel1.Location = New System.Drawing.Point(6, 14)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(215, 65)
        Me.Panel1.TabIndex = 0
        '
        'RadioButton_05
        '
        Me.RadioButton_05.AutoSize = True
        Me.RadioButton_05.Location = New System.Drawing.Point(69, 46)
        Me.RadioButton_05.Name = "RadioButton_05"
        Me.RadioButton_05.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton_05.TabIndex = 0
        Me.RadioButton_05.TabStop = True
        Me.RadioButton_05.Text = "4小时"
        Me.RadioButton_05.UseVisualStyleBackColor = True
        '
        'RadioButton_04
        '
        Me.RadioButton_04.AutoSize = True
        Me.RadioButton_04.Location = New System.Drawing.Point(3, 46)
        Me.RadioButton_04.Name = "RadioButton_04"
        Me.RadioButton_04.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton_04.TabIndex = 0
        Me.RadioButton_04.TabStop = True
        Me.RadioButton_04.Text = "2小时"
        Me.RadioButton_04.UseVisualStyleBackColor = True
        '
        'RadioButton_03
        '
        Me.RadioButton_03.AutoSize = True
        Me.RadioButton_03.Location = New System.Drawing.Point(143, 26)
        Me.RadioButton_03.Name = "RadioButton_03"
        Me.RadioButton_03.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton_03.TabIndex = 0
        Me.RadioButton_03.TabStop = True
        Me.RadioButton_03.Text = "1小时"
        Me.RadioButton_03.UseVisualStyleBackColor = True
        '
        'RadioButton_02
        '
        Me.RadioButton_02.AutoSize = True
        Me.RadioButton_02.Location = New System.Drawing.Point(69, 24)
        Me.RadioButton_02.Name = "RadioButton_02"
        Me.RadioButton_02.Size = New System.Drawing.Size(61, 17)
        Me.RadioButton_02.TabIndex = 0
        Me.RadioButton_02.TabStop = True
        Me.RadioButton_02.Text = "30分钟"
        Me.RadioButton_02.UseVisualStyleBackColor = True
        '
        'RadioButton_01
        '
        Me.RadioButton_01.AutoSize = True
        Me.RadioButton_01.Location = New System.Drawing.Point(3, 24)
        Me.RadioButton_01.Name = "RadioButton_01"
        Me.RadioButton_01.Size = New System.Drawing.Size(61, 17)
        Me.RadioButton_01.TabIndex = 0
        Me.RadioButton_01.TabStop = True
        Me.RadioButton_01.Text = "15分钟"
        Me.RadioButton_01.UseVisualStyleBackColor = True
        '
        'RadioButton_07
        '
        Me.RadioButton_07.AutoSize = True
        Me.RadioButton_07.Location = New System.Drawing.Point(69, 3)
        Me.RadioButton_07.Name = "RadioButton_07"
        Me.RadioButton_07.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton_07.TabIndex = 0
        Me.RadioButton_07.TabStop = True
        Me.RadioButton_07.Text = "5分钟"
        Me.RadioButton_07.UseVisualStyleBackColor = True
        '
        'RadioButton_06
        '
        Me.RadioButton_06.AutoSize = True
        Me.RadioButton_06.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_06.Name = "RadioButton_06"
        Me.RadioButton_06.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton_06.TabIndex = 0
        Me.RadioButton_06.TabStop = True
        Me.RadioButton_06.Text = "1分钟"
        Me.RadioButton_06.UseVisualStyleBackColor = True
        '
        'RadioButton_00
        '
        Me.RadioButton_00.AutoSize = True
        Me.RadioButton_00.Location = New System.Drawing.Point(143, 5)
        Me.RadioButton_00.Name = "RadioButton_00"
        Me.RadioButton_00.Size = New System.Drawing.Size(61, 17)
        Me.RadioButton_00.TabIndex = 0
        Me.RadioButton_00.TabStop = True
        Me.RadioButton_00.Text = "10分钟"
        Me.RadioButton_00.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.SetHumidityTargetMacTextBox)
        Me.GroupBox10.Controls.Add(Me.SetHumiHValueTextBox)
        Me.GroupBox10.Controls.Add(Me.SetHumiLValueTextBox)
        Me.GroupBox10.Controls.Add(Me.Label40)
        Me.GroupBox10.Controls.Add(Me.Label32)
        Me.GroupBox10.Controls.Add(Me.Label41)
        Me.GroupBox10.Controls.Add(Me.SetHumidityTargetButton)
        Me.GroupBox10.Location = New System.Drawing.Point(648, 351)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(255, 113)
        Me.GroupBox10.TabIndex = 32
        Me.GroupBox10.TabStop = False
        '
        'SetHumidityTargetMacTextBox
        '
        Me.SetHumidityTargetMacTextBox.FormattingEnabled = True
        Me.SetHumidityTargetMacTextBox.Location = New System.Drawing.Point(82, 12)
        Me.SetHumidityTargetMacTextBox.Name = "SetHumidityTargetMacTextBox"
        Me.SetHumidityTargetMacTextBox.Size = New System.Drawing.Size(166, 21)
        Me.SetHumidityTargetMacTextBox.TabIndex = 47
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(6, 18)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(31, 13)
        Me.Label32.TabIndex = 29
        Me.Label32.Text = "地址"
        '
        'SetHumidityTargetButton
        '
        Me.SetHumidityTargetButton.Location = New System.Drawing.Point(5, 85)
        Me.SetHumidityTargetButton.Name = "SetHumidityTargetButton"
        Me.SetHumidityTargetButton.Size = New System.Drawing.Size(243, 23)
        Me.SetHumidityTargetButton.TabIndex = 28
        Me.SetHumidityTargetButton.Text = "设置湿度报警值"
        Me.SetHumidityTargetButton.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.SetTemperatureRightValueMacTextBox)
        Me.GroupBox11.Controls.Add(Me.SetTemperatureRightValueTextBox)
        Me.GroupBox11.Controls.Add(Me.Label30)
        Me.GroupBox11.Controls.Add(Me.Label25)
        Me.GroupBox11.Controls.Add(Me.SetTemperatureRightValueButton)
        Me.GroupBox11.Location = New System.Drawing.Point(648, 464)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(255, 98)
        Me.GroupBox11.TabIndex = 33
        Me.GroupBox11.TabStop = False
        '
        'SetTemperatureRightValueMacTextBox
        '
        Me.SetTemperatureRightValueMacTextBox.FormattingEnabled = True
        Me.SetTemperatureRightValueMacTextBox.Location = New System.Drawing.Point(83, 13)
        Me.SetTemperatureRightValueMacTextBox.Name = "SetTemperatureRightValueMacTextBox"
        Me.SetTemperatureRightValueMacTextBox.Size = New System.Drawing.Size(166, 21)
        Me.SetTemperatureRightValueMacTextBox.TabIndex = 48
        '
        'SetTemperatureRightValueTextBox
        '
        Me.SetTemperatureRightValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetTemperatureRightValueTextBox.Location = New System.Drawing.Point(105, 72)
        Me.SetTemperatureRightValueTextBox.Name = "SetTemperatureRightValueTextBox"
        Me.SetTemperatureRightValueTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetTemperatureRightValueTextBox.TabIndex = 34
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 76)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(67, 13)
        Me.Label30.TabIndex = 33
        Me.Label30.Text = "温度正确值"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(5, 26)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(31, 13)
        Me.Label25.TabIndex = 31
        Me.Label25.Text = "地址"
        '
        'SetTemperatureRightValueButton
        '
        Me.SetTemperatureRightValueButton.Location = New System.Drawing.Point(7, 39)
        Me.SetTemperatureRightValueButton.Name = "SetTemperatureRightValueButton"
        Me.SetTemperatureRightValueButton.Size = New System.Drawing.Size(243, 23)
        Me.SetTemperatureRightValueButton.TabIndex = 0
        Me.SetTemperatureRightValueButton.Text = "设置温度正确值"
        Me.SetTemperatureRightValueButton.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.SetHumidityRightValueMacTextBox)
        Me.GroupBox12.Controls.Add(Me.SetHumidityRightValueTextBox)
        Me.GroupBox12.Controls.Add(Me.Label35)
        Me.GroupBox12.Controls.Add(Me.Label36)
        Me.GroupBox12.Controls.Add(Me.SetHumidityRightValueButton)
        Me.GroupBox12.Location = New System.Drawing.Point(648, 562)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(255, 98)
        Me.GroupBox12.TabIndex = 34
        Me.GroupBox12.TabStop = False
        '
        'SetHumidityRightValueMacTextBox
        '
        Me.SetHumidityRightValueMacTextBox.FormattingEnabled = True
        Me.SetHumidityRightValueMacTextBox.Location = New System.Drawing.Point(83, 11)
        Me.SetHumidityRightValueMacTextBox.Name = "SetHumidityRightValueMacTextBox"
        Me.SetHumidityRightValueMacTextBox.Size = New System.Drawing.Size(166, 21)
        Me.SetHumidityRightValueMacTextBox.TabIndex = 49
        '
        'SetHumidityRightValueTextBox
        '
        Me.SetHumidityRightValueTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SetHumidityRightValueTextBox.Location = New System.Drawing.Point(105, 72)
        Me.SetHumidityRightValueTextBox.Name = "SetHumidityRightValueTextBox"
        Me.SetHumidityRightValueTextBox.Size = New System.Drawing.Size(143, 20)
        Me.SetHumidityRightValueTextBox.TabIndex = 34
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(6, 75)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(67, 13)
        Me.Label35.TabIndex = 33
        Me.Label35.Text = "湿度正确值"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(6, 22)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(31, 13)
        Me.Label36.TabIndex = 31
        Me.Label36.Text = "地址"
        '
        'SetHumidityRightValueButton
        '
        Me.SetHumidityRightValueButton.Location = New System.Drawing.Point(7, 39)
        Me.SetHumidityRightValueButton.Name = "SetHumidityRightValueButton"
        Me.SetHumidityRightValueButton.Size = New System.Drawing.Size(243, 23)
        Me.SetHumidityRightValueButton.TabIndex = 0
        Me.SetHumidityRightValueButton.Text = "设置湿度正确值"
        Me.SetHumidityRightValueButton.UseVisualStyleBackColor = True
        '
        'ToSQLDataButton
        '
        Me.ToSQLDataButton.Location = New System.Drawing.Point(182, 197)
        Me.ToSQLDataButton.Name = "ToSQLDataButton"
        Me.ToSQLDataButton.Size = New System.Drawing.Size(207, 23)
        Me.ToSQLDataButton.TabIndex = 37
        Me.ToSQLDataButton.Text = "数据上传"
        Me.ToSQLDataButton.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox2.ForeColor = System.Drawing.Color.Olive
        Me.TextBox2.Location = New System.Drawing.Point(2, 124)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox2.Size = New System.Drawing.Size(172, 538)
        Me.TextBox2.TabIndex = 38
        '
        'AutoUpdateDataTimer
        '
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(186, 134)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(67, 13)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "报警串口："
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(253, 128)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(65, 21)
        Me.ComboBox1.TabIndex = 3
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
        Me.Label44.Location = New System.Drawing.Point(287, 27)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(510, 46)
        Me.Label44.TabIndex = 40
        Me.Label44.Text = "PCBA B11 温湿度信息采集系统"
        '
        'Label45
        '
        Me.Label45.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(0, 107)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(127, 13)
        Me.Label45.TabIndex = 41
        Me.Label45.Text = "最后一次数据保存信息"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.MidnightBlue
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Location = New System.Drawing.Point(3, -2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(900, 94)
        Me.Panel3.TabIndex = 43
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Temperature tool"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.修改配置文件ToolStripMenuItem, Me.重新加载ToolStripMenuItem, Me.最大化ToolStripMenuItem, Me.最小化ToolStripMenuItem, Me.退出ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 114)
        '
        '修改配置文件ToolStripMenuItem
        '
        Me.修改配置文件ToolStripMenuItem.Name = "修改配置文件ToolStripMenuItem"
        Me.修改配置文件ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.修改配置文件ToolStripMenuItem.Text = "修改配置文件"
        '
        '重新加载ToolStripMenuItem
        '
        Me.重新加载ToolStripMenuItem.Name = "重新加载ToolStripMenuItem"
        Me.重新加载ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.重新加载ToolStripMenuItem.Text = "重新加载"
        '
        '最大化ToolStripMenuItem
        '
        Me.最大化ToolStripMenuItem.Name = "最大化ToolStripMenuItem"
        Me.最大化ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.最大化ToolStripMenuItem.Text = "最大化"
        '
        '最小化ToolStripMenuItem
        '
        Me.最小化ToolStripMenuItem.Name = "最小化ToolStripMenuItem"
        Me.最小化ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.最小化ToolStripMenuItem.Text = "最小化"
        '
        '退出ToolStripMenuItem
        '
        Me.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem"
        Me.退出ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.退出ToolStripMenuItem.Text = "退出"
        '
        'ConfigIPLocate
        '
        Me.ConfigIPLocate.Location = New System.Drawing.Point(180, 226)
        Me.ConfigIPLocate.Name = "ConfigIPLocate"
        Me.ConfigIPLocate.Size = New System.Drawing.Size(207, 23)
        Me.ConfigIPLocate.TabIndex = 44
        Me.ConfigIPLocate.Text = "配置设备IP地址"
        Me.ConfigIPLocate.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(182, 254)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(207, 25)
        Me.Button3.TabIndex = 45
        Me.Button3.Text = "温湿度变化图"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox14
        '
        Me.GroupBox14.Controls.Add(Me.Running_Info)
        Me.GroupBox14.Location = New System.Drawing.Point(417, 119)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(226, 93)
        Me.GroupBox14.TabIndex = 47
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "运行信息"
        '
        'Running_Info
        '
        Me.Running_Info.Location = New System.Drawing.Point(5, 16)
        Me.Running_Info.Multiline = True
        Me.Running_Info.Name = "Running_Info"
        Me.Running_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.Running_Info.Size = New System.Drawing.Size(215, 69)
        Me.Running_Info.TabIndex = 0
        '
        'DisTem
        '
        Me.DisTem.Location = New System.Drawing.Point(182, 286)
        Me.DisTem.Name = "DisTem"
        Me.DisTem.Size = New System.Drawing.Size(205, 25)
        Me.DisTem.TabIndex = 48
        Me.DisTem.Text = "实时数据监控"
        Me.DisTem.UseVisualStyleBackColor = True
        '
        'TextBox3_XMLPath
        '
        Me.TextBox3_XMLPath.Location = New System.Drawing.Point(232, 317)
        Me.TextBox3_XMLPath.Name = "TextBox3_XMLPath"
        Me.TextBox3_XMLPath.ReadOnly = True
        Me.TextBox3_XMLPath.Size = New System.Drawing.Size(155, 20)
        Me.TextBox3_XMLPath.TabIndex = 50
        '
        'XML路径
        '
        Me.XML路径.AutoSize = True
        Me.XML路径.Location = New System.Drawing.Point(177, 324)
        Me.XML路径.Name = "XML路径"
        Me.XML路径.Size = New System.Drawing.Size(54, 13)
        Me.XML路径.TabIndex = 51
        Me.XML路径.Text = "XML Path"
        '
        'BtnTest
        '
        Me.BtnTest.Location = New System.Drawing.Point(336, 98)
        Me.BtnTest.Name = "BtnTest"
        Me.BtnTest.Size = New System.Drawing.Size(73, 31)
        Me.BtnTest.TabIndex = 52
        Me.BtnTest.Text = "Test Button"
        Me.BtnTest.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightBlue
        Me.ClientSize = New System.Drawing.Size(910, 672)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.BtnTest)
        Me.Controls.Add(Me.XML路径)
        Me.Controls.Add(Me.TextBox3_XMLPath)
        Me.Controls.Add(Me.DisTem)
        Me.Controls.Add(Me.GroupBox14)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label45)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ToSQLDataButton)
        Me.Controls.Add(Me.InsertDataCheckBox)
        Me.Controls.Add(Me.GroupBox12)
        Me.Controls.Add(Me.GroupBox11)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox10)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.readbytes)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.statuslabel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.openbtn)
        Me.Controls.Add(Me.receivecheck)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ConfigIPLocate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents openbtn As System.Windows.Forms.Button
    Friend WithEvents receivecheck As System.Windows.Forms.CheckBox
    Friend WithEvents statuslabel As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents readbytes As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ReadDateAndWeeklyButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ReadWeeklyTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReadDateTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ReadDateTimeButton As System.Windows.Forms.Button
    Friend WithEvents SetDateTimeWeeklyTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SetDateTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents SetDateTimeButton As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents SelectStatusButton As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents ClearTemperatureAndHumidityExcursionButton As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents ReadDataHumiHighValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReadDataTempHighValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents ReadDataHumiLowValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents ReadDataTempLowValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents ReadDataHumiDeltaValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReadDataHumiValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReadDataTempDeltaValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ReadDataTempValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents ReadDataWeekTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents ReadDataDateTimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ReadDateButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents SetHumiHValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SetHumiLValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents SetTemHValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SetTemLValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents SetTemperatureTargetButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents SetDataTypeButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadioButton_05 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_04 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_03 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_02 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_01 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_07 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_06 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_00 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents SetHumidityTargetButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents SetTemperatureRightValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents SetTemperatureRightValueButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents SetHumidityRightValueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents SetHumidityRightValueButton As System.Windows.Forms.Button
    Friend WithEvents InsertDataCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ToSQLDataButton As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents SerialPort2 As System.IO.Ports.SerialPort
    Friend WithEvents AutoUpdateDataTimer As System.Windows.Forms.Timer
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ConfigIPLocate As System.Windows.Forms.Button
    Friend WithEvents ReadDataMACTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents SetDateTimeMACTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents MACTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents SelectStatusMACTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents ClearTemperatureAndHumidityExcursionMACTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents SetTemperatureTargetMacTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents SetHumidityTargetMacTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents SetTemperatureRightValueMacTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents SetHumidityRightValueMacTextBox As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 最大化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 最小化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 退出ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents Running_Info As System.Windows.Forms.TextBox
    Friend WithEvents DisTem As System.Windows.Forms.Button
    Friend WithEvents TextBox3_XMLPath As TextBox
    Friend WithEvents XML路径 As Label
    Friend WithEvents 修改配置文件ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 重新加载ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BtnTest As Button
End Class
