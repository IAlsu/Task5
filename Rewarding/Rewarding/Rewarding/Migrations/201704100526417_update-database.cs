namespace Rewarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Persons", "PhotoId", "dbo.Pictures");
            DropIndex("dbo.Persons", new[] { "PhotoId" });
            AlterColumn("dbo.Persons", "PhotoId", c => c.Int());
            CreateIndex("dbo.Persons", "PhotoId");
            AddForeignKey("dbo.Persons", "PhotoId", "dbo.Pictures", "ImageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Persons", "PhotoId", "dbo.Pictures");
            DropIndex("dbo.Persons", new[] { "PhotoId" });
            AlterColumn("dbo.Persons", "PhotoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Persons", "PhotoId");
            AddForeignKey("dbo.Persons", "PhotoId", "dbo.Pictures", "ImageId", cascadeDelete: true);
        }
    }
}
