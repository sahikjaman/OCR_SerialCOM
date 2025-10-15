Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Net.NetworkInformation 'Include this
Imports System.Windows.Forms
Imports System.Net

Public Class mainserver
    Dim server As String = "www.spil.co.id"
    '=====> String url = "http://softwareteknik.com/ptspill/inrfiddata.php"
    '=====> http://www.spil.co.id/RFIDApi/insRFIDData.jsp?tag=123345&macaddr=88888&ipaddr=127.0.0.1
    Dim url As String = "http://api.spil.co.id:6492/RFIDApi/insRFIDData.jsp"
    Dim webClient As System.Net.WebClient = New System.Net.WebClient()

    Public ok As String = "OK"
    Public notok As String = "NOT-OK"
    Public warning As String = "NOT-KNOWN"

    Public Function access(tag As String, macaddr As String, ipaddr As String, readon As String, sendon As String, imagename As String, rating As String)
        Try
            Dim url As String = Me.url + "?tag=" + tag + "&macaddr=" + macaddr + "&ipaddr=" + ipaddr + "&readon=" + readon + "&senton=" + sendon + "&imagename=" + imagename + "&rate=" + rating
            Return webClient.DownloadString(url)
        Catch ex As Exception
            Return "error"
        End Try
    End Function

    Public Function uploadfile(ByVal filename As String)
        Dim uri = New Uri("http://api.spil.co.id/OCR/uploadocr.php")
        'Dim bArr As Byte() = System.Text.Encoding.Default.GetBytes(filename)
        'webClient.Headers.Add("file", System.IO.Path.GetFileName(filename))
        'Dim Dt As Byte() = webClient.UploadFileAsync(uri, "POST", filename)
        webClient.UploadFileAsync(uri, "POST", filename)
        Return 1
    End Function

    Public Function isConnectServer() As String
        Dim myPing As Ping = New Ping()
        Try
            Dim reply As PingReply = myPing.Send(server, 1000)
            If reply IsNot Nothing Then
                If reply.Status.ToString() = "Success" Then
                    Return True
                Else
                    Return False
                End If
            Else

            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
