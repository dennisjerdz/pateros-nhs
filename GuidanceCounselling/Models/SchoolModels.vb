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