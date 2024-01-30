<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class adminTools
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAddTask = New System.Windows.Forms.Button()
        Me.btnAddLogs = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAddTask
        '
        Me.btnAddTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddTask.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddTask.Location = New System.Drawing.Point(12, 12)
        Me.btnAddTask.Name = "btnAddTask"
        Me.btnAddTask.Size = New System.Drawing.Size(171, 59)
        Me.btnAddTask.TabIndex = 0
        Me.btnAddTask.Text = "Add Task"
        Me.btnAddTask.UseVisualStyleBackColor = True
        '
        'btnAddLogs
        '
        Me.btnAddLogs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddLogs.Enabled = False
        Me.btnAddLogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddLogs.Location = New System.Drawing.Point(12, 77)
        Me.btnAddLogs.Name = "btnAddLogs"
        Me.btnAddLogs.Size = New System.Drawing.Size(171, 59)
        Me.btnAddLogs.TabIndex = 0
        Me.btnAddLogs.Text = "Add Logs"
        Me.btnAddLogs.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Enabled = False
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(12, 142)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(171, 59)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "Coming Soon"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(12, 207)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(171, 59)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Exit"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'adminTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(195, 274)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnAddLogs)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.btnAddTask)
        Me.Name = "adminTools"
        Me.Text = "Admin Tools"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAddTask As Button
    Friend WithEvents btnAddLogs As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
End Class
