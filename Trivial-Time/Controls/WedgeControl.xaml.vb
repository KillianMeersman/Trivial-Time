#Region "Imports"
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
#End Region

Partial Public Class WedgeControl
    Implements ITileBase

    Private _category As category
    Private _categoryBrush As Brush 'The original brush
    Private _neighbors As New List(Of ITileBase)

    Public Property Category() As category Implements ITileBase.Category
        Get
            Return _category
        End Get
        Set(ByVal value As category)
            _category = value
            Select Case _category
                Case Trivial_Time.category.Geography : MainWedge.Fill = Brushes.DodgerBlue
                    Me.categoryImage.Source = New BitmapImage(New Uri("/Resources/Geography.png", UriKind.Relative))
                Case Trivial_Time.category.Entertainment : MainWedge.Fill = Brushes.DarkViolet
                    Me.categoryImage.Source = New BitmapImage(New Uri("/Resources/Entertainment.png", UriKind.Relative))
                Case Trivial_Time.category.History : MainWedge.Fill = Brushes.Gold
                    Me.categoryImage.Source = New BitmapImage(New Uri("/Resources/History.png", UriKind.Relative))
                Case Trivial_Time.category.Arts_and_Literature : MainWedge.Fill = Brushes.Firebrick
                    Me.categoryImage.Source = New BitmapImage(New Uri("/Resources/Literature.png", UriKind.Relative))
                Case Trivial_Time.category.Science : MainWedge.Fill = Brushes.ForestGreen
                    Me.categoryImage.Source = New BitmapImage(New Uri("/Resources/Science.png", UriKind.Relative))
                Case Trivial_Time.category.Sports : MainWedge.Fill = Brushes.DarkOrange
                    Me.categoryImage.Source = New BitmapImage(New Uri("/Resources/Sports.png", UriKind.Relative))
                Case Trivial_Time.category.None : MainWedge.Fill = Brushes.Gray
                Case Else : Throw New Exception("Unspecified category")
            End Select
            _categoryBrush = MainWedge.Fill
        End Set
    End Property

    Public Property Neighbors As List(Of ITileBase) Implements ITileBase.Neighbors
        Get
            Return _neighbors
        End Get
        Set(value As List(Of ITileBase))
            _neighbors = value
        End Set
    End Property

    Private _IsEnabled As Boolean
    Public Shadows Property IsEnabled() As Boolean Implements ITileBase.IsEnabled
        Get
            Return _IsEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsEnabled = value
            If value Then
                Me.Opacity = 1
                Me.IsHitTestVisible = True
            Else
                Me.Opacity = 0.2
                Me.IsHitTestVisible = False
            End If
        End Set
    End Property

#Region "Visual Handlers"
    Private Sub button_MouseEnter() Handles UserControl.MouseEnter
        FlashUp()
    End Sub

    Private Sub button_MouseLeave() Handles UserControl.MouseLeave
        FlashDown()
    End Sub

    Private Sub button_MouseDown() Handles UserControl.MouseDown
        MainWedge.Fill = Brushes.White
    End Sub

    Private Sub button_MouseUp() Handles UserControl.MouseUp
        MainWedge.Fill = _categoryBrush
    End Sub
#End Region

    Public Sub FlashUp(Optional duration As Integer = 0) Implements ITileBase.FlashUp
        If duration > 0 Then
            Dim t As New System.Windows.Threading.DispatcherTimer()
            t.Interval = New TimeSpan(0, 0, duration)
            AddHandler t.Tick, AddressOf FlashDown
            MainWedge.Fill = Brushes.White
            MainWedge.StrokeThickness = 2
            MainWedge.Stroke = Brushes.Black
            t.Start()
        Else
            MainWedge.Fill = Brushes.White
            MainWedge.StrokeThickness = 2
            MainWedge.Stroke = Brushes.Black
        End If
    End Sub

    Public Sub FlashDown() Implements ITileBase.FlashDown
        MainWedge.StrokeThickness = 0
        MainWedge.Fill = _categoryBrush
    End Sub

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()
    End Sub
End Class