Imports System
Imports System.IO.Ports
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Public Class Form1
    Public strIncoming As Integer
    Public str1() As String
    Public x, y As Single
    Public WriteAutoUpLoadConfigV As Boolean = False
    Public bytes() As Byte
    Public insertcheckboxdata As Int16 = 0
    Public Sub ReadConfig(ByVal ConfigPath As String)
        If File.Exists(ConfigPath) = False Then
            MsgBox("Not find config file!")
            End
        End If
        Dim Str As String = My.Computer.FileSystem.ReadAllText(ConfigPath, System.Text.UnicodeEncoding.UTF8)
        Dim RowsString() As String = Str.Split(vbLf)
        If RowsString.Length > 1 Then
            For i As Int32 = 0 To RowsString.Length - 1
                If Mid(Trim(RowsString(i)), 1, 1) = "*" Then
                    Continue For
                End If
                Dim FileString() As String = RowsString(i).Split("#")
                If FileString.Length > 1 Then
                    If FileString(0) = "SQL Connection Strng" Then
                        If FileString(1) = "" Then
                            MsgBox("SQL Connection String is empty!")
                            End
                        End If
                        Dim myEncoding As Encoding = Encoding.GetEncoding("utf-8")         '解密数据库链接
                        Dim myByte() As Byte = Convert.FromBase64String(FileString(1))    '解密数据库链接
                        SqlConnectString = myEncoding.GetString(myByte)
                    ElseIf FileString(0) = "Data Save Table Name" Then
                        If FileString(1) = "" Then
                            MsgBox("Data Save Table Name is empty!")
                            End
                        Else
                            DataSaveTableName = FileString(1)
                            ConnectStr = SqlConnectString
                            Dim dr As SqlDataReader = SqlSelect("Select * From sysObjects Where Name ='" & DataSaveTableName & "' And Type In ('S','U')")
                            If dr.Read = False Then
                                Dim addTable As String = "CREATE TABLE [dbo].[" & DataSaveTableName & "](" _
                                           & "[ID] [int] IDENTITY(1,1) NOT NULL UNIQUE(ID)," _
                                           & "[dates] [date] NOT NULL," _
                                           & "[detailtime] [varchar](50) NOT NULL," _
                                           & "[tem] [varchar](50) NULL," _
                                           & "[hum] [varchar](50) NULL," _
                                           & "[temperaturelow] [float] NULL," _
                                           & "[temperaturehigh] [float] NULL," _
                                           & "[Humiditylow] [float] NULL," _
                                           & "[Humidityhigh] [float] NULL," _
                                           & "[machineno] [varchar](50) NOT NULL," _
                                           & "[assettype] [varchar](50) NULL," _
                                           & "[uploadtime] [datetime] NULL," _
                                           & "[actions] [varchar](200) NULL," _
                                           & "[strowner] [varchar](50) NULL," _
                                           & "[status] [varchar](50)  NULL DEFAULT 'Open'," _
                                           & "[closedate] [datetime] NULL)"
                                SqlUPDATE(addTable)
                            End If
                            dr.Close()
                        End If
                    ElseIf FileString(0) = "IP Address And Location Manage Table Name" Then
                        If FileString(1) = "" Then
                            MsgBox("IP Address And Location Manage Table Name is empty!")
                            End
                        Else
                            IPAddressAndLocationManageTableName = FileString(1)
                            ConnectStr = SqlConnectString
                            Dim dr As SqlDataReader = SqlSelect("Select * From sysObjects Where Name ='" & IPAddressAndLocationManageTableName & "' And Type In ('S','U')")
                            If dr.Read = False Then
                                Dim AddTableString As String = "CREATE TABLE [dbo].[" & IPAddressAndLocationManageTableName & "](" _
                                                                    & "[ID] [int] IDENTITY(1,1) NOT NULL UNIQUE(ID)," _
                                                                    & "[num] [nvarchar](50) NOT NULL," _
                                                                    & "[IPaddress] [nvarchar](50) NOT NULL," _
                                                                    & "[port] int default(5300)," _
                                                                    & "[location] [nvarchar](50) NOT NULL UNIQUE([IPaddress],[location])," _
                                                                    & "[standard] [nvarchar](max) NOT NULL," _
                                                                    & "[machineID] [nvarchar](50) NOT NULL UNIQUE([num],[machineID])," _
                                                                    & "[deadline] [nvarchar](50) NOT NULL," _
                                                                    & "[isopen] [bit] NOT NULL default(1)," _
                                                                    & "[Mac]  NVARCHAR (50)  NULL)"
                                SqlUPDATE(AddTableString)
                            End If
                            dr.Close()
                        End If
                    ElseIf FileString(0) = "SoftWare Work Location" Then
                        WorkLocationString = FileString(1)
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        '禁止窗体被关闭，当关闭窗体时候，收缩窗体隐藏
        e.Cancel = True
        If client IsNot Nothing Then
            If client.Connected = True Then
                client.Close()
            End If
        End If
        Me.Hide()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Init_log("mylog.txt")

        '获取计算机有效串口
        ReadConfig(Application.StartupPath & "\config.ini")
        Me.Text = "PCBA " & WorkLocationString & "温湿度信息采集系统"
        Label44.Text = "PCBA " & WorkLocationString & "温湿度信息采集系统"
        Call ReLoadMainWindow()
        Dim FileName As String = Application.StartupPath & "/Set Record Every Time.ini"
        Dim StrEveryTime As String = ""
        Dim srEveryTime As StreamReader = New StreamReader(FileName)
        StrEveryTime = srEveryTime.ReadToEnd()
        srEveryTime.Close()
        If StrEveryTime.Length > 1 Then
            For Each i As RadioButton In Panel1.Controls
                If i.Name = Mid(StrEveryTime, 1, StrEveryTime.Length - 2) Then
                    i.Checked = True
                End If
            Next
        End If
        receivecheck.Checked = True   '十六进制输出
        Dim ports As String() = SerialPort.GetPortNames() '必须用命名空间，用SerialPort,获取计算机的有效串口
        Dim port As String
        For Each port In ports
            ComboBox1.Items.Add(port) '向combobox中添加项
        Next port
        Call ReadConfigXML()
        If TextBox3_XMLPath.Text = "" Then
            Running_Info.Text = "xml path is blank,reset your path in config file"
            Running_Info.ForeColor = Color.Red
        End If

        ' Label3.Text = SerialPort1.IsOpen
        statuslabel.Text = "串口未连接"
        statuslabel.ForeColor = Color.Red
        '  SendText = "55 AA 01 21 22"
        ' linecheck.Enabled = True
        '  timebox.Enabled = True
        SetDataTypeButton_Click(sender, e)
        WriteAutoUpLoadConfigV = True
        x = Me.Width
        y = Me.Height
        setTag(Me)
        Me.Location = New Point((My.Computer.Screen.WorkingArea.Width - Me.Width) / 2, (My.Computer.Screen.WorkingArea.Height - Me.Height) / 2)

        '开启用于测试
        BtnTest.Visible = False
    End Sub

    ' 递归取控件的原始大小和位置，用tag来纪录
    Private Sub setTag(ByVal obj As Object)

        For Each con As Control In obj.Controls

            con.Tag = con.Width & ":" & con.Height & ":" & con.Left & ":" & con.Top & ":" & con.Font.Size

            ' 如果是容器控件, 则递归继续纪录

            If con.Controls.Count > 0 Then

                setTag(con)
            End If
        Next
    End Sub

    ' 递归重新设定控件的大小和位置
    Private Sub setControls(ByVal newx As Single, ByVal newy As Single, ByVal obj As Object)
        Try
            For Each con As Control In obj.Controls
                con.AutoSize = False
                Dim mytag() As String = con.Tag.ToString.Split(":")
                con.Width = mytag(0) * newx
                con.Height = mytag(1) * newy
                con.Left = mytag(2) * newx
                con.Top = mytag(3) * newy

                ' 计算字体缩放比例, 缩放字体
                Dim currentSize As Single = (mytag(1) * newy * mytag(4)) / mytag(1)
                con.Font = New Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit)

                ' 如果是容器控件, 则递归继续缩放
                If con.Controls.Count > 0 Then
                    setControls(newx, newy, con)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmDl_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        ' 得到现在窗体的大小, 然后根据原始大小计算缩放比例
        If x = 0 Then
            Exit Sub
        End If
        Dim newx As Single = Me.Width / x
        Dim newy As Single = Me.Height / y
        setControls(newx, newy, Me)
    End Sub

    '定时发送数据
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        RunningLocation = "自动上传"
        RestSendInt = 0
        ErrList = ""
        SendType = 5
        Timer1.Enabled = False
        InsertData = True
        SaveTime = Now
        TextBox2.Clear()
        TextBox2.Refresh()
        Dim ChksmString As String = Hex(&H23 + 0)
        Dim SendMacString = "00"
        Dim SendText As String = "55 AA " & SendMacString & " 23 " & ChksmString
        ' sendbox.Text = SendText
        'MacInt 值等于数据库中的num
        SendHex = True
        For i As Int32 = 0 To SaveMaxRunningInt - 1
            MacInt = i
            If SendDataGroup(i) = Nothing Then
                InsertData = False
                SendHex = False
                Call AllOpen()
                Exit For
            End If
            Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_num(Val(SendDataGroup(MacInt)))
            Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
            'Dim RString As String = Connect("10.201.58.55", SendText)
            If RString <> "" Then
                Call Temperature(RString)
                If MacInt >= SaveMaxRunningInt - 1 Then
                    AllOpen()
                ElseIf i = SaveMaxRunningInt - 1 Then
                    Call AllOpen()
                End If
            Else
                If MacInt >= SaveMaxRunningInt - 1 Then
                    AllOpen()
                ElseIf i = SaveMaxRunningInt - 1 Then
                    Call AllOpen()
                End If
            End If

        Next
    End Sub

    ' 读取仪器时间和星期按钮
    Private Sub ReadDateAndWeeklyButton_Click(sender As Object, e As EventArgs) Handles ReadDateAndWeeklyButton.Click
        Running_Info.Text = ""
        ReadDateTimeTextBox.Text = ""
        ReadWeeklyTextBox.Text = ""
        Dim Location As String = Trim(MACTextBox.Text)
        SendType = 0
        ReadDateTimeTextBox.Text = ""
        ReadWeeklyTextBox.Text = ""
        If MACTextBox.Text = "" Then
            Running_Info.Text = "请输入你要查询的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(MACTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        Dim ChksmString As String = Hex(&H21 + Val("&H" & Val(MACTextboxString)))
        Dim SendText As String = "55 AA " & MACTextboxString & " 21 " & ChksmString
        '  sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要设置的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)
        If IPaddress.Item1 = "" Then
            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        End If
    End Sub

    ' 设置时间， 设置设备的时间
    Private Sub SetDateTimeButton_Click(sender As Object, e As EventArgs) Handles SetDateTimeButton.Click
        Running_Info.Text = ""
        SendType = 1
        If SetDateTimeMACTextBox.Text = "" Then
            Running_Info.Text = "请输入你要设置的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(SetDateTimeMACTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        Dim SetDateTime As Date = CDate(SetDateTimeTextBox.Text)
        Dim SecondString As String = ""
        If SetDateTime.Second < 10 Then
            SecondString = "0" & SetDateTime.Second
        Else
            SecondString = SetDateTime.Second
        End If

        Dim MinuteString As String = ""
        If SetDateTime.Minute < 10 Then
            MinuteString = "0" & SetDateTime.Minute
        Else
            MinuteString = SetDateTime.Minute
        End If

        Dim HourString As String = ""
        If SetDateTime.Hour < 10 Then
            HourString = "0" & SetDateTime.Hour
        Else
            HourString = SetDateTime.Hour
        End If

        Dim DayString As String = ""
        If SetDateTime.Day < 10 Then
            DayString = "0" & SetDateTime.Day
        Else
            DayString = SetDateTime.Day
        End If

        Dim MonthString As String = ""
        If SetDateTime.Month < 10 Then
            MonthString = "0" & SetDateTime.Month
        Else
            MonthString = SetDateTime.Month
        End If
        Dim YearString As String = Mid(SetDateTime.Year, 3, 2)
        Dim WeeklyString As String = "0" & SetDateTime.DayOfWeek
        Dim ChksmString As String = Hex(&H20 + Val("&H" & Val(SecondString)) + Val("&H" & Val(MinuteString)) + Val("&H" & Val(HourString)) + Val("&H" & Val(WeeklyString)) + Val("&H" & Val(DayString)) + Val("&H" & Val(MonthString)) + Val("&H" & Val(YearString)) + Val("&H" & Val(MACTextboxString)))
        ' ChksmString = Mid(ChksmString, ChksmString.Length - 2 + 1, 2)

        Dim SendText As String = "55 AA " & MACTextboxString & " 20 " & SecondString & " " & MinuteString & " " & HourString & " " & WeeklyString & " " & DayString & " " & MonthString & " " & YearString & " " & ChksmString
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Trim(SetDateTimeMACTextBox.Text))
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        'Dim RString As String = Connect("10.201.58.55", SendText)
        If RString <> "" Then
            Call Temperature(RString)
            SetDateTimeButton.Enabled = False
            SetDateTimeButton.BackColor = Color.Gray
        Else
            Call Write_log(SetDateTimeMACTextBox.Text + " 设置时间没有返回值！")
            SetDateTimeButton.Enabled = False
            SetDateTimeButton.BackColor = Color.Red
        End If

    End Sub

    '读取时间, 读取的是本机的
    Private Sub ReadDateTimeButton_Click(sender As Object, e As EventArgs) Handles ReadDateTimeButton.Click
        Running_Info.Text = ""
        SetDateTimeTextBox.Text = ""
        SetDateTimeWeeklyTextBox.Text = ""
        SetDateTimeTextBox.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim WeeklyInt As Int16 = Now.DayOfWeek
        Dim WeeklyString As String = ""
        If WeeklyInt = 1 Then
            WeeklyString = "星期一"
        ElseIf WeeklyInt = 2 Then
            WeeklyString = "星期二"
        ElseIf WeeklyInt = 3 Then
            WeeklyString = "星期三"
        ElseIf WeeklyInt = 4 Then
            WeeklyString = "星期四"
        ElseIf WeeklyInt = 5 Then
            WeeklyString = "星期五"
        ElseIf WeeklyInt = 6 Then
            WeeklyString = "星期六"
        ElseIf WeeklyInt = 7 Then
            WeeklyString = "星期日"
        End If
        SetDateTimeWeeklyTextBox.Text = WeeklyString
        SetDateTimeButton.BackColor = Color.Lime
        SetDateTimeButton.Enabled = True
    End Sub

    ' 查询工作状态按钮
    Private Sub SelectStatusButton_Click(sender As Object, e As EventArgs) Handles SelectStatusButton.Click
        Running_Info.Text = ""
        StatusTextBox.Text = ""
        SendType = 2
        StatusTextBox.Text = ""
        StatusTextBox.BackColor = Color.White
        Dim Location As String = Trim(SelectStatusMACTextBox.Text)

        Dim MACTextboxString As String

        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        Dim ChksmString As String = Hex(&H22 + Val("&H" & Val(MACTextboxString)))
        Dim SendText As String = "55 AA " & MACTextboxString & " 22 " & ChksmString
        ' sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要设置的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If

        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)  ' "10.201.60.223" 

        If IPaddress.Item1 = "" Then
            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        Else
            Call Write_log(SetDateTimeMACTextBox.Text + " 查询工作状态没有返回值！")
        End If
    
    End Sub
    Private Sub ClearTemperatureAndHumidityExcursionButton_Click(sender As Object, e As EventArgs) Handles ClearTemperatureAndHumidityExcursionButton.Click
        Running_Info.Text = ""
        Dim Location As String = Trim(ClearTemperatureAndHumidityExcursionMACTextBox.Text)
        SendType = 4
        If ClearTemperatureAndHumidityExcursionMACTextBox.Text = "" Then
            Running_Info.Text = "请输入你要设置的地址!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(ClearTemperatureAndHumidityExcursionMACTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        Dim ChksmString As String = Hex(&H2A)
        Dim SendText As String = "55 AA " & MACTextboxString & " 2A " & ChksmString
        '  sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要设置的地址!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)
        If IPaddress.Item1 = "" Then
            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        End If
    End Sub

    ' 清除旧的状态信息
    Private Sub ClearInfoText()
        ReadDataDateTimeTextBox.Text = ""
        ReadDataWeekTextBox.Text = ""
        ReadDataTempValueTextBox.Text = ""
        ReadDataHumiValueTextBox.Text = ""
        ReadDataTempDeltaValueTextBox.Text = ""
        ReadDataHumiDeltaValueTextBox.Text = ""
        ReadDataTempLowValueTextBox.Text = ""
        SetTemLValueTextBox.Text = ""
        SetTemHValueTextBox.Text = ""
        SetHumiLValueTextBox.Text = ""
        SetHumiHValueTextBox.Text = ""
        SetTemperatureRightValueTextBox.Text = ""
        SetHumidityRightValueTextBox.Text = ""

        ReadDataTempHighValueTextBox.Text = ""
        ReadDataHumiLowValueTextBox.Text = ""
        ReadDataHumiHighValueTextBox.Text = ""

        StatusTextBox.Text = ""
        StatusTextBox.BackColor = Color.White

        SetDateTimeButton.BackColor = Color.DarkGray
    End Sub

    ' 读取仪器所有信息 按钮
    Private Sub ReadDateButton_Click(sender As Object, e As EventArgs) Handles ReadDateButton.Click
        RunningLocation = "手动测试"
        Running_Info.Text = ""

        Call ClearInfoText()
        SendType = 5
        If ReadDataMACTextBox.Text = "" Then
            Running_Info.Text = "请输入你要读取的地址!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        '全部修改成00
        SaveTime = Now
        Dim MACTextboxString As String = "0"
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        Dim ChksmString As String = Hex(&H23 + Val("&H" & Val(MACTextboxString)))
        Dim SendText As String = "55 AA " & MACTextboxString & " 23 " & ChksmString
        MacInt = Int(MACTextboxString)
        SendHex = True
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Trim(ReadDataMACTextBox.Text))
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call CheckInsertTemperature(RString)
            Call ReadDateTimeButton_Click(sender, e)
            Call SelectStatusButton_Click(sender, e)
            Call ReadDateAndWeeklyButton_Click(sender, e)
        Else
            Running_Info.Text = ReadDataMACTextBox.Text & Environment.NewLine & IPaddress.Item1 _
                                & vbTab & "通信异常！"
            Running_Info.ForeColor = Color.Red
        End If
    End Sub

    Private Sub SetTemperatureTargetButton_Click(sender As Object, e As EventArgs) Handles SetTemperatureTargetButton.Click
        Running_Info.Text = ""
        Dim Location As String = Trim(SetTemperatureTargetMacTextBox.Text)
        '设置温度极限值
        SendType = 6
        If SetTemperatureTargetMacTextBox.Text = "" Then
            Running_Info.Text = "请输入你要读取的地址!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(SetTemperatureTargetMacTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        If SetTemLValueTextBox.Text = "" Then
            Running_Info.Text = "请设置温度报警下限值!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        If SetTemHValueTextBox.Text = "" Then
            Running_Info.Text = "请设置温度报警上限值!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim TempL As Int32 = Val(SetTemLValueTextBox.Text)
        Dim TempH As Int32 = Val(SetTemHValueTextBox.Text)
        If TempL >= TempH Then
            Running_Info.Text = "下限值不能大于上限值！请重新输入"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        ElseIf TempL < -52 Or TempH < -52 Then
            Running_Info.Text = "上下限不能小于-52度"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        ElseIf TempL > 6501 Or TempH > 6501 Then
            Running_Info.Text = "上下限不能大于6501度"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim TempLowLString As String = ""
        Dim TempLowHString As String = ""
        Dim findString As String = Hex(TempL * 10 + 520)
        If findString.Length = 1 Then
            TempLowLString = "0" & findString
            TempLowHString = "00"
        ElseIf findString.Length = 2 Then
            TempLowLString = findString
            TempLowHString = "00"
        ElseIf findString.Length = 3 Then
            TempLowLString = Mid(findString, 2, 2)
            TempLowHString = "0" & Mid(findString, 1, 1)
        ElseIf findString.Length = 4 Then
            TempLowLString = Mid(findString, 3, 2)
            TempLowHString = Mid(findString, 1, 2)
        Else
            Running_Info.Text = "上下限设置错误！"
            Running_Info.ForeColor = Color.Red
        End If

        Dim TempHighLString As String = ""
        Dim TempHighHString As String = ""
        findString = Hex(TempH * 10 + 520)
        If findString.Length = 1 Then
            TempHighLString = "0" & findString
            TempHighHString = "00"
        ElseIf findString.Length = 2 Then
            TempHighLString = findString
            TempHighHString = "00"
        ElseIf findString.Length = 3 Then
            TempHighLString = Mid(findString, 2, 2)
            TempHighHString = "0" & Mid(findString, 1, 1)
        ElseIf findString.Length = 4 Then
            TempHighLString = Mid(findString, 3, 2)
            TempHighHString = Mid(findString, 1, 2)
        Else
            Running_Info.Text = "上下限设置错误！"
            Running_Info.ForeColor = Color.Red
        End If
        Dim HumiLowLString As String = "FF" 'Hex((HumiL * 10 + 100) Mod 256)
        Dim HumiLowHString As String = "FF" 'Hex(Math.Truncate((HumiL * 10 + 100) / 256))
        Dim HumiHighLString As String = "FF" 'Hex((HumiH * 10 + 100) Mod 256)
        Dim HumiHighHString As String = "FF" 'Hex(Math.Truncate((HumiH * 10 + 100) / 256))
        Dim ChksumString As String = Hex(&H26 + Val("&H" & TempLowLString) + Val("&H" & TempLowHString) + Val("&H" & TempHighLString) + Val("&H" & TempHighHString) + Val("&H" & HumiLowLString) + Val("&H" & HumiLowHString) + Val("&H" & HumiHighLString) + Val("&H" & HumiHighHString) + Val("&H" & Val(MACTextboxString)))
        ChksumString = Mid(ChksumString, ChksumString.Length - 2 + 1, 2)
        Dim SendText As String = "55 AA " & MACTextboxString & " 26" & " " & TempLowLString & " " & TempLowHString & " " & TempHighLString & " " & TempHighHString & " " & HumiLowLString & " " & HumiLowHString & " " & HumiHighLString & " " & HumiHighHString & " " & ChksumString
        'sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要设置的地址!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)
        If IPaddress.Item1 = "" Then
            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        End If
    End Sub

    Private Sub SetHumidityTargetButton_Click(sender As Object, e As EventArgs) Handles SetHumidityTargetButton.Click

        Running_Info.Text = ""
        Dim Location As String = Trim(SetHumidityTargetMacTextBox.Text)
        SendType = 9
        If SetHumidityTargetMacTextBox.Text = "" Then
            Running_Info.Text = "请输入你要设置的地址!"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(SetHumidityTargetMacTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        If SetHumiLValueTextBox.Text = "" Then

            Running_Info.Text = "请设置湿度报警下限值！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        If SetHumiHValueTextBox.Text = "" Then

            Running_Info.Text = "请设置湿度报警上限值！"
            Running_Info.ForeColor = Color.Red

            Exit Sub
        End If
        Dim HumiL As Int32 = Val(SetHumiLValueTextBox.Text)
        Dim HumiH As Int32 = Val(SetHumiHValueTextBox.Text)
        If HumiL >= HumiH Then
            Running_Info.Text = "下限值不能大于上限值！请重新输入!"
            'SetDateTimeButton.BackColor = Color.Lime
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim TempLowLString As String = "FF" 'Hex((TempL * 10 + 8) Mod 256)
        Dim TempLowHString As String = "FF" 'Hex(Math.Truncate((TempL * 10 + 8) / 256) + 2)
        Dim TempHighLString As String = "FF" 'Hex((TempH * 10 + 8) Mod 256)
        Dim TempHighHString As String = "FF" ' Hex(Math.Truncate((TempH * 10 + 8) / 256) + 2)
        Dim HumiLowLString As String = Hex((HumiL * 10 + 100) Mod 256)
        If HumiLowLString.Length < 2 Then
            HumiLowLString = "0" & HumiLowLString
        End If
        Dim HumiLowHString As String = Hex(Math.Truncate((HumiL * 10 + 100) / 256))
        If HumiLowHString.Length < 2 Then
            HumiLowHString = "0" & HumiLowHString
        End If
        Dim HumiHighLString As String = Hex((HumiH * 10 + 100) Mod 256)
        If HumiHighLString.Length < 2 Then
            HumiHighLString = "0" & HumiHighLString
        End If
        Dim HumiHighHString As String = Hex(Math.Truncate((HumiH * 10 + 100) / 256))
        If HumiHighHString.Length < 2 Then
            HumiHighHString = "0" & HumiHighHString
        End If
        Dim ChksumString As String = Hex(&H26 + Val("&H" & TempLowLString) + Val("&H" & TempLowHString) + Val("&H" & TempHighLString) + Val("&H" & TempHighHString) + Val("&H" & HumiLowLString) + Val("&H" & HumiLowHString) + Val("&H" & HumiHighLString) + Val("&H" & HumiHighHString) + Val("&H" & Val(MACTextboxString)))
        ChksumString = Mid(ChksumString, ChksumString.Length - 2 + 1, 2)
        Dim SendText As String = "55 AA " & MACTextboxString & " 26" & " " & TempLowLString & " " & TempLowHString & " " & TempHighLString & " " & TempHighHString & " " & HumiLowLString & " " & HumiLowHString & " " & HumiHighLString & " " & HumiHighHString & " " & ChksumString
        ' sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要设置的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)
        If IPaddress.Item1 = "" Then

            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        End If
    End Sub

    Private Sub SetTemperatureRightValueButton_Click(sender As Object, e As EventArgs) Handles SetTemperatureRightValueButton.Click
        Running_Info.Text = ""
        Dim Location As String = Trim(SetTemperatureRightValueMacTextBox.Text)
        SendType = 10
        If SetTemperatureRightValueMacTextBox.Text = "" Then

            Running_Info.Text = "请输入你要设置的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(SetTemperatureRightValueMacTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If

        If SetTemperatureRightValueTextBox.Text = "" Then
            Running_Info.Text = "请设置温度正确值！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim Temp As Int32 = Val(SetTemperatureRightValueTextBox.Text)
        Dim TempLString As String = Hex((Temp * 10 + 8) Mod 256)
        Dim TempHString As String = Hex(Math.Truncate((Temp * 10 + 8) / 256) + 2)
        If TempHString.Length < 2 Then
            TempHString = "0" & TempHString
        End If

        Dim HumiLString As String = "FF" 'Hex((HumiL * 10 + 100) Mod 256)
        If HumiLString.Length < 2 Then
            HumiLString = "0" & HumiLString
        End If
        Dim HumiHString As String = "FF" 'Hex(Math.Truncate((HumiL * 10 + 100) / 256))
        If HumiHString.Length < 2 Then
            HumiHString = "0" & HumiHString
        End If

        Dim ChksumString As String = Hex(&H29 + Val("&H" & TempLString) + Val("&H" & TempHString) + Val("&H" & HumiLString) + Val("&H" & HumiHString) + Val("&H" & Val(MACTextboxString)))
        ChksumString = Mid(ChksumString, ChksumString.Length - 2 + 1, 2)
        Dim SendText As String = "55 AA " & MACTextboxString & " 29 " & TempLString & " " & TempHString & " " & HumiLString & " " & HumiHString & " 0 0 0 0 " & ChksumString
        'sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要设置的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)
        If IPaddress.Item1 = "" Then
            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP！"
            Running_Info.ForeColor = Color.Red

            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        End If
    End Sub
    Private Sub SetHumidityRightValueButton_Click(sender As Object, e As EventArgs) Handles SetHumidityRightValueButton.Click
        Running_Info.Text = ""
        Dim Location As String = Trim(SetHumidityRightValueMacTextBox.Text)
        SendType = 12
        If SetHumidityRightValueMacTextBox.Text = "" Then
            Running_Info.Text = "请输入你要读取的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim MACTextboxString As String = "0" ' Val(SetHumidityRightValueMacTextBox.Text)
        If MACTextboxString < 9 Then
            MACTextboxString = "0" & MACTextboxString
        End If
        If SetHumidityRightValueTextBox.Text = "" Then
            Running_Info.Text = "请设置湿度正确值！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim Humi As Int32 = Val(SetHumidityRightValueTextBox.Text)
        Dim TempLString As String = "FF" 'Hex((Temp * 10 + 8) Mod 256)
        Dim TempHString As String = "FF" 'Hex(Math.Truncate((Temp * 10 + 8) / 256) + 2)
        If TempHString.Length < 2 Then
            TempHString = "0" & TempHString
        End If

        Dim HumiLString As String = Hex((Humi * 10 + 100) Mod 256)
        If HumiLString.Length < 2 Then
            HumiLString = "0" & HumiLString
        End If
        Dim HumiHString As String = Hex(Math.Truncate((Humi * 10 + 100) / 256))
        If HumiHString.Length < 2 Then
            HumiHString = "0" & HumiHString
        End If

        Dim ChksumString As String = Hex(&H29 + Val("&H" & TempLString) + Val("&H" & TempHString) + Val("&H" & HumiLString) + Val("&H" & HumiHString) + Val("&H" & Val(MACTextboxString)))
        ChksumString = Mid(ChksumString, ChksumString.Length - 2 + 1, 2)
        Dim SendText As String = "55 AA " & MACTextboxString & " 29 " & TempLString & " " & TempHString & " " & HumiLString & " " & HumiHString & " 0 0 0 0 " & ChksumString
        ' sendbox.Text = SendText
        SendHex = True
        If Location = "" Then
            Running_Info.Text = "请输入你要读取的地址！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(Location)
        If IPaddress.Item1 = "" Then
            Running_Info.Text = "你输入的Location错误，未找到与之对应的IP！"
            Running_Info.ForeColor = Color.Red
            Exit Sub
        End If
        Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
        If RString <> "" Then
            Call Temperature(RString)
        End If
    End Sub
    Private Sub InsertDataCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles InsertDataCheckBox.CheckedChanged
        If ReadDataMACTextBox.Text = "" Then
            Running_Info.Text = "请先选择你要读取的信息设备!"
            Running_Info.ForeColor = Color.Red
            InsertDataCheckBox.Checked = False
            Exit Sub
        Else
            If InsertDataCheckBox.Checked = True Then
                insertcheckboxdata = 1
                '   InsertData = True
                Dim Con As New SqlConnection
                Dim Cmd As New SqlCommand
                Con.ConnectionString = SqlConnectString
                Con.Open()
                Cmd.Connection = Con
                Cmd.CommandText = "select num from " & IPAddressAndLocationManageTableName & "  where location =' " & ReadDataMACTextBox.Text & " ' "
                Dim dr As SqlDataReader = Nothing
                Dim FindInt As Int32 = 0
                dr = Cmd.ExecuteReader
                If dr.HasRows Then
                    While dr.Read
                        SendDataGroup(FindInt) = dr("num")
                        FindInt = FindInt + 1
                    End While
                End If
                InsertDataCheckBox.Text = "保存数据"
                SaveMaxRunningInt = FindInt
                dr.Close()
                Con.Close()
            Else
                ' InsertData = False
                insertcheckboxdata = 0
                InsertDataCheckBox.Text = "不保存数据"
            End If
        End If
    End Sub

    ' 数据上传按钮
    Private Sub ToSQLDataButton_Click(sender As Object, e As EventArgs) Handles ToSQLDataButton.Click, ToSQLDataButton.Click
        RunningLocation = "自动上传"
        Try
            ErrList = ""
            Call AllColse()
            MacInt = 0
            SaveMaxRunningInt = 0
            Dim Con As New SqlConnection
            Dim Cmd As New SqlCommand
            Con.ConnectionString = SqlConnectString
            Con.Open()
            Cmd.Connection = Con
            Cmd.CommandText = "select num from " & IPAddressAndLocationManageTableName & "  where isopen=1 Group By num"
            Dim dr As SqlDataReader = Nothing
            dr = Cmd.ExecuteReader
            Dim FindInt As Int32 = 0
            If dr.HasRows Then
                While dr.Read
                    SendDataGroup(FindInt) = dr("num")
                    FindInt = FindInt + 1
                End While
            End If
            SaveMaxRunningInt = FindInt
            dr.Close()
            Con.Close()
            If TextBox2.Text = "" Then
                Timer1.Interval = 10
                Timer1.Enabled = True
            Else
                Timer1.Interval = 10
                Timer1.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Alert_Window.Show()
    End Sub
    Private Sub AutoUpdateDataTimer_Tick(sender As Object, e As EventArgs) Handles AutoUpdateDataTimer.Tick
        '在用户选择了radio 的timer之后，调用update data 事件（如配置文件有信息，则读取配置信息去启动对应设备，如没信息则弹出设置设备窗口）
        Call ToSQLDataButton_Click(sender, e)
        ' Call TimeDataPage.StartGetData_Click(sender, e)
    End Sub

    ' 配置自动采集的定时器
    Private Sub SetDataTypeButton_Click(sender As Object, e As EventArgs) Handles SetDataTypeButton.Click
        Running_Info.Text = ""
        SetRecordEveryTime = ""
        '当用户选择radio 时候，转换成毫秒
        Dim InsertConfigString As String = ""
        Dim FindV As Boolean = False '目的是用来判断是否选择了任意一个radio
        For Each i As RadioButton In Panel1.Controls
            If i.Checked = True Then
                FindV = True
                Dim Str() As String = i.Name.Split("_")
                Dim FindString As String = Str(1)
                If FindString = "00" Then
                    SetRecordEveryTime = 10
                    InsertConfigString = i.Name
                ElseIf FindString = "01" Then
                    SetRecordEveryTime = 15
                    InsertConfigString = i.Name
                ElseIf FindString = "02" Then
                    SetRecordEveryTime = 30
                    InsertConfigString = i.Name
                ElseIf FindString = "03" Then
                    SetRecordEveryTime = 60
                    InsertConfigString = i.Name
                ElseIf FindString = "04" Then
                    SetRecordEveryTime = 120
                    InsertConfigString = i.Name
                ElseIf FindString = "05" Then
                    SetRecordEveryTime = 240
                    InsertConfigString = i.Name
                ElseIf FindString = "06" Then
                    SetRecordEveryTime = 1
                    InsertConfigString = i.Name
                ElseIf FindString = "07" Then
                    SetRecordEveryTime = 5
                    InsertConfigString = i.Name
                End If
                AutoUpdateDataTimer.Enabled = True

            End If
        Next
        If FindV = True Then
            AutoUpdateDataTimer.Interval = Convert.ToSingle(SetRecordEveryTime) * 60 * 1000
            TimeDataPage.GetDataTimer.Interval = Convert.ToSingle(SetRecordEveryTime) * 60 * 1000
        Else
            If WriteAutoUpLoadConfigV = False Then
                Running_Info.Text = "加载自动上传时出错，请选择一个你要自上传的时间！"
                'SetDateTimeButton.BackColor = Color.Lime
                Running_Info.ForeColor = Color.Red
                ' MsgBox("加载自动上传时出错，请选择一个你要自上传的时间！")

            Else
                Running_Info.Text = "请选择一个你要设置的值！"
                Running_Info.ForeColor = Color.Red
                ' Running_Info.BackColor = Color.Red
                ' MsgBox("请选择一个你要设置的值！")
            End If

        End If
        If WriteAutoUpLoadConfigV = True Then
            Dim FileName As String = Application.StartupPath & "/Set Record Every Time.ini"
            If System.IO.File.Exists(FileName) = True Then
                System.IO.File.Delete(FileName)
            End If
            WriteTXT(FileName, InsertConfigString)
        End If

    End Sub
    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        '报警器com 端口
        AlertComSting = ComboBox1.Text
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick, Me.MouseClick
        '收缩窗体
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
        End If
        Me.Show()
        Me.Size = New Drawing.Point(x, y)
        Me.Location = New Point((My.Computer.Screen.WorkingArea.Width - Me.Width) / 2, (My.Computer.Screen.WorkingArea.Height - Me.Height) / 2)
    End Sub

    Private Sub receivecheck_CheckedChanged(sender As Object, e As EventArgs) Handles receivecheck.CheckedChanged
        If receivecheck.Checked = True Then
            RecieveHex = True
        Else
            RecieveHex = False
        End If
    End Sub

    Private Sub ConfigIPLocate_Click(sender As Object, e As EventArgs) Handles ConfigIPLocate.Click
        IP_Match_Location.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Data_Chart_Window.Close()
        Data_Chart_Window.Show()
    End Sub

    Private Sub 最大化ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 最大化ToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub 最小化ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 最小化ToolStripMenuItem.Click
        ' Me.WindowState = FormWindowState.Minimized
        ' WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.Hide()

    End Sub

    Private Sub 退出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 退出ToolStripMenuItem.Click
        NotifyIcon1.Visible = False
        End
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Running_Info.Text = ""
        Try
            SerialPort2.Open() '打开串口
            Label3.Text = SerialPort2.IsOpen
            'statuslabel.Text = SerialPort1.IsOpen
            If SerialPort2.IsOpen = True Then
                statuslabel.Text = "串口已连接"
                statuslabel.ForeColor = Color.Green
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReadDataMACTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles ReadDataMACTextBox.SelectedValueChanged
        SetDateTimeMACTextBox.Text = ReadDataMACTextBox.Text
        SelectStatusMACTextBox.Text = ReadDataMACTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = ReadDataMACTextBox.Text
        MACTextBox.Text = ReadDataMACTextBox.Text
        SetTemperatureTargetMacTextBox.Text = ReadDataMACTextBox.Text
        SetHumidityTargetMacTextBox.Text = ReadDataMACTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = ReadDataMACTextBox.Text
        SetHumidityRightValueMacTextBox.Text = ReadDataMACTextBox.Text
    End Sub

    Private Sub SetDateTimeMACTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles SetDateTimeMACTextBox.SelectedValueChanged
        SelectStatusMACTextBox.Text = SetDateTimeMACTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = SetDateTimeMACTextBox.Text
        MACTextBox.Text = SetDateTimeMACTextBox.Text
        SetTemperatureTargetMacTextBox.Text = SetDateTimeMACTextBox.Text
        SetHumidityTargetMacTextBox.Text = SetDateTimeMACTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = SetDateTimeMACTextBox.Text
        SetHumidityRightValueMacTextBox.Text = SetDateTimeMACTextBox.Text
        ReadDataMACTextBox.Text = SetDateTimeMACTextBox.Text
    End Sub

    Private Sub SelectStatusMACTextBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectStatusMACTextBox.SelectedIndexChanged
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = SelectStatusMACTextBox.Text
        MACTextBox.Text = SelectStatusMACTextBox.Text
        SetTemperatureTargetMacTextBox.Text = SelectStatusMACTextBox.Text
        SetHumidityTargetMacTextBox.Text = SelectStatusMACTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = SelectStatusMACTextBox.Text
        SetHumidityRightValueMacTextBox.Text = SelectStatusMACTextBox.Text
        ReadDataMACTextBox.Text = SetDateTimeMACTextBox.Text
        SetDateTimeMACTextBox.Text = SelectStatusMACTextBox.Text
    End Sub

    Private Sub ClearTemperatureAndHumidityExcursionMACTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles ClearTemperatureAndHumidityExcursionMACTextBox.SelectedValueChanged
        MACTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        SetTemperatureTargetMacTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        SetHumidityTargetMacTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        SetHumidityRightValueMacTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        ReadDataMACTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        SetDateTimeMACTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
        SelectStatusMACTextBox.Text = ClearTemperatureAndHumidityExcursionMACTextBox.Text
    End Sub

    Private Sub MACTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles MACTextBox.SelectedValueChanged
        SetTemperatureTargetMacTextBox.Text = MACTextBox.Text
        SetHumidityTargetMacTextBox.Text = MACTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = MACTextBox.Text
        SetHumidityRightValueMacTextBox.Text = MACTextBox.Text
        ReadDataMACTextBox.Text = MACTextBox.Text
        SetDateTimeMACTextBox.Text = MACTextBox.Text
        SelectStatusMACTextBox.Text = MACTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = MACTextBox.Text
    End Sub

    Private Sub SetTemperatureTargetMacTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles SetTemperatureTargetMacTextBox.SelectedValueChanged
        SetHumidityTargetMacTextBox.Text = SetTemperatureTargetMacTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = SetTemperatureTargetMacTextBox.Text
        SetHumidityRightValueMacTextBox.Text = SetTemperatureTargetMacTextBox.Text
        ReadDataMACTextBox.Text = SetTemperatureTargetMacTextBox.Text
        SetDateTimeMACTextBox.Text = SetTemperatureTargetMacTextBox.Text
        SelectStatusMACTextBox.Text = SetTemperatureTargetMacTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = SetTemperatureTargetMacTextBox.Text
        MACTextBox.Text = SetTemperatureTargetMacTextBox.Text
    End Sub

    Private Sub SetHumidityTargetMacTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles SetHumidityTargetMacTextBox.SelectedValueChanged
        SetTemperatureTargetMacTextBox.Text = SetHumidityTargetMacTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = SetHumidityTargetMacTextBox.Text
        SetHumidityRightValueMacTextBox.Text = SetHumidityTargetMacTextBox.Text
        ReadDataMACTextBox.Text = SetHumidityTargetMacTextBox.Text
        SetDateTimeMACTextBox.Text = SetHumidityTargetMacTextBox.Text
        SelectStatusMACTextBox.Text = SetHumidityTargetMacTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = SetHumidityTargetMacTextBox.Text
    End Sub

    Private Sub SetTemperatureRightValueMacTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles SetTemperatureRightValueMacTextBox.SelectedValueChanged
        SetTemperatureTargetMacTextBox.Text = SetTemperatureRightValueMacTextBox.Text
        SetHumidityTargetMacTextBox.Text = SetTemperatureRightValueMacTextBox.Text
        SetHumidityRightValueMacTextBox.Text = SetTemperatureRightValueMacTextBox.Text
        ReadDataMACTextBox.Text = SetTemperatureRightValueMacTextBox.Text
        SetDateTimeMACTextBox.Text = SetTemperatureRightValueMacTextBox.Text
        SelectStatusMACTextBox.Text = SetTemperatureRightValueMacTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = SetTemperatureRightValueMacTextBox.Text
    End Sub

    Private Sub SetHumidityRightValueMacTextBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles SetHumidityRightValueMacTextBox.SelectedValueChanged
        SetTemperatureTargetMacTextBox.Text = SetHumidityRightValueMacTextBox.Text
        SetHumidityTargetMacTextBox.Text = SetHumidityRightValueMacTextBox.Text
        ReadDataMACTextBox.Text = SetHumidityRightValueMacTextBox.Text
        SetDateTimeMACTextBox.Text = SetHumidityRightValueMacTextBox.Text
        SelectStatusMACTextBox.Text = SetHumidityRightValueMacTextBox.Text
        ClearTemperatureAndHumidityExcursionMACTextBox.Text = SetHumidityRightValueMacTextBox.Text
        SetTemperatureRightValueMacTextBox.Text = SetHumidityRightValueMacTextBox.Text
    End Sub

    Private Sub 修改配置文件ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 修改配置文件ToolStripMenuItem.Click
        If File.Exists(Application.StartupPath + "\XMLconfig.ini") = True Then
            Process.Start(Application.StartupPath + "\XMLconfig.ini")
        Else
            MsgBox("Not Find Configuration File！")
        End If
    End Sub

    Private Sub 重新加载ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重新加载ToolStripMenuItem.Click
        Call ReadConfigXML()
        If TextBox3_XMLPath.Text <> "" Then
            Running_Info.Text = "Load data successful"
            Running_Info.ForeColor = Color.Lime
        Else
            Running_Info.Text = "Load data fail"
            Running_Info.ForeColor = Color.Red
        End If
    End Sub
    Private Sub DisTem_Click(sender As Object, e As EventArgs) Handles DisTem.Click
        TimeDataPage.Show()
    End Sub

    ' 2018-02-27 此按钮仅用于测试
    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles BtnTest.Click
        Alert_Window.ShowTimeout("错误信息test", 4000)
    End Sub
End Class