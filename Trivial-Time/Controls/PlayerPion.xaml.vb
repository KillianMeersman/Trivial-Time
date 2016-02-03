Imports System.Windows.Media.Animation

Public Class PlayerPion

    Private animation As New DoubleAnimation

    Private _player As Player
    Public ReadOnly Property Player() As Player
        Get
            Return _player
        End Get
    End Property

    Private _position As ITileBase
    Public Property Position() As ITileBase
        Get
            Return _position
        End Get
        Set(ByVal value As ITileBase)
            _position = value
        End Set
    End Property

    Private _zIndex As Integer
    Public Property ZIndex() As Integer
        Get
            Return _zIndex
        End Get
        Set(ByVal value As Integer)
            _zIndex = value
        End Set
    End Property

    Private _size As Double
    Public Property Size() As Double
        Get
            Return _size
        End Get
        Set(ByVal value As Double)
            _size = value
            Me.Width = Size
            Me.Height = Size
        End Set
    End Property

    Public Sub New(player As Player)
        InitializeComponent()
        _player = player
        _zIndex = Canvas.GetZIndex(Me)
        initializePlayerSettings()
    End Sub

    Private Sub initializePlayerSettings()
        Try
            outerCircle.Fill = _player.Color
            innerCircle.Fill = _player.Color
        Catch
            Throw New Exception("Error while initializing playerPion")
        End Try
    End Sub

    Public Sub Highlight(size As Double, animate As Boolean)
        Me.Width = size
        Me.Height = size
        Canvas.SetZIndex(Me, Integer.MaxValue)
        strokeCircle.Visibility = Windows.Visibility.Visible
        If animate Then
            animation.From = 0
            animation.To = 1
            animation.Duration = New Duration(TimeSpan.FromMilliseconds(1000))
            animation.AutoReverse = True
            animation.RepeatBehavior = RepeatBehavior.Forever
            strokeCircle.BeginAnimation(OpacityProperty, animation)
        End If
    End Sub

    Public Sub ResetHighlight()
        Me.outerCircle.StrokeThickness = 1.5
        Me.outerCircle.Stroke = Brushes.Black
        Me.Size = _size
        Canvas.SetZIndex(Me, _zIndex)
        strokeCircle.BeginAnimation(OpacityProperty, Nothing)
        strokeCircle.Visibility = Windows.Visibility.Hidden
        strokeCircle.Opacity = 1
    End Sub

End Class
