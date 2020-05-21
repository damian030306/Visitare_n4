namespace Visitare_n1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerAndQuestion2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question1 = c.String(),
                        AnswersString = c.String(),
                        GoodAnswer = c.Int(nullable: false),
                        Correct = c.String(),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        UserId = c.String(),
                        UserName = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Points3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Test1",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Nickname = c.String(),
                        Punkty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitedPoints",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Points3Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.Points3Id })
                .ForeignKey("dbo.Points3", t => t.Points3Id, cascadeDelete: true)
                .Index(t => t.Points3Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitedPoints", "Points3Id", "dbo.Points3");
            DropForeignKey("dbo.Points3", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.AnswerAndQuestion2", "RouteId", "dbo.Routes");
            DropIndex("dbo.VisitedPoints", new[] { "Points3Id" });
            DropIndex("dbo.Points3", new[] { "RouteId" });
            DropIndex("dbo.AnswerAndQuestion2", new[] { "RouteId" });
            DropTable("dbo.VisitedPoints");
            DropTable("dbo.Test1");
            DropTable("dbo.Points3");
            DropTable("dbo.Routes");
            DropTable("dbo.AnswerAndQuestion2");
        }
    }
}
