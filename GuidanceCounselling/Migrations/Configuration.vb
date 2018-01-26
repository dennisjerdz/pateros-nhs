Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Migrations

    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of ApplicationDbContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
        End Sub

        Protected Overrides Sub Seed(context As ApplicationDbContext)
            '  This method will be called after migrating to the latest version.

            '  You can use the DbSet(Of T).AddOrUpdate() helper extension method 
            '  to avoid creating duplicate seed data. E.g.
            '
            '    context.People.AddOrUpdate(
            '       Function(c) c.FullName,
            '       New Customer() With {.FullName = "Andrew Peters"},
            '       New Customer() With {.FullName = "Brice Lambson"},
            '       New Customer() With {.FullName = "Rowan Miller"})

            Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(context))

            If Not (context.Roles.Any(Function(r) r.Name = "IT Admin")) Then
                Dim role As New IdentityRole With {.Name = "IT Admin"}
                roleManager.Create(role)
            End If

            If Not (context.Roles.Any(Function(r) r.Name = "Guidance Counselor")) Then
                Dim role As New IdentityRole With {.Name = "Guidance Counselor"}
                roleManager.Create(role)
            End If

            If Not (context.Roles.Any(Function(r) r.Name = "Academic Adviser")) Then
                Dim role As New IdentityRole With {.Name = "Academic Adviser"}
                roleManager.Create(role)
            End If

            If Not (context.Roles.Any(Function(r) r.Name = "Family Member")) Then
                Dim role As New IdentityRole With {.Name = "Family Member"}
                roleManager.Create(role)
            End If

            If Not (context.Roles.Any(Function(r) r.Name = "Student")) Then
                Dim role As New IdentityRole With {.Name = "Student"}
                roleManager.Create(role)
            End If

            If Not (context.Grades.Any(Function(g) g.Name = "Grade 10")) Then
                Dim grade As New Grade With {.Name = "Grade 10", .DateCreated = Date.Now}
                context.Grades.Add(grade)
                context.SaveChanges()
            End If

            If Not (context.Grades.Any(Function(s) s.Name = "S-A")) Then
                Dim section As New Section With {
                    .GradeId = context.Grades.FirstOrDefault(Function(g) g.Name = "Grade 10").GradeId,
                    .Name = "S-A"
                }
                context.Sections.Add(section)
                context.SaveChanges()
            End If

            Dim userManager As New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(context))

            If Not (context.Users.Any(Function(u) u.Email = "itadmin@gmail.com")) Then
                Dim user As New ApplicationUser With {
                    .FirstName = "IT", .MiddleName = "", .LastName = "Admin", .UserName = "itadmin@gmail.com",
                    .Email = "itadmin@gmail.com", .BirthDate = Date.Now, .EmailConfirmed = True, .IsDisabled = False
                }

                userManager.Create(user, "Testing@123")
                userManager.AddToRole(user.Id, "IT Admin")
            End If

            If Not (context.Users.Any(Function(u) u.Email = "guidancecounselor@gmail.com")) Then
                Dim user As New ApplicationUser With {
                    .FirstName = "Guidance", .MiddleName = "", .LastName = "Counselor", .UserName = "guidancecounselor@gmail.com",
                    .Email = "guidancecounselor@gmail.com", .BirthDate = Date.Now, .EmailConfirmed = True, .IsDisabled = False
                }

                userManager.Create(user, "Testing@123")
                userManager.AddToRole(user.Id, "Guidance Counselor")
            End If
        End Sub

    End Class

End Namespace
