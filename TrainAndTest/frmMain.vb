
Option Explicit On      'require explicit declaration of variables, this is NOT Python !!
Option Strict On        'restrict implicit data type conversions to only widening conversions

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.IO.Ports
Imports System.Net
Imports System.Text
Imports System.Threading
Imports System.Xml                  '
Imports System.Xml.Serialization    'these imports are for writing Matrix objects to file, see end of program
Imports Emgu.CV                 'usual Emgu Cv imports
Imports Emgu.CV.CvEnum          '
Imports Emgu.CV.ML              '
Imports Emgu.CV.Structure       '
Imports Emgu.CV.UI              '
Imports Emgu.CV.Util            '
Imports MySql.Data.MySqlClient '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class frmMain
    ' module level variables ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Const RESIZED_IMAGE_WIDTH As Integer = 20
    Const RESIZED_IMAGE_HEIGHT As Integer = 30

    Const IMAGE_BOX_PCT_SHOW_STEPS_NOT_CHECKED As Single = 75       'these are for changing the proportion of image box to text box based on if we are showing steps or not
    Const TEXT_BOX_PCT_SHOW_STEPS_NOT_CHECKED As Single = 25

    Const IMAGE_BOX_PCT_SHOW_STEPS_CHECKED As Single = 55           'the idea is to show more of the text box if we are showing steps since there is more text to display
    Const TEXT_BOX_PCT_SHOW_STEPS_CHECKED As Single = 45

    Dim SCALAR_RED As New MCvScalar(0.0, 0.0, 255.0)
    Dim SCALAR_YELLOW As New MCvScalar(0.0, 255.0, 255.0)
    Dim SCALAR_WHITE As New MCvScalar(255.0, 255.0, 255.0)

    '==========================================Variabel Baru======================================================================
    Const MIN_ASPECT_RATIO As Double = 0.1                 '0.1 Menentukan lebar kecil char yang dideteksi (Clear) contohnya 1
    Const MAX_ASPECT_RATIO As Double = 2.0              '2.0 Standard Foto 28/10/17

    Const MIN_PIXEL_WIDTH As Integer = 2                    '2
    Const MIN_PIXEL_HEIGHT As Integer = 5                   '5

    Public MIN_RECT_AREA As Integer = 40                     '40 45
    Public MIN_CONTOUR_AREA As Integer = 30                  '30 45

    '=====================================Bagian untuk ekstrak plat kontainer=======================================================================
    Const PLATE_WIDTH_PADDING_FACTOR As Double = 1.0            '5.0 Fix  --2.0 >> pixel video
    Const PLATE_HEIGHT_PADDING_FACTOR As Double = 27.9          '25.9 fix  --25.9

    'Bagian Capture Gambar dan Video
    Dim _capture As Capture = Nothing
    Dim frame As New Mat()
    Dim getimage As New Mat()
    Dim vertikal As New Mat()
    Dim convertImage As New Mat()
    Dim capturecontainer As Mat
    Dim _captureInProgress As Boolean

    '=========================================Bagian Proses Karakter==================================================================================
    Dim datakarakter As String
    Dim datakaraktersebelumnya As String
    Dim dataPlat As String
    Dim karakter() As Char
    Dim cekkarakter() As Char
    Dim cekPlate() As Char
    Dim datainsert As String
    Dim check() As Char
    Dim jumlahAngka As Integer
    Dim jumlahHuruf As Integer
    Dim seleksiSPNU As Integer
    Dim seleksiKarakterAneh As Integer
    Dim datenows() As String
    '============================================Bagian Save Gambar===================================================================================
    Dim saveToFile As Boolean
    Dim namafile As String
    Dim gabungdata As String
    Dim waktu As String
    Dim fixPlat As String

    Dim getimageFile As Boolean
    Dim savedata As Boolean
    Dim savedataSebelumnya As Boolean
    Dim file As Integer = 0
    Public perubahanStruktur As Integer = 203       ' ''''''''''''''''''''''Nilai variable 205 standard Untuk Pagi dan Siang 203 baru
    ' ''''''''''''''''''''''Untuk Sore menjelang maghrib settingan berubah menjadi 180
    'vm4 bisa menggunakan threshold = 175
    'vm5 bisa menggunakan threshold = 180

    Dim loopingOCR As Integer = 0                   ' ''''''''''''''''''''''Variable untuk melakukan looping ocr apabil
    Dim dataSekarang As String
    Dim dataPerbandinganWaktu As String             ' ''''''''''''''''''''''Untuk menyimpan data waktu sekarang dan untuk mengetahui waktu yang telah lewat
    Dim loopingForNextData As Integer = 0
    '================================================Class Database====================================================================================
    Dim serialnumber As Boolean
    Dim ownerkode As Boolean
    Dim checkdigit As Boolean

    '============================================Deklarasi bln detection============================================================================
    Dim SCALAR_BLACK As New MCvScalar(0.0, 0.0, 0.0)
    Dim SCALAR_BLUE As New MCvScalar(255.0, 0.0, 0.0)
    Dim SCALAR_GREEN As New MCvScalar(0.0, 255.0, 0.0)
    'Dim SCALAR_RED As New MCvScalar(0.0, 0.0, 255.0)
    Dim blnFormClosing As Boolean = False

    Private rowIndex As Integer = 0
    Dim datacell As String
    Dim bukagambar As String

    '===================================Persentase Keberhasilan================================
    Dim perbedaanWaktu As Double
    Dim secondDifferent As Double
    Dim rateSuccess As Integer
    Dim dataSukses(100) As String
    Dim flagsukses(100) As Integer

    Dim dataCompare(5) As String
    Dim dataFlag(5) As Integer
    Dim jumlah100 As Integer = 0
    Dim jumlah80 As Integer = 0
    Dim jumlah75 As Integer = 0
    Dim jumlahLess As Integer = 0
    Dim licPlate As PossiblePlate
    Dim flagForLoop As Integer
    Dim captureImage As Integer = 0

    Dim plat100 As String = ""
    Dim plat80 As String = ""
    Dim plat75 As String = ""
    Dim plat60 As String = ""
    Dim platless As String = ""
    Dim platdata(20) As String
    Dim flagLoop As Integer = 0
    Dim cekSPNUAgain As Integer = 0

    '=============================================Adataptive HSV dan Nilai Threshold Adaptive===================================================
    Dim H As RangeF
    Dim S As RangeF
    Dim V As RangeF
    Dim mean As MCvScalar

    '========================================================Variable to send data==============================================================
    Public server As mainserver = New mainserver()
    Dim str As String
    Dim code As String
    Dim ipcomputer As String
    Dim stat As Integer
    Dim readon As String
    Dim senton As String
    Dim ipcamera As String
    Dim imageName As String
    Dim dataBerulang As Integer

    Public isDetechCard As Boolean = False
    Public isChangeCard As Boolean = False
    Public isAlreadySendData As Boolean = False
    Dim eliminateResult As Boolean = False

    ' ========================= VARIABEL RTG INTEGRATION (NEW) =========================
    Delegate Sub SetTextCallback(ByVal [text] As String)

    ' RTG Position Variables
    Dim rtgPosX As Double = 0        ' Gantry position (cm)
    Dim rtgPosY As Double = 0        ' Trolley position (cm)
    Dim rtgPosZ As Double = 0        ' Hoist position (cm)
    Dim rtgLoad As Single = 0        ' Load weight (ton)
    Dim rtgLockStatus As Integer = 0 ' 0=Unlock, 1=Lock
    Dim rtgAlignment As Integer = 0  ' 0=OFF, 1=ON
    Dim lastPosZ As Double = 0       ' Last hoist position

    ' RTG Control Flags
    Dim rtgCaptureFlag As Boolean = False     ' Flag A: Capture start signal
    Dim rtgOCRFinishFlag As Boolean = False   ' Flag E: OCR finish signal
    Dim rtgDataWriteFlag As Boolean = False   ' Flag B: Write to database
    Dim rtgDataReadFlag As Boolean = False    ' Flag C: Read from database
    Dim rtgDataClearFlag As Boolean = False   ' Flag D: Clear data

    ' Serial Port Configuration
    Dim myPort As Array
    Dim serialPortRTG As SerialPort
    Dim serialConnected As Boolean = False

    ' RTG Operation State Machine
    Enum RTGState
        Idle
        WaitingForPosition
        CaptureReady
        OCRProcessing
        DataWrite
        DataRead
        Complete
    End Enum
    Dim currentRTGState As RTGState = RTGState.Idle

    ' Container Position Database Variables
    Dim containerPosition As String = ""
    Dim containerWeight As String = ""
    Dim containerTimestamp As String = ""
    Dim kontainer As String
    Dim getCam As String
    'Load Form Awal
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === EXISTING OCR INITIALIZATION ===
        'Centang semua pengaturan 
        cbThreshold.Checked = True
        cbPixelAdjust.Checked = True
        'txtInputIpCam.Text = "192.168.146.123"
        'getCam = "rtsp://admin:spil12345@192.168.5.131:554/Streaming/Channels/101/"

        gettxtCam()
        getDataOffline()               '2021 comment dulu function untuk simpan data

        txtInputIpCam.Text = Mid(getCam, 24, 15)
        'Pengaturan resolusi rendah menggunakan 704*460 ==> nantinya akan diganti dengan resolusi kamera yang lebih tinggi
        If cbPixelAdjust.Checked = True Then
            MIN_RECT_AREA = 40
            MIN_CONTOUR_AREA = 30
            perubahanStruktur = 203
            MAX_DIAG_SIZE_MULTIPLE_AWAY = 6.5
            MAX_CHANGE_IN_AREA = 0.8
            JarakaraterY = 40
            Jarakarakter = 50
        End If

        NumericRecArea.Value = MIN_RECT_AREA
        NumericContour.Value = MIN_CONTOUR_AREA
        NumericThreshold.Value = perubahanStruktur
        NumericDiagSize.Value = CInt((DetectChars.MAX_DIAG_SIZE_MULTIPLE_AWAY) * 10)
        NumericMaxChange.Value = CInt((DetectChars.MAX_CHANGE_IN_AREA) * 10)
        NumericJarakYPixel.Value = CInt(DetectChars.JarakaraterY)
        NumericJarakXPixel.Value = CInt(DetectChars.Jarakarakter)

        'Database status 1
        Call aturData()

        'Bagian untuk membuka video webcam
        CvInvoke.UseOpenCL = False

        Try
            '_capture = New Capture("v3.mp4")            'Untuk capture video

            '_capture = New Capture()                    'Untuk capture from camera

            'IP camer'a get 
            _capture = New Capture(getCam)

            '_capture = New Capture("rtsp://admin:spil12345@192.168.5.131:554/Streaming/Channels/101/")

            '_capture = New Capture("rtsp://admin:spil12345@192.168.152.131:554/Streaming/Channels/102/")

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return
        End Try

        ' '''''''''''''''''''''''''''''''''''''''''Lakukan play video''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        timerVideo.Interval = CInt(1000 / 25)                           'Rubah Frame Rate 25 fps
        timerVideo.Enabled = True
        timerVideo.Start()
        ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        AddHandler Application.Idle, New EventHandler(AddressOf Me.ProcessFrame)        'Fungsi streaming video lakukan off pada play video

        cbShowSteps_CheckedChanged(New Object, New EventArgs)                           'call check box event to update form based on check box initial state

        ' '''''''''''''''''''''''''' Bagian Load Data Training KNN
        Dim blnKNNTrainingSuccessful As Boolean = loadKNNDataAndTrainKNN()              'attempt KNN training

        If (blnKNNTrainingSuccessful = False) Then                                              'if KNN training was not successful
            txtInfo.AppendText(vbCrLf + "error: KNN traning was not successful" + vbCrLf)       'show message on text box
            MsgBox("error: KNN traning was not successful")                                     'also show message box
            btnOpenTestImage.Enabled = False                                                            'disable btn start
            Return                                                                              'and bail
        End If

        btnStartWebcam.PerformClick()

        ' === NEW RTG INITIALIZATION ===
        InitializeRTGSystem()
    End Sub
    Private Sub btnOpenTestImage_Click(sender As Object, e As EventArgs) Handles btnOpenTestImage.Click

        ' test '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim drChosenFile As DialogResult

        drChosenFile = ofdOpenFile.ShowDialog()                 'open file dialog

        If (drChosenFile <> DialogResult.OK Or ofdOpenFile.FileName = "") Then
            lblChosenFile.Text = "file not chosen"              'show error message on label
            Return
        End If

    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' ========================= RTG INITIALIZATION (NEW) =========================
    Private Sub InitializeRTGSystem()
        Try
            ' Initialize serial port
            serialPortRTG = New SerialPort()
            serialPortRTG.ReadTimeout = 500
            serialPortRTG.WriteTimeout = 500

            ' Populate COM ports
            myPort = IO.Ports.SerialPort.GetPortNames()
            cmbComPort.Items.Clear()
            cmbComPort.Items.AddRange(myPort)

            ' Set default baudrate
            cmbBaudRate.Items.Clear()
            cmbBaudRate.Items.AddRange(New String() {"9600", "19200", "38400", "57600", "115200"})
            cmbBaudRate.Text = "9600"

            ' Initialize RTG display labels
            UpdateRTGDisplay()

            ' Enable RTG monitoring timer
            timerRTGMonitor.Enabled = True
            timerRTGMonitor.Interval = 100  ' 100ms monitoring cycle

            txtInfo.AppendText(vbCrLf + "RTG System initialized successfully" + vbCrLf)

        Catch ex As Exception
            txtInfo.AppendText(vbCrLf + "RTG Initialization Error: " + ex.Message + vbCrLf)
        End Try
    End Sub

    ' ========================= SERIAL PORT HANDLERS (NEW) =========================
    Private Sub btnConnectRTG_Click(sender As Object, e As EventArgs) Handles btnConnectRTG.Click
        Try
            If Not serialConnected Then
                serialPortRTG.PortName = cmbComPort.Text
                serialPortRTG.BaudRate = CInt(cmbBaudRate.Text)
                serialPortRTG.Parity = Parity.None
                serialPortRTG.DataBits = 8
                serialPortRTG.StopBits = StopBits.One

                AddHandler serialPortRTG.DataReceived, AddressOf SerialPortRTG_DataReceived

                serialPortRTG.Open()
                serialConnected = True

                btnConnectRTG.Enabled = False
                btnDisconnectRTG.Enabled = True
                btnConnectRTG.BackColor = Color.Green

                txtInfo.AppendText(vbCrLf + "RTG Serial Port Connected: " + cmbComPort.Text + vbCrLf)
            End If
        Catch ex As Exception
            MessageBox.Show("Connection Error: " + ex.Message, "RTG Connection", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtInfo.AppendText(vbCrLf + "RTG Connection Error: " + ex.Message + vbCrLf)
        End Try
    End Sub

    Private Sub btnDisconnectRTG_Click(sender As Object, e As EventArgs) Handles btnDisconnectRTG.Click
        Try
            If serialConnected Then
                serialPortRTG.Close()
                serialConnected = False

                btnConnectRTG.Enabled = True
                btnDisconnectRTG.Enabled = False
                btnConnectRTG.BackColor = SystemColors.Control

                txtInfo.AppendText(vbCrLf + "RTG Serial Port Disconnected" + vbCrLf)
            End If
        Catch ex As Exception
            MessageBox.Show("Disconnection Error: " + ex.Message, "RTG Disconnection", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SerialPortRTG_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)
        Try
            Dim receivedData As String = serialPortRTG.ReadExisting()
            ProcessRTGData(receivedData)
        Catch ex As Exception
            ' Handle receive error silently
        End Try
    End Sub

    ' ========================= RTG DATA PROCESSING (NEW) =========================
    Private Sub ProcessRTGData(ByVal data As String)
        If Me.InvokeRequired Then
            Dim callback As New SetTextCallback(AddressOf ProcessRTGData)
            Me.Invoke(callback, New Object() {data})
        Else
            Try
                If data.Length >= 28 Then
                    ' Parse RTG data according to protocol
                    ' Format: Load(2bytes) + Hoist(2bytes) + Gantry(4bytes) + Trolley(2bytes) + Lock(2bytes) + Alignment(2bytes)

                    ' Load weight (ton)
                    rtgLoad = CSng(Convert.ToInt32(data.Substring(2, 2) + data.Substring(0, 2), 16)) / 10

                    ' Hoist position (cm)
                    rtgPosZ = Convert.ToInt32(data.Substring(6, 2) + data.Substring(4, 2), 16)

                    ' Gantry position (cm)
                    rtgPosX = CDbl(Convert.ToInt32(data.Substring(14, 2) + data.Substring(12, 2) + data.Substring(10, 2) + data.Substring(8, 2), 16)) / 10

                    ' Trolley position (cm)
                    rtgPosY = Convert.ToInt32(data.Substring(18, 2) + data.Substring(16, 2), 16)

                    ' Lock status
                    rtgLockStatus = Convert.ToInt32(data.Substring(22, 2) + data.Substring(20, 2), 16)

                    ' Alignment status
                    rtgAlignment = Convert.ToInt32(data.Substring(26, 2) + data.Substring(24, 2), 16)

                    ' Update display
                    UpdateRTGDisplay()

                    ' Process RTG state machine
                    ProcessRTGStateMachine()
                End If
            Catch ex As Exception
                txtInfo.AppendText(vbCrLf + "RTG Data Parse Error: " + ex.Message + vbCrLf)
            End Try
        End If
    End Sub

    ' ========================= RTG STATE MACHINE (NEW - Following Flowchart) =========================
    Private Sub ProcessRTGStateMachine()
        Select Case currentRTGState
            Case RTGState.Idle
                ' Check if container is in position for capture
                If rtgPosY < 600 Then  ' pos Y < 6m (600cm)
                    If rtgAlignment = 1 And rtgLockStatus = 1 Then
                        ' Alignment ON and Lock ON
                        If rtgPosZ > 5000 Then  ' pos Z > 50m (5000cm)
                            ' Send Flag A: Start Capture
                            SendRTGCommand("A")
                            rtgCaptureFlag = True
                            currentRTGState = RTGState.CaptureReady
                            txtInfo.AppendText(vbCrLf + "RTG State: Capture Ready - Flag A Sent" + vbCrLf)
                        End If
                    End If
                End If

            Case RTGState.CaptureReady
                ' Wait for OCR to complete
                If rtgOCRFinishFlag Then
                    ' OCR Finished - Check Z position
                    If rtgPosZ < 5000 Then  ' pos Z < 50m
                        ' Send Flag C: Read Database
                        SendRTGCommand("C")
                        currentRTGState = RTGState.DataRead
                        txtInfo.AppendText(vbCrLf + "RTG State: Reading Database - Flag C Sent" + vbCrLf)
                    End If
                End If

            Case RTGState.DataRead
                ' Read container data and position
                ReadContainerDatabase()

                ' Check if hoist position changed
                If Math.Abs(rtgPosZ - lastPosZ) > 10 Then  ' Position changed
                    lastPosZ = rtgPosZ

                    ' Send Flag D: Clear data
                    SendRTGCommand("D")
                    currentRTGState = RTGState.DataWrite
                    txtInfo.AppendText(vbCrLf + "RTG State: Clearing Data - Flag D Sent" + vbCrLf)
                End If

            Case RTGState.DataWrite
                ' Check if in parking area
                If rtgPosY > 600 Then  ' pos Y > 6m
                    If rtgAlignment = 1 And rtgLockStatus = 0 Then  ' Alignment ON, Unlock ON
                        ' Read final weight
                        containerWeight = rtgLoad.ToString("F1")
                        containerPosition = "X:" + rtgPosX.ToString("F1") + " Y:" + rtgPosY.ToString() + " Z:" + rtgPosZ.ToString()

                        ' Send Flag B: Write to database
                        SendRTGCommand("B")
                        WriteContainerDatabase()

                        currentRTGState = RTGState.Complete
                        txtInfo.AppendText(vbCrLf + "RTG State: Data Written - Flag B Sent" + vbCrLf)
                    End If
                End If

            Case RTGState.Complete
                ' Reset flags and return to idle
                rtgCaptureFlag = False
                rtgOCRFinishFlag = False
                rtgDataWriteFlag = False
                rtgDataReadFlag = False
                rtgDataClearFlag = False
                currentRTGState = RTGState.Idle
                txtInfo.AppendText(vbCrLf + "RTG State: Complete - Ready for next container" + vbCrLf)
        End Select
    End Sub

    ' ========================= RTG COMMAND SENDER (NEW) =========================
    Private Sub SendRTGCommand(command As String)
        Try
            If serialConnected Then
                serialPortRTG.WriteLine(command)
                txtInfo.AppendText(vbCrLf + "RTG Command Sent: " + command + vbCrLf)
            End If
        Catch ex As Exception
            txtInfo.AppendText(vbCrLf + "RTG Command Error: " + ex.Message + vbCrLf)
        End Try
    End Sub

    ' ========================= RTG DISPLAY UPDATE (NEW) =========================
    Private Sub UpdateRTGDisplay()
        lblGantryPos.Text = rtgPosX.ToString("F1") + " cm"
        lblTrolleyPos.Text = rtgPosY.ToString() + " cm"
        lblHoistPos.Text = rtgPosZ.ToString() + " cm"
        lblLoadWeight.Text = rtgLoad.ToString("F1") + " ton"

        If rtgLockStatus = 1 Then
            lblLockStatus.Text = "LOCKED"
            lblLockStatus.BackColor = Color.Red
        Else
            lblLockStatus.Text = "UNLOCKED"
            lblLockStatus.BackColor = Color.Green
        End If

        If rtgAlignment = 1 Then
            lblAlignmentStatus.Text = "ON"
            lblAlignmentStatus.BackColor = Color.Green
        Else
            lblAlignmentStatus.Text = "OFF"
            lblAlignmentStatus.BackColor = Color.Gray
        End If

        ' Update trolley position indicator
        If rtgPosY < 600 Then
            lblTrolleyArea.Text = "AREA PARKING"
            lblTrolleyArea.BackColor = Color.Yellow
        Else
            lblTrolleyArea.Text = "AREA CY"
            lblTrolleyArea.BackColor = Color.LightBlue
        End If

        ' Update RTG State display
        lblRTGState.Text = "State: " + currentRTGState.ToString()
    End Sub

    ' ========================= RTG TIMER MONITOR (NEW) =========================
    Private Sub timerRTGMonitor_Tick(sender As Object, e As EventArgs) Handles timerRTGMonitor.Tick
        If serialConnected Then
            ' Update display every tick
            UpdateRTGDisplay()

            ' Check for automatic capture trigger based on RTG position
            If rtgCaptureFlag And Not _captureInProgress Then
                ' Auto-start OCR capture when RTG is ready
                If rtgPosY < 600 And rtgAlignment = 1 And rtgLockStatus = 1 Then
                    realTimerOCR.Enabled = True
                End If
            End If
        End If
    End Sub
    ' ========================= RTG DATABASE OPERATIONS (NEW) =========================
    Private Sub WriteContainerDatabase()
        Try
            If dataPlat <> "" Then
                Call koneksi()
                Dim cmdStr As String
                cmdStr = "INSERT INTO rtg_container_log (container_no, position_x, position_y, position_z, weight, timestamp, image_name) " +
                         "VALUES ('" & dataPlat & "', '" & rtgPosX.ToString() & "', '" & rtgPosY.ToString() & "', '" &
                         rtgPosZ.ToString() & "', '" & rtgLoad.ToString() & "', '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") &
                         "', '" & namafile & ".jpg')"

                cmd = New MySqlCommand(cmdStr, conn)
                cmd.ExecuteNonQuery()

                txtInfo.AppendText(vbCrLf + "RTG Data Written: " + dataPlat + " | Weight: " + rtgLoad.ToString() + " ton" + vbCrLf)
                rtgDataWriteFlag = True
            End If
        Catch ex As Exception
            txtInfo.AppendText(vbCrLf + "RTG Database Write Error: " + ex.Message + vbCrLf)
        End Try
    End Sub

    Private Sub ReadContainerDatabase()
        Try
            Call koneksi()
            Dim cmdStr As String = "SELECT * FROM rtg_container_log WHERE container_no='" & dataPlat & "' ORDER BY timestamp DESC LIMIT 1"
            cmd = New MySqlCommand(cmdStr, conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                containerPosition = "X:" + reader("position_x").ToString() + " Y:" + reader("position_y").ToString() + " Z:" + reader("position_z").ToString()
                containerWeight = reader("weight").ToString() + " ton"
                containerTimestamp = reader("timestamp").ToString()

                lblContainerInfo.Text = "Container: " + dataPlat + vbCrLf +
                                       "Position: " + containerPosition + vbCrLf +
                                       "Weight: " + containerWeight + vbCrLf +
                                       "Time: " + containerTimestamp

                rtgDataReadFlag = True
            End If
            reader.Close()
        Catch ex As Exception
            txtInfo.AppendText(vbCrLf + "RTG Database Read Error: " + ex.Message + vbCrLf)
        End Try
    End Sub


    Sub gettxtCam()
        Try
            Dim readStream As StreamReader
            readStream = New StreamReader("setting_ipcam.txt")
            Dim stringReader As String
            stringReader = readStream.ReadLine()
            'TextBox1.Text = stringReader
            getCam = stringReader
        Catch ex As Exception
            TextBox1.Text = "Tidak terbaca"
        End Try

    End Sub

    Sub processdata()

        'ambil Nilai HSV real Time
        Call ambilHSV()
        Call adaptiveThreshold()

        'Apabila NumericUpDown Memiliki Nilai lebih dari 0
        NumericRecArea.Value = MIN_RECT_AREA
        NumericContour.Value = MIN_CONTOUR_AREA
        NumericThreshold.Value = perubahanStruktur
        NumericDiagSize.Value = CInt((DetectChars.MAX_DIAG_SIZE_MULTIPLE_AWAY) * 10)
        NumericMaxChange.Value = CInt((DetectChars.MAX_CHANGE_IN_AREA) * 10)
        NumericJarakYPixel.Value = CInt(DetectChars.JarakaraterY)


        ' Ambil Data IP Komputer
        lblip.Text = getIPCom()

        gabungdata = ""
        fixPlat = ""
        txtCekDigit.Text = ""

        Dim imgTestingNumbers As Mat            'declare the input image

        Dim blnImageOpenedSuccessfully As Boolean       'attempt to open image

        If (Not _captureInProgress Or _capture Is Nothing) Then
            Try
                imgTestingNumbers = CvInvoke.Imread(ofdOpenFile.FileName, LoadImageType.Color)      'open image
                blnImageOpenedSuccessfully = True
            Catch ex As Exception                                                                   'if error occurred
                lblChosenFile.Text = "unable to open image, error: " + ex.Message                   'show error message on label
                blnImageOpenedSuccessfully = False
                'Return                                                                              'and exit function
            End Try
        ElseIf (_captureInProgress Or _capture IsNot Nothing) Then
            imgTestingNumbers = getimage
            blnImageOpenedSuccessfully = True
        End If

        lblCaptureInProgress.Text = "CaptureInProgress= " + CStr(_captureInProgress)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If (imgTestingNumbers Is Nothing) Then                          'if image could not be opened
            lblChosenFile.Text = "unable to open image"                 'show error message on label
            Return                                                      'and exit function
        End If

        If (imgTestingNumbers.IsEmpty()) Then
            lblChosenFile.Text = "unable to open image"
            Return
        End If

        If (Not blnImageOpenedSuccessfully) Then                    'if image was not opened successfully
            ibOriginal.Image = Nothing                              'set the image box on the form to blank
            Return                                                  'and bail
        End If

        lblChosenFile.Text = ofdOpenFile.FileName               'update label with file name

        CvInvoke.DestroyAllWindows()                        'close any windows that are open from previous button press

        Dim imgGrayscale As New Mat()                   '
        Dim imgBlurred As New Mat()                     'declare various images
        Dim imgThresh As New Mat()                      '
        Dim imgThreshCopy As New Mat()                  '

        If imgTestingNumbers.Height > 1080 And imgTestingNumbers.Width > 1080 Then
            CvInvoke.Resize(imgTestingNumbers, imgTestingNumbers, New Size(1080, 720), 0, 0, Inter.Linear)    'This resizes the image into your specified width and height
        End If

        ibOriginal.Image = imgTestingNumbers

        'Save imgtestingNumber ke convertImage untuk mendapatkan nilai cuaca secara real-time
        convertImage = imgTestingNumbers

        CvInvoke.CvtColor(imgTestingNumbers, imgGrayscale, ColorConversion.Bgr2Gray)        'convert to grayscale

        CvInvoke.GaussianBlur(imgGrayscale, imgBlurred, New Size(3, 3), 0)                  'blur

        'threshold image from grayscale to black and white
        CvInvoke.Threshold(imgBlurred, imgThresh, perubahanStruktur, 255.0, ThresholdType.Binary)

        'CvInvoke.Resize(imgThresh, imgThresh, New Size(1080, 720), 0, 0, Inter.Linear)    'This resizes the image into your specified width and height

        If cbShowSteps.Checked = True Then
            CvInvoke.Imshow("threshold", imgThresh)
        End If

        If cbOpenImageDatabase.Checked = False Then
            ibNomor.SizeMode = PictureBoxSizeMode.Zoom
            ibNomor.Image = imgThresh
        End If

        imgThreshCopy = imgThresh.Clone()           'make a copy of the thresh image, this in necessary b/c findContours modifies the image

        Dim contours As New VectorOfVectorOfPoint()

        'get external countours only
        CvInvoke.FindContours(imgThreshCopy, contours, Nothing, RetrType.External, ChainApproxMethod.ChainApproxSimple)

        Dim listOfContoursWithData As New List(Of ContourWithData)          'declare a list of contours with data

        'populate list of contours with data
        For i As Integer = 0 To contours.Size - 1                   'for each contour
            'Berikut ini hanyalah komentar

            '#If False Then
            'Berikut ini adalah program modifan
            Dim possibleChar As New ContourWithData(contours(i))

            If (possibleChar.intRectArea > MIN_RECT_AREA And _
                possibleChar.dblArea > MIN_CONTOUR_AREA And
                possibleChar.boundingRect.Width > MIN_PIXEL_WIDTH And possibleChar.boundingRect.Height > MIN_PIXEL_HEIGHT And _
                MIN_ASPECT_RATIO < possibleChar.dblAspectRatio And possibleChar.dblAspectRatio < MAX_ASPECT_RATIO) Then

                listOfContoursWithData.Add(possibleChar)
            End If
        Next

        'given a list of all possible chars, find groups of matching chars
        'in the next steps each group of matching chars will attempt to be recognized as a plate container
        Dim listOfListsOfMatchingCharsInScene1 As List(Of List(Of ContourWithData)) = findListOfListsOfMatchingChars(listOfContoursWithData)

        Dim imgContours As New Mat(imgTestingNumbers.Size, DepthType.Cv8U, 3)

        Dim random As New Random()
        txtInfo.AppendText("step 3 - listOfListsOfMatchingCharsInScene.Count = " + listOfListsOfMatchingCharsInScene1.Count.ToString + vbCrLf)     '13 with MCLRNF1 image

        Try
            'Program error apabila contour maksimum diberikan
            imgContours = New Mat(imgTestingNumbers.Size, DepthType.Cv8U, 3)
        Catch ex As Exception

        End Try

        For Each listOfMatchingChars As List(Of ContourWithData) In listOfListsOfMatchingCharsInScene1
            Dim intRandomBlue = random.Next(0, 256)
            Dim intRandomGreen = random.Next(0, 256)
            Dim intRandomRed = random.Next(0, 256)
            Dim contours1 As New VectorOfVectorOfPoint()

            For Each matchingChar As ContourWithData In listOfMatchingChars
                contours1.Push(matchingChar.contour)
            Next
            CvInvoke.DrawContours(imgContours, contours1, -1, New MCvScalar(CDbl(intRandomBlue), CDbl(intRandomGreen), CDbl(intRandomRed)))
        Next
        If (cbShowSteps.Checked = True) Then
            CvInvoke.Imshow("3", imgContours)
        End If

        'Lakukan Ektrak Nomor Kontainer
        Dim listOfPossiblePlates As List(Of PossiblePlate) = New List(Of PossiblePlate)         'this will be the return value

        For Each listOfMatchingChars As List(Of ContourWithData) In listOfListsOfMatchingCharsInScene1          'for each group of matching chars
            Dim possiblePlate = extractPlate(imgTestingNumbers, listOfMatchingChars)                         'attempt to extract plate

            If (Not possiblePlate.imgPlate Is Nothing) Then                                                 'if plate was found
                listOfPossiblePlates.Add(possiblePlate)                                                     'add to list of possible plates
            End If
        Next

        If (cbShowSteps.Checked = True) Then ' show steps '''''''''''''''''''''''''''''''''
            txtInfo.AppendText(vbCrLf)
            CvInvoke.Imshow("4a", imgContours)

            For i As Integer = 0 To listOfPossiblePlates.Count - 1
                Dim ptfRectPoints(4) As PointF

                ptfRectPoints = listOfPossiblePlates(i).rrLocationOfPlateInScene.GetVertices()

                Dim pt0 As New Point(CInt(ptfRectPoints(0).X), CInt(ptfRectPoints(0).Y))
                Dim pt1 As New Point(CInt(ptfRectPoints(1).X), CInt(ptfRectPoints(1).Y))
                Dim pt2 As New Point(CInt(ptfRectPoints(2).X), CInt(ptfRectPoints(2).Y))
                Dim pt3 As New Point(CInt(ptfRectPoints(3).X), CInt(ptfRectPoints(3).Y))

                CvInvoke.Line(imgContours, pt0, pt1, SCALAR_RED, 2)
                CvInvoke.Line(imgContours, pt1, pt2, SCALAR_RED, 2)
                CvInvoke.Line(imgContours, pt2, pt3, SCALAR_RED, 2)
                CvInvoke.Line(imgContours, pt3, pt0, SCALAR_RED, 2)

                CvInvoke.Imshow("4a", imgContours)
                txtInfo.AppendText("possible plate " + i.ToString + ", click on any image and press a key to continue . . ." + vbCrLf)
                CvInvoke.Imshow("4b", listOfPossiblePlates(i).imgPlate)
                CvInvoke.WaitKey(0)
            Next
            txtInfo.AppendText(vbCrLf + "plate detection complete, click on any image and press a key to begin char recognition . . ." + vbCrLf + vbCrLf)
            CvInvoke.WaitKey(0)
        End If    ' Show steps ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        listOfPossiblePlates = DetectChars.detectCharsInPlates(listOfPossiblePlates)                            'detect chars in plates

        If (listOfPossiblePlates Is Nothing) Then                                       'check if list of plates is null or zero
            txtInfo.AppendText(vbCrLf + "no container plates were detected" + vbCrLf)
        ElseIf (listOfPossiblePlates.Count = 0) Then
            txtInfo.AppendText(vbCrLf + "no container plates were detected" + vbCrLf)
        Else
            'if we get in here list of possible plates has at leat one plate

            'sort the list of possible plates in DESCENDING order (most number of chars to least number of chars)
            listOfPossiblePlates.Sort(Function(onePlate, otherPlate) otherPlate.strChars.Length.CompareTo(onePlate.strChars.Length))

            'Bedasarkan possible plates maka cari mana yang mengandung karakter SPNU yang paling banyak
            Dim nomor As Integer = 0

            If (listOfPossiblePlates.Count > 1) Then
                Dim plate(listOfPossiblePlates.Count) As PossiblePlate
                For a = 0 To listOfPossiblePlates.Count - 1
                    Dim lictPlate2 As PossiblePlate = plate(a)
                    seleksiSPNU = 0
                    lictPlate2 = listOfPossiblePlates(a)
                    dataPlat = lictPlate2.strChars
                    For b = 0 To dataPlat.Length - 1
                        cekPlate = dataPlat.ToCharArray()
                        If cekPlate(b) = "S" Or cekPlate(b) = "P" Or cekPlate(b) = "N" Or cekPlate(b) = "U" Then
                            seleksiSPNU = seleksiSPNU + 1
                        End If
                    Next
                    If seleksiSPNU >= 3 Then
                        nomor = a
                    Else
                        nomor = 0
                    End If
                Next
            Else
                licPlate = listOfPossiblePlates(nomor)
            End If

            licPlate = listOfPossiblePlates(nomor)

            If (licPlate.strChars.Length = 0) Then                          'if no chars are present in the lic plate,
                txtInfo.AppendText(vbCrLf + "no characters were detected" + licPlate.strChars + vbCrLf)     'update info text box

                'Jika tidak ada data nomor yang diketahui maka cek apakah array platsukses masih menyimpan data
                'bila menyimpan data maka tampilkan
                If flagForLoop > 0 And perbedaanWaktu > 0 Then
                    getDataBySecond()
                End If

                If loopingForNextData >= 2 Then
                    dataPlat = dataCompare(0)
                    txtNomerBenar.AppendText(vbCrLf + "Read Container= " + dataCompare(0) + vbCrLf)
                    flagLoop = 0
                End If

                'Lakukan return
                Return                                                                                      'and return
            End If

            datakarakter = licPlate.strChars
            drawRedRectangleAroundPlate(imgTestingNumbers, licPlate)                 'draw red rectangle around plate

            If (datakaraktersebelumnya <> datakarakter) Then
                'loopingOCR = 0
            End If

            'Lakukan cek tiap karakter bedasarkan jumlah karakter
            datakaraktersebelumnya = datakarakter
            cektiapkarakter()
            'Tulis seluruh karakter yang dideteksi sebagai history pembacaan karakter
            txtInfo.AppendText(vbCrLf + "Nomor yang terbaca = " + licPlate.strChars + vbCrLf)        'write license plate text to text box
            txtInfo.AppendText(vbCrLf + "----------------------------------------" + vbCrLf)

            'Lakukan seleksi tahap 1 untuk menseleksi nomer secara kasar bedasarkan jumlah huruf dan jumlah angka
            If jumlahHuruf >= 3 And jumlahHuruf < 12 And jumlahAngka >= 4 And licPlate.strChars.Length >= 9 Then

                seleksiAngka()          'Lakukan seleksi angka bedasarkan nomer yang dideteksi

                If seleksiKarakterAneh < 3 Or seleksiSPNU >= 3 Then

                    seleksiTulisanDepan()        'Setelah seleksi dilakukan, maka step 2 ==> lakukan filter data untuk menghilangkan nomer tulisan yang tidak perlu

                    txtInfo.AppendText(vbCrLf + "Filter Nomor= " + fixPlat + vbCrLf)    'Munculkan plat nomer yang telah difilter

                    seleksiNomorYangSalah()     'Seleksi Penomoran yang salah bedasarkan kemiripan huruf dan angka

                    dataMentah()
                    ' ''''''''''''''''''''''''''''''''' Cek Nomor Terakhir Kontainer'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    cekNomor()                 'Cek nomor bedasarkan gabung data dari function datayangdikirim()
                    eliminateResult = True
                    ' ''''''''''''''''''''''''''''''''' Flag untuk memasukkan nilai dalam array tiap kali looping
                    flagForLoop = flagForLoop + 1

                    getDataBySecond()           'Ambil Data Selama 4 Detik dan bandingkan hasilnya serta kirim dalam database

                Else
                    'gabungdata = datakarakter
                End If

                'Simpan Nomor
            Else
                'cek jika data masih ada sedangkan kontainer sudah hilang
                eliminateResult = False
                savedata = False
            End If

            txtOpenFile.AppendText(vbCrLf + "Flag= " + CStr(flagForLoop) + " Perbedaan= " + CStr(perbedaanWaktu) + vbCrLf)
            lblCurrentRead.Text = gabungdata

        End If
        '2021 comment dulu
        getDataOffline()
    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim counter As Integer = 0
    Sub getDataOffline()
        counter = counter + 1
        If counter > 30 Then

            If CBool(Me.server.isConnectServer()) Then
                'get data offline
                getDataFromOffile()
                'send data to server
                If anyData = True Then
                    Exit Sub
                End If

                For i = 0 To eachData - 1
                    kontainer = containerSpil(i)
                    Dim result As String = CStr(Me.server.access(kontainer, txtInputIpCam.Text, getIPCom(), dataDate(i), getTglGetStructure(), Readimagename(i), ReadRating(i)))
                    '==========================================================================> chek apakah berhasil koneksi .
                    Dim hasil As String = result.Replace(vbCr, "").Replace(vbLf, "").Replace(vbCrLf, "")
                    If hasil = Me.server.ok Then
                        Label2.Text = "OK"
                        stat = 2
                        'update
                        set2online()
                    ElseIf hasil = Me.server.notok Then
                        Label2.Text = "NOT-OK"
                        stat = 2
                        set2online()
                    ElseIf hasil = Me.server.warning Then
                        Label2.Text = "Warning"
                        stat = 2
                        set2online()
                    Else
                        stat = 1
                    End If
                Next
            Else
                'Do Nothing
            End If

            For i = 0 To eachData - 1
                containerSpil(i) = ""
                dataDate(i) = ""
                ReadRating(i) = ""
                Readimagename(i) = ""
                containerSpil(i) = Nothing
                dataDate(i) = Nothing
            Next
            counter = 0
            eachData = 0
        End If

    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub getDataBySecond()
        'gabungdata

        If gabungdata <> "" And loopingOCR = 0 Then
            dataSekarang = DateTime.Now.ToString("H:mm:ss")
            loopingOCR = 1
        End If

        'Cegah agar tidak overload
        If flagForLoop >= 80 Then
            flagForLoop = 80
        End If

        'Save data bedasarkan data yang masuk per-4 detik
        If captureImage = 0 And flagForLoop >= 1 Then           
            capturecontainer = frame
            ibCaptureContainer.Image = capturecontainer
            captureImage = 1
        End If

        'Masukkan kedalam array tiap 4 detik untuk dibandingkan hasilnya
        dataSukses(flagForLoop) = gabungdata
        flagsukses(flagForLoop) = rateSuccess

        'Bandingkan data tiap 4 detik,
        If perbedaanWaktu >= 4 Then
            CompareDataLoop()           'Bandingkan data yang didapat selama looping
            loopingOCR = 0
            captureImage = 0
            'perbedaanWaktu = 0
        End If

    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim dataPlatSebelumnya As String = ""
    Dim dataPlatSukses As Integer = 0
    Dim dataPlatSuksesSebelumnya As Integer = 0
    Sub CompareDataLoop()
        'Inisialisasi Data Awal
        plat100 = ""
        plat80 = ""
        plat75 = ""
        plat60 = ""
        platless = ""
        isChangeCard = False
        savedata = False
        'Tampilkan flagloop 
        lblFlag.Text = CStr(flagLoop)

        'Compare data dan ambil data dengan nilai rate yang paling tinggi
        For i = 0 To flagForLoop - 1
            If flagsukses(i) = 100 Then
                jumlah100 = jumlah100 + 1
                Dim cekCharAgain() As Char
                cekCharAgain = dataSukses(i).ToCharArray()
                Try
                    If cekCharAgain(3) = "U" Or cekCharAgain(3) = "J" Or cekCharAgain(3) = "Z" Then
                        plat100 = dataSukses(i)
                    Else
                        platless = dataSukses(i)
                    End If
                Catch ex As Exception

                End Try

                If plat100.Length > 11 Then
                    plat100 = ""
                    For a = 0 To 10
                        plat100 += cekCharAgain(a)
                    Next
                End If
            End If

            If flagsukses(i) = 80 Then
                jumlah80 = jumlah80 + 1
                'plat80 = dataSukses(i)
                Dim cekCharAgain() As Char
                cekCharAgain = dataSukses(i).ToCharArray()
                Try
                    If cekCharAgain(3) = "U" Or cekCharAgain(3) = "J" Or cekCharAgain(3) = "Z" Then
                        plat80 = dataSukses(i)
                    ElseIf cekCharAgain(4) = "U" Or cekCharAgain(4) = "J" Or cekCharAgain(4) = "Z" Then
                        plat75 = dataSukses(i)
                    Else
                        platless = dataSukses(i)
                    End If
                Catch ex As Exception

                End Try

                If plat80.Length > 11 Then
                    plat80 = ""
                    For a = 0 To 10
                        plat80 += cekCharAgain(a)
                    Next
                End If
            End If

            If flagsukses(i) = 75 Then
                jumlah75 = jumlah75 + 1
                plat75 = dataSukses(i)
            End If

            If flagsukses(i) < 70 Then
                jumlahLess = jumlahLess + 1
                platless = dataSukses(i)
                Dim cekCharAgain() As Char

                Try
                    If platless IsNot Nothing Then
                        cekCharAgain = dataSukses(i).ToCharArray()
                        For b = 0 To dataSukses(i).Length - 1
                            If cekCharAgain(b) = "S" Or cekCharAgain(b) = "P" Or cekCharAgain(b) = "N" Or cekCharAgain(b) = "U" Then
                                cekSPNUAgain = cekSPNUAgain + 1
                            End If
                        Next
                    End If
                Catch ex As Exception

                End Try

                If cekSPNUAgain >= 3 Then
                    plat60 = dataSukses(i)
                Else
                    platless = dataSukses(i)
                End If
            End If
        Next
        'txtNomerBenar.AppendText(vbCrLf + "CekSPNU Again " + CStr(cekSPNUAgain) + vbCrLf)
        txtInfo.AppendText(vbCrLf + "Jml100= " + CStr(jumlah100) + "Jml80= " + CStr(jumlah80) + "Jml75= " + CStr(jumlah75) + "Jml70= " + CStr(jumlahLess) + vbCrLf)

        'Tampilkan data Plat Kontainer Yang benar
        If jumlah100 > 0 And plat100 <> "" Then
            txtNomerBenar.AppendText(vbCrLf + "Plat100 Benar= " + plat100 + vbCrLf)
            dataCompare(flagLoop) = plat100
            dataPlat = plat100
            dataPlatSukses = 100
            dataFlag(flagLoop) = 100
        ElseIf jumlah80 > 0 And plat80 <> "" Then
            txtNomerBenar.AppendText(vbCrLf + "Plat80 Benar= " + plat80 + vbCrLf)
            dataCompare(flagLoop) = plat80
            dataPlat = plat80
            dataPlatSukses = 80
            dataFlag(flagLoop) = 80
        ElseIf jumlah75 > 0 And plat75 <> "" Then
            txtNomerBenar.AppendText(vbCrLf + "Plat75 Benar= " + plat75 + vbCrLf)
            dataCompare(flagLoop) = plat75
            dataPlat = plat75
            dataPlatSukses = 75
            dataFlag(flagLoop) = 75
        ElseIf jumlahLess > 0 And plat60 <> "" Then
            txtNomerBenar.AppendText(vbCrLf + "Plat60 Benar= " + plat60 + vbCrLf)
            dataCompare(flagLoop) = plat60
            dataPlat = plat60
            dataPlatSukses = 60
            dataFlag(flagLoop) = 60
        ElseIf jumlahLess > 0 And platless <> "" Then
            txtNomerBenar.AppendText(vbCrLf + "Plat50 Benar= " + platless + vbCrLf)
            dataCompare(flagLoop) = platless
            dataPlat = platless
            dataFlag(flagLoop) = 50
            dataPlatSukses = 50
            savedata = False
        End If

        'Ambil dua data, data pertama dan kedua untuk dibandingkan
        If plat100 <> "" Or plat80 <> "" Or plat75 <> "" Or plat60 <> "" Or platless <> "" Then
            flagLoop = flagLoop + 1
            ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            dataPerbandinganWaktu = DateTime.Now.ToString("H:mm:ss")                'Dan hitung apakah ada kontainer lagi yang lewat jika data yang masuk hanya satu
            loopingForNextData = 2
        End If

        If flagLoop >= 2 Then
            'Bandingkan data ke satu dan data ke dua bedasarkan presentasi nilai
            If dataFlag(0) > dataFlag(1) Then
                dataPlat = dataCompare(0)
                isChangeCard = True
                savedata = True
            ElseIf dataFlag(0) < dataFlag(1) Then
                dataPlat = dataCompare(1)
                isChangeCard = True
                savedata = True
            ElseIf dataFlag(0) = dataFlag(1) Then
                dataPlat = dataCompare(1)
                isChangeCard = False
                savedata = False
            End If

            If isChangeCard = False Then
                If dataFlag(1) = dataFlag(2) Then
                    dataPlat = dataCompare(1)
                    txtNomerBenar.AppendText(vbCrLf + "Read Container= " + dataCompare(1) + vbCrLf)
                    flagLoop = 0
                    platdata(0) = dataCompare(1)
                    platdata(1) = ""
                    isChangeCard = False
                    savedata = False
                Else
                    txtNomerBenar.AppendText(vbCrLf + "Read Container= " + dataCompare(1) + ", " + dataCompare(2) + vbCrLf)
                    flagLoop = 0
                    platdata(0) = dataCompare(1)
                    platdata(1) = dataCompare(2)
                    savedata = True
                    isChangeCard = True
                End If

            End If

            If flagLoop >= 2 Then
                If isChangeCard = True Then
                    txtNomerBenar.AppendText(vbCrLf + "Read Container= " + dataCompare(0) + ", " + dataCompare(1) + vbCrLf)
                    flagLoop = 0
                    platdata(0) = dataCompare(0)
                    platdata(1) = dataCompare(1)
                    savedata = True
                End If
            End If
        End If

        cekPlatSukses()

        lastSelection()
        cekdatayangberulang()

        If dataPlat = dataPlatSebelumnya Or dataPlat = "" Then
            savedata = False
        Else
            'savedata = True
        End If

        dataPlatSebelumnya = dataPlat
        dataPlatSuksesSebelumnya = dataPlatSukses

        ' ''''''''''''''''''''''''''''''''' Program Untuk Save Data '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If savedata = True And eliminateResult = True Then
            If (Not System.IO.Directory.Exists("C:\xampp\tomcat\webapps\ocr")) Then
                System.IO.Directory.CreateDirectory("C:\xampp\tomcat\webapps\ocr")
                file = 0
            End If

            file = file + 1
            namafile = Convert.ToString(file) + ". " + DateTime.Now.ToString("HH") + "." + DateTime.Now.ToString("mm")
            'Dim savephoto As Mat = CvInvoke.Resize()
            capturecontainer.Save("C:\xampp\tomcat\webapps\ocr\" + namafile + ".jpg")

            'Bagian untuk Insert Data
            '. . . . . . . . . . . . .
            If dataPlat <> "" Then
                Call insertdata()
            End If

            platdata(0) = ""
            platdata(1) = ""
            platdata(2) = ""
        End If

        'Hapus seluruh data untuk dipakai pada next looping selanjutnya
        For i = 0 To flagForLoop - 1
            dataSukses(flagForLoop) = ""
            flagsukses(flagForLoop) = Nothing
        Next

        flagForLoop = 0
        loopingOCR = 0
        cekSPNUAgain = 0
        perbedaanWaktu = 0
        jumlah100 = 0
        jumlah80 = 0
        jumlah75 = 0
        jumlahLess = 0
        eliminateResult = False
    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub lastSelection()
        Dim lastDetection() As Char
        Dim calculateS As Integer = 0
        Dim calculateP As Integer = 0
        Dim calculateN As Integer = 0
        Dim calculateU As Integer = 0
        Dim calculate As Integer = 0
        Dim errorPlat As Integer = 0
        Dim impossiblePlat As Integer = 0

        If dataPlat = "" Then
            savedata = False
            eliminateResult = False
            Exit Sub
        Else
            lastDetection = dataPlat.ToCharArray()
        End If

        For i = 0 To lastDetection.Length - 1
            If lastDetection(i) = "S" Then
                calculateS = calculateS + 1
                Label2.Text = CStr(calculateS)
            End If
            If lastDetection(i) = "P" Then
                calculateP = calculateP + 1
                Label2.Text = CStr(calculateP)
            End If
            If lastDetection(i) = "N" Then
                calculateN = calculateN + 1
                Label2.Text = CStr(calculateN)
            End If
            If lastDetection(i) = "U" Then
                calculateU = calculateU + 1
                Label2.Text = CStr(calculateU)
            End If

            If lastDetection(i) = "I" Or lastDetection(i) = "l" Then
                errorPlat = errorPlat + 1
            End If

            If lastDetection(i) = "1" Then
                impossiblePlat = impossiblePlat + 1
            End If
        Next

        If calculateS <> 0 And calculateP <> 0 And calculateN <> 0 And calculateU <> 0 Then
            calculate = calculateS + calculateP + calculateN + calculateU
        Else
            calculate = 0
        End If

        If calculate >= 3 And errorPlat <= 3 Then
            eliminateResult = True
            savedata = True
            Label2.Text = "Good"
        ElseIf calculate >= 3 And impossiblePlat < 6 Then
            eliminateResult = True
            savedata = True
            Label2.Text = "Good"
        Else
            eliminateResult = False
            savedata = False
            Label2.Text = "Bad"
        End If

        'If dataPlat.Length <= 9 Or dataPlat.Length >= 13 Then
        'eliminateResult = False
        'savedata = False
        'Label2.Text = "Bad"
        'End If

    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim cekkarakterPlat() As Char
    Dim cekkarakterPlatSebelumnya() As Char

    Sub cekdatayangberulang()
        Dim selisih As Integer = 0
        dataBerulang = 0
        If savedata = True And eliminateResult = True Then
            If dataPlatSebelumnya <> "" And dataPlatSuksesSebelumnya <> 0 Then
                'Compare data Now and data Before
                cekkarakterPlat = dataPlat.ToCharArray()
                cekkarakterPlatSebelumnya = dataPlatSebelumnya.ToCharArray()

                'Bandingkan dulu besar data Now dan data Before
                If dataPlat.Length > dataPlatSebelumnya.Length Then
                    selisih = (dataPlat.Length - dataPlatSebelumnya.Length) + 1
                ElseIf dataPlat.Length < dataPlatSebelumnya.Length Then
                    selisih = (dataPlatSebelumnya.Length - dataPlat.Length) + 1
                Else
                    selisih = 1
                End If

                For i = 0 To dataPlat.Length - selisih
                    If cekkarakterPlat(i) = cekkarakterPlatSebelumnya(i) Then
                        dataBerulang = dataBerulang + 1
                    End If
                Next

                Label2.Text = "SameData= " + CStr(dataBerulang)
            End If
            'Apabila karakter berulang lebih dari 9

            If dataBerulang >= 9 And dataPlatSukses <> 100 Then
                savedata = False
                eliminateResult = False
                Label2.Text = "Data sama <false>= " + CStr(dataBerulang)
            ElseIf dataBerulang >= 9 And dataPlatSukses = dataPlatSuksesSebelumnya Then
                savedata = False
                eliminateResult = False
                Label2.Text = "Data sama <false> "
            Else
                savedata = True
                eliminateResult = True
            End If

            If dataBerulang >= 9 And dataPlatSukses = 100 And dataPlatSukses <> dataPlatSuksesSebelumnya Then
                savedata = True
                eliminateResult = True
                Label2.Text = "Data <True> "
            End If

            If dataBerulang >= 9 And dataPlatSukses = 100 And dataPlatSukses = dataPlatSuksesSebelumnya Then
                savedata = False
                eliminateResult = False
                Label2.Text = "Data <false> "
            End If

            If dataBerulang >= 9 And dataPlatSuksesSebelumnya = 100 Then
                savedata = False
                eliminateResult = False
                Label2.Text = "Data <false> "
            End If

            If dataBerulang >= 8 And dataPlatSuksesSebelumnya = 100 And dataPlatSukses <> 100 Then
                savedata = False
                eliminateResult = False
                Label2.Text = "Data <false> "
            End If

            If dataBerulang >= 8 And dataPlatSuksesSebelumnya <> 100 And dataPlatSukses <> 100 Then
                savedata = False
                eliminateResult = False
                Label2.Text = "Data <false> "
            End If

            If dataBerulang >= 8 And dataPlatSuksesSebelumnya <> 100 And dataPlatSukses = 100 Then
                savedata = True
                eliminateResult = True
                Label2.Text = "Data <true> "
            End If

        End If
    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub cekPlatSukses()
        Try
            lastcekNomorKontainer()

            If dataPlatSukses = 100 Then
                If ownerkode = True And serialnumber = True And checkdigit = True Then
                    dataPlatSukses = 100
                Else
                    dataPlatSukses = 80
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub


    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub ProcessFrame(sender As Object, arg As EventArgs)

        ' Program Untuk menjalankan webcam
        ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
#If False Then

        If _captureInProgress = True Then
            'CvInvoke.Imshow("kamera", frame)
            frame = _capture.QueryFrame()
            '_capture.Retrieve(frame, 0)
            ibOriginal.Image = frame
            'ibOriginal.Image = _capture.QueryFrame()                ' ''''''''''''''''''''''''''''''''Show Webcam in ibOriginal
            getimage = frame
            'Call processdata()                                      ' ''''''''''''''''''''''''''''''''Program Untuk proses data OCR
            lblChosenFile.Text = ""
        Else
            'CvInvoke.DestroyWindow("kamera")
        End If
        'ibOriginal.Image = _capture.QueryFrame()
#End If
        Dim rotationVideo As New Mat()         'final steps are to perform the actual rotation
        Dim rotationFinal As New Mat()
        'Fungsi untuk video capture
        ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '#If False Then
        Try
            If _captureInProgress = True Then
                frame = _capture.QueryFrame()
                'Dim ptfPlateCenter As New PointF(CSng(frame.Width / 2), CSng(frame.Height / 2))
                'CvInvoke.WarpAffine(frame, frame, 90, frame.Size)

                _capture.Retrieve(frame, 0)
                'CvInvoke.GetRotationMatrix2D(ptfPlateCenter, 90, 1.0, rotationVideo)      'get the rotation matrix for our calculated correction angle
                'CvInvoke.WarpAffine(frame, rotationFinal, rotationVideo, frame.Size)          'rotate the entire image

                '_capture.Retrieve(rotationFinal, 0)
                If frame IsNot Nothing Then
                    getimage = frame
                    'getimage = rotationFinal
                    'realTimerOCR.Enabled = True
                    '_captureInProgress = True

                    ibOriginal.Image = getimage

                    'detectBlobsAndUpdateGUI()

                    'Call processdata()
                    lblChosenFile.Text = ""
                Else
                    RemoveHandler Application.Idle, New EventHandler(AddressOf Me.ProcessFrame)
                End If
            Else
                'realTimerOCR.Enabled = False
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
            'System.Windows.Forms.Application.Exit()

            System.Windows.Forms.Application.Restart()
        End Try
        '#End If
    End Sub
    ' ''''''''''''''''' Cek tiap karakter untuk mengetahui jumlah nomor dan angka ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub cektiapkarakter()
        jumlahAngka = 0
        jumlahHuruf = 0
        lblJumlah.Text = "Jumlah :"
        If datakarakter.Length > 6 Then
            cekkarakter = datakarakter.ToCharArray()
            For a = 0 To datakarakter.Length - 1

                If cekkarakter(a) = "1" Or cekkarakter(a) = "2" Or cekkarakter(a) = "3" Or cekkarakter(a) = "4" Or cekkarakter(a) = "5" Or cekkarakter(a) = "6" Or cekkarakter(a) = "7" Or cekkarakter(a) = "8" Or cekkarakter(a) = "9" Or cekkarakter(a) = "0" Then
                    jumlahAngka = jumlahAngka + 1
                End If
            Next
            jumlahHuruf = datakarakter.Length - jumlahAngka
            lblJumlah.Text = "JumlahHuruf: " + CStr(jumlahHuruf) + " ,jumlahAngka: " + CStr(jumlahAngka)
        End If
    End Sub
    ' ''''''''''''''''''''''''''''''''Seleksi nomor pada tahap pertama ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub seleksiAngka()
        'Seleksi tahap awal dilakukan seleksi bedasarkan apakah karakter mengandung "SPNU" 
        'Khusus SPNU
        seleksiSPNU = 0
        seleksiKarakterAneh = 0
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        karakter = datakarakter.ToCharArray()

        'Cek Apakah ada tulisan S P N U dalam kontainer 
        'Nantinya akan ditambahkan nomor yang akan mungkin masuk dalam depo spil

        If datakarakter.Length < 11 Then
            For a = 0 To datakarakter.Length - 1

                If karakter(a) = "S" Or karakter(a) = "P" Or karakter(a) = "N" Or karakter(a) = "U" Then
                    seleksiSPNU = seleksiSPNU + 1
                End If

                'Seleksi nomor kontainer yang tidak masuk akal
                If karakter(a) = "I" Or karakter(a) = "Q" Or karakter(a) = "Y" Then
                    seleksiKarakterAneh = seleksiKarakterAneh + 1
                End If

            Next
        ElseIf datakarakter.Length = 11 Then
            For a = 0 To datakarakter.Length - 1

                If karakter(a) = "S" Or karakter(a) = "P" Or karakter(a) = "N" Or karakter(a) = "U" Then
                    seleksiSPNU = seleksiSPNU + 1
                End If

                'Seleksi nomor kontainer yang tidak masuk akal
                If karakter(a) = "I" Or karakter(a) = "Q" Or karakter(a) = "Y" Then
                    seleksiKarakterAneh = seleksiKarakterAneh + 1
                End If

            Next
        ElseIf datakarakter.Length > 11 Then
            For a = 0 To datakarakter.Length - 1

                If karakter(a) = "S" Or karakter(a) = "P" Or karakter(a) = "N" Or karakter(a) = "U" Then
                    seleksiSPNU = seleksiSPNU + 1
                End If

                'Seleksi nomor kontainer yang tidak masuk akal
                If karakter(a) = "I" Or karakter(a) = "Q" Or karakter(a) = "Y" Then
                    seleksiKarakterAneh = seleksiKarakterAneh + 1
                End If

            Next
        End If
        Label2.Text = CStr(seleksiSPNU)
    End Sub
    ' ''''''''''''''''''''''''''''''''''Seleksi tahap Kedua untuk seleksi tulisan depan''''''''''''''''''''''''''''''''''''''''''
    Dim fixPlat2 As String = ""
    Sub seleksiTulisanDepan()
        Dim posisi As Integer = 0
        Dim del As Integer = 0

        For i = 0 To 3
            If karakter(i) = "S" Or karakter(i) = "5" Then
                posisi = i
            End If
        Next
        If posisi > 0 Then
            For i = posisi To datakarakter.Length - 1
                fixPlat += karakter(i)
            Next
        Else
            fixPlat = datakarakter
        End If

        karakter = fixPlat.ToCharArray()

        ' IMPORTANT !!!
        If seleksiSPNU >= 3 Then
            'Seleksi Nilai 0 yang sering muncul setelah SPNU
            If fixPlat.Length >= 11 Then
                If karakter(4) = "0" Then
                    del = 5
                    fixPlat = String.Empty
                    For i = 0 To 3
                        fixPlat2 += karakter(i)
                    Next
                    For i = del To fixPlat.Length - 1
                        fixPlat2 += karakter(i)
                    Next
                    karakter = fixPlat2.ToCharArray()
                    fixPlat = fixPlat2
                    Label2.Text = "Erase 0"
                End If
            Else
                karakter = fixPlat.ToCharArray()
                Label2.Text = "<11"
            End If

            If fixPlat.Length >= 11 Then

                For i = 0 To 3
                    TextBox1.Text += karakter(i)
                Next
                For i = 4 To 9
                    TextBox2.Text += karakter(i)
                Next
                For i = 10 To fixPlat.Length - 1
                    TextBox3.Text += karakter(i)
                Next
            Else
                For i = 0 To 3
                    TextBox1.Text += karakter(i)
                Next
                For i = 4 To fixPlat.Length - 1
                    TextBox2.Text += karakter(i)
                Next

            End If
            eliminateResult = True
        Else
            eliminateResult = False
        End If
    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''Seleksi Tahap ketiga untuk mengubah nomor plat yang salah ''''''''''''''''''''''''''''''
    Sub seleksiNomorYangSalah()
        karakter = fixPlat.ToCharArray()
        'lakukan cek tiap nomor 
        'Step 1 cek tulisan SPNU
        For a = 0 To 3
            If karakter(a) = "5" Then
                karakter(a) = CChar("S")
            End If

            If karakter(a) = "2" Then
                karakter(a) = CChar("S")
            End If

            If karakter(a) = "F" Then
                karakter(a) = CChar("P")
            End If

            If karakter(a) = "R" Then
                karakter(a) = CChar("P")
            End If

            If karakter(a) = "V" Then
                karakter(a) = CChar("U")
            End If

            If karakter(a) = "O" Then
                karakter(a) = CChar("U")
            End If
        Next

        'Khusus untuk karakter ke - 3 sering terjadi masalah "SPIU" , "SPXU" , "SPLU" ==> rubah jadi "SPNU"
        If karakter(0) = "S" And karakter(1) = "P" And karakter(3) = "U" Then
            If karakter(2) = "I" Or karakter(2) = "X" Or karakter(2) = "L" Or karakter(2) = "H" Or karakter(2) = "P" Or karakter(2) <> "N" Then
                karakter(2) = CChar("N")
            End If
        End If

        If karakter(0) = "S" And karakter(1) = "P" And karakter(2) = "N" Then
            If karakter(3) <> "U" Then
                karakter(3) = CChar("U")
            End If
        End If

        If karakter(0) <> "S" And karakter(1) <> "P" And karakter(2) = "N" And karakter(3) = "U" Then
            karakter(0) = CChar("S")
            karakter(1) = CChar("P")
        End If

        If karakter(0) = "S" And karakter(1) = "P" And karakter(2) <> "N" And karakter(3) <> "U" Then
            karakter(2) = CChar("N")
            karakter(3) = CChar("U")
        End If

        'Step 2 cek tulisan 6 digit tengah + digit terakhir
        For a = 4 To fixPlat.Length - 1
            If karakter(a) = "J" Then
                karakter(a) = CChar("1")
            End If

            If karakter(a) = "Z" Then
                karakter(a) = CChar("2")
            End If

            If karakter(a) = "O" Then
                karakter(a) = CChar("0")
            End If

            If karakter(a) = "I" Then
                karakter(a) = CChar("1")
            End If

            If karakter(a) = "B" Then
                karakter(a) = CChar("8")
            End If

            If karakter(a) = "T" Then
                karakter(a) = CChar("1")
            End If

            If karakter(a) = "U" Then
                karakter(a) = CChar("0")
            End If

            'Tambahan untuk karater terakhir
            If karakter(a) = "D" Then
                karakter(a) = CChar("0")
            End If

            If karakter(a) = "Q" Then
                karakter(a) = CChar("0")
            End If

            If karakter(a) = "G" Then
                karakter(a) = CChar("6")
            End If

            If karakter(a) = "A" Then
                karakter(a) = CChar("4")
            End If

            If karakter(a) = "C" Then
                karakter(a) = CChar("0")
            End If
        Next

        For i = 0 To fixPlat.Length - 1
            gabungdata += karakter(i)
        Next

    End Sub
    ' ''''''''''''''''''''''''''''''''Lakukan pengecekan tiap nomor kontainer Step keempat
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub dataMentah()

        savedata = False
        ownerkode = False
        serialnumber = False
        checkdigit = False

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        karakter = gabungdata.ToCharArray()

        If gabungdata.Length >= 11 Then
            '''''''''''''''''''''''''''''''''Cek 4 Digit Didepan''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For i = 0 To 3
                TextBox1.Text += karakter(i)
                If karakter(i) = "A" Or karakter(i) = "B" Or karakter(i) = "C" Or karakter(i) = "D" Or karakter(i) = "E" Or karakter(i) = "F" Or karakter(i) = "G" Or karakter(i) = "H" Or karakter(i) = "I" Or karakter(i) = "J" Or karakter(i) = "K" Or karakter(i) = "L" Or karakter(i) = "M" Or karakter(i) = "N" Or karakter(i) = "O" Or karakter(i) = "P" Or karakter(i) = "Q" Or karakter(i) = "R" Or karakter(i) = "S" Or karakter(i) = "T" Or karakter(i) = "U" Or karakter(i) = "V" Or karakter(i) = "W" Or karakter(i) = "X" Or karakter(i) = "Y" Or karakter(i) = "Z" Then
                    ownerkode = True
                Else
                    ownerkode = False
                End If
            Next

            ''''''''''''''''''''''''''''''''Cek 6 Digit Ditengah''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For o = 4 To 9
                ' '''''''''''''''''''''''''''''''''''' Cek tiap karakter pada serial number
                TextBox2.Text += karakter(o)
                If karakter(o) = "1" Or karakter(o) = "2" Or karakter(o) = "3" Or karakter(o) = "4" Or karakter(o) = "5" Or karakter(o) = "6" Or karakter(o) = "7" Or karakter(o) = "8" Or karakter(o) = "9" Or karakter(o) = "10" Or karakter(o) = "0" Then
                    serialnumber = True
                Else
                    serialnumber = False
                End If
            Next

            For o = 10 To gabungdata.Length - 1
                ' '''''''''''''''''''''''''''''''''''' Cek Digit terakhir 
                TextBox3.Text += karakter(o)
                If karakter(o) = "1" Or karakter(o) = "2" Or karakter(o) = "3" Or karakter(o) = "4" Or karakter(o) = "5" Or karakter(o) = "6" Or karakter(o) = "7" Or karakter(o) = "8" Or karakter(o) = "9" Or karakter(o) = "10" Or karakter(o) = "0" Then
                    checkdigit = True
                Else
                    checkdigit = False
                End If

            Next

        Else
            '''''''''''''''''''''''''''''''''Cek 4 Digit Didepan''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For i = 0 To 3
                TextBox1.Text += karakter(i)
                If karakter(i) = "A" Or karakter(i) = "B" Or karakter(i) = "C" Or karakter(i) = "D" Or karakter(i) = "E" Or karakter(i) = "F" Or karakter(i) = "G" Or karakter(i) = "H" Or karakter(i) = "I" Or karakter(i) = "J" Or karakter(i) = "K" Or karakter(i) = "L" Or karakter(i) = "M" Or karakter(i) = "N" Or karakter(i) = "O" Or karakter(i) = "P" Or karakter(i) = "Q" Or karakter(i) = "R" Or karakter(i) = "S" Or karakter(i) = "T" Or karakter(i) = "U" Or karakter(i) = "V" Or karakter(i) = "W" Or karakter(i) = "X" Or karakter(i) = "Y" Or karakter(i) = "Z" Then
                    ownerkode = True
                Else
                    ownerkode = False
                End If
            Next

            ''''''''''''''''''''''''''''''''Cek 6 Digit Ditengah''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For o = 4 To gabungdata.Length - 1
                ' '''''''''''''''''''''''''''''''''''' Cek tiap karakter pada serial number
                TextBox2.Text += karakter(o)
                If karakter(o) = "1" Or karakter(o) = "2" Or karakter(o) = "3" Or karakter(o) = "4" Or karakter(o) = "5" Or karakter(o) = "6" Or karakter(o) = "7" Or karakter(o) = "8" Or karakter(o) = "9" Or karakter(o) = "10" Or karakter(o) = "0" Then
                    serialnumber = True
                Else
                    serialnumber = False
                End If
            Next
        End If
        ''''''''''''''''''''''''''''''''''''''''''''' Save data
        'savedata = True
        ' '''''''''''''''''''''''''''''''''Ambil data waktu
        waktu = getTglGetStructure()
    End Sub


    ' ''''''''''''''''''''''''''''''''Lakukan pengecekan tiap nomor kontainer Step Akhir
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub lastcekNomorKontainer()

        ownerkode = False
        serialnumber = False
        checkdigit = False

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""

        karakter = dataPlat.ToCharArray()

        If dataPlat.Length >= 11 Then
            '''''''''''''''''''''''''''''''''Cek 4 Digit Didepan''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For i = 0 To 3
                TextBox1.Text += karakter(i)
                If karakter(i) = "A" Or karakter(i) = "B" Or karakter(i) = "C" Or karakter(i) = "D" Or karakter(i) = "E" Or karakter(i) = "F" Or karakter(i) = "G" Or karakter(i) = "H" Or karakter(i) = "I" Or karakter(i) = "J" Or karakter(i) = "K" Or karakter(i) = "L" Or karakter(i) = "M" Or karakter(i) = "N" Or karakter(i) = "O" Or karakter(i) = "P" Or karakter(i) = "Q" Or karakter(i) = "R" Or karakter(i) = "S" Or karakter(i) = "T" Or karakter(i) = "U" Or karakter(i) = "V" Or karakter(i) = "W" Or karakter(i) = "X" Or karakter(i) = "Y" Or karakter(i) = "Z" Then
                    ownerkode = True
                Else
                    ownerkode = False
                End If
            Next

            ''''''''''''''''''''''''''''''''Cek 6 Digit Ditengah''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For o = 4 To 9
                ' '''''''''''''''''''''''''''''''''''' Cek tiap karakter pada serial number
                TextBox2.Text += karakter(o)
                If karakter(o) = "1" Or karakter(o) = "2" Or karakter(o) = "3" Or karakter(o) = "4" Or karakter(o) = "5" Or karakter(o) = "6" Or karakter(o) = "7" Or karakter(o) = "8" Or karakter(o) = "9" Or karakter(o) = "10" Or karakter(o) = "0" Then
                    serialnumber = True
                Else
                    serialnumber = False
                End If
            Next

            For o = 10 To dataPlat.Length - 1
                ' '''''''''''''''''''''''''''''''''''' Cek Digit terakhir 
                TextBox3.Text += karakter(o)
                If karakter(o) = "1" Or karakter(o) = "2" Or karakter(o) = "3" Or karakter(o) = "4" Or karakter(o) = "5" Or karakter(o) = "6" Or karakter(o) = "7" Or karakter(o) = "8" Or karakter(o) = "9" Or karakter(o) = "10" Or karakter(o) = "0" Then
                    checkdigit = True
                Else
                    checkdigit = False
                End If

            Next

        Else
            '''''''''''''''''''''''''''''''''Cek 4 Digit Didepan''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For i = 0 To 3
                TextBox1.Text += karakter(i)
                If karakter(i) = "A" Or karakter(i) = "B" Or karakter(i) = "C" Or karakter(i) = "D" Or karakter(i) = "E" Or karakter(i) = "F" Or karakter(i) = "G" Or karakter(i) = "H" Or karakter(i) = "I" Or karakter(i) = "J" Or karakter(i) = "K" Or karakter(i) = "L" Or karakter(i) = "M" Or karakter(i) = "N" Or karakter(i) = "O" Or karakter(i) = "P" Or karakter(i) = "Q" Or karakter(i) = "R" Or karakter(i) = "S" Or karakter(i) = "T" Or karakter(i) = "U" Or karakter(i) = "V" Or karakter(i) = "W" Or karakter(i) = "X" Or karakter(i) = "Y" Or karakter(i) = "Z" Then
                    ownerkode = True
                Else
                    ownerkode = False
                End If
            Next

            ''''''''''''''''''''''''''''''''Cek 6 Digit Ditengah''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            For o = 4 To dataPlat.Length - 1
                ' '''''''''''''''''''''''''''''''''''' Cek tiap karakter pada serial number
                TextBox2.Text += karakter(o)
                If karakter(o) = "1" Or karakter(o) = "2" Or karakter(o) = "3" Or karakter(o) = "4" Or karakter(o) = "5" Or karakter(o) = "6" Or karakter(o) = "7" Or karakter(o) = "8" Or karakter(o) = "9" Or karakter(o) = "10" Or karakter(o) = "0" Then
                    serialnumber = True
                Else
                    serialnumber = False
                End If
            Next
        End If

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' ''''''''''''''''''''' DataJudul Pada GridView''''''''''''''''''''''''''''''''''''''''''''''
    Sub aturData()
        Try
            DataGridView1.Columns(0).Width = 50
            DataGridView1.Columns(1).Width = 50
            DataGridView1.Columns(2).Width = 20
            DataGridView1.Columns(3).Width = 150
            DataGridView1.Columns(4).Width = 150
            DataGridView1.Columns(5).Width = 150
            DataGridView1.Columns(6).Width = 100
            DataGridView1.Columns(0).HeaderText = "container"
            DataGridView1.Columns(1).HeaderText = "depo"
            DataGridView1.Columns(2).HeaderText = "stat"
            DataGridView1.Columns(3).HeaderText = "readon"
            DataGridView1.Columns(4).HeaderText = "senton"
            DataGridView1.Columns(4).HeaderText = "ipcom"
            DataGridView1.Columns(4).HeaderText = "namafile"
        Catch ex As Exception
        End Try
    End Sub
    ' ''''''''''''''''''''''''''Penekanan tombol untuk pengecekan secara manual '''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Call processdata()
        Call ambilHSV()
    End Sub

    Sub adaptiveThreshold()
        If mean.V2 <> 0 Then
            'Value dari perubahan struktur diubah didalam sini bedasarkan check box
            If cbThreshold.Checked = True Then
                'Lakukan range perubahan

                If mean.V1 > 125 Then                                       'Parameter Siang
                    If mean.V2 > 150 And mean.V2 < 165 Then
                        perubahanStruktur = 200
                    ElseIf mean.V2 > 140 And mean.V2 < 144 Then
                        perubahanStruktur = 190
                    ElseIf mean.V2 >= 144 And mean.V2 < 146 Then
                        perubahanStruktur = 194
                    ElseIf mean.V2 >= 146 And mean.V2 < 148 Then
                        perubahanStruktur = 196
                    ElseIf mean.V2 >= 148 And mean.V2 < 165 Then
                        perubahanStruktur = 197
                    ElseIf mean.V2 >= 165 And mean.V2 < 190 Then
                        perubahanStruktur = 203
                    ElseIf mean.V2 >= 190 And mean.V2 < 200 Then
                        perubahanStruktur = 205
                    ElseIf mean.V2 <= 140 Then
                        perubahanStruktur = 190

                    End If
                ElseIf mean.V1 <= 125 And mean.V1 > 120 Then                                  'Parameter Malam
                    If mean.V2 > 115 And mean.V2 < 125 Then
                        perubahanStruktur = 177
                    ElseIf mean.V2 >= 125 And mean.V2 < 130 Then
                        perubahanStruktur = 185
                    ElseIf mean.V2 >= 130 And mean.V2 < 140 Then
                        perubahanStruktur = 190
                    ElseIf mean.V2 >= 140 And mean.V2 < 150 Then
                        perubahanStruktur = 190
                    ElseIf mean.V2 >= 150 And mean.V2 < 160 Then
                        perubahanStruktur = 192
                    ElseIf mean.V2 >= 160 And mean.V2 < 180 Then
                        perubahanStruktur = 200
                    End If

                ElseIf mean.V1 <= 120 And mean.V1 >= 100 Then
                    If mean.V2 > 110 And mean.V2 < 120 Then
                        perubahanStruktur = 182
                    ElseIf mean.V2 >= 120 And mean.V2 < 130 Then
                        perubahanStruktur = 183
                    ElseIf mean.V2 >= 130 And mean.V2 < 135 Then
                        perubahanStruktur = 185
                    ElseIf mean.V2 >= 135 And mean.V2 < 140 Then
                        perubahanStruktur = 187
                    ElseIf mean.V2 >= 140 And mean.V2 < 145 Then
                        perubahanStruktur = 190
                    ElseIf mean.V2 >= 145 And mean.V2 < 150 Then
                        perubahanStruktur = 191
                    ElseIf mean.V2 >= 150 And mean.V2 < 160 Then
                        perubahanStruktur = 192
                    ElseIf mean.V2 > 160 And mean.V2 < 165 Then
                        perubahanStruktur = 203
                    ElseIf mean.V2 >= 165 And mean.V2 < 170 Then
                        perubahanStruktur = 215             ''''''''''''case vsb2
                    ElseIf mean.V2 >= 170 And mean.V2 < 175 Then
                        perubahanStruktur = 215
                    ElseIf mean.V2 >= 175 And mean.V2 < 185 Then
                        perubahanStruktur = 215
                    ElseIf mean.V2 >= 185 Then
                        perubahanStruktur = 220
                    ElseIf mean.V2 < 110 And mean.V2 > 100 Then
                        perubahanStruktur = 177
                    ElseIf mean.V2 > 80 And mean.V2 <= 100 Then
                        perubahanStruktur = 204
                    End If

                ElseIf mean.V1 < 100 And mean.V1 > 50 Then
                    If mean.V2 > 100 And mean.V2 < 115 Then
                        perubahanStruktur = 172
                    ElseIf mean.V2 >= 115 And mean.V2 < 120 Then
                        perubahanStruktur = 174
                    ElseIf mean.V2 >= 120 And mean.V2 < 125 Then
                        perubahanStruktur = 175
                    ElseIf mean.V2 >= 125 And mean.V2 < 130 Then
                        perubahanStruktur = 215 ''''''
                    ElseIf mean.V2 >= 130 And mean.V2 < 135 Then
                        perubahanStruktur = 217 ''''''
                    ElseIf mean.V2 >= 135 And mean.V2 < 140 Then
                        perubahanStruktur = 205 ''''''
                    ElseIf mean.V2 >= 140 And mean.V2 <= 145 Then
                        perubahanStruktur = 180
                    ElseIf mean.V2 > 145 And mean.V2 <= 150 Then
                        perubahanStruktur = 193
                    ElseIf mean.V2 > 150 And mean.V2 <= 155 Then
                        perubahanStruktur = 195
                    ElseIf mean.V2 > 155 And mean.V2 <= 160 Then
                        perubahanStruktur = 195
                    ElseIf mean.V2 > 160 And mean.V2 <= 165 Then
                        perubahanStruktur = 195
                    ElseIf mean.V2 > 165 And mean.V2 <= 175 Then
                        perubahanStruktur = 200
                    ElseIf mean.V2 > 175 Then
                        perubahanStruktur = 210
                    ElseIf mean.V2 < 90 Then
                        perubahanStruktur = 210
                    End If

                ElseIf mean.V1 <= 50 Then
                    If mean.V2 < 90 Then
                        perubahanStruktur = 228
                    ElseIf mean.V2 >= 90 And mean.V2 < 130 Then
                        perubahanStruktur = 230
                    ElseIf mean.V2 >= 130 Then
                        perubahanStruktur = 236
                    End If

                End If

            End If
        End If
    End Sub

    Sub ambilHSV()
        ' '''''''''''''''''''''''''''''''Coba lakukan convert get pixel RGB lalu lakukan HSV
        If ibOriginal.Image IsNot Nothing Then

            Dim hsvImg As New Mat()
            Dim copyHsv As New Mat()
            Dim centerPixel As New Point(352, 429)
            Dim size1 As New Size(300, 200)

            CvInvoke.CvtColor(ibOriginal.Image, hsvImg, ColorConversion.Bgr2Hsv)
            'Dim center As MCvScalar = CvInvoke.cvGet2D(hsvImg, 400, 400)
            CvInvoke.GetRectSubPix(hsvImg, size1, centerPixel, copyHsv)
            Dim channels As Mat() = copyHsv.Split()

            H = channels(0).GetValueRange()
            S = channels(1).GetValueRange()
            V = channels(2).GetValueRange()

            'Dim valueH As Integer = hsvImg.Data(0, 0, 0)
            'txtAsc.AppendText("HSV Gambar = " + Convert.ToString(center.V0) + " " + Convert.ToString(center.V1) + " " + Convert.ToString(center.V2) + vbCrLf)
            txtAsc.AppendText("Max H {0} Min H {1} " + Convert.ToString(H.Max) + " " + Convert.ToString(H.Min) + vbCrLf)
            txtAsc.AppendText("Max S {0} Min S {1} " + Convert.ToString(S.Max) + " " + Convert.ToString(S.Min) + vbCrLf)
            txtAsc.AppendText("Max V {0} Min V {1} " + Convert.ToString(V.Max) + " " + Convert.ToString(V.Min) + vbCrLf)

            mean = CvInvoke.Mean(copyHsv)

            txtAsc.AppendText("Mean V {0} Mean V {1} Mean {2}" + Convert.ToString(mean.V0) + " " + Convert.ToString(mean.V1) + " " + Convert.ToString(mean.V2) + vbCrLf)
            lblHSV.Text = CStr((CInt(mean.V0))) + " " + CStr((CInt(mean.V1))) + " " + CStr((CInt(mean.V2)))

        End If
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnOpenImage_Click(sender As Object, e As EventArgs) Handles btnOpenImage.Click
        If txtOpenFile.Text = "" Then
            MsgBox("Isikan Nama file dahulu")
        Else
            Try
                Dim namaGambar As String = txtOpenFile.Text
                Process.Start("C:\xampp\tomcat\webapps\ocr\" + namaGambar + ".jpg")
            Catch ex As Exception
                MsgBox(ex)
            End Try
        End If
    End Sub
    'Function For Capture
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnStartWebcam_Click(sender As Object, e As EventArgs) Handles btnStartWebcam.Click
        If (_capture IsNot Nothing) Then
            If (_captureInProgress) Then
                btnStartWebcam.Text = "Start WebCam"
                '_capture.Pause()
                realTimerOCR.Enabled = False
            Else
                btnStartWebcam.Text = "Stop Webcam"
                realTimerOCR.Enabled = True
                '_capture.Start()
            End If
            _captureInProgress = Not _captureInProgress
        End If
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnSaveImage_Click(sender As Object, e As EventArgs) Handles btnSaveImage.Click
        'saveToFile = Not saveToFile
        If CBool(Me.server.isConnectServer()) Then
            'MsgBox("insert data server")
            Dim filenamedata As String
            filenamedata = "C:\xampp\tomcat\webapps\ocr\1. 15.13.jpg"
            'Me.server.uploadfile(filenamedata)
            Dim result As String = CStr(Me.server.uploadfile(filenamedata))
            MsgBox(result)
        End If
        'C:\xampp\tomcat\webapps\ocr\1. 14.38.jpg
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        ' Cleanup RTG connection before exit
        If serialConnected Then
            Try
                serialPortRTG.Close()
            Catch ex As Exception
            End Try
        End If
        Me.Close()
        Application.Exit()
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub drawRedRectangleAroundPlate(imgOriginalScene As Mat, licPlate As PossiblePlate)
        Dim ptfRectPoints(4) As PointF                                          'declare array of 4 points, floating point type

        ptfRectPoints = licPlate.rrLocationOfPlateInScene.GetVertices()             'get 4 vertices of rotated rect

        Dim pt0 As New Point(CInt(ptfRectPoints(0).X), CInt(ptfRectPoints(0).Y))            'declare 4 points, integer type
        Dim pt1 As New Point(CInt(ptfRectPoints(1).X), CInt(ptfRectPoints(1).Y))
        Dim pt2 As New Point(CInt(ptfRectPoints(2).X), CInt(ptfRectPoints(2).Y))
        Dim pt3 As New Point(CInt(ptfRectPoints(3).X), CInt(ptfRectPoints(3).Y))

        CvInvoke.Line(imgOriginalScene, pt0, pt1, SCALAR_RED, 2)        'draw 4 red lines
        CvInvoke.Line(imgOriginalScene, pt1, pt2, SCALAR_RED, 2)
        CvInvoke.Line(imgOriginalScene, pt2, pt3, SCALAR_RED, 2)
        CvInvoke.Line(imgOriginalScene, pt3, pt0, SCALAR_RED, 2)
    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub cbShowSteps_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowSteps.CheckedChanged
        If (cbShowSteps.Checked = False) Then
            tableLayoutPanel.RowStyles.Item(1).Height = IMAGE_BOX_PCT_SHOW_STEPS_NOT_CHECKED           'if showing steps, show more of the text box
            tableLayoutPanel.RowStyles.Item(2).Height = TEXT_BOX_PCT_SHOW_STEPS_NOT_CHECKED
        ElseIf (cbShowSteps.Checked = True) Then
            tableLayoutPanel.RowStyles.Item(1).Height = IMAGE_BOX_PCT_SHOW_STEPS_CHECKED                'if not showing steps, show less of the text box
            tableLayoutPanel.RowStyles.Item(2).Height = TEXT_BOX_PCT_SHOW_STEPS_CHECKED
        End If
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub cekNomor()
        'Dim output As Integer
        'Dim data As String
        Dim bulat As Integer
        Dim hasil1 As Decimal
        Dim FirstChar As String
        Dim SecondChar As String
        Dim ThirdChar As String
        Dim ForthChar As String
        Dim FifthChar As Byte
        Dim SixthChar As Integer
        Dim SeventhChar As Integer
        Dim EighthChar As Integer
        Dim NinthChar As Integer
        Dim TenthChar As Integer
        Dim EleventhChar As Integer
        Dim First_Convert As Byte
        Dim Second_Convert As Byte
        Dim Third_Convert As Byte
        Dim Forth_Convert As Byte
        Dim Added_Value As Decimal = 0
        Dim Check_Digit As Byte = Nothing
        Dim seluruh As Integer

        rateSuccess = 0

        If gabungdata <> "" And serialnumber = True And ownerkode = True Then

            FirstChar = karakter(0)       ' Split up container number to Characters
            SecondChar = karakter(1)
            ThirdChar = karakter(2)
            ForthChar = karakter(3)

            If serialnumber = True And ownerkode = True Then
                Try
                    seluruh = Convert.ToInt32(TextBox2.Text)
                    Dim digits() As Integer = System.Array.ConvertAll(Of Char, Integer)(seluruh.ToString.ToCharArray, Function(c As Char) Integer.Parse(c.ToString))
                    FifthChar = CByte(digits(0))
                    SixthChar = CByte(digits(1))
                    SeventhChar = CByte(digits(2))
                    EighthChar = CByte(digits(3))
                    NinthChar = CByte(digits(4))
                    TenthChar = CByte(digits(5))

                Catch ex As Exception

                End Try

            End If

            ' EleventhChar = AscW(karakter(10))

            Select Case FirstChar                      ' Convert first character of prefix to a number
                Case Is = "A"
                    First_Convert = 10
                Case Is = "B"
                    First_Convert = 12
                Case Is = "C"
                    First_Convert = 13
                Case Is = "D"
                    First_Convert = 14
                Case Is = "E"
                    First_Convert = 15
                Case Is = "F"
                    First_Convert = 16
                Case Is = "G"
                    First_Convert = 17
                Case Is = "H"
                    First_Convert = 18
                Case Is = "I"
                    First_Convert = 19
                Case Is = "J"
                    First_Convert = 20
                Case Is = "K"
                    First_Convert = 21
                Case Is = "L"
                    First_Convert = 23
                Case Is = "M"
                    First_Convert = 24
                Case Is = "N"
                    First_Convert = 25
                Case Is = "O"
                    First_Convert = 26
                Case Is = "P"
                    First_Convert = 27
                Case Is = "Q"
                    First_Convert = 28
                Case Is = "R"
                    First_Convert = 29
                Case Is = "S"
                    First_Convert = 30
                Case Is = "T"
                    First_Convert = 31
                Case Is = "U"
                    First_Convert = 32
                Case Is = "V"
                    First_Convert = 34
                Case Is = "W"
                    First_Convert = 35
                Case Is = "X"
                    First_Convert = 36
                Case Is = "Y"
                    First_Convert = 37
                Case Is = "Z"
                    First_Convert = 38
            End Select

            Select Case SecondChar                        ' Convert second character of prefix to a number
                Case Is = "A"
                    Second_Convert = 10
                Case Is = "B"
                    Second_Convert = 12
                Case Is = "C"
                    Second_Convert = 13
                Case Is = "D"
                    Second_Convert = 14
                Case Is = "E"
                    Second_Convert = 15
                Case Is = "F"
                    Second_Convert = 16
                Case Is = "G"
                    Second_Convert = 17
                Case Is = "H"
                    Second_Convert = 18
                Case Is = "I"
                    Second_Convert = 19
                Case Is = "J"
                    Second_Convert = 20
                Case Is = "K"
                    Second_Convert = 21
                Case Is = "L"
                    Second_Convert = 23
                Case Is = "M"
                    Second_Convert = 24
                Case Is = "N"
                    Second_Convert = 25
                Case Is = "O"
                    Second_Convert = 26
                Case Is = "P"
                    Second_Convert = 27
                Case Is = "Q"
                    Second_Convert = 28
                Case Is = "R"
                    Second_Convert = 29
                Case Is = "S"
                    Second_Convert = 30
                Case Is = "T"
                    Second_Convert = 31
                Case Is = "U"
                    Second_Convert = 32
                Case Is = "V"
                    Second_Convert = 34
                Case Is = "W"
                    Second_Convert = 35
                Case Is = "X"
                    Second_Convert = 36
                Case Is = "Y"
                    Second_Convert = 37
                Case Is = "Z"
                    Second_Convert = 38
            End Select

            Select Case ThirdChar                         ' Convert third character of prefix to a number
                Case Is = "A"
                    Third_Convert = 10
                Case Is = "B"
                    Third_Convert = 12
                Case Is = "C"
                    Third_Convert = 13
                Case Is = "D"
                    Third_Convert = 14
                Case Is = "E"
                    Third_Convert = 15
                Case Is = "F"
                    Third_Convert = 16
                Case Is = "G"
                    Third_Convert = 17
                Case Is = "H"
                    Third_Convert = 18
                Case Is = "I"
                    Third_Convert = 19
                Case Is = "J"
                    Third_Convert = 20
                Case Is = "K"
                    Third_Convert = 21
                Case Is = "L"
                    Third_Convert = 23
                Case Is = "M"
                    Third_Convert = 24
                Case Is = "N"
                    Third_Convert = 25
                Case Is = "O"
                    Third_Convert = 26
                Case Is = "P"
                    Third_Convert = 27
                Case Is = "Q"
                    Third_Convert = 28
                Case Is = "R"
                    Third_Convert = 29
                Case Is = "S"
                    Third_Convert = 30
                Case Is = "T"
                    Third_Convert = 31
                Case Is = "U"
                    Third_Convert = 32
                Case Is = "V"
                    Third_Convert = 34
                Case Is = "W"
                    Third_Convert = 35
                Case Is = "X"
                    Third_Convert = 36
                Case Is = "Y"
                    Third_Convert = 37
                Case Is = "Z"
                    Third_Convert = 38
            End Select

            Select Case ForthChar                         ' Convert forth character of prefix to a number
                Case Is = "A"
                    Forth_Convert = 10
                Case Is = "B"
                    Forth_Convert = 12
                Case Is = "C"
                    Forth_Convert = 13
                Case Is = "D"
                    Forth_Convert = 14
                Case Is = "E"
                    Forth_Convert = 15
                Case Is = "F"
                    Forth_Convert = 16
                Case Is = "G"
                    Forth_Convert = 17
                Case Is = "H"
                    Forth_Convert = 18
                Case Is = "I"
                    Forth_Convert = 19
                Case Is = "J"
                    Forth_Convert = 20
                Case Is = "K"
                    Forth_Convert = 21
                Case Is = "L"
                    Forth_Convert = 23
                Case Is = "M"
                    Forth_Convert = 24
                Case Is = "N"
                    Forth_Convert = 25
                Case Is = "O"
                    Forth_Convert = 26
                Case Is = "P"
                    Forth_Convert = 27
                Case Is = "Q"
                    Forth_Convert = 28
                Case Is = "R"
                    Forth_Convert = 29
                Case Is = "S"
                    Forth_Convert = 30
                Case Is = "T"
                    Forth_Convert = 31
                Case Is = "U"
                    Forth_Convert = 32
                Case Is = "V"
                    Forth_Convert = 34
                Case Is = "W"
                    Forth_Convert = 35
                Case Is = "X"
                    Forth_Convert = 36
                Case Is = "Y"
                    Forth_Convert = 37
                Case Is = "Z"
                    Forth_Convert = 38
            End Select

            Added_Value = CDec((First_Convert + (Second_Convert * 2) + (Third_Convert * 4) + (Forth_Convert * 8) + (FifthChar * 16) + (SixthChar * 32) + (SeventhChar * 64) + (EighthChar * 128) + (NinthChar * 256) + (TenthChar * 512)))        'Sum multiplied values & divide by 11
            hasil1 = Added_Value / 11
            bulat = CInt(CDec(Math.Truncate(hasil1)) * 11)
            'Multiply Right of decimal by 11 and round to whole number
            'Check_Digit = CByte(Math.Round((Added_Value - Fix(Added_Value)) * 11, 1))
            Check_Digit = CByte(Added_Value - bulat)

            If Check_Digit = 10 Then Check_Digit = 0 ' Make zero if 10

            txtCekDigit.Text = "Check Digit : " + CStr(Check_Digit)
        Else
            Added_Value = 0
            txtCekDigit.Text = ""
            Check_Digit = Nothing
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''' Lakukan Perbandingan antara textbox3 dengan cekdigit
        '''''''''''''''''''''''''''''''''''''''''''''''''''' Cek data dan ambil rate Success
        txtAsc.AppendText(vbCrLf + "________________________ " + vbCrLf)
        txtAsc.AppendText("CodeOwner = " + CStr(ownerkode) + vbCrLf)
        txtAsc.AppendText("SerialNumber = " + CStr(serialnumber) + vbCrLf)
        txtAsc.AppendText("CheckDigit = " + CStr(checkdigit) + vbCrLf)

        If ownerkode = True And serialnumber = True Then

            If TextBox3.Text <> "" And checkdigit = True Then
                If CStr(karakter(10)) = CStr(Check_Digit) Then
                    rateSuccess = 100
                Else
                    rateSuccess = 80
                End If

            ElseIf TextBox3.Text <> "" And checkdigit = False Then
                ' ''''''''''''''''''''''''''Ada digit terakhir tetapi tidak angka maka
                rateSuccess = 80
                Check_Digit = 0

            ElseIf TextBox3.Text = "" And checkdigit = False Then
                rateSuccess = 80
                If (Check_Digit = Nothing) Then
                    Check_Digit = 0
                End If
                TextBox3.Text = CStr(Check_Digit)
            Else
                rateSuccess = 50

            End If
        Else
            rateSuccess = 0
            txtCekDigit.Text = ""
            Check_Digit = Nothing

        End If

        If TextBox1.Text = "" And TextBox2.Text = "" Then
            rateSuccess = 0
            txtCekDigit.Text = ""
            Check_Digit = Nothing
        End If

        If (Check_Digit <> Nothing) Then
            txtAsc.Text = CStr(rateSuccess)
        End If

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function extractPlate(imgOriginal As Mat, listOfMatchingChars As List(Of ContourWithData)) As PossiblePlate
        Dim possiblePlate As PossiblePlate = New PossiblePlate          'this will be the return value

        'sort chars from left to right based on y position
        listOfMatchingChars.Sort(Function(firstChar, secondChar) firstChar.intCenterY.CompareTo(secondChar.intCenterY))

        'calculate the center point of the plate
        Dim dblPlateCenterX As Double = CDbl(listOfMatchingChars(0).intCenterX + listOfMatchingChars(listOfMatchingChars.Count - 1).intCenterX) / 2.0
        Dim dblPlateCenterY As Double = CDbl(listOfMatchingChars(0).intCenterY + listOfMatchingChars(listOfMatchingChars.Count - 1).intCenterY) / 2.0
        dblPlateCenterY = dblPlateCenterY - (dblPlateCenterY * (1 / 4))
        Dim ptfPlateCenter As New PointF(CSng(dblPlateCenterX), CSng(dblPlateCenterY))

        'calculate plate width and height
        Dim intPlateWidth As Integer = CInt(CDbl(listOfMatchingChars(listOfMatchingChars.Count - 1).boundingRect.X + listOfMatchingChars(listOfMatchingChars.Count - 1).boundingRect.Width - listOfMatchingChars(0).boundingRect.X) * PLATE_WIDTH_PADDING_FACTOR)

        Dim intTotalOfCharHeights As Integer = 0

        For Each matchingChar As ContourWithData In listOfMatchingChars
            intTotalOfCharHeights = intTotalOfCharHeights + matchingChar.boundingRect.Height
        Next

        Dim dblAverageCharHeight = CDbl(intTotalOfCharHeights) / CDbl(listOfMatchingChars.Count)

        Dim intPlateHeight = CInt(dblAverageCharHeight * PLATE_HEIGHT_PADDING_FACTOR)

        'calculate correction angle of plate region
        Dim dblOpposite As Double = listOfMatchingChars(listOfMatchingChars.Count - 1).intCenterY - listOfMatchingChars(0).intCenterY
        Dim dblHypotenuse As Double = DetectChars.distanceBetweenChars(listOfMatchingChars(0), listOfMatchingChars(listOfMatchingChars.Count - 1))
        Dim dblCorrectionAngleInRad As Double = Math.Asin(dblOpposite / dblHypotenuse)
        Dim dblCorrectionAngleInDeg As Double = dblCorrectionAngleInRad * (0.0 / Math.PI)

        If intPlateWidth > intPlateHeight Then
            intPlateWidth = intPlateWidth - CInt(intPlateHeight * 1)
        End If


        If intPlateHeight > imgOriginal.Height Then
            intPlateHeight = CInt(imgOriginal.Height - 80)
        End If

        'txtInfo.AppendText(vbCrLf + "plateHeight= " + CStr(intPlateHeight) + " imgHeight= " + CStr(dblPlateCenterY))

        Dim rata2 = intPlateWidth - intPlateHeight

        If rata2 > 1000 Then
            intPlateWidth = 400
            intPlateHeight = 600
        End If

        If intPlateWidth < 50 Then
            intPlateWidth = 50
        End If

        'assign rotated rect member variable of possible plate
        possiblePlate.rrLocationOfPlateInScene = New RotatedRect(ptfPlateCenter, New SizeF(CSng(intPlateWidth), CSng(intPlateHeight)), CSng(dblCorrectionAngleInDeg))

        Dim rotationMatrix As New Mat()         'final steps are to perform the actual rotation
        Dim imgRotated As New Mat()
        Dim imgCropped As New Mat()

        CvInvoke.GetRotationMatrix2D(ptfPlateCenter, dblCorrectionAngleInDeg, 1.0, rotationMatrix)      'get the rotation matrix for our calculated correction angle

        CvInvoke.WarpAffine(imgOriginal, imgRotated, rotationMatrix, imgOriginal.Size)          'rotate the entire image
        'crop out the actual plate portion of the rotated image
        CvInvoke.GetRectSubPix(imgRotated, possiblePlate.rrLocationOfPlateInScene.MinAreaRect.Size, possiblePlate.rrLocationOfPlateInScene.Center, imgCropped)

        possiblePlate.imgPlate = imgCropped         'copy the cropped plate image into the applicable member variable of the possible plate

        Return possiblePlate
    End Function
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''Timer OCR'''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub realTimerOCR_Tick(sender As Object, e As EventArgs) Handles realTimerOCR.Tick
        realTimerOCR.Enabled = False

        Call processdata()

        realTimerOCR.Enabled = True
    End Sub
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function getTglGetStructure() As String

        Return DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("HH") + ":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss")

    End Function
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function getIPCom() As String
        Dim hostname As String = Dns.GetHostName()
        Return Dns.GetHostByName(hostname).AddressList(0).ToString()
    End Function
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''Show Databases'''''''''''''''''''''''''''''''''''''''''''''''''''
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnCekData_Click(sender As Object, e As EventArgs) Handles btnCekData.Click
        tampildatabase()
    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''Part Of Database Offline''''''''''''''''''''''''''''''''''''''
    Sub tampildatabase()
        Call koneksi()
        da = New MySqlDataAdapter("select container, imagename, rating, readon, stat, depo, senton, ipcom  from deteksi order by readon desc limit 200", conn)
        'da = New MySqlDataAdapter("select container, depo, stat, readon, senton from deteksi where container=('" & "SPNU282156" & "')", conn)
        ds = New DataSet
        da.Fill(ds, "deteksi")
        DataGridView1.DataSource = ds.Tables("deteksi")
    End Sub

    Dim rating As Integer
    Sub insertdata()
        code = dataPlat
        ipcomputer = getIPCom()
        stat = 1
        readon = waktu
        senton = waktu
        ipcamera = txtInputIpCam.Text
        imageName = namafile + ".jpg"
        rating = dataPlatSukses
        If code <> "" Or code IsNot Nothing Then
            Call sendData()
        End If

    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub sendData()
        'Cek apakah terhubung dengan server
        If CBool(Me.server.isConnectServer()) Then
            Dim result As String = CStr(Me.server.access(code, ipcamera, ipcomputer, readon, senton, imageName, CStr(rating)))
            '==========================================================================> chek apakah berhasil koneksi .
            Dim hasil As String = result.Replace(vbCr, "").Replace(vbLf, "").Replace(vbCrLf, "")
            If hasil = Me.server.ok Then
                Label2.Text = "OK"
                stat = 2
                Call addDataToOffile()
            ElseIf hasil = Me.server.notok Then
                Label2.Text = "NOT-OK"
                stat = 2
                Call addDataToOffile()
            ElseIf hasil = Me.server.warning Then
                Label2.Text = "Warning"
                stat = 2
                Call addDataToOffile()
            Else
                stat = 1
                Call addDataToOffile()
            End If
            'Jika tidak terhubung dengan server maka kirim ke mysql offline

            'kirim file ke server ambil function dari mainserver
            Dim filenamedata As String '"C:\xampp\tomcat\webapps\ocr\" + bukagambar
            filenamedata = "C:\xampp\tomcat\webapps\ocr\" + imageName
            Me.server.uploadfile(filenamedata)
            'Dim resultupload As String = CStr(Me.server.uploadfile(filenamedata))
            'MsgBox(resultupload)
        Else
            'MsgBox("insert data offline")
            stat = 1
            Call addDataToOffile()

        End If
    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub addDataToOffile()
        'Buka mysql offline
        Call koneksi()
        str = "insert into deteksi (container, depo, stat,readon, senton, ipcom, imagename, rating) values ('" & code & "','" & ipcomputer & "','" & stat & "', '" & readon & "', '" & senton & "', '" & ipcamera & "' , '" & imageName & "', '" & rating & "')"
        cmd = New MySqlCommand(str, conn)
        cmd.ExecuteNonQuery()
    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim eachData As Integer = 0
    Dim containerSpil(100) As String
    Dim dataDate(100) As String
    Dim anyData As Boolean = False
    Dim Readimagename(100) As String
    Dim ReadRating(100) As String
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub getDataFromOffile()
        Call koneksi()
        da = New MySqlDataAdapter("select * from deteksi where stat='1'", conn)
        ds = New DataSet
        da.Fill(ds, "deteksi")

        If ds.Tables("deteksi").Rows.Count = 0 Then
            anyData = True
            Exit Sub
        Else
            anyData = False
        End If

        Try
            For Each pRow As DataRow In ds.Tables("deteksi").Rows
                txtInfo.AppendText("data= " + CStr(pRow("container")) + " " + CStr(eachData) + " ")
                containerSpil(eachData) = CStr(pRow("container"))
                dataDate(eachData) = CStr(pRow("readon"))
                Readimagename(eachData) = CStr(pRow("imagename"))
                ReadRating(eachData) = CStr(pRow("rating"))
                eachData = eachData + 1
                If eachData >= 99 Then
                    eachData = 0
                    Exit Sub
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub set2online()
        Call koneksi()
        str = "update deteksi set stat='2' where container=('" & kontainer & "')"
        cmd = New MySqlCommand(str, conn)
        cmd.ExecuteNonQuery()
    End Sub
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Call koneksi()
        str = "update deteksi set container=('" & TextBox2.Text & "') where container=('" & TextBox1.Text & "')"
        cmd = New MySqlCommand(str, conn)
        cmd.ExecuteNonQuery()

        ds = New DataSet
        da.Fill(ds, "deteksi")
        DataGridView1.DataSource = ds.Tables("deteksi")
    End Sub

    Private Sub btnManual_Click(sender As Object, e As EventArgs) Handles btnManual.Click
        If gabungdata <> Nothing Then
            Call koneksi()
            Dim str As String
            Dim data1 As String = gabungdata
            Dim data2 As String = "Japva"
            Dim data3 As String = "1"
            Dim data4 As String = waktu
            Dim data5 As String = waktu
            Dim data6 As String = getIPCom()
            Dim data7 As String = namafile
            str = "insert into deteksi (container, depo, stat,readon, senton, ipcom, imagename) values ('" & data1 & "','" & data2 & "','" & data3 & "', '" & data4 & "', '" & data5 & "' , '" & data6 & "' , '" & data7 & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.Columns(e.ColumnIndex).Name = "imagename" Then
            Try
                bukagambar = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString()
                If bukagambar <> "" Then
                    lblImageName.Text = bukagambar
                    'MsgBox(bukagambar)
                    Process.Start("C:\xampp\tomcat\webapps\ocr\" + bukagambar)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)               
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        If e.Button = MouseButtons.Right Then

            Try
                datacell = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
                lblImageName.Text = datacell
                Me.DataGridView1.Rows(e.RowIndex).Selected = True

                Me.rowIndex = e.RowIndex

                Me.DataGridView1.CurrentCell = Me.DataGridView1.Rows(e.RowIndex).Cells(1)

                Me.ContextMenuStrip1.Show(Me.DataGridView1, e.Location)

                ContextMenuStrip1.Show(Cursor.Position)
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub ContextMenuStrip1_Click(sender As Object, e As EventArgs) Handles ContextMenuStrip1.Click
        If Not Me.DataGridView1.Rows(Me.rowIndex).IsNewRow Then
            Try
                'Delete Data From File
                My.Computer.FileSystem.DeleteFile("C:\xampp\tomcat\webapps\ocr\" + bukagambar, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin)
            Catch ex As Exception
            End Try

            'Delete Data From MySql
            Call koneksi()
            Dim str As String
            str = "delete from deteksi where readon=('" & datacell & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            Me.DataGridView1.Rows.RemoveAt(Me.rowIndex)

        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim ini As Integer = e.ColumnIndex
        If ini <> -1 Then
            If DataGridView1.Columns(e.ColumnIndex).Name = "container" Then
                Try
                    Dim container As String
                    container = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
                    TextBox1.Text = container
                Catch ex As Exception

                End Try
            ElseIf DataGridView1.Columns(e.ColumnIndex).Name = "imagename" Then
                Try
                    Dim openInib As Mat
                    Dim ImageFilename As String
                    bukagambar = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString()

                    If bukagambar <> "" Then
                        lblImageName.Text = bukagambar
                        ImageFilename = "C:\xampp\tomcat\webapps\ocr\" + bukagambar
                        'Process.Start("C:\xampp\tomcat\webapps\ocr\" + bukagambar + ".jpg")
                        openInib = CvInvoke.Imread(ImageFilename, LoadImageType.Color)      'open image
                        If cbOpenImageDatabase.Checked = True Then
                            ibNomor.SizeMode = PictureBoxSizeMode.StretchImage
                            ibNomor.Image = openInib
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If

        'TextBox2.Text = CStr(ini)
    End Sub
    ' '''''''''''''''''''''''''''''''''''''Bagian untuk mendeteksi pergerakan menggunakan moving detection,
    ' '''''''''''''''''''''''''''''''''''''Note, perlu dilakukan trial saat dilapangan,
    ' '''''''''''''''''''''''''''''''''''''
    Sub detectBlobsAndUpdateGUI()

        Dim imgFrame1 As Mat
        Dim imgFrame2 As Mat
        Dim blnFirstFrame As Boolean = True
        imgFrame1 = getimage
        imgFrame2 = getimage

        While (blnFormClosing = False)

            Dim blobs As New List(Of Blob)

            Dim imgFrame1Copy As Mat = imgFrame1.Clone()
            Dim imgFrame2Copy As Mat = imgFrame2.Clone()

            Dim imgDifference As New Mat(imgFrame1.Size, DepthType.Cv8U, 1)
            Dim imgThresh As New Mat(imgFrame1.Size, DepthType.Cv8U, 1)

            CvInvoke.CvtColor(imgFrame1Copy, imgFrame1Copy, ColorConversion.Bgr2Gray)
            CvInvoke.CvtColor(imgFrame2Copy, imgFrame2Copy, ColorConversion.Bgr2Gray)

            CvInvoke.GaussianBlur(imgFrame1Copy, imgFrame1Copy, New Size(5, 5), 0)
            CvInvoke.GaussianBlur(imgFrame2Copy, imgFrame2Copy, New Size(5, 5), 0)

            CvInvoke.AbsDiff(imgFrame1Copy, imgFrame2Copy, imgDifference)

            CvInvoke.Threshold(imgDifference, imgThresh, 30, 255.0, ThresholdType.Binary)

            'CvInvoke.Imshow("imgThresh", imgThresh)

            Dim structuringElement3x3 As Mat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, New Size(3, 3), New Point(-1, -1))
            Dim structuringElement5x5 As Mat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, New Size(5, 5), New Point(-1, -1))
            Dim structuringElement7x7 As Mat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, New Size(7, 7), New Point(-1, -1))
            Dim structuringElement9x9 As Mat = CvInvoke.GetStructuringElement(ElementShape.Rectangle, New Size(9, 9), New Point(-1, -1))

            CvInvoke.Dilate(imgThresh, imgThresh, structuringElement5x5, New Point(-1, -1), 1, BorderType.Default, New MCvScalar(0, 0, 0))
            CvInvoke.Dilate(imgThresh, imgThresh, structuringElement5x5, New Point(-1, -1), 1, BorderType.Default, New MCvScalar(0, 0, 0))
            CvInvoke.Erode(imgThresh, imgThresh, structuringElement5x5, New Point(-1, -1), 1, BorderType.Default, New MCvScalar(0, 0, 0))

            Dim imgThreshCopy As Mat = imgThresh.Clone()

            Dim contours As New VectorOfVectorOfPoint()

            CvInvoke.FindContours(imgThreshCopy, contours, Nothing, RetrType.External, ChainApproxMethod.ChainApproxSimple)

            Dim imgContours As New Mat(imgThresh.Size, DepthType.Cv8U, 3)

            'CvInvoke.DrawContours(imgContours, contours, -1, SCALAR_WHITE, -1)

            'CvInvoke.Imshow("imgContours", imgContours)

            Dim convexHulls As New VectorOfVectorOfPoint(contours.Size())

            For i As Integer = 0 To contours.Size() - 1
                CvInvoke.ConvexHull(contours(i), convexHulls(i))
            Next

            For i As Integer = 0 To convexHulls.Size() - 1

                Dim possibleBlob As New Blob(convexHulls(i))

                If (possibleBlob.intRectArea > 100 And _
                    possibleBlob.dblAspectRatio >= 0.2 And _
                    possibleBlob.dblAspectRatio <= 1.2 And _
                    possibleBlob.boundingRect.Width > 15 And _
                    possibleBlob.boundingRect.Height > 20 And _
                    possibleBlob.dblDiagonalSize > 30.0) Then
                    blobs.Add(possibleBlob)
                End If

            Next

            Dim imgConvexHulls As New Mat(imgThresh.Size, DepthType.Cv8U, 3)

            convexHulls = New VectorOfVectorOfPoint()              're-instiantate contours since contours.Clear() does not seem to work as expected

            For Each blob As Blob In blobs
                convexHulls.Push(blob.contour)
            Next

            'CvInvoke.DrawContours(imgConvexHulls, convexHulls, -1, SCALAR_WHITE, -1)

            'CvInvoke.Imshow("imgConvexHulls", imgConvexHulls)
            imgFrame2Copy = imgFrame2.Clone()           'get another copy of frame 2 since we changed the previous frame 2 copy in the processing above

            For Each blob As Blob In blobs                                              'for each blob

                'frame minimal untuk container 600 * 700
                If blob.boundingRect.Width > 300 And blob.boundingRect.Height > 400 Then
                    CvInvoke.Rectangle(imgFrame2Copy, blob.boundingRect, SCALAR_RED, 2)             'draw a red box around the blob
                    CvInvoke.Circle(imgFrame2Copy, blob.centerPosition, 3, SCALAR_GREEN, -1)        'draw a filled-in green circle at the center
                End If

            Next

            ibOriginal.Image = imgFrame2Copy
            'now we prepare for the next iteration

            imgFrame1 = imgFrame2.Clone()                   'move frame 1 up to where frame 2 is

            If (_capture.GetCaptureProperty(CapProp.PosFrames) + 1 < _capture.GetCaptureProperty(CapProp.FrameCount)) Then      'if there is at least one more frame
                imgFrame2 = _capture.QueryFrame()               'get the next frame
            Else                                                'else if there is not at least one more frame
                txtInfo.AppendText("end of video")              'show end of video message
                Exit While                                      'and jump out of while loop
            End If

            Application.DoEvents()
            blnFirstFrame = False

        End While
    End Sub

    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '====================================================Pengaturan Control==============================================================
    Private Sub NumericThreshold_ValueChanged(sender As Object, e As EventArgs) Handles NumericThreshold.ValueChanged
        perubahanStruktur = CInt(NumericThreshold.Value)

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If
    End Sub

    Private Sub NumericRecArea_ValueChanged(sender As Object, e As EventArgs) Handles NumericRecArea.ValueChanged
        MIN_RECT_AREA = CInt(NumericRecArea.Value)

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If
    End Sub

    Private Sub NumericContour_ValueChanged(sender As Object, e As EventArgs) Handles NumericContour.ValueChanged
        MIN_CONTOUR_AREA = CInt(NumericContour.Value)

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If

    End Sub

    Private Sub NumericJarakYPixel_ValueChanged(sender As Object, e As EventArgs) Handles NumericJarakYPixel.ValueChanged
        JarakaraterY = CDbl(NumericJarakYPixel.Value)

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If

    End Sub

    Private Sub NumericMaxChange_ValueChanged(sender As Object, e As EventArgs) Handles NumericMaxChange.ValueChanged
        DetectChars.MAX_CHANGE_IN_AREA = CDbl(NumericMaxChange.Value) / 10

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If
    End Sub

    Private Sub NumericDiagSize_ValueChanged(sender As Object, e As EventArgs) Handles NumericDiagSize.ValueChanged
        DetectChars.MAX_DIAG_SIZE_MULTIPLE_AWAY = CDbl(NumericDiagSize.Value) / 10

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If
    End Sub
    Private Sub NumericJarakXPixel_ValueChanged(sender As Object, e As EventArgs) Handles NumericJarakXPixel.ValueChanged
        DetectChars.Jarakarakter = CDbl(NumericJarakXPixel.Value)

        'Panggil ProcessData apabila dalam bentuk foto
        If (Not _captureInProgress Or _capture Is Nothing) Then
            Call processdata()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        MAX_CHANGE_IN_AREA = 0.8
        MAX_DIAG_SIZE_MULTIPLE_AWAY = 6.5
        JarakaraterY = 35
        perubahanStruktur = 205
        MIN_RECT_AREA = 40
        MIN_CONTOUR_AREA = 40
        Jarakarakter = 65

        NumericContour.Value = MIN_CONTOUR_AREA
        NumericRecArea.Value = MIN_RECT_AREA
        NumericJarakYPixel.Value = CInt(JarakaraterY)
        NumericMaxChange.Value = CInt(MAX_CHANGE_IN_AREA * 10)
        NumericDiagSize.Value = CInt(MAX_DIAG_SIZE_MULTIPLE_AWAY * 10)
        NumericThreshold.Value = perubahanStruktur
        NumericJarakXPixel.Value = CInt(Jarakarakter)

    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function getSecondChangeCard(datecompare As String) As Double
        '========================================> ambil informasi data sekarang .
        Dim hourNow As Double = 0.0
        Dim minuteNow As Double = 0.0
        Dim secondNow As Double = 0.0
        '=======================================> get data .
        datenows = DateTime.Now.ToString("H:mm:ss").Split(CChar(":"))
        hourNow = CDbl(datenows(0))
        minuteNow = CDbl(datenows(1))
        secondNow = CDbl(datenows(2))
        '======================================> extrak data jam .
        Dim hourCmpr As Double = 0.0
        Dim minuteCmpr As Double = 0.0
        Dim secondCmpr As Double = 0.0
        '=======================================> get data .
        Dim dateCmpr() As String = datecompare.Split(CChar(":"))
        hourCmpr = CDbl(dateCmpr(0))
        minuteCmpr = CDbl(dateCmpr(1))
        secondCmpr = CDbl(dateCmpr(2))

        '======================================> return data .
        Return ((Math.Abs(hourNow - hourCmpr) * 3600.0) + (Math.Abs(minuteNow - minuteCmpr) * 60.0) + (Math.Abs(secondNow - secondCmpr)))
    End Function
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub RealTimeDeteksi_Tick(sender As Object, e As EventArgs) Handles RealTimeDeteksi.Tick

        If loopingOCR >= 1 Then
            perbedaanWaktu = getSecondChangeCard(dataSekarang)
        End If

        If loopingForNextData > 0 Then
            secondDifferent = getSecondChangeCard(dataPerbandinganWaktu)
        End If

        If secondDifferent >= 4 Then
            loopingForNextData = 2
            secondDifferent = 0
        End If

        'Bandingkan data tiap 4 detik,
        If perbedaanWaktu >= 4 And flagForLoop <> 0 Then
            getDataBySecond()
            'CompareDataLoop()           'Bandingkan data yang didapat selama looping
            loopingOCR = 0
            'perbedaanWaktu = 0
        End If

        If loopingForNextData >= 2 Then
            dataPlat = dataCompare(0)
            txtNomerBenar.AppendText(vbCrLf + "Read Container= " + dataCompare(0) + vbCrLf)
            flagLoop = 0
            loopingForNextData = 0
        End If

    End Sub

    Private Sub timerVideo_Tick(sender As Object, e As EventArgs) Handles timerVideo.Tick
        Dim rotationVideo As New Mat()         'final steps are to perform the actual rotation
        Dim rotationFinal As New Mat()

        If _captureInProgress = True Then
            frame = _capture.QueryFrame()
            '_capture.Retrieve(frame, 0)
            'Dim ptfPlateCenter As New PointF(CSng(frame.Width / 2), CSng(frame.Height / 2))
            'CvInvoke.GetRotationMatrix2D(ptfPlateCenter, 90, 1.0, rotationVideo)      'get the rotation matrix for our calculated correction angle
            'CvInvoke.WarpAffine(frame, rotationFinal, rotationVideo, frame.Size)          'rotate the entire image

            If frame IsNot Nothing Then
                getimage = frame
                'getimage = rotationFinal
                'realTimerOCR.Enabled = True
                '_captureInProgress = True
                ibOriginal.Image = getimage

                'detectBlobsAndUpdateGUI()
                'Call processdata()
                lblChosenFile.Text = ""

            End If
        Else
            'realTimerOCR.Enabled = False
        End If

    End Sub
    ' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub ibOriginal_MouseMove(sender As Object, e As MouseEventArgs) Handles ibOriginal.MouseMove
        Dim cursorX As Integer = e.X
        Dim curisorY As Integer = e.Y

        Label2.Text = "X = " + Convert.ToString(cursorX) + " Y =" + Convert.ToString(curisorY)
    End Sub

    Private Sub btnSendata_Click(sender As Object, e As EventArgs) Handles btnSendata.Click
        If CBool(Me.server.isConnectServer()) Then
            Dim result As String = CStr(Me.server.access("00COBA1000", "11KIRIM1111", getIPCom(), getTglGetStructure(), getTglGetStructure(), imageName, CStr(rating)))
            '==========================================================================> chek apakah berhasil koneksi .
            Dim hasil As String = result.Replace(vbCr, "").Replace(vbLf, "").Replace(vbCrLf, "")
            If hasil = Me.server.ok Then
                Label2.Text = "OK"
            ElseIf hasil = Me.server.notok Then
                Label2.Text = "NOT-OK"
            ElseIf hasil = Me.server.warning Then
                Label2.Text = "Warning"
            End If
        End If


    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
