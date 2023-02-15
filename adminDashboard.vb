Imports System
Imports System.Drawing
Imports System.Windows.Forms.DataVisualization.Charting
Imports Newtonsoft.Json
Imports RestSharp
Imports System.Linq
Imports System.Globalization

Public Class adminDashboard
    'START OF CLASS OBJECT DECLARATION
    Public Class ColumnValue
        Public Property title As String
        Public Property text As String
        Public Property value As String
    End Class
    Public Class ItemsByMultipleColumnValue
        Public Property id As String
        Public Property column_values As ColumnValue()
    End Class
    Public Class Data
        Public Property items_by_multiple_column_values As ItemsByMultipleColumnValue()
    End Class
    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class
    'END OF CLASS OBJECT DECLARATION
    Dim maxErrorCount As Integer = 30
    Public queryTimeOut As Integer = 15000
    Public taskList() As String = {"DWB", "OWTEN-C"}
    Public hoursList() As Integer = {11, 24}
    Public con As New OleDb.OleDbConnection
    Public selectedProfile As String
    'database functions and subs.
    Public Sub connectToAccessDatabase()
        Try
            Dim conInstance As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\TiTA v3\TiTA_v3\Resources\myLogs.mdb")
            con = conInstance
            con.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Oops, something went wrong!")
        End Try
    End Sub
    Public Sub closeDatabase()
        con.Close()
    End Sub
    Public Async Function myDatabase(ByVal whatKind As String, ByVal query As String) As Task(Of Object)
        Select Case whatKind
            Case "mutate"
                connectToAccessDatabase()
                Dim cmd As New OleDb.OleDbCommand
                cmd.Connection = con
                cmd.CommandText = query
                Dim i = cmd.ExecuteNonQuery
                If i > 0 Then
                    Console.WriteLine("Success!")
                End If
                closeDatabase()
                Return True
            Case "query"
                connectToAccessDatabase()
                Dim cmd As New OleDb.OleDbCommand
                cmd.Connection = con
                cmd.CommandText = query
                Dim dt As New DataTable
                Dim da As New OleDb.OleDbDataAdapter
                da.SelectCommand = cmd
                da.Fill(dt)
                closeDatabase()
                Return dt
        End Select
    End Function
    Public Async Function fetchDataFromMonday(ByVal myDate As String, ByVal optionForFetch As String) As Task(Of Root)
        Dim query As String = ""
        Select Case optionForFetch
            Case "Daily"
                Dim reformattedMyDate = """" + myDate + """"
                query =
                    "query{
              items_by_multiple_column_values (board_id:2628729848, column_id: ""date"", column_values:[" + reformattedMyDate + "]){
                id
                column_values(ids:[person, job, time_tracking0]) {
                  title
                  text
                  value
                }
              }
            }"
            Case "Weekly"
                query =
                    "query{
              items_by_multiple_column_values (board_id:2628729848, column_id: ""date"", column_values:[" + myDate + "]){
                id
                column_values(ids:[person, job, time_tracking0]) {
                  title
                  text
                  value
                }
              }
            }"
        End Select
        Dim retry As Boolean = True
        While retry
            For retries = 0 To maxErrorCount
                If retries <> maxErrorCount Then
                    Try
                        Dim response As Object = Await SendMondayRequestVersion2(query)
                        If response(0) = "error" Then
                            'something went wrong
                            Console.WriteLine(response(1))
                        Else
                            Return JsonConvert.DeserializeObject(Of Root)(response(1))
                            Exit While
                        End If
                    Catch ex As Exception
                        Dim dlgResult = MessageBox.Show(ex.Message + Environment.NewLine + "Error happened at fetchDataFromMonday", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                        If dlgResult = DialogResult.Retry Then
                            retry = True
                        Else
                            retry = False
                        End If
                        Exit Function
                    End Try
                Else
                    Throw New Exception("Error occured when fetching daily logs.")
                End If
            Next
        End While
    End Function
    Public Function generateChartData(ByVal queryResults As Root) As List(Of Object)
        Dim taskList As New List(Of String)
        Dim personsList As New List(Of String)
        For Each items In queryResults.data.items_by_multiple_column_values
            taskList.Add(items.column_values(1).text)
            personsList.Add(items.column_values(0).text)
        Next
        Dim dtaskList = taskList.Distinct()
        Dim dpersonsList = personsList.Distinct()
        Dim distinctChartDataObject As New List(Of Object)
        For Each person As String In dpersonsList
            Dim taskPerPerson As New List(Of Object)
            For Each log In queryResults.data.items_by_multiple_column_values
                If person = log.column_values(0).text Then
                    taskPerPerson.Add({log.column_values(1).text, log.column_values(2).text})
                End If
            Next
            For Each tasks In taskPerPerson
                Dim distinctTask As New List(Of Object)
                distinctTask = taskPerPerson.FindAll(Function(x)
                                                         Return x(0) = tasks(0)
                                                     End Function)
                If distinctTask.Count = 1 Then
                    'one and only.
                    distinctChartDataObject.Add({person, tasks(0), convertToDecimalHours(tasks(1))})
                Else
                    Dim hoursLogged As Double = 0
                    For Each doubleLogs In distinctTask
                        hoursLogged += convertToDecimalHours(doubleLogs(1))
                    Next
                    If distinctChartDataObject.Exists(Function(x) x(0) = person And x(1) = tasks(0)) = False Then
                        distinctChartDataObject.Add({person, tasks(0), hoursLogged})
                    End If
                End If
            Next
        Next
        Return distinctChartDataObject
    End Function
    Public Sub GenerateNewTableAndChart(ByVal chartData As List(Of Object), ByVal myJsonObject As Root, ByVal DataGridView1 As DataGridView, ByVal Chart1 As Chart)
        'Generate Table
        Dim taskList As New List(Of String)
        Dim personsList As New List(Of String)
        For Each items In myJsonObject.data.items_by_multiple_column_values
            taskList.Add(items.column_values(1).text)
            personsList.Add(items.column_values(0).text)
        Next

        Dim dpersonsList = personsList.Distinct()
        Dim dtaskList = taskList.Distinct()

        Dim myTable As New DataTable
        myTable.Columns.Add("Names")
        For Each tasks In dtaskList
            myTable.Columns.Add(tasks)
        Next
        myTable.Columns.Add("Total")
        For Each person In dpersonsList
            Dim newRow As DataRow
            newRow = myTable.NewRow()
            newRow.Item("Names") = person
            Dim personalLog = chartData.FindAll(Function(x) x(0) = person)
            For Each logs In personalLog
                newRow.Item(logs(1)) = logs(2)
            Next
            myTable.Rows.Add(newRow)
        Next
        For Each rows As DataRow In myTable.Rows
            Dim totalHours As Double = 0
            For Each task In dtaskList
                If IsDBNull(rows.Item(task)) Then
                    rows.Item(task) = 0
                Else
                    totalHours += rows.Item(task)
                End If
            Next
            rows.Item("Total") = totalHours
        Next
        DataGridView1.DataSource = myTable

        'Generate Chart
        Chart1.Series.Clear()
        For Each tasks In dtaskList
            Dim mySeries As New Series
            mySeries.Name = tasks
            mySeries.XValueMember = "Names"
            mySeries.YValueMembers = "Hours"
            mySeries.ChartType = SeriesChartType.StackedBar
            mySeries.Points.DataBindXY(myTable.Rows, "Names", myTable.Rows, tasks)
            Chart1.Series.Add(mySeries)
        Next
        Chart1.ChartAreas(0).AxisX.Interval = 1
        Chart1.ChartAreas(0).AxisY.Interval = 0.5
        Chart1.ChartAreas(0).AlignmentStyle = AreaAlignmentStyles.AxesView
        For Each series In Chart1.Series
            'series.Points.DataBindXY(myTable.Rows, "Names", myTable.Rows, series.Name)
            series.ToolTip = "#VAL hours on #SER"
        Next



        'Generate Chart Total Hours
        'For Each person In dpersonsList
        '    Dim mySeries As New Series
        '    mySeries.Name = person
        '    mySeries
        'Next
    End Sub
    Private Async Sub adminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Timer1.Start()
        'ValidateDatabase()
        'CODE Controls Setup
        'Set max date to all DateTimePickers in every Panels.
        For Each metroPanels As Control In Me.Controls
            If metroPanels.GetType().Name = "MetroPanel" Then
                For Each dtps As Control In metroPanels.Controls
                    If dtps.GetType().Name = "MetroDateTime" Then
                        Dim x = CType(dtps, MetroFramework.Controls.MetroDateTime)
                        x.MaxDate = DateTime.Now
                    End If
                Next
            End If
        Next

        'Fill up options.
        Dim cbOptionsString As String() = {"Daily", "Weekly", "Year To Date", "Custom"}
        cbLogOption.Items.AddRange(cbOptionsString)
        cbLogOption.SelectedIndex = 0
        'Change background color to white.
        Me.BackColor = Color.White

        'Center form to middle of screen.
        Me.CenterToParent()
        'Await SyncMondayAndLocalDatabase()
        Dim mostRecentDate As Date = Await mostRecentDateInDatabase()
        Try
            Await SyncMondayAndLocalDatabase(mostRecentDate)
        Catch ex As Exception

        End Try


        'START Code to Fetch and Change Chart Data
        'Fetch Logs for today, because this is the startup screen.
        Dim myJsonObject As Root = Await fetchDataFromMonday(DateTime.Now.ToString("yyyy-MM-dd"), "Daily")

        'Create list of distinct tasks and persons involved based on results.
        If myJsonObject IsNot Nothing Then
            'Generate Chart Data
            Dim distinctChartDataObject = generateChartData(myJsonObject)
            'Create and Populate Table Data
            GenerateNewTableAndChart(distinctChartDataObject, myJsonObject, DataGridView1, Chart1)
        End If
        'END Code to Fetch and Change Chart Data
    End Sub
    Public Function convertToDecimalHours(ByVal time As DateTime) As Double
        Dim decimalHours = time.Hour + (time.Minute / 60) + (time.Second / 3600)
        Return Math.Round(decimalHours, 2)
    End Function
    Public Async Function SendMondayRequestVersion2(ByVal myQuery As String) As Task(Of Object)
        Dim options = New RestClientOptions("https://api.monday.com/v2")
        options.ThrowOnAnyError = True
        options.MaxTimeout = queryTimeOut
        Dim client = New RestClient(options)
        Dim request = New RestRequest()
        request.Timeout = queryTimeOut
        request.Method = Method.Post
        request.AddHeader("Authorization", "eyJhbGciOiJIUzI1NiJ9.eyJ0aWQiOjE1MjU2NzQ3OCwidWlkIjoxNTA5MzQwNywiaWFkIjoiMjAyMi0wMy0yNVQwMTo0Njo1My4wMDBaIiwicGVyIjoibWU6d3JpdGUiLCJhY3RpZCI6NjYxMjMxMCwicmduIjoidXNlMSJ9.F1TqwLS-QsuM8Ss3UcgskbNFUIer1dfwfoLyPMq1pbc")
        request.AddQueryParameter("query", myQuery)
        Dim response = New RestResponse
        response = Await client.PostAsync(request)
        If response.IsSuccessStatusCode Then
            'response has a statuscode of 200
            'but it might have a parse error, which still is status 200.
            If response.Content.Contains("error") Or response.Content.Contains("error_message") Or response.Content.Contains("errors") Then
                'response has a status code 200, but has a monday.com error.
                Return {"error", response.Content}
            Else
                'response has a status code 200, with readable results.
                Return {"success", response.Content}
            End If
        Else
            Throw New System.Exception("An error has occured at function: SendMondayRequestVersion2")
        End If
    End Function
    Private Sub Chart1_Click(sender As Object, e As MouseEventArgs) Handles Chart1.Click
        Try
            Console.WriteLine("Click detected.")
            Dim pointindex As Integer
            Dim result As HitTestResult
            result = Chart1.HitTest(e.X, e.Y)
            If result.ChartElementType = ChartElementType.DataPoint Or result.ChartElementType = ChartElementType.LegendItem Then
                pointindex = result.PointIndex
                Console.WriteLine(pointindex)
                'MessageBox.Show("Selected Data is " + result.Series.Name)
                Dim ptLowerLeft As New Point(Cursor.Position.X - 5, Cursor.Position.Y - 5)
                contextMenuStrip.Show(ptLowerLeft)
                btnExpandedView.Text = $"Expand to {result.Series.Name} charts"
                selectedProfile = result.Series.Points(pointindex).AxisLabel
                btnViewProfile.Text = $"View {result.Series.Points(pointindex).AxisLabel}'s profile"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'For Each series As Series In Chart1.Series
        '    For Each dp As DataPoint In series.Points
        '        dp.ToolTip = "#VALX, #VALY"
        '    Next
        'Next
    End Sub

    Private Sub ViewProfile(ByVal name As String, dgv As DataGridView)
        'Dim personalDataTable As New DataTable
        ''take all column header text and store it in a list of string   
        'Dim listOfColumnValues As New List(Of String)
        'For Each columns As DataGridViewColumn In dgv.Columns
        '    'listOfColumnValues.Add(columns.HeaderText)
        '    personalDataTable.Columns.Add(columns.HeaderText)
        'Next

        'Dim newRow As DataRow
        'newRow = personalDataTable.NewRow()

        'For Each row As DataGridViewRow In dgv.Rows
        '    If row.Cells("Names").Value = name Then
        '        'newRow = DataRow(row.Cells)
        '        For Each cells As DataGridViewCell In row.Cells

        '        Next
        '    End If
        'Next

        Dim currentDataTable As DataTable = DirectCast(dgv.DataSource, DataTable)
        Dim personalDatatable As New DataTable
        For Each column As DataColumn In currentDataTable.Columns
            personalDatatable.Columns.Add(column.ColumnName)
        Next

        For Each row As DataRow In currentDataTable.Rows
            If row.Item("Names") = name Then
                'This is the record that we need.
                Dim newRow As DataRow = row.
                ''newRow = personalDatatable.NewRow()
                'newRow = row
                personalDatatable.Rows.Add(newRow)
            End If
        Next
    End Sub
    Private Sub cbLogOption_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbLogOption.SelectedValueChanged
        For Each metroPanels As Control In Me.Controls
            If metroPanels.GetType().Name = "MetroPanel" Then
                metroPanels.Visible = False
            End If
        Next
        Select Case cbLogOption.SelectedItem
            Case "Daily"
                MetroButton2.Enabled = True
                DailyPanel.Visible = True
            Case "Weekly"
                MetroButton2.Enabled = False
                WeeklyPanel.Visible = True
                WeeklyPanel.Location = New Point(12, 85)
                dtpWeekSelect.Value = DateTime.Today
            Case "Year To Date"

            Case "Custom"
                MetroButton2.Enabled = True
                CustomPanel.Visible = True
                CustomPanel.Location = New Point(12, 85)
                Chart1.Series.Clear()
                DataGridView1.DataSource = Nothing
                DataGridView1.Refresh()
                dtpTo.Enabled = False
                btnApplyCustomFilter.Enabled = False
        End Select
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
    Private Async Sub dtpDaySelect_ValueChanged(sender As Object, e As EventArgs) Handles dtpDaySelect.ValueChanged
        'disable all controls first.
        disableAllControls()
        Chart1.Series.Clear()
        'START Code to Fetch and Change Chart Data
        'Fetch Logs for today, because this is the startup screen.
        Dim myJsonObject As Root = Await fetchDataFromMonday(dtpDaySelect.Value.ToString("yyyy-MM-dd"), "Daily")
        'Create list of distinct tasks and persons involved based on results.
        If myJsonObject IsNot Nothing Then
            'Generate Chart Data
            Dim distinctChartDataObject = generateChartData(myJsonObject)
            'Create and Populate Table Data
            GenerateNewTableAndChart(distinctChartDataObject, myJsonObject, DataGridView1, Chart1)
        End If
        'END Code to Fetch and Change Chart Data

        enableAllControls()
    End Sub
    Private Async Sub MetroDateTime1_ValueChanged(sender As Object, e As EventArgs) Handles dtpWeekSelect.ValueChanged
        disableAllControls()
        Chart1.Series.Clear()
        lblDayInAWeek.Text = ""
        Dim dayInAWeek As Date = dtpWeekSelect.Value
        Dim dfi = DateTimeFormatInfo.CurrentInfo
        Dim calendar = dfi.Calendar
        Dim weekOfYear = calendar.GetWeekOfYear(dayInAWeek, dfi.CalendarWeekRule, DayOfWeek.Monday)
        lblDayInAWeek.Text = $"Week Number {weekOfYear}"

        Dim dateRange As List(Of String) = RangeOfDateInAWeek(dayInAWeek.Year, weekOfYear)
        Dim joinedDates As String = """" + String.Join(""", """, dateRange) + """"

        Dim myJsonObject As Root = Await fetchDataFromMonday(joinedDates, "Weekly")

        If myJsonObject IsNot Nothing Then
            Dim distinctChartDataObject = generateChartData(myJsonObject)
            GenerateNewTableAndChart(distinctChartDataObject, myJsonObject, DataGridView1, Chart1)
        End If
        enableAllControls()

    End Sub
    Public Function RangeOfDateInAWeek(ByVal year As Integer, ByVal weekNumber As Integer) As List(Of String)
        Dim firstDate = New DateTime(year, 1, 1)
        While (firstDate.DayOfWeek <> DayOfWeek.Monday)
            firstDate = firstDate.AddDays(-1)
        End While

        Dim dateRange As New List(Of String)
        Dim startDate = firstDate.AddDays((weekNumber - 1) * 7)
        For i = 0 To 6
            Dim dateToAdd = startDate.AddDays(i)
            dateRange.Add(dateToAdd.ToString("yyyy-MM-dd"))
        Next
        Return dateRange
    End Function
    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        btnApplyCustomFilter.Enabled = True
        dtpTo.Enabled = True
        dtpTo.MinDate = dtpFrom.Value
    End Sub
    Private Async Sub btnApplyCustomFilter_Click(sender As Object, e As EventArgs) Handles btnApplyCustomFilter.Click
        disableAllControls()
        'find the difference of days between the start date and end date.
        Dim startDate = dtpFrom.Value
        Dim endDate = dtpTo.Value

        Dim deltaDays = endDate - startDate
        Console.WriteLine(deltaDays.Days)

        'compile all dates in a list of date.
        Dim dateRange As New List(Of String)
        For i = 0 To deltaDays.Days - 1
            dateRange.Add(startDate.AddDays(i).ToString("yyyy-MM-dd"))
        Next

        Dim joinedDates As String = """" + String.Join(""", """, dateRange) + """"
        Dim myJsonObject As Root = Await fetchDataFromMonday(joinedDates, "Weekly")

        If myJsonObject IsNot Nothing Then
            Dim distinctChartDataObject = generateChartData(myJsonObject)
            GenerateNewTableAndChart(distinctChartDataObject, myJsonObject, DataGridView1, Chart1)
        End If
        enableAllControls()
    End Sub
    Private Async Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        Dim dateRange As New List(Of Date)
        Select Case cbLogOption.SelectedItem
            Case "Daily"
                dateRange.Add(dtpDaySelect.Value)
                Await projectVsTime(dateRange)
            Case "Custom"
                dateRange.Add(dtpFrom.Value)
                dateRange.Add(dtpTo.Value)
                Await projectVsTime(dateRange)
        End Select

    End Sub
    Private Async Function SyncMondayAndLocalDatabase(Optional mostRecentUpdate As Date = Nothing) As Task
        If mostRecentUpdate = Nothing Then
            mostRecentUpdate = DateTime.Parse("2022-06-01")
        End If
        Dim numberOfDaysToSyncWith As Integer = DateTime.Now.Subtract(mostRecentUpdate).Days
        Dim dateRange As New List(Of String)
        'save all dates from the start of TiTA up to YESTERDAY.
        If numberOfDaysToSyncWith > 0 Then
            For index = 0 To numberOfDaysToSyncWith - 1
                dateRange.Add(mostRecentUpdate.AddDays(index).ToString("yyyy-MM-dd"))
            Next
            Dim jsonObjectList As New List(Of Root)
            pBar.Maximum = dateRange.Count
            pBar.Value = 1
            lblStatus.Text = "Downloading logs.."
            For Each myDate In dateRange
                lblStatus.Text = $"Downloading logs for {myDate}.. ({pBar.Value}/{dateRange.Count})"
                Dim jsonObject = Await fetchDataFromMonday(myDate, "Daily")
                If jsonObject IsNot Nothing Then
                    jsonObjectList.Add(jsonObject)
                    pBar.Increment(1)
                    For Each logs In jsonObject.data.items_by_multiple_column_values
                        Dim currentItemID = logs.id
                        Dim employeeName = logs.column_values(0).text
                        Dim task = logs.column_values(1).text
                        Dim hoursSpent = logs.column_values(2).text
                        Dim query As String =
                       $"INSERT INTO myPreviousLogs(ID, EmployeeName, TaskName, CreationDate, HoursSpent)
                                              VALUES('{currentItemID}','{employeeName}','{task}','{myDate}','{convertToDecimalHours(hoursSpent)}')"
                        Await myDatabase("mutate", query)
                    Next
                End If
            Next
            My.Settings.lastMondayUpdate = DateTime.Now().ToString("yyyy-MM-dd")
            My.Settings.Save()
            MessageBox.Show("Update complete.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            lblStatus.Text = "Update complete."
            lblStatus.Visible = False
        Else
            lblStatus.Text = "Your database is updated."
        End If
    End Function
    Public Async Function mostRecentDateInDatabase() As Task(Of Date)
        Dim query As String =
            "SELECT CreationDate
             FROM myPreviousLogs"
        Dim tableOfAllDates As DataTable = Await myDatabase("query", query)
        Dim dateList As New List(Of Date)
        If tableOfAllDates.Rows.Count > 0 Then
            For Each result As DataRow In tableOfAllDates.Rows
                Dim dateString = result.Item("CreationDate").ToString()
                Dim parsedDate As Date = DateTime.Parse(dateString)
                dateList.Add(parsedDate)
            Next
            Dim distinctDateList = dateList.Distinct()
            Dim recentDate = distinctDateList.Max
            If recentDate.DayOfWeek = DayOfWeek.Friday Then
                Return DateTime.Now
            Else
                Return recentDate
            End If

        Else
            Return Nothing
        End If
    End Function
    Public Async Function ValidateDatabase() As Task
        Dim query As String =
            "SELECT MondayID FROM myPreviousLogs "
        Try
            Dim table = Await myDatabase("query", query)

            For Each rows In table.rows

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Async Function projectVsTime(ByVal myDate As List(Of Date)) As Task
        Dim personsList As New List(Of String)
        Dim tasksList As New List(Of String)
        Dim myLogs As New List(Of Object)
        Dim myDistinctLogs As New List(Of Object)
        Dim hoursPerTask As New List(Of Object)
        Dim query As String = ""
        If myDate.Count > 1 Then
            query = "SELECT * FROM myPreviousLogs
                               WHERE CreationDate BETWEEN #" + myDate.Min.ToString("yyyy-MM-dd") + "# AND #" + myDate.Max.ToString("yyyy-MM-dd") + "#"
        Else
            query = "SELECT * FROM myPreviousLogs
                     WHERE CreationDate = #" + myDate.Max.ToString("yyyy-MM-dd") + "#"
        End If
        connectToAccessDatabase()
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = con
        cmd.CommandText = query
        Try
            Dim da As New OleDb.OleDbDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            'Get all tasks first.
            For Each tasks As DataRow In dt.Rows
                Console.WriteLine(tasks.Item("TaskName"))
            Next

            'Dim reader = cmd.ExecuteReader()
            'While reader.Read()
            '    'Console.WriteLine(reader.Item("ID"))
            '    'Console.WriteLine(reader.Item("EmployeeName"))
            '    'personsList.Add(reader.Item("EmployeeName"))
            '    'tasksList.Add(reader.Item("TaskName"))
            '    'myLogs.Add({reader.Item("EmployeeName"), reader.Item("TaskName"), CDbl(reader.Item("hoursSpent"))})
            'End While

            'Dim dtasksList = tasksList.Distinct
            'For Each tasks In dtasksList
            '    Dim logsForCertainTask = myLogs.FindAll(Function(x) x(1) = tasks)
            '    Dim hoursSpent As New Double
            '    For Each logs In logsForCertainTask
            '        hoursSpent += logs(2)
            '    Next
            '    myDistinctLogs.Add({tasks, hoursSpent})
            'Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            closeDatabase()
        End Try

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Chart1_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart1.MouseMove
        Dim result As HitTestResult
        result = Chart1.HitTest(e.X, e.Y)
        If result.ChartElementType = ChartElementType.DataPoint Or result.ChartElementType = ChartElementType.LegendItem Or result.ChartElementType = ChartElementType.AxisLabels Then
            Me.Cursor = Cursors.Hand
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub contextMenuStrip_MouseLeave(sender As Object, e As EventArgs) Handles contextMenuStrip.MouseLeave
        contextMenuStrip.Hide()
    End Sub

    Private Sub btnViewProfile_Click(sender As Object, e As EventArgs) Handles btnViewProfile.Click
        If selectedProfile <> "" Or selectedProfile IsNot Nothing Then
            ViewProfile(selectedProfile, DataGridView1)
        End If
    End Sub
End Class