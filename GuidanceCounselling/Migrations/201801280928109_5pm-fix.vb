Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class _5pmfix
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Announcements", "Name", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Announcements", "Name")
        End Sub
    End Class
End Namespace
