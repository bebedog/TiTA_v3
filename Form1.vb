Imports RestSharp
Imports Newtonsoft.Json
Public Class Form1
    Public titaVersion As String = "3.0"
    Dim accounts
    Dim namesList
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
        options.MaxTimeout = -1
        Dim client = New RestClient(options)
        Dim request = New RestRequest()
        request.Method = Method.Post
        request.AddHeader("Authorization", "eyJhbGciOiJIUzI1NiJ9.eyJ0aWQiOjE1MjU2NzQ3OCwidWlkIjoxNTA5MzQwNywiaWFkIjoiMjAyMi0wMy0yNVQwMTo0Njo1My4wMDBaIiwicGVyIjoibWU6d3JpdGUiLCJhY3RpZCI6NjYxMjMxMCwicmduIjoidXNlMSJ9.F1TqwLS-QsuM8Ss3UcgskbNFUIer1dfwfoLyPMq1pbc")
        request.AddQueryParameter("query", myQuery)
        Dim response = New RestResponse
        response = Await client.ExecuteAsync(request)
        Return response.Content
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
                    MessageBox.Show("Success!")
                Else
                    MessageBox.Show("Incorrect Password.")
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
        Dim result As String = Await SendMondayRequest(fetchAccountQuery)
        Dim result2 As String = Await SendMondayRequest(fetchNames)
        accounts = JsonConvert.DeserializeObject(Of Root)(result)
        namesList = JsonConvert.DeserializeObject(Of Root)(result2)
        populateCB(namesList)
        EnableAllControls()
        lblStatus.Text = "Accounts Fetched."
        Console.Write(result2)
    End Sub

    Private Sub btnSignin_Click(sender As Object, e As EventArgs) Handles btnSignin.Click
        checkAccountDetails(cbUsername.Text, tbPassword.Text, accounts)
    End Sub
End Class
