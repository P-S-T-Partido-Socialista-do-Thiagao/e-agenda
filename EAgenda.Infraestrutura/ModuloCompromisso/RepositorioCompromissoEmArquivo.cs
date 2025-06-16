using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloCompromisso
{
    public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso>, IRepositorioCompromisso
    {
        public RepositorioCompromissoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }
        protected override List<Compromisso> ObterRegistros()
        {
            throw new NotImplementedException();
        }
    }    
    
}
