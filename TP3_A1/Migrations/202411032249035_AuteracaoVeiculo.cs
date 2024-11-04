namespace TP3_A1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuteracaoVeiculo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Veiculoes", "Marca", c => c.String(nullable: false));
            AlterColumn("dbo.Veiculoes", "Modelo", c => c.String(nullable: false));
            AlterColumn("dbo.Veiculoes", "Placa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Veiculoes", "Placa", c => c.String());
            AlterColumn("dbo.Veiculoes", "Modelo", c => c.String());
            AlterColumn("dbo.Veiculoes", "Marca", c => c.String());
        }
    }
}
