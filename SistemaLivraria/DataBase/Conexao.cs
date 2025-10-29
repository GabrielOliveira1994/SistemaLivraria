using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaLivraria.Database
{
    public class Conexao
    {
        // String de conexão - AUTENTICAÇÃO SQL SERVER
        private static string stringConexao =
            "Data Source=sqlexpress;" +
            "Initial Catalog=CJ3027422PR2;" +
            "User ID=aluno;" +
            "Password=aluno;";

        // Método para obter a conexão
        public static SqlConnection ObterConexao()
        {
            SqlConnection conexao = new SqlConnection(stringConexao);
            try
            {
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com o banco de dados: " + ex.Message,
                                "Erro de Conexão",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }
        }

        // Método para testar a conexão
        public static bool TestarConexao()
        {
            try
            {
                using (SqlConnection conexao = ObterConexao())
                {
                    if (conexao != null)
                    {
                        conexao.Close();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}