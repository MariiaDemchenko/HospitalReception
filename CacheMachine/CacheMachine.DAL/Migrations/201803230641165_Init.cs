namespace CacheMachine.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Action",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Operation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.String(maxLength: 128),
                        ActionId = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        Sum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Action", t => t.ActionId, cascadeDelete: true)
                .ForeignKey("dbo.Card", t => t.CardId)
                .Index(t => t.CardId)
                .Index(t => t.ActionId);
            
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsBlocked = c.Boolean(nullable: false),
                        PinCodeHash = c.String(),
                        PinCodeSalt = c.String(),
                        Sum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operation", "CardId", "dbo.Card");
            DropForeignKey("dbo.Operation", "ActionId", "dbo.Action");
            DropIndex("dbo.Operation", new[] { "ActionId" });
            DropIndex("dbo.Operation", new[] { "CardId" });
            DropTable("dbo.Card");
            DropTable("dbo.Operation");
            DropTable("dbo.Action");
        }
    }
}
