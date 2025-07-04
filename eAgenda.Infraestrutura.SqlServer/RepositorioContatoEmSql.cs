using EAgenda.Dominio.ModuloContato;
using Microsoft.Data.SqlClient;

namespace eAgenda.Infraestrutura.SqlServer;

public class RepositorioContatoEmSql : IRepositorioContato
{
    private readonly string connectionString =
        "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=eAgendaDb;Integrated Security=True";
    public void CadastrarRegistro(Contato novoRegistro)
    {
        throw new NotImplementedException();
    }

    public bool EditarRegistro(Guid idRegistro, Contato registroEditado)
    {
        throw new NotImplementedException();
    }

    public bool ExcluirRegistro(Guid idRegistro)
    {
        throw new NotImplementedException();
    }

    public Contato SelecionarRegistroPorId(Guid idRegistro)
    {
        throw new NotImplementedException();
    }

    public List<Contato> SelecionarRegistros()
    {
        var sqlSelecionarTodos =
            @"SELECT
                    [ID],
                    [NOME],
                    [EMAIL],
                    [TELEFONE],
                    [EMPRESA],
                    [CARGO]
                FROM
                    [TBCONTATO]
";

        SqlConnection conexaoComBanco = new SqlConnection(connectionString);

        conexaoComBanco.Open();

        SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

        SqlDataReader leitor = comandoSelecao.ExecuteReader();

        var contatos = new List<Contato>();

        while (leitor.Read())
        {
            var contato = ConverterParaContato(leitor);

            contatos.Add(contato);
        }

        return contatos;
    }
    private Contato ConverterParaContato(SqlDataReader leitor)
    {
        var contato = new Contato(
            Convert.ToString(leitor["NOME"]),
            Convert.ToString(leitor["TELEFONE"]),
            Convert.ToString(leitor["EMAIL"]),
            Convert.ToString(leitor["EMPRESA"]),
            Convert.ToString(leitor["CARGO"])
    );

        contato.Id = Guid.Parse(leitor["ID"].ToString());

        return contato;
    }
}


