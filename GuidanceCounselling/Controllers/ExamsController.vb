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
    Public Class ExamsController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: Exams
        Function Index() As ActionResult
            Return View(db.Exams.ToList())
        End Function

        ' GET: Exams/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If
            Return View(exam)
        End Function

        ' GET: Exams/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Exams/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ExamId,Name,DateCreated")> ByVal exam As Exam) As ActionResult
            If ModelState.IsValid Then
                db.Exams.Add(exam)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(exam)
        End Function

        ' GET: Exams/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If
            Return View(exam)
        End Function

        ' POST: Exams/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ExamId,Name,DateCreated")> ByVal exam As Exam) As ActionResult
            If ModelState.IsValid Then
                db.Entry(exam).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(exam)
        End Function

        ' GET: Exams/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim exam As Exam = db.Exams.Find(id)
            If IsNothing(exam) Then
                Return HttpNotFound()
            End If
            Return View(exam)
        End Function

        ' POST: Exams/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim exam As Exam = db.Exams.Find(id)
            db.Exams.Remove(exam)
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
