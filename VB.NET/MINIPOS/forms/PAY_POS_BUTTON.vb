Imports System.Data.SQLite
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Xml
Imports System.Threading.Tasks
Imports System.IO

Public Class PAY_POS_BUTTON
    Inherits Form

    Private ReadOnly connectionString As String = "Data Source=E_INVOIC.db;Version=3;"
    Private ReadOnly xmlFilePath As String = Path.Combine(Application.StartupPath, "style", "TileButtons.xml")

    Public Sub New()
        ' Form setup
        Me.Text = "POS Product Tiles (SQLite)"
        Me.Size = New Size(800, 600)
        Me.AutoScroll = True
        Me.DoubleBuffered = True ' Reduce flicker for fast rendering

        ' Add FlowLayoutPanel for tiles
        Dim flowPanel As New FlowLayoutPanel()
        flowPanel.Dock = DockStyle.Fill
        flowPanel.AutoScroll = True
        flowPanel.Padding = New Padding(10)
        Me.Controls.Add(flowPanel)

        ' Load tiles asynchronously
        Task.Run(Sub() LoadTilesAsync(flowPanel))
    End Sub

    Private Async Sub LoadTilesAsync(flowPanel As FlowLayoutPanel)
        Try
            ' Ensure style directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(xmlFilePath))
            Debug.WriteLine("Style directory created: " & Path.GetDirectoryName(xmlFilePath))

            ' Generate XML from database
            Await GenerateTileButtonsXmlAsync()

            ' Load and render tiles
            Await Me.InvokeAsync(Sub() LoadTileButtons(flowPanel))
        Catch ex As Exception
            ' Await Me.InvokeAsync(Sub() MessageBox.Show("Error loading tiles: " & ex.Message, "Error"))
            Debug.WriteLine("LoadTilesAsync Error: " & ex.Message)
        End Try
    End Sub

    Private Async Function GenerateTileButtonsXmlAsync() As Task
        Try
            Me.Invoke(Sub() Debug.WriteLine("Connection String: " & connectionString))
            Using conn As New SQLiteConnection(connectionString)
                Await conn.OpenAsync()
                Me.Invoke(Sub() MessageBox.Show("Database Connected", "Debug"))

                Dim query As String = "SELECT ID, PRO_PRICE, PRO_NAME FROM PRODUCTS"
                Using cmd As New SQLiteCommand(query, conn)
                    Using reader As SQLiteDataReader = Await cmd.ExecuteReaderAsync()
                        Dim xmlDoc As New XmlDocument()
                        Dim root As XmlElement = xmlDoc.CreateElement("TileButtons")
                        xmlDoc.AppendChild(root)

                        Me.Invoke(Sub() MessageBox.Show("Reader Depth: " & reader.Depth, "Debug"))

                        If Not reader.HasRows Then
                            Me.Invoke(Sub() MessageBox.Show("No products found in database", "Warning"))
                            Debug.WriteLine("No products found in PRODUCTS table")
                            xmlDoc.Save(xmlFilePath)
                            Debug.WriteLine("Empty XML saved to: " & xmlFilePath)
                            Return
                        End If

                        Dim productCount As Integer = 0
                        While Await reader.ReadAsync()
                            ' Safely read values with type checking
                            Dim id As Integer
                            If Not reader.IsDBNull(0) AndAlso Integer.TryParse(reader.GetValue(0).ToString(), id) Then
                                ' Handle PRO_PRICE (numeric)
                                Dim price As Decimal
                                Dim priceStr As String = "Unknown"
                                If Not reader.IsDBNull(1) AndAlso Decimal.TryParse(reader.GetValue(1).ToString(), price) Then
                                    priceStr = price.ToString("C") ' Format as currency
                                End If

                                ' Handle PRO_NAME
                                Dim name As String = If(Not reader.IsDBNull(2), reader.GetString(2), "Unknown")

                                Dim tileNode As XmlElement = xmlDoc.CreateElement("TileButton")
                                tileNode.AppendChild(CreateXmlElement(xmlDoc, "ID", id.ToString()))
                                tileNode.AppendChild(CreateXmlElement(xmlDoc, "Title", name))
                                tileNode.AppendChild(CreateXmlElement(xmlDoc, "Caption", priceStr))
                                root.AppendChild(tileNode)

                                productCount += 1
                                Debug.WriteLine($"Added product: ID={id}, Name={name}, Price={priceStr}")
                            Else
                                Debug.WriteLine("Skipping record with invalid ID: " & reader.GetValue(0)?.ToString())
                            End If
                        End While

                        xmlDoc.Save(xmlFilePath)
                        Debug.WriteLine($"XML saved to: {xmlFilePath} with {productCount} products")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("GenerateTileButtonsXmlAsync Error: " & ex.Message)
            Throw
        End Try
    End Function

    Private Function CreateXmlElement(doc As XmlDocument, name As String, value As String) As XmlElement
        Dim element As XmlElement = doc.CreateElement(name)
        element.InnerText = If(value, "")
        Return element
    End Function

    Private Sub LoadTileButtons(flowPanel As FlowLayoutPanel)
        Try
            ' Suspend layout to improve rendering speed
            flowPanel.SuspendLayout()

            If Not File.Exists(xmlFilePath) Then
                MessageBox.Show("XML file not found: " & xmlFilePath, "Error")
                Debug.WriteLine("XML file missing: " & xmlFilePath)
                Return
            End If

            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlFilePath)
            Debug.WriteLine("XML loaded: " & xmlFilePath)

            Dim tileNodes = xmlDoc.SelectNodes("//TileButton")
            If tileNodes.Count = 0 Then
                MessageBox.Show("No tiles defined in XML", "Warning")
                Debug.WriteLine("No TileButton nodes found in XML")
                Return
            End If

            Dim tileCount As Integer = 0
            For Each tileNode As XmlNode In tileNodes
                ' Validate XML nodes
                Dim idNode As XmlNode = tileNode.SelectSingleNode("ID")
                Dim titleNode As XmlNode = tileNode.SelectSingleNode("Title")
                Dim captionNode As XmlNode = tileNode.SelectSingleNode("Caption")

                If idNode Is Nothing OrElse titleNode Is Nothing OrElse captionNode Is Nothing Then
                    Debug.WriteLine("Skipping tile with missing XML nodes")
                    Continue For
                End If

                Dim id As Integer
                If Not Integer.TryParse(idNode.InnerText, id) Then
                    Debug.WriteLine("Skipping tile with invalid ID: " & idNode.InnerText)
                    Continue For
                End If

                Dim title As String = titleNode.InnerText
                Dim caption As String = captionNode.InnerText

                ' Create tile button
                Dim tileButton As New Button()
                tileButton.Size = New Size(150, 200)
                tileButton.Margin = New Padding(10)
                tileButton.BackColor = Color.FromArgb(248, 249, 250) ' Bootstrap light gray
                tileButton.FlatStyle = FlatStyle.Flat
                tileButton.FlatAppearance.BorderSize = 1
                tileButton.FlatAppearance.BorderColor = Color.Gray
                tileButton.Text = $"{title}{vbCrLf}{caption}"
                tileButton.Font = New Font("Arial", 10, FontStyle.Bold)
                tileButton.TextAlign = ContentAlignment.MiddleCenter
                tileButton.Tag = id ' Store ID in Tag
                tileButton.Cursor = Cursors.Hand

                ' Add hover effect
                AddHandler tileButton.MouseEnter, Sub(sender, e) tileButton.BackColor = Color.FromArgb(240, 240, 240)
                AddHandler tileButton.MouseLeave, Sub(sender, e) tileButton.BackColor = Color.FromArgb(248, 249, 250)

                ' Add click event
                AddHandler tileButton.Click, Sub(sender, e)
                                                 Dim button As Button = CType(sender, Button)
                                                 If button.Tag IsNot Nothing AndAlso Integer.TryParse(button.Tag.ToString(), id) Then
                                                     MessageBox.Show($"Product ID: {id}", "Tile Clicked")
                                                 Else
                                                     MessageBox.Show("Invalid product ID", "Error")
                                                 End If
                                             End Sub

                ' Add tile to FlowLayoutPanel
                flowPanel.Controls.Add(tileButton)
                tileCount += 1
                Debug.WriteLine($"Rendered tile: ID={id}, Title={title}, Caption={caption}")
            Next

            Debug.WriteLine($"Total tiles rendered: {tileCount}")
        Catch ex As Exception
            MessageBox.Show("Error loading tile buttons: " & ex.Message, "Error")
            Debug.WriteLine("LoadTileButtons Error: " & ex.Message)
        Finally
            ' Resume layout
            flowPanel.ResumeLayout()
            Debug.WriteLine("Layout resumed")
        End Try
    End Sub

    ' Helper to invoke UI updates on the main thread
    Private Async Function InvokeAsync(action As Action) As Task
        If Me.InvokeRequired Then
            Await Task.Run(Sub() Me.Invoke(action))
        Else
            action()
        End If
    End Function
    '   testuser1
    'Mom-3655807
    '10611649

    'https://portal.jofotara.gov.jo/ar/New-invoice/receivable
    'https://portal.jofotara.gov.jo/ar/new-invoice/cash
End Class