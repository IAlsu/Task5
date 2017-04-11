namespace Rewarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.Rewards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonRewards",
                c => new
                    {
                        RewardId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RewardId, t.PersonId })
                .ForeignKey("dbo.Rewards", t => t.RewardId, cascadeDelete: true)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.RewardId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonRewards", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.PersonRewards", "RewardId", "dbo.Rewards");
            DropForeignKey("dbo.Persons", "PhotoId", "dbo.Pictures");
            DropIndex("dbo.PersonRewards", new[] { "PersonId" });
            DropIndex("dbo.PersonRewards", new[] { "RewardId" });
            DropIndex("dbo.Persons", new[] { "PhotoId" });
            DropTable("dbo.PersonRewards");
            DropTable("dbo.Rewards");
            DropTable("dbo.Pictures");
            DropTable("dbo.Persons");
        }
    }
}
