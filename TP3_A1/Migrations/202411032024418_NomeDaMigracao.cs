namespace TP3_A1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeDaMigracao : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Servicoes", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servicoes", "Tipo", c => c.String());
        }
    }
}
