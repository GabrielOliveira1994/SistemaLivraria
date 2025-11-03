using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormMenuEditora : Form
    {
        private int editoraId;
        private string nomeEditora;

        public FormMenuEditora()
        {
            InitializeComponent();
            ConfigurarLayout();
        }

        // Método para configurar o layout
        private void ConfigurarLayout()
        {
            // Configurar picCapa
            picCapa.Height = 150;
            picCapa.Dock = DockStyle.Top;

            // Configurar picIcon (ajustar posição para sobrepor a capa)
            picIcon.Size = new Size(100, 100);
            picIcon.Location = new Point(20, 100);
            picIcon.BringToFront(); // Trazer para frente

            // Configurar lblBoasVindas
            lblBoasVindas.Location = new Point(130, 115);
            lblBoasVindas.Font = new Font("Arial", 14, FontStyle.Bold);
            lblBoasVindas.AutoSize = true;

            // Se quiser que o texto fique com fundo branco:
            lblBoasVindas.BackColor = Color.White;
            // OU transparente:
            // lblBoasVindas.BackColor = Color.Transparent;
        }

        // Método para definir qual editora está logada
        public void DefinirEditora(int id, string nome)
        {
            editoraId = id;
            nomeEditora = nome;
            lblBoasVindas.Text = $"Bem-vindo, {nome}!";

            // Carregar as imagens do banco
            CarregarImagensEditora();
        }

        // ===== CARREGAR IMAGENS DO BANCO =====
        private void CarregarImagensEditora()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT ICON, CAPA FROM EDITORA WHERE ID_EDITORA = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Id", editoraId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Carregar ICON
                            if (!reader.IsDBNull(0)) // Verifica se não é NULL
                            {
                                byte[] iconBytes = (byte[])reader["ICON"];
                                picIcon.Image = ConverterBytesParaImagem(iconBytes);
                            }
                            else
                            {
                                // Imagem padrão se não tiver icon
                                picIcon.BackColor = Color.LightGray;
                            }

                            // Carregar CAPA
                            if (!reader.IsDBNull(1)) // Verifica se não é NULL
                            {
                                byte[] capaBytes = (byte[])reader["CAPA"];
                                picCapa.Image = ConverterBytesParaImagem(capaBytes);
                            }
                            else
                            {
                                // Cor padrão se não tiver capa
                                picCapa.BackColor = Color.DarkGray;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar imagens: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método auxiliar para converter byte[] em Image
        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        // ===== EVENTOS DOS BOTÕES =====

        private void btnCadastrarLivro_Click(object sender, EventArgs e)
        {
            FormCadastroLivro formCadastro = new FormCadastroLivro();
            formCadastro.DefinirEditora(editoraId);
            formCadastro.ShowDialog();
        }

        private void btnMeusLivros_Click(object sender, EventArgs e)
        {
            FormMeusLivros formMeusLivros = new FormMeusLivros();
            formMeusLivros.DefinirEditora(editoraId);
            formMeusLivros.ShowDialog();
        }

        private void btnMinhasVendas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Relatório de vendas será implementado na FASE 7!",
                            "Em breve...",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            FormHome formHome = new FormHome();
            formHome.Show();
            this.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja realmente sair?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FormMenuEditora_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormHome formHome = new FormHome();
            formHome.Show();
        }
    }
}