<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pay_pos_deep
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RibbonForm1 = New ReaLTaiizor.Forms.RibbonForm()
        Me.SuspendLayout()
        '
        'RibbonForm1
        '
        Me.RibbonForm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer))
        Me.RibbonForm1.BaseColor = System.Drawing.Color.Fuchsia
        Me.RibbonForm1.BottomLineColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RibbonForm1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality
        Me.RibbonForm1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RibbonForm1.HatchType = System.Drawing.Drawing2D.HatchStyle.SmallGrid
        Me.RibbonForm1.HeaderLineColorA = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.RibbonForm1.HeaderLineColorB = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.RibbonForm1.HeaderLineColorC = System.Drawing.Color.Black
        Me.RibbonForm1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonForm1.Name = "RibbonForm1"
        Me.RibbonForm1.RibbonEdgeColorA = System.Drawing.Color.Black
        Me.RibbonForm1.RibbonEdgeColorB = System.Drawing.Color.Black
        Me.RibbonForm1.RibbonEdgeColorC = System.Drawing.Color.Black
        Me.RibbonForm1.RibbonEdgeColorD = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.RibbonForm1.RibbonEdgeColorE = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.RibbonForm1.Size = New System.Drawing.Size(800, 450)
        Me.RibbonForm1.SubTitle = Nothing
        Me.RibbonForm1.SubTitleColor = System.Drawing.Color.WhiteSmoke
        Me.RibbonForm1.SubTitleFont = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.RibbonForm1.TabIndex = 0
        Me.RibbonForm1.Text = "RibbonForm1"
        '
        'pay_pos_deep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.RibbonForm1)
        Me.Name = "pay_pos_deep"
        Me.ShowIcon = False
        Me.Text = "pay_pos_deep"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.ResumeLayout(False)

    End Sub
    Public Sub New()
        ' تهيئة النموذج
        Me.Text = "نظام الدفع POS"
        Me.WindowState = FormWindowState.Maximized
        Me.StartPosition = FormStartPosition.CenterScreen

        ' تهيئة عناصر التحكم

    End Sub
    Private WithEvents pnlDateTime As New Panel
    Private WithEvents lblDate As New Label
    Private WithEvents lblTime As New Label
    Private WithEvents timerDateTime As New Timer

    ' عناصر التحكم في لوحة إدخال المنتج
    Private WithEvents pnlProductEntry As New Panel
    Private WithEvents lblBarcode As New Label
    Private WithEvents txtBarcode As New TextBox
    Private WithEvents lblProductType As New Label
    Private WithEvents cmbProductType As New ComboBox
    Private WithEvents lblProductName As New Label
    Private WithEvents txtProductName As New TextBox
    Private WithEvents lblPrice As New Label
    Private WithEvents txtPrice As New TextBox
    Private WithEvents lblQuantity As New Label
    Private WithEvents txtQuantity As New TextBox
    Private WithEvents lblDiscount As New Label
    Private WithEvents txtDiscount As New TextBox
    Private WithEvents lblTax As New Label
    Private WithEvents txtTax As New TextBox
    Private WithEvents lblTotal As New Label
    Private WithEvents txtTotal As New TextBox
    Private WithEvents btnAdd As New Button

    ' عناصر التحكم في GridView لعرض العناصر المضافة
    Private WithEvents dgvOrderItems As New DataGridView
    Private WithEvents colBarcode As New DataGridViewTextBoxColumn
    Private WithEvents colProductName As New DataGridViewTextBoxColumn
    Private WithEvents colPrice As New DataGridViewTextBoxColumn
    Private WithEvents colQuantity As New DataGridViewTextBoxColumn
    Private WithEvents colDiscount As New DataGridViewTextBoxColumn
    Private WithEvents colTax As New DataGridViewTextBoxColumn
    Private WithEvents colTotal As New DataGridViewTextBoxColumn
    Private WithEvents colEdit As New DataGridViewButtonColumn
    Private WithEvents colDelete As New DataGridViewButtonColumn

    ' عناصر التحكم في لوحة ملخص الفاتورة
    Private WithEvents pnlInvoiceSummary As New Panel
    Private WithEvents lblSubtotal As New Label
    Private WithEvents txtSubtotal As New TextBox
    Private WithEvents lblTotalDiscount As New Label
    Private WithEvents txtTotalDiscount As New TextBox
    Private WithEvents lblTotalTax As New Label
    Private WithEvents txtTotalTax As New TextBox
    Private WithEvents lblGrandTotal As New Label
    Private WithEvents txtGrandTotal As New TextBox
    Private WithEvents btnCancel As New Button
    Private WithEvents btnPrint As New Button
    Private WithEvents btnSave As New Button

    ' عناصر التحكم في مربع حوار حفظ الفاتورة
    Private WithEvents dlgSaveInvoice As New Form
    Private WithEvents pnlCustomerInfo As New Panel
    Private WithEvents lblCustomer As New Label
    Private WithEvents cmbCustomer As New ComboBox
    Private WithEvents lblCustomerName As New Label
    Private WithEvents txtCustomerName As New TextBox
    Private WithEvents lblCustomerTel As New Label
    Private WithEvents txtCustomerTel As New TextBox
    Private WithEvents lblCustomerTaxCode As New Label
    Private WithEvents txtCustomerTaxCode As New TextBox
    Private WithEvents lblCustomerCity As New Label
    Private WithEvents cmbCustomerCity As New ComboBox
    Private WithEvents lblCustomerPostcode As New Label
    Private WithEvents txtCustomerPostcode As New TextBox

    Private WithEvents pnlInvoiceInfo As New Panel
    Private WithEvents lblPaymentType As New Label
    Private WithEvents cmbPaymentType As New ComboBox
    Private WithEvents lblInvoiceType As New Label
    Private WithEvents cmbInvoiceType As New ComboBox
    Private WithEvents lblCurrency As New Label
    Private WithEvents cmbCurrency As New ComboBox

    Private WithEvents pnlDialogButtons As New Panel
    Private WithEvents btnDialogCancel As New Button
    Private WithEvents btnDialogPrint As New Button
    Private WithEvents btnDialogSave As New Button
    Private WithEvents btnExportToInvoice As New Button
    Friend WithEvents RibbonForm1 As ReaLTaiizor.Forms.RibbonForm
End Class
