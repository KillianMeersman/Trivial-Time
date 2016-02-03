Imports System.Windows.Threading
Public Class DiceControl

    Private _dispatcherTimer As New DispatcherTimer
    Private _randomGenerator As New Random
    Private _maximum As Integer
    Private _dice As Integer
    Private _currentDiceValue As Integer
    Private _active As Boolean

    Private diceBounce As Byte = 0
    Private previousDice As Byte = 10

    Public ReadOnly Property CurrentDiceValue() As Integer
        Get
            Return _currentDiceValue
        End Get
    End Property

    Public ReadOnly Property DiceValue As Integer
        Get
            Return _dice
        End Get
    End Property

    Private _diceBeingThrown As Boolean
    Public ReadOnly Property DiceBeingThrown() As Boolean
        Get
            Return _diceBeingThrown
        End Get
    End Property

    Public Event DiceTick()
    Public Event DiceThrown()
    Public Event DiceThrownSecondAfter()

    Public Sub enableDice()
        Me.mainImage.Source = New BitmapImage(New Uri("/Resources/DiceThrow.png", UriKind.Relative))
        Me.IsHitTestVisible = True
    End Sub

    Public Function getDice(Optional maximum As Integer = 7, Optional enable As Boolean = True) As Integer
        _maximum = maximum
        Dim dice As Integer = _randomGenerator.Next(1, _maximum)
        _dice = dice
        If enable Then
            enableDice()
        End If
        Return dice
    End Function

    Public Function throwDice(Optional maximum As Integer = 7) As Integer
        _diceBeingThrown = True
        _active = True
        diceBounce = 0
        previousDice = 10
        Me.IsHitTestVisible = False
        _maximum = maximum
        _dispatcherTimer = New DispatcherTimer
        _dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, 5)
        Dim dice As Integer = _randomGenerator.Next(1, _maximum)
        _dice = dice

        AddHandler _dispatcherTimer.Tick, AddressOf dice_Tick
        _dispatcherTimer.Start()
        Return dice
    End Function

    Public Sub throwDiceByValue(diceValue As Integer)
        _diceBeingThrown = True
        _active = True
        diceBounce = 0
        previousDice = 10
        Me.IsHitTestVisible = False
        _dispatcherTimer = New DispatcherTimer
        _dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, 5)
        _dice = diceValue

        AddHandler _dispatcherTimer.Tick, AddressOf dice_Tick
        _dispatcherTimer.Start()
    End Sub

    Public Sub SuspendExecution()
        If _active Then
            _dispatcherTimer.Stop()
            _diceBeingThrown = False
        End If
    End Sub

    Public Sub ResumeExecution()
        If _active Then
            _dispatcherTimer.Start()
            _diceBeingThrown = True
        End If
    End Sub

    Public Sub StopExecution()
        _dispatcherTimer.Stop()
        _diceBeingThrown = False
        _active = False
        mainImage.Source = New BitmapImage(New Uri("/Resources/Dice" & _dice & ".png", UriKind.Relative))
    End Sub

    Private Sub dice_Tick()

        If diceBounce <= 29 Then
            Dim dice As Byte = _randomGenerator.Next(1, _maximum)

            diceBounce += 1

            _dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, diceBounce * 10)

            Do Until dice <> previousDice
                dice = _randomGenerator.Next(1, _maximum)
            Loop
            _currentDiceValue = dice
            RaiseEvent DiceTick()

            mainImage.Source = New BitmapImage(New Uri("/Resources/Dice" & dice & ".png", UriKind.Relative))

            previousDice = dice

        ElseIf diceBounce = 30 Then
            _currentDiceValue = _dice
            mainImage.Source = New BitmapImage(New Uri("/Resources/Dice" & _dice & ".png", UriKind.Relative))
            RaiseEvent DiceTick()
            RaiseEvent DiceThrown()
            _diceBeingThrown = False
            diceBounce += 1
            _dispatcherTimer.Interval = New TimeSpan(0, 0, 1)
        ElseIf diceBounce > 30 Then
            _dispatcherTimer.Stop()
            _active = False
            RaiseEvent DiceThrownSecondAfter()
        End If
    End Sub

    Private Sub Me_MouseEnter() Handles Me.MouseEnter
        Me.mainImage.Source = New BitmapImage(New Uri("/Resources/DiceThrowMouseEnter.png", UriKind.Relative))
    End Sub

    Private Sub Me_MouseLeave() Handles Me.MouseLeave
        Me.mainImage.Source = New BitmapImage(New Uri("/Resources/DiceThrow.png", UriKind.Relative))
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.IsHitTestVisible = False
    End Sub

End Class
