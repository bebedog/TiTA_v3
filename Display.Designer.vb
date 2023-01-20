<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Display
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Display))
        Me.lblSubtasks = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblProjectNumber = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSwitch = New System.Windows.Forms.Button()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.lblTimeIn = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblTask = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblUserWelcome = New System.Windows.Forms.Label()
        Me.lblDepartment1 = New System.Windows.Forms.Label()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblSubtasks
        '
        Me.lblSubtasks.AutoSize = True
        Me.lblSubtasks.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtasks.ForeColor = System.Drawing.Color.Red
        Me.lblSubtasks.Location = New System.Drawing.Point(122, 109)
        Me.lblSubtasks.Name = "lblSubtasks"
        Me.lblSubtasks.Size = New System.Drawing.Size(122, 20)
        Me.lblSubtasks.TabIndex = 23
        Me.lblSubtasks.Text = "3-Point Dunks"
        Me.lblSubtasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(44, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Subtask:"
        '
        'lblProjectNumber
        '
        Me.lblProjectNumber.AutoSize = True
        Me.lblProjectNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectNumber.ForeColor = System.Drawing.Color.Red
        Me.lblProjectNumber.Location = New System.Drawing.Point(122, 89)
        Me.lblProjectNumber.Name = "lblProjectNumber"
        Me.lblProjectNumber.Size = New System.Drawing.Size(89, 20)
        Me.lblProjectNumber.TabIndex = 24
        Me.lblProjectNumber.Text = "69696969"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 20)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Project No.:"
        '
        'btnSwitch
        '
        Me.btnSwitch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSwitch.Location = New System.Drawing.Point(12, 134)
        Me.btnSwitch.Name = "btnSwitch"
        Me.btnSwitch.Size = New System.Drawing.Size(186, 63)
        Me.btnSwitch.TabIndex = 15
        Me.btnSwitch.Text = "Switch Task"
        Me.btnSwitch.UseVisualStyleBackColor = True
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(12, 29)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(94, 20)
        Me.lblDepartment.TabIndex = 20
        Me.lblDepartment.Text = "Department"
        '
        'lblTimeIn
        '
        Me.lblTimeIn.AutoSize = True
        Me.lblTimeIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimeIn.ForeColor = System.Drawing.Color.Red
        Me.lblTimeIn.Location = New System.Drawing.Point(122, 69)
        Me.lblTimeIn.Name = "lblTimeIn"
        Me.lblTimeIn.Size = New System.Drawing.Size(124, 20)
        Me.lblTimeIn.TabIndex = 19
        Me.lblTimeIn.Text = "TASK TIME IN"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 20)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Time Started:"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(208, 134)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(186, 63)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Minimize to Tray"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lblTask
        '
        Me.lblTask.AutoSize = True
        Me.lblTask.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTask.ForeColor = System.Drawing.Color.Red
        Me.lblTask.Location = New System.Drawing.Point(122, 49)
        Me.lblTask.Name = "lblTask"
        Me.lblTask.Size = New System.Drawing.Size(109, 20)
        Me.lblTask.TabIndex = 17
        Me.lblTask.Text = "TASK NAME"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 20)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Current Task:"
        '
        'lblUserWelcome
        '
        Me.lblUserWelcome.AutoSize = True
        Me.lblUserWelcome.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserWelcome.Location = New System.Drawing.Point(12, 9)
        Me.lblUserWelcome.Name = "lblUserWelcome"
        Me.lblUserWelcome.Size = New System.Drawing.Size(190, 20)
        Me.lblUserWelcome.TabIndex = 13
        Me.lblUserWelcome.Text = "Welcome, LeBron James."
        '
        'lblDepartment1
        '
        Me.lblDepartment1.AutoSize = True
        Me.lblDepartment1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment1.ForeColor = System.Drawing.Color.Red
        Me.lblDepartment1.Location = New System.Drawing.Point(122, 29)
        Me.lblDepartment1.Name = "lblDepartment1"
        Me.lblDepartment1.Size = New System.Drawing.Size(128, 20)
        Me.lblDepartment1.TabIndex = 25
        Me.lblDepartment1.Text = "DEPARTMENT"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Timer1
        '
        '
        'Display
        '
        Me.AcceptButton = Me.btnSwitch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(416, 212)
        Me.Controls.Add(Me.lblDepartment1)
        Me.Controls.Add(Me.lblSubtasks)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblProjectNumber)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnSwitch)
        Me.Controls.Add(Me.lblDepartment)
        Me.Controls.Add(Me.lblTimeIn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.lblTask)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblUserWelcome)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Display"
        Me.Text = "Display"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSubtasks As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblProjectNumber As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnSwitch As Button
    Friend WithEvents lblDepartment As Label
    Friend WithEvents lblTimeIn As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents lblTask As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblUserWelcome As Label
    Friend WithEvents lblDepartment1 As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents Timer1 As Timer
End Class
