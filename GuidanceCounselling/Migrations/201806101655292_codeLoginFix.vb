Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class codeLoginFix
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.OTPLinks", "Usable", Function(c) c.Boolean())
            DropColumn("dbo.OTPLinks", "IsUsed")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.OTPLinks", "IsUsed", Function(c) c.Boolean())
            DropColumn("dbo.OTPLinks", "Usable")
        End Sub
    End Class
End Namespace
