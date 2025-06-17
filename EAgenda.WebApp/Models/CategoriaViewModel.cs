using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;
namespace EAgenda.WebApp.Models;

public abstract class FormularioCategoriaViewModel
{
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Título\" deve ter no mínimo 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Título\" deve ter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
    public List<Despesa> Despesas { get; set; } = new List<Despesa>(); 
}

public class CadastrarCategoriaViewModel : FormularioCategoriaViewModel
{
    public CadastrarCategoriaViewModel() { }

    public CadastrarCategoriaViewModel(string titulo, List<Despesa> despesas) : this()
    {
        Titulo = titulo;
        Despesas = despesas;
    }
}

public class EditarCategoriaViewModel : FormularioCategoriaViewModel
{
    public Guid Id { get; set; }

    public EditarCategoriaViewModel() { }

    public EditarCategoriaViewModel(Guid id, string titulo, List<Despesa> despesas)
    {
        Id = id;
        Titulo = titulo;
        Despesas = despesas;
    }
}


public class ExcluirCategoriaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirCategoriaViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
}

public class VisualizarCategoriaViewModel
{
    public List<DetalhesCategoriaViewModel> Registros { get; } = new List<DetalhesCategoriaViewModel>();

    public VisualizarCategoriaViewModel(List<Categoria> categorias)
    {
        foreach (Categoria c in categorias)
            Registros.Add(c.ParaDetalhesVM());
    }
}


    public class DetalhesCategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public List<Despesa> Despesas { get; set; }
        public DetalhesCategoriaViewModel() { }
        public DetalhesCategoriaViewModel(Guid id, string titulo, List<Despesa> despesas)
        {
            Id = id;
            Titulo = titulo;
            Despesas = despesas;
        }
    }

    public class SelecionarCategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }

        public SelecionarCategoriaViewModel(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }
