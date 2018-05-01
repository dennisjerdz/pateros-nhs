Imports System.Data.Entity
Imports System.Net
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Namespace Controllers

    <RoutePrefix("FamilyMember")>
    <Route("{action=Index}")>
    Public Class FamilyMemberController
        Inherits Controller

        Private db As New ApplicationDbContext

        Function Index() As ActionResult
            Return View()
        End Function

        Function Students() As ActionResult
            Dim au As ApplicationUser = db.Users.FirstOrDefault(Function(t) t.Email = User.Identity.Name)

            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim users = db.Users.Where(Function(u) u.Email <> User.Identity.Name And u.FamilyId = au.FamilyId.Value).ToList()

            Dim accts As List(Of StudentsViewModel) = users.Select(Function(a) New StudentsViewModel() With {
                .UserId = a.Id,
                .Name = a.getFullName,
                .Email = a.Email,
                .IsDisabled = a.IsDisabled,
                .Grade = a.Section.Grade.Name,
                .Section = a.Section.Name,
                .Role = String.Join(",", UserManager.GetRoles(a.Id))
            }).ToList()

            Return View(accts.Where(Function(c) c.Role = "Student").ToList())
        End Function

        <Route("Student/{id}/Grades")>
        Function Grades(ByVal id As String) As ActionResult
            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = id.ToString())

            Return View(u)
        End Function

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
        ' Account
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