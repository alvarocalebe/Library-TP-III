namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundaMigracao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Livroes", "NomeEditora", c => c.String(maxLength: 100));
            AddColumn("dbo.Livroes", "NomeImagem", c => c.String(maxLength: 100));
            AlterColumn("dbo.Livroes", "Titulo", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Livroes", "Titulo", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Livroes", "NomeImagem");
            DropColumn("dbo.Livroes", "NomeEditora");
        }
    }
}
