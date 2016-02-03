Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Public Class MainWindow

    'Constants
    Private Const _resizeFactor As Double = 8           'Factor for resizing same-tile pions
    Protected Const _pionSize As Double = 50            'Pion width and height (in pixels)
    Private Const _animationTime As Double = 3000       'Duration for movement animations (in milliseconds)
    Private Const _notificationTime As Double = 3000    'Duration for notifications to appear / dissappear (in milliseconds)

    'Class variables
    Private _dice As Integer = 6
    Private _rectangleRepository(65) As RectangleControl
    Private _wedgeRepository(5) As WedgeControl
    Private _dispatcherTimer As DispatcherTimer
    Private _buttonForeGround As Brush
    Private _questionTime As UInteger
    Private _soundPlayer As New MediaPlayer

    'State variables
    Private __startingPlayerChosen As Boolean = False
    Private __secondClick As Boolean = False
    Private __cheatsOn As Boolean = False

    Public Property Dice() As Integer
        Get
            Return _dice
        End Get
        Set(ByVal value As Integer)
            _dice = value
        End Set
    End Property

    Public Property QuestionTime() As UInteger
        Get
            Return _questionTime
        End Get
        Set(ByVal value As UInteger)
            _questionTime = value
            Me.questionPresenter.QuestionTime = value
        End Set
    End Property

    Public Sub startGame(player1 As Player, player2 As Player, player3 As Player, player4 As Player)
        Me.questionPresenter.QuestionTime = Settings.QuestionTime

        MainModule._players.Clear()
        MainModule._players.AddRange({player1, player2})
        MainModule._currentPlayer = 0
        player1.PlayerInfo = player1InfoControl
        player2.PlayerInfo = player2InfoControl

        If Not player3 Is Nothing Then 'Player 3
            MainModule._players.Add(player3)
            player3.PlayerInfo = player3InfoControl
        Else
            player3InfoControl.IsEnabled = False
        End If

        If Not player4 Is Nothing Then 'Player 4
            MainModule._players.Add(player4)
            player4.PlayerInfo = player4InfoControl
        Else
            player4InfoControl.IsEnabled = False
        End If
        drawPions(CenterHex)
        chooseStartingPlayer()
    End Sub

    Public Sub loadGame(dice As Integer)
        Me.questionPresenter.QuestionTime = Settings.QuestionTime

        For Each player As Player In _players
            Select Case _players.IndexOf(player)
                Case 0 : player.PlayerInfo = player1InfoControl
                Case 1 : player.PlayerInfo = player2InfoControl
                Case 2 : player.PlayerInfo = player3InfoControl
                Case 3 : player.PlayerInfo = player4InfoControl
            End Select
            drawPion(player)
        Next

        disableAllTiles()
        __startingPlayerChosen = True
        _dice = diceControl.getDice
        AddHandler diceControl.DiceTick, AddressOf Dice_Tick
        AddHandler diceControl.DiceThrown, AddressOf Dice_Thrown
        HighlightPlayer()
        notificationControl.notify(_players(_currentPlayer).Name & " was aan de beurt", _notificationTime, "Gooi de dobbelsteen!")
        MainModule.menuWindow.resumeButton.Enabled = True
        MainModule.menuWindow.saveButton.Enabled = True
    End Sub

    Public Sub SuspendExecution()
        Me.questionPresenter.SuspendExecution()
        Me.notificationControl.SuspendExecution()
        Me.diceControl.SuspendExecution()
    End Sub

    Public Sub ResumeExecution()
        Me.questionPresenter.ResumeExecution()
        Me.notificationControl.ResumeExecution()
        Me.diceControl.ResumeExecution()
    End Sub

    Public Sub New()
        InitializeComponent()
        _buttonForeGround = backRectangle.Fill
    End Sub

#Region "Routine chooseStartingPlayer"

    Private Sub chooseStartingPlayer()
        Gameboard.IsHitTestVisible = False
        If Not Settings.SkipPlayerChoiceAnimation Then
            AddHandler notificationControl.NotificationComplete, AddressOf chooseStartingPlayer_throwDice
            notificationControl.notify("Wie mag er beginnen?", _notificationTime)
        Else
            Dim rndGen As New Random
            _currentPlayer = rndGen.Next(_players.Count)
            AddHandler diceControl.DiceThrown, AddressOf Dice_Thrown
            notificationControl.notify(_players(_currentPlayer).Name & " mag beginnen!", _notificationTime, "Gooi de dobbelsteen!")
            __startingPlayerChosen = True
            _dice = diceControl.getDice
            HighlightPlayer()
        End If
    End Sub

    Private Sub chooseStartingPlayer_throwDice()
        RemoveHandler notificationControl.NotificationComplete, AddressOf chooseStartingPlayer_throwDice
        AddHandler diceControl.DiceThrownSecondAfter, AddressOf chooseStartingPlayer_diceThrown
        _currentPlayer = diceControl.throwDice(_players.Count + 1) - 1
    End Sub

    Private Sub chooseStartingPlayer_diceThrown()
        RemoveHandler diceControl.DiceThrownSecondAfter, AddressOf chooseStartingPlayer_diceThrown
        AddHandler diceControl.DiceThrown, AddressOf Dice_Thrown
        notificationControl.notify(_players(_currentPlayer).Name & " mag beginnen!", _notificationTime, "Gooi de dobbelsteen!")
        __startingPlayerChosen = True
        _dice = diceControl.getDice()
        HighlightPlayer()
    End Sub

#End Region

#Region "Private methods"

    Private Sub initializeTileSettings() 'Startup routine for initializing settings

        Dim ID As Integer = 0
        Dim Control As Control = Nothing

        For Each element As Object In Gameboard.Children 'Fill the control-repositories
            If TypeOf element Is Control Then
                Control = element
                If TypeOf Control Is RectangleControl Then
                    ID = Control.Name.Substring(1, Control.Name.Length - 1)
                    _rectangleRepository(ID) = Control
                ElseIf TypeOf element Is WedgeControl Then
                    ID = Control.Name.Substring(5, 1)
                    _wedgeRepository(ID) = Control
                End If
            End If
        Next
        For Each rectangleControl As RectangleControl In _rectangleRepository 'Set rectangleControls neighbors
            ID = rectangleControl.Name.Substring(1, rectangleControl.Name.Length - 1)
            If ID <> 5 And ID <> 11 And ID <> 17 And ID <> 23 And ID <> 29 And ID <> 35 And ID <> 65 And ID <> 40 And ID <> 45 And ID <> 50 And ID <> 55 And ID <> 60 Then
                rectangleControl.Neighbors.Add(_rectangleRepository(ID + 1))
            End If
            If ID <> 0 And ID <> 6 And ID <> 12 And ID <> 18 And ID <> 24 And ID <> 30 And ID <> 36 And ID <> 41 And ID <> 46 And ID <> 51 And ID <> 56 And ID <> 61 Then
                rectangleControl.Neighbors.Add(_rectangleRepository(ID - 1))
            End If
        Next

        'Settings for Wedge0
        T35.Neighbors.Add(Wedge0)
        T36.Neighbors.Add(Wedge0)
        T0.Neighbors.Add(Wedge0)
        Wedge0.Neighbors.AddRange({T35, T36, T0})

        'Settings for Wedge1
        T5.Neighbors.Add(Wedge1)
        T6.Neighbors.Add(Wedge1)
        T41.Neighbors.Add(Wedge1)
        Wedge1.Neighbors.AddRange({T5, T6, T41})

        'Settings for Wedge2
        T11.Neighbors.Add(Wedge2)
        T12.Neighbors.Add(Wedge2)
        T46.Neighbors.Add(Wedge2)
        Wedge2.Neighbors.AddRange({T11, T12, T46})

        'Settings for Wedge3
        T17.Neighbors.Add(Wedge3)
        T18.Neighbors.Add(Wedge3)
        T51.Neighbors.Add(Wedge3)
        Wedge3.Neighbors.AddRange({T17, T18, T51})

        'Settings for Wedge4
        T23.Neighbors.Add(Wedge4)
        T24.Neighbors.Add(Wedge4)
        T56.Neighbors.Add(Wedge4)
        Wedge4.Neighbors.AddRange({T23, T24, T56})

        'Settings for Wedge5
        T29.Neighbors.Add(Wedge5)
        T30.Neighbors.Add(Wedge5)
        T61.Neighbors.Add(Wedge5)
        Wedge5.Neighbors.AddRange({T29, T30, T61})

        'Settings for CenterHex
        T40.Neighbors.Add(CenterHex)
        T45.Neighbors.Add(CenterHex)
        T50.Neighbors.Add(CenterHex)
        T55.Neighbors.Add(CenterHex)
        T60.Neighbors.Add(CenterHex)
        T65.Neighbors.Add(CenterHex)
        CenterHex.Neighbors.AddRange({T40, T45, T50, T55, T60, T65})

    End Sub

    Private Function getAvailableTiles(sender As ITileBase, dice As Integer) As List(Of ITileBase) 'Returns a list of tiles the player can move to
        Dim testTiles As New List(Of ITileBase)
        Dim currentTiles As New List(Of ITileBase)
        Dim checkedTiles As New List(Of ITileBase)
        Dim returnList As New List(Of ITileBase)
        testTiles.Add(sender)
        currentTiles.Add(sender)
        checkedTiles.Add(sender)

        For i As Integer = 0 To dice - 1
            For Each tile As ITileBase In currentTiles
                Dim fsfkl As Control = tile
                Dim testName As String = fsfkl.Name
                testTiles.RemoveAt(testTiles.IndexOf(tile))
                For Each neighbor As ITileBase In tile.Neighbors
                    If Not checkedTiles.Contains(neighbor) Then
                        testTiles.Add(neighbor)
                    End If
                    checkedTiles.Add(neighbor)
                Next
            Next
            currentTiles.Clear()
            currentTiles.AddRange(testTiles)
        Next

        Dim interfaceToControl As Control
        For Each tile As ITileBase In currentTiles
            interfaceToControl = tile
            returnList.Add(tile)
        Next

        Return returnList
    End Function

    Private Sub highlightAvailabeTiles(sender As ITileBase, dice As Integer)
        Dim tiles As List(Of ITileBase) = getAvailableTiles(sender, dice)

        For Each tile As Object In Gameboard.Children
            If TypeOf tile Is ITileBase Then
                If tiles.Contains(tile) Then
                    tile.Opacity = 1
                Else
                    tile.Opacity = 0.2
                End If
            End If
        Next
    End Sub

    Private Sub enableAvailableTiles(sender As ITileBase)
        Dim tiles As List(Of ITileBase) = getAvailableTiles(sender, _dice)

        For Each tile As Object In Gameboard.Children
            If TypeOf tile Is ITileBase Then
                If tiles.Contains(tile) Then
                    tile.IsEnabled = True
                Else
                    tile.IsEnabled = False
                End If
            End If
        Next
    End Sub

    Private Sub enableAllTiles()
        For Each tile As Object In Gameboard.Children
            If TypeOf tile Is ITileBase Then
                tile.IsEnabled = True
            End If
        Next
    End Sub

    Private Sub disableAllTiles()
        For Each tile As Object In Gameboard.Children
            If TypeOf tile Is ITileBase Then
                tile.IsEnabled = False
            End If
        Next
    End Sub

    Private Sub drawCenters() 'A development method
        For i As Integer = 0 To Gameboard.Children.Count - 1
            If TypeOf Gameboard.Children(i) Is RectangleControl Or TypeOf Gameboard.Children(i) Is WedgeControl Then
                Dim circle As New Ellipse
                Dim host As Control = Gameboard.Children(i)
                circle.Height = 10
                circle.Width = 10
                circle.Fill = Brushes.Red
                circle.HorizontalAlignment = Windows.HorizontalAlignment.Left
                circle.VerticalAlignment = Windows.VerticalAlignment.Top
                Canvas.SetLeft(circle, Canvas.GetLeft(host) + host.Width / 2.5)
                Canvas.SetTop(circle, Canvas.GetTop(host) + host.Height / 2.5)
                Gameboard.Children.Add(circle)
            End If
        Next
    End Sub

    Public Sub drawPions(location As Control) 'Draws all playerPions at the given location
        Dim dFactor As Double

        If TypeOf location Is RectangleControl Then
            dFactor = 2.5
        Else
            dFactor = 2
        End If

        Dim pion As PlayerPion
        For i As Byte = 0 To MainModule._players.Count - 1
            If Not MainModule._players(i) Is Nothing Then
                pion = MainModule._players(i).Pion
                pion.Position = location
                pion.HorizontalAlignment = Windows.HorizontalAlignment.Left
                pion.VerticalAlignment = Windows.VerticalAlignment.Top
                pion.Size = _pionSize

                AddHandler pion.MouseEnter, AddressOf PlayerPion_MouseEnter
                AddHandler pion.MouseLeave, AddressOf PlayerPion_MouseLeave
                AddHandler pion.MouseDown, AddressOf PlayerPion_MouseDown

                Canvas.SetLeft(pion, location.GetValue(Canvas.LeftProperty) + location.Width / dFactor - pion.Width / dFactor)
                Canvas.SetTop(pion, location.GetValue(Canvas.TopProperty) + location.Height / dFactor - pion.Height / dFactor)

                Gameboard.Children.Add(pion)
            End If
        Next
        'resizePions(location)
    End Sub

    Private Sub drawPion(player As Player)
        Dim dFactor As Double
        AddHandler player.Pion.MouseEnter, AddressOf PlayerPion_MouseEnter
        AddHandler player.Pion.MouseLeave, AddressOf PlayerPion_MouseLeave
        AddHandler player.Pion.MouseDown, AddressOf PlayerPion_MouseDown

        If TypeOf player.Pion.Position Is RectangleControl Then
            dFactor = 2.5
        Else
            dFactor = 2
        End If

        player.Pion.HorizontalAlignment = Windows.HorizontalAlignment.Left
        player.Pion.VerticalAlignment = Windows.VerticalAlignment.Top
        player.Pion.Size = _pionSize

        Dim location As Control = player.Pion.Position

        Canvas.SetLeft(player.Pion, location.GetValue(Canvas.LeftProperty) + location.Width / dFactor - player.Pion.Width / dFactor)
        Canvas.SetTop(player.Pion, location.GetValue(Canvas.TopProperty) + location.Height / dFactor - player.Pion.Height / dFactor)

        Gameboard.Children.Add(player.Pion)
    End Sub

    Private Sub movePion(player As Player, destination As Control) 'Animates a pion's movement from one control to another
        Dim dFactor As Double

        If TypeOf destination Is RectangleControl Then
            dFactor = 2.5
        Else
            dFactor = 2
        End If

        player.Pion.Position = destination

        Dim numberOfPions As Byte = 0
        For i As Byte = 0 To _players.Count - 1                 'Resize pions
            If (_players(i).Pion.Position Is destination) Then
                Dim xAnimation As New DoubleAnimation
                Dim yAnimation As New DoubleAnimation
                Dim sizeAnimation As New DoubleAnimation

                xAnimation.To = Canvas.GetLeft(destination) + (destination.Width / dFactor) - (_pionSize / dFactor) + (numberOfPions * _resizeFactor / 2)
                yAnimation.To = Canvas.GetTop(destination) + (destination.Height / dFactor) - (_pionSize / dFactor) + (numberOfPions * _resizeFactor / 2)

                xAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(_animationTime))
                yAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(_animationTime))

                _players(i).Pion.BeginAnimation(Canvas.LeftProperty, xAnimation)
                _players(i).Pion.BeginAnimation(Canvas.TopProperty, yAnimation)

                sizeAnimation.To = _pionSize - (numberOfPions * _resizeFactor)
                sizeAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(_animationTime / 2))

                _players(i).Pion.BeginAnimation(WidthProperty, sizeAnimation)
                _players(i).Pion.BeginAnimation(HeightProperty, sizeAnimation)
                numberOfPions += 1
            End If
        Next
    End Sub

    Private Sub resizePions(location As Control)
        Dim numberOfPions As Byte = 0
        For i As Integer = 0 To MainModule._players.Count - 1
            If Not _players(i) Is Nothing Then
                If _players(i).Pion.Position Is location Then

                    Dim widthAnimation As New DoubleAnimation
                    Dim heightAnimation As New DoubleAnimation

                    widthAnimation.From = _players(i).Pion.Width
                    heightAnimation.From = _players(i).Pion.Height

                    widthAnimation.To = _pionSize - (numberOfPions * _resizeFactor)
                    heightAnimation.To = _pionSize - (numberOfPions * _resizeFactor)

                    widthAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(_animationTime / 2))
                    heightAnimation.Duration = New Duration(TimeSpan.FromMilliseconds(_animationTime / 2))

                    _players(i).Pion.BeginAnimation(WidthProperty, widthAnimation)
                    _players(i).Pion.BeginAnimation(HeightProperty, heightAnimation)

                    Canvas.SetLeft(_players(i).Pion, (Canvas.GetLeft(_players(i).Pion) + (numberOfPions * _resizeFactor) / 2))
                    Canvas.SetTop(_players(i).Pion, (Canvas.GetTop(_players(i).Pion) + (numberOfPions * _resizeFactor) / 2))

                    Gameboard.UpdateLayout()
                    numberOfPions += 1

                End If
            End If
        Next
    End Sub

    Private Sub HighlightPlayer()
        For Each Player As Player In _players
            If Not Player Is Nothing Then
                If Player Is _players(_currentPlayer) Then
                    Player.Pion.Highlight(_pionSize, True)
                    Player.PlayerInfo.Highlight()
                Else
                    Player.Pion.ResetHighlight()
                    Player.PlayerInfo.ResetHighlight()
                End If
            End If
        Next
    End Sub

#End Region

#Region "Event handlers"

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        initializeTileSettings()

        For Each element As Object In Gameboard.Children
            If TypeOf element Is ITileBase Then
                Dim control As Control = element
                AddHandler control.MouseDown, AddressOf Tile_MouseDown
            End If
        Next
    End Sub

    Private Sub MainWindow_Closing(sender As Object, e As EventArgs) Handles Me.Closing
        MainModule.Shutdown()
    End Sub

    Private Sub Me_MouseDown() Handles Me.MouseDown
        If __secondClick And diceControl.DiceBeingThrown And Settings.SkipDiceWithClick And __startingPlayerChosen Then
            diceControl.StopExecution()
            Dice_Thrown()
        ElseIf diceControl.DiceBeingThrown = True And __secondClick = False And Settings.SkipDiceWithClick And __startingPlayerChosen Then
            __secondClick = True
        End If
    End Sub

    Private Sub DiceControl_MouseDown() Handles diceControl.MouseDown
        MainModule.menuWindow.saveButton.Enabled = False
        If __cheatsOn Then
            enableAllTiles()
            Gameboard.IsHitTestVisible = True
        Else
            _dice = diceControl.throwDice
        End If
    End Sub

    Private Sub Tile_MouseDown(sender As ITileBase, e As EventArgs)
        MainModule.menuWindow.saveButton.Enabled = False
        movePion(MainModule._players(_currentPlayer), sender)

        Gameboard.IsHitTestVisible = False
        Select Case sender.Category
            Case Category.Arts_and_Literature : questionPresenter.throwQuestion(MainModule.questionRepository.getQuestion(Category.Arts_and_Literature))
            Case Category.Entertainment : questionPresenter.throwQuestion(MainModule.questionRepository.getQuestion(Category.Entertainment))
            Case Category.Geography : questionPresenter.throwQuestion(MainModule.questionRepository.getQuestion(Category.Geography))
            Case Category.History : questionPresenter.throwQuestion(MainModule.questionRepository.getQuestion(Category.History))
            Case Category.Science : questionPresenter.throwQuestion(MainModule.questionRepository.getQuestion(Category.Science))
            Case Category.Sports : questionPresenter.throwQuestion(MainModule.questionRepository.getQuestion(Category.Sports))
            Case Category.None
                disableAllTiles()
                Gameboard.IsHitTestVisible = True
                diceControl.enableDice()
            Case Else
                Throw New Exception("Unknown category")
                Gameboard.IsHitTestVisible = True
        End Select

        If sender Is CenterHex Then
            Dim player As Player = MainModule._players(_currentPlayer)
            If player.HasArtsAndLiterature And player.HasEntertainment And player.HasGeography And player.HasHistory And player.HasScience And player.HasSports Then
                winControl.Text = _players(_currentPlayer).Name & " wint!"
                winControl.Visibility = Windows.Visibility.Visible
            End If
        End If
    End Sub

    Private Sub winControl_Ok() Handles winControl.Ok
        MainModule.menuWindow.resumeButton.Enabled = False
        MainModule.menuWindow.saveButton.Enabled = False
        MainModule.switchWindow(Me, MainModule.menuWindow)
    End Sub

    Private Sub Dice_Thrown()
        __secondClick = False
        MainModule.menuWindow.saveButton.Enabled = False
        enableAvailableTiles(_players(_currentPlayer).Pion.Position)
        Gameboard.IsHitTestVisible = True
    End Sub

    Private Sub Dice_Tick() Handles diceControl.DiceTick
        _soundPlayer.Open(New Uri("../../Resources/TickSound.wav", UriKind.Relative))
        _soundPlayer.Play()

        If Settings.DiceTickHighlightEffect And __startingPlayerChosen Then
            highlightAvailabeTiles(_players(_currentPlayer).Pion.Position, diceControl.CurrentDiceValue)
        End If
    End Sub

    Private Sub backRectangle_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles backRectangle.MouseDown, backImage.MouseDown
        SuspendExecution()
        MainModule.switchWindow(Me, MainModule.menuWindow)
    End Sub

    Private Sub backRectangle_MouseEnter(sender As Object, e As MouseEventArgs) Handles backRectangle.MouseEnter, backImage.MouseEnter
        backRectangle.Fill = Brushes.White
    End Sub

    Private Sub backRectangle_MouseLeave(sender As Object, e As MouseEventArgs) Handles backRectangle.MouseLeave, backImage.MouseLeave
        backRectangle.Fill = _buttonForeGround
    End Sub

    Private Sub questionPresenter_fadeOutComplete() Handles questionPresenter.FadeComplete
        disableAllTiles()
        Gameboard.IsHitTestVisible = True
        _dice = diceControl.getDice
    End Sub

    Private Sub wrongAnswer() Handles questionPresenter.WrongAnswer
        MainModule.menuWindow.saveButton.Enabled = True
        _soundPlayer.Open(New Uri("../../Resources/WrongSound.wav", UriKind.Relative))
        _soundPlayer.Play()
        If Settings.InformQuestionOutcome Then
            upperNotificationControl.notify("Fout!", _notificationTime / 2)
        End If
        MainModule.nextPlayer()
        HighlightPlayer()
    End Sub

    Private Sub correctAnswer() Handles questionPresenter.CorrectAnswer
        MainModule.menuWindow.saveButton.Enabled = True
        _soundPlayer.Open(New Uri("../../Resources/CorrectSound.wav", UriKind.Relative))
        _soundPlayer.Play()
        If Settings.InformQuestionOutcome Then
            upperNotificationControl.notify("Juist!", _notificationTime / 2)
        End If

        Dim player As Player = MainModule._players(MainModule._currentPlayer)
        If TypeOf player.Pion.Position Is WedgeControl Then
            Select Case player.Pion.Position.Category
                Case Category.Arts_and_Literature : player.HasArtsAndLiterature = True
                Case Category.Entertainment : player.HasEntertainment = True
                Case Category.Geography : player.HasGeography = True
                Case Category.History : player.HasHistory = True
                Case Category.Science : player.HasScience = True
                Case Category.Sports : player.HasSports = True
                Case Else : Throw New Exception("Unknown category")
            End Select
        End If
    End Sub

    Private Sub PlayerPion_MouseDown(sender As PlayerPion, e As RoutedEventArgs)
        If getAvailableTiles(_players(_currentPlayer).Pion.Position, _dice).Contains(sender.Position) Or __cheatsOn Then
            Tile_MouseDown(sender.Position, e)
        End If
    End Sub

    Private Sub PlayerPion_MouseEnter(sender As PlayerPion, e As RoutedEventArgs)
        If getAvailableTiles(_players(_currentPlayer).Pion.Position, _dice).Contains(sender.Position) Or __cheatsOn Then
            sender.Position.FlashUp()
        End If
    End Sub

    Private Sub PlayerPion_MouseLeave(sender As PlayerPion, e As RoutedEventArgs)
        sender.Position.FlashDown()
    End Sub

#End Region

End Class

Public Enum Category
    Geography = 1
    Entertainment = 2
    History = 3
    Arts_and_Literature = 4
    Science = 5
    Sports = 6
    None = 7
End Enum