'Title          :   frmFileNameEditor
'Author         :   Self
'URL            :   onsaurav@yahoo.com/onsaurav@gmail.com/onsaurav@hotmail.com
'Description    :   This form will used to edit the file name from selected folder.
'Created        :   Saurav Biswas / Nov-11-2010
'Modified       :   Saurav Biswas / 

Imports System.IO
Public Class frmFileNameEditor
#Region "Method"
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Summary    :   This method will use to exit the project.
        'Created    :   Saurav Biswas / Nov-11-2010
        'Modified   :   Saurav Biswas
        'Parameters :
        Application.Exit()
    End Sub

    Private Sub frmFileNameEditor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            Me.Focus()
        Catch
        End Try
    End Sub

    Private Sub frmFileNameEditor_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Summary    :   This method will use to exit the project.
        'Created    :   Saurav Biswas / Nov-11-2010
        'Modified   :   Saurav Biswas
        'Parameters :
        Application.Exit()
    End Sub

    Private Sub btnFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolder.Click
        'Summary    :   This method will use to load the selected path.
        'Created    :   Saurav Biswas / Nov-11-2010
        'Modified   :   Saurav Biswas
        'Parameters :

        Try
            If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
                txtFolderPath.Text = FolderBrowserDialog1.SelectedPath.ToString()
            Else
                MessageBox.Show("Sorry! No file psth is seected.", "File", MessageBoxButtons.OK)
                txtFolderPath.Text = ""
            End If
            txtSearch.Focus()
        Catch
        End Try
    End Sub

    Private Sub txtFolderPath_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolderPath.KeyDown
        If e.KeyValue = 13 Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtStartWith_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyValue = 13 Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtEndWidth_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReplace.KeyDown
        If e.KeyValue = 13 Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtBeginning_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBeginning.KeyDown
        If e.KeyValue = 13 Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtEnding_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEnding.KeyDown
        If e.KeyValue = 13 Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtNumber_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyDown
        If e.KeyValue = 13 Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub

    Private Sub chkNumberStart_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkNumberStart.KeyDown
        If e.KeyValue = 13 Then btnRename.Focus()
    End Sub

    Private Sub txtNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumber.TextChanged
        If IsNumeric(txtNumber.Text) = False Then txtNumber.Text = ""
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtFolderPath.Text = ""
        txtSearch.Text = ""
        txtReplace.Text = ""
        txtBeginning.Text = ""
        txtEnding.Text = ""
        txtNumber.Text = ""
        chkNumberStart.Checked = False
        btnFolder.Focus()
    End Sub

    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click
        'Summary    :   This method will use to exit the project.
        'Created    :   Saurav Biswas / Nov-11-2010
        'Modified   :   Saurav Biswas
        'Parameters :

        If CheckInputs() = False Then Exit Sub

        Dim i As Integer
        Dim strFile As String = ""
        Dim di As New IO.DirectoryInfo(txtFolderPath.Text.Trim())
        Dim aryFi As IO.FileInfo() = di.GetFiles()
        Dim fi As IO.FileInfo

        i = 0
        For Each fi In aryFi
            Try
                If fi.Extension <> "" Then strFile = fi.Name.Replace(fi.Extension, "")
                If strFile.IndexOf(txtSearch.Text.Trim()) >= 0 Then
                    strFile = strFile.Replace(txtSearch.Text.Trim(), txtReplace.Text.Trim())
                    strFile = txtBeginning.Text.Trim() & strFile & txtEnding.Text.Trim()
                    If chkNumberStart.Checked = True Then i = Val(txtNumber.Text.Trim()) + i
                    strFile = i.ToString() & strFile
                    strFile = strFile + fi.Extension
                    If (File.Exists(fi.FullName.Replace(fi.Name, "") & "\" + strFile)) Then
                    Else
                        fi.MoveTo(fi.FullName.Replace(fi.Name, "") & "\" + strFile)
                        If chkNumberStart.Checked = True Then i = i - Val(txtNumber.Text.Trim())
                        i = i + 1
                    End If
                End If
            Catch ex As Exception
            End Try
        Next
        MsgBox("File renaming compete ....", MsgBoxStyle.Information + MsgBoxStyle.DefaultButton1, "File")
    End Sub

    Private Function CheckInputs() As Boolean
        'Summary    :   This method will use to exit the project.
        'Created    :   Saurav Biswas / Nov-11-2010
        'Modified   :   Saurav Biswas
        'Parameters :
        If (txtFolderPath.Text.Trim() = "") Then
            MsgBox("Sorry! No folder is selected.", MsgBoxStyle.Information + MsgBoxStyle.DefaultButton1, "Check Inputs")
            CheckInputs = False : Exit Function
        End If

        If chkNumberStart.Checked = True Then
            If (txtNumber.Text.Trim() = "") Then
                MsgBox("Sorry! No File serial number is entered.", MsgBoxStyle.Information + MsgBoxStyle.DefaultButton1, "Check Inputs")
                CheckInputs = False : txtNumber.Focus() : Exit Function
            End If
        End If
        CheckInputs = True
    End Function

    Private Sub chkNumberStart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNumberStart.CheckedChanged
        If chkNumberStart.Checked = False Then txtNumber.Text = ""
    End Sub
#End Region
End Class
