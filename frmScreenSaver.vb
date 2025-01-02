Option Strict On

Imports System.Windows.Media.Imaging
Imports System.IO
Imports System.Collections.ObjectModel
Imports System.Collections.Generic

' This form is the main form for the screen saver. It does all the painting
'   of the screen, and handles when it should terminate and release control 
'   back to Windows.
Public Class frmScreenSaver

#Region "Variables"
    ' Declare the class variables that will be used for the Screen Saver.
    ' Options object that contains information about the user selected options
    Private m_Options As New Options()

    ' Random object to support the drawing
    Private m_Random As New Random()

    ' Used to for first setting MouseMove location
    Private m_IsActive As Boolean = False

    ' Used to determine if the Mouse has actually been moved
    Private m_MouseLocation As Point

    Private m_files As ReadOnlyCollection(Of String) ' List of files
    Private m_fileCount As Integer = 0
#End Region

#Region "Events"

    ''' <summary>
    ''' This subroutine initializes the Screen Saver form when it is loaded
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSceenSaver_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Load the Saved options. 
        m_Options.LoadOptions()

        ' Reset location of label
        SetLabelLocation()

        ' Set the speed based on the user defined Options.
        Me.tmrUpdateScreen.Interval = m_Options.Speed * 1000

        ' Enable the timer.
        Me.tmrUpdateScreen.Enabled = True

        If m_Options.UseSubdirectories = True Then
            m_files = My.Computer.FileSystem.GetFiles(m_Options.Directory, FileIO.SearchOption.SearchAllSubDirectories, "*.jpg")
        Else
            m_files = My.Computer.FileSystem.GetFiles(m_Options.Directory, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
        End If
        If m_files IsNot Nothing Then
            m_fileCount = m_files.Count
        Else
            Me.lblDescription.Text = "No pictures found!"
            SetLabelLocation()
        End If

    End Sub

    ''' <summary>
    ''' Close the screen saver when the user moves the mouse. Unfortunately, the 
    '''   MouseMove event is fired by some very trivial moves of the mouse, so 
    '''   instead, verify that the mouse has actually been moved by at least
    '''   a few pixels before exiting.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSceenSaver_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, PictureBox1.MouseMove, lblDescription.MouseMove

        ' If the MouseLocation still points to 0,0, move it to its actual location
        '   and save the location for later. Otherwise, check to see if the user
        '   has moved the mouse at least 10 pixels.
        If Not m_IsActive Then
            Me.m_MouseLocation = New Point(e.X, e.Y)
            m_IsActive = True
        Else
            If Math.Abs(e.X - Me.m_MouseLocation.X) > 10 Or _
                Math.Abs(e.Y - Me.m_MouseLocation.Y) > 10 Then
                ' The user has moved the mouse so leave this program
                Application.Exit()
            End If
        End If
    End Sub

    ''' <summary>
    ''' The subroutine causes the screen saver to close if the user pushes a mouse button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSceenSaver_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, PictureBox1.MouseDown, lblDescription.MouseDown
        Application.Exit()
    End Sub

    ''' <summary>
    ''' ' This code is executed whenever the timer tick event if fired. It draws a picture and label to the screen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tmrUpdateScreen_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdateScreen.Tick
        If m_fileCount <> 0 Then
            DrawPicture()
        End If
    End Sub
#End Region

#Region "Class Methods"
    Private Sub DrawPicture()

        ' Which picture?  Get a new random number
        Dim index As Integer = Me.m_Random.Next(0, m_fileCount - 1)

        ' Get text -- dates and tags
        If m_Options.ShowDate = True OrElse m_Options.ShowTags = True Then
            ' First, I'll need to read the metadata.  This is a rather arcane process, which I learned some time
            ' ago from a forum posting somewhere.  (And don't even get me started about opening & setting/reading EXIF data, 
            ' the process for which was created by a maniac.  http://www.codeproject.com/KB/vb/exif_reader.aspx has an *excellent* 
            ' example of that insane process, if you're interested.)

            ' Open up the file as normal
            Dim jpegStream As New System.IO.FileStream(m_files(index), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)

            ' Create a decoder to grok all of the image goodness in the stream I just opened
            Dim jpegDecoder As New JpegBitmapDecoder(jpegStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default)

            ' Grab the first frame in the JPEG, which is the only one anyone ever uses 99.9999% of the time.
            Dim jpegFrame As BitmapFrame = jpegDecoder.Frames(0)

            ' Now create a metadata writer on that frame to actually access the metadata.  ("But we're not writing!"  I hear you cry.
            ' Doesn't matter; this is the way you do it.) 
            Dim jpegInplace As InPlaceBitmapMetadataWriter = jpegFrame.CreateInPlaceBitmapMetadataWriter()

            ' Now, If you *did* want to write something, like maybe change the date for photos
            ' that you've scanned in, it's not too hard.  See my CleanTags method in the "Hypothetical methods" section
            ' at the bottom of this file.
            ' Today, though, we're just reading the data.

            Dim sDescription As String = ""
            If m_Options.ShowDate = True Then
                Dim dt As String = jpegInplace.DateTaken
                If Not String.IsNullOrEmpty(dt) Then
                    Dim ddt As Date = CDate("#" & dt & "#")
                    sDescription = ddt.Date & ": "
                End If
            End If
            If m_Options.ShowTags = True Then
                If jpegInplace.Keywords IsNot Nothing AndAlso jpegInplace.Keywords.Count > 0 Then
                    For Each keyword As String In jpegInplace.Keywords
                        If Not keyword.StartsWith(" ") Then
                            sDescription += keyword & ", "

                        End If
                    Next
                End If
            End If
            If sDescription.EndsWith(", ") OrElse sDescription.EndsWith(": ") Then
                sDescription = Microsoft.VisualBasic.Left(sDescription, sDescription.Length - 2)
            End If
            Me.lblDescription.Text = sDescription
            ' Set the size of the text control
            SetLabelLocation()
            jpegStream.Close()
        Else
            Me.lblDescription.Hide()
        End If

        ' Show picture and text here
        Me.PictureBox1.Load(m_files(index))
    End Sub
    ''' <summary>
    ''' Just a helper app to center the label appropriately in the window
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLabelLocation()
        ' Put the label at the bottom...
        Me.lblDescription.Top = Me.Size.Height - Me.lblDescription.Height

        ' And center it in the screen
        Me.lblDescription.Left = (Me.Size.Width - Me.lblDescription.Width) \ 2
        Me.lblDescription.Show()
    End Sub
    ' This sub is the first one that executes when the Screen Saver 
    '   program is run.  Since Windows will pass parameters to the this
    '   program whenever a user is setting up the screen saver using the 
    '   Display Properties -> Screen Saver property screen.
    <STAThread()> Shared Sub Main(ByVal args As String())

        ' Check to see if there were any passed arguments. If not, then
        '   the user simply double-clicked on the .scr file.
        If args.Length > 0 Then
            ' This means we have some passed arguments.  Windows will
            '   automatically pass a "/s", "/p" or a "/c" depending
            '   on how the screen saver should behave.  The meanings for each
            '   of these parameters is seen below.

            ' Check to see if the Screen saver should preview.
            If args(0).ToLower = "/p" Then
                ' This functionality is not implemented here because it involves
                '   creating and joining threads and is beyond the scope of this
                '   How-To.

                ' Simply exit the application
                Application.Exit()
            End If

            ' Check to see if the Screen saver should show user definable options.
            If args(0).ToLower.Trim().Substring(0, 2) = "/c" Then
                ' Create a frmOptions form and display it.
                Dim userOptionsForm As New frmOptions()
                userOptionsForm.ShowDialog()

                ' Exit the application.
                Application.Exit()
            End If

            ' Check to see if the Screen saver should simply execute
            If args(0).ToLower = "/s" Then

                ' Create a frmSceenSaver form and display it.
                Dim screenSaverForm As New ScreenSaver.frmScreenSaver()
                screenSaverForm.ShowDialog()

                ' Exit the application when the form is closed
                Application.Exit()
            End If
        Else
            ' Fire up the Screen saver.  Note: This is only used when the user
            '   double clicks on the EXE, since otherwise windows passes a
            '   parameter to the application.

            ' Create a frmSceenSaver form and display it.
            Dim screenSaverForm As New ScreenSaver.frmScreenSaver()
            screenSaverForm.ShowDialog()

            ' Exit the application when the form is closed
            Application.Exit()
        End If
    End Sub

#End Region

#Region "Hypothetical methods"
#If CONFIG = "Debug" Then
    ''' <summary>
    ''' This isn't actually used by the screensaver; I'm just demonstrating how to persist metadata.
    ''' </summary>
    ''' <param name="jpegInplace"></param>
    ''' <param name="FileName"></param>
    ''' <param name="GetRidOf"></param>
    ''' <remarks></remarks>
    Public Sub CleanTags(ByVal jpegInplace As InPlaceBitmapMetadataWriter, ByVal FileName As String, ByVal GetRidOf As String)
        ' Let's assume that I accidentally added a tag to a photo in error.  Unfortunately, scrapbooking programs, Adobe Elements,
        ' etc. won't remove burned tags from the file's metadata, although they are always happy to add more. 
        ' (I found this out when I tagged a bunch of newly scanned photos with 'Needs dates' for later retrieval -- I couldn't
        ' get them to go away once I'd corrected the dates to teh time taken instead of the time scanned.)
        '
        ' Since TrySave gets cranky if I change the size of the metadata, and since the keywords (i.e., "Tags") are 
        ' in a read-only collection, I have to do some odd things.  (Note that it's easier to set things like "DateTaken,"
        ' which is just a method off of jpegInPlace; i.e., jpegInplace.DateTaken = "1/23/2009 12:00:00AM")

        ' Does the picture have the offending tag?
        If jpegInplace.Keywords.Contains(GetRidOf) Then
            ' Why, yes it does.  We can't modify the original collection, so we'll recreate it
            ' the way we want to:
            Dim newlist As New List(Of String)

            Dim count As Integer = 0
            For Each keyword As String In jpegInplace.Keywords
                If keyword <> GetRidOf Then
                    newlist.Add(keyword)
                Else
                    count += 1 ' Skip over the offending keyword.
                End If
            Next

            ' This is such a hack, but that's life with metadata.  Attach a number of spaces to the end equivalent to the
            ' number of those that were in the offending keyword, so that the overall size remains constant.  (Please, someone
            ' tell me a better way to do this!)
            Dim spaces As New String(" "c, GetRidOf.Length)
            For i As Integer = 0 To count - 1
                newlist.Add(spaces)
            Next

            ' Now copy our string to a new collection object and replace teh one that was already there.
            Dim newcollection As New ReadOnlyCollection(Of String)(newlist)
            jpegInplace.Keywords = newcollection

            ' Try to save -- if it fails, write a log entry.  (Use My.Application.Log.DefaultFileLogWriter.FullLogFileName
            ' to find out where this gets persisted for your app.)
            If Not jpegInplace.TrySave() Then
                My.Application.Log.WriteEntry("Unable to save tag information to " & FileName)
            End If
        End If

    End Sub
#End If
#End Region

End Class
