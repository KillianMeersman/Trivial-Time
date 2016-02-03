Public Class PromtControl

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

    Public Event Ok()
    Public Event Cancel()

    Private Sub okRectangle_MouseEnter(sender As Object, e As RoutedEventArgs) Handles okRectangle.MouseEnter, okTextblock.MouseEnter
        okRectangle.Fill = Brushes.White
    End Sub

    Private Sub cancelRectangle_MouseEnter(sender As Object, e As RoutedEventArgs) Handles cancelRectangle.MouseEnter, cancelTextblock.MouseEnter
        cancelRectangle.Fill = Brushes.White
    End Sub

    Private Sub cancelRectangle_MouseLeave(sender As Rectangle, e As RoutedEventArgs) Handles cancelRectangle.MouseLeave
        cancelRectangle.Fill = _cancelColor
    End Sub

    Private Sub okRectangle_MouseLeave(sender As Rectangle, e As RoutedEventArgs) Handles okRectangle.MouseLeave
        okRectangle.Fill = _okColor
    End Sub

    Private Sub okRectangle_MouseDown(sender As Object, e As RoutedEventArgs) Handles okRectangle.MouseDown, okTextblock.MouseDown
        cancelRectangle.Fill = _cancelColor
        okRectangle.Fill = _okColor
        Me.Visibility = Windows.Visibility.Hidden
        RaiseEvent Ok()
    End Sub

    Private Sub cancelRectangle_MouseDown(sender As Object, e As RoutedEventArgs) Handles cancelRectangle.MouseDown, cancelTextblock.MouseDown
        cancelRectangle.Fill = _cancelColor
        okRectangle.Fill = _okColor
        Me.Visibility = Windows.Visibility.Hidden
        RaiseEvent Cancel()
    End Sub

End Class
