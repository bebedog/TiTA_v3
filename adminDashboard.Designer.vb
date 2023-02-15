<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class adminDashboard
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
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.DailyPanel = New MetroFramework.Controls.MetroPanel()
        Me.dtpDaySelect = New MetroFramework.Controls.MetroDateTime()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.cbLogOption = New MetroFramework.Controls.MetroComboBox()
        Me.WeeklyPanel = New MetroFramework.Controls.MetroPanel()
        Me.dtpWeekSelect = New MetroFramework.Controls.MetroDateTime()
        Me.lblDayInAWeek = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.CustomPanel = New MetroFramework.Controls.MetroPanel()
        Me.btnApplyCustomFilter = New MetroFramework.Controls.MetroButton()
        Me.MetroLabel6 = New MetroFramework.Controls.MetroLabel()
        Me.dtpTo = New MetroFramework.Controls.MetroDateTime()
        Me.MetroLabel5 = New MetroFramework.Controls.MetroLabel()
        Me.dtpFrom = New MetroFramework.Controls.MetroDateTime()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.MetroButton2 = New MetroFramework.Controls.MetroButton()
        Me.pBar = New MetroFramework.Controls.MetroProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetroTabPage2.SuspendLayout()
        Me.DailyPanel.SuspendLayout()
        Me.WeeklyPanel.SuspendLayout()
        Me.CustomPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea3.AlignmentOrientation = CType((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical Or System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal), System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)
        ChartArea3.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend3)
        Me.Chart1.Location = New System.Drawing.Point(6, 6)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones
        Me.Chart1.Size = New System.Drawing.Size(874, 254)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DataGridView1.Location = New System.Drawing.Point(3, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(877, 558)
        Me.DataGridView1.TabIndex = 1
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.FontSize = MetroFramework.MetroTabControlSize.Small
        Me.MetroTabControl1.HotTrack = True
        Me.MetroTabControl1.ItemSize = New System.Drawing.Size(200, 34)
        Me.MetroTabControl1.Location = New System.Drawing.Point(345, 12)
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Size = New System.Drawing.Size(891, 609)
        Me.MetroTabControl1.TabIndex = 2
        Me.MetroTabControl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.MetroTabControl1.UseSelectable = True
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.Chart2)
        Me.MetroTabPage1.Controls.Add(Me.Chart1)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.HorizontalScrollbarSize = 10
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(883, 567)
        Me.MetroTabPage1.TabIndex = 0
        Me.MetroTabPage1.Text = "Chart View"
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.VerticalScrollbarSize = 10
        '
        'Chart2
        '
        Me.Chart2.BackColor = System.Drawing.Color.LightCoral
        ChartArea4.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend4)
        Me.Chart2.Location = New System.Drawing.Point(6, 264)
        Me.Chart2.Name = "Chart2"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.Chart2.Series.Add(Series2)
        Me.Chart2.Size = New System.Drawing.Size(874, 300)
        Me.Chart2.TabIndex = 2
        Me.Chart2.Text = "Chart2"
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.DataGridView1)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.HorizontalScrollbarSize = 10
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(883, 567)
        Me.MetroTabPage2.TabIndex = 1
        Me.MetroTabPage2.Text = "Table View"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.VerticalScrollbarSize = 10
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Location = New System.Drawing.Point(117, 56)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(88, 19)
        Me.MetroLabel1.TabIndex = 4
        Me.MetroLabel1.Text = "Logs By Date:"
        '
        'DailyPanel
        '
        Me.DailyPanel.Controls.Add(Me.dtpDaySelect)
        Me.DailyPanel.Controls.Add(Me.MetroLabel2)
        Me.DailyPanel.HorizontalScrollbarBarColor = True
        Me.DailyPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.DailyPanel.HorizontalScrollbarSize = 10
        Me.DailyPanel.Location = New System.Drawing.Point(12, 85)
        Me.DailyPanel.Name = "DailyPanel"
        Me.DailyPanel.Size = New System.Drawing.Size(327, 50)
        Me.DailyPanel.TabIndex = 5
        Me.DailyPanel.VerticalScrollbarBarColor = True
        Me.DailyPanel.VerticalScrollbarHighlightOnWheel = False
        Me.DailyPanel.VerticalScrollbarSize = 10
        '
        'dtpDaySelect
        '
        Me.dtpDaySelect.Location = New System.Drawing.Point(130, 9)
        Me.dtpDaySelect.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtpDaySelect.Name = "dtpDaySelect"
        Me.dtpDaySelect.Size = New System.Drawing.Size(194, 29)
        Me.dtpDaySelect.TabIndex = 3
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(62, 14)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(72, 19)
        Me.MetroLabel2.TabIndex = 2
        Me.MetroLabel2.Text = "Select Day:"
        '
        'cbLogOption
        '
        Me.cbLogOption.FormattingEnabled = True
        Me.cbLogOption.ItemHeight = 23
        Me.cbLogOption.Location = New System.Drawing.Point(211, 50)
        Me.cbLogOption.Name = "cbLogOption"
        Me.cbLogOption.Size = New System.Drawing.Size(128, 29)
        Me.cbLogOption.TabIndex = 6
        Me.cbLogOption.UseSelectable = True
        '
        'WeeklyPanel
        '
        Me.WeeklyPanel.Controls.Add(Me.dtpWeekSelect)
        Me.WeeklyPanel.Controls.Add(Me.lblDayInAWeek)
        Me.WeeklyPanel.Controls.Add(Me.MetroLabel3)
        Me.WeeklyPanel.HorizontalScrollbarBarColor = True
        Me.WeeklyPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.WeeklyPanel.HorizontalScrollbarSize = 10
        Me.WeeklyPanel.Location = New System.Drawing.Point(12, 141)
        Me.WeeklyPanel.Name = "WeeklyPanel"
        Me.WeeklyPanel.Size = New System.Drawing.Size(331, 116)
        Me.WeeklyPanel.TabIndex = 5
        Me.WeeklyPanel.VerticalScrollbarBarColor = True
        Me.WeeklyPanel.VerticalScrollbarHighlightOnWheel = False
        Me.WeeklyPanel.VerticalScrollbarSize = 10
        '
        'dtpWeekSelect
        '
        Me.dtpWeekSelect.Location = New System.Drawing.Point(130, 37)
        Me.dtpWeekSelect.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtpWeekSelect.Name = "dtpWeekSelect"
        Me.dtpWeekSelect.Size = New System.Drawing.Size(194, 29)
        Me.dtpWeekSelect.TabIndex = 3
        '
        'lblDayInAWeek
        '
        Me.lblDayInAWeek.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.lblDayInAWeek.Location = New System.Drawing.Point(119, 69)
        Me.lblDayInAWeek.Name = "lblDayInAWeek"
        Me.lblDayInAWeek.Size = New System.Drawing.Size(205, 30)
        Me.lblDayInAWeek.TabIndex = 2
        Me.lblDayInAWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.Location = New System.Drawing.Point(159, 15)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(137, 19)
        Me.MetroLabel3.TabIndex = 2
        Me.MetroLabel3.Text = "Select a day in a week"
        '
        'CustomPanel
        '
        Me.CustomPanel.Controls.Add(Me.btnApplyCustomFilter)
        Me.CustomPanel.Controls.Add(Me.MetroLabel6)
        Me.CustomPanel.Controls.Add(Me.dtpTo)
        Me.CustomPanel.Controls.Add(Me.MetroLabel5)
        Me.CustomPanel.Controls.Add(Me.dtpFrom)
        Me.CustomPanel.Controls.Add(Me.MetroLabel4)
        Me.CustomPanel.HorizontalScrollbarBarColor = True
        Me.CustomPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.CustomPanel.HorizontalScrollbarSize = 10
        Me.CustomPanel.Location = New System.Drawing.Point(12, 263)
        Me.CustomPanel.Name = "CustomPanel"
        Me.CustomPanel.Size = New System.Drawing.Size(331, 140)
        Me.CustomPanel.TabIndex = 7
        Me.CustomPanel.VerticalScrollbarBarColor = True
        Me.CustomPanel.VerticalScrollbarHighlightOnWheel = False
        Me.CustomPanel.VerticalScrollbarSize = 10
        '
        'btnApplyCustomFilter
        '
        Me.btnApplyCustomFilter.Location = New System.Drawing.Point(234, 103)
        Me.btnApplyCustomFilter.Name = "btnApplyCustomFilter"
        Me.btnApplyCustomFilter.Size = New System.Drawing.Size(90, 23)
        Me.btnApplyCustomFilter.TabIndex = 5
        Me.btnApplyCustomFilter.Text = "Apply Filter"
        Me.btnApplyCustomFilter.UseSelectable = True
        '
        'MetroLabel6
        '
        Me.MetroLabel6.AutoSize = True
        Me.MetroLabel6.Location = New System.Drawing.Point(158, 11)
        Me.MetroLabel6.Name = "MetroLabel6"
        Me.MetroLabel6.Size = New System.Drawing.Size(126, 19)
        Me.MetroLabel6.TabIndex = 4
        Me.MetroLabel6.Text = "Custom Date Range"
        '
        'dtpTo
        '
        Me.dtpTo.Location = New System.Drawing.Point(119, 68)
        Me.dtpTo.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(205, 29)
        Me.dtpTo.TabIndex = 3
        '
        'MetroLabel5
        '
        Me.MetroLabel5.AutoSize = True
        Me.MetroLabel5.Location = New System.Drawing.Point(91, 73)
        Me.MetroLabel5.Name = "MetroLabel5"
        Me.MetroLabel5.Size = New System.Drawing.Size(22, 19)
        Me.MetroLabel5.TabIndex = 2
        Me.MetroLabel5.Text = "To"
        '
        'dtpFrom
        '
        Me.dtpFrom.Location = New System.Drawing.Point(119, 33)
        Me.dtpFrom.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(205, 29)
        Me.dtpFrom.TabIndex = 3
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.Location = New System.Drawing.Point(72, 38)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(41, 19)
        Me.MetroLabel4.TabIndex = 2
        Me.MetroLabel4.Text = "From"
        '
        'MetroButton2
        '
        Me.MetroButton2.Location = New System.Drawing.Point(184, 462)
        Me.MetroButton2.Name = "MetroButton2"
        Me.MetroButton2.Size = New System.Drawing.Size(154, 107)
        Me.MetroButton2.TabIndex = 8
        Me.MetroButton2.Text = "Try Add Query"
        Me.MetroButton2.UseSelectable = True
        '
        'pBar
        '
        Me.pBar.Location = New System.Drawing.Point(12, 598)
        Me.pBar.Name = "pBar"
        Me.pBar.Size = New System.Drawing.Size(324, 23)
        Me.pBar.TabIndex = 9
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(12, 572)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(324, 23)
        Me.lblStatus.TabIndex = 10
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(103, 546)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'adminDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1248, 626)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.pBar)
        Me.Controls.Add(Me.MetroButton2)
        Me.Controls.Add(Me.CustomPanel)
        Me.Controls.Add(Me.cbLogOption)
        Me.Controls.Add(Me.WeeklyPanel)
        Me.Controls.Add(Me.DailyPanel)
        Me.Controls.Add(Me.MetroLabel1)
        Me.Controls.Add(Me.MetroTabControl1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "adminDashboard"
        Me.Text = "Admin"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetroTabPage2.ResumeLayout(False)
        Me.DailyPanel.ResumeLayout(False)
        Me.DailyPanel.PerformLayout()
        Me.WeeklyPanel.ResumeLayout(False)
        Me.WeeklyPanel.PerformLayout()
        Me.CustomPanel.ResumeLayout(False)
        Me.CustomPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents MetroTabControl1 As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MetroTabPage1 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroTabPage2 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents DailyPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents cbLogOption As MetroFramework.Controls.MetroComboBox
    Friend WithEvents dtpDaySelect As MetroFramework.Controls.MetroDateTime
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents WeeklyPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents dtpWeekSelect As MetroFramework.Controls.MetroDateTime
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents lblDayInAWeek As MetroFramework.Controls.MetroLabel
    Friend WithEvents CustomPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents MetroLabel6 As MetroFramework.Controls.MetroLabel
    Friend WithEvents dtpTo As MetroFramework.Controls.MetroDateTime
    Friend WithEvents MetroLabel5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents dtpFrom As MetroFramework.Controls.MetroDateTime
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents btnApplyCustomFilter As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroButton2 As MetroFramework.Controls.MetroButton
    Friend WithEvents pBar As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents lblStatus As Label
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents Button1 As Button
    Friend WithEvents Timer1 As Timer
End Class
