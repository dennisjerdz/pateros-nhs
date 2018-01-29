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
    Public Class SubjectGradesController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: SubjectGrades
        Function Index() As ActionResult
            Dim subjectGrades = db.SubjectGrades.Include(Function(s) s.StudentGrade)
            Return View(subjectGrades.ToList())
        End Function

        ' GET: SubjectGrades/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim subjectGrade As SubjectGrade = db.SubjectGrades.Find(id)
            If IsNothing(subjectGrade) Then
                Return HttpNotFound()
            End If
            Return View(subjectGrade)
        End Function

        ' GET: SubjectGrades/Create
        Function Create() As ActionResult
            ViewBag.StudentGradeId = New SelectList(db.StudentGrades, "StudentGradeId", "UserId")
            Return View()
        End Function

        ' POST: SubjectGrades/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="SubjectGradeId,StudentGradeId,Subject,Grade,DateCreated")> ByVal subjectGrade As SubjectGrade) As ActionResult
            If ModelState.IsValid Then
                db.SubjectGrades.Add(subjectGrade)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.StudentGradeId = New SelectList(db.StudentGrades, "StudentGradeId", "UserId", subjectGrade.StudentGradeId)
            Return View(subjectGrade)
        End Function

        ' GET: SubjectGrades/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim subjectGrade As SubjectGrade = db.SubjectGrades.Find(id)
            If IsNothing(subjectGrade) Then
                Return HttpNotFound()
            End If
            ViewBag.StudentGradeId = New SelectList(db.StudentGrades, "StudentGradeId", "UserId", subjectGrade.StudentGradeId)
            Return View(subjectGrade)
        End Function

        ' POST: SubjectGrades/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="SubjectGradeId,StudentGradeId,Subject,Grade,DateCreated")> ByVal subjectGrade As SubjectGrade) As ActionResult
            If ModelState.IsValid Then
                db.Entry(subjectGrade).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.StudentGradeId = New SelectList(db.StudentGrades, "StudentGradeId", "UserId", subjectGrade.StudentGradeId)
            Return View(subjectGrade)
        End Function

        ' GET: SubjectGrades/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim subjectGrade As SubjectGrade = db.SubjectGrades.Find(id)
            If IsNothing(subjectGrade) Then
                Return HttpNotFound()
            End If
            Return View(subjectGrade)
        End Function

        ' POST: SubjectGrades/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim subjectGrade As SubjectGrade = db.SubjectGrades.Find(id)
            db.SubjectGrades.Remove(subjectGrade)
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
