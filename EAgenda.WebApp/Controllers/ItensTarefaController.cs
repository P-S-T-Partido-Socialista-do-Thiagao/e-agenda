using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Dominio.ModuloItensTarefa;
using EAgenda.Infraestrutura.ModuloItensTarefa;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.ModuloTarefa;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;

namespace EAgenda.WebApp.Controllers;


[Route("ItensTarefa")]
public class ItensTarefaController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioItensTarefa repositorioItensTarefa;
    private readonly IRepositorioTarefa repositorioTarefa;

    public ItensTarefaController()
    {
        contextoDados = new ContextoDados(true);
        repositorioItensTarefa = new RepositorioItensTarefaEmArquivo(contextoDados);
        repositorioTarefa = new RepositorioTarefaEmArquivo(contextoDados);
    }

    public IActionResult Index()
    {
        var registros = repositorioItensTarefa.SelecionarRegistros();

        var visualizarVM = new VisualizarItensTarefaViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar(Guid tarefaId)
    {
        var cadastrarVM = new CadastrarItensTarefaViewModel();
        cadastrarVM.Tarefa = repositorioTarefa.SelecionarRegistroPorId(tarefaId);

        cadastrarVM.TarefaId = tarefaId;

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarItensTarefaViewModel cadastrarVM, Guid tarefaId)
    {
        cadastrarVM.Tarefa = repositorioTarefa.SelecionarRegistroPorId(tarefaId);
        var registros = repositorioItensTarefa.SelecionarRegistros();

        //foreach (var item in registros)
        //{
        //    if (item.Titulo.Equals(cadastrarVM.Titulo))
        //    {
        //        ModelState.AddModelError("CadastroUnico", "Já existe um item com esse nome registrado.");
        //        break;
        //    }
        //}

        //if (!ModelState.IsValid)
        //    return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioItensTarefa.CadastrarRegistro(entidade);

        return RedirectToAction("PorTarefa", new { tarefaId });
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioItensTarefa.SelecionarRegistros()
            .FirstOrDefault(x => x.Id == id);

        var excluirVM = new ExcluirItensTarefaViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var registroSelecionado = repositorioItensTarefa.SelecionarRegistros()
          .FirstOrDefault(x => x.Id == id);

        Guid tarefaIdDaQualVeio = registroSelecionado.Tarefa.Id;

        repositorioItensTarefa.ExcluirItem(registroSelecionado);

        return RedirectToAction("PorTarefa", new { tarefaId = tarefaIdDaQualVeio });
    }

    [HttpPost("AlternarStatus/{id:guid}")]
    public IActionResult AlternarStatus(Guid id, EditarItensTarefaViewModel editarVM)
    {
        var registroSelecionado = repositorioItensTarefa.SelecionarRegistros()
          .FirstOrDefault(x => x.Id == id);

        Guid tarefaIdDaQualVeio = registroSelecionado.Tarefa.Id;

        registroSelecionado.Status = registroSelecionado.Status == "Concluído" ? "Incompleto" : "Concluído";
        
        repositorioItensTarefa.EditarRegistro(id, registroSelecionado);
        contextoDados.Salvar();

        return RedirectToAction("PorTarefa", new { tarefaId = tarefaIdDaQualVeio});
    }

    [HttpGet("PorTarefa/{tarefaId:guid}")]
    public IActionResult PorTarefa(Guid tarefaId)
    {
        var itensDaTarefa = repositorioItensTarefa
            .SelecionarRegistros()
            .Where(x => x.Tarefa != null && x.Tarefa.Id == tarefaId)
            .ToList();

        var visualizarVM = new VisualizarItensTarefaViewModel(itensDaTarefa);

        ViewBag.TarefaId = tarefaId; // Para usar em links de adicionar item

        return View("Index", visualizarVM);
    }
}
