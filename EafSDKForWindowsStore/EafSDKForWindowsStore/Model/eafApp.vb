Namespace Model
    Public Class eafApp
        Private _AppId As Guid
        Private _AppKey As String

        Public Property AppId As Guid
            Get
                Return _AppId
            End Get
            Set(value As Guid)
                _AppId = value
            End Set
        End Property

        Public Property AppKey As String
            Get
                Return _AppKey
            End Get
            Set(value As String)
                _AppKey = value
            End Set
        End Property

        Public Sub New(ByVal AppId As Guid, AppKey As String)
            _AppId = AppId
            _AppKey = AppKey
        End Sub

        
    End Class


End Namespace
