Imports System.IO
Imports System.Data.SqlClient
Imports System.Xml
Module Module1
    Public SaveTime As Date
    Public RunningLocation As String = ""
    Public SaveMaxRunningInt As Int32 = 0
    ' Public TableName As String = "IPlocationTest"
    Public DataSaveTableName As String = "" '温度保存表名！
    Public WorkLocationString As String = "" '软件工作的位置
    Public IPAddressAndLocationManageTableName As String = "" 'IP Address配置表名！
    Public SqlConnectString As String = "" 'Data Source=10.201.62.84;Initial Catalog=erecordcontrol;User ID=sa;Password=TryTest:123"
    Public InsertMacString As Int32 = 0
    Public SendMACListString As String = ""
    Public AlertWindowV As Boolean = False
    Public AlertComSting As String = ""
    Public ErrList As String = ""
    Public SetRecordEveryTime As String = ""
    Public SendType As Int32 = -1
    Public str2() As String
    Public i As Integer
    Public InsertData As Boolean = False
    Public Cmd As New SqlCommand
    Public MacInt As Int32 = 0
    Public SendDataGroup(254) As String
    Public RestSendInt As Int16 = 0
    Public Con As New SqlConnection
    Public InsertFlag As Int16 = 0
    Public WeeklyString As String = ""
    Public DataOutPath As String = ""
    Public strDate As String
    Public strFile As String
    Public Sub AllColse()
        '打开设备过程中，main page 中的各个对应button 都被灰掉
        Form1.ReadDateAndWeeklyButton.Enabled = False
        Form1.SetDateTimeButton.Enabled = False
        Form1.SetHumidityRightValueButton.Enabled = False
        Form1.SetHumidityTargetButton.Enabled = False
        Form1.SetTemperatureRightValueButton.Enabled = False
        Form1.SetTemperatureTargetButton.Enabled = False
        Form1.SelectStatusButton.Enabled = False
        'Form1.SetMACButton.Enabled = False
        Form1.ClearTemperatureAndHumidityExcursionButton.Enabled = False
        Form1.ReadDateButton.Enabled = False
    End Sub
    Public Sub AllOpen()
        '打开设备完成后，main page 中的各个对应button 都被打开
        Form1.ReadDateAndWeeklyButton.Enabled = True
        Form1.SetDateTimeButton.Enabled = True
        Form1.SetHumidityRightValueButton.Enabled = True
        Form1.SetHumidityTargetButton.Enabled = True
        Form1.SetTemperatureRightValueButton.Enabled = True
        Form1.SetTemperatureTargetButton.Enabled = True
        Form1.SelectStatusButton.Enabled = True
        'Form1.SetMACButton.Enabled = True
        Form1.ClearTemperatureAndHumidityExcursionButton.Enabled = True
        Form1.ReadDateButton.Enabled = True
        SendType = 0
        DataToXml()
        If ErrList <> "" Then
            'Alert_Window.Show()
            Alert_Window.ShowTimeout(ErrList, 15 * 1000) '15s之后自动关闭
        Else
            Alert_Window.Close()
        End If
    End Sub
    Public Sub DataToXml()
        Try
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Dim ds As DataSet = New DataSet
            Dim dr As SqlDataReader
            con.ConnectionString = SqlConnectString
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "Select Count(*) as a from " & DataSaveTableName & " "
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr("a") IsNot DBNull.Value Then
                    If dr("a") = 0 Then
                        dr.Close()
                        con.Close()
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    con.Close()
                    Exit Sub
                End If
            Else
                dr.Close()
                con.Close()
                Exit Sub
            End If
            dr.Close()
            Dim FileNameLastR As String = Application.StartupPath & "/The last record.ini"
            Dim StrLastR As String = ""
            Dim srLastR As StreamReader = New StreamReader(FileNameLastR)
            StrLastR = Trim(srLastR.ReadToEnd())
            Dim StartTime As Int32
            If StrLastR.Length > 0 Then
                StartTime = Val(StrLastR)
            End If
            srLastR.Close()
            If StrLastR.Length <= 0 Then
                cmd.CommandText = "select " & DataSaveTableName & ".*," & IPAddressAndLocationManageTableName & ".machineID from  " & DataSaveTableName & ", " & IPAddressAndLocationManageTableName & " where " & DataSaveTableName & ".machineno = " & IPAddressAndLocationManageTableName & ".num   order by " & DataSaveTableName & ".uploadtime desc" 'convert(varchar(34),AddTime,20)    >" & StartTime & "
            Else                                                           '.ToString("yyyy-mm-dd hh:mm:ss")
                cmd.CommandText = "select " & DataSaveTableName & ".*," & IPAddressAndLocationManageTableName & ".machineID from  " & DataSaveTableName & ", " & IPAddressAndLocationManageTableName & " where " & DataSaveTableName & ".machineno = " & IPAddressAndLocationManageTableName & ".num  AND " & DataSaveTableName & ".ID > '" & Val(StartTime) & "' order by " & DataSaveTableName & ".uploadtime desc "
            End If
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd.CommandText, con)
            adapter.Fill(ds)
            dr = cmd.ExecuteReader()
            If Form1.TextBox3_XMLPath.Text <> "" Then
                If dr.HasRows Then
                    strDate = System.DateTime.Now.ToString("yyyyMMdd HHmmss")
                    strFile = Trim(Form1.TextBox3_XMLPath.Text) + strDate + ".xml"  '以每次产生的日期为file name 进行命名
                    Dim myTW As New XmlTextWriter(strFile, Nothing)
                    myTW.WriteStartDocument()
                    myTW.Formatting = Formatting.Indented
                    myTW.WriteStartElement("flex")
                    myTW.WriteStartElement("trs")
                    myTW.WriteAttributeString("pj", "FE")
                    myTW.WriteAttributeString("station", "FE")
                    While dr.Read()
                        Dim machineID As String = ""
                        Dim temperature As Single = 0.0
                        Dim humidity As String = Nothing
                        Dim addtime As String = Nothing
                        myTW.WriteStartElement("tr")
                        If dr("machineID") IsNot DBNull.Value Then
                            machineID = dr("machineID")
                        Else
                            machineID = Nothing
                        End If
                        If dr("tem") IsNot DBNull.Value Then
                            temperature = dr("tem")
                        Else
                            temperature = 0
                        End If

                        If dr("hum") IsNot DBNull.Value Then
                            humidity = dr("hum")
                        Else
                            humidity = 0
                        End If
                        If dr("uploadtime") IsNot DBNull.Value Then
                            addtime = dr("uploadtime")
                        Else
                            addtime = 0
                        End If
                        If dr("ID") IsNot DBNull.Value Then
                            Dim FileNameLastW As String = Application.StartupPath & "/The last record.ini"
                            Dim srFileNameLastW As StreamReader = New StreamReader(FileNameLastW)
                            Dim StrFileNameLastW As String = ""
                            StrFileNameLastW = srFileNameLastW.ReadToEnd()
                            srFileNameLastW.Close()
                            If StrFileNameLastW.Length > 0 Then
                                If Val(StrFileNameLastW) < dr("ID") Then
                                    If System.IO.File.Exists(FileNameLastW) = True Then
                                        System.IO.File.Delete(FileNameLastW)
                                    End If
                                    WriteTXT(FileNameLastW, dr("ID"))
                                Else

                                End If
                            Else
                                WriteTXT(FileNameLastW, dr("ID"))
                            End If
                        End If
                        myTW.WriteElementString("machineID", machineID)
                        myTW.WriteElementString("temperature", temperature)
                        myTW.WriteElementString("humirity", humidity)
                        myTW.WriteElementString("addtime", addtime)
                        myTW.WriteEndElement()
                    End While
                    myTW.WriteEndElement()
                    myTW.WriteEndElement()
                    myTW.WriteEndDocument()
                    myTW.Close()
                    con.Close()
                    dr.Close()
                    Form1.Running_Info.Text = "Convert to xml file successfully"
                    Form1.Running_Info.ForeColor = Color.Lime
                Else
                    Form1.Running_Info.Text = "No new data to convert to xml！"
                    Form1.Running_Info.ForeColor = Color.Red
                End If
            Else
                Form1.Running_Info.Text = " '" & Form1.TextBox3_XMLPath.Text & " 'xml Path is not exist,pls enter again！"
                Form1.Running_Info.ForeColor = Color.Red
            End If
            Exit Sub
        Catch ex As Exception
            Form1.Running_Info.Text = ex.Message
            Form1.Running_Info.ForeColor = Color.Red
        End Try
    End Sub
    Public Sub ReLoadMainWindow()
        Form1.ReadDataMACTextBox.Items.Clear()
        Form1.SetDateTimeMACTextBox.Items.Clear()
        Form1.SelectStatusMACTextBox.Items.Clear()
        'Form1.SetMACTextBox.Items.Clear()
        Form1.ClearTemperatureAndHumidityExcursionMACTextBox.Items.Clear()
        Form1.MACTextBox.Items.Clear()
        Form1.SetTemperatureTargetMacTextBox.Items.Clear()
        Form1.SetTemperatureRightValueMacTextBox.Items.Clear()
        Form1.SetHumidityRightValueMacTextBox.Items.Clear()
        Form1.SetHumidityTargetMacTextBox.Items.Clear()
        '  Data_Chart_Window.AddComboBoxItme.Items.Clear()
        Data_Chart_Window.ComboBox3.Items.Clear()
        Con.ConnectionString = SqlConnectString
        Con.Open()
        Cmd.Connection = Con
        Dim sqlstr As String = "select location from " & IPAddressAndLocationManageTableName & " where isopen=1"
        Cmd.CommandText = sqlstr
        Dim dr As SqlDataReader = Cmd.ExecuteReader
        If dr.HasRows Then
            While dr.Read
                If dr("Location") IsNot DBNull.Value Then
                    Form1.ReadDataMACTextBox.Items.Add(dr("Location"))
                    Form1.SetDateTimeMACTextBox.Items.Add(dr("Location"))
                    Form1.SelectStatusMACTextBox.Items.Add(dr("Location"))
                    ' Form1.SetMACTextBox.Items.Add(dr("Location"))
                    Form1.ClearTemperatureAndHumidityExcursionMACTextBox.Items.Add(dr("Location"))
                    Form1.MACTextBox.Items.Add(dr("Location"))
                    Form1.SetTemperatureTargetMacTextBox.Items.Add(dr("Location"))
                    Form1.SetTemperatureRightValueMacTextBox.Items.Add(dr("Location"))
                    Form1.SetHumidityRightValueMacTextBox.Items.Add(dr("Location"))
                    Form1.SetHumidityTargetMacTextBox.Items.Add(dr("Location"))
                    Data_Chart_Window.ComboBox3.Items.Add(dr("location"))
                    ' Data_Chart_Window.ComboBox3.Items.Add("All")
                End If
            End While
        End If
        dr.Close()
        Con.Close()
    End Sub

    ' 解释日期时间
    Private Sub CastDateTime(ByRef InSting As String, ByRef Str2() As String, ByRef outDateTime As Date, ByRef outWeeklyString As String)
        outDateTime = New DateTime(1990, 1, 1, 1, 1, 1)
        Try
            outDateTime = CDate("20" & Str2(8) & "-" & Str2(7) & "-" & Str2(6) & " " & Str2(4) & ":" & Str2(3) & ":" & Str2(2))
        Catch ex As InvalidCastException
            Write_Exp(InSting, ex)
            Form1.Running_Info.Text = "读取到的日期数据有误！ " + vbNewLine + InSting
            Form1.Running_Info.ForeColor = Color.Red
            Return
        End Try

        If Str2(5) = 1 Then
            outWeeklyString = "星期一"
        ElseIf Str2(5) = 2 Then
            outWeeklyString = "星期二"
        ElseIf Str2(5) = 3 Then
            outWeeklyString = "星期三"
        ElseIf Str2(5) = 4 Then
            outWeeklyString = "星期四"
        ElseIf Str2(5) = 5 Then
            outWeeklyString = "星期五"
        ElseIf Str2(5) = 6 Then
            outWeeklyString = "星期六"
        ElseIf Str2(5) = 7 Then
            outWeeklyString = "星期日"
        End If
    End Sub
    ' 解释设备返回值, 写数据库
    ' 定时器触发，不更新界面
    Public Sub Temperature(ByVal InSting As String)
        Dim LocationString As String = RetrunLocation_num(Val(SendDataGroup(MacInt)))
        Try
            Dim Str2() As String = InSting.Split(" ")
            '本机解释地址
            If SendType = 0 Then
                Dim DateTime As Date
                CastDateTime(InSting, Str2, DateTime, WeeklyString)
                Form1.ReadDateTimeTextBox.Text = DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                Form1.ReadWeeklyTextBox.Text = WeeklyString
            ElseIf SendType = 100 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        ' MsgBox("设置成功！")
                        'Form1.Timer3.Enabled = False
                        Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & " Pass" & Environment.NewLine
                        ' Form1.Timer2.Interval = 10
                        ' Form1.Timer2.Enabled = True
                    Else
                        Form1.Running_Info.Text = "设置失败！"
                        Form1.Running_Info.ForeColor = Color.Red
                        'MsgBox("设置失败！")
                        ' Form1.Timer3.Enabled = False
                        Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & " Fail" & Environment.NewLine
                        '  Call Form1.Timer3_Tick(Nothing, Nothing)
                    End If
                Else
                    Form1.Running_Info.Text = "设置失败！"
                    Form1.Running_Info.ForeColor = Color.Red
                    '  MsgBox("设置失败！")
                    'Form1.Timer3.Enabled = False
                    Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & " Fail" & Environment.NewLine
                    '  Call Form1.Timer3_Tick(Nothing, Nothing)
                End If
            ElseIf SendType = 1 Or SendType = 3 Or SendType = 6 Or SendType = 9 Or SendType = 10 Or SendType = 12 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.Running_Info.Text = "设置成功！"
                        Form1.Running_Info.ForeColor = Color.Linen
                        ' MsgBox("设置成功！")
                    Else
                        Form1.Running_Info.Text = "设置失败！"
                        Form1.Running_Info.ForeColor = Color.Red
                        ' MsgBox("设置失败！")
                    End If
                Else
                    Form1.Running_Info.Text = "设置失败！"
                    Form1.Running_Info.ForeColor = Color.Red
                    ' MsgBox("设置失败！")
                End If
            ElseIf SendType = 2 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.StatusTextBox.Text = "该机器工作正常！"
                        Form1.StatusTextBox.BackColor = Color.Lime
                    Else
                        Form1.StatusTextBox.Text = "该机器工作异常！"
                        Form1.StatusTextBox.BackColor = Color.Red
                    End If
                Else
                    Form1.StatusTextBox.Text = "该机器工作异常！"
                    Form1.StatusTextBox.BackColor = Color.Red
                End If
            ElseIf SendType = 4 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.Running_Info.Text = "清除温度和湿度的偏移量成功!"
                        Form1.Running_Info.ForeColor = Color.Red
                    Else
                        Form1.Running_Info.Text = "清除温度和湿度的偏移量失败!"
                        Form1.Running_Info.ForeColor = Color.Red
                        ' MsgBox("清除温度和湿度的偏移量失败！")
                    End If
                Else
                    Form1.Running_Info.Text = "清除温度和湿度的偏移量失败!"
                    Form1.Running_Info.ForeColor = Color.Red
                    '   MsgBox("清除温度和湿度的偏移量失败！")
                End If
            ElseIf SendType = 5 Then
                ' System.Threading.Thread.Sleep(5000)
                If Val(SendDataGroup(MacInt)) = 5 Then
                    Dim p As Int16 = 0
                End If
                If Str2.Length <= 24 Then
                    If SendType = 5 And RunningLocation = "自动上传" Then
                        If ErrList = "" Then
                            ErrList = MacInt & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "发送地址长度不正确！" & vbCr
                        Else
                            ErrList = ErrList & MacInt & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "发送地址长度不正确！" & vbCr
                        End If
                        ' ElseIf SendType = 5 And Form1.InsertDataCheckBox.Checked = True Then
                        '    Form1.Running_Info.Text = "你选择的设备工作异常!"
                        '    Form1.Running_Info.ForeColor = Color.Red
                    End If
                End If
                ' Form1.Timer3.Enabled = False
                Dim DateTime As Date
                CastDateTime(InSting, Str2, DateTime, WeeklyString)

                '从设备中获取数据填写到信息表中
                If InsertData = True Then
                    Dim con1 As New SqlConnection
                    Dim cmd1 As New SqlCommand
                    con1.ConnectionString = SqlConnectString
                    con1.Open()
                    cmd1.Connection = con1
                    Dim tem As Single = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Dim Humi As Single = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Dim TemLow As Single = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                    Dim Temhigh As Single = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                    Dim HumiLow As Single = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                    Dim HumiHigh As Single = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                    Dim dr As SqlDataReader = Nothing
                    If Form1.InsertDataCheckBox.Checked = True Then
                        Form1.ReadDataDateTimeTextBox.Text = DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                        Form1.ReadDataWeekTextBox.Text = WeeklyString
                        Form1.ReadDataTempValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                        Form1.SetTemperatureRightValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                        Form1.ReadDataHumiValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                        Form1.SetHumidityRightValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                        Form1.ReadDataTempDeltaValueTextBox.Text = Val("&H" & Str2(13)) / 10
                        Form1.ReadDataHumiDeltaValueTextBox.Text = Val("&H" & Str2(14)) / 10
                        Form1.ReadDataTempLowValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                        Form1.SetTemLValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                        Form1.ReadDataTempHighValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                        Form1.SetTemHValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                        Form1.ReadDataHumiLowValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                        Form1.SetHumiLValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                        Form1.ReadDataHumiHighValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                        Form1.SetHumiHValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                    End If
                    cmd1.CommandText = "Select * From " & DataSaveTableName & " Where Dates='" & DateTime.Date.ToString("yyyy-MM-dd") & "' and detailtime='" & DateTime.ToString("HH:mm") & "' and machineno ='" & Val(SendDataGroup(MacInt)) & "'"
                    dr = cmd1.ExecuteReader
                    If dr.Read = False Then
                        dr.Close()
                        cmd1.CommandText = "Insert Into " & DataSaveTableName & " (dates,detailtime,tem,hum,machineno,uploadtime,assettype,temperaturelow,temperaturehigh,Humiditylow,Humidityhigh) Values ('" & DateTime.Date.ToString("yyyy-MM-dd") & "','" & DateTime.ToString("HH:mm") & "' , '" & tem & "','" & Humi & "' ,'" & Val(SendDataGroup(MacInt)) & "', GETDATE (),'0','" & TemLow & "','" & Temhigh & "','" & HumiLow & "','" & HumiHigh & "')"
                        cmd1.ExecuteNonQuery()
                        InsertFlag = 1
                        con1.Close()
                        If InsertFlag = 1 Then
                            Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & Now.ToString("yyyy-MM-dd HH:mm:ss:ms") & vbTab & ((Now.ToFileTime - SaveTime.ToFileTime) / 10000).ToString("0") & vbTab & Val(SendDataGroup(MacInt)) & " Insert OK" & Environment.NewLine
                            Form1.TextBox2.Refresh()
                        Else '判断插入数据库失败时候报警

                            If SendType = 5 And RunningLocation = "自动上传" Then
                                If ErrList = "" Then
                                    ErrList = LocationString & vbTab & Now & vbTab & "写入数据库失败！" & vbCrLf
                                Else
                                    ErrList = ErrList & LocationString & vbTab & Now & vbTab & "写入数据库失败！" & vbCrLf

                                End If
                                ' ElseIf SendType = 5 And Form1.InsertDataCheckBox.Checked = True Then
                                '   Form1.Running_Info.Text = "你选择的设备工作异常!"
                                '     Form1.Running_Info.ForeColor = Color.Red
                            End If
                        End If
                        If MacInt >= SendDataGroup.Length - 2 Then
                            AllOpen()

                        End If
                    Else
                        dr.Close()
                        con1.Close()
                        Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & Now.ToString("yyyy-MM-dd HH:mm:ss:ms") & vbTab & ((Now.ToFileTime - SaveTime.ToFileTime) / 10000).ToString("0") & vbTab & Val(SendDataGroup(MacInt)) & " 数据重复" & Environment.NewLine
                        Form1.TextBox2.Refresh()
                        If MacInt >= SendDataGroup.Length - 2 Then
                            AllOpen()

                        End If
                    End If
                Else
                    Form1.ReadDataDateTimeTextBox.Text = DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                    Form1.ReadDataWeekTextBox.Text = WeeklyString
                    Form1.ReadDataTempValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Form1.SetTemperatureRightValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Form1.ReadDataHumiValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Form1.SetHumidityRightValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Form1.ReadDataTempDeltaValueTextBox.Text = Val("&H" & Str2(13)) / 10
                    Form1.ReadDataHumiDeltaValueTextBox.Text = Val("&H" & Str2(14)) / 10
                    Form1.ReadDataTempLowValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                    Form1.SetTemLValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                    Form1.ReadDataTempHighValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                    Form1.SetTemHValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                    Form1.ReadDataHumiLowValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                    Form1.SetHumiLValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                    Form1.ReadDataHumiHighValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                    Form1.SetHumiHValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                End If
            ElseIf SendType = 7 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.Running_Info.Text = "设置成功！"
                        Form1.Running_Info.ForeColor = Color.Linen
                        ' MsgBox("设置成功！")
                    Else
                        Form1.Running_Info.Text = "设置失败！"
                        Form1.Running_Info.ForeColor = Color.Red
                        'MsgBox("设置失败！")
                    End If
                Else
                    Form1.Running_Info.Text = "设置失败！"
                    Form1.Running_Info.ForeColor = Color.Red
                    ' MsgBox("设置失败！")
                End If
            ElseIf SendType = 8 Then
                Dim BaseYear As Int32 = Val("&H" & Str2(2))
                Dim DataLongInt As Int64 = Str2(5) * 256 + Str2(4)
                If Str2.Length < 7 Then
                    Form1.Running_Info.Text = "数据接收错误！"
                    Form1.Running_Info.ForeColor = Color.Red
                    '  MsgBox("数据接收错误！")
                Else
                    If Str2.Length <> DataLongInt + 7 Then
                    Else
                        For s = 7 To (Str2.Length - 1) Step 8

                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Write_Exp(InSting, ex)
            MsgBox(ex.Message, "in Temperature()")
        End Try
    End Sub

    ' 解释设备返回值, 写数据库
    ' 手动触发, 更新界面
    Public Sub CheckInsertTemperature(ByVal InSting As String)
        Dim LocationString As String = RetrunLocation_num(Val(SendDataGroup(MacInt)))
        Try
            Dim Str2() As String = InSting.Split(" ")
            '本机解释地址
            If SendType = 0 Then
                Dim DateTime As Date
                CastDateTime(InSting, Str2, DateTime, WeeklyString)
                Form1.ReadDateTimeTextBox.Text = DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                Form1.ReadWeeklyTextBox.Text = WeeklyString
            ElseIf SendType = 100 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        ' MsgBox("设置成功！")
                        ' Form1.Timer3.Enabled = False
                        Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & " Pass" & Environment.NewLine
                        '  Form1.Timer2.Interval = 10
                        '  Form1.Timer2.Enabled = True
                    Else
                        Form1.Running_Info.Text = "设置失败！"
                        Form1.Running_Info.ForeColor = Color.Red
                        'MsgBox("设置失败！")
                        ' Form1.Timer3.Enabled = False
                        Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & " Fail" & Environment.NewLine
                        ' Call Form1.Timer3_Tick(Nothing, Nothing)
                    End If
                Else
                    Form1.Running_Info.Text = "设置失败！"
                    Form1.Running_Info.ForeColor = Color.Red
                    '  MsgBox("设置失败！")
                    '  Form1.Timer3.Enabled = False
                    Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & " Fail" & Environment.NewLine
                    'Call Form1.Timer3_Tick(Nothing, Nothing)
                End If
            ElseIf SendType = 1 Or SendType = 3 Or SendType = 6 Or SendType = 9 Or SendType = 10 Or SendType = 12 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.Running_Info.Text = "设置成功！"
                        Form1.Running_Info.ForeColor = Color.Linen
                        ' MsgBox("设置成功！")
                    Else
                        Form1.Running_Info.Text = "设置失败！"
                        Form1.Running_Info.ForeColor = Color.Red
                        ' MsgBox("设置失败！")
                    End If
                Else
                    Form1.Running_Info.Text = "设置失败！"
                    Form1.Running_Info.ForeColor = Color.Red
                    ' MsgBox("设置失败！")
                End If
            ElseIf SendType = 2 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.StatusTextBox.Text = "该机器工作正常！"
                        Form1.StatusTextBox.BackColor = Color.Lime
                    Else
                        Form1.StatusTextBox.Text = "该机器工作异常！"
                        Form1.StatusTextBox.BackColor = Color.Red
                    End If
                Else
                    Form1.StatusTextBox.Text = "该机器工作异常！"
                    Form1.StatusTextBox.BackColor = Color.Red
                End If
            ElseIf SendType = 4 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.Running_Info.Text = "清除温度和湿度的偏移量成功!"
                        Form1.Running_Info.ForeColor = Color.Red
                    Else
                        Form1.Running_Info.Text = "清除温度和湿度的偏移量失败!"
                        Form1.Running_Info.ForeColor = Color.Red
                        ' MsgBox("清除温度和湿度的偏移量失败！")
                    End If
                Else
                    Form1.Running_Info.Text = "清除温度和湿度的偏移量失败!"
                    Form1.Running_Info.ForeColor = Color.Red
                    '   MsgBox("清除温度和湿度的偏移量失败！")
                End If
            ElseIf SendType = 5 Then
                ' System.Threading.Thread.Sleep(5000)
                If Val(SendDataGroup(MacInt)) = 5 Then
                    Dim p As Int16 = 0
                End If
                If Str2.Length <= 24 Then
                    If SendType = 5 And RunningLocation = "自动上传" Then
                        If ErrList = "" Then
                            ErrList = MacInt & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "发送地址长度不正确！" & vbCr
                        Else
                            ErrList = ErrList & MacInt & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "发送地址长度不正确！" & vbCr
                        End If
                        ' ElseIf SendType = 5 And Form1.InsertDataCheckBox.Checked = True Then
                        '    Form1.Running_Info.Text = "你选择的设备工作异常!"
                        '    Form1.Running_Info.ForeColor = Color.Red
                    End If
                End If
                'Form1.Timer3.Enabled = False
                Dim DateTime As Date
                Dim WeeklyString As String = ""
                CastDateTime(InSting, Str2, DateTime, WeeklyString)

                '从设备中获取数据填写到信息表中
                If Form1.insertcheckboxdata = 1 Then
                    Dim con1 As New SqlConnection
                    Dim cmd1 As New SqlCommand
                    con1.ConnectionString = SqlConnectString
                    con1.Open()
                    cmd1.Connection = con1
                    Dim tem As Single = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Dim Humi As Single = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Dim TemLow As Single = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                    Dim Temhigh As Single = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                    Dim HumiLow As Single = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                    Dim HumiHigh As Single = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                    Dim dr As SqlDataReader = Nothing
                    If Form1.InsertDataCheckBox.Checked = True Then
                        Form1.ReadDataDateTimeTextBox.Text = DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                        Form1.ReadDataWeekTextBox.Text = WeeklyString
                        Form1.ReadDataTempValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                        Form1.SetTemperatureRightValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                        Form1.ReadDataHumiValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                        Form1.SetHumidityRightValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                        Form1.ReadDataTempDeltaValueTextBox.Text = Val("&H" & Str2(13)) / 10
                        Form1.ReadDataHumiDeltaValueTextBox.Text = Val("&H" & Str2(14)) / 10
                        Form1.ReadDataTempLowValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                        Form1.SetTemLValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                        Form1.ReadDataTempLowValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                        Form1.ReadDataTempHighValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                        Form1.SetTemHValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                        Form1.ReadDataHumiLowValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                        Form1.SetHumiLValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                        Form1.ReadDataHumiHighValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                        Form1.SetHumiHValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                    End If
                    cmd1.CommandText = "Select * From " & DataSaveTableName & " Where Dates='" & DateTime.Date.ToString("yyyy-MM-dd") & "' and detailtime='" & DateTime.ToString("HH:mm") & "' and machineno ='" & Val(SendDataGroup(MacInt)) & "'"
                    dr = cmd1.ExecuteReader
                    If dr.Read = False Then
                        dr.Close()
                        cmd1.CommandText = "Insert Into " & DataSaveTableName & " (dates,detailtime,tem,hum,machineno,uploadtime,assettype,temperaturelow,temperaturehigh,Humiditylow,Humidityhigh) Values ('" & DateTime.Date.ToString("yyyy-MM-dd") & "','" & DateTime.ToString("HH:mm") & "' , '" & tem & "','" & Humi & "' ,'" & Val(SendDataGroup(MacInt)) & "', GETDATE (),'0','" & TemLow & "','" & Temhigh & "','" & HumiLow & "','" & HumiHigh & "')"
                        cmd1.ExecuteNonQuery()
                        InsertFlag = 1
                        con1.Close()
                        If InsertFlag = 1 Then
                            Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & Now.ToString("yyyy-MM-dd HH:mm:ss:ms") & vbTab & ((Now.ToFileTime - SaveTime.ToFileTime) / 10000).ToString("0") & vbTab & Val(SendDataGroup(MacInt)) & " Insert OK" & Environment.NewLine
                            Form1.TextBox2.Refresh()
                        Else '判断插入数据库失败时候报警

                            If SendType = 5 And RunningLocation = "自动上传" Then
                                If ErrList = "" Then
                                    ErrList = LocationString & vbTab & Now & vbTab & "写入数据库失败！" & vbCrLf
                                Else
                                    ErrList = ErrList & LocationString & vbTab & Now & vbTab & "写入数据库失败！" & vbCrLf

                                End If
                                ' ElseIf SendType = 5 And Form1.InsertDataCheckBox.Checked = True Then
                                '   Form1.Running_Info.Text = "你选择的设备工作异常!"
                                '     Form1.Running_Info.ForeColor = Color.Red
                            End If
                        End If
                        If MacInt >= SendDataGroup.Length - 2 Then
                            AllOpen()

                        End If
                    Else
                        dr.Close()
                        con1.Close()
                        Form1.TextBox2.Text &= Val(SendDataGroup(MacInt)) & Now.ToString("yyyy-MM-dd HH:mm:ss:ms") & vbTab & ((Now.ToFileTime - SaveTime.ToFileTime) / 10000).ToString("0") & vbTab & Val(SendDataGroup(MacInt)) & " 数据重复" & Environment.NewLine
                        Form1.TextBox2.Refresh()
                        If MacInt >= SendDataGroup.Length - 2 Then
                            AllOpen()

                        End If
                    End If
                Else
                    Form1.ReadDataDateTimeTextBox.Text = DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                    Form1.ReadDataWeekTextBox.Text = WeeklyString
                    Form1.ReadDataTempValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Form1.SetTemperatureRightValueTextBox.Text = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Form1.ReadDataHumiValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Form1.SetHumidityRightValueTextBox.Text = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Form1.ReadDataTempDeltaValueTextBox.Text = Val("&H" & Str2(13)) / 10
                    Form1.ReadDataHumiDeltaValueTextBox.Text = Val("&H" & Str2(14)) / 10
                    Form1.ReadDataTempLowValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                    Form1.SetTemLValueTextBox.Text = (Val("&H" & Str2(16)) * 256 + Val("&H" & Str2(15)) - 520) / 10
                    Form1.ReadDataTempHighValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                    Form1.SetTemHValueTextBox.Text = (Val("&H" & Str2(18)) * 256 + Val("&H" & Str2(17)) - 520) / 10
                    Form1.ReadDataHumiLowValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                    Form1.SetHumiLValueTextBox.Text = (Val("&H" & Str2(20)) * 256 + Val("&H" & Str2(19)) - 100) / 10
                    Form1.ReadDataHumiHighValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                    Form1.SetHumiHValueTextBox.Text = (Val("&H" & Str2(22)) * 256 + Val("&H" & Str2(21)) - 100) / 10
                End If
            ElseIf SendType = 7 Then
                If Str2.Length >= 3 Then
                    If Str2(0) = "44" And Str2(1) = "99" And Str2(2) = "CC" Then
                        Form1.Running_Info.Text = "设置成功！"
                        Form1.Running_Info.ForeColor = Color.Linen
                        ' MsgBox("设置成功！")
                    Else
                        Form1.Running_Info.Text = "设置失败！"
                        Form1.Running_Info.ForeColor = Color.Red
                        'MsgBox("设置失败！")
                    End If
                Else
                    Form1.Running_Info.Text = "设置失败！"
                    Form1.Running_Info.ForeColor = Color.Red
                    ' MsgBox("设置失败！")
                End If
            ElseIf SendType = 8 Then
                Dim BaseYear As Int32 = Val("&H" & Str2(2))
                Dim DataLongInt As Int64 = Str2(5) * 256 + Str2(4)
                If Str2.Length < 7 Then
                    Form1.Running_Info.Text = "数据接收错误！"
                    Form1.Running_Info.ForeColor = Color.Red
                    '  MsgBox("数据接收错误！")
                Else
                    If Str2.Length <> DataLongInt + 7 Then
                    Else
                        For s = 7 To (Str2.Length - 1) Step 8

                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Write_Exp(InSting, ex)
            MsgBox(ex.Message, "in CheckInsertTemperature()")
        End Try
    End Sub

    '通过num 去获取location，因为之前是通过配置文件去启动，这里是为了方便配合之前的代码
    Public Function RetrunLocation_num(ByVal num As String) As String
        Dim LocationString As String = ""
        Dim Con As New SqlClient.SqlConnection
        Con.ConnectionString = SqlConnectString
        Dim Cmd As New SqlClient.SqlCommand
        Con.Open()
        Cmd.Connection = Con
        Dim dr As SqlDataReader = Nothing
        Cmd.CommandText = "Select location  From " & IPAddressAndLocationManageTableName & " Where num='" & num & "' and isopen=1"
        dr = Cmd.ExecuteReader
        If dr.Read Then
            If dr("location") IsNot DBNull.Value Then
                LocationString = dr("location")
            End If
        End If
        dr.Close()
        Con.Close()
        Return LocationString
    End Function
    Public Function RetrunNum_Location(ByVal Location As String) As String
        ' Dim NumString As String = ""
        Dim Con As New SqlClient.SqlConnection
        Con.ConnectionString = SqlConnectString
        Dim Cmd As New SqlClient.SqlCommand
        Con.Open()
        Cmd.Connection = Con
        Dim dr As SqlDataReader = Nothing
        Cmd.CommandText = "Select num From " & IPAddressAndLocationManageTableName & " Where location='" & Location & "'and isopen=1"
        dr = Cmd.ExecuteReader
        If dr.Read Then

            If dr("num") IsNot DBNull.Value Then
                InsertMacString = dr("num")
            End If
        End If
        dr.Close()
        Con.Close()
        Return InsertMacString
    End Function
    Public Function RetrunAddress_num(ByVal num As String) As Tuple(Of String, Int32)
        Dim AddressString As String = ""
        Dim Port As Int32 = 5300
        Dim Con As New SqlClient.SqlConnection
        Con.ConnectionString = SqlConnectString
        Dim Cmd As New SqlClient.SqlCommand
        Con.Open()
        Cmd.Connection = Con
        Dim dr As SqlDataReader = Nothing
        Cmd.CommandText = "Select IPaddress,Port  From " & IPAddressAndLocationManageTableName & " Where num='" & num & "' and isopen=1"
        dr = Cmd.ExecuteReader
        If dr.Read Then
            If dr("IPaddress") IsNot DBNull.Value Then
                AddressString = dr("IPaddress")
            End If
            If dr("Port") IsNot DBNull.Value Then
                Port = dr("Port")
            End If
        End If
        dr.Close()
        Con.Close()
        Dim ret As Tuple(Of String, Int32) = New Tuple(Of String, Int32)(AddressString, Port)
        Return ret
    End Function
    '通过location 去获取IP，在主页面的button中调用
    Public Function RetrunAddress_Location(ByVal Location As String) As Tuple(Of String, Int32)
        Dim AddressString As String = ""
        Dim Port As Int32 = 5300
        Dim Con As New SqlClient.SqlConnection
        Con.ConnectionString = SqlConnectString
        Dim Cmd As New SqlClient.SqlCommand
        Con.Open()
        Cmd.Connection = Con
        Dim dr As SqlDataReader = Nothing
        Cmd.CommandText = "Select IPaddress,Port,num From " & IPAddressAndLocationManageTableName & " Where location='" & Location & "'and isopen=1"
        dr = Cmd.ExecuteReader
        If dr.Read Then
            If dr("IPaddress") IsNot DBNull.Value Then
                AddressString = dr("IPaddress")
            End If
            If dr("Port") IsNot DBNull.Value Then
                Port = dr("Port")
            End If
            If dr("num") IsNot DBNull.Value Then
                InsertMacString = dr("num")
            End If
        End If
        dr.Close()
        Con.Close()

        Dim ret As Tuple(Of String, Int32) = New Tuple(Of String, Integer)(AddressString, Port)
        Return ret
    End Function

    ' 读取XMLconfig.ini
    ' 读取到设备数值时可以输出到XML文件，这里可以配置这些文件的输出路径
    Public Sub ReadConfigXML()
        Try
            Dim Str As String = ""
            Dim sr As StreamReader = New StreamReader(Application.StartupPath + "\XMLconfig.ini")
            Str = sr.ReadToEnd()
            sr.Close()
            Dim RowsString() As String = Str.Split(vbLf)
            If RowsString.Length > 1 Then
                For i As Int32 = 0 To RowsString.Length - 1
                    If Mid(Trim(RowsString(i)), 1, 1) = "*" Then
                        Continue For
                    End If
                    Dim FileString() As String = RowsString(i).Split("#")
                    If FileString.Length > 1 Then
                        If FileString(0) = "Data Out Path" Then
                            DataOutPath = FileString(1)
                            If Directory.Exists(DataOutPath) Then
                                Form1.Running_Info.Text = "XML Path is existing"
                            Else
                                Directory.CreateDirectory(DataOutPath)
                            End If
                            If System.IO.Directory.Exists(DataOutPath) = False Then  '判断文件是否存在或者是否合法
                                Form1.Running_Info.Text = "XML Path is not existing,pls enter again"
                                Form1.Running_Info.ForeColor = Color.Red
                                Form1.TextBox3_XMLPath.Text = ""
                            Else
                                If DataOutPath.Last = "/" Then
                                    'Dim PathOutString As String = Nothing
                                    DataOutPath = Replace(DataOutPath, "/", "\")
                                End If
                                If DataOutPath.Last <> "\" Then
                                    DataOutPath = DataOutPath & "\"
                                End If
                                Form1.TextBox3_XMLPath.Text = DataOutPath

                            End If
                            Form1.TextBox3_XMLPath.Text = DataOutPath

                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Form1.Running_Info.Text = Now & vbTab & ex.Message & vbCr
            Form1.Running_Info.ForeColor = Color.Red
        End Try
    End Sub
End Module
