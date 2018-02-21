Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

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
            questionGroup.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

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
            questionTFRank.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

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
            questionTFList.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

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
            questionEssay.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

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
            questionOneToFive.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

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
            exam.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            If ModelState.IsValid Then
                db.Exams.Add(exam)
                db.SaveChanges()
                Return RedirectToAction("Exams")
            End If
            Return View(exam)
        End Function

        <Route("Exam/{id}/Edit")>
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

        <Route("Exam/{id}/Edit")>
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

            Dim allQG As List(Of QuestionGroup) = db.QuestionGroups.ToList()

            ' Get Current Question Groups Array
            Dim arrayOfExamQG() As Integer = exam.ExamQuestionGroups.Select(Function(a) a.QuestionGroupId).ToArray()

            ' Get Curret Question Groups List
            Dim currentQG As List(Of QuestionGroup) = allQG.Where(Function(a) arrayOfExamQG.Contains(a.QuestionGroupId)).ToList()

            ' Get Available Question Groups
            Dim availableQG As List(Of QuestionGroup) = allQG.Where(Function(b) Not (arrayOfExamQG.Contains(b.QuestionGroupId))).ToList()

            Dim aqg As New List(Of QuestionGroupExamCreateModel)
            Dim cqg As New List(Of QuestionGroupExamCreateModel)

            ' Assign CURRENT
            If currentQG IsNot Nothing Then
                For Each i As QuestionGroup In currentQG
                    Dim insert_cqg As New QuestionGroupExamCreateModel(i)
                    cqg.Add(insert_cqg)
                Next
            End If

            ' Assign AVAILABLE (non-member)
            If availableQG IsNot Nothing Then
                For Each i As QuestionGroup In availableQG
                    Dim insert_aqg As New QuestionGroupExamCreateModel(i)
                    aqg.Add(insert_aqg)
                Next
            End If

            Dim ec As New ExamCreateModel With {.AvailableQuestionGroups = aqg, .CurrentQuestionGroups = cqg, .ExamId = exam.ExamId, .Name = exam.Name}

            Return View(ec)
        End Function

        <Route("Exam/{id}/Setup")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function SetupExam(model As ExamCreateModel)
            If ModelState.IsValid Then
                Dim el As List(Of ExamQuestionGroup) = db.ExamQuestionGroups.Where(Function(e) e.ExamId = model.ExamId).ToList()

                db.ExamQuestionGroups.RemoveRange(el)
                db.SaveChanges()

                If model.CurrentQuestionGroups IsNot Nothing Then
                    For Each item As QuestionGroupExamCreateModel In model.CurrentQuestionGroups
                        db.ExamQuestionGroups.Add(New ExamQuestionGroup With {.ExamId = model.ExamId, .QuestionGroupId = item.QuestionGroupId})
                    Next

                    db.SaveChanges()
                End If

                Return RedirectToAction("Exams")
            End If

            Dim exam As Exam = db.Exams.Find(model.ExamId)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Dim allQG As List(Of QuestionGroup) = db.QuestionGroups.ToList()

            ' Get Current Question Groups Array
            Dim arrayOfExamQG() As Integer = exam.ExamQuestionGroups.Select(Function(a) a.QuestionGroupId).ToArray()

            ' Get Curret Question Groups List
            Dim currentQG As List(Of QuestionGroup) = allQG.Where(Function(a) arrayOfExamQG.Contains(a.QuestionGroupId)).ToList()

            ' Get Available Question Groups
            Dim availableQG As List(Of QuestionGroup) = allQG.Where(Function(b) Not (arrayOfExamQG.Contains(b.QuestionGroupId))).ToList()

            Dim aqg As New List(Of QuestionGroupExamCreateModel)
            Dim cqg As New List(Of QuestionGroupExamCreateModel)

            ' Assign CURRENT
            If currentQG IsNot Nothing Then
                For Each i As QuestionGroup In currentQG
                    Dim insert_cqg As New QuestionGroupExamCreateModel(i)
                    cqg.Add(insert_cqg)
                Next
            End If

            ' Assign AVAILABLE (non-member)
            If availableQG IsNot Nothing Then
                For Each i As QuestionGroup In availableQG
                    Dim insert_aqg As New QuestionGroupExamCreateModel(i)
                    aqg.Add(insert_aqg)
                Next
            End If

            Dim ec As New ExamCreateModel With {.AvailableQuestionGroups = aqg, .CurrentQuestionGroups = cqg, .ExamId = exam.ExamId, .Name = exam.Name}

            Return View(ec)
        End Function

        <Route("Exam/{id}/Assign")>
        Public Function AssignExam(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Dim e As New AssignExamModel(exam)
            e.Grades = db.Grades.ToList()

            Return View(e)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken>
        <Route("Exam/{id}/Assign")>
        Public Function AssignExam(model As AssignExamModel) As ActionResult

            If ModelState.IsValid Then
                If model.Students IsNot Nothing Then
                    For Each s As StudentExamTakerModel In model.Students
                        If s.Included = True Then
                            db.ExamStudents.Add(New ExamStudent With {.AvailabilityEnd = model.AvailabilityEnd.ToOffset(New TimeSpan(8, 0, 0)).AddHours(-8), .AvailabilityStart = model.AvailabilityStart.ToOffset(New TimeSpan(8, 0, 0)).AddHours(-8), .ExamId = model.ExamId, .UserId = s.UserId, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))})
                        End If
                    Next

                    db.SaveChanges()
                    Return RedirectToAction("ExamAssignments", New With {.id = model.ExamId})
                End If
            End If

            Dim exam As Exam = db.Exams.Find(model.ExamId)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Dim e As New AssignExamModel(exam)
            e.Grades = db.Grades.ToList()

            Return View(e)
        End Function

        <Route("Exam/{id}/Assignments")>
        Public Function ExamAssignments(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Return View(exam)
        End Function

        <Route("Exam/Assignments/{id}/Edit")>
        Public Function EditAssignment(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As ExamStudent = db.ExamStudents.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            Return View(exam)
        End Function

        <Route("Exam/Assignments/{id}/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Public Function EditAssignment(<Bind(Include:="ExamId,AvailabilityStart,AvailabilityEnd,ExamStudentId,UserId,DateCreated,TakenAt")> ByVal model As ExamStudent) As ActionResult
            model.AvailabilityStart = model.AvailabilityStart.ToOffset(New TimeSpan(8, 0, 0)).AddHours(-8)
            model.AvailabilityEnd = model.AvailabilityEnd.ToOffset(New TimeSpan(8, 0, 0)).AddHours(-8)

            If ModelState.IsValid Then
                db.Entry(model).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("ExamAssignments", New With {.id = model.ExamId})
            End If

            Return View(model)
        End Function

        Public Function DeleteAssignment(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As ExamStudent = db.ExamStudents.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If

            db.ExamStudents.Remove(exam)
            db.SaveChanges()

            Return RedirectToAction("ExamAssignments", New With {.id = exam.ExamId})
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

        '
        '
        '
        '
        ' Announcements

        Function Announcements() As ActionResult
            Return View(db.Announcements.ToList())
        End Function

        <Route("Announcement/Create")>
        Function AddAnnouncement(ByVal id As Integer?)
            Return View()
        End Function

        <Route("Announcement/Create")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function AddAnnouncement(<Bind(Include:="AnnouncementId,Name,Content,DateExpired")> ByVal announcement As Announcement)
            announcement.Active = True
            announcement.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))
            announcement.DateExpired = announcement.DateExpired.ToOffset(New TimeSpan(8, 0, 0)).AddHours(-8)

            If ModelState.IsValid Then
                db.Announcements.Add(announcement)
                db.SaveChanges()
                Return RedirectToAction("Announcements")
            End If
            Return View(announcement)
        End Function

        <Route("Announcement/{id}/Edit")>
        Function EditAnnouncement(ByVal id As Integer?)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim announcement As Announcement = db.Announcements.Find(id)
            If IsNothing(announcement) Then
                Return HttpNotFound()
            End If
            Return View(announcement)
        End Function

        <Route("Announcement/{id}/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function EditAnnouncement(<Bind(Include:="AnnouncementId,Name,Content,Active,DateCreated,DateExpired")> ByVal announcement As Announcement)
            announcement.DateExpired = announcement.DateExpired.ToOffset(New TimeSpan(8, 0, 0)).AddHours(-8)

            If ModelState.IsValid Then
                db.Entry(announcement).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Announcements")
            End If
            Return View(announcement)
        End Function

        Function ChangeStatus(ByVal id As Integer?)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim announcement As Announcement = db.Announcements.Find(id)
            If IsNothing(announcement) Then
                Return HttpNotFound()
            End If

            If announcement.Active = True Then
                announcement.Active = False
            Else
                announcement.Active = True
            End If
            db.SaveChanges()

            Return RedirectToAction("Announcements")
        End Function

        '
        '
        '
        '
        ' Student Grades

        Async Function Students() As Task(Of ActionResult)
            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim users = db.Users.Where(Function(u) u.Email <> User.Identity.Name).ToList()

            Dim acctss As List(Of StudentsViewModel) = Await db.Users.Where(Function(a) a.Email <> User.Identity.Name).Select(Function(s) New StudentsViewModel() With {
            .UserId = s.Id,
            .Name = s.LastName + ", " + s.FirstName + " " + s.MiddleName,
            .Email = s.Email,
            .IsDisabled = s.IsDisabled,
            .Grade = If(s.Section IsNot Nothing, s.Section.Grade.Name, ""),
            .Section = If(s.Section IsNot Nothing, s.Section.Name, ""),
            .Role = db.Roles.FirstOrDefault(Function(r) r.Id = s.Roles.FirstOrDefault().RoleId).Name
            }).ToListAsync()

            'Dim accts As List(Of StudentsViewModel) = users.Select(Function(a) New StudentsViewModel() With {
            '.UserId = a.Id,
            '.Name = a.getFullName,
            '.Email = a.Email,
            '.IsDisabled = a.IsDisabled,
            '.Grade = If(a.Section IsNot Nothing, a.Section.Grade.Name, ""),
            '.Section = If(a.Section IsNot Nothing, a.Section.Name, ""),
            '.Role = String.Join(",", UserManager.GetRoles(a.Id))
            '}).ToList()

            Return View(acctss.Where(Function(c) c.Role = "Student").ToList())
        End Function

        <Route("Student/{id}/Grades")>
        Function Grades(ByVal id As String) As ActionResult
            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = id.ToString())

            Return View(u)
        End Function

        <Route("Student/{id}/Grades/Add")>
        Function AddGrade(ByVal id As String) As ActionResult
            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = id.ToString())

            If u Is Nothing Then
                Return HttpNotFound()
            End If

            Dim g As New StudentGrade With {.UserId = u.Id}

            Return View(g)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken>
        <Route("Student/{id}/Grades/Add")>
        Function AddGrade(<Bind(Include:="StudentGradeId,UserId,Name")> ByVal studentGrade As StudentGrade) As ActionResult
            studentGrade.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            If ModelState.IsValid Then
                db.StudentGrades.Add(studentGrade)
                db.SaveChanges()
                Return RedirectToAction("Grades", New With {.id = studentGrade.UserId})
            End If

            Return View(studentGrade)
        End Function

        <Route("Student/Grade/{id}/Edit")>
        Function EditGrade(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            If IsNothing(studentGrade) Then
                Return HttpNotFound()
            End If

            Return View(studentGrade)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Route("Student/Grade/{id}/Edit")>
        Function EditGrade(<Bind(Include:="StudentGradeId,UserId,Name,DateCreated")> ByVal studentGrade As StudentGrade) As ActionResult
            If ModelState.IsValid Then
                db.Entry(studentGrade).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Grades", New With {.id = studentGrade.UserId})
            End If

            Return View(studentGrade)
        End Function

        '
        '
        '
        '
        ' Subject Grades

        <Route("Student/Grade/{id}/Subjects")>
        Function ViewGrades(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            If IsNothing(studentGrade) Then
                Return HttpNotFound()
            End If

            Return View(studentGrade)
        End Function

        <Route("Student/Grade/{id}/Subjects/Add")>
        Function AddSubjectGrade(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            If IsNothing(studentGrade) Then
                Return HttpNotFound()
            End If

            Dim g As New SubjectGrade With {.StudentGradeId = id}

            Return View(g)
        End Function

        <Route("Student/Grade/{id}/Subjects/Add")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Function AddSubjectGrade(<Bind(Include:="SubjectGradeId,StudentGradeId,Subject,Grade")> ByVal subjectGrade As SubjectGrade) As ActionResult
            subjectGrade.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            If ModelState.IsValid Then
                db.SubjectGrades.Add(subjectGrade)
                db.SaveChanges()
                Return RedirectToAction("ViewGrades", New With {.id = subjectGrade.StudentGradeId})
            End If

            Return View(subjectGrade)
        End Function

        <Route("Student/Subject/{id}/Grade/Edit")>
        Function EditSubjectGrade(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim subjectGrade As SubjectGrade = db.SubjectGrades.Find(id)
            If IsNothing(subjectGrade) Then
                Return HttpNotFound()
            End If

            Return View(subjectGrade)
        End Function

        <Route("Student/Subject/{id}/Grade/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditSubjectGrade(<Bind(Include:="SubjectGradeId,StudentGradeId,Subject,Grade,DateCreated")> ByVal subjectGrade As SubjectGrade) As ActionResult
            If ModelState.IsValid Then
                db.Entry(subjectGrade).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("ViewGrades", New With {.id = subjectGrade.StudentGradeId})
            End If

            Return View(subjectGrade)
        End Function

        '
        '
        '
        '
        ' Conversations
        Async Function Accounts() As Task(Of ActionResult)
            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim acctss As List(Of AccountsViewModel) = Await db.Users.Where(Function(a) a.Email <> User.Identity.Name).Select(Function(s) New AccountsViewModel() With {
            .UserId = s.Id,
            .Name = s.LastName + " " + s.FirstName + " " + s.MiddleName,
            .Email = s.Email,
            .IsDisabled = s.IsDisabled,
            .Role = db.Roles.FirstOrDefault(Function(r) r.Id = s.Roles.FirstOrDefault().RoleId).Name
            }).ToListAsync()

            'Dim users = db.Users.Where(Function(u) u.Email <> User.Identity.Name).ToList()

            'Dim accts As List(Of AccountsViewModel) = users.Select(Function(a) New AccountsViewModel() With {
            '    .UserId = a.Id,
            '    .Name = a.getFullName,
            '    .Email = a.Email,
            '    .IsDisabled = a.IsDisabled,
            '    .Role = String.Join(",", UserManager.GetRoles(a.Id))
            '}).ToList()

            Return View(acctss)
        End Function

        Function Conversations() As ActionResult
            Return View(db.Conversations.Where(Function(c) c.Receiver.Email = User.Identity.Name Or c.Sender.Email = User.Identity.Name).ToList())
        End Function

        <Route("Conversation/{id}")>
        Function OpenConversation(ByVal id As String)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim cue As String = User.Identity.Name

            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = id)
            Dim cu As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.UserName = cue)

            If u Is Nothing Or cu Is Nothing Then
                Return RedirectToAction("Accounts")
            Else
                Dim conversation As Conversation = db.Conversations.FirstOrDefault(Function(c) (c.ReceiverId = u.Id And c.SenderId = cu.Id) Or (c.SenderId = u.Id And c.ReceiverId = cu.Id))

                If conversation Is Nothing Then
                    Dim nc As New Conversation With {.SenderId = cu.Id, .ReceiverId = u.Id, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))}
                    db.Conversations.Add(nc)
                    db.SaveChanges()

                    Dim nm As New Message With {.UserId = cu.Id, .ConversationId = nc.ConversationId, .Conversation = nc}
                    Return View(nm)
                Else
                    Dim nm As New Message With {.UserId = cu.Id, .ConversationId = conversation.ConversationId, .Conversation = conversation}
                    Return View(nm)
                End If
            End If
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken>
        <Route("Conversation/{id}")>
        Function OpenConversation(model As Message)
            model.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            Dim cu As String = User.Identity.Name
            model.UserId = db.Users.FirstOrDefault(Function(u) u.UserName = cu).Id

            If ModelState.IsValid Then
                db.Messages.Add(model)
                db.SaveChanges()

                Dim c As Conversation = db.Conversations.FirstOrDefault(Function(o) o.ConversationId = model.ConversationId)

                If c.ReceiverId = model.UserId Then
                    Return RedirectToAction("OpenConversation", New With {.id = c.SenderId})
                End If

                If c.SenderId = model.UserId Then
                    Return RedirectToAction("OpenConversation", New With {.id = c.ReceiverId})
                End If
            End If

            Return View()
        End Function

        Function SendMessage()

            Return View()
        End Function
    End Class
End Namespace