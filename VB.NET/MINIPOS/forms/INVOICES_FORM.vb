Imports System.Data.SQLite

Public Class INVOICES_FORM
    Private dbHandler As New DatabaseHandler()

    Private Sub INVOICES_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInvoices()
    End Sub

    Private Sub LoadInvoices()
        Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim adapter As New SQLiteDataAdapter("SELECT i.*, c.C_NAME FROM INVOICES i JOIN CUSTOMERS c ON i.C_ID = c.ID", conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridViewInvoices.DataSource = table
        End Using
    End Sub

    Private Sub btnCreateInvoice_Click(sender As Object, e As EventArgs) Handles btnCreateInvoice.Click
        Dim invoiceForm As New Form()
        ' Add controls for invoice creation
        ' INVOICES fields
        Dim txtCustomer As New ComboBox()
        Dim txtPayKind As New ComboBox()
        Dim txtInvKind As New ComboBox()
        ' ... other controls

        ' ORDER_PRODUCTS grid
        Dim gridOrderProducts As New DataGridView()
        gridOrderProducts.Columns.Add("PRO_NAME", "Product Name")
        gridOrderProducts.Columns.Add("PRO_COUNT", "Quantity")
        gridOrderProducts.Columns.Add("PRO_PRICE", "Price")
        ' ... other columns

        ' Save button
        Dim btnSave As New Button()
        btnSave.Text = "Save Invoice"
        AddHandler btnSave.Click, Sub()
                                      SaveInvoice(txtCustomer, txtPayKind, txtInvKind, gridOrderProducts)
                                  End Sub

        invoiceForm.Controls.AddRange({txtCustomer, txtPayKind, txtInvKind, gridOrderProducts, btnSave})
        invoiceForm.ShowDialog()
    End Sub

    Private Sub SaveInvoice(customer As ComboBox, payKind As ComboBox, invKind As ComboBox, orderGrid As DataGridView)
        Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim trans = conn.BeginTransaction()
            Try
                ' Insert INVOICES
                Dim invoiceCmd As New SQLiteCommand("INSERT INTO INVOICES (INV_PAY_KIND, INV_KIND, C_ID, DATE_TIME) VALUES (@payKind, @invKind, @cid, @date)", conn)
                invoiceCmd.Parameters.AddWithValue("@payKind", payKind.SelectedValue)
                invoiceCmd.Parameters.AddWithValue("@invKind", invKind.SelectedValue)
                invoiceCmd.Parameters.AddWithValue("@cid", customer.SelectedValue)
                invoiceCmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                invoiceCmd.ExecuteNonQuery()

                Dim invoiceId = conn.LastInsertRowId

                ' Insert ORDER_PRODUCTS
                For Each row As DataGridViewRow In orderGrid.Rows
                    If Not row.IsNewRow Then
                        Dim orderCmd As New SQLiteCommand("INSERT INTO ORDER_PRODUCTS (PRO_NAME, PRO_COUNT, PRO_PRICE, INV_ID) VALUES (@name, @count, @price, @invId)", conn)
                        orderCmd.Parameters.AddWithValue("@name", row.Cells("PRO_NAME").Value)
                        orderCmd.Parameters.AddWithValue("@count", row.Cells("PRO_COUNT").Value)
                        orderCmd.Parameters.AddWithValue("@price", row.Cells("PRO_PRICE").Value)
                        orderCmd.Parameters.AddWithValue("@invId", invoiceId)
                        orderCmd.ExecuteNonQuery()
                    End If
                Next

                trans.Commit()
                LoadInvoices()
            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show($"Error: {ex.Message}")
            End Try
        End Using
    End Sub

    Private Sub btnPrintInvoice_Click(sender As Object, e As EventArgs) Handles btnPrintInvoice.Click
        ' Implement invoice printing
        If DataGridViewInvoices.SelectedRows.Count > 0 Then
            ' Generate print layout with invoice details and order products
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ' Implement export with filters (to be defined later)
        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "CSV files (*.csv)|*.csv"
        If saveDialog.ShowDialog() = DialogResult.OK Then
            ' Export filtered data to CSV
        End If
    End Sub
End Class