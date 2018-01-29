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
    Public Class StudentGradesController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: StudentGrades
        Function Index() As ActionResult
            Dim studentGrades = db.StudentGrades.Include(Function(s) s.User)
            Return View(studentGrades.ToList())
        End Function

        ' GET: StudentGrades/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            If IsNothing(studentGrade) Then
                Return HttpNotFound()
            End If
            Return View(studentGrade)
        End Function

        ' GET: StudentGrades/Create
        Function Create() As ActionResult
            ViewBag.UserId = New SelectList(db.Users, "Id", "FirstName")
            Return View()
        End Function

        ' POST: StudentGrades/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="StudentGradeId,UserId,Name,DateCreated")> ByVal studentGrade As StudentGrade) As ActionResult
            If ModelState.IsValid Then
                db.StudentGrades.Add(studentGrade)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.UserId = New SelectList(db.Users, "Id", "FirstName", studentGrade.UserId)
            Return View(studentGrade)
        End Function

        ' GET: StudentGrades/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            If IsNothing(studentGrade) Then
                Return HttpNotFound()
            End If
            ViewBag.UserId = New SelectList(db.Users, "Id", "FirstName", studentGrade.UserId)
            Return View(studentGrade)
        End Function

        ' POST: StudentGrades/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="StudentGradeId,UserId,Name,DateCreated")> ByVal studentGrade As StudentGrade) As ActionResult
            If ModelState.IsValid Then
                db.Entry(studentGrade).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.UserId = New SelectList(db.Users, "Id", "FirstName", studentGrade.UserId)
            Return View(studentGrade)
        End Function

        ' GET: StudentGrades/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            If IsNothing(studentGrade) Then
                Return HttpNotFound()
            End If
            Return View(studentGrade)
        End Function

        ' POST: StudentGrades/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim studentGrade As StudentGrade = db.StudentGrades.Find(id)
            db.StudentGrades.Remove(studentGrade)
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
