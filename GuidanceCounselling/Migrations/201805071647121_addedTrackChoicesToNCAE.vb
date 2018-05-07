Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class addedTrackChoicesToNCAE
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.NCAEGrades", "TrackChoice", Function(c) c.String())
            AddColumn("dbo.NCAEGrades", "StrandConcentrationChoice", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.NCAEGrades", "StrandConcentrationChoice")
            DropColumn("dbo.NCAEGrades", "TrackChoice")
        End Sub
    End Class
End Namespace
