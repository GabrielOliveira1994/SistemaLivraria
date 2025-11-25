using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SistemaLivraria.Database;
using SistemaLivraria.Models;

namespace SistemaLivraria.Forms
{
    public partial class FormDetalhesLivro : Form
    {
        private int livroId;
        private int? clienteIdLogado;
        private decimal precoLivro;
        private int editoraId;
        private string nomeEditora;
        private byte[] capaBytes;

        public FormDetalhesLivro()
        {
            InitializeComponent();
            ConfigurarLayout();
        }

        private void ConfigurarLayout()
        {
            // Configurar PictureBox da capa
            picCapa.Size = new Size(250, 350);
            picCapa.Location = new Point(30, 30);
            picCapa.BorderStyle = BorderStyle.FixedSingle;
            picCapa.BackColor = Color.LightGray;
            picCapa.SizeMode = PictureBoxSizeMode.Zoom;

            // Configurar TextBox da sinopse
            txtSinopse.Multiline = true;
            txtSinopse.ScrollBars = ScrollBars.Vertical;
            txtSinopse.ReadOnly = true;
            txtSinopse.BackColor = Color.White;
        }

        // Método público para carregar os dados do livro
        public void CarregarLivro(int idLivro, int? clienteId = null)
        {
            livroId = idLivro;
            clienteIdLogado = clienteId;

            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    // Query completa com JOIN
                    string query = @"SELECT 
                                        L.TITULO,
                                        L.ISBN,
                                        L.SINOPSE,
                                        L.ANO_PUBLICACAO,
                                        L.PAGINAS,
                                        L.ACABAMENTO,
                                        L.PRECO,
                                        L.QUANTIDADE_ESTOQUE,
                                        L.CAPA,
                                        C.NOME_CATEGORIA,
                                        E.RAZAO_SOCIAL,
                                        E.ID_EDITORA
                                    FROM LIVROS L
                                    INNER JOIN CATEGORIAS C ON L.ID_CATEGORIA = C.ID_CATEGORIA
                                    INNER JOIN EDITORA E ON L.ID_EDITORA = E.ID_EDITORA
                                    WHERE L.ID_LIVRO = @LivroId";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@LivroId", idLivro);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Preencher campos
                            lblTitulo.Text = reader["TITULO"].ToString();
                            lblAutores.Text = "Autor(es): " + CarregarAutores(idLivro);
                            lblEditora.Text = "Editora: " + reader["RAZAO_SOCIAL"].ToString();
                            lblCategoria.Text = "Categoria: " + reader["NOME_CATEGORIA"].ToString();

                            lblISBN.Text = "ISBN: " + (reader["ISBN"] != DBNull.Value ? reader["ISBN"].ToString() : "Não informado");

                            string ano = reader["ANO_PUBLICACAO"] != DBNull.Value ? reader["ANO_PUBLICACAO"].ToString() : "N/A";
                            string paginas = reader["PAGINAS"] != DBNull.Value ? reader["PAGINAS"].ToString() + " páginas" : "N/A";
                            string acabamento = reader["ACABAMENTO"] != DBNull.Value ? reader["ACABAMENTO"].ToString() : "N/A";
                            lblDetalhes.Text = $"Ano: {ano} | Páginas: {paginas} | {acabamento}";

                            txtSinopse.Text = reader["SINOPSE"] != DBNull.Value ? reader["SINOPSE"].ToString() : "Sem sinopse disponível.";

                            decimal preco = reader.GetDecimal(6);
                            lblPreco.Text = $"R$ {preco:F2}";

                            int estoque = reader.GetInt32(7);
                            lblEstoque.Text = $"Estoque: {estoque} unidade(s) disponível(eis)";
                            lblEstoque.ForeColor = estoque > 0 ? Color.Green : Color.Red;

                            // Carregar capa
                            if (reader["CAPA"] != DBNull.Value)
                            {
                                byte[] capaBytes = (byte[])reader["CAPA"];
                                picCapa.Image = ConverterBytesParaImagem(capaBytes);
                            }

                            // Desabilitar botão se não tiver estoque
                            btnAdicionarCarrinho.Enabled = estoque > 0;
                            if (estoque == 0)
                            {
                                btnAdicionarCarrinho.Text = "Sem Estoque";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Livro não encontrado!", "Erro",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar detalhes do livro: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // Carregar autores do livro (da tabela LIVRO_AUTOR)
        private string CarregarAutores(int idLivro)
        {
            try
            {
                StringBuilder autores = new StringBuilder();

                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT A.NOME_AUTOR
                                    FROM LIVRO_AUTOR LA
                                    INNER JOIN AUTORES A ON LA.ID_AUTOR = A.ID_AUTOR
                                    WHERE LA.ID_LIVRO = @LivroId";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@LivroId", idLivro);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (autores.Length > 0)
                                autores.Append(", ");

                            autores.Append(reader["NOME_AUTOR"].ToString());
                        }
                    }
                }

                return autores.Length > 0 ? autores.ToString() : "Autor desconhecido";
            }
            catch
            {
                return "Erro ao carregar autores";
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

        // Botão ADICIONAR AO CARRINHO
        private void btnAdicionarCarrinho_Click(object sender, EventArgs e)
        {
            // Verificar se o cliente está logado
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
                    this.Close();
                }
                return;
            }

            try
            {
                // Criar item do carrinho
                ItemCarrinho item = new ItemCarrinho(
                    livroId,
                    lblTitulo.Text,
                    precoLivro,
                    1, // Quantidade = 1 por padrão
                    editoraId,
                    nomeEditora,
                    capaBytes
                );

                // Adicionar ao carrinho
                GerenciadorCarrinho.AdicionarItem(item);

                // Mostrar confirmação
                MessageBox.Show(
                    $"'{lblTitulo.Text}' adicionado ao carrinho!\n\nTotal de itens: {GerenciadorCarrinho.ContarItens()}",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar ao carrinho: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão VOLTAR
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDetalhesLivro_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Não precisa fazer nada especial, só fecha a janela
        }

        private void FormDetalhesLivro_Load(object sender, EventArgs e)
        {

        }
    }
}