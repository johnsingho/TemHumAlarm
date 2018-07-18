Public Class Alert_Window

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SerialPort1.DtrEnable = False
        SerialPort1.Close()
        Me.Close()
    End Sub
    Public Sub ShowTimeout(errmsg As String, ms As Integer)
        TextBox1.Text = errmsg
        If ms <= 0 Then
            ms = 10 * 1000
        End If
        lblPromptClose.Text = String.Format("{0}秒后自动关闭", ms / 1000)
        lblPromptClose.Visible = True
        TimerClose.Interval = ms
        TimerClose.Start()

        Me.Show()
    End Sub

    Private Sub Alert_Window_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        AlertWindowV = False
        If SerialPort1.IsOpen = True Then
            SerialPort1.DtrEnable = False
            SerialPort1.Close()
        End If
    End Sub
    Private Sub Alert_Window_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            ' Me.TopMost = True
            Me.WindowState = FormWindowState.Maximized
            lblPromptClose.Location = New Point(TextBox1.Left, TextBox1.Bottom + 12)
            AlertWindowV = True
            SerialPort1.PortName = AlertComSting
            SerialPort1.BaudRate = 9600
            SerialPort1.DataBits = 8 '数据位
            ' SerialPort1.StopBits = IO.Ports.StopBits.One '停止位
            '  SerialPort1.Parity = IO.Ports.Parity.None '校验位
            If SerialPort1.IsOpen = False Then
                SerialPort1.Open()
            End If
            '  Exit Sub
            SerialPort1.DtrEnable = True
            If ErrList <> "" Then
                TextBox1.Text = ErrList
            End If
        Catch ex As Exception
            Form1.Running_Info.Text = "报警串口链接出错!"
            Form1.Running_Info.ForeColor = Color.Red
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TimerClose_Tick(sender As Object, e As EventArgs) Handles TimerClose.Tick
        TimerClose.Stop()
        Me.Close()
    End Sub
End Class