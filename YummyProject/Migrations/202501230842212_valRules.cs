namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valRules : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Ingrdients", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Features", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Features", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Features", "Description", c => c.String());
            AlterColumn("dbo.Features", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Ingrdients", c => c.String());
            AlterColumn("dbo.Products", "ProductName", c => c.String());
            AlterColumn("dbo.Categories", "CategoryName", c => c.String());
        }
    }
}
