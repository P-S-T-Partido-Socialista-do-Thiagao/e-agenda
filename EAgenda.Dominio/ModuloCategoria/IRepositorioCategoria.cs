using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloDespesa;

namespace EAgenda.Dominio.ModuloCategoria
{
    public interface IRepositorioCategoria : IRepositorio<Categoria>
    {
        List<Despesa> SelecionarDespesasPorCategoria(Guid idCategoria);
    }
}
