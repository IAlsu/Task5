namespace Rewarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rewardsindex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rewards", "ImageId", c => c.Int());
            CreateIndex("dbo.Rewards", "ImageId");
            AddForeignKey("dbo.Rewards", "ImageId", "dbo.Pictures", "ImageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rewards", "ImageId", "dbo.Pictures");
            DropIndex("dbo.Rewards", new[] { "ImageId" });
            DropColumn("dbo.Rewards", "ImageId");
        }
    }
}
