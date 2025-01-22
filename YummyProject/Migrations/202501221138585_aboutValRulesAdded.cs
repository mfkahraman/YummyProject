namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aboutValRulesAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Abouts", "Item1", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Abouts", "Item2", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Abouts", "Item3", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Abouts", "Description", c => c.String(nullable: false, maxLength: 700));
            AlterColumn("dbo.Abouts", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Abouts", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Abouts", "Description", c => c.String());
            AlterColumn("dbo.Abouts", "Item3", c => c.String());
            AlterColumn("dbo.Abouts", "Item2", c => c.String());
            AlterColumn("dbo.Abouts", "Item1", c => c.String());
        }
    }
}
