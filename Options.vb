Option Strict On

''' <summary>
''' This is the class that holds the Options information.  The Options information
'''   is defined as a class so that it can be easily serialized and deserialized.
''' </summary>
''' <remarks></remarks>
Public Class Options

#Region "Member variables"
    Private m_Speed As Integer = 6
    Private m_ShowTags As Boolean = True
    Private m_ShowDates As Boolean = True
    Private m_UseSubdirectories As Boolean = True
    Private m_Directory As String = ""
#End Region

#Region "Class Properties"
    ''' <summary>
    ''' Set or get whether or not to show the date
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowDate() As Boolean
        Get
            Return m_ShowDates
        End Get
        Set(ByVal Value As Boolean)
            m_ShowDates = Value
        End Set
    End Property

    ''' <summary>
    ''' Set or get whether or not to look in subdirectories for pictures
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UseSubdirectories() As Boolean
        Get
            Return m_UseSubdirectories
        End Get
        Set(ByVal Value As Boolean)
            m_UseSubdirectories = Value
        End Set
    End Property

    ''' <summary>
    ''' Set or get whether or not to show tags
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowTags() As Boolean
        Get
            Return m_ShowTags
        End Get
        Set(ByVal Value As Boolean)
            m_ShowTags = Value
        End Set
    End Property

    ''' <summary>
    ''' Set or get which directory to look in for pictures
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Directory() As String
        Get
            Return m_Directory
        End Get
        Set(ByVal Value As String)
            m_Directory = Value
        End Set
    End Property

    ''' <summary>
    ''' Set or get what speed at which to show pictures
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Speed() As Integer
        Get
            Return m_Speed
        End Get
        Set(ByVal Value As Integer)
            m_Speed = Value
        End Set
    End Property

#End Region

#Region "Class Methods"
    ''' <summary>
    ''' This function loads the user defined options from the application settings file.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadOptions()
        Me.Speed = My.Settings.Speed
        Me.ShowDate = My.Settings.ShowDate
        Me.ShowTags = My.Settings.ShowTags
        Me.Directory = My.Settings.Directory
        Me.UseSubdirectories = My.Settings.UseSubdirectories

        If String.IsNullOrEmpty(Directory) Then ' Default to "My Pictures" if not initialized
            Me.Directory = My.Computer.FileSystem.SpecialDirectories.MyPictures
        End If
    End Sub

    ''' <summary>
    ''' This function saves the user defined options to the application settings file.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SaveOptions()
        My.Settings.Speed = Me.Speed
        My.Settings.ShowDate = Me.ShowDate
        My.Settings.ShowTags = Me.ShowTags
        If Not String.IsNullOrEmpty(Directory) Then ' Don't bother saving
            My.Settings.Directory = Me.Directory
        End If
        My.Settings.UseSubdirectories = Me.UseSubdirectories
        My.Settings.Save()
    End Sub

#End Region



End Class
