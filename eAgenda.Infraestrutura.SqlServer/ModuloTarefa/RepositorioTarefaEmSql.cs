
using eAgenda.Dominio.ModuloTarefa;
using Microsoft.Data.SqlClient;

namespace eAgenda.Infraestrutura.SqlServer.ModuloTarefa
{
    public class RepositorioTarefaEmSql : IRepositorioTarefa
    {
        private readonly string connectionString =
        "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=eAgendaDb;Integrated Security=True";
        public void AdicionarItem(ItemTarefa item)
        {
            var sqlAdicionarItemTarefa =
            @"INSERT INTO [TBITEMTAREFA]
                    (
                        [ID],
                        [TITULO],
                        [CONCLUIDO],
                        [TAREFA_ID]
                    )
                    VALUES
                    (
                        @ID,
                        @TITULO,
                        @CONCLUIDO,
                        @TAREFA_ID
                    );";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoInsercao = new SqlCommand(sqlAdicionarItemTarefa, conexaoComBanco);

            ConfigurarParametrosItemTarefa(item, comandoInsercao);

            conexaoComBanco.Open();

            comandoInsercao.ExecuteScalar();

            conexaoComBanco.Close();
        }

        private void ConfigurarParametrosItemTarefa(ItemTarefa item, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("ID", item.Id);
            comandoInsercao.Parameters.AddWithValue("TITULO", item.Titulo);
            comandoInsercao.Parameters.AddWithValue("CONCLUIDO", item.Concluido);
            comandoInsercao.Parameters.AddWithValue("TAREFA_ID", item.Tarefa.Id);
        }

        public bool AtualizarItem(ItemTarefa itemAtualizado)
        {
            var sqlEditar =
            @"UPDATE [TBITEMTAREFA]
            SET
                [TITULO] = @TITUlO,
                [CONCLUIDO] = @CONCLUIDO,
                [TAREFA_ID] = @TAREFA_ID
            WHERE
                [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosItemTarefa(itemAtualizado, comandoEdicao);

            conexaoComBanco.Open();

            var alteracoesRealizadas = comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return alteracoesRealizadas > 0;
        }

        public void Cadastrar(Tarefa tarefa)
        {
            var sqlInserir =
            @"INSERT  INTO [TAREFA]
            (
                [ID],
                [TITULO],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PRIORIDADE],
                [CONCLUIDA]
            )
            VALUES
            (
                @ID,
                @TITULO,
                @DATACRIACAO,
                @DATACONCLUSAO,
                @PRIORIDADE,
                @CONCLUIDA
            )";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosTarefa(tarefa, comandoInsercao);

            conexaoComBanco.Open();

            comandoInsercao.ExecuteScalar();

            conexaoComBanco.Close();

        }

        private void ConfigurarParametrosTarefa(Tarefa tarefa, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("ID", tarefa.Id);
            comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
            comandoInsercao.Parameters.AddWithValue("DATACRIACAO", tarefa.DataCriacao);
            comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.DataConclusao ?? (object)DBNull.Value);
            comandoInsercao.Parameters.AddWithValue("CONCLUIDA", tarefa.Concluida);
        }

        public bool Editar(Guid idTarefa, Tarefa tarefaEditada)
        {
            var sqlEditar =
                @"UPDATE [TBTAREFA]
                SET
                    [TITULO] = @TITULO,
                    [DATACRIACAO] = @DATACRIACAO,
                    [DATACONCLUSAO] = @DATACONCLUSAO,
                    [PRIORIDADE] = @PRIORIDADE,
                    [CONCLUIDA] = @CONCLUIDA
                WHERE
                    [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            tarefaEditada.Id = idTarefa;

            ConfigurarParametrosTarefa(tarefaEditada, comandoEdicao);

            conexaoComBanco.Open();

            var alteracoesRealizadas = comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return alteracoesRealizadas > 0;
        }

        public bool Excluir(Guid idTarefa)
        {
            var sqlExcluir =
            @"DELETE FROM [TBTAREFA]
            WHERE
                [ID] == @ID";

            ExcluirItensTarefa(idTarefa);

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", idTarefa);

            conexaoComBanco.Open();

            var numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return numeroRegistrosExcluidos > 0;
        }

        private void ExcluirItensTarefa(Guid idTarefa)
        {
            var sqlExcluirItensTarefa =
            @"DELETE FROM [TBITEMTAREFA]
                WHERE
                    [TAREFA_ID] = @TAREFA_ID
";
            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirItensTarefa, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("TAREFA_ID", idTarefa);

            conexaoComBanco.Open();

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public bool RemoverItem(ItemTarefa item)
        {
            var sqlExcluir =
            @"DELETE FROM [TBITEMTAREFA]
            WHERE
                [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", item.Id);

            conexaoComBanco.Open();

            var numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close() ;

            return numeroRegistrosExcluidos > 0;
        }

        public Tarefa? SelecionarTarefaPorId(Guid idTarefa)
        {
            var sqlSelecionarPorId =
                @"SELECT
                    [ID],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [CONCLUIDA]
                FROM
                    [TBTAREFA]
                WHERE
                    [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", idTarefa);

            conexaoComBanco.Open();

            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            Tarefa? tarefa = null;

            if (leitorTarefa.Read())
                tarefa = ConverterParaTarefa(leitorTarefa);

            conexaoComBanco.Close();

            return tarefa;
        }

        public List<Tarefa> SelecionarTarefas()
        {
            var sqlSelecionarTodos =
                @"SELECT
                    [ID],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [CONCLUIDA]
                FROM
                [TBTAREFA]";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            var tarefas = new List<Tarefa>();

            while (leitorTarefa.Read())
            {
                var tarefa = ConverterParaTarefa(leitorTarefa);

                tarefas.Add(tarefa);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        private Tarefa ConverterParaTarefa(SqlDataReader leitorTarefa)
        {
            DateTime? dataConclusao = null;

            if (!leitorTarefa["DATACONCLUSAO"].Equals(DBNull.Value))
                dataConclusao = Convert.ToDateTime(leitorTarefa["DATACONCLUSAO"]);

            var tarefa = new Tarefa
            {
                Id = Guid.Parse(leitorTarefa["ID"].ToString()),
                Titulo = Convert.ToString(leitorTarefa["TITULO"]),
                DataCriacao = Convert.ToDateTime(leitorTarefa["DATACRIACAO"]),
                DataConclusao = dataConclusao,
                Prioridade = (PrioridadeTarefa)leitorTarefa["PRIORIDADE"],
                Concluida = Convert.ToBoolean(leitorTarefa["CONCLUIDA"])
            };

            CarregarItensTarefa(tarefa);

            return tarefa;
        }

        private void CarregarItensTarefa(Tarefa tarefa)
        {
            var sqlSelecionarItensTarefa =
                @"SELECT
                    [ID],
                    [TITULO],
                    [CONCLUIDO],
                    [TAREFA_ID]
                FROM
                    [TBITEMTAREFA]
                WHERE
                    [TAREFA_ID] = @TAREFA_ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarItensTarefa, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("TAREFA_ID", tarefa.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorItemTarefa = comandoSelecao.ExecuteReader();

            while (leitorItemTarefa.Read())
            {
                var itemTarefa = ConverterParaItemTarefa(leitorItemTarefa, tarefa);

                tarefa.AdicionarItem(itemTarefa);
            }

            conexaoComBanco.Close();
        }

        private ItemTarefa ConverterParaItemTarefa(SqlDataReader leitorItemTarefa, Tarefa tarefa)
        {
            var itemTarefa = new ItemTarefa
            {
                Id = Guid.Parse(leitorItemTarefa["ID"].ToString()),
                Titulo = Convert.ToString(leitorItemTarefa["TITULO"]),
                Concluido = Convert.ToBoolean(leitorItemTarefa["CONCLUIDO"]),
                Tarefa = tarefa
            };

            return itemTarefa;
        }

        public List<Tarefa> SelecionarTarefasConcluidas()
        {
            var sqlSelecionarTarefasConcluidas =
                @"SELECT
                    [ID],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [CONCLUIDA]
                FROM
                    [TBTAREFA]
                WHERE
                    [CONCLUIDA] = 1";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTarefasConcluidas, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            var tarefasConcluidas = new List<Tarefa>();

            while (leitorTarefa.Read())
            {
                var tarefa = ConverterParaTarefa(leitorTarefa);
                tarefasConcluidas.Add(tarefa);
            }

            conexaoComBanco.Close();

            return tarefasConcluidas;
        }

        public List<Tarefa> SelecionarTarefasPendentes()
        {
            var sqlSelecionarTarefasPendentes =
                 @"SELECT
                    [ID],
                    [TITULO],
                    [PRIORIDADE],
                    [DATACRIACAO],
                    [DATACONCLUSAO],
                    [CONCLUIDA]
                FROM
                    [TBTAREFA]
                WHERE
                    [CONCLUIDA] = 0";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTarefasPendentes, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorTarefa = comandoSelecao.ExecuteReader();

            var tarefasPendentes = new List<Tarefa>();

            while (leitorTarefa.Read())
            {
                var tarefa = ConverterParaTarefa(leitorTarefa);
                tarefasPendentes.Add(tarefa);
            }

            conexaoComBanco.Close();

            return tarefasPendentes;
        }
    }
}
