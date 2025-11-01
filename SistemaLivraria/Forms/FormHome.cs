using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaLivraria.Forms
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
            ConfigurarLayout();
            CarregarLivrosMockup(); // Dados fake por enquanto
        }

        private void ConfigurarLayout()
        {
            // Configurações do formulário
            this.Text = "Livraria - Página Inicial";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized; // Tela cheia
        }

        private void CarregarLivrosMockup()
        {
            // Por enquanto, vamos criar alguns "cards" de exemplo
            // Na FASE 5, você busca do banco de dados
            for (int i = 1; i <= 20; i++)
            {
                Panel cardLivro = CriarCardLivro(
                    $"Livro Exemplo {i}",
                    "Autor Desconhecido",
                    "Editora ABC",
                    29.90m + i
                );
                flowPanelLivros.Controls.Add(cardLivro);
            }
        }

        private Panel CriarCardLivro(string titulo, string autor, string editora, decimal preco)
        {
            Panel card = new Panel();
            card.Size = new Size(200, 280);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;

            // PictureBox para capa (por enquanto cinza)
            PictureBox picCapa = new PictureBox();
            picCapa.Size = new Size(180, 200);
            picCapa.Location = new Point(10, 10);
            picCapa.BackColor = Color.LightGray;
            picCapa.SizeMode = PictureBoxSizeMode.StretchImage;

            // Label título
            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Location = new Point(10, 215);
            lblTitulo.Size = new Size(180, 20);
            lblTitulo.Font = new Font("Arial", 10, FontStyle.Bold);

            // Label preço
            Label lblPreco = new Label();
            lblPreco.Text = $"R$ {preco:F2}";
            lblPreco.Location = new Point(10, 240);
            lblPreco.Size = new Size(180, 20);
            lblPreco.Font = new Font("Arial", 10, FontStyle.Regular);
            lblPreco.ForeColor = Color.Green;

            card.Controls.Add(picCapa);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblPreco);

            // Evento de clique (por enquanto só mostra mensagem)
            card.Click += (s, e) => MessageBox.Show($"Você clicou em: {titulo}");

            return card;
        }

        // ===== EVENTOS DOS BOTÕES =====

        // Botão LOGIN - Abre FormMenu para escolher Cliente/Editora
        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormMenu formMenu = new FormMenu();
            formMenu.Show();
            this.Hide();
        }

        // Botão CARRINHO - Por enquanto pede login
        private void btnCarrinho_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Você precisa fazer login para acessar o carrinho!",
                            "Atenção",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            // Abre o menu de login
            FormMenu formMenu = new FormMenu();
            formMenu.Show();
            this.Hide();
        }

        // Botão VOLTAR AO TOPO
        private void btnTopo_Click(object sender, EventArgs e)
        {
            flowPanelLivros.VerticalScroll.Value = 0;
        }

        // PESQUISA - Quando pressionar Enter
        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string termoPesquisa = txtPesquisa.Text;

                if (string.IsNullOrWhiteSpace(termoPesquisa))
                {
                    MessageBox.Show("Digite algo para pesquisar!", "Atenção",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Pesquisa por '{termoPesquisa}' será implementada na FASE 5!",
                                "Em breve...",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // FILTROS - ComboBox de Categoria (se você tiver)
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Filtro por categoria será implementado na FASE 5!",
                            "Em breve...",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // FILTROS - ComboBox de Editora (se você tiver)
        private void cmbEditora_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Filtro por editora será implementado na FASE 5!",
                            "Em breve...",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // Quando fechar o FormHome, fecha o aplicativo
        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {

        }
    }
}