<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class addTask
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbProjectType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbProjectName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbProjectNo = New System.Windows.Forms.TextBox()
        Me.btnAuto = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbBudgetHours = New System.Windows.Forms.TextBox()
        Me.chckTags = New System.Windows.Forms.GroupBox()
        Me.chckSBM = New System.Windows.Forms.CheckBox()
        Me.chckSD = New System.Windows.Forms.CheckBox()
        Me.chckEN = New System.Windows.Forms.CheckBox()
        Me.chckMRD = New System.Windows.Forms.CheckBox()
        Me.chckERD = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chckTags.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AllowDrop = True
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(446, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "In this window, the manager can add new task to Cebu Projects List board on Monda" &
    "y.com by supplying all the required fields (*) with details."
        '
        'cbProjectType
        '
        Me.cbProjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProjectType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProjectType.FormattingEnabled = True
        Me.cbProjectType.Location = New System.Drawing.Point(108, 64)
        Me.cbProjectType.Name = "cbProjectType"
        Me.cbProjectType.Size = New System.Drawing.Size(195, 24)
        Me.cbProjectType.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(59, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "*Type:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "*Task Name:"
        '
        'tbProjectName
        '
        Me.tbProjectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProjectName.Location = New System.Drawing.Point(108, 94)
        Me.tbProjectName.Name = "tbProjectName"
        Me.tbProjectName.Size = New System.Drawing.Size(195, 22)
        Me.tbProjectName.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(25, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 16)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "*Project No.:"
        '
        'tbProjectNo
        '
        Me.tbProjectNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProjectNo.Location = New System.Drawing.Point(108, 122)
        Me.tbProjectNo.Name = "tbProjectNo"
        Me.tbProjectNo.Size = New System.Drawing.Size(121, 22)
        Me.tbProjectNo.TabIndex = 3
        '
        'btnAuto
        '
        Me.btnAuto.Location = New System.Drawing.Point(241, 122)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(62, 23)
        Me.btnAuto.TabIndex = 4
        Me.btnAuto.Text = "Auto"
        Me.btnAuto.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 16)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Budget Hours"
        '
        'tbBudgetHours
        '
        Me.tbBudgetHours.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBudgetHours.Location = New System.Drawing.Point(108, 150)
        Me.tbBudgetHours.Name = "tbBudgetHours"
        Me.tbBudgetHours.Size = New System.Drawing.Size(136, 22)
        Me.tbBudgetHours.TabIndex = 3
        '
        'chckTags
        '
        Me.chckTags.Controls.Add(Me.chckSBM)
        Me.chckTags.Controls.Add(Me.chckSD)
        Me.chckTags.Controls.Add(Me.chckEN)
        Me.chckTags.Controls.Add(Me.chckMRD)
        Me.chckTags.Controls.Add(Me.chckERD)
        Me.chckTags.Location = New System.Drawing.Point(309, 45)
        Me.chckTags.Name = "chckTags"
        Me.chckTags.Size = New System.Drawing.Size(149, 127)
        Me.chckTags.TabIndex = 5
        Me.chckTags.TabStop = False
        Me.chckTags.Text = "Tags"
        '
        'chckSBM
        '
        Me.chckSBM.AutoSize = True
        Me.chckSBM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckSBM.Location = New System.Drawing.Point(6, 99)
        Me.chckSBM.Name = "chckSBM"
        Me.chckSBM.Size = New System.Drawing.Size(126, 20)
        Me.chckSBM.TabIndex = 0
        Me.chckSBM.Text = "Small Batch Mfg."
        Me.chckSBM.UseVisualStyleBackColor = True
        '
        'chckSD
        '
        Me.chckSD.AutoSize = True
        Me.chckSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckSD.Location = New System.Drawing.Point(6, 79)
        Me.chckSD.Name = "chckSD"
        Me.chckSD.Size = New System.Drawing.Size(118, 20)
        Me.chckSD.TabIndex = 0
        Me.chckSD.Text = "System Design"
        Me.chckSD.UseVisualStyleBackColor = True
        '
        'chckEN
        '
        Me.chckEN.AutoSize = True
        Me.chckEN.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckEN.Location = New System.Drawing.Point(6, 39)
        Me.chckEN.Name = "chckEN"
        Me.chckEN.Size = New System.Drawing.Size(94, 20)
        Me.chckEN.TabIndex = 0
        Me.chckEN.Text = "Enclosures"
        Me.chckEN.UseVisualStyleBackColor = True
        '
        'chckMRD
        '
        Me.chckMRD.AutoSize = True
        Me.chckMRD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckMRD.Location = New System.Drawing.Point(6, 59)
        Me.chckMRD.Name = "chckMRD"
        Me.chckMRD.Size = New System.Drawing.Size(126, 20)
        Me.chckMRD.TabIndex = 0
        Me.chckMRD.Text = "Mechanical RnD"
        Me.chckMRD.UseVisualStyleBackColor = True
        '
        'chckERD
        '
        Me.chckERD.AutoSize = True
        Me.chckERD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckERD.Location = New System.Drawing.Point(6, 19)
        Me.chckERD.Name = "chckERD"
        Me.chckERD.Size = New System.Drawing.Size(123, 20)
        Me.chckERD.TabIndex = 0
        Me.chckERD.Text = "Electronics RnD"
        Me.chckERD.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(37, 178)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 39)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Add Task"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(179, 178)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(113, 39)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Clear Form"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(321, 178)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(113, 39)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Exit"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(250, 153)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 16)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Hours"
        '
        'addTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(470, 237)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.chckTags)
        Me.Controls.Add(Me.btnAuto)
        Me.Controls.Add(Me.tbBudgetHours)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbProjectNo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbProjectName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbProjectType)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "addTask"
        Me.Text = "Add New Task"
        Me.chckTags.ResumeLayout(False)
        Me.chckTags.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cbProjectType As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tbProjectName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents tbProjectNo As TextBox
    Friend WithEvents btnAuto As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents tbBudgetHours As TextBox
    Friend WithEvents chckTags As GroupBox
    Friend WithEvents chckEN As CheckBox
    Friend WithEvents chckERD As CheckBox
    Friend WithEvents chckSBM As CheckBox
    Friend WithEvents chckSD As CheckBox
    Friend WithEvents chckMRD As CheckBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label6 As Label
End Class
