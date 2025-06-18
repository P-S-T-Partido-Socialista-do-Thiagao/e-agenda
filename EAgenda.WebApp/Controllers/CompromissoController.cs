using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Infraestrutura.ModuloCompromisso;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers;

[Route("compromissos")]
public class CompromissoController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioCompromisso repositorioCompromisso;

    public CompromissoController()
    {
        contextoDados = new ContextoDados(true);
        repositorioCompromisso = new RepositorioCompromissoEmArquivo(contextoDados);
    }

    public IActionResult Index()
    {
        var registros = repositorioCompromisso.SelecionarRegistros();

        var visualizarVM = new VisualizarCompromissoViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarCompromissoViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
    {
        var registros = repositorioCompromisso.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (item.HoraDeInicio.Equals(cadastrarVM.HoraDeInicio))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um compromisso registrado nesse horário.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioCompromisso.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));

    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

        var editarVM = new EditarCompromissoViewModel(
            id,
            registroSelecionado.Assunto, 
            registroSelecionado.DataDeOcorrencia, 
            registroSelecionado.HoraDeInicio, 
            registroSelecionado.HoraDeTermino, 
            registroSelecionado.TipoCompromisso,
            registroSelecionado.Local, 
            registroSelecionado.Link, 
            registroSelecionado.Contato
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarCompromissoViewModel editarVM)
    {
        var registros = repositorioCompromisso.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.HoraDeInicio.Equals(editarVM.HoraDeInicio))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um compromisso registrado nesse horário.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        repositorioCompromisso.EditarRegistro(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirCompromissoViewModel(registroSelecionado.Id, registroSelecionado.Assunto);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioCompromisso.ExcluirRegistro(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

        var detalhesVM = new DetalhesCompromissoViewModel(
            id,
            registroSelecionado.Assunto,
            registroSelecionado.DataDeOcorrencia,
            registroSelecionado.HoraDeInicio,
            registroSelecionado.HoraDeTermino,
            registroSelecionado.TipoCompromisso,
            registroSelecionado.Local,
            registroSelecionado.Link
            );

        return View(detalhesVM);
    }

}
