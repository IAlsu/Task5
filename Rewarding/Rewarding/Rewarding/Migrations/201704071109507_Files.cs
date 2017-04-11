namespace Rewarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "ImageName", c => c.String());
            AddColumn("dbo.Pictures", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.Pictures", "Content", c => c.Binary());
            DropColumn("dbo.Pictures", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Path", c => c.Binary());
            DropColumn("dbo.Pictures", "Content");
            DropColumn("dbo.Pictures", "ContentType");
            DropColumn("dbo.Pictures", "ImageName");
        }
    }
}
