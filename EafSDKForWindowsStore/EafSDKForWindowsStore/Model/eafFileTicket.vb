Namespace Model
    Public Class eafFileTicket
        Private _Author As String
        Private _ticketId As String
        Private _expirationDate As DateTime
        Private _BlobUri As String

        Public Property Author As String
            Get
                Return _Author
            End Get
            Set(value As String)
                _Author = value
            End Set
        End Property

        Public Property TicketId As String
            Get
                Return _ticketId
            End Get
            Set(value As String)
                _ticketId = value
            End Set
        End Property

        Public Property ExpirationDate As DateTime
            Get
                Return _expirationDate
            End Get
            Set(value As DateTime)
                _expirationDate = value
            End Set
        End Property

        Public Property BlobUri As String
            Get
                Return _BlobUri
            End Get
            Set(value As String)
                _BlobUri = value
            End Set
        End Property

        Public Shadows Function ToString() As String
            Return String.Format("Author: {0}, TicketID: {1}, ExpirationDate: {2}, BlobUri: {3}",
                                 Me.Author, Me.TicketId, Me.ExpirationDate.ToString, Me.BlobUri)

        End Function

    End Class

    Public Class CreateTicketParams
        Private _fileName As String
        Private _ContentType As String
        Private _fileSize As Long
        Private _directory As String
        Private _Session As Model.eafSession

        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(value As String)
                _fileName = value
            End Set
        End Property

        Public Property ContentType As String
            Get
                Return _ContentType
            End Get
            Set(value As String)
                _ContentType = value
            End Set
        End Property

        Public Property FileSize As Long
            Get
                Return _fileSize
            End Get
            Set(value As Long)
                _fileSize = value
            End Set
        End Property

        Public Property directory As String
            Get
                Return _directory
            End Get
            Set(value As String)
                _directory = value
            End Set
        End Property

        Public Property Session As Model.eafSession
            Get
                Return _Session
            End Get
            Set(value As Model.eafSession)
                _Session = value
            End Set
        End Property

    End Class
End Namespace
