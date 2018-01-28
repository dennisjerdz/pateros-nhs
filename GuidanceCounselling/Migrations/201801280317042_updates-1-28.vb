Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class updates128
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.ExamStudents", "TakenAt", Function(c) c.DateTimeOffset(precision := 7))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.ExamStudents", "TakenAt", Function(c) c.DateTimeOffset(nullable := False, precision := 7))
        End Sub
    End Class
End Namespace
