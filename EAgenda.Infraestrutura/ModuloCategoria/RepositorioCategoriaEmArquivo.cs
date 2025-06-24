using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloCategoria
{
    public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoriaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }
        protected override List<Categoria> ObterRegistros()
        {
            return contexto.Categorias;
        }
    }
}