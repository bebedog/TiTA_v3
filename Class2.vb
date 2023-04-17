Public Class Class2
    Public Class CreateItem
        Public Property id As String
    End Class

    Public Class Data
        Public Property create_item As CreateItem
    End Class

    Public Class AddNewTaskResponse
        Public Property data As Data
        Public Property account_id As Integer
    End Class

    Public Class myPayload
        Public Property text As String ''project number
        Public Property dup__of_budget_expense As Integer ''Budget Hours
        Public Property text6 As String 'ERD
        Public Property text64 As String 'EN
        Public Property text79 As String 'MRD
        Public Property text0 As String 'SD
        Public Property text_1 As String 'SBM
    End Class
End Class
