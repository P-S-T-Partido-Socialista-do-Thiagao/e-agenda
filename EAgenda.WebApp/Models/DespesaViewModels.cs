using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EAgenda.WebApp.Models
{
    public class FormularioDespesaViewModel
    {
        [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Descrição\" deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Descrição\" deve ter no máximo 100 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo \"Data De Ocorrencia\" é obrigatório.")]
        public DateTime DataOcorrencia { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" deve ser maior que zero.")]
        public decimal Valor { get; set; }
        public string? FormaPagamento { get; set; }
        public List<SelectListItem>? CategoriasDisponiveis { get; set; }
        public List<Guid>? Categorias { get; set; }
    }

    public class CadastrarDespesaViewModel : FormularioDespesaViewModel
    {
        public CadastrarDespesaViewModel()
        {
            Categorias = new List<Guid>();
            CategoriasDisponiveis = new List<SelectListItem>();
        }

        public CadastrarDespesaViewModel(List<Categoria> categorias) : this()
        {

            foreach (var categoria in categorias)
            {
                CategoriasDisponiveis.Add(new SelectListItem
                {
                    Value = categoria.Id.ToString(),
                    Text = categoria.Titulo
                });
            }
        }

        public CadastrarDespesaViewModel(List<Categoria> categorias, string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento)
        {
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;

            CategoriasDisponiveis = new List<SelectListItem>();
            foreach (var categoria in categorias)
            {
                CategoriasDisponiveis.Add(new SelectListItem
                {
                    Value = categoria.Id.ToString(),
                    Text = categoria.Titulo
                });
            }
        }

    }

    public class EditarDespesaViewModel : FormularioDespesaViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }
        public EditarDespesaViewModel() { }
        public EditarDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento, List<Categoria> categorias)
        {
            Id = id;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
            CategoriasDisponiveis = new List<SelectListItem>();
            foreach (var categoria in categorias)
            {
                CategoriasDisponiveis.Add(new SelectListItem
                {
                    Value = categoria.Id.ToString(),
                    Text = categoria.Titulo
                });
            }
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

        public DetalhesDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento, List<string> categorias)
        {
            Id = id;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
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
