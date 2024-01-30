Imports RestSharp
Imports Newtonsoft.Json
Imports System.Threading


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
        cbProjectsList.Items.Clear()
        If Form1.department = "UK" Then
            For Each groups In Form1.UKTasks.data.boards(0).groups
                For Each tsks In groups.items
                    cbProjectsList.Items.Add(tsks.name + " - " + tsks.column_values(0).text)
                Next
            Next
            cbProjectsList.SelectedIndex = 0
        Else
            For Each groups In Form1.allTasks.data.boards(0).groups
                For Each tsks In groups.items
                    cbProjectsList.Items.Add(tsks.name)
                Next
            Next
            cbProjectsList.SelectedIndex = 0
        End If

    End Sub

    Private Function filterJobs(ByVal category As String)
        If Form1.department = "UK" Then
            For Each groups In Form1.UKTasks.data.boards(0).groups
                For Each tosks In groups.items
                    Select Case category
                        Case "Show All"
                            populateJobsList()
                            GoTo go_to_end_of_for
                        Case "Lasermet Lab Testing"
                            If tosks.group.id = "topics" Then
                                cbProjectsList.Items.Add(tosks.name + " - " + tosks.column_values(0).text)
                            End If
                        Case "Lasermet Europe GmbH Testing"
                            If tosks.group.id = "new_group15371" Then
                                cbProjectsList.Items.Add(tosks.name + " - " + tosks.column_values(0).text)
                            End If
                        Case "Calibration"
                            If tosks.group.id = "new_group82548" Then
                                cbProjectsList.Items.Add(tosks.name + " - " + tosks.column_values(0).text)
                            End If
                    End Select
                Next
            Next
        Else
            For Each groups In Form1.allTasks.data.boards(0).groups
                For Each tsks In groups.items
                    Select Case category
                        Case "Show All"
                            populateJobsList()
                            GoTo go_to_end_of_for
                        Case "R&D"
                            If tsks.group.id = "topics" Then
                                cbProjectsList.Items.Add(tsks.name)
                            End If
                        Case "Jobs"
                            If tsks.group.id = "group_title" Then
                                cbProjectsList.Items.Add(tsks.name)
                            End If
                        Case "Admin"
                            If tsks.group.id = "new_group1823" Then
                                cbProjectsList.Items.Add(tsks.name)
                            End If
                        Case "Electronics R&D"
                            For Each cvals In tsks.column_values
                                If cvals.title = "ERD Tag" And cvals.text = "x" Then
                                    cbProjectsList.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Mechanical R&D"
                            For Each cvals In tsks.column_values
                                If cvals.title = "MRD Tag" And cvals.text = "x" Then
                                    cbProjectsList.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Enclosure"
                            For Each cvals In tsks.column_values
                                If cvals.title = "EN Tag" And cvals.text = "x" Then
                                    cbProjectsList.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Systems Designs"
                            For Each cvals In tsks.column_values
                                If cvals.title = "SD Tag" And cvals.text = "x" Then
                                    cbProjectsList.Items.Add(tsks.name)
                                End If
                            Next
                        Case "Small Batch Manufacturing"
                            For Each cvals In tsks.column_values
                                If cvals.title = "SMB Tag" And cvals.text = "x" Then
                                    cbProjectsList.Items.Add(tsks.name)
                                End If
                            Next
                    End Select
                Next
            Next
go_to_end_of_for:
        End If

    End Function

    Private Function doesGroupContainItems(groupTitle As String)
        For Each groups In Form1.UKTasks.data.boards(0).groups
            If groups.title = groupTitle Then
                Dim itemCount = groups.items.Length
                If itemCount <> 0 Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Sub populateSubtasks()
        cbSubtasks.Items.Clear()
        If Form1.department = "UK" Then
            For Each groups In Form1.UKTasks.data.boards(0).groups
                For Each items In groups.items
                    If cbProjectsList.Text = items.name + " - " + items.column_values(0).text Then
                        projectCode = items.column_values(0).text
                        If items?.subitems IsNot Nothing Then
                            For Each subtask In items.subitems
                                cbSubtasks.Items.Add(subtask.name)
                                cbSubtasks.SelectedIndex = 0
                            Next
                        Else
                            cbSubtasks.Items.Add("N/A")
                            cbSubtasks.SelectedIndex = 0
                        End If
                    End If
                Next
            Next
        Else
            For Each groups In Form1.allTasks.data.boards(0).groups
                For Each items In groups.items
                    If items.name = cbProjectsList.SelectedItem Then
                        projectCode = items.column_values(0).text.ToString
                        If items?.subitems IsNot Nothing Then
                            For Each subtasks In items.subitems
                                cbSubtasks.Items.Add(subtasks.name)
                                cbSubtasks.SelectedIndex = 0
                            Next
                        Else cbSubtasks.Items.Add("N/A")
                            cbSubtasks.SelectedIndex = 0
                        End If
                    End If
                Next
            Next
        End If
    End Sub
    'Add new item to TiTO timeline
    Public Async Function createNewItem() As Task
        disableAllControls()
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
                        'response is an error. must send the request again.
                        ToolLabel1.Text = $"Error occured when creating new item. Retrying ({retries}/{Form1.maxErrorCount})"
                    Else
                        'successful response.
                        ToolLabel1.Text = "item successfully created!"
                        recon = 0
                        'deserialize
                        newItemObj = JsonConvert.DeserializeObject(Of Root)(response(1))
                        Await getItemID(newItemObj)
                        Exit For
                    End If
                Else
                    'max retries reached.
                    Throw New Exception("Error occured when creating new item.")
                End If
            Next
        Catch ex As Exception
            If recon >= 0 And recon < Form1.maxErrorCount Then
                recon += 1
                ToolLabel1.Text = $"Attempting to reconnect to Monday {recon}/{Form1.maxErrorCount}"
                Thread.Sleep(1000)
                GoTo createNewItem
            ElseIf recon >= Form1.maxErrorCount Then
                MessageBox.Show("Failed to connect to Monday. Press OK to restart TiTA", "Connection Issue", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If DialogResult.OK Then
                    recon = 0
                    Application.Restart()
                End If
            End If
            Exit Function
        End Try

        'START OLD CODE HERE
        'Try
        '    Dim createItemResult As String = Await Form1.SendMondayRequest(createItemQuery)
        '    newItemObj = JsonConvert.DeserializeObject(Of Root)(createItemResult)
        '    Await getItemID(newItemObj)
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
        mutatePOST.text64 = Form1.titaVersion
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
            "mutation {change_multiple_column_values(item_id:" + personID + ", board_id:2628729848, column_values: """ + formattedJSON + """, create_labels_if_missing: true) {id}}"

        Dim recon As Integer
buildQuery:
        Try
            For retries = 1 To Form1.maxErrorCount
                If retries <> Form1.maxErrorCount Then
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(changeColumnQuery)
                    If response(0) = "error" Then
                        'monday request encountered an error.
                        'repeat request!
                        ToolLabel1.Text = $"Error occured when changing column values. Retrying ({retries}/{Form1.maxErrorCount})."
                    Else
                        'result success.
                        MessageBox.Show("Manual login successfully created.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'start stopwatch
                        Form1.watch.Restart()
                        recon = 0
                        Dashboard1.Show()
                        Me.Close()
                        Exit For
                    End If
                Else
                    'max retries spent.
                    Throw New Exception("Error occured when changing column values.")
                End If
            Next
        Catch ex As Exception
            If recon >= 0 And recon < Form1.maxErrorCount Then
                recon += 1
                ToolLabel1.Text = $"Attempting to reconnect to Monday {recon}/{Form1.maxErrorCount}"
                Thread.Sleep(1000)
                GoTo buildQuery
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
        '    Dim result As String = Await Form1.SendMondayRequest(changeColumnQuery)
        '    Console.WriteLine(changeColumnQuery)
        '    'checkAddedItem()
        '    Dim msgSuccess = MessageBox.Show("Nice", "Success", MessageBoxButtons.OK)
        '    If msgSuccess = DialogResult.OK Then
        '        Me.Close()
        '        Dashboard1.Show()
        '        'Change delay time for Dashboard1 from 0 to 1 minute.
        '        Form1.Timer1.Start()
        '    End If

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
    Private Async Sub ManualClockIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.positionLoginScreen()
        Me.TopMost = True
        Me.Text = $"{Form1.fFirstName} {Form1.fSurname} | {Form1.mondayID} | {Form1.department}"
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "HH:mm:ss"
        DateTimePicker1.MinDate = DateAdd(DateInterval.Day, -1, DateTime.Today)
        DateTimePicker1.MaxDate = DateAdd(DateInterval.Day, 1, DateTime.Today)
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
        populateJobsList()
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

    Private Function validateTime(ByVal timeIn As String) As Boolean
        Dim timeInParsed = DateTime.Parse(timeIn)
        Dim timeDiff = timeInParsed - DateTime.Now
        If timeDiff.TotalMinutes <= -30 Or timeDiff.TotalMinutes >= 5 Then
            Dim invalidTime = MessageBox.Show("Please make sure the time you enter is less than 30 minutes earlier than current time", "Invalid time entered", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If invalidTime.OK Then
                enableAllControls()
            End If
        Else Return True
        End If

    End Function

    'Assign Create Item > Update column events to Time In button
    Private Async Sub btnTimeIn_Click(sender As Object, e As EventArgs) Handles btnTimeIn.Click

        If Form1.checkBudgetHrs(cbProjectsList.Text, Form1.allTasks) = True Then

            If validateTime(DateTimePicker1.Text) = True Then
                Await createNewItem()
                TiTA_v3.My.Settings.lastMondayUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                My.Settings.Save()
                Form1.watch.Start()
            End If

        Else
            MessageBox.Show("Project has already reached its budget hours. Reach out to your lead for extension.", "Budget Hours reached for " + cbProjectsList.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'If DialogResult.OK Then
            '    Me.Close()
            '    Dashboard1.Show()
            'End If
        End If
    End Sub

    'Populate subtasks list based on selected task
    Private Sub cbProjectsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProjectsList.SelectedIndexChanged
        populateSubtasks()
    End Sub

    'Return to Dashboard1
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Application.Restart()
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        cbProjectsList.Items.Clear()
        filterJobs(cbFilter.Text.ToString)
        cbProjectsList.SelectedIndex = 0
    End Sub

    Private Sub btnCurrentTime_Click(sender As Object, e As EventArgs) Handles btnCurrentTime.Click
        DateTimePicker1.Value = Now()
    End Sub
End Class