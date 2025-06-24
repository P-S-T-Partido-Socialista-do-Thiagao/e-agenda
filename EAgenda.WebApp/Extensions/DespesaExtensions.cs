using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Models;

namespace eAgenda.WebApp.Extensions;

public static class DespesaExtensions
{
    public static Despesa ParaEntidade(this FormularioDespesaViewModel formularioVM)
    {
        return new Despesa(
            formularioVM.Descricao,
            formularioVM.DataOcorrencia,
            formularioVM.Valor,
            formularioVM.FormaPagamento
        );
    }

    public static DetalhesDespesaViewModel ParaDetalhesVM(this Despesa despesa)
    {
        return new DetalhesDespesaViewModel(
                despesa.Id,
                despesa.Descricao,
                despesa.DataOcorrencia,
                despesa.Valor,
                despesa.FormaPagamento,
                despesa.Categorias
        );
    }
}