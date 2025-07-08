using eAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public string Descricao { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public decimal Valor {  get; set; }
        public string FormaPagamento {  get; set; }
        public List<Categoria> Categorias { get; set; }

        public Despesa()
        {
            Categorias = new List<Categoria>();
        }
        public Despesa (string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento) : this()
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
        }

        public void RegistarCategoria(Categoria categoria)
        {
            if (Categorias.Contains(categoria))
                return;

            categoria.Despesas.Add(this);
            Categorias.Add(categoria);
        }

        public void RemoverCategoria(Categoria categoria)
        {
            if (!Categorias.Contains(categoria))
                return;

            categoria.Despesas.Remove(this);
            Categorias.Remove(categoria);
        }

        public override void AtualizarRegistro(Despesa registroEditado)
        {
            Descricao = registroEditado.Descricao;
            DataOcorrencia = registroEditado.DataOcorrencia;
            Valor = registroEditado.Valor;
            FormaPagamento = registroEditado.FormaPagamento;
            Categorias = registroEditado.Categorias;
        }
    }
}
