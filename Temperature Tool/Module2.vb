Imports Microsoft.Office.Interop
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.IO


'网络控制器命名空间.
Imports System.Threading
Imports System.Linq
Imports System.Text
Imports System.Net
Imports System.Net.Sockets
Imports System.Collections
Imports System.Drawing.Drawing2D

''' <summary>
'''
''' </summary>
''' <remarks></remarks>
Module Module2
    ''' <summary>
    ''' 网络控制器公用变量!
    ''' </summary>
    ''' <remarks></remarks>
    Public allDone As ManualResetEvent = New ManualResetEvent(False)
    Public udpClient As UdpClient = Nothing
    Public device_id As Int32 = 64
    Public _ipaddress As IPAddress = Nothing
    '<截图变量!
    Public m_x, m_y As Int16
    Public F_x, F_y As Int16
    Public bit2 As Bitmap
    Public sizes As Size
    Public BMP(200) As Bitmap
    Public sBMP As Bitmap
    Public bm As Integer = 0
    Public tm As Integer = 0
    Public bmsize As Size
    Public BmWhere As String = "" '为区分是交接与信息截图时所用.
    '网络控件器模块!
    Public Function TurnOnLed(ByVal num As Integer, ByVal Number As Int16, ByVal ip() As Byte, ByVal localPort As Integer, ByVal remotehostport As Integer) As Boolean
        _ipaddress = New IPAddress(ip)
        udpClient = New UdpClient(localPort)
        udpClient.Connect(_ipaddress, remotehostport)
        Dim num_dev As Byte = Convert.ToByte(Math.Truncate(num / 16))
        Dim device As Byte = Convert.ToByte(device_id + num_dev * 2)
        Dim port_Io As Byte = Convert.ToByte(Math.Truncate((num Mod 16) / 8) + 48)
        Dim pin As Byte = Convert.ToByte((num Mod 16) Mod 8 + 48)
        Dim crc As Byte = Number + device + port_Io + pin + 1
        Dim crc_1 As Byte = Convert.ToByte(crc And 255)
        Dim bcmd() As Byte = {Number, device, port_Io, pin, crc_1}
        Dim Remoteipendpoint As IPEndPoint = New IPEndPoint(_ipaddress, 0)
        Dim count As Int16 = 0
        For Each b As Byte In bcmd
            Thread.Sleep(50)
            Dim sendByte() As Byte = {b}
            udpClient.Send(sendByte, sendByte.Length)
            Dim Bytes() As Byte = udpClient.Receive(Remoteipendpoint)
            If count < 4 Then
                If Bytes(0) = bcmd(count) Then
                    count = count + 1
                    Continue For
                Else
                    Return False
                End If
            End If
        Next
        udpClient.Close()
        Return True
    End Function


    Public Sub SendMessage(ByVal IP As String, ByVal SendMsg As String)
        Try
            If IP <> "" Then
                Dim tcpc As New System.Net.Sockets.TcpClient(IP, 5656)
                Dim tcpStream As Net.Sockets.NetworkStream = tcpc.GetStream
                Dim reqStream As New IO.StreamWriter(tcpStream)
                reqStream.Write(SendMsg)
                reqStream.Flush()
                tcpStream.Close()
                tcpc.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    ''' <summary> 在某个table中，获取某个Table 的字段
    ''' </summary>    
    ''' <param name="DBEqualValue">表示对应的对等的内容 </param>
    ''' <param name="DBString">表示要获取的字段名称 </param>
    ''' <param name="DBTable">表示要获取的表名称 </param>
    '''  <param name="DBEqualString">表示要条件对等的字段名称 </param>    ‘num
    ''' <varsion>  1.0.0.0
    ''' </varsion>
    Public Function RetrunDataValue(ByVal DBString As String, ByVal DBTable As String, ByVal DBEqualString As String, ByVal DBEqualValue As String)
        Dim ReDBValueName As New ComboBox '主要用来存放fixture 的control name'
        Try
            Dim connStr As String = SqlConnectString
            Dim conn As New SqlConnection(connStr)
            Dim Cmd As New SqlClient.SqlCommand
            Cmd.Connection = conn
            conn.Open()
            Dim sSQL As String = "select  b." & DBString & " as a from " & IPAddressAndLocationManageTableName & " b where b." & DBEqualString & "='" & DBEqualValue & "' "
            Dim da As New SqlDataAdapter(sSQL, conn)
            Dim ds As New DataSet
            da.Fill(ds)
            ReDBValueName.Items.Clear()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                ReDBValueName.Items.Add(ds.Tables(0).Rows(i).Item("a").ToString)
            Next
            conn.Close()
        Catch ex As Exception
            MsgBox("ERROR : " & ex.Message.ToString)
        End Try
        Return ReDBValueName
    End Function
    ''' <summary> 计算正在工作的设备个数
    ''' </summary>    
    ''' <varsion>  1.0.0.0
    ''' </varsion>
    Public Function RetrunDataValue()
        Dim CountActive As Int16
        Try
            Dim connStr As String = SqlConnectString
            Dim conn As New SqlConnection(connStr)
            Dim Cmd As New SqlClient.SqlCommand
            Cmd.Connection = conn
            conn.Open()
            Dim sSQL As String = "select count(*) as a from " & IPAddressAndLocationManageTableName & " where isopen=1"
            Dim dr As SqlClient.SqlDataReader
            Cmd.CommandText = sSQL
            dr = Cmd.ExecuteReader
            If dr.Read Then
                If dr("a") IsNot DBNull.Value Then
                    CountActive = dr("a")
                End If
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox("ERROR : " & ex.Message.ToString)
        End Try
        Return CountActive
    End Function

    ''' <summary>
    ''' 以上是网络控制器模快!
    ''' </summary>
    ''' <remarks></remarks>
    Public Function VerifyLog(ByVal FilePath As String) As String
        If FileIO.FileSystem.FileExists(FilePath) = True Then
            Dim FindText As String = My.Computer.FileSystem.ReadAllText(FilePath, System.Text.UnicodeEncoding.ASCII)
            ' TextBox1.Text = FindText
            Dim c = FindText.Split(vbLf)
            Dim FailCode As String = ""
            For i As Int32 = 0 = 0 To c.Length - 1
                If i = 0 Then
                    If c(0) = "" Then
                        Return False & vbTab & i & vbTab & "第一行为空！"
                    Else
                        Dim FirstText() As String = c(i).Split(" ")
                        If FirstText.Length > 1 Then
                            Return False & vbTab & i & vbTab & "第一行内容中有空格，或ＰＮ错误！"
                        End If
                        FirstText = c(i).Split("|")
                        If FirstText.Length <> 15 Then
                            Return False & vbTab & i & vbTab & "第一内容没有输入完整！"
                        Else
                            If FirstText(0) <> "{@BATCH" Then
                                Return False & vbTab & i & vbTab & "第一行头文件错误！"
                            End If
                        End If
                    End If
                ElseIf i = 1 Then
                    If c(i) = "" Then
                        Return False & vbTab & i & vbTab & "第二行为空！"
                    Else
                        Dim FirstText() As String = c(i).Split(" ")
                        If FirstText.Length > 1 Then
                            Return False & vbTab & i & vbTab & "第二行中有空格！"
                        End If
                        FirstText = c(i).Split("|")
                        If FirstText.Length < 13 Then
                            Return False & vbTab & i & vbTab & "第二内容没有输入完整！"
                        Else
                            If FirstText(0) <> "{@BTEST" Then
                                Return False & vbTab & i & vbTab & "第二行头文件错误！"
                            End If
                            FailCode = Val(FirstText(2))
                            If Val(FirstText(2)) = 89 Then
                                Return False & vbTab & i & vbTab & "Fail Code错误！"
                            End If
                            If Val(FirstText(4)) < 1 Then
                                Return False & vbTab & i & vbTab & "测试所用时间错误！"
                            End If

                        End If
                    End If
                Else
                    If FailCode = 0 Then
                        If c(i) <> "" Then
                            Dim d() As String = c(i).Split("|")
                            If d(0) = "{@ECID" Then
                                If Val(d(1)) <> 0 Then
                                    Return False & vbTab & i & vbTab & "ECID Code错误！"
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Else
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 以下为公共部分!在此之上部分，为本工程专有！
    ''' </summary>
    ''' <remarks></remarks>

    Public ConnectStr As String = ""
    Public PublicCon As New SqlConnection
    Public PublicCmd As New SqlCommand
    Public PublicDr As SqlDataReader = Nothing

    Public ConnectStr1 As String = ""
    Public PublicCon1 As New SqlConnection
    Public PublicCmd1 As New SqlCommand
    Public PublicDr1 As SqlDataReader = Nothing

    Public Function SqlSelect1(ByVal Str As String) As SqlClient.SqlDataReader
        If PublicDr1 IsNot Nothing Then
            PublicDr1.Close()
            PublicCon1.Close()
        Else
            PublicCon1.Close()
        End If
        If PublicCon1.State = ConnectionState.Closed Then
            PublicCon1.ConnectionString = ConnectStr1
            PublicCon1.Open()
        End If
        PublicCmd1.Connection = PublicCon1
        If PublicDr1 IsNot Nothing Then
            PublicDr1.Close()
        End If
        PublicCmd1.CommandText = Str
        PublicDr1 = PublicCmd1.ExecuteReader
        Return PublicDr1
    End Function

    Public Sub SqlDelete1(ByVal Str As String)
        If PublicDr1 IsNot Nothing Then
            PublicDr1.Close()
            PublicCon1.Close()
        Else
            PublicCon1.Close()
        End If
        If PublicCon1.State = ConnectionState.Closed Then
            PublicCon1.ConnectionString = ConnectStr1
            PublicCon1.Open()
        End If
        PublicCmd1.Connection = PublicCon1
        ' "DELETE FROM UserID WHERE (UserID = '" & DelUserID & "')"
        PublicCmd1.CommandText = Str
        PublicCmd1.ExecuteReader()
        PublicCon1.Close()
    End Sub
    Public Sub SqlUPDATE1(ByVal Str As String)
        If PublicDr1 IsNot Nothing Then
            PublicDr1.Close()
            PublicCon1.Close()
        Else
            PublicCon1.Close()
        End If
        If PublicCon1.State = ConnectionState.Closed Then
            PublicCon1.ConnectionString = ConnectStr1
            PublicCon1.Open()
        End If
        PublicCmd1.Connection = PublicCon1
        '  "UPDATE UserID SET [PassWord] = '" & UpDataPassWword & "'WHERE (UserID = '" & UpDataUserID & "')"
        PublicCmd1.CommandText = Str
        PublicCmd1.ExecuteReader()
        PublicCon1.Close()
    End Sub
    Public Sub SqlInsert1(ByVal Str As String)
        If PublicDr1 IsNot Nothing Then
            PublicDr1.Close()
            PublicCon1.Close()
        Else
            PublicCon1.Close()
        End If
        If PublicCon1.State = ConnectionState.Closed Then
            PublicCon1.ConnectionString = ConnectStr1
            PublicCon1.Open()
        End If
        PublicCmd1.Connection = PublicCon1
        '  "INSERT INTO UserID VALUES ('" & AddUserID & "','" & CheseName & "', '" & AddPassWword & "', '" & Limit & "', '" & Employee & "' , '" & Shift & "', '" & Station & "', '','','','','','','','','','','','" & Now.AddDays(-100).Date.ToString("yyyy-MM-dd") & "','" & BUStr & "')"
        PublicCmd1.CommandText = Str
        PublicCmd1.ExecuteReader()
        PublicCon1.Close()
    End Sub
    Public Function RetrunTimeRange(ByVal StartTime As Date, ByVal EndTime As Date) As String

        Dim TimeRange As String = ""
        Dim StartTimeMinuteString As String = ""
        If StartTime.Minute = 0 Then
            StartTimeMinuteString = "00"
        Else
            StartTimeMinuteString = StartTime.Minute
        End If
        Dim EndTimeMinuteString As String = ""
        If EndTime.Minute = 0 Then
            EndTimeMinuteString = "00"
        Else
            EndTimeMinuteString = EndTime.Minute
        End If
        Dim StartTimeHourString As String = ""
        If Mid(StartTime.Hour, 1, 1) = "0" Then
            StartTimeHourString = "0" & StartTime.Hour
        Else
            StartTimeHourString = StartTime.Hour
        End If
        Dim EndTimeHourString As String = ""
        If Mid(StartTime.Hour, 1, 1) = "0" Then
            EndTimeHourString = "0" & EndTime.Hour
        Else
            EndTimeHourString = EndTime.Hour
        End If
        TimeRange = StartTimeHourString & ":" & StartTimeMinuteString & "~" & EndTimeHourString & ":" & EndTimeMinuteString

        Return TimeRange
    End Function
    Public Sub AddComboBoxItme(ByVal Str As String, ByVal ComboBox As ComboBox, ByVal CoumnsName As String)
        ComboBox.Items.Clear()
        '  ComboBox.Items.Add("All")
        'Find Flexflow Data table SN to Fixture ID combobox!

        'Dim dr As SqlClient.SqlDataReader = Nothing
        ' dr = SqlSelect(Str)
        ' If dr.HasRows Then
        'While dr.Read
        'ComboBox.Items.Add(dr(CoumnsName))
        '  End While
        '  End If
        '  dr.Close()
        Call ReLoadMainWindow()
        'ComboBox.Items.Add("")
    End Sub
    Public Sub MoveFile(ByVal OriginaFile As String, ByVal ObjectFile As String)
        If File.Exists(OriginaFile) = False Then
            ' This statement ensures that the file is created,
            ' but the handle is not kept.
            Dim fst As FileStream = File.Create(OriginaFile)
            fst.Close()
        End If
        ' Ensure that the target does not exist.
        If File.Exists(ObjectFile) Then
            File.Delete(ObjectFile)
        End If

        ' Move the file.
        File.Move(OriginaFile, ObjectFile)
        Console.WriteLine("{0} moved to {1}", OriginaFile, ObjectFile)

        ' See if the original file exists now.
        If File.Exists(OriginaFile) Then
            Console.WriteLine("The original file still exists, which is unexpected.")
        Else
            Console.WriteLine("The original file no longer exists, which is expected.")
        End If
    End Sub

    Public Function SqlSelect(ByVal Str As String) As SqlClient.SqlDataReader
        If PublicDr IsNot Nothing Then
            PublicDr.Close()
            PublicCon.Close()
        Else
            PublicCon.Close()
        End If
        If PublicCon.State = ConnectionState.Closed Then
            PublicCon.ConnectionString = ConnectStr
            PublicCon.Open()
        End If
        PublicCmd.Connection = PublicCon
        If PublicDr IsNot Nothing Then
            PublicDr.Close()
        End If
        PublicCmd.CommandText = Str
        PublicDr = PublicCmd.ExecuteReader
        Return PublicDr

    End Function

    Public Sub SqlDelete(ByVal Str As String)
        If PublicDr IsNot Nothing Then
            PublicDr.Close()
            PublicCon.Close()
        Else
            PublicCon.Close()
        End If
        If PublicCon.State = ConnectionState.Closed Then
            PublicCon.ConnectionString = ConnectStr
            PublicCon.Open()
        End If
        PublicCmd.Connection = PublicCon
        ' "DELETE FROM UserID WHERE (UserID = '" & DelUserID & "')"
        PublicCmd.CommandText = Str
        PublicCmd.ExecuteReader()
        PublicCon.Close()
    End Sub

    Public Sub SqlUPDATE(ByVal Str As String)
        Try
            If PublicDr IsNot Nothing Then
                PublicDr.Close()
                PublicCon.Close()
            Else
                PublicCon.Close()
            End If
            If PublicCon.State = ConnectionState.Closed Then
                PublicCon.ConnectionString = ConnectStr
                PublicCon.Open()
            End If
            PublicCmd.Connection = PublicCon
            '  "UPDATE UserID SET [PassWord] = '" & UpDataPassWword & "'WHERE (UserID = '" & UpDataUserID & "')"
            PublicCmd.CommandText = Str
            PublicCmd.ExecuteReader()
            PublicCon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub SqlInsert(ByVal Str As String)
        If PublicDr IsNot Nothing Then
            PublicDr.Close()
            PublicCon.Close()
        Else
            PublicCon.Close()
        End If
        If PublicCon.State = ConnectionState.Closed Then
            PublicCon.ConnectionString = ConnectStr
            PublicCon.Open()
        End If
        PublicCmd.Connection = PublicCon
        '  "INSERT INTO UserID VALUES ('" & AddUserID & "','" & CheseName & "', '" & AddPassWword & "', '" & Limit & "', '" & Employee & "' , '" & Shift & "', '" & Station & "', '','','','','','','','','','','','" & Now.AddDays(-100).Date.ToString("yyyy-MM-dd") & "','" & BUStr & "')"
        PublicCmd.CommandText = Str
        PublicCmd.ExecuteReader()
        PublicCon.Close()
    End Sub

    Public Function NewPX(ByVal NewDefectCode() As String, ByVal PXtype As Boolean, ByVal SplitString As String, ByVal ArrangeNumber As Integer)
        Dim t As String
        For i As Int32 = 0 = 0 To NewDefectCode.Length - 1
            If NewDefectCode(i) = "" Then
                Exit For
            End If
            For j = 0 To NewDefectCode.Length - 1
                If NewDefectCode(j) = "" Then
                    Exit For
                End If
                Dim a, b As Single
                Dim StrA() As String = NewDefectCode(j).Split(SplitString)
                a = Val(StrA(ArrangeNumber))
                Dim StrB() As String = NewDefectCode(i).Split(SplitString)
                b = Val(StrB(ArrangeNumber))

                If PXtype = False Then
                    If a > b Then
                        t = NewDefectCode(i)
                        NewDefectCode(i) = NewDefectCode(j)
                        NewDefectCode(j) = t
                    End If
                Else
                    PXtype = True
                    If a < b Then
                        t = NewDefectCode(i)
                        NewDefectCode(i) = NewDefectCode(j)
                        NewDefectCode(j) = t
                    End If
                End If
            Next
        Next

        Return NewDefectCode

    End Function
    Public Function ReturnWorkTime(ByVal StartTime As Date, ByVal EndTime As Date, ByVal UserID As Int32, ByVal projectID As Int32, ByVal RunningType As String, ByVal Shift As String) As Int64

        '计算二个时间的间隔天数。
        Dim DayInt As Int32 = Math.Truncate((EndTime.ToFileTime - StartTime.ToFileTime) / 864000000000)

        '取出整天后的开始时间。
        Dim SaveStartTime As Date = StartTime.AddDays(DayInt)


        Dim Con As New SqlConnection
        Dim Cmd As New SqlCommand
        Con.ConnectionString = ConnectStr
        Con.Open()
        Cmd.Connection = Con
        Dim dr As SqlDataReader = Nothing
        Dim DTime(1) As String
        Dim NTime(1) As String
        If dr IsNot Nothing Then
            dr.Close()
        End If
        '计算出休息时间。
        If RunningType = "Debugger" Then
            Cmd.CommandText = "Select * From Rest_Time_Table Where UserID=N'" & UserID & "'"
            dr = Cmd.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    If dr("RestType") = "白班休息时间1" Then
                        DTime(0) = CDate(EndTime.Date & " " & dr("StartTime")) & "@" & CDate(EndTime.Date & " " & dr("EndTime"))
                    ElseIf dr("RestType") = "白班休息时间2" Then
                        DTime(1) = CDate(EndTime.Date & " " & dr("StartTime")) & "@" & CDate(EndTime.Date & " " & dr("EndTime"))
                    ElseIf dr("RestType") = "夜班休息时间1" Then
                        NTime(0) = CDate(SaveStartTime.Date & " " & dr("StartTime")).ToString("yyyy-MM-dd HH:mm:ss") & "@" & CDate(EndTime.Date & " " & dr("EndTime"))
                    ElseIf dr("RestType") = "夜班休息时间2" Then
                        NTime(1) = CDate(EndTime.Date & " " & dr("StartTime")) & "@" & CDate(EndTime.Date & " " & dr("EndTime"))
                    End If
                End While
            End If
        Else
            DTime(0) = CDate(EndTime.Date & " 11:50:00") & "@" & CDate(EndTime.Date & " 12:50:00")
            DTime(1) = CDate(EndTime.Date & " 17:30:00") & "@" & CDate(EndTime.Date & " 18:30:00")
            NTime(0) = CDate(EndTime.Date & " 23:50:00") & "@" & CDate(EndTime.Date & " 00:50:00")
            NTime(1) = CDate(EndTime.Date & " 05:30:00") & "@" & CDate(EndTime.Date & " 06:30:00")
        End If
        If dr IsNot Nothing Then
            dr.Close()
        End If
        '计算出人员和间隔天数的总和
        Dim SumDay As Int64 = 0
        For i As Int32 = 0 = 0 To DayInt - 1
            If RunningType = "Debugger" Then
                Cmd.CommandText = "Select Count(EmployeeID) as a From Debug_Out_Table Where EmployeeID In (Select EmployeeID From UserID Where UserID=N'" & UserID & "' and ProjectID=N'" & projectID & "') and RepairTime Between '" & StartTime.AddDays(i) & "' and '" & StartTime.AddDays(i + 1) & "' Group By EmployeeID"
            ElseIf RunningType = "Project" Then
                Cmd.CommandText = "Select Count(EmployeeID) as a From Debug_Out_Table Where ProjectID=N'" & projectID & "' and RepairTime Between '" & StartTime.AddDays(i) & "' and '" & StartTime.AddDays(i + 1) & "' Group By EmployeeID"
            ElseIf RunningType = "BU" Then
                Cmd.CommandText = "Select Count(EmployeeID) as a From Debug_Out_Table Where ProjectID In (Select ID From udtRMProject Where BUID=N'" & UserID & "') and RepairTime Between '" & StartTime.AddDays(i) & "' and '" & StartTime.AddDays(i + 1) & "' Group By EmployeeID"
            End If
            dr = Cmd.ExecuteReader
            If dr.HasRows Then
                If dr("a") IsNot DBNull.Value Then
                    SumDay = (i + 1) * dr("a")
                End If
            End If
            dr.Close()
        Next
        Dim WorkTime As Single = 0
        If EndTime.ToFileTime - SaveStartTime.ToFileTime > 12 * 36000000000 Then
            WorkTime = ((EndTime.ToFileTime - SaveStartTime.ToFileTime) / 36000000000) - 12 + (10 * DayInt)
        Else
            WorkTime = ((EndTime.ToFileTime - SaveStartTime.ToFileTime) / 36000000000) + (10 * DayInt)
        End If
        If Shift = "D" Then
            For s = 0 To DTime.Length - 1
                If DTime(s) = "" Then
                    Exit For
                End If
                Dim DNewFindStartTime As Date
                Dim DNewFindEndTime As Date
                Dim b() = DTime(s).Split("@")
                DNewFindStartTime = CDate(b(0))
                DNewFindEndTime = CDate(b(1))
                If EndTime > DNewFindStartTime Then
                    If EndTime >= DNewFindEndTime Then
                        WorkTime = WorkTime - ((DNewFindEndTime.ToFileTime - DNewFindStartTime.ToFileTime) / 36000000000)
                    Else
                        WorkTime = WorkTime - ((EndTime.ToFileTime - DNewFindStartTime.ToFileTime) / 36000000000)
                    End If
                End If
            Next
        End If
        If Shift = "N" Then
            For s = 0 To DTime.Length - 1
                If NTime(s) = "" Then
                    Exit For
                End If
                Dim NNewFindStartTime As Date
                Dim NNewFindEndTime As Date
                Dim b() = NTime(s).Split("@")
                NNewFindStartTime = CDate(b(0))
                NNewFindEndTime = CDate(b(1))
                If EndTime > NNewFindStartTime Then
                    If EndTime >= NNewFindEndTime Then
                        WorkTime = WorkTime - ((NNewFindEndTime.ToFileTime - NNewFindStartTime.ToFileTime) / 36000000000)
                    Else
                        WorkTime = WorkTime - ((EndTime.ToFileTime - NNewFindStartTime.ToFileTime) / 36000000000)
                    End If
                End If
            Next
        End If
        Dim TabTime As Single = 0
        If dr IsNot Nothing Then
            dr.Close()
        End If
        If RunningType = "Debugger" Then
            Cmd.CommandText = "Select * From Leave_Table where UserID=N'" & UserID & "' and EndTime>'" & StartTime & "' and ProjectID=N'" & projectID & "'"
        ElseIf RunningType = "BU" Then
            Cmd.CommandText = "Select * From Leave_Table where UserID=N'" & UserID & "' and EndTime>'" & StartTime & "' and ProjectID=N'" & projectID & "'"
        ElseIf RunningType = "Project" Then
            Cmd.CommandText = "Select * From Leave_Table where UserID=N'" & UserID & "' and EndTime>'" & StartTime & "' and ProjectID=N'" & projectID & "'"
        End If
        dr = Cmd.ExecuteReader
        If dr.HasRows Then
            While dr.Read
                Dim FindStartTime As Date = CDate(dr("StartTime"))
                Dim FindEndTime As Date = CDate(dr("EndTime"))
                If EndTime > FindStartTime And StartTime < FindEndTime Then
                    If EndTime >= FindEndTime Then
                        WorkTime = WorkTime - ((FindEndTime.ToFileTime - FindStartTime.ToFileTime))
                    Else
                        WorkTime = WorkTime - ((EndTime.ToFileTime - FindStartTime.ToFileTime))
                    End If
                End If
                Dim cccc = 1
                If cccc = 1 Then
                    If StartTime.Hour = 8 And StartTime.Minute = 30 Then
                        For s = 0 To DTime.Length - 1
                            Dim DNewFindStartTime As Date
                            Dim DNewFindEndTime As Date
                            If DTime(s) = "" Then
                                Exit For
                            End If
                            Dim b() = DTime(s).Split("@")
                            DNewFindStartTime = CDate(b(0))
                            DNewFindEndTime = CDate(b(1))
                            If EndTime > DNewFindStartTime Then
                                If FindEndTime > DNewFindStartTime And FindStartTime < DNewFindEndTime And EndTime >= FindStartTime Then
                                    If FindEndTime >= DNewFindEndTime And FindStartTime <= DNewFindStartTime Then
                                        TabTime = TabTime + ((DNewFindEndTime.ToFileTime - DNewFindStartTime.ToFileTime))
                                    ElseIf FindEndTime >= DNewFindEndTime And FindStartTime > DNewFindStartTime Then
                                        TabTime = TabTime + ((FindStartTime.ToFileTime - DNewFindStartTime.ToFileTime))
                                    ElseIf FindEndTime < DNewFindEndTime Then
                                        TabTime = TabTime + ((FindEndTime.ToFileTime - DNewFindStartTime.ToFileTime))
                                    End If
                                End If
                            End If
                        Next
                    End If
                    If StartTime.Hour = 20 And StartTime.Minute = 30 Then
                        For s = 0 To NTime.Length - 1
                            If NTime(s) = "" Then
                                Exit For
                            End If
                            Dim NNewFindStartTime As Date
                            Dim NNewFindEndTime As Date
                            Dim b() = NTime(s).Split("@")
                            NNewFindStartTime = CDate(b(0))
                            NNewFindEndTime = CDate(b(1))
                            If EndTime > NNewFindStartTime Then
                                If FindEndTime > NNewFindStartTime And FindStartTime < NNewFindEndTime And EndTime >= FindStartTime Then
                                    If FindEndTime >= NNewFindEndTime And FindStartTime <= NNewFindStartTime Then
                                        TabTime = TabTime + ((NNewFindEndTime.ToFileTime - NNewFindStartTime.ToFileTime))
                                    ElseIf FindEndTime >= NNewFindEndTime And FindStartTime > NNewFindStartTime Then
                                        TabTime = TabTime + ((FindStartTime.ToFileTime - NNewFindStartTime.ToFileTime))
                                    ElseIf FindEndTime < NNewFindEndTime Then
                                        TabTime = TabTime + ((FindEndTime.ToFileTime - NNewFindStartTime.ToFileTime))
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End While
        End If
        If dr IsNot Nothing Then
            dr.Close()
            Con.Close()
        Else
            Con.Close()
        End If
        Return WorkTime = WorkTime + TabTime
    End Function
    Public DownloadWindownIsOpen As Boolean = False  '验证下载窗口是否有打开。
    Public VDownloadDataGridView As DataGridView = Nothing '记录被下载的数据源。
    Public Sub FindDataToDataGridView(ByVal FindString As String, ByVal DataGridView As DataGridView)
        Try

            If VDownloadDataGridView IsNot Nothing Then
                If VDownloadDataGridView.Name = DataGridView.Name Then
                    MsgBox("该表格中数据正在被下载中，待下载完成后才能做新的查询！")
                    Exit Sub
                End If
            End If
            Dim PublicCon As New SqlConnection
            PublicCon.ConnectionString = ConnectStr
            PublicCon.Open()
            PublicCmd.Connection = PublicCon
            Dim STRsql As String
            If FindString = "" Then
                Exit Sub
            End If
            Dim builder As SqlClient.SqlCommandBuilder
            Dim adapter As New SqlClient.SqlDataAdapter
            STRsql = FindString
            adapter.SelectCommand = New SqlClient.SqlCommand(STRsql, PublicCon)
            builder = New SqlClient.SqlCommandBuilder(adapter)
            Dim MYDATASET As New DataSet
            adapter.Fill(MYDATASET)
            DataGridView.DataSource = MYDATASET.Tables("table") '=MyTable
            '填充数据表()
            Dim MyTable As New DataTable()
            MyTable.Clear()
            adapter.Fill(MyTable)
            DataGridView.DataSource = MyTable
            If PublicDr IsNot Nothing Then
                PublicDr.Close()
                PublicCon.Close()
            Else
                PublicCon.Close()
            End If
            Dim FindWIPTable As Boolean = False
            Dim a As String = "Select * From Debug_WIP_Table_Select_View"
            If FindString.Length > a.Length Then
                If Mid(FindString, 1, a.Length) = a Then

                    FindWIPTable = True
                End If
            End If
            If FindWIPTable = True Then
                Dim systime As Date = ReturnSystime()
                For i As Int32 = 0 = 0 To DataGridView.RowCount - 1
                    If DataGridView(11, i) IsNot DBNull.Value Then
                        If DataGridView(11, i).Value.ToString <> "" Then
                            Dim oldtime As Date = DataGridView(11, i).Value
                            DataGridView(18, i).Value = Math.Truncate((systime.ToFileTime - oldtime.ToFileTime) / 864000000000)
                        Else
                            DataGridView(18, i).Value = 0
                        End If
                    Else
                        DataGridView(18, i).Value = 0
                    End If
                Next
            End If
            For i As Int32 = 0 = 0 To DataGridView.Columns.Count - 1
                DataGridView.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Next
        Catch ex As Exception
        End Try
    End Sub
    Public Sub DataToFile(ByVal DataGridView As DataGridView)
        Dim DataStr As String = Application.StartupPath & "\UPH.txt"
        If IO.File.Exists(DataStr) = True Then
            File.Delete(DataStr)
        End If
        Dim Reportfs As New FileStream(DataStr, FileMode.Append)
        Dim Reportsw As New StreamWriter(Reportfs)

        Dim a As String = ""
        For Cols = 1 To DataGridView.Columns.Count
            a = a & DataGridView.Columns(Cols - 1).HeaderText & vbTab
        Next
        '  a = a & vbLf
        Reportsw.WriteLine(a)
        a = ""
        For i As Int32 = 0 = 0 To DataGridView.RowCount - 2
            a = ""
            Dim j As Integer
            For j = 0 To DataGridView.ColumnCount - 1

                If DataGridView(j, i).Value Is System.DBNull.Value Then
                    a = a & vbTab
                Else
                    a = a & Trim(DataGridView(j, i).Value) & vbTab
                End If
            Next (j)
            '  a = a & vbLf
            Reportsw.WriteLine(a)
        Next (i)
        Reportsw.Close()
        Reportfs.Close()
        Process.Start(DataStr)
    End Sub

    Public Function PX(ByVal NewDefectCode() As String, ByVal PXtype As Boolean)
        Dim t As String
        For i As Int32 = 0 = 0 To NewDefectCode.Length - 1
            If NewDefectCode(i) = "" Then
                Exit For
            End If
            For j = 0 To NewDefectCode.Length - 1
                If NewDefectCode(j) = "" Then
                    Exit For
                End If
                Dim a, b As Int64
                Dim StrMname As MatchCollection
                Dim StrSN As MatchCollection
                StrMname = Regex.Matches(NewDefectCode(j), "[^@]+")
                a = Val(StrMname(0).Value)
                StrSN = Regex.Matches(NewDefectCode(i), "[^@]+")
                b = Val(StrSN(0).Value)

                If PXtype = False Then
                    If a > b Then
                        t = NewDefectCode(i)
                        NewDefectCode(i) = NewDefectCode(j)
                        NewDefectCode(j) = t
                    End If
                Else
                    PXtype = True
                    If a < b Then
                        t = NewDefectCode(i)
                        NewDefectCode(i) = NewDefectCode(j)
                        NewDefectCode(j) = t
                    End If
                End If
            Next
        Next
        Return NewDefectCode
    End Function

    Public Sub FastDataToExcel(ByVal Datagridview1 As DataGridView, ByVal StartColumn As Int16)
        If DownloadWindownIsOpen = True Then
            Download_Window.Close()
            Download_Window.Show()
        Else
            Download_Window.Show()
        End If
        Download_Window.DownLoadDatagridview = Datagridview1
        Download_Window.StartColumn = StartColumn
        Download_Window.DownloadFile()
    End Sub

    Public Sub DataToExcel(ByVal DataGridView1 As DataGridView, ByVal StartColumn As Int16)
        Dim Rows As Int64 = DataGridView1.RowCount - 2
        If Rows = -2 Then
            Exit Sub
        End If
        Dim MyExcel As New Microsoft.Office.Interop.Excel.Application()

        '获取标题   
        Dim Cols As Integer
        Dim XlsBook As Excel.Workbook = MyExcel.Application.Workbooks.Add()
        Dim XlsSheet As Excel.Worksheet = XlsBook.Sheets("Sheet1")
        For Cols = StartColumn + 1 To DataGridView1.Columns.Count
            MyExcel.Cells(1, Cols - StartColumn) = DataGridView1.Columns(Cols - 1).HeaderText
        Next
        '往excel表里添加数据()   
        Dim Column As Int64 = DataGridView1.ColumnCount
        Dim i As Integer
        For i = 0 To DataGridView1.RowCount - 2
            Dim j As Integer
            For j = StartColumn To DataGridView1.ColumnCount - 1
                If DataGridView1(j, i).Value Is System.DBNull.Value Then
                    MyExcel.Cells(i + 2, j + 1 - StartColumn) = ""
                Else
                    If DataGridView1(j, i).Value IsNot DBNull.Value Then
                        MyExcel.Cells(i + 2, j + 1 - StartColumn) = Trim(DataGridView1(j, i).Value)
                    Else
                        MyExcel.Cells(i + 2, j + 1) = ""
                    End If
                End If
            Next (j)
        Next (i)
        MyExcel.Visible = True
        Dim rn As Excel.Range = XlsSheet.Range(XlsSheet.Cells(1, 1), XlsSheet.Cells(1, Column - StartColumn))
        rn.Interior.Color = Color.GreenYellow
        rn = XlsSheet.Range(XlsSheet.Cells(1, 1), XlsSheet.Cells(DataGridView1.RowCount, Column - StartColumn))
        rn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        rn.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter
        rn.WrapText = False
        rn.Orientation = 0
        rn.AddIndent = False
        rn.IndentLevel = 0
        rn.ShrinkToFit = False
        rn.ReadingOrder = 3
        rn.MergeCells = False
        MyExcel.Columns("A:AZ").EntireColumn.AutoFit()
        Dim xlshell As Excel.Worksheet = MyExcel.Sheets("Sheet1")
        xlshell.UsedRange.Borders.Color = Color.Black
    End Sub

    Public Sub SetDataGridViewFormat(ByVal DataGridView As DataGridView, ByVal Columns As Integer, ByVal FormatValues As Single, ByVal Type As Boolean)
        For i As Int32 = 0 = 0 To DataGridView.RowCount - 2
            Try
                ' If DataGridView(i, Columns) IsNot DBNull.Value Then
                If Type = True Then
                    If DataGridView(Columns, i).Value < FormatValues Then
                        DataGridView(Columns, i).Style.BackColor = Color.Red
                    Else
                        DataGridView(Columns, i).Style.BackColor = Color.Lime
                    End If
                Else
                    If DataGridView(Columns, i).Value > FormatValues Then
                        DataGridView(Columns, i).Style.BackColor = Color.Red
                    Else
                        DataGridView(Columns, i).Style.BackColor = Color.Lime
                    End If
                End If

                '  End If
            Catch ex As Exception
                Continue For
            End Try
        Next
    End Sub
    Public Function ReturnSystime() As Date
        If PublicDr IsNot Nothing Then
            PublicDr.Close()
            PublicCon.Close()
        Else
            PublicCon.Close()
        End If
        If PublicCon.State = ConnectionState.Closed Then
            PublicCon.ConnectionString = ConnectStr
            PublicCon.Open()
        End If
        PublicCmd.Connection = PublicCon
        PublicCmd.CommandText = "SELECT GETDATE() AS SvrTime"
        PublicDr = PublicCmd.ExecuteReader
        Dim SysTime As Date
        If PublicDr.Read Then
            SysTime = PublicDr("SvrTime")
        End If
        If PublicDr IsNot Nothing Then
            PublicDr.Close()
            PublicCon.Close()
        End If
        Return SysTime
    End Function
    Public Sub WriteTXT(ByVal FileName As String, ByVal AddText As String)
        Dim Reportfs As New FileStream(FileName, FileMode.Append)
        Dim Reportsw As New StreamWriter(Reportfs)
        Reportfs.Seek(0, SeekOrigin.End)
        Reportsw.WriteLine(AddText.ToString)
        Reportsw.Flush()
        Reportsw.Close()
    End Sub
    Public Sub Two_Paint(ByVal PillarQYT As Int16, ByVal YMax As Single, ByVal YTextFontInt As Single, ByVal PicturePanel As System.Windows.Forms.Panel, ByVal e As System.Windows.Forms.PaintEventArgs, ByVal ShowText() As String, ByVal ShowTextFontInt As Single, ByVal PillarOneData() As Single, ByVal PillarOneText As String, ByVal PillarOneTextFontInt As Single, ByVal PillarTwoData() As Single, ByVal PillarTwoText As String, ByVal PillarTwoTextFontInt As Single, ByVal TitleText As String, ByVal TitleTextFontInt As Single)

        Dim max_y As Single = YMax
        If max_y = 0 Then
            max_y = 6
        End If
        Dim height As Integer = PicturePanel.Height
        Dim width As Integer = PicturePanel.Width
        Dim space As Integer = 15 '原点到左边和下边的距离
        ' Dim interval As Integer = 10 '单位长度
        Dim max_x As Integer = PillarQYT * 3 + 1
        Dim intervalY As Single = (height - space * 4) / max_y
        Dim intervalX As Single = (width - space * 2 - 12) / max_x
        'Dim bmp As New Bitmap(width, height)
        'Dim g As Graphics = Graphics.FromImage(bmp)
        Dim pen As Pen = New Pen(Color.White, 2)
        '定义一组数据
        '   Dim arrData() As Integer = {TreeView1.Nodes(0).GetNodeCount(True), TreeView1.Nodes(1).GetNodeCount(True), TreeView1.Nodes(2).GetNodeCount(True), TreeView1.Nodes(0).GetNodeCount(True)}
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.Clear(Color.Black)
        'g.DrawLine(pen,point1,point2)
        'Pen 对象，它确定线条的颜色、宽度和样式。 
        'Point 结构，它表示要连接的第一个点。 
        e.Graphics.DrawLine(pen, New Point(space * 3, height - 2 * space), New Point(width, height - 2 * space)) 'x轴
        e.Graphics.DrawLine(pen, New Point(space * 3, 2.5 * space), New Point(space * 3, height - 2 * space))  'y轴
        pen = New Pen(Color.White, 4)
        e.Graphics.DrawLine(pen, New Point(2, 0), New Point(2, height))
        e.Graphics.DrawLine(pen, New Point(width - 2, 0), New Point(width - 2, height))
        e.Graphics.DrawLine(pen, New Point(2, 2), New Point(width, 2)) 'x轴
        e.Graphics.DrawLine(pen, New Point(2, height - 2), New Point(width, height - 2)) 'x轴
        Dim g As Graphics = Form1.CreateGraphics()
        Dim flpWidht As Integer = 0
        pen = New Pen(Color.White, 2)
        'x轴上的刻度
        Dim sf As Integer = 0
        Dim DrawStringA1 As Single = 0
        Dim DrawStringA2 As Single = 0
        For i As Int32 = 0 = 1 To max_x Step 2
            If sf > PillarQYT - 1 Then
                Exit For
            End If
            Dim TextHigeh As Single
            If ShowText(sf) <> "" Then
                TextHigeh = height - space * 2
                Try
                    flpWidht = g.MeasureString(ShowText(sf), New Font("Bodoni MT", ShowTextFontInt)).Width
                    DrawStringA1 = (space * 3 + (sf * 3 + 2) * intervalX) - (flpWidht / 2)
                    e.Graphics.DrawString(ShowText(sf), New Font("Bodoni MT", ShowTextFontInt, FontStyle.Regular), Brushes.White, New PointF(DrawStringA1, TextHigeh))
                    sf = sf + 1
                Catch ex As Exception
                    Exit Try
                End Try
            Else
                Exit For
            End If
        Next
        'y轴上的刻度
        Dim StepInt As Single = max_y / 5
        sf = 0
        For i As Int32 = 0 = 0 To max_y Step StepInt
            Dim lineY As Single = i * intervalY
            If lineY <= max_y * intervalY Then
                e.Graphics.DrawLine(pen, New Point(space * 3, height - lineY - 2 * space), New Point(space * 3 + 20, height - lineY - 2 * space))
                If lineY <> 0 Then

                    flpWidht = g.MeasureString(i.ToString("0"), New Font("Bodoni MT", YTextFontInt)).Width
                    e.Graphics.DrawString(i.ToString("0"), New Font("Bodoni MT", YTextFontInt, FontStyle.Bold), Brushes.White, New PointF((55 - flpWidht) / 2, height - 2 * space - lineY - 6))
                End If
            End If
        Next

        For i As Int32 = 0 = 0 To PillarQYT - 1
            Dim Clo As Color = Color.BlueViolet
            Dim pen_chart As Pen
            If PillarOneData(i) > 0 Then
                pen_chart = New Pen(Clo, (width - space * 2) / (max_x) - 5)
                e.Graphics.DrawLine(pen_chart, New Point(space * 3 + (i * 3 + 1.5) * intervalX, height - 2 * space - PillarOneData(i) * intervalY - 1.5), New Point(space * 3 + (i * 3 + 1.5) * intervalX, height - 2 * space - 1.5))
                DrawStringA2 = height - 3.2 * space - PillarOneData(i) * intervalY
                If DrawStringA2 > PicturePanel.Height - 5 * space Then
                    DrawStringA2 = PicturePanel.Height - 5 * space
                End If
                flpWidht = g.MeasureString(PillarOneData(i), New Font("Bodoni MT", PillarOneTextFontInt)).Width
                DrawStringA1 = (space * 3 + (i * 3 + 1.5) * intervalX) - (flpWidht / 2)
                e.Graphics.DrawString(PillarOneData(i), New Font("Bodoni MT", PillarOneTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, DrawStringA2))
                flpWidht = g.MeasureString(PillarOneText, New Font("Bodoni MT", PillarOneTextFontInt)).Width
                DrawStringA1 = (space * 3 + (i * 3 + 1.5) * intervalX) - (flpWidht / 2)
                e.Graphics.DrawString(PillarOneText, New Font("Bodoni MT", PillarOneTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, PicturePanel.Height - 3.5 * space))
            End If

            If PillarTwoData(i) > 0 Then
                Clo = Color.DodgerBlue
                pen_chart = New Pen(Clo, (width - space * 2) / (max_x) - 5)
                e.Graphics.DrawLine(pen_chart, New Point(space * 3 + (i * 3 + 2.5) * intervalX, height - 2 * space - PillarTwoData(i) * intervalY - 1.5), New Point(space * 3 + (i * 3 + 2.5) * intervalX, height - 2 * space - 1.5))
                DrawStringA2 = height - 3.2 * space - PillarTwoData(i) * intervalY
                If DrawStringA2 > PicturePanel.Height - 5 * space Then
                    DrawStringA2 = PicturePanel.Height - 5 * space
                End If
                flpWidht = g.MeasureString(PillarTwoData(i), New Font("Bodoni MT", PillarTwoTextFontInt)).Width
                DrawStringA1 = (space * 3 + (i * 3 + 2.5) * intervalX) - (flpWidht / 2)
                e.Graphics.DrawString(PillarTwoData(i), New Font("Bodoni MT", PillarTwoTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, DrawStringA2))
                flpWidht = g.MeasureString(PillarTwoText, New Font("Bodoni MT", PillarTwoTextFontInt)).Width
                DrawStringA1 = (space * 3 + (i * 3 + 2.5) * intervalX) - (flpWidht / 2)
                e.Graphics.DrawString(PillarTwoText, New Font("Bodoni MT", PillarTwoTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, PicturePanel.Height - 3.5 * space))
            End If
        Next
        flpWidht = g.MeasureString(TitleText, New Font("Bodoni MT", TitleTextFontInt)).Width
        DrawStringA1 = (PicturePanel.Width - flpWidht) / 2
        e.Graphics.DrawString(TitleText, New Font("Bodoni MT", TitleTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, 10))
        Try
            e.Graphics.Dispose()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub One_Paint(ByVal PillarQYT As Int16, ByVal YMax As Single, ByVal YTextFontInt As Single, ByVal PicturePanel As System.Windows.Forms.Panel, ByVal e As System.Windows.Forms.PaintEventArgs, ByVal ShowText() As String, ByVal ShowTextFontInt As Single, ByVal PillarOneData() As Single, ByVal PillarOneTextFontInt As Single, ByVal Target As Single, ByVal TitleText As String, ByVal TitleTextFontInt As Single)

        Dim max_y As Single = YMax
        If max_y = 0 Then
            max_y = 6
        End If
        Dim height As Integer = PicturePanel.Height
        Dim width As Integer = PicturePanel.Width
        Dim space As Integer = 15 '原点到左边和下边的距离
        ' Dim interval As Integer = 10 '单位长度
        Dim max_x As Integer = PillarQYT * 2 + 1
        Dim intervalY As Single = (height - space * 4) / max_y
        Dim intervalX As Single = (width - space * 2 - 12) / max_x
        'Dim bmp As New Bitmap(width, height)
        'Dim g As Graphics = Graphics.FromImage(bmp)
        Dim pen As Pen = New Pen(Color.White, 2)
        '定义一组数据
        '   Dim arrData() As Integer = {TreeView1.Nodes(0).GetNodeCount(True), TreeView1.Nodes(1).GetNodeCount(True), TreeView1.Nodes(2).GetNodeCount(True), TreeView1.Nodes(0).GetNodeCount(True)}
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.Clear(Color.Black)
        'g.DrawLine(pen,point1,point2)
        'Pen 对象，它确定线条的颜色、宽度和样式。 
        'Point 结构，它表示要连接的第一个点。 
        e.Graphics.DrawLine(pen, New Point(space * 3, height - 2 * space), New Point(width, height - 2 * space)) 'x轴
        e.Graphics.DrawLine(pen, New Point(space * 3, 2.5 * space), New Point(space * 3, height - 2 * space))  'y轴
        pen = New Pen(Color.White, 4)
        e.Graphics.DrawLine(pen, New Point(2, 0), New Point(2, height))
        e.Graphics.DrawLine(pen, New Point(width - 2, 0), New Point(width - 2, height))
        e.Graphics.DrawLine(pen, New Point(2, 2), New Point(width, 2)) 'x轴
        e.Graphics.DrawLine(pen, New Point(2, height - 2), New Point(width, height - 2)) 'x轴
        Dim g As Graphics = Form1.CreateGraphics()
        Dim flpWidht As Integer = 0
        pen = New Pen(Color.White, 2)
        'x轴上的刻度
        Dim sf As Integer = 0
        Dim DrawStringA1 As Single = 0
        Dim DrawStringA2 As Single = 0
        For i As Int32 = 0 = 1 To max_x Step 2
            If sf > PillarQYT - 1 Then
                Exit For
            End If
            Dim TextHigeh As Single
            If ShowText(sf) <> "" Then
                TextHigeh = height - space * 2
                Try
                    flpWidht = g.MeasureString(ShowText(sf), New Font("Arail", ShowTextFontInt)).Width
                    DrawStringA1 = (space * 3 + (sf * 2 + 1.5) * intervalX) - (flpWidht / 2)
                    e.Graphics.DrawString(ShowText(sf), New Font("Arail", ShowTextFontInt, FontStyle.Regular), Brushes.White, New PointF(DrawStringA1, TextHigeh))
                    sf = sf + 1
                Catch ex As Exception
                    Exit Try
                End Try
            Else
                Exit For
            End If
        Next
        'y轴上的刻度
        Dim StepInt As Single = max_y / 5

        sf = 0
        For i As Int32 = 0 = 0 To max_y Step StepInt
            Dim lineY As Single = i * intervalY
            If lineY <= max_y * intervalY Then
                e.Graphics.DrawLine(pen, New Point(space * 3, height - lineY - 2 * space), New Point(space * 3 + 20, height - lineY - 2 * space))
                If lineY <> 0 Then
                    flpWidht = g.MeasureString(i.ToString("0"), New Font("Bodoni MT", YTextFontInt)).Width
                    e.Graphics.DrawString(i.ToString("0"), New Font("Bodoni MT", YTextFontInt, FontStyle.Bold), Brushes.White, New PointF((55 - flpWidht) / 2, height - 2 * space - lineY - 6))
                End If
            End If
        Next
        pen = New Pen(Color.Lime, 2)
        e.Graphics.DrawLine(pen, New Point(space * 3, height - 2 * space - Target * intervalY), New Point(width, height - 2 * space - Target * intervalY)) 'x轴
        For i As Int32 = 0 = 0 To PillarQYT - 1
            Dim Clo As Color = Color.DodgerBlue
            Dim pen_chart As Pen
            If PillarOneData(i) > 0 Then
                pen_chart = New Pen(Clo, (width - space * 2) / (max_x) - 5)
                e.Graphics.DrawLine(pen_chart, New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - 2 * space - PillarOneData(i) * intervalY - 1.5), New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - 2 * space - 1.5))
                DrawStringA2 = height - 3.2 * space - PillarOneData(i) * intervalY
                If DrawStringA2 > PicturePanel.Height - 5 * space Then
                    DrawStringA2 = PicturePanel.Height - 5 * space
                End If
                flpWidht = g.MeasureString(PillarOneData(i).ToString("0.00") & "%", New Font("Bodoni MT", PillarOneTextFontInt)).Width
                DrawStringA1 = (space * 3 + (i * 2 + 1.5) * intervalX) - (flpWidht / 2)
                e.Graphics.DrawString(PillarOneData(i).ToString("0.00") & "%", New Font("Bodoni MT", PillarOneTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, DrawStringA2))
            End If
        Next
        flpWidht = g.MeasureString(TitleText, New Font("Bodoni MT", TitleTextFontInt)).Width
        DrawStringA1 = (PicturePanel.Width - flpWidht) / 2
        e.Graphics.DrawString(TitleText, New Font("Bodoni MT", TitleTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, 10))
        Try
            e.Graphics.Dispose()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub OverlayPicture(ByVal PillarQYT As Int16, ByVal YMax As Single, ByVal YTextFontInt As Single, ByVal PicturePanel As System.Windows.Forms.Panel, ByVal e As System.Windows.Forms.PaintEventArgs, ByVal ShowText() As String, ByVal ShowTextFontInt As Single, ByVal PillarOneData() As Single, ByVal PillarTwoData() As Single, ByVal PillarThreeData() As Single, ByVal WIP() As Single, ByVal WIPTextFontInt As Single, ByVal TitleText As String, ByVal TitleTextFontInt As Single, ByVal WIPTarget As Single)
        Dim max_y As Single = YMax
        If max_y = 0 Then
            max_y = 6
        End If
        Dim height As Integer = PicturePanel.Height
        Dim width As Integer = PicturePanel.Width
        Dim space As Integer = 15 '原点到左边和下边的距离
        ' Dim interval As Integer = 10 '单位长度
        Dim max_x As Integer = PillarQYT * 2 + 1 'x
        Dim intervalY As Single = (height - space * 4) / max_y
        Dim intervalX As Single = (width - width * 0.17) / max_x
        'Dim bmp As New Bitmap(width, height)
        'Dim g As Graphics = Graphics.FromImage(bmp)
        Dim pen As Pen = New Pen(Color.White, 2)
        '定义一组数据
        '   Dim arrData() As Integer = {TreeView1.Nodes(0).GetNodeCount(True), TreeView1.Nodes(1).GetNodeCount(True), TreeView1.Nodes(2).GetNodeCount(True), TreeView1.Nodes(0).GetNodeCount(True)}

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.Clear(Color.Black)
        'g.DrawLine(pen,point1,point2)
        'Pen 对象，它确定线条的颜色、宽度和样式。 
        'Point 结构，它表示要连接的第一个点。 
        e.Graphics.DrawLine(pen, New Point(space * 3, height - 2 * space), New Point(width, height - 2 * space)) 'x轴
        e.Graphics.DrawLine(pen, New Point(space * 3, 2.5 * space), New Point(space * 3, height - 2 * space))  'y轴
        pen = New Pen(Color.White, 4)
        e.Graphics.DrawLine(pen, New Point(2, 0), New Point(2, height))
        e.Graphics.DrawLine(pen, New Point(2, 2), New Point(width, 2)) 'x轴
        e.Graphics.DrawLine(pen, New Point(2, height - 2), New Point(width, height - 2)) 'x轴

        e.Graphics.DrawLine(pen, New Point(width - 2, 0), New Point(width - 2, height))
        pen = New Pen(Color.Lime, 2)
        e.Graphics.DrawLine(pen, New Point(space * 3, height - 2 * space - WIPTarget * intervalY), New Point(width - width * 0.15, height - 2 * space - WIPTarget * intervalY)) 'x轴
        pen = New Pen(Color.White, 0.5)
        Dim g As Graphics = Form1.CreateGraphics()
        Dim flpWidht As Integer = 0

        pen = New Pen(Color.White, 2)
        'x轴上的刻度
        Dim sf As Integer = 0
        Dim DrawStringA1 As Single = 0
        Dim DrawStringA2 As Single = 0
        For i As Int32 = 0 = 1 To max_x Step 2
            If sf > PillarQYT - 1 Then
                Exit For
            End If
            Dim TextHigeh As Single
            If ShowText(sf) <> "" Then
                TextHigeh = height - space * 2
                Try
                    flpWidht = g.MeasureString(ShowText(sf), New Font("Arail", ShowTextFontInt)).Width
                    DrawStringA1 = (space * 3 + (sf * 2 + 1.5) * intervalX) - (flpWidht / 2)
                    e.Graphics.DrawString(ShowText(sf), New Font("Arail", ShowTextFontInt, FontStyle.Regular), Brushes.White, New PointF(DrawStringA1, TextHigeh))
                    sf = sf + 1
                Catch ex As Exception
                    Exit Try
                End Try
            Else
                Exit For
            End If
        Next
        'y轴上的刻度
        Dim StepInt As Single = max_y / 5
        sf = 0
        For i As Int32 = 0 = 0 To max_y Step StepInt
            Dim lineY As Single = i * intervalY
            If lineY <= max_y * intervalY Then
                e.Graphics.DrawLine(pen, New Point(space * 3, height - lineY - 2 * space), New Point(space * 3 + 20, height - lineY - 2 * space))
                If lineY <> 0 Then
                    flpWidht = g.MeasureString(i.ToString("0"), New Font("Arail", YTextFontInt)).Width
                    e.Graphics.DrawString(i.ToString("0"), New Font("Arial", YTextFontInt, FontStyle.Bold), Brushes.White, New PointF((55 - flpWidht) / 2, height - 2 * space - lineY - 6))
                End If
            End If
        Next
        Dim FlpHight As Single = 0
        For i As Int32 = 0 = 0 To PillarQYT - 1
            Dim DrawStringAgingHight7 As Single = 0
            Dim DrawStringAgingHight7_30 As Single = 0
            Dim DrawStringAgingHight30 As Single = 0
            Dim SaveAging7Hight As Single = 0
            Dim SaveAging7_30Hight As Single = 0
            Dim SaveAging30Hight As Single = 0
            If sf > WIP.Length - 1 Then
                Exit For
            End If
            Dim Clo As Color = Color.DodgerBlue
            Dim pen_chart As Pen
            Dim Y0 As Single = 2 * space + 1.5
            If PillarOneData(i) > 0 Then
                pen_chart = New Pen(Clo, (width - space * 2) / (max_x) - 10)
                SaveAging7Hight = PillarOneData(i) * intervalY
                e.Graphics.DrawLine(pen_chart, New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - SaveAging7Hight - Y0), New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - 2 * space - 1.5))
            End If

            If PillarTwoData(i) > 0 Then
                Clo = Color.BlueViolet
                pen_chart = New Pen(Clo, (width - space * 2) / (max_x) - 10)
                SaveAging7_30Hight = PillarTwoData(i) * intervalY
                e.Graphics.DrawLine(pen_chart, New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - SaveAging7Hight - SaveAging7_30Hight - Y0), New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - SaveAging7Hight - Y0))
            End If
            If PillarThreeData(i) > 0 Then
                Clo = Color.Violet
                pen_chart = New Pen(Clo, (width - space * 2) / (max_x) - 10)
                SaveAging30Hight = PillarThreeData(i) * intervalY
                e.Graphics.DrawLine(pen_chart, New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - SaveAging30Hight - SaveAging7_30Hight - SaveAging7Hight - Y0), New Point(space * 3 + (i * 2 + 1.5) * intervalX, height - SaveAging7_30Hight - SaveAging7Hight - Y0))
            End If
            flpWidht = g.MeasureString(WIP(i), New Font("Arail", WIPTextFontInt)).Width
            FlpHight = g.MeasureString(WIP(i), New Font("Arail", WIPTextFontInt)).Height
            DrawStringAgingHight30 = height - SaveAging30Hight - SaveAging7_30Hight - SaveAging7Hight - Y0 - FlpHight
            DrawStringA1 = (space * 3 + (i * 2 + 1.5) * intervalX) - (flpWidht / 2)
            e.Graphics.DrawString(WIP(i), New Font("Arial", 12, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, DrawStringAgingHight30))
        Next
        flpWidht = g.MeasureString(TitleText, New Font("Arail", TitleTextFontInt)).Width
        DrawStringA1 = (PicturePanel.Width - flpWidht) / 2
        e.Graphics.DrawString(TitleText, New Font("Arial", TitleTextFontInt, FontStyle.Bold), Brushes.White, New PointF(DrawStringA1, 10))
        Try
            e.Graphics.Dispose()
        Catch ex As Exception

        End Try
    End Sub
    Public Function ReturnStartTime(ByVal RunType As String, ByVal FindString As String) As Date
        Dim Con As New SqlClient.SqlConnection
        Dim Cmd As New SqlClient.SqlCommand
        Con.ConnectionString = ConnectStr
        Con.Open()
        Cmd.Connection = Con
        Dim dr As SqlClient.SqlDataReader = Nothing
        Dim SystemTime As Date = ReturnSystime()
        Dim StartTime As Date
        Dim D_StartTimeString As String = ""
        Dim N_StartTimeString As String = ""
        If RunType = "Debugger" Then
            Cmd.CommandText = "Select * From Shift_Work_Time Where ProjectID=(Select ProjectID From UserID Where CheseName=N'" & FindString & "')"
        ElseIf RunType = "Project" Then
            Cmd.CommandText = "Select * From Shift_Work_Time Where ProjectID=(Select ID From udtRMProject Where Project=N'" & FindString & "')"
        Else
            Cmd.CommandText = "Select * From Shift_Work_Time Where ProjectID=(Select ID From udtRMProject Where BUID=(Select ID From udtRMBU Where BUName=N'" & FindString & "'))"
        End If
        dr = Cmd.ExecuteReader
        If dr.HasRows Then
            While dr.Read
                If D_StartTimeString <> "" And N_StartTimeString <> "" Then
                    Exit While
                End If
                If dr("Shift") = "D" Then
                    D_StartTimeString = dr("StartTime")
                Else
                    N_StartTimeString = dr("StartTime")
                End If
            End While
        End If
        dr.Close()
        Dim D_StartTime As Date = CDate(SystemTime.Date & " " & D_StartTimeString)
        Dim N_StartTime As Date = CDate(SystemTime.Date & " " & N_StartTimeString)
        If SystemTime.Hour < D_StartTime.Hour Then
            StartTime = CDate(SystemTime.AddDays(-1).Date & " " & N_StartTimeString)
        ElseIf SystemTime.Hour = D_StartTime.Hour And SystemTime.Minute < D_StartTime.Minute Then
            StartTime = CDate(SystemTime.AddDays(-1).Date & " " & N_StartTimeString)
        Else
            StartTime = CDate(SystemTime.Date & " " & D_StartTimeString)
        End If
        Return StartTime
    End Function
End Module
