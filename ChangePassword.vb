Imports Newtonsoft.Json


Public Class ChangePassword

    Dim namesList
    Dim accounts
    Dim accountItemID

    Public Class Item
        Public Property name As String
        Public Property id As String
        Public Property column_values As ColumnValue()
    End Class

    Public Class Board
        Public Property items As Item()
    End Class

    Public Class Data
        Public Property boards As Board()
        Public Property change_simple_column_value As ChangeSimpleColumnValue
    End Class

    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Public Class ColumnValue
        Public Property title As String
        Public Property text As String
    End Class

    Public Class ChangeSimpleColumnValue
        Public Property id As String
    End Class

    Private Sub populateCB2(ByVal usernames As Root)
        For Each x In usernames.data.boards(0).items
            cbUsername2.Items.Add(x.name)
        Next
    End Sub

    Private Async Sub ChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.positionScreen()
        Me.Text = $"Lasermet TiTA v{Form1.titaVersion}"
        cbUsername2.AutoCompleteSource = AutoCompleteSource.ListItems
        'START OF QUERIES HERE
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
        'END OF QUERIES HERE
        Dim listOfQueries As New List(Of String)
        listOfQueries.Add(fetchAccountQuery)
        listOfQueries.Add(fetchNames)
        Dim listOfResponse As New List(Of Root)
        Try
            For Each queries In listOfQueries
                For retries = 0 To Form1.maxErrorCount
                    If retries <> Form1.maxErrorCount Then
                        Dim response As Object = Await Form1.SendMondayRequestVersion2(queries)
                        If response(0) = "error" Then
                            'response is error.
                        Else
                            'response is a success.
                            listOfResponse.Add(JsonConvert.DeserializeObject(Of Root)(response(1)))
                            Exit For
                        End If
                    Else
                        'max retries reached.
                        Throw New Exception("Error occured when fetching accounts and names.")
                    End If
                Next
            Next
            accounts = listOfResponse(0)
            namesList = listOfResponse(1)
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Form1.Close()
            End If
            Exit Sub
        End Try

        'START OLD CODE HERE
        'Try
        '    Dim result As String = Await Form1.SendMondayRequest(fetchNames)
        '    Dim result2 As String = Await Form1.SendMondayRequest(fetchAccountQuery)
        '    namesList = JsonConvert.DeserializeObject(Of Root)(result)
        '    accounts = JsonConvert.DeserializeObject(Of Root)(result2)
        'Catch ex As Exception
        '    Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        '    If result = DialogResult.Retry Then
        '        Application.Restart()
        '    Else
        '        Me.Close()
        '    End If
        '    Exit Sub
        'End Try
        'END OLD CODE HERE

        populateCB2(namesList)
    End Sub

    Public Sub positionScreen()
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

    Private Function getAccountItemID(ByVal accountList As Root, ByVal userName As String)
        For Each x In accountList.data.boards(0).items
            If x.name = userName Then
                accountItemID = x.id
                updatePassword(cbUsername2.Text, tbOldPassword.Text, tbNewPassword.Text, accountItemID)
            End If
        Next
    End Function

    Private Async Function updatePassword(ByVal userName As String, ByVal oldPassword As String, ByVal newPassword As String, ByVal accountID As String) As Task
        Dim changePW As String
        changePW =
                "mutation {
              change_simple_column_value(item_id:" + accountID + ", board_id:3428362986, column_id:""" + "text9" + """, value:""" + newPassword + """){
                id
              }
            }"
        Try
            For retries = 0 To Form1.maxErrorCount
                If retries <> Form1.maxErrorCount Then
                    Dim response As Object = Await Form1.SendMondayRequestVersion2(changePW)
                    If response(0) = "error" Then
                        'error
                        lblStatus.Text = $"Error occured when changing password. Retrying ({retries}/{Form1.maxErrorCount})"
                    Else
                        'success
                        Exit For
                    End If
                Else
                    'max retries reached.
                    Throw New Exception("Error when changing password.")
                End If
            Next
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Function
        End Try
        'START OF OLD CODE HERE
        'Try
        '    Dim changePWResult As String = Await Form1.SendMondayRequest(changePW)
        '    'Console.WriteLine($"Username: {userName}, Old Password: {oldPassword}, New PW: {newPassword}, Item ID: {accountID}")
        '    Console.WriteLine(changePW)
        'Catch ex As Exception

        '    Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        '    If result = DialogResult.Retry Then
        '        Application.Restart()
        '    Else
        '        Me.Close()
        '    End If
        '    Exit Function

        'End Try
        'END OF OLD CODE HERE
    End Function

    Private Sub btnChangePass_Click(sender As Object, e As EventArgs) Handles btnChangePass.Click
        If String.IsNullOrEmpty(tbNewPassword.Text) = False And String.IsNullOrWhiteSpace(tbNewPassword.Text) = False Then
            If Form1.checkAccountDetails(cbUsername2.Text, tbOldPassword.Text, Form1.accounts) = True Then
                'Account detail matches
                getAccountItemID(accounts, cbUsername2.Text)
                Dim updateMsg As DialogResult = MessageBox.Show($"Password Changed for {Form1.fFirstName} {Form1.fSurname}", "Password Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If updateMsg = DialogResult.OK Then
                    Application.Restart()
                End If
            Else
                MessageBox.Show("Incorrect Password.")
                'Account Detail don't match.
            End If
        Else MessageBox.Show("No New Password Entered", "Please enter your new password", MessageBoxButtons.OK, MessageBoxIcon.Question)
        End If
    End Sub

    Private Function validateInputs() As Boolean
        For Each c As Control In Me.Controls
            If c.Text = "" Then
                'user forgot to input something!
                c.Select()
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Me.Close()
        Form1.Show()
    End Sub
End Class