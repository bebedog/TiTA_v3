Imports System.Threading
Imports Newtonsoft.Json

Public Class Dashboard1

    Dim previousLog As Root
    Public Class ItemsByColumnValue
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Data
        Public Property items_by_column_values As ItemsByColumnValue()
    End Class

    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Private Async Function beLazy() As Task(Of Integer)
        Thread.Sleep(45000)
        Return 1
    End Function

    Private Async Sub Dashboard1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.Text = $"{Form1.fFirstName} {Form1.fSurname} | {Form1.mondayID} | {Form1.department}"
        Await beLazy()
        Dim fetchStatus As String =
            "query{
              items_by_column_values(board_id: 2628729848, column_id: ""text_1"", column_value: ""START_" + Form1.fSurname + """) {
                id
                name
              }
            }"


        Try

            Dim result As String = Await Form1.SendMondayRequest(fetchStatus)
            previousLog = JsonConvert.DeserializeObject(Of Root)(result)
            Dim count As Integer = previousLog.data.items_by_column_values.Length

            If count = 0 Then
                Label1.Text = "No previous log found. Clock In Manually?"
                Dim msgResult = MessageBox.Show("No previous log found. Clock In Manually?", "No Record Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                'If msgResult = DialogResult.Yes Then

                'Else

                'End If
            ElseIf count > 1 Then
                Label1.Text = "Duplicate Entries found. Would you like to update manually?"
            Else
                Label1.Text = "Success"
            End If


        Catch ex As Exception

            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Sub

        End Try


    End Sub

End Class