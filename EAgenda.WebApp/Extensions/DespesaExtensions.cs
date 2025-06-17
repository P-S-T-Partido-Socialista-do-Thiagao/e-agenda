using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions
{
    public static class DespesaExtensions
    {
        public static DetalhesDespesaViewModel ParaDetalhesVM(this Despesa despesa)
        {
           return new DetalhesDespesaViewModel
            {
                Id = despesa.Id,
                Descricao = despesa.Descricao,
                DataOcorrencia = despesa.DataOcorrencia,
                Valor = despesa.Valor,
                FormaPagamento = despesa.FormaPagamento,
                Categorias = despesa.Categorias,
                DataCadastro = despesa.DataCadastro,
            };
        }
    }
}
