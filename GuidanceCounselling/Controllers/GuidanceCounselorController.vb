Imports System.Data.Entity
Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    '<Authorize(Roles:="Guidance Counselor")>
    <RoutePrefix("GuidanceCounselor")>
    <Route("{action=Index}")>
    Public Class GuidanceCounselorController
        Inherits Controller

        Private db As New ApplicationDbContext

        ' GET: GuidanceCounsellor
        Function Index() As ActionResult
            Return View()
        End Function

        Function QuestionGroups() As ActionResult
            Return View(db.QuestionGroups.ToList())
        End Function

        <Route("QuestionGroup/Add")>
        Function AddQuestionGroup() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        <Route("QuestionGroup/Add")>
        Function AddQuestionGroup(<Bind(Include:="QuestionGroupId,Name,DisplayName,ExamType")> ByVal questionGroup As QuestionGroup) As ActionResult
            questionGroup.DateCreated = DateTimeOffset.Now

            If ModelState.IsValid Then
                db.QuestionGroups.Add(questionGroup)
                db.SaveChanges()
                Return RedirectToAction("QuestionGroups")
            End If

            Return View(questionGroup)
        End Function

        <Route("QuestionGroup/{id}/Edit")>
        Function EditQuestionGroup(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionGroup As QuestionGroup = db.QuestionGroups.Find(id)
            If IsNothing(questionGroup) Then
                Return HttpNotFound()
            End If

            Return View(questionGroup)
        End Function

        <Route("QuestionGroup/{id}/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="QuestionGroupId,Name,DisplayName,ExamType,DateCreated")> ByVal questionGroup As QuestionGroup) As ActionResult
            If ModelState.IsValid Then
                db.Entry(questionGroup).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("QuestionGroups")
            End If
            Return View(questionGroup)
        End Function

        <Route("QuestionGroup/{id}/Questions/Manage")>
        Function ManageQuestions(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionGroup As QuestionGroup = db.QuestionGroups.Find(id)
            If IsNothing(questionGroup) Then
                Return HttpNotFound()
            End If

            Dim et As Integer = Convert.ChangeType(questionGroup.ExamType, GetType(Integer))

            ViewBag.Id = questionGroup.QuestionGroupId

            Select Case et
                Case 0
                    Return View("TFRank", questionGroup.QuestionTFRanks)
                Case 1
                    Return View("TFList", questionGroup.QuestionTFLists)
                Case 2
                    Return View("Essay", questionGroup.QuestionEssays)
                Case 3
                    Return View("OneToFive", questionGroup.QuestionOneToFives)
                Case 4
                    Return View("RowGroup", questionGroup.QuestionRowGroups)
                Case Else
                    Return View()
            End Select
        End Function

        '
        '
        '
        '
        ' TF Rank CRUD
        <Route("QuestionGroup/{id}/Questions/TFRank/Add")>
        Function AddTFRank(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionGroup As QuestionGroup = db.QuestionGroups.Find(id)
            If IsNothing(questionGroup) Then
                Return HttpNotFound()
            End If

            ViewBag.QuestionGroupId = id
            Return View()
        End Function

        <HttpPost()>
        <Route("QuestionGroup/{id}/Questions/TFRank/Add")>
        <ValidateAntiForgeryToken()>
        Function AddTFRank(<Bind(Include:="QuestionTFRankId,QuestionGroupId,Question")> ByVal questionTFRank As QuestionTFRank) As ActionResult
            questionTFRank.DateCreated = DateTimeOffset.Now

            If ModelState.IsValid Then
                db.QuestionTFRanks.Add(questionTFRank)
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionTFRank.QuestionGroupId})
            End If

            ViewBag.QuestionGroupId = questionTFRank.QuestionGroupId
            Return View(questionTFRank)
        End Function

        <Route("QuestionGroup/Questions/TFRank/{id}/Edit")>
        Function EditTFRank(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionTFRank As QuestionTFRank = db.QuestionTFRanks.Find(id)
            If IsNothing(questionTFRank) Then
                Return HttpNotFound()
            End If

            Return View(questionTFRank)
        End Function

        <HttpPost()>
        <Route("QuestionGroup/Questions/TFRank/{id}/Edit")>
        <ValidateAntiForgeryToken()>
        Function EditTFRank(<Bind(Include:="QuestionTFRankId,QuestionGroupId,Question,DateCreated")> ByVal questionTFRank As QuestionTFRank) As ActionResult
            If ModelState.IsValid Then
                db.Entry(questionTFRank).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionTFRank.QuestionGroupId})
            End If
            Return View(questionTFRank)
        End Function

        Function DeleteTFRank(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionTFRank As QuestionTFRank = db.QuestionTFRanks.Find(id)
            If IsNothing(questionTFRank) Then
                Return HttpNotFound()
            End If

            db.QuestionTFRanks.Remove(questionTFRank)
            db.SaveChanges()
            Return RedirectToAction("ManageQuestions", New With {.id = questionTFRank.QuestionGroupId})
        End Function

        '
        '
        '
        '
        ' TFList CRUD
        <Route("QuestionGroup/{id}/Questions/TFList/Add")>
        Function AddTFList(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionGroup As QuestionGroup = db.QuestionGroups.Find(id)
            If IsNothing(questionGroup) Then
                Return HttpNotFound()
            End If

            ViewBag.QuestionGroupId = id
            Return View()
        End Function

        <HttpPost()>
        <Route("QuestionGroup/{id}/Questions/TFList/Add")>
        <ValidateAntiForgeryToken()>
        Function AddTFList(<Bind(Include:="QuestionTFListId,QuestionGroupId,Question")> ByVal questionTFList As QuestionTFList) As ActionResult
            questionTFList.DateCreated = DateTimeOffset.Now

            If ModelState.IsValid Then
                db.QuestionTFLists.Add(questionTFList)
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionTFList.QuestionGroupId})
            End If

            ViewBag.QuestionGroupId = questionTFList.QuestionGroupId
            Return View(questionTFList)
        End Function

        <Route("QuestionGroup/Questions/TFList/{id}/Edit")>
        Function EditTFList(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionTFList As QuestionTFList = db.QuestionTFLists.Find(id)
            If IsNothing(questionTFList) Then
                Return HttpNotFound()
            End If

            Return View(questionTFList)
        End Function

        <HttpPost()>
        <Route("QuestionGroup/Questions/TFList/{id}/Edit")>
        <ValidateAntiForgeryToken()>
        Function EditTFList(<Bind(Include:="QuestionTFListId,QuestionGroupId,Question,DateCreated")> ByVal questionTFList As QuestionTFList) As ActionResult
            If ModelState.IsValid Then
                db.Entry(questionTFList).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionTFList.QuestionGroupId})
            End If
            Return View(questionTFList)
        End Function

        '
        '
        '
        '
        ' Essay CRUD
        <Route("QuestionGroup/{id}/Questions/Essay/Add")>
        Function AddEssay(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionGroup As QuestionGroup = db.QuestionGroups.Find(id)
            If IsNothing(questionGroup) Then
                Return HttpNotFound()
            End If

            ViewBag.QuestionGroupId = id
            Return View()
        End Function

        <HttpPost()>
        <Route("QuestionGroup/{id}/Questions/Essay/Add")>
        <ValidateAntiForgeryToken()>
        Function AddEssay(<Bind(Include:="QuestionEssayId,QuestionGroupId,Question")> ByVal questionEssay As QuestionEssay) As ActionResult
            questionEssay.DateCreated = DateTimeOffset.Now

            If ModelState.IsValid Then
                db.QuestionEssays.Add(questionEssay)
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionEssay.QuestionGroupId})
            End If

            ViewBag.QuestionGroupId = questionEssay.QuestionGroupId
            Return View(questionEssay)
        End Function

        <Route("QuestionGroup/Questions/Essay/{id}/Edit")>
        Function EditEssay(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionEssay As QuestionEssay = db.QuestionEssays.Find(id)
            If IsNothing(questionEssay) Then
                Return HttpNotFound()
            End If

            Return View(questionEssay)
        End Function

        <HttpPost()>
        <Route("QuestionGroup/Questions/Essay/{id}/Edit")>
        <ValidateAntiForgeryToken()>
        Function EditEssay(<Bind(Include:="QuestionEssayId,QuestionGroupId,Question,DateCreated")> ByVal questionEssay As QuestionEssay) As ActionResult
            If ModelState.IsValid Then
                db.Entry(questionEssay).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionEssay.QuestionGroupId})
            End If
            Return View(questionEssay)
        End Function

        '
        '
        '
        '
        ' One To Five CRUD
        <Route("QuestionGroup/{id}/Questions/OneToFive/Add")>
        Function AddOneToFive(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionGroup As QuestionGroup = db.QuestionGroups.Find(id)
            If IsNothing(questionGroup) Then
                Return HttpNotFound()
            End If

            ViewBag.QuestionGroupId = id
            Return View()
        End Function

        <HttpPost()>
        <Route("QuestionGroup/{id}/Questions/OneToFive/Add")>
        <ValidateAntiForgeryToken()>
        Function AddOneToFive(<Bind(Include:="QuestionOneToFiveId,QuestionGroupId,Question")> ByVal questionOneToFive As QuestionOneToFive) As ActionResult
            questionOneToFive.DateCreated = DateTimeOffset.Now

            If ModelState.IsValid Then
                db.QuestionOneToFives.Add(questionOneToFive)
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionOneToFive.QuestionGroupId})
            End If

            ViewBag.QuestionGroupId = questionOneToFive.QuestionGroupId
            Return View(questionOneToFive)
        End Function

        <Route("QuestionGroup/Questions/OneToFive/{id}/Edit")>
        Function EditOneToFive(ByVal id As Integer) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionOneToFive As QuestionOneToFive = db.QuestionOneToFives.Find(id)
            If IsNothing(questionOneToFive) Then
                Return HttpNotFound()
            End If

            Return View(questionOneToFive)
        End Function

        <HttpPost()>
        <Route("QuestionGroup/Questions/OneToFive/{id}/Edit")>
        <ValidateAntiForgeryToken()>
        Function EditEssay(<Bind(Include:="QuestionOneToFiveId,QuestionGroupId,Question,DateCreated")> ByVal questionOneToFive As QuestionOneToFive) As ActionResult
            If ModelState.IsValid Then
                db.Entry(questionOneToFive).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("ManageQuestions", New With {.id = questionOneToFive.QuestionGroupId})
            End If
            Return View(questionOneToFive)
        End Function

        '
        '
        '
        '
        ' Exams
        Function Exams() As ActionResult
            Return View(db.Exams.ToList())
        End Function

        <Route("Exams/Create")>
        Function AddExam() As ActionResult
            Return View()
        End Function

        <Route("Exams/Create")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function AddExam(<Bind(Include:="ExamId,Name")> ByVal exam As Exam) As ActionResult
            If ModelState.IsValid Then
                db.Exams.Add(exam)
                db.SaveChanges()
                Return RedirectToAction("Exams")
            End If
            Return View(exam)
        End Function

        <Route("Exams/Edit")>
        Function EditExam(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If
            Return View(exam)
        End Function

        <Route("Exams/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditExam(<Bind(Include:="ExamId,Name,DateCreated")> ByVal exam As Exam) As ActionResult
            If ModelState.IsValid Then
                db.Entry(exam).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Exams")
            End If
            Return View(exam)
        End Function

        <Route("Exam/{id}/Setup")>
        Function SetupExam(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Dim ec As New ExamCreateModel With {.QuestionGroups = db.QuestionGroups.ToList(), .ExamId = exam.ExamId, .Name = exam.Name}

            Return View(ec)
        End Function

        <Route("Exam/{id}/Setup")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function SetupExam(model As ExamCreateModel)
            Return View(model)
        End Function
    End Class
End Namespace