Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class codeLogin
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.OTPLinks", "IsUsed", Function(c) c.Boolean())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.OTPLinks", "IsUsed")
        End Sub
    End Class
End Namespace
