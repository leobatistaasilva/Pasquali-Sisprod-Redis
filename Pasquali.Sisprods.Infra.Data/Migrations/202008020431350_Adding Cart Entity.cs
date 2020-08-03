namespace Pasquali.Sisprods.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCartEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastUpdateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cart");
        }
    }
}
