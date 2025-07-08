using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
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
