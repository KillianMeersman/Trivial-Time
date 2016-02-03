Imports MySql.Data.MySqlClient
Public Class QuestionMapper

    Public Function getQuestions(connectionString As String) As List(Of Question)
        Dim connection As New MySqlConnection(connectionString)
        Dim cmd As New MySqlCommand("SELECT * FROM tblvragen INNER JOIN tblcategorie ON tblvragen.categorieID = tblcategorie.categorieID;", connection)
        Dim questions As New List(Of Question)
        Try
            connection.Open()

            Dim dr As MySqlDataReader = cmd.ExecuteReader()

            Dim q As Question
            Do While (dr.Read)
                q = New Question

                q.Question = dr.Item("vraag")
                q.CorrectAnswer = dr.Item("correctAntwoord")
                q.Answer1 = dr.Item("foutAntwoord1")
                q.Answer2 = dr.Item("foutAntwoord2")
                q.Answer3 = dr.Item("foutAntwoord3")
                Select Case dr.Item("categorieNaam")
                    Case "Geography" : q.Category = category.Geography
                    Case "Entertainment" : q.Category = category.Entertainment
                    Case "History" : q.Category = category.History
                    Case "Arts_and_Literature" : q.Category = category.Arts_and_Literature
                    Case "Science" : q.Category = category.Science
                    Case "Sports" : q.Category = category.Sports
                    Case Else : Throw New Exception("Unknown question category")
                End Select

                questions.Add(q)
            Loop

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connection.Close()
            connection.Dispose()
            cmd.Dispose()
        End Try
        Return questions
    End Function
End Class
