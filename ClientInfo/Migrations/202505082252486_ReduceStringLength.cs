namespace ClientInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Street", c => c.String(maxLength: 100));
            AlterColumn("dbo.Addresses", "Email", c => c.String(maxLength: 30));
            AlterColumn("dbo.Clients", "FullName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "FullName", c => c.String());
            AlterColumn("dbo.Addresses", "Email", c => c.String());
            AlterColumn("dbo.Addresses", "Street", c => c.String());
        }
    }
}
