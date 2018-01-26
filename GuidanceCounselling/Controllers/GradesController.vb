Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports GuidanceCounselling

Namespace Controllers
    Public Class GradesController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: Grades
        Function Index() As ActionResult
            Return View(db.Grades.ToList())
        End Function

        ' GET: Grades/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim grade As Grade = db.Grades.Find(id)
            If IsNothing(grade) Then
                Return HttpNotFound()
            End If
            Return View(grade)
        End Function

        ' GET: Grades/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Grades/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="GradeId,Name,DateCreated")> ByVal grade As Grade) As ActionResult
            If ModelState.IsValid Then
                db.Grades.Add(grade)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(grade)
        End Function

        ' GET: Grades/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim grade As Grade = db.Grades.Find(id)
            If IsNothing(grade) Then
                Return HttpNotFound()
            End If
            Return View(grade)
        End Function

        ' POST: Grades/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="GradeId,Name,DateCreated")> ByVal grade As Grade) As ActionResult
            If ModelState.IsValid Then
                db.Entry(grade).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(grade)
        End Function

        ' GET: Grades/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim grade As Grade = db.Grades.Find(id)
            If IsNothing(grade) Then
                Return HttpNotFound()
            End If
            Return View(grade)
        End Function

        ' POST: Grades/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim grade As Grade = db.Grades.Find(id)
            db.Grades.Remove(grade)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
