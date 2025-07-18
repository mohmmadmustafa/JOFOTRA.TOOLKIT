<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PAY_POS
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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblDateTime = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.HopeButton1 = New ReaLTaiizor.Controls.HopeButton()
        Me.btnCancel = New ReaLTaiizor.Controls.HopeButton()
        Me.btnSave = New ReaLTaiizor.Controls.HopeButton()
        Me.pnlProductEntry = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblProductType = New System.Windows.Forms.Label()
        Me.cboProductType = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.txtTotalValue = New System.Windows.Forms.TextBox()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.lblTaxRate = New System.Windows.Forms.Label()
        Me.txtProductName = New System.Windows.Forms.TextBox()
        Me.txtTaxRate = New System.Windows.Forms.TextBox()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblBarcode = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.grdProducts = New System.Windows.Forms.DataGridView()
        Me.txtGrandTotal = New System.Windows.Forms.TextBox()
        Me.txtTotalTax = New System.Windows.Forms.TextBox()
        Me.txtTotalDiscount = New System.Windows.Forms.TextBox()
        Me.txtTotalPrice = New System.Windows.Forms.TextBox()
        Me.lblGrandTotal = New System.Windows.Forms.Label()
        Me.lblTotalTax = New System.Windows.Forms.Label()
        Me.lblTotalDiscount = New System.Windows.Forms.Label()
        Me.lblTotalPrice = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlProductEntry.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblDateTime)
        Me.pnlTop.Controls.Add(Me.Panel2)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1155, 59)
        Me.pnlTop.TabIndex = 0
        '
        'lblDateTime
        '
        Me.lblDateTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDateTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDateTime.Font = New System.Drawing.Font("Cairo", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblDateTime.Location = New System.Drawing.Point(437, 0)
        Me.lblDateTime.Name = "lblDateTime"
        Me.lblDateTime.Size = New System.Drawing.Size(718, 59)
        Me.lblDateTime.TabIndex = 0
        Me.lblDateTime.Text = "التاريخ والوقت"
        Me.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.HopeButton1)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(437, 59)
        Me.Panel2.TabIndex = 1
        '
        'HopeButton1
        '
        Me.HopeButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.HopeButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Font = New System.Drawing.Font("Cairo", 10.0!, System.Drawing.FontStyle.Bold)
        Me.HopeButton1.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.HopeButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton1.Location = New System.Drawing.Point(12, 8)
        Me.HopeButton1.Name = "HopeButton1"
        Me.HopeButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Size = New System.Drawing.Size(120, 40)
        Me.HopeButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton1.TabIndex = 5
        Me.HopeButton1.Text = "اغلاق"
        Me.HopeButton1.TextColor = System.Drawing.Color.White
        Me.HopeButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnCancel
        '
        Me.btnCancel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnCancel.ButtonType = ReaLTaiizor.Util.HopeButtonType.Danger
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCancel.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Cairo", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnCancel.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(159, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.Size = New System.Drawing.Size(120, 40)
        Me.btnCancel.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "الغاء"
        Me.btnCancel.TextColor = System.Drawing.Color.White
        Me.btnCancel.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnSave
        '
        Me.btnSave.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnSave.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnSave.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Cairo", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnSave.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnSave.Location = New System.Drawing.Point(311, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.Size = New System.Drawing.Size(120, 40)
        Me.btnSave.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "حفظ"
        Me.btnSave.TextColor = System.Drawing.Color.White
        Me.btnSave.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'pnlProductEntry
        '
        Me.pnlProductEntry.BackColor = System.Drawing.Color.White
        Me.pnlProductEntry.Controls.Add(Me.TableLayoutPanel2)
        Me.pnlProductEntry.Controls.Add(Me.Panel1)
        Me.pnlProductEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlProductEntry.Location = New System.Drawing.Point(0, 59)
        Me.pnlProductEntry.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.pnlProductEntry.Name = "pnlProductEntry"
        Me.pnlProductEntry.Size = New System.Drawing.Size(1155, 180)
        Me.pnlProductEntry.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 15
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblProductType, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cboProductType, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnAdd, 9, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTotalValue, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtTotalValue, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lblProductName, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTaxRate, 10, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtProductName, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtTaxRate, 11, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblDiscount, 8, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblPrice, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblQuantity, 6, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtDiscount, 9, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtPrice, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtQuantity, 7, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox2, 6, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 5, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBox1, 2, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 44)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1155, 136)
        Me.TableLayoutPanel2.TabIndex = 18
        '
        'lblProductType
        '
        Me.lblProductType.AutoSize = True
        Me.lblProductType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProductType.Location = New System.Drawing.Point(1078, 0)
        Me.lblProductType.Name = "lblProductType"
        Me.lblProductType.Size = New System.Drawing.Size(74, 40)
        Me.lblProductType.TabIndex = 10
        Me.lblProductType.Text = "نوع المادة"
        Me.lblProductType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboProductType
        '
        Me.cboProductType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboProductType.Location = New System.Drawing.Point(998, 6)
        Me.cboProductType.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.cboProductType.Name = "cboProductType"
        Me.cboProductType.Size = New System.Drawing.Size(74, 31)
        Me.cboProductType.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAdd.Location = New System.Drawing.Point(364, 46)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(101, 43)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "إضافة"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblTotalValue
        '
        Me.lblTotalValue.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.lblTotalValue, 3)
        Me.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalValue.Location = New System.Drawing.Point(918, 40)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(234, 55)
        Me.lblTotalValue.TabIndex = 16
        Me.lblTotalValue.Text = "القيمة الإجمالية"
        Me.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalValue
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.txtTotalValue, 5)
        Me.txtTotalValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTotalValue.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalValue.Location = New System.Drawing.Point(518, 46)
        Me.txtTotalValue.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtTotalValue.Name = "txtTotalValue"
        Me.txtTotalValue.ReadOnly = True
        Me.txtTotalValue.Size = New System.Drawing.Size(394, 40)
        Me.txtTotalValue.TabIndex = 7
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProductName.Location = New System.Drawing.Point(918, 0)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(74, 40)
        Me.lblProductName.TabIndex = 11
        Me.lblProductName.Text = "اسم المادة"
        Me.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTaxRate
        '
        Me.lblTaxRate.AutoSize = True
        Me.lblTaxRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTaxRate.Location = New System.Drawing.Point(278, 0)
        Me.lblTaxRate.Name = "lblTaxRate"
        Me.lblTaxRate.Size = New System.Drawing.Size(80, 40)
        Me.lblTaxRate.TabIndex = 15
        Me.lblTaxRate.Text = "نسبة الضريبة"
        Me.lblTaxRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtProductName
        '
        Me.txtProductName.Location = New System.Drawing.Point(838, 6)
        Me.txtProductName.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.Size = New System.Drawing.Size(74, 30)
        Me.txtProductName.TabIndex = 2
        '
        'txtTaxRate
        '
        Me.txtTaxRate.Location = New System.Drawing.Point(198, 6)
        Me.txtTaxRate.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtTaxRate.Name = "txtTaxRate"
        Me.txtTaxRate.Size = New System.Drawing.Size(74, 30)
        Me.txtTaxRate.TabIndex = 6
        '
        'lblDiscount
        '
        Me.lblDiscount.AutoSize = True
        Me.lblDiscount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDiscount.Location = New System.Drawing.Point(471, 0)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(41, 40)
        Me.lblDiscount.TabIndex = 14
        Me.lblDiscount.Text = "الخصم"
        Me.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPrice
        '
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPrice.Location = New System.Drawing.Point(782, 0)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(50, 40)
        Me.lblPrice.TabIndex = 12
        Me.lblPrice.Text = "السعر"
        Me.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblQuantity.Location = New System.Drawing.Point(623, 0)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(49, 40)
        Me.lblQuantity.TabIndex = 13
        Me.lblQuantity.Text = "الكمية"
        Me.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDiscount
        '
        Me.txtDiscount.Location = New System.Drawing.Point(364, 6)
        Me.txtDiscount.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(101, 30)
        Me.txtDiscount.TabIndex = 5
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(678, 6)
        Me.txtPrice.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(98, 30)
        Me.txtPrice.TabIndex = 3
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(518, 6)
        Me.txtQuantity.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(99, 30)
        Me.txtQuantity.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label1, 2)
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(998, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 41)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "السعر شامل الضريبه"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox2
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.TextBox2, 2)
        Me.TextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox2.Location = New System.Drawing.Point(471, 101)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(146, 30)
        Me.TextBox2.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label2, 2)
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(623, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 41)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "الخصم بعد الضريبه"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.TextBox1, 2)
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(838, 101)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(154, 30)
        Me.TextBox1.TabIndex = 18
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1155, 44)
        Me.Panel1.TabIndex = 17
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1005.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblBarcode, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtBarcode, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1155, 44)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblBarcode
        '
        Me.lblBarcode.AutoSize = True
        Me.lblBarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBarcode.Font = New System.Drawing.Font("Cairo", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblBarcode.Location = New System.Drawing.Point(1008, 0)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(144, 44)
        Me.lblBarcode.TabIndex = 9
        Me.lblBarcode.Text = "الباركود"
        Me.lblBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBarcode
        '
        Me.txtBarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBarcode.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.txtBarcode.Location = New System.Drawing.Point(3, 6)
        Me.txtBarcode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(999, 37)
        Me.txtBarcode.TabIndex = 0
        '
        'grdProducts
        '
        Me.grdProducts.AllowUserToAddRows = False
        Me.grdProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdProducts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProducts.Location = New System.Drawing.Point(0, 239)
        Me.grdProducts.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.grdProducts.Name = "grdProducts"
        Me.grdProducts.Size = New System.Drawing.Size(1155, 454)
        Me.grdProducts.TabIndex = 2
        '
        'txtGrandTotal
        '
        Me.txtGrandTotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGrandTotal.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.txtGrandTotal.Location = New System.Drawing.Point(-111, 6)
        Me.txtGrandTotal.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtGrandTotal.Name = "txtGrandTotal"
        Me.txtGrandTotal.ReadOnly = True
        Me.txtGrandTotal.Size = New System.Drawing.Size(244, 40)
        Me.txtGrandTotal.TabIndex = 7
        '
        'txtTotalTax
        '
        Me.txtTotalTax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTotalTax.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalTax.Location = New System.Drawing.Point(346, 6)
        Me.txtTotalTax.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtTotalTax.Name = "txtTotalTax"
        Me.txtTotalTax.ReadOnly = True
        Me.txtTotalTax.Size = New System.Drawing.Size(143, 40)
        Me.txtTotalTax.TabIndex = 6
        '
        'txtTotalDiscount
        '
        Me.txtTotalDiscount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTotalDiscount.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalDiscount.Location = New System.Drawing.Point(639, 6)
        Me.txtTotalDiscount.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtTotalDiscount.Name = "txtTotalDiscount"
        Me.txtTotalDiscount.ReadOnly = True
        Me.txtTotalDiscount.Size = New System.Drawing.Size(157, 40)
        Me.txtTotalDiscount.TabIndex = 5
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTotalPrice.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.txtTotalPrice.Location = New System.Drawing.Point(934, 6)
        Me.txtTotalPrice.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtTotalPrice.Name = "txtTotalPrice"
        Me.txtTotalPrice.ReadOnly = True
        Me.txtTotalPrice.Size = New System.Drawing.Size(113, 40)
        Me.txtTotalPrice.TabIndex = 4
        '
        'lblGrandTotal
        '
        Me.lblGrandTotal.AutoSize = True
        Me.lblGrandTotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGrandTotal.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblGrandTotal.Location = New System.Drawing.Point(139, 0)
        Me.lblGrandTotal.Name = "lblGrandTotal"
        Me.lblGrandTotal.Size = New System.Drawing.Size(166, 56)
        Me.lblGrandTotal.TabIndex = 3
        Me.lblGrandTotal.Text = "المجموع النهائي"
        Me.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalTax
        '
        Me.lblTotalTax.AutoSize = True
        Me.lblTotalTax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalTax.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalTax.Location = New System.Drawing.Point(495, 0)
        Me.lblTotalTax.Name = "lblTotalTax"
        Me.lblTotalTax.Size = New System.Drawing.Size(138, 56)
        Me.lblTotalTax.TabIndex = 2
        Me.lblTotalTax.Text = "مجموع الضريبة"
        Me.lblTotalTax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalDiscount
        '
        Me.lblTotalDiscount.AutoSize = True
        Me.lblTotalDiscount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalDiscount.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalDiscount.Location = New System.Drawing.Point(802, 0)
        Me.lblTotalDiscount.Name = "lblTotalDiscount"
        Me.lblTotalDiscount.Size = New System.Drawing.Size(126, 56)
        Me.lblTotalDiscount.TabIndex = 1
        Me.lblTotalDiscount.Text = "مجموع الخصم"
        Me.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalPrice
        '
        Me.lblTotalPrice.AutoSize = True
        Me.lblTotalPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalPrice.Font = New System.Drawing.Font("Cairo", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalPrice.Location = New System.Drawing.Point(1053, 0)
        Me.lblTotalPrice.Name = "lblTotalPrice"
        Me.lblTotalPrice.Size = New System.Drawing.Size(99, 56)
        Me.lblTotalPrice.TabIndex = 0
        Me.lblTotalPrice.Text = "المجموع"
        Me.lblTotalPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.White
        Me.pnlButtons.Controls.Add(Me.TableLayoutPanel3)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 693)
        Me.pnlButtons.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(1155, 56)
        Me.pnlButtons.TabIndex = 4
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 10
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.lblTotalPrice, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtGrandTotal, 8, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtTotalPrice, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblGrandTotal, 7, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtTotalTax, 5, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblTotalDiscount, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtTotalDiscount, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblTotalTax, 4, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1155, 56)
        Me.TableLayoutPanel3.TabIndex = 8
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'PAY_POS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1155, 749)
        Me.Controls.Add(Me.grdProducts)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlProductEntry)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Name = "PAY_POS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "نقطة البيع"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlProductEntry.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.grdProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblDateTime As System.Windows.Forms.Label
    Friend WithEvents pnlProductEntry As System.Windows.Forms.Panel
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents cboProductType As System.Windows.Forms.ComboBox
    Friend WithEvents txtProductName As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtTaxRate As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalValue As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents lblProductType As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents lblQuantity As System.Windows.Forms.Label
    Friend WithEvents lblDiscount As System.Windows.Forms.Label
    Friend WithEvents lblTaxRate As System.Windows.Forms.Label
    Friend WithEvents lblTotalValue As System.Windows.Forms.Label
    Friend WithEvents grdProducts As System.Windows.Forms.DataGridView
    Friend WithEvents lblTotalPrice As System.Windows.Forms.Label
    Friend WithEvents lblTotalDiscount As System.Windows.Forms.Label
    Friend WithEvents lblTotalTax As System.Windows.Forms.Label
    Friend WithEvents lblGrandTotal As System.Windows.Forms.Label
    Friend WithEvents txtTotalPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTax As System.Windows.Forms.TextBox
    Friend WithEvents txtGrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSave As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents btnCancel As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents HopeButton1 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
End Class