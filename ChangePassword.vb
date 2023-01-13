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
        Me.CenterToParent()
        Me.Text = $"Lasermet TiTA v{Form1.titaVersion}"
        cbUsername2.AutoCompleteSource = AutoCompleteSource.ListItems
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


        Try
            Dim result As String = Await Form1.SendMondayRequest(fetchNames)
            Dim result2 As String = Await Form1.SendMondayRequest(fetchAccountQuery)
            namesList = JsonConvert.DeserializeObject(Of Root)(result)
            accounts = JsonConvert.DeserializeObject(Of Root)(result2)
        Catch ex As Exception
            Dim result As DialogResult = MessageBox.Show(ex.Message + Environment.NewLine + "Would you like to retry?", "Oops, something went wrong!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            If result = DialogResult.Retry Then
                Application.Restart()
            Else
                Me.Close()
            End If
            Exit Sub
        End Try
        populateCB2(namesList)
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
            Await Form1.SendMondayRequest(changePW)
            Dim changePWResult As String = Await Form1.SendMondayRequest(changePW)
            'Console.WriteLine($"Username: {userName}, Old Password: {oldPassword}, New PW: {newPassword}, Item ID: {accountID}")
            Console.WriteLine(changePW)

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

    Private Sub btnChangePass_Click(sender As Object, e As EventArgs) Handles btnChangePass.Click
        If String.IsNullOrEmpty(tbNewPassword.Text) = False And String.IsNullOrWhiteSpace(tbNewPassword.Text) = False Then

            If Form1.checkAccountDetails(cbUsername2.Text, tbOldPassword.Text, Form1.accounts) = True Then
                'Account detail matches
                getAccountItemID(accounts, cbUsername2.Text)
                MessageBox.Show($"Password Changed for {Form1.fFirstName} {Form1.fSurname}", "Password Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Incorrect Password.")
                'Account Detail don't match.
            End If

        Else MessageBox.Show("No New Password Entered", "Please enter your new password", MessageBoxButtons.OK, MessageBoxIcon.Question)

        End If

    End Sub
End Class