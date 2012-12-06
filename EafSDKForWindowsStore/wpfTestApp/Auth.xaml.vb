Public Class Auth

    Private Async Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim Auth As New EafSDKForWindowsStore.Client.Auth

        'Visit www.elasticframework.com to get AppID And AppKey

        Dim App As New EafSDKForWindowsStore.Model.eafApp(Guid.Parse("AppID"), "AppKey")

        Try
            Dim authRes As EafSDKForWindowsStore.Client.Auth.AuthResult =
                        Await Auth.Auth(App, nicknameTB.Text, passwordTB.Text)

            If authRes.Session IsNot Nothing Then
                If authRes.Error = "" Then
                    MsgBox("You're logged in " & nicknameTB.Text)
                    My.Settings.Session = authRes.Session
                    My.Settings.Save()
                Else
                    MsgBox("This is not ok")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try





    End Sub
End Class
