using EAgenda.Dominio.Compartilhado;
using EAgenda.Dominio.ModuloTarefa;
namespace EAgenda.Dominio.ModuloItensTarefa
{
    public class ItensTarefa : EntidadeBase<ItensTarefa>
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Status { get; set; }
        public Tarefa Tarefa { get; set; }
        public ItensTarefa() { }
        public ItensTarefa(string titulo, string status, Tarefa tarefa)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Status = status;
            Tarefa = tarefa;
        }

        public override void AtualizarRegistro(ItensTarefa registroEditado)
        {
            Titulo = registroEditado.Titulo;
            Status = registroEditado.Status;
            Tarefa = registroEditado.Tarefa;
        }
    }
}
