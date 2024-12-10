namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kargo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KargoDetays", "Cariid", c => c.Int(nullable: false));
            CreateIndex("dbo.KargoDetays", "Cariid");
            AddForeignKey("dbo.KargoDetays", "Cariid", "dbo.Carilers", "Cariid", cascadeDelete: true);
            DropColumn("dbo.KargoDetays", "Alici");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KargoDetays", "Alici", c => c.String(maxLength: 20, unicode: false));
            DropForeignKey("dbo.KargoDetays", "Cariid", "dbo.Carilers");
            DropIndex("dbo.KargoDetays", new[] { "Cariid" });
            DropColumn("dbo.KargoDetays", "Cariid");
        }
    }
}
