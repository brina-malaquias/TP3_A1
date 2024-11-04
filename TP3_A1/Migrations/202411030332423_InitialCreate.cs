namespace TP3_A1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaServicoes",
                c => new
                    {
                        CategoriaServicoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaServicoId);
            
            CreateTable(
                "dbo.ServicoCategorias",
                c => new
                    {
                        ServicoId = c.Int(nullable: false),
                        CategoriaServicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServicoId, t.CategoriaServicoId })
                .ForeignKey("dbo.CategoriaServicoes", t => t.CategoriaServicoId, cascadeDelete: true)
                .ForeignKey("dbo.Servicoes", t => t.ServicoId, cascadeDelete: true)
                .Index(t => t.ServicoId)
                .Index(t => t.CategoriaServicoId);
            
            CreateTable(
                "dbo.Servicoes",
                c => new
                    {
                        ServicoId = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                        Data = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Observacoes = c.String(),
                        VeiculoId = c.Int(nullable: false),
                        OficinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServicoId)
                .ForeignKey("dbo.Oficinas", t => t.OficinaId, cascadeDelete: true)
                .ForeignKey("dbo.Veiculoes", t => t.VeiculoId, cascadeDelete: true)
                .Index(t => t.VeiculoId)
                .Index(t => t.OficinaId);
            
            CreateTable(
                "dbo.Oficinas",
                c => new
                    {
                        OficinaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Responsavel = c.String(),
                        Telefone = c.String(),
                    })
                .PrimaryKey(t => t.OficinaId);
            
            CreateTable(
                "dbo.Veiculoes",
                c => new
                    {
                        VeiculoId = c.Int(nullable: false, identity: true),
                        Marca = c.String(),
                        Modelo = c.String(),
                        Ano = c.Int(nullable: false),
                        Quilometragem = c.Int(nullable: false),
                        Placa = c.String(),
                        Observacoes = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VeiculoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Despesas",
                c => new
                    {
                        DespesaId = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Observacoes = c.String(),
                        VeiculoId = c.Int(nullable: false),
                        TipoDespesaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DespesaId)
                .ForeignKey("dbo.TipoDespesas", t => t.TipoDespesaId, cascadeDelete: true)
                .ForeignKey("dbo.Veiculoes", t => t.VeiculoId, cascadeDelete: true)
                .Index(t => t.VeiculoId)
                .Index(t => t.TipoDespesaId);
            
            CreateTable(
                "dbo.TipoDespesas",
                c => new
                    {
                        TipoDespesaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.TipoDespesaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ServicoCategorias", "ServicoId", "dbo.Servicoes");
            DropForeignKey("dbo.Veiculoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Servicoes", "VeiculoId", "dbo.Veiculoes");
            DropForeignKey("dbo.Despesas", "VeiculoId", "dbo.Veiculoes");
            DropForeignKey("dbo.Despesas", "TipoDespesaId", "dbo.TipoDespesas");
            DropForeignKey("dbo.Servicoes", "OficinaId", "dbo.Oficinas");
            DropForeignKey("dbo.ServicoCategorias", "CategoriaServicoId", "dbo.CategoriaServicoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Despesas", new[] { "TipoDespesaId" });
            DropIndex("dbo.Despesas", new[] { "VeiculoId" });
            DropIndex("dbo.Veiculoes", new[] { "UserId" });
            DropIndex("dbo.Servicoes", new[] { "OficinaId" });
            DropIndex("dbo.Servicoes", new[] { "VeiculoId" });
            DropIndex("dbo.ServicoCategorias", new[] { "CategoriaServicoId" });
            DropIndex("dbo.ServicoCategorias", new[] { "ServicoId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TipoDespesas");
            DropTable("dbo.Despesas");
            DropTable("dbo.Veiculoes");
            DropTable("dbo.Oficinas");
            DropTable("dbo.Servicoes");
            DropTable("dbo.ServicoCategorias");
            DropTable("dbo.CategoriaServicoes");
        }
    }
}
