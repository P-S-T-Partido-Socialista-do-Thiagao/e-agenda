using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloDespesa;

namespace EAgenda.Dominio.ModuloCategoria
{
    public class Categoria : EntidadeBase<Categoria>
    {
        public string Titulo { get; set; }
        public List<Despesa> Despesas { get; set; }

        public override void AtualizarRegistro(Categoria registroEditado)
        {
            throw new NotImplementedException();
        }
    }
}
