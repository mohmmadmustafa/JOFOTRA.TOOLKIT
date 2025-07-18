<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAbout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.lblAppInfo = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtRegNumber = New System.Windows.Forms.TextBox()
        Me.txtRegKey = New System.Windows.Forms.TextBox()
        Me.lblRegKey = New System.Windows.Forms.Label()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblAppInfo
        '
        Me.lblAppInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAppInfo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppInfo.Location = New System.Drawing.Point(0, 0)
        Me.lblAppInfo.Name = "lblAppInfo"
        Me.lblAppInfo.Size = New System.Drawing.Size(486, 53)
        Me.lblAppInfo.TabIndex = 0
        Me.lblAppInfo.Text = "MyApplication - Version"
        Me.lblAppInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(191, 96)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(67, 20)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "حالة التسجيل"
        '
        'txtRegNumber
        '
        Me.txtRegNumber.Font = New System.Drawing.Font("Cairo", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtRegNumber.Location = New System.Drawing.Point(191, 120)
        Me.txtRegNumber.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtRegNumber.Name = "txtRegNumber"
        Me.txtRegNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegNumber.Size = New System.Drawing.Size(287, 30)
        Me.txtRegNumber.TabIndex = 2
        Me.txtRegNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRegKey
        '
        Me.txtRegKey.Font = New System.Drawing.Font("Cairo", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtRegKey.Location = New System.Drawing.Point(189, 188)
        Me.txtRegKey.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtRegKey.Name = "txtRegKey"
        Me.txtRegKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegKey.Size = New System.Drawing.Size(287, 30)
        Me.txtRegKey.TabIndex = 3
        Me.txtRegKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblRegKey
        '
        Me.lblRegKey.AutoSize = True
        Me.lblRegKey.Location = New System.Drawing.Point(187, 164)
        Me.lblRegKey.Name = "lblRegKey"
        Me.lblRegKey.Size = New System.Drawing.Size(75, 20)
        Me.lblRegKey.TabIndex = 4
        Me.lblRegKey.Text = "مفتاح التسجيل"
        '
        'btnRegister
        '
        Me.btnRegister.Location = New System.Drawing.Point(191, 223)
        Me.btnRegister.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(75, 36)
        Me.btnRegister.TabIndex = 5
        Me.btnRegister.Text = "تسجيل"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(401, 223)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 36)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "اغلاق"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Cairo", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(0, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(486, 33)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "برمجه : محمد عمر مصطفى"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 96)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(161, 156)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 10
        Me.PictureBox2.TabStop = False
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(486, 264)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.lblRegKey)
        Me.Controls.Add(Me.txtRegKey)
        Me.Controls.Add(Me.txtRegNumber)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblAppInfo)
        Me.Font = New System.Drawing.Font("Cairo", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "frmAbout"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "عن البرنامج"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblAppInfo As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtRegNumber As TextBox
    Friend WithEvents txtRegKey As TextBox
    Friend WithEvents lblRegKey As Label
    Friend WithEvents btnRegister As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class