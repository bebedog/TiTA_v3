Imports RestSharp
Imports Newtonsoft.Json
Public Class Form1
    Public watch As Stopwatch

    Public queryTimeOut As Integer = 15000
    Public titaVersion As String = "3.0"
    Public allTasks As Root
    Public accounts
    Dim namesList

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
    'Class Declaration for Serialization (Changing ColumnValues for Previous Log)
    Public Class ColumnValuesToChange
        Public Property text_1 As String ' START_Surname
        Public Property dup__of_time_in As String 'Timeout
        Public Property text64 As String 'TiTA Version
    End Class
    'End of Class Declaration for Serialization (Changing ColumnValues for Previous Log)

    'Classes Declaration
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
    End Class
    Public Class Group
        Public Property items As Item()
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
            Return response.Content
        Else
            Return False
        End If

    End Function
    Public Function checkAccountDetails(ByVal surname As String, ByVal password As String, ByVal accounts As Root) As Boolean
        '0 - First Name
        '1 - Monday ID
        '2 - Password
        '3 - Department

        For Each x In accounts.data.boards(0).items
            If x.name = surname Then
                'account found.
                If x.column_values(2).text = password Then
                    'account verified
                    'save all account details in a global variable.
                    fSurname = x.name
                    fFirstName = x.column_values(0).text
                    mondayID = x.column_values(1).text
                    department = x.column_values(3).text
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
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Me.positionLoginScreen()
        watch = Stopwatch.StartNew()
        watch.Stop()
        Me.Text = $"Lasermet TiTA v{titaVersion}"
        cbUsername.AutoCompleteSource = AutoCompleteSource.ListItems
        Dim fetchTasksQuery As String =
            "query{
                boards(ids: 2718204773){
                    groups(ids:[""topics"",""group_title"", ""new_group1823""]){
                        items{
                            name
                            subitems{
                                name
                            }
                            column_values(ids:""text""){
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

        lblStatus.Text = "Fetching Accounts..."
        DisableAllControls()

        Try
            Dim result As String = Await SendMondayRequest(fetchAccountQuery)
            Dim result2 As String = Await SendMondayRequest(fetchNames)
            Dim result3 As String = Await SendMondayRequest(fetchTasksQuery)
            accounts = JsonConvert.DeserializeObject(Of Root)(result)
            namesList = JsonConvert.DeserializeObject(Of Root)(result2)
            allTasks = JsonConvert.DeserializeObject(Of Root)(result3)
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Sub
        End Try
        populateCB(namesList)
        If TiTA_v3.My.Settings.recentUser <> "" Then
            cbUsername.SelectedItem = TiTA_v3.My.Settings.recentUser
        End If
        EnableAllControls()
        lblStatus.Text = "Accounts Fetched."
    End Sub
    Private Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignin.Click
        If checkAccountDetails(cbUsername.Text, tbPassword.Text, accounts) = True Then
            'Account detail matches
            MessageBox.Show($"Welcome back, {fFirstName}!", "Log In Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Hide()
            Dashboard1.Show()
        Else
            MessageBox.Show("Incorrect Password.")
            'Account Detail don't match.
        End If
        TiTA_v3.My.Settings.recentUser = cbUsername.Text
        My.Settings.Save()
    End Sub
    'Initiated after Manual Time In.
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dashboard1.elapsedTime = Dashboard1.elapsedTime - 1000
    End Sub
    Private Sub btnChangePW_Click(sender As Object, e As EventArgs) Handles btnChangePW.Click
        ChangePassword.Show()
        Me.Hide()
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
End Class
