# JOFOTRA.TOOLKIT
مكتبه لتسهيل مزامنه الفواتير على نظام الفوترة الوطني
بحيث سيتمكن المطورمن ربط الاداه , ليتم مزمنه الفواتير الى منصه الفوترة الوطني
تدعم اصدار الفوترة 1.4
فاتورة : نقديه , ذمم
فاتورة جديده  , ارجاع
فاتورة محليه , توريد ,
فاتورة دخل , مبيعات , خاصه
يمكن الربط مع 
VB.NET
C#
PYTHON
مازال هذا الاصدار قيد التطويروالتحديث
ارحب باستفساراتكم وتوجيهاتكم
كود VB.NET 



Imports jofotaratoolkit

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set Seller properties
    End Sub	
    
	 Private Async Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        '' Start the invoice process
        sendinvoic(1)  ''فاتورة دخل
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sendinvoic(2)'فاتورة مبيعات
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        sendinvoic(3)'فاتورة خاصه
    End Sub
	
    Private Async Sub sendinvoic(k As Integer)
	''تعريف بيانات البائع
        InvoiceTool1.Seller = New InvoiceTool.SellerInfo With {
         .CommercialName = "Seller Inc.", 'الاسم كمافي السجل التجاري(الضريبي)
         .IncomeSourceNumber = "12345",'تسلسل مصدر الدخل
         .Address = "123 Main St",'العنوان
         .TaxNumber = "123456789",'الرقم الضريبي
         .CountryCode = "1" ' JO'رمز الوله وهو 1 دائما
     }
        ' Set Buyer properties
		'بيانات المشتري
        InvoiceTool1.Buyer = New InvoiceTool.BuyerInfo With {
            .IsCashCustomer = True,'TRUE اذا كان البيع لعميل نقدي FALSE اذا كان البيع لعميل حقيقي
            .Governorate = InvoiceTool.Governorate.Zarqa.ToString,'العنوان 
            .PartyIdentification = New InvoiceTool.BuyerPartyIdentification With {'التعريف الشخصي
            .CODE = "123456",'رقم المعرف للعميل
            .KIND = InvoiceTool.Identification.NIN.ToString 'نوع المعرف للعميل
        }
        } ' Automatically sets TaxNumber to "10" and clears other fields

        ' Set Invoice properties
		'بيانات الفاتوره
        InvoiceTool1.Invoice = New InvoiceTool.InvoiceInfo With {'بيانات الفاتوره
            .InvoiceNumber = "INV001",'رقم الفاتورة
            .InvoiceType = 0, '0 محليه 1 تصدير  2 تنمويه
            .PaymentMethod = 1, '1 Cash 2 Receivables 
            .InvoiceCurrency = InvoiceTool.Currency.JOD.ToString, 'عمله الفاتورة
            .TaxCurrency = InvoiceTool.Currency.JOD.ToString,'عمله الضريبه
            .IncomeSourceType = k, ' 1  Income  2 General tax 3 Special tax 'نوع مصدر الدخل 
            .TransactionType = 381, '388 New Invoice 381 AS BACK INVOICE  فاتورة ارجاع او جديده 
            .Notes = "Sample invoice",'ملاحظات
            .RETURN_INVOICE = New InvoiceTool.BillingReference With {'تعريف بيانت فاتورة الارجاع
            .INVOICE_NUMBER = "RET001",'رقم الفاتورة المراد ارجاعها
            .INVOICE_TOATAL = 100D,'قيمة الفاتورة المراد ارجاعها
            .INVOICE_UUID = "UUID123"'الرمز الفريد المعاد من الضريبيه عند مزامنه الفاتوره المراد ارجاعها
        }
        }

        


        ' Set SetupInvoice properties
        InvoiceTool1.PortalUrl = "https://portal.jofotara.gov.jo/ar/invoices/"'مسار PORTAL
        InvoiceTool1.UserKey = "804bb4f8-f745-4SDDaD-b9dD-95cDDDDD6009"'اسم المستخدم المعرف في موقع الضريبيه
        InvoiceTool1.UserSecureKey = "Gj5nS9ASDRadaVffz5VKB4v4wlVWyPhcJvrTD4NHtPk6ZAzKgbQdrVlgQOfASDD4x9HtOiRWkLQJ4DASDaqgmMTG2xL7rI0WJfvFDgzF1wD5uHcfdU0idPw7dZ2s+H4nzaOJzETDDDL3JGDan0O58+iiSKAyeASDbmshoRmbmBPCukJrIZZHSQGXJJjjTv5rUASDdj7pFYk/ZoUNd6TFrXK1FsdbEQdHMAqYfELi3vGtK24J2gOhy+OJQ6mpTwHn10VoGKGSJLMNzk6vQ=="'المفتاح الشخصي للمستخدم
        InvoiceTool1.DigitalSignaturePath = "C:\signature.pfx"'موقع ملف التوقيع الالكتروني (اختياري(
        InvoiceTool1.AutoClose = True'اغلاق شاشه المزامنه تلقائيا في حال نجاح المزامنه
        InvoiceTool1.showresulttoast = True'اظهار اشعار اسفل الشاشه بنتيجه المزامنه
        ' Add invoice items
		'تعريف المواد
        Dim itemRow = InvoiceTool1.InvoiceItems.NewRow()
        itemRow("ItemName") = "Product A"'اسم الماده
        itemRow("ItemNo") = "ISIC5-001"'تصنيف الماده
        itemRow("ItemPrice") = 100.0'سعر الوحده
        itemRow("Discount") = 10.0'الخصم
        itemRow("VatTax") = 16.0'الضريبه العامه
        itemRow("SpecialTax") = 5'الضريبيه الخاصه
        itemRow("ItemCount") = 2'عدد المواد
        itemRow("TaxTYPE") = "S"  
		'نوع الضريبه
		' O لا يوجد ضريبيه 
		'Z ضريبيه صفريه 
		'S الضريبيه الخاصه او عليه ضريبه
        InvoiceTool1.InvoiceItems.Rows.Add(itemRow)
        
        InvoiceTool1.Initialize() 'ارسال البيانات التي تم تعريفها الى الاداه ليتم تهيئه الفاتوره

        Await InvoiceTool1.Start()'بدء المزامنه 
		
         'مخرجات الاداه 
		 ' F NOT SEND
		 ' P INVOICE SEND
        If InvoiceTool1.Status = "P" Then 
		' ٍيتم ارجاع القيم التاليه
		'InvoiceTool1.UUID الرقم الفريد للفاتوره
		'InvoiceTool1.QRCode الباركود الخاص بالفاتوره
		'InvoiceTool1.Status الحاله
		'InvoiceTool1.msg رساله
		'InvoiceTool1.SIGNIT التوقيع الرقمي
		   '  MessageBox.Show($"UUID: {InvoiceTool1.UUID}{vbCrLf}Status: {InvoiceTool1.QRCode}")
            Me.Text = "MSG=" & InvoiceTool1.Status
        Else
            '  InvoiceTool1.ShowToast("فشل في المزامنة", 3000, True) ' Red toast for 3 seconds
            '  MessageBox.Show($"ERROR: {InvoiceTool1.msg} ")
            Me.Text = InvoiceTool1.Status
        End If
    End Sub
   
End Class

