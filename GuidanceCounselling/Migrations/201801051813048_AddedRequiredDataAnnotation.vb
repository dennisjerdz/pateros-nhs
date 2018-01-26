Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedRequiredDataAnnotation
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Exams", "Name", Function(c) c.String(nullable := False))
            AlterColumn("dbo.QuestionGroups", "Name", Function(c) c.String(nullable := False))
            AlterColumn("dbo.QuestionGroups", "DisplayName", Function(c) c.String(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.QuestionGroups", "DisplayName", Function(c) c.String())
            AlterColumn("dbo.QuestionGroups", "Name", Function(c) c.String())
            AlterColumn("dbo.Exams", "Name", Function(c) c.String())
        End Sub
    End Class
End Namespace
