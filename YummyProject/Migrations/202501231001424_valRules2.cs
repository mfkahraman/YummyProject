namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valRules2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Chefs", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Chefs", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Chefs", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.ChefSocials", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.ChefSocials", "Icon", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ChefSocials", "SocialMediaName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChefSocials", "SocialMediaName", c => c.String());
            AlterColumn("dbo.ChefSocials", "Icon", c => c.String());
            AlterColumn("dbo.ChefSocials", "Url", c => c.String());
            AlterColumn("dbo.Chefs", "Description", c => c.String());
            AlterColumn("dbo.Chefs", "Title", c => c.String());
            AlterColumn("dbo.Chefs", "Name", c => c.String());
        }
    }
}
