Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedLoginHistory
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.LoginHistories",
                Function(c) New With
                    {
                        .LoginHistoryId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .Login = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.LoginHistoryId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.LoginHistories")
        End Sub
    End Class
End Namespace
