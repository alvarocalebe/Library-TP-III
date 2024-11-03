namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validations2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Livroes", "NomeEditora", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Livroes", "NomeEditora", c => c.String(maxLength: 100));
        }
    }
}
