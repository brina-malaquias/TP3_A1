namespace TP3_A1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecoesDeServicoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServicoCategorias", "ServicoCategoriaId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServicoCategorias", "ServicoCategoriaId");
        }
    }
}
