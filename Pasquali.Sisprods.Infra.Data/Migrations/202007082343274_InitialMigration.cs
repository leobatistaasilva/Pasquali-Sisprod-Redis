namespace Pasquali.Sisprods.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProductSKU = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductClient",
                c => new
                    {
                        Product_ProductId = c.Int(nullable: false),
                        Client_ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductId, t.Client_ClientId })
                .ForeignKey("dbo.Product", t => t.Product_ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.Client_ClientId, cascadeDelete: true)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Client_ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductClient", "Client_ClientId", "dbo.Client");
            DropForeignKey("dbo.ProductClient", "Product_ProductId", "dbo.Product");
            DropIndex("dbo.ProductClient", new[] { "Client_ClientId" });
            DropIndex("dbo.ProductClient", new[] { "Product_ProductId" });
            DropTable("dbo.ProductClient");
            DropTable("dbo.Product");
            DropTable("dbo.Client");
        }
    }
}
