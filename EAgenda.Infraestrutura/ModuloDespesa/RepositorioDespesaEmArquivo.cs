using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Infraestrutura.Compartilhado;

namespace EAgenda.Infraestrutura.ModuloDespesa;

public class RepositorioDespesaEmArquivo : RepositorioBaseEmArquivo<Despesa>, IRepositorioDespesa
{
    public RepositorioDespesaEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    { 
    }
    protected override List<Despesa> ObterRegistros()
    {
        return contexto.Despesas;
    }

}
