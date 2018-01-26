Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Family
    Public Property FamilyId As Integer
    Public Property Name As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property FamilyMembers As List(Of ApplicationUser)
    'Public Overridable Property FamilyMember As List(Of FamilyMember)
    'Public Overridable Property FamilyStudent As List(Of FamilyStudent)
End Class

Public Class FamilyCreateModel
    <Required>
    Public Property Name As String

    Public Property FamilyMembers As List(Of UsersFamilyModel)
End Class

Public Class FamilyEditModel
    Public Sub New()

    End Sub

    Public Sub New(ByVal f As Family)
        Me.FamilyId = f.FamilyId
        Me.Name = f.Name
    End Sub

    Public Property FamilyId As Integer
    <Required>
    Public Property Name As String

    Public Property FamilyMembers As List(Of UsersFamilyModel)
    Public Property NonMembers As List(Of UsersFamilyModel)
End Class

Public Class UsersFamilyModel
    Public Property UserId As String
    Public Property Name As String
    Public Property Role As String
End Class

'Public Class FamilyMember
'    Public Property FamilyRelationshipId As Integer

'    Public Property FamilyId As Integer
'    <ForeignKey("FamilyId")>
'    Public Overridable Property Family As Family

'    Public Property UserId As Integer
'    <ForeignKey("UserId")>
'    Public Overridable Property User As ApplicationUser
'End Class

'Public Class FamilyStudent
'    Public Property FamilyStudentId As Integer

'    Public Property FamilyId As Integer
'    <ForeignKey("FamilyId")>
'    Public Overridable Property Family As Family

'    Public Property UserId As Integer
'    <ForeignKey("UserId")>
'    Public Overridable Property User As ApplicationUser
'End Class