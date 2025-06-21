using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Dominio.ModuloItensTarefa;
using EAgenda.Infraestrutura.ModuloItensTarefa;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

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

        foreach (var item in registros)
        {
            if (item.Titulo.Equals(cadastrarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um item com esse nome registrado.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioItensTarefa.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

}
