namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "MessageContent", c => c.Int(nullable: false));
        }
    }
}
