using eAgenda.Infraestrutura.Orm.Compartilhado;
using EAgenda.Dominio.ModuloCompromisso;

namespace eAgenda.Infraestrutura.Orm.ModuloCompromisso;
public class RepositorioCompromissoEmOrm : RepositorioBaseEmOrm<Compromisso>, IRepositorioCompromisso
{
    public RepositorioCompromissoEmOrm(eAgendaDbContext contexto) : base(contexto)
    {
    }
}
