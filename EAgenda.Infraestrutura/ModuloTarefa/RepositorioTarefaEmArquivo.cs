using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloTarefa
{
    public class RepositorioTarefaEmArquivo : RepositorioBaseEmArquivo<Tarefa>, IRepositorioTarefa
    {
        public RepositorioTarefaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Tarefa> ObterRegistros()
        {
            return contexto.Tarefas;
        }

    }
}
