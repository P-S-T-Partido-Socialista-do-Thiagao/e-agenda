using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Dominio.ModuloItensTarefa;
using EAgenda.Infraestrutura.ModuloItensTarefa;
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

    public ItensTarefaController()
    {
        contextoDados = new ContextoDados(true);
        repositorioItensTarefa = new RepositorioItensTarefaEmArquivo(contextoDados);
    }

    public IActionResult Index()
    {
        var registros = repositorioItensTarefa.SelecionarRegistros();

        var visualizarVM = new VisualizarItensTarefaViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarItensTarefaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarItensTarefaViewModel cadastrarVM)
    {
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

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioItensTarefa.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirItensTarefaViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var registros = repositorioItensTarefa.SelecionarRegistros();

        repositorioItensTarefa.ExcluirRegistro(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("AlternarStatus/{id:guid}")]
    public IActionResult AlternarStatus(Guid id)
    {
        var item = repositorioItensTarefa.SelecionarRegistroPorId(id);
        if (item == null)
            
            return NotFound();

        item.Status = item.Status == "Concluído" ? "Incompleto" : "Concluído";
        repositorioItensTarefa.EditarRegistro(id, item);

        return RedirectToAction(nameof(Index));
    }
}
