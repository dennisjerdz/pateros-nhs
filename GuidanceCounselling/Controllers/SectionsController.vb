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
    Public Class SectionsController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: Sections
        Function Index() As ActionResult
            Dim sections = db.Sections.Include(Function(s) s.Grade)
            Return View(sections.ToList())
        End Function

        ' GET: Sections/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim section As Section = db.Sections.Find(id)
            If IsNothing(section) Then
                Return HttpNotFound()
            End If
            Return View(section)
        End Function

        ' GET: Sections/Create
        Function Create() As ActionResult
            ViewBag.GradeId = New SelectList(db.Grades, "GradeId", "Name")
            Return View()
        End Function

        ' POST: Sections/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Sectionid,Name,GradeId,DateCreated")> ByVal section As Section) As ActionResult
            If ModelState.IsValid Then
                db.Sections.Add(section)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.GradeId = New SelectList(db.Grades, "GradeId", "Name", section.GradeId)
            Return View(section)
        End Function

        ' GET: Sections/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim section As Section = db.Sections.Find(id)
            If IsNothing(section) Then
                Return HttpNotFound()
            End If
            ViewBag.GradeId = New SelectList(db.Grades, "GradeId", "Name", section.GradeId)
            Return View(section)
        End Function

        ' POST: Sections/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Sectionid,Name,GradeId,DateCreated")> ByVal section As Section) As ActionResult
            If ModelState.IsValid Then
                db.Entry(section).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.GradeId = New SelectList(db.Grades, "GradeId", "Name", section.GradeId)
            Return View(section)
        End Function

        ' GET: Sections/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim section As Section = db.Sections.Find(id)
            If IsNothing(section) Then
                Return HttpNotFound()
            End If
            Return View(section)
        End Function

        ' POST: Sections/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim section As Section = db.Sections.Find(id)
            db.Sections.Remove(section)
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
