namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeatureVal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Features", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Features", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Features", "Title", c => c.String());
            AlterColumn("dbo.Features", "ImageUrl", c => c.String());
        }
    }
}
