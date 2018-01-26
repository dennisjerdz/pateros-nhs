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
    Public Class QuestionTFRanksController
        Inherits System.Web.Mvc.Controller

        Private db As New ApplicationDbContext

        ' GET: QuestionTFRanks
        Function Index() As ActionResult
            Dim questionTFRanks = db.QuestionTFRanks.Include(Function(q) q.QuestionGroup)
            Return View(questionTFRanks.ToList())
        End Function

        ' GET: QuestionTFRanks/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionTFRank As QuestionTFRank = db.QuestionTFRanks.Find(id)
            If IsNothing(questionTFRank) Then
                Return HttpNotFound()
            End If
            Return View(questionTFRank)
        End Function

        ' GET: QuestionTFRanks/Create
        Function Create() As ActionResult
            ViewBag.QuestionGroupId = New SelectList(db.QuestionGroups, "QuestionGroupId", "Name")
            Return View()
        End Function

        ' POST: QuestionTFRanks/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="QuestionTFRankId,QuestionGroupId,Question,DateCreated")> ByVal questionTFRank As QuestionTFRank) As ActionResult
            If ModelState.IsValid Then
                db.QuestionTFRanks.Add(questionTFRank)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.QuestionGroupId = New SelectList(db.QuestionGroups, "QuestionGroupId", "Name", questionTFRank.QuestionGroupId)
            Return View(questionTFRank)
        End Function

        ' GET: QuestionTFRanks/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionTFRank As QuestionTFRank = db.QuestionTFRanks.Find(id)
            If IsNothing(questionTFRank) Then
                Return HttpNotFound()
            End If
            ViewBag.QuestionGroupId = New SelectList(db.QuestionGroups, "QuestionGroupId", "Name", questionTFRank.QuestionGroupId)
            Return View(questionTFRank)
        End Function

        ' POST: QuestionTFRanks/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="QuestionTFRankId,QuestionGroupId,Question,DateCreated")> ByVal questionTFRank As QuestionTFRank) As ActionResult
            If ModelState.IsValid Then
                db.Entry(questionTFRank).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.QuestionGroupId = New SelectList(db.QuestionGroups, "QuestionGroupId", "Name", questionTFRank.QuestionGroupId)
            Return View(questionTFRank)
        End Function

        ' GET: QuestionTFRanks/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim questionTFRank As QuestionTFRank = db.QuestionTFRanks.Find(id)
            If IsNothing(questionTFRank) Then
                Return HttpNotFound()
            End If
            Return View(questionTFRank)
        End Function

        ' POST: QuestionTFRanks/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim questionTFRank As QuestionTFRank = db.QuestionTFRanks.Find(id)
            db.QuestionTFRanks.Remove(questionTFRank)
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
