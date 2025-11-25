using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormMenuCliente : Form
    {
        private int clienteId; // ID do cliente logado
        private string nomeCliente; // Nome do cliente

        public FormMenuCliente()
        {
            InitializeComponent();
            ConfigurarLayout();
        }

        // Método para definir qual cliente está logado
        public void DefinirCliente(int id, string nome)
        {
            clienteId = id;
            nomeCliente = nome;
            lblBoasVindas.Text = $"Bem-vindo, {nome}!";

            // Carregar foto de perfil
            CarregarIconCliente();
        }

        private void ConfigurarLayout()
        {
            // Configurar PictureBox do icon
            picIcon.Size = new Size(100, 100);
            picIcon.Location = new Point(350, 80);
            picIcon.BorderStyle = BorderStyle.FixedSingle;
            picIcon.BackColor = Color.LightGray;
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;

            // Configurar label
            lblBoasVindas.Location = new Point(300, 200);
            lblBoasVindas.Font = new Font("Arial", 14, FontStyle.Bold);
            lblBoasVindas.AutoSize = true;
        }

        // Carregar foto de perfil do cliente
        private void CarregarIconCliente()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT ICON FROM CLIENTE WHERE IDCLIENTE = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Id", clienteId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                byte[] iconBytes = (byte[])reader["ICON"];
                                picIcon.Image = ConverterBytesParaImagem(iconBytes);
                            }
                            else
                            {
                                picIcon.BackColor = Color.LightGray;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar foto de perfil: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        // ===== EVENTOS DOS BOTÕES =====

        // Botão CATÁLOGO - Volta para o FormHome
        private void btnCatalogo_Click(object sender, EventArgs e)
        {
            FormHome formHome = new FormHome();
            formHome.DefinirClienteLogado(clienteId, nomeCliente); // ← Mantém logado!
            formHome.Show();
            this.Close();
        }

        // Botão CARRINHO
        private void btnCarrinho_Click(object sender, EventArgs e)
        {
            FormCarrinho formCarrinho = new FormCarrinho();
            formCarrinho.DefinirCliente(clienteId, nomeCliente);
            formCarrinho.ShowDialog();
        }

        // Botão PEDIDOS
        private void btnPedidos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Histórico de pedidos será implementado na FASE 7!",
                            "Em breve...",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // Botão EDITAR PERFIL
        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Editar perfil será implementado na FASE 8!",
                            "Em breve...",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // Botão SAIR
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

        private void FormMenuCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormHome formHome = new FormHome();
            formHome.Show();
        }
    }
}