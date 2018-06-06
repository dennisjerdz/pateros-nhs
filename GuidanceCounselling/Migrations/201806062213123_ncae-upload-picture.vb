Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ncaeuploadpicture
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.NCAEGrades", "PictureLocation", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.NCAEGrades", "PictureLocation")
        End Sub
    End Class
End Namespace
