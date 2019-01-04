Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Text

Public Class cryptool
    
    Public _sifreli As String

    Public Sub Sifrele(ByVal _SourceText As String)
        Try
            Dim _md5 As New MD5CryptoServiceProvider
            Dim ByteHash() As Byte
            Dim _count As Byte
            Dim _sha1 As New SHA1CryptoServiceProvider
            Dim _sBuilder As New StringBuilder()

            _sha1.Initialize()
            _md5.Initialize()
            ByteHash = _md5.ComputeHash(_sha1.ComputeHash(Encoding.UTF8.GetBytes(("ÜÐPOGIOÖÝ.Ç" + Encoding.UTF8.GetString(_md5.ComputeHash(Encoding.UTF8.GetBytes("{ð*/-ýd21" + _SourceText.Trim + "%32sði@"))) + "poöýKÖKOpðlpð"))))
            _sBuilder.Remove(0, _sBuilder.Length)

            For _count = 0 To ByteHash.Length - 1
                _sBuilder.Append((255 - (ByteHash(_count))).ToString("x2"))
            Next _count
            _sifreli = _sBuilder.ToString
            _md5.Clear()
            _sha1.Clear()
        Catch ex As Exception
        End Try
    End Sub
End Class
