Imports RestSharp
Imports Newtonsoft.Json
Public Class Form1
    Public titaVersion As String = "3.0"
    Dim accounts
    Dim namesList

    Public fSurname As String
    Public fFirstName As String
    Public mondayID As String
    Public department As String

    'Classes Declaration
    Public Class ColumnValue
        Public Property title As String
        Public Property text As String
    End Class
    Public Class Item
        Public Property name As String
        Public Property column_values As ColumnValue()
    End Class
    Public Class Board
        Public Property items As Item()
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
        options.MaxTimeout = 6000
        Dim client = New RestClient(options)
        Dim request = New RestRequest()
        request.Timeout = 5000
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
    End Sub
    Public Sub EnableAllControls()
        tbPassword.Enabled = True
        cbUsername.Enabled = True
        btnSignin.Enabled = True
    End Sub
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        Me.Text = $"Lasermet TiTA v{titaVersion}"
        Dim fetchAccountQuery As String =
            "query{
                boards(ids:3428362986){
                    items{
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
            accounts = JsonConvert.DeserializeObject(Of Root)(result)
            namesList = JsonConvert.DeserializeObject(Of Root)(result2)
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
            MessageBox.Show("Success!")
            Me.Hide()
            Dashboard1.Show()
        Else
            MessageBox.Show("Incorrect Password.")
            'Account Detail don't match.
        End If
        TiTA_v3.My.Settings.recentUser = cbUsername.Text
        My.Settings.Save()
    End Sub
End Class
