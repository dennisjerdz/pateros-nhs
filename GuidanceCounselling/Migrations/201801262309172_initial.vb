Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ExamQuestionGroups",
                Function(c) New With
                    {
                        .ExamQuestionGroupId = c.Int(nullable := False, identity := True),
                        .ExamId = c.Int(nullable := False),
                        .QuestionGroupId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ExamQuestionGroupId) _
                .ForeignKey("dbo.Exams", Function(t) t.ExamId, cascadeDelete := True) _
                .ForeignKey("dbo.QuestionGroups", Function(t) t.QuestionGroupId, cascadeDelete := True) _
                .Index(Function(t) t.ExamId) _
                .Index(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.Exams",
                Function(c) New With
                    {
                        .ExamId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamId)
            
            CreateTable(
                "dbo.QuestionGroups",
                Function(c) New With
                    {
                        .QuestionGroupId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False),
                        .DisplayName = c.String(nullable := False),
                        .ExamType = c.Int(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.QuestionEssays",
                Function(c) New With
                    {
                        .QuestionEssayId = c.Int(nullable := False, identity := True),
                        .QuestionGroupId = c.Int(nullable := False),
                        .Question = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.QuestionEssayId) _
                .ForeignKey("dbo.QuestionGroups", Function(t) t.QuestionGroupId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.QuestionOneToFives",
                Function(c) New With
                    {
                        .QuestionOneToFiveId = c.Int(nullable := False, identity := True),
                        .QuestionGroupId = c.Int(nullable := False),
                        .Question = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.QuestionOneToFiveId) _
                .ForeignKey("dbo.QuestionGroups", Function(t) t.QuestionGroupId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.QuestionRowGroups",
                Function(c) New With
                    {
                        .QuestionRowGroupId = c.Int(nullable := False, identity := True),
                        .QuestionGroupId = c.Int(nullable := False),
                        .Col1Question = c.String(),
                        .Col1Result = c.String(),
                        .Col2Question = c.String(),
                        .Col2Result = c.String(),
                        .Col3Question = c.String(),
                        .Col3Result = c.String(),
                        .Col4Question = c.String(),
                        .Col4Result = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.QuestionRowGroupId) _
                .ForeignKey("dbo.QuestionGroups", Function(t) t.QuestionGroupId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.QuestionTFLists",
                Function(c) New With
                    {
                        .QuestionTFListId = c.Int(nullable := False, identity := True),
                        .QuestionGroupId = c.Int(nullable := False),
                        .Question = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.QuestionTFListId) _
                .ForeignKey("dbo.QuestionGroups", Function(t) t.QuestionGroupId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.QuestionTFRanks",
                Function(c) New With
                    {
                        .QuestionTFRankId = c.Int(nullable := False, identity := True),
                        .QuestionGroupId = c.Int(nullable := False),
                        .Question = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.QuestionTFRankId) _
                .ForeignKey("dbo.QuestionGroups", Function(t) t.QuestionGroupId, cascadeDelete := True) _
                .Index(Function(t) t.QuestionGroupId)
            
            CreateTable(
                "dbo.ExamStudentEssays",
                Function(c) New With
                    {
                        .ExamStudentEssayId = c.Int(nullable := False, identity := True),
                        .ExamStudentId = c.Int(nullable := False),
                        .QuestionEssayId = c.Int(nullable := False),
                        .Answer = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamStudentEssayId) _
                .ForeignKey("dbo.ExamStudents", Function(t) t.ExamStudentId, cascadeDelete := True) _
                .ForeignKey("dbo.QuestionEssays", Function(t) t.QuestionEssayId, cascadeDelete := True) _
                .Index(Function(t) t.ExamStudentId) _
                .Index(Function(t) t.QuestionEssayId)
            
            CreateTable(
                "dbo.ExamStudents",
                Function(c) New With
                    {
                        .ExamStudentId = c.Int(nullable := False, identity := True),
                        .ExamId = c.Int(nullable := False),
                        .UserId = c.String(maxLength := 128),
                        .AvailabilityStart = c.DateTimeOffset(nullable := False, precision := 7),
                        .AvailabilityEnd = c.DateTimeOffset(nullable := False, precision := 7),
                        .TakenAt = c.DateTimeOffset(nullable := False, precision := 7),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamStudentId) _
                .ForeignKey("dbo.Exams", Function(t) t.ExamId, cascadeDelete := True) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId) _
                .Index(Function(t) t.ExamId) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.AspNetUsers",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .FirstName = c.String(),
                        .MiddleName = c.String(),
                        .LastName = c.String(),
                        .BirthDate = c.DateTimeOffset(nullable := False, precision := 7),
                        .Contact = c.String(),
                        .IsDisabled = c.Boolean(nullable := False),
                        .Gender = c.Byte(nullable := False),
                        .FamilyId = c.Int(),
                        .SectionId = c.Int(),
                        .Email = c.String(maxLength := 256),
                        .EmailConfirmed = c.Boolean(nullable := False),
                        .PasswordHash = c.String(),
                        .SecurityStamp = c.String(),
                        .PhoneNumber = c.String(),
                        .PhoneNumberConfirmed = c.Boolean(nullable := False),
                        .TwoFactorEnabled = c.Boolean(nullable := False),
                        .LockoutEndDateUtc = c.DateTime(),
                        .LockoutEnabled = c.Boolean(nullable := False),
                        .AccessFailedCount = c.Int(nullable := False),
                        .UserName = c.String(nullable := False, maxLength := 256)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Families", Function(t) t.FamilyId) _
                .ForeignKey("dbo.Sections", Function(t) t.SectionId) _
                .Index(Function(t) t.FamilyId) _
                .Index(Function(t) t.SectionId) _
                .Index(Function(t) t.UserName, unique := True, name := "UserNameIndex")
            
            CreateTable(
                "dbo.AspNetUserClaims",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .ClaimType = c.String(),
                        .ClaimValue = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.Families",
                Function(c) New With
                    {
                        .FamilyId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.FamilyId)
            
            CreateTable(
                "dbo.AspNetUserLogins",
                Function(c) New With
                    {
                        .LoginProvider = c.String(nullable := False, maxLength := 128),
                        .ProviderKey = c.String(nullable := False, maxLength := 128),
                        .UserId = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.LoginProvider, t.ProviderKey, t.UserId }) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.AspNetUserRoles",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .RoleId = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserId, t.RoleId }) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .ForeignKey("dbo.AspNetRoles", Function(t) t.RoleId, cascadeDelete := True) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.RoleId)
            
            CreateTable(
                "dbo.Sections",
                Function(c) New With
                    {
                        .SectionId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .GradeId = c.Int(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.SectionId) _
                .ForeignKey("dbo.Grades", Function(t) t.GradeId, cascadeDelete := True) _
                .Index(Function(t) t.GradeId)
            
            CreateTable(
                "dbo.Grades",
                Function(c) New With
                    {
                        .GradeId = c.Int(nullable := False, identity := True),
                        .Name = c.String(),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.GradeId)
            
            CreateTable(
                "dbo.ExamStudentOneToFives",
                Function(c) New With
                    {
                        .ExamStudentOneToFiveId = c.Int(nullable := False, identity := True),
                        .ExamStudentId = c.Int(nullable := False),
                        .QuestionOneToFiveId = c.Int(nullable := False),
                        .Answer = c.Boolean(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamStudentOneToFiveId) _
                .ForeignKey("dbo.ExamStudents", Function(t) t.ExamStudentId, cascadeDelete := True) _
                .ForeignKey("dbo.QuestionOneToFives", Function(t) t.QuestionOneToFiveId, cascadeDelete := True) _
                .Index(Function(t) t.ExamStudentId) _
                .Index(Function(t) t.QuestionOneToFiveId)
            
            CreateTable(
                "dbo.ExamStudentRowGroups",
                Function(c) New With
                    {
                        .ExamStudentRowGroupId = c.Int(nullable := False, identity := True),
                        .ExamStudentId = c.Int(nullable := False),
                        .QuestionRowGroupId = c.Int(nullable := False),
                        .Col1Answer = c.Boolean(nullable := False),
                        .Col2Answer = c.Boolean(nullable := False),
                        .Col3Answer = c.Boolean(nullable := False),
                        .Col4Answer = c.Boolean(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamStudentRowGroupId) _
                .ForeignKey("dbo.ExamStudents", Function(t) t.ExamStudentId, cascadeDelete := True) _
                .ForeignKey("dbo.QuestionRowGroups", Function(t) t.QuestionRowGroupId, cascadeDelete := True) _
                .Index(Function(t) t.ExamStudentId) _
                .Index(Function(t) t.QuestionRowGroupId)
            
            CreateTable(
                "dbo.ExamStudentTFLists",
                Function(c) New With
                    {
                        .ExamStudentTFListId = c.Int(nullable := False, identity := True),
                        .ExamStudentId = c.Int(nullable := False),
                        .QuestionTFListId = c.Int(nullable := False),
                        .Answer = c.Boolean(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamStudentTFListId) _
                .ForeignKey("dbo.ExamStudents", Function(t) t.ExamStudentId, cascadeDelete := True) _
                .ForeignKey("dbo.QuestionTFLists", Function(t) t.QuestionTFListId, cascadeDelete := True) _
                .Index(Function(t) t.ExamStudentId) _
                .Index(Function(t) t.QuestionTFListId)
            
            CreateTable(
                "dbo.ExamStudentTFRanks",
                Function(c) New With
                    {
                        .ExamStudentTFRankId = c.Int(nullable := False, identity := True),
                        .ExamStudentId = c.Int(nullable := False),
                        .QuestionTFRankId = c.Int(nullable := False),
                        .Answer = c.Boolean(nullable := False),
                        .DateCreated = c.DateTimeOffset(nullable := False, precision := 7)
                    }) _
                .PrimaryKey(Function(t) t.ExamStudentTFRankId) _
                .ForeignKey("dbo.ExamStudents", Function(t) t.ExamStudentId, cascadeDelete := True) _
                .ForeignKey("dbo.QuestionTFRanks", Function(t) t.QuestionTFRankId, cascadeDelete := True) _
                .Index(Function(t) t.ExamStudentId) _
                .Index(Function(t) t.QuestionTFRankId)
            
            CreateTable(
                "dbo.AspNetRoles",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .Name = c.String(nullable := False, maxLength := 256)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .Index(Function(t) t.Name, unique := True, name := "RoleNameIndex")
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles")
            DropForeignKey("dbo.ExamStudentTFRanks", "QuestionTFRankId", "dbo.QuestionTFRanks")
            DropForeignKey("dbo.ExamStudentTFRanks", "ExamStudentId", "dbo.ExamStudents")
            DropForeignKey("dbo.ExamStudentTFLists", "QuestionTFListId", "dbo.QuestionTFLists")
            DropForeignKey("dbo.ExamStudentTFLists", "ExamStudentId", "dbo.ExamStudents")
            DropForeignKey("dbo.ExamStudentRowGroups", "QuestionRowGroupId", "dbo.QuestionRowGroups")
            DropForeignKey("dbo.ExamStudentRowGroups", "ExamStudentId", "dbo.ExamStudents")
            DropForeignKey("dbo.ExamStudentOneToFives", "QuestionOneToFiveId", "dbo.QuestionOneToFives")
            DropForeignKey("dbo.ExamStudentOneToFives", "ExamStudentId", "dbo.ExamStudents")
            DropForeignKey("dbo.ExamStudentEssays", "QuestionEssayId", "dbo.QuestionEssays")
            DropForeignKey("dbo.ExamStudentEssays", "ExamStudentId", "dbo.ExamStudents")
            DropForeignKey("dbo.ExamStudents", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUsers", "SectionId", "dbo.Sections")
            DropForeignKey("dbo.Sections", "GradeId", "dbo.Grades")
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUsers", "FamilyId", "dbo.Families")
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.ExamStudents", "ExamId", "dbo.Exams")
            DropForeignKey("dbo.QuestionTFRanks", "QuestionGroupId", "dbo.QuestionGroups")
            DropForeignKey("dbo.QuestionTFLists", "QuestionGroupId", "dbo.QuestionGroups")
            DropForeignKey("dbo.QuestionRowGroups", "QuestionGroupId", "dbo.QuestionGroups")
            DropForeignKey("dbo.QuestionOneToFives", "QuestionGroupId", "dbo.QuestionGroups")
            DropForeignKey("dbo.QuestionEssays", "QuestionGroupId", "dbo.QuestionGroups")
            DropForeignKey("dbo.ExamQuestionGroups", "QuestionGroupId", "dbo.QuestionGroups")
            DropForeignKey("dbo.ExamQuestionGroups", "ExamId", "dbo.Exams")
            DropIndex("dbo.AspNetRoles", "RoleNameIndex")
            DropIndex("dbo.ExamStudentTFRanks", New String() { "QuestionTFRankId" })
            DropIndex("dbo.ExamStudentTFRanks", New String() { "ExamStudentId" })
            DropIndex("dbo.ExamStudentTFLists", New String() { "QuestionTFListId" })
            DropIndex("dbo.ExamStudentTFLists", New String() { "ExamStudentId" })
            DropIndex("dbo.ExamStudentRowGroups", New String() { "QuestionRowGroupId" })
            DropIndex("dbo.ExamStudentRowGroups", New String() { "ExamStudentId" })
            DropIndex("dbo.ExamStudentOneToFives", New String() { "QuestionOneToFiveId" })
            DropIndex("dbo.ExamStudentOneToFives", New String() { "ExamStudentId" })
            DropIndex("dbo.Sections", New String() { "GradeId" })
            DropIndex("dbo.AspNetUserRoles", New String() { "RoleId" })
            DropIndex("dbo.AspNetUserRoles", New String() { "UserId" })
            DropIndex("dbo.AspNetUserLogins", New String() { "UserId" })
            DropIndex("dbo.AspNetUserClaims", New String() { "UserId" })
            DropIndex("dbo.AspNetUsers", "UserNameIndex")
            DropIndex("dbo.AspNetUsers", New String() { "SectionId" })
            DropIndex("dbo.AspNetUsers", New String() { "FamilyId" })
            DropIndex("dbo.ExamStudents", New String() { "UserId" })
            DropIndex("dbo.ExamStudents", New String() { "ExamId" })
            DropIndex("dbo.ExamStudentEssays", New String() { "QuestionEssayId" })
            DropIndex("dbo.ExamStudentEssays", New String() { "ExamStudentId" })
            DropIndex("dbo.QuestionTFRanks", New String() { "QuestionGroupId" })
            DropIndex("dbo.QuestionTFLists", New String() { "QuestionGroupId" })
            DropIndex("dbo.QuestionRowGroups", New String() { "QuestionGroupId" })
            DropIndex("dbo.QuestionOneToFives", New String() { "QuestionGroupId" })
            DropIndex("dbo.QuestionEssays", New String() { "QuestionGroupId" })
            DropIndex("dbo.ExamQuestionGroups", New String() { "QuestionGroupId" })
            DropIndex("dbo.ExamQuestionGroups", New String() { "ExamId" })
            DropTable("dbo.AspNetRoles")
            DropTable("dbo.ExamStudentTFRanks")
            DropTable("dbo.ExamStudentTFLists")
            DropTable("dbo.ExamStudentRowGroups")
            DropTable("dbo.ExamStudentOneToFives")
            DropTable("dbo.Grades")
            DropTable("dbo.Sections")
            DropTable("dbo.AspNetUserRoles")
            DropTable("dbo.AspNetUserLogins")
            DropTable("dbo.Families")
            DropTable("dbo.AspNetUserClaims")
            DropTable("dbo.AspNetUsers")
            DropTable("dbo.ExamStudents")
            DropTable("dbo.ExamStudentEssays")
            DropTable("dbo.QuestionTFRanks")
            DropTable("dbo.QuestionTFLists")
            DropTable("dbo.QuestionRowGroups")
            DropTable("dbo.QuestionOneToFives")
            DropTable("dbo.QuestionEssays")
            DropTable("dbo.QuestionGroups")
            DropTable("dbo.Exams")
            DropTable("dbo.ExamQuestionGroups")
        End Sub
    End Class
End Namespace
