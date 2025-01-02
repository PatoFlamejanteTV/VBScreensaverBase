Option Strict On
Imports System.IO

''' <summary>
''' This code just closes the form, when the user decides to Cancel.
''' </summary>
''' <remarks></remarks>
Public Class frmOptions
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' This code changes the values in the Options object to the new user
    ''' selected values, and saves it to disk.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim myOptions As New Options()

        Dim Speed As Integer = CInt(Me.txtSpeed.Text)
        If Speed = 0 Then
            Speed = 1
            Me.txtSpeed.Text = "1"
        End If
        myOptions.Speed = Speed
        myOptions.ShowDate = Me.chkShowDates.Checked
        myOptions.ShowTags = Me.chkShowTags.Checked
        myOptions.UseSubdirectories = Me.chkIncludeSubdirectories.Checked
        myOptions.Directory = Me.txtDirectory.Text

        ' Save the options to disk.
        myOptions.SaveOptions()

        ' Close this object.
        Me.Close()

    End Sub

    ''' <summary>
    ''' This code loads the current user defined options and sets the UI elements 
    ''' in this form to their proper values.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Load the options file.  Recall that the load method will
        '   always return an options object, even if the file doesn't
        '   currently exist
        Dim myOptions As New Options()
        myOptions.LoadOptions()

        Me.txtSpeed.Text = myOptions.Speed.ToString
        Me.chkShowDates.Checked = myOptions.ShowDate
        Me.chkShowTags.Checked = myOptions.ShowTags
        Me.chkIncludeSubdirectories.Checked = myOptions.UseSubdirectories
        Me.txtDirectory.Text = myOptions.Directory

    End Sub

    ''' <summary>
    ''' Browse for the folder that we should read from
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDirectory.Click
        Me.FolderBrowserDialog1.SelectedPath = Me.txtDirectory.Text
        Dim result As DialogResult = Me.FolderBrowserDialog1.ShowDialog
        If result = Windows.Forms.DialogResult.OK OrElse result = Windows.Forms.DialogResult.Yes Then
            Me.txtDirectory.Text = Me.FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' This is a base class which restricts text boxes to numeric inputs and arrow navigation.  
    ''' (It's modified from an existing snippet designed for combo boxes.)
    ''' </summary>
    ''' <remarks></remarks>
    Class restrictedTextBoxClass
        Inherits TextBox
        Const WM_KEYDOWN As Integer = &H100

        Protected Overrides Function ProcessCmdKey _
            (ByRef msg As Message, _
            ByVal keyData As Keys) As Boolean

            If msg.Msg = WM_KEYDOWN Then
                Return Not ((keyData >= Keys.D0 And keyData <= Keys.D9) _
                    Or keyData = Keys.Back Or keyData = Keys.Left _
                    Or keyData = Keys.Right Or keyData = Keys.Up _
                    Or keyData = Keys.Down Or keyData = Keys.Delete)
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function
    End Class
End Class
