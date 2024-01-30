Imports Newtonsoft.Json

Public Class AddManualLogs
    Public Class CreateItem
        Public Property id As String
    End Class

    Public Class Data
        Public Property create_item As CreateItem
    End Class

    Public Class Rootirino
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Public Class PersonsAndTeam
        Public Property id As String
        Public Property kind As String
    End Class

    Public Class People
        Public Property personsAndTeams As PersonsAndTeam()
    End Class

    Public Class Payload
        Public Property person As People
        Public Property [date] As String
        Public Property job As String
        Public Property dropdown As String
        Public Property text4 As String
        Public Property text_1 As String
        Public Property text As String
        Public Property dup__of_time_in As String
        Public Property text64 As String

    End Class


    Public tasksAndSubtasks As New List(Of Object)
    Public myCustomAutoComplete As New List(Of String)
    Public myAccounts As New List(Of Object)

    Public myPayload As New List(Of Object)
    Private Sub AddManualLogs_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        adminTools.Show()
    End Sub
    Public Sub populateUsers()
        Dim accounts = Form1.accounts

        For Each x In accounts.data.boards(0).items
            cbName.Items.Add(x.name)
        Next
    End Sub
    Private Sub AddManualLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()


        'I plan to structure the data as the following:
        '{
        'taskName, [subtask]
        '}
        populateUsers()

        populateTasks()



    End Sub

    Public Sub populateTasks()
        Dim allTasks = Form1.allTasks
        For Each group In allTasks.data.boards(0).groups
            For Each item In group.items
                Dim itemName = item.name
                Dim mySubitems As New List(Of String)
                Dim projectNumber As String

                If item.column_values(0).text IsNot Nothing Then
                    projectNumber = item.column_values(0).text
                Else
                    projectNumber = "N/A"
                End If

                If item.subitems IsNot Nothing Then
                    For Each subitems In item.subitems
                        mySubitems.Add(subitems.name.ToString)
                    Next
                Else
                    'do nothing
                End If
                tasksAndSubtasks.Add({itemName, mySubitems, projectNumber})
            Next
        Next
        For Each tasks In tasksAndSubtasks
            cbTask.Items.Add(tasks(0).ToString)
        Next
        cbSubtask.Enabled = False
    End Sub

    Private Sub cbTask_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTask.SelectedIndexChanged
        updateSubtask()
    End Sub

    Public Sub updateSubtask()
        cbSubtask.Items.Clear()
        For Each tasks In tasksAndSubtasks
            Dim mytask = tasks(0)
            'cbSubtask.Enabled = True
            If tasks(0) = cbTask.Text Then
                If tasks(1).Count = 0 Then
                    cbSubtask.Items.Clear()
                    cbSubtask.Text = ""
                    cbSubtask.Enabled = False
                    Exit For
                Else
                    cbSubtask.Items.AddRange(tasks(1).toArray)
                    cbSubtask.Enabled = True
                    cbSubtask.SelectedIndex = 0
                    Exit For
                End If
            Else
                'do nothing
                cbSubtask.Enabled = False
            End If
        Next
    End Sub

    Private Sub cbTask_TextUpdate(sender As Object, e As EventArgs) Handles cbTask.TextUpdate

        'cbTask.DroppedDown = False
        cbTask.Items.Clear()
        cbSubtask.Items.Clear()
        myCustomAutoComplete.Clear()
        For Each item In tasksAndSubtasks
            If item(0).ToLower.Contains(cbTask.Text.ToLower) Then
                myCustomAutoComplete.Add(item(0))
            End If
        Next
        cbTask.Items.AddRange(myCustomAutoComplete.ToArray)
        cbTask.SelectionStart = cbTask.Text.Length
        cbTask.DroppedDown = True
        cbTask.Cursor = Cursors.Default
        cbSubtask.Text = ""

        updateSubtask()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'find mondayID of selected user
        Dim selectedUser = cbName.SelectedItem
        Dim selectedMondayID As String
        For Each names In Form1.accounts.data.boards(0).items
            If selectedUser = names.name Then
                selectedMondayID = names.column_values(2).text
            End If
        Next

        'specify that the command came from manual add log
        Dim origin As String = "Manual Add Log v" + My.Application.Info.Version.ToString

        'find the project number of the task.
        Dim selectedTask = cbTask.SelectedItem
        Dim selectedProjectNumber As String
        For Each entry In tasksAndSubtasks
            '0 - task
            '1 - array of subtask
            '2 - project number

            If selectedTask = entry(0) Then
                selectedProjectNumber = entry(2)
            End If
        Next

        'append the current data to the list to be uploaded to monday.com
        Dim myNewData As Object =
            {cbName.Text, cbTask.SelectedItem, cbSubtask.SelectedItem, dtpDate.Value.ToString("yyyy-MM-dd"), dtpIn.Value.ToString("HH:mm"), dtpOut.Value.ToString("HH:mm"), selectedMondayID, selectedProjectNumber, origin}

        myPayload.Add(myNewData)
        MessageBox.Show("Log added to queue.")
        ClearInputs()
    End Sub

    Public Sub ClearInputs()
        cbTask.Items.Clear()
        cbSubtask.Items.Clear()
        dtpDate.Value = DateTime.Now
        dtpIn.Value = DateTime.Now
        dtpOut.Value = DateTime.Now

        populateTasks()

    End Sub

    Private Async Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        DisableAllControls()
        For Each items In myPayload
            'create item first
            '0 - name
            '1 - task
            '2 - subtask
            '3 - date
            '4 - in
            '5 - out
            '6 - MondayID
            '7 - Project Number
            '8 - Origin

            'person - Names
            'job - Tasks
            'dropdown - projectNumber
            'text4 - Subtasks
            'text_1 - X
            'text - Time-in
            'dup__of_time_in - Time-out
            'date - Creation Date
            'text64 - Origin of mutation

            Dim createItemQuery = "mutation{
                                        create_item(board_id: 2628729848, group_id:""topics"", item_name: """ + items(0) + """){
                                            id
                                        }
                                    }"
            Dim newItemID As String
            Dim newItem As Object = Await Form1.SendMondayRequestVersion2(createItemQuery)
            If newItem(0) = "error" Then
                MessageBox.Show("Error when uploading")
            Else
                newItemID = JsonConvert.DeserializeObject(Of Rootirino)(newItem(1)).data.create_item.id
                Dim temp As New PersonsAndTeam
                Dim myPerson As New List(Of PersonsAndTeam)
                Dim myPeopleValue As New People
                Dim myValues As New Payload

                temp.id = items(6)
                temp.kind = "person"
                myPerson.Add(temp)
                myPeopleValue.personsAndTeams = myPerson.ToArray

                myValues.person = myPeopleValue
                myValues.job = items(1)
                myValues.dropdown = items(7)
                myValues.text4 = items(2)
                myValues.text_1 = "X"
                myValues.text = items(4) + ":00"
                myValues.dup__of_time_in = items(5) + ":00"
                myValues.[date] = items(3)
                myValues.text64 = items(8)

                Dim myJSONSerializerSettings As New JsonSerializerSettings
                myJSONSerializerSettings.NullValueHandling = NullValueHandling.Ignore
                Dim mySerializedPayload = JsonConvert.SerializeObject(myValues, Formatting.None, myJSONSerializerSettings)
                Dim formattedJSON = mySerializedPayload.Replace("""", "\""")
                Dim changeColumnValueQuery = "mutation{
    change_multiple_column_values(item_id: " + newItemID + ", board_id: 2628729848, column_values: "" " + formattedJSON + " ""){id}
}"

                Try
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(changeColumnValueQuery)
                    If response(0) = "error" Then
                        MessageBox.Show(response(1))
                    Else
                        Console.WriteLine("Done!" + Environment.NewLine + response(1))
                    End If
                Catch ex As Exception
                    EnableAllControls()
                    MessageBox.Show(ex.Message, "Exception thrown!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Next
        MessageBox.Show("Success!")
        EnableAllControls()
        myPayload.Clear()
    End Sub

    Public Sub DisableAllControls()
        Panel1.Enabled = False
    End Sub

    Public Sub EnableAllControls()
        Panel1.Enabled = True
    End Sub

End Class