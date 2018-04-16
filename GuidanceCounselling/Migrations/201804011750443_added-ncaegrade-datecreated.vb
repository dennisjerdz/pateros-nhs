Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class addedncaegradedatecreated
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.NCAEGrades", "DateCreated", Function(c) c.DateTimeOffset(nullable := False, precision := 7))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.NCAEGrades", "DateCreated")
        End Sub
    End Class
End Namespace
