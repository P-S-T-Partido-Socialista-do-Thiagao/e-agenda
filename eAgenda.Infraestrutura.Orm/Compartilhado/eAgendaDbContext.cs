using EAgenda.Dominio.ModuloContato;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.Compartilhado;
public class eAgendaDbContext : DbContext
{
    public DbSet<Contato> Contatos {  get; set; }
    public eAgendaDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(eAgendaDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }
}
