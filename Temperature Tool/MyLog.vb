
Imports System.IO

Module MyLog
    Private logPath As String

    Public Sub Init_log(ByRef logfilename As String)
        logPath = Application.StartupPath & "\" & logfilename
    End Sub

    Public Sub Write_log(ByRef str As String)
        MyWriteTXT(logPath, str)
    End Sub

    Public Sub Write_Exp(ByRef str As String, ByRef ex As Exception)
        MyWriteTXT(logPath, str)
        MyWriteTXT(logPath, ex.Message)
    End Sub


    Private Sub MyWriteTXT(ByVal FileName As String, ByRef AddText As String)
        Dim Reportfs As New FileStream(FileName, FileMode.Append)
        Dim Reportsw As New StreamWriter(Reportfs)
        Reportfs.Seek(0, SeekOrigin.End)
        Reportsw.WriteLine(AddText.ToString)
        Reportsw.Flush()
        Reportsw.Close()
    End Sub
End Module