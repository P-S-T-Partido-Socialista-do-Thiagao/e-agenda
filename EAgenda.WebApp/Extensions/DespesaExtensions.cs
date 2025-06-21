using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions
{
    public static class DespesaExtensions
    {
        public static Despesa ParaEntidade(this FormularioDespesaViewModel cadastrarVM, List<Categoria> categoriasDisponiveis)
        {
            var categoriasSelecionadas = new List<Categoria>();

            foreach(var categoria in categoriasDisponiveis)
            {
                if (cadastrarVM.Categorias != null && cadastrarVM.Categorias.Contains(categoria.Id))
                {
                    categoriasSelecionadas.Add(categoria);
                }
            }
            return new Despesa(cadastrarVM.Descricao, cadastrarVM.DataOcorrencia, cadastrarVM.Valor, cadastrarVM.FormaPagamento, categoriasSelecionadas);
        }
        public static DetalhesDespesaViewModel ParaDetalhesVM(this Despesa despesa)
        {
            var titulosCategorias = new List<string>();
            foreach (var categoria in despesa.Categorias)
            {
                titulosCategorias.Add(categoria.Titulo);
            }
            return new DetalhesDespesaViewModel
            {
                Id = despesa.Id,
                Descricao = despesa.Descricao,
                DataOcorrencia = despesa.DataOcorrencia,
                Valor = despesa.Valor,
                FormaPagamento = despesa.FormaPagamento,
                Categorias = titulosCategorias,
            };
        }
    }
}
