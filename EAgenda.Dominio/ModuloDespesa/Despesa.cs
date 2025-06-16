using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public string Descricao { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public decimal Valor {  get; set; }
        public string FormaPagamento {  get; set; }
        public List<string> Categorias { get; set; }
        public DateTime DataCadastro {  get; set; }
        
        public Despesa (string descricao, DateTime dataOcorrencia, decimal valor, string formaPagamento, List<string> categorias, DateTime dataCadastro)
        {
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
            DataCadastro = dataCadastro;
        }

        public override void AtualizarRegistro(Despesa registroEditado)
        {
            throw new NotImplementedException();
        }
    }
}
