Public Class QuestionRepository

    Private Const questionTresshold As Integer = 30

    Private _questionMapper As New QuestionMapper

    Private _geographyQuestions As New List(Of Question)
    Private _usedGeographyQuestions As New List(Of Question)
    Public Property GeographyQuestions() As List(Of Question)
        Get
            Return _geographyQuestions
        End Get
        Set(ByVal value As List(Of Question))
            _geographyQuestions = value
        End Set
    End Property

    Private _entertainmentQuestions As New List(Of Question)
    Private _usedEntertainmentQuestions As New List(Of Question)
    Public Property EntertainmentQuestions() As List(Of Question)
        Get
            Return _entertainmentQuestions
        End Get
        Set(ByVal value As List(Of Question))
            _entertainmentQuestions = value
        End Set
    End Property

    Private _historyQuestions As New List(Of Question)
    Private _usedHistoryQuestions As New List(Of Question)
    Public Property HistoryQuestions() As List(Of Question)
        Get
            Return _historyQuestions
        End Get
        Set(ByVal value As List(Of Question))
            _historyQuestions = value
        End Set
    End Property

    Private _artsAndLiteratureQuestions As New List(Of Question)
    Private _usedArtsAndLiteratureQuestions As New List(Of Question)
    Public Property ArtsAndLiteratureQuestions() As List(Of Question)
        Get
            Return _artsAndLiteratureQuestions
        End Get
        Set(ByVal value As List(Of Question))
            _artsAndLiteratureQuestions = value
        End Set
    End Property

    Private _scienceQuestions As New List(Of Question)
    Private _usedScienceQuestions As New List(Of Question)
    Public Property ScienceQuestions() As List(Of Question)
        Get
            Return _scienceQuestions
        End Get
        Set(ByVal value As List(Of Question))
            _scienceQuestions = value
        End Set
    End Property

    Private _sportsQuestions As New List(Of Question)
    Private _usedSportsQuestions As New List(Of Question)
    Public Property SportsQuestions() As List(Of Question)
        Get
            Return _sportsQuestions
        End Get
        Set(ByVal value As List(Of Question))
            _sportsQuestions = value
        End Set
    End Property

    Public ReadOnly Property IsReady() As Boolean
        Get
            If (_geographyQuestions.Count >= questionTresshold) And (_entertainmentQuestions.Count >= questionTresshold) And (_historyQuestions.Count >= questionTresshold) And (_artsAndLiteratureQuestions.Count >= questionTresshold) And (_scienceQuestions.Count >= questionTresshold) And (_sportsQuestions.Count >= questionTresshold) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property


    Public Sub New(connectionString As String)
        sortQuestions(_questionMapper.getQuestions(connectionString))
        shuffleAllQuestions()
    End Sub

    Public Function getQuestion(category As Category) As Question
        Dim question As Question
        Select Case category
            Case Trivial_Time.category.Arts_and_Literature
                question = _artsAndLiteratureQuestions(0)
                _usedArtsAndLiteratureQuestions.Add(question)
                _artsAndLiteratureQuestions.RemoveAt(0)
                If _artsAndLiteratureQuestions.Count < 1 Then
                    _artsAndLiteratureQuestions.AddRange(_usedArtsAndLiteratureQuestions)
                    _usedArtsAndLiteratureQuestions.Clear()
                End If
                Return question
            Case Trivial_Time.category.Entertainment
                question = _entertainmentQuestions(0)
                _usedEntertainmentQuestions.Add(question)
                _entertainmentQuestions.RemoveAt(0)
                If _entertainmentQuestions.Count < 1 Then
                    _entertainmentQuestions.AddRange(_usedEntertainmentQuestions)
                    _usedEntertainmentQuestions.Clear()
                End If
                Return question
            Case Trivial_Time.category.Geography
                question = _geographyQuestions(0)
                _usedGeographyQuestions.Add(question)
                _geographyQuestions.RemoveAt(0)
                If _geographyQuestions.Count < 1 Then
                    _geographyQuestions.AddRange(_usedGeographyQuestions)
                    _usedGeographyQuestions.Clear()
                End If
                Return question
            Case Trivial_Time.category.History
                question = _historyQuestions(0)
                _usedHistoryQuestions.Add(question)
                _historyQuestions.RemoveAt(0)
                If _historyQuestions.Count < 1 Then
                    _historyQuestions.AddRange(_usedHistoryQuestions)
                    _usedHistoryQuestions.Clear()
                End If
                Return question
            Case Trivial_Time.category.Science
                question = _scienceQuestions(0)
                _usedScienceQuestions.Add(question)
                _scienceQuestions.RemoveAt(0)
                If _scienceQuestions.Count < 1 Then
                    _scienceQuestions.AddRange(_usedScienceQuestions)
                    _usedScienceQuestions.Clear()
                End If
                Return question
            Case Trivial_Time.category.Sports
                question = _sportsQuestions(0)
                _usedSportsQuestions.Add(question)
                _sportsQuestions.RemoveAt(0)
                If _sportsQuestions.Count < 1 Then
                    _sportsQuestions.AddRange(_usedSportsQuestions)
                    _usedSportsQuestions.Clear()
                End If
                Return question
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub shuffleAllQuestions()
        shuffleQuestions(_geographyQuestions)
        shuffleQuestions(_entertainmentQuestions)
        shuffleQuestions(_historyQuestions)
        shuffleQuestions(_artsAndLiteratureQuestions)
        shuffleQuestions(_scienceQuestions)
        shuffleQuestions(_sportsQuestions)
    End Sub

    Private Sub sortQuestions(questions As List(Of Question))
        For Each question As Question In questions
            Select Case question.Category
                Case category.Arts_and_Literature : _artsAndLiteratureQuestions.Add(question)
                Case category.Entertainment : _entertainmentQuestions.Add(question)
                Case category.Geography : _geographyQuestions.Add(question)
                Case category.History : _historyQuestions.Add(question)
                Case category.Science : _scienceQuestions.Add(question)
                Case category.Sports : _sportsQuestions.Add(question)
            End Select
        Next
    End Sub

    Private Sub shuffleQuestions(questions As List(Of Question)) 'Fisher-Yates algorithm
        Dim r As Random = New Random()
        For i = 0 To questions.Count - 1
            Dim index As Integer = r.Next(i, questions.Count)
            If i <> index Then

                Dim temp As Question = questions(i)
                questions(i) = questions(index)
                questions(index) = temp
            End If
        Next
    End Sub

End Class
