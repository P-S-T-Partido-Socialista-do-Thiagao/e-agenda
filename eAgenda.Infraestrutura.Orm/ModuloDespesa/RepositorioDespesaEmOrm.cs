using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Orm.Compartilhado;

namespace eAgenda.Infraestrutura.Orm.ModuloDespesa;
public class RepositorioDespesaEmOrm : RepositorioBaseEmOrm<Despesa>, IRepositorioDespesa
{
    public RepositorioDespesaEmOrm(eAgendaDbContext contexto) : base(contexto)
    {
    }
}
