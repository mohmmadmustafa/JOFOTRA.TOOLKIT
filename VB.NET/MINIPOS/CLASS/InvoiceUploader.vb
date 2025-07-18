Imports System.Net
Imports System.Net.Http
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Text
Imports System.Xml
Imports System.IO
Imports Newtonsoft.Json
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq
Imports System.Security.Cryptography

Public Class InvoiceUploader

    Public Async Function StartProcess() As Task
        Dim token = Await GetToken("10611649", "testuser1", "Mom-3655807")
        Debug.Write("TOKEN IS: " & token)
        If token IsNot Nothing Then
            Dim xmlPath = CreateInvoiceXml()
            Dim signedXmlPath = SignXml(xmlPath, "D:\k\myCertificate.pfx", "mom-3655807")
            Dim base64Xml = Convert.ToBase64String(File.ReadAllBytes(signedXmlPath))
            Await SendInvoice(token, base64Xml)
        End If
    End Function

    Private Async Function GetToken(taxNumber As String, username As String, password As String) As Task(Of String)
        Dim client As New HttpClient()
        Dim loginData = New With {
            .taxNumber = taxNumber,
            .username = username,
            .password = password,
            .rememberPassword = True
        }
        Dim json = JsonConvert.SerializeObject(loginData)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")
        Dim response As HttpResponseMessage = client.PostAsync("https://backend.jofotara.gov.jo/users/auth/login", content).Result
        Dim jsonResponse As String = response.Content.ReadAsStringAsync().Result

        If response.IsSuccessStatusCode Then
            Dim jsonObject As JObject = JObject.Parse(jsonResponse)
            Dim accessToken As String = jsonObject("access_token").ToString()
            Return accessToken
        Else
            MessageBox.Show("فشل تسجيل الدخول: " & response.StatusCode)
            Return Nothing
        End If
    End Function

    Private Function CreateInvoiceXml() As String
        Dim xml As String =
"<?xml version=""1.0"" encoding=""UTF-8""?>
<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2""
         xmlns:cac=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""
         xmlns:cbc=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"">
  <cbc:ID>INV-002</cbc:ID>
  <cbc:IssueDate>2025-05-13</cbc:IssueDate>
  <cbc:InvoiceTypeCode>388</cbc:InvoiceTypeCode> <!-- 388 = نقدية -->
  <cbc:DocumentCurrencyCode>JOD</cbc:DocumentCurrencyCode>
  
  <cac:AccountingSupplierParty>
    <cac:Party>
      <cbc:Name>شركتي</cbc:Name>
      <cac:PartyIdentification>
        <cbc:ID>10611649</cbc:ID>
      </cac:PartyIdentification>
    </cac:Party>
  </cac:AccountingSupplierParty>
  
  <cac:AccountingCustomerParty>
    <cac:Party>
      <cbc:Name>الزبون</cbc:Name>
      <cac:PartyIdentification>
        <cbc:ID>123456789</cbc:ID>
      </cac:PartyIdentification>
    </cac:Party>
  </cac:AccountingCustomerParty>

  <cac:InvoiceLine>
    <cbc:ID>1</cbc:ID>
    <cbc:InvoicedQuantity unitCode=""EA"">2</cbc:InvoicedQuantity>
    <cbc:LineExtensionAmount currencyID=""JOD"">1.00</cbc:LineExtensionAmount>
    <cac:PricingReference>
      <cac:AlternativeConditionPrice>
        <cbc:PriceAmount currencyID=""JOD"">0.5</cbc:PriceAmount>
        <cbc:PriceTypeCode>01</cbc:PriceTypeCode>
      </cac:AlternativeConditionPrice>
    </cac:PricingReference>
    <cac:AllowanceCharge>
      <cbc:ChargeIndicator>false</cbc:ChargeIndicator>
      <cbc:Amount currencyID=""JOD"">0.25</cbc:Amount>
    </cac:AllowanceCharge>
    <cac:Item>
      <cbc:Name>منتج تجريبي</cbc:Name>
    </cac:Item>
    <cac:Price>
      <cbc:PriceAmount currencyID=""JOD"">0.5</cbc:PriceAmount>
    </cac:Price>
  </cac:InvoiceLine>

  <cac:LegalMonetaryTotal>
    <cbc:LineExtensionAmount currencyID=""JOD"">1.00</cbc:LineExtensionAmount>
    <cbc:AllowanceTotalAmount currencyID=""JOD"">0.25</cbc:AllowanceTotalAmount>
    <cbc:PayableAmount currencyID=""JOD"">0.75</cbc:PayableAmount>
  </cac:LegalMonetaryTotal>
</Invoice>"

        Dim filePath = "invoice.xml"
        File.WriteAllText(filePath, xml, Encoding.UTF8)
        Return filePath
    End Function

    Private Function SignXml(xmlFile As String, certPath As String, certPassword As String) As String
        Try
            ' Load the XML document
            Dim xmlDoc As New XmlDocument()
            xmlDoc.PreserveWhitespace = True
            xmlDoc.Load(xmlFile)

            ' Load the certificate
            Dim cert As New X509Certificate2(certPath, certPassword, X509KeyStorageFlags.MachineKeySet Or X509KeyStorageFlags.PersistKeySet)
            If Not cert.HasPrivateKey Then
                Throw New Exception("Certificate does not contain a private key.")
            End If

            ' Create SignedXml object
            Dim signedXml As New SignedXml(xmlDoc)

            ' Explicitly set the signing key and algorithm (SHA-256)
            signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigCanonicalizationUrl

            ' Use the certificate's private key
            Dim rsa As RSA = cert.GetRSAPrivateKey()
            If rsa Is Nothing Then
                Throw New Exception("Failed to retrieve RSA private key from certificate.")
            End If
            signedXml.SigningKey = rsa

            ' Create a reference to the entire document
            Dim reference As New Reference()
            reference.Uri = ""
            reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256"

            ' Add an enveloped signature transform
            Dim env As New XmlDsigEnvelopedSignatureTransform()
            reference.AddTransform(env)

            ' Add the reference to SignedXml
            signedXml.AddReference(reference)

            ' Add certificate information to KeyInfo
            Dim keyInfo As New KeyInfo()
            keyInfo.AddClause(New KeyInfoX509Data(cert))
            signedXml.KeyInfo = keyInfo

            ' Compute the signature
            signedXml.ComputeSignature()

            ' Append the signature to the XML document
            Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))

            ' Save the signed XML
            Dim signedPath = "signed_invoice.xml"
            xmlDoc.Save(signedPath)
            Return signedPath

        Catch ex As Exception
            MessageBox.Show("Error signing XML: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Async Function SendInvoice(xmlContent As String, url As String) As Task(Of String)
        Try
            ' تحويل XML إلى Base64
            Dim xmlBytes As Byte() = Encoding.UTF8.GetBytes(xmlContent)
            Dim base64Xml As String = Convert.ToBase64String(xmlBytes)

            ' إعداد الطلب JSON بصيغة {"invoice": "Base64XML"}
            Dim requestJson As String = "{""invoice"": """ & base64Xml & """}"
            Debug.Write(requestJson)
            ' إنشاء الطلب
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Accept = "application/json"

            ' كتابة البيانات إلى جسم الطلب
            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(requestJson)
                streamWriter.Flush()
            End Using

            ' استقبال الرد
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()
                    Return responseText
                End Using
            End Using

        Catch ex As Exception
            Return "خطأ أثناء إرسال الفاتورة: " & ex.Message
        End Try
    End Function

End Class