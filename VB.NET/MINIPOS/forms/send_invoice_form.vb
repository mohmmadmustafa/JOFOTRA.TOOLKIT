Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq
Imports System.Xml.Schema
Imports System.Net
Imports jofotaratoolkit
Imports System.Threading.Tasks
Public Class send_invoice_form

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

    Public Sub New(OID As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        O_ID = OID

    End Sub
    Private Sub LoadInvoiceDetails(invoiceId As Integer)
        Try
            ' Ensure connection is open
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim itemsS As New List(Of Itemsys)()
            InvoiceTool1.InvoiceItems.Clear()
            ' Step 1: Retrieve invoice data
            Dim invoiceQuery As String = "SELECT * FROM INVOICES WHERE ID = @id"
            Using cmd As New SQLiteCommand(invoiceQuery, conn)
                cmd.Parameters.AddWithValue("@id", invoiceId)

                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Store invoice data in variables
                        Dim invId As Integer = reader.GetInt32(0) ' ID
                        Dim invPayKind As Integer = reader.GetInt32(1) ' INV_PAY_KIND
                        Dim invKind As Integer = reader.GetInt32(2) ' INV_KIND
                        Dim userName As String = reader.GetString(3) ' USER_NAME
                        Dim customerId As Integer = reader.GetInt32(4) ' C_ID
                        Dim remarkKind As Integer = reader.GetInt32(5) ' REMARK_KIND
                        'Dim eCode As String = reader.GetString(6) ' E_CODE
                        'Dim eBarcode As String = If(reader.IsDBNull(7), "", reader.GetString(7)) ' E_BARCODE (may be null)
                        Dim dateTime As String = reader.GetString(8) ' DATE_TIME
                        Dim invValue As Double = reader.GetDouble(9) ' INV_VALUE
                        Dim discountValue As Double = reader.GetDouble(10) ' DISCOUNT_VALUE
                        Dim itemValue As Double = reader.GetDouble(11) ' ITEM_VALUE
                        Dim taxValue As Double = reader.GetDouble(12) ' TAX_VALUE
                        Dim currencyKind As Integer = reader.GetInt32(13) ' CURRENCY_KIND
                        Dim invOutIn As Integer = reader.GetInt32(14) ' INV_OUT_IN

                        ' Step 2: Retrieve customer data
                        Dim customerQuery As String = "SELECT * FROM CUSTOMERS WHERE ID = @customerId"
                        Using cmdCustomer As New SQLiteCommand(customerQuery, conn)
                            cmdCustomer.Parameters.AddWithValue("@customerId", customerId)

                            Using readerCustomer As SQLiteDataReader = cmdCustomer.ExecuteReader()
                                If readerCustomer.Read() Then
                                    custId = readerCustomer.GetInt32(0) ' ID
                                    eId = readerCustomer.GetInt32(1) ' E_ID
                                    cName = readerCustomer.GetString(2) ' C_NAME
                                    cTel = If(readerCustomer.IsDBNull(3), "", readerCustomer.GetString(3)) ' C_TEL (may be null)
                                    cTaxCode = If(readerCustomer.IsDBNull(4), "", readerCustomer.GetString(4)) ' C_TAX_CODE (may be null)
                                    cuCity = readerCustomer.GetInt32(5) ' CU_CITY
                                    cPostcode = If(readerCustomer.IsDBNull(6), "", readerCustomer.GetString(6)) ' C_POSTCODE (may be null)

                                    ' Step 3: Retrieve order products data
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
                                                itemsS.Add(newItem)
                                                AddItemToDataTable(newItem)
                                                ' Store or process product data (e.g., add to a list or display)
                                                ' productList.Add($"Product: {proName}, Count: {proCount}, Price: {proPrice}")
                                            End While

                                            items = itemsS.ToArray
                                            ' Example: Display or use the data
                                            '  MessageBox.Show($"Invoice ID: {invId}{Environment.NewLine}Customer: {cName}{Environment.NewLine}Products:{Environment.NewLine}{String.Join(Environment.NewLine, productList)}", "Invoice Details")
                                        End Using
                                    End Using
                                Else
                                    MessageBox.Show("Customer not found for this invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End Using
                        End Using
                    Else
                        MessageBox.Show("Invoice not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving data2: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Timer1.Enabled = True
        End Try
    End Sub
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
    Private Sub FillToolStripButton()
        Try
            '  Me.OrdersTableAdapter.Fill(Me.E_ENVOICE_DATASET.orders, New System.Nullable(Of Long)(CType(O_ID, Long)))


            'Me.Order_dTableAdapter.Fill(Me.E_ENVOICE_DATASET.order_d, CType(O_ID, Integer))
            Timer1.Enabled = True

        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub FillToolStripButton_Click_1(sender As Object, e As EventArgs)


    End Sub

    Private Sub syc_einvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'E_ENVOICE_DATASET.invoice_sys_fwtercom' table. You can move, or remove it, as needed.
        '  Me.Invoice_sys_fwtercomTableAdapter.Fill(Me.E_ENVOICE_DATASET.invoice_sys_fwtercom)
        stratdo(1)
    End Sub
    Public Sub add_debug(txt As String)
        ListBox1.Items.Add(Now.ToString("HH:mm:ss") & " " & txt)
        Application.DoEvents()
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        ProgressPanel1.Text = "يرجى الانتظار" & txt
        ' ProgressPanel1.Description = txt
    End Sub
    Private Async Sub stratdo(ste As Int32)
        If ste = 1 Then
            SimpleButton4.Enabled = False
            LabelControl5.Text = "...."
            add_debug("يتم تحضير الفاتوره")
            ProgressPanel1.Enabled = True
            'FillToolStripButton()
            LoadCompanySettings()
            LoadInvoiceDetails(O_ID)
        End If
        If ste = 2 Then
            add_debug("يتم تجهيز بيانات الضريبه")
            Dim o As Integer = Await try_send_to_fwatercom()
            ProgressPanel1.Enabled = False
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Timer1.Enabled = False
        stratdo(2)
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
            add_debug("Item added to DataTable successfully.")
        Catch ex As Exception
            add_debug("Error adding item to DataTable: " & ex.Message)
        End Try
    End Sub
    Private Async Function try_send_to_fwatercom() As Task(Of Integer)
        Try


            Dim taxkind As String = 1 ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_kind").ToString
            Dim tax_serial As String = tax_source_nu ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_source_nu").ToString
            Dim tax_number As String = companyTaxNo ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_number").ToString
            Dim tax_fwater_key As String = companyName ' DirectCast(Me.OrdersBindingSource.Current, DataRowView).Item("tax_fwater_key").ToString
            '    Select Case orders.ser, orders.o_status, orders.finishtime, orders.id, orders.pay_kind, orders.b_id, branch.p_name, branch.p_tel, branch.p_address, branch.tax_kind, branch.tax_source_nu, branch.fwtara_kind, branch.tax_fwater_key, 
            'ater_id
            add_debug(tax_serial)
            add_debug(tax_number)
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
            .InvoiceNumber = "INV-" & O_ID,'رقم الفاتورة
            .InvoiceType = 0, '0 محليه 1 تصدير  2 تنمويه
            .PaymentMethod = 1, '1 Cash 2 Receivables 
            .InvoiceCurrency = InvoiceTool.Currency.JOD.ToString, 'عمله الفاتورة
            .TaxCurrency = InvoiceTool.Currency.JOD.ToString,'عمله الضريبه
            .IncomeSourceType = 1, ' 1  Income  2 General tax 3 Special tax 'نوع مصدر الدخل 
            .TransactionType = 388, '388 New Invoice 381 AS BACK INVOICE  فاتورة ارجاع او جديده 
            .Notes = "Sample invoice"
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
                    Dim cmd As New SQLiteCommand("UPDATE INVOICES SET E_CODE = @ecode ,E_UUID=@BACKINVOICENUMBER, E_NUM = @ENUM, E_UUID = @ebarcode WHERE ID = @id", conn)
                    cmd.Parameters.AddWithValue("@ecode", InvoiceTool1.QRCode)
                    cmd.Parameters.AddWithValue("@ENUM", InvoiceTool1.Signature)
                    cmd.Parameters.AddWithValue("@ebarcode", InvoiceTool1.UUID)
                    Try
                        cmd.Parameters.AddWithValue("@BACKINVOICENUMBER", InvoiceTool1.InvoiceENumber)
                    Catch ex As Exception
                        cmd.Parameters.AddWithValue("@BACKINVOICENUMBER", "---")
                    End Try

                    cmd.Parameters.AddWithValue("@id", O_ID)
                    cmd.ExecuteNonQuery()
                End Using
                '   TABLE `res_order`.`invoice_sys_fwtercom` (`oid` INT NOT NULL , `sys_status` INT NOT NULL , `e_uuid` VARCHAR(150) NULL , `e_barcod` TEXT NULL , `e_number` VARCHAR(50) NULL , `e_sig` TEXT NULL , `id` INT NOT NULL AUTO_INCREMENT
                ''  Me.Invoice_sys_fwtercomTableAdapter.Insert_new(Convert.ToInt32(O_ID), Convert.ToInt32(result("Response")), result("EINV_INV_UUID"), result("EINV_QR"), result("EINV_NUM"), result("EINV_SINGED_INVOICE"))
                SimpleButton4.Enabled = False

                Me.Text = "MSG=" & InvoiceTool1.Status
            Else
                '  InvoiceTool1.ShowToast("فشل في المزامنة", 3000, True) ' Red toast for 3 seconds
                '  MessageBox.Show($"ERROR: {InvoiceTool1.msg} ")
                Me.Text = InvoiceTool1.Status
            End If



            Dim t As Integer
            Return t
        Catch ex As Exception

        End Try
    End Function





    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        ListBox1.Items.Clear()
        stratdo(1)

    End Sub
End Class




' DataClasses.vb - Data structures
Public Class Sellersys
    Public Property Id As String
    Public Property TaxId As String
    Public Property Name As String
    Public Property Address As String

    Public Sub New(id As String, taxId As String, name As String, address As String)
        Me.Id = id
        Me.TaxId = taxId
        Me.Name = name
        Me.Address = address
    End Sub
End Class

Public Class Buyersys
    Public Property Id As String
    Public Property TaxId As String
    Public Property Name As String
    Public Property Address As String
    Public Property IdType As String

    Public Sub New(id As String, taxId As String, name As String, address As String, idType As String)
        Me.Id = id
        Me.TaxId = taxId
        Me.Name = name
        Me.Address = address
        Me.IdType = idType
    End Sub
End Class

Public Class Itemsys
    Public Property ItemId As String
    Public Property Quantity As String
    Public Property Subtotal As String
    Public Property VatTax As String
    Public Property SpecialTax As String
    Public Property UnitPrice As String
    Public Property Discount As String
    Public Property Description As String
    Public Property Isic4 As String


    Public Sub New(itemId As String, quantity As String, subtotal As String, vatTax As String, specialTax As String, unitPrice As String, discount As String, description As String, isic4 As String)
        Me.ItemId = itemId
        Me.Quantity = quantity
        Me.Subtotal = subtotal
        Me.VatTax = vatTax
        Me.SpecialTax = specialTax
        Me.UnitPrice = unitPrice
        Me.Discount = discount
        Me.Description = description
        Me.Isic4 = isic4
    End Sub
End Class

Public Class InvoiceTypesys
    Public Property Code As String
    Public Property Name As String
    Public Property NUMBER As String
    Public Sub New(code As String, name As String, NUM As String)
        Me.Code = code
        Me.Name = name
        Me.NUMBER = NUM
    End Sub
End Class