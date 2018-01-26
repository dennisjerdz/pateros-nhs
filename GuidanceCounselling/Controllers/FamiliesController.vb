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
    Public Class FamiliesController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: Families
        Function Index() As ActionResult
            Return View(db.Families.ToList())
        End Function

        ' GET: Families/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim family As Family = db.Families.Find(id)
            If IsNothing(family) Then
                Return HttpNotFound()
            End If
            Return View(family)
        End Function

        ' GET: Families/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Families/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="FamilyId,Name,DateCreated")> ByVal family As Family) As ActionResult
            If ModelState.IsValid Then
                db.Families.Add(family)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(family)
        End Function

        ' GET: Families/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim family As Family = db.Families.Find(id)
            If IsNothing(family) Then
                Return HttpNotFound()
            End If
            Return View(family)
        End Function

        ' POST: Families/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="FamilyId,Name,DateCreated")> ByVal family As Family) As ActionResult
            If ModelState.IsValid Then
                db.Entry(family).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(family)
        End Function

        ' GET: Families/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim family As Family = db.Families.Find(id)
            If IsNothing(family) Then
                Return HttpNotFound()
            End If
            Return View(family)
        End Function

        ' POST: Families/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim family As Family = db.Families.Find(id)
            db.Families.Remove(family)
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
