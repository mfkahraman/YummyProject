namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class featureImgValRem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Features", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Features", "ImageUrl", c => c.String(nullable: false));
        }
    }
}
