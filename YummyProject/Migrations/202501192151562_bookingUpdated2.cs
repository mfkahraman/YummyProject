namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingUpdated2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Bookings", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Bookings", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "MessageContent", c => c.String());
            AlterColumn("dbo.Bookings", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Bookings", "Email", c => c.String());
            AlterColumn("dbo.Bookings", "Name", c => c.String());
        }
    }
}
