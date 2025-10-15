<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.btnOpenTestImage = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblJudul = New System.Windows.Forms.Label()
        Me.gbListData = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblText2 = New System.Windows.Forms.Label()
        Me.lblText1 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.txtCekDigit = New System.Windows.Forms.TextBox()
        Me.lblImageName = New System.Windows.Forms.Label()
        Me.lblip = New System.Windows.Forms.Label()
        Me.lblJumlah = New System.Windows.Forms.Label()
        Me.gbCek = New System.Windows.Forms.GroupBox()
        Me.lblInputIPCam = New System.Windows.Forms.Label()
        Me.txtInputIpCam = New System.Windows.Forms.TextBox()
        Me.btnSendata = New System.Windows.Forms.Button()
        Me.lblHSV = New System.Windows.Forms.Label()
        Me.lblFlag = New System.Windows.Forms.Label()
        Me.txtOpenFile = New System.Windows.Forms.TextBox()
        Me.btnCekData = New System.Windows.Forms.Button()
        Me.lblCaptureInProgress = New System.Windows.Forms.Label()
        Me.btnOpenImage = New System.Windows.Forms.Button()
        Me.ibOriginal = New Emgu.CV.UI.ImageBox()
        Me.ibNomor = New Emgu.CV.UI.ImageBox()
        Me.lblCurrentRead = New System.Windows.Forms.Label()
        Me.cbShowSteps = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.lblChosenFile = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtAsc = New System.Windows.Forms.TextBox()
        Me.GbSetting = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NumericJarakXPixel = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericJarakYPixel = New System.Windows.Forms.NumericUpDown()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NumericContour = New System.Windows.Forms.NumericUpDown()
        Me.NumericRecArea = New System.Windows.Forms.NumericUpDown()
        Me.NumericMaxChange = New System.Windows.Forms.NumericUpDown()
        Me.NumericDiagSize = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NumericThreshold = New System.Windows.Forms.NumericUpDown()
        Me.txtNomerBenar = New System.Windows.Forms.TextBox()
        Me.ibCaptureContainer = New Emgu.CV.UI.ImageBox()
        Me.gbUpdate = New System.Windows.Forms.GroupBox()
        Me.cbPixelAdjust = New System.Windows.Forms.CheckBox()
        Me.cbThreshold = New System.Windows.Forms.CheckBox()
        Me.btnStartWebcam = New System.Windows.Forms.Button()
        Me.btnSaveImage = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbOpenImageDatabase = New System.Windows.Forms.CheckBox()
        Me.gbRTGControl = New System.Windows.Forms.GroupBox()
        Me.lblRTGConnection = New System.Windows.Forms.Label()
        Me.lblComPort = New System.Windows.Forms.Label()
        Me.cmbComPort = New System.Windows.Forms.ComboBox()
        Me.lblBaudRate = New System.Windows.Forms.Label()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.btnConnectRTG = New System.Windows.Forms.Button()
        Me.btnDisconnectRTG = New System.Windows.Forms.Button()
        Me.gbRTGStatus = New System.Windows.Forms.GroupBox()
        Me.lblGantryLabel = New System.Windows.Forms.Label()
        Me.lblGantryPos = New System.Windows.Forms.Label()
        Me.lblTrolleyLabel = New System.Windows.Forms.Label()
        Me.lblTrolleyPos = New System.Windows.Forms.Label()
        Me.lblHoistLabel = New System.Windows.Forms.Label()
        Me.lblHoistPos = New System.Windows.Forms.Label()
        Me.lblLoadLabel = New System.Windows.Forms.Label()
        Me.lblLoadWeight = New System.Windows.Forms.Label()
        Me.lblLockLabel = New System.Windows.Forms.Label()
        Me.lblLockStatus = New System.Windows.Forms.Label()
        Me.lblAlignmentLabel = New System.Windows.Forms.Label()
        Me.lblAlignmentStatus = New System.Windows.Forms.Label()
        Me.lblTrolleyAreaLabel = New System.Windows.Forms.Label()
        Me.lblTrolleyArea = New System.Windows.Forms.Label()
        Me.lblRTGStateLabel = New System.Windows.Forms.Label()
        Me.lblRTGState = New System.Windows.Forms.Label()
        Me.lblContainerInfo = New System.Windows.Forms.Label()
        Me.ofdOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.realTimerOCR = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RealTimeDeteksi = New System.Windows.Forms.Timer(Me.components)
        Me.timerVideo = New System.Windows.Forms.Timer(Me.components)
        Me.timerRTGMonitor = New System.Windows.Forms.Timer(Me.components)
        Me.tableLayoutPanel.SuspendLayout()
        Me.gbListData.SuspendLayout()
        Me.gbCek.SuspendLayout()
        CType(Me.ibOriginal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ibNomor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbSetting.SuspendLayout()
        CType(Me.NumericJarakXPixel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericJarakYPixel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericContour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericRecArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericMaxChange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericDiagSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ibCaptureContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbUpdate.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbRTGControl.SuspendLayout()
        Me.gbRTGStatus.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tableLayoutPanel
        '
        Me.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control
        Me.tableLayoutPanel.ColumnCount = 7
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.54386!))
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.92982!))
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.54386!))
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579!))
        Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290.0!))
        Me.tableLayoutPanel.Controls.Add(Me.btnStart, 0, 4)
        Me.tableLayoutPanel.Controls.Add(Me.txtInfo, 0, 3)
        Me.tableLayoutPanel.Controls.Add(Me.btnOpenTestImage, 0, 1)
        Me.tableLayoutPanel.Controls.Add(Me.Panel1, 0, 0)
        Me.tableLayoutPanel.Controls.Add(Me.Panel2, 1, 0)
        Me.tableLayoutPanel.Controls.Add(Me.lblJudul, 2, 0)
        Me.tableLayoutPanel.Controls.Add(Me.gbListData, 3, 2)
        Me.tableLayoutPanel.Controls.Add(Me.gbCek, 4, 2)
        Me.tableLayoutPanel.Controls.Add(Me.ibOriginal, 0, 2)
        Me.tableLayoutPanel.Controls.Add(Me.ibNomor, 5, 2)
        Me.tableLayoutPanel.Controls.Add(Me.lblCurrentRead, 5, 1)
        Me.tableLayoutPanel.Controls.Add(Me.cbShowSteps, 4, 1)
        Me.tableLayoutPanel.Controls.Add(Me.DataGridView1, 2, 3)
        Me.tableLayoutPanel.Controls.Add(Me.lblChosenFile, 1, 1)
        Me.tableLayoutPanel.Controls.Add(Me.btnExit, 4, 0)
        Me.tableLayoutPanel.Controls.Add(Me.txtAsc, 5, 4)
        Me.tableLayoutPanel.Controls.Add(Me.GbSetting, 4, 3)
        Me.tableLayoutPanel.Controls.Add(Me.txtNomerBenar, 5, 0)
        Me.tableLayoutPanel.Controls.Add(Me.ibCaptureContainer, 3, 0)
        Me.tableLayoutPanel.Controls.Add(Me.gbUpdate, 4, 4)
        Me.tableLayoutPanel.Controls.Add(Me.btnStartWebcam, 1, 4)
        Me.tableLayoutPanel.Controls.Add(Me.btnSaveImage, 2, 4)
        Me.tableLayoutPanel.Controls.Add(Me.GroupBox1, 3, 4)
        Me.tableLayoutPanel.Controls.Add(Me.gbRTGControl, 6, 0)
        Me.tableLayoutPanel.Controls.Add(Me.gbRTGStatus, 6, 2)
        Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.tableLayoutPanel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tableLayoutPanel.Name = "tableLayoutPanel"
        Me.tableLayoutPanel.RowCount = 5
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.3354!))
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.6646!))
        Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tableLayoutPanel.Size = New System.Drawing.Size(1867, 825)
        Me.tableLayoutPanel.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStart.ForeColor = System.Drawing.Color.White
        Me.btnStart.Location = New System.Drawing.Point(5, 754)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(217, 32)
        Me.btnStart.TabIndex = 50
        Me.btnStart.Text = "Start OCR"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'txtInfo
        '
        Me.tableLayoutPanel.SetColumnSpan(Me.txtInfo, 2)
        Me.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInfo.Location = New System.Drawing.Point(5, 660)
        Me.txtInfo.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtInfo.Size = New System.Drawing.Size(453, 51)
        Me.txtInfo.TabIndex = 2
        Me.txtInfo.WordWrap = False
        '
        'btnOpenTestImage
        '
        Me.btnOpenTestImage.Location = New System.Drawing.Point(4, 234)
        Me.btnOpenTestImage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnOpenTestImage.Name = "btnOpenTestImage"
        Me.btnOpenTestImage.Size = New System.Drawing.Size(100, 28)
        Me.btnOpenTestImage.TabIndex = 51
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Location = New System.Drawing.Point(5, 43)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(217, 143)
        Me.Panel1.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Location = New System.Drawing.Point(232, 78)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(226, 73)
        Me.Panel2.TabIndex = 14
        '
        'lblJudul
        '
        Me.lblJudul.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJudul.AutoSize = True
        Me.lblJudul.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJudul.Location = New System.Drawing.Point(468, 97)
        Me.lblJudul.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblJudul.Name = "lblJudul"
        Me.lblJudul.Size = New System.Drawing.Size(286, 36)
        Me.lblJudul.TabIndex = 15
        Me.lblJudul.Text = "System OCR SPIL"
        '
        'gbListData
        '
        Me.gbListData.Controls.Add(Me.Label2)
        Me.gbListData.Controls.Add(Me.lblText2)
        Me.gbListData.Controls.Add(Me.lblText1)
        Me.gbListData.Controls.Add(Me.btnUpdate)
        Me.gbListData.Controls.Add(Me.TextBox1)
        Me.gbListData.Controls.Add(Me.TextBox2)
        Me.gbListData.Controls.Add(Me.TextBox3)
        Me.gbListData.Controls.Add(Me.btnManual)
        Me.gbListData.Controls.Add(Me.txtCekDigit)
        Me.gbListData.Controls.Add(Me.lblImageName)
        Me.gbListData.Controls.Add(Me.lblip)
        Me.gbListData.Controls.Add(Me.lblJumlah)
        Me.gbListData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbListData.Location = New System.Drawing.Point(764, 274)
        Me.gbListData.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gbListData.Name = "gbListData"
        Me.gbListData.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gbListData.Size = New System.Drawing.Size(226, 376)
        Me.gbListData.TabIndex = 39
        Me.gbListData.TabStop = False
        Me.gbListData.Text = "Data List"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(92, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 16)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "List Container"
        '
        'lblText2
        '
        Me.lblText2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblText2.AutoSize = True
        Me.lblText2.Location = New System.Drawing.Point(224, 110)
        Me.lblText2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblText2.Name = "lblText2"
        Me.lblText2.Size = New System.Drawing.Size(60, 16)
        Me.lblText2.TabIndex = 21
        Me.lblText2.Text = "dUpdate"
        Me.lblText2.Visible = False
        '
        'lblText1
        '
        Me.lblText1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblText1.AutoSize = True
        Me.lblText1.Location = New System.Drawing.Point(224, 57)
        Me.lblText1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblText1.Name = "lblText1"
        Me.lblText1.Size = New System.Drawing.Size(72, 16)
        Me.lblText1.TabIndex = 20
        Me.lblText1.Text = "dContainer"
        Me.lblText1.Visible = False
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnUpdate.BackColor = System.Drawing.Color.Aqua
        Me.btnUpdate.Location = New System.Drawing.Point(52, 272)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(58, 41)
        Me.btnUpdate.TabIndex = 19
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBox1.Location = New System.Drawing.Point(-10, 54)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(175, 22)
        Me.TextBox1.TabIndex = 13
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBox2.Location = New System.Drawing.Point(-10, 107)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(175, 22)
        Me.TextBox2.TabIndex = 16
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBox3.Location = New System.Drawing.Point(-8, 160)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(173, 22)
        Me.TextBox3.TabIndex = 14
        '
        'btnManual
        '
        Me.btnManual.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnManual.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnManual.BackColor = System.Drawing.Color.Lime
        Me.btnManual.Location = New System.Drawing.Point(52, 219)
        Me.btnManual.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(58, 44)
        Me.btnManual.TabIndex = 18
        Me.btnManual.Text = "Manual Input"
        Me.btnManual.UseVisualStyleBackColor = False
        '
        'txtCekDigit
        '
        Me.txtCekDigit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtCekDigit.Location = New System.Drawing.Point(-5, 347)
        Me.txtCekDigit.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtCekDigit.Name = "txtCekDigit"
        Me.txtCekDigit.Size = New System.Drawing.Size(169, 22)
        Me.txtCekDigit.TabIndex = 24
        '
        'lblImageName
        '
        Me.lblImageName.AutoSize = True
        Me.lblImageName.Location = New System.Drawing.Point(47, 514)
        Me.lblImageName.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblImageName.Name = "lblImageName"
        Me.lblImageName.Size = New System.Drawing.Size(88, 16)
        Me.lblImageName.TabIndex = 46
        Me.lblImageName.Text = "image name :"
        '
        'lblip
        '
        Me.lblip.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblip.AutoSize = True
        Me.lblip.Location = New System.Drawing.Point(47, 440)
        Me.lblip.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblip.Name = "lblip"
        Me.lblip.Size = New System.Drawing.Size(57, 16)
        Me.lblip.TabIndex = 45
        Me.lblip.Text = "IP Addr :"
        '
        'lblJumlah
        '
        Me.lblJumlah.AutoSize = True
        Me.lblJumlah.Location = New System.Drawing.Point(49, 598)
        Me.lblJumlah.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblJumlah.Name = "lblJumlah"
        Me.lblJumlah.Size = New System.Drawing.Size(56, 16)
        Me.lblJumlah.TabIndex = 35
        Me.lblJumlah.Text = "Jumlah :"
        '
        'gbCek
        '
        Me.gbCek.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbCek.Controls.Add(Me.lblInputIPCam)
        Me.gbCek.Controls.Add(Me.txtInputIpCam)
        Me.gbCek.Controls.Add(Me.btnSendata)
        Me.gbCek.Controls.Add(Me.lblHSV)
        Me.gbCek.Controls.Add(Me.lblFlag)
        Me.gbCek.Controls.Add(Me.txtOpenFile)
        Me.gbCek.Controls.Add(Me.btnCekData)
        Me.gbCek.Controls.Add(Me.lblCaptureInProgress)
        Me.gbCek.Controls.Add(Me.btnOpenImage)
        Me.gbCek.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCek.Location = New System.Drawing.Point(1000, 274)
        Me.gbCek.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gbCek.Name = "gbCek"
        Me.gbCek.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gbCek.Size = New System.Drawing.Size(215, 376)
        Me.gbCek.TabIndex = 40
        Me.gbCek.TabStop = False
        '
        'lblInputIPCam
        '
        Me.lblInputIPCam.AutoSize = True
        Me.lblInputIPCam.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInputIPCam.Location = New System.Drawing.Point(49, 121)
        Me.lblInputIPCam.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblInputIPCam.Name = "lblInputIPCam"
        Me.lblInputIPCam.Size = New System.Drawing.Size(111, 18)
        Me.lblInputIPCam.TabIndex = 49
        Me.lblInputIPCam.Text = "Input Ip Camera"
        Me.lblInputIPCam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInputIpCam
        '
        Me.txtInputIpCam.Location = New System.Drawing.Point(48, 149)
        Me.txtInputIpCam.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtInputIpCam.Name = "txtInputIpCam"
        Me.txtInputIpCam.Size = New System.Drawing.Size(175, 22)
        Me.txtInputIpCam.TabIndex = 48
        '
        'btnSendata
        '
        Me.btnSendata.Location = New System.Drawing.Point(75, 578)
        Me.btnSendata.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnSendata.Name = "btnSendata"
        Me.btnSendata.Size = New System.Drawing.Size(144, 39)
        Me.btnSendata.TabIndex = 47
        Me.btnSendata.Text = "Send Data"
        Me.btnSendata.UseVisualStyleBackColor = True
        Me.btnSendata.Visible = False
        '
        'lblHSV
        '
        Me.lblHSV.AutoSize = True
        Me.lblHSV.Location = New System.Drawing.Point(55, 58)
        Me.lblHSV.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblHSV.Name = "lblHSV"
        Me.lblHSV.Size = New System.Drawing.Size(57, 16)
        Me.lblHSV.TabIndex = 23
        Me.lblHSV.Text = "C.Value:"
        '
        'lblFlag
        '
        Me.lblFlag.AutoSize = True
        Me.lblFlag.Location = New System.Drawing.Point(23, 203)
        Me.lblFlag.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblFlag.Name = "lblFlag"
        Me.lblFlag.Size = New System.Drawing.Size(83, 16)
        Me.lblFlag.TabIndex = 22
        Me.lblFlag.Text = "FlagLooping"
        '
        'txtOpenFile
        '
        Me.txtOpenFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOpenFile.Location = New System.Drawing.Point(15, 320)
        Me.txtOpenFile.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtOpenFile.Multiline = True
        Me.txtOpenFile.Name = "txtOpenFile"
        Me.txtOpenFile.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOpenFile.Size = New System.Drawing.Size(196, 77)
        Me.txtOpenFile.TabIndex = 21
        '
        'btnCekData
        '
        Me.btnCekData.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCekData.BackColor = System.Drawing.Color.Yellow
        Me.btnCekData.Location = New System.Drawing.Point(28, 201)
        Me.btnCekData.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnCekData.Name = "btnCekData"
        Me.btnCekData.Size = New System.Drawing.Size(176, 41)
        Me.btnCekData.TabIndex = 19
        Me.btnCekData.Text = "Cek Data"
        Me.btnCekData.UseVisualStyleBackColor = False
        '
        'lblCaptureInProgress
        '
        Me.lblCaptureInProgress.AutoSize = True
        Me.lblCaptureInProgress.Location = New System.Drawing.Point(37, 546)
        Me.lblCaptureInProgress.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCaptureInProgress.Name = "lblCaptureInProgress"
        Me.lblCaptureInProgress.Size = New System.Drawing.Size(60, 16)
        Me.lblCaptureInProgress.TabIndex = 20
        Me.lblCaptureInProgress.Text = "Capture: "
        '
        'btnOpenImage
        '
        Me.btnOpenImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnOpenImage.Location = New System.Drawing.Point(47, 265)
        Me.btnOpenImage.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnOpenImage.Name = "btnOpenImage"
        Me.btnOpenImage.Size = New System.Drawing.Size(135, 44)
        Me.btnOpenImage.TabIndex = 0
        Me.btnOpenImage.Text = "Open Image"
        Me.btnOpenImage.UseVisualStyleBackColor = True
        '
        'ibOriginal
        '
        Me.ibOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tableLayoutPanel.SetColumnSpan(Me.ibOriginal, 3)
        Me.ibOriginal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ibOriginal.Enabled = False
        Me.ibOriginal.Location = New System.Drawing.Point(5, 274)
        Me.ibOriginal.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ibOriginal.Name = "ibOriginal"
        Me.ibOriginal.Size = New System.Drawing.Size(749, 376)
        Me.ibOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ibOriginal.TabIndex = 41
        Me.ibOriginal.TabStop = False
        '
        'ibNomor
        '
        Me.ibNomor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ibNomor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ibNomor.Enabled = False
        Me.ibNomor.Location = New System.Drawing.Point(1225, 274)
        Me.ibNomor.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ibNomor.Name = "ibNomor"
        Me.ibNomor.Size = New System.Drawing.Size(345, 376)
        Me.ibNomor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ibNomor.TabIndex = 42
        Me.ibNomor.TabStop = False
        '
        'lblCurrentRead
        '
        Me.lblCurrentRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrentRead.AutoSize = True
        Me.lblCurrentRead.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentRead.Location = New System.Drawing.Point(1225, 235)
        Me.lblCurrentRead.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblCurrentRead.Name = "lblCurrentRead"
        Me.lblCurrentRead.Size = New System.Drawing.Size(345, 29)
        Me.lblCurrentRead.TabIndex = 38
        Me.lblCurrentRead.Text = "Data : "
        Me.lblCurrentRead.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbShowSteps
        '
        Me.cbShowSteps.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbShowSteps.AutoSize = True
        Me.cbShowSteps.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbShowSteps.Location = New System.Drawing.Point(1000, 235)
        Me.cbShowSteps.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbShowSteps.Name = "cbShowSteps"
        Me.cbShowSteps.Size = New System.Drawing.Size(215, 29)
        Me.cbShowSteps.TabIndex = 37
        Me.cbShowSteps.Text = "Show Steps"
        Me.cbShowSteps.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.tableLayoutPanel.SetColumnSpan(Me.DataGridView1, 2)
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(468, 660)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(522, 51)
        Me.DataGridView1.TabIndex = 44
        '
        'lblChosenFile
        '
        Me.lblChosenFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblChosenFile.AutoSize = True
        Me.tableLayoutPanel.SetColumnSpan(Me.lblChosenFile, 2)
        Me.lblChosenFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChosenFile.Location = New System.Drawing.Point(232, 237)
        Me.lblChosenFile.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblChosenFile.Name = "lblChosenFile"
        Me.lblChosenFile.Size = New System.Drawing.Size(522, 25)
        Me.lblChosenFile.TabIndex = 1
        Me.lblChosenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnExit.BackColor = System.Drawing.Color.DarkRed
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1000, 184)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(215, 41)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'txtAsc
        '
        Me.txtAsc.Location = New System.Drawing.Point(1225, 721)
        Me.txtAsc.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtAsc.Multiline = True
        Me.txtAsc.Name = "txtAsc"
        Me.txtAsc.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAsc.Size = New System.Drawing.Size(345, 64)
        Me.txtAsc.TabIndex = 43
        '
        'GbSetting
        '
        Me.tableLayoutPanel.SetColumnSpan(Me.GbSetting, 2)
        Me.GbSetting.Controls.Add(Me.Label8)
        Me.GbSetting.Controls.Add(Me.NumericJarakXPixel)
        Me.GbSetting.Controls.Add(Me.Label7)
        Me.GbSetting.Controls.Add(Me.NumericJarakYPixel)
        Me.GbSetting.Controls.Add(Me.btnReset)
        Me.GbSetting.Controls.Add(Me.Label6)
        Me.GbSetting.Controls.Add(Me.Label5)
        Me.GbSetting.Controls.Add(Me.Label4)
        Me.GbSetting.Controls.Add(Me.Label3)
        Me.GbSetting.Controls.Add(Me.NumericContour)
        Me.GbSetting.Controls.Add(Me.NumericRecArea)
        Me.GbSetting.Controls.Add(Me.NumericMaxChange)
        Me.GbSetting.Controls.Add(Me.NumericDiagSize)
        Me.GbSetting.Controls.Add(Me.Label1)
        Me.GbSetting.Controls.Add(Me.NumericThreshold)
        Me.GbSetting.Location = New System.Drawing.Point(1000, 660)
        Me.GbSetting.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GbSetting.Name = "GbSetting"
        Me.GbSetting.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GbSetting.Size = New System.Drawing.Size(570, 50)
        Me.GbSetting.TabIndex = 51
        Me.GbSetting.TabStop = False
        Me.GbSetting.Text = "Setting"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 62)
        Me.Label8.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 16)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "JarakX:"
        '
        'NumericJarakXPixel
        '
        Me.NumericJarakXPixel.Location = New System.Drawing.Point(79, 57)
        Me.NumericJarakXPixel.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericJarakXPixel.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.NumericJarakXPixel.Name = "NumericJarakXPixel"
        Me.NumericJarakXPixel.Size = New System.Drawing.Size(76, 22)
        Me.NumericJarakXPixel.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(165, 66)
        Me.Label7.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 16)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "JarakY:"
        '
        'NumericJarakYPixel
        '
        Me.NumericJarakYPixel.Location = New System.Drawing.Point(255, 60)
        Me.NumericJarakYPixel.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericJarakYPixel.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.NumericJarakYPixel.Name = "NumericJarakYPixel"
        Me.NumericJarakYPixel.Size = New System.Drawing.Size(76, 22)
        Me.NumericJarakYPixel.TabIndex = 11
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(47, 17)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(80, 34)
        Me.btnReset.TabIndex = 10
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(503, 64)
        Me.Label6.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 16)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Max.Change:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(521, 31)
        Me.Label5.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Diag. Size:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(335, 66)
        Me.Label4.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Contour:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(331, 27)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "RecArea:"
        '
        'NumericContour
        '
        Me.NumericContour.Location = New System.Drawing.Point(421, 60)
        Me.NumericContour.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericContour.Maximum = New Decimal(New Integer() {250, 0, 0, 0})
        Me.NumericContour.Name = "NumericContour"
        Me.NumericContour.Size = New System.Drawing.Size(76, 22)
        Me.NumericContour.TabIndex = 5
        '
        'NumericRecArea
        '
        Me.NumericRecArea.Location = New System.Drawing.Point(421, 25)
        Me.NumericRecArea.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericRecArea.Maximum = New Decimal(New Integer() {250, 0, 0, 0})
        Me.NumericRecArea.Name = "NumericRecArea"
        Me.NumericRecArea.Size = New System.Drawing.Size(76, 22)
        Me.NumericRecArea.TabIndex = 4
        '
        'NumericMaxChange
        '
        Me.NumericMaxChange.Location = New System.Drawing.Point(624, 59)
        Me.NumericMaxChange.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericMaxChange.Name = "NumericMaxChange"
        Me.NumericMaxChange.Size = New System.Drawing.Size(76, 22)
        Me.NumericMaxChange.TabIndex = 3
        '
        'NumericDiagSize
        '
        Me.NumericDiagSize.Location = New System.Drawing.Point(624, 22)
        Me.NumericDiagSize.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericDiagSize.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.NumericDiagSize.Name = "NumericDiagSize"
        Me.NumericDiagSize.Size = New System.Drawing.Size(76, 22)
        Me.NumericDiagSize.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(149, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Threshold:"
        '
        'NumericThreshold
        '
        Me.NumericThreshold.Location = New System.Drawing.Point(252, 25)
        Me.NumericThreshold.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.NumericThreshold.Maximum = New Decimal(New Integer() {250, 0, 0, 0})
        Me.NumericThreshold.Name = "NumericThreshold"
        Me.NumericThreshold.Size = New System.Drawing.Size(76, 22)
        Me.NumericThreshold.TabIndex = 0
        '
        'txtNomerBenar
        '
        Me.txtNomerBenar.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNomerBenar.Location = New System.Drawing.Point(1225, 44)
        Me.txtNomerBenar.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.txtNomerBenar.Multiline = True
        Me.txtNomerBenar.Name = "txtNomerBenar"
        Me.txtNomerBenar.ReadOnly = True
        Me.txtNomerBenar.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNomerBenar.Size = New System.Drawing.Size(345, 141)
        Me.txtNomerBenar.TabIndex = 52
        '
        'ibCaptureContainer
        '
        Me.ibCaptureContainer.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ibCaptureContainer.BackColor = System.Drawing.SystemColors.Control
        Me.ibCaptureContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ibCaptureContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ibCaptureContainer.Enabled = False
        Me.ibCaptureContainer.Location = New System.Drawing.Point(764, 44)
        Me.ibCaptureContainer.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ibCaptureContainer.Name = "ibCaptureContainer"
        Me.ibCaptureContainer.Size = New System.Drawing.Size(226, 141)
        Me.ibCaptureContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ibCaptureContainer.TabIndex = 2
        Me.ibCaptureContainer.TabStop = False
        '
        'gbUpdate
        '
        Me.gbUpdate.Controls.Add(Me.cbPixelAdjust)
        Me.gbUpdate.Controls.Add(Me.cbThreshold)
        Me.gbUpdate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbUpdate.Location = New System.Drawing.Point(1000, 721)
        Me.gbUpdate.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gbUpdate.Name = "gbUpdate"
        Me.gbUpdate.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.gbUpdate.Size = New System.Drawing.Size(215, 99)
        Me.gbUpdate.TabIndex = 53
        Me.gbUpdate.TabStop = False
        '
        'cbPixelAdjust
        '
        Me.cbPixelAdjust.AutoSize = True
        Me.cbPixelAdjust.Location = New System.Drawing.Point(21, 42)
        Me.cbPixelAdjust.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbPixelAdjust.Name = "cbPixelAdjust"
        Me.cbPixelAdjust.Size = New System.Drawing.Size(135, 20)
        Me.cbPixelAdjust.TabIndex = 54
        Me.cbPixelAdjust.Text = "Set. Pixel Rendah"
        Me.cbPixelAdjust.UseVisualStyleBackColor = True
        '
        'cbThreshold
        '
        Me.cbThreshold.AutoSize = True
        Me.cbThreshold.Location = New System.Drawing.Point(21, 17)
        Me.cbThreshold.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbThreshold.Name = "cbThreshold"
        Me.cbThreshold.Size = New System.Drawing.Size(144, 20)
        Me.cbThreshold.TabIndex = 53
        Me.cbThreshold.Text = "AdaptiveThreshold"
        Me.cbThreshold.UseVisualStyleBackColor = True
        '
        'btnStartWebcam
        '
        Me.btnStartWebcam.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnStartWebcam.BackColor = System.Drawing.Color.Blue
        Me.btnStartWebcam.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartWebcam.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btnStartWebcam.Location = New System.Drawing.Point(232, 751)
        Me.btnStartWebcam.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnStartWebcam.Name = "btnStartWebcam"
        Me.btnStartWebcam.Size = New System.Drawing.Size(215, 39)
        Me.btnStartWebcam.TabIndex = 48
        Me.btnStartWebcam.Text = "Start Webcam"
        Me.btnStartWebcam.UseVisualStyleBackColor = False
        '
        'btnSaveImage
        '
        Me.btnSaveImage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnSaveImage.Location = New System.Drawing.Point(468, 753)
        Me.btnSaveImage.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnSaveImage.Name = "btnSaveImage"
        Me.btnSaveImage.Size = New System.Drawing.Size(161, 34)
        Me.btnSaveImage.TabIndex = 47
        Me.btnSaveImage.Text = "Save Image"
        Me.btnSaveImage.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbOpenImageDatabase)
        Me.GroupBox1.Location = New System.Drawing.Point(764, 721)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(226, 80)
        Me.GroupBox1.TabIndex = 54
        Me.GroupBox1.TabStop = False
        '
        'cbOpenImageDatabase
        '
        Me.cbOpenImageDatabase.AutoSize = True
        Me.cbOpenImageDatabase.Location = New System.Drawing.Point(33, 17)
        Me.cbOpenImageDatabase.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbOpenImageDatabase.Name = "cbOpenImageDatabase"
        Me.cbOpenImageDatabase.Size = New System.Drawing.Size(166, 20)
        Me.cbOpenImageDatabase.TabIndex = 0
        Me.cbOpenImageDatabase.Text = "Open Image Database"
        Me.cbOpenImageDatabase.UseVisualStyleBackColor = True
        '
        'gbRTGControl
        '
        Me.gbRTGControl.Controls.Add(Me.lblRTGConnection)
        Me.gbRTGControl.Controls.Add(Me.lblComPort)
        Me.gbRTGControl.Controls.Add(Me.cmbComPort)
        Me.gbRTGControl.Controls.Add(Me.lblBaudRate)
        Me.gbRTGControl.Controls.Add(Me.cmbBaudRate)
        Me.gbRTGControl.Controls.Add(Me.btnConnectRTG)
        Me.gbRTGControl.Controls.Add(Me.btnDisconnectRTG)
        Me.gbRTGControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRTGControl.Location = New System.Drawing.Point(1579, 4)
        Me.gbRTGControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbRTGControl.Name = "gbRTGControl"
        Me.gbRTGControl.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbRTGControl.Size = New System.Drawing.Size(284, 222)
        Me.gbRTGControl.TabIndex = 100
        Me.gbRTGControl.TabStop = False
        Me.gbRTGControl.Text = "RTG Connection"
        '
        'lblRTGConnection
        '
        Me.lblRTGConnection.AutoSize = True
        Me.lblRTGConnection.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblRTGConnection.ForeColor = System.Drawing.Color.Red
        Me.lblRTGConnection.Location = New System.Drawing.Point(13, 25)
        Me.lblRTGConnection.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRTGConnection.Name = "lblRTGConnection"
        Me.lblRTGConnection.Size = New System.Drawing.Size(133, 20)
        Me.lblRTGConnection.TabIndex = 0
        Me.lblRTGConnection.Text = "Not Connected"
        '
        'lblComPort
        '
        Me.lblComPort.AutoSize = True
        Me.lblComPort.Location = New System.Drawing.Point(13, 55)
        Me.lblComPort.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComPort.Name = "lblComPort"
        Me.lblComPort.Size = New System.Drawing.Size(67, 16)
        Me.lblComPort.TabIndex = 1
        Me.lblComPort.Text = "COM Port:"
        '
        'cmbComPort
        '
        Me.cmbComPort.FormattingEnabled = True
        Me.cmbComPort.Location = New System.Drawing.Point(13, 75)
        Me.cmbComPort.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbComPort.Name = "cmbComPort"
        Me.cmbComPort.Size = New System.Drawing.Size(235, 24)
        Me.cmbComPort.TabIndex = 2
        '
        'lblBaudRate
        '
        Me.lblBaudRate.AutoSize = True
        Me.lblBaudRate.Location = New System.Drawing.Point(13, 111)
        Me.lblBaudRate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBaudRate.Name = "lblBaudRate"
        Me.lblBaudRate.Size = New System.Drawing.Size(74, 16)
        Me.lblBaudRate.TabIndex = 3
        Me.lblBaudRate.Text = "Baud Rate:"
        '
        'cmbBaudRate
        '
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Location = New System.Drawing.Point(13, 130)
        Me.cmbBaudRate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbBaudRate.Name = "cmbBaudRate"
        Me.cmbBaudRate.Size = New System.Drawing.Size(235, 24)
        Me.cmbBaudRate.TabIndex = 4
        '
        'btnConnectRTG
        '
        Me.btnConnectRTG.BackColor = System.Drawing.Color.Green
        Me.btnConnectRTG.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnConnectRTG.ForeColor = System.Drawing.Color.White
        Me.btnConnectRTG.Location = New System.Drawing.Point(13, 166)
        Me.btnConnectRTG.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnConnectRTG.Name = "btnConnectRTG"
        Me.btnConnectRTG.Size = New System.Drawing.Size(113, 43)
        Me.btnConnectRTG.TabIndex = 5
        Me.btnConnectRTG.Text = "Connect"
        Me.btnConnectRTG.UseVisualStyleBackColor = False
        '
        'btnDisconnectRTG
        '
        Me.btnDisconnectRTG.BackColor = System.Drawing.Color.Red
        Me.btnDisconnectRTG.Enabled = False
        Me.btnDisconnectRTG.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDisconnectRTG.ForeColor = System.Drawing.Color.White
        Me.btnDisconnectRTG.Location = New System.Drawing.Point(136, 166)
        Me.btnDisconnectRTG.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnDisconnectRTG.Name = "btnDisconnectRTG"
        Me.btnDisconnectRTG.Size = New System.Drawing.Size(113, 43)
        Me.btnDisconnectRTG.TabIndex = 6
        Me.btnDisconnectRTG.Text = "Disconnect"
        Me.btnDisconnectRTG.UseVisualStyleBackColor = False
        '
        'gbRTGStatus
        '
        Me.gbRTGStatus.Controls.Add(Me.lblGantryLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblGantryPos)
        Me.gbRTGStatus.Controls.Add(Me.lblTrolleyLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblTrolleyPos)
        Me.gbRTGStatus.Controls.Add(Me.lblHoistLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblHoistPos)
        Me.gbRTGStatus.Controls.Add(Me.lblLoadLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblLoadWeight)
        Me.gbRTGStatus.Controls.Add(Me.lblLockLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblLockStatus)
        Me.gbRTGStatus.Controls.Add(Me.lblAlignmentLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblAlignmentStatus)
        Me.gbRTGStatus.Controls.Add(Me.lblTrolleyAreaLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblTrolleyArea)
        Me.gbRTGStatus.Controls.Add(Me.lblRTGStateLabel)
        Me.gbRTGStatus.Controls.Add(Me.lblRTGState)
        Me.gbRTGStatus.Controls.Add(Me.lblContainerInfo)
        Me.gbRTGStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRTGStatus.Location = New System.Drawing.Point(1579, 273)
        Me.gbRTGStatus.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbRTGStatus.Name = "gbRTGStatus"
        Me.gbRTGStatus.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.gbRTGStatus.Size = New System.Drawing.Size(284, 378)
        Me.gbRTGStatus.TabIndex = 101
        Me.gbRTGStatus.TabStop = False
        Me.gbRTGStatus.Text = "RTG Status"
        '
        'lblGantryLabel
        '
        Me.lblGantryLabel.AutoSize = True
        Me.lblGantryLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblGantryLabel.Location = New System.Drawing.Point(13, 31)
        Me.lblGantryLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGantryLabel.Name = "lblGantryLabel"
        Me.lblGantryLabel.Size = New System.Drawing.Size(63, 18)
        Me.lblGantryLabel.TabIndex = 0
        Me.lblGantryLabel.Text = "Gantry:"
        '
        'lblGantryPos
        '
        Me.lblGantryPos.AutoSize = True
        Me.lblGantryPos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblGantryPos.Location = New System.Drawing.Point(133, 31)
        Me.lblGantryPos.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGantryPos.Name = "lblGantryPos"
        Me.lblGantryPos.Size = New System.Drawing.Size(41, 18)
        Me.lblGantryPos.TabIndex = 1
        Me.lblGantryPos.Text = "0 cm"
        '
        'lblTrolleyLabel
        '
        Me.lblTrolleyLabel.AutoSize = True
        Me.lblTrolleyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTrolleyLabel.Location = New System.Drawing.Point(13, 62)
        Me.lblTrolleyLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTrolleyLabel.Name = "lblTrolleyLabel"
        Me.lblTrolleyLabel.Size = New System.Drawing.Size(64, 18)
        Me.lblTrolleyLabel.TabIndex = 2
        Me.lblTrolleyLabel.Text = "Trolley:"
        '
        'lblTrolleyPos
        '
        Me.lblTrolleyPos.AutoSize = True
        Me.lblTrolleyPos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblTrolleyPos.Location = New System.Drawing.Point(133, 62)
        Me.lblTrolleyPos.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTrolleyPos.Name = "lblTrolleyPos"
        Me.lblTrolleyPos.Size = New System.Drawing.Size(41, 18)
        Me.lblTrolleyPos.TabIndex = 3
        Me.lblTrolleyPos.Text = "0 cm"
        '
        'lblHoistLabel
        '
        Me.lblHoistLabel.AutoSize = True
        Me.lblHoistLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblHoistLabel.Location = New System.Drawing.Point(13, 92)
        Me.lblHoistLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHoistLabel.Name = "lblHoistLabel"
        Me.lblHoistLabel.Size = New System.Drawing.Size(53, 18)
        Me.lblHoistLabel.TabIndex = 4
        Me.lblHoistLabel.Text = "Hoist:"
        '
        'lblHoistPos
        '
        Me.lblHoistPos.AutoSize = True
        Me.lblHoistPos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblHoistPos.Location = New System.Drawing.Point(133, 92)
        Me.lblHoistPos.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHoistPos.Name = "lblHoistPos"
        Me.lblHoistPos.Size = New System.Drawing.Size(41, 18)
        Me.lblHoistPos.TabIndex = 5
        Me.lblHoistPos.Text = "0 cm"
        '
        'lblLoadLabel
        '
        Me.lblLoadLabel.AutoSize = True
        Me.lblLoadLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadLabel.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblLoadLabel.Location = New System.Drawing.Point(13, 129)
        Me.lblLoadLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoadLabel.Name = "lblLoadLabel"
        Me.lblLoadLabel.Size = New System.Drawing.Size(62, 24)
        Me.lblLoadLabel.TabIndex = 6
        Me.lblLoadLabel.Text = "Load:"
        '
        'lblLoadWeight
        '
        Me.lblLoadWeight.AutoSize = True
        Me.lblLoadWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadWeight.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblLoadWeight.Location = New System.Drawing.Point(133, 129)
        Me.lblLoadWeight.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoadWeight.Name = "lblLoadWeight"
        Me.lblLoadWeight.Size = New System.Drawing.Size(73, 24)
        Me.lblLoadWeight.TabIndex = 7
        Me.lblLoadWeight.Text = "0.0 ton"
        '
        'lblLockLabel
        '
        Me.lblLockLabel.AutoSize = True
        Me.lblLockLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLockLabel.Location = New System.Drawing.Point(13, 166)
        Me.lblLockLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLockLabel.Name = "lblLockLabel"
        Me.lblLockLabel.Size = New System.Drawing.Size(50, 18)
        Me.lblLockLabel.TabIndex = 8
        Me.lblLockLabel.Text = "Lock:"
        '
        'lblLockStatus
        '
        Me.lblLockStatus.AutoSize = True
        Me.lblLockStatus.BackColor = System.Drawing.Color.Gray
        Me.lblLockStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLockStatus.ForeColor = System.Drawing.Color.White
        Me.lblLockStatus.Location = New System.Drawing.Point(133, 166)
        Me.lblLockStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLockStatus.Name = "lblLockStatus"
        Me.lblLockStatus.Padding = New System.Windows.Forms.Padding(7, 2, 7, 2)
        Me.lblLockStatus.Size = New System.Drawing.Size(114, 22)
        Me.lblLockStatus.TabIndex = 9
        Me.lblLockStatus.Text = "UNLOCKED"
        '
        'lblAlignmentLabel
        '
        Me.lblAlignmentLabel.AutoSize = True
        Me.lblAlignmentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAlignmentLabel.Location = New System.Drawing.Point(13, 203)
        Me.lblAlignmentLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAlignmentLabel.Name = "lblAlignmentLabel"
        Me.lblAlignmentLabel.Size = New System.Drawing.Size(86, 18)
        Me.lblAlignmentLabel.TabIndex = 10
        Me.lblAlignmentLabel.Text = "Alignment:"
        '
        'lblAlignmentStatus
        '
        Me.lblAlignmentStatus.AutoSize = True
        Me.lblAlignmentStatus.BackColor = System.Drawing.Color.Gray
        Me.lblAlignmentStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblAlignmentStatus.ForeColor = System.Drawing.Color.White
        Me.lblAlignmentStatus.Location = New System.Drawing.Point(133, 203)
        Me.lblAlignmentStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAlignmentStatus.Name = "lblAlignmentStatus"
        Me.lblAlignmentStatus.Padding = New System.Windows.Forms.Padding(13, 2, 13, 2)
        Me.lblAlignmentStatus.Size = New System.Drawing.Size(67, 22)
        Me.lblAlignmentStatus.TabIndex = 11
        Me.lblAlignmentStatus.Text = "OFF"
        '
        'lblTrolleyAreaLabel
        '
        Me.lblTrolleyAreaLabel.AutoSize = True
        Me.lblTrolleyAreaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTrolleyAreaLabel.Location = New System.Drawing.Point(13, 240)
        Me.lblTrolleyAreaLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTrolleyAreaLabel.Name = "lblTrolleyAreaLabel"
        Me.lblTrolleyAreaLabel.Size = New System.Drawing.Size(47, 18)
        Me.lblTrolleyAreaLabel.TabIndex = 12
        Me.lblTrolleyAreaLabel.Text = "Area:"
        '
        'lblTrolleyArea
        '
        Me.lblTrolleyArea.AutoSize = True
        Me.lblTrolleyArea.BackColor = System.Drawing.Color.Gray
        Me.lblTrolleyArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTrolleyArea.ForeColor = System.Drawing.Color.White
        Me.lblTrolleyArea.Location = New System.Drawing.Point(133, 240)
        Me.lblTrolleyArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTrolleyArea.Name = "lblTrolleyArea"
        Me.lblTrolleyArea.Padding = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.lblTrolleyArea.Size = New System.Drawing.Size(42, 21)
        Me.lblTrolleyArea.TabIndex = 13
        Me.lblTrolleyArea.Text = "N/A"
        '
        'lblRTGStateLabel
        '
        Me.lblRTGStateLabel.AutoSize = True
        Me.lblRTGStateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblRTGStateLabel.Location = New System.Drawing.Point(13, 277)
        Me.lblRTGStateLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRTGStateLabel.Name = "lblRTGStateLabel"
        Me.lblRTGStateLabel.Size = New System.Drawing.Size(52, 18)
        Me.lblRTGStateLabel.TabIndex = 14
        Me.lblRTGStateLabel.Text = "State:"
        '
        'lblRTGState
        '
        Me.lblRTGState.AutoSize = True
        Me.lblRTGState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.lblRTGState.ForeColor = System.Drawing.Color.Blue
        Me.lblRTGState.Location = New System.Drawing.Point(13, 302)
        Me.lblRTGState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRTGState.Name = "lblRTGState"
        Me.lblRTGState.Size = New System.Drawing.Size(71, 17)
        Me.lblRTGState.TabIndex = 15
        Me.lblRTGState.Text = "State: Idle"
        '
        'lblContainerInfo
        '
        Me.lblContainerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblContainerInfo.Font = New System.Drawing.Font("Courier New", 8.0!)
        Me.lblContainerInfo.Location = New System.Drawing.Point(13, 332)
        Me.lblContainerInfo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblContainerInfo.Name = "lblContainerInfo"
        Me.lblContainerInfo.Size = New System.Drawing.Size(235, 147)
        Me.lblContainerInfo.TabIndex = 16
        Me.lblContainerInfo.Text = "Container Info:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "No data"
        '
        'ofdOpenFile
        '
        Me.ofdOpenFile.FileName = "OpenFileDialog1"
        '
        'realTimerOCR
        '
        Me.realTimerOCR.Interval = 400
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteRowToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(156, 28)
        '
        'DeleteRowToolStripMenuItem
        '
        Me.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem"
        Me.DeleteRowToolStripMenuItem.Size = New System.Drawing.Size(155, 24)
        Me.DeleteRowToolStripMenuItem.Text = "Delete Row"
        '
        'RealTimeDeteksi
        '
        Me.RealTimeDeteksi.Enabled = True
        Me.RealTimeDeteksi.Interval = 150
        '
        'timerVideo
        '
        Me.timerVideo.Interval = 1000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1867, 825)
        Me.Controls.Add(Me.tableLayoutPanel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Name = "frmMain"
        Me.Text = "SPIL OCR System with RTG Integration"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tableLayoutPanel.ResumeLayout(False)
        Me.tableLayoutPanel.PerformLayout()
        Me.gbListData.ResumeLayout(False)
        Me.gbListData.PerformLayout()
        Me.gbCek.ResumeLayout(False)
        Me.gbCek.PerformLayout()
        CType(Me.ibOriginal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ibNomor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbSetting.ResumeLayout(False)
        Me.GbSetting.PerformLayout()
        CType(Me.NumericJarakXPixel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericJarakYPixel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericContour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericRecArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericMaxChange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericDiagSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericThreshold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ibCaptureContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbUpdate.ResumeLayout(False)
        Me.gbUpdate.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbRTGControl.ResumeLayout(False)
        Me.gbRTGControl.PerformLayout()
        Me.gbRTGStatus.ResumeLayout(False)
        Me.gbRTGStatus.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOpenTestImage As System.Windows.Forms.Button
    Friend WithEvents lblChosenFile As System.Windows.Forms.Label
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents ofdOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnSaveImage As System.Windows.Forms.Button
    Friend WithEvents lblCaptureInProgress As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblJudul As System.Windows.Forms.Label
    Friend WithEvents lblJumlah As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents gbListData As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblText2 As System.Windows.Forms.Label
    Friend WithEvents lblText1 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents txtCekDigit As System.Windows.Forms.TextBox
    Friend WithEvents gbCek As System.Windows.Forms.GroupBox
    Friend WithEvents txtOpenFile As System.Windows.Forms.TextBox
    Friend WithEvents btnCekData As System.Windows.Forms.Button
    Friend WithEvents btnOpenImage As System.Windows.Forms.Button
    Friend WithEvents ibOriginal As Emgu.CV.UI.ImageBox
    Friend WithEvents ibNomor As Emgu.CV.UI.ImageBox
    Friend WithEvents lblCurrentRead As System.Windows.Forms.Label
    Friend WithEvents cbShowSteps As System.Windows.Forms.CheckBox
    Friend WithEvents txtAsc As System.Windows.Forms.TextBox
    Friend WithEvents lblip As System.Windows.Forms.Label
    Friend WithEvents lblImageName As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnStartWebcam As System.Windows.Forms.Button
    Friend WithEvents realTimerOCR As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GbSetting As System.Windows.Forms.GroupBox
    Friend WithEvents NumericDiagSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NumericThreshold As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NumericContour As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericRecArea As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericMaxChange As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents NumericJarakYPixel As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents NumericJarakXPixel As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtNomerBenar As System.Windows.Forms.TextBox
    Friend WithEvents RealTimeDeteksi As System.Windows.Forms.Timer
    Friend WithEvents lblFlag As System.Windows.Forms.Label
    Friend WithEvents timerVideo As System.Windows.Forms.Timer
    Friend WithEvents lblHSV As System.Windows.Forms.Label
    Friend WithEvents cbThreshold As System.Windows.Forms.CheckBox
    Friend WithEvents btnSendata As System.Windows.Forms.Button
    Friend WithEvents ibCaptureContainer As Emgu.CV.UI.ImageBox
    Friend WithEvents gbUpdate As System.Windows.Forms.GroupBox
    Friend WithEvents cbPixelAdjust As System.Windows.Forms.CheckBox
    Friend WithEvents lblInputIPCam As System.Windows.Forms.Label
    Friend WithEvents txtInputIpCam As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbOpenImageDatabase As System.Windows.Forms.CheckBox
    ' ========================= NEW RTG CONTROLS DECLARATIONS =========================
    Friend WithEvents gbRTGControl As System.Windows.Forms.GroupBox
    Friend WithEvents lblRTGConnection As System.Windows.Forms.Label
    Friend WithEvents lblComPort As System.Windows.Forms.Label
    Friend WithEvents cmbComPort As System.Windows.Forms.ComboBox
    Friend WithEvents lblBaudRate As System.Windows.Forms.Label
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents btnConnectRTG As System.Windows.Forms.Button
    Friend WithEvents btnDisconnectRTG As System.Windows.Forms.Button

    Friend WithEvents gbRTGStatus As System.Windows.Forms.GroupBox
    Friend WithEvents lblGantryLabel As System.Windows.Forms.Label
    Friend WithEvents lblGantryPos As System.Windows.Forms.Label
    Friend WithEvents lblTrolleyLabel As System.Windows.Forms.Label
    Friend WithEvents lblTrolleyPos As System.Windows.Forms.Label
    Friend WithEvents lblHoistLabel As System.Windows.Forms.Label
    Friend WithEvents lblHoistPos As System.Windows.Forms.Label
    Friend WithEvents lblLoadLabel As System.Windows.Forms.Label
    Friend WithEvents lblLoadWeight As System.Windows.Forms.Label
    Friend WithEvents lblLockLabel As System.Windows.Forms.Label
    Friend WithEvents lblLockStatus As System.Windows.Forms.Label
    Friend WithEvents lblAlignmentLabel As System.Windows.Forms.Label
    Friend WithEvents lblAlignmentStatus As System.Windows.Forms.Label
    Friend WithEvents lblTrolleyAreaLabel As System.Windows.Forms.Label
    Friend WithEvents lblTrolleyArea As System.Windows.Forms.Label
    Friend WithEvents lblRTGStateLabel As System.Windows.Forms.Label
    Friend WithEvents lblRTGState As System.Windows.Forms.Label
    Friend WithEvents lblContainerInfo As System.Windows.Forms.Label
    Friend WithEvents timerRTGMonitor As System.Windows.Forms.Timer

End Class
