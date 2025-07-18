Imports System.IO

Public Class frmAbout
    Public Shared IsRegistered3 As Boolean = False
    Private Const AppName As String = "mini pos"
    Private Const AppVersion As String = "0.0.1"

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblAppInfo.Text = $"{AppName} - Version {AppVersion}"
            CheckRegistrationStatus()
            UpdateUI()
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء التحميل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CheckRegistrationStatus()
        Try
            Dim regData As (RegName As String, RegCode As String) = RegistrationManager.GetRegistrationData()
            ' MsgBox(regData.RegCode)
            If String.IsNullOrEmpty(regData.RegName) OrElse regData.RegName = "DEMO" Then
                IsRegistered = False
            Else
                '  IsRegistered = RegistrationManager.CheckAndInitializeRegistration(RegistrationManager.Decrypt(regData.RegName))

                IsRegistered = RegistrationManager.VerifyRegistration(RegistrationManager.Decrypt(regData.RegName), RegistrationManager.Decrypt(regData.RegCode))


            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء التحقق من التسجيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            IsRegistered = False
        End Try
    End Sub

    Private Sub UpdateUI()
        Try

            If IsRegistered Then
                lblStatus.Text = "النسخة مسجلة"
                txtRegNumber.Text = RegistrationManager.Decrypt(RegistrationManager.GetRegistrationData().RegName)
                Dim regKey As String = RegistrationManager.Decrypt(RegistrationManager.GetRegistrationData().RegCode)
                txtRegNumber.ReadOnly = True
                txtRegKey.ReadOnly = True
                txtRegKey.Text = regKey
                'lblRegKey.Enabled = False
                btnRegister.Visible = False
            Else
                lblStatus.Text = "النسخة غير مسجلة"
                Dim regData As (RegName As String, RegCode As String) = RegistrationManager.GetRegistrationData()
                Dim regName As String = RegistrationManager.Decrypt(regData.RegName)
                ' Dim regKey As String = RegistrationManager.Decrypt(regData.RegCode)
                txtRegNumber.Text = regName
                txtRegNumber.ReadOnly = True
                '  txtRegKey.Text = regKey
                txtRegKey.Visible = True
                lblRegKey.Visible = True
                btnRegister.Visible = True
                '  Clipboard.SetText(regKey)
            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء تحديث الواجهة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Try
            Dim regName As String = txtRegNumber.Text.Trim()
            Dim regKey As String = txtRegKey.Text.Trim()

            If String.IsNullOrEmpty(regName) OrElse String.IsNullOrEmpty(regKey) Then
                MessageBox.Show("يرجى إدخال رقم التسجيل ومفتاح التسجيل", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If RegistrationManager.VerifyRegistration(regName, regKey) Then
                RegistrationManager.SaveRegistration(regName, regKey)
                IsRegistered = True
                UpdateUI()
                MessageBox.Show("تم التسجيل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("مفتاح التسجيل غير صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                UpdateUI()
            End If
        Catch ex As Exception
            MessageBox.Show($"خطأ أثناء التسجيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class