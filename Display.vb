Public Class Display
    Public Sub positionLoginScreen()
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
    Private Async Sub Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lunchAndBreakReminder.Enabled = True
        lunchAndBreakReminder.Start()
        positionLoginScreen()
        Me.TopMost = True
        Dim elapsedTimeInSeconds As Integer
        If Form1.watch.IsRunning <> True Then
            elapsedTimeInSeconds = 0
        ElseIf elapsedTimeInSeconds >= 60 Then
            Form1.watch.Restart()
        Else
            elapsedTimeInSeconds = 60 - Int(Form1.watch.Elapsed.TotalSeconds)
        End If
        Await Task.Delay(elapsedTimeInSeconds)
        lblTask.Text = Form1.currentTask
        lblSubtasks.Text = Form1.currentSubTask
        lblTimeIn.Text = Form1.currentTimeIn
        lblDepartment1.Text = Form1.department
        lblProjectNumber.Text = Form1.currentProjectNumber
        lblUserWelcome.Text = $"Welcome, {Form1.fFirstName} {Form1.fSurname}"
    End Sub

    Private Sub disableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = False
        Next
    End Sub

    Private Sub enableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = True
        Next
    End Sub
    Private Sub btnSwitch_Click(sender As Object, e As EventArgs) Handles btnSwitch.Click
        'Form1.loadDelay = 60000 - Form1.elapsedTime
        Dim result = MessageBox.Show("Are you sure you want to switch to other tasks?", "Switch Tasks", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dashboard1.Show()
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        NotifyIcon1.Text = $"{Form1.fFirstName} | {Form1.currentTask}"
        NotifyIcon1.BalloonTipText = "TiTA is now working in the background and will remind you in an hour about your current task."
        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.ShowBalloonTip(30000)
        NotifyIcon1.Visible = True
        Timer1.Enabled = True
        Timer1.Interval = 3600000
        Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        reminder()
    End Sub
    Private Sub showToolTip()
        Me.Show()
        Timer1.Enabled = False
        Timer1.Stop()
        NotifyIcon1.Visible = False
    End Sub
    Private Sub reminder()
        NotifyIcon1.ShowBalloonTip(0)
        NotifyIcon1.BalloonTipText = $"It has been an hour.{Environment.NewLine}Are you still working on {Form1.currentTask}?"
    End Sub
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        showToolTip()
    End Sub
    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon1.BalloonTipClicked
        showToolTip()
    End Sub

    Private Async Sub lunchAndBreakReminder_Tick(sender As Object, e As EventArgs) Handles lunchAndBreakReminder.Tick
        If TimeOfDay.ToString("HH:mm") = "12:00" Then
            Dim dlgrslt = MessageBox.Show($"Your tita says it's time for lunch!{Environment.NewLine}Would you like to switch to lunch right now?", "Lunch Reminder", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dlgrslt = DialogResult.Yes Then
                'switch to lunch
                Dashboard1.Show()
                Me.Close()
            Else
                'no
                lunchAndBreakReminder.Enabled = False
                lunchAndBreakReminder.Stop()
                Await Task.Delay(60000)
                lunchAndBreakReminder.Enabled = True
                lunchAndBreakReminder.Start()
            End If
        ElseIf TimeOfDay.ToString("HH:mm") = "16:00" Then
            Dim dlgrslt = MessageBox.Show($"Your tita says it's time for snacks!{Environment.NewLine}Would you like to switch to lunch right now?", "Lunch Reminder", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dlgrslt = DialogResult.Yes Then
                'switch to break
                Dashboard1.Show()
                Me.Close()
            Else
                'no
                lunchAndBreakReminder.Enabled = False
                lunchAndBreakReminder.Stop()
                Await Task.Delay(60000)
                lunchAndBreakReminder.Enabled = True
                lunchAndBreakReminder.Start()
            End If
        End If
    End Sub
End Class