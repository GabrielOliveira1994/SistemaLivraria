using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormLogin : Form
    {
        private string tipoUsuario; // "Cliente" ou "Editora"

        // Construtor SEM parâmetros (para o designer funcionar)
        public FormLogin()
        {
            InitializeComponent();
        }

        // Método público para definir o tipo depois
        public void DefinirTipo(string tipo)
        {
            tipoUsuario = tipo;
            this.Text = $"Login - {tipo}";
            // Se você tiver uma label lblTipoUsuario no form:
            lblTipoUsuario.Text = $"Login - {tipo}";
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha todos os campos!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tentar fazer login
            if (tipoUsuario == "Cliente")
            {
                if (FazerLoginCliente(txtEmail.Text, txtSenha.Text))
                {
                    MessageBox.Show("Login realizado com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // TODO: Abrir FormMenuCliente
                }
                else
                {
                    MessageBox.Show("Email ou senha incorretos!", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoUsuario == "Editora")
            {
                // ← MUDANÇA AQUI!
                var dadosEditora = FazerLoginEditora(txtEmail.Text, txtSenha.Text);

                if (dadosEditora.sucesso)
                {
                    MessageBox.Show("Login realizado com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abre o menu da editora
                    FormMenuEditora formMenu = new FormMenuEditora();
                    formMenu.DefinirEditora(dadosEditora.id, dadosEditora.nome);
                    formMenu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email ou senha incorretos!", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool FazerLoginCliente(string email, string senha)
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT COUNT(*) FROM CLIENTE WHERE EMAIL = @Email AND SENHA = @Senha";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fazer login: " + ex.Message);
                return false;
            }
        }

        // ← MUDA ESTE MÉTODO TAMBÉM!
        private (bool sucesso, int id, string nome) FazerLoginEditora(string email, string senha)
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT ID_EDITORA, RAZAO_SOCIAL FROM EDITORA WHERE EMAIL = @Email AND SENHA = @Senha";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nome = reader.GetString(1);
                            return (true, id, nome);
                        }
                        else
                        {
                            return (false, 0, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fazer login: " + ex.Message);
                return (false, 0, "");
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Abre tela de cadastro
            FormCadastro formCadastro = new FormCadastro();
            formCadastro.DefinirTipo(tipoUsuario);
            formCadastro.Show();
            this.Hide();
        }

       // private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
       // {
       //     Application.Exit();
       // }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnVoltar3_Click(object sender, EventArgs e)
        {
            FormMenu formLogin = new FormMenu();
            formLogin.Show();
            this.Close();
        }
    }
}