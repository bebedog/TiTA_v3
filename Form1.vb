Imports RestSharp
Imports Newtonsoft.Json
Imports Squirrel
Imports System.Threading.Tasks

Public Class Form1
    'START of Variable Declaration
    Dim resultsList As List(Of Object)
    Public watch As Stopwatch
    Public maxErrorCount As Integer = 30

    Public queryTimeOut As Integer = 15000
    Public titaVersion As String = TiTA_v3.My.Application.Info.Version.ToString
    Public allTasks As Root
    Public accounts
    Public elapsedTime As Integer
    Public loadDelay As Integer
    Dim namesList
    Dim apiKey As String = "eyJhbGciOiJIUzI1NiJ9.eyJ0aWQiOjIxNTg3NTczMCwidWlkIjozNjkzODg5OSwiaWFkIjoiMjAyMy0wMS0wM1QwMzoxNjoyOC4wMDBaIiwicGVyIjoibWU6d3JpdGUiLCJhY3RpZCI6NjYxMjMxMCwicmduIjoidXNlMSJ9.WyM7DJEbXNeF4r6leiLcLbb9oFe57alDkwMhHWEkKrM"

    Public projectListBoard As String = "2718204773
"

    'Stores no. of minutes since last update sent to Monday.com
    Public howLong

    'Task categories
    Public taskCategories() As String = {"Show All", "R&D", "Jobs", "Admin", "Electronics R&D", "Mechanical R&D", "Enclosure", "Systems Designs", "Small Batch Manufacturing"}

    'Variable for the ID of the current log
    Public currentID As String
    Public fSurname As String
    Public fFirstName As String
    Public mondayID As String
    Public department As String
    Public manualLogInID As String
    Public accountItemID As String

    'variables for Switch forms
    Public currentTask As String
    Public currentSubTask As String
    Public currentTimeIn As String
    Public currentProjectNumber As String
    'END of Variable Declaration

    'START of Squirrel Objects
    Public Class UpdateInfo
        Public CurrentlyInstalledVersion As ReleaseEntry
        Public FutureReleaseEntry As ReleaseEntry
        Public ReleasesToApply As List(Of ReleaseEntry)
    End Class

    Public Class ReleaseEntry
        Public Property SHA1 As String
        Public Property Filename As String
        Public Property Filesize As Long
        Public Property IsDelta As Boolean
    End Class
    'END of Squirrel Objects


    'START of Class Declaration for Deserialization (Errors)
    Public Class Location1
        Public Property line As Integer
        Public Property column As Integer
    End Class
    Public Class Error1
        Public Property message As String
        Public Property locations As Location1()
        Public Property fields As String()
    End Class
    Public Class ErrorRoot
        Public Property errors As Error1()
        Public Property account_id As Integer
        Public Property error_code As String
        Public Property status_code As Integer
        Public Property error_message As String
        Public Property error_data As String
    End Class
    'END of Class Declaration for Deserialization (Errors)

    'START of Class Declaration for Serialization (Changing ColumnValues for Previous Log)
    Public Class ColumnValuesToChange
        Public Property text_1 As String ' START_Surname
        Public Property dup__of_time_in As String 'Timeout
        Public Property text64 As String 'TiTA Version
    End Class
    'End of Class Declaration for Serialization (Changing ColumnValues for Previous Log)

    'Start Root Classes Declaration
    Public Class Group
        Public Property id As String
        Public Property items As Item()
    End Class
    Public Class Subitem
        Public Property name As String
    End Class
    Public Class ColumnValue
        Public Property title As String
        Public Property text As String
    End Class
    Public Class Item
        Public Property name As String
        Public Property subitems As Subitem()
        Public Property column_values As ColumnValue()
        Public Property id As String
        Public Property group As Group
    End Class
    Public Class Board
        Public Property items As Item()
        Public Property groups As Group()
    End Class
    Public Class Data
        Public Property boards As Board()
    End Class
    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class
    'END Root Classes Declaration
    Public Async Function SendMondayRequest(ByVal myQuery As String) As Task(Of String)
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
        If response.IsSuccessStatusCode = True Then
            If response.Content.Contains("error_message") Or response.Content.Contains("error_code") Or response.Content.Contains("errors") Then
                Console.WriteLine("Error caught: " + response.Content)
            Else
                Return response.Content
            End If
        Else
            Return False
        End If
    End Function

    Public Function checkBudgetHrs(ByVal task As String, ByVal tasksList As Form1.Root) As Boolean

        For Each group In tasksList.data.boards(0).groups
            For Each item In group.items
                If item.name = task Then
                    If String.IsNullOrWhiteSpace(item.column_values(1).text) Or item.column_values(1).text.ToString = "0" Then
                        Return True
                    Else
                        If item.column_values(1).text <= item.column_values(2).text Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If
            Next
        Next

    End Function
    Public Function checkAccountDetails(ByVal surname As String, ByVal password As String, ByVal accounts As Root) As Boolean
        '0 - First Name
        '1 - Monday ID
        '2 - Password
        '3 - Department

        For Each x In accounts.data.boards(0).items
            If x.name = surname Then
                'account found.
                If x.column_values(3).text = password Then
                    'account verified
                    'save all account details in a global variable.
                    fSurname = x.name
                    fFirstName = x.column_values(1).text
                    mondayID = x.column_values(2).text
                    department = x.column_values(4).text
                    accountItemID = x.id
                    Return True
                Else
                    Return False
                End If
            End If
        Next
    End Function
    Public Function populateCB(ByVal namesList As Root)
        For Each x In namesList.data.boards(0).items
            cbUsername.Items.Add(x.name)
        Next
    End Function
    Public Sub DisableAllControls()
        tbPassword.Enabled = False
        cbUsername.Enabled = False
        btnSignin.Enabled = False
        btnChangePW.Enabled = False
    End Sub
    Public Sub EnableAllControls()
        tbPassword.Enabled = True
        cbUsername.Enabled = True
        btnSignin.Enabled = True
        btnChangePW.Enabled = True
    End Sub

    Private Async Function CheckForUpdates() As Task
        Try
            Using manager = Await UpdateManager.GitHubUpdateManager($"https://github.com/bebedog/TiTA_v3")
                Await manager.UpdateApp()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Oops, something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Async Function fetchMondayData() As Task
        'QUERIES STARTS HERE
        Dim fetchTasksQuery As String =
            "query{
                boards(ids: " + projectListBoard + "){
                    groups(ids:[""topics"",""group_title"", ""new_group1823""]){
                        items{
                            name
                            group{
                            id
                            }
                            subitems{
                                name
                            }
                            column_values(ids:[""text"", ""text6"", ""text64"", ""text79"", ""text0"", ""text_1"", ""dup__of_budget_expense"",""ytd_hours""]){
                                title
                                text
                            }
                        }
                    }
                }
            }"
        Dim fetchAccountQuery As String =
            "query{
                boards(ids:3428362986){
                    items{
                        id    
                        name
                        column_values{
                            title
                            text
                        }
                    }
                }
            }"
        Dim fetchNames As String =
            "query{
                boards(ids:[3428362986]) 
                {
                  items{
                    name
                    id
                }
                }}"
        'QUERIES ENDS HERE

        lblStatus.Text = "Fetching Accounts..."
        DisableAllControls()
        Dim queries As New List(Of String)
        'add all queries in this list
        queries.Add(fetchAccountQuery)
        queries.Add(fetchNames)
        queries.Add(fetchTasksQuery)
        Try
            Dim deserializedResults As New List(Of Object)
            Dim isError As New Boolean
            Dim badQuery As New List(Of String)
            Console.WriteLine("Trying to fetch....")
            For retries = 1 To maxErrorCount
                deserializedResults.Clear()
                ToolStripProgressBar1.Maximum = maxErrorCount
                Dim goodQueryCounter As Integer = 0
                If retries <> maxErrorCount Then
                    For Each query In queries
                        Dim result = (Await SendMondayRequestVersion2(query))
                        If result(0) = "error" Then
                            'the result is an error.
                            'deserialize into ErrorRoot
                            'deserializedResults.Add(JsonConvert.DeserializeObject(Of ErrorRoot)(result(1)))
                            'badQuery.Add(query)
                            Console.WriteLine($"Something went wrong. Retrying... {retries + 1}/{maxErrorCount}")
                            ToolStripProgressBar1.Increment(-10)
                            Exit For
                        Else
                            'the result is success.
                            'deserialize into Root
                            deserializedResults.Add(JsonConvert.DeserializeObject(Of Root)(result(1)))
                            goodQueryCounter += 1
                            ToolStripProgressBar1.Increment(10)
                        End If
                    Next
                Else
                    'max retries are all used.
                    Throw New Exception("Could not fetch accounts.")
                    Exit Function
                End If

                If goodQueryCounter = queries.Count Then
                    Exit For
                End If
            Next
            accounts = deserializedResults(0)
            namesList = deserializedResults(1)

            allTasks = deserializedResults(2)
            populateCB(namesList)
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

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Me.positionLoginScreen()
        watch = Stopwatch.StartNew()
        watch.Stop()
        lblVersion.Text = $"v{titaVersion}"
        Me.Text = $"Lasermet TiTA v{titaVersion}"
        cbUsername.AutoCompleteSource = AutoCompleteSource.ListItems
        timesinceLastUpdate()

        Await CheckForUpdates()
        Await fetchMondayData()

        If TiTA_v3.My.Settings.recentUser <> "" Then
            cbUsername.SelectedItem = TiTA_v3.My.Settings.recentUser
            'timesinceLastUpdate()
        End If
        'If TiTA_v3.My.Settings.lastMondayUpdate <> "" Then
        '    lblStatus.Text = "It's been " + howLong.ToString + " seconds since your last update to Monday"
        'Else lblStatus.Text = "Accounts Fetched."
        'End If
        lblStatus.Text = "Accounts fetched."
        EnableAllControls()

    End Sub

    Private Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignin.Click
        fetchMondayTimer.Enabled = True
        fetchMondayTimer.Interval = 7200000
        fetchMondayTimer.Start()
        If checkAccountDetails(cbUsername.Text, tbPassword.Text, accounts) = True Then
            'Account detail matches
            MessageBox.Show($"Welcome back, {fFirstName}!", "Log In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Visible = False
            Dashboard1.Show()
        Else
            MessageBox.Show("Incorrect Password.")
            'Account Detail don't match.
            tbPassword.Select()
            tbPassword.Clear()
        End If
        TiTA_v3.My.Settings.recentUser = cbUsername.Text
        My.Settings.Save()
    End Sub

    Private Sub fetchMondayTimer_Tick(sender As Object, e As EventArgs) Handles fetchMondayTimer.Tick
        fetchMondayData()
    End Sub

    'Initiated after Manual Time In.
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        elapsedTime = elapsedTime + 1000
        Console.WriteLine(elapsedTime)
    End Sub
    Private Sub btnChangePW_Click(sender As Object, e As EventArgs) Handles btnChangePW.Click
        ChangePassword.Show()
        Me.Hide()
    End Sub
    Private Sub timesinceLastUpdate()
        If TiTA_v3.My.Settings.lastMondayUpdate = "" Then
            'empty setting
            howLong = 0
        Else
            Dim lastUpdateTime As String = TiTA_v3.My.Settings.lastMondayUpdate
            Dim lastUpdateTime_parsed = DateTime.Parse(lastUpdateTime)
            Dim minuteSinceLastUpdate = DateTime.Now - lastUpdateTime_parsed
            howLong = Math.Round(minuteSinceLastUpdate.TotalSeconds, 0)
            Console.WriteLine(howLong.ToString + " seconds since last update")
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
    Public Async Function SendMondayRequestVersion2(ByVal myQuery As String) As Task(Of Object)
        Dim options = New RestClientOptions("https://api.monday.com/v2")
        options.ThrowOnAnyError = True
        options.MaxTimeout = queryTimeOut
        Dim client = New RestClient(options)
        Dim request = New RestRequest()
        request.Timeout = queryTimeOut
        request.Method = Method.Post
        request.AddHeader("Authorization", apiKey)
        request.AddQueryParameter("query", myQuery)
        Dim response = New RestResponse
        response = Await client.PostAsync(request)
        'If response.IsSuccessStatusCode = True Then
        '    Return response.Content
        'Else
        '    Return False
        'End If
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
End Class
