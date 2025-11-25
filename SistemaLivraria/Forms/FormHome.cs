using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormHome : Form
    {
        private int? clienteIdLogado = null; // Null = não logado
        private string nomeCliente = "";

        public FormHome()
        {
            InitializeComponent();
            ConfigurarLayout();
            CarregarCategorias();
            CarregarLivros(); // ← Agora carrega livros reais!
        }

        // Método para definir que o cliente está logado
        public void DefinirClienteLogado(int id, string nome)
        {
            clienteIdLogado = id;
            nomeCliente = nome;

            // Mudar texto do botão Login para mostrar nome do cliente
            btnLogin.Text = $"👤 {nome}";
        }

        private void ConfigurarLayout()
        {
            this.Text = "Livraria - Página Inicial";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        // ===== CARREGAR CATEGORIAS NO COMBOBOX =====
        private void CarregarCategorias()
        {
            try
            {
                cmbCategoria.Items.Clear();
                cmbCategoria.Items.Add("Todas as Categorias"); // Item padrão

                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT ID_CATEGORIA, NOME_CATEGORIA FROM CATEGORIAS ORDER BY NOME_CATEGORIA";
                    SqlCommand cmd = new SqlCommand(query, conexao);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategoria.Items.Add(new ItemComboBox
                            {
                                Value = reader.GetInt32(0),
                                Text = reader.GetString(1)
                            });
                        }
                    }
                }

                cmbCategoria.DisplayMember = "Text";
                cmbCategoria.ValueMember = "Value";
                cmbCategoria.SelectedIndex = 0; // Seleciona "Todas"
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Classe auxiliar para ComboBox
        public class ItemComboBox
        {
            public int Value { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        // ===== CARREGAR LIVROS DO BANCO =====
        private void CarregarLivros(string termoPesquisa = "", int? categoriaId = null)
        {
            try
            {
                flowPanelLivros.Controls.Clear(); // Limpa os cards anteriores

                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    // Query com filtros opcionais
                    string query = @"SELECT 
                                        L.ID_LIVRO,
                                        L.TITULO,
                                        L.PRECO,
                                        L.CAPA,
                                        C.NOME_CATEGORIA,
                                        E.RAZAO_SOCIAL
                                    FROM LIVROS L
                                    INNER JOIN CATEGORIAS C ON L.ID_CATEGORIA = C.ID_CATEGORIA
                                    INNER JOIN EDITORA E ON L.ID_EDITORA = E.ID_EDITORA
                                    WHERE L.ATIVO = 1 AND L.QUANTIDADE_ESTOQUE > 0";

                    // Adicionar filtro de pesquisa
                    if (!string.IsNullOrWhiteSpace(termoPesquisa))
                    {
                        query += " AND (L.TITULO LIKE @Pesquisa OR E.RAZAO_SOCIAL LIKE @Pesquisa)";
                    }

                    // Adicionar filtro de categoria
                    if (categoriaId.HasValue)
                    {
                        query += " AND L.ID_CATEGORIA = @CategoriaId";
                    }

                    query += " ORDER BY L.TITULO";

                    SqlCommand cmd = new SqlCommand(query, conexao);

                    if (!string.IsNullOrWhiteSpace(termoPesquisa))
                    {
                        cmd.Parameters.AddWithValue("@Pesquisa", "%" + termoPesquisa + "%");
                    }

                    if (categoriaId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@CategoriaId", categoriaId.Value);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int contador = 0;

                        while (reader.Read())
                        {
                            int livroId = reader.GetInt32(0);
                            string titulo = reader.GetString(1);
                            decimal preco = reader.GetDecimal(2);
                            byte[] capaBytes = reader.IsDBNull(3) ? null : (byte[])reader["CAPA"];
                            string categoria = reader.GetString(4);
                            string editora = reader.GetString(5);

                            Panel card = CriarCardLivro(livroId, titulo, editora, categoria, preco, capaBytes);
                            flowPanelLivros.Controls.Add(card);
                            contador++;
                        }

                        // Mensagem se não encontrar livros
                        if (contador == 0)
                        {
                            Label lblVazio = new Label();
                            lblVazio.Text = "Nenhum livro encontrado.";
                            lblVazio.Font = new Font("Arial", 14, FontStyle.Bold);
                            lblVazio.ForeColor = Color.Gray;
                            lblVazio.AutoSize = true;
                            flowPanelLivros.Controls.Add(lblVazio);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar livros: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== CRIAR CARD DE LIVRO =====
        private Panel CriarCardLivro(int livroId, string titulo, string editora, string categoria, decimal preco, byte[] capaBytes)
        {
            Panel card = new Panel();
            card.Size = new Size(200, 320);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            card.Cursor = Cursors.Hand;

            // PictureBox para capa
            PictureBox picCapa = new PictureBox();
            picCapa.Size = new Size(180, 220);
            picCapa.Location = new Point(10, 10);
            picCapa.BackColor = Color.LightGray;
            picCapa.SizeMode = PictureBoxSizeMode.Zoom;

            // Carregar imagem da capa
            if (capaBytes != null && capaBytes.Length > 0)
            {
                picCapa.Image = ConverterBytesParaImagem(capaBytes);
            }

            // Label título
            Label lblTitulo = new Label();
            lblTitulo.Text = titulo.Length > 30 ? titulo.Substring(0, 27) + "..." : titulo;
            lblTitulo.Location = new Point(10, 235);
            lblTitulo.Size = new Size(180, 20);
            lblTitulo.Font = new Font("Arial", 10, FontStyle.Bold);

            // Label editora
            Label lblEditora = new Label();
            lblEditora.Text = editora.Length > 25 ? editora.Substring(0, 22) + "..." : editora;
            lblEditora.Location = new Point(10, 255);
            lblEditora.Size = new Size(180, 18);
            lblEditora.Font = new Font("Arial", 8, FontStyle.Regular);
            lblEditora.ForeColor = Color.Gray;

            // Label categoria
            Label lblCategoria = new Label();
            lblCategoria.Text = categoria;
            lblCategoria.Location = new Point(10, 273);
            lblCategoria.Size = new Size(180, 18);
            lblCategoria.Font = new Font("Arial", 8, FontStyle.Italic);
            lblCategoria.ForeColor = Color.DarkBlue;

            // Label preço
            Label lblPreco = new Label();
            lblPreco.Text = $"R$ {preco:F2}";
            lblPreco.Location = new Point(10, 291);
            lblPreco.Size = new Size(180, 20);
            lblPreco.Font = new Font("Arial", 11, FontStyle.Bold);
            lblPreco.ForeColor = Color.Green;

            card.Controls.Add(picCapa);
            card.Controls.Add(lblTitulo);
            card.Controls.Add(lblEditora);
            card.Controls.Add(lblCategoria);
            card.Controls.Add(lblPreco);

            // Evento de clique - Abre detalhes do livro
            card.Click += (s, e) => AbrirDetalhesLivro(livroId);
            picCapa.Click += (s, e) => AbrirDetalhesLivro(livroId);
            lblTitulo.Click += (s, e) => AbrirDetalhesLivro(livroId);
            lblPreco.Click += (s, e) => AbrirDetalhesLivro(livroId);

            return card;
        }

        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        // ===== ABRIR DETALHES DO LIVRO =====
        private void AbrirDetalhesLivro(int livroId)
        {
            FormDetalhesLivro formDetalhes = new FormDetalhesLivro();
            formDetalhes.CarregarLivro(livroId, clienteIdLogado);
            formDetalhes.ShowDialog();
        }

        // ===== EVENTOS DOS BOTÕES =====

        // Botão LOGIN/PERFIL
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Se está logado, abre menu do cliente
            if (clienteIdLogado.HasValue)
            {
                FormMenuCliente formMenu = new FormMenuCliente();
                formMenu.DefinirCliente(clienteIdLogado.Value, nomeCliente);
                formMenu.Show();
                this.Hide();
            }
            // Se não está logado, abre tela de login
            else
            {
                FormMenu formMenu = new FormMenu();
                formMenu.Show();
                this.Hide();
            }
        }

        // Botão CARRINHO
        private void btnCarrinho_Click(object sender, EventArgs e)
        {
            if (!clienteIdLogado.HasValue)
            {
                DialogResult resultado = MessageBox.Show(
                    "Você precisa fazer login para adicionar ao carrinho!\n\nDeseja fazer login agora?",
                    "Login Necessário",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    FormMenu formMenu = new FormMenu();
                    formMenu.Show();
                    this.Hide();
                }
                return;
            }
            else
            {
                // Abre o carrinho!
                FormCarrinho formCarrinho = new FormCarrinho();
                formCarrinho.DefinirCliente(clienteIdLogado.Value, nomeCliente);
                formCarrinho.ShowDialog();
            }
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
                string termoPesquisa = txtPesquisa.Text.Trim();

                // Pegar categoria selecionada
                int? categoriaId = null;
                if (cmbCategoria.SelectedIndex > 0) // Não é "Todas"
                {
                    ItemComboBox item = (ItemComboBox)cmbCategoria.SelectedItem;
                    categoriaId = item.Value;
                }

                CarregarLivros(termoPesquisa, categoriaId);
            }
        }

        // FILTRO - ComboBox de Categoria
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string termoPesquisa = txtPesquisa.Text.Trim();

            // Pegar categoria selecionada
            int? categoriaId = null;
            if (cmbCategoria.SelectedIndex > 0) // Não é "Todas"
            {
                ItemComboBox item = (ItemComboBox)cmbCategoria.SelectedItem;
                categoriaId = item.Value;
            }

            CarregarLivros(termoPesquisa, categoriaId);
        }

        // Quando fechar o FormHome, fecha o aplicativo
        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}