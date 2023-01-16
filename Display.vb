Public Class Display
    Private Sub Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Form1.loadDelay = 60000 - Form1.elapsedTime
        Dim result = MessageBox.Show("Are you sure you want to switch to other tasks?", "Switch Tasks", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result.Yes Then
            disableAllControls()
        End If
        Dashboard1.Show()
        Me.Close()
    End Sub
End Class