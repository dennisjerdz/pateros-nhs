Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class OTP
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.OTPLinks",
                Function(c) New With
                    {
                        .OTPLinkId = c.Guid(nullable := False),
                        .Code = c.Int(),
                        .UserId = c.String(maxLength := 128),
                        .AvailabilityEnd = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.OTPLinkId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.OTPLinks", "UserId", "dbo.AspNetUsers")
            DropIndex("dbo.OTPLinks", New String() { "UserId" })
            DropTable("dbo.OTPLinks")
        End Sub
    End Class
End Namespace
