Public Class listboxPractice

    Private myArray As New List(Of String)
    Private mynewArray As New List(Of String)

    Private Sub hideResult()

    End Sub
    Private Sub listboxPractice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myArray.Add("Hello - 22819")
        myArray.Add("World - 12837")
        myArray.Add("Foo - 189273")
        myArray.Add("Bar - 17237")
        ComboBox1.Items.AddRange(myArray.ToArray)



    End Sub

    Private Sub ComboBox1_TextUpdate(sender As Object, e As EventArgs) Handles ComboBox1.TextUpdate
        ComboBox1.Items.Clear()
        mynewArray.Clear()
        For Each item In myArray
            If item.ToLower.Contains(ComboBox1.Text) Then
                mynewArray.Add(item)
            End If
        Next
        ComboBox1.Items.AddRange(mynewArray.ToArray)
        ComboBox1.SelectionStart = ComboBox1.Text.Length
        ComboBox1.DroppedDown = True
    End Sub
End Class