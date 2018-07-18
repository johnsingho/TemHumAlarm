Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Data.SqlClient
Module Com_And_Internet_Connect
    Public HexCheck As Boolean = False
    Public RecieveHex As Boolean = False
    Public SendHex As Boolean = False
    Public client As TcpClient = Nothing
    Public Function DEC_to_HEX(ByVal Dec As Long) As String
        '将10进制转换成16进制
        Dim a As String
        DEC_to_HEX = ""
        Do While Dec > 0
            a = CStr(Dec Mod 16)
            Select Case a
                Case "10" : a = "A"
                Case "11" : a = "B"
                Case "12" : a = "C"
                Case "13" : a = "D"
                Case "14" : a = "E"
                Case "15" : a = "F"
            End Select
            DEC_to_HEX = a & DEC_to_HEX
            Dec = Dec \ 16
        Loop
    End Function
    Public Function ComSend(ByVal SendText As String, ByVal SerialPort1 As System.IO.Ports.SerialPort) As Boolean
        '表示十六进制或者其他进制进行发送
        Try
            If HexCheck = True Then
                Dim TestArray() As String = Split(SendText)
                Dim hexBytes() As Byte
                ReDim hexBytes(TestArray.Length - 1)
                Dim i As Integer
                For i = 0 To TestArray.Length - 1
                    hexBytes(i) = Val("&H" & TestArray(i))
                Next
                SerialPort1.Write(hexBytes, 0, hexBytes.Length)
                Return True
            Else
                SerialPort1.Write(SendText)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '接收数据过程
    Public Function COMRecive(ByVal SerialPort1 As System.IO.Ports.SerialPort) As String
        '接收数据，控件已经被隐藏
        '  receivebox.Text = ""
        Dim RecieveString As String = ""
        Dim strIncoming As Byte
        Try
            Threading.Thread.Sleep(100) '添加延时
            If SerialPort1.BytesToRead > 0 Then
                Dim bytes() As Byte

                ReDim bytes(SerialPort1.BytesToRead)
                'strIncoming = Convert.ToByte(SerialPort1.ReadByte())
                If RecieveHex = True Then
                    strIncoming = SerialPort1.ReadByte()
                    bytes(0) = strIncoming
                    Dim i As Int16
                    For i = 1 To SerialPort1.BytesToRead
                        strIncoming = SerialPort1.ReadByte() '读取缓冲区中的数据
                        bytes(i) = strIncoming
                    Next
                    ' SerialPort1.Write(sendbox.Text)'发送数据
                    SerialPort1.DiscardInBuffer()
                    Dim str1() As String = Split(BitConverter.ToString(bytes), "-")
                    Dim str2(str1.Length - 1) '去除str1中最后的字符
                    For i = 0 To str1.Length - 2
                        str2(i) = str1(i)
                    Next
                    RecieveString = RecieveString & Join(str2, " ")
                Else
                    RecieveString = RecieveString & SerialPort1.ReadExisting()
                End If
            End If
            Return RecieveString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return RecieveString
    End Function
    '网络控件器模块!
    Public Function UDPSendRecieve(ByVal num As Integer, ByVal Number As Int16, ByVal ip() As Byte, ByVal localPort As Integer, ByVal remotehostport As Integer) As Boolean
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
    Public Function TCPSend(ByVal IP As String, ByVal SendMsg As String, ByVal NetPorts As Int16)
        Try
            If IP <> "" Then
                Dim tcpc As New System.Net.Sockets.TcpClient(IP, NetPorts)
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
        Return True
    End Function

    Public Function TCPRecieve(ByVal IP As System.Net.IPAddress, ByVal RecieveMsg As String, ByVal NetPorts As Int16)
        Dim tcpi As System.Net.Sockets.TcpListener
        Try
            tcpi = New System.Net.Sockets.TcpListener(IP, NetPorts)
            tcpi.Start()
            While True
                Dim s As System.Net.Sockets.Socket = tcpi.AcceptSocket()
                Dim MyBuffer(1024) As Byte
                Dim i As Int16
                i = s.Receive(MyBuffer)
                If i > 0 Then

                    Dim j As Int16
                    For j = 0 To i - 1
                        RecieveMsg += Chr(MyBuffer(j)) & ","
                    Next
                End If
            End While
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Return RecieveMsg
    End Function
    Public Function UDPSend(ByVal IP As String, ByVal SendMsg As String, ByVal NetPorts As Int16)
        Try
            If IP <> "" Then
                Dim UDPClient As New System.Net.Sockets.UdpClient(IP, NetPorts)
                Dim SendBytes As [Byte]() = Encoding.ASCII.GetBytes(SendMsg)
                UDPClient.Send(SendBytes, SendBytes.Length)
                UDPClient.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Return True
    End Function
    Public Function UDPRecieve(ByVal IP As String, ByVal RecieveMsg As String, ByVal NetPorts As Int16)
        Try
            Dim UDPClient As New System.Net.Sockets.UdpClient(IP, NetPorts)
            Dim RemoteIpEndPoint As New IPEndPoint(IP, NetPorts)
            Dim RecieveBytes As [Byte]() = UDPClient.Receive(RemoteIpEndPoint)
            RecieveMsg = Encoding.ASCII.GetString(RecieveBytes)
            UDPClient.Close()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        Return RecieveMsg
    End Function

    ' 发送指令给设备，读取返回值
    ' TODO 远程端口可能不是5300
    Public Function Connect(server As [String], message As [String], remotePort As Int32) As String
        Dim responseData As [String] = [String].Empty
        Dim LocationString As String = RetrunLocation_num(Val(SendDataGroup(MacInt)))
        Dim stream As NetworkStream = Nothing
        Dim port As Int32 = remotePort

        Try
            ' Create a TcpClient.
            ' Note, for this client to work you need to have a TcpServer 
            ' connected to the same address as specified by the server, port
            ' combination.
            client = New TcpClient(server, port)

            ' Translate the passed message into ASCII and store it as a Byte array.
            Dim data As [Byte]() = Nothing 'System.Text.Encoding.ASCII.GetBytes(message)
            ' Get a client stream for reading and writing.
            '  Stream stream = client.GetStream();
            stream = client.GetStream()
            ' Send the message to the connected TcpServer. 
            If SendHex = True Then
                Dim TestArray() As String = Split(message)
                Dim hexBytes() As Byte
                ReDim hexBytes(TestArray.Length - 1)
                System.Threading.Thread.Sleep(10)
                client.SendTimeout = 1000
                Dim i As Integer
                For i = 0 To TestArray.Length - 1
                    hexBytes(i) = Val("&H" & TestArray(i))
                Next
                data = hexBytes
                Try
                    stream.Write(data, 0, data.Length)
                Catch ex As Exception
                    Form1.TextBox2.Text &= ex.Message & Environment.NewLine
                    If SendType = 5 And RunningLocation = "自动上传" Then
                        If ErrList = "" Then
                            ErrList = LocationString & vbTab & Now & vbTab & "发信息命令时出错！" & vbCrLf
                        Else
                            ErrList = ErrList & LocationString & vbTab & Now & vbTab & "发信息命令时出错！" & vbCrLf
                        End If
                    End If
                    If client IsNot Nothing Then
                        If client.Connected = True Then
                            client.Close()

                        End If
                    End If
                End Try
            Else
                Dim tcpStream As Net.Sockets.NetworkStream = client.GetStream
                Dim reqStream As New IO.StreamWriter(tcpStream)
                Try
                    reqStream.Write(message)
                Catch ex As Exception
                    Form1.TextBox2.Text &= ex.Message & Environment.NewLine
                    If SendType = 5 And RunningLocation = "自动上传" Then
                        If ErrList = "" Then
                            ErrList = LocationString & vbTab & Now & vbTab & "发信息命令时出错！" & vbCrLf
                        Else
                            ErrList = ErrList & LocationString & vbTab & Now & vbTab & "发信息命令时出错！" & vbCrLf
                        End If
                    End If
                    If client IsNot Nothing Then
                        If client.Connected = True Then
                            client.Close()
                        End If
                    End If
                End Try
            End If
            Console.WriteLine("Sent: {0}", message)
            System.Threading.Thread.Sleep(200)
            ' Receive the TcpServer.response.
            ' Buffer to store the response bytes.
            ' String to store the response ASCII representation.
            '  Dim a As System.Net.Sockets.Socket
            data = New [Byte](1024) {}
            client.ReceiveTimeout = 2000
            ' Read the first batch of the TcpServer response bytes.
            Dim bytes As Int32 = 0
            Try
                bytes = stream.Read(data, 0, data.Length)
            Catch ex As Exception
                If SendType = 5 And RunningLocation = "自动上传" Then
                    If ErrList = "" Then
                        ErrList = LocationString & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "读取信息命令时出错！" & vbCrLf
                    Else
                        ErrList = ErrList & LocationString & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "读取信息命令时出错！" & vbCrLf
                    End If
                End If
                If client IsNot Nothing Then
                    If client.Connected = True Then
                        client.Close()

                    End If

                End If
            End Try
            Dim Data1(bytes) As Byte
            For i As Int32 = 0 To bytes - 1
                Data1(i) = data(i)
            Next
            '  responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            Dim str1() As String = Split(BitConverter.ToString(Data1), "-")
            Dim str2(str1.Length - 1) '去除str1中最后的字符
            For i As Int32 = 0 To str1.Length - 2
                str2(i) = str1(i)
            Next
            responseData = responseData & Join(str2, " ")
            stream.Close()
            client.Close()

        Catch e As ArgumentNullException
            Form1.TextBox2.Text &= e.Message & Environment.NewLine
            If client IsNot Nothing Then
                If client.Connected = True Then
                    client.Close()
                End If
            End If
            If SendType = 5 And RunningLocation = "自动上传" Then
                If e.Message <> "" Then
                    ErrList = LocationString & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & e.Message & Environment.NewLine & vbCr
                Else
                    ErrList = ErrList & LocationString & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & e.Message & Environment.NewLine & vbCr
                End If
            End If
        Catch e As SocketException
            'Form1.TextBox2.Text &= e.Message & Environment.NewLine 
            Dim str As String
            str = "与 " & server & ":" & port & " 通讯失败"
            Form1.TextBox2.Text &= Environment.NewLine & str & Environment.NewLine
            If client IsNot Nothing Then
                If client.Connected = True Then
                    client.Close()
                End If
            End If
            If SendType = 5 And RunningLocation = "自动上传" Then
                If ErrList = "" Then
                    ErrList = LocationString & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "链接网络时出错！" & vbCr
                Else
                    ErrList = ErrList & LocationString & vbTab & Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & "链接网络时出错！" & vbCr
                End If
            End If
        End Try
        Return responseData
    End Function

#Region "TCP通信模块"
    ''' <summary> 
    ''' TCP通信模块
    ''' </summary> 
    ''' <param name="server">输入目IP地址！</param>    
    ''' <param name="message">输入要发送的字符串！</param> 
    ''' <param name="port">设备通信端口！</param> 
    ''' <param name="SendHex">是否按16进制发送！</param> 
    ''' <remarks></remarks>
    Public Function TCPConnect(server As [String], message As [String], port As Int32, SendHex As Boolean) As String
        Dim responseData As [String] = [String].Empty
        Dim stream As NetworkStream = Nothing
        Try
            ' Create a TcpClient.
            ' Note, for this client to work you need to have a TcpServer 
            ' connected to the same address as specified by the server, port
            ' combination.
            client = New TcpClient(server, port)
            ' Translate the passed message into ASCII and store it as a Byte array.
            Dim data As [Byte]() = Nothing 'System.Text.Encoding.ASCII.GetBytes(message)
            ' Get a client stream for reading and writing.
            '  Stream stream = client.GetStream();
            stream = client.GetStream()
            ' Send the message to the connected TcpServer. 
            If SendHex = True Then
                Dim TestArray() As String = Split(message)
                Dim hexBytes() As Byte
                ReDim hexBytes(TestArray.Length - 1)
                System.Threading.Thread.Sleep(10)
                client.SendTimeout = 1000
                Dim i As Integer
                For i = 0 To TestArray.Length - 1
                    hexBytes(i) = Val("&H" & TestArray(i))
                Next
                data = hexBytes
                Try
                    stream.Write(data, 0, data.Length)
                Catch ex As Exception
                    If client IsNot Nothing Then
                        If client.Connected = True Then
                            client.Close()
                        End If
                    End If
                End Try
            Else
                Dim tcpStream As Net.Sockets.NetworkStream = client.GetStream
                Dim reqStream As New IO.StreamWriter(tcpStream)
                Try
                    reqStream.Write(message)
                Catch ex As Exception
                    If client IsNot Nothing Then
                        If client.Connected = True Then
                            client.Close()
                        End If
                    End If
                End Try
            End If
            Console.WriteLine("Sent: {0}", message)
            System.Threading.Thread.Sleep(200)
            ' Receive the TcpServer.response.
            ' Buffer to store the response bytes.
            ' String to store the response ASCII representation.
            '  Dim a As System.Net.Sockets.Socket
            data = New [Byte](1024) {}
            client.ReceiveTimeout = 2000
            ' Read the first batch of the TcpServer response bytes.
            Dim bytes As Int32 = 0
            Try
                bytes = stream.Read(data, 0, data.Length)  '没有这样的环境，所以此处报警没办法测试
            Catch ex As Exception
                If client IsNot Nothing Then
                    If client.Connected = True Then
                        client.Close()
                    End If
                End If
            End Try
            Dim Data1(bytes) As Byte
            For i As Int32 = 0 To bytes - 1
                Data1(i) = data(i)
            Next
            '  responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes)
            Dim str1() As String = Split(BitConverter.ToString(Data1), "-")
            Dim str2(str1.Length - 1) '去除str1中最后的字符
            For i As Int32 = 0 To str1.Length - 2
                str2(i) = str1(i)
            Next
            responseData = responseData & Join(str2, " ")
            stream.Close()
            client.Close()
        Catch e As ArgumentNullException
            If client IsNot Nothing Then
                If client.Connected = True Then
                    client.Close()
                End If
            End If
        Catch e As SocketException
            If client IsNot Nothing Then
                If client.Connected = True Then
                    client.Close()
                End If
            End If

        End Try
        Return responseData
    End Function
#End Region


End Module
