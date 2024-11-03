namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "UsuarioID", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "UsuarioID" });
            AlterColumn("dbo.Autors", "Nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categorias", "Nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Reservas", "UsuarioID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reservas", "UsuarioID");
            AddForeignKey("dbo.Reservas", "UsuarioID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "UsuarioID", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "UsuarioID" });
            AlterColumn("dbo.Reservas", "UsuarioID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categorias", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Autors", "Nome", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Reservas", "UsuarioID");
            AddForeignKey("dbo.Reservas", "UsuarioID", "dbo.AspNetUsers", "Id");
        }
    }
}
