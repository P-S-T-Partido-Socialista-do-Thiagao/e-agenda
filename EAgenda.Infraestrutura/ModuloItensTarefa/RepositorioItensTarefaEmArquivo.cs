using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Dominio.ModuloItensTarefa;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloItensTarefa
{
    public class RepositorioItensTarefaEmArquivo : RepositorioBaseEmArquivo<ItensTarefa>, IRepositorioItensTarefa
    {
        public RepositorioItensTarefaEmArquivo(ContextoDados contextoDados) : base(contextoDados) 
        {
        }

        protected override List<ItensTarefa> ObterRegistros() 
        {
            return contexto.ItensTarefa;
        }
    }
}
