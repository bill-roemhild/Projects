'Date: 05/10/2016 - Bill Roemhild  - Initial Release
'Version: .1
'Comment: Slotomania Facebook Game Auto Clicker
'License: CPOL
'
'Revisions:
'
'05/10/2016 Initial Release
'
'
'**DISCLAIMER**
'THIS MATERIAL IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
'EITHER EXPRESS OR IMPLIED, INCLUDING, BUT Not LIMITED TO, THE
'IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
'PURPOSE, OR NON-INFRINGEMENT. SOME JURISDICTIONS DO NOT ALLOW THE
'EXCLUSION OF IMPLIED WARRANTIES, SO THE ABOVE EXCLUSION MAY NOT
'APPLY TO YOU. IN NO EVENT WILL I BE LIABLE TO ANY PARTY FOR ANY
'DIRECT, INDIRECT, SPECIAL OR OTHER CONSEQUENTIAL DAMAGES FOR ANY
'USE OF THIS MATERIAL INCLUDING, WITHOUT LIMITATION, ANY LOST
'PROFITS, BUSINESS INTERRUPTION, LOSS OF PROGRAMS OR OTHER DATA ON
'YOUR INFORMATION HANDLING SYSTEM OR OTHERWISE, EVEN If WE ARE
'EXPRESSLY ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.

Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Threading
Imports Sloto_Matic.IniFile

Public Class Form1

    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Public Const MOUSEEVENTF_MIDDLEUP = &H40
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10
    Public Const MOD_ALT As Integer = &H1
    Public Const MOD_CONTROL = &H2
    Public Const MOD_SHIFT = &H4
    Public Const WM_HOTKEY As Integer = &H312
    Public Shared KeyWatchModifier = MOD_ALT + MOD_CONTROL
    Public Shared KeyWatchStop = Keys.Q
    Public Shared KeyWatchPixelCapture = Keys.Z
    Public Shared CurrentScreenshot As Bitmap
    Public Shared screenPixel As New Bitmap(1, 1, PixelFormat.Format32bppArgb)
    Public Shared CurrentPixelColor As Color
    Public Shared MP As Point
    Public Shared NewTabName As String
    Public Shared IniFilePath As String
    Public Shared SpinLocationX As Integer
    Public Shared SpinLocationY As Integer
    Public Shared ClickSpeed As Integer
    Public Shared IniSections As New Hashtable
    Public Shared table1 As New DataTable
    Public Shared IsRunning As Boolean
    Public Shared ChangeMadeNoSave As Boolean
    Public Shared DateOfLastOtherClick As DateTime = Date.Now
    Public Shared CurrentDate As DateTime

    Public Declare Auto Function RegisterHotKey Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Public Declare Auto Function UnregisterHotKey Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Declare Function GetCursorPos Lib "User32.dll" (ByRef lpPoint As Point) As Boolean
    Public Declare Function SetCursorPos Lib "User32.dll" (ByVal X As Integer, ByVal Y As Integer) As Integer
    Public Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = table1
        RegisterHotKey(Me.Handle, 9, KeyWatchModifier, KeyWatchStop)
        RegisterHotKey(Me.Handle, 10, KeyWatchModifier, KeyWatchPixelCapture)


        table1.Columns.Add("Tab")
        table1.Columns.Add("FirstLocationX")
        table1.Columns.Add("FirstLocationY")
        table1.Columns.Add("FirstLocationColor")
        table1.Columns.Add("SecondLocationX")
        table1.Columns.Add("SecondLocationY")
        table1.Columns.Add("SecondLocationColor")
        table1.Columns.Add("ThirdLocationX")
        table1.Columns.Add("ThirdLocationY")
        table1.Columns.Add("ThirdLocationColor")
        table1.Columns.Add("FourthLocationX")
        table1.Columns.Add("FourthLocationY")
        table1.Columns.Add("FourthLocationColor")
        table1.Columns.Add("ButtonLocationX")
        table1.Columns.Add("ButtonLocationY")

        'TabInfo.Show()

    End Sub


    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "9"
                    tmrCheckScreen.Enabled = False
                    IsRunning = False
                    Text = "Stopped"
                Case "10"
                    Me.Text = "Pixel Captured"
                    CurrentPixelColor = GetPixelColorAtMouseLocation()

                    If cbSpinLocation.Checked = True Then
                        txtSpinLocationX.Text = MP.X
                        txtSpinLocationY.Text = MP.Y
                        SpinLocationX = MP.X
                        SpinLocationY = MP.Y
                        cbSpinLocation.Checked = False

                    End If

                    If cbSpinLocation.Checked = False Then
                        For Each Tabpage In TabControl1.TabPages
                            For Each c As Control In Tabpage.controls
                                If TypeOf c Is CheckBox Then
                                    Dim d As CheckBox = c
                                    If d.Checked Then
                                        Dim array = Split(d.Name, "_", 3)
                                        'first Location
                                        If array(2) = "FirstLocationCheckBox" Then
                                            Dim matches = TabControl1.Controls.Find("tb_" & array(1) & "_FirstLocationX", True)
                                            Dim locationx As TextBox = DirectCast(matches(0), TextBox)
                                            locationx.Text = MP.X

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_FirstLocationY", True)
                                            Dim locationy As TextBox = DirectCast(matches(0), TextBox)
                                            locationy.Text = MP.Y

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_FirstLocationColor", True)
                                            Dim locationcolor As TextBox = DirectCast(matches(0), TextBox)
                                            locationcolor.Text = CurrentPixelColor.ToString

                                            For Each row In table1.Rows
                                                If row(0) = array(1) Then
                                                    row("FirstLocationX") = MP.X
                                                    row("FirstLocationY") = MP.Y
                                                    row("FirstLocationColor") = CurrentPixelColor.ToString
                                                End If
                                            Next

                                        End If
                                        'Second Location
                                        If array(2) = "SecondLocationCheckBox" Then
                                            Dim matches = TabControl1.Controls.Find("tb_" & array(1) & "_SecondLocationX", True)
                                            Dim locationx As TextBox = DirectCast(matches(0), TextBox)
                                            locationx.Text = MP.X

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_SecondLocationY", True)
                                            Dim locationy As TextBox = DirectCast(matches(0), TextBox)
                                            locationy.Text = MP.Y

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_SecondLocationColor", True)
                                            Dim locationcolor As TextBox = DirectCast(matches(0), TextBox)
                                            locationcolor.Text = CurrentPixelColor.ToString

                                            For Each row In table1.Rows
                                                If row(0) = array(1) Then
                                                    row("SecondLocationX") = MP.X
                                                    row("SecondLocationY") = MP.Y
                                                    row("SecondLocationColor") = CurrentPixelColor.ToString
                                                End If
                                            Next

                                        End If
                                        'Third Location
                                        If array(2) = "ThirdLocationCheckBox" Then
                                            Dim matches = TabControl1.Controls.Find("tb_" & array(1) & "_ThirdLocationX", True)
                                            Dim locationx As TextBox = DirectCast(matches(0), TextBox)
                                            locationx.Text = MP.X

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_ThirdLocationY", True)
                                            Dim locationy As TextBox = DirectCast(matches(0), TextBox)
                                            locationy.Text = MP.Y

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_ThirdLocationColor", True)
                                            Dim locationcolor As TextBox = DirectCast(matches(0), TextBox)
                                            locationcolor.Text = CurrentPixelColor.ToString

                                            For Each row In table1.Rows
                                                If row(0) = array(1) Then
                                                    row("ThirdLocationX") = MP.X
                                                    row("ThirdLocationY") = MP.Y
                                                    row("ThirdLocationColor") = CurrentPixelColor.ToString
                                                End If
                                            Next

                                        End If
                                        'Fourth Location
                                        If array(2) = "FourthLocationCheckBox" Then
                                            Dim matches = TabControl1.Controls.Find("tb_" & array(1) & "_FourthLocationX", True)
                                            Dim locationx As TextBox = DirectCast(matches(0), TextBox)
                                            locationx.Text = MP.X

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_FourthLocationY", True)
                                            Dim locationy As TextBox = DirectCast(matches(0), TextBox)
                                            locationy.Text = MP.Y

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_FourthLocationColor", True)
                                            Dim locationcolor As TextBox = DirectCast(matches(0), TextBox)
                                            locationcolor.Text = CurrentPixelColor.ToString

                                            For Each row In table1.Rows
                                                If row(0) = array(1) Then
                                                    row("FourthLocationX") = MP.X
                                                    row("FourthLocationY") = MP.Y
                                                    row("FourthLocationColor") = CurrentPixelColor.ToString
                                                End If
                                            Next

                                        End If
                                        'Button Location
                                        If array(2) = "ButtonLocationCheckBox" Then
                                            Dim matches = TabControl1.Controls.Find("tb_" & array(1) & "_ButtonLocationX", True)
                                            Dim locationx As TextBox = DirectCast(matches(0), TextBox)
                                            locationx.Text = MP.X

                                            matches = TabControl1.Controls.Find("tb_" & array(1) & "_ButtonLocationY", True)
                                            Dim locationy As TextBox = DirectCast(matches(0), TextBox)
                                            locationy.Text = MP.Y

                                            For Each row In table1.Rows
                                                If row(0) = array(1) Then
                                                    row("ButtonLocationX") = MP.X
                                                    row("ButtonLocationY") = MP.Y
                                                End If
                                            Next

                                        End If

                                        d.Checked = False

                                    End If
                                End If
                            Next
                        Next
                    End If
                    ChangeMadeNoSave = True
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        If IsRunning = False Then
            IsRunning = True
            Me.Text = "Running"
            tmrCheckScreen.Enabled = True
        Else
            IsRunning = False
            Me.Text = "Stopped"
            tmrCheckScreen.Enabled = False
        End If

    End Sub

    Public Function TakeScreenShot()
        Dim bmpScreenshot = New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb)
        Dim gfxScreenshot = Graphics.FromImage(bmpScreenshot)
        gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy)
        Return bmpScreenshot
    End Function

    Public Function GetPixel(ScreenShot As Bitmap, X As Integer, Y As Integer)
        Dim pixelColor As Color = ScreenShot.GetPixel(X, Y)
        Return pixelColor
    End Function

    Public Function GetPixelColorAtMouseLocation()
        CurrentScreenshot = TakeScreenShot()
        MP = MousePosition()
        CurrentPixelColor = GetPixel(CurrentScreenshot, MP.X, MP.Y)
        Return CurrentPixelColor
    End Function

    Private Sub TabControl1_DoubleClick(sender As Object, e As MouseEventArgs) Handles TabControl1.DoubleClick
        For ix As Integer = 0 To TabControl1.TabCount - 1
            If TabControl1.GetTabRect(ix).Contains(e.Location) Then
                Dim tabpage As New TabPage
                TabName.ShowDialog()
                If NewTabName <> "Cancel" Then
                    tabpage.Text = NewTabName
                    tabpage.Name = NewTabName
                    TabControl1.TabPages.Add(tabpage)
                    tabpage.Text = NewTabName

                    Dim location As Point
                    'First Location Text Box
                    Dim FirstLocationXTextBox As New TextBox
                    FirstLocationXTextBox.Name = "tb_" & tabpage.Name & "_FirstLocationX"
                    FirstLocationXTextBox.Width = 50
                    tabpage.Controls.Add(FirstLocationXTextBox)
                    location.X = 5
                    location.Y = 5
                    FirstLocationXTextBox.Location = location
                    FirstLocationXTextBox.Text = "1"
                    FirstLocationXTextBox.Enabled = False

                    Dim FirstLocationYTextBox As New TextBox
                    FirstLocationYTextBox.Name = "tb_" & tabpage.Name & "_FirstLocationY"
                    tabpage.Controls.Add(FirstLocationYTextBox)
                    FirstLocationYTextBox.Width = 50
                    location.X = 65
                    location.Y = 5
                    FirstLocationYTextBox.Location = location
                    FirstLocationYTextBox.Text = "1"
                    FirstLocationYTextBox.Enabled = False

                    Dim FirstLocationColorTextBox As New TextBox
                    FirstLocationColorTextBox.Name = "tb_" & tabpage.Name & "_FirstLocationColor"
                    tabpage.Controls.Add(FirstLocationColorTextBox)
                    FirstLocationColorTextBox.Width = 240
                    location.X = 125
                    location.Y = 5
                    FirstLocationColorTextBox.Location = location
                    FirstLocationColorTextBox.Text = "Color"
                    FirstLocationColorTextBox.Enabled = False

                    Dim FirstLocationCheckBox As New CheckBox
                    FirstLocationCheckBox.Name = "cb_" & tabpage.Name & "_FirstLocationCheckBox"
                    tabpage.Controls.Add(FirstLocationCheckBox)
                    location.X = 380
                    location.Y = 5
                    FirstLocationCheckBox.Location = location

                    'Second Location Text Box
                    Dim SecondLocationXTextBox As New TextBox
                    SecondLocationXTextBox.Name = "tb_" & tabpage.Name & "_SecondLocationX"
                    SecondLocationXTextBox.Width = 50
                    tabpage.Controls.Add(SecondLocationXTextBox)
                    location.X = 5
                    location.Y = 35
                    SecondLocationXTextBox.Location = location
                    SecondLocationXTextBox.Text = "1"
                    SecondLocationXTextBox.Enabled = False

                    Dim SecondLocationYTextBox As New TextBox
                    SecondLocationYTextBox.Name = "tb_" & tabpage.Name & "_SecondLocationY"
                    tabpage.Controls.Add(SecondLocationYTextBox)
                    SecondLocationYTextBox.Width = 50
                    location.X = 65
                    location.Y = 35
                    SecondLocationYTextBox.Location = location
                    SecondLocationYTextBox.Text = "1"
                    SecondLocationYTextBox.Enabled = False

                    Dim SecondLocationColorTextBox As New TextBox
                    SecondLocationColorTextBox.Name = "tb_" & tabpage.Name & "_SecondLocationColor"
                    tabpage.Controls.Add(SecondLocationColorTextBox)
                    SecondLocationColorTextBox.Width = 240
                    location.X = 125
                    location.Y = 35
                    SecondLocationColorTextBox.Location = location
                    SecondLocationColorTextBox.Text = "Color"
                    SecondLocationColorTextBox.Enabled = False

                    Dim SecondLocationCheckBox As New CheckBox
                    SecondLocationCheckBox.Name = "cb_" & tabpage.Name & "_SecondLocationCheckBox"
                    tabpage.Controls.Add(SecondLocationCheckBox)
                    location.X = 380
                    location.Y = 35
                    SecondLocationCheckBox.Location = location

                    'Third Location Text Box
                    Dim ThirdLocationXTextBox As New TextBox
                    ThirdLocationXTextBox.Name = "tb_" & tabpage.Name & "_ThirdLocationX"
                    ThirdLocationXTextBox.Width = 50
                    tabpage.Controls.Add(ThirdLocationXTextBox)
                    location.X = 5
                    location.Y = 65
                    ThirdLocationXTextBox.Location = location
                    ThirdLocationXTextBox.Text = "1"
                    ThirdLocationXTextBox.Enabled = False

                    Dim ThirdLocationYTextBox As New TextBox
                    ThirdLocationYTextBox.Name = "tb_" & tabpage.Name & "_ThirdLocationY"
                    tabpage.Controls.Add(ThirdLocationYTextBox)
                    ThirdLocationYTextBox.Width = 50
                    location.X = 65
                    location.Y = 65
                    ThirdLocationYTextBox.Location = location
                    ThirdLocationYTextBox.Text = "1"
                    ThirdLocationYTextBox.Enabled = False

                    Dim ThirdLocationColorTextBox As New TextBox
                    ThirdLocationColorTextBox.Name = "tb_" & tabpage.Name & "_ThirdLocationColor"
                    tabpage.Controls.Add(ThirdLocationColorTextBox)
                    ThirdLocationColorTextBox.Width = 240
                    location.X = 125
                    location.Y = 65
                    ThirdLocationColorTextBox.Location = location
                    ThirdLocationColorTextBox.Text = "Color"
                    ThirdLocationColorTextBox.Enabled = False

                    Dim ThirdLocationCheckBox As New CheckBox
                    ThirdLocationCheckBox.Name = "cb_" & tabpage.Name & "_ThirdLocationCheckBox"
                    tabpage.Controls.Add(ThirdLocationCheckBox)
                    location.X = 380
                    location.Y = 65
                    ThirdLocationCheckBox.Location = location

                    'Fourth Location Text Box
                    Dim FourthLocationXTextBox As New TextBox
                    FourthLocationXTextBox.Name = "tb_" & tabpage.Name & "_FourthLocationX"
                    FourthLocationXTextBox.Width = 50
                    tabpage.Controls.Add(FourthLocationXTextBox)
                    location.X = 5
                    location.Y = 95
                    FourthLocationXTextBox.Location = location
                    FourthLocationXTextBox.Text = "1"
                    FourthLocationXTextBox.Enabled = False

                    Dim FourthLocationYTextBox As New TextBox
                    FourthLocationYTextBox.Name = "tb_" & tabpage.Name & "_FourthLocationY"
                    tabpage.Controls.Add(FourthLocationYTextBox)
                    FourthLocationYTextBox.Width = 50
                    location.X = 65
                    location.Y = 95
                    FourthLocationYTextBox.Location = location
                    FourthLocationYTextBox.Text = "1"
                    FourthLocationYTextBox.Enabled = False

                    Dim FourthLocationColorTextBox As New TextBox
                    FourthLocationColorTextBox.Name = "tb_" & tabpage.Name & "_FourthLocationColor"
                    tabpage.Controls.Add(FourthLocationColorTextBox)
                    FourthLocationColorTextBox.Width = 240
                    location.X = 125
                    location.Y = 95
                    FourthLocationColorTextBox.Location = location
                    FourthLocationColorTextBox.Text = "Color"
                    FourthLocationColorTextBox.Enabled = False

                    Dim FourthLocationCheckBox As New CheckBox
                    FourthLocationCheckBox.Name = "cb_" & tabpage.Name & "_FourthLocationCheckBox"
                    tabpage.Controls.Add(FourthLocationCheckBox)
                    location.X = 380
                    location.Y = 95
                    FourthLocationCheckBox.Location = location

                    'Button Location Text Box
                    Dim fifthLocationXTextBox As New TextBox
                    fifthLocationXTextBox.Name = "tb_" & tabpage.Name & "_ButtonLocationX"
                    fifthLocationXTextBox.Width = 50
                    tabpage.Controls.Add(fifthLocationXTextBox)
                    location.X = 5
                    location.Y = 125
                    fifthLocationXTextBox.Location = location
                    fifthLocationXTextBox.Text = "1"
                    fifthLocationXTextBox.Enabled = False

                    Dim fifthLocationYTextBox As New TextBox
                    fifthLocationYTextBox.Name = "tb_" & tabpage.Name & "_ButtonLocationY"
                    tabpage.Controls.Add(fifthLocationYTextBox)
                    fifthLocationYTextBox.Width = 50
                    location.X = 65
                    location.Y = 125
                    fifthLocationYTextBox.Location = location
                    fifthLocationYTextBox.Text = "1"
                    fifthLocationYTextBox.Enabled = False

                    Dim ButtonLabel As New Label
                    ButtonLabel.Name = "lbl_" & tabpage.Name & "_ButtonLabel"
                    tabpage.Controls.Add(ButtonLabel)
                    ButtonLabel.Width = 240
                    location.X = 122
                    location.Y = 128
                    ButtonLabel.Location = location
                    ButtonLabel.Text = "Button Location"

                    Dim fifthLocationCheckBox As New CheckBox
                    fifthLocationCheckBox.Name = "cb_" & tabpage.Name & "_ButtonLocationCheckBox"
                    tabpage.Controls.Add(fifthLocationCheckBox)
                    location.X = 380
                    location.Y = 125
                    fifthLocationCheckBox.Location = location

                    table1.Rows.Add(tabpage.Name, FirstLocationXTextBox.Text, FirstLocationYTextBox.Text, FirstLocationColorTextBox.Text, SecondLocationXTextBox.Text, SecondLocationYTextBox.Text, SecondLocationColorTextBox.Text, ThirdLocationXTextBox.Text, ThirdLocationYTextBox.Text, ThirdLocationColorTextBox.Text, FourthLocationXTextBox.Text, FourthLocationYTextBox.Text, FourthLocationColorTextBox.Text, fifthLocationXTextBox.Text, fifthLocationYTextBox.Text)
                    ChangeMadeNoSave = True

                End If
            End If
        Next
    End Sub

    Public Sub ReadValuesFromIniFile()
        Dim ini As New IniFile
        ini.Load(IniFilePath)

        SpinLocationX = Integer.Parse(ini.GetSection("GAME").GetKey("SpinLocationX").Value)
        SpinLocationY = Integer.Parse(ini.GetSection("GAME").GetKey("SpinLocationY").Value)
        ClickSpeed = Integer.Parse(ini.GetSection("GAME").GetKey("ClickSpeed").Value)

        Dim counter As Integer = 0

        For Each s As IniSection In ini.Sections
            IniSections(counter) = s.Name
            counter = counter + 1
        Next

        For Each element As DictionaryEntry In IniSections
            If element.Value <> "GAME" Then
                Dim FirstLocationX = Integer.Parse(ini.GetSection(element.Value).GetKey("FirstLocationX").Value)
                Dim FirstLocationY = Integer.Parse(ini.GetSection(element.Value).GetKey("FirstLocationY").Value)
                Dim FirstLocationColor = ini.GetSection(element.Value).GetKey("FirstLocationColor").Value
                Dim SecondLocationX = Integer.Parse(ini.GetSection(element.Value).GetKey("SecondLocationX").Value)
                Dim SecondLocationY = Integer.Parse(ini.GetSection(element.Value).GetKey("SecondLocationY").Value)
                Dim SecondLocationColor = ini.GetSection(element.Value).GetKey("SecondLocationColor").Value
                Dim ThirdLocationX = Integer.Parse(ini.GetSection(element.Value).GetKey("ThirdLocationX").Value)
                Dim ThirdLocationY = Integer.Parse(ini.GetSection(element.Value).GetKey("ThirdLocationY").Value)
                Dim ThirdLocationColor = ini.GetSection(element.Value).GetKey("ThirdLocationColor").Value
                Dim FourthLocationX = Integer.Parse(ini.GetSection(element.Value).GetKey("FourthLocationX").Value)
                Dim FourthLocationY = Integer.Parse(ini.GetSection(element.Value).GetKey("FourthLocationY").Value)
                Dim FourthLocationColor = ini.GetSection(element.Value).GetKey("FourthLocationColor").Value
                Dim ButtonLocationX = Integer.Parse(ini.GetSection(element.Value).GetKey("ButtonLocationX").Value)
                Dim ButtonLocationY = Integer.Parse(ini.GetSection(element.Value).GetKey("ButtonLocationY").Value)
                table1.Rows.Add(element.Value, FirstLocationX, FirstLocationY, FirstLocationColor, SecondLocationX, SecondLocationY, SecondLocationColor, ThirdLocationX, ThirdLocationY, ThirdLocationColor, FourthLocationX, FourthLocationY, FourthLocationColor, ButtonLocationX, ButtonLocationY)
            End If
        Next


    End Sub

    Public Sub WriteValuesToIniFile()
        Dim ini As New IniFile

        ini.AddSection("GAME").AddKey("SpinLocationX").Value = SpinLocationX
        ini.AddSection("GAME").AddKey("SpinLocationY").Value = SpinLocationY
        ini.AddSection("GAME").AddKey("ClickSpeed").Value = ClickSpeed

        For i = (table1.Rows.Count - 1) To 0 Step -1
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("FirstLocationX").Value = table1.Rows(i).Item(1)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("FirstLocationY").Value = table1.Rows(i).Item(2)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("FirstLocationColor").Value = table1.Rows(i).Item(3)

            ini.AddSection(table1.Rows(i).Item(0)).AddKey("SecondLocationX").Value = table1.Rows(i).Item(4)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("SecondLocationY").Value = table1.Rows(i).Item(5)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("SecondLocationColor").Value = table1.Rows(i).Item(6)

            ini.AddSection(table1.Rows(i).Item(0)).AddKey("ThirdLocationX").Value = table1.Rows(i).Item(7)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("ThirdLocationY").Value = table1.Rows(i).Item(8)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("ThirdLocationColor").Value = table1.Rows(i).Item(9)

            ini.AddSection(table1.Rows(i).Item(0)).AddKey("FourthLocationX").Value = table1.Rows(i).Item(10)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("FourthLocationY").Value = table1.Rows(i).Item(11)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("FourthLocationColor").Value = table1.Rows(i).Item(12)

            ini.AddSection(table1.Rows(i).Item(0)).AddKey("ButtonLocationX").Value = table1.Rows(i).Item(13)
            ini.AddSection(table1.Rows(i).Item(0)).AddKey("ButtonLocationy").Value = table1.Rows(i).Item(14)
        Next

        ini.Save(IniFilePath)
        ChangeMadeNoSave = False
    End Sub

    Public Sub CreateTabsFromIniFile()
        For i = (table1.Rows.Count - 1) To 0 Step -1

            Dim tabpage As New TabPage
            tabpage.Text = table1.Rows(i).Item(0)
            tabpage.Name = table1.Rows(i).Item(0)
            TabControl1.TabPages.Add(tabpage)

            Dim location As Point
            'First Location Text Box
            Dim FirstLocationXTextBox As New TextBox
            FirstLocationXTextBox.Name = "tb_" & tabpage.Name & "_FirstLocationX"
            FirstLocationXTextBox.Width = 50
            tabpage.Controls.Add(FirstLocationXTextBox)
            location.X = 5
            location.Y = 5
            FirstLocationXTextBox.Location = location
            FirstLocationXTextBox.Text = table1.Rows(i).Item(1)
            FirstLocationXTextBox.Enabled = False

            Dim FirstLocationYTextBox As New TextBox
            FirstLocationYTextBox.Name = "tb_" & tabpage.Name & "_FirstLocationY"
            tabpage.Controls.Add(FirstLocationYTextBox)
            FirstLocationYTextBox.Width = 50
            location.X = 65
            location.Y = 5
            FirstLocationYTextBox.Location = location
            FirstLocationYTextBox.Text = table1.Rows(i).Item(2)
            FirstLocationYTextBox.Enabled = False

            Dim FirstLocationColorTextBox As New TextBox
            FirstLocationColorTextBox.Name = "tb_" & tabpage.Name & "_FirstLocationColor"
            tabpage.Controls.Add(FirstLocationColorTextBox)
            FirstLocationColorTextBox.Width = 240
            location.X = 125
            location.Y = 5
            FirstLocationColorTextBox.Location = location
            FirstLocationColorTextBox.Text = table1.Rows(i).Item(3)
            FirstLocationColorTextBox.Enabled = False

            Dim FirstLocationCheckBox As New CheckBox
            FirstLocationCheckBox.Name = "cb_" & tabpage.Name & "_FirstLocationCheckBox"
            tabpage.Controls.Add(FirstLocationCheckBox)
            location.X = 380
            location.Y = 5
            FirstLocationCheckBox.Location = location

            'Second Location Text Box
            Dim SecondLocationXTextBox As New TextBox
            SecondLocationXTextBox.Name = "tb_" & tabpage.Name & "_SecondLocationX"
            SecondLocationXTextBox.Width = 50
            tabpage.Controls.Add(SecondLocationXTextBox)
            location.X = 5
            location.Y = 35
            SecondLocationXTextBox.Location = location
            SecondLocationXTextBox.Text = table1.Rows(i).Item(4)
            SecondLocationXTextBox.Enabled = False

            Dim SecondLocationYTextBox As New TextBox
            SecondLocationYTextBox.Name = "tb_" & tabpage.Name & "_SecondLocationY"
            tabpage.Controls.Add(SecondLocationYTextBox)
            SecondLocationYTextBox.Width = 50
            location.X = 65
            location.Y = 35
            SecondLocationYTextBox.Location = location
            SecondLocationYTextBox.Text = table1.Rows(i).Item(5)
            SecondLocationYTextBox.Enabled = False

            Dim SecondLocationColorTextBox As New TextBox
            SecondLocationColorTextBox.Name = "tb_" & tabpage.Name & "_SecondLocationColor"
            tabpage.Controls.Add(SecondLocationColorTextBox)
            SecondLocationColorTextBox.Width = 240
            location.X = 125
            location.Y = 35
            SecondLocationColorTextBox.Location = location
            SecondLocationColorTextBox.Text = table1.Rows(i).Item(6)
            SecondLocationColorTextBox.Enabled = False

            Dim SecondLocationCheckBox As New CheckBox
            SecondLocationCheckBox.Name = "cb_" & tabpage.Name & "_SecondLocationCheckBox"
            tabpage.Controls.Add(SecondLocationCheckBox)
            location.X = 380
            location.Y = 35
            SecondLocationCheckBox.Location = location

            'Third Location Text Box
            Dim ThirdLocationXTextBox As New TextBox
            ThirdLocationXTextBox.Name = "tb_" & tabpage.Name & "_ThirdLocationX"
            ThirdLocationXTextBox.Width = 50
            tabpage.Controls.Add(ThirdLocationXTextBox)
            location.X = 5
            location.Y = 65
            ThirdLocationXTextBox.Location = location
            ThirdLocationXTextBox.Text = table1.Rows(i).Item(7)
            ThirdLocationXTextBox.Enabled = False

            Dim ThirdLocationYTextBox As New TextBox
            ThirdLocationYTextBox.Name = "tb_" & tabpage.Name & "_ThirdLocationY"
            tabpage.Controls.Add(ThirdLocationYTextBox)
            ThirdLocationYTextBox.Width = 50
            location.X = 65
            location.Y = 65
            ThirdLocationYTextBox.Location = location
            ThirdLocationYTextBox.Text = table1.Rows(i).Item(8)
            ThirdLocationYTextBox.Enabled = False

            Dim ThirdLocationColorTextBox As New TextBox
            ThirdLocationColorTextBox.Name = "tb_" & tabpage.Name & "_ThirdLocationColor"
            tabpage.Controls.Add(ThirdLocationColorTextBox)
            ThirdLocationColorTextBox.Width = 240
            location.X = 125
            location.Y = 65
            ThirdLocationColorTextBox.Location = location
            ThirdLocationColorTextBox.Text = table1.Rows(i).Item(9)
            ThirdLocationColorTextBox.Enabled = False

            Dim ThirdLocationCheckBox As New CheckBox
            ThirdLocationCheckBox.Name = "cb_" & tabpage.Name & "_ThirdLocationCheckBox"
            tabpage.Controls.Add(ThirdLocationCheckBox)
            location.X = 380
            location.Y = 65
            ThirdLocationCheckBox.Location = location

            'Fourth Location Text Box
            Dim FourthLocationXTextBox As New TextBox
            FourthLocationXTextBox.Name = "tb_" & tabpage.Name & "_FourthLocationX"
            FourthLocationXTextBox.Width = 50
            tabpage.Controls.Add(FourthLocationXTextBox)
            location.X = 5
            location.Y = 95
            FourthLocationXTextBox.Location = location
            FourthLocationXTextBox.Text = table1.Rows(i).Item(10)
            FourthLocationXTextBox.Enabled = False

            Dim FourthLocationYTextBox As New TextBox
            FourthLocationYTextBox.Name = "tb_" & tabpage.Name & "_FourthLocationY"
            tabpage.Controls.Add(FourthLocationYTextBox)
            FourthLocationYTextBox.Width = 50
            location.X = 65
            location.Y = 95
            FourthLocationYTextBox.Location = location
            FourthLocationYTextBox.Text = table1.Rows(i).Item(11)
            FourthLocationYTextBox.Enabled = False

            Dim FourthLocationColorTextBox As New TextBox
            FourthLocationColorTextBox.Name = "tb_" & tabpage.Name & "_FourthLocationColor"
            tabpage.Controls.Add(FourthLocationColorTextBox)
            FourthLocationColorTextBox.Width = 240
            location.X = 125
            location.Y = 95
            FourthLocationColorTextBox.Location = location
            FourthLocationColorTextBox.Text = table1.Rows(i).Item(12)
            FourthLocationColorTextBox.Enabled = False

            Dim FourthLocationCheckBox As New CheckBox
            FourthLocationCheckBox.Name = "cb_" & tabpage.Name & "_FourthLocationCheckBox"
            tabpage.Controls.Add(FourthLocationCheckBox)
            location.X = 380
            location.Y = 95
            FourthLocationCheckBox.Location = location

            'Button Location Text Box
            Dim fifthLocationXTextBox As New TextBox
            fifthLocationXTextBox.Name = "tb_" & tabpage.Name & "_ButtonLocationX"
            fifthLocationXTextBox.Width = 50
            tabpage.Controls.Add(fifthLocationXTextBox)
            location.X = 5
            location.Y = 125
            fifthLocationXTextBox.Location = location
            fifthLocationXTextBox.Text = table1.Rows(i).Item(13)
            fifthLocationXTextBox.Enabled = False

            Dim fifthLocationYTextBox As New TextBox
            fifthLocationYTextBox.Name = "tb_" & tabpage.Name & "_ButtonLocationY"
            tabpage.Controls.Add(fifthLocationYTextBox)
            fifthLocationYTextBox.Width = 50
            location.X = 65
            location.Y = 125
            fifthLocationYTextBox.Location = location
            fifthLocationYTextBox.Text = table1.Rows(i).Item(14)
            fifthLocationYTextBox.Enabled = False

            Dim ButtonLabel As New Label
            ButtonLabel.Name = "lbl_" & tabpage.Name & "_ButtonLabel"
            tabpage.Controls.Add(ButtonLabel)
            ButtonLabel.Width = 240
            location.X = 122
            location.Y = 128
            ButtonLabel.Location = location
            ButtonLabel.Text = "Button Location"

            Dim fifthLocationCheckBox As New CheckBox
            fifthLocationCheckBox.Name = "cb_" & tabpage.Name & "_ButtonLocationCheckBox"
            tabpage.Controls.Add(fifthLocationCheckBox)
            location.X = 380
            location.Y = 125
            fifthLocationCheckBox.Location = location

        Next

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        OpenFileDialog1.Filter = "Sloto-Matic|*.ini"
        OpenFileDialog1.Title = "Open Sloto-Matic File"
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = DialogResult.OK Then
            IniFilePath = OpenFileDialog1.FileName

            Do Until TabControl1.TabCount = 1
                TabControl1.TabPages.Remove(TabControl1.TabPages(1))
            Loop

            table1.Rows.Clear()

            ReadValuesFromIniFile()
            CreateTabsFromIniFile()
            txtSpinLocationX.Text = SpinLocationX
            txtSpinLocationY.Text = SpinLocationY
            txtClickSpeed.Text = ClickSpeed

            ChangeMadeNoSave = False

        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        WriteValuesToIniFile()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileDialog1.Filter = "Sloto-Matic|*.ini"
        SaveFileDialog1.Title = "Save Sloto-Matic File"
        Dim results = SaveFileDialog1.ShowDialog()
        If results.Cancel Then
            Exit Sub
        End If
        If results.OK Then
            If SaveFileDialog1.FileName <> "" Then
                IniFilePath = SaveFileDialog1.FileName
                WriteValuesToIniFile()
            End If
        End If
    End Sub


    Private Sub txtClickSpeed_Leave(sender As Object, e As EventArgs) Handles txtClickSpeed.Leave
        ClickSpeed = txtClickSpeed.Text
        tmrCheckScreen.Interval = ClickSpeed
        ChangeMadeNoSave = True
    End Sub

    Private Sub txtClickSpeed_TextChanged(sender As Object, e As EventArgs) Handles txtClickSpeed.TextChanged
        ClickSpeed = txtClickSpeed.Text
        tmrCheckScreen.Interval = ClickSpeed
    End Sub

    Private Sub tmrCheckScreen_Tick(sender As Object, e As EventArgs) Handles tmrCheckScreen.Tick

        CurrentScreenshot = TakeScreenShot()
        Dim FoundScreen As Boolean = False
        For Each row In table1.Rows
            Dim testFirstLocationColor = GetPixel(CurrentScreenshot, row("FirstLocationX"), row("FirstLocationY"))
            Dim testSecondLocationColor = GetPixel(CurrentScreenshot, row("SecondLocationX"), row("SecondLocationY"))
            Dim testThirdLocationColor = GetPixel(CurrentScreenshot, row("ThirdLocationX"), row("ThirdLocationY"))
            Dim testFourthLocationColor = GetPixel(CurrentScreenshot, row("FourthLocationX"), row("FourthLocationY"))

            If testFirstLocationColor.ToString = row("FirstLocationColor") And testSecondLocationColor.ToString = row("SecondLocationColor") And testThirdLocationColor.ToString = row("ThirdLocationColor") And testFourthLocationColor.ToString = row("FourthLocationColor") Then
                Me.Text = row("Tab")
                TextBox1.AppendText(row("tab") + vbCrLf)
                SetCursorPos(row("ButtonLocationX"), row("ButtonLocationY"))
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
                FoundScreen = True
            End If
        Next

        If FoundScreen = False Then
            If SpinLocationY <> 0 Then
                SetCursorPos(SpinLocationX, SpinLocationY)
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            End If

        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        If ChangeMadeNoSave = True Then
            Dim result As Integer = MessageBox.Show("Do you want to save first?", "Unsaved Changes!", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            ElseIf result = DialogResult.No Then
                Me.Close()
            ElseIf result = DialogResult.Yes Then
                SaveFileDialog1.Filter = "Sloto-Matic|*.ini"
                SaveFileDialog1.Title = "Save Sloto-Matic File"
                Dim results = SaveFileDialog1.ShowDialog()
                If results.Cancel Then
                    Exit Sub
                End If
                If results.OK Then
                    If SaveFileDialog1.FileName <> "" Then
                        IniFilePath = SaveFileDialog1.FileName
                        WriteValuesToIniFile()
                    End If
                End If

                Me.Close()
            End If
        End If
        Try
            UnregisterHotKey(Me.Handle, 9)
            UnregisterHotKey(Me.Handle, 10)
        Catch ex As Exception
            MsgBox("Couldn't Unregister HotKeys")
        End Try
        Me.Close()
    End Sub

    Private Sub btnConfigure_Click(sender As Object, e As EventArgs) Handles btnConfigure.Click
        If Size.Height = 118 Then
            Size = New Size(437, 296)
        Else
            Size = New Size(437, 118)
        End If
    End Sub

    Private Sub TabControl1_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TabControl1.MouseClick
        If e.Button = MouseButtons.Right Then
            For index As Integer = 0 To Me.TabControl1.TabCount - 1 Step 1
                If Me.TabControl1.GetTabRect(index).Contains(e.Location) Then
                    Me.TabControl1.SelectedIndex = index
                    If Me.TabControl1.SelectedIndex > 0 Then
                        ContextMenuStrip1.Show(Me, e.Location)
                        ContextMenuStrip1.Top = ContextMenuStrip1.Top + 75
                    End If
                    Exit For
                End If
            Next index
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click

        If Me.TabControl1.SelectedIndex > 0 Then
            Dim RowToRemove As DataRow
            For Each row In table1.Rows
                If row("Tab") = Me.TabControl1.SelectedTab.Name Then
                    RowToRemove = row
                End If
            Next
            Try
                table1.Rows.Remove(RowToRemove)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            ChangeMadeNoSave = True

            TabControl1.TabPages.Remove(Me.TabControl1.SelectedTab)

        End If

    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub DebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugToolStripMenuItem.Click
        If DebugToolStripMenuItem.Checked Then
            DebugToolStripMenuItem.Checked = False
            Size = New Size(437, 118)
        Else
            DebugToolStripMenuItem.Checked = True
            Size = New Size(1190, 488)
        End If
    End Sub
End Class


