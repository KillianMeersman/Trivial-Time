Public Class GameSave

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _players As New List(Of Player)
    Public Property Players As List(Of Player)
        Get
            Return _players
        End Get
        Set(ByVal value As List(Of Player))
            _players = value
        End Set
    End Property

    Private _lastPlayer As Byte
    Public Property LastPlayer() As Byte
        Get
            Return _lastPlayer
        End Get
        Set(ByVal value As Byte)
            _lastPlayer = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(name As String)
        _name = name
    End Sub
End Class
