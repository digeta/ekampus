Imports Microsoft.VisualBasic
Imports System.Security.Cryptography

Public Class mdV
    ' Hash an input string and return the hash as
    ' a 32 character hexadecimal string.
    Public Function getMd5Hash(ByVal input As String) As String
        Try
            ' Create a new instance of the MD5 object.
            Dim md5Hasher As MD5 = MD5.Create()

            ' Convert the input string to a byte array and compute the hash.
            Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))

            ' Create a new Stringbuilder to collect the bytes
            ' and create a string.
            Dim sBuilder As New StringBuilder()

            ' Loop through each byte of the hashed data 
            ' and format each one as a hexadecimal string.
            Dim i As Integer
            For i = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next i

            ' Return the hexadecimal string.
            Return sBuilder.ToString()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
