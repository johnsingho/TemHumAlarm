Imports System.Data.SqlClient

Public Class TimeDataPage
    Public x, y As Single
    Private Sub TimeDataPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        focustext.Focus()
        Try
            GetDataTimer.Enabled = True
            Me.Text = "PCBA " & WorkLocationString & "温湿度信息采集系统"
            Dim LocationArr(0) As String
            Dim i As Integer = 0
            Dim mycon1 As String = SqlConnectString
            Dim mycon As New SqlConnection(mycon1)
            mycon.Open()
            Dim mycom As New SqlCommand("select location from " & IPAddressAndLocationManageTableName & " where isopen='1'", mycon)
            With mycom.ExecuteReader
                If .HasRows Then
                    Do While .Read
                        If Not (String.IsNullOrEmpty(.GetString(0))) Then
                            ReDim Preserve LocationArr(i)
                            LocationArr(i) = .GetString(0)
                            i = i + 1
                        End If
                    Loop
                End If
            End With
            '  For Each LabelControl1 As Control In MoniPanel.Controls
            Dim j As Int16
            Dim locationwidth As Int16 = 10
            Dim locationheight As Int16 = 10
            For j = 0 To LocationArr.Length - 1
                'If (TypeOf LabelControl1 Is Label) Then
                Dim LabelControl1 As New Label
                LabelControl1.Name = "Label" & j + 1
                LabelControl1.Text = LocationArr(j)
                LabelControl1.Visible = True
                LabelControl1.AutoSize = True
                LabelControl1.Font = New System.Drawing.Font("宋体", 9.5F, FontStyle.Bold)
                MoniPanel.Controls.Add(LabelControl1)

                If MoniPanel.Width - locationwidth < 220 Then
                    locationheight = locationheight + 30
                    locationwidth = 10
                End If
                ' locationwidth = locationwidth
                LabelControl1.Location = New Point(locationwidth, locationheight)
                LabelControl1.BringToFront()
                Dim MACTextboxString As String = "0"
                If MACTextboxString < 9 Then
                    MACTextboxString = "0" & MACTextboxString
                End If
                Dim ChksmString As String = Hex(&H23 + Val("&H" & Val(MACTextboxString)))
                Dim SendText As String = "55 AA " & MACTextboxString & " 23 " & ChksmString
                Dim IPaddress As Tuple(Of String, Int32) = RetrunAddress_Location(LocationArr(j))
                SendHex = True
                Dim RString As String = Connect(IPaddress.Item1, IPaddress.Item2, SendText)
                If RString <> "" Then
                    ' Call DisplayTem(RString)
                    Dim Str2() As String = RString.Split(" ")
                    '从设备中获取数据填写到信息表中
                    Dim tem As Single = (Val("&H" & Str2(10)) * 256 + Val("&H" & Str2(9)) - 520) / 10
                    Dim Humi As Single = (Val("&H" & Str2(12)) * 256 + Val("&H" & Str2(11)) - 100) / 10
                    Dim TextControl1 As New TextBox
                    TextControl1.Name = "Text" & j + 1
                    TextControl1.Text = tem
                    TextControl1.Visible = True
                    MoniPanel.Controls.Add(TextControl1)
                    locationwidth = LabelControl1.Width + locationwidth
                    TextControl1.Location = New Point(locationwidth, locationheight)
                    TextControl1.BringToFront()
                    TextControl1.BackColor = Color.Black
                    TextControl1.ForeColor = Color.Red
                    TextControl1.Width = 50
                    TextControl1.TextAlign = HorizontalAlignment.Center
                    TextControl1.ReadOnly = True

                    Dim TextControl2 As New TextBox
                    TextControl2.Name = "Text" & j + 2
                    TextControl2.Text = Humi
                    TextControl2.Visible = True
                    MoniPanel.Controls.Add(TextControl2)
                    locationwidth = TextControl1.Width + locationwidth
                    TextControl2.Location = New Point(locationwidth, locationheight)
                    locationwidth = locationwidth + TextControl2.Width
                    TextControl2.BringToFront()
                    TextControl2.BackColor = Color.Black
                    TextControl2.ForeColor = Color.Red
                    TextControl2.Width = 50
                    TextControl2.TextAlign = HorizontalAlignment.Center
                    TextControl2.ReadOnly = True
                End If
                'End If
                ' j = j + 1
            Next
            'Next
            mycon.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        x = Me.Width
        y = Me.Height
        setTag(Me)
        Me.Location = New Point((My.Computer.Screen.WorkingArea.Width - Me.Width) / 2, (My.Computer.Screen.WorkingArea.Height - Me.Height) / 2)
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

    Private Sub GetDataTimer_Tick_1(sender As Object, e As EventArgs) Handles GetDataTimer.Tick
        MoniPanel.Controls.Clear()
        Call TimeDataPage_Load(sender, e)
    End Sub
End Class