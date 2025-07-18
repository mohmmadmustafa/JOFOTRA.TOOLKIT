' SETTING_PRO.vb (Form for CRUD operations)
Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.IO

Public Class SETTING_PRO
    Private dbHandler As New DatabaseHandler()
    Private currentTable As String = ""
    Private tables As String() = {"PRODUCTS", "CUSTOMERS", "USER_TABLE"}
    ' Private WithEvents ComboBoxTables As New ComboBox()

    Private Sub SETTING_PRO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbHandler.InitializeDatabase()
        InitializeComboBox()
        LoadTableComboBox()
    End Sub

    Private Sub InitializeComboBox()
        '  ComboBoxTables.Name = "ComboBoxTables"
        '  ComboBoxTables.Location = New Point(10, 10)
        '  ComboBoxTables.Size = New Size(200, 25)
        '  ComboBoxTables.DropDownStyle = ComboBoxStyle.DropDownList
        '  Me.Controls.Add(ComboBoxTables)
    End Sub

    Private Sub LoadTableComboBox()
        ComboBoxTables.Items.Clear()
        ComboBoxTables.Items.AddRange(tables)
        If ComboBoxTables.Items.Count > 0 Then
            ComboBoxTables.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBoxTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTables.SelectedIndexChanged
        currentTable = ComboBoxTables.SelectedItem?.ToString()
        If Not String.IsNullOrEmpty(currentTable) Then
            LoadDataGrid()
        End If
    End Sub

    Private Sub LoadDataGrid()
        Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim adapter As New SQLiteDataAdapter($"SELECT * FROM {currentTable}", conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table

            ' Set custom header text based on the selected table
            Select Case currentTable
                Case "COM_SEETING"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("COMPANY_NAME", "اسم الشركة")
                    SetColumnHeader("POS_KIND", "نوع شاشه البيع")
                    SetColumnHeader("COMPANY_TEL", "هاتف الشركة")
                    SetColumnHeader("COMPANY_TAX_NO", "الرقم الضريبي")
                    SetColumnHeader("tax_source_nu", "تسلسل مصدر الدخل")
                    SetColumnHeader("COMPANY_ADDRESS", "عنوان الشركة")
                    SetColumnHeader("COMPANY_LOGO", "شعار الشركة")
                    SetColumnHeader("key0", "اسم المستخدم")
                    SetColumnHeader("key1", "كلمة المرور")

                Case "PRODUCTS"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("BARCODE", "الباركود")
                    SetColumnHeader("PRO_NAME", "اسم المنتج")
                    SetColumnHeader("PRO_PRICE", "سعر المنتج")
                    SetColumnHeader("PRO_TAX_VALUE", "قيمة الضريبة")
                    SetColumnHeader("PRO_TAX_KIND", "نوع الضريبة")
                    SetColumnHeader("PRO_KIND", "نوع المنتج")
                Case "PRO_KIND"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "النوع")
                Case "POS_SCREEN_KIND"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "شاشه الطلب")

                Case "ID_CUSTOMER"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "نوع الهوية")
                Case "CUSTOMERS"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("E_ID", "نوع الهوية")
                    SetColumnHeader("C_TAX_CODE", "الرقم الضريبي")
                    SetColumnHeader("C_NAME", "اسم العميل")
                    SetColumnHeader("C_TEL", "هاتف العميل")
                    SetColumnHeader("CU_CITY", "المدينة")
                    SetColumnHeader("C_POSTCODE", "الرمز البريدي")
                Case "CITY_TABLE"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "اسم المدينة")
                Case "INV_PAY_KIND"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "نوع الدفع")
                Case "IN_KIND"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "نوع الفاتورة")
                Case "INV_OUT_IN"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "نوع الحركة")
                Case "REMARK_KIND_TABLE"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "نوع الملاحظة")
                Case "CURRENCY_TABLE"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("TXT", "اسم العملة")
                    SetColumnHeader("C_CODE", "كود العملة")
                Case "USER_TABLE"
                    SetColumnHeader("ID", "المعرف")
                    SetColumnHeader("USER_NAME", "اسم المستخدم")
                    SetColumnHeader("USER_CODE", "كود المستخدم")
                    SetColumnHeader("USER_KEY", "مفتاح المستخدم")
                    SetColumnHeader("USER_FROM", "مصدر المستخدم")
            End Select
        End Using
    End Sub

    Private Sub SetColumnHeader(columnName As String, headerText As String)
        If DataGridView1.Columns.Contains(columnName) Then
            DataGridView1.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrEmpty(currentTable) Then
            MessageBox.Show("يرجى تحديد جدول أولاً", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim inputForm As New Form()
        inputForm.Text = $"إضافة سجل جديد - {currentTable}"
        inputForm.Size = New Size(400, 300)
        Dim inputs As New Dictionary(Of String, Control)
        Dim yPos As Integer = 10

        ' Define fields for each table
        Dim fields As Dictionary(Of String, String()) = GetTableFields()
        If Not fields.ContainsKey(currentTable) Then Return

        ' Create input controls for each field (excluding ID as it's AUTOINCREMENT)
        For Each field In fields(currentTable)
            If field.ToUpper() = "ID" Then Continue For ' Skip ID field
            Dim label As New Label()
            label.Text = GetFieldDisplayName(currentTable, field)
            label.Location = New Point(10, yPos)
            inputForm.Controls.Add(label)

            Dim input As Control
            If field.EndsWith("_KIND") Or field = "E_ID" Or field = "CU_CITY" Then
                input = New ComboBox()
                input.Name = field
                LoadComboBoxData(DirectCast(input, ComboBox), field)
            ElseIf field = "COMPANY_LOGO" Then
                input = New Button()
                input.Text = "تحميل شعار"
                input.Name = field
                AddHandler input.Click, Sub()
                                            Dim openFileDialog As New OpenFileDialog()
                                            openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg"
                                            If openFileDialog.ShowDialog() = DialogResult.OK Then
                                                input.Tag = openFileDialog.FileName
                                            End If
                                        End Sub
            Else
                input = New TextBox()
                input.Name = field
            End If
            input.Location = New Point(150, yPos)
            input.Size = New Size(200, 25)
            inputForm.Controls.Add(input)
            inputs.Add(field, input)
            yPos += 30
        Next

        ' Add Save button
        Dim btnSave As New Button()
        btnSave.Text = "حفظ"
        btnSave.Location = New Point(150, yPos)
        btnSave.Size = New Size(100, 30)
        AddHandler btnSave.Click, Sub()
                                      SaveNewRecord(inputs)
                                      inputForm.Close()
                                  End Sub
        inputForm.Controls.Add(btnSave)

        inputForm.ShowDialog()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("يرجى تحديد سجل للتعديل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try


            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim id As Integer = Convert.ToInt32(selectedRow.Cells("ID").Value)

            Dim inputForm As New Form()
            inputForm.Text = $"تعديل سجل - {currentTable}"
            inputForm.Size = New Size(400, 300)
            Dim inputs As New Dictionary(Of String, Control)
            Dim yPos As Integer = 10

            ' Define fields for each table
            Dim fields As Dictionary(Of String, String()) = GetTableFields()
            If Not fields.ContainsKey(currentTable) Then Return

            ' Create input controls for each field (excluding ID)
            For Each field In fields(currentTable)
                If field.ToUpper() = "ID" Then Continue For ' Skip ID field
                Dim label As New Label()
                label.Text = GetFieldDisplayName(currentTable, field)
                label.Location = New Point(10, yPos)
                inputForm.Controls.Add(label)

                Dim input As Control
                If field.EndsWith("_KIND") Or field = "E_ID" Or field = "CU_CITY" Then
                    input = New ComboBox()
                    input.Name = field
                    LoadComboBoxData(DirectCast(input, ComboBox), field)
                    DirectCast(input, ComboBox).SelectedValue = selectedRow.Cells(field).Value
                ElseIf field = "COMPANY_LOGO" Then
                    input = New Button()
                    input.Text = "تحميل شعار"
                    input.Name = field
                    AddHandler input.Click, Sub()
                                                Dim openFileDialog As New OpenFileDialog()
                                                openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg"
                                                If openFileDialog.ShowDialog() = DialogResult.OK Then
                                                    input.Tag = openFileDialog.FileName
                                                End If
                                            End Sub
                Else
                    input = New TextBox()
                    input.Name = field
                    input.Text = selectedRow.Cells(field).Value?.ToString()
                End If
                input.Location = New Point(150, yPos)
                input.Size = New Size(200, 25)
                inputForm.Controls.Add(input)
                inputs.Add(field, input)
                yPos += 30
            Next

            ' Add Save button
            Dim btnSave As New Button()
            btnSave.Text = "حفظ"
            btnSave.Location = New Point(150, yPos)
            btnSave.Size = New Size(100, 30)
            AddHandler btnSave.Click, Sub()
                                          UpdateRecord(inputs, id)
                                          inputForm.Close()
                                      End Sub
            inputForm.Controls.Add(btnSave)

            inputForm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetTableFields() As Dictionary(Of String, String())
        Return New Dictionary(Of String, String()) From {
            {"COM_SEETING", {"ID", "COMPANY_NAME", "COMPANY_TEL", "COMPANY_TAX_NO", "COMPANY_ADDRESS", "COMPANY_LOGO", "POS_KIND", "key0", "key1", "tax_source_nu"}},
            {"PRODUCTS", {"ID", "BARCODE", "PRO_NAME", "PRO_PRICE", "PRO_TAX_VALUE", "PRO_TAX_KIND", "PRO_KIND"}},
            {"PRO_KIND", {"ID", "TXT"}},
            {"ID_CUSTOMER", {"ID", "TXT"}},
            {"CUSTOMERS", {"ID", "E_ID", "C_NAME", "C_TEL", "CU_CITY", "C_POSTCODE"}},
            {"CITY_TABLE", {"ID", "TXT"}},
            {"INV_PAY_KIND", {"ID", "TXT"}},
            {"IN_KIND", {"ID", "TXT"}},
            {"INV_OUT_IN", {"ID", "TXT"}},
            {"REMARK_KIND_TABLE", {"ID", "TXT"}},
            {"CURRENCY_TABLE", {"ID", "TXT", "C_CODE"}},
            {"POS_SCREEN_KIND", {"ID", "TXT"}},
            {"USER_TABLE", {"ID", "USER_NAME", "USER_CODE", "USER_KEY", "USER_FROM"}}
        }
    End Function

    Private Function GetFieldDisplayName(table As String, field As String) As String
        Dim displayNames As New Dictionary(Of String, Dictionary(Of String, String)) From {
            {"COM_SEETING", New Dictionary(Of String, String) From {{"COMPANY_NAME", "اسم الشركة"}, {"COMPANY_TEL", "هاتف الشركة"}, {"COMPANY_TAX_NO", "الرقم الضريبي"}, {"COMPANY_ADDRESS", "عنوان الشركة"}, {"COMPANY_LOGO", "شعار الشركة"}, {"POS_KIND", "نوع شاشه البيع"}, {"key0", "اسم الدخول"}, {"key1", "كلمة المرور"}, {"tax_source_nu", "تسلسل مصدر الدخل"}}},
            {"PRODUCTS", New Dictionary(Of String, String) From {{"BARCODE", "الباركود"}, {"PRO_NAME", "اسم المنتج"}, {"PRO_PRICE", "سعر المنتج"}, {"PRO_TAX_VALUE", "قيمة الضريبة"}, {"PRO_TAX_KIND", "نوع الضريبة"}, {"PRO_KIND", "نوع المنتج"}}},
            {"PRO_KIND", New Dictionary(Of String, String) From {{"TXT", "النوع"}}},
             {"POS_SCREEN_KIND", New Dictionary(Of String, String) From {{"TXT", "شاشه الطلب"}}},
            {"ID_CUSTOMER", New Dictionary(Of String, String) From {{"TXT", "نوع الهوية"}}},
            {"CUSTOMERS", New Dictionary(Of String, String) From {{"E_ID", "نوع الهوية"}, {"C_NAME", "اسم العميل"}, {"C_TEL", "هاتف العميل"}, {"CU_CITY", "المدينة"}, {"C_POSTCODE", "الرمز البريدي"}}},
            {"CITY_TABLE", New Dictionary(Of String, String) From {{"TXT", "اسم المدينة"}}},
            {"INV_PAY_KIND", New Dictionary(Of String, String) From {{"TXT", "نوع الدفع"}}},
            {"IN_KIND", New Dictionary(Of String, String) From {{"TXT", "نوع الفاتورة"}}},
            {"INV_OUT_IN", New Dictionary(Of String, String) From {{"TXT", "نوع الحركة"}}},
            {"REMARK_KIND_TABLE", New Dictionary(Of String, String) From {{"TXT", "نوع الملاحظة"}}},
            {"CURRENCY_TABLE", New Dictionary(Of String, String) From {{"TXT", "اسم العملة"}, {"C_CODE", "كود العملة"}}},
            {"USER_TABLE", New Dictionary(Of String, String) From {{"USER_NAME", "اسم المستخدم"}, {"USER_CODE", "كود المستخدم"}, {"USER_KEY", "مفتاح المستخدم"}, {"USER_FROM", "مصدر المستخدم"}}}
        }
        Return If(displayNames(table).ContainsKey(field), displayNames(table)(field), field)
    End Function

    Private Sub LoadComboBoxData(combo As ComboBox, field As String)
        Dim lookupTable As String = ""
        Select Case field
            Case "PRO_KIND" : lookupTable = "PRO_KIND"
            Case "E_ID" : lookupTable = "ID_CUSTOMER"
            Case "CU_CITY" : lookupTable = "CITY_TABLE"
            Case "INV_PAY_KIND" : lookupTable = "INV_PAY_KIND"
            Case "INV_KIND" : lookupTable = "IN_KIND"
            Case "INV_OUT_IN" : lookupTable = "INV_OUT_IN"
            Case "CURRENCY_KIND" : lookupTable = "CURRENCY_TABLE"
            Case "REMARK_KIND" : lookupTable = "REMARK_KIND_TABLE"
            Case "POS_SCREEN_KIND" : lookupTable = "POS_SCREEN_KIND"
        End Select

        If Not String.IsNullOrEmpty(lookupTable) Then
            Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
                conn.Open()
                Dim adapter As New SQLiteDataAdapter($"SELECT ID, TXT FROM {lookupTable}", conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                combo.DataSource = table
                combo.DisplayMember = "TXT"
                combo.ValueMember = "ID"
            End Using
        End If
    End Sub

    Private Sub SaveNewRecord(inputs As Dictionary(Of String, Control))
        Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim fields = GetTableFields()(currentTable).Where(Function(f) f.ToUpper() <> "ID").ToArray()
            Dim columns = String.Join(", ", fields)
            Dim parameters = String.Join(", ", fields.Select(Function(f) "@" & f))
            Dim cmd As New SQLiteCommand($"INSERT INTO {currentTable} ({columns}) VALUES ({parameters})", conn)

            For Each kvp In inputs
                If kvp.Value.GetType() = GetType(ComboBox) Then
                    cmd.Parameters.AddWithValue("@" & kvp.Key, DirectCast(kvp.Value, ComboBox).SelectedValue)
                ElseIf kvp.Key = "COMPANY_LOGO" AndAlso kvp.Value.Tag IsNot Nothing Then
                    Dim imageBytes As Byte() = File.ReadAllBytes(kvp.Value.Tag.ToString())
                    cmd.Parameters.AddWithValue("@" & kvp.Key, imageBytes)
                Else
                    cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value.Text)
                End If
            Next

            Try
                cmd.ExecuteNonQuery()
                LoadDataGrid()
                MessageBox.Show("تم إضافة السجل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"خطأ أثناء الإضافة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub UpdateRecord(inputs As Dictionary(Of String, Control), id As Integer)
        Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
            conn.Open()
            Dim fields = GetTableFields()(currentTable).Where(Function(f) f.ToUpper() <> "ID").ToArray()
            Dim setClause = String.Join(", ", fields.Select(Function(f) $"{f} = @{f}"))
            Dim cmd As New SQLiteCommand($"UPDATE {currentTable} SET {setClause} WHERE ID = @ID", conn)

            For Each kvp In inputs
                If kvp.Value.GetType() = GetType(ComboBox) Then
                    cmd.Parameters.AddWithValue("@" & kvp.Key, DirectCast(kvp.Value, ComboBox).SelectedValue)
                ElseIf kvp.Key = "COMPANY_LOGO" AndAlso kvp.Value.Tag IsNot Nothing Then
                    Dim imageBytes As Byte() = File.ReadAllBytes(kvp.Value.Tag.ToString())
                    cmd.Parameters.AddWithValue("@" & kvp.Key, imageBytes)
                Else
                    cmd.Parameters.AddWithValue("@" & kvp.Key, kvp.Value.Text)
                End If
            Next
            cmd.Parameters.AddWithValue("@ID", id)

            Try
                cmd.ExecuteNonQuery()
                LoadDataGrid()
                MessageBox.Show("تم تعديل السجل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"خطأ أثناء التعديل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Using conn As New SQLiteConnection($"Data Source=E_INVOIC.db;Version=3;")
                conn.Open()
                Dim id = DataGridView1.SelectedRows(0).Cells("ID").Value
                Dim cmd As New SQLiteCommand($"DELETE FROM {currentTable} WHERE ID = @id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
                LoadDataGrid()
            End Using
        End If
    End Sub

    Private Sub SETTING_PRO_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Form1.Show()
    End Sub

    Private Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Me.Close()
    End Sub
End Class