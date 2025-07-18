Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Xml

Module EInvoiceVBNet
    Sub Main()
        Try
            ' Step 1: Create the XML invoice
            Dim xmlInvoice As String = CreateUBLInvoice()

            ' Step 2: Send the invoice to the API
            'SendInvoiceToAPI(xmlInvoice)
            Console.WriteLine("Invoice sent successfully.")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
        Console.ReadLine()
    End Sub

    Function CreateUBLInvoice() As String
        ' Create XML document
        Dim doc As New XmlDocument()
        Dim declaration As XmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
        doc.AppendChild(declaration)

        ' Create root element (UBL Invoice)
        Dim invoice As XmlElement = doc.CreateElement("Invoice")
        invoice.SetAttribute("xmlns", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")
        invoice.SetAttribute("xmlns:cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")
        invoice.SetAttribute("xmlns:cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
        doc.AppendChild(invoice)

        ' Add UBL version
        Dim ublVersion As XmlElement = doc.CreateElement("cbc:UBLVersionID")
        ublVersion.InnerText = "2.1"
        invoice.AppendChild(ublVersion)

        ' Invoice ID
        Dim invoiceId As XmlElement = doc.CreateElement("cbc:ID")
        invoiceId.InnerText = "INV" & DateTime.Now.Ticks.ToString()
        invoice.AppendChild(invoiceId)

        ' Issue Date
        Dim issueDate As XmlElement = doc.CreateElement("cbc:IssueDate")
        issueDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd")
        invoice.AppendChild(issueDate)

        ' Invoice Type Code
        Dim invoiceType As XmlElement = doc.CreateElement("cbc:InvoiceTypeCode")
        invoiceType.InnerText = "388" ' Standard invoice
        invoice.AppendChild(invoiceType)

        ' Supplier (Seller) Party
        Dim supplierParty As XmlElement = doc.CreateElement("cac:AccountingSupplierParty")
        invoice.AppendChild(supplierParty)

        Dim supplier As XmlElement = doc.CreateElement("cac:Party")
        supplierParty.AppendChild(supplier)

        Dim supplierTax As XmlElement = doc.CreateElement("cac:PartyTaxScheme")
        Dim taxId As XmlElement = doc.CreateElement("cbc:CompanyID")
        taxId.InnerText = "YOUR_TAX_NUMBER" ' Replace with your tax number
        supplierTax.AppendChild(taxId)
        Dim taxScheme As XmlElement = doc.CreateElement("cac:TaxScheme")
        Dim taxSchemeId As XmlElement = doc.CreateElement("cbc:ID")
        taxSchemeId.InnerText = "VAT"
        taxScheme.AppendChild(taxSchemeId)
        supplierTax.AppendChild(taxScheme)
        supplier.AppendChild(supplierTax)

        Dim supplierName As XmlElement = doc.CreateElement("cac:PartyName")
        Dim name As XmlElement = doc.CreateElement("cbc:Name")
        name.InnerText = "Your Company Name"
        supplierName.AppendChild(name)
        supplier.AppendChild(supplierName)

        ' Customer (Buyer) Party
        Dim customerParty As XmlElement = doc.CreateElement("cac:AccountingCustomerParty")
        invoice.AppendChild(customerParty)

        Dim customer As XmlElement = doc.CreateElement("cac:Party")
        customerParty.AppendChild(customer)

        Dim customerName As XmlElement = doc.CreateElement("cac:PartyName")
        Dim custName As XmlElement = doc.CreateElement("cbc:Name")
        custName.InnerText = "Customer Name"
        customerName.AppendChild(custName)
        customer.AppendChild(customerName)

        ' Invoice Line
        Dim invoiceLine As XmlElement = doc.CreateElement("cac:InvoiceLine")
        invoice.AppendChild(invoiceLine)

        Dim lineId As XmlElement = doc.CreateElement("cbc:ID")
        lineId.InnerText = "1"
        invoiceLine.AppendChild(lineId)

        Dim quantity As XmlElement = doc.CreateElement("cbc:InvoicedQuantity")
        quantity.SetAttribute("unitCode", "unit")
        quantity.InnerText = "10"
        invoiceLine.AppendChild(quantity)

        Dim lineAmount As XmlElement = doc.CreateElement("cbc:LineExtensionAmount")
        lineAmount.SetAttribute("currencyID", "JOD")
        lineAmount.InnerText = "100.00"
        invoiceLine.AppendChild(lineAmount)

        Dim item As XmlElement = doc.CreateElement("cac:Item")
        Dim itemName As XmlElement = doc.CreateElement("cbc:Name")
        itemName.InnerText = "Sample Product"
        item.AppendChild(itemName)
        invoiceLine.AppendChild(item)

        Dim price As XmlElement = doc.CreateElement("cac:Price")
        Dim priceAmount As XmlElement = doc.CreateElement("cbc:PriceAmount")
        priceAmount.SetAttribute("currencyID", "JOD")
        priceAmount.InnerText = "10.00"
        price.AppendChild(priceAmount)
        invoiceLine.AppendChild(price)

        ' Legal Monetary Total
        Dim monetaryTotal As XmlElement = doc.CreateElement("cac:LegalMonetaryTotal")
        Dim lineExtAmount As XmlElement = doc.CreateElement("cbc:LineExtensionAmount")
        lineExtAmount.SetAttribute("currencyID", "JOD")
        lineExtAmount.InnerText = "100.00"
        monetaryTotal.AppendChild(lineExtAmount)

        Dim taxExclusive As XmlElement = doc.CreateElement("cbc:TaxExclusiveAmount")
        taxExclusive.SetAttribute("currencyID", "JOD")
        taxExclusive.InnerText = "100.00"
        monetaryTotal.AppendChild(taxExclusive)

        Dim taxInclusive As XmlElement = doc.CreateElement("cbc:TaxInclusiveAmount")
        taxInclusive.SetAttribute("currencyID", "JOD")
        taxInclusive.InnerText = "116.00" ' Assuming 16% VAT
        monetaryTotal.AppendChild(taxInclusive)

        Dim payableAmount As XmlElement = doc.CreateElement("cbc:PayableAmount")
        payableAmount.SetAttribute("currencyID", "JOD")
        payableAmount.InnerText = "116.00"
        monetaryTotal.AppendChild(payableAmount)
        invoice.AppendChild(monetaryTotal)

        ' Convert to string
        Dim writer As New StringWriter()
        Dim xmlWriter As New XmlTextWriter(writer)
        xmlWriter.Formatting = Formatting.Indented
        doc.WriteTo(xmlWriter)
        xmlWriter.Flush()
        Return writer.ToString()
    End Function

    Async Sub SendInvoiceToAPI3(xmlInvoice As String)
        Using client As New HttpClient()
            ' Set up authentication
            Dim username As String = "YOUR_USERNAME" ' Replace with your username
            Dim secretKey As String = "YOUR_SECRET_KEY" ' Replace with your secret key
            username = "804bb4f8-f745-4a38-b9dd-95c09eec6009"
            secretKey = "Gj5nS9wyYHRadaVffz5VKB4v4wlVWyPhcJvrTD4NHtPk6ZAzKgbQdrVlgQOfzt0r+g4x9HtOiRWkLQJ4aqgmMTG2xL7rI0WJfvFDgzF1wD5uHcfdU0idPw7dZ2s+H4nzaOJzETIhZk9L3JGDan0O58+iiSKAyemIzGbmshoRmbmBPCukJrIZZHSQGXJJjjTv5rUFxpfT3dj7pFYk/ZoUNd6TFrXK1FsdbEQdHMAqYfELi3vGtK24J2gOhy+OJQ6mpTwHn10VoGKGSJLMNzk6vQ=="

            Dim authHeader As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(username & ":" & secretKey))
            client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader)

            ' API endpoint (replace with actual ISTD API endpoint)
            Dim apiUrl As String = "https://backend.jofotara.gov.jo/core/invoices/" ' Hypothetical URL

            ' Prepare content
            Dim content As New StringContent(xmlInvoice, Encoding.UTF8, "application/xml")

            ' Send POST request
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)
            response.EnsureSuccessStatusCode()

            ' Read response
            Dim responseContent As String = Await response.Content.ReadAsStringAsync()
            Console.WriteLine("API Response: " & responseContent)
        End Using
    End Sub
End Module