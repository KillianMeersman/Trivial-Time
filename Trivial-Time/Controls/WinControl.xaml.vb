Public Class WinControl

    Private _OkColor As Brush
    Public Property OkColor() As Brush
        Get
            Return _OkColor
        End Get
        Set(ByVal value As Brush)
            _OkColor = value
            okRectangle.Fill = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return winTextbox.Text
        End Get
        Set(ByVal value As String)
            winTextbox.Text = value
        End Set
    End Property

    Public Event Ok()

    Private Sub OkRectangle_MouseEnter() Handles okRectangle.MouseEnter, okTextblock.MouseEnter
        okRectangle.Fill = Brushes.White
    End Sub

    Private Sub OkRectangle_MouseLeave() Handles okRectangle.MouseLeave
        okRectangle.Fill = _OkColor
    End Sub

    Private Sub OkRectangle_MouseDown() Handles okRectangle.MouseDown, okTextblock.MouseDown
        Me.Visibility = Windows.Visibility.Hidden
        okRectangle.Fill = _OkColor
        RaiseEvent Ok()
    End Sub

End Class
