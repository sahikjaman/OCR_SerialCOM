# OCR_SerialCOM

A Windows application written in Visual Basic .NET that performs Optical Character Recognition (OCR) and communicates over a serial (COM) port. Use it to capture text from images or the screen and send/receive data with serial devices such as microcontrollers, meters, and other instruments.

> Note: This is a starter README. If parts differ from your implementation (OCR engine, target .NET version, packaging), update the relevant sections or let me know and I’ll tailor it precisely.

## Features
- OCR text extraction from images or screenshots
- Serial (COM) port communication (configurable COM port, baud rate, parity, data/stop bits)
- Basic logging/monitoring of sent and received data
- Configurable OCR language data (if using engines like Tesseract)

## Tech stack
- Language: Visual Basic .NET
- Platform: Windows (WinForms/WPF depending on project)
- OCR: Windows OCR API or Tesseract OCR (to be confirmed in code)
- Serial: System.IO.Ports (SerialPort)

## Requirements
- Windows 10/11
- Visual Studio (recommended) with .NET tooling for VB.NET
- If using Tesseract OCR:
  - Tesseract runtime and language data (e.g., tessdata for desired languages)
  - Corresponding .NET wrapper/NuGet package (e.g., Tesseract)

## Getting started

### 1) Clone
```bash
git clone https://github.com/sahikjaman/OCR_SerialCOM.git
cd OCR_SerialCOM
```

### 2) Open and restore
- Open the solution in Visual Studio
- Restore NuGet packages if prompted

### 3) Build and run
- Build the solution (Debug/Release)
- Press F5 to run from Visual Studio, or run the built executable from the output directory

## Usage

1) Configure Serial
- Connect your device and note the COM port (e.g., COM3)
- In the app, choose COM port, baud rate, data bits, parity, stop bits
- Open the port and verify device communication

2) Perform OCR
- Select an image or capture a screenshot (depending on the app’s UI)
- Run OCR to extract text
- Copy or send extracted text over serial as needed

## Configuration

- Serial defaults (port, baud, etc.) may be configurable via UI or app config (e.g., App.config). Update them according to your device.
- OCR settings:
  - If using Windows OCR: ensure supported language packs are installed in Windows
  - If using Tesseract: install Tesseract and place language data (tessdata) in an accessible folder; set the path in app settings if required
