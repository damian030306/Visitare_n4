namespace Visitare_n1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfirmedAnswers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        AnswerAndQuestion2Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AnswerAndQuestion2Id })
                .ForeignKey("dbo.AnswerAndQuestion2", t => t.AnswerAndQuestion2Id, cascadeDelete: true)
                .Index(t => t.AnswerAndQuestion2Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConfirmedAnswers", "AnswerAndQuestion2Id", "dbo.AnswerAndQuestion2");
            DropIndex("dbo.ConfirmedAnswers", new[] { "AnswerAndQuestion2Id" });
            DropTable("dbo.ConfirmedAnswers");
        }
    }
}
