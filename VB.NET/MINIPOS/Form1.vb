Imports System.Threading.Tasks

Public Class Form1


    Private Sub HopeButton2_Click(sender As Object, e As EventArgs) Handles HopeButton2.Click
        Dim M As New INV_LIST
        M.Show()
        Me.Hide()
    End Sub

    Private Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Dim M As New SETTING_PRO
        M.Show()
        Me.Hide()
    End Sub

    Private Sub HopeButton3_Click(sender As Object, e As EventArgs) Handles HopeButton3.Click
        Dim dbHandler As New DataGenerator()
        dbHandler.GenerateTestData()
        MessageBox.Show("تم إنشاء البيانات العشوائية", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub HopeButton4_Click(sender As Object, e As EventArgs) Handles HopeButton4.Click
        Dim m As New PAY_POS
        m.Show()
        Me.Hide()
    End Sub

    Private Async Sub HopeButton5_Click(sender As Object, e As EventArgs) Handles HopeButton5.Click
        ' Try
        ' TryWRITE
        ' Dim M As New InvoiceUploader
        ' Await sendinvoic()
        'Console.WriteLine("Result: " & result)
        'Catch ex As Exception
        '   MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ' End Try
        Await STARDO()
        ' Dim result = Await InvoiceSender.SendInvoiceToAPI()
        ' Console.WriteLine(result)
        ' EInvoiceVBNet.Main()
    End Sub
    Private Async Function STARDO() As Task
        Try
            ' Sample data
            Dim seller As New Seller(
                id:="14807394",
                taxId:="10611649",
                name:="مجد عوض محمود الجيزاوي",
                address:="شارع الملكة رانيا, عمان, 11181, JO"
            )

            Dim buyer As New Buyer(
                id:="",
                taxId:="1",
                name:="عميل نقدي",
                address:="غير معروف, JO",
                idType:=""
            )
            '383 تصحيحه
            '388 

            Dim invType As New InvoiceType("388", "011")

            Dim items As Item() = {New Item("939c0905-182d-4b05-909e-acb217807ddb", "2", "1.000", "0.000", "0.000", "0.500", "0.000", "Meal Service", "5610")}
            ',
            ' New Item("a1b2c3d4-5678-9012-3456-789012345678", "2", "0.50", "0.00", "0.00", "0.50", "0", "Beverage Service", "5610")
            ' }

            ' Generate and send invoice
            Dim sender As New InvoiceSender(seller, buyer, invType, items)
            Dim result = Await sender.SendInvoiceToAPI(10)
            MessageBox.Show(result("Response"), "Invoice Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Private Sub CheckRegistrationStatus()
        Try
            Dim regData As (RegName As String, RegCode As String) = RegistrationManager.GetRegistrationData()

            If String.IsNullOrEmpty(regData.RegName) OrElse regData.RegName = "DEMO" Then
                IsRegistered = False
            Else
                ' IsRegistered = RegistrationManager.CheckAndInitializeRegistration(Environment.UserName)

                IsRegistered = RegistrationManager.VerifyRegistration(RegistrationManager.Decrypt(regData.RegName), RegistrationManager.Decrypt(regData.RegCode))



            End If
            If IsRegistered Then
                Me.Text = "النسخه  مسجله"
            Else
                Me.Text = "النسخه غير مسجله - مقيده الاستخدام"
                '  MessageBox.Show("البرنامج غير مسجل، يرجى إدخال مفتاح التسجيل", "حالة التسجيل", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            '  MessageBox.Show($"خطأ أثناء التحقق من التسجيل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            IsRegistered = False
        End Try
    End Sub

    Private Sub HopeButton6_Click(sender As Object, e As EventArgs) Handles HopeButton6.Click
        Dim M As New PAY_POS_BUTTON
        M.Show()
    End Sub

    Private Sub HopeButton8_Click(sender As Object, e As EventArgs) Handles HopeButton8.Click
        Dim M As New return_invoice
        M.Show()
    End Sub

    Private Sub HopeButton9_Click(sender As Object, e As EventArgs) Handles HopeButton9.Click
        Dim M As New company_setting
        M.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckRegistrationStatus()
    End Sub

    Private Sub HopeButton7_Click(sender As Object, e As EventArgs) Handles HopeButton7.Click
        Dim m As New frmAbout
        m.ShowDialog()
        CheckRegistrationStatus()
    End Sub
End Class
