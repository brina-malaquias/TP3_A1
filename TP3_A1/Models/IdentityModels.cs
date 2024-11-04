using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TP3_A1.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Veiculo> Veiculos { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que a authenticationType deve corresponder a uma definida em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações do usuário personalizadas aqui
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Veiculo> Veiculoes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<CategoriaServico> CategoriaServicos { get; set; }
        public DbSet<ServicoCategoria> ServicoCategorias { get; set; }
        public DbSet<TipoDespesa> TipoDespesas { get; set; }
        public DbSet<Oficina> Oficinas { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração muitos para muitos entre Servico e CategoriaServico
            modelBuilder.Entity<ServicoCategoria>()
                .HasKey(sc => new { sc.ServicoId, sc.CategoriaServicoId });

            modelBuilder.Entity<ServicoCategoria>()
                .HasRequired(sc => sc.Servico)
                .WithMany(s => s.ServicoCategorias)
                .HasForeignKey(sc => sc.ServicoId);

            modelBuilder.Entity<ServicoCategoria>()
                .HasRequired(sc => sc.CategoriaServico)
                .WithMany(cs => cs.ServicoCategorias)
                .HasForeignKey(sc => sc.CategoriaServicoId);


        }

    }
}