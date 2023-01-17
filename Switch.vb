﻿Imports Newtonsoft.Json
Public Class Switch
    Dim idOfNewItem As String
    Dim selectedTaskProjectCode As String
    'Class Object for Serialization
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
    'Class Object Declaration
    Public Class ColumnValue
        Public Property text As String
    End Class
    Public Class Item
        Public Property column_values As ColumnValue()
    End Class
    Public Class Board
        Public Property items As Item()
    End Class
    Public Class CreateItem
        Public Property id As String
    End Class
    Public Class Data
        Public Property create_item As CreateItem
        Public Property boards As Board()
    End Class
    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class
    'END of Class Object Declaration
    Private Function populateTasksComboBox()
        'populate comboboxes first
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each tasks1 In groups.items
                cbTasks.Items.Add(tasks1.name)
            Next
        Next
        cbTasks.SelectedIndex = 0
    End Function


    Private Function filterJobs(ByVal category As String)
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each tasks In groups.items
                Select Case category
                    Case "Show All"
                        populateTasksComboBox()
                    Case "R&D"
                        If tasks.group.id = "topics" Then
                            cbTasks.Items.Add(tasks.name)
                        End If
                    Case "Jobs"
                        If tasks.group.id = "group_title" Then
                            cbTasks.Items.Add(tasks.name)
                        End If
                    Case "Admin"
                        If tasks.group.id = "new_group1823" Then
                            cbTasks.Items.Add(tasks.name)
                        End If
                    Case "Electronics R&D"
                        For Each cvals In tasks.column_values
                            If cvals.title = "ERD Tag" And cvals.text = "x" Then
                                cbTasks.Items.Add(tasks.name)
                            End If
                        Next
                    Case "Mechanical R&D"
                        For Each cvals In tasks.column_values
                            If cvals.title = "MRD Tag" And cvals.text = "x" Then
                                cbTasks.Items.Add(tasks.name)
                            End If
                        Next
                    Case "Enclosure"
                        For Each cvals In tasks.column_values
                            If cvals.title = "EN Tag" And cvals.text = "x" Then
                                cbTasks.Items.Add(tasks.name)
                            End If
                        Next
                    Case "Systems Designs"
                        For Each cvals In tasks.column_values
                            If cvals.title = "SD Tag" And cvals.text = "x" Then
                                cbTasks.Items.Add(tasks.name)
                            End If
                        Next
                    Case "Small Batch Manufacturing"
                        For Each cvals In tasks.column_values
                            If cvals.title = "SMB Tag" And cvals.text = "x" Then
                                cbTasks.Items.Add(tasks.name)
                            End If
                        Next
                End Select
            Next
        Next
    End Function

    Private Sub updateSubTasksComboBox()
        cbSubTasks.Items.Clear()
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each tasks In groups.items
                If tasks.name = cbTasks.Text Then
                    selectedTaskProjectCode = tasks.column_values(0).text
                    If tasks?.subitems IsNot Nothing Then
                        For Each subtasks In tasks.subitems
                            cbSubTasks.Items.Add(subtasks.name)
                        Next
                        cbSubTasks.SelectedIndex = 0
                    Else cbSubTasks.Items.Add("N/A")
                        cbSubTasks.SelectedIndex = 0
                    End If
                End If
            Next
        Next



    End Sub
    Private Async Sub DisplayAndSwitch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'stop Timer
        Form1.Timer1.Stop()
        cbFilter.Items.AddRange(Form1.taskCategories)
        cbFilter.SelectedIndex = 0
        positionLoginScreen()
        'upon load, disable all controls
        disableAllControls()
        'Populate tasks combobox
        populateTasksComboBox()
        'set combo box to autocomplete
        cbTasks.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cbTasks.AutoCompleteSource = AutoCompleteSource.ListItems
        'Fetch Previous task on TiTA Timeline board on monday.com
        Await FetchPreviousTaskAndSubTask(Form1.currentID)

        'when fetchprevious task is finished, enable all controls
        enableAllControls()


    End Sub
    Public Async Function FetchPreviousTaskAndSubTask(ByVal itemID As String) As Task
        Dim queryFetchPreviousTask =
                "query{
                boards(ids: 2628729848){
                    items(ids: " + itemID + "){
                        column_values(ids: [""job"", ""text4""]){
                            text
                        }
                    }
                }
            }"
        Try
            Dim response As String = Await Form1.SendMondayRequest(queryFetchPreviousTask)
            Dim jsonObj = JsonConvert.DeserializeObject(Of Root)(response)
            Dim previousTask As String = jsonObj.data.boards(0).items(0).column_values(0).text
            Dim previousSubTask As String = jsonObj.data.boards(0).items(0).column_values(0).text
            cbTasks.SelectedItem = previousTask
            cbSubTasks.SelectedItem = previousSubTask
        Catch ex As Exception
            Console.WriteLine("Error!")
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Function
        End Try
    End Function
    Private Sub cbTasks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTasks.SelectedIndexChanged
        updateSubTasksComboBox()
        cbSubTasks.Enabled = True
        btnSwitch.Enabled = True
    End Sub
    Private Async Sub btnSwitch_Click(sender As Object, e As EventArgs) Handles btnSwitch.Click
        TiTA_v3.My.Settings.lastMondayUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        My.Settings.Save()
        'Mark the previous log as done, and put in timeout column and tita 
        Dim dialogResult = MessageBox.Show($"Are you sure you want to switch to {cbTasks.SelectedItem} {cbSubTasks.SelectedItem}?", "Task Switch", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialogResult = DialogResult.Yes Then
            Try
                'Switch confirmed
                lblStatus.Text = "Marking X the previous log.."
                'Build Object to Change Column Value
                Dim payload = New ColumnValuesToChange()
                'Assign values to each property of the object.
                payload.text_1 = "X"
                payload.dup__of_time_in = DateTime.Now.ToString("HH:mm:ss")
                payload.text64 = Form1.titaVersion.ToString

                'Mark previous log as done
                disableAllControls()
                Await MarkAsDonePreviousLog(payload)
                lblStatus.Text = "Creating new log..."

                'Create new item on monday.com board
                'and save its id on idOfNewItem
                idOfNewItem = Await createNewItem()


                'Change the value of the createdItem
                'But create first the values of the column.
                Dim payload2 = New ColumnValuesToChange()
                payload2.job = cbTasks.SelectedItem
                payload2.text_1 = $"START_{Form1.fSurname}"
                payload2.text = DateTime.Now.ToString("HH:mm:ss")
                payload2.text64 = "3.0"
                payload2.text4 = cbSubTasks.SelectedItem

                Dim payloadLabel As New Labels()
                Dim labelList As New List(Of String)
                labelList.Add(selectedTaskProjectCode)
                payloadLabel.labels = labelList.ToArray

                payload2.dropdown = payloadLabel




                Form1.currentProjectNumber = selectedTaskProjectCode

                'create the payload for persons (a nested loop, so we need to construct a new payload for it)
                Dim person As New Person()
                Dim personIDandKind As New PersonsAndTeams()
                Dim personValueList As New List(Of PersonsAndTeams)
                personIDandKind.id = Form1.mondayID
                personIDandKind.kind = "person"
                personValueList.Add(personIDandKind)
                person.personsAndTeams = personValueList.ToArray
                payload2.person = person

                'Send multiple column value change request to monday
                Await ChangeMultipleColumnValues(idOfNewItem, payload2)
                'Start Cooldown
                Form1.watch.Start()
                'save all details to form1
                Form1.currentTask = cbTasks.SelectedItem
                Form1.currentSubTask = cbSubTasks.SelectedItem
                Form1.currentTimeIn = DateTime.Now.ToString("HH:mm:ss")
                'show display
                Display.Show()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Oops, something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Restart()
            End Try
        Else
            'user pressed no.
            'do nothing.
        End If
    End Sub
    Public Async Function createNewItem() As Task(Of String)
        Dim createItemQuery As String
        createItemQuery =
        "mutation{
          create_item(board_id: 2628729848 group_id: ""topics"" item_name:""" + Form1.fSurname + """){ 
            id
            name
          }
        }"
        Dim createItemResult As String = Await Form1.SendMondayRequest(createItemQuery)
        Dim OidOfNewItem As Root = JsonConvert.DeserializeObject(Of Root)(createItemResult)
        Return OidOfNewItem.data.create_item.id
    End Function
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
        Try
            Dim resultString As String = Await Form1.SendMondayRequest(MarkAsDonePreviousLogQuery)
            Console.WriteLine(resultString)
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
    Public Async Function ChangeMultipleColumnValues(ByVal itemID As String, ByVal columnValues As ColumnValuesToChange) As Task(Of String)
        Dim jsonToLoad As String = JsonConvert.SerializeObject(columnValues)
        Dim formattedJSON = jsonToLoad.Replace("""", "\""")
        Dim ChangeMultipleColumnValuesQuery As String =
            "mutation{
                change_multiple_column_values(board_id: 2628729848, item_id:" + itemID + ", column_values: """ + formattedJSON + """, create_labels_if_missing: true){
                    id
                }
            }"
        Try
            Dim resultString As String = Await Form1.SendMondayRequest(ChangeMultipleColumnValuesQuery)
            Console.WriteLine(resultString)
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
    Public Sub disableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = False
        Next
    End Sub
    Public Sub enableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = True
        Next
    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        TiTA_v3.My.Settings.lastMondayUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        My.Settings.Save()
        If Form1.elapsedTime >= 0 Then
            Form1.elapsedTime = 0
            Form1.Timer1.Start()
        End If
        'Mark the previous log as done, and put in timeout column and tita 
        Dim dialogResult = MessageBox.Show($"Are you sure you want to switch to start your lunch break?", "Go to Lunch", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialogResult = DialogResult.Yes Then
            disableAllControls()
            'Switch confirmed
            lblStatus.Text = "Marking X the previous log.."
            'Build Object to Change Column Value
            Dim payload = New ColumnValuesToChange()
            'Assign values to each property of the object.
            payload.text_1 = "X"
            payload.dup__of_time_in = DateTime.Now.ToString("HH:mm:ss")
            payload.text64 = Form1.titaVersion.ToString

            'Mark previous log as done
            Await MarkAsDonePreviousLog(payload)
            lblStatus.Text = "Creating new log..."

            'Create new item on monday.com board
            'and save its id on idOfNewItem
            idOfNewItem = Await createNewItem()


            'Change the value of the createdItem
            'But create first the values of the column.
            Dim payload2 = New ColumnValuesToChange()
            cbTasks.SelectedIndex = cbTasks.FindStringExact("Lunch")
            payload2.job = cbTasks.SelectedItem
            payload2.text_1 = $"START_{Form1.fSurname}"
            payload2.text = DateTime.Now.ToString("HH:mm:ss")
            payload2.text64 = Form1.titaVersion.ToString
            payload2.text4 = cbSubTasks.SelectedItem

            Dim payloadLabel As New Labels()
            Dim labelList As New List(Of String)
            labelList.Add(selectedTaskProjectCode)
            payloadLabel.labels = labelList.ToArray

            payload2.dropdown = payloadLabel
            selectedTaskProjectCode = Form1.currentProjectNumber

            'create the payload for persons (a nested loop, so we need to construct a new payload for it)
            Dim person As New Person()
            Dim personIDandKind As New PersonsAndTeams()
            Dim personValueList As New List(Of PersonsAndTeams)
            personIDandKind.id = Form1.mondayID
            personIDandKind.kind = "person"
            personValueList.Add(personIDandKind)
            person.personsAndTeams = personValueList.ToArray
            payload2.person = person

            'Send multiple column value change request to monday
            Await ChangeMultipleColumnValues(idOfNewItem, payload2)

            'save all details to form1
            Form1.currentTask = cbTasks.SelectedItem
            Form1.currentSubTask = cbSubTasks.SelectedItem
            Form1.currentTimeIn = DateTime.Now.ToString("HH:mm:ss")

            'show display
            Display.Show()
            Me.Close()
        Else
            'user pressed no.
            'do nothing
        End If

    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        TiTA_v3.My.Settings.lastMondayUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        My.Settings.Save()
        If Form1.elapsedTime > 0 Then
            Form1.elapsedTime = 0
            Form1.loadDelay = 60000 - Form1.elapsedTime
            Form1.Timer1.Start()
        End If
        'Mark the previous log as done, and put in timeout column and tita 
        Dim dialogResult = MessageBox.Show($"Are you sure you want to switch to start your break?", "Start Break", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialogResult = DialogResult.Yes Then
            disableAllControls()
            'Switch confirmed
            lblStatus.Text = "Marking X the previous log.."
            'Build Object to Change Column Value
            Dim payload = New ColumnValuesToChange()
            'Assign values to each property of the object.
            payload.text_1 = "X"
            payload.dup__of_time_in = DateTime.Now.ToString("HH:mm:ss")
            payload.text64 = Form1.titaVersion.ToString

            'Mark previous log as done
            Await MarkAsDonePreviousLog(payload)
            lblStatus.Text = "Creating new log..."

            'Create new item on monday.com board
            'and save its id on idOfNewItem
            idOfNewItem = Await createNewItem()


            'Change the value of the createdItem
            'But create first the values of the column.
            Dim payload2 = New ColumnValuesToChange()
            cbTasks.SelectedIndex = cbTasks.FindStringExact("Break")
            payload2.job = cbTasks.SelectedItem
            payload2.text_1 = $"START_{Form1.fSurname}"
            payload2.text = DateTime.Now.ToString("HH:mm:ss")
            payload2.text64 = Form1.titaVersion.ToString
            payload2.text4 = cbSubTasks.SelectedItem

            Dim payloadLabel As New Labels()
            Dim labelList As New List(Of String)
            labelList.Add(selectedTaskProjectCode)
            payloadLabel.labels = labelList.ToArray

            payload2.dropdown = payloadLabel
            selectedTaskProjectCode = Form1.currentProjectNumber

            'create the payload for persons (a nested loop, so we need to construct a new payload for it)
            Dim person As New Person()
            Dim personIDandKind As New PersonsAndTeams()
            Dim personValueList As New List(Of PersonsAndTeams)
            personIDandKind.id = Form1.mondayID
            personIDandKind.kind = "person"
            personValueList.Add(personIDandKind)
            person.personsAndTeams = personValueList.ToArray
            payload2.person = person

            'Send multiple column value change request to monday
            Await ChangeMultipleColumnValues(idOfNewItem, payload2)

            'save all details to form1
            Form1.currentTask = cbTasks.SelectedItem
            Form1.currentSubTask = cbSubTasks.SelectedItem
            Form1.currentTimeIn = DateTime.Now.ToString("HH:mm:ss")

            'show display
            Display.Show()
            Me.Close()
        Else
            'user pressed no.
            'do nothing.
        End If

    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        cbTasks.Items.Clear()
        filterJobs(cbFilter.SelectedItem.ToString)
        cbTasks.SelectedIndex = 0

    End Sub
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
    Private Sub cbTasks_TextUpdate(sender As Object, e As EventArgs) Handles cbTasks.TextUpdate
        cbSubTasks.Items.Clear()
        cbSubTasks.Text = ""
        If cbTasks.Items.Contains(cbTasks.Text) Then
            btnSwitch.Enabled = True
            cbSubTasks.Enabled = True
            cbTasks.SelectedItem = cbSubTasks.Text
            updateSubTasksComboBox()
        Else
            btnSwitch.Enabled = False
            cbSubTasks.Enabled = False
        End If
    End Sub
End Class