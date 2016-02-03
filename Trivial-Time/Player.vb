Public Class Player

    Private _name As String
    Private _color As Brush

    Private _pion As PlayerPion
    Private _playerInfo As RoundPlayerInfoControl

    Private _geography As Boolean
    Private _entertainment As Boolean
    Private _history As Boolean
    Private _artsAndLiterature As Boolean
    Private _science As Boolean
    Private _sports As Boolean

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public ReadOnly Property Pion() As PlayerPion
        Get
            Return _pion
        End Get
    End Property

    Public Property PlayerInfo As RoundPlayerInfoControl
        Get
            Return _playerInfo
        End Get
        Set(value As RoundPlayerInfoControl)
            _playerInfo = value
            _playerInfo.IsEnabled = True
            _playerInfo.playerNameTextblock.Text = Me._name
            _playerInfo.Color = Me._color
        End Set
    End Property

    Public Property Color As Brush
        Get
            Return _color
        End Get
        Set(value As Brush)
            _color = value
        End Set
    End Property

    Public Property HasGeography() As Boolean
        Get
            Return _geography
        End Get
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _pion.geographyPie.Fill = Brushes.DodgerBlue
                    _playerInfo.GeographyEnabled = True
                Else
                    _pion.geographyPie.Fill = Brushes.DarkBlue
                    _playerInfo.GeographyEnabled = False
                End If
                _geography = value
            Catch ex As Exception
                Throw New Exception("Player: " & ex.Message)
            End Try
        End Set
    End Property

    Public Property HasEntertainment() As Boolean
        Get
            Return _entertainment
        End Get
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _pion.entertainmentPie.Fill = Brushes.DarkViolet
                    _playerInfo.EntertainmentEnabled = True
                Else
                    _pion.entertainmentPie.Fill = Brushes.DarkBlue
                    _playerInfo.EntertainmentEnabled = False
                End If
                _entertainment = value
            Catch ex As Exception
                Throw New Exception("Player: " & ex.Message)
            End Try
        End Set
    End Property

    Public Property HasHistory() As Boolean
        Get
            Return _history
        End Get
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _pion.historyPie.Fill = Brushes.Gold
                    _playerInfo.HistoryEnabled = True
                Else
                    _pion.historyPie.Fill = Brushes.DarkBlue
                    _playerInfo.HistoryEnabled = False
                End If
                _history = value
            Catch ex As Exception
                Throw New Exception("Player: " & ex.Message)
            End Try
        End Set
    End Property

    Public Property HasArtsAndLiterature() As Boolean
        Get
            Return _artsAndLiterature
        End Get
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _pion.Arts_and_LiteraturePie.Fill = Brushes.Firebrick
                    _playerInfo.ArtsAndLiteratureEnabled = True
                Else
                    _pion.Arts_and_LiteraturePie.Fill = Brushes.DarkBlue
                    _playerInfo.ArtsAndLiteratureEnabled = False
                End If
                _artsAndLiterature = value
            Catch ex As Exception
                Throw New Exception("Player: " & ex.Message)
            End Try
        End Set
    End Property

    Public Property HasScience() As Boolean
        Get
            Return _science
        End Get
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _pion.sciencePie.Fill = Brushes.ForestGreen
                    _playerInfo.ScienceEnabled = True
                Else
                    _pion.sciencePie.Fill = Brushes.DarkBlue
                    _playerInfo.ScienceEnabled = False
                End If
                _science = value
            Catch ex As Exception
                Throw New Exception("Player: " & ex.Message)
            End Try
        End Set
    End Property

    Public Property HasSports() As Boolean
        Get
            Return _sports
        End Get
        Set(ByVal value As Boolean)
            Try
                If value Then
                    _pion.sportsPie.Fill = Brushes.DarkOrange
                    _playerInfo.SportsEnabled = True
                Else
                    _pion.sportsPie.Fill = Brushes.DarkBlue
                    _playerInfo.SportsEnabled = False
                End If
                _sports = value
            Catch ex As Exception
                Throw New Exception("Player: " & ex.Message)
            End Try
        End Set
    End Property

    Public Sub New(name As String, color As Brush, infoControl As RoundPlayerInfoControl)
        _color = color
        _name = name
        _pion = New PlayerPion(Me)
        _playerInfo = infoControl

        Me.HasArtsAndLiterature = False
        Me.HasEntertainment = False
        Me.HasGeography = False
        Me.HasHistory = False
        Me.HasScience = False
        Me.HasSports = False
    End Sub

    Public Sub New()
        _pion = New PlayerPion(Me)
    End Sub

End Class
