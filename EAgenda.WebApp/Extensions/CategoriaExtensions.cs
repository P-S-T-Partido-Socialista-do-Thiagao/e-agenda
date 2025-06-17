using EAgenda.Dominio.ModuloCategoria;
using EAgenda.WebApp.Models;
using static EAgenda.WebApp.Models.VisualizarCategoriaViewModel;

namespace EAgenda.WebApp.Extensions;

public static class CategoriaExtensions
{
    public static DetalhesCategoriaViewModel ParaDetalhesVM(this Categoria categoria)
    {
        return new DetalhesCategoriaViewModel
        {
            Id = categoria.Id,
            Titulo = categoria.Titulo,
            Despesas = categoria.Despesas,
        };
    }
}
