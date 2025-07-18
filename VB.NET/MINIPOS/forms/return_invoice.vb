Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.Net.Http
Imports System.Text
Imports jofotaratoolkit
Public Class return_invoice
    Private conn As SQLiteConnection = New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
    Dim id As Integer '= reader.GetInt32(0) ' ID
    Dim companyName As String ' = reader.GetString(1) ' COMPANY_NAME
    Dim companyTel As String '= If(reader.IsDBNull(2), "", reader.GetString(2)) ' COMPANY_TEL (may be null)
    Dim companyTaxNo As String ' = If(reader.IsDBNull(3), "", reader.GetString(3)) ' COMPANY_TAX_NO (may be null)
    Dim companyAddress As String ' = If(reader.IsDBNull(4), "", reader.GetString(4)) ' COMPANY_ADDRESS (may be null)
    '   companyLogo = If(reader.IsDBNull(5), Nothing, DirectCast(reader(5), Byte())) ' COMPANY_LOGO (BLOB)
    Dim posKind As Integer '= reader.GetInt32(6) ' POS_KIND
    Dim key0 As String '= If(reader.IsDBNull(7), "", reader.GetString(7)) ' key0 (may be null)
    Dim key1 As String ' = If(reader.IsDBNull(8), "", reader.GetString(8)) ' key1 (may be null)

    Dim custId As Integer '= readerCustomer.GetInt32(0) ' ID
    Dim eId As Integer '= readerCustomer.GetInt32(1) ' E_ID
    Dim cName As String '= readerCustomer.GetString(2) ' C_NAME
    Dim cTel As String '= If(readerCustomer.IsDBNull(3), "", readerCustomer.GetString(3)) ' C_TEL (may be null)
    Dim cTaxCode As String ' = If(readerCustomer.IsDBNull(4), "", readerCustomer.GetString(4)) ' C_TAX_CODE (may be null)
    Dim cuCity As Integer ' = readerCustomer.GetInt32(5) ' CU_CITY
    Dim cPostcode As String '= If(readerCustomer.IsDBNull(6), "", readerCustomer.GetString(6)) ' C_POSTCODE (may be null)

    Dim id1 As Integer '= reader.GetInt32(0) ' i.ID
    Dim dateTime1 As String ' = reader.GetString(1) ' i.DATE_TIME
    Dim returnvalue As Double

    Dim eCode As String '= reader.GetString(4) ' i.E_CODE
    Dim invValue As Double ' = reader.GetDouble(5) ' i.INV_VALUE
    Dim taxValue As Double '= reader.GetDouble(6) ' i.TAX_VALUE
    Dim invKind As String '= reader.GetString(7) ' ik.TXT (INV_KIND)
    Dim invPayKind As String ' = reader.GetString(8) ' ipk.TXT (INV_PAY_KIND)
    Dim invOutIn As String '= reader.GetString(9) ' ioi.TXT (INV_OUT_IN)
    Dim INUIDD As String ' = reader.GetString(10)

    Dim tax_source_nu As String
    Public O_ID As Long
    Private ReadOnly client As New HttpClient()
    Private Const API_URL As String = "https://backend.jofotara.gov.jo/core/invoices/"
    Private Const QR_FOLDER As String = "QR_Codes"
    Private Const SCHEMA_PATH As String = "Schemas\UBL-Invoice-2.1.xsd"
    Public Seller As Sellersys
    Public Buyer As Buyersys
    Public InvoiceType As InvoiceTypesys
    Public items As Itemsys()
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        DataGridView1.Rows.Clear()
        Dim i As Long = BarCodeControl1.Text
        RetrieveInvoiceDetails(i)
    End Sub
    Private Sub RetrieveInvoiceDetails(invId As Long)
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            Try
                conn.Open()

                ' Build the query
                Dim query As New StringBuilder("SELECT i.ID, i.DATE_TIME, c.C_NAME, c.C_TAX_CODE, i.E_CODE, i.INV_VALUE, i.TAX_VALUE, " &
                                              "ik.TXT AS INV_KIND, ipk.TXT AS INV_PAY_KIND, ioi.TXT AS INV_OUT_IN ,i.E_UUID,c.C_TEL,c.CU_CITY,c.C_POSTCODE " &
                                              "FROM INVOICES i " &
                                              "LEFT JOIN CUSTOMERS c ON i.C_ID = c.ID " &
                                              "LEFT JOIN IN_KIND ik ON i.INV_KIND = ik.ID " &
                                              "LEFT JOIN INV_PAY_KIND ipk ON i.INV_PAY_KIND = ipk.ID " &
                                              "LEFT JOIN INV_OUT_IN ioi ON i.INV_OUT_IN = ioi.ID " &
                                              "WHERE i.id=" & invId) ' You can add WHERE conditions here, e.g., " AND i.ID = 1"

                Using cmd As New SQLiteCommand(query.ToString(), conn)
                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Store each column value in a variable
                            id1 = reader.GetInt32(0) ' i.ID
                            DateTime1 = reader.GetString(1) ' i.DATE_TIME
                            cName = reader.GetString(2) ' c.C_NAME
                            cTaxCode = If(reader.IsDBNull(3), "", reader.GetString(3)) ' c.C_TAX_CODE (may be null)
                            eCode = If(reader.IsDBNull(4), "", reader.GetString(4)) ' i.E_CODE
                            invValue = reader.GetDouble(5) ' i.INV_VALUE
                            taxValue = reader.GetDouble(6) ' i.TAX_VALUE
                            invKind = reader.GetString(7) ' ik.TXT (INV_KIND)
                            invPayKind = reader.GetString(8) ' ipk.TXT (INV_PAY_KIND)
                            invOutIn = reader.GetString(9) ' ioi.TXT (INV_OUT_IN)
                            INUIDD = If(reader.IsDBNull(10), "", reader.GetString(10))
                            '''set customer data
                            If String.IsNullOrEmpty(INUIDD) Then
                                HopeButton1.Enabled = False
                            Else
                                HopeButton1.Enabled = True
                            End If

                            cTel = If(reader.IsDBNull(11), "", reader.GetString(11)) ' C_TEL (may be null)

                            cuCity = reader.GetInt32(12) ' CU_CITY
                            cPostcode = If(reader.IsDBNull(13), "", reader.GetString(13)) ' C_POSTCODE (may be null)


                            Label1.Text = "التاريخ : " & dateTime1
                            Label2.Text = "العميل : " & cName
                            Label3.Text = "قيمة الفاتورة : " & invValue
                            Label4.Text = INUIDD
                            Dim productsQuery As String = "SELECT * FROM ORDER_PRODUCTS WHERE INV_ID = @invId"
                            Using cmdProducts As New SQLiteCommand(productsQuery, conn)
                                cmdProducts.Parameters.AddWithValue("@invId", invId)

                                Using readerProducts As SQLiteDataReader = cmdProducts.ExecuteReader()
                                    Dim productList As New List(Of String) ' To store product details (example)
                                    While readerProducts.Read()


                                        Dim prodId As Integer = readerProducts.GetInt32(0) ' ID
                                        Dim proKind As Integer = readerProducts.GetInt32(1) ' PRO_KIND
                                        Dim proName As String = readerProducts.GetString(2) ' PRO_NAME
                                        Dim proCount As Integer = readerProducts.GetInt32(3) ' PRO_COUNT
                                        Dim proPrice As Double = readerProducts.GetDouble(4) ' PRO_PRICE
                                        Dim proDiscount As Double = readerProducts.GetDouble(5) ' PRO_DISCOUNT
                                        Dim proTxPublic As Double = readerProducts.GetDouble(6) ' PRO_TX_PUBLIC
                                        Dim proTax As Double = readerProducts.GetDouble(7) ' PRO_TAX
                                        Dim valueAfterDiscount As Double = readerProducts.GetDouble(8) ' VALUE_AFTER_DISCOUNT
                                        Dim valueOfTax As Double = readerProducts.GetDouble(9) ' VALUE_OF_TAX
                                        Dim valueOriginal As Double = readerProducts.GetDouble(10) ' VALUE_ORGINAL
                                        Dim finalValue As Double = readerProducts.GetDouble(11) ' FINAL_VALUE
                                        Dim invIdRef As Integer = readerProducts.GetInt32(12) ' INV_ID

                                        Dim newItem As New Itemsys(
                                             itemId:=Guid.NewGuid().ToString(), ' Generate UUID
                                             quantity:=proCount.ToString(), ' Map p_count to Quantity
                                             subtotal:=finalValue.ToString(), ' Map p_total to Subtotal
                                             vatTax:=valueOfTax.ToString, ' Default
                                            specialTax:="0.00000", ' Default
                                            unitPrice:=proPrice.ToString(), ' Map p_price to UnitPrice
                                            discount:=proDiscount, ' Default
                                            description:=proName.ToString(), ' Map nots_p to Description
                                            isic4:=prodId.ToString() ' Map p_id to Isic4
                )
                                        If DataGridView1.Columns.Count = 1 Then
                                            With DataGridView1
                                                .Columns.Add("ItemId", "Item ID")
                                                .Columns.Add("Description", "Item Name")
                                                .Columns.Add("UnitPrice", "Unit Price")
                                                .Columns.Add("Discount", "Discount")
                                                .Columns.Add("VatTax", "VAT Tax")
                                                .Columns.Add("SpecialTax", "Special Tax")
                                                .Columns.Add("Quantity", "Quantity")
                                                .Columns.Add("Subtotal", "Subtotal")
                                            End With
                                        End If

                                        ' Add a new row to the DataGridView
                                        DataGridView1.Rows.Add(
                                            0,
                                            newItem.ItemId,
                                            newItem.Description,
                                            Convert.ToDouble(newItem.UnitPrice),
                                            Convert.ToDouble(newItem.Discount),
                                            Convert.ToDouble(newItem.VatTax),
                                            Convert.ToDouble(newItem.SpecialTax),
                                             Convert.ToInt32(newItem.Quantity),
                                            Convert.ToDouble(newItem.Subtotal)
                                        )
                                        UpdateSubtotalSum()
                                        ' itemsS.Add(newItem)
                                        ' AddItemToDataTable(newItem)
                                        ' Store or process product data (e.g., add to a list or display)
                                        ' productList.Add($"Product: {proName}, Count: {proCount}, Price: {proPrice}")
                                    End While

                                    '  items = itemsS.ToArray
                                    ' Example: Display or use the data
                                    '  MessageBox.Show($"Invoice ID: {invId}{Environment.NewLine}Customer: {cName}{Environment.NewLine}Products:{Environment.NewLine}{String.Join(Environment.NewLine, productList)}", "Invoice Details")
                                End Using
                            End Using
                            ' Use the variables (e.g., display or process)
                            '  add_debug($"ID: {id}, DateTime: {dateTime}, Customer Name: {cName}, Tax Code: {cTaxCode}, " &
                            '     $"E_Code: {eCode}, Invoice Value: {invValue}, Tax Value: {taxValue}, " &
                            '      $"Invoice Kind: {invKind}, Payment Kind: {invPayKind}, Out/In: {invOutIn}")
                        Else
                            ' add_debug("No records found.")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' add_debug("Error retrieving invoice details: " & ex.Message)
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            If DataGridView1 IsNot Nothing AndAlso e.RowIndex >= 0 Then
                If e.ColumnIndex = 0 Then
                    ' Commit the checkbox change immediately
                    DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)

                    ' Update the sum
                    UpdateSubtotalSum()
                End If
            Else
                ' add_debug("DataGridView1 is null or invalid row index.")
            End If
        Catch ex As Exception
            ' add_debug("Error in CellContentClick: " & ex.Message)
        End Try
    End Sub
    Private Sub UpdateSubtotalSum()
        Try
            Dim total As Double = 0
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(0).Value IsNot Nothing AndAlso CBool(row.Cells(0).Value) Then
                    If Not row.Cells("Subtotal").Value Is Nothing AndAlso IsNumeric(row.Cells("Subtotal").Value) Then
                        total += Convert.ToDouble(row.Cells("Subtotal").Value)
                    End If
                End If
            Next
            returnvalue = total.ToString("N2")
            Label7.Text = total.ToString("N2") ' Display with 2 decimal places
        Catch ex As Exception
            ' add_debug("Error updating subtotal sum: " & ex.Message)
            Label7.Text = "0.00"
        End Try
    End Sub
    Private Sub AddItemToDataTable(newItem As Itemsys)
        Try
            ' Create a new row in the DataTable
            Dim itemRow = InvoiceTool1.InvoiceItems.NewRow()

            ' Map properties to DataTable columns
            itemRow("ItemName") = newItem.Description ' Description as ItemName
            itemRow("ItemNo") = newItem.Isic4 ' Isic4 as ItemNo
            itemRow("ItemPrice") = Convert.ToDouble(newItem.UnitPrice) ' UnitPrice as ItemPrice
            itemRow("Discount") = Convert.ToDouble(newItem.Discount) ' Discount
            itemRow("VatTax") = Convert.ToDouble(newItem.VatTax) ' VatTax
            itemRow("SpecialTax") = Convert.ToDouble(newItem.SpecialTax) ' SpecialTax
            itemRow("ItemCount") = Convert.ToInt32(newItem.Quantity) ' Quantity as ItemCount

            ' Determine TaxTYPE based on tax values
            If Convert.ToDouble(newItem.VatTax) > 0 OrElse Convert.ToDouble(newItem.SpecialTax) > 0 Then
                itemRow("TaxTYPE") = "S" ' Special tax or general tax applied
            ElseIf Convert.ToDouble(newItem.VatTax) = 0 AndAlso Convert.ToDouble(newItem.SpecialTax) = 0 Then
                itemRow("TaxTYPE") = "O" ' No tax
            Else
                itemRow("TaxTYPE") = "Z" ' Zero tax
            End If

            ' Add the row to the DataTable
            InvoiceTool1.InvoiceItems.Rows.Add(itemRow)
            'add_debug("Item added to DataTable successfully.")
        Catch ex As Exception
            ' add_debug("Error adding item to DataTable: " & ex.Message)
        End Try
    End Sub
    Private Async Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Try
            If String.IsNullOrEmpty(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("يرجى تعبئة سبب الارجاع ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return

            End If
            Dim checkedItems As New List(Of Dictionary(Of String, Object))()
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(0).Value IsNot Nothing AndAlso CBool(row.Cells(0).Value) Then



                    Dim itemData As New Dictionary(Of String, Object) From {
                        {"ItemId", row.Cells("ItemId").Value},
                        {"Description", row.Cells("Description").Value},
                        {"UnitPrice", row.Cells("UnitPrice").Value},
                        {"Discount", row.Cells("Discount").Value},
                        {"VatTax", row.Cells("VatTax").Value},
                        {"SpecialTax", row.Cells("SpecialTax").Value},
                        {"Quantity", row.Cells("Quantity").Value},
                        {"Subtotal", row.Cells("Subtotal").Value}
                    }
                    checkedItems.Add(itemData)
                End If
            Next
            '  MsgBox(checkedItems.Count)
            If checkedItems.Count > 0 Then
                ' Example: Display or process the checked items
                Dim message As String = "Checked Items:" & Environment.NewLine
                For Each item In checkedItems
                    Dim newItem As New Itemsys(
                                                     itemId:=Guid.NewGuid().ToString(), ' Generate UUID
                                                     quantity:=item("Quantity").ToString(), ' Map p_count to Quantity
                                                     subtotal:=item("Subtotal").ToString(), ' Map p_total to Subtotal
                                                     vatTax:=item("VatTax").ToString, ' Default
                                                    specialTax:="0.00000", ' Default
                                                    unitPrice:=item("UnitPrice").ToString(), ' Map p_price to UnitPrice
                                                    discount:=item("Discount"), ' Default
                                                    description:=item("Description").ToString(), ' Map nots_p to Description
                                                    isic4:="" ' Map p_id to Isic4
                        )

                    AddItemToDataTable(newItem)

                    message &= $"Item ID: {item("ItemId")}, Name: {item("Description")}, Subtotal: {item("Subtotal")}" & Environment.NewLine
                Next
                '  MessageBox.Show(message, "Selected Items", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '  add_debug("Retrieved data from checked rows.")
                Dim o As Integer = Await try_send_to_fwatercom()
            Else
                MessageBox.Show("No items selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            ' add_debug("Error retrieving checked items: " & ex.Message)
        End Try
    End Sub
    Private Async Function try_send_to_fwatercom() As Task(Of Integer)

        Dim taxkind As String = 1 ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_kind").ToString
            Dim tax_serial As String = tax_source_nu ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_source_nu").ToString
            Dim tax_number As String = companyTaxNo ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_number").ToString
            Dim tax_fwater_key As String = companyName ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_fwater_key").ToString
        '    Select Case orders.ser, orders.o_status, orders.finishtime, orders.id, orders.pay_kind, orders.b_id, branch.p_name, branch.p_tel, branch.p_address, branch.tax_kind, branch.tax_source_nu, branch.fwtara_kind, branch.tax_fwater_key, 
        'ater_id
        ' add_debug(tax_serial)
        'add_debug(tax_number)
        'From orders LEFT OUTER Join
        '  branch On orders.id = branch.id
        'Where (orders.id = @OID)
        ' Sample data

        InvoiceTool1.Seller = New InvoiceTool.SellerInfo With {
         .CommercialName = tax_fwater_key, 'الاسم كمافي السجل التجاري(الضريبي)
         .IncomeSourceNumber = tax_serial,'تسلسل مصدر الدخل
          .Address = companyAddress,' "123 Main St,AMMAN,11181,JO",'العنوان
         .TaxNumber = tax_number,'الرقم الضريبي
         .CountryCode = "1" ' JO'رمز الوله وهو 1 دائما
     }

        ' Set Buyer properties
        'بيانات المشتري
        InvoiceTool1.Buyer = New InvoiceTool.BuyerInfo With {
            .IsCashCustomer = True,'TRUE اذا كان البيع لعميل نقدي FALSE اذا كان البيع لعميل حقيقي
            .CommercialName = cName,
            .TaxNumber = cTaxCode,
            .Governorate = InvoiceTool.Governorate.Zarqa.ToString,'العنوان 
            .PartyIdentification = New InvoiceTool.BuyerPartyIdentification With {'التعريف الشخصي
            .CODE = "123456",'رقم المعرف للعميل
            .KIND = InvoiceTool.Identification.NIN.ToString 'نوع المعرف للعميل
        }
        } ' Automatically sets TaxNumber to "10" and clears other fields

        ' Set Invoice properties
        'بيانات الفاتوره
        InvoiceTool1.Invoice = New InvoiceTool.InvoiceInfo With {'بيانات الفاتوره
            .InvoiceNumber = "INV-" & id1,'رقم الفاتورة
            .InvoiceType = 0, '0 محليه 1 تصدير  2 تنمويه
            .PaymentMethod = 1, '1 Cash 2 Receivables 
            .InvoiceCurrency = InvoiceTool.Currency.JOD.ToString, 'عمله الفاتورة
            .TaxCurrency = InvoiceTool.Currency.JOD.ToString,'عمله الضريبه
            .IncomeSourceType = 1, ' 1  Income  2 General tax 3 Special tax 'نوع مصدر الدخل 
            .TransactionType = 381, '388 New Invoice 381 AS BACK INVOICE  فاتورة ارجاع او جديده 
        .RETURN_INVOICE = New InvoiceTool.BillingReference With {'تعريف بيانت فاتورة الارجاع
       .INVOICE_NUMBER = "INV-" & id1,'رقم الفاتورة المراد ارجاعها
        .INVOICE_TOATAL = returnvalue,'قيمة الفاتورة المراد ارجاعها
       .INVOICE_UUID = INUIDD,'الرمز الفريد المعاد من الضريبيه عند مزامنه الفاتوره المراد ارجاعها
        .RESON_RETURN = TextBox1.Text
        },
        .Notes = "فاتورة ارجاع"
                    }

        ' .RETURN_INVOICE = New InvoiceTool.BillingReference With {'تعريف بيانت فاتورة الارجاع
        '  .INVOICE_NUMBER = "RET001",'رقم الفاتورة المراد ارجاعها
        ' .INVOICE_TOATAL = 100D,'قيمة الفاتورة المراد ارجاعها
        ' .INVOICE_UUID = "UUID123"'الرمز الفريد المعاد من الضريبيه عند مزامنه الفاتوره المراد ارجاعها
        ' }


        ' Set SetupInvoice properties
        InvoiceTool1.PortalUrl = "https://portal.jofotara.gov.jo/ar/invoices/" 'مسار PORTAL
            InvoiceTool1.UserKey = key0 'اسم المستخدم المعرف في موقع الضريبيه
            InvoiceTool1.UserSecureKey = key1 ' "Gj5nS9ASDRadaVffz5VKB4v4wlVWyPhcJvrTD4NHtPk6ZAzKgbQdrVlgQOfASDD4x9HtOiRWkLQJ4aqgmMTG2xL7rI0WJfvFDgzF1wD5uHcfdU0idPw7dZ2s+H4nzaOJzETDDDL3JGDan0O58+iiSKAyeASDbmshoRmbmBPCukJrIZZHSQGXJJjjTv5rUASDdj7pFYk/ZoUNd6TFrXK1FsdbEQdHMAqYfELi3vGtK24J2gOhy+OJQ6mpTwHn10VoGKGSJLMNzk6vQ==" 'المفتاح الشخصي للمستخدم
            ' InvoiceTool1.DigitalSignaturePath = "C:\signature.pfx" 'موقع ملف التوقيع الالكتروني (اختياري(
            InvoiceTool1.AutoClose = True 'اغلاق شاشه المزامنه تلقائيا في حال نجاح المزامنه
            InvoiceTool1.showresulttoast = True 'اظهار اشعار اسفل الشاشه بنتيجه المزامنه
            ' Add invoice items

            'تعريف المواد


            InvoiceTool1.Initialize() 'ارسال البيانات التي تم تعريفها الى الاداه ليتم تهيئه الفاتوره

            Await InvoiceTool1.Start() 'بدء المزامنه 

            'مخرجات الاداه 
            ' F NOT SEND
            ' P INVOICE SEND
            If InvoiceTool1.Status = "P" Then
                ' ٍيتم ارجاع القيم التاليه
                '  InvoiceTool1.UUID الرقم الفريد للفاتوره
                '  InvoiceTool1.QRCode الباركود الخاص بالفاتوره
                '  InvoiceTool1.Status الحاله
                '  InvoiceTool1.msg رساله
                ''   InvoiceTool1.SIGNIT التوقيع الرقمي
                '  MessageBox.Show($"UUID: {InvoiceTool1.UUID}{vbCrLf}Status: {InvoiceTool1.QRCode}")

                Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
                    conn.Open()
                    Dim cmd As New SQLiteCommand("UPDATE INVOICES SET E_CODE = @ecode , E_NUM = @ENUM, E_UUID = @ebarcode WHERE ID = @id", conn)
                    cmd.Parameters.AddWithValue("@ecode", InvoiceTool1.QRCode)
                    cmd.Parameters.AddWithValue("@ENUM", InvoiceTool1.Signature)
                    cmd.Parameters.AddWithValue("@ebarcode", InvoiceTool1.UUID)
                    cmd.Parameters.AddWithValue("@id", O_ID)
                    cmd.ExecuteNonQuery()
                End Using
                '   TABLE `res_order`.`invoice_sys_fwtercom` (`oid` INT NOT NULL , `sys_status` INT NOT NULL , `e_uuid` VARCHAR(150) NULL , `e_barcod` TEXT NULL , `e_number` VARCHAR(50) NULL , `e_sig` TEXT NULL , `id` INT NOT NULL AUTO_INCREMENT
                ''  Me.Invoice_sys_fwtercomTableAdapter.Insert_new(Convert.ToInt32(O_ID), Convert.ToInt32(result("Response")), result("EINV_INV_UUID"), result("EINV_QR"), result("EINV_NUM"), result("EINV_SINGED_INVOICE"))
                '  SimpleButton4.Enabled = False

                Me.Text = "MSG=" & InvoiceTool1.Status
            Else
                '  InvoiceTool1.ShowToast("فشل في المزامنة", 3000, True) ' Red toast for 3 seconds
                '  MessageBox.Show($"ERROR: {InvoiceTool1.msg} ")
                Me.Text = InvoiceTool1.Status
            End If



    End Function
    Private Sub LoadCompanySettings()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Fetch the first record or a specific ID (e.g., ID = 1)
            Dim query As String = "SELECT * FROM COM_SEETING WHERE ID = 1" ' Adjust ID as needed
            Using cmd As New SQLiteCommand(query, conn)
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Store company settings in variables

                        companyName = reader.GetString(1) ' COMPANY_NAME
                        companyTel = If(reader.IsDBNull(2), "", reader.GetString(2)) ' COMPANY_TEL (may be null)
                        companyTaxNo = If(reader.IsDBNull(3), "", reader.GetString(3)) ' COMPANY_TAX_NO (may be null)
                        companyAddress = If(reader.IsDBNull(4), "---", reader.GetString(4)) ' COMPANY_ADDRESS (may be null)
                        '   companyLogo = If(reader.IsDBNull(5), Nothing, DirectCast(reader(5), Byte())) ' COMPANY_LOGO (BLOB)
                        ' posKind = reader.GetInt32(6) ' POS_KIND
                        key0 = If(reader.IsDBNull(7), "", reader.GetString(7)) ' key0 (may be null)
                        key1 = If(reader.IsDBNull(8), "", reader.GetString(8)) ' key1 (may be null)
                        tax_source_nu = If(reader.IsDBNull(9), "", reader.GetString(9))
                        ' Example: Display or use the data (e.g., set form labels)
                        ' lblCompanyName.Text = companyName ' Assuming a label control
                        ' lblCompanyTel.Text = companyTel
                        ' lblCompanyAddress.Text = companyAddress
                        ' Handle logo (e.g., display in a PictureBox)
                        ' If companyLogo IsNot Nothing Then
                        'Using ms As New MemoryStream(companyLogo)
                        'picCompanyLogo.Image = Image.FromStream(ms) ' Assuming a PictureBox control
                        'End Using

                    Else
                        MessageBox.Show("Company settings not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading company settings: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub return_invoice_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadCompanySettings()
    End Sub



    Private Sub BarCodeControl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BarCodeControl1.KeyPress
        If e.KeyChar = Chr(13) Then
            DataGridView1.Rows.Clear()
            Dim i As Long = BarCodeControl1.Text
            RetrieveInvoiceDetails(i)
        End If
    End Sub
End Class