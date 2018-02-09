Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class addedexpiryDateonAnnouncements
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Announcements", "DateExpired", Function(c) c.DateTimeOffset(nullable := False, precision := 7))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Announcements", "DateExpired")
        End Sub
    End Class
End Namespace
