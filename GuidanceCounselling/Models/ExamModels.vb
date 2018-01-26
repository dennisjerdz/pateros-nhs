Imports System.ComponentModel.DataAnnotations

Public Class QuestionGroup
    Public Property QuestionGroupId As Integer
    <Required>
    Public Property Name As String
    <Required>
    Public Property DisplayName As String
    <Required>
    Public Property ExamType As ExamType
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property QuestionTFRanks As List(Of QuestionTFRank)
    Public Overridable Property QuestionTFLists As List(Of QuestionTFList)
    Public Overridable Property QuestionEssays As List(Of QuestionEssay)
    Public Overridable Property QuestionOneToFives As List(Of QuestionOneToFive)
    Public Overridable Property QuestionRowGroups As List(Of QuestionRowGroup)

    Public Overridable Property ExamQuestionGroups As List(Of ExamQuestionGroup)
End Class

Public Class Exam
    Public Property ExamId As Integer
    <Required>
    Public Property Name As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property ExamQuestionGroups As List(Of ExamQuestionGroup)
End Class

Public Class ExamQuestionGroup
    Public Property ExamQuestionGroupId As Integer
    Public Property ExamId As Integer
    Public Property QuestionGroupId As Integer

    Public Overridable Property Exam As Exam
    Public Overridable Property QuestionGroup As QuestionGroup
End Class


Public Class ExamCreateModel
    Public Sub New()

    End Sub

    Public Property ExamId As Integer
    Public Property Name As String
    Public Property QuestionGroups As List(Of QuestionGroup)
End Class

Public Class ExamStudent
    Public Property ExamStudentId As Integer
    Public Property ExamId As Integer
    Public Property UserId As String
    Public Property AvailabilityStart As DateTimeOffset
    Public Property AvailabilityEnd As DateTimeOffset
    Public Property TakenAt As DateTimeOffset
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property Exam As Exam
    Public Overridable Property User As ApplicationUser
End Class

' Start of Questions
Public Class QuestionTFRank 'TF = True/False
    Public Property QuestionTFRankId As Integer
    Public Property QuestionGroupId As Integer
    Public Property Question As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property QuestionGroup As QuestionGroup
End Class

Public Class QuestionTFList
    Public Property QuestionTFListId As Integer
    Public Property QuestionGroupId As Integer
    Public Property Question As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property QuestionGroup As QuestionGroup
End Class

Public Class QuestionEssay
    Public Property QuestionEssayId As Integer
    Public Property QuestionGroupId As Integer
    Public Property Question As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property QuestionGroup As QuestionGroup
End Class

Public Class QuestionOneToFive
    Public Property QuestionOneToFiveId As Integer
    Public Property QuestionGroupId As Integer
    Public Property Question As String
    Public Property DateCreated As DateTimeOffset

    Public Overridable Property QuestionGroup As QuestionGroup
End Class

Public Class QuestionRowGroup ' 4 Columns
    Public Property QuestionRowGroupId As Integer
    Public Property QuestionGroupId As Integer

    Public Property Col1Question As String
    Public Property Col1Result As String ' display result only as string

    Public Property Col2Question As String
    Public Property Col2Result As String ' display result only as string

    Public Property Col3Question As String
    Public Property Col3Result As String ' display result only as string

    Public Property Col4Question As String
    Public Property Col4Result As String ' display result only as string

    Public Property DateCreated As DateTimeOffset

    Public Overridable Property QuestionGroup As QuestionGroup
End Class

'Start of Answers
Public Class ExamStudentTFRank ' or QuestionTFRank Answer
    Public Property ExamStudentTFRankId As Integer
    Public Property ExamStudentId As Integer
    Public Property QuestionTFRankId As Integer
    Public Property Answer As Boolean
    Public Property DateCreated As DateTimeOffset

    Public Property ExamStudent As ExamStudent
    Public Property QuestionTFRank As QuestionTFRank
End Class

Public Class ExamStudentTFList ' or QuestionTFList Answer
    Public Property ExamStudentTFListId As Integer
    Public Property ExamStudentId As Integer
    Public Property QuestionTFListId As Integer
    Public Property Answer As Boolean
    Public Property DateCreated As DateTimeOffset

    Public Property ExamStudent As ExamStudent
    Public Property QuestionTFList As QuestionTFList
End Class

Public Class ExamStudentEssay ' or QuestionEssay Answer
    Public Property ExamStudentEssayId As Integer
    Public Property ExamStudentId As Integer
    Public Property QuestionEssayId As Integer
    Public Property Answer As String
    Public Property DateCreated As DateTimeOffset

    Public Property ExamStudent As ExamStudent
    Public Property QuestionEssay As QuestionEssay
End Class

Public Class ExamStudentOneToFive ' or QuestionOneToFive Answer
    Public Property ExamStudentOneToFiveId As Integer
    Public Property ExamStudentId As Integer
    Public Property QuestionOneToFiveId As Integer
    Public Property Answer As Boolean
    Public Property DateCreated As DateTimeOffset

    Public Property ExamStudent As ExamStudent
    Public Property QuestionOneToFive As QuestionOneToFive
End Class

Public Class ExamStudentRowGroup ' or QuestionTFList Answer
    Public Property ExamStudentRowGroupId As Integer
    Public Property ExamStudentId As Integer
    Public Property QuestionRowGroupId As Integer
    Public Property Col1Answer As Boolean
    Public Property Col2Answer As Boolean
    Public Property Col3Answer As Boolean
    Public Property Col4Answer As Boolean
    Public Property DateCreated As DateTimeOffset

    Public Property ExamStudent As ExamStudent
    Public Property QuestionRowGroup As QuestionRowGroup
End Class

Public Enum ExamType
    <Display(Name:="True Or False Ranked-Result")>
    TFRank = 0
    <Display(Name:="True Or False List-Result")>
    TFList = 1
    <Display(Name:="Essay")>
    Essay = 2
    <Display(Name:="Rate 1-5")>
    OneToFive = 3
    <Display(Name:="Select Column Per Row")>
    RowGroup = 4
End Enum