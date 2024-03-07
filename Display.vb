Imports Newtonsoft.Json
Imports System.Threading

Public Class Display

    'START of Class Object Declaration
    Public Class Labels
        Public Property labels As String()
    End Class
    Public Class ColumnValuesToChange
        Public Property text_1 As String ' START_Surname
        Public Property text As String 'Time-in
        Public Property dup__of_time_in As String 'Time-out
        Public Property text64 As String 'TiTA Version
        Public Property job As String 'dropdown | Jobs/Task Column
        Public Property dropdown As Labels 'dropdown | Project Code
        Public Property text4 As String 'dropdown | Subtask
        Public Property person As Person
    End Class
    Public Class Person
        Public Property personsAndTeams As PersonsAndTeams()
    End Class
    Public Class PersonsAndTeams
        Public Property id As Integer
        Public Property kind As String
    End Class

    Public Class ItemsByColumnValue
        Public Property id As String
        Public Property name As String
        Public Property column_values As ColumnValue()
    End Class
    Public Class ColumnValue

        Public Property text As String
        Public Property title As String
        Public Property value As String
    End Class
    Public Class Item
        Public Property name As String
        Public Property column_values As ColumnValue()
    End Class
    Public Class Group
        Public Property items As Item()
    End Class
    Public Class Board
        Public Property groups As Group()
    End Class
    Public Class Data
        Public Property items_by_column_values As ItemsByColumnValue()
    End Class
    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class
    'END of Class Object Declaration

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
        Me.TopMost = True
        elpasedDutyTimer.Enabled = True
        elpasedDutyTimer.Start()
        'Check if user has admin privilege(1) or not(0).
        If (Form1.privilege = 1) Then
            btnAdminToolsAndLogout.Enabled = True
            btnAdminToolsAndLogout.Visible = True
            btnAdminToolsAndLogout.Text = "Admin Tools"
            Me.Size = New Point(600, 230)

        ElseIf Form1.privilege = 2 Then

            btnAdminToolsAndLogout.Enabled = True
            btnAdminToolsAndLogout.Visible = True
            btnAdminToolsAndLogout.Text = "Log Out"
            Me.Size = New Point(600, 230)
        Else

            ToolStrip1.Visible = False
            Me.Size = New Point(400, 212)

        End If
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
        reminder(Form1.currentTask)
    End Sub
    Private Sub showToolTip()
        Me.Show()
        Timer1.Enabled = False
        Timer1.Stop()
        NotifyIcon1.Visible = False
    End Sub
    Private Sub reminder(task)
        If task = "Lunch" Then
            NotifyIcon1.BalloonTipText = $"You've been on lunch for an hour."
        ElseIf task = "Break" Then
            NotifyIcon1.BalloonTipText = $"You've been on break for an hour."
        Else
            NotifyIcon1.BalloonTipText = $"It has been an hour.{Environment.NewLine}Are you still working on {task}?"
        End If
        NotifyIcon1.ShowBalloonTip(0)
    End Sub
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        showToolTip()
    End Sub
    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon1.BalloonTipClicked
        showToolTip()
    End Sub

    Private Async Sub lunchAndBreakReminder_Tick(sender As Object, e As EventArgs) Handles lunchAndBreakReminder.Tick
        If TimeOfDay.ToString("HH:mm") = "12:00" Then
            lunchAndBreakReminder.Enabled = False
            lunchAndBreakReminder.Stop()
            Dim dlgrslt = MessageBox.Show($"Your tita says it's time for lunch!{Environment.NewLine}Would you like to switch to lunch right now?", "Lunch Reminder", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dlgrslt = DialogResult.Yes Then
                'switch to lunch
                Dashboard1.Show()
                Me.Close()
            Else
                'no
                Await Task.Delay(60000)
                lunchAndBreakReminder.Enabled = True
                lunchAndBreakReminder.Start()
            End If
        ElseIf TimeOfDay.ToString("HH:mm") = "16:00" Then
            lunchAndBreakReminder.Enabled = False
            lunchAndBreakReminder.Stop()
            Dim dlgrslt = MessageBox.Show($"Your tita says it's time for snacks!{Environment.NewLine}Would you like to switch to lunch right now?", "Lunch Reminder", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dlgrslt = DialogResult.Yes Then
                'switch to break
                Dashboard1.Show()
                Me.Close()
            Else
                'no
                Await Task.Delay(60000)
                lunchAndBreakReminder.Enabled = True
                lunchAndBreakReminder.Start()
            End If
        End If
        Dim time = Form1.elapsedDutyHours.Elapsed
        Label3.Text = String.Format("You have been working for: {0} Hours, {1} Minutes and {2} Seconds", time.Hours, time.Minutes, time.Seconds)
    End Sub

    Private Sub elpasedDutyTimer_Tick(sender As Object, e As EventArgs) Handles elpasedDutyTimer.Tick
        If Form1.elapsedDutyHours.Elapsed.TotalSeconds > 32400 Then
            elpasedDutyTimer.Enabled = False
            elpasedDutyTimer.Stop()
            Dim dlgrslt = MessageBox.Show("Your TiTA noticed that you've been working for 9 hours straight! Consider getting some rest!", "Friendly reminder from TiTA", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Async Sub btnAdminTools_Click(sender As Object, e As EventArgs) Handles btnAdminToolsAndLogout.Click
        If btnAdminToolsAndLogout.Text = "Admin Tools" Then

            Me.Hide()
            adminTools.Show()

        ElseIf btnAdminToolsAndLogout.Text = "Log Out" Then

            Dim confirmLogout As DialogResult = MessageBox.Show("Are you sure you want to log out for today?", "Confirm Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirmLogout = DialogResult.Yes Then
                disableAllControls2(Me)
                statusLabel.Text = "Marking X your last log.."
                'Build Object to Change Column Value
                Dim payload = New ColumnValuesToChange()
                'Assign values to each property of the object.
                payload.text_1 = "X"
                payload.dup__of_time_in = DateTime.Now.ToString("HH:mm:ss")
                payload.text64 = Form1.titaVersion.ToString

                'Mark previous log as done

                Dim logout = Await MarkAsDonePreviousLog(payload)

                If logout = "Success" Then
                    Dim logoutSuccess As DialogResult = MessageBox.Show($"Log out successful. See you tomorrow {Form1.fFirstName}", ":)", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If logoutSuccess = DialogResult.OK Then
                        Application.Exit()
                    End If

                End If

            End If

        End If

    End Sub



    Private Async Function MarkAsDonePreviousLog(ByVal columnValues As ColumnValuesToChange) As Task(Of String)
        Dim serializerSettings = New JsonSerializerSettings
        serializerSettings.NullValueHandling = NullValueHandling.Ignore
        Dim jsonToLoad As String = JsonConvert.SerializeObject(columnValues, serializerSettings)
        Dim formattedJSON = jsonToLoad.Replace("""", "\""")
        Dim MarkAsDonePreviousLogQuery As String =
            "mutation{
                change_multiple_column_values(item_id: " + Form1.currentID + ", board_id:2628729848, column_values:""" + formattedJSON + """, create_labels_if_missing: true){
                    id
                }
            }"

        Dim recon
MarkAsDonePreviousLog:
        Try
            For retries = 1 To Form1.maxErrorCount
                If retries <> Form1.maxErrorCount Then
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(MarkAsDonePreviousLogQuery)
                    If response(0) = "error" Then
                        statusLabel.Text = $"Error occured while marking previous log. Retrying({retries}/{Form1.maxErrorCount})"
                        Return "Error"
                    Else
                        statusLabel.Text = "Previous log successfully marked."
                        Return "Success"
                        Exit For
                    End If
                Else
                    Throw New Exception("Error occured while marking previous log.")
                End If
            Next
        Catch ex As Exception
            'Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            'If result = DialogResult.Retry Then
            '    Dashboard1.Show()
            '    Me.Close()
            'Else
            '    Form1.Close()
            'End If
            'Exit Function
            If recon >= 0 And recon < Form1.maxErrorCount Then
                recon += 1
                statusLabel.Text = $"Attempting to reconnect to Monday {recon}/{Form1.maxErrorCount}"
                Thread.Sleep(1000)
                GoTo MarkAsDonePreviousLog
            ElseIf recon >= Form1.maxErrorCount Then
                MessageBox.Show("Failed to connect to Monday. Press OK to restart TiTA", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If DialogResult.OK Then
                    Application.Restart()
                End If
            End If
            Exit Function
        End Try
        'START OLD CODE HERE
        'Try
        '    Dim resultString As String = Await Form1.SendMondayRequest(MarkAsDonePreviousLogQuery)
        '    Console.WriteLine(resultString)
        'Catch ex As Exception
        '    Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        '    If result = DialogResult.Retry Then
        '        Application.Restart()
        '    Else
        '        Me.Close()
        '    End If
        '    Exit Function
        'End Try
        'END OLD CODE HERE
    End Function


End Class