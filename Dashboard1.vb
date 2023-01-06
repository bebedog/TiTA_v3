Public Class Dashboard1
    Private Sub Dashboard1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.Text = $"Welcome, {Form1.firstName} {Form1.fsurname}! | {Form1.mondayID} | {Form1.department}"
    End Sub
End Class