
' PAY_POS.vb
' ----------------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Text

Public Class PAY_POS
    Public Const CASH_CUSTOMER As String = "زبون نقدي"
    Public Const NEW_CUSTOMER As String = "زبون جديد"
    Private productTable As New DataTable()
    Private currentInvoiceId As Long? = Nothing

    Private Sub PAY_POS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGridColumns()
        LoadProductTypes()
        UpdateDateTime()
        InitializeProductTable()
        StyleDataGridView()
        LoadAutoComplete()
    End Sub

    Private Sub InitializeGridColumns()
        grdProducts.Columns.Clear()
        ' With grdProducts.Columns
        '.Add("BARCODE", "الباركود")
        '.Add("PRO_KIND", "نوع المادة")
        '.Add("PRO_NAME", "اسم المادة")
        'Add("PRO_PRICE", "السعر")
        '.Add("PRO_COUNT", "الكمية")
        '.Add("PRO_DISCOUNT", "الخصم")
        '.Add("PRO_TX_PUBLIC", "نسبة الضريبة")
        '.Add("FINAL_VALUE", "القيمة الإجمالية")
        ' End With
        Dim btnDelete As New DataGridViewButtonColumn()
        btnDelete.Name = "Delete"
        btnDelete.HeaderText = "حذف"
        btnDelete.Text = "حذف"
        btnDelete.UseColumnTextForButtonValue = True
        grdProducts.Columns.Add(btnDelete)
    End Sub

    Private Sub StyleDataGridView()
        With grdProducts
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray
            .DefaultCellStyle.BackColor = Color.White
            .ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue
            .ColumnHeadersDefaultCellStyle.Font = New Font(.Font, FontStyle.Bold)
            .EnableHeadersVisualStyles = False
            .RowTemplate.Height = 30
            .DefaultCellStyle.ForeColor = Color.Black
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            For Each col As DataGridViewColumn In .Columns
                If col.Name <> "Delete" Then
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            Next
            .GridColor = Color.Gray
            .BorderStyle = BorderStyle.Fixed3D
        End With
    End Sub

    Private Sub InitializeProductTable()
        productTable.Columns.Add("BARCODE", GetType(String))
        productTable.Columns.Add("PRO_KIND", GetType(Integer))
        productTable.Columns.Add("PRO_NAME", GetType(String))
        productTable.Columns.Add("PRO_PRICE", GetType(Double))
        productTable.Columns.Add("PRO_COUNT", GetType(Double))
        productTable.Columns.Add("PRO_DISCOUNT", GetType(Double))
        productTable.Columns.Add("PRO_TX_PUBLIC", GetType(Double))
        productTable.Columns.Add("FINAL_VALUE", GetType(Double))
        grdProducts.DataSource = productTable
    End Sub

    Private Sub LoadProductTypes()
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim adapter As New SQLiteDataAdapter("SELECT ID, TXT FROM PRO_KIND", conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            cboProductType.DataSource = table
            cboProductType.DisplayMember = "TXT"
            cboProductType.ValueMember = "ID"
            cboProductType.SelectedIndex = -1
        End Using
    End Sub

    Private Sub UpdateDateTime()
        lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd dddd       HH:mm:ss")
    End Sub

    Private Sub btnRefreshTime_Click(sender As Object, e As EventArgs)
        UpdateDateTime()
    End Sub

    Private Sub txtBarcode_Leave(sender As Object, e As EventArgs) Handles txtBarcode.Leave
        getbarcode()
    End Sub
    Private Sub getbarcode()
        If Not String.IsNullOrWhiteSpace(txtBarcode.Text) Then
            Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
                conn.Open()
                Dim cmd As New SQLiteCommand("SELECT PRO_NAME, PRO_PRICE, PRO_TAX_VALUE, PRO_KIND,price_with_tax FROM PRODUCTS WHERE BARCODE = @barcode", conn)
                cmd.Parameters.AddWithValue("@barcode", txtBarcode.Text)
                Dim reader As SQLiteDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    txtProductName.Text = reader("PRO_NAME").ToString()
                    txtPrice.Text = reader("PRO_PRICE").ToString()
                    txtTaxRate.Text = reader("PRO_TAX_VALUE").ToString()
                    cboProductType.SelectedValue = reader("PRO_KIND")
                    TextBox1.Text = reader("price_with_tax").ToString()
                    txtQuantity.Text = "1"
                    txtDiscount.Text = "0"
                    CalculateTotalValue()
                    txtQuantity.Focus()
                Else
                    txtProductName.Clear()
                    txtPrice.Clear()
                    txtTaxRate.Clear()
                    cboProductType.SelectedIndex = 0
                    txtQuantity.Text = "1"
                    txtDiscount.Text = "0"
                    txtTotalValue.Clear()
                    txtProductName.Focus()
                End If
                reader.Close()
            End Using
        End If
    End Sub
    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged, txtQuantity.TextChanged, txtDiscount.TextChanged, txtTaxRate.TextChanged
        CalculateTotalValue()
    End Sub

    Private Sub CalculateTotalValue()
        Try
            Dim price As Double = If(String.IsNullOrWhiteSpace(txtPrice.Text), 0, Convert.ToDouble(txtPrice.Text))
            Dim quantity As Double = If(String.IsNullOrWhiteSpace(txtQuantity.Text), 0, Convert.ToDouble(txtQuantity.Text))
            Dim discount As Double = If(String.IsNullOrWhiteSpace(txtDiscount.Text), 0, Convert.ToDouble(txtDiscount.Text))
            Dim taxRate As Double = If(String.IsNullOrWhiteSpace(txtTaxRate.Text), 0, Convert.ToDouble(txtTaxRate.Text))
            Dim total As Double = RoundToFiveCents((price * quantity - discount) * ((taxRate / 100) + 1))
            Dim PRICE_WITH_TAX As Double = RoundToFiveCents(price * ((taxRate / 100) + 1))
            txtTotalValue.Text = total.ToString("F3")
            TextBox1.Text = PRICE_WITH_TAX.ToString("F3")
        Catch
            txtTotalValue.Text = "0.000"
        End Try
    End Sub

    Private Sub CalculateTotalValuefrompriceaftertax()
        Try
            Dim price As Double = If(String.IsNullOrWhiteSpace(TextBox1.Text), 0, Convert.ToDouble(TextBox1.Text))
            '  Dim quantity As Double = If(String.IsNullOrWhiteSpace(txtQuantity.Text), 0, Convert.ToDouble(txtQuantity.Text))
            '  Dim fd As Double = If(String.IsNullOrWhiteSpace(TextBox2.Text), 0, Convert.ToDouble(TextBox2.Text))
            Dim taxRate As Double = If(String.IsNullOrWhiteSpace(txtTaxRate.Text), 0, Convert.ToDouble(txtTaxRate.Text))
            '  Dim total As Double = RoundToFiveCents((price * quantity) * ((taxRate / 100) + 1))
            ' Dim PRICE_WITH_TAX As Doubl e = RoundToFiveCents(price * ((taxRate / 100) + 1))

            Dim pricebefortax As Double = Convert.ToDouble((price / ((taxRate / 100) + 1)))
            txtPrice.Text = pricebefortax.ToString("F3")
            ' MsgBox(price & "tax =" & (taxRate + 1))
        Catch
            txtPrice.Text = "0.000"
        End Try
    End Sub
    Private Sub CalculateTotalValuefromdiscountaftertax()
        Try
            '   Dim price As Double = If(String.IsNullOrWhiteSpace(txtPrice.Text), 0, Convert.ToDouble(txtPrice.Text))
            '  Dim quantity As Double = If(String.IsNullOrWhiteSpace(txtQuantity.Text), 0, Convert.ToDouble(txtQuantity.Text))
            Dim fd As Double = If(String.IsNullOrWhiteSpace(TextBox2.Text), 0, Convert.ToDouble(TextBox2.Text))
            Dim taxRate As Double = If(String.IsNullOrWhiteSpace(txtTaxRate.Text), 0, Convert.ToDouble(txtTaxRate.Text))
            '  Dim total As Double = RoundToFiveCents((price * quantity) * ((taxRate / 100) + 1))
            ' Dim PRICE_WITH_TAX As Double = RoundToFiveCents(price * ((taxRate / 100) + 1))

            Dim discount As Double = Convert.ToDouble((fd / ((taxRate / 100) + 1)))
            txtDiscount.Text = discount.ToString("F3")

        Catch
            txtDiscount.Text = "0.000"
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        '  If String.IsNullOrWhiteSpace(txtBarcode.Text) OrElse cboProductType.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtProductName.Text) Then
        ' MessageBox.Show("يرجى تعبئة جميع الحقول", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Return
        '  End If
        If cboProductType.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(txtProductName.Text) Then
            MessageBox.Show("يرجى تعبئة جميع الحقول", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Check if product exists in PRODUCTS, if not, add it
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim cmdCheck As New SQLiteCommand("SELECT COUNT(*) FROM PRODUCTS WHERE BARCODE = @barcode", conn)
            cmdCheck.Parameters.AddWithValue("@barcode", txtBarcode.Text)
            Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
            If exists = 0 Then
                Dim cmdInsert As New SQLiteCommand("INSERT INTO PRODUCTS (BARCODE, PRO_NAME, PRO_PRICE, PRO_TAX_VALUE, PRO_TAX_KIND, PRO_KIND,price_with_tax) VALUES (@barcode, @name, @price, @tax, @taxKind, @kind,@pricewithtax)", conn)
                cmdInsert.Parameters.AddWithValue("@barcode", txtBarcode.Text)
                cmdInsert.Parameters.AddWithValue("@name", txtProductName.Text)
                cmdInsert.Parameters.AddWithValue("@price", Convert.ToDouble(txtPrice.Text))
                cmdInsert.Parameters.AddWithValue("@tax", Convert.ToDouble(txtTaxRate.Text))
                cmdInsert.Parameters.AddWithValue("@taxKind", If(Convert.ToDouble(txtTaxRate.Text) > 0, "ضريبة القيمة المضافة", "معفاة"))
                cmdInsert.Parameters.AddWithValue("@kind", cboProductType.SelectedValue)
                cmdInsert.Parameters.AddWithValue("@pricewithtax", Convert.ToDouble(TextBox1.Text))
                cmdInsert.ExecuteNonQuery()
            End If
        End Using
        LoadAutoComplete()
        ' Add or update in DataGridView
        Dim row = productTable.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("PRO_NAME") = txtProductName.Text)
        If row IsNot Nothing Then
            ' Update existing row
            row("PRO_COUNT") = row.Field(Of Double)("PRO_COUNT") + Convert.ToDouble(txtQuantity.Text)
            row("PRO_DISCOUNT") = row.Field(Of Double)("PRO_DISCOUNT") + Convert.ToDouble(txtDiscount.Text)
            Dim price As Double = Convert.ToDouble(txtPrice.Text)
            Dim quantity As Double = row.Field(Of Double)("PRO_COUNT")
            Dim discount As Double = row.Field(Of Double)("PRO_DISCOUNT")
            Dim taxRate As Double = Convert.ToDouble(txtTaxRate.Text)
            row("FINAL_VALUE") = Convert.ToDouble(txtTotalValue.Text) '(price * quantity - discount) * ((taxRate / 100) + 1)
        Else
            ' Add new row
            productTable.Rows.Add(txtBarcode.Text, cboProductType.SelectedValue, txtProductName.Text, Convert.ToDouble(txtPrice.Text), Convert.ToDouble(txtQuantity.Text), Convert.ToDouble(txtDiscount.Text), Convert.ToDouble(txtTaxRate.Text), Convert.ToDouble(txtTotalValue.Text))
        End If

        UpdateSummary()
        ClearProductEntry()
    End Sub

    Private Sub ClearProductEntry()
        txtBarcode.Clear()
        cboProductType.SelectedIndex = -1
        txtProductName.Clear()
        txtPrice.Clear()
        txtQuantity.Text = "1"
        txtDiscount.Text = "0"
        txtTaxRate.Clear()
        txtTotalValue.Clear()
        txtBarcode.Focus()
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub UpdateSummary()
        Dim totalPrice As Double = 0
        Dim totalDiscount As Double = 0
        Dim totalTax As Double = 0
        Dim grandTotal As Double = 0

        For Each row As DataRow In productTable.Rows
            Dim price As Double = row.Field(Of Double)("PRO_PRICE")
            Dim quantity As Double = row.Field(Of Double)("PRO_COUNT")
            Dim discount As Double = row.Field(Of Double)("PRO_DISCOUNT")
            Dim taxRate As Double = row.Field(Of Double)("PRO_TX_PUBLIC")
            Dim valueBeforeTax As Double = price * quantity - discount
            Dim taxValue As Double = valueBeforeTax * (taxRate / 100)
            Dim finalValue As Double = row.Field(Of Double)("final_value") 'valueBeforeTax + taxValue

            totalPrice += price * quantity
            totalDiscount += discount
            totalTax += taxValue
            grandTotal += finalValue
        Next

        txtTotalPrice.Text = totalPrice.ToString("F3")
        txtTotalDiscount.Text = totalDiscount.ToString("F3")
        txtTotalTax.Text = totalTax.ToString("F3")
        txtGrandTotal.Text = grandTotal.ToString("F3")
    End Sub

    Private Sub grdProducts_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdProducts.CellEndEdit
        If e.RowIndex >= 0 Then
            Dim row As DataRow = productTable.Rows(e.RowIndex)
            Dim price As Double = row.Field(Of Double)("PRO_PRICE")
            Dim quantity As Double = row.Field(Of Double)("PRO_COUNT")
            Dim discount As Double = row.Field(Of Double)("PRO_DISCOUNT")
            Dim taxRate As Double = row.Field(Of Double)("PRO_TX_PUBLIC")
            row("FINAL_VALUE") = RoundToFiveCents((price * quantity - discount) * ((taxRate / 100) + 1)).ToString("F3")
            UpdateSummary()
        End If
    End Sub

    Private Sub grdProducts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdProducts.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = grdProducts.Columns("Delete").Index Then
            productTable.Rows(e.RowIndex).Delete()
            UpdateSummary()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MessageBox.Show("هل تريد إلغاء الفاتورة والبدء من جديد؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ResetForm()
        End If
    End Sub

    Public Sub ResetForm()
        productTable.Clear()
        ClearProductEntry()
        UpdateSummary()
        currentInvoiceId = Nothing
        '  btnPrint.Enabled = False
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        If currentInvoiceId.HasValue Then
            MessageBox.Show($"طباعة الفاتورة رقم: {currentInvoiceId.Value}", "طباعة")
        Else
            MessageBox.Show("لا توجد فاتورة لحفظها أو طباعتها", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If productTable.Rows.Count = 0 Then
            MessageBox.Show("يرجى إضافة مواد إلى الفاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim dialog As New CustomerInvoiceDialog()
        dialog.Owner = Me ' Explicitly set the Owner
        If dialog.ShowDialog() = DialogResult.OK Then
            currentInvoiceId = dialog.InvoiceId
            'btnPrint.Enabled = True
            ResetForm()
        End If
    End Sub

    Private Sub PAY_POS_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Form1.Show()
    End Sub

    Private Sub pnlProductEntry_Paint(sender As Object, e As PaintEventArgs) Handles pnlProductEntry.Paint

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateDateTime()
    End Sub



    Private Sub txtBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcode.KeyPress
        If e.KeyChar = Chr(13) Then
            getbarcode()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        CalculateTotalValuefromdiscountaftertax()
    End Sub



    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            CalculateTotalValuefrompriceaftertax()
        End If
    End Sub

    Private Sub txtBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged

    End Sub

    Private Sub txtProductName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductName.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the Enter key from adding a new line
            AutofillProductDetails(txtProductName.Text.Trim())
        End If
    End Sub
    Private Sub LoadAutoComplete()
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            Try

                conn.Open()
                ' Ensure connection is open
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                ' Create a collection for autocomplete suggestions
                Dim collection As New AutoCompleteStringCollection()
                Using cmd As New SQLiteCommand("SELECT PRO_NAME FROM PRODUCTS", conn)
                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            collection.Add(reader("PRO_NAME").ToString())
                        End While
                    End Using
                End Using

                ' Configure TextBox1 for autocomplete
                txtProductName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource
                txtProductName.AutoCompleteCustomSource = collection
            Catch ex As Exception
                MessageBox.Show("Error loading autocomplete: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub
    Private Sub AutofillProductDetails(searchText As String)
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            Try
                ' Ensure connection is open
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                ' Use parameterized query to avoid SQL injection
                Dim query As String = "SELECT PRO_NAME, PRO_PRICE, PRO_TAX_VALUE, PRO_KIND, price_with_tax FROM PRODUCTS WHERE PRO_NAME = @searchText"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@searchText", searchText)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Populate form fields with data
                            txtProductName.Text = reader("PRO_NAME").ToString()
                            txtPrice.Text = reader("PRO_PRICE").ToString()
                            txtTaxRate.Text = reader("PRO_TAX_VALUE").ToString()
                            cboProductType.SelectedValue = reader("PRO_KIND").ToString()
                            TextBox1.Text = reader("price_with_tax").ToString()
                            txtQuantity.Text = "1"
                            txtDiscount.Text = "0"
                            CalculateTotalValue()
                            txtQuantity.Focus()
                        Else
                            ' Clear fields if no product is found
                            txtProductName.Clear()
                            txtPrice.Clear()
                            txtTaxRate.Clear()
                            cboProductType.SelectedIndex = 0
                            txtQuantity.Text = "1"
                            txtDiscount.Text = "0"
                            txtTotalValue.Clear()
                            txtProductName.Focus()
                            MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error retrieving product data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub
End Class

' CustomerInvoiceDialog.vb (Nested Dialog Form for Customer and Invoice Details)
' ----------------------------------------------------------------------------

Public Class CustomerInvoiceDialog
    Inherits Form

    Private WithEvents cboCustomer As New ComboBox()
    Private WithEvents txtCustomerName As New TextBox()
    Private WithEvents txtCustomerPhone As New TextBox()
    Private WithEvents cboCustomerCity As New ComboBox()
    Private WithEvents cboCustomerIdType As New ComboBox()
    Private WithEvents txtCustomerPostcode As New TextBox()
    Private WithEvents cboPayKind As New ComboBox()
    Private WithEvents cboInvoiceKind As New ComboBox()
    Private WithEvents cboCurrency As New ComboBox()
    Private WithEvents btnCancel As New Button()
    ' Private WithEvents btnPrint As New Button()
    Private WithEvents btnSave As New Button()
    Private WithEvents btnExport As New Button()
    Private lblCustomer As New Label()
    Private lblCustomerName As New Label()
    Private lblCustomerPhone As New Label()
    Private lblCustomerCity As New Label()
    Private lblCustomerIdType As New Label()
    Private lblCustomerPostcode As New Label()
    Private lblPayKind As New Label()
    Private lblInvoiceKind As New Label()
    Private lblCurrency As New Label()
    Private pnlButtons As New Panel()
    Public Property InvoiceId As Long? = Nothing

    Public Sub New()
        InitializeComponents()
    End Sub

    Private Sub InitializeComponents()
        Me.Size = New Size(600, 500)
        Me.Text = "بيانات العميل والفاتورة"
        Me.RightToLeft = RightToLeft.Yes

        lblCustomer.Text = "العميل"
        lblCustomer.Location = New Point(450, 20)
        lblCustomer.AutoSize = True
        Me.Controls.Add(lblCustomer)

        cboCustomer.Location = New Point(250, 20)
        cboCustomer.Size = New Size(180, 21)
        cboCustomer.DropDownStyle = ComboBoxStyle.DropDown
        cboCustomer.AutoCompleteSource = AutoCompleteSource.ListItems
        Me.Controls.Add(cboCustomer)

        lblCustomerName.Text = "اسم العميل"
        lblCustomerName.Location = New Point(450, 60)
        lblCustomerName.AutoSize = True
        Me.Controls.Add(lblCustomerName)

        txtCustomerName.Location = New Point(250, 60)
        txtCustomerName.Size = New Size(180, 20)
        txtCustomerName.Enabled = False
        Me.Controls.Add(txtCustomerName)

        lblCustomerPhone.Text = "رقم الهاتف"
        lblCustomerPhone.Location = New Point(450, 100)
        lblCustomerPhone.AutoSize = True
        Me.Controls.Add(lblCustomerPhone)

        txtCustomerPhone.Location = New Point(250, 100)
        txtCustomerPhone.Size = New Size(180, 20)
        txtCustomerPhone.Enabled = False
        Me.Controls.Add(txtCustomerPhone)

        lblCustomerCity.Text = "المدينة"
        lblCustomerCity.Location = New Point(450, 140)
        lblCustomerCity.AutoSize = True
        Me.Controls.Add(lblCustomerCity)

        cboCustomerCity.Location = New Point(250, 140)
        cboCustomerCity.Size = New Size(180, 21)
        cboCustomerCity.DropDownStyle = ComboBoxStyle.DropDownList
        cboCustomerCity.Enabled = False
        Me.Controls.Add(cboCustomerCity)

        lblCustomerIdType.Text = "نوع الهوية"
        lblCustomerIdType.Location = New Point(450, 180)
        lblCustomerIdType.AutoSize = True
        Me.Controls.Add(lblCustomerIdType)

        cboCustomerIdType.Location = New Point(250, 180)
        cboCustomerIdType.Size = New Size(180, 21)
        cboCustomerIdType.DropDownStyle = ComboBoxStyle.DropDownList
        cboCustomerIdType.Enabled = False
        Me.Controls.Add(cboCustomerIdType)

        lblCustomerPostcode.Text = "الرمز البريدي"
        lblCustomerPostcode.Location = New Point(450, 220)
        lblCustomerPostcode.AutoSize = True
        Me.Controls.Add(lblCustomerPostcode)

        txtCustomerPostcode.Location = New Point(250, 220)
        txtCustomerPostcode.Size = New Size(180, 20)
        txtCustomerPostcode.Enabled = False
        Me.Controls.Add(txtCustomerPostcode)

        lblPayKind.Text = "طريقة الدفع"
        lblPayKind.Location = New Point(450, 300)
        lblPayKind.AutoSize = True
        Me.Controls.Add(lblPayKind)

        cboPayKind.Location = New Point(250, 300)
        cboPayKind.Size = New Size(180, 21)
        cboPayKind.DropDownStyle = ComboBoxStyle.DropDownList
        Me.Controls.Add(cboPayKind)

        lblInvoiceKind.Text = "نوع الفاتورة"
        lblInvoiceKind.Location = New Point(450, 340)
        lblInvoiceKind.AutoSize = True
        Me.Controls.Add(lblInvoiceKind)

        cboInvoiceKind.Location = New Point(250, 340)
        cboInvoiceKind.Size = New Size(180, 21)
        cboInvoiceKind.DropDownStyle = ComboBoxStyle.DropDownList
        Me.Controls.Add(cboInvoiceKind)

        lblCurrency.Text = "العملة"
        lblCurrency.Location = New Point(450, 380)
        lblCurrency.AutoSize = True
        Me.Controls.Add(lblCurrency)

        cboCurrency.Location = New Point(250, 380)
        cboCurrency.Size = New Size(180, 21)
        cboCurrency.DropDownStyle = ComboBoxStyle.DropDownList
        Me.Controls.Add(cboCurrency)

        pnlButtons.Dock = DockStyle.Bottom
        pnlButtons.Size = New Size(600, 50)
        Me.Controls.Add(pnlButtons)

        btnCancel.Text = "إلغاء"
        btnCancel.Location = New Point(500, 10)
        btnCancel.Size = New Size(80, 25)
        pnlButtons.Controls.Add(btnCancel)

        ' btnPrint.Text = "طباعة"
        ' btnPrint.Location = New Point(400, 10)
        ' btnPrint.Size = New Size(80, 25)
        ' pnlButtons.Controls.Add(btnPrint)

        btnSave.Text = "حفظ محلي"
        btnSave.Location = New Point(350, 10)
        btnSave.Size = New Size(80, 25)
        pnlButtons.Controls.Add(btnSave)

        btnExport.Text = "حفظ إلى فوترة"
        btnExport.Location = New Point(50, 10)
        btnExport.Size = New Size(200, 25)
        '  btnExport.Enabled = False
        pnlButtons.Controls.Add(btnExport)

        LoadCustomers()
        LoadInvoiceDetails()
    End Sub

    Private Sub LoadCustomers()
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim adapter As New SQLiteDataAdapter("SELECT ID, C_NAME FROM CUSTOMERS", conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            ' Dim cashCustomerRow As DataRow = table.NewRow()
            ' cashCustomerRow("ID") = 0
            ' cashCustomerRow("C_NAME") = PAY_POS.CASH_CUSTOMER
            ' table.Rows.InsertAt(cashCustomerRow, 0)
            Dim newCustomerRow As DataRow = table.NewRow()
            newCustomerRow("ID") = -1
            newCustomerRow("C_NAME") = PAY_POS.NEW_CUSTOMER
            table.Rows.InsertAt(newCustomerRow, 1)
            cboCustomer.DataSource = table
            cboCustomer.DisplayMember = "C_NAME"
            cboCustomer.ValueMember = "ID"
            cboCustomer.SelectedIndex = 0

            Dim cityAdapter As New SQLiteDataAdapter("SELECT ID, TXT FROM CITY_TABLE", conn)
            Dim cityTable As New DataTable()
            cityAdapter.Fill(cityTable)
            cboCustomerCity.DataSource = cityTable
            cboCustomerCity.DisplayMember = "TXT"
            cboCustomerCity.ValueMember = "ID"

            Dim idTypeAdapter As New SQLiteDataAdapter("SELECT ID, TXT FROM ID_CUSTOMER", conn)
            Dim idTypeTable As New DataTable()
            idTypeAdapter.Fill(idTypeTable)
            cboCustomerIdType.DataSource = idTypeTable
            cboCustomerIdType.DisplayMember = "TXT"
            cboCustomerIdType.ValueMember = "ID"
        End Using
    End Sub

    Private Sub LoadInvoiceDetails()
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim payKindAdapter As New SQLiteDataAdapter("SELECT ID, TXT FROM INV_PAY_KIND", conn)
            Dim payKindTable As New DataTable()
            payKindAdapter.Fill(payKindTable)
            cboPayKind.DataSource = payKindTable
            cboPayKind.DisplayMember = "TXT"
            cboPayKind.ValueMember = "ID"

            Dim invoiceKindAdapter As New SQLiteDataAdapter("SELECT ID, TXT FROM IN_KIND", conn)
            Dim invoiceKindTable As New DataTable()
            invoiceKindAdapter.Fill(invoiceKindTable)
            cboInvoiceKind.DataSource = invoiceKindTable
            cboInvoiceKind.DisplayMember = "TXT"
            cboInvoiceKind.ValueMember = "ID"

            Dim currencyAdapter As New SQLiteDataAdapter("SELECT ID, C_CODE FROM CURRENCY_TABLE", conn)
            Dim currencyTable As New DataTable()
            currencyAdapter.Fill(currencyTable)
            cboCurrency.DataSource = currencyTable
            cboCurrency.DisplayMember = "C_CODE"
            cboCurrency.ValueMember = "ID"
            cboCurrency.SelectedValue = currencyTable.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("C_CODE") = "JO")?("ID")
        End Using
    End Sub

    Private Sub cboCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomer.SelectedIndexChanged
        Try


            Dim selectedRow As DataRowView = TryCast(cboCustomer.SelectedItem, DataRowView)
            Dim isNewCustomer As Boolean = selectedRow IsNot Nothing AndAlso Convert.ToInt32(selectedRow("ID")) = -1 ' زبون جديد
            txtCustomerName.Enabled = isNewCustomer
            txtCustomerPhone.Enabled = isNewCustomer
            cboCustomerCity.Enabled = isNewCustomer
            cboCustomerIdType.Enabled = isNewCustomer
            txtCustomerPostcode.Enabled = isNewCustomer
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    ' Update in PAY_POS.vb to open InvoicePrintForm
    ' Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    'If InvoiceId.HasValue Then
    'Dim printForm As New InvoicePrintForm(InvoiceId.Value)
    '        printForm.ShowDialog()
    'Else
    '        MessageBox.Show("لا توجد فاتورة لحفظها أو طباعتها", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    ' End If
    '  End Sub


    ' Update in CustomerInvoiceDialog.vb: btnSave_Click to add null checks
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        saveinvo()
        Dim K As New InvoicePrintForm(InvoiceId.Value, False)
        DirectCast(Me.Owner, PAY_POS).ResetForm()
        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub
    Private Sub saveinvo()
        Dim customerId As Long
        If cboCustomer.SelectedValue = 0 Then ' زبون نقدي
            customerId = 0
        ElseIf cboCustomer.SelectedValue = -1 Then ' زبون جديد
            If String.IsNullOrWhiteSpace(txtCustomerName.Text) OrElse String.IsNullOrWhiteSpace(txtCustomerPhone.Text) Then
                MessageBox.Show("يرجى تعبئة بيانات العميل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
                conn.Open()
                Dim cmd As New SQLiteCommand("INSERT INTO CUSTOMERS (E_ID, C_NAME, C_TEL, CU_CITY, C_POSTCODE) VALUES (@eid, @name, @tel, @city, @postcode)", conn)
                cmd.Parameters.AddWithValue("@eid", cboCustomerIdType.SelectedValue)
                cmd.Parameters.AddWithValue("@name", txtCustomerName.Text)
                cmd.Parameters.AddWithValue("@tel", txtCustomerPhone.Text)
                cmd.Parameters.AddWithValue("@city", cboCustomerCity.SelectedValue)
                cmd.Parameters.AddWithValue("@postcode", If(String.IsNullOrWhiteSpace(txtCustomerPostcode.Text), DBNull.Value, txtCustomerPostcode.Text))
                cmd.ExecuteNonQuery()
                customerId = conn.LastInsertRowId
            End Using
        Else
            customerId = Convert.ToInt64(cboCustomer.SelectedValue)
        End If

        ' Save invoice
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim trans = conn.BeginTransaction()
            Try
                Dim totalPrice As Double = 0
                Dim totalDiscount As Double = 0
                Dim totalTax As Double = 0
                Dim grandTotal As Double = 0

                Dim parentForm As PAY_POS = TryCast(Me.Owner, PAY_POS)
                If parentForm Is Nothing OrElse parentForm.grdProducts Is Nothing OrElse parentForm.grdProducts.Rows Is Nothing Then
                    MessageBox.Show("خطأ في الوصول إلى بيانات الفاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    trans.Rollback()
                    Return
                End If

                For Each row As DataGridViewRow In parentForm.grdProducts.Rows
                    Dim price As Double = row.Cells("PRO_PRICE").Value
                    Dim quantity As Double = row.Cells("PRO_COUNT").Value
                    Dim discount As Double = row.Cells("PRO_DISCOUNT").Value
                    Dim taxRate As Double = row.Cells("PRO_TX_PUBLIC").Value
                    Dim valueBeforeTax As Double = price * quantity - discount
                    Dim taxValue As Double = valueBeforeTax * (taxRate / 100)
                    Dim finalValue As Double = valueBeforeTax + taxValue

                    totalPrice += price * quantity
                    totalDiscount += discount
                    totalTax += taxValue
                    grandTotal += finalValue
                Next

                Dim cmdInvoice As New SQLiteCommand("INSERT INTO INVOICES (INV_PAY_KIND, INV_KIND, USER_NAME, C_ID, REMARK_KIND, E_CODE, E_BARCODE, DATE_TIME, INV_VALUE, DISCOUNT_VALUE, ITEM_VALUE, TAX_VALUE, CURRENCY_KIND, INV_OUT_IN) " &
                                              "VALUES (@payKind, @invKind, @user, @cid, @remark, @ecode, @ebarcode, @date, @invValue, @discount, @itemValue, @taxValue, @currency, @outIn)", conn)
                cmdInvoice.Parameters.AddWithValue("@payKind", cboPayKind.SelectedValue)
                cmdInvoice.Parameters.AddWithValue("@invKind", cboInvoiceKind.SelectedValue)
                cmdInvoice.Parameters.AddWithValue("@user", "Cashier")
                cmdInvoice.Parameters.AddWithValue("@cid", customerId)
                cmdInvoice.Parameters.AddWithValue("@remark", 1) ' Default remark
                cmdInvoice.Parameters.AddWithValue("@ecode", DBNull.Value)
                cmdInvoice.Parameters.AddWithValue("@ebarcode", DBNull.Value)
                cmdInvoice.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmdInvoice.Parameters.AddWithValue("@invValue", grandTotal)
                cmdInvoice.Parameters.AddWithValue("@discount", totalDiscount)
                cmdInvoice.Parameters.AddWithValue("@itemValue", totalPrice)
                cmdInvoice.Parameters.AddWithValue("@taxValue", totalTax)
                cmdInvoice.Parameters.AddWithValue("@currency", cboCurrency.SelectedValue)
                cmdInvoice.Parameters.AddWithValue("@outIn", 1) ' فاتورة مبيعات
                cmdInvoice.ExecuteNonQuery()

                InvoiceId = conn.LastInsertRowId

                For Each row As DataGridViewRow In parentForm.grdProducts.Rows
                    Dim cmdOrder As New SQLiteCommand("INSERT INTO ORDER_PRODUCTS (PRO_KIND, PRO_NAME, PRO_COUNT, PRO_PRICE, PRO_DISCOUNT, PRO_TX_PUBLIC, PRO_TAX, VALUE_AFTER_DISCOUNT, VALUE_OF_TAX, VALUE_ORGINAL, FINAL_VALUE, INV_ID) " &
                                                "VALUES (@kind, @name, @count, @price, @discount, @txPublic, @tax, @valueAfter, @valueTax, @valueOrg, @final, @invId)", conn)
                    Dim price As Double = row.Cells("PRO_PRICE").Value
                    Dim quantity As Double = row.Cells("PRO_COUNT").Value
                    Dim discount As Double = row.Cells("PRO_DISCOUNT").Value
                    Dim taxRate As Double = row.Cells("PRO_TX_PUBLIC").Value
                    Dim valueOriginal As Double = price * quantity
                    Dim valueAfterDiscount As Double = valueOriginal - discount
                    Dim taxValue As Double = valueAfterDiscount * (taxRate / 100)
                    Dim finalValue As Double = valueAfterDiscount + taxValue

                    cmdOrder.Parameters.AddWithValue("@kind", row.Cells("PRO_KIND").Value)
                    cmdOrder.Parameters.AddWithValue("@name", row.Cells("PRO_NAME").Value)
                    cmdOrder.Parameters.AddWithValue("@count", quantity)
                    cmdOrder.Parameters.AddWithValue("@price", price)
                    cmdOrder.Parameters.AddWithValue("@discount", discount)
                    cmdOrder.Parameters.AddWithValue("@txPublic", taxRate)
                    cmdOrder.Parameters.AddWithValue("@tax", taxValue)
                    cmdOrder.Parameters.AddWithValue("@valueAfter", valueAfterDiscount)
                    cmdOrder.Parameters.AddWithValue("@valueTax", taxValue)
                    cmdOrder.Parameters.AddWithValue("@valueOrg", valueOriginal)
                    cmdOrder.Parameters.AddWithValue("@final", finalValue)
                    cmdOrder.Parameters.AddWithValue("@invId", InvoiceId)
                    cmdOrder.ExecuteNonQuery()
                Next

                trans.Commit()
                btnExport.Enabled = True
                '  MessageBox.Show($"تم حفظ الفاتورة بنجاح، رقم الفاتورة: {InvoiceId}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show($"خطأ أثناء حفظ الفاتورة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If Not InvoiceId.HasValue Then
            saveinvo()
        End If

        ' Simulate UBL 2.1 XML generation and API call to Jordanian tax authority
        ' Dim ublXml As String = GenerateUBLXml(InvoiceId.Value)
        ' Simulate sending to tax authority and getting response
        ' Dim eCode As String = "TAX" & InvoiceId.Value.ToString("D8")
        '  Dim eBarcode As String = "BAR" & InvoiceId.Value.ToString("D8")

        ' Update invoice with E_CODE and E_BARCODE
        '  Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
        '  conn.Open()
        '  Dim cmd As New SQLiteCommand("UPDATE INVOICES SET E_CODE = @ecode, E_BARCODE = @ebarcode WHERE ID = @id", conn)
        ' cmd.Parameters.AddWithValue("@ecode", eCode)
        ' cmd.Parameters.AddWithValue("@ebarcode", eBarcode)
        ' cmd.Parameters.AddWithValue("@id", InvoiceId.Value)
        ' cmd.ExecuteNonQuery()
        ' End Using
        Dim OO As New send_invoice_form(InvoiceId.Value)
        OO.ShowDialog()
        ' MessageBox.Show($"تم تصدير الفاتورة بنجاح. رقم الفاتورة الضريبي: {eCode}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Dim K As New InvoicePrintForm(InvoiceId.Value, False)
        '  K.LoadInvoiceData()
        '  K.STRART_PRINT()
        ' MessageBox.Show($"طباعة الفاتورة رقم: {InvoiceId.Value}", "طباعة")

        DirectCast(Me.Owner, PAY_POS).ResetForm()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Function GenerateUBLXml(invoiceId As Long) As String
        ' Simplified UBL 2.1 XML generation (placeholder)
        Dim sb As New StringBuilder()
        sb.AppendLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
        sb.AppendLine("<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"">")
        sb.AppendLine($"<cbc:ID>{invoiceId}</cbc:ID>")
        sb.AppendLine("<cbc:IssueDate>" & DateTime.Now.ToString("yyyy-MM-dd") & "</cbc:IssueDate>")
        ' Add more UBL elements as required by Jordanian tax authority
        sb.AppendLine("</Invoice>")
        Return sb.ToString()
    End Function
End Class