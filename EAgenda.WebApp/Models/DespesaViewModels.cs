using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace EAgenda.WebApp.Models
{
    public class FormularioDespesaViewModel
    {
        [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Descrição\" deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Descrição\" deve ter no máximo 100 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo \"Data De Ocorrencia\" é obrigatório.")]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataOcorrencia { get; set; }

        [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
        [DisplayFormat(DataFormatString = "{0,c}")]
        public decimal Valor { get; set; }
        public string? FormaPagamento { get; set; }
        public List<string>? Categorias { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class CadastrarDespesaViewModel : FormularioDespesaViewModel
    {
        public CadastrarDespesaViewModel() { }

        public CadastrarDespesaViewModel(string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento, List<string> categorias, DateTime dataCadastro)
        {
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
            DataCadastro = dataCadastro;
        }

    }

    public class EditarDespesaViewModel : FormularioDespesaViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }
        public EditarDespesaViewModel() { }
        public EditarDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento, List<string> categorias, DateTime dataCadastro)
        {
            Id = id;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
            DataCadastro = dataCadastro;
        }
    }

    public class ExcluirDespesaViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
        public string Descricao { get; set; }
        public ExcluirDespesaViewModel() { }
        public ExcluirDespesaViewModel(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }

    public class VisualizarDespesaViewModel
    {
        public List<DetalhesDespesaViewModel> Registros { get; set; }

        public VisualizarDespesaViewModel(List<Despesa> despesas)
        {
            Registros = new List<DetalhesDespesaViewModel>();

            foreach (var despesa in despesas)
            {
                Registros.Add(despesa.ParaDetalhesVM());
            }
        }
    }

    public class DetalhesDespesaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public decimal Valor { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<string> Categorias { get; set; } = new List<string>();

        public DetalhesDespesaViewModel() { }

        public DetalhesDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento, List<string> categorias, DateTime dataCadastro)
        {
            Id = id;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
            DataCadastro = dataCadastro;
        }
    }

    public class SelecionarDespesaViewModel
    {
        public Guid Id { get; set; }
        public string Despesa { get; set; }
        public SelecionarDespesaViewModel() { }
        public SelecionarDespesaViewModel(Guid id, string despesa)
        {
            Id = id;
            Despesa = despesa;
        }
    }
}
