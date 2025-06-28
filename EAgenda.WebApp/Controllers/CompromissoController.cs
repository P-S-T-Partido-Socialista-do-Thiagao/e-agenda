﻿using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Infraestrutura.ModuloCompromisso;
using EAgenda.Infraestrutura.ModuloContato;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EAgenda.WebApp.Controllers;

[Route("compromissos")]
public class CompromissoController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioCompromisso repositorioCompromisso;
    private readonly IRepositorioContato repositorioContato;

    public CompromissoController(ContextoDados contextoDados, IRepositorioCompromisso repositorioCompromisso, IRepositorioContato repositorioContato)
    {
        this.contextoDados = contextoDados;
        this.repositorioCompromisso = repositorioCompromisso;
        this.repositorioContato = repositorioContato;
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
        var contatos = repositorioContato.SelecionarRegistros();
        var cadastrarVM = new CadastrarCompromissoViewModel(contatos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
    {
        var registros = repositorioCompromisso.SelecionarRegistros();

        if (cadastrarVM.TipoCompromisso.Equals("Remoto"))
        {
            if(cadastrarVM.Link == null)
            {
                ModelState.AddModelError("CadastroUnico", "É necessário fornecer um link caso o compromisso seja remoto");
            }
        }
        else if (cadastrarVM.TipoCompromisso.Equals("Presencial"))
        {
            if(cadastrarVM.Local == null)
            {
                ModelState.AddModelError("CadastroUnico", "É necessário fornecer um local caso o compromisso seja presencial");
            }
        }

        foreach (var compromisso in registros)
        {
            if (compromisso.DataDeOcorrencia.Date == cadastrarVM.DataDeOcorrencia.Date)
            {
                if (cadastrarVM.HoraDeInicio < compromisso.HoraDeTermino &&
                    cadastrarVM.HoraDeTermino > compromisso.HoraDeInicio)
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um compromisso nesse horário.");
                    break;
                }
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var contatosDisponiveis = repositorioContato.SelecionarRegistros();

        var entidade = cadastrarVM.ParaEntidade(contatosDisponiveis);

        repositorioCompromisso.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));

    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var contatos = repositorioContato.SelecionarRegistros();
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
            registroSelecionado.Contato.Id
        );

        editarVM.ContatosDisponiveis = contatos.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Nome,
            Selected = registroSelecionado.Contato != null && c.Id == registroSelecionado.Contato.Id
        }).ToList();

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarCompromissoViewModel editarVM)
    {
        var registros = repositorioCompromisso.SelecionarRegistros();

        if (editarVM.TipoCompromisso.Equals("Remoto"))
        {
            if (editarVM.Link == null)
            {
                ModelState.AddModelError("CadastroUnico", "É necessário fornecer um link caso o compromisso seja remoto");
            }
        }
        else if (editarVM.TipoCompromisso.Equals("Presencial"))
        {
            if (editarVM.Local == null)
            {
                ModelState.AddModelError("CadastroUnico", "É necessário fornecer um local caso o compromisso seja presencial");
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var contatosDisponiveis = repositorioContato.SelecionarRegistros();

        var entidadeEditada = editarVM.ParaEntidade(contatosDisponiveis);

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

        var NomeContato = repositorioContato.SelecionarRegistroPorId(registroSelecionado.Contato.Id)?.Nome;

        var detalhesVM = new DetalhesCompromissoViewModel(
            id,
            registroSelecionado.Assunto,
            registroSelecionado.DataDeOcorrencia,
            registroSelecionado.HoraDeInicio,
            registroSelecionado.HoraDeTermino,
            registroSelecionado.TipoCompromisso,
            registroSelecionado.Local,
            registroSelecionado.Link,
            NomeContato
            );

        return View(detalhesVM);
    }

}
