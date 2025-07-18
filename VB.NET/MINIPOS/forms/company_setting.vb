Imports System.Data.SQLite
Imports System.IO

Public Class company_setting
    Private conn As SQLiteConnection
    Private currentId As Integer = 1 ' Default to ID 1, adjust as needed

    Private Sub company_setting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
        Try
            conn.Open()
            LoadData()
        Catch ex As Exception
            MessageBox.Show("Error connecting to database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "SELECT * FROM COM_SEETING WHERE ID = @id"
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", currentId)
                Using reader As SQLiteDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        txtCompanyName.Text = If(reader.IsDBNull(1), "", reader.GetString(1))
                        txtCompanyTel.Text = If(reader.IsDBNull(2), "", reader.GetString(2))
                        txtCompanyTaxNo.Text = If(reader.IsDBNull(3), "", reader.GetString(3))
                        txtCompanyAddress.Text = If(reader.IsDBNull(4), "", reader.GetString(4))
                        txtPosKind.Text = If(reader.IsDBNull(6), "", reader.GetInt32(6).ToString())
                        txtKey0.Text = If(reader.IsDBNull(7), "", reader.GetString(7))
                        txtTaxSourceNo.Text = If(reader.IsDBNull(9), "", reader.GetString(9))
                        Try


                            ' Load COMPANY_LOGO (BLOB) into PictureBox
                            If Not reader.IsDBNull(5) Then
                                Dim logoBytes() As Byte = DirectCast(reader(5), Byte())
                                Using ms As New MemoryStream(logoBytes)
                                    pictureBoxLogo.Image = Image.FromStream(ms)
                                End Using
                            Else
                                pictureBoxLogo.Image = Nothing
                            End If
                        Catch ex As Exception

                        End Try
                        ' Load KEY1 (BLOB) and convert to Base64 for preview
                        If Not reader.IsDBNull(8) Then

                            txtKey1Preview.Text = If(reader.IsDBNull(8), "", reader.GetString(8))
                        Else
                            txtKey1Preview.Text = ""
                        End If
                    Else
                        MessageBox.Show("No record found for ID " & currentId, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ClearFields()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub ClearFields()
        txtCompanyName.Text = ""
        txtCompanyTel.Text = ""
        txtCompanyTaxNo.Text = ""
        txtCompanyAddress.Text = ""
        txtPosKind.Text = ""
        txtKey0.Text = ""
        txtTaxSourceNo.Text = ""
        pictureBoxLogo.Image = Nothing
        txtKey1Preview.Text = ""
    End Sub

    Private Sub btnLoadImage_Click(sender As Object, e As EventArgs) Handles btnLoadImage.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                pictureBoxLogo.Image = Image.FromFile(openFileDialog.FileName)
            End If
        End Using
    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim query As String = "INSERT OR REPLACE INTO COM_SEETING (ID, COMPANY_NAME, COMPANY_TEL, COMPANY_TAX_NO, COMPANY_ADDRESS, COMPANY_LOGO, POS_KIND, key0, key1, tax_source_nu) " &
                                 "VALUES (@id, @companyName, @companyTel, @companyTaxNo, @companyAddress, @companyLogo, @posKind, @key0, @key1, @taxSourceNo)"
            Using cmd As New SQLiteCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", currentId)
                cmd.Parameters.AddWithValue("@companyName", If(String.IsNullOrEmpty(txtCompanyName.Text), DBNull.Value, txtCompanyName.Text))
                cmd.Parameters.AddWithValue("@companyTel", If(String.IsNullOrEmpty(txtCompanyTel.Text), DBNull.Value, txtCompanyTel.Text))
                cmd.Parameters.AddWithValue("@companyTaxNo", If(String.IsNullOrEmpty(txtCompanyTaxNo.Text), DBNull.Value, txtCompanyTaxNo.Text))
                cmd.Parameters.AddWithValue("@companyAddress", If(String.IsNullOrEmpty(txtCompanyAddress.Text), DBNull.Value, txtCompanyAddress.Text))
                cmd.Parameters.AddWithValue("@posKind", If(String.IsNullOrEmpty(txtPosKind.Text), DBNull.Value, Integer.Parse(txtPosKind.Text)))
                cmd.Parameters.AddWithValue("@key0", If(String.IsNullOrEmpty(txtKey0.Text), DBNull.Value, txtKey0.Text))
                cmd.Parameters.AddWithValue("@key1", If(String.IsNullOrEmpty(txtKey1Preview.Text), DBNull.Value, txtKey1Preview.Text))
                cmd.Parameters.AddWithValue("@taxSourceNo", If(String.IsNullOrEmpty(txtTaxSourceNo.Text), DBNull.Value, txtTaxSourceNo.Text))
                Try


                    ' Handle COMPANY_LOGO (BLOB)
                    If pictureBoxLogo.Image IsNot Nothing Then
                        Using ms As New MemoryStream()
                            pictureBoxLogo.Image.Save(ms, pictureBoxLogo.Image.RawFormat)
                            cmd.Parameters.AddWithValue("@companyLogo", ms.ToArray())
                        End Using
                    Else
                        cmd.Parameters.AddWithValue("@companyLogo", DBNull.Value)
                    End If
                Catch ex As Exception

                End Try



                cmd.ExecuteNonQuery()
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        LoadData()
    End Sub

    Private Sub company_setting_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
            conn.Close()
            conn.Dispose()
        End If
    End Sub

    ' Debug method
    Private Sub add_debug(message As String)
        Console.WriteLine(message)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class