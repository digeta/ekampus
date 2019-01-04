Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Diagnostics
Imports System.Text
Imports System.Web
Imports System.Configuration

''' <summary> 
''' Upload handler for uploading files. 
''' </summary> 
Public Class Upload
    Implements IHttpHandler

    Private uploadFile As HttpPostedFile
    'Private dbConn As New dConn(0)
    'Private conn As New SqlConnection(dbConn.ConnectionString)
    Private conn As New SqlConnection(ConfigurationManager.AppSettings("conn0"))

    Public Sub New()
    End Sub

#Region "IHttpHandler Members"

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim dosyavar As Boolean = False
            Dim dizin As String = ""


            If context.Request.QueryString("Dizin") IsNot Nothing Then
                dizin = context.Request.QueryString("Dizin")
            Else
                dizin = "diger"
            End If

            If context.Request.Files.Count > 0 Then
                Dim mainFolder As String = ConfigurationManager.AppSettings("UploadDir")
                Dim uploadFolder As String = mainFolder & "Upload\" & dizin
                Dim swfFolder As String = mainFolder & "UploadSWF\" & dizin

                For j As Integer = 0 To context.Request.Files.Count - 1
                    uploadFile = context.Request.Files(j)

                    If uploadFile.ContentLength > 0 Then
                        Try
                            Dim folder As New DirectoryInfo(uploadFolder)
                            If Not folder.Exists Then Directory.CreateDirectory(uploadFolder)
                            Dim folderSWF As New DirectoryInfo(swfFolder)
                            If Not folderSWF.Exists Then Directory.CreateDirectory(swfFolder)
                            Dim dosyax As New FileInfo(uploadFolder & "\" & uploadFile.FileName)

                            Dim command As New SqlCommand("stp_DOSYA_KONTROL", conn)
                            command.CommandType = Data.CommandType.StoredProcedure

                            Dim fileName As String = Replace(Replace(Replace(uploadFile.FileName.ToString, "'", ""), Chr(0), ""), "-", "")
                            With command.Parameters
                                .AddWithValue("KISINO", CType(dizin, Long))   'Kurumsicil
                                .AddWithValue("DOSYA_AD", fileName.Trim)
                            End With

                            Dim reader As SqlDataReader
                            If conn.State <> Data.ConnectionState.Open Then conn.Open()
                            reader = command.ExecuteReader

                            If reader.HasRows = True Then
                                dosyavar = True
                                'Dim dosyasay As Integer = reader("DERSSAY")
                                'If dosyasay > 0 Then dosyavar = True Else dosyavar = False
                            End If
                            reader.Close()
                            If conn.State = Data.ConnectionState.Open Then conn.Close()
                        Catch _excp As Exception
                            dosyavar = True
                        Finally
                            If conn.State = Data.ConnectionState.Open Then conn.Close()
                        End Try

                        If Not dosyavar Then
                            uploadFile.SaveAs(String.Format("{0}{1}{2}", mainFolder, ("Upload\" + dizin + "\"), uploadFile.FileName))
                        End If

                        If Not dosyavar Then
                            Try
                                Dim avsonuc As Integer = scanForViruses(uploadFolder & "\" & uploadFile.FileName)
                                'If avsonuc = 0 Then
                                Dim ipno As String = Trim(CType(context.Request.ServerVariables("REMOTE_ADDR"), String))
                                If ipno Is Nothing Then ipno = "0.0.0.0"
                                Dim flashsonuc As Integer = 0
                                Dim swfFile As String = ""

                                Dim extension As String = uploadFile.FileName.Remove(0, uploadFile.FileName.LastIndexOf("."))
                                If extension.ToLower = ".zip" Or extension.ToLower = ".rar" Then
                                    flashsonuc = 3
                                Else
                                    flashsonuc = 2
                                End If

                                Dim command As New SqlCommand("stp_DOSYA_EKLE", conn)
                                command.CommandType = Data.CommandType.StoredProcedure

                                With command.Parameters
                                    .AddWithValue("KISINO", CType(dizin, Long))
                                    .AddWithValue("DOSYA_AD", uploadFile.FileName)
                                    .AddWithValue("BOYUT", uploadFile.ContentLength)
                                    .AddWithValue("IP", ipno)
                                    .AddWithValue("AV", avsonuc)
                                    .AddWithValue("SWF", flashsonuc)
                                End With

                                If conn.State <> Data.ConnectionState.Open Then conn.Open()
                                command.ExecuteNonQuery()
                                If conn.State = Data.ConnectionState.Open Then conn.Close()
                                'End If
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Public Function scanForViruses(ByVal fileLocation As String) As Integer
        Try
            Dim konsol As New Process
            konsol.StartInfo.FileName = ConfigurationManager.AppSettings("AntivirusPath")
            'konsol.StartInfo.Arguments = " --base-dir=""" & ConfigurationManager.AppSettings("BaseDir") & """ " & " --action=clean " & fileLocation
            konsol.StartInfo.Arguments = ConfigurationManager.AppSettings("ScanOptions") & fileLocation
            konsol.Start()

            konsol.WaitForExit()
            If konsol.HasExited Then
                If Not (konsol.ExitCode = 0 Or konsol.ExitCode = 1) Then
                    Return 2
                ElseIf konsol.ExitCode = 0 Or konsol.ExitCode = 1 Then
                    Return konsol.ExitCode
                End If
            End If

        Catch _excp As Exception
            Return 2
        End Try
    End Function
End Class