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
Public Class TabInfo
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim a As New ArrayList

        If Form1.TabControl1.TabCount <> Nothing Then
            a.Add("Current Tab Count: " & Form1.TabControl1.TabCount)
            For Each Tabpage In Form1.TabControl1.TabPages
                a.Add(Tabpage.text)
                For Each c As Control In Tabpage.controls

                    a.Add(c.Name)

                Next
            Next

        End If

        TextBox1.Text = ""
        For Each b In a
            TextBox1.AppendText(b.ToString)
            TextBox1.AppendText(vbCrLf)
        Next

    End Sub

    Private Sub TabInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class