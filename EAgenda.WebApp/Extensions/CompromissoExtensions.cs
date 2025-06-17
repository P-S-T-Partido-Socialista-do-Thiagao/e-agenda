using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions
{
    public static class CompromissoExtensions
    {
        public static DetalhesCompromissoViewModel ParaDetalhesVM(this Compromisso compromisso)
        {
            return new DetalhesCompromissoViewModel
            {
                Id = compromisso.Id,
                Assunto = compromisso.Assunto,
                DataDeOcorrencia = compromisso.DataDeOcorrencia,
                HoraDeInicio = compromisso.HoraDeInicio,
                HoraDeTermino = compromisso.HoraDeTermino,
                TipoCompromisso = compromisso.TipoCompromisso,
                Local = compromisso.Local,
                Link = compromisso.Link,
                NomeContato = compromisso.Contato.Nome
            };
        }
    }
}
