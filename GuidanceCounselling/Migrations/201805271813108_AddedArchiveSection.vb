Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedArchiveSection
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ArchivedSectionStudents",
                Function(c) New With
                    {
                        .ArchivedSectionStudentsId = c.Int(nullable := False, identity := True),
                        .UserId = c.String(maxLength := 128),
                        .SectionId = c.Int(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ArchivedSectionStudentsId) _
                .ForeignKey("dbo.Sections", Function(t) t.SectionId, cascadeDelete := True) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.SectionId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ArchivedSectionStudents", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.ArchivedSectionStudents", "SectionId", "dbo.Sections")
            DropIndex("dbo.ArchivedSectionStudents", New String() { "SectionId" })
            DropIndex("dbo.ArchivedSectionStudents", New String() { "UserId" })
            DropTable("dbo.ArchivedSectionStudents")
        End Sub
    End Class
End Namespace
