Public Class SavePromptControl

    Private _okColor As Brush
    Public Property OkColor() As Brush
        Get
            Return _okColor
        End Get
        Set(ByVal value As Brush)
            _okColor = value
            okRectangle.Fill = value
        End Set
    End Property

    Private _cancelColor As Brush
    Public Property CancelColor() As Brush
        Get
            Return _cancelColor
        End Get
        Set(ByVal value As Brush)
            _cancelColor = value
            cancelRectangle.Fill = value
        End Set
    End Property

    Private _OkEnabled As Boolean
    Public Property OkEnabled() As Boolean
        Get
            Return _OkEnabled
        End Get
        Set(ByVal value As Boolean)
            _OkEnabled = value
            If value Then
                okRectangle.IsHitTestVisible = True
                okTextblock.IsHitTestVisible = True
                okTextblock.Opacity = 1
            Else
                okRectangle.IsHitTestVisible = False
                okTextblock.IsHitTestVisible = False
                okTextblock.Opacity = 0.5
            End If
        End Set
    End Property

    Public Property Text() As String
        Get
            Return saveNameTextblock.Text
        End Get
        Set(ByVal value As String)
            saveNameTextblock.Text = value
        End Set
    End Property

    Public Event Ok()
    Public Event Cancel()

    Private Sub okRectangle_MouseEnter(sender As Object, e As RoutedEventArgs) Handles okRectangle.MouseEnter, okTextblock.MouseEnter
        okRectangle.Fill = Brushes.White
    End Sub

    Private Sub okRectangle_MouseLeave(sender As Rectangle, e As RoutedEventArgs) Handles okRectangle.MouseLeave
        okRectangle.Fill = _okColor
    End Sub

    Private Sub okRectangle_MouseDown(sender As Object, e As RoutedEventArgs) Handles okRectangle.MouseDown, okTextblock.MouseDown
        okRectangle.Fill = _okColor
        Me.Visibility = Windows.Visibility.Hidden
        RaiseEvent Ok()
        OkEnabled = False
        Text = ""
    End Sub

    Private Sub cancelRectangle_MouseEnter(sender As Object, e As RoutedEventArgs) Handles cancelRectangle.MouseEnter, cancelTextblock.MouseEnter
        cancelRectangle.Fill = Brushes.White
    End Sub

    Private Sub cancelRectangle_MouseLeave(sender As Rectangle, e As RoutedEventArgs) Handles cancelRectangle.MouseLeave
        cancelRectangle.Fill = _cancelColor
    End Sub

    Private Sub cancelRectangle_MouseDown(sender As Object, e As RoutedEventArgs) Handles cancelRectangle.MouseDown, cancelTextblock.MouseDown
        cancelRectangle.Fill = _cancelColor
        Me.Visibility = Windows.Visibility.Hidden
        RaiseEvent Cancel()
        OkEnabled = False
        Text = ""
    End Sub

    Private Sub saveNameTextblock_TextChanged(sender As Object, e As TextChangedEventArgs) Handles saveNameTextblock.TextChanged
        If saveNameTextblock.Text <> "" Or saveNameTextblock.Text <> Nothing Then
            OkEnabled = True
        Else
            OkEnabled = False
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
        OkEnabled = False
    End Sub
End Class
