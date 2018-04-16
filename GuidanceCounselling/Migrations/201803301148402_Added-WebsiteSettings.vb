Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedWebsiteSettings
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.WebsiteConfigs",
                Function(c) New With
                    {
                        .WebSiteConfigId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .Value = c.String()
                    }) _
                .PrimaryKey(Function(t) t.WebSiteConfigId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.WebsiteConfigs")
        End Sub
    End Class
End Namespace
