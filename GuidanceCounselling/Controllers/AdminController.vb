Imports System.Data.Entity
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin

Namespace Controllers
    <RoutePrefix("Admin")>
    <Route("{action=Index}")>
    <Authorize(Roles:="IT Admin")>
    Public Class AdminController
        Inherits Controller

        Private db As New ApplicationDbContext

        Function Index() As ActionResult
            Return View()
        End Function

        Function Accounts() As ActionResult
            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim users = db.Users.Where(Function(u) u.Email <> User.Identity.Name).ToList()

            Dim accts As List(Of AccountsViewModel) = users.Select(Function(a) New AccountsViewModel() With {
                .UserId = a.Id,
                .Name = a.getFullName,
                .Email = a.Email,
                .IsDisabled = a.IsDisabled,
                .Role = String.Join(",", UserManager.GetRoles(a.Id))
            }).ToList()

            Return View(accts)
        End Function

        Function ChangeStatus(ByVal id As String) As ActionResult
            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = id)

            If u Is Nothing Then
                Return RedirectToAction("Accounts")
            Else
                u.IsDisabled = Not (u.IsDisabled)
                db.SaveChanges()
                Return RedirectToAction("Accounts")
            End If
        End Function

        '
        '
        '
        '
        'Grades

        Function Grades() As ActionResult
            Return View(db.Grades.ToList())
        End Function

        <Route("Grades/Add")>
        Function GradesAdd() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <Route("Grades/Add")>
        Function GradesAdd(<Bind(Include:="GradeId,Name,DateCreated")> ByVal grade As Grade) As ActionResult
            grade.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            If ModelState.IsValid Then
                db.Grades.Add(grade)
                db.SaveChanges()
                Return RedirectToAction("Grades")
            End If

            Return View(grade)
        End Function

        <Route("Grades/{id}/Edit")>
        Function GradesEdit(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim grade As Grade = db.Grades.FirstOrDefault(Function(g) g.GradeId = id)

            If grade Is Nothing Then
                Return HttpNotFound()
            End If

            Return View(grade)
        End Function

        <HttpPost>
        <Route("Grades/{id}/Edit")>
        Function GradesEdit(<Bind(Include:="GradeId,Name,DateCreated")> ByVal grade As Grade) As ActionResult
            If ModelState.IsValid Then
                db.Entry(grade).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Grades")
            End If

            Return View(grade)
        End Function

        '
        '
        '
        '
        'Sections

        <Route("{id}/Sections")>
        Function Sections(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim grade As Grade = db.Grades.FirstOrDefault(Function(g) g.GradeId = id)

            If grade Is Nothing Then
                Return HttpNotFound()
            End If

            ViewBag.GradeId = grade.GradeId

            Return View(grade.Sections)
        End Function

        <Route("{id}/Sections/Add")>
        Function SectionsAdd(ByVal id As Integer?) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim grade As Grade = db.Grades.FirstOrDefault(Function(g) g.GradeId = id)

            If grade Is Nothing Then
                Return HttpNotFound()
            End If

            Dim section As New Section With {.GradeId = id}

            Return View(section)
        End Function

        <Route("{id}/Sections/Add")>
        <HttpPost>
        Function SectionsAdd(<Bind(Include:="Sectionid,Name,GradeId")> ByVal section As Section) As ActionResult
            section.DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))

            If ModelState.IsValid Then
                db.Sections.Add(section)
                db.SaveChanges()
                Return RedirectToAction("Sections", New With {.id = section.GradeId})
            End If

            Return View(section)
        End Function

        <Route("Sections/{id}/Edit")>
        Function SectionsEdit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim section As Section = db.Sections.Find(id)

            If IsNothing(section) Then
                Return HttpNotFound()
            End If

            Return View(section)
        End Function

        <Route("Sections/{id}/Edit")>
        <HttpPost>
        Function SectionsEdit(<Bind(Include:="SectionId,Name,GradeId,DateCreated")> ByVal section As Section) As ActionResult
            If ModelState.IsValid Then
                db.Entry(section).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Sections", New With {.id = section.GradeId})
            End If

            Return View(section)
        End Function

        <Route("Sections/{id}/Students/Manage")>
        Function ManageStudents(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim section As Section = db.Sections.Find(id)

            If IsNothing(section) Then
                Return HttpNotFound()
            End If

            ' Get All Users
            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim users = db.Users.Where(Function(u) u.Email <> User.Identity.Name And u.IsDisabled = False).ToList()
            Dim accts As List(Of SectionAccountsViewModel) = users.Select(Function(a) New SectionAccountsViewModel() With {
                .UserId = a.Id,
                .Name = a.getFullName,
                .Email = a.Email,
                .IsDisabled = a.IsDisabled,
                .SectionId = a.SectionId,
                .Role = String.Join(",", UserManager.GetRoles(a.Id))
            }).ToList()

            ' Get Users with Student Role
            Dim s_accts As String() = accts.Where(Function(a) a.Role = "Student").Select(Function(c) c.UserId).ToArray()

            ' Instantiate Create Model
            Dim msm As New ManageStudentsModel(section)

            ' Assign students of the section
            Dim currentStudents As List(Of ApplicationUser) = db.Users.Where(Function(u) s_accts.Contains(u.Id) And u.SectionId = section.SectionId).ToList()

            ' Assign students that don't belong to that section
            Dim nonStudents As List(Of ApplicationUser) = db.Users.Where(Function(u) s_accts.Contains(u.Id) And u.SectionId Is Nothing).ToList()

            msm.Students = currentStudents.Select(Function(ss) New UsersSectionModel With {
            .Name = ss.getFullName, .UserId = ss.Id, .SectionId = ss.SectionId}).ToList()

            msm.NonStudents = nonStudents.Select(Function(ss) New UsersSectionModel With {
            .Name = ss.getFullName, .UserId = ss.Id, .SectionId = Nothing}).ToList()

            Return View(msm)
        End Function

        <Route("Sections/{id}/Students/Manage")>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function ManageStudents(model As ManageStudentsModel) As ActionResult

            ' Check section
            Dim section As Section = db.Sections.Find(model.SectionId)

            If IsNothing(model.SectionId) Then
                Return HttpNotFound()
            End If

            If ModelState.IsValid Then
                ' Remove All Current Students beloning to the section
                Dim refresh As List(Of ApplicationUser) = db.Users.Where(Function(r) r.SectionId = model.SectionId).ToList()
                For Each m In refresh
                    m.SectionId = Nothing
                Next

                ' Assign new students
                If Not (model.Students Is Nothing) Then
                    Dim newStudentsArray = model.Students.Select(Function(s) s.UserId).ToArray()
                    Dim newStudents As List(Of ApplicationUser) = db.Users.Where(Function(n) newStudentsArray.Contains(n.Id)).ToList()

                    For Each m In newStudents
                        m.SectionId = model.SectionId
                    Next
                End If

                db.SaveChanges()

                Return RedirectToAction("Sections", New With {.id = section.GradeId})
            End If
        End Function

        '
        '
        '
        '
        'Students

        Function Students() As ActionResult
            Dim student_role As String = db.Roles.FirstOrDefault(Function(r) r.Name = "Student").Id
            Dim users = db.Users.Where(Function(u) u.Roles.Any(Function(r) r.RoleId = student_role)).ToList()
            Return View(users)
        End Function

        '
        '
        '
        '
        ' Families

        Function Families() As ActionResult
            'Dim families = db.Families.ToList()
            'families.ForEach(Function(f) f.FamilyMembers = db.Users.Where(Function(u) u.FamilyId = f.FamilyId))
            Return View(db.Families.ToList())
        End Function

        <Route("Family/Add")>
        Public Async Function AddFamily() As Task(Of ActionResult)
            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim fm As New FamilyCreateModel
            ' Non-async method:
            'fm.FamilyMembers = db.Users.Where(Function(u) u.FamilyId Is Nothing).ToList().Select(Function(um) New UsersFamilyModel() With {
            '.Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            Dim ufm As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId Is Nothing).ToListAsync()

            fm.FamilyMembers = ufm.Select(Function(um) New UsersFamilyModel With {
            .Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            Return View(fm)
        End Function

        <Route("Family/Add")>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function AddFamily(model As FamilyCreateModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim f As New Family With {.Name = model.Name, .DateCreated = DateTimeOffset.Now.ToOffset(New TimeSpan(8, 0, 0))}

                db.Families.Add(f)

                Dim u_a As String() = model.FamilyMembers.Select(Function(s) s.UserId).ToArray()
                Dim new_members As List(Of ApplicationUser) = db.Users.Where(Function(u) u_a.Contains(u.Id)).ToList()
                ' new_members.ForEach(Function(n) n.FamilyId = f.FamilyId)

                For Each m In new_members
                    m.FamilyId = f.FamilyId
                Next

                Await db.SaveChangesAsync()

                Return RedirectToAction("Families")
            End If

            ' if error
            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim fm As New FamilyCreateModel
            Dim ufm As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId Is Nothing).ToListAsync()

            fm.FamilyMembers = ufm.Select(Function(um) New UsersFamilyModel With {
            .Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            Return View(fm)
        End Function

        <Route("Family/{id}/Edit")>
        Public Async Function EditFamily(ByVal id As Integer?) As Task(Of ActionResult)
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim family As Family = db.Families.FirstOrDefault(Function(f) f.FamilyId = id)
            If family Is Nothing Then
                Return HttpNotFound()
            End If

            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim fm As New FamilyEditModel(family)
            ' Non-async method:
            'fm.FamilyMembers = db.Users.Where(Function(u) u.FamilyId Is Nothing).ToList().Select(Function(um) New UsersFamilyModel() With {
            '.Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            ' Attach family members to model
            Dim ufm As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId = fm.FamilyId).ToListAsync()

            fm.FamilyMembers = ufm.Select(Function(um) New UsersFamilyModel With {
            .Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            ' Attach nonfamily members to model
            Dim nfm As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId Is Nothing).ToListAsync()

            fm.NonMembers = nfm.Select(Function(um) New UsersFamilyModel With {
            .Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            Return View(fm)
        End Function

        <Route("Family/{id}/Edit")>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function EditFamily(model As FamilyEditModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim check_family As Family = db.Families.FirstOrDefault(Function(f) f.FamilyId = model.FamilyId)
                If check_family Is Nothing Then
                    Return HttpNotFound()
                End If

                check_family.Name = model.Name

                Dim remove_users As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId = model.FamilyId).ToListAsync()

                For Each item In remove_users
                    item.FamilyId = Nothing
                Next

                If model.FamilyMembers IsNot Nothing Then
                    Dim u_a As String() = model.FamilyMembers.Select(Function(s) s.UserId).ToArray()
                    Dim new_members As List(Of ApplicationUser) = Await db.Users.Where(Function(n) u_a.Contains(n.Id)).ToListAsync()

                    For Each item In new_members
                        item.FamilyId = model.FamilyId
                    Next
                End If

                Await db.SaveChangesAsync()
                Return RedirectToAction("Families")
            End If

            ' If error
            Dim family As Family = db.Families.FirstOrDefault(Function(f) f.FamilyId = model.FamilyId)
            If family Is Nothing Then
                Return HttpNotFound()
            End If

            Dim UserManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim fm As New FamilyEditModel(family)

            ' Attach family members to model
            Dim ufm As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId = fm.FamilyId).ToListAsync()

            fm.FamilyMembers = ufm.Select(Function(um) New UsersFamilyModel With {
            .Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            ' Attach nonfamily members to model
            Dim nfm As List(Of ApplicationUser) = Await db.Users.Where(Function(u) u.FamilyId Is Nothing).ToListAsync()

            fm.NonMembers = nfm.Select(Function(um) New UsersFamilyModel With {
            .Name = um.getFullName, .Role = String.Join(",", UserManager.GetRoles(um.Id)), .UserId = um.Id}).ToList()

            Return View(fm)
        End Function

        '
        '
        '
        '
        ' Accounts

        Private _signInManager As ApplicationSignInManager
        Private _userManager As ApplicationUserManager

        Public Sub New()
        End Sub

        Public Sub New(appUserMan As ApplicationUserManager, signInMan As ApplicationSignInManager)
            UserManager = appUserMan
            SignInManager = signInMan
        End Sub

        Public Property SignInManager() As ApplicationSignInManager
            Get
                Return If(_signInManager, HttpContext.GetOwinContext().[Get](Of ApplicationSignInManager)())
            End Get
            Private Set
                _signInManager = Value
            End Set
        End Property

        Public Property UserManager() As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Private Set
                _userManager = Value
            End Set
        End Property

        <Route("Account/Add")>
        Function AddAccount() As ActionResult
            ViewBag.Roles = db.Roles.ToList()
            Return View()
        End Function

        <HttpPost>
        <Route("Account/Add")>
        <ValidateAntiForgeryToken>
        Public Async Function AddAccount(model As RegisterViewModel) As Task(Of ActionResult)

            Dim role As String = Request.Form("Role")
            If db.Roles.Any(Function(s) s.Name = role) Then

            Else
                ViewBag.Roles = db.Roles.ToList()
                Return View(model)
            End If

            Dim gender As Byte = Convert.ToByte(Request.Form("Gender"))

            If ModelState.IsValid Then
                Dim user = New ApplicationUser() With {
                    .UserName = model.Email,
                    .Email = model.Email,
                    .FirstName = model.FirstName,
                    .MiddleName = model.MiddleName,
                    .Gender = gender,
                    .LastName = model.LastName,
                    .BirthDate = model.BirthDate.ToOffset(New TimeSpan(8, 0, 0))
                }

                Dim result = Await UserManager.CreateAsync(user, model.Password)
                If result.Succeeded Then
                    ' Await SignInManager.SignInAsync(user, isPersistent:=False, rememberBrowser:=False)

                    ' For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    ' Send an email with this link
                    ' Dim code = Await UserManager.GenerateEmailConfirmationTokenAsync(user.Id)
                    ' Dim callbackUrl = Url.Action("ConfirmEmail", "Account", New With { .userId = user.Id, .code = code }, protocol := Request.Url.Scheme)
                    ' Await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=""" & callbackUrl & """>here</a>")
                    UserManager.AddToRole(user.Id, role)
                    Return RedirectToAction("Accounts", "Admin")
                End If
                AddErrors(result)
            End If

            ViewBag.Roles = db.Roles.ToList()
            ' If we got this far, something failed, redisplay form
            Return View(model)
        End Function

        <Route("Account/{id}/Edit")>
        Public Function EditAccount(ByVal id As Guid) As ActionResult
            Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = id.ToString())

            Dim r As New UserEditModel(u)

            Return View(r)
        End Function

        <Route("Account/{id}/Edit")>
        <HttpPost()>
        <ValidateAntiForgeryToken>
        Public Function EditAccount(model As UserEditModel) As ActionResult
            If ModelState.IsValid Then
                If db.Users.Any(Function(k) k.Email = model.Email And k.Id = model.UserId) Then
                    ModelState.AddModelError(String.Empty, "There is already another user with this email.")
                    Return View(model)
                End If

                Dim u As ApplicationUser = db.Users.FirstOrDefault(Function(a) a.Id = model.UserId.ToString())

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

        Private Sub AddErrors(result As IdentityResult)
            For Each [error] In result.Errors
                ModelState.AddModelError("", [error])
            Next
        End Sub
    End Class
End Namespace