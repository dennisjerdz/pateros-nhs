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