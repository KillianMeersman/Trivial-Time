Imports System.Windows.Media.Animation

Public Class MenuWindow

    'Class variables
    Private _selectedSave As String

    'State variables
    Private __saveMode As Boolean = False

    Public Sub New()
        InitializeComponent()
        resumeButton.Enabled = False
        saveButton.Enabled = False
        startButton.Enabled = False
        If Not Settings.ConnectionString = "" Then
            Dim parts() As String = ConnectionString.Split(New Char() {";"})
            serverTextbox.Text = parts(0)
            databaseTextbox.Text = parts(1)
            userIdTextbox.Text = parts(2)
            passwordBox.Password = parts(3)
        Else
            ConnectionString = "server=" & serverTextbox.Text & ";database=" & databaseTextbox.Text & ";uid=" & userIdTextbox.Text & ";pwd=" & passwordBox.Password
        End If
        refreshSaves()
    End Sub

    Public Sub warnUser(message As String, messageState As messageState)
        If messageState = MenuWindow.messageState.fault Then
            warningImage.Source = New BitmapImage(New Uri("/Resources/warningIcon.png", UriKind.Relative))
            warningTextblock.Foreground = Brushes.DarkRed
            warningTextblock.Text = message
            Dim animation As New DoubleAnimation
            animation.From = 1
            animation.To = 0
            animation.Duration = New Duration(TimeSpan.FromSeconds(1))
            animation.AutoReverse = True
            animation.RepeatBehavior = RepeatBehavior.Forever
            Me.warningTextblock.BeginAnimation(OpacityProperty, animation)

        ElseIf messageState = MenuWindow.messageState.warning Then
            warningImage.Source = New BitmapImage(New Uri("/Resources/yellowWarningIcon.png", UriKind.Relative))
            warningTextblock.BeginAnimation(OpacityProperty, Nothing)
            warningTextblock.Foreground = Brushes.Goldenrod
            warningTextblock.Text = message

        ElseIf messageState = MenuWindow.messageState.normal Then
            warningImage.Source = New BitmapImage(New Uri("/Resources/tickIcon.png", UriKind.Relative))
            warningTextblock.BeginAnimation(OpacityProperty, Nothing)
            warningTextblock.Foreground = Brushes.DarkGreen
            warningTextblock.Text = message
        End If
    End Sub

    Public Enum messageState
        normal = 1
        warning = 2
        fault = 3
    End Enum

#Region "Private methods"

    Private Sub exitButton_Click(sender As Object, e As RoutedEventArgs) Handles exitButton.MouseDown
        MainModule.Shutdown()
    End Sub

    Private Sub playButton_Click(sender As Object, e As RoutedEventArgs) Handles playButton.MouseDown
        switchGrid(startGrid)
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        MainModule.Shutdown()
    End Sub

    Private Sub startButton_Click(sender As Object, e As RoutedEventArgs) Handles startButton.MouseDown
        Dim player1 As New Player(player1CreationControl.nameTextBox.Text, player1CreationControl.SelectedRectangle.Fill, MainModule.mainWindow.player1InfoControl)
        Dim player2 As New Player(player2CreationControl.nameTextBox.Text, player2CreationControl.SelectedRectangle.Fill, MainModule.mainWindow.player2InfoControl)
        Dim player3 = Nothing
        Dim player4 = Nothing

        If player3CreationControl.PlayerEnabled Then
            player3 = New Player(player3CreationControl.nameTextBox.Text, player3CreationControl.SelectedRectangle.Fill, MainModule.mainWindow.player3InfoControl)
        End If

        If player4CreationControl.PlayerEnabled Then
            player4 = New Player(player4CreationControl.nameTextBox.Text, player4CreationControl.SelectedRectangle.Fill, MainModule.mainWindow.player4InfoControl)
        End If

        MainModule.startGame(player1, player2, player3, player4)
        resumeButton.Enabled = True

        'If player1CreationControl.PlayerColor.ToString = player2CreationControl.PlayerColor.ToString Then
        '    warnUser("Er zijn 2 of meer spelers met dezelfde pion kleur", True)
        'ElseIf player3CreationControl.PlayerEnabled And player4CreationControl.PlayerEnabled Then
        '    If player1CreationControl.PlayerColor.ToString = player3CreationControl.PlayerColor.ToString Or player2CreationControl.PlayerColor.ToString = player3CreationControl.PlayerColor.ToString Or player1CreationControl.PlayerColor.ToString = player4CreationControl.PlayerColor.ToString Or player2CreationControl.PlayerColor.ToString = player4CreationControl.PlayerColor.ToString Or player3CreationControl.PlayerColor.ToString = player4CreationControl.PlayerColor.ToString Then
        '        warnUser("Er zijn 2 of meer spelers met dezelfde pion kleur", True)
        '    Else
        '        MainModule.mainWindow.startGame(player1, player2, player3, player4)
        '        Me.Hide()
        '        MainModule.mainWindow.Show()
        '    End If
        'ElseIf player3CreationControl.PlayerEnabled Then
        '    If player1CreationControl.PlayerColor.ToString = player3CreationControl.PlayerColor.ToString Or player2CreationControl.PlayerColor.ToString = player3CreationControl.PlayerColor.ToString Then
        '        warnUser("Er zijn 2 of meer spelers met dezelfde pion kleur", True)
        '    Else
        '        MainModule.mainWindow.startGame(player1, player2, player3, player4)
        '        Me.Hide()
        '        MainModule.mainWindow.Show()
        '    End If
        'ElseIf player4CreationControl.PlayerEnabled Then
        '    If player1CreationControl.PlayerColor.ToString = player4CreationControl.PlayerColor.ToString Or player2CreationControl.PlayerColor.ToString = player4CreationControl.PlayerColor.ToString Then
        '        warnUser("Er zijn 2 of meer spelers met dezelfde pion kleur", True)
        '    Else
        '        MainModule.mainWindow.startGame(player1, player2, player3, player4)
        '        Me.Hide()
        '        MainModule.mainWindow.Show()
        '    End If
        'Else
        '    MainModule.mainWindow.startGame(player1, player2, player3, player4)
        '    Me.Hide()
        '    MainModule.mainWindow.Show()
        'End If
    End Sub

    Private Sub connectButton_Click(sender As Object, e As RoutedEventArgs) Handles connectButton.MouseDown
        'server=ineke.broeders.be; database=trivialtime1415; uid=ll-64375; pwd=thetarun
        ConnectionString = "server=" & serverTextbox.Text & ";database=" & databaseTextbox.Text & ";uid=" & userIdTextbox.Text & ";pwd=" & passwordBox.Password
        Try
            If resumeButton.Enabled = False Then
                MainModule.connectToDatabase()
                If MainModule.questionRepository.IsReady Then
                    warnUser("De vragen zijn geladen", messageState.normal)
                Else
                    warnUser("Er zijn minder dan 30 vragen per categorie!", messageState.warning)
                End If
            Else
                promptControl.Visibility = Windows.Visibility.Visible
            End If
        Catch
            If MainModule.questionRepository Is Nothing Then
                warnUser("De vragen zijn niet correct geladen!", messageState.fault)
                startButton.Enabled = False
            Else
                If MainModule.questionRepository.IsReady Then
                    warnUser("De vragen zijn niet correct geladen!" & Environment.NewLine & "De vragen in de cache worden gebruikt", messageState.warning)
                Else
                    warnUser("De vragen zijn niet correct geladen!" & Environment.NewLine & "De vragen in de cache worden gebruikt" & Environment.NewLine & "(minder dan 30 per categorie!)", messageState.warning)
                End If
            End If
        End Try
    End Sub

    Private Sub promptControl_Ok() Handles promptControl.Ok
        Try
            MainModule.connectToDatabase()
            If MainModule.questionRepository.IsReady Then
                warnUser("De vragen zijn geladen", messageState.normal)
            Else
                warnUser("Er zijn minder dan 30 vragen per categorie!", messageState.warning)
            End If
            resumeButton.Enabled = False
        Catch
            If MainModule.questionRepository Is Nothing Then
                warnUser("De vragen zijn niet correct geladen!", messageState.fault)
                startButton.Enabled = False
            Else
                If MainModule.questionRepository.IsReady Then
                    warnUser("De vragen zijn niet correct geladen!" & Environment.NewLine & "De vragen in de cache worden gebruikt", messageState.warning)
                Else
                    warnUser("De vragen zijn niet correct geladen!" & Environment.NewLine & "De vragen in de cache worden gebruikt" & Environment.NewLine & "(minder dan 30 per categorie!)", messageState.warning)
                End If
            End If
        End Try
    End Sub

    Private Sub optionsButton_Click(sender As Object, e As RoutedEventArgs) Handles optionsButton.MouseDown
        Me.fullscreenCheckbox.IsChecked = Settings.FullScreenEnabled
        Me.skipStartingPlayerCheckbox.IsChecked = Settings.SkipPlayerChoiceAnimation
        Me.diceTileHighlightCheckbox.IsChecked = Settings.DiceTickHighlightEffect
        Me.skipDiceWithClickCheckbox.IsChecked = Settings.SkipDiceWithClick
        Me.informQuestionCheckbox.IsChecked = Settings.InformQuestionOutcome
        Me.questionTimeSlider.Value = Settings.QuestionTime / 1000
        timeValueTextblock.Text = CInt(questionTimeSlider.Value)
        switchGrid(optionGrid)
    End Sub

    Private Sub helpButton_Click(sender As Object, e As RoutedEventArgs) Handles helpButton.MouseDown
        switchGrid(helpGrid)
    End Sub

    Private Sub fullscreenCheckbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles fullscreenCheckbox.Unchecked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.FullScreenEnabled = False
        End If
    End Sub

    Private Sub fullscreenCheckbox_Checked(sender As Object, e As RoutedEventArgs) Handles fullscreenCheckbox.Checked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.FullScreenEnabled = True
        End If
    End Sub

    Private Sub refreshSaves()
        Dim saveNames As List(Of String) = MainModule.getSaveNames
        saveSlot1.Enabled = False
        saveSlot1.Text = "Leeg"
        saveSlot2.Enabled = False
        saveSlot2.Text = "Leeg"
        saveSlot3.Enabled = False
        saveSlot3.Text = "Leeg"
        saveSlot4.Enabled = False
        saveSlot4.Text = "Leeg"
        saveSlot5.Enabled = False
        saveSlot5.Text = "Leeg"

        For i As Integer = 0 To saveNames.Count - 1
            If i >= 0 And i <= 4 Then
                saveNames(i) = saveNames(i).Substring(4)
                saveNames(i) = saveNames(i).Substring(0, saveNames(i).Length - 4)
                Select Case i
                    Case 0 : saveSlot1.Text = saveNames(0)
                    Case 1 : saveSlot2.Text = saveNames(1)
                    Case 2 : saveSlot3.Text = saveNames(2)
                    Case 3 : saveSlot4.Text = saveNames(3)
                    Case 4 : saveSlot5.Text = saveNames(4)
                End Select
            End If
        Next

        For Each child As Object In saveSlotStackpanel.Children
            If TypeOf child Is SaveSlotControl Then
                Dim saveSlot As SaveSlotControl = child
                If __saveMode Then
                    saveSlot.Enabled = True
                Else
                    If saveSlot.Text <> "Leeg" Then
                        saveSlot.Enabled = True
                    Else
                        saveSlot.Enabled = False
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub switchGrid(grid As Grid)
        helpGrid.Visibility = Windows.Visibility.Hidden
        startGrid.Visibility = Windows.Visibility.Hidden
        optionGrid.Visibility = Windows.Visibility.Hidden
        loadSaveGrid.Visibility = Windows.Visibility.Hidden
        grid.Visibility = Windows.Visibility.Visible
    End Sub

#End Region

#Region "Event handlers"

    Private Sub PlayerCreationControl_ColorChanged(sender As PlayerCreationControl, e As RoutedEventArgs, message As String) Handles player1CreationControl.PlayerChanged, player2CreationControl.PlayerChanged, player3CreationControl.PlayerChanged, player4CreationControl.PlayerChanged
        Dim ready As Boolean = True
        Dim playerCardReady(3) As Boolean
        Dim cardIndex As Integer = 0
        For i As Integer = 0 To startGrid.Children.Count - 1
            If TypeOf startGrid.Children(i) Is PlayerCreationControl Then
                Dim playerCard As PlayerCreationControl = startGrid.Children(i)

                If message <> "nameChanged" Then
                    If Not playerCard Is sender Then 'Toggle rectangles
                        If Not sender.SelectedRectangle Is Nothing Then
                            playerCard.toggleRectangle(sender.SelectedRectangle.Name, False)
                        End If
                        If Not sender.PreviousRectangle Is Nothing Then
                            playerCard.toggleRectangle(sender.PreviousRectangle.Name, True)
                        End If
                    End If
                End If

                If playerCard.PlayerEnabled Then 'Make list of playerCards and their status
                    If Not playerCard.SelectedRectangle Is Nothing And Not playerCard.PlayerName = "" Then
                        playerCardReady(cardIndex) = True
                    Else
                        playerCardReady(cardIndex) = False
                    End If
                Else
                    playerCardReady(cardIndex) = True
                End If
                cardIndex += 1
            End If
        Next
        For i As Integer = 0 To playerCardReady.Count - 1
            If playerCardReady(i) = False Then
                ready = False
            End If
        Next
        If ready Then
            startButton.Enabled = True
        Else
            startButton.Enabled = False
        End If
    End Sub

    Private Sub informQuestionCheckbox_Checked(sender As Object, e As RoutedEventArgs) Handles informQuestionCheckbox.Checked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.InformQuestionOutcome = True
        End If
    End Sub

    Private Sub informQuestionCheckbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles informQuestionCheckbox.Unchecked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.InformQuestionOutcome = False
        End If
    End Sub

    Private Sub resumeButton_Click(sender As Object, e As RoutedEventArgs) Handles resumeButton.MouseDown
        MainModule.switchWindow(Me, MainModule.mainWindow)
        MainModule.mainWindow.ResumeExecution()
    End Sub

    Private Sub diceTileHighlightCheckbox_Checked(sender As Object, e As RoutedEventArgs) Handles diceTileHighlightCheckbox.Checked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.DiceTickHighlightEffect = True
        End If
    End Sub

    Private Sub diceTileHighlightCheckbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles diceTileHighlightCheckbox.Unchecked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.DiceTickHighlightEffect = False
        End If
    End Sub

    Private Sub skipStartingPlayerCheckbox_Checked(sender As Object, e As RoutedEventArgs) Handles skipStartingPlayerCheckbox.Checked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.SkipPlayerChoiceAnimation = True
        End If
    End Sub

    Private Sub skipStartingPlayerCheckbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles skipStartingPlayerCheckbox.Unchecked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.SkipPlayerChoiceAnimation = False
        End If
    End Sub

    Private Sub questionTimeSlider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles questionTimeSlider.ValueChanged
        If Not MainModule.menuWindow Is Nothing Then
            Settings.QuestionTime = questionTimeSlider.Value * 1000
            timeValueTextblock.Text = CInt(questionTimeSlider.Value)
        End If
    End Sub

    Private Sub skipDiceWithClickCheckbox_Checked(sender As Object, e As RoutedEventArgs) Handles skipDiceWithClickCheckbox.Checked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.SkipDiceWithClick = True
        End If
    End Sub

    Private Sub skipDiceWithClickCheckbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles skipDiceWithClickCheckbox.Unchecked
        If Not MainModule.menuWindow Is Nothing Then
            Settings.SkipDiceWithClick = False
        End If
    End Sub

    Private Sub saveButton_MouseDown(sender As Object, e As RoutedEventArgs) Handles saveButton.MouseDown
        switchGrid(loadSaveGrid)
        __saveMode = True
        refreshSaves()
    End Sub

    Private Sub loadButton_MouseDown(sender As Object, e As RoutedEventArgs) Handles loadButton.MouseDown
        switchGrid(loadSaveGrid)
        __saveMode = False
        refreshSaves()
    End Sub

    Private Sub saveSlot_MouseDown(sender As SaveSlotControl, e As RoutedEventArgs) Handles saveSlot1.MouseDown, saveSlot2.MouseDown, saveSlot3.MouseDown, saveSlot4.MouseDown, saveSlot5.MouseDown
        If __saveMode Then
            If sender.Text <> "Leeg" Then
                _selectedSave = sender.Text
            Else
                _selectedSave = Nothing
            End If
            savePromptControl.Visibility = Windows.Visibility.Visible
        Else
            Try
                MainModule.loadGame(sender.Text)
            Catch
                warnUser("De save kon helaas niet geladen worden", messageState.warning)
                refreshSaves()
            End Try
        End If
    End Sub

    Private Sub savePromtControl_Ok() Handles savePromptControl.Ok
        If Not _selectedSave = Nothing Then
            Try
                MainModule.deleteSave(_selectedSave)
            Catch
            End Try
        End If
        Try
            MainModule.saveGame(savePromptControl.Text)
            refreshSaves()
        Catch
            warnUser("Er is een fout opgetreden tijdens het opslaan!", messageState.fault)
        End Try
    End Sub

#End Region

End Class