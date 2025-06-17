using EAgenda.Dominio.ModuloContato;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions
{
    public static class ContatoExtensions
    {
        public static DetalhesContatoViewModel ParaDetalhesVM(this Contato contato)
        {
           return new DetalhesContatoViewModel
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Empresa = contato.Empresa,
                Cargo = contato.Cargo,
            };
        }
    }
}
