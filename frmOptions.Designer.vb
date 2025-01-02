<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
    Inherits System.Windows.Forms.Form
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkShowTags As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSpeed As frmOptions.restrictedTextBoxClass
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnDirectory As System.Windows.Forms.Button
    Friend WithEvents chkIncludeSubdirectories As System.Windows.Forms.CheckBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chkShowDates As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.chkShowTags = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkShowDates = New System.Windows.Forms.CheckBox
        Me.txtSpeed = New ScreenSaver.frmOptions.restrictedTextBoxClass
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDirectory = New System.Windows.Forms.TextBox
        Me.btnDirectory = New System.Windows.Forms.Button
        Me.chkIncludeSubdirectories = New System.Windows.Forms.CheckBox
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.SuspendLayout()
        '
        'btnOK
        '
        resources.ApplyResources(Me.btnOK, "btnOK")
        Me.btnOK.Name = "btnOK"
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.Name = "btnCancel"
        '
        'chkShowTags
        '
        resources.ApplyResources(Me.chkShowTags, "chkShowTags")
        Me.chkShowTags.Name = "chkShowTags"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'chkShowDates
        '
        resources.ApplyResources(Me.chkShowDates, "chkShowDates")
        Me.chkShowDates.Name = "chkShowDates"
        '
        'txtSpeed
        '
        resources.ApplyResources(Me.txtSpeed, "txtSpeed")
        Me.txtSpeed.Name = "txtSpeed"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'txtDirectory
        '
        resources.ApplyResources(Me.txtDirectory, "txtDirectory")
        Me.txtDirectory.Name = "txtDirectory"
        '
        'btnDirectory
        '
        resources.ApplyResources(Me.btnDirectory, "btnDirectory")
        Me.btnDirectory.Name = "btnDirectory"
        Me.btnDirectory.UseVisualStyleBackColor = True
        '
        'chkIncludeSubdirectories
        '
        resources.ApplyResources(Me.chkIncludeSubdirectories, "chkIncludeSubdirectories")
        Me.chkIncludeSubdirectories.Name = "chkIncludeSubdirectories"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyPictures
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'frmOptions
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.btnDirectory)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDirectory)
        Me.Controls.Add(Me.txtSpeed)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkIncludeSubdirectories)
        Me.Controls.Add(Me.chkShowDates)
        Me.Controls.Add(Me.chkShowTags)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmOptions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
