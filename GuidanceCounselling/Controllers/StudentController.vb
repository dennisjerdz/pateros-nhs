Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    <RoutePrefix("Student")>
    <Route("{action=Index}")>
    Public Class StudentController
        Inherits Controller

        Private db As New ApplicationDbContext

        Function ExamAlreadyTaken() As ActionResult
            Return View()
        End Function

        Function ExamRestriction() As ActionResult
            Return View()
        End Function

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

            Dim now As DateTimeOffset = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))
            Dim currentUser As String = User.Identity.Name

            Dim checkUser As ApplicationUser = db.Users.FirstOrDefault(Function(u) u.Email = currentUser)

            If now < exam.AvailabilityStart Then
                Return RedirectToAction("ExamRestriction")
            End If

            If now > exam.AvailabilityEnd Then
                Return RedirectToAction("ExamRestriction")
            End If

            If db.ExamStudents.Any(Function(e) e.UserId = checkUser.Id And e.ExamStudentId = id And e.TakenAt IsNot Nothing) Then
                Return RedirectToAction("ExamAlreadyTaken")
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

        <Route("Student/Exam/{id}/{count}/Taking")>
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
        Public Function StudentExam(model As SubmitExamModel) As ActionResult
            'If ModelState.IsValid Then
            'Dim i As Integer = 0
            'Do While (i < Collection("ExamTFRank").Count)
            'Dim value = Collection("")(i.ToString)
            '' do whatever with value
            'i  += 1
            'Loop
            'End If

            Dim exam As ExamStudent = db.ExamStudents.Find(model.ExamStudentId)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            exam.TakenAt = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            If model.ExamStudentTFRank IsNot Nothing Then
                For Each i As ExamStudentTFRank In model.ExamStudentTFRank
                    db.ExamStudentTFRanks.Add(New ExamStudentTFRank() With {.ExamStudentId = model.ExamStudentId, .Answer = i.Answer, .QuestionTFRankId = i.QuestionTFRankId, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))})
                Next
            End If

            If model.ExamStudentTFList IsNot Nothing Then
                For Each i As ExamStudentTFList In model.ExamStudentTFList
                    db.ExamStudentTFLists.Add(New ExamStudentTFList() With {.ExamStudentId = model.ExamStudentId, .Answer = i.Answer, .QuestionTFListId = i.QuestionTFListId, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))})
                Next
            End If

            If model.ExamStudentEssay IsNot Nothing Then
                For Each i As ExamStudentEssay In model.ExamStudentEssay
                    db.ExamStudentEssays.Add(New ExamStudentEssay() With {.ExamStudentId = model.ExamStudentId, .Answer = i.Answer, .QuestionEssayId = i.QuestionEssayId, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))})
                Next
            End If

            If model.ExamStudentOneToFive IsNot Nothing Then
                For Each i As ExamStudentOneToFive In model.ExamStudentOneToFive
                    db.ExamStudentOneToFives.Add(New ExamStudentOneToFive() With {.ExamStudentId = model.ExamStudentId, .Answer = i.Answer, .QuestionOneToFiveId = i.QuestionOneToFiveId, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))})
                Next
            End If

            db.SaveChanges()

            Return RedirectToAction("AssignedExams")
        End Function

        <Route("Exam/{id}/Results")>
        Function ExamResults(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As ExamStudent = db.ExamStudents.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Return View(exam)
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