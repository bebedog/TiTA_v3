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
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCurrentTime = New System.Windows.Forms.Button()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelTimeIn
        '
        Me.labelTimeIn.AutoSize = True
        Me.labelTimeIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelTimeIn.Location = New System.Drawing.Point(44, 143)
        Me.labelTimeIn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelTimeIn.Name = "labelTimeIn"
        Me.labelTimeIn.Size = New System.Drawing.Size(73, 20)
        Me.labelTimeIn.TabIndex = 0
        Me.labelTimeIn.Text = "Time In:"
        Me.labelTimeIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnTimeIn
        '
        Me.btnTimeIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTimeIn.Location = New System.Drawing.Point(168, 294)
        Me.btnTimeIn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTimeIn.Name = "btnTimeIn"
        Me.btnTimeIn.Size = New System.Drawing.Size(132, 35)
        Me.btnTimeIn.TabIndex = 2
        Me.btnTimeIn.Text = "Submit Time In"
        Me.btnTimeIn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(61, 219)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Task:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbProjectsList
        '
        Me.cbProjectsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProjectsList.FormattingEnabled = True
        Me.cbProjectsList.Location = New System.Drawing.Point(128, 213)
        Me.cbProjectsList.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbProjectsList.Name = "cbProjectsList"
        Me.cbProjectsList.Size = New System.Drawing.Size(368, 28)
        Me.cbProjectsList.TabIndex = 4
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker1.Location = New System.Drawing.Point(128, 139)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(184, 26)
        Me.DateTimePicker1.TabIndex = 5
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 361)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(2, 0, 21, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(545, 25)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolLabel1
        '
        Me.ToolLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolLabel1.Name = "ToolLabel1"
        Me.ToolLabel1.Size = New System.Drawing.Size(56, 20)
        Me.ToolLabel1.Text = "Status"
        '
        'cbSubtasks
        '
        Me.cbSubtasks.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSubtasks.FormattingEnabled = True
        Me.cbSubtasks.Location = New System.Drawing.Point(129, 253)
        Me.cbSubtasks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbSubtasks.Name = "cbSubtasks"
        Me.cbSubtasks.Size = New System.Drawing.Size(368, 28)
        Me.cbSubtasks.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 259)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Subtask:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(310, 294)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(132, 35)
        Me.btnBack.TabIndex = 9
        Me.btnBack.Text = "Go Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(88, 18)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(370, 95)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'cbFilter
        '
        Me.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Location = New System.Drawing.Point(128, 175)
        Me.cbFilter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(368, 28)
        Me.cbFilter.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(61, 181)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 20)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Filter: "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCurrentTime
        '
        Me.btnCurrentTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCurrentTime.Location = New System.Drawing.Point(320, 136)
        Me.btnCurrentTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCurrentTime.Name = "btnCurrentTime"
        Me.btnCurrentTime.Size = New System.Drawing.Size(176, 32)
        Me.btnCurrentTime.TabIndex = 13
        Me.btnCurrentTime.Text = "Use Current Time"
        Me.btnCurrentTime.UseVisualStyleBackColor = True
        '
        'ManualClockIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 386)
        Me.Controls.Add(Me.btnCurrentTime)
        Me.Controls.Add(Me.cbFilter)
        Me.Controls.Add(Me.Label3)
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
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
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
    Friend WithEvents cbFilter As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnCurrentTime As Button
End Class
