Imports System.IO
Public Class SettingsLogger

    Public Sub writeSettings()
        Using writer As New StreamWriter("settings.txt", False)
            writer.WriteLine("*** Game settings ***")
            writer.WriteLine("fullscreen: " & Settings.FullScreenEnabled)
            writer.WriteLine("skipStartingSequence: " & Settings.SkipPlayerChoiceAnimation)
            writer.WriteLine("diceTickHighlight: " & Settings.DiceTickHighlightEffect)
            writer.WriteLine("skipDiceClick: " & Settings.SkipDiceWithClick)
            writer.WriteLine("informQuestion: " & Settings.InformQuestionOutcome)
            writer.WriteLine("questionTime: " & Settings.QuestionTime)
            writer.WriteLine("connectionString: " & Settings.ConnectionString)
        End Using
    End Sub

    Public Sub getSettings()
        Using reader As New StreamReader("settings.txt")
            reader.ReadLine()
            Settings.FullScreenEnabled = reader.ReadLine.Substring("fullscreen: ".Length)
            Settings.SkipPlayerChoiceAnimation = reader.ReadLine.Substring("skipStartingSequence: ".Length)
            Settings.DiceTickHighlightEffect = reader.ReadLine.Substring("diceTickHighlight: ".Length)
            Settings.SkipDiceWithClick = reader.ReadLine.Substring("skipDiceClick: ".Length)
            Settings.InformQuestionOutcome = reader.ReadLine.Substring("informQuestion: ".Length)
            Settings.QuestionTime = reader.ReadLine.Substring("questionTime: ".Length)
            Settings.ConnectionString = reader.ReadLine.Substring("connectionString: ".Length)
        End Using
    End Sub

End Class
