Imports System.IO
Public Class GameLogger

    Public Sub saveGame(save As GameSave)
        Using writer As New StreamWriter("save" & save.Name & ".txt")
            writer.WriteLine("*** " & save.Name & " ***")
            writer.WriteLine()
            writer.WriteLine("PlayerCount: " & save.Players.Count)
            For i As Byte = 0 To save.Players.Count - 1
                writer.WriteLine("*** Player " & i + 1 & " ***")
                writer.WriteLine(save.Players(i).Name)
                writer.WriteLine(save.Players(i).Color.ToString)
                writer.WriteLine(save.Players(i).PlayerInfo.Name)
                writer.WriteLine(save.Players(i).Pion.Position.Name)
                writer.WriteLine(save.Players(i).HasArtsAndLiterature)
                writer.WriteLine(save.Players(i).HasEntertainment)
                writer.WriteLine(save.Players(i).HasGeography)
                writer.WriteLine(save.Players(i).HasHistory)
                writer.WriteLine(save.Players(i).HasScience)
                writer.WriteLine(save.Players(i).HasSports)
                writer.WriteLine()
            Next
        End Using
    End Sub

    Public Function loadGame(saveName As String) As GameSave
        Try
            Dim save As New GameSave
            Dim player As Player
            Using reader As New StreamReader("save" & saveName & ".txt")
                reader.ReadLine()
                reader.ReadLine
                Dim numberOfPlayers As Byte = reader.ReadLine.Substring("PlayerCount: ".Length)
                save.LastPlayer = numberOfPlayers - 1
                For i As Byte = 0 To numberOfPlayers - 1
                    reader.ReadLine()
                    player = New Player(reader.ReadLine, New BrushConverter().ConvertFromString(reader.ReadLine), MainModule.mainWindow.FindName(reader.ReadLine))
                    player.Pion.Position = MainModule.mainWindow.FindName(reader.ReadLine)
                    player.HasArtsAndLiterature = reader.ReadLine
                    player.HasEntertainment = reader.ReadLine
                    player.HasGeography = reader.ReadLine
                    player.HasHistory = reader.ReadLine
                    player.HasScience = reader.ReadLine
                    player.HasSports = reader.ReadLine
                    reader.ReadLine()
                    save.addPlayer(player)
                Next
            End Using
            Return save
        Catch
            Throw New Exception("Savefile not found")
        End Try
    End Function

    Public Sub deleteSave(saveName As String)
        Try
            File.Delete(My.Application.Info.DirectoryPath & "\save" & saveName & ".txt")
        Catch
            Throw New Exception("File not found")
        End Try
    End Sub

    Public Function getSaveNames() As List(Of String)
        Dim saveNames As New List(Of String)
        Dim directory As New DirectoryInfo(My.Application.Info.DirectoryPath)
        For Each File As FileInfo In directory.GetFiles("save*.txt")
            saveNames.Add(File.Name)
        Next
        Return saveNames
    End Function


End Class
