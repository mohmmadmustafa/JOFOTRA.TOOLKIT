' LoginForm.vb - Code Behind
Imports System.Data.SQLite

Public Class LoginForm
    ' تعريف الهيكلية


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) Or String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("يرجى إدخال اسم المستخدم وكلمة المرور", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Using conn As New SQLiteConnection("Data Source=E_INVOIC.db;Version=3;")
                conn.Open()

                Dim query As String = "SELECT * FROM USER_TABLE WHERE username = @username AND password = @password"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text)
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' تعبئة بيانات المستخدم في الهيكلية
                            userInfo.id = reader.GetInt32(reader.GetOrdinal("ID"))
                            userInfo.name = reader.GetString(reader.GetOrdinal("USER_NAME"))
                            userInfo.scurty_id = reader.GetString(reader.GetOrdinal("USER_CODE"))
                            userInfo.key = reader.GetString(reader.GetOrdinal("USER_KEY"))
                            userInfo.kind = reader.GetInt32(reader.GetOrdinal("u_kind"))

                            MessageBox.Show("تم تسجيل الدخول بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = DialogResult.OK
                            Me.Close()
                        Else
                            MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class