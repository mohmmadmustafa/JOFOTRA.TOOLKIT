<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class return_invoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(return_invoice))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SimpleButton1 = New ReaLTaiizor.Controls.HopeButton()
        Me.BarCodeControl1 = New System.Windows.Forms.TextBox()
        Me.LabelControl5 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.HopeButton1 = New ReaLTaiizor.Controls.HopeButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.checkbox1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.InvoiceTool1 = New jofotaratoolkit.InvoiceTool()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.SimpleButton1)
        Me.Panel1.Controls.Add(Me.BarCodeControl1)
        Me.Panel1.Controls.Add(Me.LabelControl5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(998, 84)
        Me.Panel1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.Yellow
        Me.Label4.Font = New System.Drawing.Font("Cairo", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(12, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(323, 34)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "رقم الفاتوره:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Cairo", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(341, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(241, 34)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "رقم الفاتوره:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Yellow
        Me.Label2.Font = New System.Drawing.Font("Cairo", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(588, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(203, 34)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "رقم الفاتوره:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Cairo", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(796, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(199, 34)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "رقم الفاتوره:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.SimpleButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.SimpleButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SimpleButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.SimpleButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SimpleButton1.Font = New System.Drawing.Font("Cairo", 12.0!)
        Me.SimpleButton1.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.SimpleButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.SimpleButton1.Location = New System.Drawing.Point(725, 9)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SimpleButton1.Size = New System.Drawing.Size(71, 27)
        Me.SimpleButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.SimpleButton1.TabIndex = 8
        Me.SimpleButton1.Text = "عرض"
        Me.SimpleButton1.TextColor = System.Drawing.Color.White
        Me.SimpleButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'BarCodeControl1
        '
        Me.BarCodeControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BarCodeControl1.Location = New System.Drawing.Point(802, 9)
        Me.BarCodeControl1.Name = "BarCodeControl1"
        Me.BarCodeControl1.Size = New System.Drawing.Size(100, 27)
        Me.BarCodeControl1.TabIndex = 7
        '
        'LabelControl5
        '
        Me.LabelControl5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl5.AutoSize = True
        Me.LabelControl5.Location = New System.Drawing.Point(908, 12)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(65, 20)
        Me.LabelControl5.TabIndex = 6
        Me.LabelControl5.Text = "رقم الفاتوره:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.HopeButton1)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 391)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(998, 41)
        Me.Panel2.TabIndex = 1
        '
        'HopeButton1
        '
        Me.HopeButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.HopeButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Enabled = False
        Me.HopeButton1.Font = New System.Drawing.Font("Cairo", 12.0!)
        Me.HopeButton1.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.HopeButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton1.Location = New System.Drawing.Point(12, 8)
        Me.HopeButton1.Name = "HopeButton1"
        Me.HopeButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Size = New System.Drawing.Size(71, 27)
        Me.HopeButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton1.TabIndex = 17
        Me.HopeButton1.Text = "ارجاع"
        Me.HopeButton1.TextColor = System.Drawing.Color.White
        Me.HopeButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(563, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 30)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "سبب الارجاع"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(323, 10)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(234, 27)
        Me.TextBox1.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(674, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(172, 30)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "القيمة المرتجعه"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(852, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 30)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "القيمة المرتجعه"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkbox1})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 84)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(998, 307)
        Me.DataGridView1.TabIndex = 2
        '
        'checkbox1
        '
        Me.checkbox1.HeaderText = "اختيار"
        Me.checkbox1.Name = "checkbox1"
        '
        'InvoiceTool1
        '
        Me.InvoiceTool1.AutoClose = False
        Me.InvoiceTool1.DigitalSignaturePath = Nothing
        Me.InvoiceTool1.PortalUrl = "https://portal.jofotara.gov.jo/ar/invoices/"
        Me.InvoiceTool1.showresulttoast = False
        Me.InvoiceTool1.UserKey = Nothing
        Me.InvoiceTool1.UserSecureKey = Nothing
        '
        'return_invoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 432)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Cairo", 8.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "return_invoice"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "return_invoice"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents SimpleButton1 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents BarCodeControl1 As TextBox
    Friend WithEvents LabelControl5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents HopeButton1 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents checkbox1 As DataGridViewCheckBoxColumn
    Friend WithEvents InvoiceTool1 As jofotaratoolkit.InvoiceTool
End Class
