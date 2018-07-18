Imports Microsoft.Office.Interop

Public Class Download_Window
    Public x, y As Single
    Public SaveTime As Date = Nothing
    Public RunDownloadTime As Date = Nothing
    Public SaveMaxint As Int64 = 0
    Public DownLoadDatagridview As DataGridView
    Public StartColumn As Int16 = 0
    Dim ProValue As Int64 = 0
    Dim t1 As Threading.Thread
    Private Sub Download_Window_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        DownloadWindownIsOpen = False
    End Sub
    Private Sub Download_Window_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox3.Focus()
        Timer3.Interval = 1000
        Timer3.Enabled = True
        x = Me.Width
        y = Me.Height
        setTag(Me)
        VDownloadDataGridView = DownLoadDatagridview
        DownloadWindownIsOpen = True
        Me.Location = New Point(0, 0)
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

        For Each con As Control In obj.Controls

            con.AutoSize = True

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
    Public Sub Finddate()
        If DownLoadDatagridview IsNot Nothing Then
            Me.FastDataToExcel(DownLoadDatagridview, StartColumn)
        End If

    End Sub

    Public Sub DownloadFile()
        Me.Show()
        SaveTime = Now
        t1 = New Threading.Thread(AddressOf Finddate)
        t1.Start()
        'Me.Close()
    End Sub

    Public Function FastDataToExcel(ByVal Datagridview1 As DataGridView, ByVal StartColumn As Int16) As Boolean
        Try
            SaveTime = Now
            Dim arr As Array = Array.CreateInstance(GetType(String), Datagridview1.RowCount, Datagridview1.ColumnCount - StartColumn)
            Dim Rows As Int64 = Datagridview1.RowCount - 2
            If Rows = -2 Then
                Return False
            End If
            Dim MyExcel As New Microsoft.Office.Interop.Excel.Application()
            '获取标题   
            Dim Cols As Integer
            Dim XlsBook As Excel.Workbook = MyExcel.Application.Workbooks.Add()
            Dim XlsSheet As Excel.Worksheet = XlsBook.Sheets("Sheet1")
            RunDownloadTime = Now
            For Cols = StartColumn To Datagridview1.Columns.Count - 1
                MyExcel.Cells(1, Cols - StartColumn + 1) = Datagridview1.Columns(Cols).HeaderText
            Next
            '往excel表里添加数据()   
            Dim Column As Int64 = Datagridview1.ColumnCount - StartColumn
            Dim i As Integer
            If Datagridview1.RowCount = 1 Then
                SaveMaxint = Datagridview1.RowCount - 2 + 12
            Else
                SaveMaxint = Datagridview1.RowCount - 2 + 11
            End If
            MyExcel.Visible = False
            For i = 0 To Datagridview1.RowCount - 2
                ProValue = i
                Dim j As Integer
                For j = StartColumn To Datagridview1.ColumnCount - 1
                    If Datagridview1(j, i).Value Is System.DBNull.Value Then
                        MyExcel.Cells(i + 2, j + 1 - StartColumn) = ""
                    Else
                        If Datagridview1(j, i).Value IsNot DBNull.Value Then
                            Dim oo = Trim(Datagridview1(j, i).Value.ToString)
                            MyExcel.Cells(i + 2, j + 1 - StartColumn) = Trim(Datagridview1(j, i).Value.ToString)
                            Dim pp = MyExcel.Cells(i + 2, j + 1 - StartColumn)
                            Dim kk = 0
                        Else
                            MyExcel.Cells(i + 2, j + 1 - StartColumn) = ""
                        End If
                    End If
                Next (j)
            Next (i)
            Dim rb As Excel.Range = XlsSheet.Range(XlsSheet.Cells(1, 1), XlsSheet.Cells(XlsSheet.Rows.Count, XlsSheet.Columns.Count))
            rb.Interior.Color = Color.White
            Dim rn As Excel.Range = XlsSheet.Range(XlsSheet.Cells(1, 1), XlsSheet.Cells(Datagridview1.RowCount, Column))
            rn.AutoFormat()
            ProValue = ProValue + 1
            rn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            ProValue = ProValue + 1
            rn.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter
            ProValue = ProValue + 1
            rn.WrapText = False
            ProValue = ProValue + 1
            rn.Orientation = 0
            ProValue = ProValue + 1
            rn.AddIndent = False
            ProValue = ProValue + 1
            rn.IndentLevel = 0
            ProValue = ProValue + 1
            rn.ShrinkToFit = False
            ProValue = ProValue + 1
            rn.MergeCells = False
            ProValue = ProValue + 1
            rn.Borders.Color = Color.Black
            ProValue = ProValue + 1
            rn = XlsSheet.Range(XlsSheet.Cells(1, 1), XlsSheet.Cells(1, Column))
            rn.Interior.Color = Color.GreenYellow
            ProValue = ProValue + 1
            MyExcel.Visible = True
            VDownloadDataGridView = Nothing
            DownLoadDatagridview = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            Me.ProgressBar1.Maximum = SaveMaxint
            Me.ProgressBar1.Value = ProValue
            If ProValue < 1 Then
                TextBox2.Text = "正在准备下载资源..."

            ElseIf ProValue < SaveMaxint Then
                TextBox2.Text = "已用时间" & ReturnUesTimeString() & "剩余时间" & ReturnRemainTimeString()

            ElseIf ProValue >= SaveMaxint Then
                TextBox1.Text = "下载完成"
                TextBox1.ForeColor = Color.LimeGreen
                TextBox2.Text = "下载用时" & ReturnUesTimeString()
                Timer3.Enabled = False
                Timer1.Interval = 60000
                Timer1.Enabled = True
            End If

            TextBox2.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function ReturnUesTimeString() As String
        Dim UseTime As Int64 = Now.ToFileTime - SaveTime.ToFileTime
        Dim b As Single = UseTime / 10000000
        Dim HString As String = ""
        Dim MString As String = ""
        Dim SString As String = ""
        Dim H As Int32 = Math.Truncate(b / 3600)
        If H <= 9 Then
            HString = "0" & H
        Else
            HString = H
        End If
        Dim M As Int32 = Math.Truncate(b / 60)
        If M <= 9 Then
            MString = "0" & M
        Else
            MString = M
        End If
        Dim S As Int32 = b Mod 60
        If S <= 9 Then
            SString = "0" & S.ToString("0")
        Else
            SString = S.ToString("0")
        End If
        Return HString & ":" & MString & ":" & SString
    End Function


    Public Function ReturnRemainTimeString() As String
        Dim UseTime As Int64 = Now.ToFileTime - RunDownloadTime.ToFileTime
        Dim a As Single = (1 - (ProValue / SaveMaxint)) / (ProValue / SaveMaxint)
        Dim b As Single = UseTime * a / 10000000
        Dim HString As String = ""
        Dim MString As String = ""
        Dim SString As String = ""
        Dim H As Int32 = Math.Truncate(b / 3600)
        If H <= 9 Then
            HString = "0" & H
        Else
            HString = H
        End If
        Dim M As Int32 = Math.Truncate(b / 60) Mod 60
        If M <= 9 Then
            MString = "0" & M
        Else
            MString = M
        End If
        Dim S As Int32 = b Mod 60
        If S <= 9 Then
            SString = "0" & S.ToString("0")
        Else
            SString = S.ToString("0")
        End If
        Return HString & ":" & MString & ":" & SString
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Me.Close()
    End Sub
End Class