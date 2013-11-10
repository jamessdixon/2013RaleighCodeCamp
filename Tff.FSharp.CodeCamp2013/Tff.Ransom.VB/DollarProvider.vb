Public Class DollarProvider

    Public Function GetDollars(ByVal numberOfDollars As Int32) As List(Of Dollar)
        Dim dollars As New List(Of Dollar)()
        Dim currentDollar As Dollar = Nothing
        Dim random As New Random()
        For dollarIndex As Integer = 0 To numberOfDollars - 1
            currentDollar = New Dollar()
            currentDollar.FederalReserveDistrictNumber = random.Next(1, 13)
            currentDollar.Id = dollarIndex

            Dim serialNumer As String = String.Empty
            For serialNumberIndex As Integer = 0 To 9
                serialNumer += random.Next(0, 9).ToString()
            Next

            currentDollar.SerialNumber = serialNumer
            currentDollar.SeriesDate = 2000 + random.Next(0, 10)
            dollars.Add(currentDollar)
        Next
        Return dollars
    End Function

End Class
