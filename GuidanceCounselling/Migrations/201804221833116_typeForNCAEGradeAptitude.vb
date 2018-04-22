Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class typeForNCAEGradeAptitude
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.NCAEGradeAptitudes", "Type", Function(c) c.Int())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.NCAEGradeAptitudes", "Type")
        End Sub
    End Class
End Namespace
