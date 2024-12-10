namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kargo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KargoTakips",
                c => new
                    {
                        KargoTakipid = c.Int(nullable: false, identity: true),
                        Takipkodu = c.String(maxLength: 10, unicode: false),
                        Aciklama = c.String(maxLength: 100, unicode: false),
                        TarihZaman = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KargoTakipid);
            
            AlterColumn("dbo.KargoDetays", "Aciklama", c => c.String(maxLength: 250, unicode: false));
            AlterColumn("dbo.KargoDetays", "TakipKodu", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.KargoDetays", "Alici", c => c.String(maxLength: 20, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KargoDetays", "Alici", c => c.String());
            AlterColumn("dbo.KargoDetays", "TakipKodu", c => c.String());
            AlterColumn("dbo.KargoDetays", "Aciklama", c => c.String());
            DropTable("dbo.KargoTakips");
        }
    }
}
