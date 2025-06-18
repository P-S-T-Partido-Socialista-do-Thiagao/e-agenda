using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions
{
    public static class TarefaExtensions
    {
        public static Tarefa ParaEntidade(this FormularioTarefaViewModel formularioVM)
        {
            return new Tarefa(formularioVM.Titulo, formularioVM.Prioridade, formularioVM.DataCriacao, formularioVM.DataConclusao, formularioVM.PercentualConcluido);
        }


        public static DetalhesTarefaViewModel ParaDetalhesVM(this Tarefa tarefa)
        {
           return new DetalhesTarefaViewModel
            {
                Titulo = tarefa.Titulo,
                Prioridade = tarefa.Prioridade,
                DataCriacao = tarefa.DataCriacao,
                DataConclusao = tarefa.DataConclusao,
                PercentualConcluido = tarefa.PercentualConcluido,
           };
        }
    }
}
