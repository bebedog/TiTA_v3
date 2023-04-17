Public Class adminTools
    Private Sub adminTools_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        positionScreen()
        Me.TopMost = True
    End Sub

    Public Sub positionScreen()
        Me.Visible = True
        Dim x As Integer
        Dim y As Integer
        x = Screen.PrimaryScreen.WorkingArea.Width
        y = Screen.PrimaryScreen.WorkingArea.Height - Me.Height
        Do Until x = Screen.PrimaryScreen.WorkingArea.Width - Me.Width
            x = x - 1
            Me.Location = New Point(x, y)
        Loop
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Display.Show()
        Me.Close()
    End Sub

    Private Sub btnAddTask_Click(sender As Object, e As EventArgs) Handles btnAddTask.Click
        addTask.Show()
        Me.Hide()
    End Sub
End Class