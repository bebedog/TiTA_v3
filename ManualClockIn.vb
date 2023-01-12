Imports Newtonsoft.Json


Public Class ManualClockIn

    Dim projectsList

    Public Class PersonsAndTeam
        Public Property id As Integer
        Public Property kind As String
    End Class

    Public Class Person
        Public Property personsAndTeams As PersonsAndTeam()
    End Class

    Public Class Example
        Public Property job As String
        Public Property person As Person
        Public Property text As String 'Time-in 24hr format (HH:mm:ss)
        Public Property text_1 As String 'ClockSharkStart
        Public Property text64 As String 'TiTA version
    End Class

    Public Class Item
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Group
        Public Property items As Item()
    End Class

    Public Class Board
        Public Property groups As Group()
    End Class

    Public Class Data
        Public Property boards As Board()
    End Class

    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Public Function populateJobsList(ByVal projects As Root)
        For Each x In projects.data.boards(0).groups(0).items
            cbProjectsList.Items.Add(x.name)
        Next
    End Function

    Public Async Function buildQuery(ByVal personID As String, ByVal currentJob As String, ByVal logInTime As String) As Task
        Dim mutatePOST = New Example()
        mutatePOST.job = currentJob
        mutatePOST.text = logInTime
        mutatePOST.text_1 = "Start_TEST"
        mutatePOST.text64 = "3.0"
        Dim person As New Person()
        Dim personIDandKind As New PersonsAndTeam()
        Dim personValueList As New List(Of PersonsAndTeam)
        personIDandKind.id = Form1.mondayID
        personIDandKind.kind = "person"
        personValueList.Add(personIDandKind)
        person.personsAndTeams = personValueList.ToArray
        mutatePOST.person = person
        Dim newJSON = JsonConvert.SerializeObject(mutatePOST).ToString
        Dim formattedJSON = newJSON.Replace("""", "\""")
        Dim changeColumnQuery As String
        changeColumnQuery =
            "mutation {change_multiple_column_values(item_id:" + personID + ", board_id:2628729848, column_values: """ + formattedJSON + """) {id}}"
        Try
            Dim result As String = Await Form1.SendMondayRequest(changeColumnQuery)
            Console.WriteLine(changeColumnQuery)
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Function
        End Try
    End Function

    Private Async Sub ManualClockIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.Text = $"{Form1.fFirstName} {Form1.fSurname} | {Form1.mondayID} | {Form1.department}"
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "HH:mm:ss"
        Dim projectsListQuery As String =
            "query{
                  boards(ids:[2718204773]){
                    groups(ids:""topics""){
                      items {
                        id
                        name
                      }
                    }
                  }
                }"

        Try
            Dim result As String = Await Form1.SendMondayRequest(projectsListQuery)
            projectsList = JsonConvert.DeserializeObject(Of Root)(result)
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Sub
        End Try

        populateJobsList(projectsList)

        'Dim updateColumns As String =
        '    "query{
        '      items_by_column_values(board_id: 2628729848, column_id: ""text_1"", column_value: ""START_" + Form1.fSurname + """) {
        '        id
        '        name
        '      }
        '    }"



        'Try

        '    Dim result As String = Await Form1.SendMondayRequest(updateColumns)
        '    previousLog = JsonConvert.DeserializeObject(Of Root)(result)
        '    Dim count As Integer = previousLog.data.items_by_column_values.Length
        '    Dim itemID As String = previousLog.data.items_by.column_values.id

        '    If count = 0 Then
        '        Label1.Text = "No previous log found. Clock In Manually?"
        '        Dim msgResult = MessageBox.Show("No previous log found. Clock In Manually?", "No Record Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        '        If msgResult = DialogResult.Yes Then

        '            MessageBox.Show("Ok", "Nice", MessageBoxButtons.OK)
        '            Me.Hide()
        '            ManualClockIn.Show()

        '        End If
        '    ElseIf count > 1 Then
        '        Label1.Text = "Duplicate Entries found. Would you like to update manually?"
        '    Else
        '        Label1.Text = "Success"
        '    End If


        'Catch ex As Exception

        '    Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        '    If result = DialogResult.Retry Then
        '        Application.Restart()
        '    Else
        '        Me.Close()
        '    End If
        '    Exit Sub

        'End Try


    End Sub

    Private Sub btnTimeIn_Click(sender As Object, e As EventArgs) Handles btnTimeIn.Click

        Dim personId = Dashboard1.newItemID
        Dim currentJob = cbProjectsList.SelectedItem
        Dim logInTime = DateTimePicker1.Value.ToString("HH:mm:ss")
        buildQuery(personId, currentJob, logInTime)

    End Sub
End Class