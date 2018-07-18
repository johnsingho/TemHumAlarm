Module Module3
    Public Function DecryptString(inputString As String) As String
        Dim str As String = Nothing
        Dim managed As System.Security.Cryptography.RijndaelManaged = New System.Security.Cryptography.RijndaelManaged()
        Dim encoding As System.Text.ASCIIEncoding = New System.Text.ASCIIEncoding()
        Try
            Dim buffer As Byte() = New Byte() {186, 237, 232, 22, 49, 121, 195, 141, 62, 227, 111, 28, 197, 41, 49, 144, 53, 192, 110, 160, 15, 5, 196, 95, 131, 58, 66, 234, 139, 3, 29, 157}
            Dim buffer2 As Byte() = New Byte() {217, 90, 231, 80, 136, 176, 7, 90, 35, 89, 156, 20, 17, 65, 91, 73}
            Dim buffer3 As Byte() = System.Convert.FromBase64String(inputString)
            Dim buffer4 As Byte() = New Byte(buffer3.Length + 1 - 1) {}
            managed.IV = buffer2
            managed.Key = buffer
            Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream(buffer3)
            Dim stream2 As System.Security.Cryptography.CryptoStream = New System.Security.Cryptography.CryptoStream(stream, managed.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Read)
            stream2.Read(buffer4, 0, buffer4.Length)
            Dim chars As Char() = encoding.GetChars(buffer4)
            For i As Integer = 0 To chars.Length - 1
                Dim ch As Char = chars(i)
                If ch = vbNullChar Then
                    Exit For
                End If
                str += ch.ToString()
            Next
            stream2.Close()
            stream.Close()
        Catch exception As System.Exception
            Throw exception
        End Try
        Return str
    End Function
End Module
