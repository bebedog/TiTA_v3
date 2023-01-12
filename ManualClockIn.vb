Imports RestSharp
Imports Newtonsoft.Json


Public Class ManualClockIn

    Dim projectsList
    Public newItemID
    Public newItemObj
    Dim projectCode

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


        Dim currentProjectCode As New Labels()
        Dim projectCodeLabels As New List(Of String)
        projectCodeLabels.Add(projectcode)
        currentProjectCode.labels = projectCodeLabels.ToArray

        mutatePOST.dropdown = currentProjectCode



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
            Await Form1.SendMondayRequest(changeColumnQuery)
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
        populateJobsList()


    End Sub

    Private Async Sub btnTimeIn_Click(sender As Object, e As EventArgs) Handles btnTimeIn.Click

        Await createNewItem()

    End Sub

    Private Sub cbProjectsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProjectsList.SelectedIndexChanged
        populateSubtasks()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
        Dashboard1.Show()
    End Sub
End Class