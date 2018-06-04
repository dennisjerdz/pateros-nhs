Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class OTPupdate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.OTPLinks")
            AlterColumn("dbo.OTPLinks", "OTPLinkId", Function(c) c.String(nullable := False, maxLength := 128))
            AddPrimaryKey("dbo.OTPLinks", "OTPLinkId")
        End Sub
        
        Public Overrides Sub Down()
            DropPrimaryKey("dbo.OTPLinks")
            AlterColumn("dbo.OTPLinks", "OTPLinkId", Function(c) c.Guid(nullable := False))
            AddPrimaryKey("dbo.OTPLinks", "OTPLinkId")
        End Sub
    End Class
End Namespace
