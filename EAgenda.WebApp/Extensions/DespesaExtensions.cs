using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions
{
    public static class DespesaExtensions
    {
        public static Despesa ParaEntidade(this FormularioDespesaViewModel cadastrarVM)
        {
            return new Despesa(cadastrarVM.Descricao, cadastrarVM.DataOcorrencia, cadastrarVM.Valor, cadastrarVM.FormaPagamento, cadastrarVM.Categorias, cadastrarVM.DataCadastro);
        }
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
