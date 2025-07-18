Imports System.Data.SQLite
Imports System.IO
Imports System.Text

Public Class DataGenerator
    Private ReadOnly connectionString As String = "Data Source=E_INVOIC.db;Version=3;"
    Private ReadOnly random As New Random()

    Public Sub GenerateTestData()
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()
            Dim trans = conn.BeginTransaction()
            Try
                ' Ensure lookup tables have data
                EnsureLookupData(conn)
                ' Insert products if none exist
                EnsureProducts(conn)
                ' Insert customers
                InsertRandomCustomers(conn, 100)
                ' Insert invoices and order products
                InsertRandomInvoices(conn, 10000, 2)

                trans.Commit()
                Console.WriteLine("تم إنشاء البيانات العشوائية بنجاح")
            Catch ex As Exception
                trans.Rollback()
                Console.WriteLine($"خطأ أثناء إنشاء البيانات: {ex.Message}")
            End Try
        End Using
    End Sub

    Private Sub EnsureLookupData(conn As SQLiteConnection)
        ' Insert ID_CUSTOMER
        Dim idCustomerData As String() = {"رقم وطني", "رقم ضريبي", "رقم شخصي"}
        For Each txt In idCustomerData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO ID_CUSTOMER (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next

        ' Insert CITY_TABLE
        Dim cityData As String() = {"عمان", "إربد", "الزرقاء"}
        For Each txt In cityData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO CITY_TABLE (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next

        ' Insert PRO_KIND
        Dim proKindData As String() = {"سلعة", "بدل خدمة"}
        For Each txt In proKindData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO PRO_KIND (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next

        ' Insert INV_PAY_KIND
        Dim payKindData As String() = {"نقدي", "ذمم", "أخرى"}
        For Each txt In payKindData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO INV_PAY_KIND (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next

        ' Insert IN_KIND
        Dim inKindData As String() = {"فاتورة محلية", "فاتورة تصدير", "فاتورة مناطق تنموية"}
        For Each txt In inKindData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO IN_KIND (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next

        ' Insert INV_OUT_IN
        Dim outInData As String() = {"فاتورة مبيعات", "مرتجع"}
        For Each txt In outInData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO INV_OUT_IN (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next

        ' Insert CURRENCY_TABLE
        Dim currencyData As String(,) = {{"دينار أردني", "JO"}, {"دولار أمريكي", "USD"}, {"يورو", "EUR"}}
        For i As Integer = 0 To currencyData.GetLength(0) - 1
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO CURRENCY_TABLE (TXT, C_CODE) VALUES (@txt, @code)", conn)
            cmd.Parameters.AddWithValue("@txt", currencyData(i, 0))
            cmd.Parameters.AddWithValue("@code", currencyData(i, 1))
            cmd.ExecuteNonQuery()
        Next

        ' Insert REMARK_KIND_TABLE
        Dim remarkData As String() = {"بدون ملاحظات", "توصيل مجاني", "خصم خاص"}
        For Each txt In remarkData
            Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO REMARK_KIND_TABLE (TXT) VALUES (@txt)", conn)
            cmd.Parameters.AddWithValue("@txt", txt)
            cmd.ExecuteNonQuery()
        Next
    End Sub

    Private Sub EnsureProducts(conn As SQLiteConnection)
        ' Check if products exist
        Dim cmdCheck As New SQLiteCommand("SELECT COUNT(*) FROM PRODUCTS", conn)
        Dim productCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
        If productCount < 50 Then
            Dim products As String(,) = {
                {"PRO001", "مياه معدنية", 0.5, 0.16, "ضريبة القيمة المضافة", 1},
                {"PRO002", "عصير برتقال", 1.0, 0.16, "ضريبة القيمة المضافة", 1},
                {"PRO003", "قهوة فورية", 2.5, 0.16, "ضريبة القيمة المضافة", 1},
                {"PRO004", "شاي أخضر", 1.5, 0.16, "ضريبة القيمة المضافة", 1},
                {"PRO005", "خدمة توصيل", 2.0, 0.0, "معفاة", 2}                ' Add more products up to 50
            }
            For i As Integer = 0 To Math.Min(49, products.GetLength(0) - 1)
                Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO PRODUCTS (BARCODE, PRO_NAME, PRO_PRICE, PRO_TAX_VALUE, PRO_TAX_KIND, PRO_KIND) VALUES (@barcode, @name, @price, @tax, @taxKind, @kind)", conn)
                cmd.Parameters.AddWithValue("@barcode", products(i, 0))
                cmd.Parameters.AddWithValue("@name", products(i, 1))
                cmd.Parameters.AddWithValue("@price", Convert.ToDouble(products(i, 2)))
                cmd.Parameters.AddWithValue("@tax", Convert.ToDouble(products(i, 3)))
                cmd.Parameters.AddWithValue("@taxKind", products(i, 4))
                cmd.Parameters.AddWithValue("@kind", Convert.ToInt32(products(i, 5)))
                cmd.ExecuteNonQuery()
            Next
            ' Generate additional products if needed
            For i As Integer = products.GetLength(0) To 49
                Dim barcode As String = $"PRO{i:000}"
                Dim name As String = $"منتج {i}"
                Dim price As Double = random.Next(1, 50) + random.NextDouble()
                Dim tax As Double = If(random.Next(0, 2) = 0, 0.16, 0.0)
                Dim taxKind As String = If(tax > 0, "ضريبة القيمة المضافة", "معفاة")
                Dim kind As Integer = random.Next(1, 3)
                Dim cmd As New SQLiteCommand("INSERT OR IGNORE INTO PRODUCTS (BARCODE, PRO_NAME, PRO_PRICE, PRO_TAX_VALUE, PRO_TAX_KIND, PRO_KIND) VALUES (@barcode, @name, @price, @tax, @taxKind, @kind)", conn)
                cmd.Parameters.AddWithValue("@barcode", barcode)
                cmd.Parameters.AddWithValue("@name", name)
                cmd.Parameters.AddWithValue("@price", price)
                cmd.Parameters.AddWithValue("@tax", tax)
                cmd.Parameters.AddWithValue("@taxKind", taxKind)
                cmd.Parameters.AddWithValue("@kind", kind)
                cmd.ExecuteNonQuery()
            Next
        End If
    End Sub

    Private Sub InsertRandomCustomers(conn As SQLiteConnection, count As Integer)
        Dim firstNames As String() = {"أحمد", "محمد", "علي", "خالد", "سارة", "فاطمة", "عائشة", "زينب"}
        Dim lastNames As String() = {"الخالدي", "الصرايرة", "العمري", "الزعبي", "الرشيد", "القاضي"}
        Dim cities As Integer() = GetLookupIds(conn, "CITY_TABLE")
        Dim idTypes As Integer() = GetLookupIds(conn, "ID_CUSTOMER")

        For i As Integer = 1 To count
            Dim firstName As String = firstNames(random.Next(0, firstNames.Length))
            Dim lastName As String = lastNames(random.Next(0, lastNames.Length))
            Dim fullName As String = $"{firstName} {lastName}"
            Dim phone As String = $"079{random.Next(1000000, 9999999)}"
            Dim city As Integer = cities(random.Next(0, cities.Length))
            Dim idType As Integer = idTypes(random.Next(0, idTypes.Length))
            Dim postcode As String = random.Next(10000, 99999).ToString()
            Dim TAXCODE As String = random.Next(1000, 99999).ToString()
            Dim cmd As New SQLiteCommand("INSERT INTO CUSTOMERS (E_ID, C_NAME,C_TAX_CODE, C_TEL, CU_CITY, C_POSTCODE) VALUES (@eid, @name,@CTAXCODE, @tel, @city, @postcode)", conn)
            cmd.Parameters.AddWithValue("@eid", idType)
            cmd.Parameters.AddWithValue("@name", fullName)
            cmd.Parameters.AddWithValue("@CTAXCODE", TAXCODE)
            cmd.Parameters.AddWithValue("@tel", phone)
            cmd.Parameters.AddWithValue("@city", city)
            cmd.Parameters.AddWithValue("@postcode", postcode)
            cmd.ExecuteNonQuery()
        Next
    End Sub

    Private Sub InsertRandomInvoices(conn As SQLiteConnection, invoiceCount As Integer, itemsPerInvoice As Integer)
        Dim customers As Integer() = GetLookupIds(conn, "CUSTOMERS")
        Dim payKinds As Integer() = GetLookupIds(conn, "INV_PAY_KIND")
        Dim invKinds As Integer() = GetLookupIds(conn, "IN_KIND")
        Dim outIns As Integer() = GetLookupIds(conn, "INV_OUT_IN")
        Dim currencies As Integer() = GetLookupIds(conn, "CURRENCY_TABLE")
        Dim remarks As Integer() = GetLookupIds(conn, "REMARK_KIND_TABLE")
        Dim products As Integer() = GetLookupIds(conn, "PRODUCTS")
        Dim productDetails As DataTable = GetProductDetails(conn)

        For i As Integer = 1 To invoiceCount
            Dim customerId As Integer = customers(random.Next(0, customers.Length))
            Dim payKind As Integer = payKinds(random.Next(0, payKinds.Length))
            Dim invKind As Integer = invKinds(random.Next(0, invKinds.Length))
            Dim outIn As Integer = outIns(random.Next(0, outIns.Length))
            Dim currency As Integer = currencies(random.Next(0, currencies.Length))
            Dim remark As Integer = remarks(random.Next(0, remarks.Length))
            Dim dateTime As Date = DateTime.Now.AddDays(-random.Next(0, 365)).AddHours(random.Next(0, 24))
            Dim eCode As String = $"INV{i:0000}"
            Dim eBarcode As String = $"BAR{i:0000}"

            ' Calculate invoice values
            Dim itemValue As Double = 0
            Dim taxValue As Double = 0
            Dim discountValue As Double = 0
            Dim orderProducts As New List(Of Dictionary(Of String, Object))

            For j As Integer = 1 To itemsPerInvoice
                Dim productId As Integer = products(random.Next(0, products.Length))
                Dim productRow As DataRow = productDetails.Select($"ID = {productId}").First()
                Dim proName As String = productRow("PRO_NAME").ToString()
                Dim proPrice As Double = Convert.ToDouble(productRow("PRO_PRICE"))
                Dim proTaxValue As Double = Convert.ToDouble(productRow("PRO_TAX_VALUE"))
                Dim proKind As Integer = Convert.ToInt32(productRow("PRO_KIND"))
                Dim proCount As Integer = random.Next(1, 10)
                Dim proDiscount As Double = If(random.Next(0, 3) = 0, random.NextDouble() * proPrice * 0.2, 0) ' 20% max discount, sometimes 0
                Dim valueOriginal As Double = proPrice * proCount
                Dim valueAfterDiscount As Double = valueOriginal - proDiscount
                Dim valueOfTax As Double = valueAfterDiscount * proTaxValue
                Dim finalValue As Double = valueAfterDiscount + valueOfTax

                itemValue += valueOriginal
                discountValue += proDiscount
                taxValue += valueOfTax

                orderProducts.Add(New Dictionary(Of String, Object) From {
                    {"PRO_KIND", proKind},
                    {"PRO_NAME", proName},
                    {"PRO_COUNT", proCount},
                    {"PRO_PRICE", proPrice},
                    {"PRO_DISCOUNT", proDiscount},
                    {"PRO_TX_PUBLIC", proTaxValue},
                    {"PRO_TAX", valueOfTax},
                    {"VALUE_AFTER_DISCOUNT", valueAfterDiscount},
                    {"VALUE_OF_TAX", valueOfTax},
                    {"VALUE_ORGINAL", valueOriginal},
                    {"FINAL_VALUE", finalValue}
                })
            Next

            Dim invValue As Double = itemValue - discountValue + taxValue

            ' Insert invoice
            Dim cmdInvoice As New SQLiteCommand("INSERT INTO INVOICES (INV_PAY_KIND, INV_KIND, USER_NAME, C_ID, REMARK_KIND, E_CODE, E_BARCODE, DATE_TIME, INV_VALUE, DISCOUNT_VALUE, ITEM_VALUE, TAX_VALUE, CURRENCY_KIND, INV_OUT_IN) " &
                                              "VALUES (@payKind, @invKind, @user, @cid, @remark, @ecode, @ebarcode, @date, @invValue, @discount, @itemValue, @taxValue, @currency, @outIn)", conn)
            cmdInvoice.Parameters.AddWithValue("@payKind", payKind)
            cmdInvoice.Parameters.AddWithValue("@invKind", invKind)
            cmdInvoice.Parameters.AddWithValue("@user", "TestUser")
            cmdInvoice.Parameters.AddWithValue("@cid", customerId)
            cmdInvoice.Parameters.AddWithValue("@remark", remark)
            cmdInvoice.Parameters.AddWithValue("@ecode", eCode)
            cmdInvoice.Parameters.AddWithValue("@ebarcode", eBarcode)
            cmdInvoice.Parameters.AddWithValue("@date", dateTime.ToString("yyyy-MM-dd HH:mm:ss"))
            cmdInvoice.Parameters.AddWithValue("@invValue", invValue)
            cmdInvoice.Parameters.AddWithValue("@discount", discountValue)
            cmdInvoice.Parameters.AddWithValue("@itemValue", itemValue)
            cmdInvoice.Parameters.AddWithValue("@taxValue", taxValue)
            cmdInvoice.Parameters.AddWithValue("@currency", currency)
            cmdInvoice.Parameters.AddWithValue("@outIn", outIn)
            cmdInvoice.ExecuteNonQuery()

            Dim invoiceId As Long = conn.LastInsertRowId

            ' Insert order products
            For Each item In orderProducts
                Dim cmdOrder As New SQLiteCommand("INSERT INTO ORDER_PRODUCTS (PRO_KIND, PRO_NAME, PRO_COUNT, PRO_PRICE, PRO_DISCOUNT, PRO_TX_PUBLIC, PRO_TAX, VALUE_AFTER_DISCOUNT, VALUE_OF_TAX, VALUE_ORGINAL, FINAL_VALUE, INV_ID) " &
                                                "VALUES (@kind, @name, @count, @price, @discount, @txPublic, @tax, @valueAfter, @valueTax, @valueOrg, @final, @invId)", conn)
                cmdOrder.Parameters.AddWithValue("@kind", item("PRO_KIND"))
                cmdOrder.Parameters.AddWithValue("@name", item("PRO_NAME"))
                cmdOrder.Parameters.AddWithValue("@count", item("PRO_COUNT"))
                cmdOrder.Parameters.AddWithValue("@price", item("PRO_PRICE"))
                cmdOrder.Parameters.AddWithValue("@discount", item("PRO_DISCOUNT"))
                cmdOrder.Parameters.AddWithValue("@txPublic", item("PRO_TX_PUBLIC"))
                cmdOrder.Parameters.AddWithValue("@tax", item("PRO_TAX"))
                cmdOrder.Parameters.AddWithValue("@valueAfter", item("VALUE_AFTER_DISCOUNT"))
                cmdOrder.Parameters.AddWithValue("@valueTax", item("VALUE_OF_TAX"))
                cmdOrder.Parameters.AddWithValue("@valueOrg", item("VALUE_ORGINAL"))
                cmdOrder.Parameters.AddWithValue("@final", item("FINAL_VALUE"))
                cmdOrder.Parameters.AddWithValue("@invId", invoiceId)
                cmdOrder.ExecuteNonQuery()
            Next
        Next
    End Sub

    Private Function GetLookupIds(conn As SQLiteConnection, table As String) As Integer()
        Dim cmd As New SQLiteCommand($"SELECT ID FROM {table}", conn)
        Dim reader As SQLiteDataReader = cmd.ExecuteReader()
        Dim ids As New List(Of Integer)
        While reader.Read()
            ids.Add(reader.GetInt32(0))
        End While
        reader.Close()
        Return ids.ToArray()
    End Function

    Private Function GetProductDetails(conn As SQLiteConnection) As DataTable
        Dim adapter As New SQLiteDataAdapter("SELECT ID, PRO_NAME, PRO_PRICE, PRO_TAX_VALUE, PRO_KIND FROM PRODUCTS", conn)
        Dim table As New DataTable()
        adapter.Fill(table)
        Return table
    End Function
End Class