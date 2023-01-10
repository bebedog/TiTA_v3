<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManualClockIn
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
        Me.labelTimeIn = New System.Windows.Forms.Label()
        Me.btnTimeIn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbProjectsList = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'labelTimeIn
        '
        Me.labelTimeIn.AutoSize = True
        Me.labelTimeIn.Location = New System.Drawing.Point(63, 83)
        Me.labelTimeIn.Name = "labelTimeIn"
        Me.labelTimeIn.Size = New System.Drawing.Size(45, 13)
        Me.labelTimeIn.TabIndex = 0
        Me.labelTimeIn.Text = "Time In:"
        '
        'btnTimeIn
        '
        Me.btnTimeIn.Location = New System.Drawing.Point(211, 145)
        Me.btnTimeIn.Name = "btnTimeIn"
        Me.btnTimeIn.Size = New System.Drawing.Size(75, 23)
        Me.btnTimeIn.TabIndex = 2
        Me.btnTimeIn.Text = "Submit Time In"
        Me.btnTimeIn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(74, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Task:"
        '
        'cbProjectsList
        '
        Me.cbProjectsList.FormattingEnabled = True
        Me.cbProjectsList.Location = New System.Drawing.Point(114, 108)
        Me.cbProjectsList.Name = "cbProjectsList"
        Me.cbProjectsList.Size = New System.Drawing.Size(301, 21)
        Me.cbProjectsList.TabIndex = 4
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker1.Location = New System.Drawing.Point(114, 77)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(301, 20)
        Me.DateTimePicker1.TabIndex = 5
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 202)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(481, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolLabel1
        '
        Me.ToolLabel1.Name = "ToolLabel1"
        Me.ToolLabel1.Size = New System.Drawing.Size(39, 17)
        Me.ToolLabel1.Text = "Status"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(358, 154)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Label2"
        '
        'ManualClockIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 224)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.cbProjectsList)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnTimeIn)
        Me.Controls.Add(Me.labelTimeIn)
        Me.Name = "ManualClockIn"
        Me.Text = "ManualClockIn"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelTimeIn As Label
    Friend WithEvents btnTimeIn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cbProjectsList As ComboBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolLabel1 As ToolStripStatusLabel
    Friend WithEvents Label2 As Label
End Class
