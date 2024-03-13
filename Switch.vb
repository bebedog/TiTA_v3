Imports Newtonsoft.Json
Imports System.Threading
Public Class Switch
    'START of Variable Declaration
    Dim idOfNewItem As String
    Dim selectedTaskProjectCode As String
    Public myTasks As New List(Of String)
    Public myCustomAutoComplete As New List(Of String)
    'END of Variable Declaration


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
    'END of Class Object Declaration


    'START of Class Object Declaration
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
    Public Class ChangeMultipleColumnValues
        Public Property id As String
    End Class

    Public Class Data
        Public Property change_multiple_column_values As ChangeMultipleColumnValues
        Public Property create_item As CreateItem
        Public Property boards As Board()
        Public Property items As Item()
    End Class
    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class
    'END of Class Object Declaration
    Private Function populateTasksComboBox()
        myTasks.Clear()
        cbTasks.Items.Clear()
        'populate comboboxes first
        If Form1.department = "UK" Then
            ' Show a different task list
            For Each groups In Form1.UKTasks.data.boards(0).groups
                For Each tasks2 In groups.items_page.items
                    'cbTasks.Items.Add(tasks2.name.ToString + " - " + tasks2.column_values(0).text)
                    'myTasks.Add(tasks2.name.ToString + " - " + tasks2.column_values(0).text)
                    Dim jobNo As String = ""
                    For Each task3 In tasks2.column_values
                        If task3.column.title = "Job No." Then
                            jobNo = task3.text
                        End If
                    Next

                    If String.IsNullOrEmpty(jobNo) Then
                        cbTasks.Items.Add(tasks2.name)
                        myTasks.Add(tasks2.name)
                    Else
                        cbTasks.Items.Add(tasks2.name + " - " + jobNo)
                        myTasks.Add(tasks2.name + " - " + jobNo)
                    End If
                Next
            Next
        Else
            For Each groups In Form1.allTasks.data.boards(0).groups
                For Each tasks1 In groups.items_page.items
                    cbTasks.Items.Add(tasks1.name)
                    myTasks.Add(tasks1.name.ToString)
                Next
            Next
        End If
        cbTasks.SelectedIndex = 0
    End Function

    Private Function filterJobs(ByVal category As String)
        If Form1.department = "UK" Then
            For Each groups In Form1.UKTasks.data.boards(0).groups
                For Each tosks In groups.items_page.items
                    Select Case category
                        Case "Show All"
                            populateTasksComboBox()
                            GoTo end_of_for
                        Case "Lasermet Lab Testing"
                            If tosks.group.id = "topics" Then
                                For Each x In tosks.column_values
                                    If x.column.title = "Job No." Then
                                        If String.IsNullOrWhiteSpace(x.text) Then
                                            cbTasks.Items.Add(tosks.name)
                                        Else
                                            cbTasks.Items.Add(tosks.name + " - " + x.text)
                                        End If
                                    End If
                                Next
                                'cbTasks.Items.Add(tosks.name + " - " + tosks.column_values(0).text)
                            End If
                        Case "Lasermet Europe GmbH Testing"
                            If tosks.group.id = "new_group15371" Then
                                For Each x In tosks.column_values
                                    If x.column.title = "Job No." Then
                                        If String.IsNullOrWhiteSpace(x.text) Then
                                            cbTasks.Items.Add(tosks.name)
                                        Else
                                            cbTasks.Items.Add(tosks.name + " - " + x.text)
                                        End If
                                    End If
                                Next
                            End If
                        Case "Calibration"
                            If tosks.group.id = "new_group82548" Then
                                For Each x In tosks.column_values
                                    If x.column.title = "Job No." Then
                                        If String.IsNullOrWhiteSpace(x.text) Then
                                            cbTasks.Items.Add(tosks.name)
                                        Else
                                            cbTasks.Items.Add(tosks.name + " - " + x.text)
                                        End If
                                    End If
                                Next
                            End If
                        Case "Other Lab Duties"
                            If tosks.group.id = "new_group56008" Then
                                For Each x In tosks.column_values
                                    If x.column.title = "Job No." Then
                                        If String.IsNullOrWhiteSpace(x.text) Then
                                            cbTasks.Items.Add(tosks.name)
                                        Else
                                            cbTasks.Items.Add(tosks.name + " - " + x.text)
                                        End If
                                    End If
                                Next
                            End If
                    End Select
                Next
            Next
        Else
            For Each groups In Form1.allTasks.data.boards(0).groups
                For Each tsks In groups.items_page.items
                    Select Case category
                        Case "Show All"
                            populateTasksComboBox()
                            GoTo end_of_for
                        Case "R&D"
                            If tsks.group.id = "topics" Then
                                cbTasks.Items.Add(tsks.name)
                            End If
                        Case "Jobs"
                            If tsks.group.id = "group_title" Then
                                cbTasks.Items.Add(tsks.name)
                            End If
                        Case "Admin"
                            If tsks.group.id = "new_group1823" Then
                                cbTasks.Items.Add(tsks.name)
                            End If
                        Case "Electronics R&D"
                            For Each cvals In tsks.column_values
                                If cvals.column.title = "ERD Tag" And cvals.text = "x" Then
                                    cbTasks.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Mechanical R&D"
                            For Each cvals In tsks.column_values
                                If cvals.column.title = "MRD Tag" And cvals.text = "x" Then
                                    cbTasks.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Enclosure"
                            For Each cvals In tsks.column_values
                                If cvals.column.title = "EN Tag" And cvals.text = "x" Then
                                    cbTasks.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Systems Designs"
                            For Each cvals In tsks.column_values
                                If cvals.column.title = "SD Tag" And cvals.text = "x" Then
                                    cbTasks.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Small Batch Manufacturing"
                            For Each cvals In tsks.column_values
                                If cvals.column.title = "SMB Tag" And cvals.text = "x" Then
                                    cbTasks.Items.Add(tsks.name)
                                End If
                            Next
                    End Select
                Next
            Next
        End If
end_of_for:

    End Function
    Private Sub updateSubTasksComboBox()
        ' When called, this sub updates the subtask combo box based on the current task/job combo box.
        cbSubTasks.Items.Clear()
        If Form1.department = "UK" Then
            '' Start of OLD CODE
            'For Each groups In Form1.UKTasks.data.boards(0).groups
            '    For Each task In groups.items_page.items
            '        If cbTasks.Text = task.name + " - " + task.column_values(0).text Then
            '            selectedTaskProjectCode = task.column_values(0).text
            '            If task?.subitems IsNot Nothing Then
            '                For Each subtask In task.subitems
            '                    cbSubTasks.Items.Add(subtask.name)
            '                Next
            '                cbSubTasks.SelectedIndex = 0
            '            Else
            '                cbSubTasks.Items.Add("N/A")
            '                cbSubTasks.SelectedIndex = 0
            '            End If
            '        End If
            '    Next
            'Next
            '' END of OLD CODE

            For Each groups In Form1.UKTasks.data.boards(0).groups
                For Each task In groups.items_page.items
                    'find job number column
                    Dim jobNo As String = Nothing
                    For Each column In task.column_values
                        If column.column.title = "Job No." Then
                            jobNo = column.text
                            selectedTaskProjectCode = column.text
                        End If
                    Next

                    If cbTasks.Text = task.name + " - " + jobNo Then
                        selectedTaskProjectCode = jobNo
                        If task?.subitems.Count > 0 Then
                            For Each subtask In task.subitems
                                cbSubTasks.Items.Add(subtask.name)
                            Next
                            cbSubTasks.SelectedIndex = 0
                        Else
                            cbSubTasks.Items.Add("N/A")
                            cbSubTasks.SelectedIndex = 0
                        End If
                    End If

                Next
            Next
        Else
            '' START of OLD CODE
            'For Each groups In Form1.allTasks.data.boards(0).groups
            '    For Each tsks In groups.items_page.items
            '        If tsks.name = cbTasks.Text Then
            '            selectedTaskProjectCode = tsks.column_values(0).text
            '            If tsks?.subitems.Count > 0 Then
            '                For Each subtsks In tsks.subitems
            '                    cbSubTasks.Items.Add(subtsks.name)
            '                Next
            '                cbSubTasks.SelectedIndex = 0
            '            Else
            '                cbSubTasks.Items.Add("N/A")
            '                cbSubTasks.SelectedIndex = 0
            '            End If
            '        End If
            '    Next
            'Next
            '' END of OLD CODE

            For Each groups In Form1.allTasks.data.boards(0).groups
                For Each tsk In groups.items_page.items
                    If tsk.name = cbTasks.Text Then
                        Dim jobNo As String
                        For Each column In tsk.column_values
                            If column.column.title = "Project No." Then
                                jobNo = column.text
                                selectedTaskProjectCode = column.text
                            End If
                        Next
                        If tsk?.subitems.Count > 0 Then
                            For Each subtask In tsk.subitems
                                cbSubTasks.Items.Add(subtask.name)
                            Next
                            cbSubTasks.SelectedIndex = 0
                        Else
                            cbSubTasks.Items.Add("N/A")
                            cbSubTasks.SelectedIndex = 0
                        End If
                    End If
                Next
            Next
        End If

    End Sub
    Private Function doesGroupContainItems(groupTitle As String)
        For Each groups In Form1.UKTasks.data.boards(0).groups
            If groups.title = groupTitle Then
                Dim itemCount = groups.items_page.items.Count
                If itemCount = 0 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Async Sub DisplayAndSwitch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        disableAllControls()
        If Form1.department = "UK" Then
            Button2.Visible = False
            Button3.Visible = False
            btnSwitch.Size = New Size(401, 80)
        Else
            Button2.Visible = True
            Button3.Visible = True
            btnSwitch.Size = New Size(129, 80)
        End If

        cbFilter.Items.Clear()

        Me.TopMost = True
        'stop Timer
        Me.Text = "Task Switch"

        Form1.Timer1.Stop()

        If Form1.department = "UK" Then
            For Each category In Form1.UKtaskCategories
                If category = "Show All" Then
                    cbFilter.Items.Add(category)
                Else
                    If doesGroupContainItems(category) = True Then
                        cbFilter.Items.Add(category)
                    End If
                End If
            Next
        Else
            cbFilter.Items.AddRange(Form1.taskCategories)
        End If
        cbFilter.SelectedIndex = 0
        positionLoginScreen()
        'upon load, disable all controls

        'Populate tasks combobox
        populateTasksComboBox()
        updateSubTasksComboBox()
        'set combo box to autocomplete
        'cbTasks.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'cbTasks.AutoCompleteSource = AutoCompleteSource.ListItems
        'Fetch Previous task on TiTA Timeline board on monday.com
        Await FetchPreviousTaskAndSubTask(Form1.currentID)

        'when fetchprevious task is finished, enable all controls
        enableAllControls()
    End Sub
    Public Async Function FetchPreviousTaskAndSubTask(ByVal itemID As String) As Task
        Dim queryFetchPreviousTask =
            "
                query{
                    items(ids:" + itemID + "){
                        column_values(ids:[""job"",""text4""]){
                            text
                        }
                    }
                }
            "

        Try
            For retries = 0 To Form1.maxErrorCount
                If retries <> Form1.maxErrorCount Then
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(queryFetchPreviousTask)
                    If response(0) = "error" Then
                        'error occured
                        lblStatus.Text = $"Error occured when fetching previous task. Retrying({retries}/{Form1.maxErrorCount})"
                    Else
                        'success
                        Dim jsonObj = JsonConvert.DeserializeObject(Of Root)(response(1))
                        Dim previousTask As String = jsonObj.data.items(0).column_values(0).text
                        Dim previousSubTask As String = jsonObj.data.items(0).column_values(1).text
                        If previousSubTask Is Nothing Then
                            cbSubTasks.Items.Add("N/A")
                            cbSubTasks.SelectedIndex = 0
                        Else
                            cbTasks.SelectedItem = previousTask
                            cbSubTasks.SelectedItem = previousSubTask
                        End If
                        Exit For
                    End If
                Else
                    'max retries reached
                    Throw New Exception("Error occured when fetching previous task.")
                End If
            Next
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Dashboard1.Show()
                Me.Close()
            Else
                Form1.Close()
            End If
        End Try
    End Function
    Private Sub cbTasks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTasks.SelectedIndexChanged
        updateSubTasksComboBox()
        cbSubTasks.Enabled = True
        btnSwitch.Enabled = True
    End Sub
    Private Async Sub btnSwitch_Click(sender As Object, e As EventArgs) Handles btnSwitch.Click
        If Form1.checkBudgetHrs(cbTasks.SelectedItem, Form1.allTasks) = True Then
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
                    Form1.currentID = idOfNewItem


                    'Change the value of the createdItem
                    'But create first the values of the column.
                    Dim payload2 = New ColumnValuesToChange()
                    payload2.job = cbTasks.SelectedItem
                    payload2.text_1 = $"START_{Form1.fSurname}"
                    payload2.text = DateTime.Now.ToString("HH:mm:ss")
                    payload2.text64 = Form1.titaVersion
                    payload2.text4 = cbSubTasks.SelectedItem 'subtask

                    Dim payloadLabel As New Labels()
                    Dim labelList As New List(Of String)
                    labelList.Add(selectedTaskProjectCode) 'project code
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
                    Await ChangeMultipleColumnValues1(idOfNewItem, payload2)
                    'Start Cooldown
                    Form1.watch.Start()
                    'save all details to form1
                    Form1.currentTask = cbTasks.SelectedItem
                    Form1.currentSubTask = cbSubTasks.SelectedItem
                    Form1.currentTimeIn = DateTime.Now.ToString("HH:mm:ss")
                    'show display
                    TiTA_v3.My.Settings.lastMondayUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    My.Settings.Save()
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
        Else
            MessageBox.Show("Project has already reached its budget hours. Reach out to your lead for extension.", "Budget Hours reached for " + cbTasks.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If DialogResult.OK Then
                Me.Close()
                Dashboard1.Show()
            End If
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

        Dim recon As Integer
createNewItem:
        Try
            For retries = 1 To Form1.maxErrorCount
                If retries <> Form1.maxErrorCount Then
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(createItemQuery)
                    If response(0) = "error" Then
                        lblStatus.Text = "Error occured when creating new item."
                    Else
                        lblStatus.Text = "Item successfully created."
                        Dim jsonObject As Root = JsonConvert.DeserializeObject(Of Root)(response(1))
                        Return jsonObject.data.create_item.id
                        Exit For
                    End If
                Else
                    Throw New Exception("Error occured when creating new item.")
                End If
            Next
        Catch ex As Exception
            If recon >= 0 And recon < Form1.maxErrorCount Then
                recon += 1
                lblStatus.Text = $"Attempting to reconnect to Monday {recon}/{Form1.maxErrorCount}"
                Thread.Sleep(1000)
                GoTo createNewItem
            ElseIf recon >= Form1.maxErrorCount Then
                MessageBox.Show("Failed to connect to Monday. Press OK to restart TiTA", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If DialogResult.OK Then
                    Application.Restart()
                End If
            End If
            Exit Function
        End Try
        'START OLD CODE HERE
        'Dim createItemResult As String = Await Form1.SendMondayRequest(createItemQuery)
        'Dim OidOfNewItem As Root = JsonConvert.DeserializeObject(Of Root)(createItemResult)
        'Return OidOfNewItem.data.create_item.id
        'END OLD CODE HERE
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

        Dim recon
MarkAsDonePreviousLog:
        Try
            For retries = 1 To Form1.maxErrorCount
                If retries <> Form1.maxErrorCount Then
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(MarkAsDonePreviousLogQuery)
                    If response(0) = "error" Then
                        lblStatus.Text = $"Error occured while marking previous log. Retrying({retries}/{Form1.maxErrorCount})"
                    Else
                        lblStatus.Text = "Previous log successfully marked."
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
                lblStatus.Text = $"Attempting to reconnect to Monday {recon}/{Form1.maxErrorCount}"
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
    Public Async Function ChangeMultipleColumnValues1(ByVal itemID As String, ByVal columnValues As ColumnValuesToChange) As Task(Of String)
        Dim serializerSettings = New JsonSerializerSettings
        serializerSettings.NullValueHandling = NullValueHandling.Ignore
        Dim jsonToLoad As String = JsonConvert.SerializeObject(columnValues, serializerSettings)
        Dim formattedJSON = jsonToLoad.Replace("""", "\""")
        Dim ChangeMultipleColumnValuesQuery As String =
            "mutation{
                change_multiple_column_values(board_id: 2628729848, item_id:" + itemID + ", column_values: """ + formattedJSON + """, create_labels_if_missing: true){
                    id
                }
            }"
        Dim recon As Integer
ChangeMultipleColumnValues:
        Try
            For retries = 1 To Form1.maxErrorCount
                Dim response As Object = Await Form1.SendMondayRequestVersion2(ChangeMultipleColumnValuesQuery)
                If retries <> Form1.maxErrorCount Then
                    If response(0) = "error" Then
                        'retry request.
                        lblStatus.Text = $"Error occured while changing values. Retrying({retries}/{Form1.maxErrorCount})"
                    Else
                        'not error.
                        lblStatus.Text = "Log values changed successfully."
                        Exit For
                    End If
                Else
                    'max retry
                    Throw New Exception("Error occured while changing values.")
                End If
            Next
        Catch ex As Exception
            'Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            'If result = DialogResult.Retry Then
            '    Application.Restart()
            'Else
            '    Me.Close()
            'End If
            'Exit Function
            If recon >= 0 And recon < Form1.maxErrorCount Then
                recon += 1
                lblStatus.Text = $"Attempting to reconnect to Monday {recon}/{Form1.maxErrorCount}"
                Thread.Sleep(1000)
                GoTo ChangeMultipleColumnValues
            ElseIf recon >= Form1.maxErrorCount Then
                MessageBox.Show("Failed to connect to Monday. Press OK to restart TiTA", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If DialogResult.OK Then
                    Application.Restart()
                End If
            End If
            Exit Function
        End Try
        'START OLD CODE
        'Try
        '    Dim resultString As String = Await Form1.SendMondayRequest(ChangeMultipleColumnValuesQuery)
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
        'END OLD CODE
    End Function
    Public Sub disableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = False
        Next
        btnSwitch.Enabled = False
    End Sub
    Public Sub enableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = True
        Next
    End Sub
    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TiTA_v3.My.Settings.lastMondayUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        My.Settings.Save()
        'If Form1.elapsedTime >= 0 Then
        '    Form1.elapsedTime = 0
        '    Form1.Timer1.Start()
        'End If
        'Mark the previous log as done, and put in timeout column and tita 
        Dim dialogResult = MessageBox.Show($"Are you sure you want to switch to start your lunch break?", "Go to Lunch", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialogResult = DialogResult.Yes Then
            disableAllControls()
            selectedTaskProjectCode = "Lunch"
            Form1.currentProjectNumber = "Lunch"
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
            Await ChangeMultipleColumnValues1(idOfNewItem, payload2)

            'Start stopwatch
            Form1.watch.Start()

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
        'If Form1.elapsedTime > 0 Then
        '    Form1.elapsedTime = 0
        '    Form1.loadDelay = 60000 - Form1.elapsedTime
        '    Form1.Timer1.Start()
        'End If
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
            labelList.Add("Break")
            payloadLabel.labels = labelList.ToArray

            payload2.dropdown = payloadLabel
            selectedTaskProjectCode = "Break"
            Form1.currentSubTask = cbSubTasks.SelectedItem
            Form1.currentProjectNumber = "Break"

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
            Await ChangeMultipleColumnValues1(idOfNewItem, payload2)


            'Start stopwatch
            Form1.watch.Start()

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
        'cbTasks.DroppedDown = False
        cbSubTasks.Items.Clear()
        cbTasks.Items.Clear()
        myCustomAutoComplete.Clear()
        For Each item As String In myTasks
            If (item.ToLower.Contains(cbTasks.Text)) Then
                myCustomAutoComplete.Add(item)
            End If
        Next
        cbTasks.Items.AddRange(myCustomAutoComplete.ToArray)
        cbTasks.SelectionStart = cbTasks.Text.Length
        cbTasks.DroppedDown = True
        cbTasks.Cursor = Cursors.Default
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
    Private Sub btnAdminTools_Click(sender As Object, e As EventArgs) Handles btnAdminTools.Click
        Me.Hide()

    End Sub

    Private Async Sub btnRefreshTask_Click(sender As Object, e As EventArgs) Handles btnRefreshTask.Click
        disableAllControls()
        Await Form1.fetchMondayData()
        If Form1.department = "UK" Then
            Button2.Visible = False
            Button3.Visible = False
            btnSwitch.Size = New Size(401, 80)
        Else
            Button2.Visible = True
            Button3.Visible = True
            btnSwitch.Size = New Size(129, 80)
        End If

        cbFilter.Items.Clear()

        Me.TopMost = True
        'stop Timer
        Me.Text = "Task Switch"

        Form1.Timer1.Stop()

        If Form1.department = "UK" Then
            For Each category In Form1.UKtaskCategories
                If category = "Show All" Then
                    cbFilter.Items.Add(category)
                Else
                    If doesGroupContainItems(category) = True Then
                        cbFilter.Items.Add(category)
                    End If
                End If
            Next
        Else
            cbFilter.Items.AddRange(Form1.taskCategories)
        End If
        cbFilter.SelectedIndex = 0
        positionLoginScreen()
        'upon load, disable all controls

        'Populate tasks combobox
        populateTasksComboBox()
        updateSubTasksComboBox()
        'set combo box to autocomplete
        'cbTasks.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'cbTasks.AutoCompleteSource = AutoCompleteSource.ListItems
        'Fetch Previous task on TiTA Timeline board on monday.com
        Await FetchPreviousTaskAndSubTask(Form1.currentID)

        'when fetchprevious task is finished, enable all controls
        enableAllControls()
    End Sub
End Class