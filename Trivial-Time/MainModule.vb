Imports System.Windows.Threading
Friend Module MainModule

    Private app As Application
    Public mainWindow As MainWindow
    Public menuWindow As MenuWindow
    Public questionRepository As QuestionRepository
    Private settingsLogger As SettingsLogger
    Private gameLogger As GameLogger

    Public _players As List(Of Player)
    Public _currentPlayer As Byte = 0

    Public Sub main()
        app = New Application
        settingsLogger = New SettingsLogger
        gameLogger = New GameLogger
        mainWindow = New MainWindow
        menuWindow = New MenuWindow

        Try
            settingsLogger.getSettings()
        Catch
            settingsLogger.writeSettings()
        End Try

        Try
            connectToDatabase()
            menuWindow.warnUser("De vragen zijn geladen", Trivial_Time.MenuWindow.messageState.normal)
        Catch
            menuWindow.warnUser("De vragen zijn niet correct geladen", Trivial_Time.MenuWindow.messageState.fault)
            menuWindow.startButton.Enabled = False
        End Try

        _players = New List(Of Player)

        app.Run(menuWindow)

    End Sub

    Public Sub startGame(player1 As Player, player2 As Player, player3 As Player, player4 As Player)
        mainWindow = New MainWindow
        mainWindow.startGame(player1, player2, player3, player4)
        mainWindow.QuestionTime = menuWindow.questionTimeSlider.Value * 1000
        changeWindowState(mainWindow, Settings.FullScreenEnabled)
        switchWindow(menuWindow, mainWindow)
    End Sub

    Public Sub loadGame(saveName As String)
        mainWindow = New MainWindow

        Dim save As GameSave = gameLogger.loadGame(saveName)
        _players.Clear()
        _players.AddRange(save.Players)

        '_players(_currentPlayer).Pion.Position = save.Players(0).Pion.Position
        _currentPlayer = save.LastPlayer
        mainWindow.loadGame(1)
        switchWindow(menuWindow, mainWindow)
    End Sub

    Public Sub saveGame(saveName As String)
        Dim save = New GameSave(saveName)
        save.LastPlayer = _currentPlayer
        save.Players = _players
        gameLogger.saveGame(save)
    End Sub

    Public Function getSaveNames() As List(Of String)
        Return gameLogger.getSaveNames
    End Function

    Public Sub deleteSave(saveName As String)
        gameLogger.deleteSave(saveName)
    End Sub

    Public Sub switchWindow(fromWindow As Window, toWindow As Window)
        fromWindow.Hide()
        toWindow.Show()
        toWindow.Focus()
    End Sub

    Public Sub changeWindowState(window As Window, isMaximized As Boolean)
        If isMaximized Then
            window.WindowState = WindowState.Maximized
            window.WindowStyle = WindowStyle.None
        Else
            window.WindowState = WindowState.Normal
            window.WindowStyle = WindowStyle.SingleBorderWindow
        End If
    End Sub

    Public Sub connectToDatabase()
        questionRepository = New QuestionRepository(Settings.ConnectionString)
    End Sub

    Public Sub nextPlayer()
        If _currentPlayer = 0 Then
            _currentPlayer += 1
        ElseIf (_currentPlayer = 1) And (_players.Count > 2) Then
            _currentPlayer += 1
        ElseIf (_currentPlayer = 2) And (_players.Count > 3) Then
            _currentPlayer += 1
        Else
            _currentPlayer = 0
        End If
    End Sub

    Public Sub Shutdown()
        settingsLogger.writeSettings()
        Application.Current.Shutdown()
        End
    End Sub

End Module