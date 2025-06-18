using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace EAgenda.WebApp.Models
{
    public class FormularioCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo \"Assunto\" é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo \"Assunto\" deve ter no mínimo 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Assunto\" deve ter no máximo 100 caracteres.")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "O campo \"Data de Ocorrência\" é obrigatório.")]
        public DateTime DataDeOcorrencia { get; set; }

        [Required(ErrorMessage = "O campo \"Hora de Início\" é obrigatório.")]
        public TimeSpan HoraDeInicio { get; set; }

        [Required(ErrorMessage = "O campo \"Hora de Término\" é obrigatório.")]
        public TimeSpan HoraDeTermino { get; set; }

        [Required(ErrorMessage = "O campo \"Tipo de Compromisso\" é obrigatório.")]
        public string TipoCompromisso { get; set; }

        public string? Local { get; set; }
        public string? Link { get; set; }
        public Contato? Contato { get; set; }
    }

    public class CadastrarCompromissoViewModel : FormularioCompromissoViewModel
    {
        public CadastrarCompromissoViewModel() { }

        public CadastrarCompromissoViewModel(string assunto, DateTime dataDeOcorrencia, TimeSpan horaDeInicio, TimeSpan horaDeTermino, string tipoCompromisso, string? local, string? link, Contato? contato)
        {
            Assunto = assunto;
            DataDeOcorrencia = dataDeOcorrencia;
            HoraDeInicio = horaDeInicio;
            HoraDeTermino = horaDeTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
            Contato = contato;
        }
    }

    public class EditarCompromissoViewModel : FormularioCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }

        public EditarCompromissoViewModel() { }

        public EditarCompromissoViewModel(Guid id, string assunto, DateTime dataDeOcorrencia, TimeSpan horaDeInicio, TimeSpan horaDeTermino, string tipoCompromisso, string? local, string? link, Contato? contato)
        {
            Id = id;
            Assunto = assunto;
            DataDeOcorrencia = dataDeOcorrencia;
            HoraDeInicio = horaDeInicio;
            HoraDeTermino = horaDeTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
            Contato = contato;
        }
    }

    public class ExcluirCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo \"Id\" é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo \"Assunto\" é obrigatório.")]
        public string Assunto { get; set; }

        public ExcluirCompromissoViewModel() { }

        public ExcluirCompromissoViewModel(Guid id, string assunto)
        {
            Id = id;
            Assunto = assunto;
        }
    }

    public class VisualizarCompromissoViewModel
    {
        public List<DetalhesCompromissoViewModel> Registros { get; set; }

        public VisualizarCompromissoViewModel(List<Compromisso> compromissos)
        {
            Registros = new List<DetalhesCompromissoViewModel>();

            foreach (var compromisso in compromissos)
            {
                Registros.Add(compromisso.ParaDetalhesVM());
            }
        }
    }


    public class DetalhesCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public DateTime DataDeOcorrencia { get; set; }
        public TimeSpan HoraDeInicio { get; set; }
        public TimeSpan HoraDeTermino { get; set; }
        public string TipoCompromisso { get; set; }
        public string? Local { get; set; }
        public string? Link { get; set; }
        public string? NomeContato { get; set; }

        public DetalhesCompromissoViewModel() { }

        public DetalhesCompromissoViewModel(Guid id, string assunto, DateTime dataDeOcorrencia, TimeSpan horaDeInicio, TimeSpan horaDeTermino, string tipoCompromisso, string? local, string? link, string? nomeContato)
        {
            Id = id;
            Assunto = assunto;
            DataDeOcorrencia = dataDeOcorrencia;
            HoraDeInicio = horaDeInicio;
            HoraDeTermino = horaDeTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
            NomeContato = nomeContato;
        }

        public DetalhesCompromissoViewModel(Guid id, string assunto, DateTime dataDeOcorrencia, TimeSpan horaDeInicio, TimeSpan horaDeTermino, string tipoCompromisso, string? local, string? link)
        {
            Id = id;
            Assunto = assunto;
            DataDeOcorrencia = dataDeOcorrencia;
            HoraDeInicio = horaDeInicio;
            HoraDeTermino = horaDeTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
        }
    }

    public class SelecionarCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public SelecionarCompromissoViewModel() { }
        public SelecionarCompromissoViewModel(Guid id, string assunto)
        {
            Id = id;
            Assunto = assunto;
        }
    }
}