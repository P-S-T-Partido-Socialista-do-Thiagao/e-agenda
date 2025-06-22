using EAgenda.Dominio.ModuloItensTarefa;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.Extensions;

namespace EAgenda.WebApp.Models;

public class FormularioItensTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Status { get; set; } = "Incompleto";
    public Tarefa Tarefa { get; set; }

}

public class CadastrarItensTarefaViewModel : FormularioItensTarefaViewModel
{
    public CadastrarItensTarefaViewModel() { }

    public CadastrarItensTarefaViewModel(string titulo, string status, Tarefa tarefa)
    {
        Titulo = titulo;
        Status = status;
        Tarefa = tarefa;
    }    
}

public class ExcluirItensTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirItensTarefaViewModel() { }

    public ExcluirItensTarefaViewModel(Guid id, string titulo) 
    {
        Id = id;
        Titulo = titulo;
    }
}

public class VisualizarItensTarefaViewModel
{
    public List<DetalhesItensTarefaViewModel> Registros { get; set; }

    public VisualizarItensTarefaViewModel(List<ItensTarefa> itens)
    {
        Registros = new List<DetalhesItensTarefaViewModel>();

        foreach (var item in itens)
        {
            Registros.Add(item.ItemParaDetalhesVM());
        }
    }
}

public class DetalhesItensTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Status { get; set; }
    public Tarefa Tarefa { get; set; }

    public DetalhesItensTarefaViewModel () { }

    public DetalhesItensTarefaViewModel(Guid id, string titulo, string status, Tarefa tarefa) 
    {
        Id = id;
        Titulo = titulo;
        Status = status;
        Tarefa = tarefa;
    }

    public class SelecionarItensTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public SelecionarItensTarefaViewModel() { }
        public SelecionarItensTarefaViewModel(Guid id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }
}

