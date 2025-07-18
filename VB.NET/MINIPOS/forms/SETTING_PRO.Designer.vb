<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SETTING_PRO
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnAdd = New ReaLTaiizor.Controls.HopeButton()
        Me.btnDelete = New ReaLTaiizor.Controls.HopeButton()
        Me.btnUpdate = New ReaLTaiizor.Controls.HopeButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxTables = New ReaLTaiizor.Controls.PoisonComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.HopeButton1 = New ReaLTaiizor.Controls.HopeButton()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 72)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(786, 452)
        Me.DataGridView1.TabIndex = 0
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnAdd.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAdd.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAdd.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAdd.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.btnAdd.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnAdd.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnAdd.Location = New System.Drawing.Point(686, 6)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAdd.Size = New System.Drawing.Size(77, 45)
        Me.btnAdd.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "جديد"
        Me.btnAdd.TextColor = System.Drawing.Color.White
        Me.btnAdd.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnDelete.ButtonType = ReaLTaiizor.Util.HopeButtonType.Danger
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelete.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDelete.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.btnDelete.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnDelete.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnDelete.Location = New System.Drawing.Point(603, 6)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDelete.Size = New System.Drawing.Size(77, 45)
        Me.btnDelete.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "حذف"
        Me.btnDelete.TextColor = System.Drawing.Color.White
        Me.btnDelete.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnUpdate.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUpdate.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnUpdate.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUpdate.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.btnUpdate.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnUpdate.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnUpdate.Location = New System.Drawing.Point(520, 6)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnUpdate.Size = New System.Drawing.Size(77, 45)
        Me.btnUpdate.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnUpdate.TabIndex = 5
        Me.btnUpdate.Text = "تحديث"
        Me.btnUpdate.TextColor = System.Drawing.Color.White
        Me.btnUpdate.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ComboBoxTables)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(786, 72)
        Me.Panel1.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(721, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "الجدول:"
        '
        'ComboBoxTables
        '
        Me.ComboBoxTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTables.FormattingEnabled = True
        Me.ComboBoxTables.ItemHeight = 23
        Me.ComboBoxTables.Location = New System.Drawing.Point(526, 21)
        Me.ComboBoxTables.Name = "ComboBoxTables"
        Me.ComboBoxTables.Size = New System.Drawing.Size(189, 29)
        Me.ComboBoxTables.TabIndex = 0
        Me.ComboBoxTables.UseSelectable = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.HopeButton1)
        Me.Panel2.Controls.Add(Me.btnUpdate)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 524)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(786, 57)
        Me.Panel2.TabIndex = 7
        '
        'HopeButton1
        '
        Me.HopeButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HopeButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Info
        Me.HopeButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.HopeButton1.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.HopeButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton1.Location = New System.Drawing.Point(12, 6)
        Me.HopeButton1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.HopeButton1.Name = "HopeButton1"
        Me.HopeButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Size = New System.Drawing.Size(77, 45)
        Me.HopeButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton1.TabIndex = 6
        Me.HopeButton1.Text = "اغلاق"
        Me.HopeButton1.TextColor = System.Drawing.Color.White
        Me.HopeButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'SETTING_PRO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(786, 581)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Name = "SETTING_PRO"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SETTING_PRO"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnAdd As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents btnDelete As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents btnUpdate As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ComboBoxTables As ReaLTaiizor.Controls.PoisonComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents HopeButton1 As ReaLTaiizor.Controls.HopeButton
End Class
