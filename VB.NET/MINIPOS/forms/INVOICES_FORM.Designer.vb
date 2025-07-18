<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class INVOICES_FORM
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
        Me.DataGridViewInvoices = New System.Windows.Forms.DataGridView()
        Me.btnCreateInvoice = New ReaLTaiizor.Controls.HopeButton()
        Me.btnPrintInvoice = New ReaLTaiizor.Controls.HopeButton()
        Me.btnExport = New ReaLTaiizor.Controls.HopeButton()
        CType(Me.DataGridViewInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewInvoices
        '
        Me.DataGridViewInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewInvoices.Location = New System.Drawing.Point(21, 12)
        Me.DataGridViewInvoices.Name = "DataGridViewInvoices"
        Me.DataGridViewInvoices.Size = New System.Drawing.Size(240, 150)
        Me.DataGridViewInvoices.TabIndex = 0
        '
        'btnCreateInvoice
        '
        Me.btnCreateInvoice.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnCreateInvoice.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.btnCreateInvoice.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCreateInvoice.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCreateInvoice.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCreateInvoice.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnCreateInvoice.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnCreateInvoice.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnCreateInvoice.Location = New System.Drawing.Point(419, 43)
        Me.btnCreateInvoice.Name = "btnCreateInvoice"
        Me.btnCreateInvoice.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCreateInvoice.Size = New System.Drawing.Size(120, 40)
        Me.btnCreateInvoice.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCreateInvoice.TabIndex = 1
        Me.btnCreateInvoice.Text = "btnCreateInvoice"
        Me.btnCreateInvoice.TextColor = System.Drawing.Color.White
        Me.btnCreateInvoice.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnPrintInvoice
        '
        Me.btnPrintInvoice.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnPrintInvoice.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.btnPrintInvoice.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrintInvoice.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnPrintInvoice.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnPrintInvoice.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnPrintInvoice.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnPrintInvoice.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnPrintInvoice.Location = New System.Drawing.Point(419, 115)
        Me.btnPrintInvoice.Name = "btnPrintInvoice"
        Me.btnPrintInvoice.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnPrintInvoice.Size = New System.Drawing.Size(120, 40)
        Me.btnPrintInvoice.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnPrintInvoice.TabIndex = 2
        Me.btnPrintInvoice.Text = "btnPrintInvoice"
        Me.btnPrintInvoice.TextColor = System.Drawing.Color.White
        Me.btnPrintInvoice.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnExport
        '
        Me.btnExport.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnExport.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.btnExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExport.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnExport.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnExport.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnExport.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnExport.Location = New System.Drawing.Point(419, 180)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExport.Size = New System.Drawing.Size(120, 40)
        Me.btnExport.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnExport.TabIndex = 3
        Me.btnExport.Text = "btnExport"
        Me.btnExport.TextColor = System.Drawing.Color.White
        Me.btnExport.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'INVOICES_FORM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnPrintInvoice)
        Me.Controls.Add(Me.btnCreateInvoice)
        Me.Controls.Add(Me.DataGridViewInvoices)
        Me.Name = "INVOICES_FORM"
        Me.Text = "INVOICES_FORM"
        CType(Me.DataGridViewInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridViewInvoices As DataGridView
    Friend WithEvents btnCreateInvoice As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents btnPrintInvoice As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents btnExport As ReaLTaiizor.Controls.HopeButton
End Class
