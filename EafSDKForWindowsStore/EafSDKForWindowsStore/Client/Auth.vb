Imports System.Net
Imports System.Linq
Imports System.Xml.Linq
Imports System.Xml

Namespace Client
    Public Class Auth
        Public Async Function Auth(ByVal App As Model.eafApp, ByVal Nickname As String, ByVal Password As String) As Threading.Tasks.Task(Of AuthResult)


            Dim UrlBuilder As New Text.StringBuilder

            With UrlBuilder
                .Append(String.Format("{0}/auth.api.xml", ConfigHelper.ApiUrl))
                .Append("?appId=" & App.AppId.ToString())
                .Append("&appKey=" & App.AppKey)
                .Append("&nickname=" & Nickname)
                .Append("&password=" & Password)
            End With

            Dim AuthUri As Uri = New Uri(UrlBuilder.ToString())
            Dim client As New Http.HttpClient



            Dim StringResponse As String =
                Await client.GetStringAsync(AuthUri)

            Dim xmlResponse As XDocument = XDocument.Parse(StringResponse)

            If xmlResponse.<AuthResult>.<Error>.Value = "" Then
                Dim Result = xmlResponse.<AuthResult>

                Dim aReulsult As New Model.eafSession With
                    {.SessionId = Guid.Parse(Result.<SessionID>.Value),
                     .SessionKey = Result.<SessionKey>.Value,
                     .HomeDir = Result.<UserInformation>.<HomeDirectory>.Value
                    }

                Return New AuthResult With {.Error = "", .Session = aReulsult}
            Else
                Return New AuthResult With {.Session = Nothing, .Error = xmlResponse.<Error>.Value}

            End If


        End Function

        Public Structure AuthResult
            Public Session As Model.eafSession
            Public [Error] As String
        End Structure
    End Class
End Namespace
