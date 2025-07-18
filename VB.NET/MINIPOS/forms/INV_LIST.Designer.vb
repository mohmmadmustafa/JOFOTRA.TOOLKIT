<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class INV_LIST
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.HopeToggle1 = New ReaLTaiizor.Controls.HopeToggle()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.HopeToggle2 = New ReaLTaiizor.Controls.HopeToggle()
        Me.lblTaxNo = New System.Windows.Forms.Label()
        Me.txtTaxNo = New System.Windows.Forms.TextBox()
        Me.HopeToggle3 = New ReaLTaiizor.Controls.HopeToggle()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnFilter = New ReaLTaiizor.Controls.HopeButton()
        Me.btnShowAll = New ReaLTaiizor.Controls.HopeButton()
        Me.HopeButton2 = New ReaLTaiizor.Controls.HopeButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlPagination = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HopeButton1 = New ReaLTaiizor.Controls.HopeButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.HopeButton3 = New ReaLTaiizor.Controls.HopeButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DoubleBufferedDataGridView1 = New ReaLTaiizor.Controls.PoisonDataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.pnlFilter.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DoubleBufferedDataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlFilter
        '
        Me.pnlFilter.BackColor = System.Drawing.Color.White
        Me.pnlFilter.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.pnlFilter.Size = New System.Drawing.Size(1155, 39)
        Me.pnlFilter.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 19
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.HopeToggle1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCustomer, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.HopeToggle2, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTaxNo, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtTaxNo, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.HopeToggle3, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblFrom, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dtpFrom, 10, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTo, 11, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dtpTo, 12, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnFilter, 14, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnShowAll, 15, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.HopeButton2, 18, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1155, 39)
        Me.TableLayoutPanel1.TabIndex = 16
        '
        'HopeToggle1
        '
        Me.HopeToggle1.BaseColor = System.Drawing.Color.White
        Me.HopeToggle1.BaseColorA = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeToggle1.BaseColorB = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeToggle1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HopeToggle1.ForeColor = System.Drawing.Color.White
        Me.HopeToggle1.HeadColorA = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeToggle1.HeadColorB = System.Drawing.Color.White
        Me.HopeToggle1.HeadColorC = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle1.HeadColorD = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle1.Location = New System.Drawing.Point(1074, 3)
        Me.HopeToggle1.Name = "HopeToggle1"
        Me.HopeToggle1.Size = New System.Drawing.Size(48, 20)
        Me.HopeToggle1.TabIndex = 11
        Me.HopeToggle1.Text = "HopeToggle1"
        Me.HopeToggle1.UseVisualStyleBackColor = True
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCustomer.Font = New System.Drawing.Font("Cairo", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(973, 0)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(95, 39)
        Me.lblCustomer.TabIndex = 0
        Me.lblCustomer.Text = "رقم الفاتوره"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(893, 4)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(74, 20)
        Me.TextBox1.TabIndex = 10
        '
        'HopeToggle2
        '
        Me.HopeToggle2.BaseColor = System.Drawing.Color.White
        Me.HopeToggle2.BaseColorA = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeToggle2.BaseColorB = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeToggle2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HopeToggle2.ForeColor = System.Drawing.Color.White
        Me.HopeToggle2.HeadColorA = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeToggle2.HeadColorB = System.Drawing.Color.White
        Me.HopeToggle2.HeadColorC = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle2.HeadColorD = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle2.Location = New System.Drawing.Point(829, 3)
        Me.HopeToggle2.Name = "HopeToggle2"
        Me.HopeToggle2.Size = New System.Drawing.Size(48, 20)
        Me.HopeToggle2.TabIndex = 12
        Me.HopeToggle2.Text = "HopeToggle2"
        Me.HopeToggle2.UseVisualStyleBackColor = True
        '
        'lblTaxNo
        '
        Me.lblTaxNo.AutoSize = True
        Me.lblTaxNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTaxNo.Font = New System.Drawing.Font("Cairo", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxNo.Location = New System.Drawing.Point(736, 0)
        Me.lblTaxNo.Name = "lblTaxNo"
        Me.lblTaxNo.Size = New System.Drawing.Size(87, 39)
        Me.lblTaxNo.TabIndex = 2
        Me.lblTaxNo.Text = "الرقم الضريبي"
        Me.lblTaxNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTaxNo
        '
        Me.txtTaxNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTaxNo.Enabled = False
        Me.txtTaxNo.Location = New System.Drawing.Point(639, 4)
        Me.txtTaxNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTaxNo.Name = "txtTaxNo"
        Me.txtTaxNo.Size = New System.Drawing.Size(91, 20)
        Me.txtTaxNo.TabIndex = 3
        '
        'HopeToggle3
        '
        Me.HopeToggle3.BaseColor = System.Drawing.Color.White
        Me.HopeToggle3.BaseColorA = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeToggle3.BaseColorB = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeToggle3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HopeToggle3.ForeColor = System.Drawing.Color.White
        Me.HopeToggle3.HeadColorA = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeToggle3.HeadColorB = System.Drawing.Color.White
        Me.HopeToggle3.HeadColorC = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle3.HeadColorD = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeToggle3.Location = New System.Drawing.Point(577, 3)
        Me.HopeToggle3.Name = "HopeToggle3"
        Me.HopeToggle3.Size = New System.Drawing.Size(48, 20)
        Me.HopeToggle3.TabIndex = 13
        Me.HopeToggle3.Text = "HopeToggle3"
        Me.HopeToggle3.UseVisualStyleBackColor = True
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFrom.Font = New System.Drawing.Font("Cairo", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(535, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(36, 39)
        Me.lblFrom.TabIndex = 4
        Me.lblFrom.Text = "من"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpFrom
        '
        Me.dtpFrom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFrom.Enabled = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(428, 4)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpFrom.MinDate = New Date(2025, 4, 1, 0, 0, 0, 0)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(101, 20)
        Me.dtpFrom.TabIndex = 5
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTo.Font = New System.Drawing.Font("Cairo", 8.999999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(386, 0)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(36, 39)
        Me.lblTo.TabIndex = 6
        Me.lblTo.Text = "إلى"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpTo
        '
        Me.dtpTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpTo.Enabled = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(278, 4)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dtpTo.MinDate = New Date(2025, 4, 1, 0, 0, 0, 0)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(102, 20)
        Me.dtpTo.TabIndex = 7
        '
        'btnFilter
        '
        Me.btnFilter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnFilter.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFilter.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnFilter.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilter.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.btnFilter.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnFilter.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnFilter.Location = New System.Drawing.Point(170, 3)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnFilter.Size = New System.Drawing.Size(67, 33)
        Me.btnFilter.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnFilter.TabIndex = 14
        Me.btnFilter.Text = "تصفيه"
        Me.btnFilter.TextColor = System.Drawing.Color.White
        Me.btnFilter.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'btnShowAll
        '
        Me.btnShowAll.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.btnShowAll.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
        Me.btnShowAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnShowAll.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnShowAll.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnShowAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnShowAll.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.btnShowAll.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.btnShowAll.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnShowAll.Location = New System.Drawing.Point(92, 3)
        Me.btnShowAll.Name = "btnShowAll"
        Me.btnShowAll.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnShowAll.Size = New System.Drawing.Size(72, 33)
        Me.btnShowAll.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.btnShowAll.TabIndex = 15
        Me.btnShowAll.Text = "الجميع"
        Me.btnShowAll.TextColor = System.Drawing.Color.White
        Me.btnShowAll.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'HopeButton2
        '
        Me.HopeButton2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton2.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
        Me.HopeButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton2.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton2.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton2.Dock = System.Windows.Forms.DockStyle.Right
        Me.HopeButton2.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.HopeButton2.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.HopeButton2.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton2.Location = New System.Drawing.Point(3, 3)
        Me.HopeButton2.Name = "HopeButton2"
        Me.HopeButton2.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton2.Size = New System.Drawing.Size(58, 33)
        Me.HopeButton2.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton2.TabIndex = 16
        Me.HopeButton2.Text = "➕ جديد"
        Me.HopeButton2.TextColor = System.Drawing.Color.White
        Me.HopeButton2.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.HopeButton1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 663)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1155, 46)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlPagination)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(77, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1078, 46)
        Me.Panel2.TabIndex = 9
        '
        'pnlPagination
        '
        Me.pnlPagination.AutoSize = True
        Me.pnlPagination.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlPagination.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPagination.Location = New System.Drawing.Point(140, 0)
        Me.pnlPagination.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Size = New System.Drawing.Size(938, 46)
        Me.pnlPagination.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Cairo", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 46)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "00"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HopeButton1
        '
        Me.HopeButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Info
        Me.HopeButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Dock = System.Windows.Forms.DockStyle.Left
        Me.HopeButton1.Font = New System.Drawing.Font("Cairo", 9.0!)
        Me.HopeButton1.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.HopeButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton1.Location = New System.Drawing.Point(0, 0)
        Me.HopeButton1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.HopeButton1.Name = "HopeButton1"
        Me.HopeButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Size = New System.Drawing.Size(77, 46)
        Me.HopeButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton1.TabIndex = 7
        Me.HopeButton1.Text = "اغلاق"
        Me.HopeButton1.TextColor = System.Drawing.Color.White
        Me.HopeButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.HopeButton3)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 542)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1024, 64)
        Me.Panel3.TabIndex = 2
        '
        'HopeButton3
        '
        Me.HopeButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HopeButton3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton3.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.HopeButton3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton3.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton3.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton3.Font = New System.Drawing.Font("Cairo", 12.0!)
        Me.HopeButton3.HoverTextColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.HopeButton3.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton3.Location = New System.Drawing.Point(21, 14)
        Me.HopeButton3.Margin = New System.Windows.Forms.Padding(4)
        Me.HopeButton3.Name = "HopeButton3"
        Me.HopeButton3.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton3.Size = New System.Drawing.Size(95, 40)
        Me.HopeButton3.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton3.TabIndex = 17
        Me.HopeButton3.Text = "اغلاق"
        Me.HopeButton3.TextColor = System.Drawing.Color.White
        Me.HopeButton3.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(592, 8)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(229, 45)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "القيمة المرتجعه"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label7.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(829, 8)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 30)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "القيمة المرتجعه"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'DoubleBufferedDataGridView1
        '
        Me.DoubleBufferedDataGridView1.AllowUserToAddRows = False
        Me.DoubleBufferedDataGridView1.AllowUserToDeleteRows = False
        Me.DoubleBufferedDataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DoubleBufferedDataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DoubleBufferedDataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DoubleBufferedDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DoubleBufferedDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DoubleBufferedDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cairo", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DoubleBufferedDataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DoubleBufferedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DoubleBufferedDataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DoubleBufferedDataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DoubleBufferedDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DoubleBufferedDataGridView1.EnableHeadersVisualStyles = False
        Me.DoubleBufferedDataGridView1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.DoubleBufferedDataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DoubleBufferedDataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DoubleBufferedDataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DoubleBufferedDataGridView1.Name = "DoubleBufferedDataGridView1"
        Me.DoubleBufferedDataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DoubleBufferedDataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DoubleBufferedDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DoubleBufferedDataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DoubleBufferedDataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DoubleBufferedDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DoubleBufferedDataGridView1.Size = New System.Drawing.Size(1024, 542)
        Me.DoubleBufferedDataGridView1.TabIndex = 3
        '
        'Column1
        '
        Me.Column1.HeaderText = "مزامنه"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "طباعه"
        Me.Column2.Name = "Column2"
        Me.Column2.Text = "🖨️"
        Me.Column2.Width = 50
        '
        'INV_LIST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 30.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 606)
        Me.Controls.Add(Me.DoubleBufferedDataGridView1)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Cairo", 12.0!)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "INV_LIST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "قائمة الفواتير"
        Me.pnlFilter.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DoubleBufferedDataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents txtTaxNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents lblTaxNo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label

    Friend WithEvents Panel1 As Panel
    Friend WithEvents HopeButton1 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents pnlPagination As FlowLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents HopeToggle3 As ReaLTaiizor.Controls.HopeToggle
    Friend WithEvents HopeToggle2 As ReaLTaiizor.Controls.HopeToggle
    Friend WithEvents HopeToggle1 As ReaLTaiizor.Controls.HopeToggle
    Friend WithEvents btnFilter As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents btnShowAll As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents HopeButton2 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents HopeButton3 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DoubleBufferedDataGridView1 As ReaLTaiizor.Controls.PoisonDataGridView
    Friend WithEvents Column1 As DataGridViewButtonColumn
    Friend WithEvents Column2 As DataGridViewButtonColumn
End Class