Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ncaegradesUpdate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.NCAEGrades", "Name", Function(c) c.String(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.NCAEGrades", "Name", Function(c) c.String())
        End Sub
    End Class
End Namespace
