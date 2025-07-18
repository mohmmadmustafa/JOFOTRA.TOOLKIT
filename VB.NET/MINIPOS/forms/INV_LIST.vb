Imports System.Data.SQLite
Imports System.Text

Public Class INV_LIST
    Private dbHandler As New DatabaseHandler()
    Private Const RecordsPerPage As Integer = 50
    Private CurrentPage As Integer = 1
    Private TotalRecords As Integer = 0
    Private IsFiltered As Boolean = False
    Private invoiceData As DataTable
    Private invoiceCache As New Dictionary(Of Integer, DataTable)()
    Private isInitialLoad As Boolean = True

    Private Sub INV_LIST_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor

        ' إعداد أولي للواجهة
        ' SetupInitialUI()

        ' تحميل البيانات الأولية
        ' LoadCustomers()
        LoadInvoices()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SetupInitialUI()
        Try


            ' تعطيم التحديثات البصرية مؤقتاً
            '   grdInvoices.SuspendLayout()

            ' إعداد خصائص أساسية لتحسين الأداء
            DoubleBufferedDataGridView1.AutoGenerateColumns = False
            DoubleBufferedDataGridView1.AllowUserToAddRows = False
            DoubleBufferedDataGridView1.AllowUserToDeleteRows = False
            DoubleBufferedDataGridView1.ReadOnly = True
            DoubleBufferedDataGridView1.RowHeadersVisible = False
            DoubleBufferedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            DoubleBufferedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            DoubleBufferedDataGridView1.ColumnHeadersHeight = 35

            ' إعداد الأعمدة
            ' InitializeGridColumns()

            ' التنسيق العام
            ' StyleDataGridView()

            ' إعادة تفعيل التحديثات البصرية
            DoubleBufferedDataGridView1.ResumeLayout(False)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub InitializeGridColumns()
        DoubleBufferedDataGridView1.Columns.Clear()

        ' أعمدة البيانات
        Dim columns As New List(Of DataGridViewColumn) From {
            New DataGridViewTextBoxColumn() With {
                .Name = "ID",
                .DataPropertyName = "ID",
                .HeaderText = "رقم الفاتورة",
                .Width = 70
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "DATE_TIME",
                .DataPropertyName = "DATE_TIME",
                .HeaderText = "تاريخ الفاتورة",
                .Width = 100
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "C_NAME",
                .DataPropertyName = "C_NAME",
                .HeaderText = "اسم العميل",
                .Width = 150,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            },
             New DataGridViewTextBoxColumn() With {
                .Name = " C_TAX_CODE",
                .DataPropertyName = " C_TAX_CODE",
                .HeaderText = "الرقم الضريبي",
                .Width = 70
            },
        New DataGridViewTextBoxColumn() With {
                .Name = "E_CODE",
                .DataPropertyName = "E_CODE",
                .HeaderText = "رقم الفاتورة",
                .Width = 100,
                .DefaultCellStyle = New DataGridViewCellStyle() With {
                        .Alignment = DataGridViewContentAlignment.MiddleRight
                }
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "INV_VALUE",
                .DataPropertyName = "INV_VALUE",
                .HeaderText = "قيمة الفاتورة",
                .Width = 100,
                .DefaultCellStyle = New DataGridViewCellStyle() With {
                    .Format = "N2",
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                }
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "TAX_VALUE",
                .DataPropertyName = "TAX_VALUE",
                .HeaderText = "قيمة الضريبة",
                .Width = 100,
                .DefaultCellStyle = New DataGridViewCellStyle() With {
                    .Format = "N2",
                    .Alignment = DataGridViewContentAlignment.MiddleRight
                }
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "INV_KIND",
                .DataPropertyName = "INV_KIND",
                .HeaderText = "نوع الفاتورة",
                .Width = 50
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "INV_PAY_KIND",
                .DataPropertyName = "INV_PAY_KIND",
                .HeaderText = "نوع الدفع",
                .Width = 50
            },
            New DataGridViewTextBoxColumn() With {
                .Name = "INV_OUT_IN",
                .DataPropertyName = "INV_OUT_IN",
                .HeaderText = "نوع الحركة",
                .Width = 50
            }
        }

        ' أعمدة الأزرار
        '        Dim buttonColumns As New List(Of DataGridViewColumn) From {
        '    New DataGridViewButtonColumn() With {
        '        .Name = "Print",
        '        .HeaderText = "طباعة",
        '        .Text = "🖨️", ' أيقونة طابعة Unicode
        '        .UseColumnTextForButtonValue = True,
        '        .Width = 80,
        '        .DefaultCellStyle = New DataGridViewCellStyle() With {
        '            .BackColor = Color.White,
        '            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '        }
        '    },
        '    New DataGridViewButtonColumn() With {
        '        .Name = "View",
        '        .HeaderText = "عرض",
        '        .Text = "👁️", ' أيقونة عين Unicode
        '        .UseColumnTextForButtonValue = True,
        '        .Width = 80,
        '        .DefaultCellStyle = New DataGridViewCellStyle() With {
        '            .BackColor = Color.White,
        '            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '        }
        '    },
        '    New DataGridViewButtonColumn() With {
        '        .Name = "Return",
        '        .HeaderText = "إرجاع",
        '        .Text = "↩️", ' أيقونة إرجاع Unicode
        '        .UseColumnTextForButtonValue = True,
        '        .Width = 80,
        '        .DefaultCellStyle = New DataGridViewCellStyle() With {
        '            .BackColor = Color.White,
        '            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
        '            .Alignment = DataGridViewContentAlignment.MiddleCenter
        '        }
        '    }
        '}



        '        ' إضافة الأعمدة دفعة واحدة
        '        DoubleBufferedDataGridView1.Columns.AddRange(columns.ToArray())
        '        DoubleBufferedDataGridView1.Columns.AddRange(buttonColumns.ToArray())
    End Sub

    Private Sub StyleDataGridView()
        With DoubleBufferedDataGridView1
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
            .DefaultCellStyle.BackColor = Color.White
            .DefaultCellStyle.Font = New Font("CAIRO", 9)
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180)
            .ColumnHeadersDefaultCellStyle.Font = New Font("CAIRO", 9, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .EnableHeadersVisualStyles = False
            .RowTemplate.Height = 30
            .GridColor = Color.LightGray
            .BorderStyle = BorderStyle.FixedSingle
        End With
    End Sub

    Private Sub LoadCustomers()
        Try
            Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
                conn.Open()
                Dim query As String = "SELECT ID, C_NAME FROM CUSTOMERS ORDER BY C_NAME"
                Using cmd As New SQLiteCommand(query, conn)
                    Using adapter As New SQLiteDataAdapter(cmd)
                        Dim table As New DataTable()
                        adapter.Fill(table)

                        ' تحميل البيانات في ComboBox على خيط الواجهة
                        Me.Invoke(Sub()
                                      'cboCustomer.BeginUpdate()
                                      '   cboCustomer.DataSource = table
                                      '   cboCustomer.DisplayMember = "C_NAME"
                                      '  cboCustomer.ValueMember = "ID"
                                      '  cboCustomer.SelectedIndex = -1
                                      '  cboCustomer.EndUpdate()
                                  End Sub)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء تحميل بيانات العملاء: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub LoadInvoices()
        Try
            Me.Cursor = Cursors.WaitCursor
            pnlPagination.Enabled = False
            btnFilter.Enabled = False
            btnShowAll.Enabled = False

            ' تحميل البيانات في الخلفية
            invoiceData = Await Task.Run(Function() GetInvoiceData())

            ' عرض البيانات في الواجهة
            Me.Invoke(Sub()
                          DoubleBufferedDataGridView1.SuspendLayout()
                          DoubleBufferedDataGridView1.DataSource = invoiceData
                          UpdatePaginationButtons()
                          DoubleBufferedDataGridView1.ResumeLayout(True)
                      End Sub)

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء تحميل الفواتير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            pnlPagination.Enabled = True
            btnFilter.Enabled = True
            btnShowAll.Enabled = True
        End Try
    End Sub

    Private Function GetInvoiceData() As DataTable
        ' التحقق من وجود البيانات في الذاكرة المؤقتة
        If Not IsFiltered AndAlso invoiceCache.ContainsKey(CurrentPage) Then
            Return invoiceCache(CurrentPage)
        End If

        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()

            ' بناء الاستعلام الأساسي
            Dim query As New StringBuilder("SELECT i.ID, i.DATE_TIME, c.C_NAME,C.C_TAX_CODE,i.E_CODE, i.INV_VALUE, i.TAX_VALUE, 
                                          ik.TXT AS INV_KIND, ipk.TXT AS INV_PAY_KIND, ioi.TXT AS INV_OUT_IN 
                                          FROM INVOICES i 
                                          JOIN CUSTOMERS c ON i.C_ID = c.ID 
                                          JOIN IN_KIND ik ON i.INV_KIND = ik.ID 
                                          JOIN INV_PAY_KIND ipk ON i.INV_PAY_KIND = ipk.ID 
                                          JOIN INV_OUT_IN ioi ON i.INV_OUT_IN = ioi.ID 
                                          WHERE 1=1")

            Dim conditions As New List(Of String)
            Dim parameters As New List(Of SQLiteParameter)

            ' إضافة شروط التصفية
            If IsFiltered Then
                '  If cboCustomer.SelectedIndex <> -1 Then
                'conditions.Add("i.C_ID = @CustomerID")
                'parameters.Add(New SQLiteParameter("@CustomerID", cboCustomer.SelectedValue))
                'End If
                If HopeToggle2.Checked = True Then
                    conditions.Add("c.C_TAX_CODE LIKE @TaxNo")
                    parameters.Add(New SQLiteParameter("@TaxNo", "%" & txtTaxNo.Text.Trim() & "%"))
                End If

                If HopeToggle1.Checked = True Then
                    conditions.Add("LOWER(i.e_code) LIKE @ecode")
                    parameters.Add(New SQLiteParameter("@ecode", (TextBox1.Text.Trim().ToLower)))
                End If
                If HopeToggle3.Checked = True Then
                    Try



                        conditions.Add("i.DATE_TIME BETWEEN @DateFrom AND @DateTo")
                        parameters.Add(New SQLiteParameter("@DateFrom", dtpFrom.Value.ToString("yyyy-MM-dd")))
                        parameters.Add(New SQLiteParameter("@DateTo", dtpTo.Value.AddDays(1).ToString("yyyy-MM-dd")))

                    Catch ex As Exception
                        Clipboard.SetText(ex.Message)
                    End Try
                End If
            End If

            ' إضافة الشروط إلى الاستعلام
            If conditions.Count > 0 Then
                query.Append(" AND " & String.Join(" AND ", conditions))
            End If

            ' حساب العدد الكلي للسجلات
            Dim countQuery As String = "SELECT COUNT(*) " & query.ToString().Substring(query.ToString().IndexOf("FROM"))
            TotalRecords = GetRecordCount(conn, countQuery, parameters)
            Me.Invoke(Sub()

                          Label1.Text = TotalRecords
                      End Sub)
            ' تطبيق التقسيم الصفحي إذا لم يتم التصفية
            If Not IsFiltered Then
                query.Append($" ORDER BY i.ID DESC LIMIT {RecordsPerPage} OFFSET {(CurrentPage - 1) * RecordsPerPage}")
            Else
                query.Append(" ORDER BY i.ID DESC")
            End If

            ' تنفيذ الاستعلام
            Dim table As New DataTable()
            Using cmd As New SQLiteCommand(query.ToString(), conn)
                cmd.Parameters.AddRange(parameters.ToArray())
                Using adapter As New SQLiteDataAdapter(cmd)
                    adapter.Fill(table)
                End Using
            End Using

            ' تخزين النتائج في الذاكرة المؤقتة إذا لم يتم التصفية
            If Not IsFiltered Then
                invoiceCache(CurrentPage) = table
            End If

            Return table
        End Using
    End Function

    Private Function GetRecordCount(conn As SQLiteConnection, query As String, parameters As List(Of SQLiteParameter)) As Integer
        Using cmd As New SQLiteCommand(query, conn)
            cmd.Parameters.AddRange(parameters.ToArray())
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function
    Private Sub UpdatePaginationButtons()
        pnlPagination.Controls.Clear()
        pnlPagination.Padding = New Padding(10)
        pnlPagination.AutoSize = True
        pnlPagination.AutoSizeMode = AutoSizeMode.GrowAndShrink

        If Not IsFiltered AndAlso TotalRecords > 0 Then
            Dim totalPages As Integer = CInt(Math.Ceiling(TotalRecords / RecordsPerPage))

            ' زر الصفحة السابقة
            Dim btnPrev As New ReaLTaiizor.Controls.HopeButton()
            btnPrev.Text = "‹"
            btnPrev.Size = New Size(40, 30)
            btnPrev.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
            btnPrev.Enabled = (CurrentPage > 1)
            AddHandler btnPrev.Click, Sub(sender, e)
                                          If CurrentPage > 1 Then
                                              CurrentPage -= 1
                                              LoadInvoices()
                                          End If
                                      End Sub
            pnlPagination.Controls.Add(btnPrev)

            ' إضافة زر الصفحة الأولى
            If CurrentPage > 3 Then
                Dim btnFirst As New ReaLTaiizor.Controls.HopeButton()
                btnFirst.Text = "1"
                btnFirst.Size = New Size(40, 30)
                btnFirst.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
                AddHandler btnFirst.Click, Sub(sender, e)
                                               CurrentPage = 1
                                               LoadInvoices()
                                           End Sub
                pnlPagination.Controls.Add(btnFirst)

                ' إضافة نقاط (...) إذا كانت هناك صفحات مخفية
                If CurrentPage > 4 Then
                    Dim lblDots1 As New Label()
                    lblDots1.Text = "..."
                    lblDots1.AutoSize = True
                    lblDots1.TextAlign = ContentAlignment.MiddleCenter
                    lblDots1.Font = New Font("Tahoma", 10, FontStyle.Bold)
                    pnlPagination.Controls.Add(lblDots1)
                End If
            End If

            ' إضافة أزرار الصفحات المحيطة بالصفحة الحالية
            Dim startPage As Integer = Math.Max(1, CurrentPage - 2)
            Dim endPage As Integer = Math.Min(totalPages, CurrentPage + 2)

            For i As Integer = startPage To endPage
                Dim btn As New ReaLTaiizor.Controls.HopeButton()
                btn.Text = i.ToString()
                btn.Size = New Size(40, 30)

                If i = CurrentPage Then
                    btn.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
                    btn.Enabled = False
                Else
                    btn.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
                    Dim page = i ' نسخ القيمة للتجنب مشاكل الclosure
                    AddHandler btn.Click, Sub(sender, e)
                                              CurrentPage = page
                                              LoadInvoices()
                                          End Sub
                End If

                pnlPagination.Controls.Add(btn)
            Next

            ' إضافة نقاط (...) إذا كانت هناك صفحات مخفية
            If CurrentPage < totalPages - 3 Then
                Dim lblDots2 As New Label()
                lblDots2.Text = "..."
                lblDots2.AutoSize = True
                lblDots2.TextAlign = ContentAlignment.MiddleCenter
                lblDots2.Font = New Font("Tahoma", 10, FontStyle.Bold)
                pnlPagination.Controls.Add(lblDots2)
            End If

            ' إضافة زر الصفحة الأخيرة
            If CurrentPage < totalPages - 2 Then
                Dim btnLast As New ReaLTaiizor.Controls.HopeButton()
                btnLast.Text = totalPages.ToString()
                btnLast.Size = New Size(40, 30)
                btnLast.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
                AddHandler btnLast.Click, Sub(sender, e)
                                              CurrentPage = totalPages
                                              LoadInvoices()
                                          End Sub
                pnlPagination.Controls.Add(btnLast)
            End If

            ' زر الصفحة التالية
            Dim btnNext As New ReaLTaiizor.Controls.HopeButton()
            btnNext.Text = "›"
            btnNext.Size = New Size(40, 30)
            btnNext.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
            btnNext.Enabled = (CurrentPage < totalPages)
            AddHandler btnNext.Click, Sub(sender, e)
                                          If CurrentPage < totalPages Then
                                              CurrentPage += 1
                                              LoadInvoices()
                                          End If
                                      End Sub
            pnlPagination.Controls.Add(btnNext)

            ' تنسيق المسافات بين الأزرار
            For Each ctrl As Control In pnlPagination.Controls
                ctrl.Margin = New Padding(2)
            Next
        End If

        ' توسيط لوحة الترقيم
        pnlPagination.Left = (Me.ClientSize.Width - pnlPagination.Width) \ 2
        pnlPagination.Top = Me.ClientSize.Height - pnlPagination.Height - 20
    End Sub
    Private Sub UpdatePaginationButtonsص()
        pnlPagination.Controls.Clear()
        If Not IsFiltered Then
            Dim totalPages As Integer = Math.Ceiling(TotalRecords / RecordsPerPage)
            For i As Integer = 1 To totalPages
                Dim btn As New ReaLTaiizor.Controls.HopeButton()
                btn.Text = i.ToString()
                btn.Size = New Size(40, 30)
                btn.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success
                btn.Tag = i
                AddHandler btn.Click, AddressOf PaginationButton_Click
                If i = CurrentPage Then
                    btn.Enabled = False
                    btn.ButtonType = ReaLTaiizor.Util.HopeButtonType.Info
                End If
                pnlPagination.Controls.Add(btn)
            Next
        End If
    End Sub
    Private Sub UpdatePaginationButtonsD()
        pnlPagination.SuspendLayout()
        pnlPagination.Controls.Clear()

        If Not IsFiltered AndAlso TotalRecords > RecordsPerPage Then
            Dim totalPages As Integer = Math.Ceiling(TotalRecords / RecordsPerPage)
            Dim startPage As Integer = Math.Max(1, CurrentPage - 2)
            Dim endPage As Integer = Math.Min(totalPages, CurrentPage + 2)

            ' زر الصفحة الأولى
            If startPage > 1 Then
                AddPageButton("1", 1)
                If startPage > 2 Then
                    AddLabel("...")
                End If
            End If

            ' أزرار الصفحات
            For i As Integer = startPage To endPage
                AddPageButton(i.ToString(), i)
            Next

            ' زر الصفحة الأخيرة
            If endPage < totalPages Then
                If endPage < totalPages - 1 Then
                    AddLabel("...")
                End If
                AddPageButton(totalPages.ToString(), totalPages)
            End If
        End If

        pnlPagination.ResumeLayout(False)
    End Sub

    Private Sub AddPageButton(text As String, pageNumber As Integer)
        Dim btn As New Button() With {
            .Text = text,
            .Size = New Size(35, 30),
            .Margin = New Padding(2),
            .Tag = pageNumber,
            .FlatStyle = FlatStyle.Flat
        }

        If pageNumber = CurrentPage Then
            btn.BackColor = Color.SteelBlue
            btn.ForeColor = Color.White
            btn.Enabled = False
        Else
            btn.BackColor = SystemColors.Control
            btn.ForeColor = SystemColors.ControlText
            AddHandler btn.Click, AddressOf PaginationButton_Click
        End If

        pnlPagination.Controls.Add(btn)
    End Sub

    Private Sub AddLabel(text As String)
        Dim lbl As New Label() With {
            .Text = text,
            .AutoSize = True,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Margin = New Padding(5, 10, 5, 0)
        }
        pnlPagination.Controls.Add(lbl)
    End Sub

    Private Sub PaginationButton_Click(sender As Object, e As EventArgs)
        Dim btn As ReaLTaiizor.Controls.HopeButton = DirectCast(sender, ReaLTaiizor.Controls.HopeButton)
        CurrentPage = Convert.ToInt32(btn.Tag)
        LoadInvoices()
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        IsFiltered = True
        CurrentPage = 1
        invoiceCache.Clear() ' مسح الذاكرة المؤقتة عند التصفية
        LoadInvoices()
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        IsFiltered = False
        CurrentPage = 1
        'cboCustomer.SelectedIndex = -1
        TextBox1.Clear()
        txtTaxNo.Clear()
        HopeToggle1.Checked = False
        HopeToggle2.Checked = False
        HopeToggle3.Checked = False
        dtpFrom.Value = dtpFrom.MinDate
        dtpTo.Value = dtpTo.MinDate
        LoadInvoices()
    End Sub

    Private Sub grdInvoices_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        Dim invoiceId As Integer = Convert.ToInt32(DoubleBufferedDataGridView1.Rows(e.RowIndex).Cells("ID").Value)
        Dim colName As String = DoubleBufferedDataGridView1.Columns(e.ColumnIndex).Name

        Select Case colName
            Case "Print"
                PrintInvoice(invoiceId)
            Case "View"
                ViewInvoice(invoiceId)
            Case "Return"
                CreateReturnInvoice(invoiceId)
        End Select
    End Sub

    Private Sub PrintInvoice(invoiceId As Integer)
        If invoiceId Then
            Dim printForm As New InvoicePrintForm(invoiceId, False)
            'printForm.ShowDialog()
        Else
            MessageBox.Show("لا توجد فاتورة لحفظها أو طباعتها", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ViewInvoice(invoiceId As Integer)
        ' كود عرض الفاتورة
        '  MessageBox.Show($"جاري عرض الفاتورة رقم {invoiceId}", "عرض")
        If invoiceId Then
            Dim printForm As New InvoicePrintForm(invoiceId, True)
            'printForm.ShowDialog()
        Else
            MessageBox.Show("لا توجد فاتورة لحفظها أو طباعتها", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub CreateReturnInvoice(invoiceId As Integer)
        ' كود إنشاء فاتورة إرجاع
        If MessageBox.Show($"هل تريد إنشاء فاتورة إرجاع للفاتورة رقم {invoiceId}؟",
                         "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            MessageBox.Show($"تم إنشاء فاتورة إرجاع للفاتورة رقم {invoiceId}", "إرجاع")
        End If
    End Sub

    Private Sub INV_LIST_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' تنظيف الذاكرة المؤقتة عند إغلاق النموذج
        invoiceCache.Clear()
        If invoiceData IsNot Nothing Then
            invoiceData.Dispose()
        End If
        Form1.Show()
    End Sub

    Private Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Me.Close()
    End Sub

    Private Sub HopeToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles HopeToggle1.CheckedChanged
        TextBox1.Enabled = HopeToggle1.Checked
    End Sub

    Private Sub HopeToggle2_CheckedChanged(sender As Object, e As EventArgs) Handles HopeToggle2.CheckedChanged
        txtTaxNo.Enabled = HopeToggle2.Checked
    End Sub

    Private Sub HopeToggle3_CheckedChanged(sender As Object, e As EventArgs) Handles HopeToggle3.CheckedChanged
        dtpFrom.Enabled = HopeToggle3.Checked
        dtpTo.Enabled = HopeToggle3.Checked
    End Sub

    Private Sub DoubleBufferedDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DoubleBufferedDataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim InvoiceId As Long = DoubleBufferedDataGridView1.Rows(e.RowIndex).Cells(2).Value

            If e.ColumnIndex = 0 Then
                Dim OO As New send_invoice_form(InvoiceId)
                OO.ShowDialog()
                ' MessageBox.Show($"تم تصدير الفاتورة بنجاح. رقم الفاتورة الضريبي: {eCode}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
            If e.ColumnIndex = 1 Then
                Dim K As New InvoicePrintForm(InvoiceId, False)
                '  K.LoadInvoiceData()
                '  K.STRART_PRINT()
                ' MessageBox.Show($"طباعة الفاتورة رقم: {InvoiceId.Value}", "طباعة")
            End If
        End If

    End Sub

    Private Sub HopeButton3_Click(sender As Object, e As EventArgs) Handles HopeButton3.Click
        Me.Close()
    End Sub
End Class
