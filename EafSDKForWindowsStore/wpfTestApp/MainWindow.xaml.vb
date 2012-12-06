Imports System.Net
Class MainWindow

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim auth As New Auth
        auth.Show()
    End Sub

    Private Async Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Dim dlg As New System.Windows.Forms.OpenFileDialog
        If dlg.ShowDialog = Forms.DialogResult.OK Then
            Dim fileInfo As New IO.FileInfo(dlg.FileName)

            fileTB.Text = fileInfo.FullName

            Dim contentType As String = ""
            Select Case fileInfo.Extension
                Case ".jpg"
                    contentType = "image/jpeg"
                Case ".png"
                    contentType = "image/png"
            End Select

            Dim uploadClient As New EafSDKForWindowsStore.Client.UploadClient

            Dim params As New EafSDKForWindowsStore.Model.CreateTicketParams With
                {.ContentType = contentType,
                 .directory = My.Settings.Session.HomeDir,
                 .FileName = fileInfo.Name,
                 .FileSize = fileInfo.Length,
                 .Session = My.Settings.Session
                }

            Dim ticket As EafSDKForWindowsStore.Client.UploadClient.CreateTicketResult =
                Await uploadClient.CreateTicket(params)

            If ticket.Error = "" Then
                StatusTB.Text &= ticket.Result.ToString()
            Else
                StatusTB.Text &= ticket.Error
            End If

            StatusTB.Text &= vbCrLf & "Uploading file" & vbCrLf

            Dim sr As New IO.StreamReader(fileInfo.FullName)
            Dim content As New Http.StreamContent(sr.BaseStream)
            content.Headers().ContentType = Http.Headers.MediaTypeHeaderValue.Parse(contentType)
            content.Headers.Add("x-amz-acl", "public-read")

            Dim fileUploadClient As New Http.HttpClient
            Dim response As Http.HttpResponseMessage =
                Await fileUploadClient.PutAsync(ticket.Result.BlobUri, content)

            StatusTB.Text &= Await response.Content.ReadAsStringAsync

            Dim instanceResult As EafSDKForWindowsStore.Client.UploadClient.InstanceFileResult =
                Await uploadClient.InstanceFile(My.Settings.Session, ticket.Result)


            If instanceResult.Error = "" Then
                StatusTB.Text &= vbCrLf & instanceResult.Result.FileUrl.AbsoluteUri
            Else
                StatusTB.Text &= vbCrLf & instanceResult.Error
            End If

        End If
    End Sub
End Class
