namespace Visitare_n1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerAndQuestion2", "UserId", c => c.String());
            AddColumn("dbo.AnswerAndQuestion2", "UserName", c => c.String());
            AddColumn("dbo.Points3", "UserId", c => c.String());
            AddColumn("dbo.Points3", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Points3", "UserName");
            DropColumn("dbo.Points3", "UserId");
            DropColumn("dbo.AnswerAndQuestion2", "UserName");
            DropColumn("dbo.AnswerAndQuestion2", "UserId");
        }
    }
}
