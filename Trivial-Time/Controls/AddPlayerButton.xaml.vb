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

Public Class addPlayerButton

    Private _RectangleColor As Brush
    Public Property RectangleColor() As Brush
        Get
            Return _RectangleColor
        End Get
        Set(ByVal value As Brush)
            _RectangleColor = value
            MainRectangle.Fill = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()

        ' Insert code required on object creation below this point.
    End Sub

    Private Sub Mouse_Enter(sender As Grid, e As RoutedEventArgs) Handles LayoutRoot.MouseEnter
        MainRectangle.Fill = Brushes.White
    End Sub

    Private Sub Mouse_Leave(sender As Grid, e As RoutedEventArgs) Handles LayoutRoot.MouseLeave
        MainRectangle.Fill = _RectangleColor
    End Sub
End Class
