﻿Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private db As New ApplicationDbContext

    Function Index() As ActionResult

        If User.Identity.Name IsNot Nothing Then
            Return View(db.Announcements.Where(Function(a) a.Active = True).ToList())
        Else
            Return View()
        End If

    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function
End Class
