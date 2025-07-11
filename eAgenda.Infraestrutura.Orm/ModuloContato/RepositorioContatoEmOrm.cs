using ControleDeBar.Infraestrutura.Orm.Compartilhado;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using EAgenda.Dominio.ModuloContato;

namespace eAgenda.Infraestrutura.Orm.ModuloContato;
public class RepositorioContatoEmOrm : RepositorioBaseEmOrm<Contato>, IRepositorioContato
{
    public RepositorioContatoEmOrm(eAgendaDbContext contexto) : base(contexto)
    {
    }
}
