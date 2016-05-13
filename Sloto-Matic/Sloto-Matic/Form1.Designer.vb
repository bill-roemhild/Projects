<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.btnStartStop = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.SpinInfo = New System.Windows.Forms.TabPage()
        Me.cbSpinLocation = New System.Windows.Forms.CheckBox()
        Me.txtClickSpeed = New System.Windows.Forms.TextBox()
        Me.lblSpinClickSpeed = New System.Windows.Forms.Label()
        Me.lblSpinButtonLocation = New System.Windows.Forms.Label()
        Me.txtSpinLocationY = New System.Windows.Forms.TextBox()
        Me.txtSpinLocationX = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.tmrCheckScreen = New System.Windows.Forms.Timer(Me.components)
        Me.btnConfigure = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SpinInfo.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStartStop
        '
        Me.btnStartStop.Location = New System.Drawing.Point(12, 27)
        Me.btnStartStop.Name = "btnStartStop"
        Me.btnStartStop.Size = New System.Drawing.Size(191, 46)
        Me.btnStartStop.TabIndex = 0
        Me.btnStartStop.Text = "Start/Stop"
        Me.btnStartStop.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(421, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ExitToolStripMenuItem, Me.DebugToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'DebugToolStripMenuItem
        '
        Me.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem"
        Me.DebugToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.DebugToolStripMenuItem.Text = "Debug"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.SpinInfo)
        Me.TabControl1.Location = New System.Drawing.Point(2, 79)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(418, 176)
        Me.TabControl1.TabIndex = 5
        '
        'SpinInfo
        '
        Me.SpinInfo.Controls.Add(Me.cbSpinLocation)
        Me.SpinInfo.Controls.Add(Me.txtClickSpeed)
        Me.SpinInfo.Controls.Add(Me.lblSpinClickSpeed)
        Me.SpinInfo.Controls.Add(Me.lblSpinButtonLocation)
        Me.SpinInfo.Controls.Add(Me.txtSpinLocationY)
        Me.SpinInfo.Controls.Add(Me.txtSpinLocationX)
        Me.SpinInfo.Location = New System.Drawing.Point(4, 22)
        Me.SpinInfo.Name = "SpinInfo"
        Me.SpinInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.SpinInfo.Size = New System.Drawing.Size(410, 150)
        Me.SpinInfo.TabIndex = 0
        Me.SpinInfo.Text = "Spin Info"
        Me.SpinInfo.UseVisualStyleBackColor = True
        '
        'cbSpinLocation
        '
        Me.cbSpinLocation.AutoSize = True
        Me.cbSpinLocation.Location = New System.Drawing.Point(250, 9)
        Me.cbSpinLocation.Name = "cbSpinLocation"
        Me.cbSpinLocation.Size = New System.Drawing.Size(15, 14)
        Me.cbSpinLocation.TabIndex = 5
        Me.cbSpinLocation.UseVisualStyleBackColor = True
        '
        'txtClickSpeed
        '
        Me.txtClickSpeed.Location = New System.Drawing.Point(118, 32)
        Me.txtClickSpeed.Name = "txtClickSpeed"
        Me.txtClickSpeed.Size = New System.Drawing.Size(54, 20)
        Me.txtClickSpeed.TabIndex = 4
        '
        'lblSpinClickSpeed
        '
        Me.lblSpinClickSpeed.AutoSize = True
        Me.lblSpinClickSpeed.Location = New System.Drawing.Point(6, 35)
        Me.lblSpinClickSpeed.Name = "lblSpinClickSpeed"
        Me.lblSpinClickSpeed.Size = New System.Drawing.Size(88, 13)
        Me.lblSpinClickSpeed.TabIndex = 3
        Me.lblSpinClickSpeed.Text = "Spin Click Speed"
        '
        'lblSpinButtonLocation
        '
        Me.lblSpinButtonLocation.AutoSize = True
        Me.lblSpinButtonLocation.Location = New System.Drawing.Point(6, 9)
        Me.lblSpinButtonLocation.Name = "lblSpinButtonLocation"
        Me.lblSpinButtonLocation.Size = New System.Drawing.Size(106, 13)
        Me.lblSpinButtonLocation.TabIndex = 2
        Me.lblSpinButtonLocation.Text = "Spin Button Location"
        '
        'txtSpinLocationY
        '
        Me.txtSpinLocationY.Enabled = False
        Me.txtSpinLocationY.Location = New System.Drawing.Point(178, 6)
        Me.txtSpinLocationY.Name = "txtSpinLocationY"
        Me.txtSpinLocationY.Size = New System.Drawing.Size(54, 20)
        Me.txtSpinLocationY.TabIndex = 1
        '
        'txtSpinLocationX
        '
        Me.txtSpinLocationX.Enabled = False
        Me.txtSpinLocationX.Location = New System.Drawing.Point(118, 6)
        Me.txtSpinLocationX.Name = "txtSpinLocationX"
        Me.txtSpinLocationX.Size = New System.Drawing.Size(54, 20)
        Me.txtSpinLocationX.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'tmrCheckScreen
        '
        '
        'btnConfigure
        '
        Me.btnConfigure.Location = New System.Drawing.Point(218, 27)
        Me.btnConfigure.Name = "btnConfigure"
        Me.btnConfigure.Size = New System.Drawing.Size(191, 46)
        Me.btnConfigure.TabIndex = 6
        Me.btnConfigure.Text = "Configure"
        Me.btnConfigure.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 26)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(2, 257)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1171, 192)
        Me.DataGridView1.TabIndex = 7
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(424, 27)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(749, 228)
        Me.TextBox1.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 79)
        Me.ControlBox = False
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnConfigure)
        Me.Controls.Add(Me.btnStartStop)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "box"
        Me.TopMost = True
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.SpinInfo.ResumeLayout(False)
        Me.SpinInfo.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStartStop As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As TabControl
    Public WithEvents SpinInfo As TabPage
    Friend WithEvents txtSpinLocationY As TextBox
    Friend WithEvents txtSpinLocationX As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents txtClickSpeed As TextBox
    Friend WithEvents lblSpinClickSpeed As Label
    Friend WithEvents lblSpinButtonLocation As Label
    Friend WithEvents cbSpinLocation As CheckBox
    Friend WithEvents tmrCheckScreen As Timer
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnConfigure As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DebugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox1 As TextBox
End Class
