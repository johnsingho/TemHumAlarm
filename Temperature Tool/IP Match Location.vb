Imports System.Data.SqlClient

Public Class IP_Match_Location
    Public x, y As Single
    Dim checktext As Int16 = 0
    Private Sub IP_Match_Location_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TemStandard.Text = "温度标准"
        HumidityStandard.Text = "湿度标准"
        Me.Text = "PCBA " & WorkLocationString & "温湿度信息采集系统"
        'Deadline.Format = DateTimePickerFormat.Custom
        'Deadline.CustomFormat = "yyyy/mm/dd" '调整格式，并且去掉星期
        ' Deadline.Value = Now '日期控件显示当前系统时间
        Deadline.MinDate = Now '日期控件显示当前日期为最小日期
        ' Deadline.Format = "yyyy-mm-dd"
        ConnectStr = SqlConnectString
        Label1_IP_Point.BackColor = Color.Transparent
        Dim buttonOP4 As New DataGridViewButtonColumn
        buttonOP4.HeaderText = "保存"
        buttonOP4.Width = 50
        buttonOP4.Text = "保存"
        buttonOP4.UseColumnTextForButtonValue = True
        buttonOP4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        buttonOP4.Frozen = True
        buttonOP4.ReadOnly = True
        DataGridView1.Columns.Add(buttonOP4)

        Dim buttonOP1 As New DataGridViewButtonColumn
        buttonOP1.HeaderText = "删除"
        buttonOP1.Width = 50
        buttonOP1.Text = "删除"
        buttonOP1.UseColumnTextForButtonValue = True
        buttonOP1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        buttonOP1.Frozen = True
        buttonOP1.ReadOnly = True
        DataGridView1.Columns.Add(buttonOP1)
        RefreshDataGridView()

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

    Private Sub Button_Save_Click(sender As Object, e As EventArgs) Handles Button_Save.Click
        'Deadline.Format = DateTimePickerFormat.Custom
        'Deadline.CustomFormat = "yyy/mmm/dd"

        'Dim DeadLineDate As Date = Deadline.Value.Date
        Try
            Dim flag As Int32 = 0
            Dim connStr As String = SqlConnectString
            Dim conn As New SqlConnection(connStr)
            Cmd.Connection = conn
            If TextBox1_num.Text <> "" And TextBox_PI1.Text <> "" And TextBox_PI2.Text <> "" And TextBox_PI3.Text <> "" And TextBox_PI4.Text <> "" And TextBox_Location.Text <> "" And TemStandard.Text <> "温度标准" And HumidityStandard.Text <> "湿度标准" And MachineID.Text <> "" Then
                'Dim checkdata As String = "select count(num) from " & TableName & " where num ='" & TextBox1_num.Text & "' or IPaddress= '" & Trim(TextBox_PI1.Text) & "." & Trim(TextBox_PI2.Text) & "." & Trim(TextBox_PI3.Text) & "." & Trim(TextBox_PI4.Text) & "' or location = '" & Trim(TextBox_Location.Text) & "' or machineID= '" & Trim(MachineID.Text) & "'"
                Dim Checknum As String = "select count(num) from " & IPAddressAndLocationManageTableName & " where num ='" & TextBox1_num.Text & "'"
                Dim mycommandnum As New SqlCommand(Checknum, conn)
                Dim CheckIP As String = "select count(IPaddress) from " & IPAddressAndLocationManageTableName & " where IPaddress= '" & Trim(TextBox_PI1.Text) & "." & Trim(TextBox_PI2.Text) & "." & Trim(TextBox_PI3.Text) & "." & Trim(TextBox_PI4.Text) & "'"
                Dim mycommandIP As New SqlCommand(CheckIP, conn)
                Dim CheckLocation As String = "select count(location) from " & IPAddressAndLocationManageTableName & " where location = '" & Trim(TextBox_Location.Text) & "'"
                Dim mycommandLocation As New SqlCommand(CheckLocation, conn)
                Dim CheckMachineID As String = "select count(machineID) from " & IPAddressAndLocationManageTableName & " where machineID= '" & Trim(MachineID.Text) & "'"
                Dim mycommandMachineID As New SqlCommand(CheckMachineID, conn)
                conn.Open()
                If mycommandnum.ExecuteScalar() Then
                    MsgBox("请检查 编码 是否已经存在")
                    Exit Sub
                    'ElseIf mycommandIP.ExecuteScalar() Then
                    '  MsgBox("请检查 IP 是否已经存在")
                    '   Exit Sub
                ElseIf mycommandLocation.ExecuteScalar() Then
                    MsgBox("请检查 设备位置 是否已经存在")
                    Exit Sub
                ElseIf mycommandMachineID.ExecuteScalar() Then
                    MsgBox("请检查 设备ID 是否已经存在")
                    Exit Sub
                Else
                    Dim Inserttbl As String = "insert into " & IPAddressAndLocationManageTableName & " (num,IPaddress,location,standard,machineID,deadline,Mac) values(N'" & TextBox1_num.Text & "',N'" & Trim(TextBox_PI1.Text) & "." & Trim(TextBox_PI2.Text) & "." & Trim(TextBox_PI3.Text) & "." & Trim(TextBox_PI4.Text) & "',N'" & Trim(TextBox_Location.Text) & "',N'" & Trim(TemStandard.Text) & "  " & Trim(HumidityStandard.Text) & "',N'" & Trim(MachineID.Text) & "',N'" & Deadline.Value.ToString("yyyy-MM-dd") & "',N'" & MacText.Text & "')"
                    Dim sqlcmd As New SqlCommand(Inserttbl, conn)
                    sqlcmd.ExecuteNonQuery()
                    flag = 1
                    ' Dim ds1 As New DataSet()
                    ' Dim da As New SqlDataAdapter(Inserttbl, conn)
                    'da.Fill(ds1, TableName)
                    'If ds1.Tables(0).Rows.Count > 0 Then
                    '  TextBox1_num.Text = ""
                    'TextBox_PI1.Text = ""
                    '' TextBox_PI2.Text = ""
                    'TextBox_PI3.Text = ""
                    ' TextBox_PI4.Text = ""
                    ' TextBox_Location.Text = ""
                    'TemStandard.Text = "温度标准"
                    ' HumidityStandard.Text = "湿度标准"
                    ' MachineID.Text = ""
                    Deadline.Text = Date.Now
                    RefreshDataGridView()
                    'Else
                End If
            Else
                MsgBox("请填写完所有信息再点击确定保存")
            End If
            If flag = 1 Then
                MsgBox("成功插入数据")
            Else
                MsgBox("插入数据失败")
            End If
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            'Call DataGridView1_KeyPress(DataGridView1.CurrentCell.Value)
            If e.RowIndex < 0 Or e.ColumnIndex < 0 Then
                Exit Sub
            End If
            If e.ColumnIndex > 2 Then
                Exit Sub
            End If
            Dim SaveID As Int64 = 0
            If DataGridView1(0, e.RowIndex) IsNot DBNull.Value Then
                SaveID = DataGridView1(2, e.RowIndex).Value
            End If
            Dim Num As Int64 = Trim(Val(DataGridView1(3, e.RowIndex).Value))
            Dim IPAddress As String = Trim(DataGridView1(4, e.RowIndex).Value)
            Dim Port As Int32 = Trim(DataGridView1(5, e.RowIndex).Value)
            Dim Location As String = Trim(DataGridView1(6, e.RowIndex).Value)
            Dim Standard As String = Trim(DataGridView1(7, e.RowIndex).Value)
            Dim MachineID As String = Trim(DataGridView1(8, e.RowIndex).Value)
            Dim Deadine As String = Trim(DataGridView1(9, e.RowIndex).Value)
            'ConnectStr = SqlConnectString
            If e.ColumnIndex = 0 Then
                SqlUPDATE("Update " & IPAddressAndLocationManageTableName & " Set num=N'" & Num & "',IPaddress=N'" & IPAddress & "',Port=N'" & Port &
                          "',location=N'" & Location & "' ,standard=N'" & Standard & "'  ,machineID=N'" & MachineID & "' ,deadline=N'" & Deadine & "',isopen=N'" & DataGridView1(10, e.RowIndex).Value & "',Mac=N'" & DataGridView1(11, e.RowIndex).Value & "' Where ID='" & SaveID & "'")
                Call ReLoadMainWindow()
            ElseIf e.ColumnIndex = 1 Then
                SqlDelete("Delete From " & IPAddressAndLocationManageTableName & " Where ID='" & SaveID & "'")
                Call ReLoadMainWindow()
            End If
            RefreshDataGridView()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RefreshDataGridView()
        FindDataToDataGridView("Select ID,num as 编号,IPaddress as IP,Port as 端口, location as 设备位置,standard as 标准,machineID as 设备ID,deadline as 到期日期,isopen as 开启,Mac from " & IPAddressAndLocationManageTableName & " order by convert(int,num)", DataGridView1)
    End Sub

    Private Sub DataGridView1_CellValueChanged_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Try
            Dim connStr As String = SqlConnectString
            Dim conn As New SqlConnection(connStr)
            Cmd.Connection = conn
            conn.Open()
            Dim i As Int16 = DataGridView1.CurrentCellAddress.X
            If i = 3 Then
                Dim Num As Int32 = Trim(Val(DataGridView1(3, e.RowIndex).Value))
                Dim CheckGridnum As String = "select count(num) from " & IPAddressAndLocationManageTableName & " where num ='" & Num & "'"
                Dim mycommandGridnum As New SqlCommand(CheckGridnum, conn)
                If Num <= 0 Then
                    MsgBox("编码 只能输入大于0的数字")
                    RefreshDataGridView()
                    Exit Sub
                End If
                If mycommandGridnum.ExecuteScalar() Then
                    MsgBox("请检查 编码 是否已经存在")
                    RefreshDataGridView()
                    Exit Sub
                End If
            ElseIf i = 4 Then
                Dim IPAddress As String = Trim(DataGridView1(4, e.RowIndex).Value)
                Dim CheckGridIP As String = "select count(IPaddress) from " & IPAddressAndLocationManageTableName & " where IPaddress= '" & IPAddress & "'"
                Dim mycommandGridIP As New SqlCommand(CheckGridIP, conn)
                If Not System.Text.RegularExpressions.Regex.IsMatch(IPAddress, "^(?:(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))$") Then
                    'sender.text 表示获取当前输入的内容值
                    MsgBox(" '" & IPAddress & " '服务器地址输入不正确。")
                    'IPAddress.Focus()
                    DataGridView1.BeginEdit(True)
                    RefreshDataGridView()
                    Exit Sub
                ElseIf mycommandGridIP.ExecuteScalar() Then
                    MsgBox("请检查 IP 是否已经存在")
                    RefreshDataGridView()
                    Exit Sub
                End If
            ElseIf i = 6 Then
                Dim Location As String = Trim(DataGridView1(5, e.RowIndex).Value)
                Dim CheckGridLocation As String = "select count(location) from " & IPAddressAndLocationManageTableName & " where location = '" & Location & "'"
                Dim mycommandGridLocation As New SqlCommand(CheckGridLocation, conn)
                If mycommandGridLocation.ExecuteScalar() Then
                    MsgBox("请检查 设备位置 是否已经存在")
                    RefreshDataGridView()
                    Exit Sub
                End If
            ElseIf i = 8 Then
                Dim MachineID As String = Trim(DataGridView1(7, e.RowIndex).Value)
                Dim CheckGridMachineID As String = "select count(machineID) from " & IPAddressAndLocationManageTableName & " where machineID= '" & MachineID & "'"
                Dim mycommandGridMachineID As New SqlCommand(CheckGridMachineID, conn)
                If mycommandGridMachineID.ExecuteScalar() Then
                    MsgBox("请检查 设备ID 是否已经存在")
                    RefreshDataGridView()
                    Exit Sub
                End If
            ElseIf i = 9 Then
                Dim Deadine As String = Trim(DataGridView1(8, e.RowIndex).Value)
                If Not IsDate(Deadine & " 00:00:00") Then
                    MsgBox("日期输入不合法，请重新输入")
                    RefreshDataGridView()
                    Exit Sub
                End If
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Class ClsDataGridViewPage

        Private _RowsPerPage As Integer '每页记录数
        Private _TotalPage As Integer '总页数
        Private _curPage As Integer = 0 '当前页数
        Private _DataGridView As Windows.Forms.DataGridView '要分页的DataGridView
        Private _dv As DataView '与需要分页显示的的DataView

        Public Property RowSsPerPage() As Integer '获取与设置每页记录数
            Get
                Return _RowsPerPage
            End Get
            Set(ByVal value As Integer)
                _RowsPerPage = value
            End Set
        End Property

        '获取总页数
        Public ReadOnly Property TotalPage() As Integer
            Get
                Return _TotalPage
            End Get
        End Property

        '获取与设置当前页数
        Public Property curPage() As Integer
            Get
                Return _curPage
            End Get
            Set(ByVal value As Integer)
                _curPage = value
            End Set
        End Property

        '设置需要分页的GetDataGridView
        Public WriteOnly Property SetDataGridView()
            Set(ByVal value)
                _DataGridView = value
            End Set
        End Property

        '设置需要分页显示的的DataView
        Public WriteOnly Property SetDataView()
            Set(ByVal value)
                _dv = value
            End Set
        End Property

        Public Sub New()

        End Sub

        '重载NEW函数，在构造时就可以对成员赋值
        Public Sub New(ByVal datagridview As Windows.Forms.DataGridView, ByVal dv As DataView, ByVal RowsPerPage As Integer)
            _DataGridView = datagridview
            _dv = dv
            _RowsPerPage = RowsPerPage
        End Sub

        '开始分页啦
        Public Sub Paging()
            '首先判断DataView中的记录数是否足够形成多页，
            '如果不能，那么就只有一页，且DataGridView需要显示的记录等同于“最后一页”的记录
            If _dv.Count <= _RowsPerPage Then
                _TotalPage = 1
                GoLastPage()
                Exit Sub
            End If

            '可以分为多页的话就要计算总的页数咯，然后DataGridView显示第一页
            If _dv.Count Mod _RowsPerPage = 0 Then
                _TotalPage = Int(_dv.Count / _RowsPerPage)
            Else
                _TotalPage = Int(_dv.Count / _RowsPerPage) + 1
            End If
            GoFirstPage()
        End Sub

        '到第一页
        Public Sub GoFirstPage()
            '如果只有一页，那么显示的记录等同于“最后一页”的记录
            If _TotalPage = 1 Then
                GoLastPage()
                Exit Sub
            End If
            '如果有多页，就到第“1”页
            _curPage = 0
            GoNoPage(_curPage)
        End Sub

        Public Sub GoNextPage()
            '这段代码主要是为了防止当前页号溢出
            _curPage += 1
            If _curPage > _TotalPage - 1 Then
                _curPage = _TotalPage - 1
                Exit Sub
            End If

            '如果到了最后一页，那就显示最后一页的记录
            If _curPage = _TotalPage - 1 Then
                GoLastPage()
                Exit Sub
            End If

            '如果没到最后一页，就到指定的“下一页”
            GoNoPage(_curPage)
        End Sub

        Public Sub GoPrevPage()
            '防止不合法的当前页号
            _curPage -= 1
            If _curPage < 0 Then
                _curPage = 0
                Exit Sub
            End If

            '到指定的“上一页”
            GoNoPage(_curPage)
        End Sub

        '到最后一页
        Public Sub GoLastPage()
            _curPage = _TotalPage - 1
            Dim i As Integer
            Dim dt As New DataTable
            'dt只是个临时的DataTable，用来获取所需页数的记录
            dt = _dv.ToTable.Clone

            For i = (_TotalPage - 1) * _RowsPerPage To _dv.Count - 1
                'i值上下限很关键，调试的时候常常这里报错找不到行
                '就是因为i值溢出
                Dim dr As DataRow = dt.NewRow
                dr.ItemArray = _dv.ToTable.Rows(i).ItemArray
                dt.Rows.Add(dr)
            Next
            _DataGridView.DataSource = dt
        End Sub

        '到指定的页
        Public Sub GoNoPage(ByVal PageNo As Integer)
            _curPage = PageNo
            '防止不合法的页号
            If _curPage < 0 Then
                MsgBox("页号不能小于1")
                Exit Sub
            End If

            '防止页号溢出
            If _curPage >= _TotalPage Then
                MsgBox("页号超出上限")
                Exit Sub
            End If

            '如果页号是最后一页，就显示最后一页
            If _curPage = _TotalPage - 1 Then
                GoLastPage()
                Exit Sub
            End If

            '不是最后一页，那显示指定页号的页
            Dim dt As New DataTable
            dt = _dv.ToTable.Clone
            Dim i As Integer
            For i = PageNo * _RowsPerPage To (PageNo + 1) * _RowsPerPage - 1
                Dim dr As DataRow = dt.NewRow
                dr.ItemArray = _dv.ToTable.Rows(i).ItemArray
                dt.Rows.Add(dr)
            Next
            _DataGridView.DataSource = dt
        End Sub

    End Class

    Private Sub TextBox_PI1_Leave(sender As Object, e As EventArgs) Handles TextBox_PI1.Leave
        If (Val(TextBox_PI1.Text) < 1 Or Val(TextBox_PI1.Text) > 233) And TextBox_PI1.Text <> "" Then
            MsgBox("'" & TextBox_PI1.Text & "'并不是一个有效值，请输入1-253 S之间的数字")
            TextBox_PI1.Focus()
            TextBox_PI1.Text = ""

        End If
    End Sub


    Private Sub TextBox_PI2_Leave(sender As Object, e As EventArgs) Handles TextBox_PI2.Leave
        If (Val(TextBox_PI2.Text) < 0 Or Val(TextBox_PI2.Text) > 255) And TextBox_PI2.Text <> "" Then
            MsgBox("'" & TextBox_PI2.Text & "'并不是一个有效值，请输入0-255 之间的数字")
            TextBox_PI2.Focus()
            TextBox_PI2.Text = ""
        End If
    End Sub

    Private Sub TextBox_PI3_Leave(sender As Object, e As EventArgs) Handles TextBox_PI3.Leave
        If (Val(TextBox_PI3.Text) < 0 Or Val(TextBox_PI3.Text) > 255) And TextBox_PI3.Text <> "" Then
            MsgBox("'" & TextBox_PI3.Text & "'并不是一个有效值，请输入0-255 之间的数字")
            TextBox_PI3.Focus()
            TextBox_PI3.Text = ""

        End If
    End Sub

    Private Sub TextBox_PI4_Leave(sender As Object, e As EventArgs) Handles TextBox_PI4.Leave
        If (Val(TextBox_PI4.Text) < 0 Or Val(TextBox_PI4.Text) > 255) And TextBox_PI4.Text <> "" Then
            MsgBox("'" & TextBox_PI4.Text & "'并不是一个有效值，请输入0-255 之间的数字")
            TextBox_PI4.Focus()
            TextBox_PI4.Text = ""
        End If
    End Sub
    Private Sub TextBox1_num_Leave(sender As Object, e As EventArgs) Handles TextBox1_num.Leave
        If Val(TextBox1_num.Text) <= 0 And TextBox1_num.Text <> "" Then
            MsgBox("'" & TextBox1_num.Text & "'并不是一个有效值，请输入大于0的数字")
            TextBox1_num.Focus()
            TextBox1_num.Text = ""
        End If
    End Sub

    Private Sub DownloadButton_Click(sender As Object, e As EventArgs) Handles DownloadButton.Click
        FastDataToExcel(DataGridView1, 2)
    End Sub




End Class