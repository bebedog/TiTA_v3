Public Class DisplayAndSwitch
    Private Sub populateTasksComboBox()
        'populate comboboxes first
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each tasks In groups.items
                cbTasks.Items.Add(tasks.name)
            Next
        Next
        cbTasks.SelectedIndex = 0
    End Sub
    Private Sub updateSubTasksComboBox()
        cbSubTasks.Items.Clear()
        For Each groups In Form1.allTasks.data.boards(0).groups
            For Each tasks In groups.items
                If tasks.name = cbTasks.SelectedItem Then
                    For Each subtasks In tasks.subitems
                        cbSubTasks.Items.Add(subtasks.name)
                    Next
                End If
            Next
        Next
        cbSubTasks.SelectedIndex = 0

    End Sub
    Private Sub DisplayAndSwitch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Populate tasks combobox
        populateTasksComboBox()
        'set combo box to autocomplete
        cbTasks.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub cbTasks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTasks.SelectedIndexChanged
        updateSubTasksComboBox()
    End Sub

    Private Sub btnSwitch_Click(sender As Object, e As EventArgs) Handles btnSwitch.Click
        'Mark the previous log as done, and put in timeout column and tita version
        Dim markPreviousLogAsDone As String =
            ""
    End Sub
End Class