Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class StudentGradeAdd
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.StudentGrades",
                Function(c) New With
                    {
                        .StudentGradeId = c.Int(nullable := False, identity := True),
                        .UserId = c.String(maxLength := 128),
                        .Name = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.StudentGradeId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.SubjectGrades",
                Function(c) New With
                    {
                        .SubjectGradeId = c.Int(nullable := False, identity := True),
                        .StudentGradeId = c.Int(nullable := False),
                        .Subject = c.String(),
                        .Grade = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.SubjectGradeId) _
                .ForeignKey("dbo.StudentGrades", Function(t) t.StudentGradeId, cascadeDelete := True) _
                .Index(Function(t) t.StudentGradeId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.StudentGrades", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.SubjectGrades", "StudentGradeId", "dbo.StudentGrades")
            DropIndex("dbo.SubjectGrades", New String() { "StudentGradeId" })
            DropIndex("dbo.StudentGrades", New String() { "UserId" })
            DropTable("dbo.SubjectGrades")
            DropTable("dbo.StudentGrades")
        End Sub
    End Class
End Namespace
