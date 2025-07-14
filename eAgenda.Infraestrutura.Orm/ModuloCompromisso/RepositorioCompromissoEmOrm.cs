using eAgenda.Infraestrutura.Orm.Compartilhado;
using EAgenda.Dominio.ModuloCompromisso;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.ModuloCompromisso;
public class RepositorioCompromissoEmOrm : RepositorioBaseEmOrm<Compromisso>, IRepositorioCompromisso
{
    public RepositorioCompromissoEmOrm(eAgendaDbContext contexto) : base(contexto)
    {
        
    }

    public override Compromisso? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(x => x.Contato)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }
}
