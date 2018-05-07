Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports GuidanceCounselling

Public Class Grade
    Public Property GradeId As Integer
    Public Property Name As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property Sections As List(Of Section)
End Class

Public Class Section
    Public Property SectionId As Integer
    Public Property Name As String

    Public Property GradeId As Integer
    <ForeignKey("GradeId")>
    Public Overridable Property Grade As Grade

    Public Overridable Property Students As List(Of ApplicationUser)

    Public Property DateCreated As DateTimeOffset
End Class

Public Class ManageStudentsModel
    Public Sub New()

    End Sub

    Public Sub New(ByVal s As Section)
        Me.GradeId = s.GradeId
        Me.SectionId = s.SectionId
        Me.Name = s.Name
    End Sub

    Public Property GradeId As Integer
    Public Property SectionId As Integer
    Public Property Name As String

    Public Property Students As List(Of UsersSectionModel)
    Public Property NonStudents As List(Of UsersSectionModel)

End Class

Public Class UsersSectionModel
    Public Property UserId As String
    Public Property Name As String
    Public Property SectionId As Integer?
End Class

Public Class SectionAccountsViewModel
    Public Property UserId As String
    Public Property Name As String
    Public Property Email As String
    Public Property Role As String
    Public Property SectionId As Integer?
    Public Property IsDisabled As Boolean
End Class

Public Class Announcement
    Public Property AnnouncementId As Integer
    Public Property Name As String
    Public Property Content As String
    Public Property Active As Boolean
    Public Property DateCreated As DateTimeOffset
    Public Property DateExpired As DateTimeOffset
End Class

Public Class Conversation
    Public Property ConversationId As Integer

    Public Property SenderId As String
    Public Overridable Property Sender As ApplicationUser

    Public Property ReceiverId As String
    Public Overridable Property Receiver As ApplicationUser

    Public Property DateCreated As DateTimeOffset

    Public Overridable Property Messages As List(Of Message)
End Class

Public Class Message
    Public Property MessageId As Integer

    Public Property Content As String

    Public Property UserId As String
    Public Property User As ApplicationUser

    Public Property ConversationId As Integer
    Public Overridable Property Conversation As Conversation

    Public Property DateCreated As DateTimeOffset
End Class

Public Class StudentGrade
    Public Sub New()

    End Sub

    Public Property StudentGradeId As Integer

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Property Name As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property SubjectGrades As List(Of SubjectGrade)
End Class

Public Class SubjectGrade
    Public Sub New()

    End Sub

    Public Property SubjectGradeId As Integer

    Public Property StudentGradeId As Integer
    Public Overridable Property StudentGrade As StudentGrade

    Public Property Subject As String
    Public Property Grade As Decimal

    Public Property DateCreated As DateTimeOffset
End Class

Public Class WebsiteConfig
    Public Sub New()

    End Sub

    Public Property WebSiteConfigId As Integer
    Public Property Name As String
    Public Property Value As String
End Class

Public Class LoginHistory
    Public Sub New()

    End Sub

    Public Property LoginHistoryId As Integer
    Public Property Name As String
    Public Property Login As DateTimeOffset
End Class

Public Class NCAEGrade
    Public Sub New()

    End Sub

    Public Property NCAEGradeId As Integer
    <Required>
    Public Property Name As String

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

    Public Property DateCreated As DateTimeOffset

    Public Property TrackChoice As String
    Public Property StrandConcentrationChoice As String

    Public Overridable Property NCAEGradeSubjects As List(Of NCAEGradeSubject)
    Public Overridable Property NCAEGradeAptitudes As List(Of NCAEGradeAptitude)
End Class

Public Class NCAEGradeSubject
    Public Sub New()

    End Sub

    Public Property NCAEGradeSubjectId As Integer
    Public Property Name As String

    Public Property PercentageScore As Decimal?
    ' Public Property PreferenceLevel As Integer? HP,NP,LP,VLP
    Public Property RankOverall As Integer?

    Public Property NCAEGradeId As Integer
    Public Overridable Property NCAEGrade As NCAEGrade
End Class

Public Class NCAEGradeAptitude
    Public Sub New()

    End Sub

    Public Property NCAEGradeAptitudeId As Integer
    Public Property Name As String

    Public Property StandardScore As Decimal?
    Public Property PercentileRank As Integer?
    Public Property Type As Integer?

    Public Property NCAEGradeId As Integer
    Public Overridable Property NCAEGrade As NCAEGrade
End Class

Public Class NCAEGradeEditModel
    Public Sub New()

    End Sub

    Public Sub New(ByVal ng As NCAEGrade)
        NCAEGradeId = ng.NCAEGradeId
        Name = ng.Name
        UserId = ng.UserId
        TrackChoice = ng.TrackChoice
        StrandConcentrationChoice = ng.StrandConcentrationChoice
    End Sub

    Public Property NCAEGradeId As Integer
    <Required>
    Public Property Name As String
    Public Property StudentName As String

    Public Property UserId As String

    Public Property TrackChoice As String
    Public Property StrandConcentrationChoice As String

    Public Property NCAEGradeSubjects As List(Of NCAEGradeSubject)
    Public Property NCAEGradeAptitudes As List(Of NCAEGradeAptitude)
End Class