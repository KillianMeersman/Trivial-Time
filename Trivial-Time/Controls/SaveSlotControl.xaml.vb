Public Class SaveSlotControl

    Public Property Text() As String
        Get
            Return SaveNameTextblock.Text
        End Get
        Set(ByVal value As String)
            SaveNameTextblock.Text = value
        End Set
    End Property

    Private _color As Brush
    Public Property Color() As Brush
        Get
            Return _color
        End Get
        Set(ByVal value As Brush)
            _color = value
            MainRectangle.Fill = value
        End Set
    End Property

    Private _enabled As Boolean
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
            If value Then
                Me.Opacity = 1
                Me.IsHitTestVisible = True
            Else
                Me.Opacity = 0.5
                Me.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private Sub Me_MouseEnter() Handles Me.MouseEnter
        MainRectangle.Fill = Brushes.White
    End Sub

    Private Sub Me_MouseLeave() Handles Me.MouseLeave
        MainRectangle.Fill = _color
    End Sub

End Class
