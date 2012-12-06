Namespace Model
    Public Class eafSession
        Private _sessionId As Guid
        Private _sessionKey As String
        Private _homeDir As String

        Public Property SessionId As Guid
            Get
                Return _sessionId
            End Get
            Set(value As Guid)
                _sessionId = value
            End Set
        End Property

        Public Property SessionKey As String
            Get
                Return _sessionKey
            End Get
            Set(value As String)
                _sessionKey = value
            End Set
        End Property

        Public Property HomeDir As String
            Get
                Return _homeDir
            End Get
            Set(value As String)
                _homeDir = value
            End Set
        End Property
    End Class
End Namespace
