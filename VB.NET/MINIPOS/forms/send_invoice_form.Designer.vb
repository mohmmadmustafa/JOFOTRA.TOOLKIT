<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class send_invoice_form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(send_invoice_form))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl5 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ProgressPanel1 = New System.Windows.Forms.ProgressBar()
        Me.SimpleButton4 = New ReaLTaiizor.Controls.HopeButton()
        Me.BarCodeControl1 = New System.Windows.Forms.TextBox()
        Me.SimpleButton1 = New ReaLTaiizor.Controls.HopeButton()
        Me.InvoiceTool1 = New jofotaratoolkit.InvoiceTool()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'LabelControl5
        '
        Me.LabelControl5.AutoSize = True
        Me.LabelControl5.Location = New System.Drawing.Point(482, 191)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "Label1"
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(691, 173)
        Me.ListBox1.TabIndex = 1
        '
        'ProgressPanel1
        '
        Me.ProgressPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressPanel1.Location = New System.Drawing.Point(0, 223)
        Me.ProgressPanel1.Name = "ProgressPanel1"
        Me.ProgressPanel1.Size = New System.Drawing.Size(691, 23)
        Me.ProgressPanel1.TabIndex = 2
        '
        'SimpleButton4
        '
        Me.SimpleButton4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.SimpleButton4.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.SimpleButton4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SimpleButton4.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.SimpleButton4.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SimpleButton4.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.SimpleButton4.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.SimpleButton4.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.SimpleButton4.Location = New System.Drawing.Point(12, 177)
        Me.SimpleButton4.Name = "SimpleButton4"
        Me.SimpleButton4.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SimpleButton4.Size = New System.Drawing.Size(120, 40)
        Me.SimpleButton4.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.SimpleButton4.TabIndex = 3
        Me.SimpleButton4.Text = "اعاده المحاوله"
        Me.SimpleButton4.TextColor = System.Drawing.Color.White
        Me.SimpleButton4.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'BarCodeControl1
        '
        Me.BarCodeControl1.Location = New System.Drawing.Point(138, 188)
        Me.BarCodeControl1.Name = "BarCodeControl1"
        Me.BarCodeControl1.Size = New System.Drawing.Size(314, 20)
        Me.BarCodeControl1.TabIndex = 4
        '
        'SimpleButton1
        '
        Me.SimpleButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.SimpleButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.SimpleButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SimpleButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.SimpleButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SimpleButton1.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.SimpleButton1.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.SimpleButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.SimpleButton1.Location = New System.Drawing.Point(559, 177)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SimpleButton1.Size = New System.Drawing.Size(120, 40)
        Me.SimpleButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.Text = "اغلاق"
        Me.SimpleButton1.TextColor = System.Drawing.Color.White
        Me.SimpleButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
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
        'send_invoice_form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 246)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.BarCodeControl1)
        Me.Controls.Add(Me.SimpleButton4)
        Me.Controls.Add(Me.ProgressPanel1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.LabelControl5)
        Me.Name = "send_invoice_form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "send_invoice_form"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelControl5 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ProgressPanel1 As ProgressBar
    Friend WithEvents SimpleButton4 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents BarCodeControl1 As TextBox
    Friend WithEvents SimpleButton1 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents InvoiceTool1 As jofotaratoolkit.InvoiceTool
End Class
