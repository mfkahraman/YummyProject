namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valRules3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChefSocials", "Url", c => c.String());
            AlterColumn("dbo.ChefSocials", "Icon", c => c.String());
            AlterColumn("dbo.ChefSocials", "SocialMediaName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChefSocials", "SocialMediaName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ChefSocials", "Icon", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ChefSocials", "Url", c => c.String(nullable: false));
        }
    }
}
