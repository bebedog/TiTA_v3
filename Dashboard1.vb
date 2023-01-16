Imports System.Threading
Imports Newtonsoft.Json
Imports QuoteBank

Public Class Dashboard1

    Public elapsedTime As Integer
    Dim previousLog As Root
    Dim duplicateIDstoDelete As New List(Of String)
    Public newItemID
    Public newItemObj

    Public fetchStatus As String



    'Timer and StopWatch
    Dim elapsedTimeInSeconds As Integer
    Dim timeToWaitInSeconds As Integer


    Public Class ChangeMultipleColumnValues
        Public Property id As String
    End Class
    Public Class ItemsByColumnValue
        Public Property id As String
        Public Property name As String
        Public Property column_values As ColumnValue()
    End Class
    Public Class ColumnValue

        Public Property text As String
        Public Property title As String
        Public Property value As String
    End Class
    Public Class Item
        Public Property name As String
        Public Property column_values As ColumnValue()
    End Class
    Public Class Group
        Public Property items As Item()
    End Class
    Public Class Board
        Public Property groups As Group()
    End Class
    Public Class CreateItem
        Public Property id As String
        Public Property name As String
    End Class
    Public Class Data
        Public Property items_by_column_values As ItemsByColumnValue()
        Public Property change_multiple_column_values As ChangeMultipleColumnValues
        Public Property create_item As CreateItem
    End Class
    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Private Async Sub Dashboard1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        positionLoginScreen()
        lblQuotes.Text = Await getRandomQuotes()
        Me.Text = $"{Form1.fFirstName} {Form1.fSurname} | {Form1.mondayID} | {Form1.department}"
        Me.TopMost = True
        DataGridView1.Visible = False
        DataGridView1.Enabled = True
        ''Create datatable instance
        'Dim table As New DataTable
        ''create 3 typed columns in the datatable.
        'table.Columns.Add("Dosage", GetType(Integer))
        'table.Columns.Add("Drug", GetType(String))
        'table.Columns.Add("PatientID", GetType(String))

        ''add rows with those columns filled in the datatable.
        'table.Rows.Add(25, "Drug A", "10")


        'DataGridView1.DataSource = table
    End Sub
    Public Async Sub Dashboard1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Check the stopwatch if it is running.
        If Form1.watch.IsRunning Then
            If Int(Form1.watch.Elapsed.ToString("ss")) > 60 Then
                'the stopwatch is running, but it has been 60 seconds already
                'that means, the program doesn't have to introduce a delay anymore.
                timeToWaitInSeconds = 0
            Else
                'if it's running, take the elapsed time and minus it by
                '60 seconds. That is the duration of the delay before querying again.
                elapsedTimeInSeconds = Int(Form1.watch.Elapsed.ToString("ss"))
                timeToWaitInSeconds = 60 - elapsedTimeInSeconds
                Timer1.Start()
            End If
        Else
            'if it is not running, that means that this is the first time querying.
            'This means that the duration of delay is 0 seconds!
            timeToWaitInSeconds = 0
        End If
        Await Task.Delay(timeToWaitInSeconds * 1000) 'The delay method takes time in milliseconds. Thus the x1000.
        fetchStatus =
            "query{
                items_by_column_values(board_id: 2628729848, column_id: ""text_1"", column_value: ""START_" + Form1.fSurname + """){
                    id
                    name
                    column_values(ids:[""text"",""job"", ""text4""]){
                        title
                        text
                        value
                    }
                }
            }"
        Try
            Dim result As String = Await Form1.SendMondayRequest(fetchStatus)
            previousLog = JsonConvert.DeserializeObject(Of Root)(result)
            ShowDuplicateLogs(previousLog, DataGridView1)
            Dim count As Integer = previousLog.data.items_by_column_values.Length
            If count = 0 Then
                Label1.Text = "No previous log found. Clock In Manually?"
                Dim msgResult = MessageBox.Show("No previous log found. Clock In Manually?", "No Record Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If msgResult = DialogResult.Yes Then
                    ManualClockIn.Show()
                    Me.Close()
                Else
                    Application.Restart()
                End If
            ElseIf count > 1 Then
                Me.Label1.Text = "Duplicate Entries Found. Please select your most recent record."
                DataGridView1.Visible = True
                DataGridView1.Enabled = True
            Else
                Me.Label1.Text = "Success"
                Form1.currentID = previousLog.data.items_by_column_values(0).id
                My.Forms.Switch.Show()
                Me.Close()
            End If
        Catch ex As Exception
            Console.WriteLine("Error!")
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Sub
        Finally
            Form1.watch.Reset()
        End Try
    End Sub
    Public Async Sub createNewItem()
        Dim createItemQuery As String
        createItemQuery =
        "mutation{
          create_item(board_id: 2628729848 group_id: ""topics"" item_name:""" + Form1.fSurname + """){ 
            id
            name
          }
        }"
        Await Form1.SendMondayRequest(createItemQuery)
        Dim createItemResult As String = Await Form1.SendMondayRequest(createItemQuery)
        newItemObj = JsonConvert.DeserializeObject(Of Root)(createItemResult)
        getItemID(newItemObj)
    End Sub
    Public Function getItemID(createdItem As Root)
        newItemID = createdItem.data.create_item.id
        ManualClockIn.ToolLabel1.Text = newItemID
    End Function
    Private Sub resetDashboardForm(ByVal form_name As Form)
        Dim newInstanceOfDashboard1 As New Dashboard1
        form_name.Dispose()
        newInstanceOfDashboard1.Show()
    End Sub
    Public Sub ShowDuplicateLogs(ByVal results As Root, ByVal targetDataGridView As DataGridView)
        Dim duplicateLogs As New DataTable
        duplicateLogs.Columns.Add("Task", GetType(String))
        duplicateLogs.Columns.Add("Time-in", GetType(String))
        duplicateLogs.Columns.Add("Item ID", GetType(String))
        Dim btn As New DataGridViewButtonColumn()
        For Each c In results.data.items_by_column_values
            duplicateLogs.Rows.Add(c.column_values(0).text, c.column_values(2).text, c.id)
        Next
        targetDataGridView.Columns.Add(btn)
        btn.HeaderText = "Control"
        btn.Text = "Select Log"
        btn.Name = "btn"
        btn.UseColumnTextForButtonValue = True
        targetDataGridView.DataSource = duplicateLogs
    End Sub
    Private Async Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Form1.watch.Start()
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso e.RowIndex >= 0 Then
            Form1.currentID = senderGrid.CurrentRow.Cells(3).Value.ToString
            'MsgBox(senderGrid.CurrentRow.Cells(1).Value.ToString)
            Dim result As DialogResult =
            MessageBox.Show($"You have selected the log with the following details as your previous log: {Environment.NewLine}
                              Task: {senderGrid.CurrentRow.Cells(1).Value.ToString} {Environment.NewLine}
                              Time-in: {senderGrid.CurrentRow.Cells(2).Value.ToString} {Environment.NewLine}
                              ID: {senderGrid.CurrentRow.Cells(3).Value.ToString} {Environment.NewLine}{Environment.NewLine} Would you like to delete the other found logs?", "Log Selected", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                'delete all remaining logs
                Label1.Text = "Deleting remaining logs..."
                'remove the selected id from the dataset.
                senderGrid.Rows.RemoveAt(e.RowIndex)
                For Each rows As DataGridViewRow In DataGridView1.Rows
                    Console.WriteLine($"ID: {rows.Cells("Item ID").Value}")
                    duplicateIDstoDelete.Add(rows.Cells("Item ID").Value)
                Next
                'loop to delete items from TiTO timeline board.
                For Each deleteItemRequest As String In duplicateIDstoDelete
                    Dim requestString As String =
                        "mutation{
                            delete_item(item_id:" + deleteItemRequest + "){
                                id
                            }
                        }"
                    Try
                        Label1.Text = $"Deleting Item with ID: {deleteItemRequest}"
                        Await Form1.SendMondayRequest(requestString)
                        MessageBox.Show($"Successfully deleted item with ID:{deleteItemRequest}")
                    Catch ex As Exception
                        Dim result1 As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                        If result1 = DialogResult.Retry Then
                            resetDashboardForm(My.Forms.Dashboard1)
                        Else
                            Me.Close()
                        End If
                        Exit Sub
                    End Try
                Next
                resetDashboardForm(My.Forms.Dashboard1)
            ElseIf result = DialogResult.No Then
                'Mark the previous logs as done ("X")
                MsgBox("marking logs")
            Else
                'returns the user to the selection screen
                Form1.currentID = ""
            End If

        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (60 - Int(Form1.watch.Elapsed.TotalSeconds)) Then
            Label1.Text = $"The program is sleeping for {60 - Int(Form1.watch.Elapsed.TotalSeconds)} seconds to avoid overloading the monday.com servers."
        Else
            Timer1.Stop()
            Label1.Text = "Countdown Done!"
        End If
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
    Public Async Function getRandomQuotes() As Task(Of String)
        Dim englishQuotes As Quotes.English = New Quotes.English()
        Return englishQuotes.Motivation
    End Function
End Class