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
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Empresa);

        builder.Property(x => x.Cargo);

        builder.HasMany(x => x.Compromissos)
            .WithOne(c => c.Contato);
    }
}
