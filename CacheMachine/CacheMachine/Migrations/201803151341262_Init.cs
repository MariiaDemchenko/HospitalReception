namespace CacheMachine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        PinCode = c.Int(nullable: false),
                        Sum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Operation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.Long(nullable: false),
                        OptionId = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        Sum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Card", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.Action", t => t.OptionId, cascadeDelete: true)
                .Index(t => t.CardId)
                .Index(t => t.OptionId);
            
            CreateTable(
                "dbo.Action",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operation", "OptionId", "dbo.Action");
            DropForeignKey("dbo.Operation", "CardId", "dbo.Card");
            DropIndex("dbo.Operation", new[] { "OptionId" });
            DropIndex("dbo.Operation", new[] { "CardId" });
            DropTable("dbo.Action");
            DropTable("dbo.Operation");
            DropTable("dbo.Card");
        }
    }
}
