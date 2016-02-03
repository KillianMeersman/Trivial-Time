Imports System.Threading
Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Const minimumTime As Integer = 2000
    Private Const fadeTime As Integer = 500
    Private splashScreen As SplashScreen

    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        splashScreen = New SplashScreen("Resources/LoadingDice.png")
        splashScreen.Show(False, True)

        Dim timer As New Stopwatch()
        timer.Start()

        MyBase.OnStartup(e)
        timer.Stop()

        Dim remainingTime As Integer = minimumTime - CInt(timer.ElapsedMilliseconds)
        If remainingTime > 0 Then
            Thread.Sleep(remainingTime)
        End If

        splashScreen.Close(TimeSpan.FromMilliseconds(fadeTime))
    End Sub

End Class
