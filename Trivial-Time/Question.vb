Public Class Question

    Private _question As String
    Private _wrongAnswers(3) As String
    Private _correctAnswer As String
    Private _category As category

    Public Property Question() As String
        Get
            Return _Question
        End Get
        Set(ByVal value As String)
            _Question = value
        End Set
    End Property

    Public Property Answer1() As String
        Get
            Return _wrongAnswers(0)
        End Get
        Set(ByVal value As String)
            _wrongAnswers(0) = value
        End Set
    End Property

    Public Property Answer2() As String
        Get
            Return _wrongAnswers(1)
        End Get
        Set(ByVal value As String)
            _wrongAnswers(1) = value
        End Set
    End Property

    Public Property Answer3() As String
        Get
            Return _wrongAnswers(2)
        End Get
        Set(ByVal value As String)
            _wrongAnswers(2) = value
        End Set
    End Property

    Public Property CorrectAnswer() As String
        Get
            Return _correctAnswer
        End Get
        Set(ByVal value As String)
            _correctAnswer = value
        End Set
    End Property

    Public Property Category() As category
        Get
            Return _category
        End Get
        Set(ByVal value As category)
            _category = value
        End Set
    End Property

    Public Sub New()

    End Sub

End Class