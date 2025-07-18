Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class Form2
    Private ReadOnly client As New HttpClient()
    Private Const API_URL As String = "https://backend.jofotara.gov.jo/core/invoices/"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TEST()
    End Sub
    Private Async Sub TEST()
        Dim result As New Dictionary(Of String, String) From {
            {"Response", ""},
            {"EINV_QR", ""},
            {"EINV_NUM", ""},
            {"EINV_INV_UUID", ""},
            {"EINV_SINGED_INVOICE", ""}
        }
        Dim invoiceXml As String = RichTextBox1.Text()
        Try
            invoiceXml = CleanXml(invoiceXml)
            Dim base64String As String = ConvertToBase64(invoiceXml)
            TextBox1.Text = base64String

            ' Create JSON payload
            Dim jsonData As New Dictionary(Of String, String) From {
            {"invoice", base64String}
        }
            Dim jsonSerializer As New JavaScriptSerializer()
            Dim jsonContent As String = jsonSerializer.Serialize(jsonData)


            ' Set up HTTP client
            client.DefaultRequestHeaders.Clear()
            client.DefaultRequestHeaders.Add("Client-Id", "804bb4f8-f745-4a38-b9dd-95c09eec6009")
            client.DefaultRequestHeaders.Add("Secret-Key", "Gj5nS9wyYHRadaVffz5VKB4v4wlVWyPhcJvrTD4NHtPk6ZAzKgbQdrVlgQOfzt0r+g4x9HtOiRWkLQJ4aqgmMTG2xL7rI0WJfvFDgzF1wD5uHcfdU0idPw7dZ2s+H4nzaOJzETIhZk9L3JGDan0O58+iiSKAyemIzGbmshoRmbmBPCukJrIZZHSQGXJJjjTv5rUFxpfT3dj7pFYk/ZoUNd6TFrXK1FsdbEQdHMAqYfELi3vGtK24J2gOhy+OJQ6mpTwHn10VoGKGSJLMNzk6vQ==")
            client.DefaultRequestHeaders.Add("Timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"))
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:138.0) Gecko/20100101 Firefox/138.0")
            client.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))

            ' Send POST request
            Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync(API_URL, content)
            Dim responseContent As String = Await response.Content.ReadAsStringAsync()
            Dim contentType As String = response.Content.Headers.ContentType?.ToString()
            Console.WriteLine(responseContent)
            ' Process response
            If response.IsSuccessStatusCode Then
                result("Response") = ProcessSuccessfulResponse(responseContent)
                If result("Response").StartsWith("SUCCESS") Then
                    Dim jsonResponse As JObject = JObject.Parse(responseContent)
                    result("EINV_QR") = jsonResponse("qr_code")?.ToString()
                    result("EINV_NUM") = jsonResponse("invoice_id")?.ToString()
                    result("EINV_INV_UUID") = jsonResponse("invoice_id")?.ToString()
                End If
            Else
                result("Response") = ProcessErrorResponse(response.StatusCode, responseContent)
            End If

            ' Save QR code if present
            If Not String.IsNullOrEmpty(result("EINV_QR")) Then
                ' SaveQrCode(result("EINV_QR"), result("EINV_NUM"))
            End If
        Catch ex As Exception
            result("Response") = $"Error: {ex.Message}"
        End Try

        TextBox1.Text = result("Response")
    End Sub
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
    Private Function ConvertToBase64(xmlString As String) As String
        xmlString = xmlString.Replace(vbNullChar, "").Trim()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(xmlString)
        Dim base64 = Convert.ToBase64String(bytes)
        Return base64.Replace(vbCr, "").Replace(vbLf, "")
    End Function
    Private Function CleanXml(xmlString As String) As String
        xmlString = Regex.Replace(xmlString, ">\s+<", "><")
        xmlString = xmlString.Replace(vbCr, "").Replace(vbLf, "").Replace(vbTab, "")
        xmlString = Regex.Replace(xmlString, "<!DOCTYPE[^>]*>", "")
        xmlString = Regex.Replace(xmlString, "<!--.*?-->", "", RegexOptions.Singleline)
        xmlString = Regex.Replace(xmlString, "\s+xmlns:", " xmlns:")
        Return xmlString.Trim()
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim invoiceXml As String = RichTextBox1.Text
        File.WriteAllText("invoice.xml", invoiceXml)
        Application.DoEvents()
        UBLToPDFConverter.Main()
    End Sub
End Class