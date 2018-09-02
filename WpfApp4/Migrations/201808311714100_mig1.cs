namespace WpfApp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Length = c.Int(nullable: false),
                        Image = c.String(),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        DatePublished = c.DateTime(nullable: false, storeType: "date"),
                        ShopUrl = c.String(),
                        Author_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Length = c.Int(nullable: false),
                        Album_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .Index(t => t.Album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Albums", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Albums", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Songs", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "Genre_Id" });
            DropIndex("dbo.Albums", new[] { "Author_Id" });
            DropTable("dbo.Songs");
            DropTable("dbo.Genres");
            DropTable("dbo.Authors");
            DropTable("dbo.Albums");
        }
    }
}
