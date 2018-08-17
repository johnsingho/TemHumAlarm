Imports System.Drawing.Drawing2D

Public Class Data_Chart_Window
    Public AddData_1() As Single
    Public AddData_2() As Single
    Public AddData_3() As Single
    Public AddData_4() As Single
    Public AddData_5() As Single
    Public AddData_6() As Single
    Public AddString_1() As String
    Public FindInt As Int32 = 0
    Public x, y As Single
   
    Private Sub Data_Chart_Window_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Text = "8:00"
        ComboBox2.Text = "8:00"
        Me.Text = "PCBA " & WorkLocationString & "温湿度信息采集系统"
        'ComboBox3.Text = "1"
        Call ReLoadMainWindow()
        '  ConnectStr = SqlConnectString
        ' AddComboBoxItme("Select location From " & TableName & " where isopen =1", ComboBox3, "num")
        '  ComboBox3.Items.Add("All")
        DateTimePicker2.Value = Now.AddDays(1)
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
    Public Sub AddPicture1(ByVal e As System.Windows.Forms.PaintEventArgs, ByVal AddData() As Single,
                           ByVal Height As Int32, ByVal Width As Int32, ByVal MinTarget As Single,
                           ByVal AddXString() As String, ByVal MaxTarget As Single)
        'Dim MinValue As Single = AddData.Min - (AddData.Min * 0.2)
        ' Dim MaxValue As Single = AddData.Max + (AddData.Max * 0.2)

        Dim SpaceX As Integer = 40 '绘画区宽度起始值。
        Dim SpaceY As Integer = 20 '绘画区高度起始值。
        Dim interval As Integer = (Height - SpaceY) / 6  '单位长度
        Dim max_x As Single = AddData.Length  'x轴最大刻度
        Dim max_y As Single = 6
        Dim bmp As New Bitmap(Width, Height)
        Dim IntervalX As Single = (Width - SpaceX) / max_x
        Dim g As Graphics = e.Graphics
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality
        Dim pen As Pen = New Pen(Color.White, 1)
        Dim pen_chart As Pen = New Pen(Color.Blue, 2)    '在此处设置折线宽度和颜色
        '定义一组数据
        ' Dim arrData() As Double = {5.21, 3.34, 2.5, 7.65, 6.54, 1.2, 2.32, 6.48}
        Dim i As Integer
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.Clear(Color.Black)
        'g.DrawLine(pen,point1,point2)
        'Pen 对象，它确定线条的颜色、宽度和样式。 
        'Point 结构，它表示要连接的第一个点。 
        'Point 结构，它表示要连接的第二个点。 
        g.DrawLine(pen, New Point(SpaceX, Height - SpaceY), New Point(Width, Height - SpaceY)) 'x轴
        g.DrawLine(pen, New Point(SpaceX, 0), New Point(SpaceX, Height - SpaceY))  'y轴

        'x轴上的刻度
        Dim Xint As Int32 = 0
        Dim StepValue As Single = 0
        If max_x <= 20 Then
            StepValue = 1
        Else
            StepValue = Math.Truncate(max_x / 20)
        End If
        For i = 0 To Width - IntervalX Step IntervalX * StepValue
            If i <= max_x * IntervalX Then
                'g.DrawString(string,Font,Brush,PointF)
                'String对象，要绘制的字符串。 
                'Font 对象，它定义字符串的文本格式。 
                'Brush 对象，它确定所绘制文本的颜色和纹理。 
                'PointF 结构，它指定所绘制文本的左上角。
                '可以单独先定义变量drawString, drawFont, drawBrush, drawPoint
                Dim AddTxt As String = ""
                If i / IntervalX > AddXString.Length Then
                    AddTxt = ""
                Else
                    AddTxt = AddXString(Math.Truncate(i / IntervalX))
                End If

                g.DrawString(AddTxt, New Font("Arail", 9, FontStyle.Regular), Brushes.White, New PointF(SpaceX + i - 5, Height - SpaceY))

            End If
        Next
        'y轴上的刻度
        Dim f As Int16 = 0
        Dim MinInt As Single = AddData.Min * 0.9
        If MinTarget < AddData.Min Then
            MinInt = MinTarget * 0.9
        End If
        Dim MaxInt As Single = AddData.Max + AddData.Max * 0.1
        If MaxTarget > AddData.Max Then
            MaxInt = MaxTarget * 1.1
        End If

        For i = 0 To Height - interval Step interval
            If i <= max_y * interval Then
                g.DrawLine(pen, New Point(SpaceX, Height - i - SpaceY), New Point(SpaceX + 5, Height - i - SpaceY))
                If i <> 0 Then
                    Dim d As Single = (MinInt + f * ((MaxInt - MinInt) / 6)).ToString("0.00")

                    g.DrawString(d, New Font("Arial", 9, FontStyle.Regular), Brushes.White, New PointF(4, Height - SpaceY - i - 6))
                End If

            End If
            f = f + 1
        Next


        '画折线图
        For i = 0 To UBound(AddData)
            pen_chart = New Pen(Color.Black, 2)
            Dim d As Single = (AddData(i) - MinInt) / ((MaxInt - MinInt) / 6)

            g.DrawLine(pen_chart, New Point(SpaceX + i * IntervalX, Height - SpaceY - d * interval), New Point(SpaceX + (i) * IntervalX + 2, Height - SpaceY - d * interval))
            If i <> 0 Then
                pen_chart = New Pen(Color.Aqua, 3)
                Dim P As Single = (AddData(i) - MinInt) / ((MaxInt - MinInt) / 6)
                d = (AddData(i - 1) - MinInt) / ((MaxInt - MinInt) / 6)
                g.DrawLine(pen_chart, New Point(SpaceX + (i - 1) * IntervalX, Height - SpaceY - d * interval), New Point(SpaceX + (i) * IntervalX, Height - SpaceY - P * interval))
            End If
            ' g.DrawLine(pen_chart, New Point(space + i * interval, Height - space - d * interval), New Point(space + (i + 1) * interval, Height - space - d * interval))
            ' g.DrawLine(pen_chart, New Point(space + (i + 1) * interval, Height - space - arrData(i) * interval), New Point(space + (i + 1) * interval, Height - space))
        Next
        pen = New Pen(Color.Red, 3)
        Dim TargetInt As Single = (MinTarget - MinInt) / ((MaxInt - MinInt) / 6)
        g.DrawLine(pen, New Point(SpaceX, Height - SpaceY - TargetInt * interval), New Point(Width, Height - SpaceY - TargetInt * interval)) 'x轴
        TargetInt = (MaxTarget - MinInt) / ((MaxInt - MinInt) / 6)
        g.DrawLine(pen, New Point(SpaceX, Height - SpaceY - TargetInt * interval), New Point(Width, Height - SpaceY - TargetInt * interval)) 'x轴


        g.Dispose()
        bmp.Dispose()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        FindInt = 0

        If ComboBox1.Text = "" Then
            MsgBox("请选择你要查询的开始时间！")
            Exit Sub
        End If

        If ComboBox2.Text = "" Then
            MsgBox("请选择你要查询的结束时间！")
            Exit Sub

        End If
        If ComboBox3.Text = "" Then
            MsgBox("请选择你要查询的机器编号！")
            Exit Sub
        End If
        Dim StartTime As DateTime = CDate(DateTimePicker1.Text & " " & ComboBox1.Text).ToString("yyyy-MM-dd HH:mm:ss")
        Dim EndTime As DateTime = CDate(DateTimePicker2.Text & " " & ComboBox2.Text).ToString("yyyy-MM-dd HH:mm:ss")
        StartTime = StartTime.ToString("yyyy-MM-dd HH:mm:ss")
        EndTime = EndTime.ToString("yyyy-MM-dd HH:mm:ss")
      
        Dim LocationChar As String = Trim(ComboBox3.Text)
        'Dim MacInt As String = Trim(ComboBox3.Text)
        Dim MacInt As String = RetrunNum_Location(ComboBox3.Text)
        Dim dr As SqlClient.SqlDataReader = Nothing


        ConnectStr = SqlConnectString '"Data Source=10.201.62.84;Initial Catalog=erecordcontrol;Persist Security Info=True;User ID=sa;Password=TryTest:123"
        FindDataToDataGridView("Select * From " & DataSaveTableName & " Where uploadtime Between '" & StartTime.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & EndTime.ToString("yyyy-MM-dd HH:mm:ss") & "' and machineno='" & MacInt & "' order by uploadtime ", DataGridView1)
        Dim FindCount As Int32 = 0

        dr = SqlSelect("Select count(*) as a  From " & DataSaveTableName & " Where uploadtime Between '" & StartTime.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & EndTime.ToString("yyyy-MM-dd HH:mm:ss") & "' and machineno='" & MacInt & "'")
        If dr.Read Then
            If dr("a") IsNot DBNull.Value Then
                FindCount = dr("a")
            End If
        End If
        dr.Close()
        Dim AddData1(FindCount - 1) As Single
        Dim AddData2(FindCount - 1) As Single
        Dim AddData3(FindCount - 1) As Single
        Dim AddData4(FindCount - 1) As Single
        Dim AddData5(FindCount - 1) As Single
        Dim AddData6(FindCount - 1) As Single
        Dim AddString(FindCount - 1) As String
        dr = SqlSelect("Select tem,hum,detailtime,temperaturelow,temperaturehigh,Humiditylow,Humidityhigh From " & DataSaveTableName & " Where uploadtime Between '" & StartTime.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & EndTime.ToString("yyyy-MM-dd HH:mm:ss") & "' and machineno='" & MacInt & "' order by uploadtime ")
        If dr.HasRows Then
            While dr.Read
                If dr("tem") IsNot DBNull.Value Then
                    AddData1(FindInt) = dr("tem")
                Else
                    AddData1(FindInt) = 0
                End If
                If dr("hum") IsNot DBNull.Value Then
                    AddData2(FindInt) = dr("hum")
                Else
                    AddData2(FindInt) = 0
                End If
                If dr("temperaturelow") IsNot DBNull.Value Then
                    AddData3(FindInt) = dr("temperaturelow")
                Else
                    AddData3(FindInt) = 0
                End If
                If dr("temperaturehigh") IsNot DBNull.Value Then
                    AddData4(FindInt) = dr("temperaturehigh")
                Else
                    AddData4(FindInt) = 0
                End If
                If dr("Humiditylow") IsNot DBNull.Value Then
                    AddData5(FindInt) = dr("Humiditylow")
                Else
                    AddData5(FindInt) = 0
                End If
                If dr("Humidityhigh") IsNot DBNull.Value Then
                    AddData6(FindInt) = dr("Humidityhigh")
                Else
                    AddData6(FindInt) = 0
                End If
                AddString(FindInt) = dr("detailtime")
                FindInt = FindInt + 1
            End While
        End If
        AddData_1 = AddData1
        AddData_2 = AddData2
        AddData_3 = AddData3
        AddData_4 = AddData4
        AddData_5 = AddData5
        AddData_6 = AddData6
        AddString_1 = AddString

        Panel1.Invalidate()
        Panel2.Invalidate()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call FastDataToExcel(DataGridView1, 0)
    End Sub
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
        Try
            If FindInt > 1 Then
                Dim MinHumi As Single = AddData_5.Average.ToString("0.00")
                Dim MaxHumi As Single = AddData_6.Average.ToString("0.00")
                AddPicture1(e, AddData_2, Panel2.Height, Panel2.Width, MinHumi, AddString_1, MaxHumi)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Try
            If FindInt > 1 Then
                Dim MinTemp As Single = AddData_3.Average.ToString("0.00")
                Dim MaxTemp As Single = AddData_4.Average.ToString("0.00")
                AddPicture1(e, AddData_1, Panel1.Height, Panel1.Width, MinTemp, AddString_1, MaxTemp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class