Imports System.Xml
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports System.Text

Module UBLToPDFConverter

    Sub Main()
        Dim ublXmlPath As String = "invoice.xml" ' مسار ملف UBL2.1
        Dim htmlOutputPath As String = "invoice.html" ' مسار ملف HTML الناتج
        Dim pdfOutputPath As String = "invoice.pdf" ' مسار ملف PDF الناتج

        ' تحويل UBL إلى HTML
        Dim htmlContent As String = ConvertUBLToHTML(ublXmlPath)
        File.WriteAllText(htmlOutputPath, htmlContent)

        ' تحويل HTML إلى PDF
        ConvertHtmlToPdf(htmlContent, pdfOutputPath)

        Console.WriteLine("تم تحويل الفاتورة بنجاح!")
        Console.WriteLine("HTML: " & htmlOutputPath)
        Console.WriteLine("PDF: " & pdfOutputPath)
    End Sub

    Function ConvertUBLToHTML(xmlFilePath As String) As String
        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(xmlFilePath)

        ' إنشاء مساحة الأسماء لإجراء استعلامات XPath
        Dim nsManager As New XmlNamespaceManager(xmlDoc.NameTable)
        nsManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        nsManager.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")

        ' استخراج البيانات الأساسية
        Dim invoiceId As String = xmlDoc.SelectSingleNode("//cbc:ID", nsManager).InnerText
        Dim issueDate As String = xmlDoc.SelectSingleNode("//cbc:IssueDate", nsManager).InnerText
        Dim issueTime As String = xmlDoc.SelectSingleNode("//cbc:IssueTime", nsManager).InnerText
        Dim currency As String = xmlDoc.SelectSingleNode("//cbc:DocumentCurrencyCode", nsManager).InnerText
        Dim totalAmount As String = xmlDoc.SelectSingleNode("//cbc:PayableAmount", nsManager).InnerText

        ' بيانات البائع
        Dim supplierName As String = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty//cbc:RegistrationName", nsManager).InnerText
        Dim supplierTaxId As String = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty//cbc:CompanyID", nsManager).InnerText
        Dim supplierAddress As String = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty//cbc:StreetName", nsManager).InnerText & ", " &
                                       xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty//cbc:CityName", nsManager).InnerText

        ' بيانات المشتري
        Dim customerName As String = xmlDoc.SelectSingleNode("//cac:AccountingCustomerParty//cbc:RegistrationName", nsManager).InnerText
        Dim customerTaxId As String = xmlDoc.SelectSingleNode("//cac:AccountingCustomerParty//cbc:CompanyID", nsManager).InnerText

        ' بنود الفاتورة
        Dim invoiceLines As XmlNodeList = xmlDoc.SelectNodes("//cac:InvoiceLine", nsManager)
        Dim itemsHtml As New StringBuilder()

        For Each line As XmlNode In invoiceLines
            Dim id As String = line.SelectSingleNode("cbc:ID", nsManager).InnerText
            Dim name As String = line.SelectSingleNode("cac:Item/cbc:Name", nsManager).InnerText
            Dim quantity As String = line.SelectSingleNode("cbc:InvoicedQuantity", nsManager).InnerText
            Dim unitPrice As String = line.SelectSingleNode("cac:Price/cbc:PriceAmount", nsManager).InnerText
            Dim lineTotal As String = line.SelectSingleNode("cbc:LineExtensionAmount", nsManager).InnerText

            itemsHtml.AppendLine("<tr>")
            itemsHtml.AppendLine($"<td>{id}</td>")
            itemsHtml.AppendLine($"<td>{name}</td>")
            itemsHtml.AppendLine($"<td>{quantity}</td>")
            itemsHtml.AppendLine($"<td>{unitPrice} {currency}</td>")
            itemsHtml.AppendLine($"<td>{lineTotal} {currency}</td>")
            itemsHtml.AppendLine("</tr>")
        Next

        ' استخراج بيانات الضرائب
        Dim taxTotalHtml As New StringBuilder()
        Dim taxSubtotals As XmlNodeList = xmlDoc.SelectNodes("//cac:TaxTotal/cac:TaxSubtotal", nsManager)

        For Each taxSub As XmlNode In taxSubtotals
            Dim taxableAmount As String = taxSub.SelectSingleNode("cbc:TaxableAmount", nsManager).InnerText
            Dim taxAmount As String = taxSub.SelectSingleNode("cbc:TaxAmount", nsManager).InnerText
            Dim taxPercent As String = taxSub.SelectSingleNode("cac:TaxCategory/cbc:Percent", nsManager).InnerText
            Dim taxScheme As String = taxSub.SelectSingleNode("cac:TaxCategory/cac:TaxScheme/cbc:ID", nsManager).InnerText

            Dim taxName As String = If(taxScheme = "VAT", "ضريبة القيمة المضافة", "ضريبة المبيعات")

            taxTotalHtml.AppendLine("<tr>")
            taxTotalHtml.AppendLine($"<td>{taxName}</td>")
            taxTotalHtml.AppendLine($"<td>{taxPercent}%</td>")
            taxTotalHtml.AppendLine($"<td>{taxableAmount} {currency}</td>")
            taxTotalHtml.AppendLine($"<td>{taxAmount} {currency}</td>")
            taxTotalHtml.AppendLine("</tr>")
        Next

        ' استخراج المجموعات المالية
        Dim legalMonetaryTotal As XmlNode = xmlDoc.SelectSingleNode("//cac:LegalMonetaryTotal", nsManager)
        Dim lineExtensionAmount As String = legalMonetaryTotal.SelectSingleNode("cbc:LineExtensionAmount", nsManager).InnerText
        Dim taxExclusiveAmount As String = legalMonetaryTotal.SelectSingleNode("cbc:TaxExclusiveAmount", nsManager).InnerText
        Dim taxInclusiveAmount As String = legalMonetaryTotal.SelectSingleNode("cbc:TaxInclusiveAmount", nsManager).InnerText
        Dim allowanceTotalAmount As String = legalMonetaryTotal.SelectSingleNode("cbc:AllowanceTotalAmount", nsManager).InnerText
        Dim payableAmount As String = legalMonetaryTotal.SelectSingleNode("cbc:PayableAmount", nsManager).InnerText

        ' إنشاء HTML
        Dim htmlTemplate As String = $"
        <!DOCTYPE html>
        <html dir='rtl'>
        <head>
            <meta charset='UTF-8'>
            <title>فاتورة #{invoiceId}</title>
            <style>
                body {{ font-family: Arial, sans-serif; margin: 0; padding: 20px; }}
                .invoice {{ max-width: 800px; margin: 0 auto; border: 1px solid #ddd; padding: 20px; }}
                .header {{ text-align: center; margin-bottom: 20px; }}
                .details {{ display: flex; justify-content: space-between; margin-bottom: 30px; }}
                .supplier, .customer {{ width: 45%; }}
                table {{ width: 100%; border-collapse: collapse; margin-bottom: 20px; }}
                th, td {{ border: 1px solid #ddd; padding: 8px; text-align: right; }}
                th {{ background-color: #f2f2f2; }}
                .total {{ text-align: left; font-weight: bold; font-size: 1.2em; }}
                .footer {{ margin-top: 30px; text-align: center; font-size: 0.9em; color: #666; }}
                .section-title {{ background-color: #f5f5f5; padding: 8px; margin: 20px 0 10px 0; }}
            </style>
        </head>
        <body>
            <div class='invoice'>
                <div class='header'>
                    <h1>فاتورة ضريبية مبسطة</h1>
                    <p>رقم الفاتورة: {invoiceId} | تاريخ الاصدار: {issueDate} {issueTime}</p>
                </div>

                <div class='details'>
                    <div class='supplier'>
                        <h3>البائع:</h3>
                        <p>{supplierName}</p>
                        <p>الرقم الضريبي: {supplierTaxId}</p>
                        <p>{supplierAddress}</p>
                    </div>

                    <div class='customer'>
                        <h3>المشتري:</h3>
                        <p>{customerName}</p>
                        <p>الرقم الضريبي: {customerTaxId}</p>
                    </div>
                </div>

                <div class='section-title'>تفاصيل البنود</div>
                <table>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>الوصف</th>
                            <th>الكمية</th>
                            <th>سعر الوحدة</th>
                            <th>المجموع</th>
                        </tr>
                    </thead>
                    <tbody>
                        {itemsHtml.ToString()}
                    </tbody>
                </table>

                <div class='section-title'>تفاصيل الضرائب</div>
                <table>
                    <thead>
                        <tr>
                            <th>نوع الضريبة</th>
                            <th>النسبة</th>
                            <th>المبلغ الخاضع للضريبة</th>
                            <th>مبلغ الضريبة</th>
                        </tr>
                    </thead>
                    <tbody>
                        {taxTotalHtml.ToString()}
                    </tbody>
                </table>

                <div class='section-title'>المجموعات المالية</div>
                <table>
                    <tr>
                        <td>المجموع قبل الضريبة:</td>
                        <td>{taxExclusiveAmount} {currency}</td>
                    </tr>
                    <tr>
                        <td>مجموع الضرائب:</td>
                        <td>{CDbl(taxInclusiveAmount) - CDbl(taxExclusiveAmount)} {currency}</td>
                    </tr>
                    <tr>
                        <td>المجموع بعد الضريبة:</td>
                        <td>{taxInclusiveAmount} {currency}</td>
                    </tr>
                    <tr>
                        <td>إجمالي الخصومات:</td>
                        <td>{allowanceTotalAmount} {currency}</td>
                    </tr>
                    <tr style='font-weight:bold;'>
                        <td>المبلغ المستحق:</td>
                        <td>{payableAmount} {currency}</td>
                    </tr>
                </table>

                <div class='footer'>
                    <p>شكراً لتعاملكم معنا</p>
                    <p>هذه الفاتورة تم إنشاؤها تلقائياً ولا تحتاج إلى توقيع</p>
                </div>
            </div>
        </body>
        </html>"

        Return htmlTemplate
    End Function

    Sub ConvertHtmlToPdf(htmlContent As String, outputPath As String)
        Using stream As New FileStream(outputPath, FileMode.Create)
            Dim document As New Document()
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, stream)
            document.Open()

            Dim htmlWorker As New HTMLWorker(document)
            Using sr As New StringReader(htmlContent)
                htmlWorker.Parse(sr)
            End Using

            document.Close()
        End Using
    End Sub
End Module