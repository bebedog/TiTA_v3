Module helpers

    Public Sub disableAllControls2(form As Form)

        For Each c As Control In form.Controls
            c.Enabled = False
        Next

    End Sub

    Public Sub enableAllControls2(form As Form)

        For Each c As Control In form.Controls
            c.Enabled = True
        Next

    End Sub

End Module
