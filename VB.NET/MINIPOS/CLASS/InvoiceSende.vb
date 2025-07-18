

' InvoiceSender.vb - Class for generating and sending invoices
Imports System.Net.Http
Imports System.Text
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq
Imports System.Xml.Schema
Imports System.Net
Imports System.Data.SQLite

Public Class InvoiceSender
    Private ReadOnly client As New HttpClient()
    Private Const API_URL As String = "https://backend.jofotara.gov.jo/core/invoices/"
    Private Const QR_FOLDER As String = "QR_Codes"
    Private Const SCHEMA_PATH As String = "Schemas\UBL-Invoice-2.1.xsd"
    Public Seller As Seller
    Public Buyer As Buyer
    Public InvoiceType As InvoiceType
    Public items As Item()

    Public Sub New(seller1 As Seller, buyer1 As Buyer, invoiceType1 As InvoiceType, items1 As Item())
        Seller = seller1
        Buyer = buyer1
        InvoiceType = invoiceType1
        items = items1
    End Sub

    Public Function Generate() As String
        If items Is Nothing OrElse items.Length = 0 Then
            Throw New ArgumentException("Items array cannot be empty")
        End If
        If Seller Is Nothing OrElse Buyer Is Nothing OrElse InvoiceType Is Nothing Then
            Throw New ArgumentException("Seller, Buyer, and InvoiceType cannot be null")
        End If

        Return XmlBuilder01.BuildInvoiceXml(Seller, Buyer, InvoiceType, items)
    End Function

    Public Async Function SendInvoiceToAPI(INVOID As Integer) As Task(Of Dictionary(Of String, String))
        Dim result As New Dictionary(Of String, String) From {
            {"Response", ""},
            {"EINV_QR", ""},
            {"EINV_NUM", ""},
            {"EINV_INV_UUID", ""},
            {"EINV_SINGED_INVOICE", ""}
        }

        Try
            ' Generate invoice XML
            Dim invoiceXml As String = Generate()
            File.WriteAllText("O3_invoice.xml", invoiceXml)

            ' Validate XML
            '  If Not ValidateXML(invoiceXml) Then
            'result("Response") = "فشل التحقق من صحة XML"
            'Return result
            ' End If

            ' Load certificate and sign XML (commented out until certificate is provided)
            ' Dim cert As X509Certificate2 = LoadCertificate("d:\k\myCertificate.pfx", "mom-3655807")
            ' If cert Is Nothing Then
            '     result("Response") = "فشل تحميل الشهادة الرقمية"
            '     Return result
            ' End If
            ' Dim signedXml As String = SignInvoice(invoiceXml, cert)
            ' If signedXml Is Nothing Then
            '     result("Response") = "فشل في توقيع الفاتورة"
            '     Return result
            ' End If
            ' invoiceXml = signedXml
            ' File.WriteAllText("signed_invoice.xml", invoiceXml)

            ' Convert to Base64
            invoiceXml = CleanXml(invoiceXml)
            Dim base64String As String = ConvertToBase64(invoiceXml)
            If Not ValidateBase64(base64String) Then
                result("Response") = "فشل تحويل XML إلى Base64 صالح"
                Return result
            End If

            ' Create JSON payload
            Dim jsonData As New Dictionary(Of String, String) From {
                {"invoice", base64String}
            }
            Dim jsonSerializer As New JavaScriptSerializer()
            Dim jsonContent As String = jsonSerializer.Serialize(jsonData)
            File.WriteAllText("O3D_invoice.xml", jsonContent)

            ' Set up HTTP client
            '''''MUST WRITE    client.DefaultRequestHeaders.Clear()
            '''''MUST WRITE    client.DefaultRequestHeaders.Add("Client-Id", "804bb4f8-f745-4a38-b9dd-95c09eec6009")
            '''''MUST WRITE    client.DefaultRequestHeaders.Add("Secret-Key", "Gj5nS9wyYHRadaVffz5VKB4v4wlVWyPhcJvrTD4NHtPk6ZAzKgbQdrVlgQOfzt0r+g4x9HtOiRWkLQJ4aqgmMTG2xL7rI0WJfvFDgzF1wD5uHcfdU0idPw7dZ2s+H4nzaOJzETIhZk9L3JGDan0O58+iiSKAyemIzGbmshoRmbmBPCukJrIZZHSQGXJJjjTv5rUFxpfT3dj7pFYk/ZoUNd6TFrXK1FsdbEQdHMAqYfELi3vGtK24J2gOhy+OJQ6mpTwHn10VoGKGSJLMNzk6vQ==")
            '''''MUST WRITE     client.DefaultRequestHeaders.Add("Timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"))
            '''''MUST WRITE     client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:138.0) Gecko/20100101 Firefox/138.0")
            '''''MUST WRITE      client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))

            ' Send POST request
            '''''MUST WRITE  Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
            '''''MUST WRITE  Dim response As HttpResponseMessage  = Await client.PostAsync(API_URL, content)
            Dim responseContent As String = GETVIRCUALRESPOND() ' Await response.Content.ReadAsStringAsync()
            '''''MUST WRITE Dim contentType As String = response.Content.Headers.ContentType?.ToString()
            Console.WriteLine(responseContent)
            ' تحليل J SON
            Dim startIndex As Integer = responseContent.IndexOf("{")
            Dim endIndex As Integer = responseContent.LastIndexOf("}") + 1
            If startIndex >= 0 AndAlso endIndex > startIndex Then
                responseContent = responseContent.Substring(startIndex, endIndex - startIndex)
            Else
                result("Response") = ("لم يتم العثور على JSON صالح في الاستجابة")
                '' "فشل تحويل XML إلى Base64 صالح"
                Return result
            End If

            ' Process response
            '''''MUST WRITE  If response.IsSuccessStatusCode Then
            '  MsgBox("0")
            Dim jsonObject As JObject = JObject.Parse(responseContent)
            '  MsgBox("1")
            ' استخراج القيم وتخزينها في متغيرات
            Dim status As String = jsonObject("EINV_RESULTS")("status").ToString()
            '  MsgBox(status)
            If status = "PASS" Then

                Dim einvStatus As String = jsonObject("EINV_STATUS").ToString()
                Dim signedInvoice As String = jsonObject("EINV_SINGED_INVOICE").ToString()
                Dim qrCode As String = jsonObject("EINV_QR").ToString()
                Dim invoiceNumber As String = jsonObject("EINV_NUM").ToString()
                Dim invoiceUUID As String = jsonObject("EINV_INV_UUID").ToString()
                '  Dim infoType As String = jsonObject("EINV_RESULTS")("INFO")(0)("type").ToString()
                '  Dim infoStatus As String = jsonObject("EINV_RESULTS")("INFO")(0)("status").ToString()
                '  Dim einvCode As String = jsonObject("EINV_RESULTS")("INFO")(0)("EINV_CODE").ToString()
                '  Dim einvCategory As String = jsonObject("EINV_RESULTS")("INFO")(0)("EINV_CATEGORY").ToString()
                '  Dim einvMessage As String = jsonObject("EINV_RESULTS")("INFO")(0)("EINV_MESSAGE").ToString()
                SAVEDATA(INVOID, einvStatus, signedInvoice, qrCode, invoiceNumber, invoiceUUID)
            Else
                '''''MUST WRITE   result("Response") = ProcessErrorResponse(response.StatusCode, responseContent)


            End If
            '''''MUST WRITE  Else
            '''''MUST WRITE  result("Response") = ProcessErrorResponse(response.StatusCode, responseContent)
            '''''MUST WRITE   End If

            ' Save QR code if present

        Catch ex As Exception
            Clipboard.SetText(ex.Message)
            result("Response") = $"Error: {ex.Message}"
        End Try

        Return result
    End Function
    Private Function GETVIRCUALRESPOND() As String
        Return "الاستجابة غير متوقعة: {""EINV_RESULTS"":{""status"":""PASS"",""INFO"":[{""type"":""INFO"",""status"":""PASS"",""EINV_CODE"":""XSD_VALID"",""EINV_CATEGORY"":""XSD validation"",""EINV_MESSAGE"":""complied With UBL 2.1 standards""}],""WARNINGS"":[],""ERRORS"":[]},""EINV_STATUS"":""ALREADY_SUBMITTED"",""EINV_SINGED_INVOICE"":""UEQ5NGJXd2dkbVZ5YzJsdmJqMGlNUzR3SWlCbGJtTnZaR2x1WnowaVZWUkdMVGdpUHo0S1BFbHVkbTlwWTJVZ2VHMXNibk05SW5WeWJqcHZZWE5wY3pwdVlXMWxjenB6Y0dWamFXWnBZMkYwYVc5dU9uVmliRHB6WTJobGJXRTZlSE5rT2tsdWRtOXBZMlV0TWlJZ2VHMXNibk02WTJGalBTSjFjbTQ2YjJGemFYTTZibUZ0WlhNNmMzQmxZMmxtYVdOaGRHbHZianAxWW13NmMyTm9aVzFoT25oelpEcERiMjF0YjI1QloyZHlaV2RoZEdWRGIyMXdiMjVsYm5SekxUSWlJSGh0Ykc1ek9tTmlZejBpZFhKdU9tOWhjMmx6T201aGJXVnpPbk53WldOcFptbGpZWFJwYjI0NmRXSnNPbk5qYUdWdFlUcDRjMlE2UTI5dGJXOXVRbUZ6YVdORGIyMXdiMjVsYm5SekxUSWlJSGh0Ykc1ek9tVjRkRDBpZFhKdU9tOWhjMmx6T201aGJXVnpPbk53WldOcFptbGpZWFJwYjI0NmRXSnNPbk5qYUdWdFlUcDRjMlE2UTI5dGJXOXVSWGgwWlc1emFXOXVRMjl0Y0c5dVpXNTBjeTB5SWo0OFpYaDBPbFZDVEVWNGRHVnVjMmx2Ym5NK1BHVjRkRHBWUWt4RmVIUmxibk5wYjI0K1BHVjRkRHBGZUhSbGJuTnBiMjVWVWtrK2RYSnVPbTloYzJsek9tNWhiV1Z6T25Od1pXTnBabWxqWVhScGIyNDZkV0pzT21SemFXYzZaVzUyWld4dmNHVmtPbmhoWkdWelBDOWxlSFE2UlhoMFpXNXphVzl1VlZKSlBqeGxlSFE2UlhoMFpXNXphVzl1UTI5dWRHVnVkRDQ4SVMwdElGQnNaV0Z6WlNCdWIzUmxJSFJvWVhRZ2RHaGxJSE5wWjI1aGRIVnlaU0IyWVd4MVpYTWdZWEpsSUhOaGJYQnNaU0IyWVd4MVpYTWdiMjVzZVNBdExUNDhjMmxuT2xWQ1RFUnZZM1Z0Wlc1MFUybG5ibUYwZFhKbGN5QjRiV3h1Y3pwemFXYzlJblZ5YmpwdllYTnBjenB1WVcxbGN6cHpjR1ZqYVdacFkyRjBhVzl1T25WaWJEcHpZMmhsYldFNmVITmtPa052YlcxdmJsTnBaMjVoZEhWeVpVTnZiWEJ2Ym1WdWRITXRNaUlnZUcxc2JuTTZjMkZqUFNKMWNtNDZiMkZ6YVhNNmJtRnRaWE02YzNCbFkybG1hV05oZEdsdmJqcDFZbXc2YzJOb1pXMWhPbmh6WkRwVGFXZHVZWFIxY21WQloyZHlaV2RoZEdWRGIyMXdiMjVsYm5SekxUSWlJSGh0Ykc1ek9uTmlZejBpZFhKdU9tOWhjMmx6T201aGJXVnpPbk53WldOcFptbGpZWFJwYjI0NmRXSnNPbk5qYUdWdFlUcDRjMlE2VTJsbmJtRjBkWEpsUW1GemFXTkRiMjF3YjI1bGJuUnpMVElpUGp4ellXTTZVMmxuYm1GMGRYSmxTVzVtYjNKdFlYUnBiMjQrUEdOaVl6cEpSRDUxY200NmIyRnphWE02Ym1GdFpYTTZjM0JsWTJsbWFXTmhkR2x2YmpwMVltdzZjMmxuYm1GMGRYSmxPakU4TDJOaVl6cEpSRDQ4YzJKak9sSmxabVZ5Wlc1alpXUlRhV2R1WVhSMWNtVkpSRDUxY200NmIyRnphWE02Ym1GdFpYTTZjM0JsWTJsbWFXTmhkR2x2YmpwMVltdzZjMmxuYm1GMGRYSmxPa2x1ZG05cFkyVThMM05pWXpwU1pXWmxjbVZ1WTJWa1UybG5ibUYwZFhKbFNVUStQR1J6T2xOcFoyNWhkSFZ5WlNCNGJXeHVjenBrY3owaWFIUjBjRG92TDNkM2R5NTNNeTV2Y21jdk1qQXdNQzh3T1M5NGJXeGtjMmxuSXlJZ1NXUTlJbk5wWjI1aGRIVnlaU0krUEdSek9sTnBaMjVsWkVsdVptOCtQR1J6T2tOaGJtOXVhV05oYkdsNllYUnBiMjVOWlhSb2IyUWdRV3huYjNKcGRHaHRQU0pvZEhSd09pOHZkM2QzTG5jekxtOXlaeTh5TURBMkx6RXlMM2h0YkMxak1UUnVNVEVpTHo0OFpITTZVMmxuYm1GMGRYSmxUV1YwYUc5a0lFRnNaMjl5YVhSb2JUMGlhSFIwY0RvdkwzZDNkeTUzTXk1dmNtY3ZNakF3TVM4d05DOTRiV3hrYzJsbkxXMXZjbVVqWldOa2MyRXRjMmhoTWpVMklpOCtQR1J6T2xKbFptVnlaVzVqWlNCSlpEMGlhVzUyYjJsalpWTnBaMjVsWkVSaGRHRWlJRlZTU1QwaUlqNDhaSE02VkhKaGJuTm1iM0p0Y3o0OFpITTZWSEpoYm5ObWIzSnRJRUZzWjI5eWFYUm9iVDBpYUhSMGNEb3ZMM2QzZHk1M015NXZjbWN2VkZJdk1UazVPUzlTUlVNdGVIQmhkR2d0TVRrNU9URXhNVFlpUGp4a2N6cFlVR0YwYUQ1dWIzUW9MeTloYm1ObGMzUnZjaTF2Y2kxelpXeG1PanBsZUhRNlZVSk1SWGgwWlc1emFXOXVjeWs4TDJSek9saFFZWFJvUGp3dlpITTZWSEpoYm5ObWIzSnRQanhrY3pwVWNtRnVjMlp2Y20wZ1FXeG5iM0pwZEdodFBTSm9kSFJ3T2k4dmQzZDNMbmN6TG05eVp5OVVVaTh4T1RrNUwxSkZReTE0Y0dGMGFDMHhPVGs1TVRFeE5pSStQR1J6T2xoUVlYUm9QbTV2ZENndkwyRnVZMlZ6ZEc5eUxXOXlMWE5sYkdZNk9tTmhZenBUYVdkdVlYUjFjbVVwUEM5a2N6cFlVR0YwYUQ0OEwyUnpPbFJ5WVc1elptOXliVDQ4WkhNNlZISmhibk5tYjNKdElFRnNaMjl5YVhSb2JUMGlhSFIwY0RvdkwzZDNkeTUzTXk1dmNtY3ZWRkl2TVRrNU9TOVNSVU10ZUhCaGRHZ3RNVGs1T1RFeE1UWWlQanhrY3pwWVVHRjBhRDV1YjNRb0x5OWhibU5sYzNSdmNpMXZjaTF6Wld4bU9qcGpZV002UVdSa2FYUnBiMjVoYkVSdlkzVnRaVzUwVW1WbVpYSmxibU5sVzJOaVl6cEpSRDBuVVZJblhTazhMMlJ6T2xoUVlYUm9Qand2WkhNNlZISmhibk5tYjNKdFBqeGtjenBVY21GdWMyWnZjbTBnUVd4bmIzSnBkR2h0UFNKb2RIUndPaTh2ZDNkM0xuY3pMbTl5Wnk4eU1EQTJMekV5TDNodGJDMWpNVFJ1TVRFaUx6NDhMMlJ6T2xSeVlXNXpabTl5YlhNK1BHUnpPa1JwWjJWemRFMWxkR2h2WkNCQmJHZHZjbWwwYUcwOUltaDBkSEE2THk5M2QzY3Vkek11YjNKbkx6SXdNREV2TURRdmVHMXNaVzVqSTNOb1lUSTFOaUl2UGp4a2N6cEVhV2RsYzNSV1lXeDFaVDV4ZEdGSlVsUndVM1J4VVhNd1UyRk9la0p5Tm14RlVtUjVSMlJ4WlVGbUswaEZjWEUxTUd0RWF6aDNQVHd2WkhNNlJHbG5aWE4wVm1Gc2RXVStQQzlrY3pwU1pXWmxjbVZ1WTJVK1BHUnpPbEpsWm1WeVpXNWpaU0JVZVhCbFBTSm9kSFJ3T2k4dmQzZDNMbmN6TG05eVp5OHlNREF3THpBNUwzaHRiR1J6YVdjalUybG5ibUYwZFhKbFVISnZjR1Z5ZEdsbGN5SWdWVkpKUFNJamVHRmtaWE5UYVdkdVpXUlFjbTl3WlhKMGFXVnpJajQ4WkhNNlJHbG5aWE4wVFdWMGFHOWtJRUZzWjI5eWFYUm9iVDBpYUhSMGNEb3ZMM2QzZHk1M015NXZjbWN2TWpBd01TOHdOQzk0Yld4bGJtTWpjMmhoTWpVMklpOCtQR1J6T2tScFoyVnpkRlpoYkhWbFBsbFhSVFJPVkZVeFRrZFZNMWxVV21sWmVtUnBUbFJPYWs1SFVURk9SMFpyVDFSV2FFMUVhR3RaVkZacVRtcEZOVmxxYUcxWmFrWnRUMFJvYkZsVVl6QmFSRTB4VGpKVk5VMXRWbXhaZW1jd1RucFdiRmwzUFQwOEwyUnpPa1JwWjJWemRGWmhiSFZsUGp3dlpITTZVbVZtWlhKbGJtTmxQand2WkhNNlUybG5ibVZrU1c1bWJ6NDhaSE02VTJsbmJtRjBkWEpsVm1Gc2RXVStUVVZWUTBsUlJHUkRUR1JUT0U1eVVFTk5WbmRaU0VKbWJWTjViRGxIZW1kelVGQkdSVEpFZFV4YU4yNWhNamxETmxGSloxTjZjbWhFTmk5ekwwbFlka1JyVDJkUVVqaGpWRTEwU0dseVJFVXdWWGRoZW5aQ1JXVlFOMVpoUlRBOVBDOWtjenBUYVdkdVlYUjFjbVZXWVd4MVpUNDhaSE02UzJWNVNXNW1iejQ4WkhNNldEVXdPVVJoZEdFK1BHUnpPbGcxTURsRFpYSjBhV1pwWTJGMFpUNU5TVWxEWW1wRFEwRm9VMmRCZDBsQ1FXZEpWVlU0UkRkR2RHb3pWVTF1ZG1sWVIyZGljMFpqU1VKbmFGVlpkM2REWjFsSlMyOWFTWHBxTUVWQmQwbDNaMWt3ZUVONlFVcENaMDVXUWtGWlZFRnJjRkJOVVRSM1JFRlpSRlpSVVVsRVFWWkNZbGN4YUdKcVJVOU5RWGRIUVRGVlJVSjNkMFpSVnpGMFdWYzBlRVJVUVV4Q1owNVdRa0Z2VFVKRmJGUldSVkY0UkZSQlRFSm5UbFpDUVhOTlFrVldWVkZXWjNoSFZFRllRbWRPVmtKQlRVMUZSMVl3V1ZobmRXRllUakJhUXpWdVlqTlpkV0Z0T0hoS1ZFRnFRbWRyY1docmFVYzVkekJDUTFGRlYwWnRSbXRpVjJ4MVVVZFdNRmxZWjNWaFdFNHdXa00xYm1JeldYVmhiVGgzU0doalRrMXFTWGRQVkVWNlRWUkpkMDU2U1RSWGFHTk9UV3BOZDA5VVJYcE5WRWwzVG5wSk5GZHFRMEpxVkVWTVRVRnJSMEV4VlVWQ2FFMURVMnM0ZUVScVFVMUNaMDVXUWtGblRVSlZSblJpVjBaMVRWRTBkMFJCV1VSV1VWRklSRUZXUW1KWE1XaGlha1ZPVFVGelIwRXhWVVZEWjNkRlUxWk9WVkpFUlU1TlFYTkhRVEZWUlVOM2QwVlNWbEpDVjBSRldrMUNZMGRCTVZWRlFYZDNVVnBZVW1obFF6VndZek5TYTB4dFpIWmthVFZ4WW5wRmJFMURUVWREVTNGSFUwbGlNMFJSUlVwQlVsbFhXVmRTZEdGWE5VRmFXRkpvWlVNMWNHTXpVbXRNYldSMlpHazFjV0o2UWxkTlFrRkhRbmx4UjFOTk5EbEJaMFZIUWxOMVFrSkJRVXRCTUVsQlFrbExlRzlMY0VGNGJqSmtkeXRVY2l0Q1dUSlVVeTl1TVdnemJITkZVRFpvYm1oaWJuZ3dkMnA2TTJ0b1prSmlTRlpOVTBSSFZrc3JXak55TUc5UFRIWnJlRWhEU1hoNVZVUnhWVEoyWTBFeVUyUkpTRWdyYWxWNlFsSk5RakJIUVRGVlpFUm5VVmRDUWxJNE5GQk5kVlpqTVdGS1IzcEVNSFJpZHpKbEwwd3pkR1prUm5wQlprSm5UbFpJVTAxRlIwUkJWMmRDVWpnMFVFMTFWbU14WVVwSGVrUXdkR0ozTW1VdlRETjBabVJHZWtGUVFtZE9Wa2hTVFVKQlpqaEZRbFJCUkVGUlNDOU5RVzlIUTBOeFIxTk5ORGxDUVUxRFFUQm5RVTFGVlVOSlVVUjFRWGx6VTFsU1pIcGpiMWR2TXpGc2VYcFVXVWRQUjBWck1HZ3ZSV3g2YkVkR01uRmtNMDAxWTBoQlNXZFlMMkYzZVdOd1R6SkdNa2RMV1RWeFFtTm5kMDlzYm1OUGIyOUZUVEZIYUU4NWRHdEJMMGR4VDBGUlBUd3ZaSE02V0RVd09VTmxjblJwWm1sallYUmxQand2WkhNNldEVXdPVVJoZEdFK1BDOWtjenBMWlhsSmJtWnZQanhrY3pwUFltcGxZM1ErUEhoaFpHVnpPbEYxWVd4cFpubHBibWRRY205d1pYSjBhV1Z6SUhodGJHNXpPbmhoWkdWelBTSm9kSFJ3T2k4dmRYSnBMbVYwYzJrdWIzSm5MekF4T1RBekwzWXhMak11TWlNaUlGUmhjbWRsZEQwaWMybG5ibUYwZFhKbElqNDhlR0ZrWlhNNlUybG5ibVZrVUhKdmNHVnlkR2xsY3lCSlpEMGllR0ZrWlhOVGFXZHVaV1JRY205d1pYSjBhV1Z6SWo0OGVHRmtaWE02VTJsbmJtVmtVMmxuYm1GMGRYSmxVSEp2Y0dWeWRHbGxjejQ4ZUdGa1pYTTZVMmxuYm1sdVoxUnBiV1UrTWpBeU5TMHdOUzB4TjFReU1Ub3pORG8wTWxvOEwzaGhaR1Z6T2xOcFoyNXBibWRVYVcxbFBqeDRZV1JsY3pwVGFXZHVhVzVuUTJWeWRHbG1hV05oZEdVK1BIaGhaR1Z6T2tObGNuUStQSGhoWkdWek9rTmxjblJFYVdkbGMzUStQR1J6T2tScFoyVnpkRTFsZEdodlpDQkJiR2R2Y21sMGFHMDlJbWgwZEhBNkx5OTNkM2N1ZHpNdWIzSm5Mekl3TURFdk1EUXZlRzFzWlc1akkzTm9ZVEkxTmlJdlBqeGtjenBFYVdkbGMzUldZV3gxWlQ1T2FrNXFUakpHYTFsNlFtcFphbXhxVDBkTk5FNVVWWGROUjFFMFdrZFZkMDFFVVRGUFJGbDVUa1JOZWsxdFVYbE5SMWwzVDBSQ2FFNXFSVEJhVjBacFdrUnJNbHBIVlROT1ZFbDVXVmRGTVZwcWEzaGFRVDA5UEM5a2N6cEVhV2RsYzNSV1lXeDFaVDQ4TDNoaFpHVnpPa05sY25SRWFXZGxjM1ErUEhoaFpHVnpPa2x6YzNWbGNsTmxjbWxoYkQ0OFpITTZXRFV3T1VsemMzVmxjazVoYldVK1JVMUJTVXhCUkVSU1JWTlRQV0ZrYldsdVFHVjBZWGd1YVhOMFpDNW5iM1l1YW04c0lFTk9QV1YwWVhndWFYTjBaQzVuYjNZdWFtOHNJRTlWUFVWVVFWZ3NJRTg5U1ZOVVJDd2dURDFCYlcxaGJpd2dVMVE5UVcxdFlXNHNJRU05U2s4OEwyUnpPbGcxTURsSmMzTjFaWEpPWVcxbFBqeGtjenBZTlRBNVUyVnlhV0ZzVG5WdFltVnlQalEzT0RFME9UZzFNREF4TkRnM05EZzROekkwTWpRME1qSTJNemd3TWpJNU16VTFNalUzTkRneE1ETTNPRFl6Tmp3dlpITTZXRFV3T1ZObGNtbGhiRTUxYldKbGNqNDhMM2hoWkdWek9rbHpjM1ZsY2xObGNtbGhiRDQ4TDNoaFpHVnpPa05sY25RK1BDOTRZV1JsY3pwVGFXZHVhVzVuUTJWeWRHbG1hV05oZEdVK1BDOTRZV1JsY3pwVGFXZHVaV1JUYVdkdVlYUjFjbVZRY205d1pYSjBhV1Z6UGp3dmVHRmtaWE02VTJsbmJtVmtVSEp2Y0dWeWRHbGxjejQ4TDNoaFpHVnpPbEYxWVd4cFpubHBibWRRY205d1pYSjBhV1Z6UGp3dlpITTZUMkpxWldOMFBqd3ZaSE02VTJsbmJtRjBkWEpsUGp3dmMyRmpPbE5wWjI1aGRIVnlaVWx1Wm05eWJXRjBhVzl1UGp3dmMybG5PbFZDVEVSdlkzVnRaVzUwVTJsbmJtRjBkWEpsY3o0OEwyVjRkRHBGZUhSbGJuTnBiMjVEYjI1MFpXNTBQand2WlhoME9sVkNURVY0ZEdWdWMybHZiajQ4TDJWNGREcFZRa3hGZUhSbGJuTnBiMjV6UGp4alltTTZVSEp2Wm1sc1pVbEVQbkpsY0c5eWRHbHVaem94TGpBOEwyTmlZenBRY205bWFXeGxTVVErUEdOaVl6cEpSRDVKVGxZd01EQXdNVE04TDJOaVl6cEpSRDQ4WTJKak9sVlZTVVErTmpSbE9UY3lZV0l0TkRrek15MDBORFZpTFRjeVlqVXRNRGhrWkRneVptSmlNREZpUEM5alltTTZWVlZKUkQ0OFkySmpPa2x6YzNWbFJHRjBaVDR5TURJMUxUQTBMVEkwUEM5alltTTZTWE56ZFdWRVlYUmxQanhqWW1NNlNXNTJiMmxqWlZSNWNHVkRiMlJsSUc1aGJXVTlJakF4TVNJK016ZzRQQzlqWW1NNlNXNTJiMmxqWlZSNWNHVkRiMlJsUGp4alltTTZUbTkwWlQ3Wmh0bUMySy9aaWp3dlkySmpPazV2ZEdVK1BHTmlZenBFYjJOMWJXVnVkRU4xY25KbGJtTjVRMjlrWlQ1S1QwUThMMk5pWXpwRWIyTjFiV1Z1ZEVOMWNuSmxibU41UTI5a1pUNDhZMkpqT2xSaGVFTjFjbkpsYm1ONVEyOWtaVDVLVDBROEwyTmlZenBVWVhoRGRYSnlaVzVqZVVOdlpHVStQR05oWXpwQlpHUnBkR2x2Ym1Gc1JHOWpkVzFsYm5SU1pXWmxjbVZ1WTJVK1BHTmlZenBKUkQ1SlExWThMMk5pWXpwSlJENDhZMkpqT2xWVlNVUStNVHd2WTJKak9sVlZTVVErUEM5allXTTZRV1JrYVhScGIyNWhiRVJ2WTNWdFpXNTBVbVZtWlhKbGJtTmxQanhqWVdNNlFXUmthWFJwYjI1aGJFUnZZM1Z0Wlc1MFVtVm1aWEpsYm1ObFBqeGpZbU02U1VRK1VWSThMMk5pWXpwSlJENDhZMkZqT2tGMGRHRmphRzFsYm5RK1BHTmlZenBGYldKbFpHUmxaRVJ2WTNWdFpXNTBRbWx1WVhKNVQySnFaV04wSUcxcGJXVkRiMlJsUFNKMFpYaDBMM0JzWVdsdUlqNUJVVUZEUVc1ME9VRjNWbTFaVjNoNldsRlJTRTFUTkhkTlJFRjNUVUZWU2xOVk5WZE5SRUYzVFVSRmVrSm5RVWhEYWtsM1RXcFZkRTFFVVhSTmFsRkpRMFJGZDA1cVJYaE9hbEUxUTFObVdtaGthWE15U3pobk1reHVXbWxPYVRKSlRtMUdNa3N6V21oa2FYWkpUbWx1TWxsVVdYSk9iVXN5VEV4WmNEbHRTVEpaYjB0WlJURkdWMVZPU2xWVlRrTmhNa1pHVWtWR1NWbHJWVFZoV0ZaVFpWUk9ZVlpGTVcxVmFscGFWbGRTTlV3eU5VMWpWR1EwVGpGS2Vsb3pZekJYVkdSclZEQmFRbE5YYUVKVWEzUTJWVWhhYkdKcVFtOWlSMFpGWWtkb1NHUkljRmxaYWs1WVZFaEJlRlpHUmpaaFJXeHlaRWhTUW1ORE9WaFZNMVpEVGxSTk1HUkJQVDA4TDJOaVl6cEZiV0psWkdSbFpFUnZZM1Z0Wlc1MFFtbHVZWEo1VDJKcVpXTjBQand2WTJGak9rRjBkR0ZqYUcxbGJuUStQQzlqWVdNNlFXUmthWFJwYjI1aGJFUnZZM1Z0Wlc1MFVtVm1aWEpsYm1ObFBqeGpZV002VTJsbmJtRjBkWEpsUGp4alltTTZTVVErZFhKdU9tOWhjMmx6T201aGJXVnpPbk53WldOcFptbGpZWFJwYjI0NmRXSnNPbk5wWjI1aGRIVnlaVHBKYm5admFXTmxQQzlqWW1NNlNVUStQR05pWXpwVGFXZHVZWFIxY21WTlpYUm9iMlErZFhKdU9tOWhjMmx6T201aGJXVnpPbk53WldOcFptbGpZWFJwYjI0NmRXSnNPbVJ6YVdjNlpXNTJaV3h2Y0dWa09uaGhaR1Z6UEM5alltTTZVMmxuYm1GMGRYSmxUV1YwYUc5a1Bqd3ZZMkZqT2xOcFoyNWhkSFZ5WlQ0OFkyRmpPa0ZqWTI5MWJuUnBibWRUZFhCd2JHbGxjbEJoY25SNVBqeGpZV002VUdGeWRIaytQR05oWXpwUWIzTjBZV3hCWkdSeVpYTnpQanhqWVdNNlEyOTFiblJ5ZVQ0OFkySmpPa2xrWlc1MGFXWnBZMkYwYVc5dVEyOWtaVDVLVHp3dlkySmpPa2xrWlc1MGFXWnBZMkYwYVc5dVEyOWtaVDQ4TDJOaFl6cERiM1Z1ZEhKNVBqd3ZZMkZqT2xCdmMzUmhiRUZrWkhKbGMzTStQR05oWXpwUVlYSjBlVlJoZUZOamFHVnRaVDQ4WTJKak9rTnZiWEJoYm5sSlJENHhNRFl4TVRZME9Ud3ZZMkpqT2tOdmJYQmhibmxKUkQ0OFkyRmpPbFJoZUZOamFHVnRaVDQ4WTJKak9rbEVQbFpCVkR3dlkySmpPa2xFUGp3dlkyRmpPbFJoZUZOamFHVnRaVDQ4TDJOaFl6cFFZWEowZVZSaGVGTmphR1Z0WlQ0OFkyRmpPbEJoY25SNVRHVm5ZV3hGYm5ScGRIaytQR05pWXpwU1pXZHBjM1J5WVhScGIyNU9ZVzFsUHRtRjJLellyeURZdWRtSTJMWWcyWVhZcmRtRjJLOGcyS2ZaaE5pczJZcllzdGluMllqWmlqd3ZZMkpqT2xKbFoybHpkSEpoZEdsdmJrNWhiV1UrUEM5allXTTZVR0Z5ZEhsTVpXZGhiRVZ1ZEdsMGVUNDhMMk5oWXpwUVlYSjBlVDQ4TDJOaFl6cEJZMk52ZFc1MGFXNW5VM1Z3Y0d4cFpYSlFZWEowZVQ0OFkyRmpPa0ZqWTI5MWJuUnBibWREZFhOMGIyMWxjbEJoY25SNVBqeGpZV002VUdGeWRIaytQR05oWXpwUVlYSjBlVWxrWlc1MGFXWnBZMkYwYVc5dVBqeGpZbU02U1VRZ2MyTm9aVzFsU1VROUlpSXZQand2WTJGak9sQmhjblI1U1dSbGJuUnBabWxqWVhScGIyNCtQR05oWXpwUWIzTjBZV3hCWkdSeVpYTnpQanhqWW1NNlVHOXpkR0ZzV205dVpUNHhNVEU0TVR3dlkySmpPbEJ2YzNSaGJGcHZibVUrUEdOaVl6cERiM1Z1ZEhKNVUzVmlaVzUwYVhSNVEyOWtaVDVCVFR3dlkySmpPa052ZFc1MGNubFRkV0psYm5ScGRIbERiMlJsUGp4allXTTZRMjkxYm5SeWVUNDhZMkpqT2tsa1pXNTBhV1pwWTJGMGFXOXVRMjlrWlQ1S1R6d3ZZMkpqT2tsa1pXNTBhV1pwWTJGMGFXOXVRMjlrWlQ0OEwyTmhZenBEYjNWdWRISjVQand2WTJGak9sQnZjM1JoYkVGa1pISmxjM00rUEdOaFl6cFFZWEowZVZSaGVGTmphR1Z0WlQ0OFkyRmpPbFJoZUZOamFHVnRaVDQ4WTJKak9rbEVQbFpCVkR3dlkySmpPa2xFUGp3dlkyRmpPbFJoZUZOamFHVnRaVDQ4TDJOaFl6cFFZWEowZVZSaGVGTmphR1Z0WlQ0OFkyRmpPbEJoY25SNVRHVm5ZV3hGYm5ScGRIaytQR05pWXpwU1pXZHBjM1J5WVhScGIyNU9ZVzFsUHRpMDJMSFpnOWlwSU5pbjJZVFl0TmluMkxmWXBpRFlwOW1FMkxEWmg5aW8yWW9nMllUWmhOaXEySy9Zc2RtSzJLZ2cyWWpZcDltRTJLcll0OW1JMllyWXNTRFppTmluMksvWXA5aXgyS2tnMktmWmhObUIyWWJZcDlpdjJZSWcyS2ZaaE5pejJZcllwOWl0MllyWXFUd3ZZMkpqT2xKbFoybHpkSEpoZEdsdmJrNWhiV1UrUEM5allXTTZVR0Z5ZEhsTVpXZGhiRVZ1ZEdsMGVUNDhMMk5oWXpwUVlYSjBlVDQ4WTJGak9rRmpZMjkxYm5ScGJtZERiMjUwWVdOMFBqeGpZbU02VkdWc1pYQm9iMjVsUGpBd09UWXlOemt5T1RnMk9EZzJQQzlqWW1NNlZHVnNaWEJvYjI1bFBqd3ZZMkZqT2tGalkyOTFiblJwYm1kRGIyNTBZV04wUGp3dlkyRmpPa0ZqWTI5MWJuUnBibWREZFhOMGIyMWxjbEJoY25SNVBqeGpZV002VTJWc2JHVnlVM1Z3Y0d4cFpYSlFZWEowZVQ0OFkyRmpPbEJoY25SNVBqeGpZV002VUdGeWRIbEpaR1Z1ZEdsbWFXTmhkR2x2Ymo0OFkySmpPa2xFUGpFME9EQTNNemswUEM5alltTTZTVVErUEM5allXTTZVR0Z5ZEhsSlpHVnVkR2xtYVdOaGRHbHZiajQ4TDJOaFl6cFFZWEowZVQ0OEwyTmhZenBUWld4c1pYSlRkWEJ3YkdsbGNsQmhjblI1UGp4allXTTZUR1ZuWVd4TmIyNWxkR0Z5ZVZSdmRHRnNQanhqWW1NNlZHRjRSWGhqYkhWemFYWmxRVzF2ZFc1MElHTjFjbkpsYm1ONVNVUTlJa3BQSWo0eExqQXdNREF3UEM5alltTTZWR0Y0UlhoamJIVnphWFpsUVcxdmRXNTBQanhqWW1NNlZHRjRTVzVqYkhWemFYWmxRVzF2ZFc1MElHTjFjbkpsYm1ONVNVUTlJa3BQSWo0eExqQXdNREF3UEM5alltTTZWR0Y0U1c1amJIVnphWFpsUVcxdmRXNTBQanhqWW1NNlFXeHNiM2RoYm1ObFZHOTBZV3hCYlc5MWJuUWdZM1Z5Y21WdVkzbEpSRDBpU2s4aVBqQXVNREF3TURBOEwyTmlZenBCYkd4dmQyRnVZMlZVYjNSaGJFRnRiM1Z1ZEQ0OFkySmpPbEJoZVdGaWJHVkJiVzkxYm5RZ1kzVnljbVZ1WTNsSlJEMGlTazhpUGpFdU1EQXdNREE4TDJOaVl6cFFZWGxoWW14bFFXMXZkVzUwUGp3dlkyRmpPa3hsWjJGc1RXOXVaWFJoY25sVWIzUmhiRDQ4WTJGak9rbHVkbTlwWTJWTWFXNWxQanhqWW1NNlNVUStNVHd2WTJKak9rbEVQanhqWW1NNlNXNTJiMmxqWldSUmRXRnVkR2wwZVNCMWJtbDBRMjlrWlQwaVVFTkZJajR5TGpBd01EQXdQQzlqWW1NNlNXNTJiMmxqWldSUmRXRnVkR2wwZVQ0OFkySmpPa3hwYm1WRmVIUmxibk5wYjI1QmJXOTFiblFnWTNWeWNtVnVZM2xKUkQwaVNrOGlQakV1TURBd01EQThMMk5pWXpwTWFXNWxSWGgwWlc1emFXOXVRVzF2ZFc1MFBqeGpZV002U1hSbGJUNDhZMkpqT2s1aGJXVSsyWVhaaU5peUlOaW8yWVRZcjltS1BDOWpZbU02VG1GdFpUNDhMMk5oWXpwSmRHVnRQanhqWVdNNlVISnBZMlUrUEdOaVl6cFFjbWxqWlVGdGIzVnVkQ0JqZFhKeVpXNWplVWxFUFNKS1R5SStNQzQxTURBd01Ed3ZZMkpqT2xCeWFXTmxRVzF2ZFc1MFBqeGpZV002UVd4c2IzZGhibU5sUTJoaGNtZGxQanhqWW1NNlEyaGhjbWRsU1c1a2FXTmhkRzl5UG1aaGJITmxQQzlqWW1NNlEyaGhjbWRsU1c1a2FXTmhkRzl5UGp4alltTTZRV3hzYjNkaGJtTmxRMmhoY21kbFVtVmhjMjl1UGtSSlUwTlBWVTVVUEM5alltTTZRV3hzYjNkaGJtTmxRMmhoY21kbFVtVmhjMjl1UGp4alltTTZRVzF2ZFc1MElHTjFjbkpsYm1ONVNVUTlJa3BQSWo0d0xqQXdNREF3UEM5alltTTZRVzF2ZFc1MFBqd3ZZMkZqT2tGc2JHOTNZVzVqWlVOb1lYSm5aVDQ4TDJOaFl6cFFjbWxqWlQ0OEwyTmhZenBKYm5admFXTmxUR2x1WlQ0OEwwbHVkbTlwWTJVKw=="",""EINV_QR"":""AQACAnt9AwVmYWxzZQQHMS4wMDAwMAUJSU5WMDAwMDEzBgAHCjIwMjUtMDQtMjQICDEwNjExNjQ5CSfZhdis2K8g2LnZiNi2INmF2K3ZhdivINin2YTYrNmK2LLYp9mI2YoKYE1FWUNJUUNCa2FFREFIYkU5aXVSeTNaVE1mUjZZVWR5L25McTd4N1JzZ3c0WTdkT0ZBSWhBTkt6UHZlbjBobGFEbGhHdHpYYjNXTHAxVFF6aElrdHRBcC9XU3VCNTM0dA=="",""EINV_NUM"":""INV000013"",""EINV_INV_UUID"":""64E972ab-4933-445b-72b5-08Dd82fbb01b""}"
    End Function
    Private Function SAVEDATA(LOACLID As Integer, STATUS As String, signedInvoice As String, qrCode As String, invoiceNumber As String, invoiceUUID As String)
        Dim connectionString As String = "Data Source=E_INVOIC.db;Version=3;"
        Using conn As New SQLiteConnection(connectionString)
            conn.Open()

            ' إنشاء جدول إذا لم يكن موجودًا
            Dim createTableQuery As String = "
                CREATE TABLE IF NOT EXISTS e_Invoices (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    LOCALID INTEGER,
                    Status TEXT,
EinvStatus text,
                    SignedInvoice TEXT,
                    QRCode TEXT,
                    InvoiceNumber TEXT,
                    InvoiceUUID TEXT 
                     
                )"
            Using cmd As New SQLiteCommand(createTableQuery, conn)
                cmd.ExecuteNonQuery()
            End Using

            ' إدخال البيانات
            Dim insertQuery As String = "
                INSERT INTO e_Invoices (
                  LOCALID,  Status, EinvStatus, SignedInvoice, QRCode, InvoiceNumber,
                    InvoiceUUID
                ) VALUES (
                  @LOCALID,  @Status, @EinvStatus, @SignedInvoice, @QRCode, @InvoiceNumber,
                    @InvoiceUUID
                )"
            Debug.WriteLine("@LOCALID" & LOACLID)
            Debug.WriteLine("@Status" & STATUS)
            Debug.WriteLine("@QRCode" & qrCode)
            Debug.WriteLine("@SignedInvoice" & signedInvoice)
            Debug.WriteLine("@InvoiceNumber" & invoiceNumber)
            Debug.WriteLine("@invoiceUUID" & invoiceUUID)


            Using cmd As New SQLiteCommand(insertQuery, conn)
                cmd.Parameters.AddWithValue("@LOCALID", LOACLID)
                cmd.Parameters.AddWithValue("@Status", STATUS)
                cmd.Parameters.AddWithValue("@EinvStatus", STATUS)
                cmd.Parameters.AddWithValue("@SignedInvoice", signedInvoice)
                cmd.Parameters.AddWithValue("@QRCode", qrCode)
                cmd.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber)
                cmd.Parameters.AddWithValue("@InvoiceUUID", invoiceUUID)

                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Function
    Private Function ValidateXML(xmlString As String) As Boolean
        Dim settings As New XmlReaderSettings()
        settings.DtdProcessing = DtdProcessing.Ignore
        settings.ValidationType = ValidationType.Schema
        settings.ValidationFlags = XmlSchemaValidationFlags.ProcessInlineSchema Or
                                  XmlSchemaValidationFlags.ProcessSchemaLocation Or
                                  XmlSchemaValidationFlags.ReportValidationWarnings
        settings.XmlResolver = Nothing

        Dim schemaSet As New XmlSchemaSet()
        Dim schemaSettings As New XmlReaderSettings()
        schemaSettings.DtdProcessing = DtdProcessing.Ignore
        schemaSettings.XmlResolver = Nothing

        Try
            If File.Exists(SCHEMA_PATH) Then
                Using schemaReader As XmlReader = XmlReader.Create(SCHEMA_PATH, schemaSettings)
                    schemaSet.Add("urn:oasis:names:specification:ubl:schema:xsd:Invoice-2", schemaReader)
                End Using
            Else
                Throw New FileNotFoundException("UBL Invoice schema file not found: " & SCHEMA_PATH)
            End If
            schemaSet.Compile()
            settings.Schemas = schemaSet
        Catch ex As Exception
            Console.WriteLine("Schema loading error: " & ex.Message)
            Return False
        End Try

        Dim isValid As Boolean = True
        AddHandler settings.ValidationEventHandler,
            Sub(sender, e)
                If e.Severity = XmlSeverityType.Error Then
                    isValid = False
                    Console.WriteLine("Validation Error: " & e.Message)
                End If
            End Sub

        Try
            Dim cleanedXml As String = xmlString
            If xmlString.Contains("<!DOCTYPE") Then
                cleanedXml = Regex.Replace(xmlString, "<!DOCTYPE[^>]*?(?:\s*\[.*?\])?\s*>", "", RegexOptions.IgnoreCase)
            End If

            Using textReader As New StringReader(cleanedXml)
                Using reader As XmlReader = XmlReader.Create(textReader, settings)
                    While reader.Read()
                    End While
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("Validation Exception: " & ex.Message)
            isValid = False
        End Try

        Return isValid
    End Function

    Private Function CleanXml(xmlString As String) As String
        xmlString = Regex.Replace(xmlString, ">\s+<", "><")
        xmlString = xmlString.Replace(vbCr, "").Replace(vbLf, "").Replace(vbTab, "")
        xmlString = Regex.Replace(xmlString, "<!DOCTYPE[^>]*>", "")
        xmlString = Regex.Replace(xmlString, "<!--.*?-->", "", RegexOptions.Singleline)
        xmlString = Regex.Replace(xmlString, "\s+xmlns:", " xmlns:")
        Return xmlString.Trim()
    End Function

    Private Function ConvertToBase64(xmlString As String) As String
        xmlString = xmlString.Replace(vbNullChar, "").Trim()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(xmlString)
        Dim base64 = Convert.ToBase64String(bytes)
        Return base64.Replace(vbCr, "").Replace(vbLf, "")
    End Function

    Private Function ValidateBase64(base64String As String) As Boolean
        Try
            Dim bytes = Convert.FromBase64String(base64String)
            Dim decoded = Encoding.UTF8.GetString(bytes)
            File.WriteAllText("decoded_invoice.xml", decoded)
            Return decoded.Trim().StartsWith("<?xml")
        Catch
            Return False
        End Try
    End Function

    Private Function ProcessSuccessfulResponse(responseString As String) As String
        Try
            If String.IsNullOrWhiteSpace(responseString) OrElse Not responseString.Trim().StartsWith("{") Then
                Return $"الاستجابة غير متوقعة (غير JSON): {responseString}"
            End If

            Dim json = JObject.Parse(responseString)
            Dim status = json("status")?.ToString()
            Dim invoiceId = json("invoice_id")?.ToString()
            Dim qrCode = json("qr_code")?.ToString()

            If status = "success" AndAlso Not String.IsNullOrEmpty(invoiceId) Then
                Return $"SUCCESS: تم إرسال الفاتورة بنجاح. رقم الفاتورة: {invoiceId}"
            Else
                Return $"الاستجابة غير متوقعة: {responseString}"
            End If
        Catch ex As Exception
            Return $"فشل تحليل الرد: {ex.Message}, Response: {responseString}"
        End Try
    End Function

    Private Function ProcessErrorResponse(statusCode As HttpStatusCode, responseString As String) As String
        Dim errorMessage = $"خطأ في الخادم ({statusCode}): "
        Try
            If String.IsNullOrWhiteSpace(responseString) OrElse Not responseString.Trim().StartsWith("{") Then
                Return errorMessage & $"استجابة غير متوقعة (غير JSON): {responseString}"
            End If

            Dim json = JObject.Parse(responseString)
            Dim errors = json("EINV_RESULTS")?("ERRORS")
            If errors IsNot Nothing AndAlso errors.Any() Then
                For Each Erpr In errors
                    errorMessage += $"{Erpr("EINV_MESSAGE")?.ToString()}; "
                Next
            Else
                errorMessage += responseString
            End If
        Catch ex As Exception
            errorMessage += $"خطأ في تحليل الاستجابة: {ex.Message}, Response: {responseString}"
        End Try
        Return errorMessage
    End Function

    Private Sub SaveQRCode(qrCodeBase64 As String, invoiceId As String)
        Try
            Dim cleanBase64 = qrCodeBase64.Replace("data:image/png;base64,", "")
            Dim bytes = Convert.FromBase64String(cleanBase64)
            Dim path = IO.Path.Combine(QR_FOLDER, $"{invoiceId}.png")
            File.WriteAllBytes(path, bytes)
            Console.WriteLine($"تم حفظ QR Code في: {path}")
        Catch ex As Exception
            Console.WriteLine($"فشل حفظ QR Code: {ex.Message}")
        End Try
    End Sub

    Private Function LoadCertificate(path As String, password As String) As X509Certificate2
        Try
            Return New X509Certificate2(path, password, X509KeyStorageFlags.Exportable)
        Catch ex As Exception
            Console.WriteLine($"خطأ في تحميل الشهادة: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Public Function SignInvoice(xmlString As String, cert As X509Certificate2) As String
        Try
            Dim doc As New XmlDocument()
            doc.PreserveWhitespace = True
            Dim settings As New XmlReaderSettings()
            settings.DtdProcessing = DtdProcessing.Ignore
            settings.XmlResolver = Nothing

            Using reader As XmlReader = XmlReader.Create(New StringReader(xmlString), settings)
                doc.Load(reader)
            End Using

            Dim nsManager As New XmlNamespaceManager(doc.NameTable)
            nsManager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")
            nsManager.AddNamespace("sig", "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2")
            nsManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
            nsManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")

            Dim extensionsNode As XmlNode = doc.SelectSingleNode("//ext:UBLExtensions", nsManager)
            If extensionsNode Is Nothing Then
                Throw New Exception("Element UBLExtensions not found in the XML document")
            End If

            Dim signedXml As New SignedXml(doc)
            signedXml.SigningKey = cert.GetRSAPrivateKey()
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url

            Dim reference As New Reference("")
            reference.AddTransform(New XmlDsigEnvelopedSignatureTransform())
            reference.AddTransform(New XmlDsigExcC14NTransform())
            reference.DigestMethod = SignedXml.XmlDsigSHA256Url
            signedXml.AddReference(reference)

            Dim keyInfo As New KeyInfo()
            keyInfo.AddClause(New KeyInfoX509Data(cert))
            signedXml.KeyInfo = keyInfo
            signedXml.ComputeSignature()

            Dim extension As XmlElement = doc.CreateElement("ext", "UBLExtension", nsManager.LookupNamespace("ext"))
            Dim content As XmlElement = doc.CreateElement("ext", "ExtensionContent", nsManager.LookupNamespace("ext"))
            Dim sigNode As XmlNode = doc.ImportNode(signedXml.GetXml(), True)
            content.AppendChild(sigNode)
            extension.AppendChild(content)
            extensionsNode.AppendChild(extension)

            If String.IsNullOrEmpty(doc.DocumentElement.GetAttribute("xmlns:ds")) Then
                doc.DocumentElement.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#")
            End If

            Return doc.OuterXml
        Catch ex As Exception
            File.WriteAllText("signing_error.txt", $"Error: {ex.Message}{Environment.NewLine}Stack Trace: {ex.StackTrace}")
            Return Nothing
        End Try
    End Function
End Class

' DataClasses.vb - Data structures
Public Class Seller
    Public Property Id As String
    Public Property TaxId As String
    Public Property Name As String
    Public Property Address As String

    Public Sub New(id As String, taxId As String, name As String, address As String)
        Me.Id = id
        Me.TaxId = taxId
        Me.Name = name
        Me.Address = address
    End Sub
End Class

Public Class Buyer
    Public Property Id As String
    Public Property TaxId As String
    Public Property Name As String
    Public Property Address As String
    Public Property IdType As String

    Public Sub New(id As String, taxId As String, name As String, address As String, idType As String)
        Me.Id = id
        Me.TaxId = taxId
        Me.Name = name
        Me.Address = address
        Me.IdType = idType
    End Sub
End Class

Public Class Item
    Public Property ItemId As String
    Public Property Quantity As String
    Public Property Subtotal As String
    Public Property VatTax As String
    Public Property SpecialTax As String
    Public Property UnitPrice As String
    Public Property Discount As String
    Public Property Description As String
    Public Property Isic4 As String

    Public Sub New(itemId As String, quantity As String, subtotal As String, vatTax As String, specialTax As String, unitPrice As String, discount As String, description As String, isic4 As String)
        Me.ItemId = itemId
        Me.Quantity = quantity
        Me.Subtotal = subtotal
        Me.VatTax = vatTax
        Me.SpecialTax = specialTax
        Me.UnitPrice = unitPrice
        Me.Discount = discount
        Me.Description = description
        Me.Isic4 = isic4
    End Sub
End Class

Public Class InvoiceType
    Public Property Code As String
    Public Property Name As String

    Public Sub New(code As String, name As String)
        Me.Code = code
        Me.Name = name
    End Sub
End Class

' XmlBuilder.vb - Module for XML construction
Module XmlBuilder01
    Public Function BuildInvoiceXml(seller As Seller, buyer As Buyer, invoiceType As InvoiceType, items As Item()) As String
        Dim currentDate As String = DateTime.Now.ToString("yyyy-MM-dd")
        Dim currentTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim invoiceNumber As String = "INV-" & DateTime.Now.ToString("yyyyMMddHHmmss")
        Dim uuid As String = Guid.NewGuid().ToString()
        Dim xmlBuilder As New StringBuilder()

        Dim totals = CalculateTotals(items)

        xmlBuilder.AppendLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
        xmlBuilder.AppendLine("<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2""")
        xmlBuilder.AppendLine("         xmlns:cac=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""")
        xmlBuilder.AppendLine("         xmlns:cbc=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2""")
        xmlBuilder.AppendLine("         xmlns:ext=""urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"">")

        ' xmlBuilder.AppendLine("    <ext:UBLExtensions>")
        '  xmlBuilder.AppendLine("        <ext:UBLExtension>")
        '   xmlBuilder.AppendLine("            <ext:ExtensionContent>")
        '  xmlBuilder.AppendLine("            </ext:ExtensionContent>")
        '  xmlBuilder.AppendLine("        </ext:UBLExtension>")
        ' xmlBuilder.AppendLine("    </ext:UBLExtensions>")
        xmlBuilder.AppendLine("    <cbc:ProfileID>reporting:1.0</cbc:ProfileID>")
        xmlBuilder.AppendLine($"    <cbc:ID>{invoiceNumber}</cbc:ID>")
        xmlBuilder.AppendLine($"    <cbc:UUID>{uuid}</cbc:UUID>")
        xmlBuilder.AppendLine($"    <cbc:IssueDate>{currentDate}</cbc:IssueDate>")
        xmlBuilder.AppendLine($"    <cbc:InvoiceTypeCode name=""{invoiceType.Name}"">{invoiceType.Code}</cbc:InvoiceTypeCode>")
        xmlBuilder.AppendLine($"<cbc:Note>نقدي</cbc:Note>")
        xmlBuilder.AppendLine("    <cbc:DocumentCurrencyCode>JOD</cbc:DocumentCurrencyCode>")
        xmlBuilder.AppendLine("    <cbc:TaxCurrencyCode>JOD</cbc:TaxCurrencyCode>")
        xmlBuilder.AppendLine("    <cac:AdditionalDocumentReference>")
        xmlBuilder.AppendLine("        <cbc:ID>ICV</cbc:ID>")
        xmlBuilder.AppendLine($"        <cbc:UUID>1</cbc:UUID>")
        xmlBuilder.AppendLine("    </cac:AdditionalDocumentReference>")
        AppendSeller(xmlBuilder, seller)
        AppendBuyer(xmlBuilder, buyer)
        SellerSupplierParty(xmlBuilder, seller)
        AppendMonetaryTotal(xmlBuilder, totals)
        AppendInvoiceLines(xmlBuilder, items)
        xmlBuilder.AppendLine("</Invoice>")
        Return xmlBuilder.ToString()
    End Function

    Private Function CalculateTotals(items As Item()) As (LineExtension As Decimal, TaxAmount As Decimal, TaxableAmount As Decimal, Discount As Decimal, Payable As Decimal)
        Dim lineExtension As Decimal = 0
        Dim taxAmount As Decimal = 0
        Dim taxableAmount As Decimal = 0
        Dim discount As Decimal = 0
        Dim payable As Decimal = 0

        For Each item In items
            Try
                lineExtension += Decimal.Parse(item.Subtotal)
                taxAmount += Decimal.Parse(item.VatTax) + Decimal.Parse(item.SpecialTax)
                taxableAmount += Decimal.Parse(item.Subtotal)
                discount += Decimal.Parse(item.Discount)
                payable += Decimal.Parse(item.Subtotal) + Decimal.Parse(item.VatTax) + Decimal.Parse(item.SpecialTax)
            Catch ex As FormatException
                Throw New ArgumentException($"Invalid numeric format in item {item.ItemId}")
            End Try
        Next

        Return (lineExtension, taxAmount, taxableAmount, discount, payable)
    End Function

    Private Sub AppendSeller(xmlBuilder As StringBuilder, seller As Seller)
        Dim addressParts = seller.Address.Split(","c).Select(Function(s) s.Trim()).ToArray()
        Dim street = If(addressParts.Length > 0, addressParts(0), "")
        Dim city = If(addressParts.Length > 1, addressParts(1), "")
        Dim postal = If(addressParts.Length > 2, addressParts(2), "")
        Dim country = If(addressParts.Length > 3, addressParts(3), "JO")

        xmlBuilder.AppendLine("    <cac:AccountingSupplierParty>")
        xmlBuilder.AppendLine("        <cac:Party>")
        xmlBuilder.AppendLine("            <cac:PostalAddress>")
        xmlBuilder.AppendLine("<cac:country>")
        xmlBuilder.AppendLine("<cbc:IdentificationCode> JO</cbc:IdentificationCode>")
        xmlBuilder.AppendLine(" </cac:country>")
        '  xmlBuilder.AppendLine($"                <cbc:StreetName>{street}</cbc:StreetName>")
        '  If Not String.IsNullOrEmpty(city) Then
        ' xmlBuilder.AppendLine($"                <cbc:CityName>{city}</cbc:CityName>")
        ' End If
        If Not String.IsNullOrEmpty(postal) Then
            xmlBuilder.AppendLine($"                <cbc:PostalZone>{postal}</cbc:PostalZone>")
        End If
        ' xmlBuilder.AppendLine("                <cac:Country>")
        ' xmlBuilder.AppendLine($"                    <cbc:IdentificationCode>{country}</cbc:IdentificationCode>")
        ' xmlBuilder.AppendLine("                </cac:Country>")
        ' xmlBuilder.AppendLine("            </cac:PostalAddress>")
        xmlBuilder.AppendLine("            <cac:PartyTaxScheme>")
        xmlBuilder.AppendLine($"                <cbc:CompanyID>{seller.TaxId}</cbc:CompanyID>")
        xmlBuilder.AppendLine("                <cac:TaxScheme>")
        xmlBuilder.AppendLine("                    <cbc:ID>VAT</cbc:ID>")
        xmlBuilder.AppendLine("                </cac:TaxScheme>")
        xmlBuilder.AppendLine("            </cac:PartyTaxScheme>")
        xmlBuilder.AppendLine("            <cac:PartyLegalEntity>")
        xmlBuilder.AppendLine($"                <cbc:RegistrationName>{seller.Name}</cbc:RegistrationName>")
        xmlBuilder.AppendLine("            </cac:PartyLegalEntity>")
        xmlBuilder.AppendLine("        </cac:Party>")
        xmlBuilder.AppendLine("    </cac:AccountingSupplierParty>")
    End Sub
    Private Sub SellerSupplierParty(xmlBuilder As StringBuilder, seller As Seller)

        xmlBuilder.AppendLine("    <cac:SellerSupplierParty>")
        xmlBuilder.AppendLine("        <cac:Party>")
        xmlBuilder.AppendLine("            <cac:PartyIdentification>")
        xmlBuilder.AppendLine($"<  cbc:ID>{seller.Id}</cbc:ID>")
        xmlBuilder.AppendLine("</cac:PartyIdentification>")
        xmlBuilder.AppendLine(" </cac:Party>")

        xmlBuilder.AppendLine("    </cac:SellerSupplierParty>")
    End Sub
    Private Sub AppendBuyer(xmlBuilder As StringBuilder, buyer As Buyer)
        Dim addressParts = buyer.Address.Split(","c).Select(Function(s) s.Trim()).ToArray()
        Dim street = If(addressParts.Length > 0, addressParts(0), "")
        Dim country = If(addressParts.Length > 1, addressParts(1), "JO")

        xmlBuilder.AppendLine("    <cac:AccountingCustomerParty>")
        xmlBuilder.AppendLine("        <cac:Party>")
        xmlBuilder.AppendLine("            <cac:PartyIdentification>")
        xmlBuilder.AppendLine($"                <cbc:ID schemeID=""{buyer.IdType}"">{buyer.Id}</cbc:ID>")
        xmlBuilder.AppendLine("            </cac:PartyIdentification>")
        xmlBuilder.AppendLine("            <cac:PostalAddress>")
        xmlBuilder.AppendLine($"                <cbc:PostalZone>11181</cbc:PostalZone>")
        xmlBuilder.AppendLine($"                <cbc:CountrySubentityCode>AM</cbc:CountrySubentityCode>")
        xmlBuilder.AppendLine("                <cac:Country>")
        xmlBuilder.AppendLine($"                    <cbc:IdentificationCode>{country}</cbc:IdentificationCode>")
        xmlBuilder.AppendLine("                </cac:Country>")
        xmlBuilder.AppendLine("            </cac:PostalAddress>")
        xmlBuilder.AppendLine("            <cac:PartyTaxScheme>")
        'xmlBuilder.AppendLine($"                <cbc:CompanyID>{buyer.TaxId}</cbc:CompanyID>")
        xmlBuilder.AppendLine("                <cac:TaxScheme>")
        xmlBuilder.AppendLine("                    <cbc:ID>VAT</cbc:ID>")
        xmlBuilder.AppendLine("                </cac:TaxScheme>")
        xmlBuilder.AppendLine("            </cac:PartyTaxScheme>")
        xmlBuilder.AppendLine("            <cac:PartyLegalEntity>")
        xmlBuilder.AppendLine($"                <cbc:RegistrationName>{buyer.Name}</cbc:RegistrationName>")
        xmlBuilder.AppendLine("            </cac:PartyLegalEntity>")
        xmlBuilder.AppendLine("        </cac:Party>")
        xmlBuilder.AppendLine("<cac:AccountingContact>")
        xmlBuilder.AppendLine("<cbc:Telephone> 962792986886</cbc:Telephone>")
        xmlBuilder.AppendLine(" </cac:AccountingContact>")
        xmlBuilder.AppendLine("    </cac:AccountingCustomerParty>")
    End Sub

    Private Sub AppendTaxTotal(xmlBuilder As StringBuilder, totals As (LineExtension As Decimal, TaxAmount As Decimal, TaxableAmount As Decimal, Discount As Decimal, Payable As Decimal))
        xmlBuilder.AppendLine("    <cac:TaxTotal>")
        xmlBuilder.AppendLine($"        <cbc:TaxAmount currencyID=""JOD"">0.00</cbc:TaxAmount>") ' Total tax amount is 0
        ' VAT Tax Subtotal
        '  xmlBuilder.AppendLine("        <cac:TaxSubtotal>")
        '  xmlBuilder.AppendLine($"            <cbc:TaxAmount currencyID=""JOD"">{totals.LineExtension:F5}</cbc:TaxAmount>")
        '  xmlBuilder.AppendLine($"            <cbc:TaxAmount currencyID=""JOD"">0.00</cbc:TaxAmount>") ' VAT is 0
        ' xmlBuilder.AppendLine("            <cac:TaxCategory>")
        ' xmlBuilder.AppendLine("                <cbc:ID schemeID=""UN/ECE 5305"">O</cbc:ID>")
        ' xmlBuilder.AppendLine("                <cbc:Percent>0</cbc:Percent>")
        ' xmlBuilder.AppendLine("                <cac:TaxScheme>")
        ' xmlBuilder.AppendLine("                    <cbc:ID>VAT</cbc:ID>")
        ' xmlBuilder.AppendLine("                </cac:TaxScheme>")
        ' xmlBuilder.AppendLine("            </cac:TaxCategory>")
        'xmlBuilder.AppendLine("        </cac:TaxSubtotal>")
        ' Special Tax Subtotal
        ' xmlBuilder.AppendLine("        <cac:TaxSubtotal>")
        ' xmlBuilder.AppendLine($"            <cbc:TaxAmount currencyID=""JOD"">{totals.LineExtension:F5}</cbc:TaxAmount>")
        ' xmlBuilder.AppendLine($"            <cbc:RoundingAmount currencyID=""JOD"">0.00</cbc:RoundingAmount>") ' Special Tax is 0
        ' xmlBuilder.AppendLine("            <cac:TaxCategory>")
        '  xmlBuilder.AppendLine("                <cbc:ID schemeID=""UN/ECE 5305"">O</cbc:ID>")
        ' xmlBuilder.AppendLine("                <cbc:Percent>0</cbc:Percent>")
        ' xmlBuilder.AppendLine("                <cac:TaxScheme>")
        ' xmlBuilder.AppendLine("                    <cbc:ID>ST</cbc:ID>")
        'xmlBuilder.AppendLine("                </cac:TaxScheme>")
        ' xmlBuilder.AppendLine("            </cac:TaxCategory>")
        'xmlBuilder.AppendLine("        </cac:TaxSubtotal>")
        xmlBuilder.AppendLine("    </cac:TaxTotal>")
    End Sub

    Private Sub AppendMonetaryTotal(xmlBuilder As StringBuilder, totals As (LineExtension As Decimal, TaxAmount As Decimal, TaxableAmount As Decimal, Discount As Decimal, Payable As Decimal))
        xmlBuilder.AppendLine("    <cac:LegalMonetaryTotal>")
        'xmlBuilder.AppendLine($"        <cbc:LineExtensionAmount currencyID=""JOD"">{totals.LineExtension:F5}</cbc:LineExtensionAmount>")
        xmlBuilder.AppendLine($"        <cbc:TaxExclusiveAmount currencyID=""JOD"">{totals.LineExtension:F5}</cbc:TaxExclusiveAmount>")
        xmlBuilder.AppendLine($"        <cbc:TaxInclusiveAmount currencyID=""JOD"">{totals.LineExtension + totals.TaxAmount:F5}</cbc:TaxInclusiveAmount>")
        xmlBuilder.AppendLine($"        <cbc:AllowanceTotalAmount currencyID=""JOD"">{totals.Discount:F5}</cbc:AllowanceTotalAmount>")
        xmlBuilder.AppendLine($"        <cbc:PayableAmount currencyID=""JOD"">{totals.LineExtension + totals.TaxAmount - totals.Discount:F5}</cbc:PayableAmount>")
        xmlBuilder.AppendLine("    </cac:LegalMonetaryTotal>")
    End Sub

    Private Sub AppendInvoiceLines(xmlBuilder As StringBuilder, items As Item())
        For i As Integer = 0 To items.Length - 1
            Dim item As Item = items(i)
            xmlBuilder.AppendLine("    <cac:InvoiceLine>")
            xmlBuilder.AppendLine($"        <cbc:ID>{i + 1}</cbc:ID>")
            xmlBuilder.AppendLine($"        <cbc:InvoicedQuantity unitCode=""PCE"">{item.Quantity}</cbc:InvoicedQuantity>")
            xmlBuilder.AppendLine($"        <cbc:LineExtensionAmount currencyID=""JOD"">{item.Subtotal}</cbc:LineExtensionAmount>")
            xmlBuilder.AppendLine("        <cac:Item>")
            xmlBuilder.AppendLine($"            <cbc:Name>{item.Description}</cbc:Name>")
            xmlBuilder.AppendLine("        </cac:Item>")
            xmlBuilder.AppendLine("        <cac:Price>")
            xmlBuilder.AppendLine($"            <cbc:PriceAmount currencyID=""JOD"">{item.UnitPrice}</cbc:PriceAmount>")
            xmlBuilder.AppendLine("            <cac:AllowanceCharge>")
            xmlBuilder.AppendLine("                <cbc:ChargeIndicator>false</cbc:ChargeIndicator>")
            xmlBuilder.AppendLine("                <cbc:AllowanceChargeReason>DISCOUNT</cbc:AllowanceChargeReason>")
            xmlBuilder.AppendLine($"                <cbc:Amount currencyID=""JOD"">{item.Discount}</cbc:Amount>")
            xmlBuilder.AppendLine("            </cac:AllowanceCharge>")
            xmlBuilder.AppendLine("        </cac:Price>")
            xmlBuilder.AppendLine("    </cac:InvoiceLine>")


        Next
    End Sub
End Module