Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class NCAEGradesfixidentitymodel
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.NCAEGradeAptitudes",
                Function(c) New With
                    {
                        .NCAEGradeAptitudeId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .StandardScore = c.Decimal(precision := 18, scale := 2),
                        .PercentileRank = c.Int(),
                        .NCAEGradeId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.NCAEGradeAptitudeId) _
                .ForeignKey("dbo.NCAEGrades", Function(t) t.NCAEGradeId, cascadeDelete := True) _
                .Index(Function(t) t.NCAEGradeId)
            
            CreateTable(
                "dbo.NCAEGrades",
                Function(c) New With
                    {
                        .NCAEGradeId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .UserId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.NCAEGradeId) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.NCAEGradeSubjects",
                Function(c) New With
                    {
                        .NCAEGradeSubjectId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .PercentageScore = c.Decimal(precision := 18, scale := 2),
                        .RankOverall = c.Int(),
                        .NCAEGradeId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.NCAEGradeSubjectId) _
                .ForeignKey("dbo.NCAEGrades", Function(t) t.NCAEGradeId, cascadeDelete := True) _
                .Index(Function(t) t.NCAEGradeId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.NCAEGradeSubjects", "NCAEGradeId", "dbo.NCAEGrades")
            DropForeignKey("dbo.NCAEGradeAptitudes", "NCAEGradeId", "dbo.NCAEGrades")
            DropForeignKey("dbo.NCAEGrades", "UserId", "dbo.AspNetUsers")
            DropIndex("dbo.NCAEGradeSubjects", New String() { "NCAEGradeId" })
            DropIndex("dbo.NCAEGrades", New String() { "UserId" })
            DropIndex("dbo.NCAEGradeAptitudes", New String() { "NCAEGradeId" })
            DropTable("dbo.NCAEGradeSubjects")
            DropTable("dbo.NCAEGrades")
            DropTable("dbo.NCAEGradeAptitudes")
        End Sub
    End Class
End Namespace
