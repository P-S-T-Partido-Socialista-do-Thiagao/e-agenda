using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloTarefa
{
    public class Tarefa : EntidadeBase<Tarefa>
    {
        public string Titulo { get; set; }
        public string Prioridade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public float PercentualConcluido { get; set; }
        public List<ItensTarefa> Itens { get; set; }

        public Tarefa()
        {
            Itens = new List<ItensTarefa>();
        }

        public Tarefa(string titulo, string prioridade, DateTime dataCriacao, DateTime dataConclusao, float percentualConcluido)
        {
            Titulo = titulo;
            Prioridade = prioridade;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            PercentualConcluido = percentualConcluido;
            Itens = new List<ItensTarefa>();
        }
        public override void AtualizarRegistro(Tarefa registroEditado)
        {
            throw new NotImplementedException();
        }
    }
}
