Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ExamPropertyFix
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.ExamStudentOneToFives", "Answer", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.ExamStudentOneToFives", "Answer", Function(c) c.Boolean(nullable := False))
        End Sub
    End Class
End Namespace
