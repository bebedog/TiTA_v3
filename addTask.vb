Imports Newtonsoft.Json
Imports TiTA_v3.Class2

Public Class addTask
    Public Class Column
        Public Property title As String
    End Class

    Public Class ColumnValue
        Public Property column As Column
        Public Property text As String
    End Class

    Public Class Item
        Public Property column_values As ColumnValue()
    End Class

    Public Class ItemsPage
        Public Property cursor As String
        Public Property items As IList(Of Item)
    End Class

    Public Class Group
        Public Property items_page As ItemsPage
    End Class

    Public Class Board
        Public Property groups As Group()
    End Class
    Public Class NextItemsPage
        Public Property cursor As Object
        Public Property items As IList(Of Item)
    End Class

    Public Class Data
        Public Property boards As Board()
        Public Property next_items_page As NextItemsPage
    End Class

    Public Class Root
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Public taskList As New Root

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
        'disable all control
        disableAllControl()

        ' query monday boards and fetch all projects
        ' the code below employs loops that will make sure to take all items found in the board.
        Dim myQuery = "query{
                            boards(ids: 2718204773){
                                groups(ids: [""topics"", ""group_title"", ""new_group84073""]){
                                    items_page(limit: 50){
                                        cursor
                                        items{
                                            column_values(ids: ""text""){
                                                column{
                                                    title
                                                }
                                                text
                                            }
                                        }
                                    }
                                }
                            }
                        }"
        Dim result = Await Form1.SendMondayRequestVersion2(myQuery)
        If result(0) = "error" Then
            'something wrong with the result
            MessageBox.Show("Something went wrong when fetching for project numbers!", "Oops, something went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            ' deserialize the resulting object
            Dim resultObject = JsonConvert.DeserializeObject(Of Root)(result(1))

            ' iterate through group
            For Each group In resultObject.data.boards(0).groups
                ' check if cursor is blank.
                If String.IsNullOrWhiteSpace(group.items_page.cursor) Then
                    'no cursor, so no need to request more.
                Else
                    'cursor is present. loop query until no more cursor.
                    Dim isCursor = True
                    Dim firstCursor = group.items_page.cursor
                    While isCursor
                        Dim query = "query{
                                        next_items_page(cursor: """ + firstCursor + """){
                                            cursor
                                                items{
                                                    name
                                                    column_values(ids: [""text""]){
                                                        text
                                                    }
                                                }
                                        }
                                    }"
                        Dim nextPageResult = Await Form1.SendMondayRequestVersion2(query)
                        If nextPageResult(0) = "success" Then
                            Dim nextPageObject = JsonConvert.DeserializeObject(Of Root)(nextPageResult(1))
                            'iterate through this new page, and add it to taskList
                            For Each item In nextPageObject.data.next_items_page.items
                                group.items_page.items.Add(item)
                            Next

                            If String.IsNullOrWhiteSpace(nextPageObject.data.next_items_page.cursor) Then
                                'no more cursor. end loop!
                                isCursor = False
                            Else
                                'replace with new cursor
                                firstCursor = nextPageObject.data.next_items_page.cursor
                            End If
                        End If
                    End While
                End If
            Next
            'save to global variable.
            taskList = resultObject
        End If

        ' put all project number found in the search on a string
        Dim projectNumbers As New List(Of String)
        For Each groups In taskList.data.boards(0).groups
            For Each item In groups.items_page.items
                projectNumbers.Add(item.column_values(0).text)
            Next
        Next

        Console.WriteLine(projectNumbers)

        Dim filtered As List(Of String) = projectNumbers.FindAll(Function(p) p.StartsWith("PH20"))
        ' The new project number format is: PHYYYY-XXXX

        Dim lastNumbers As New List(Of Integer)
        For Each projectNo In filtered
            lastNumbers.Add(Convert.ToInt32(projectNo.Split("-")(1)))
        Next

        lastNumbers.Sort(Function(x, y) y.CompareTo(x))


        Console.WriteLine(filtered)
        tbProjectNo.Text = "PH" + DateTime.Now().ToString("yyyy") + "-" + (lastNumbers(0) + 1).ToString()

        enableAllControls()

        ' 



        'Dim myQuery = "        query{
        '    boards(ids:2718204773){
        '        groups(ids: ""topics""){
        '            items{
        '                column_values(ids:""text""){
        '                    text
        '                }
        '            }
        '        }
        '        }
        '    }"
        'Dim result As Object = Await Form1.SendMondayRequestVersion2(myQuery)
        'Dim myInteger As New List(Of Integer)
        'If result(0) = "success" Then
        '    projectNumbers = JsonConvert.DeserializeObject(Of Root)(result(1))
        '    For Each items In projectNumbers.data.boards(0).groups(0).items
        '        Dim myProjectNumber As String() = items.column_values(0).text.Split("-")
        '        If myProjectNumber.Length > 1 Then
        '            myInteger.Add(Int32.Parse(myProjectNumber(1)))
        '        End If
        '    Next
        '    myInteger.Sort(Function(x, y)
        '                       Return x.CompareTo(y)
        '                   End Function)
        '    Dim currentDate = Date.Now
        '    Dim myOutput As String
        '    If myInteger(myInteger.Count - 1).ToString.Count = 2 Then
        '        myOutput = "PH" + currentDate.ToString("yyyy") + "-0" + (myInteger(myInteger.Count - 1) + 1).ToString
        '    Else
        '        myOutput = "PH" + currentDate.ToString("yyyy") + "-0" + (myInteger(myInteger.Count - 1) + 1).ToString
        '    End If
        '    tbProjectNo.Text = myOutput
        'End If
        'enableAllControls()
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
            'tbProjectName.Clear()
            'tbProjectNo.Clear()
            'tbProjectNo.Select()
            enableAllControls()
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
                If String.IsNullOrWhiteSpace(tbBudgetHours.Text) Then
                    'do nothing
                Else
                    payload.dup__of_budget_expense = Convert.ToInt32(tbBudgetHours.Text)
                End If
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
            ElseIf TypeOf control Is ComboBox Then
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