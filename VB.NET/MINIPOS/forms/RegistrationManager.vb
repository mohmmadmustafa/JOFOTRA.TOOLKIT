Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Management
Imports Newtonsoft.Json

Public Class RegistrationManager

    Private Const SecretSalt As String = "YourUniqueSalt123"
    Private Const RegistrationFile As String = "registration.dat"

    ''' <summary>
    ''' التحقق من حالة التسجيل عند بدء البرنامج وإنشاء رقم تسجيل فريد إذا لزم الأمر
    ''' </summary>
    Public Shared Function CheckAndInitializeRegistration(userName As String) As Boolean
        Try
            Dim regData As (RegName As String, RegCode As String) = GetRegistrationData()
            Dim decryptedRegName As String = Decrypt(regData.RegName)


            If decryptedRegName <> GenerateUniqueRegNumber(Environment.UserName) Then
                '  Throw New Exception( {userName}")
                MsgBox(decryptedRegName & " " & GenerateUniqueRegNumber(Environment.UserName))
                Return False
            End If
            If String.IsNullOrEmpty(decryptedRegName) Then
                Dim newRegNumber As String = GenerateUniqueRegNumber(userName)
                Dim newRegKey As String = GenerateRegKey(newRegNumber)
                SaveRegCodeOnly(newRegKey, newRegNumber)
                Return False
            Else
                If VerifyRegistration(decryptedRegName, Decrypt(regData.RegCode)) Then
                    Return True
                Else
                    Dim newRegNumber As String = GenerateUniqueRegNumber(userName)
                    Dim newRegKey As String = GenerateRegKey(newRegNumber)
                    SaveRegCodeOnly(newRegKey, newRegNumber)
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء التحقق من التسجيل: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' توليد رقم تسجيل فريد للمستخدم
    ''' </summary>
    Public Shared Function GenerateUniqueRegNumber(userName As String) As String
        Try
            Dim deviceId As String = GetDeviceId()
            Dim guide As String = Guid.NewGuid().ToString()
            ' Dim rawData As String = $"{guide}{userName}{deviceId}"
            Dim rawData As String = $"{userName}{deviceId}"
            Using sha256 As SHA256 = SHA256.Create()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(rawData & SecretSalt)
                Dim hash As Byte() = sha256.ComputeHash(bytes)
                Dim regNumber As String = Convert.ToBase64String(hash).Replace("=", "").Substring(0, 20)
                Return regNumber
            End Using
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء توليد رقم التسجيل: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' توليد مفتاح تسجيل بناءً على رقم التسجيل
    ''' </summary>
    Public Shared Function GenerateRegKey(regNumber As String) As String
        Try
            Using sha256 As SHA256 = SHA256.Create()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(regNumber & SecretSalt)
                Dim hash As Byte() = sha256.ComputeHash(bytes)
                Return Convert.ToBase64String(hash).Substring(0, 20)
            End Using
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء توليد مفتاح التسجيل: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' الحصول على معرف الجهاز (مثال: معرف اللوحة الأم)
    ''' </summary>
    Private Shared Function GetDeviceId() As String
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard")
            For Each info As ManagementObject In searcher.Get()
                Return info("SerialNumber").ToString()
            Next
            Return "UnknownDevice"
        Catch
            Return "UnknownDevice"
        End Try
    End Function

    ''' <summary>
    ''' تشفير النص باستخدام AES
    ''' </summary>
    Public Shared Function Encrypt(text As String) As String
        If String.IsNullOrEmpty(text) Then Return text
        Try
            Using aes As Aes = Aes.Create()
                aes.Key = Encoding.UTF8.GetBytes("Your32ByteSecretKey1234567890123")
                aes.IV = New Byte(15) {}
                Dim encryptor = aes.CreateEncryptor(aes.Key, aes.IV)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor, CryptoStreamMode.Write)
                        Using sw As New StreamWriter(cs)
                            sw.Write(text)
                        End Using
                    End Using
                    Return Convert.ToBase64String(ms.ToArray())
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء التشفير: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' فك تشفير النص
    ''' </summary>
    Public Shared Function Decrypt(encryptedText As String) As String
        If String.IsNullOrEmpty(encryptedText) Then Return encryptedText
        Try
            Dim cipherText As Byte() = Convert.FromBase64String(encryptedText)
            Using aes As Aes = Aes.Create()
                aes.Key = Encoding.UTF8.GetBytes("Your32ByteSecretKey1234567890123")
                aes.IV = New Byte(15) {}
                Dim decryptor = aes.CreateDecryptor(aes.Key, aes.IV)
                Using ms As New MemoryStream(cipherText)
                    Using cs As New CryptoStream(ms, decryptor, CryptoStreamMode.Read)
                        Using sr As New StreamReader(cs)
                            Return sr.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' استرجاع بيانات التسجيل من الملف
    ''' </summary>
    Public Shared Function GetRegistrationData() As (RegName As String, RegCode As String)
        Try
            If File.Exists(RegistrationFile) Then
                Dim encryptedData As String = File.ReadAllText(RegistrationFile)
                Dim jsonData As String = Decrypt(encryptedData)
                Dim data As Dictionary(Of String, String) = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(jsonData)
                Return (data("reg_name"), data("REG_CODE"))
            Else
                ' Generate a unique registration number for the default record
                Dim defaultRegNumber As String = GenerateUniqueRegNumber(Environment.UserName)
                Dim defaultRegKey As String = GenerateRegKey("DEMO")
                CreateDefaultRecord(defaultRegNumber, defaultRegKey)
                Return (Encrypt(defaultRegNumber), Encrypt(defaultRegKey))
            End If
        Catch ex As Exception
            ' Generate a unique registration number in case of error
            Dim defaultRegNumber As String = GenerateUniqueRegNumber(Environment.UserName)
            Dim defaultRegKey As String = GenerateRegKey("DEMO")
            CreateDefaultRecord(defaultRegNumber, defaultRegKey)
            Return (Encrypt(defaultRegNumber), Encrypt(defaultRegKey))
        End Try
    End Function

    ''' <summary>
    ''' حفظ مفتاح التسجيل فقط في الملف
    ''' </summary>
    Public Shared Sub SaveRegCodeOnly(regKey As String, Optional regNumber As String = Nothing)
        Try
            Dim effectiveRegNumber As String = If(regNumber, GenerateUniqueRegNumber(Environment.UserName))
            Dim data As New Dictionary(Of String, String) From {
                {"reg_name", Encrypt(effectiveRegNumber)},
                {"REG_CODE", Encrypt(regKey)}
            }
            Dim jsonData As String = JsonConvert.SerializeObject(data)
            Dim encryptedData As String = Encrypt(jsonData)
            File.WriteAllText(RegistrationFile, encryptedData)
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء حفظ مفتاح التسجيل: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' حفظ بيانات التسجيل الكاملة في الملف
    ''' </summary>
    Public Shared Sub SaveRegistration(regNumber As String, regKey As String)
        Try
            Dim data As New Dictionary(Of String, String) From {
                {"reg_name", Encrypt(regNumber)},
                {"REG_CODE", Encrypt(regKey)}
            }
            Dim jsonData As String = JsonConvert.SerializeObject(data)
            Dim encryptedData As String = Encrypt(jsonData)
            File.WriteAllText(RegistrationFile, encryptedData)
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء حفظ بيانات التسجيل: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' التحقق من صحة التسجيل
    ''' </summary>
    Public Shared Function VerifyRegistration(regNumber As String, regKey As String) As Boolean
        Try
            Dim expectedKey As String = GenerateRegKey(regNumber)
            ' MsgBox(expectedKey & " -- " & regNumber)
            ' Clipboard.SetText(expectedKey)
            Return regKey = expectedKey
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' إنشاء سجل افتراضي
    ''' </summary>
    Public Shared Sub CreateDefaultRecord(regNumber As String, regKey As String)
        Try
            Dim data As New Dictionary(Of String, String) From {
                {"reg_name", Encrypt(regNumber)},
                {"REG_CODE", Encrypt(regKey)}
            }
            Dim jsonData As String = JsonConvert.SerializeObject(data)
            Dim encryptedData As String = Encrypt(jsonData)
            File.WriteAllText(RegistrationFile, encryptedData)
        Catch ex As Exception
            Throw New Exception($"خطأ أثناء إنشاء سجل افتراضي: {ex.Message}")
        End Try
    End Sub
End Class