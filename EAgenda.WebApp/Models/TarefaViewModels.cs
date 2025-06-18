using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace EAgenda.WebApp.Models
{
    public class FormularioTarefaViewModel
    {
        [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Título\" deve ter no mínimo 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Título\" deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo \"Prioridade\" é obrigatório.")]
        public string Prioridade { get; set; }

        [Required(ErrorMessage = "O campo \"Data de Criação\" é obrigatório.")]
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "O campo \"Data de Conclusão\" é obrigatório.")]
        public DateTime DataConclusao { get; set; }
        public float PercentualConcluido { get; set; }
    }

    public class CadastrarTarefaViewModel : FormularioTarefaViewModel
    {
        public CadastrarTarefaViewModel() { }

        public CadastrarTarefaViewModel(string titulo, string prioridade, DateTime dataCriacao, DateTime dataConclusao, float percentualConcluido)
        {
            Titulo = titulo;
            Prioridade = prioridade;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            PercentualConcluido = percentualConcluido;
        }
    }

    public class EditarTarefaViewModel : FormularioTarefaViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }

        public EditarTarefaViewModel() { }

        public EditarTarefaViewModel(Guid id, string titulo, string prioridade, DateTime dataCriacao, DateTime dataConclusao, float percentualConcluido)
        {
            Id = id;
            Titulo = titulo;
            Prioridade = prioridade;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            PercentualConcluido = percentualConcluido;
        }
    }

    public class ExcluirTarefaViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo \"Titulo\" é obrigatório.")]
        public string Titulo { get; set; }

        public ExcluirTarefaViewModel() { }

        public ExcluirTarefaViewModel(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }

    public class VisualizarTarefaViewModel
    {
        public List<DetalhesTarefaViewModel> Registros { get; set; }

        public VisualizarTarefaViewModel(List<Tarefa> tarefas)
        {
            Registros = new List<DetalhesTarefaViewModel>();

            foreach (var tarefa in tarefas)
            {
                Registros.Add(tarefa.ParaDetalhesVM());
            }
        }
    }


    public class DetalhesTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public float PercentualConcluido { get; set; }
        public List<string> Itens { get; set; } = new List<string>();

        public DetalhesTarefaViewModel() { }

        public DetalhesTarefaViewModel(Guid id, string titulo, string prioridade, DateTime dataCriacao, DateTime dataConclusao, float percentualConcluido, List<string> itens)
        {
            Id = id;
            Titulo = titulo;
            Prioridade = prioridade;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            PercentualConcluido = percentualConcluido;
            Itens = itens ?? new List<string>();
        }
    }

    public class SelecionarTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public SelecionarTarefaViewModel() { }
        public SelecionarTarefaViewModel(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }
}