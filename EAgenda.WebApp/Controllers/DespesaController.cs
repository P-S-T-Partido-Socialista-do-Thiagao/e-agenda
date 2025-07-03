using eAgenda.WebApp.Extensions;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Compartilhado;
using EAgenda.Infraestrutura.ModuloCategoria;
using EAgenda.Infraestrutura.ModuloDespesa;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EAgenda.WebApp.Controllers
{
    [Route("despesas")]
    public class DespesaController : Controller
    {
        private readonly IRepositorioDespesa repositorioDespesa;
        private readonly IRepositorioCategoria repositorioCategoria;

        public DespesaController( IRepositorioDespesa repositorioDespesa, IRepositorioCategoria repositorioCategoria)
        {
            this.repositorioDespesa = repositorioDespesa;
            this.repositorioCategoria = repositorioCategoria;
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
            var registros = repositorioCategoria.SelecionarRegistros();
            var cadastrarVM = new CadastrarDespesaViewModel(registros);

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
        {
            var categoriasDisponiveis = repositorioCategoria.SelecionarRegistros();

            if (!ModelState.IsValid)
            {
                foreach (var cd in categoriasDisponiveis)
                {
                    var selecionarVM = new SelectListItem(cd.Titulo, cd.Id.ToString());

                    cadastrarVM.CategoriasDisponiveis?.Add(selecionarVM);
                }

                return View(cadastrarVM);
            }

            var despesa = cadastrarVM.ParaEntidade();

            // Adiciona as categorias selecionadas à despesa
            var categoriasSelecionadas = cadastrarVM.Categorias;

            if (categoriasSelecionadas is not null)
            {
                foreach (var cs in categoriasSelecionadas)
                {
                    foreach (var cd in categoriasDisponiveis)
                    {
                        if (cs.Equals(cd.Id))
                        {
                            despesa.RegistarCategoria(cd);
                            break;
                        }
                    }
                }
            }

            repositorioDespesa.CadastrarRegistro(despesa);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var categorias = repositorioCategoria.SelecionarRegistros();
            var registro = repositorioDespesa.SelecionarRegistroPorId(id);

            var editarVM = new EditarDespesaViewModel(
                id,
                registro.Descricao,
                registro.DataOcorrencia,
                registro.Valor,
                registro.FormaPagamento,
                registro.Categorias
                );

            editarVM.CategoriasDisponiveis = categorias.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Titulo,
                    Selected = registro.Categorias.Any(rc => rc.Id == c.Id)
                }).ToList();

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
        {
            var categoriasDisponiveis = repositorioCategoria.SelecionarRegistros();

            if (!ModelState.IsValid)
            {
                foreach (var cd in categoriasDisponiveis)
                {
                    var selecionarVM = new SelectListItem(cd.Titulo, cd.Id.ToString());

                    editarVM.CategoriasDisponiveis?.Add(selecionarVM);
                }

                return View(editarVM);
            }

            // Obtém dados editados
            var despesaEditada = editarVM.ParaEntidade();
            var categoriasSelecionadas = editarVM.Categorias;

            var despesaSelecionada = repositorioDespesa.SelecionarRegistroPorId(id);

            // Remove as categorias anteriores da despesa
            foreach (var categoria in despesaSelecionada.Categorias.ToList())
                despesaSelecionada.RemoverCategoria(categoria);

            // Adiciona as categorias selecionadas
            if (categoriasSelecionadas is not null)
            {
                foreach (var idSelecionado in categoriasSelecionadas)
                {
                    foreach (var categoriaDisponivel in categoriasDisponiveis)
                    {
                        if (categoriaDisponivel.Id.Equals(idSelecionado))
                        {
                            despesaSelecionada.RegistarCategoria(categoriaDisponivel);
                            break;
                        }
                    }
                }
            }

            // Atualiza os dados da despesa selecionada
            repositorioDespesa.EditarRegistro(id, despesaEditada);

            return RedirectToAction(nameof(Index));
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
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

            var detalhesVM = new DetalhesDespesaViewModel(
                id,
                registroSelecionado.Descricao,
                registroSelecionado.DataOcorrencia,
                registroSelecionado.Valor,
                registroSelecionado.FormaPagamento,
                registroSelecionado.Categorias
            );

            return View(detalhesVM);
        }
    }
}