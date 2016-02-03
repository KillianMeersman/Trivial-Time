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

Partial Public Class PlayerCreationControl

#Region "Properties"

    Private _previousRectangle As Rectangle
    Public Property PreviousRectangle() As Rectangle
        Get
            Return _previousRectangle
        End Get
        Set(ByVal value As Rectangle)
            _previousRectangle = value
        End Set
    End Property

    Private _selectedRectangle As Rectangle
    Public Property SelectedRectangle() As Rectangle
        Get
            Return _selectedRectangle
        End Get
        Private Set(value As Rectangle)
            _selectedRectangle = value
        End Set
    End Property

    Private _color As Brush
    Public Property Color() As Brush
        Get
            Return _color
        End Get
        Set(ByVal value As Brush)
            _color = value
            normalRectangle.Fill = value
            addRectangle.Fill = value
        End Set
    End Property

    Private _redEnabled As Boolean
    Public Property RedEnabled() As Boolean
        Get
            Return _redEnabled
        End Get
        Set(ByVal value As Boolean)
            _redEnabled = value
            If value Then
                redRectangle.Opacity = 1
                redRectangle.IsHitTestVisible = True
            Else

                redRectangle.Opacity = 0.2
                redRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _blueEnabled As Boolean
    Public Property BlueEnabled() As Boolean
        Get
            Return _blueEnabled
        End Get
        Set(ByVal value As Boolean)
            _blueEnabled = value
            If value Then
                blueRectangle.Opacity = 1
                blueRectangle.IsHitTestVisible = True
            Else

                blueRectangle.Opacity = 0.2
                blueRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _greenEnabled As Boolean
    Public Property GreenEnabled() As Boolean
        Get
            Return _greenEnabled
        End Get
        Set(ByVal value As Boolean)
            _greenEnabled = value
            If value Then
                greenRectangle.Opacity = 1
                greenRectangle.IsHitTestVisible = True
            Else

                greenRectangle.Opacity = 0.2
                greenRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _pinkEnabled As Boolean
    Public Property PinkEnabled() As Boolean
        Get
            Return _pinkEnabled
        End Get
        Set(ByVal value As Boolean)
            _pinkEnabled = value
            If value Then
                pinkRectangle.Opacity = 1
                pinkRectangle.IsHitTestVisible = True
            Else

                pinkRectangle.Opacity = 0.2
                pinkRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _cyanEnabled As Boolean
    Public Property CyanEnabled() As Boolean
        Get
            Return _cyanEnabled
        End Get
        Set(ByVal value As Boolean)
            _cyanEnabled = value
            If value Then
                cyanRectangle.Opacity = 1
                cyanRectangle.IsHitTestVisible = True
            Else

                cyanRectangle.Opacity = 0.2
                cyanRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _orangeEnabled As Boolean
    Public Property OrangeEnabled() As Boolean
        Get
            Return _orangeEnabled
        End Get
        Set(ByVal value As Boolean)
            _orangeEnabled = value
            If value Then
                orangeRectangle.Opacity = 1
                orangeRectangle.IsHitTestVisible = True
            Else

                orangeRectangle.Opacity = 0.2
                orangeRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _violetEnabled As Boolean
    Public Property VioletEnabled() As Boolean
        Get
            Return _violetEnabled
        End Get
        Set(ByVal value As Boolean)
            _violetEnabled = value
            If value Then
                violetRectangle.Opacity = 1
                violetRectangle.IsHitTestVisible = True
            Else

                violetRectangle.Opacity = 0.2
                violetRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _greyEnabled As Boolean
    Public Property GreyEnabled() As Boolean
        Get
            Return _greyEnabled
        End Get
        Set(ByVal value As Boolean)
            _greyEnabled = value
            If value Then
                greyRectangle.Opacity = 1
                greyRectangle.IsHitTestVisible = True
            Else

                greyRectangle.Opacity = 0.2
                greyRectangle.IsHitTestVisible = False
            End If
        End Set
    End Property

    Private _canBeDisabled As Boolean
    Public Property CanBeDisabled() As Boolean
        Get
            Return _canBeDisabled
        End Get
        Set(ByVal value As Boolean)
            _canBeDisabled = value
            If _canBeDisabled Then
                removeRectangle.Visibility = Windows.Visibility.Visible
                removeSign.Visibility = Windows.Visibility.Visible
            Else
                PlayerEnabled = True
                removeRectangle.Visibility = Windows.Visibility.Hidden
                removeSign.Visibility = Windows.Visibility.Hidden
            End If
        End Set
    End Property

    Private _removeButtonColor As Brush
    Public Property RemoveButtonColor() As Brush
        Get
            Return _removeButtonColor
        End Get
        Set(ByVal value As Brush)
            _removeButtonColor = value
            removeRectangle.Fill = value
        End Set
    End Property

    Private _playerEnabled As Boolean
    Public Property PlayerEnabled() As Boolean
        Get
            Return _playerEnabled
        End Get
        Set(ByVal value As Boolean)
            _playerEnabled = value
            If _playerEnabled Then
                nameLabel.Visibility = Windows.Visibility.Visible
                nameTextBox.Visibility = Windows.Visibility.Visible
                colorLabel.Visibility = Windows.Visibility.Visible
                normalRectangle.Visibility = Windows.Visibility.Visible
                normalsStrokeRectangle.Visibility = Windows.Visibility.Visible
                redRectangle.Visibility = Windows.Visibility.Visible
                greenRectangle.Visibility = Windows.Visibility.Visible
                blueRectangle.Visibility = Windows.Visibility.Visible
                violetRectangle.Visibility = Windows.Visibility.Visible
                cyanRectangle.Visibility = Windows.Visibility.Visible
                orangeRectangle.Visibility = Windows.Visibility.Visible
                pinkRectangle.Visibility = Windows.Visibility.Visible
                greyRectangle.Visibility = Windows.Visibility.Visible
                If _canBeDisabled Then
                    removeRectangle.Visibility = Windows.Visibility.Visible
                    removeSign.Visibility = Windows.Visibility.Visible
                Else
                    removeRectangle.Visibility = Windows.Visibility.Hidden
                    removeSign.Visibility = Windows.Visibility.Hidden
                End If
                addSign.Visibility = Windows.Visibility.Hidden
                addRectangle.Visibility = Windows.Visibility.Hidden
            Else
                nameLabel.Visibility = Windows.Visibility.Hidden
                nameTextBox.Visibility = Windows.Visibility.Hidden
                colorLabel.Visibility = Windows.Visibility.Hidden
                normalRectangle.Visibility = Windows.Visibility.Hidden
                normalsStrokeRectangle.Visibility = Windows.Visibility.Hidden
                removeRectangle.Visibility = Windows.Visibility.Hidden
                removeSign.Visibility = Windows.Visibility.Hidden
                redRectangle.Visibility = Windows.Visibility.Hidden
                greenRectangle.Visibility = Windows.Visibility.Hidden
                blueRectangle.Visibility = Windows.Visibility.Hidden
                violetRectangle.Visibility = Windows.Visibility.Hidden
                cyanRectangle.Visibility = Windows.Visibility.Hidden
                orangeRectangle.Visibility = Windows.Visibility.Hidden
                pinkRectangle.Visibility = Windows.Visibility.Hidden
                greyRectangle.Visibility = Windows.Visibility.Hidden
                addRectangle.Visibility = Windows.Visibility.Visible
                addSign.Visibility = Windows.Visibility.Visible
            End If
        End Set
    End Property

    Private _playerName As String
    Public Property PlayerName() As String
        Get
            Return _playerName
        End Get
        Set(ByVal value As String)
            _playerName = value
            nameTextBox.Text = value
        End Set
    End Property
#End Region

    Public Event PlayerChanged(sender As PlayerCreationControl, e As RoutedEventArgs, message As String)

    'State variables
    Private _initialized As Boolean = False

    Public Sub New()
        MyBase.New()

        Me.InitializeComponent()
    End Sub



    Public Sub toggleRectangle(rectangleName As String, enabled As Boolean)
        Select Case rectangleName
            Case "redRectangle" : RedEnabled = enabled
            Case "greenRectangle" : GreenEnabled = enabled
            Case "blueRectangle" : BlueEnabled = enabled
            Case "violetRectangle" : VioletEnabled = enabled
            Case "cyanRectangle" : CyanEnabled = enabled
            Case "orangeRectangle" : OrangeEnabled = enabled
            Case "pinkRectangle" : PinkEnabled = enabled
            Case "greyRectangle" : GreyEnabled = enabled
            Case Else
        End Select
    End Sub

    Private Sub Rectangle_MouseDown(sender As Rectangle, e As RoutedEventArgs) Handles redRectangle.MouseDown, greenRectangle.MouseDown, blueRectangle.MouseDown, violetRectangle.MouseDown, cyanRectangle.MouseDown, orangeRectangle.MouseDown, pinkRectangle.MouseDown, greyRectangle.MouseDown
        _selectedRectangle = sender
        changeSelectedRectangle(sender)
        If Not _previousRectangle Is _selectedRectangle Then
            RaiseEvent PlayerChanged(Me, Nothing, "colorChanged")
        End If
            _previousRectangle = sender
    End Sub

    Private Sub changeSelectedRectangle(rectangle As Rectangle)
        redRectangle.Stroke = Nothing
        greenRectangle.Stroke = Nothing
        blueRectangle.Stroke = Nothing
        violetRectangle.Stroke = Nothing
        cyanRectangle.Stroke = Nothing
        orangeRectangle.Stroke = Nothing
        pinkRectangle.Stroke = Nothing
        greyRectangle.Stroke = Nothing
        If Not rectangle Is Nothing Then
            rectangle.Stroke = Brushes.White
        End If
    End Sub

#Region "Event handlers"

    Private Sub nameTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles nameTextBox.TextChanged
        _playerName = nameTextBox.Text
        If _initialized Then
                RaiseEvent PlayerChanged(Me, Nothing, "nameChanged")
        End If
    End Sub

    Private Sub addRectangle_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles addRectangle.MouseDown, addSign.MouseDown
        If Not Me._playerEnabled Then
            PlayerEnabled = True
            RaiseEvent PlayerChanged(Me, Nothing, "playerEnabled")
            addRectangle.Fill = Nothing
        End If
    End Sub

    Private Sub addRectangle_MouseEnter(sender As Object, e As MouseEventArgs) Handles addRectangle.MouseEnter, addSign.MouseEnter
        If Not Me._playerEnabled Then
            addRectangle.Fill = Brushes.White
        End If
    End Sub

    Private Sub addRectangle_MouseLeave(sender As Object, e As MouseEventArgs) Handles addRectangle.MouseLeave, addSign.MouseLeave
        addRectangle.Fill = _color
    End Sub

    Private Sub removeRectangle_MouseEnter(sender As Object, e As MouseEventArgs) Handles removeRectangle.MouseEnter, removeSign.MouseEnter
        removeRectangle.Fill = Brushes.White
    End Sub

    Private Sub removeRectangle_MouseLeave(sender As Object, e As MouseEventArgs) Handles removeRectangle.MouseLeave, removeSign.MouseLeave
        removeRectangle.Fill = _removeButtonColor
    End Sub

    Private Sub removeRectangle_MouseDown(sender As Object, e As MouseEventArgs) Handles removeRectangle.MouseDown, removeSign.MouseDown
        PlayerEnabled = False
        RaiseEvent PlayerChanged(Me, Nothing, "playerDisabled")
        PreviousRectangle = SelectedRectangle
        SelectedRectangle = Nothing
        changeSelectedRectangle(Nothing)
    End Sub

    Private Sub PlayerCreationControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If Not _canBeDisabled Then
            removeRectangle.Visibility = Windows.Visibility.Hidden
            removeSign.Visibility = Windows.Visibility.Hidden
        End If

        If _playerEnabled Then
            PlayerEnabled = True
        Else
            PlayerEnabled = False
        End If
        _initialized = True
    End Sub
#End Region

End Class

