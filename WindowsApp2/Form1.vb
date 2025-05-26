Imports ist.jofotara

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set Seller properties
    End Sub
    Private Async Sub sendinvoic(k As Integer)
        InvoiceTool1.Seller = New InvoiceTool.SellerInfo With {
           .CommercialName = "Seller Inc.",
           .IncomeSourceNumber = "12345",
           .Address = "123 Main St",
           .TaxNumber = "123456789",
           .CountryCode = "1" ' JO
       }

        ' Set Buyer properties
        InvoiceTool1.Buyer = New InvoiceTool.BuyerInfo With {
            .IsCashCustomer = True,
            .Governorate = InvoiceTool.Governorate.Zarqa.ToString,
            .PartyIdentification = New InvoiceTool.BuyerPartyIdentification With {
            .CODE = "123456",
            .KIND = InvoiceTool.Identification.NIN.ToString
        }
        } ' Automatically sets TaxNumber to "10" and clears other fields

        ' Set Invoice properties
        InvoiceTool1.Invoice = New InvoiceTool.InvoiceInfo With {
            .InvoiceNumber = "INV001",
            .InvoiceType = 0, '0 Local 1 Export  2 Development Area
            .PaymentMethod = 1, '1 Cash 2 Receivables 
            .InvoiceCurrency = InvoiceTool.Currency.JOD.ToString,
            .TaxCurrency = InvoiceTool.Currency.JOD.ToString,
            .IncomeSourceType = k, ' 1  Income  2 General tax 3 Special tax
            .TransactionType = 381, '388 New Invoice 381 AS BACK INVOICE  
            .Notes = "Sample invoice",
            .RETURN_INVOICE = New InvoiceTool.BillingReference With {
            .INVOICE_NUMBER = "RET001",
            .INVOICE_TOATAL = 100D,
            .INVOICE_UUID = "UUID123"
        }
        }

        ''في حال الارجاع


        ' Set SetupInvoice properties
        InvoiceTool1.PortalUrl = "https://portal.jofotara.gov.jo/ar/invoices/"
        InvoiceTool1.UserKey = "key123"
        InvoiceTool1.UserSecureKey = "secure123"
        InvoiceTool1.DigitalSignaturePath = "C:\signature.pfx"
        InvoiceTool1.AutoClose = True
        InvoiceTool1.showresulttoast = True
        ' Add invoice items
        Dim itemRow = InvoiceTool1.InvoiceItems.NewRow()
        itemRow("ItemName") = "Product A"
        itemRow("ItemNo") = "ISIC5-001"
        itemRow("ItemPrice") = 100.0
        itemRow("Discount") = 10.0
        itemRow("VatTax") = 16.0
        itemRow("SpecialTax") = 5
        itemRow("ItemCount") = 2
        itemRow("TaxTYPE") = "S"    ' O Z S
        InvoiceTool1.InvoiceItems.Rows.Add(itemRow)



        '' Add additional row 2
        'Dim itemRow2 = InvoiceTool1.InvoiceItems.NewRow()
        'itemRow2("ItemName") = "Product C"
        'itemRow2("ItemNo") = "ISIC5-003"
        'itemRow2("ItemPrice") = 80.0
        'itemRow2("Discount") = 5.0
        'itemRow2("VatTax") = 8.0
        'itemRow2("SpecialTax") = 2
        'itemRow2("ItemCount") = 5
        'itemRow2("TaxTYPE") = "Z"    ' O Z S
        'InvoiceTool1.InvoiceItems.Rows.Add(itemRow2)

        '' Add additional row 3
        'Dim itemRow3 = InvoiceTool1.InvoiceItems.NewRow()
        'itemRow3("ItemName") = "Product D"
        'itemRow3("ItemNo") = "ISIC5-004"
        'itemRow3("ItemPrice") = 200.0
        'itemRow3("Discount") = 20.0
        'itemRow3("VatTax") = 16.0
        'itemRow3("SpecialTax") = 7
        'itemRow3("ItemCount") = 1
        'itemRow3("TaxTYPE") = "S"    ' O Z S
        'InvoiceTool1.InvoiceItems.Rows.Add(itemRow3)

        '' Add additional row 4
        'Dim itemRow4 = InvoiceTool1.InvoiceItems.NewRow()
        'itemRow4("ItemName") = "Product E"
        'itemRow4("ItemNo") = "ISIC5-005"
        'itemRow4("ItemPrice") = 50.0
        'itemRow4("Discount") = 0.0
        'itemRow4("VatTax") = 0.0
        'itemRow4("SpecialTax") = 0
        'itemRow4("ItemCount") = 10
        'itemRow4("TaxTYPE") = "Z"    ' O Z S
        'InvoiceTool1.InvoiceItems.Rows.Add(itemRow4)

        '' Add additional row 5
        'Dim itemRow5 = InvoiceTool1.InvoiceItems.NewRow()
        'itemRow5("ItemName") = "Product F"
        'itemRow5("ItemNo") = "ISIC5-006"
        'itemRow5("ItemPrice") = 120.0
        'itemRow5("Discount") = 12.0
        'itemRow5("VatTax") = 16.0
        'itemRow5("SpecialTax") = 4
        'itemRow5("ItemCount") = 4
        'itemRow5("TaxTYPE") = "O"    ' O Z S
        'InvoiceTool1.InvoiceItems.Rows.Add(itemRow5)


        '        Dim itemRow = InvoiceTool1.InvoiceItems.NewRow()
        '        itemRow("ItemName") = "Product A"
        '        itemRow("ItemNo") = "ISIC5-001"
        '        itemRow("ItemPrice") = 100.0
        '        itemRow("Discount") = 10.0
        '        itemRow("VatTax") = 16.0
        '        itemRow("SpecialTax") = 5
        '        itemRow("ItemCount") = 2
        '        itemRow("TaxTYPE") = "S"    ' O Z S
        '        InvoiceTool1.InvoiceItems.Rows.Add(itemRow)

        '        ' Define sample data for 5 additional rows
        '        Dim additionalItems = {
        '    New With {.Name = "Product B", .No = "ISIC5-002", .Price = 150.0, .Discount = 15.0, .VatTax = 16.0, .SpecialTax = 3, .Count = 3, .TaxType = "O"},
        '    New With {.Name = "Product C", .No = "ISIC5-003", .Price = 80.0, .Discount = 5.0, .VatTax = 8.0, .SpecialTax = 2, .Count = 5, .TaxType = "Z"},
        '    New With {.Name = "Product D", .No = "ISIC5-004", .Price = 200.0, .Discount = 20.0, .VatTax = 16.0, .SpecialTax = 7, .Count = 1, .TaxType = "S"},
        '    New With {.Name = "Product E", .No = "ISIC5-005", .Price = 50.0, .Discount = 0.0, .VatTax = 0.0, .SpecialTax = 0, .Count = 10, .TaxType = "Z"},
        '    New With {.Name = "Product F", .No = "ISIC5-006", .Price = 120.0, .Discount = 12.0, .VatTax = 16.0, .SpecialTax = 4, .Count = 4, .TaxType = "O"}
        '}

        '        ' Add the additional rows using a loop
        '        For Each item In additionalItems
        '            Dim newRow = InvoiceTool1.InvoiceItems.NewRow()
        '            newRow("ItemName") = item.Name
        '            newRow("ItemNo") = item.No
        '            newRow("ItemPrice") = item.Price
        '            newRow("Discount") = item.Discount
        '            newRow("VatTax") = item.VatTax
        '            newRow("SpecialTax") = item.SpecialTax
        '            newRow("ItemCount") = item.Count
        '            newRow("TaxTYPE") = item.TaxType
        '            InvoiceTool1.InvoiceItems.Rows.Add(newRow)
        '        Next


        ' Initialize
        InvoiceTool1.Initialize()

        Await InvoiceTool1.Start()
        ' InvoiceTool1.ShowToast("تمت المزامنة بنجاح", 3000, False) ' Green toast for 3 seconds

        ' Access output properties
        '  MsgBox(InvoiceTool1.Status)
        If InvoiceTool1.Status = "P" Then

            '  MessageBox.Show($"UUID: {InvoiceTool1.UUID}{vbCrLf}Status: {InvoiceTool1.QRCode}")
            Me.Text = "MSG=" & InvoiceTool1.Status
        Else
            '  InvoiceTool1.ShowToast("فشل في المزامنة", 3000, True) ' Red toast for 3 seconds
            '  MessageBox.Show($"ERROR: {InvoiceTool1.msg} ")
            Me.Text = InvoiceTool1.Status
        End If
    End Sub
    Private Async Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        '' Start the invoice process
        sendinvoic(1)



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sendinvoic(2)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        sendinvoic(3)
    End Sub
End Class

'''كود C#
''''```csharp
'using System;
'using System.Windows.Forms;
'using System.Threading.Tasks;
'using InvoiceTool; // Assuming InvoiceTool is the namespace of the DLL

'namespace InvoiceApp
'{
'    public partial class Form1 : Form
'    {
'        public Form1()
'        {
'            InitializeComponent();
'        }

'        private async void Button1_Click(object sender, EventArgs e)
'        {
'            try
'            {
'                // Initialize InvoiceTool1 (assuming it's a class instance)
'                var invoiceTool1 = new InvoiceTool1();

'                // Set Seller properties
'                invoiceTool1.Seller = new SellerInfo
'                {
'                    CommercialName = "Seller Inc.",
'                    IncomeSourceNumber = "12345",
'                    Address = "123 Main St",
'                    TaxNumber = "123456789",
'                    CountryCode = "1" // JO
'                };

'                // Set Buyer properties
'                invoiceTool1.Buyer = new BuyerInfo
'                {
'                    IsCashCustomer = true,
'                    Governorate = Governorate.Zarqa.ToString(),
'                    PartyIdentification = new BuyerPartyIdentification
'                    {
'                        CODE = "123456",
'                        KIND = Identification.NIN.ToString()
'                    }
'                }; // Automatically sets TaxNumber to "10" and clears other fields

'                // Set Invoice properties
'                invoiceTool1.Invoice = new InvoiceInfo
'                {
'                    InvoiceNumber = "INV001",
'                    InvoiceType = 0, // 0 Local, 1 Export, 2 Development Area
'                    PaymentMethod = 1, // 1 Cash, 2 Receivables
'                    InvoiceCurrency = Currency.JOD.ToString(),
'                    TaxCurrency = Currency.JOD.ToString(),
'                    IncomeSourceType = 1, // 1 Income, 2 General tax, 3 Special tax
'                    TransactionType = 381, // 388 New Invoice, 381 AS BACK INVOICE
'                    Notes = "Sample invoice",
'                    RETURN_INVOICE = new BillingReference
'                    {
'                        INVOICE_NUMBER = "RET001",
'                        INVOICE_TOATAL = 100M, // Decimal in C#
'                        INVOICE_UUID = "UUID123"
'                    }
'                };

'                // Set SetupInvoice properties
'                invoiceTool1.PortalUrl = "https://portal.jofotara.gov.jo/ar/invoices/";
'                invoiceTool1.UserKey = "key123";
'                invoiceTool1.UserSecureKey = "secure123";
'                invoiceTool1.DigitalSignaturePath = @"C:\signature.pfx";
'                invoiceTool1.AutoClose = true;
'                invoiceTool1.ShowResultToast = true;

'                // Add invoice items
'                var itemRow = invoiceTool1.InvoiceItems.NewRow();
'                itemRow["ItemName"] = "Product A";
'                itemRow["ItemNo"] = "ISIC5-001";
'                itemRow["ItemPrice"] = 100.0;
'                itemRow["Discount"] = 10.0;
'                itemRow["VatTax"] = 16.0;
'                itemRow["SpecialTax"] = 5;
'                itemRow["ItemCount"] = 2;
'                itemRow["TaxTYPE"] = "S"; // O Z S
'                invoiceTool1.InvoiceItems.Rows.Add(itemRow);

'                invoiceTool1.Initialize();

'                // Start the invoice process asynchronously
'                await invoiceTool1.Start();

'                // Check status
'                if (invoiceTool1.Status == "P")
'                {
'                    this.Text = $"MSG={invoiceTool1.Status}";
'                }
'                else
'                {
'                    this.Text = invoiceTool1.Status;
'                }
'            }
'            catch (Exception ex)
'            {
'                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
'            }
'        }
'    }
'}
'```

'''PYTHON
'''