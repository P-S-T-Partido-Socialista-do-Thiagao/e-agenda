using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Infraestrutura.ModuloDespesa;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers
{
    [Route("despesas")]
    public class DespesaController : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly IRepositorioDespesa repositorioDespesa;

        public DespesaController()
        {
            contextoDados = new ContextoDados(true);
            repositorioDespesa = new RepositorioDespesaEmArquivo(contextoDados);
        }
        public IActionResult Index()
        {
            var registros = repositorioDespesa.SelecionarRegistros();
            var visualizarVM = new VisualizarDespesaViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarDespesaViewModel();

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
        {
            var registros = repositorioDespesa.SelecionarRegistros();
            foreach (var item in registros)
            {
                if (item.Descricao.Equals(cadastrarVM.Descricao))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma despesa registrada com essa descrição.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();

            repositorioDespesa.CadastrarRegistro(entidade);

            return RedirectToAction("Index");
        }

        [HttpGet("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id)
        {
            var registro = repositorioDespesa.SelecionarRegistroPorId(id);

            var editarVM = new EditarDespesaViewModel(
                id,
                registro.Descricao,
                registro.DataOcorrencia,
                registro.Valor,
                registro.FormaPagamento,
                registro.Categorias,
                registro.DataCadastro
                );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
        {
            var registros = repositorioDespesa.SelecionarRegistros();
            foreach (var item in registros)
            {
                if (item.Descricao.Equals(editarVM.Descricao) && item.Id != id)
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma despesa registrada com essa descrição.");
                    break;
                }
            }
            if (!ModelState.IsValid)
                return View(editarVM);

            var entidade = editarVM.ParaEntidade();

            repositorioDespesa.EditarRegistro(id, entidade);

            return RedirectToAction("Index");
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registro = repositorioDespesa.SelecionarRegistroPorId(id);
            var excluirVM = new ExcluirDespesaViewModel(id, registro.Descricao);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioDespesa.ExcluirRegistro(id);
            return RedirectToAction("Index");
        }

        [HttpGet("detalhes/{id:guid}")]
        public IActionResult Detalhes(Guid id)
        {
            var registro = repositorioDespesa.SelecionarRegistroPorId(id);

            var detalhesVM = new DetalhesDespesaViewModel(
                registro.Id,
                registro.Descricao,
                registro.DataOcorrencia,
                registro.Valor,
                registro.FormaPagamento,
                registro.Categorias,
                registro.DataCadastro
            );

            return View(detalhesVM);
        }
    }
}