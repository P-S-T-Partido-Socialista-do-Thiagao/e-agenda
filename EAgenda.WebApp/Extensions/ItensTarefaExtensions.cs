using EAgenda.Dominio.ModuloItensTarefa;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions;

public static class ItensTarefaExtensions
{
    public static ItensTarefa ParaEntidade(this FormularioItensTarefaViewModel formularioVM)
    {
        return new ItensTarefa(formularioVM.Titulo, formularioVM.Status, formularioVM.Tarefa);
    }

    public static DetalhesItensTarefaViewModel ItemParaDetalhesVM(this ItensTarefa item)
    {
        return new DetalhesItensTarefaViewModel
        {
            Id = item.Id,
            Titulo = item.Titulo,
            Status = item.Status,
            Tarefa = item.Tarefa
        };
    }
}
