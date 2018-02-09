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
    End Class
End Namespace