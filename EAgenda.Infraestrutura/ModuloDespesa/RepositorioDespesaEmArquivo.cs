using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloDespesa;

public class RepositorioDespesaEmArquivo : RepositorioBaseEmArquivo<Despesa>, IRepositorioDespesa
{
    public RepositorioDespesaEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    { 
    }
    protected override List<Despesa> ObterRegistros()
    {
        throw new NotImplementedException();
    }
}
