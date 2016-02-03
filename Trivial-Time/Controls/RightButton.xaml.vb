Public Class RightButton

    Private _color As Brush
    Public Property Color() As Brush
        Get
            Return MainRectangle.Fill
        End Get
        Set(ByVal value As Brush)
            _color = value
            MainRectangle.Fill = value
            rightRectangle.Fill = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return MainTextblock.Text
        End Get
        Set(ByVal value As String)
            MainTextblock.Text = value
        End Set
    End Property

    Public Property Image() As ImageSource
        Get
            Return mainImage.Source
        End Get
        Set(ByVal value As ImageSource)
            mainImage.Source = value
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
        rightRectangle.Fill = Brushes.White
    End Sub

    Private Sub Me_MouseLeave() Handles Me.MouseLeave
        MainRectangle.Fill = _color
        rightRectangle.Fill = _color
    End Sub

    Public Sub New()
        InitializeComponent()
        _color = MainRectangle.Fill
    End Sub

End Class
