using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloDespesa;

namespace EAgenda.Dominio.ModuloCategoria
{
    public class Categoria : EntidadeBase<Categoria>
    {
        public string Titulo { get; set; }
        public List<Despesa> Despesas { get; set; }

        public Categoria() { }

        public Categoria(string titulo, List<Despesa> despesas)
        {
            Titulo = titulo;
            Despesas = despesas;
        }

        public override void AtualizarRegistro(Categoria registroEditado)
        {
            throw new NotImplementedException();
        }
    }
}
