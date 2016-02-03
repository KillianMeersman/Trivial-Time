Imports System.Windows.Media.Animation
Imports System.Windows.Threading

Public Class QuestionControl

    Private Const fadeDuration As Integer = 1000

    Private _ticks As Integer
    Private _timer As New DispatcherTimer
    Private _question As Question
    Private _buttonColor As Brush
    Private _active As Boolean

    Private _questionTime As Integer
    Public Property QuestionTime() As Integer
        Get
            Return _questionTime
        End Get
        Set(ByVal value As Integer)
            _questionTime = value
        End Set
    End Property

    Public Event CorrectAnswer()
    Public Event WrongAnswer()
    Public Event FadeComplete()

    Public Sub throwQuestion(question As Question)
        _active = True
        _question = question

        Me.IsHitTestVisible = True

        Dim rndGen As New Random
        Dim positionOccupied(3) As Boolean

        If question.Question.Length > 115 Then
            questionTextblock.FontSize = 18
        Else
            questionTextblock.FontSize = 24
        End If
        questionTextblock.Text = question.Question

        Select Case question.Category
            Case category.Arts_and_Literature : categoryImage.Source = New BitmapImage(New Uri("/Resources/Literature.png", UriKind.Relative))
                categoryTextblock.Text = "Kunst en literatuur"
            Case category.Entertainment : categoryImage.Source = New BitmapImage(New Uri("/Resources/Entertainment.png", UriKind.Relative))
                categoryTextblock.Text = "Ontspanning"
            Case category.Geography : categoryImage.Source = New BitmapImage(New Uri("/Resources/Geography.png", UriKind.Relative))
                categoryTextblock.Text = "Aardrijkskunde"
            Case category.History : categoryImage.Source = New BitmapImage(New Uri("/Resources/History.png", UriKind.Relative))
                categoryTextblock.Text = "Geschiedenis"
            Case category.Science : categoryImage.Source = New BitmapImage(New Uri("/Resources/Science.png", UriKind.Relative))
                categoryTextblock.Text = "Wetenschap"
            Case category.Sports : categoryImage.Source = New BitmapImage(New Uri("/Resources/Sports.png", UriKind.Relative))
                categoryTextblock.Text = "Sport"
        End Select

        For i As Byte = 0 To 3
            Dim answer As String = Nothing

            Select Case i
                Case 0 : answer = question.Answer1
                Case 1 : answer = question.Answer2
                Case 2 : answer = question.Answer3
                Case 3 : answer = question.CorrectAnswer
            End Select

            Dim position As Byte = rndGen.Next(0, 4)
            Do Until positionOccupied(position) = False
                If position < 3 Then
                    position += 1
                Else
                    position = 0
                End If
            Loop

            positionOccupied(position) = True
            Select Case position
                Case 0 : answer1Button.Content = answer
                Case 1 : answer2Button.Content = answer
                Case 2 : answer3Button.Content = answer
                Case 3 : answer4Button.Content = answer
            End Select
        Next

        If _questionTime <> 0 Then
            timeTextblock.Text = CInt(_questionTime / 1000)
            timeTextblock.Foreground = Brushes.White
            _timer.Start()
        Else
            timeTextblock.Text = ""
        End If

        Me.Visibility = Windows.Visibility.Visible
        fadeIn()
    End Sub

    Public Sub SuspendExecution()
        If _active Then
            _timer.Stop()
        End If
    End Sub

    Public Sub ResumeExecution()
        If _active Then
            _timer.Start()
        End If
    End Sub

    Private Sub Timer_Tick()
        If _ticks >= (_questionTime / 1000) Then 'Time ran out
            timeTextblock.Text = "0"
            _timer.Stop()
            _active = False
            _ticks = 0
            RaiseEvent WrongAnswer()
            fadeOut()
        Else
            If (_questionTime / 1000) - _ticks <= 5 Then 'Time becomes red if <= 5 seconds
                timeTextblock.Foreground = Brushes.Red
            End If
            timeTextblock.Text = CInt((_questionTime / 1000) - _ticks)
            _ticks += 1
        End If
    End Sub

    Private Sub answerChosen(sender As Button, e As RoutedEventArgs) Handles answer1Button.Click, answer2Button.Click, answer3Button.Click, answer4Button.Click
        _timer.Stop()
        _active = False
        _ticks = 0
        sender.BorderThickness = New Thickness(3, 3, 3, 3)
        If sender.Content = _question.CorrectAnswer Then 'Correct answer
            sender.Background = Brushes.DarkGreen
            sender.BorderBrush = Brushes.DarkGreen
            RaiseEvent CorrectAnswer()
        Else                                            'Wrong answer
            sender.Background = Brushes.Red
            sender.BorderBrush = Brushes.Red
            RaiseEvent WrongAnswer()
        End If
        Me.IsHitTestVisible = False
        fadeOut()
    End Sub

    Public Sub New()
        InitializeComponent()
        _timer.Interval = New TimeSpan(0, 0, 1)
        AddHandler _timer.Tick, AddressOf Timer_Tick

        _buttonColor = answer1Button.Background
    End Sub

    Private Sub fadeIn()
        Dim animation As New DoubleAnimation
        animation.From = 0
        animation.To = 1
        animation.Duration = New Duration(TimeSpan.FromMilliseconds(fadeDuration))
        mainGrid.BeginAnimation(Grid.OpacityProperty, animation)
    End Sub

    Private Sub fadeOut()
        Dim animation As New DoubleAnimation
        AddHandler animation.Completed, AddressOf fadeOut_Complete
        animation.From = 1
        animation.To = 0
        animation.Duration = New Duration(TimeSpan.FromMilliseconds(fadeDuration))
        mainGrid.BeginAnimation(Grid.OpacityProperty, animation)
    End Sub

    Private Sub fadeOut_Complete()
        Me.Visibility = Windows.Visibility.Hidden
        resetButtons()
        RaiseEvent FadeComplete()
    End Sub

    Private Sub resetButtons()
        answer1Button.Background = _buttonColor
        answer1Button.BorderBrush = Nothing
        answer1Button.BorderThickness = New Thickness(1, 1, 1, 1)
        answer2Button.Background = _buttonColor
        answer2Button.BorderBrush = Nothing
        answer2Button.BorderThickness = New Thickness(1, 1, 1, 1)
        answer3Button.Background = _buttonColor
        answer3Button.BorderBrush = Nothing
        answer3Button.BorderThickness = New Thickness(1, 1, 1, 1)
        answer4Button.Background = _buttonColor
        answer4Button.BorderBrush = Nothing
        answer4Button.BorderThickness = New Thickness(1, 1, 1, 1)
    End Sub

End Class
