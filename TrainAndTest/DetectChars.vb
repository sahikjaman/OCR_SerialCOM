Option Explicit On      'require explicit declaration of variables, this is NOT Python !!
Option Strict On        'restrict implicit data type conversions to only widening conversions

Imports Emgu.CV                     '
Imports Emgu.CV.CvEnum              'Emgu Cv imports
Imports Emgu.CV.Structure           '
Imports Emgu.CV.UI                  '
Imports Emgu.CV.ML                  '
Imports Emgu.CV.Util

Imports System.Xml
Imports System.Xml.Serialization    'these imports are for reading Matrix objects from file
Imports System.IO




Module DetectChars

    'Const MIN_RECT_AREA As Integer = 40                     '40
    'Const MIN_CONTOUR_AREA As Integer = 40                  '30

    'constants for comparing two chars          Jarak diantara char
    Const MIN_DIAG_SIZE_MULTIPLE_AWAY As Double = 0.2
    Public MAX_DIAG_SIZE_MULTIPLE_AWAY As Double = 6.5          '6.5

    Public MAX_CHANGE_IN_AREA As Double = 0.8                '0.8                (Karakter 1 dengan yang lain)

    Public Jarakarakter As Double = 50                       '65 fix  - 65 >> untuk video yg besar
    Public JarakaraterY As Double = 40                      '35 Fix   - 42

    Const MAX_ANGLE_BETWEEN_CHARS As Double = 270.0
    Const MIN_ANGLE_BETWEEN_CHARS As Double = 10.0

    Const MIN_NUMBER_OF_MATCHING_CHARS As Integer = 6

    Const MAX_NUMBER_OF_MATCHING_CHARS As Integer = 15

    Const RESIZED_CHAR_IMAGE_WIDTH As Integer = 20
    Const RESIZED_CHAR_IMAGE_HEIGHT As Integer = 30

    Dim SCALAR_WHITE As New MCvScalar(255.0, 255.0, 255.0)
    Dim SCALAR_GREEN As New MCvScalar(0.0, 255.0, 0.0)

    'MAXIMUM area yang dideteksi sebagai char
    Public MAX_RECT_AREA As Double = 8000                  '8000
    Public MAX_CONTOUR_AREA As Double = 8000               '8000

    Public MIN_REC_AREA2 As Double = 50                    '150
    Public MIN_CONTOUR_AREA2 As Double = 50                '150

    'Bagian Manipulasi deteksi charakter
    'Variabel Baru
    Const MIN_ASPECT_RATIO As Double = 0.1                 ' Menentukan lebar kecil char yang dideteksi (Clear) contohnya 1
    Const MAX_ASPECT_RATIO As Double = 2.0              '1.5

    Const MIN_PIXEL_WIDTH As Integer = 2                    '1
    Const MIN_PIXEL_HEIGHT As Integer = 5                   '5



    'variables
    Dim kNearest As New KNearest()
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function findListOfListsOfMatchingChars(listOfPossibleChars As List(Of ContourWithData)) As List(Of List(Of ContourWithData))
        'with this function, we start off with all the possible chars in one big list
        'the purpose of this function is to re-arrange the one big list of chars into a list of lists of matching chars,
        'note that chars that are not found to be in a group of matches do not need to be considered further
        Dim listOfListsOfMatchingChars As List(Of List(Of ContourWithData)) = New List(Of List(Of ContourWithData))       'this will be the return value

        'Disini kita masukkan program tiap region,

        For Each possibleChar As ContourWithData In listOfPossibleChars        'for each possible char in the one big list of chars

            'find all chars in the big list that match the current char
            Dim listOfMatchingChars As List(Of ContourWithData) = findListOfMatchingChars(possibleChar, listOfPossibleChars)

            listOfMatchingChars.Add(possibleChar)       'also add the current char to current possible list of matching chars

            'if current possible list of matching chars is not long enough to constitute a possible plate
            If (listOfMatchingChars.Count < MIN_NUMBER_OF_MATCHING_CHARS) Then
                Continue For                    'jump back to the top of the for loop and try again with next char, note that it's not necessary
                'to save the list in any way since it did not have enough chars to be a possible plate
            End If
            'if we get here, the current list passed test as a "group" or "cluster" of matching chars
            listOfListsOfMatchingChars.Add(listOfMatchingChars)     'so add to our list of lists of matching chars

            'remove the current list of matching chars from the big list so we don't use those same chars twice,
            'make sure to make a new big list for this since we don't want to change the original big list

            Dim listOfPossibleCharsWithCurrentMatchesRemoved As List(Of ContourWithData) = listOfPossibleChars.Except(listOfMatchingChars).ToList()


            'declare new list of lists of chars to get result from recursive call
            Dim recursiveListOfListsOfMatchingChars As List(Of List(Of ContourWithData)) = New List(Of List(Of ContourWithData))

            recursiveListOfListsOfMatchingChars = findListOfListsOfMatchingChars(listOfPossibleCharsWithCurrentMatchesRemoved)      'recursive call

            For Each recursiveListOfMatchingChars As List(Of ContourWithData) In recursiveListOfListsOfMatchingChars       'for each list of matching chars found by recursive call
                listOfListsOfMatchingChars.Add(recursiveListOfMatchingChars)                'add to our original list of lists of matching chars
            Next

            Exit For                'jump out of for loop
        Next

        Return listOfListsOfMatchingChars           'return result
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function findListOfMatchingChars(possibleChar As ContourWithData, listOfChars As List(Of ContourWithData)) As List(Of ContourWithData)
        'the purpose of this function is, given a possible char and a big list of possible chars,
        'find all chars in the big list that are a match for the single possible char, and return those matching chars as a list
        Dim listOfMatchingChars As List(Of ContourWithData) = New List(Of ContourWithData)            'this will be the return value

        For Each possibleMatchingChar As ContourWithData In listOfChars        'for each char in big list

            'if the char we attempting to find matches for is the exact same char as the char in the big list we are currently checking
            If (possibleMatchingChar.Equals(possibleChar)) Then
                'then we should not include it in the list of matches b/c that would end up double including the current char
                Continue For        'so do not add to list of matches and jump back to top of for loop
            End If
            'compute stuff to see if chars are a match
            Dim dblDistanceBetweenChars As Double = distanceBetweenChars(possibleChar, possibleMatchingChar)

            Dim JarakCharsX As Double = distanceBetweenCharsX(possibleChar, possibleMatchingChar)
            Dim JarakCharsY As Double = distanceBetweenCharsY(possibleChar, possibleMatchingChar)

            Dim dblAngleBetweenChars As Double = angleBetweenChars(possibleChar, possibleMatchingChar)

            Dim dblChangeInArea As Double = Math.Abs(possibleMatchingChar.intRectArea - possibleChar.intRectArea) / possibleChar.intRectArea

            Dim dblChangeInWidth As Double = Math.Abs(possibleMatchingChar.boundingRect.Width - possibleChar.boundingRect.Width) / possibleChar.boundingRect.Width
            Dim dblChangeInHeight As Double = Math.Abs(possibleMatchingChar.boundingRect.Height - possibleChar.boundingRect.Height) / possibleChar.boundingRect.Height

            'frmMain.txtInfo.AppendText(vbCrLf + "Jarak Char= " + CStr(JarakCharsY) + vbCrLf)
            'And JarakCharsY < 300
            'JarakCharY > 30
            'check if chars match

            If frmMain.NumericDiagSize.Value > 0 And frmMain.NumericMaxChange.Value > 0 And frmMain.NumericJarakYPixel.Value > 0 And frmMain.NumericJarakXPixel.Value > 0 And frmMain.NumericJarakYPixel.Value > 0 Then
                MAX_DIAG_SIZE_MULTIPLE_AWAY = (CDbl(frmMain.NumericDiagSize.Value) / 10)
                MAX_CHANGE_IN_AREA = (CDbl(frmMain.NumericMaxChange.Value) / 10)
                JarakaraterY = CDbl(frmMain.NumericJarakYPixel.Value)
                Jarakarakter = CDbl(frmMain.NumericJarakXPixel.Value)


                If (dblDistanceBetweenChars < (possibleChar.dblDiagonalSIze * MAX_DIAG_SIZE_MULTIPLE_AWAY) And _
                    dblAngleBetweenChars > MIN_ANGLE_BETWEEN_CHARS And _
                    dblAngleBetweenChars < MAX_ANGLE_BETWEEN_CHARS And _
                    JarakCharsX < Jarakarakter And _
                    JarakCharsY > JarakaraterY And _
                    dblChangeInArea < MAX_CHANGE_IN_AREA) Then
                    'dblChangeInWidth < MAX_CHANGE_IN_WIDTH And _
                    'dblChangeInHeight < MAX_CHANGE_IN_HEIGHT) Then

                    listOfMatchingChars.Add(possibleMatchingChar)       'if the chars are a match, add the current char to list of matching chars
                End If
            Else
                MsgBox("Diag.Size dan Rec.Area Tidak Boleh 0")
                frmMain.NumericDiagSize.Value = 80
                frmMain.NumericRecArea.Value = 35

            End If

        Next

        Return listOfMatchingChars          'return result
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'use Pythagorean theorem to calculate distance between two chars
    Function distanceBetweenChars(firstChar As ContourWithData, secondChar As ContourWithData) As Double
        Dim intX As Integer = Math.Abs(firstChar.intCenterX - secondChar.intCenterX)
        Dim intY As Integer = Math.Abs(firstChar.intCenterY - secondChar.intCenterY)

        Return Math.Sqrt((intX ^ 2) + (intY ^ 2))
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'use Pythagorean theorem to calculate distance between two chars
    Function distanceBetweenCharsY(firstChar As ContourWithData, secondChar As ContourWithData) As Double
        Dim intY As Integer = Math.Abs(firstChar.intCenterY - secondChar.intCenterY)

        Return intY
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'use Pythagorean theorem to calculate distance between two chars
    Function distanceBetweenCharsX(firstChar As ContourWithData, secondChar As ContourWithData) As Double
        Dim intX As Integer = Math.Abs(firstChar.intCenterX - secondChar.intCenterX)

        Return intX
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'use basic trigonometry (SOH CAH TOA) to calculate angle between chars
    Function angleBetweenChars(firstChar As ContourWithData, secondChar As ContourWithData) As Double
        Dim dblAdj As Double = CDbl(Math.Abs(firstChar.intCenterX - secondChar.intCenterX))
        Dim dblOpp As Double = CDbl(Math.Abs(firstChar.intCenterY - secondChar.intCenterY))

        Dim dblAngleInRad As Double = Math.Atan(dblOpp / dblAdj)

        Dim dblAngleInDeg As Double = dblAngleInRad * (180.0 / Math.PI)
        Return dblAngleInDeg
    End Function


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'if we have two chars overlapping or to close to each other to possibly be separate chars, remove the inner (smaller) char,
    'this is to prevent including the same char twice if two contours are found for the same char,
    'for example for the letter 'O' both the inner ring and the outer ring may be found as contours, but we should only include the char once
    Function removeInnerOverlappingChars(listOfMatchingChars As List(Of ContourWithData)) As List(Of ContourWithData)
        Dim listOfMatchingCharsWithInnerCharRemoved As List(Of ContourWithData) = New List(Of ContourWithData)(listOfMatchingChars)

        For Each currentChar As ContourWithData In listOfMatchingChars
            For Each otherChar As ContourWithData In listOfMatchingChars
                If (Not currentChar.Equals(otherChar)) Then                                     'if current char and other char are not the same char . . .
                    'if current char and other char have center points at almost the same location . . .
                    If (distanceBetweenChars(currentChar, otherChar) < (currentChar.dblDiagonalSIze * MIN_DIAG_SIZE_MULTIPLE_AWAY)) Then
                        'if we get in here we have found overlapping chars
                        'next we identify which char is smaller, then if that char was not already removed on a previous pass, remove it
                        If (currentChar.intRectArea < otherChar.intRectArea) Then                       'if current char is smaller than other char
                            If (listOfMatchingCharsWithInnerCharRemoved.Contains(currentChar)) Then     'if current char was not already removed on a previous pass . . .
                                listOfMatchingCharsWithInnerCharRemoved.Remove(currentChar)             'then remove current char
                            End If
                        Else                                                                            'else if other char is smaller than current char
                            If (listOfMatchingCharsWithInnerCharRemoved.Contains(otherChar)) Then       'if other char was not already removed on a previous pass . . .
                                listOfMatchingCharsWithInnerCharRemoved.Remove(otherChar)               'then remove other char
                            End If

                        End If
                    End If
                End If
            Next
        Next

        Return listOfMatchingCharsWithInnerCharRemoved
    End Function



    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function detectCharsInPlates(listOfPossiblePlates As List(Of PossiblePlate)) As List(Of PossiblePlate)
        Dim intPlateCounter As Integer = 0              'this is only for showing steps
        Dim imgContours As Mat

        Dim random As New Random()                      'this is only for showing steps
        Dim data As Integer = frmMain.perubahanStruktur

        If (listOfPossiblePlates Is Nothing) Then           'if list of possible plates is null,
            Return listOfPossiblePlates                     'return
        ElseIf (listOfPossiblePlates.Count = 0) Then        'if list of possible plates has zero plates
            Return listOfPossiblePlates                     'return
        End If

        'at this point we can be sure list of possible plates has at least one plate

        For Each possiblePlate As PossiblePlate In listOfPossiblePlates     ' for each possible plate, this is a big for loop that takes up most of the function

            CvInvoke.CvtColor(possiblePlate.imgPlate, possiblePlate.imgGrayscale, ColorConversion.Bgr2Gray)        'convert to grayscale

            CvInvoke.GaussianBlur(possiblePlate.imgGrayscale, possiblePlate.imgGrayscale, New Size(3, 3), 0)                  'blur

            CvInvoke.Threshold(possiblePlate.imgGrayscale, possiblePlate.imgThresh, data, 255.0, ThresholdType.Binary)

            If (frmMain.cbShowSteps.Checked = True) Then ' show steps '''''''''''''''''''''''''''''
                CvInvoke.Imshow("threshold", possiblePlate.imgThresh)

                CvInvoke.Imshow("5a", possiblePlate.imgPlate)
                CvInvoke.Imshow("5b", possiblePlate.imgGrayscale)
                CvInvoke.Imshow("5c", possiblePlate.imgThresh)
            End If


            ' '''''''''''''''''''''''''''''''''Untuk gambar satu
            Try
                CvInvoke.Resize(possiblePlate.imgThresh, possiblePlate.imgThresh, New Size(), 1.6, 1.6)     'upscale size by 60% for better viewing and character recognition
            Catch ex As Exception

            End Try

            Dim imgThreshCopyPlates As New Mat()

            'CvInvoke.Threshold(possiblePlate.imgThresh, possiblePlate.imgThresh, 0.0, 255.0, ThresholdType.Binary Or ThresholdType.Otsu)    'threshold again to eliminate any gray areas    'Important Things
            'CvInvoke.Imshow("threshold", possiblePlate.imgThresh)
            imgThreshCopyPlates = possiblePlate.imgThresh.Clone()           'make a copy of the thresh image, this in necessary b/c findContours modifies the image

            Dim contoursPlates As New VectorOfVectorOfPoint()

            'get external countours only
            CvInvoke.FindContours(imgThreshCopyPlates, contoursPlates, Nothing, RetrType.External, ChainApproxMethod.ChainApproxSimple)

            'Percobaan tambahan dilate
            'Dim imgDilated As Mat
            Dim element As Mat = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross, New Size(3, 3), New Point(1, 1))
            Dim elementDilate As Mat = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross, New Size(3, 3), New Point(1, 1))

            Dim listOfContoursWithData As New List(Of ContourWithData)          'declare a list of contours with data
            'Dapatkan Contours
            'populate list of contours with data
            For i As Integer = 0 To contoursPlates.Size - 1                   'for each contour
                'Berikut ini hanyalah komentar

                '#If False Then

                Dim possibleChar As New ContourWithData(contoursPlates(i))


                'Lakukan pengambilan data dari NumericalUpdown Value
                If frmMain.NumericContour.Value > 0 And frmMain.NumericRecArea.Value > 0 Then
                    
                    'Lakukan Seleksi Akhir untuk pendeteksian Karakter
                    If (possibleChar.intRectArea > MIN_REC_AREA2 And _
                         possibleChar.dblArea > MIN_CONTOUR_AREA2 And _
                         possibleChar.dblArea < MAX_CONTOUR_AREA And _
                         possibleChar.intRectArea < MAX_RECT_AREA And _
                         possibleChar.boundingRect.Width > MIN_PIXEL_WIDTH And _
                         possibleChar.boundingRect.Height > MIN_PIXEL_HEIGHT And _
                         MIN_ASPECT_RATIO < possibleChar.dblAspectRatio And _
                         possibleChar.dblAspectRatio < MAX_ASPECT_RATIO) Then

                        'Jika seleksi berhasil jumlahkan countur yang berupa angka dan huruf
                        listOfContoursWithData.Add(possibleChar)
                    End If

                End If


            Next
            If listOfContoursWithData.Count < 20 Then
                'sort chars dari atas ke bawah
                listOfContoursWithData.Sort(Function(oneChar, otherChar) oneChar.boundingRect.Y.CompareTo(otherChar.boundingRect.Y))

                possiblePlate.strChars = recognizeCharsInPlate(possiblePlate.imgThresh, listOfContoursWithData)      'perform char recognition on the longest list of matching chars in the plate
            End If

            ' ''''''''''''''''''''''''''''''''''''''''''''''''''Next Steps'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If (frmMain.cbShowSteps.Checked = True) Then ' show steps '''''''''''''''''''''''''''''
                frmMain.txtInfo.AppendText("chars found in plate number " + intPlateCounter.ToString + " = " + possiblePlate.strChars + ", click on any image and press a key to continue . . ." + vbCrLf)
                intPlateCounter = intPlateCounter + 1
                CvInvoke.WaitKey(0)
            End If ' show steps '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        Next        'end for each possible plate big for loop that takes up most of the function

        Return listOfPossiblePlates
    End Function


    Function loadKNNDataAndTrainKNN() As Boolean
        'note: we effectively have to read the first XML file twice
        'first, we read the file to get the number of rows (which is the same as the number of samples)
        'the first time reading the file we can't get the data yet, since we don't know how many rows of data there are
        'next, reinstantiate our classifications Matrix and training images Matrix with the correct number of rows
        'then, read the file again and this time read the data into our resized classifications Matrix and training images Matrix

        Dim mtxClassifications As Matrix(Of Single) = New Matrix(Of Single)(1, 1)           'for the first time through, declare these to be 1 row by 1 column
        Dim mtxTrainingImages As Matrix(Of Single) = New Matrix(Of Single)(1, 1)            'we will resize these when we know the number of rows (i.e. number of training samples)

        Dim intValidChars As New List(Of Integer)(New Integer() {Asc("0"), Asc("1"), Asc("2"), Asc("3"), Asc("4"), Asc("5"), Asc("6"), Asc("7"), Asc("8"), Asc("9"),
                                                                  Asc("A"), Asc("B"), Asc("C"), Asc("D"), Asc("E"), Asc("F"), Asc("G"), Asc("H"), Asc("I"), Asc("J"),
                                                                  Asc("K"), Asc("L"), Asc("M"), Asc("N"), Asc("O"), Asc("P"), Asc("Q"), Asc("R"), Asc("S"), Asc("T"),
                                                                  Asc("U"), Asc("V"), Asc("W"), Asc("X"), Asc("Y"), Asc("Z")})

        Dim xmlSerializer As XmlSerializer = New XmlSerializer(mtxClassifications.GetType)              'these variables are for
        Dim streamReader As StreamReader                                                                'reading from the XML files

        Try
            streamReader = New StreamReader("classifications.xml")              'attempt to open classifications file
        Catch ex As Exception                                                   'if error is encountered, show error and return
            frmMain.txtInfo.AppendText(vbCrLf + "unable to open 'classifications.xml', error: ")
            frmMain.txtInfo.AppendText(ex.Message + vbCrLf)
            Return False
        End Try

        'read from the classifications file the 1st time, this is only to get the number of rows, not the actual data
        mtxClassifications = CType(xmlSerializer.Deserialize(streamReader), Matrix(Of Single))

        streamReader.Close()            'close the classifications XML file

        Dim intNumberOfTrainingSamples As Integer = mtxClassifications.Rows             'get the number of rows, i.e. the number of training samples

        'now that we know the number of rows, reinstantiate classifications Matrix and training images Matrix with the actual number of rows
        mtxClassifications = New Matrix(Of Single)(intNumberOfTrainingSamples, 1)
        mtxTrainingImages = New Matrix(Of Single)(intNumberOfTrainingSamples, RESIZED_CHAR_IMAGE_WIDTH * RESIZED_CHAR_IMAGE_HEIGHT)

        Try
            streamReader = New StreamReader("classifications.xml")              'reinitialize the stream reader
        Catch ex As Exception                                                   'if error is encountered, show error and return
            frmMain.txtInfo.AppendText(vbCrLf + "unable to open 'classifications.xml', error:" + vbCrLf)
            frmMain.txtInfo.AppendText(ex.Message + vbCrLf + vbCrLf)
            Return False
        End Try
        'read from the classifications file again, this time we can get the actual data
        mtxClassifications = CType(xmlSerializer.Deserialize(streamReader), Matrix(Of Single))

        streamReader.Close()            'close the classifications XML file

        xmlSerializer = New XmlSerializer(mtxTrainingImages.GetType)                'reinstantiate file reading variable

        Try
            streamReader = New StreamReader("images.xml")
        Catch ex As Exception                                               'if error is encountered, show error and return
            frmMain.txtInfo.AppendText("unable to open 'images.xml', error:" + vbCrLf)
            frmMain.txtInfo.AppendText(ex.Message + vbCrLf + vbCrLf)
            Return False
        End Try

        mtxTrainingImages = CType(xmlSerializer.Deserialize(streamReader), Matrix(Of Single))           'read from training images file
        streamReader.Close()                                            'close the training images XML file

        ' train '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        kNearest.DefaultK = 1

        kNearest.Train(mtxTrainingImages, MlEnum.DataLayoutType.RowSample, mtxClassifications)

        Return True         'if we got here training was successful so return true
    End Function


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'this is where we apply the actual char recognition
    Function recognizeCharsInPlate(imgThresh As Mat, listOfMatchingChars As List(Of ContourWithData)) As String
        Dim strChars As String = ""         'this will be the return value, the chars in the lic plate

        Dim imgThreshColor As New Mat()

        listOfMatchingChars.Sort(Function(oneChar, otherChar) oneChar.boundingRect.Y.CompareTo(otherChar.boundingRect.Y))   'sort chars from up to down

        CvInvoke.CvtColor(imgThresh, imgThreshColor, ColorConversion.Gray2Bgr)

        For Each currentChar As ContourWithData In listOfMatchingChars                                 'for each char in plate
            CvInvoke.Rectangle(imgThreshColor, currentChar.boundingRect, SCALAR_GREEN, 2)           'draw green box around the char

            Dim imgROItoBeCloned As New Mat(imgThresh, currentChar.boundingRect)            'get ROI image of bounding rect

            Dim imgROI As Mat = imgROItoBeCloned.Clone()            'clone ROI image so we don't change original when we resize

            Dim imgROIResized As New Mat()

            'resize image, this is necessary for char recognition
            CvInvoke.Resize(imgROI, imgROIResized, New Size(RESIZED_CHAR_IMAGE_WIDTH, RESIZED_CHAR_IMAGE_HEIGHT))

            'declare a Matrix of the same dimensions as the Image we are adding to the data structure of training images
            Dim mtxTemp As Matrix(Of Single) = New Matrix(Of Single)(imgROIResized.Size())

            'declare a flattened (only 1 row) matrix of the same total size
            Dim mtxTempReshaped As Matrix(Of Single) = New Matrix(Of Single)(1, RESIZED_CHAR_IMAGE_WIDTH * RESIZED_CHAR_IMAGE_HEIGHT)

            imgROIResized.ConvertTo(mtxTemp, DepthType.Cv32F)       'convert Image to a Matrix of Singles with the same dimensions

            For intRow As Integer = 0 To RESIZED_CHAR_IMAGE_HEIGHT - 1          'flatten Matrix into one row by RESIZED_IMAGE_WIDTH * RESIZED_IMAGE_HEIGHT number of columns
                For intCol As Integer = 0 To RESIZED_CHAR_IMAGE_WIDTH - 1
                    mtxTempReshaped(0, (intRow * RESIZED_CHAR_IMAGE_WIDTH) + intCol) = mtxTemp(intRow, intCol)
                Next
            Next

            Dim sngCurrentChar As Single

            sngCurrentChar = kNearest.Predict(mtxTempReshaped)      'finally we can call Predict !!!

            strChars = strChars + Chr(Convert.ToInt32(sngCurrentChar))      'append current char to full string of chars
        Next

        'If (frmMain.cbShowSteps.Checked = True) Then ' show steps '''''''''''''''''''''''''''''''''
        'CvInvoke.Imshow("10", imgThreshColor)
        'End If ' show steps '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Try
            'frmMain.ibNomor.Image = imgThreshColor
        Catch ex As Exception

        End Try

        Return strChars         'return result
    End Function

End Module
