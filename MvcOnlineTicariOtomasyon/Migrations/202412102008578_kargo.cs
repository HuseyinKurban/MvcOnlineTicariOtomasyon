namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kargo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KargoDetays", "Personelid", "dbo.Personels");
            DropIndex("dbo.KargoDetays", new[] { "Personelid" });
            AddColumn("dbo.KargoDetays", "Personel", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.KargoDetays", "Alici", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.KargoDetays", "Personelid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KargoDetays", "Personelid", c => c.Int(nullable: false));
            AlterColumn("dbo.KargoDetays", "Alici", c => c.String(maxLength: 20, unicode: false));
            DropColumn("dbo.KargoDetays", "Personel");
            CreateIndex("dbo.KargoDetays", "Personelid");
            AddForeignKey("dbo.KargoDetays", "Personelid", "dbo.Personels", "Personelid", cascadeDelete: true);
        }
    }
}
