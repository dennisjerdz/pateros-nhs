Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AnnouncementAndMessaging
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Announcements",
                Function(c) New With
                    {
                        .AnnouncementId = c.Int(nullable := False, identity := True),
                        .Content = c.String(),
                        .Active = c.Boolean(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.AnnouncementId)
            
            CreateTable(
                "dbo.Messages",
                Function(c) New With
                    {
                        .MessageId = c.Int(nullable := False, identity := True),
                        .Content = c.String(),
                        .UserId = c.String(maxLength := 128),
                        .ConversationId = c.Int(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.MessageId) _
                .ForeignKey("dbo.Conversations", Function(t) t.ConversationId, cascadeDelete := True) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.ConversationId)
            
            CreateTable(
                "dbo.Conversations",
                Function(c) New With
                    {
                        .ConversationId = c.Int(nullable := False, identity := True),
                        .SenderId = c.String(maxLength := 128),
                        .ReceiverId = c.String(maxLength := 128),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ConversationId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.ReceiverId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.SenderId) _
                .Index(Function(t) t.SenderId) _
                .Index(Function(t) t.ReceiverId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.Conversations", "SenderId", "dbo.AspNetUsers")
            DropForeignKey("dbo.Conversations", "ReceiverId", "dbo.AspNetUsers")
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations")
            DropIndex("dbo.Conversations", New String() { "ReceiverId" })
            DropIndex("dbo.Conversations", New String() { "SenderId" })
            DropIndex("dbo.Messages", New String() { "ConversationId" })
            DropIndex("dbo.Messages", New String() { "UserId" })
            DropTable("dbo.Conversations")
            DropTable("dbo.Messages")
            DropTable("dbo.Announcements")
        End Sub
    End Class
End Namespace
