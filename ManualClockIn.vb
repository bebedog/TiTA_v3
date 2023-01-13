Imports RestSharp
Imports Newtonsoft.Json


Public Class ManualClockIn

    Dim newLog
    Dim projectsList
    Public newItemID
    Public newItemObj
    Dim projectCode

    'Declare classes based on JSON input/output. Generated via JSONUtils.com

    Public Class ItemsByColumnValue
        Public Property id As String
        Public Property name As String
        Public Property column_values As ColumnValue()
    End Class

    Public Class Labels
        Public Property labels As String()
    End Class
    Public Class Subitem
        Public Property name As String
    End Class
    Public Class PersonsAndTeam
        Public Property id As Integer
        Public Property kind As String
    End Class

    Public Class Person
        Public Property personsAndTeams As PersonsAndTeam()
    End Class

    'Classes for column update, using Column IDs from Monday as variable names
    Public Class Example
        Public Property job As String
        Public Property person As Person
        Public Property text As String 'Time-in 24hr format (HH:mm:ss)
        Public Property text_1 As String 'ClockSharkStart
        Public Property text64 As String 'TiTA version
        Public Property text4 As String 'Subtask
        Public Property dropdown As Labels  'Project Code
    End Class

    Public Class ColumnValue
        Public Property text As String
    End Class
    Public Class Item
        Public Property id As String
        Public Property name As String
        Public Property subitems As Subitem()
        Public Property column_values As ColumnValue()
    End Class

    Public Class Group
        Public Property items As Item()
    End Class

    Public Class CreateItem
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Board
        Public Property groups As Group()
    End Class

    Public Class Data
        Public Property boards As Board()
        Public Property create_item As CreateItem
        Public Property items_by_column_values As ItemsByColumnValue()

    End Class


    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Private Sub populateJobsList()
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each tasks In groups.items
                cbProjectsList.Items.Add(tasks.name)
            Next
        Next
        cbProjectsList.SelectedIndex = 0
    End Sub

    Private Sub populateSubtasks()
        cbSubtasks.Items.Clear()
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each items In groups.items
                If items.name = cbProjectsList.SelectedItem Then
                    projectCode = items.column_values(0).text.ToString
                    For Each subtasks In items.subitems
                        cbSubtasks.Items.Add(subtasks.name)
                    Next
                End If
            Next
        Next
    End Sub

    'Add new item to TiTO timeline
    Public Async Function createNewItem() As Task
        Dim createItemQuery As String
        createItemQuery =
        "mutation{
          create_item(board_id: 2628729848 group_id: ""topics"" item_name:""" + Form1.fSurname + """){ 
            id
            name
          }
        }"
        Dim createItemResult As String = Await Form1.SendMondayRequest(createItemQuery)
        newItemObj = JsonConvert.DeserializeObject(Of Root)(createItemResult)
        Await getItemID(newItemObj)
    End Function

    'Retrieve Item ID from created Manual Clock In Record (currently blank)
    Public Async Function getItemID(createdItem As Root) As Task
        newItemID = createdItem.data.create_item.id
        ToolLabel1.Text = newItemID
        Dim currentJob = cbProjectsList.SelectedItem
        Dim logInTime = DateTimePicker1.Value.ToString("HH:mm:ss")
        Dim subtask = cbSubtasks.SelectedItem.ToString
        Await buildQuery(newItemID, currentJob, logInTime, subtask, projectCode)

    End Function

    'Create query for updating column values to manual log in item
    Public Async Function buildQuery(ByVal personID As String, ByVal currentJob As String, ByVal logInTime As String, ByVal subtask As String, ByVal projectcode As String) As Task
        Dim mutatePOST = New Example()
        mutatePOST.job = currentJob
        mutatePOST.text = logInTime
        mutatePOST.text_1 = "START_" + Form1.fSurname
        mutatePOST.text64 = "3.0"
        mutatePOST.text4 = subtask

        'Set entry to Project Code column: JSON Format
        Dim currentProjectCode As New Labels()
        Dim projectCodeLabels As New List(Of String)
        projectCodeLabels.Add(projectcode)
        currentProjectCode.labels = projectCodeLabels.ToArray
        mutatePOST.dropdown = currentProjectCode


        'Set entry for People column: modified JSON Format
        Dim Person As New Person()
        Dim personIDandKind As New PersonsAndTeam()
        Dim personValueList As New List(Of PersonsAndTeam)
        personIDandKind.id = Form1.mondayID
        personIDandKind.kind = "person"
        personValueList.Add(personIDandKind)
        Person.personsAndTeams = personValueList.ToArray
        mutatePOST.person = Person
        Dim newJSON = JsonConvert.SerializeObject(mutatePOST).ToString

        'Replace (") with (\") to be compatible with Monday API
        Dim formattedJSON = newJSON.Replace("""", "\""")
        Dim changeColumnQuery As String
        changeColumnQuery =
            "mutation {change_multiple_column_values(item_id:" + personID + ", board_id:2628729848, column_values: """ + formattedJSON + """) {id}}"
        Try
            Dim result As String = Await Form1.SendMondayRequest(changeColumnQuery)
            Console.WriteLine(changeColumnQuery)
            'checkAddedItem()
            Dim msgSuccess = MessageBox.Show("Nice", "Success", MessageBoxButtons.OK)
            If msgSuccess = DialogResult.OK Then
                Me.Close()
                Dashboard1.Show()
                'Change delay time for Dashboard1 from 0 to 1 minute.
                Dashboard1.elapsedTime = 60000
            End If

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

    'Private Async Sub checkAddedItem()
    '    Try
    '        Dim result As String = Await Form1.SendMondayRequest(Dashboard1.fetchStatus)
    '        newLog = JsonConvert.DeserializeObject(Of Root)(result)
    '        Dim count As Integer = newLog.data.items_by_column_values.Length
    '        If count = 0 Then
    '            Label1.Text = "No new log found. Retry Manual Clock In?"
    '            Dim msgResult = MessageBox.Show("No new log found. Retry Manual Clock In?", "No Record Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    '            If msgResult = DialogResult.Yes Then
    '                Me.Refresh()
    '            End If
    '        ElseIf count > 1 Then
    '            Me.Label1.Text = "Duplicate Entries Found. Please select your most recent record."
    '            Me.Hide()
    '            Dashboard1.Show()
    '            Dashboard1.DataGridView1.Visible = True
    '            Dashboard1.DataGridView1.Enabled = True
    '        Else
    '            Me.Label1.Text = "Success"
    '            DisplayAndSwitch.Show()
    '            Me.Close()
    '        End If
    '    Catch ex As Exception
    '        Console.WriteLine("Error!")
    '        Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
    '        If result = DialogResult.Retry Then
    '            Application.Restart()
    '        Else
    '            Me.Close()
    '        End If
    '        Exit Sub
    '    Finally
    '    End Try
    'End Sub

    Private Async Sub ManualClockIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.Text = $"{Form1.fFirstName} {Form1.fSurname} | {Form1.mondayID} | {Form1.department}"
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "HH:mm:ss"
        populateJobsList()

    End Sub

    'Assign Create Item > Update column events to Time In button
    Private Async Sub btnTimeIn_Click(sender As Object, e As EventArgs) Handles btnTimeIn.Click
        Await createNewItem()
        Form1.Timer1.Start()

    End Sub

    'Populate subtasks list based on selected task
    Private Sub cbProjectsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProjectsList.SelectedIndexChanged
        populateSubtasks()
    End Sub

    'Return to Dashboard1
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Application.Restart()
    End Sub
End Class