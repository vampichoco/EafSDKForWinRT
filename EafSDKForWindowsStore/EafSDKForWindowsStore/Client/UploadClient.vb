Imports System.Net
Imports System.Linq
Imports System.Xml.Linq
Imports System.Xml

Namespace Client
    Public Class UploadClient
        Public Async Function CreateTicket(ByVal CreateTicketParams As Model.CreateTicketParams) As Threading.Tasks.Task(Of CreateTicketResult)
            Dim UrlBuilder As New Text.StringBuilder
            With UrlBuilder
                .Append(String.Format("{0}/createticket.api.xml", ConfigHelper.ApiUrl))
                .Append("?sessid=" & CreateTicketParams.Session.SessionId.ToString())
                .Append("&session=" & CreateTicketParams.Session.SessionKey)
                .Append("&fileName=" & CreateTicketParams.FileName)
                .Append("&fileSize=" & CreateTicketParams.FileSize.ToString())
                .Append("&directoryId=" & CreateTicketParams.directory)
                .Append("&contentType=" & CreateTicketParams.ContentType)
            End With

            Dim AuthUri As New Uri(UrlBuilder.ToString())

            Dim Client As New Http.HttpClient

            Dim StringResponse As String =
                Await Client.GetStringAsync(AuthUri)

            Dim xmlResponse As XDocument = XDocument.Parse(StringResponse)

            Dim resData = xmlResponse.<CreateTicketResult>

            If resData.<Error>.Value = "" Then
                Dim Ticket As New Model.eafFileTicket With
                    {.Author = resData.<Result>.<Author>.Value,
                     .TicketId = resData.<Result>.<TicketID>.Value,
                     .BlobUri = resData.<Result>.<BlobUri>.Value
                    }

                

                Dim expDate As String = resData.<Result>.<ExpirationDate>.Value
                Try
                    Ticket.ExpirationDate = DateTime.Parse(expDate)

                Catch ex As Exception
                End Try


                Return New CreateTicketResult With {.Error = "", .Result = Ticket}
            Else
                Return New CreateTicketResult With {.Error = resData.<Error>.Value, .Result = Nothing}
            End If



        End Function

        Public Async Function InstanceFile(ByVal Session As Model.eafSession, ByVal Ticket As Model.eafFileTicket) As Threading.Tasks.Task(Of InstanceFileResult)

            Dim UrlBuilder As New Text.StringBuilder

            With UrlBuilder
                .Append(String.Format("{0}/instancefile.api.xml", ConfigHelper.ApiUrl))
                .Append("?sessid=" & Session.SessionId.ToString())
                .Append("&session=" & Session.SessionKey)
                .Append("&ticket=" & Ticket.TicketId)
            End With

            Dim AuthUri As New Uri(UrlBuilder.ToString())

            Dim Client As New Http.HttpClient

            Dim StringResponse As String =
                Await Client.GetStringAsync(AuthUri)

            Dim ResponseData As XDocument = XDocument.Parse(StringResponse)

            Dim resData = ResponseData.<InstanceFileResult>


            If resData.<Error>.Value = "" Then
                Dim file As New Model.eafFile With
                    {.Author = resData.<Result>.<Author>.Value,
                     .FileId = resData.<Result>.<FileId>.Value,
                     .Directory = resData.<Result>.<DirectoryId>.Value,
                     .FileSize = Convert.ToInt64(resData.<Result>.<FileSize>.Value),
                     .FileUrl = New Uri(resData.<Result>.<FileUrl>.Value)
                    }

                For Each item As XElement In resData.<Result>.<Metadata>...<Object>
                    file.Metadata.Add(item.@Key, item.Value)
                Next

                Return New InstanceFileResult With {.Error = "", .Result = file}
            Else
                Return New InstanceFileResult With {.Error = resData.<Error>.Value, .Result = Nothing}
            End If


        End Function

        Public Structure CreateTicketResult
            Public [Error] As String
            Public Result As Model.eafFileTicket
        End Structure

        Public Structure InstanceFileResult
            Public [Error] As String
            Public Result As Model.eafFile
        End Structure
    End Class
End Namespace
