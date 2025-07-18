Imports System.Data.SQLite
Imports System.Drawing.Printing
Imports System.Text
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class InvoicePrintForm
    Inherits Form

    Private invoiceId As Long
    Private previewFirst As Boolean
    Private printDocument As New PrintDocument()
    Private PrintPreviewDialog1 As New PrintPreviewDialog
    Private invoiceData As DataTable
    Private customerData As DataRow
    Private orderData As DataTable
    Private companyData As DataRow
    Private isPosPrinter As Boolean = True
    Private pageWidth As Integer
    Private pageHeight As Integer
    Private currentY As Integer
    Private currentX As Integer
    Private fontHeader As Font
    Private fontRegular As Font
    Private fontRegularSMALL As Font
    Private fontCOMPANY As Font
    Private fontTableHeader As Font
    Private fontTableRow As Font
    Private companyLogo As Image = Nothing
    Private qrCodeImage As Image = Nothing
    Public Sub New(invoiceId As Long, previewFirst As Boolean)
        Me.invoiceId = invoiceId
        Me.previewFirst = previewFirst
        LoadInvoiceData()
        ' Generate QR code
        GenerateQRCode()

        ' Load company logo
        LoadCompanyLogo()
        '  LoadCompanyLogoFromBlob()
        ' InitializeComponents()
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
        STRART_PRINT()
        Me.Close()
    End Sub
    Private Sub GenerateQRCode()
        If invoiceData IsNot Nothing AndAlso invoiceData.Rows.Count > 0 Then
            Dim eCode = invoiceData.Rows(0)("E_CODE").ToString()
            If Not String.IsNullOrEmpty(eCode) Then
                Dim writer As New ZXing.BarcodeWriter()
                writer.Format = ZXing.BarcodeFormat.QR_CODE
                writer.Options = New ZXing.QrCode.QrCodeEncodingOptions() With {
                    .DisableECI = True,
                    .CharacterSet = "UTF-8",
                    .Width = 150,
                    .Height = 150
                }
                Dim result = writer.Write(eCode)
                qrCodeImage = New Bitmap(result)
            End If
        End If
    End Sub
    Private Sub LoadCompanyLogoFromBlob()
        If companyData IsNot Nothing AndAlso companyData.Table.Columns.Contains("COMPANY_LOGO") Then
            Dim logoBytes As Byte() = TryCast(companyData("COMPANY_LOGO"), Byte())
            ' MsgBox(logoBytes.Length)
            If logoBytes IsNot Nothing AndAlso logoBytes.Length > 0 Then
                Try
                    Using ms As New MemoryStream(logoBytes)
                        companyLogo = Image.FromStream(ms)
                        ' Resize to appropriate dimensions
                        companyLogo = New Bitmap(companyLogo, New Size(100, 100))
                    End Using
                Catch ex As Exception
                    ' MsgBox(ex.Message)
                    companyLogo = Nothing
                End Try
            End If
        End If
    End Sub
    Private Sub LoadCompanyLogo()
        ' If companyData IsNot Nothing AndAlso companyData.Table.Columns.Contains("LOGO_PATH") Then
        Dim logoPath = Application.StartupPath & "\logo.png" ' companyData("LOGO_PATH").ToString()
        ' MsgBox(logoPath)
        If File.Exists(logoPath) Then
            ' MsgBox(logoPath)
            Try
                    companyLogo = Image.FromFile(logoPath)
                    ' Resize if needed
                    companyLogo = New Bitmap(companyLogo, New Size(100, 100))
                Catch ex As Exception
                ' MsgBox(ex.Message)
                companyLogo = Nothing
                End Try
            End If
        ' End If
    End Sub
    Private Sub InitializeComponents()
        'Me.Text = "طباعة الفاتورة"
        'Me.Size = New Size(300, 200)
        'Me.RightToLeft = RightToLeft.Yes

        'Dim btnPrint As New Button()
        'btnPrint.Text = "طباعة"
        'btnPrint.Location = New Point(50, 50)
        'btnPrint.Size = New Size(80, 25)
        'AddHandler btnPrint.Click, AddressOf btnPrint_Click
        'Me.Controls.Add(btnPrint)

        'Dim btnExport As New Button()
        'btnExport.Text = "تصدير PDF"
        'btnExport.Location = New Point(150, 50)
        'btnExport.Size = New Size(80, 25)
        'AddHandler btnExport.Click, AddressOf btnExport_Click
        'Me.Controls.Add(btnExport)



        ' Auto-print if previewFirst is False
        ' If Not previewFirst Then

        ' End If
    End Sub

    Public Sub LoadInvoiceData()
        Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
            conn.Open()

            ' Load company data
            Dim companyCmd As New SQLiteCommand("SELECT COMPANY_NAME, COMPANY_TEL, COMPANY_TAX_NO FROM COM_SEETING WHERE ID = 1", conn)
            Dim companyAdapter As New SQLiteDataAdapter(companyCmd)
            Dim companyTable As New DataTable()
            companyAdapter.Fill(companyTable)
            companyData = If(companyTable.Rows.Count > 0, companyTable.Rows(0), Nothing)

            ' Load invoice data
            Dim invoiceCmd As New SQLiteCommand("SELECT INV.*, IPK.TXT AS PAY_KIND, IK.TXT AS INV_KIND, CT.C_CODE AS CURRENCY " &
                                               "FROM INVOICES INV " &
                                               "LEFT JOIN INV_PAY_KIND IPK ON INV.INV_PAY_KIND = IPK.ID " &
                                               "LEFT JOIN IN_KIND IK ON INV.INV_KIND = IK.ID " &
                                               "LEFT JOIN CURRENCY_TABLE CT ON INV.CURRENCY_KIND = CT.ID " &
                                               "WHERE INV.ID = @id", conn)
            invoiceCmd.Parameters.AddWithValue("@id", invoiceId)
            Dim invoiceAdapter As New SQLiteDataAdapter(invoiceCmd)
            invoiceData = New DataTable()
            invoiceAdapter.Fill(invoiceData)

            ' Load customer data
            Dim customerCmd As New SQLiteCommand("SELECT C_NAME, C_TEL, E_ID FROM CUSTOMERS WHERE ID = (SELECT C_ID FROM INVOICES WHERE ID = @id)", conn)
            customerCmd.Parameters.AddWithValue("@id", invoiceId)
            Dim customerAdapter As New SQLiteDataAdapter(customerCmd)
            Dim customerTable As New DataTable()
            customerAdapter.Fill(customerTable)
            customerData = If(customerTable.Rows.Count > 0, customerTable.Rows(0), Nothing)

            ' Load order data
            Dim orderCmd As New SQLiteCommand("SELECT OP.PRO_NAME,OP.PRO_PRICE, OP.PRO_COUNT, OP.FINAL_VALUE,OP.PRO_DISCOUNT, OP.VALUE_OF_TAX,PR.BARCODE FROM ORDER_PRODUCTS OP LEFT JOIN PRODUCTS PR ON OP.PRO_NAME=PR.PRO_NAME WHERE INV_ID = @id ORDER BY OP.PRO_NAME", conn)
            orderCmd.Parameters.AddWithValue("@id", invoiceId)
            Dim orderAdapter As New SQLiteDataAdapter(orderCmd)
            orderData = New DataTable()
            orderAdapter.Fill(orderData)
        End Using
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        STRART_PRINT()
    End Sub

    Public Sub STRART_PRINT()
        ' Detect printer type
        Dim printerSettings As New PrinterSettings()
        Dim paperSizes = printerSettings.PaperSizes
        Dim defaultPaperSize = paperSizes.Cast(Of PaperSize).FirstOrDefault(Function(ps) ps.RawKind = printerSettings.DefaultPageSettings.PaperSize.RawKind)
        isPosPrinter = If(defaultPaperSize IsNot Nothing AndAlso defaultPaperSize.Width <= 315, True, False)

        ' Set page dimensions
        If isPosPrinter Then
            pageWidth = 315
            pageHeight = 1000
        Else
            pageWidth = 827
            pageHeight = 1169
        End If

        fontHeader = New Font("Cairo", If(isPosPrinter, 10, 12), FontStyle.Bold)
        fontRegular = New Font("Cairo", If(isPosPrinter, 8, 10))
        fontRegularSMALL = New Font("Cairo", If(isPosPrinter, 8, 8))
        fontTableHeader = New Font("Cairo", If(isPosPrinter, 8, 10), FontStyle.Bold)
        fontTableRow = New Font("Cairo", If(isPosPrinter, 7, 9))
        fontCOMPANY = New Font("Cairo", If(isPosPrinter, 7, 15), FontStyle.Bold)
        printDocument.DefaultPageSettings.PaperSize = New PaperSize("Custom", pageWidth, pageHeight)

        If previewFirst Then
            PrintPreviewDialog1.Document = printDocument
            PrintPreviewDialog1.ShowDialog()
        Else
            '  Dim printDialog As New PrintDialog()
            '  printDialog.Document = printDocument
            ' If printDialog.ShowDialog() = DialogResult.OK Then
            printDocument.Print()
            ' End If
        End If
    End Sub



    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        currentY = 10
        currentX = 5
        Dim g As Graphics = e.Graphics
        DrawInvoiceLayout(g)
        e.HasMorePages = False
    End Sub
    Private Sub DrawInvoiceLayout_old1(g As Graphics)
        Dim margin As Integer = If(isPosPrinter, 5, 20)
        Dim contentWidth As Integer = pageWidth - 2 * margin

        ' Green wave header
        Dim headerPath As New GraphicsPath()
        headerPath.AddArc(margin, currentY, 100, 100, 0, 180)
        headerPath.AddArc(pageWidth - margin - 100, currentY, 100, 100, 0, -180)
        headerPath.CloseFigure()
        Using headerBrush As New SolidBrush(Color.FromArgb(0, 128, 0)) ' Green
            g.FillPath(headerBrush, headerPath)
        End Using
        g.DrawPath(Pens.Black, headerPath)

        ' Company Logo and Tagline
        Dim logoText As String = "COMPANY LOGO"
        Dim taglineText As String = "TAGLINE HERE"
        Dim logoSize As SizeF = g.MeasureString(logoText, fontHeader)
        Dim taglineSize As SizeF = g.MeasureString(taglineText, fontRegular)
        g.DrawString(logoText, fontHeader, Brushes.White, margin + 10, currentY + 20)
        g.DrawString(taglineText, fontRegular, Brushes.White, margin + 10, currentY + 50)

        ' Invoice Title
        g.DrawString("INVOICE", fontHeader, Brushes.Black, margin + 10, currentY + 120)

        ' Invoice Details (Left)
        Dim invoiceTo As String = "Invoice to:" & vbCrLf &
                                 "Your project name" & vbCrLf &
                                 "A - Your address, #4310 city here," & vbCrLf &
                                 "E - your@gmail.com" & vbCrLf &
                                 "W - Your website here" & vbCrLf &
                                 "P - +000 131785634"
        Dim invoiceInfo As String = "Invoice # 12345676" & vbCrLf &
                                   "Date 01-01-2023"
        g.DrawString(invoiceTo, fontRegular, Brushes.Black, margin + 10, currentY + 150)
        g.DrawString(invoiceInfo, fontRegular, Brushes.Black, pageWidth - margin - 100, currentY + 150)

        currentY += 250

        ' Items Table
        Dim tableRect As New Rectangle(margin, currentY, contentWidth, (orderData.Rows.Count + 1) * 25)
        g.FillRectangle(Brushes.LightGreen, tableRect)
        g.DrawRectangle(Pens.Black, tableRect)

        Dim col1Width As Integer = contentWidth * 0.1 ' S/L
        Dim col2Width As Integer = contentWidth * 0.4 ' Item Description
        Dim col3Width As Integer = contentWidth * 0.15 ' Qty
        Dim col4Width As Integer = contentWidth * 0.15 ' Rate
        Dim col5Width As Integer = contentWidth * 0.2 ' Amount

        ' Table Headers
        g.DrawString("S/L", fontTableHeader, Brushes.White, margin + 5, currentY + 5)
        g.DrawLine(Pens.White, margin + col1Width, currentY, margin + col1Width, currentY + 25)
        g.DrawString("ITEM DESCRIPTION", fontTableHeader, Brushes.White, margin + col1Width + 5, currentY + 5)
        g.DrawLine(Pens.White, margin + col1Width + col2Width, currentY, margin + col1Width + col2Width, currentY + 25)
        g.DrawString("QTY", fontTableHeader, Brushes.White, margin + col1Width + col2Width + 5, currentY + 5)
        g.DrawLine(Pens.White, margin + col1Width + col2Width + col3Width, currentY, margin + col1Width + col2Width + col3Width, currentY + 25)
        g.DrawString("RATE", fontTableHeader, Brushes.White, margin + col1Width + col2Width + col3Width + 5, currentY + 5)
        g.DrawLine(Pens.White, margin + col1Width + col2Width + col3Width + col4Width, currentY, margin + col1Width + col2Width + col3Width + col4Width, currentY + 25)
        g.DrawString("AMOUNT", fontTableHeader, Brushes.White, margin + col1Width + col2Width + col3Width + col4Width + 5, currentY + 5)
        currentY += 25

        ' Table Rows
        For i As Integer = 0 To orderData.Rows.Count - 1
            Dim row As DataRow = orderData.Rows(i)
            Dim rowBrush As Brush = If(i Mod 2 = 0, Brushes.White, Brushes.LightGray)
            g.FillRectangle(rowBrush, margin, currentY, contentWidth, 25)
            g.DrawString((i + 1).ToString(), fontTableRow, Brushes.Black, margin + 5, currentY + 5)
            g.DrawLine(Pens.Black, margin + col1Width, currentY, margin + col1Width, currentY + 25)
            g.DrawString(row("PRO_NAME").ToString(), fontTableRow, Brushes.Black, margin + col1Width + 5, currentY + 5)
            g.DrawLine(Pens.Black, margin + col1Width + col2Width, currentY, margin + col1Width + col2Width, currentY + 25)
            g.DrawString(Convert.ToDouble(row("PRO_COUNT")).ToString("F2"), fontTableRow, Brushes.Black, margin + col1Width + col2Width + 5, currentY + 5)
            g.DrawLine(Pens.Black, margin + col1Width + col2Width + col3Width, currentY, margin + col1Width + col2Width + col3Width, currentY + 25)
            g.DrawString((Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT"))).ToString("F2"), fontTableRow, Brushes.Black, margin + col1Width + col2Width + col3Width + 5, currentY + 5)
            g.DrawLine(Pens.Black, margin + col1Width + col2Width + col3Width + col4Width, currentY, margin + col1Width + col2Width + col3Width + col4Width, currentY + 25)
            g.DrawString(Convert.ToDouble(row("FINAL_VALUE")).ToString("F2"), fontTableRow, Brushes.Black, margin + col1Width + col2Width + col3Width + col4Width + 5, currentY + 5)
            currentY += 25
        Next
        g.DrawRectangle(Pens.Black, tableRect)

        currentY += 20

        ' Summary
        Dim itemValue As Double = Convert.ToDouble(invoiceData.Rows(0)("ITEM_VALUE"))
        Dim taxValue As Double = Convert.ToDouble(invoiceData.Rows(0)("TAX_VALUE"))
        Dim invValue As Double = Convert.ToDouble(invoiceData.Rows(0)("INV_VALUE"))

        Dim summary As String = "Sub Total:" & vbTab & itemValue.ToString("F2") & vbCrLf &
                               "Tax:" & vbTab & taxValue.ToString("F2") & vbCrLf &
                               "Total:" & vbTab & invValue.ToString("F2")
        g.DrawString(summary, fontRegular, Brushes.Black, margin + 10, currentY)

        currentY += 80

        ' Payment Info
        Dim paymentInfo As String = "Payment Info:" & vbCrLf &
                                   "Account #: 1234 5678 9012" & vbCrLf &
                                   "A/C Name: Lorem ipsum" & vbCrLf &
                                   "Bank Details: Add your bank details"
        g.DrawString(paymentInfo, fontRegular, Brushes.Black, margin + 10, currentY)

        currentY += 60

        ' Green wave footer
        Dim footerPath As New GraphicsPath()
        footerPath.AddArc(margin, currentY, 100, 100, 0, 180)
        footerPath.AddArc(pageWidth - margin - 100, currentY, 100, 100, 0, -180)
        footerPath.CloseFigure()
        Using footerBrush As New SolidBrush(Color.FromArgb(0, 128, 0)) ' Green
            g.FillPath(footerBrush, footerPath)
        End Using
        g.DrawPath(Pens.Black, footerPath)

        ' Footer Content
        Dim qrText As String = "QR"
        Dim phoneText As String = "+000 23354746"
        Dim websiteText As String = "Your @ gmail here"
        Dim addressText As String = "Street address here, city name, zip code"

        Dim footerX As Integer = margin + 10
        g.DrawRectangle(Pens.Black, footerX, currentY + 20, 50, 50)
        g.DrawString(qrText, fontRegular, Brushes.White, footerX + 15, currentY + 35)
        g.DrawString(phoneText, fontRegular, Brushes.White, footerX + 70, currentY + 25)
        g.DrawString(websiteText, fontRegular, Brushes.White, footerX + 70, currentY + 45)
        g.DrawString(addressText, fontRegular, Brushes.White, footerX + 200, currentY + 35)
    End Sub

    Private Sub DrawInvoiceLayout_old(g As Graphics)
        Dim margin As Integer = If(isPosPrinter, 5, 20)
        Dim contentWidth As Integer = pageWidth - 2 * margin
        ' Draw QR Code
        If qrCodeImage IsNot Nothing Then
            g.DrawImage(qrCodeImage, pageWidth - margin - 170, currentY + 5, 170, 170)
        Else
            g.DrawRectangle(Pens.Black, pageWidth - margin - 170, currentY + 5, 170, 170)
            g.DrawString("QR", fontRegular, Brushes.Black, pageWidth - margin - 45, currentY + 85)
        End If

        ' Draw Company Logo
        If companyLogo IsNot Nothing Then
            Dim logoWidth As Integer = CInt(pageWidth * 0.25)
            ' Calculate proportional height maintaining aspect ratio
            Dim aspectRatio As Single = companyLogo.Height / companyLogo.Width
            Dim logoHeight As Integer = CInt(logoWidth * aspectRatio)

            ' Center the logo horizontally
            Dim logoX As Integer = margin + (contentWidth - logoWidth) / 2

            g.DrawImage(companyLogo, logoX, currentY + 5, logoWidth, logoHeight)
        Else
            ' Fallback if no logo
            Dim logoX As Integer = margin + (contentWidth - 100) / 2
            g.DrawString("[شعار الشركة]", fontHeader, Brushes.DarkBlue, logoX, currentY + 30)
        End If

        currentY += 120

        ' Customer Info
        Dim customerInfo As String = "الاسم: " & If(customerData IsNot Nothing, customerData("C_NAME").ToString(), "زبون نقدي") & vbCrLf &
                                    "الهاتف: " & If(customerData IsNot Nothing, customerData("C_TEL").ToString(), "-") & vbCrLf &
                                    "الرقم الضريبي: " & If(customerData IsNot Nothing, customerData("E_ID").ToString(), "-")
        g.DrawString(customerInfo, fontRegular, Brushes.Black, pageWidth - margin - contentWidth + 5, currentY + 5)
        currentY += 70

        ' Items Table
        g.DrawString("بيانات المواد", fontHeader, Brushes.Navy, pageWidth - margin - 80, currentY)
        currentY += 30
        Dim tableRect As New Rectangle(margin, currentY, contentWidth, (orderData.Rows.Count + 1) * 25)
        g.FillRectangle(Brushes.White, tableRect)
        g.DrawRectangle(Pens.DarkGray, tableRect)

        Dim colWidths As New List(Of Integer)
        If isPosPrinter Then
            colWidths.Add(contentWidth * 0.5) ' Material name
            colWidths.Add(contentWidth * 0.2) ' Price with tax
            colWidths.Add(contentWidth * 0.15) ' Quantity
            colWidths.Add(contentWidth * 0.15) ' Total
        Else
            colWidths.Add(contentWidth * 0.15) ' Barcode
            colWidths.Add(contentWidth * 0.3) ' Material name
            colWidths.Add(contentWidth * 0.15) ' Price
            colWidths.Add(contentWidth * 0.1) ' Quantity
            colWidths.Add(contentWidth * 0.1) ' Discount
            colWidths.Add(contentWidth * 0.1) ' Tax
            colWidths.Add(contentWidth * 0.1) ' Total
        End If

        ' Draw table headers with internal borders
        Dim xPos As Integer = pageWidth - margin
        If isPosPrinter Then
            g.DrawString("المادة", fontTableHeader, Brushes.Black, xPos - colWidths(0), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0), currentY, xPos - colWidths(0), currentY + 25)
            g.DrawString("السعر شامل الضريبة", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1), currentY, xPos - colWidths(0) - colWidths(1), currentY + 25)
            g.DrawString("الكمية", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 25)
            g.DrawString("الإجمالي", fontTableHeader, Brushes.Black, margin, currentY + 5)
        Else
            g.DrawString("الباركود", fontTableHeader, Brushes.Black, xPos - colWidths(0), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0), currentY, xPos - colWidths(0), currentY + 25)
            g.DrawString("اسم المادة", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1), currentY, xPos - colWidths(0) - colWidths(1), currentY + 25)
            g.DrawString("السعر", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 25)
            g.DrawString("الكمية", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3), currentY + 25)
            g.DrawString("الخصم", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4), currentY + 25)
            g.DrawString("الضريبة", fontTableHeader, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4) - colWidths(5), currentY + 5)
            g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4) - colWidths(5), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4) - colWidths(5), currentY + 25)
            g.DrawString("الإجمالي", fontTableHeader, Brushes.Black, margin, currentY + 5)
        End If
        currentY += 25

        ' Draw table rows with alternating colors and internal borders
        For i As Integer = 0 To orderData.Rows.Count - 1
            Dim row As DataRow = orderData.Rows(i)
            Dim rowBrush As Brush = If(i Mod 2 = 0, Brushes.White, Brushes.LightGray)
            g.FillRectangle(rowBrush, margin, currentY, contentWidth, 25)
            If isPosPrinter Then
                Dim priceWithTax As Double = Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT")) * (1 + Convert.ToDouble(row("TAX_VALUE")) / 100)
                g.DrawString(row("PRO_NAME").ToString(), fontTableRow, Brushes.Black, xPos - colWidths(0), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0), currentY, xPos - colWidths(0), currentY + 25)
                g.DrawString(priceWithTax.ToString("F3"), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1), currentY, xPos - colWidths(0) - colWidths(1), currentY + 25)
                g.DrawString(Convert.ToDouble(row("PRO_COUNT")).ToString("F3"), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 25)
                g.DrawString(Convert.ToDouble(row("FINAL_VALUE")).ToString("F3"), fontTableRow, Brushes.Black, margin, currentY + 5)
            Else
                g.DrawString(row("BARCODE").ToString(), fontTableRow, Brushes.Black, xPos - colWidths(0), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0), currentY, xPos - colWidths(0), currentY + 25)
                g.DrawString(row("PRO_NAME").ToString(), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1), currentY, xPos - colWidths(0) - colWidths(1), currentY + 25)
                ' g.DrawString((Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT"))).ToString("F3"), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 5)
                g.DrawString((Convert.ToDouble(row("PRO_PRICE"))).ToString("F3"), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 5)

                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 25)
                g.DrawString(Convert.ToDouble(row("PRO_COUNT")).ToString("F3"), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3), currentY + 25)
                g.DrawString("0.00", fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4), currentY + 5) ' Placeholder for discount
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4), currentY + 25)
                g.DrawString(row("VALUE_OF_TAX").ToString(), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4) - colWidths(5), currentY + 5)
                g.DrawLine(Pens.DarkGray, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4) - colWidths(5), currentY, xPos - colWidths(0) - colWidths(1) - colWidths(2) - colWidths(3) - colWidths(4) - colWidths(5), currentY + 25)
                g.DrawString(Convert.ToDouble(row("FINAL_VALUE")).ToString("F3"), fontTableRow, Brushes.Black, margin, currentY + 5)
            End If
            currentY += 25
        Next
        g.DrawRectangle(Pens.DarkGray, tableRect)

        ' Summary (right-aligned)
        currentY += 20
        Dim itemValue As Double = Convert.ToDouble(invoiceData.Rows(0)("ITEM_VALUE"))
        Dim discountValue As Double = Convert.ToDouble(invoiceData.Rows(0)("DISCOUNT_VALUE"))
        Dim taxValue As Double = Convert.ToDouble(invoiceData.Rows(0)("TAX_VALUE"))
        Dim invValue As Double = Convert.ToDouble(invoiceData.Rows(0)("INV_VALUE"))
        Dim priceAfterDiscount As Double = itemValue - discountValue

        Dim summary As String = "إجمالي المواد: " & itemValue.ToString("F3") & vbCrLf &
                               "كمية الخصم: " & discountValue.ToString("F3") & vbCrLf &
                               "السعر بعد الخصم: " & priceAfterDiscount.ToString("F3") & vbCrLf &
                               "إجمالي الضريبة: " & taxValue.ToString("F3") & vbCrLf &
                               "الإجمالي: " & invValue.ToString("F3")
        Dim summarySize As SizeF = g.MeasureString(summary, fontHeader)
        g.DrawString(summary, fontHeader, Brushes.Navy, pageWidth - margin - summarySize.Width, currentY + 5) ' Right-aligned
        currentY += 100

        ' Footer (right-aligned)
        Dim footer As String = "نوع الفاتورة: " & invoiceData.Rows(0)("INV_KIND").ToString() & vbCrLf &
                              "طريقة الدفع: " & invoiceData.Rows(0)("PAY_KIND").ToString()
        Dim footerSize As SizeF = g.MeasureString(footer, fontRegular)
        g.DrawString(footer, fontRegular, Brushes.DarkGreen, pageWidth - margin - footerSize.Width, currentY + 5) ' Right-aligned
    End Sub
    Private Sub DrawInvoiceLayout(g As Graphics)
        Dim rtlFormat As New StringFormat()
        rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
        Dim margin As Integer = If(isPosPrinter, 5, 20)
        Dim contentWidth As Integer = pageWidth - 2 * margin
        Dim TEXT_SIZ As SizeF
        Dim MAXY As Integer
        ' Draw QR Code
        If qrCodeImage IsNot Nothing Then
            g.DrawImage(qrCodeImage, margin + 5, currentY + 5, 150, 150)
        Else
            g.DrawRectangle(Pens.Black, margin + 5, currentY + 5, 150, 150)
            g.DrawString("QR", fontRegular, Brushes.Black, pageWidth - margin - 45, currentY + 75)
        End If
        MAXY = currentY + 155
        Dim logoWidth As Integer = 150
        Dim logoHeight As Integer = 150
        Dim YY As Integer
        Dim XX As Integer
        ' Draw Company Logo
        If companyLogo IsNot Nothing Then
            logoWidth = CInt(pageWidth * 0.2)
            ' Calculate proportional height maintaining aspect ratio
            Dim aspectRatio As Single = companyLogo.Height / companyLogo.Width
            logoHeight = CInt(logoWidth * aspectRatio)

            ' Center the logo horizontally
            Dim logoX As Integer = pageWidth - margin - logoWidth - 5

            g.DrawImage(companyLogo, logoX, currentY + 5, logoWidth, logoHeight)
            YY = currentY + 5 + logoHeight
        Else
            ' Fallback if no logo
            Dim logoX1 As Integer = pageWidth - margin - 150
            g.DrawString("[شعار الشركة]", fontHeader, Brushes.DarkBlue, logoX1, currentY + 150)
            YY = currentY + 150
        End If
        If YY > MAXY Then MAXY = YY
        Dim COPANYX As Integer = pageWidth - margin - logoWidth - 5
        Dim customerInfo As String
        customerInfo = If(companyData IsNot Nothing, companyData("COMPANY_NAME").ToString(), "شركة المثالية")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontCOMPANY, g)
        g.DrawString(customerInfo, fontCOMPANY, Brushes.Black, COPANYX - 5 - TEXT_SIZ.Width, currentY + 15)
        currentY = currentY + 4 + TEXT_SIZ.Height
        customerInfo = If(companyData IsNot Nothing, "هاتف: " & companyData("COMPANY_TEL").ToString(), "0785750652")
        customerInfo = customerInfo + If(companyData IsNot Nothing, "رقم ضريبي: " & companyData("COMPANY_TAX_NO").ToString(), "0000")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegularSMALL, g)
        g.DrawString(customerInfo, fontRegularSMALL, Brushes.Black, COPANYX - 5 - TEXT_SIZ.Width, currentY)
        YY = currentY + TEXT_SIZ.Height + 5
        If YY > MAXY Then MAXY = YY
        currentY = MAXY + 2
        Dim MPEN As Pen = New Pen(Color.Blue, 2)

        g.DrawLine(MPEN, margin, currentY, pageWidth - margin, currentY)
        currentY = currentY + 2
        ' Customer Info
        customerInfo = "الاسم: " & If(customerData IsNot Nothing, customerData("C_NAME").ToString(), "زبون نقدي")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, pageWidth - margin - 3, currentY, rtlFormat)

        XX = pageWidth * 0.3
        customerInfo = "رقم الفاتورة الالكتروني: " & If(customerData IsNot Nothing, invoiceData.Rows(0)("E_CODE").ToString(), "-")

        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)

        g.DrawString(customerInfo, fontRegular, Brushes.Black, XX, currentY, rtlFormat)



        currentY = currentY + TEXT_SIZ.Height
        customerInfo = "الهاتف: " & If(customerData IsNot Nothing, customerData("C_TEL").ToString(), "-")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, pageWidth - margin - 3, currentY, rtlFormat)

        customerInfo = "رقم الفاتورة  : " & If(customerData IsNot Nothing, invoiceData.Rows(0)("ID").ToString(), "-")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, XX, currentY, rtlFormat)



        currentY = currentY + TEXT_SIZ.Height
        customerInfo = "الرقم الضريبي: " & If(customerData IsNot Nothing, customerData("E_ID").ToString(), "-")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, pageWidth - margin - 3, currentY, rtlFormat)
        customerInfo = "تاريخها :  " & If(customerData IsNot Nothing, invoiceData.Rows(0)("DATE_TIME").ToString(), "-")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, XX, currentY, rtlFormat)

        currentY = currentY + TEXT_SIZ.Height

        customerInfo = "الدفع :  " & If(customerData IsNot Nothing, invoiceData.Rows(0)("PAY_KIND").ToString(), "-")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, XX, currentY, rtlFormat)

        currentY = currentY + TEXT_SIZ.Height
        customerInfo = "نوع الفاتورة: " & If(customerData IsNot Nothing, invoiceData.Rows(0)("INV_KIND").ToString(), "-")
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        g.DrawString(customerInfo, fontRegular, Brushes.Black, XX, currentY, rtlFormat)

        currentY = currentY + TEXT_SIZ.Height

        '  latexContent.AppendLine("\textbf{اسم الشركة: " & If(companyData IsNot Nothing, companyData("COMPANY_NAME").ToString(), "شركة المثالية") & "} \\")
        '  latexContent.AppendLine("\textbf{الهاتف: " & If(companyData IsNot Nothing, companyData("COMPANY_TEL").ToString(), "123-456-7890") & "} \\")
        '  latexContent.AppendLine("\textbf{الرقم الضريبي: " & If(companyData IsNot Nothing, companyData("COMPANY_TAX_NO").ToString(), "987654321") & "}")


        currentY += 5
        customerInfo = "بيانات المواد"
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontRegular, g)
        ' Items Table
        g.DrawString(customerInfo, fontHeader, Brushes.Navy, (pageWidth / 2) + (TEXT_SIZ.Width / 2), currentY, rtlFormat)
        currentY = currentY + TEXT_SIZ.Height


        Dim xPos As Integer = pageWidth - margin
        Dim colWidths As New List(Of Integer)
        Dim LABELPOSX As New List(Of Integer)
        customerInfo = "الباركود"
        TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)
        If isPosPrinter Then
            colWidths.Add(xPos - contentWidth * 0.5) ' Material name
            colWidths.Add(xPos - contentWidth * 0.7) ' Price with tax
            colWidths.Add(xPos - contentWidth * 0.85) ' Quantity
            colWidths.Add(xPos - contentWidth) ' Total
            LABELPOSX.Add(((colWidths(0) + xPos) / 2) + (TEXT_SIZ.Width / 2))
            LABELPOSX.Add(((colWidths(1) + colWidths(0)) / 2) + (TEXT_SIZ.Width / 2))
            LABELPOSX.Add(((colWidths(1) + colWidths(2)) / 2) + (TEXT_SIZ.Width / 2))
            LABELPOSX.Add(((colWidths(3) + colWidths(2)) / 2) + (TEXT_SIZ.Width / 2))

        Else
            colWidths.Add(xPos - contentWidth * 0.05) ' Barcode
            colWidths.Add(xPos - contentWidth * 0.5) ' Material name
            colWidths.Add(xPos - contentWidth * 0.6) ' Price
            colWidths.Add(xPos - contentWidth * 0.7) ' Quantity
            colWidths.Add(xPos - contentWidth * 0.8) ' Discount
            colWidths.Add(xPos - contentWidth * 0.9) ' Tax
            colWidths.Add(xPos - contentWidth * 1) ' Total

            LABELPOSX.Add(((colWidths(0) + xPos) / 2))
            LABELPOSX.Add(((colWidths(1) + colWidths(0)) / 2))
            LABELPOSX.Add(((colWidths(1) + colWidths(2)) / 2))
            LABELPOSX.Add(((colWidths(3) + colWidths(2)) / 2))
            LABELPOSX.Add(((colWidths(3) + colWidths(4)) / 2))
            LABELPOSX.Add(((colWidths(5) + colWidths(4)) / 2))
            LABELPOSX.Add(((colWidths(6) + colWidths(5)) / 2))


            ' LABELPOSX.Add(margin + (colWidths(6) / 2) + (TEXT_SIZ.Width / 2))
        End If
        xPos = pageWidth - margin
        Dim tableRect As New Rectangle(margin, currentY, contentWidth, TEXT_SIZ.Height + 5)
        ' Dim tableRect As New Rectangle(margin, currentY, contentWidth, (orderData.Rows.Count + 1) * 25)
        g.FillRectangle(Brushes.White, tableRect)
        g.DrawRectangle(Pens.DarkGray, tableRect)

        ' Draw table headers with internal borders

        If isPosPrinter Then
            g.DrawString("المادة", fontTableHeader, Brushes.Black, LABELPOSX(0), currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(0), currentY, colWidths(0), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("السعر ض", fontTableHeader, Brushes.Black, LABELPOSX(1), currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(1), currentY, colWidths(1), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("الكمية", fontTableHeader, Brushes.Black, LABELPOSX(2), currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(2), currentY, colWidths(2), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("الإجمالي", fontTableHeader, Brushes.Black, LABELPOSX(3), currentY + 5, rtlFormat)
        Else
            g.DrawString("#", fontTableHeader, Brushes.Black, LABELPOSX(0), currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(0), currentY, colWidths(0), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("اسم المادة", fontTableHeader, Brushes.Black, LABELPOSX(1) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(1), currentY, colWidths(1), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("السعر", fontTableHeader, Brushes.Black, LABELPOSX(2) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(2), currentY, colWidths(2), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("الكمية", fontTableHeader, Brushes.Black, LABELPOSX(3) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(3), currentY, colWidths(3), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("الخصم", fontTableHeader, Brushes.Black, LABELPOSX(4) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(4), currentY, colWidths(4), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("الضريبة", fontTableHeader, Brushes.Black, LABELPOSX(5) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
            g.DrawLine(Pens.DarkGray, colWidths(5), currentY, colWidths(5), currentY + TEXT_SIZ.Height + 5)
            g.DrawString("الإجمالي", fontTableHeader, Brushes.Black, LABELPOSX(6) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
        End If
        currentY += TEXT_SIZ.Height

        ' Draw table rows with alternating colors and internal borders
        For i As Integer = 0 To orderData.Rows.Count - 1
            Dim row As DataRow = orderData.Rows(i)
            Dim rowBrush As Brush = If(i Mod 2 = 0, Brushes.White, Brushes.LightGray)
            g.FillRectangle(rowBrush, margin, currentY, contentWidth, TEXT_SIZ.Height + 5)
            If isPosPrinter Then
                Dim priceWithTax As Double = Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT")) * (1 + Convert.ToDouble(row("TAX_VALUE")) / 100)
                g.DrawString(row("PRO_NAME").ToString(), fontTableRow, Brushes.Black, LABELPOSX(0), currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(0), currentY, colWidths(0), currentY + TEXT_SIZ.Height + 5)
                g.DrawString(priceWithTax.ToString("F3"), fontTableRow, Brushes.Black, LABELPOSX(1), currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(1), currentY, colWidths(1), currentY + TEXT_SIZ.Height + 5)
                g.DrawString(Convert.ToDouble(row("PRO_COUNT")).ToString("F3"), fontTableRow, Brushes.Black, LABELPOSX(2), currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(2), currentY, colWidths(2), currentY + TEXT_SIZ.Height + 5)
                g.DrawString(Convert.ToDouble(row("FINAL_VALUE")).ToString("F3"), fontTableRow, Brushes.Black, LABELPOSX(3), currentY + 5, rtlFormat)
            Else
                ' g.DrawString(row("BARCODE").ToString(), fontTableRow, Brushes.Black, LABELPOSX(0), currentY + 5, rtlFormat)
                customerInfo = "#" & (i + 1)
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)
                g.DrawString(customerInfo, fontTableRow, Brushes.Black, LABELPOSX(0) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(0), currentY, colWidths(0), currentY + TEXT_SIZ.Height + 5)
                customerInfo = row("PRO_NAME").ToString()
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)

                g.DrawString(customerInfo, fontTableRow, Brushes.Black, LABELPOSX(1) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(1), currentY, colWidths(1), currentY + TEXT_SIZ.Height + 5)
                ' g.DrawString((Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT"))).ToString("F3"), fontTableRow, Brushes.Black, xPos - colWidths(0) - colWidths(1) - colWidths(2), currentY + 5)
                customerInfo = (Convert.ToDouble(row("PRO_PRICE"))).ToString("F3")
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)
                g.DrawString(customerInfo, fontTableRow, Brushes.Black, LABELPOSX(2) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)

                g.DrawLine(Pens.DarkGray, colWidths(2), currentY, colWidths(2), currentY + TEXT_SIZ.Height + 5)
                customerInfo = (Convert.ToDouble(row("PRO_COUNT"))).ToString("F3")
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)
                g.DrawString(Convert.ToDouble(row("PRO_COUNT")).ToString("F3"), fontTableRow, Brushes.Black, LABELPOSX(3) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(3), currentY, colWidths(3), currentY + TEXT_SIZ.Height + 5)
                customerInfo = (Convert.ToDouble(row("PRO_DISCOUNT"))).ToString("F3")
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)

                g.DrawString(customerInfo, fontTableRow, Brushes.Black, LABELPOSX(4) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat) ' Placeholder for discount
                g.DrawLine(Pens.DarkGray, colWidths(4), currentY, colWidths(4), currentY + TEXT_SIZ.Height + 5)
                customerInfo = (Convert.ToDouble(row("VALUE_OF_TAX")))
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)
                g.DrawString(customerInfo, fontTableRow, Brushes.Black, LABELPOSX(5) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
                g.DrawLine(Pens.DarkGray, colWidths(5), currentY, colWidths(5), currentY + TEXT_SIZ.Height + 5)
                customerInfo = (Convert.ToDouble(row("FINAL_VALUE"))).ToString("F3")
                TEXT_SIZ = GET_FONT_SIZE(customerInfo, fontTableHeader, g)
                g.DrawString(customerInfo, fontTableRow, Brushes.Black, LABELPOSX(6) + TEXT_SIZ.Width / 2, currentY + 5, rtlFormat)
            End If
            g.DrawLine(Pens.DarkGray, margin, currentY + TEXT_SIZ.Height + 5, pageWidth - margin, currentY + TEXT_SIZ.Height + 5)
            currentY += TEXT_SIZ.Height + 5
        Next
        g.DrawRectangle(Pens.DarkGray, tableRect)

        ' Summary (right-aligned)
        currentY += 10
        Dim itemValue As Double = Convert.ToDouble(invoiceData.Rows(0)("ITEM_VALUE"))
        Dim discountValue As Double = Convert.ToDouble(invoiceData.Rows(0)("DISCOUNT_VALUE"))
        Dim taxValue As Double = Convert.ToDouble(invoiceData.Rows(0)("TAX_VALUE"))
        Dim invValue As Double = Convert.ToDouble(invoiceData.Rows(0)("INV_VALUE"))
        Dim priceAfterDiscount As Double = itemValue - discountValue

        Dim summary As String = "إجمالي المواد: " & itemValue.ToString("F3") & vbCrLf &
                               "كمية الخصم: " & discountValue.ToString("F3") & vbCrLf &
                               "السعر بعد الخصم: " & priceAfterDiscount.ToString("F3") & vbCrLf &
                               "إجمالي الضريبة: " & taxValue.ToString("F3") & vbCrLf &
                               "الإجمالي: " & invValue.ToString("F3")
        Dim summarySize As SizeF = g.MeasureString(summary, fontHeader)
        g.DrawString(summary, fontHeader, Brushes.Navy, pageWidth - margin, currentY + 5, rtlFormat) ' Right-aligned
        currentY += 100

        ' Footer (right-aligned)
        ' Dim footer As String = "نوع الفاتورة: " & invoiceData.Rows(0)("INV_KIND").ToString() & vbCrLf &
        '      "طريقة الدفع: " & invoiceData.Rows(0)("PAY_KIND").ToString()
        ' Dim footerSize As SizeF = g.MeasureString(footer, fontRegular)
        ' g.DrawString(footer, fontRegular, Brushes.DarkGreen, pageWidth - margin - footerSize.Width, currentY + 5) ' Right-aligned
    End Sub

    Private Sub GeneratePDF()
        Dim latexContent As New StringBuilder()

        ' Building LaTeX document
        latexContent.AppendLine("\documentclass[a4paper,10pt]{article}")
        latexContent.AppendLine("\usepackage[utf8]{inputenc}")
        latexContent.AppendLine("\usepackage[arabic]{babel}")
        latexContent.AppendLine("\usepackage{geometry}")
        latexContent.AppendLine("\geometry{paperwidth=80mm, paperheight=200mm, margin=5mm}")
        latexContent.AppendLine("\usepackage{graphicx}")
        latexContent.AppendLine("\usepackage{qrcode}")
        latexContent.AppendLine("\usepackage{xcolor}")
        latexContent.AppendLine("\usepackage{fancyhdr}")
        latexContent.AppendLine("\usepackage{fontspec}")
        latexContent.AppendLine("\setmainfont{Cairo}")
        latexContent.AppendLine("\pagestyle{fancy}")
        latexContent.AppendLine("\fancyhf{}")
        latexContent.AppendLine("\fancyfoot[C]{\tiny تصميم عصري - " & If(companyData IsNot Nothing, companyData("COMPANY_NAME").ToString(), "شركة المثالية") & "}")
        latexContent.AppendLine("\begin{document}")
        latexContent.AppendLine("\begin{titlepage}")
        latexContent.AppendLine("\pagecolor{white}")

        ' Header
        latexContent.AppendLine("\begin{flushright}")
        latexContent.AppendLine("\begin{tabular}{p{2.5cm} p{2.5cm} p{2.5cm}}")
        latexContent.AppendLine("\qrcode[height=2cm]{\detokenize{" & invoiceData.Rows(0)("E_CODE").ToString() & "}} &")
        latexContent.AppendLine("\textbf{\Large \u25CF [شعار الشركة]} &")
        latexContent.AppendLine("\begin{flushleft}")
        latexContent.AppendLine("\textbf{اسم الشركة: " & If(companyData IsNot Nothing, companyData("COMPANY_NAME").ToString(), "شركة المثالية") & "} \\")
        latexContent.AppendLine("\textbf{الهاتف: " & If(companyData IsNot Nothing, companyData("COMPANY_TEL").ToString(), "123-456-7890") & "} \\")
        latexContent.AppendLine("\textbf{الرقم الضريبي: " & If(companyData IsNot Nothing, companyData("COMPANY_TAX_NO").ToString(), "987654321") & "}")
        latexContent.AppendLine("\end{flushleft} \\")
        latexContent.AppendLine("\multicolumn{1}{r}{\small رقم الفاتورة: " & invoiceId & "} & & \\")
        latexContent.AppendLine("\multicolumn{1}{r}{\small التاريخ: " & invoiceData.Rows(0)("DATE_TIME").ToString() & "} & & \\")
        latexContent.AppendLine("\end{tabular}")
        latexContent.AppendLine("\end{flushright}")

        ' Customer Info
        latexContent.AppendLine("\vspace{1cm}")
        latexContent.AppendLine("\begin{flushleft}")
        latexContent.AppendLine("\textbf{الاسم: " & If(customerData IsNot Nothing, customerData("C_NAME").ToString(), "زبون نقدي") & "} \\")
        latexContent.AppendLine("\textbf{الهاتف: " & If(customerData IsNot Nothing, customerData("C_TEL").ToString(), "-") & "} \\")
        latexContent.AppendLine("\textbf{الرقم الضريبي: " & If(customerData IsNot Nothing, customerData("E_ID").ToString(), "-") & "}")
        latexContent.AppendLine("\end{flushleft}")

        ' Items Table
        latexContent.AppendLine("\vspace{1cm}")
        latexContent.AppendLine("\begin{flushleft}")
        latexContent.AppendLine("\textbf{\Large بيانات المواد}")
        latexContent.AppendLine("\end{flushleft}")
        latexContent.AppendLine("\begin{tabular}{|")
        If isPosPrinter Then
            latexContent.AppendLine("p{4cm}|p{1.5cm}|p{1cm}|p{1.5cm}|}")
        Else
            latexContent.AppendLine("p{1.5cm}|p{3cm}|p{1.5cm}|p{1cm}|p{1cm}|p{1cm}|p{1.5cm}|}")
        End If
        latexContent.AppendLine("\hline")
        If isPosPrinter Then
            latexContent.AppendLine("\rowcolor{white}\textbf{المادة} & \textbf{السعر شامل الضريبة} & \textbf{الكمية} & \textbf{الإجمالي} \\")
        Else
            latexContent.AppendLine("\rowcolor{white}\textbf{الباركود} & \textbf{اسم المادة} & \textbf{السعر} & \textbf{الكمية} & \textbf{الخصم} & \textbf{الضريبة} & \textbf{الإجمالي} \\")
        End If
        latexContent.AppendLine("\hline")
        For i As Integer = 0 To orderData.Rows.Count - 1
            Dim row As DataRow = orderData.Rows(i)
            Dim rowColor As String = If(i Mod 2 = 0, "white", "lightgray!10")
            If isPosPrinter Then
                Dim priceWithTax As Double = Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT")) * (1 + Convert.ToDouble(row("TAX_VALUE")) / 100)
                latexContent.AppendLine("\rowcolor{" & rowColor & "}" & row("PRO_NAME").ToString() & " & " & priceWithTax.ToString("F3") & " & " & Convert.ToDouble(row("PRO_COUNT")).ToString("F3") & " & " & Convert.ToDouble(row("FINAL_VALUE")).ToString("F3") & " \\")
            Else
                latexContent.AppendLine("\rowcolor{" & rowColor & "}" & row("PRO_BARCODE").ToString() & " & " & row("PRO_NAME").ToString() & " & " & (Convert.ToDouble(row("FINAL_VALUE")) / Convert.ToDouble(row("PRO_COUNT"))).ToString("F3") & " & " & Convert.ToDouble(row("PRO_COUNT")).ToString("F3") & " & 0.00 & " & row("TAX_VALUE").ToString("F3") & " & " & Convert.ToDouble(row("FINAL_VALUE")).ToString("F3") & " \\")
            End If
            latexContent.AppendLine("\hline")
        Next
        latexContent.AppendLine("\end{tabular}")

        ' Summary (right-aligned)
        latexContent.AppendLine("\vspace{1cm}")
        latexContent.AppendLine("\begin{flushright}")
        latexContent.AppendLine("\textbf{\large إجمالي المواد: " & Convert.ToDouble(invoiceData.Rows(0)("ITEM_VALUE")).ToString("F3") & "} \\")
        latexContent.AppendLine("\textbf{\large كمية الخصم: " & Convert.ToDouble(invoiceData.Rows(0)("DISCOUNT_VALUE")).ToString("F3") & "} \\")
        latexContent.AppendLine("\textbf{\large السعر بعد الخصم: " & (Convert.ToDouble(invoiceData.Rows(0)("ITEM_VALUE")) - Convert.ToDouble(invoiceData.Rows(0)("DISCOUNT_VALUE"))).ToString("F3") & "} \\")
        latexContent.AppendLine("\textbf{\large إجمالي الضريبة: " & Convert.ToDouble(invoiceData.Rows(0)("TAX_VALUE")).ToString("F3") & "} \\")
        latexContent.AppendLine("\textbf{\large الإجمالي: " & Convert.ToDouble(invoiceData.Rows(0)("INV_VALUE")).ToString("F3") & "}")
        latexContent.AppendLine("\end{flushright}")

        ' Footer (right-aligned)
        latexContent.AppendLine("\vspace{1cm}")
        latexContent.AppendLine("\begin{flushright}")
        latexContent.AppendLine("\textit{نوع الفاتورة: " & invoiceData.Rows(0)("INV_KIND").ToString() & "} \\")
        latexContent.AppendLine("\textit{طريقة الدفع: " & invoiceData.Rows(0)("PAY_KIND").ToString() & "}")
        latexContent.AppendLine("\end{flushright}")

        latexContent.AppendLine("\end{titlepage}")
        latexContent.AppendLine("\end{document}")

        Dim latexFilePath As String = "invoice_" & invoiceId & ".tex"
        File.WriteAllText(latexFilePath, latexContent.ToString())
        MessageBox.Show("تم تصدير الفاتورة كملف PDF: " & latexFilePath, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    '   Protected Overrides Sub Dispose(disposing As Boolean)
    '  If disposing Then
    ' If companyLogo IsNot Nothing Then companyLogo.Dispose()
    'If qrCodeImage IsNot Nothing Then qrCodeImage.Dispose()
    '  End If
    ''    MyBase.Dispose(disposing)
    ' End Sub
End Class