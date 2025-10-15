'TrainAndTest
'ContourWithData.vb

Option Explicit On      'require explicit declaration of variables, this is NOT Python !!
Option Strict On        'restrict implicit data type conversions to only widening conversions

Imports System.Math

Imports Emgu.CV                     '
Imports Emgu.CV.CvEnum              'Emgu Cv imports
Imports Emgu.CV.Structure           '
Imports Emgu.CV.UI                  '
Imports Emgu.CV.Util                '

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class ContourWithData

    ' member variables ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Const MIN_CONTOUR_AREA As Integer = 40
    Const MIN_Rectange As Integer = 100

    Public contour As VectorOfPoint             'contour
    Public boundingRect As Rectangle            'bounding rect for contour
    Public dblArea As Double                    'area of contour

    Public intRectArea As Integer
    Public dblAspectRatio As Double
    Public dblDiagonalSIze As Double

    Public intCenterX As Integer
    Public intCenterY As Integer

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' constructor '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub New(_contour As VectorOfPoint)
        contour = _contour

        boundingRect = CvInvoke.BoundingRectangle(contour)

        dblArea = CvInvoke.ContourArea(contour)

        intCenterX = CInt((boundingRect.Left + boundingRect.Right) / 2)
        intCenterY = CInt((boundingRect.Top + boundingRect.Bottom) / 2)

        dblDiagonalSize = Math.Sqrt((boundingRect.Width ^ 2) + (boundingRect.Height ^ 2))

        dblAspectRatio = CDbl(boundingRect.Width) / CDbl(boundingRect.Height)

        intRectArea = boundingRect.Width * boundingRect.Height
    End Sub

    Public Function checkIfContourIsValid() As Boolean      'this is oversimplified, for a production grade program better validity checking would be necessary
        'Dim panjang As Integer
        Dim luas As Integer

        luas = boundingRect.Width * boundingRect.Height

        If (dblArea < MIN_CONTOUR_AREA) And MIN_Rectange < luas Then
            Return False
        Else
            Return True
        End If
    End Function

End Class

