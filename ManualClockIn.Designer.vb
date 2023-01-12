<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManualClockIn
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.labelTimeIn = New System.Windows.Forms.Label()
        Me.btnTimeIn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbProjectsList = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cbSubtasks = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelTimeIn
        '
        Me.labelTimeIn.AutoSize = True
        Me.labelTimeIn.Location = New System.Drawing.Point(57, 84)
        Me.labelTimeIn.Name = "labelTimeIn"
        Me.labelTimeIn.Size = New System.Drawing.Size(45, 13)
        Me.labelTimeIn.TabIndex = 0
        Me.labelTimeIn.Text = "Time In:"
        '
        'btnTimeIn
        '
        Me.btnTimeIn.Location = New System.Drawing.Point(168, 166)
        Me.btnTimeIn.Name = "btnTimeIn"
        Me.btnTimeIn.Size = New System.Drawing.Size(75, 23)
        Me.btnTimeIn.TabIndex = 2
        Me.btnTimeIn.Text = "Submit Time In"
        Me.btnTimeIn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(68, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Task:"
        '
        'cbProjectsList
        '
        Me.cbProjectsList.FormattingEnabled = True
        Me.cbProjectsList.Location = New System.Drawing.Point(108, 110)
        Me.cbProjectsList.Name = "cbProjectsList"
        Me.cbProjectsList.Size = New System.Drawing.Size(301, 21)
        Me.cbProjectsList.TabIndex = 4
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker1.Location = New System.Drawing.Point(108, 81)
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
        'cbSubtasks
        '
        Me.cbSubtasks.FormattingEnabled = True
        Me.cbSubtasks.Location = New System.Drawing.Point(108, 139)
        Me.cbSubtasks.Name = "cbSubtasks"
        Me.cbSubtasks.Size = New System.Drawing.Size(301, 21)
        Me.cbSubtasks.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 143)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Subtask:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(249, 166)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 9
        Me.btnBack.Text = "Go Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(108, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(301, 62)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'ManualClockIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 224)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbSubtasks)
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
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cbSubtasks As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
