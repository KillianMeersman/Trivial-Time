Imports System.Windows.Threading
Imports System.Windows.Media.Animation
Public Class NotificationControl

    Private Const fadeDuration As Integer = 1000

    Private _timer As New DispatcherTimer
    Private _duration As UInteger
    Private _animation As DoubleAnimation
    Private _active As Boolean

    Public Property Color() As Brush
        Get
            Return mainRectangle.Fill
        End Get
        Set(ByVal value As Brush)
            mainRectangle.Fill = value
        End Set
    End Property


    Public Event NotificationComplete()

    Public Sub notify(message As String, duration As UInteger, Optional subMessage As String = "")
        _active = True
        _animation = New DoubleAnimation
        Me.Visibility = Windows.Visibility.Visible
        _timer.Interval = TimeSpan.FromMilliseconds(duration)
        _animation.From = 0
        _animation.To = 1
        _animation.Duration = New Duration(TimeSpan.FromMilliseconds(fadeDuration))
        _duration = duration
        Me.notificationTextblock.Text = message
        Me.subNotificationTextblock.Text = subMessage
        mainGrid.BeginAnimation(Grid.OpacityProperty, _animation)
        _timer.Start()
    End Sub

    Public Sub SuspendExecution()
        If _active Then
            _timer.Stop()
        End If
    End Sub

    Public Sub ResumeExecution()
        If _active Then
            _timer.Start()
        End If
    End Sub

    Private Sub timer_tick()
        _timer.Stop()
        _active = False
        _animation.From = 1
        _animation.To = 0
        _animation.Duration = New Duration(TimeSpan.FromMilliseconds(fadeDuration))
        AddHandler _animation.Completed, AddressOf fadeComplete
        mainGrid.BeginAnimation(Grid.OpacityProperty, _animation)
    End Sub

    Private Sub fadeComplete()
        Me.Visibility = Windows.Visibility.Hidden
        RaiseEvent NotificationComplete()
    End Sub

    Public Sub New()
        InitializeComponent()
        AddHandler _timer.Tick, AddressOf timer_tick
        Me.mainGrid.Opacity = 0
    End Sub

End Class
