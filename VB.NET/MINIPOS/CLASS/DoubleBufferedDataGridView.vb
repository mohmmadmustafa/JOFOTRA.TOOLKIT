Public Class DoubleBufferedDataGridView
    Inherits DataGridView

    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub
End Class