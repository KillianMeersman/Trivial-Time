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

    Private _players(3) As Player
    Public Property Players As Player()
        Get
            Return _players
        End Get
        Set(ByVal value As Player())
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

    Public Sub addPlayer(player As Player)
        If _players(0) Is Nothing Then
            _players(0) = player
        ElseIf _players(1) Is Nothing Then
            _players(1) = player
        ElseIf _players(2) Is Nothing Then
            _players(2) = player
        ElseIf _players(3) Is Nothing Then
            _players(3) = player
        End If
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(name As String, players() As Player, currentPlayer As Byte)
        _name = name
        _players = players
        _lastPlayer = currentPlayer
    End Sub
End Class
