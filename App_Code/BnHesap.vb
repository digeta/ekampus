Imports Microsoft.VisualBasic
Imports System.Math

Public Class BnHesap
    Public Function bnHesapla(ByVal hbn As Integer, ByVal genelOrtalama As Integer, ByVal stdSapma As Double) As Double
        Try
            Dim z As Double
            Dim t As Double

            z = SafeDivide((hbn - genelOrtalama), stdSapma)
            t = (z * 10) + 50

            Return t
        Catch ex As Exception
        End Try
    End Function

    Public Function SafeDivide(ByVal dbl1 As Double, ByVal dbl2 As Double) As Double
        If (dbl1 = 0) Or (dbl2 = 0) Then Return 0 Else Return dbl1 / dbl2
    End Function

    Public Function Average(ByVal dblData As Double()) As Double
        Try
            Dim DataTotal As Double = 0

            For i As Integer = 0 To dblData.Length - 1
                DataTotal += dblData(i)
            Next

            Return SafeDivide(DataTotal, dblData.Length)
        Catch ex As Exception
        End Try
    End Function

    Public Function CalculateStandardDeviation(ByVal dblData As Double()) As Double
        Try
            Dim dblDataAverage As Double = 0
            Dim TotalVariance As Double = 0

            If dblData.Length = 0 Then Return 0

            dblDataAverage = Average(dblData)

            For i As Integer = 0 To dblData.Length - 1
                TotalVariance += Math.Pow(dblData(i) - dblDataAverage, 2)
            Next

            Return Math.Sqrt(SafeDivide(TotalVariance, dblData.Length))
        Catch ex As Exception
        End Try
    End Function
End Class
