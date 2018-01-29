Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    <RoutePrefix("Student")>
    <Route("{action=Index}")>
    Public Class StudentController
        Inherits Controller

        Private db As New ApplicationDbContext

        Function Index() As ActionResult
            Return View()
        End Function

        Function AssignedExams() As ActionResult
            Dim UserId As String = db.Users.FirstOrDefault(Function(u) u.Email = User.Identity.Name).Id

            Dim exams As List(Of ExamStudent) = db.ExamStudents.Where(Function(e) e.UserId = UserId).OrderByDescending(Function(s) s.DateCreated).ToList()
            Return View(exams)
        End Function

        Function TakeExamConfirm(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As ExamStudent = db.ExamStudents.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Return View(exam)
        End Function

        Function TakeExamConfirmed(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As ExamStudent = db.ExamStudents.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Return RedirectToAction("StudentExam", New With {.id = exam.ExamStudentId, .count = 0})
        End Function

        Function StudentExam(ByVal id As Integer, ByVal count As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As ExamStudent = db.ExamStudents.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Return View(exam)
            'If exam.Exam.ExamQuestionGroups.ElementAt(count) Is Nothing Then
            'Return Content("Exam Finished.")
            'Else

            'Select Case exam.Exam.ExamQuestionGroups.ElementAt(count).QuestionGroup.ExamType.ToString()
            'Case "TFRank"
            'Return Content("TRUE OR FALSE RANKED")
            'Case "TFList"
            'Return Content("TRUE OR FALSE LIST")
            'Case "Essay"
            'Return Content("Essay")
            'End Select
            'End If
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken>
        Public Function StudentExam(model As ExamStudent, collection As FormCollection) As ActionResult
            If ModelState.IsValid Then
                Dim i As Integer = 0
                Do While (i < collection("ExamTFRank").Count)
                    Dim value = collection("")(i.ToString)
                    ' do whatever with value
                    i += 1
                Loop
            End If

            Return View()
        End Function

        '
        '
        '
        '
        ' Edit Account

        <Route("Account/Edit")>
        Public Function EditAccount() As ActionResult
            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Email = User.Identity.Name)

            Dim r As New UserEditModelL(u)

            Return View(r)
        End Function

        <Route("Account/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Public Function EditAccount(model As UserEditModelL) As ActionResult
            If ModelState.IsValid Then
                Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Email = User.Identity.Name)

                If IsNothing(u) Then
                    Return HttpNotFound()
                End If

                u.FirstName = model.FirstName
                u.MiddleName = model.MiddleName
                u.LastName = model.LastName
                u.IsDisabled = model.IsDisabled
                u.Gender = model.Gender
                u.UserName = model.Email
                u.Email = model.Email

                db.SaveChanges()

                Return RedirectToAction("Accounts")
            End If

            Return View(model)
        End Function
    End Class
End Namespace