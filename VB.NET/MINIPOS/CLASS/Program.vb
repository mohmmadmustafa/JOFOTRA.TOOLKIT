Imports System.Net.Http
Imports System.Text
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Linq

Module Program
    Private Const QrCodeFolder As String = "qrcode"
    Private ReadOnly client As New HttpClient()

    Sub Main()
        sendinvoic().Wait()
    End Sub

    Public Async Function sendinvoic() As Task
        ' الخطوة 1: تحميل XML كسلسلة نصية
        Dim xmlString As String = "<?xml version=""1.0"" encoding=""UTF-8""?>" &
    "<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"" " &
    "xmlns:cac=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"" " &
    "xmlns:cbc=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"" " &
    "xmlns:ccts=""urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2"" " &
    "xmlns:ext=""urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"">" &
    "<ext:UBLExtensions/>" &
    "<cbc:UBLVersionID>2.1</cbc:UBLVersionID>" &
    "<cbc:CustomizationID>urn:www.joprtal.gov.jo:names:profile:invoice:v1.0</cbc:CustomizationID>" &
    "<cbc:ProfileID>ISTD</cbc:ProfileID>" &
    "<cbc:ID>EIN00001</cbc:ID>" &
    "<cbc:IssueDate>" & DateTime.Now.ToString("yyyy-MM-dd") & "</cbc:IssueDate>" &
    "<cbc:IssueTime>" & DateTime.Now.ToString("HH:mm:ss") & "+03:00</cbc:IssueTime>" &
    "<cbc:InvoiceTypeCode>380</cbc:InvoiceTypeCode>" &
    "<cbc:Note>فاتورة بسيطة بقيمة أقل من 1 دينار</cbc:Note>" &
    "<cbc:DocumentCurrencyCode>JOD</cbc:DocumentCurrencyCode>" &
    "<cac:AccountingSupplierParty>" &
    "<cac:Party>" &
    "<cac:PartyIdentification>" &
    "<cbc:ID schemeID=""TAX_NUMBER"">10611649</cbc:ID>" &
        "</cac:PartyIdentification>" &
        "<cac:PartyIdentification>" &
     "<cbc:ID schemeID=""ACTIVITY_NUMBER"">14807394</cbc:ID>" &
    "</cac:PartyIdentification>" &
    "<cac:PartyName>" &
    "<cbc:Name>مطعم ومشاوي مجد الجيزاوي</cbc:Name>" &
    "</cac:PartyName>" &
    "<cac:PostalAddress>" &
    "<cbc:StreetName>شارع الملك حسين</cbc:StreetName>" &
    "<cbc:BuildingNumber>1</cbc:BuildingNumber>" &
    "<cbc:CityName>عمان</cbc:CityName>" &
    "<cbc:PostalZone>11110</cbc:PostalZone>" &
    "<cac:Country>" &
    "<cbc:IdentificationCode>JO</cbc:IdentificationCode>" &
    "</cac:Country>" &
    "</cac:PostalAddress>" &
    "<cac:PartyTaxScheme>" &
    "<cbc:CompanyID>10611649</cbc:CompanyID>" &
    "<cac:TaxScheme>" &
    "<cbc:ID>VAT</cbc:ID>" &
    "</cac:TaxScheme>" &
    "</cac:PartyTaxScheme>" &
    "<cac:Contact>" &
    "<cbc:Telephone>786786333</cbc:Telephone>" &
    "</cac:Contact>" &
    "</cac:Party>" &
    "</cac:AccountingSupplierParty>" &
    "<cac:AdditionalDocumentReference>" &
        "<cbc:ID>SRC-14807394</cbc:ID>" &
        "<cbc:DocumentType>SRC</cbc:DocumentType>" &
   " </cac:AdditionalDocumentReference>" &
    "<cac:AccountingCustomerParty>" &
    "<cac:Party>" &
    "<cac:PartyName>" &
    "<cbc:Name>عميل نقدي</cbc:Name>" &
    "</cac:PartyName>" &
    "<cac:PostalAddress>" &
    "<cbc:StreetName>شارع الملك حسين</cbc:StreetName>" &
    "<cbc:BuildingNumber>1</cbc:BuildingNumber>" &
    "<cbc:CityName>عمان</cbc:CityName>" &
    "<cbc:PostalZone>11110</cbc:PostalZone>" &
    "<cac:Country>" &
    "<cbc:IdentificationCode>JO</cbc:IdentificationCode>" &
    "</cac:Country>" &
    "</cac:PostalAddress>" &
    "<cac:PartyTaxScheme>" &
    "<cbc:CompanyID>0987654321</cbc:CompanyID>" &
    "<cac:TaxScheme>" &
    "<cbc:ID>VAT</cbc:ID>" &
    "</cac:TaxScheme>" &
    "</cac:PartyTaxScheme>" &
    "<cac:Contact>" &
    "<cbc:Telephone>123456789</cbc:Telephone>" &
    "</cac:Contact>" &
    "</cac:Party>" &
    "</cac:AccountingCustomerParty>" &
    "<cac:TaxTotal>" &
    "<cbc:TaxAmount currencyID=""JOD"">0.05</cbc:TaxAmount>" &
    "<cac:TaxSubtotal>" &
    "<cbc:TaxableAmount currencyID=""JOD"">0.50</cbc:TaxableAmount>" &
    "<cbc:TaxAmount currencyID=""JOD"">0.05</cbc:TaxAmount>" &
    "<cac:TaxCategory>" &
    "<cbc:ID schemeID=""UN/ECE 5305"" schemeAgencyID=""6"">S</cbc:ID>" &
    "<cbc:Percent>10</cbc:Percent>" &
    "<cac:TaxScheme>" &
    "<cbc:ID>VAT</cbc:ID>" &
    "</cac:TaxScheme>" &
    "</cac:TaxCategory>" &
    "</cac:TaxSubtotal>" &
    "</cac:TaxTotal>" &
    "<cac:LegalMonetaryTotal>" &
    "<cbc:LineExtensionAmount currencyID=""JOD"">0.50</cbc:LineExtensionAmount>" &
    "<cbc:TaxExclusiveAmount currencyID=""JOD"">0.50</cbc:TaxExclusiveAmount>" &
    "<cbc:TaxInclusiveAmount currencyID=""JOD"">0.55</cbc:TaxInclusiveAmount>" &
    "<cbc:PayableAmount currencyID=""JOD"">0.55</cbc:PayableAmount>" &
    "</cac:LegalMonetaryTotal>" &
    "<cac:InvoiceLine>" &
    "<cbc:ID>1</cbc:ID>" &
    "<cbc:InvoicedQuantity unitCode=""UN"">1</cbc:InvoicedQuantity>" &
    "<cbc:LineExtensionAmount currencyID=""JOD"">0.50</cbc:LineExtensionAmount>" &
    "<cac:Item>" &
    "<cbc:Description>وجبة صغيرة</cbc:Description>" &
    "<cac:ClassifiedTaxCategory>" &
    "<cbc:ID schemeID=""UN/ECE 5305"" schemeAgencyID=""6"">S</cbc:ID>" &
    "<cbc:Percent>10</cbc:Percent>" &
    "<cac:TaxScheme>" &
    "<cbc:ID>VAT</cbc:ID>" &
    "</cac:TaxScheme>" &
    "</cac:ClassifiedTaxCategory>" &
    "</cac:Item>" &
    "<cac:Price>" &
    "<cbc:PriceAmount currencyID=""JOD"">0.50</cbc:PriceAmount>" &
    "</cac:Price>" &
    "</cac:InvoiceLine>" &
    "</Invoice>"

        ' الخطوة 2: تحميل وتوثيق XML
        Dim doc As New XmlDocument()
        Try
            doc.LoadXml(xmlString)
            Console.WriteLine("تم تحميل XML بنجاح.")
        Catch ex As XmlException
            Console.WriteLine("خطأ في تحميل XML: " & ex.Message)
            Return
        End Try

        ' الخطوة 3: إعداد مدير مساحة الأسماء
        Dim nsManager As New XmlNamespaceManager(doc.NameTable)
        nsManager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        nsManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")

        ' الخطوة 4: توقيع XML باستخدام الشهادة
        Dim cert As X509Certificate2
        Try
            ' استبدل المسار وكلمة المرور بشهادتك الخاصة
            cert = New X509Certificate2("d:\k\myCertificate.pfx", "mom-3655807")
            If cert.GetRSAPrivateKey() Is Nothing Then
                Throw New Exception("لم يتم العثور على المفتاح الخاص في الشهادة.")
            End If
        Catch ex As Exception
            Console.WriteLine("خطأ في تحميل الشهادة: " & ex.Message)
            Return
        End Try

        Dim signedXml As New SignedXml(doc)
        signedXml.SigningKey = cert.GetRSAPrivateKey()

        ' تحديد طريقة التوقيع
        signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigCanonicalizationUrl
        signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"

        ' إعداد المرجع للتوقيع
        Dim reference As New Reference("")
        reference.AddTransform(New XmlDsigEnvelopedSignatureTransform())
        reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256"
        signedXml.AddReference(reference)

        ' إضافة معلومات المفتاح
        Dim keyInfo As New KeyInfo()
        keyInfo.AddClause(New KeyInfoX509Data(cert))
        signedXml.KeyInfo = keyInfo

        ' حساب التوقيع
        Try
            signedXml.ComputeSignature()
        Catch ex As CryptographicException
            Console.WriteLine("خطأ في حساب التوقيع: " & ex.Message)
            Return
        End Try

        ' إضافة التوقيع إلى XML
        Dim signatureElement As XmlElement = signedXml.GetXml()
        Dim extensions As XmlElement = doc.SelectSingleNode("//ext:UBLExtensions", nsManager)
        If extensions Is Nothing Then
            Console.WriteLine("لم يتم العثور على عنصر UBLExtensions في XML.")
            Return
        End If

        Dim extension As XmlElement = doc.CreateElement("ext", "UBLExtension", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        Dim content As XmlElement = doc.CreateElement("ext", "ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
        content.AppendChild(doc.ImportNode(signatureElement, True))
        extension.AppendChild(content)
        extensions.AppendChild(extension)

        ' الخطوة 5: تنظيف XML الموقع من الأحرف غير الصالحة
        Dim signedXmlString As String = doc.OuterXml
        signedXmlString = Regex.Replace(signedXmlString, "[\x00-\x08\x0B\x0C\x0E-\x1F]", "")

        ' الخطوة 6: تحويل XML الموقع إلى بايت ثم ترميز base64
        Dim xmlBytes As Byte() = Encoding.UTF8.GetBytes(signedXmlString)
        Dim base64Encoded As String = Convert.ToBase64String(xmlBytes, Base64FormattingOptions.None)
        base64Encoded = Regex.Replace(base64Encoded, "[\r\n\t]", "")

        ' الخطوة 7: إعداد طلب HTTP لإرسال الفاتورة
        Dim jsonBody As String = "{""invoice"": """ & base64Encoded & """}"
        Dim url As String = "https://backend.jofotara.gov.jo/core/invoices/"
        Dim contenta As New StringContent(jsonBody, Encoding.UTF8, "application/json")
        Debug.Write(jsonBody)
        ' إضافة رؤوس HTTP المطلوبة
        client.DefaultRequestHeaders.Clear()
        client.DefaultRequestHeaders.Add("Client-Id", "804bb4f8-f745-4a38-b9dd-95c09eec6009")
        client.DefaultRequestHeaders.Add("Secret-Key", "Gj5nS9wyYHRadaVffz5VKB4v4wlVWyPhcJvrTD4NHtPk6ZAzKgbQdrVlgQOfzt0r+g4x9HtOiRWkLQJ4aqgmMTG2xL7rI0WJfvFDgzF1wD5uHcfdU0idPw7dZ2s+H4nzaOJzETIhZk9L3JGDan0O58+iiSKAyemIzGbmshoRmbmBPCukJrIZZHSQGXJJjjTv5rUFxpfT3dj7pFYk/ZoUNd6TFrXK1FsdbEQdHMAqYfELi3vGtK24J2gOhy+OJQ6mpTwHn10VoGKGSJLMNzk6vQ==")
        client.DefaultRequestHeaders.Add("User-Agent", "MyInvoiceApp/1.0")
        client.DefaultRequestHeaders.Add("Accept", "*/*")

        ' الخطوة 8: إرسال الطلب
        Try
            Dim response As HttpResponseMessage = Await client.PostAsync(url, contenta)
            Dim responseString As String = Await response.Content.ReadAsStringAsync()
            Console.WriteLine("رمز الحالة: " & response.StatusCode)
            Console.WriteLine("الاستجابة: " & responseString)

            If response.IsSuccessStatusCode Then
                Try
                    Dim jsonResponse As JObject = JObject.Parse(responseString)
                    Dim status As String = jsonResponse("status")?.ToString()
                    Dim message As String = jsonResponse("message")?.ToString()
                    Dim invoiceId As String = jsonResponse("invoice_id")?.ToString()

                    If status = "success" Then
                        Console.WriteLine("تم إرسال الفاتورة بنجاح!")
                        Console.WriteLine("معرف الفاتورة: " & invoiceId)
                        Console.WriteLine("الرسالة: " & message)

                        ' حفظ رمز الاستجابة QR إذا وجد
                        If jsonResponse("qr_code") IsNot Nothing Then
                            Dim qrCodeBase64 = jsonResponse("qr_code").ToString().Replace("data:image/png;base64,", "")
                            SaveQrCode(qrCodeBase64, invoiceId)
                        End If
                    Else
                        Console.WriteLine("فشل إرسال الفاتورة.")
                        Console.WriteLine("الرسالة: " & message)
                    End If
                Catch ex As Exception
                    Console.WriteLine("خطأ في تحليل الاستجابة: " & ex.Message)
                    Console.WriteLine("الاستجابة الخام: " & responseString)
                End Try
            Else
                Console.WriteLine("خطأ HTTP: " & response.StatusCode)
                Console.WriteLine("الاستجابة: " & responseString)
            End If
        Catch ex As Exception
            Console.WriteLine("خطأ في إرسال الطلب: " & ex.Message)
        End Try
    End Function

    Private Sub SaveQrCode(qrCodeBase64 As String, invoiceNumber As String)
        Try
            If Not Directory.Exists(QrCodeFolder) Then
                Directory.CreateDirectory(QrCodeFolder)
            End If

            Dim qrBytes = Convert.FromBase64String(qrCodeBase64)
            Dim filePath = Path.Combine(QrCodeFolder, $"{invoiceNumber}.png")

            Using fs As New FileStream(filePath, FileMode.Create)
                fs.Write(qrBytes, 0, qrBytes.Length)
            End Using

            Console.WriteLine($"تم حفظ رمز QR في: {filePath}")
        Catch ex As Exception
            Console.WriteLine($"خطأ في حفظ رمز QR: {ex.Message}")
        End Try
    End Sub
End Module