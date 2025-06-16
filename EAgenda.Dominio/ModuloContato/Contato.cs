using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloContato
{
    public class Contato : EntidadeBase<Contato>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public Contato()
        {
        }
        public Contato(string nome, string email, string telefone, string empresa, string cargo) : this()
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Empresa = empresa;
            Cargo = cargo;
        }
    
        public override void AtualizarRegistro(Contato registroEditado)
        {
            throw new NotImplementedException();
        }
    }
}
