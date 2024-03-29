﻿Imports Newtonsoft.Json
Imports TiTA_v3.Class2

Public Class addTask
    Public Class ColumnValue
        Public Property text As String
    End Class

    Public Class Item
        Public Property column_values As ColumnValue()
    End Class

    Public Class Group
        Public Property items As Item()
    End Class

    Public Class Board
        Public Property groups As Group()
    End Class

    Public Class Data
        Public Property boards As Board()
    End Class

    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Private projectNumbers As New Root
    Private myAddTaskResponse As New AddNewTaskResponse
    Private payload As New myPayload
    Private Sub addTask_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Dim types As String() = {"R&D", "Jobs"}
        cbProjectType.Items.AddRange(types)
        cbProjectType.SelectedIndex = 0
        positionScreen()
        AssignValidation(tbBudgetHours, ValidationType.Only_Numbers)
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

    Private Sub cbProjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProjectType.SelectedIndexChanged
        For Each c As Control In Me.Controls
            If TypeOf c Is TextBox Then
                c.Text = ""
            End If

            If TypeOf c Is CheckBox Then
                DirectCast(c, CheckBox).Checked = False
            End If
        Next


        If cbProjectType.Text = "Jobs" Then
            tbProjectNo.Size = cbProjectType.Size
            btnAuto.Visible = False
            btnAuto.Enabled = False
        Else
            tbProjectNo.Size = New Point(121, 22)
            btnAuto.Visible = True
            btnAuto.Enabled = True
        End If
    End Sub

    Private Async Function btnAuto_ClickAsync(sender As Object, e As EventArgs) As Task Handles btnAuto.Click
        disableAllControl()
        Dim myQuery = "        query{
            boards(ids:2718204773){
                groups(ids: ""topics""){
                    items{
                        column_values(ids:""text""){
                            text
                        }
                    }
                }
                }
            }"
        Dim result As Object = Await Form1.SendMondayRequestVersion2(myQuery)
        Dim myInteger As New List(Of Integer)
        If result(0) = "success" Then
            projectNumbers = JsonConvert.DeserializeObject(Of Root)(result(1))
            For Each items In projectNumbers.data.boards(0).groups(0).items
                Dim myProjectNumber As String() = items.column_values(0).text.Split("-")
                If myProjectNumber.Length > 1 Then
                    myInteger.Add(Int32.Parse(myProjectNumber(1)))
                End If
            Next
            myInteger.Sort(Function(x, y)
                               Return x.CompareTo(y)
                           End Function)
            Dim currentDate = Date.Now
            Dim myOutput As String
            If myInteger(myInteger.Count - 1).ToString.Count = 2 Then
                myOutput = "PH" + currentDate.ToString("yyyy") + "-0" + (myInteger(myInteger.Count - 1) + 1).ToString
            Else
                myOutput = "PH" + currentDate.ToString("yyyy") + "-0" + (myInteger(myInteger.Count - 1) + 1).ToString
            End If
            tbProjectNo.Text = myOutput
        End If
        enableAllControls()
    End Function

    Private Sub disableAllControl()
        For Each c As Control In Me.Controls
            c.Enabled = False
        Next
    End Sub
    Private Sub enableAllControls()
        For Each c As Control In Me.Controls
            c.Enabled = True
        Next
    End Sub

    Private Async Function Button2_ClickAsync(sender As Object, e As EventArgs) As Task Handles Button2.Click
        disableAllControl()
        'Check first if all required parameters has a value.
        Dim isChanged = False
        For Each checkBox As Control In chckTags.Controls
            If DirectCast(checkBox, CheckBox).Checked = True Then
                isChanged = True
            End If
        Next

        If tbProjectName.Text = "" Or tbProjectName.Text = "" Or isChanged = False Then
            MessageBox.Show("One or more required fields are left blank.", "Oops, something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbProjectName.Clear()
            tbProjectNo.Clear()
            tbProjectNo.Select()
        Else
            Console.WriteLine("Adding new task...")
            Dim myQuery = ""
            If cbProjectType.SelectedItem = "R&D" Then
                myQuery = "mutation{
    create_item(board_id: 2718204773, group_id: ""topics"", item_name: """ + tbProjectName.Text + """){
        id
    }
}"
            Else
                myQuery = "mutation{
    create_item(board_id: 2718204773, group_id: ""group_title"", item_name: """ + tbProjectName.Text + """){
        id
    }
}"
            End If

            Try
                While True
                    Dim result = Await Form1.SendMondayRequestVersion2(myQuery)
                    If result(0) = "error" Then
                        Console.WriteLine("Something went wrong.")
                    Else
                        myAddTaskResponse = JsonConvert.DeserializeObject(Of AddNewTaskResponse)(result(1))
                        Exit While
                    End If
                End While

                'add all the values for the payload.
                Dim myMondayId = myAddTaskResponse.data.create_item.id
                payload.text = tbProjectNo.Text
                payload.dup__of_budget_expense = tbBudgetHours.Text
                If chckEN.Checked = True Then
                    payload.text64 = "x"
                End If

                If chckERD.Checked = True Then
                    payload.text6 = "x"
                End If

                If chckMRD.Checked = True Then
                    payload.text79 = "x"
                End If

                If chckSBM.Checked = True Then
                    payload.text_1 = "x"
                End If

                If chckSD.Checked = True Then
                    payload.text0 = "x"
                End If

                Dim myJSONSerializerSettings As New JsonSerializerSettings
                myJSONSerializerSettings.NullValueHandling = NullValueHandling.Ignore
                Dim mySerializedPayload = JsonConvert.SerializeObject(payload, Formatting.None, myJSONSerializerSettings)
                Dim formattedJSON = mySerializedPayload.Replace("""", "\""")
                Dim changeColumnValueQuery = "mutation{
    change_multiple_column_values(item_id: " + myMondayId + ", board_id: 2718204773, column_values: "" " + formattedJSON + " ""){id}
}"


                While True
                    Dim myResponseObject = Await Form1.SendMondayRequestVersion2(changeColumnValueQuery)
                    If myResponseObject(0) = "error" Then
                        Console.WriteLine("Retrying..")
                    Else
                        Dim dlgResult =
                        MessageBox.Show("A new project was added." + Environment.NewLine +
                                        "Project Name: " + tbProjectName.Text + Environment.NewLine +
                                        "Project Number: " + tbProjectNo.Text + Environment.NewLine +
                                        "Would you like to add another task?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        If dlgResult = DialogResult.Yes Then
                            'Yes
                            tbBudgetHours.Clear()
                            tbProjectName.Clear()
                            tbProjectNo.Clear()
                            For Each checkBox As Control In chckTags.Controls
                                DirectCast(checkBox, CheckBox).Checked = False
                            Next
                            tbProjectName.Select()
                        Else
                            'No.
                            adminTools.Show()
                            Me.Close()
                        End If
                        Exit While
                    End If
                End While

                'Console.WriteLine(mySerializedPayload)



            Catch ex As Exception
                MessageBox.Show("Exception thrown!" + ex.Message, "Oops, something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                enableAllControls()
            End Try
        End If
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each control As Control In Me.Controls
            If TypeOf control Is GroupBox Then
                For Each checkBox As Control In control.Controls
                    DirectCast(checkBox, CheckBox).Checked = False
                Next
            ElseIf TypeOf control Is ComBoBox Then
                DirectCast(control, ComboBox).SelectedIndex = 0
            ElseIf TypeOf control Is TextBox Then
                DirectCast(control, TextBox).Clear()
            End If
        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        adminTools.Show()
        Me.Close()
    End Sub
End Class