using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;

namespace eAgenda.Infraestrutura.Orm.ModuloTarefa;
public class RepositorioTarefaEmOrm : RepositorioBaseEmOrm<Tarefa>, IRepositorioTarefa
{
    public RepositorioTarefaEmOrm(eAgendaDbContext contexto) : base(contexto)
    {
    }

    public void AdicionarItem(ItemTarefa item)
    {
        throw new NotImplementedException();
    }

    public bool AtualizarItem(ItemTarefa itemAtualizado)
    {
        throw new NotImplementedException();
    }

    public bool RemoverItem(ItemTarefa item)
    {
        throw new NotImplementedException();
    }

    public List<Tarefa> SelecionarTarefasConcluidas()
    {
        throw new NotImplementedException();
    }

    public List<Tarefa> SelecionarTarefasPendentes()
    {
        throw new NotImplementedException();
    }
}
