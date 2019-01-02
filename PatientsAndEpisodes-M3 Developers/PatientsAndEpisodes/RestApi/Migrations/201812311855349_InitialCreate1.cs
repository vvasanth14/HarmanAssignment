namespace RestApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Episode", "PatientId");
            AddForeignKey("dbo.Episode", "PatientId", "dbo.Patient", "PatientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Episode", "PatientId", "dbo.Patient");
            DropIndex("dbo.Episode", new[] { "PatientId" });
        }
    }
}
