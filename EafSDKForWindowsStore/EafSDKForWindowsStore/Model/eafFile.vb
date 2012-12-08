Namespace Model
    Public Class eafFile
        Private _directory As String
        Private _fileId As String
        Private _fileUrl As Uri
        Private _fileSize As Long
        Private _author As String
        Private _metadata As Dictionary(Of String, String)

        Public Property Directory As String
            Get
                Return _directory
            End Get
            Set(value As String)
                _directory = value
            End Set
        End Property

        Public Property FileId As String
            Get
                Return _fileId
            End Get
            Set(value As String)
                _fileId = value
            End Set
        End Property

        Public Property FileUrl As Uri
            Get
                Return _fileUrl
            End Get
            Set(value As Uri)
                _fileUrl = value
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

        Public Property Author As String
            Get
                Return _author
            End Get
            Set(value As String)
                _author = value
            End Set
        End Property

        Public ReadOnly Property Metadata As Dictionary(Of String, String)
            Get
                Return _metadata
            End Get
        End Property

        Public Sub New()
            _metadata = New Dictionary(Of String, String)
        End Sub

    End Class
End Namespace
