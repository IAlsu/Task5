namespace Rewarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PicturePathToBytes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pictures", "Path");
            AddColumn("dbo.Pictures", "Path", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "Path");
            AddColumn("dbo.Pictures", "Path", c => c.String());
        }
    }
}
