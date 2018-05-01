Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Namespace Controllers
    Public Class AcademicAdviserController
        Inherits Controller

        Private db As New ApplicationDbContext

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
    End Class
End Namespace