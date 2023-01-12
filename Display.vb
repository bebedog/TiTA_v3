Public Class Display
    Private Sub Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblTask.Text = Form1.currentTask
        lblSubtasks.Text = Form1.currentSubTask
        lblTimeIn.Text = Form1.currentTimeIn
        lblDepartment1.Text = Form1.department
        lblProjectNumber.Text = Form1.currentProjectNumber
        lblUserWelcome.Text = $"Welcome, {Form1.fFirstName} {Form1.fSurname}"
    End Sub

    Private Sub btnSwitch_Click(sender As Object, e As EventArgs) Handles btnSwitch.Click
        Dim result = MessageBox.Show("Are you sure you want to switch to other tasks?", "Switch Tasks", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Dashboard1.Show()
        Me.Close()
    End Sub
End Class