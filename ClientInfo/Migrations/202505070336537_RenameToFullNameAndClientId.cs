namespace ClientInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameToFullNameAndClientId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Email = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ClientId", "dbo.Clients");
            DropIndex("dbo.Addresses", new[] { "ClientId" });
            DropTable("dbo.Clients");
            DropTable("dbo.Addresses");
        }
    }
}
