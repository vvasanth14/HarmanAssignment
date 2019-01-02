namespace RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episode",
                c => new
                    {
                        EpisodeId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        AdmissionDate = c.DateTime(nullable: false),
                        DischargeDate = c.DateTime(nullable: false),
                        Diagnosis = c.String(),
                    })
                .PrimaryKey(t => t.EpisodeId);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        NhsNumber = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patient");
            DropTable("dbo.Episode");
        }
    }
}
