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

Public Class TabName

    Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short

    Private Sub Button1_Click() Handles Button1.Click
        If TextBox1.Text = "" Or TextBox1.Text.Contains(" ") Or TextBox1.Text.Contains("_") Then

            MsgBox("Enter a proper name first.  No spaces or underscores.")
        Else
            Dim PageExist As Boolean = False
            For Each Tabpage In Form1.TabControl1.TabPages
                If Tabpage.name = TextBox1.Text Then
                    PageExist = True
                End If
            Next
            If PageExist = False Then
                Form1.NewTabName = TextBox1.Text
                Me.Close()
            Else
                MsgBox("Tab With That Name Exists.")
            End If
        End If
        TextBox1.Text = ""

    End Sub

    Private Sub Button2_Click() Handles Button2.Click
        Form1.NewTabName = "Cancel"
        TextBox1.Text = ""
        Me.Close()
    End Sub

    Private Sub TabName_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If GetAsyncKeyState(13) Then
            Button1_Click()
        End If
        If GetAsyncKeyState(27) Then
            Button2_Click()
        End If
    End Sub

    Private Sub TabName_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        TextBox1.Focus()
    End Sub
End Class