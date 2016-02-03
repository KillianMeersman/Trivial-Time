Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Partial Public Class RoundPlayerInfoControl

    Private _color As Brush
    Public Property Color() As Brush
        Get
            Return _color
        End Get
        Set(ByVal value As Brush)
            _color = value
            mainArc.Fill = value
        End Set
    End Property

    Private _isEnabled As Boolean
    Public Shadows Property IsEnabled() As Boolean
        Get
            Return _isEnabled
        End Get
        Set(ByVal value As Boolean)
            _isEnabled = value
            If _isEnabled Then
                Me.Opacity = 1
            Else
                Me.playerNameTextblock.Text = ""
                Me.Opacity = 0.05
                Me.mainArc.Fill = Brushes.Gray
            End If
        End Set
    End Property

    Private _geographyEnabled As Boolean
    Public Property GeographyEnabled() As Boolean
        Get
            Return _geographyEnabled
        End Get
        Set(ByVal value As Boolean)
            _geographyEnabled = value
            If value Then
                Me.geographyRectangle.Fill = Brushes.DodgerBlue
            Else
                Me.geographyRectangle.Fill = Brushes.DarkSlateGray
            End If
        End Set
    End Property

    Private _scienceEnabled As Boolean
    Public Property ScienceEnabled() As Boolean
        Get
            Return _scienceEnabled
        End Get
        Set(ByVal value As Boolean)
            _scienceEnabled = value
            If value Then
                Me.scienceRectangle.Fill = Brushes.ForestGreen
            Else
                Me.scienceRectangle.Fill = Brushes.DarkSlateGray
            End If
        End Set
    End Property

    Private _historyEnabled As Boolean
    Public Property HistoryEnabled() As Boolean
        Get
            Return _historyEnabled
        End Get
        Set(ByVal value As Boolean)
            _historyEnabled = value
            If value Then
                Me.historyRectangle.Fill = Brushes.Gold
            Else
                Me.historyRectangle.Fill = Brushes.DarkSlateGray
            End If
        End Set
    End Property

    Private _entertainmentEnabled As Boolean
    Public Property EntertainmentEnabled() As Boolean
        Get
            Return _entertainmentEnabled
        End Get
        Set(ByVal value As Boolean)
            _entertainmentEnabled = value
            If value Then
                Me.entertainmentRectangle.Fill = Brushes.DarkViolet
            Else
                Me.entertainmentRectangle.Fill = Brushes.DarkSlateGray
            End If
        End Set
    End Property

    Private _artsAndLiteratureEnabled As Boolean
    Public Property ArtsAndLiteratureEnabled() As Boolean
        Get
            Return _artsAndLiteratureEnabled
        End Get
        Set(ByVal value As Boolean)
            _artsAndLiteratureEnabled = value
            If value Then
                Me.artAndLiteratureRectangle.Fill = Brushes.Firebrick
            Else
                Me.artAndLiteratureRectangle.Fill = Brushes.DarkSlateGray
            End If
        End Set
    End Property

    Private _sportsEnabled As Boolean
    Public Property SportsEnabled() As Boolean
        Get
            Return _sportsEnabled
        End Get
        Set(ByVal value As Boolean)
            _sportsEnabled = value
            If value Then
                Me.sportsRectangle.Fill = Brushes.DarkOrange
            Else
                Me.sportsRectangle.Fill = Brushes.DarkSlateGray
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        Me.GeographyEnabled = False
        Me.ScienceEnabled = False
        Me.HistoryEnabled = False
        Me.EntertainmentEnabled = False
        Me.ArtsAndLiteratureEnabled = False
        Me.SportsEnabled = False
        Me.Opacity = 0.25
    End Sub

    Public Sub Highlight()
        Me.Opacity = 1
    End Sub

    Public Sub ResetHighlight()
        Me.Opacity = 0.25
    End Sub

End Class
