using EAgenda.Dominio.ModuloContato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Orm.ModuloContato;
public class MapeadorContatoEmOrm : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Telefone)
            .IsRequired();

        builder.Property(x => x.Empresa)
            .HasMaxLength(20);

        builder.Property(x => x.Cargo)
            .HasMaxLength(20);

        builder.HasMany(x => x.Compromissos)
            .WithOne(c => c.Contato);
    }
}
