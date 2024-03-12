Imports RestSharp
Imports Newtonsoft.Json
Imports Squirrel
Imports System.Threading.Tasks

Public Class Form1
    'START of Variable Declaration
    Dim resultsList As List(Of Object)
    Public watch As Stopwatch
    Public maxErrorCount As Integer = 30
    Public elapsedDutyHours As Stopwatch = Stopwatch.StartNew()

    Public queryTimeOut As Integer = 15000
    Public titaVersion As String = TiTA_v3.My.Application.Info.Version.ToString
    Public allTasks As Root
    Public UKTasks As Root
    Public accounts As Root
    Public elapsedTime As Integer
    Public loadDelay As Integer
    Dim namesList
    Dim apiKey As String = "eyJhbGciOiJIUzI1NiJ9.eyJ0aWQiOjE1MjU2NzQ3OCwiYWFpIjoxMSwidWlkIjoxNTA5MzQwNywiaWFkIjoiMjAyMi0wMy0yNVQwMTo0Njo1My4wMDBaIiwicGVyIjoibWU6d3JpdGUiLCJhY3RpZCI6NjYxMjMxMCwicmduIjoidXNlMSJ9.lin5hxWOEzA2OeKffCZALMKXkLVS7vMYbn7B_M6BaOc"

    Public projectListBoard As String = "2718204773
"

    'Stores no. of minutes since last update sent to Monday.com
    Public howLong

    'Task categories
    Public taskCategories() As String = {"Show All", "R&D", "Jobs", "Admin", "Electronics R&D", "Mechanical R&D", "Enclosure", "Systems Designs", "Small Batch Manufacturing"}
    Public UKtaskCategories() As String = {"Show All", "Lasermet Lab Testing", "Lasermet Europe GmbH Testing", "Calibration", "Admin", "Other Lab Duties"}
    'Variable for the ID of the current log
    Public currentID As String
    Public fSurname As String
    Public fFirstName As String
    Public mondayID As String
    Public department As String
    Public manualLogInID As String
    Public accountItemID As String
    Public privilege As Integer

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
    End Class

    Public Class Subitem
        Public Property name As String
    End Class
    Public Class Column
        Public Property title As String
    End Class
    Public Class ColumnValue
        Public Property text As String
        Public Property column As Column

    End Class

    Public Class Item
        Public Property id As String
        Public Property name As String
        Public Property group As Group
        Public Property subitems As IList(Of Subitem)
        Public Property column_values As IList(Of ColumnValue)
    End Class
    Public Class ItemsPage
        Public Property cursor As String
        Public Property items As IList(Of Item)
    End Class

    Public Class Groups
        Public Property id As String
        Public Property title As String
        Public Property items_page As ItemsPage
    End Class

    Public Class Board
        Public Property groups As IList(Of Groups)
        Public Property items_page As ItemsPage
    End Class
    Public Class NextItemPage
        Public Property cursor As String
        Public Property items As IList(Of Item)
    End Class
    Public Class Data
        Public Property boards As IList(Of Board)
        Public Property next_items_page As NextItemPage
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
        If department = "UK" Then
            Return True
        Else
            ''OLD CODE START
            'For Each group In tasksList.data.boards(0).groups
            '    For Each item In group.items_page.items
            '        If item.name = task Then
            '            If String.IsNullOrWhiteSpace(item.column_values(1).text) Or item.column_values(1).text.ToString = "0" Then
            '                Return True
            '            Else
            '                If item.column_values(1).text <= item.column_values(2).text Then
            '                    Return False
            '                Else
            '                    Return True
            '                End If
            '            End If
            '        End If
            '    Next
            'Next
            ''OLD CODE END

            ' For the new code, I've already incorporated the column title so it'll be easier to compare.
            ' Loop through the group
            For Each group In tasksList.data.boards(0).groups
                'Find the task in task list
                For Each item In group.items_page.items
                    If item.name = task Then
                        ' task found
                        ' loop through columns and look for budget hours
                        For Each column In item.column_values
                            If (column.column.title = "Budget Hours") Then
                                ' Budget hours column found.
                                If String.IsNullOrWhiteSpace(column.text) Or column.text.ToString = "0" Then
                                    ' this task does not have a budget hours so anyone can time in.
                                    Return True
                                Else
                                    'this task does have an integer value in place.
                                    ' we find the ytd hours in this task
                                    For Each column2 In item.column_values
                                        If column2.column.title = "YTD Hours" Then
                                            ' YTD Hours column found
                                            If String.IsNullOrWhiteSpace(column2.text) Or column.text <= column2.text Then
                                                ' YTD Hours column has nothing in it
                                                ' OR
                                                ' YTD Hours is way past its budget hours.
                                                Return False
                                            Else
                                                Return True
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End If
    End Function
    Public Function checkAccountDetails(ByVal surname As String, ByVal password As String, ByVal accounts As Root) As Boolean
        '0 - First Name
        '1 - Monday ID
        '2 - Password
        '3 - Department

        For Each x In accounts.data.boards(0).items_page.items
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
                    privilege = Int32.Parse(x.column_values(5).text)
                    Return True
                Else
                    Return False
                End If
            End If
        Next
    End Function
    Public Sub populateCB()
        For Each account In accounts.data.boards(0).items_page.items
            cbUsername.Items.Add(account.name)
        Next
    End Sub
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
        ' For this query, this postman sample is used: https://interstellar-zodiac-223984.postman.co/workspace/My-Workspace~e1229d4d-0716-4512-8c82-8155033a6f8b/request/10748988-63cd8496-4c62-43d0-8448-68bf58704dd3
        Dim fetchTasksQuery As String =
            "
                query{
                    boards(ids: 2718204773){
                        groups(ids: [""topics"", ""group_title"", ""new_group1823""]){
                            id
                            items_page{
                                cursor
                                items{
                                    name
                                    group{
                                        id
                                    }
                                    subitems{
                                        name
                                    }
                                    column_values{
                                        column{
                                            title
                                        }
                                        text
                                    }
                                }
                            }
                        }
                    }
                }
           "
        'This query uses postman sample: https://interstellar-zodiac-223984.postman.co/workspace/My-Workspace~e1229d4d-0716-4512-8c82-8155033a6f8b/request/10748988-4b6ae90b-accc-4e3e-83c8-b880d0374f94?tab=body
        Dim fetchUKTaskQuery As String =
            "
            query{
                boards(ids:2821516243){
                    groups(ids: [""topics"", ""new_group15371"", ""new_group82548"", ""new_group56008""]){
                        id
                        title
                        items_page{
                            cursor
                            items{
                                    name
                                    group{
                                        id
                                    }
                                    subitems{
                                        name
                                    }
                                    column_values{
                                        column{
                                            title
                                        }
                                        text
                                    }
                                }
                        }
                    }
                }
            }
"
        'This query uses this postman sample: https://interstellar-zodiac-223984.postman.co/workspace/My-Workspace~e1229d4d-0716-4512-8c82-8155033a6f8b/request/10748988-c4754e6f-0424-4423-87e2-c1e4291b7ca7
        Dim fetchAccountQuery As String = "query{
                boards(ids:3428362986){
                    items_page{
                        cursor
                        items{
                        id    
                        name
                        column_values{
                            column{
                                title
                                }
                         text
                            }
                        }
                    }
                }
            }"
        'This query uses this postman sample: https://interstellar-zodiac-223984.postman.co/workspace/My-Workspace~e1229d4d-0716-4512-8c82-8155033a6f8b/request/10748988-7127ee01-7ea5-49f1-9275-7ca542836b54?tab=body
        Dim fetchNames As String = "query{
        boards(ids: 3428362986){
            items_page{
                items{
                    name
                    id
                }
            }
        }
    }"

        'QUERIES ENDS HERE

        lblStatus.Text = "Fetching Accounts..."
        Dim queries As New List(Of String)
        'add all queries in this list
        queries.Add(fetchAccountQuery)
        queries.Add(fetchNames)
        'queries.Add(fetchTasksQuery)
        'queries.Add(fetchUKTaskQuery)

        ' Due to monday's update, fetchTasksQuery and fetchTaskQueryUK should have a different approach
        ' because they can only handle 25 items/page of tasks, and we need all of them pages.
        Try
            Dim deserializedResults As New List(Of Object)
            Dim phTaskObject As New Root
            Dim ukTaskObject As New Root
            Dim accountsListObject As New Root
            Dim isError As New Boolean
            Dim badQuery As New List(Of String)
            Console.WriteLine("Trying to fetch....")
            For retries = 1 To maxErrorCount
                deserializedResults.Clear()
                ToolStripProgressBar1.Maximum = maxErrorCount
                Dim goodQueryCounter As Integer = 0
                If retries <> maxErrorCount Then
                    ' Fetch PH tasks
                    Dim phTask = Await SendMondayRequestVersion2(fetchTasksQuery)
                    If phTask(0) = "error" Then
                        ToolStripProgressBar1.Increment(-10)
                        Exit For
                    Else
                        phTaskObject = JsonConvert.DeserializeObject(Of Root)(phTask(1))
                        ' Iterate through all groups in PH tasks
                        For Each group In phTaskObject.data.boards(0).groups
                            Console.WriteLine(group)
                            Dim currrIndex = phTaskObject.data.boards(0).groups.IndexOf(group)
                            'check if item page of that group contains a cursor
                            If group.items_page.cursor IsNot vbNullString Then
                                ' this items page still has a next page. So we query again.
                                Dim nextPageQuery = "query {
                                          next_items_page (limit: 100,cursor: """ + group.items_page.cursor + """) {
                                            cursor
                                            items {
                                              name
                                              group {
                                                id
                                              }
                                              subitems{
                                                name
                                              }
                                              column_values{
                                                column{
                                                    title
                                                }
                                                text
                                              }
                                            }
                                          }
                                        }
                                   "
                                Dim nextPageQueryResult = Await SendMondayRequestVersion2(nextPageQuery)
                                If nextPageQueryResult(0) = "error" Then
                                    'something went wrong.
                                    Console.WriteLine($"Something went wrong. Retrying... {retries + 1} / {maxErrorCount}")
                                    ToolStripProgressBar1.Increment(-10)
                                Else
                                    Dim nextPage = JsonConvert.DeserializeObject(Of Root)(nextPageQueryResult(1))
                                    ' add the new page to the list of string.
                                    For Each items In nextPage.data.next_items_page.items
                                        phTaskObject.data.boards(0).groups(currrIndex).items_page.items.Add(items)
                                        Console.WriteLine(allTasks)
                                    Next
                                End If
                            End If
                        Next
                        goodQueryCounter += 1
                    End If
                    ' Fetch UK tasks
                    Dim UKTasks = Await SendMondayRequestVersion2(fetchUKTaskQuery)
                    If UKTasks(0) = "error" Then
                        'error api call
                        ToolStripProgressBar1.Increment(-10)
                        Exit For
                    Else
                        'successful api call
                        ukTaskObject = JsonConvert.DeserializeObject(Of Root)(UKTasks(1))
                        'Iterate through all groups in UK tasks
                        For Each group In ukTaskObject.data.boards(0).groups
                            Dim currIndex = ukTaskObject.data.boards(0).groups.IndexOf(group)
                            'check if this group contains cursor.
                            If group.items_page.cursor IsNot vbNullString Then
                                ' this group contains a cursor, that means it has pending pages to be fetched.
                                Dim nextPageQuery = "query {
                                          next_items_page (limit: 100,cursor: """ + group.items_page.cursor + """) {
                                            cursor
                                            items {
                                              name
                                              group {
                                                id
                                              }
                                              subitems{
                                                name
                                              }
                                              column_values{
                                                column{
                                                    title
                                                }
                                                text
                                              }
                                            }
                                          }
                                        }
                                   "
                                Dim nextPageQueryResult = Await SendMondayRequestVersion2(nextPageQuery)
                                If nextPageQueryResult(0) = "error" Then
                                    'something went wrong.
                                    Console.WriteLine($"Something went wrong. Retrying... {retries + 1} / {maxErrorCount}")
                                    ToolStripProgressBar1.Increment(-10)
                                Else
                                    'deserialize json
                                    Dim nextPage = JsonConvert.DeserializeObject(Of Root)(nextPageQueryResult(1))
                                    'add each items to their respective groups
                                    For Each item In nextPage.data.next_items_page.items
                                        ukTaskObject.data.boards(0).groups(currIndex).items_page.items.Add(item)
                                    Next
                                End If
                            End If
                        Next
                        goodQueryCounter += 1
                    End If

                    ' Fetch Account Details
                    Dim accountsList = Await SendMondayRequestVersion2(fetchAccountQuery)
                    If accountsList(0) = "error" Then
                        'something went wrong.
                        ToolStripProgressBar1.Increment(-10)
                    Else
                        accountsListObject = JsonConvert.DeserializeObject(Of Root)(accountsList(1))
                        Dim firstCursor = accountsListObject.data.boards(0).items_page.cursor
                        If (firstCursor IsNot vbNullString) Then
                            'keep on requesting until we get the whole accounts list.
                            Dim stillHasCursor = True
                            Dim cursor = firstCursor
                            While stillHasCursor
                                Dim nextPageQuery = "
                                                query {
                                                  next_items_page (limit: 100, cursor: """ + cursor + """) {
                                                    cursor
                                                    items {
                                                      name
                                                      group {
                                                        id
                                                      }
                                                      subitems{
                                                        name
                                                      }
                                                      column_values{
                                                        column{
                                                            title
                                                        }
                                                        text
                                                      }
                                                    }
                                                  }
                                                }"
                                Dim nextPage = Await SendMondayRequestVersion2(nextPageQuery)
                                If nextPage(0) = "error" Then
                                    'something went wrong
                                    ToolStripProgressBar1.Increment(-10)
                                    Exit For
                                Else
                                    Dim nextPageObject = JsonConvert.DeserializeObject(Of Root)(nextPage(1))
                                    For Each account In nextPageObject.data.next_items_page.items
                                        accountsListObject.data.boards(0).items_page.items.Add(account)
                                    Next
                                    'check if nextPage still has account
                                    If nextPageObject.data.next_items_page.cursor IsNot vbNullString Then
                                        ' still has cursor
                                        cursor = nextPageObject.data.next_items_page.cursor
                                    Else
                                        'end loop
                                        stillHasCursor = False
                                    End If
                                End If
                            End While
                        End If
                        goodQueryCounter += 1
                    End If
                Else
                    'max retries are all used.
                    Throw New Exception("Could not fetch accounts.")
                    Exit Function
                End If

                If goodQueryCounter = 3 Then
                    Exit For
                End If
            Next
            accounts = accountsListObject
            allTasks = phTaskObject
            UKTasks = ukTaskObject
            'namesList = deserializedResults(1)
            'UKTasks = deserializedResults(3)
            'Console.WriteLine("UK Tasks: ", UKTasks)
            populateCB()
            ToolStripProgressBar1.Value = ToolStripProgressBar1.Maximum
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
        DisableAllControls()
        'timesinceLastUpdate()
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

        'this was temporarily added.
        'AddManualLogs.Show()

    End Sub

    Private Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignin.Click
        'fetchMondayTimer.Enabled = True
        'fetchMondayTimer.Interval = 7200000
        'fetchMondayTimer.Start()
        If checkAccountDetails(cbUsername.Text, tbPassword.Text, accounts) = True Then
            'Account detail matches
            MessageBox.Show($"Welcome back, {fFirstName}!", "Log In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Visible = False
            elapsedDutyHours.Start()
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
        Try

        Catch ex As Exception

        End Try
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
