Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin

' You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

Public Class ApplicationUser
    Inherits IdentityUser

    Public Property FirstName As String
    Public Property MiddleName As String
    Public Property LastName As String

    Public Property BirthDate As DateTimeOffset
    Public Property Contact As String
    Public Property IsDisabled As Boolean
    Public Property Gender As Byte

    Public Property FamilyId As Integer?
    <ForeignKey("FamilyId")>
    Public Overridable Property Family As Family

    Public Property SectionId As Integer?
    <ForeignKey("SectionId")>
    Public Overridable Property Section As Section

    Public Overridable Property ExamStudents As List(Of ExamStudent)
    Public Overridable Property Messages As List(Of Message)
    Public Overridable Property Grades As List(Of StudentGrade)

    Public Function getFullName() As String
        ' Return $"{Me.LastName}, {Me.FirstName} {Me.MiddleName.Substring(0, 1)}."
        Return $"{Me.LastName}, {Me.FirstName} {Me.MiddleName}"
    End Function

    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
        ' Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
        ' Add custom user claims here
        Return userIdentity
    End Function
End Class

Public Class UserEditModel
    Public Sub New()

    End Sub

    Public Sub New(u As ApplicationUser)
        UserId = u.Id
        FirstName = u.FirstName
        MiddleName = u.MiddleName
        LastName = u.LastName
        Email = u.Email
        BirthDate = u.BirthDate
        Contact = u.Contact
        IsDisabled = u.IsDisabled
        Gender = u.Gender
    End Sub

    Public Property UserId As String
    Public Property FirstName As String
    Public Property MiddleName As String
    Public Property LastName As String
    Public Property Email As String
    Public Property BirthDate As DateTimeOffset
    Public Property Contact As String
    Public Property IsDisabled As Boolean
    Public Property Gender As Byte
End Class

Public Class UserEditModelL
    Public Sub New()

    End Sub

    Public Sub New(u As ApplicationUser)
        FirstName = u.FirstName
        MiddleName = u.MiddleName
        LastName = u.LastName
        Email = u.Email
        BirthDate = u.BirthDate
        Contact = u.Contact
        IsDisabled = u.IsDisabled
        Gender = u.Gender
    End Sub

    Public Property FirstName As String
    Public Property MiddleName As String
    Public Property LastName As String
    Public Property Email As String
    Public Property BirthDate As DateTimeOffset
    Public Property Contact As String
    Public Property IsDisabled As Boolean
    Public Property Gender As Byte
End Class


Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)
    Public Sub New()
        MyBase.New("DefaultConnection", throwIfV1Schema:=False)
    End Sub

    Public Property Families As DbSet(Of Family)
    Public Property Grades As DbSet(Of Grade)
    Public Property Sections As DbSet(Of Section)

    Public Property QuestionGroups As DbSet(Of QuestionGroup)
    Public Property Exams As DbSet(Of Exam)
    Public Property ExamQuestionGroups As DbSet(Of ExamQuestionGroup)
    Public Property ExamStudents As DbSet(Of ExamStudent)
    Public Property QuestionTFRanks As DbSet(Of QuestionTFRank)
    Public Property QuestionTFLists As DbSet(Of QuestionTFList)
    Public Property QuestionEssays As DbSet(Of QuestionEssay)
    Public Property QuestionOneToFives As DbSet(Of QuestionOneToFive)
    Public Property QuestionRowGroup As DbSet(Of QuestionRowGroup)
    Public Property ExamStudentTFRanks As DbSet(Of ExamStudentTFRank)
    Public Property ExamStudentTFLists As DbSet(Of ExamStudentTFList)
    Public Property ExamStudentEssays As DbSet(Of ExamStudentEssay)
    Public Property ExamStudentOneToFives As DbSet(Of ExamStudentOneToFive)
    Public Property ExamStudentRowGroup As DbSet(Of ExamStudentRowGroup)
    Public Property Announcements As DbSet(Of Announcement)
    Public Property Conversations As DbSet(Of Conversation)
    Public Property Messages As DbSet(Of Message)
    Public Property StudentGrades As DbSet(Of StudentGrade)
    Public Property SubjectGrades As DbSet(Of SubjectGrade)

    Public Shared Function Create() As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function
End Class
