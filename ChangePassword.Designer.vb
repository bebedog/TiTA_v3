<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangePassword))
        Me.labelUsername = New System.Windows.Forms.Label()
        Me.labelOldPass = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbOldPassword = New System.Windows.Forms.TextBox()
        Me.tbNewPassword = New System.Windows.Forms.TextBox()
        Me.cbUsername2 = New System.Windows.Forms.ComboBox()
        Me.btnChangePass = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnGoBack = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelUsername
        '
        Me.labelUsername.AutoSize = True
        Me.labelUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelUsername.Location = New System.Drawing.Point(56, 168)
        Me.labelUsername.Name = "labelUsername"
        Me.labelUsername.Size = New System.Drawing.Size(87, 20)
        Me.labelUsername.TabIndex = 0
        Me.labelUsername.Text = "Username:"
        '
        'labelOldPass
        '
        Me.labelOldPass.AutoSize = True
        Me.labelOldPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelOldPass.Location = New System.Drawing.Point(32, 208)
        Me.labelOldPass.Name = "labelOldPass"
        Me.labelOldPass.Size = New System.Drawing.Size(110, 20)
        Me.labelOldPass.TabIndex = 1
        Me.labelOldPass.Text = "Old Password:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "New Password:"
        '
        'tbOldPassword
        '
        Me.tbOldPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.tbOldPassword.Location = New System.Drawing.Point(144, 208)
        Me.tbOldPassword.Name = "tbOldPassword"
        Me.tbOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbOldPassword.Size = New System.Drawing.Size(204, 26)
        Me.tbOldPassword.TabIndex = 7
        '
        'tbNewPassword
        '
        Me.tbNewPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.tbNewPassword.Location = New System.Drawing.Point(144, 248)
        Me.tbNewPassword.Name = "tbNewPassword"
        Me.tbNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbNewPassword.Size = New System.Drawing.Size(204, 26)
        Me.tbNewPassword.TabIndex = 8
        '
        'cbUsername2
        '
        Me.cbUsername2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbUsername2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUsername2.FormattingEnabled = True
        Me.cbUsername2.Location = New System.Drawing.Point(144, 168)
        Me.cbUsername2.Name = "cbUsername2"
        Me.cbUsername2.Size = New System.Drawing.Size(204, 28)
        Me.cbUsername2.TabIndex = 9
        '
        'btnChangePass
        '
        Me.btnChangePass.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btnChangePass.Location = New System.Drawing.Point(32, 288)
        Me.btnChangePass.Name = "btnChangePass"
        Me.btnChangePass.Size = New System.Drawing.Size(151, 37)
        Me.btnChangePass.TabIndex = 11
        Me.btnChangePass.Text = "Change Password"
        Me.btnChangePass.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Location = New System.Drawing.Point(40, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(288, 120)
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'btnGoBack
        '
        Me.btnGoBack.CausesValidation = False
        Me.btnGoBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btnGoBack.Location = New System.Drawing.Point(192, 288)
        Me.btnGoBack.Name = "btnGoBack"
        Me.btnGoBack.Size = New System.Drawing.Size(151, 37)
        Me.btnGoBack.TabIndex = 13
        Me.btnGoBack.Text = "Cancel"
        Me.btnGoBack.UseVisualStyleBackColor = True
        '
        'ChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(369, 341)
        Me.Controls.Add(Me.btnGoBack)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnChangePass)
        Me.Controls.Add(Me.cbUsername2)
        Me.Controls.Add(Me.tbNewPassword)
        Me.Controls.Add(Me.tbOldPassword)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.labelOldPass)
        Me.Controls.Add(Me.labelUsername)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChangePassword"
        Me.Text = "Form2"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelUsername As Label
    Friend WithEvents labelOldPass As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tbOldPassword As TextBox
    Friend WithEvents tbNewPassword As TextBox
    Friend WithEvents cbUsername2 As ComboBox
    Friend WithEvents btnChangePass As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnGoBack As Button
End Class
