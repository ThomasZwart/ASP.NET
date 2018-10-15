namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNumberAvailableToMovies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));

            Sql("UPDATE Movies SET NumberAvailable = NumberInStock");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "NumberInStock", c => c.Byte(nullable: false));
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
