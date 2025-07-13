using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.ModuloTarefa;
public class RepositorioTarefaEmOrm : RepositorioBaseEmOrm<Tarefa>, IRepositorioTarefa
{
    private readonly DbSet<Tarefa> tarefas;
    private readonly DbSet<ItemTarefa> itemsTarefa;
    public RepositorioTarefaEmOrm(eAgendaDbContext contexto) : base(contexto)
    {
    }

    public void AdicionarItem(ItemTarefa item)
    {
        itemsTarefa.Add(item);
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
        return tarefas
            .Where(x => x.Concluida)
            .Include(x => x.Itens)
            .ToList();
    }

    public List<Tarefa> SelecionarTarefasPendentes()
    {
        return tarefas
            .Where(x => !x.Concluida)
            .Include(x => x.Itens)
            .ToList();
    }
}
