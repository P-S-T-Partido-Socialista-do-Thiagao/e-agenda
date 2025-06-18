using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Dominio.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using EAgenda.Infraestrutura.ModuloContato;
using EAgenda.WebApp.Models;
using EAgenda.WebApp.Extensions;

namespace EAgenda.WebApp.Controllers;

[Route("contatos")]
public class ContatoController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioContato repositorioContato;

    public ContatoController()
    {
        contextoDados = new ContextoDados(true);
        repositorioContato = new RepositorioContatoEmArquivo(contextoDados);
    }

    public IActionResult Index()
    {
        var registros = repositorioContato.SelecionarRegistros();

        var visualizarVM = new VisualizarContatoViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarContatoViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarContatoViewModel cadastrarVM)
    {
        var registros = repositorioContato.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (item.Email.Equals(cadastrarVM.Email))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um contato com esse email registrado.");
                break;
            }

            if (item.Telefone.Equals(cadastrarVM.Telefone))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um contato com esse telefone registrado");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioContato.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

        var editarVM = new EditarContatoViewModel(
                id,
                registroSelecionado.Nome,
                registroSelecionado.Email,
                registroSelecionado.Telefone,
                registroSelecionado.Cargo,
                registroSelecionado.Empresa
            );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarContatoViewModel editarVM)
    {
        var registros = repositorioContato.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Email.Equals(editarVM.Email))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um contato com esse email.");
                break;
            }

            if (!item.Id.Equals(id) && item.Telefone.Equals(editarVM.Telefone))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um contato com esse telefone");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        repositorioContato.EditarRegistro(id, entidadeEditada);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirContatoViewModel(registroSelecionado.Id, registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioContato.ExcluirRegistro(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

        var detalhesVM = new DetalhesContatoViewModel(
            id,
            registroSelecionado.Nome,
            registroSelecionado.Email,
            registroSelecionado.Telefone,
            registroSelecionado.Cargo,
            registroSelecionado.Empresa
            );

        return View(detalhesVM);
    }
}
