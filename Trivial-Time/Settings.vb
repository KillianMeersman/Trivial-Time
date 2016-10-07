Public Module Settings

    Private _fullScreen As Boolean
    Public Property FullScreenEnabled() As Boolean
        Get
            Return _fullScreen
        End Get
        Set(ByVal value As Boolean)
            _fullScreen = value
        End Set
    End Property

    Private _skipPlayerChoiceAnimation As Boolean
    Public Property SkipPlayerChoiceAnimation() As Boolean
        Get
            Return _skipPlayerChoiceAnimation
        End Get
        Set(ByVal value As Boolean)
            _skipPlayerChoiceAnimation = value
        End Set
    End Property

    Private _diceTickHighlight As Boolean
    Public Property DiceTickHighlightEffect() As Boolean
        Get
            Return _diceTickHighlight
        End Get
        Set(ByVal value As Boolean)
            _diceTickHighlight = value
        End Set
    End Property

    Private _skipDiceClick As Boolean
    Public Property SkipDiceWithClick() As Boolean
        Get
            Return _skipDiceClick
        End Get
        Set(ByVal value As Boolean)
            _skipDiceClick = value
        End Set
    End Property

    Private _informQuestion As Boolean
    Public Property InformQuestionOutcome() As Boolean
        Get
            Return _informQuestion
        End Get
        Set(ByVal value As Boolean)
            _informQuestion = value
        End Set
    End Property

    Private _questionTime As UInteger
    Public Property QuestionTime() As UInteger
        Get
            Return _questionTime
        End Get
        Set(ByVal value As UInteger)
            _questionTime = value
        End Set
    End Property

    Private _connectionString As String
    Public Property ConnectionString() As String
        Get
            Return _connectionString
        End Get
        Set(ByVal value As String)
            _connectionString = value
        End Set
    End Property

    Public Sub createSettings()
        _fullScreen = True
        _skipPlayerChoiceAnimation = False
        _diceTickHighlight = True
        _skipDiceClick = True
        _informQuestion = False
        _questionTime = 15
    End Sub

End Module

