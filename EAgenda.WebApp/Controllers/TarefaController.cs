using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Infraestrutura.ModuloTarefa;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers
{
    [Route("tarefas")]
    public class TarefaController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly IRepositorioTarefa repositorioTarefa;

        public TarefaController()
        {
            contextoDados = new ContextoDados(true);
            repositorioTarefa = new RepositorioTarefaEmArquivo(contextoDados);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarTarefaViewModel();
            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarTarefaViewModel cadastrarVM)
        {
            var registros = repositorioTarefa.SelecionarRegistros();

            var entidade = cadastrarVM.ParaEntidade();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var tarefa = repositorioTarefa.SelecionarRegistroPorId(id);

            var editarVM = new EditarTarefaViewModel(
                id,
                tarefa.Titulo,
                tarefa.Prioridade,
                tarefa.DataCriacao,
                tarefa.DataConclusao,
                tarefa.PercentualConcluido
                );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarTarefaViewModel editarVM)
        {
            var registros = repositorioTarefa.SelecionarRegistros();

            var entidadeEditada = editarVM.ParaEntidade();

            repositorioTarefa.EditarRegistro(id, entidadeEditada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registro = repositorioTarefa.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirTarefaViewModel(registro.Id, registro.Titulo);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioTarefa.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("visualizar/{id:guid}")]
        public IActionResult Visualizar(Guid id)
        {
            var registro = repositorioTarefa.SelecionarRegistroPorId(id);

            var detalhesVM = new DetalhesTarefaViewModel(
                registro.Id,
                registro.Titulo,
                registro.Prioridade,
                registro.DataCriacao,
                registro.DataConclusao,
                registro.PercentualConcluido,
                registro.Itens
            );

            return View(detalhesVM);
        }
    }
}