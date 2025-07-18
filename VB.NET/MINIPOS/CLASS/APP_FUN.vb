Module APP_FUN
    Public Structure userinfom
        Dim id As Integer
        Dim name As String
        Dim scurty_id As String
        Dim key As String
        Dim kind As Integer
    End Structure

    Public userInfo As userinfom
    Public Function RoundToFiveCents(ByVal value As Double) As Double
        Return Math.Round(value * 20) / 20
    End Function
    Public Function GET_FONT_SIZE(TXT As String, FON As Font, g As Graphics) As SizeF
        Dim textSize As SizeF = g.MeasureString(TXT, FON)
        ' Dim textWidth As Single = textSize.Width
        ' Dim textHeight As Single = textSize.Height

        Return textSize
    End Function

    Public IsRegistered As Boolean = False
End Module
