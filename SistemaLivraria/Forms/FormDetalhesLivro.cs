using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SistemaLivraria.Database;
using SistemaLivraria.Models; // Importar Models!

namespace SistemaLivraria.Forms
{
    public partial class FormDetalhesLivro : Form
    {
        // ✅ VARIÁVEIS DA CLASSE - DECLARADAS NO TOPO
        private int livroId;
        private int? clienteIdLogado;
        private decimal precoLivro = 0;
        private int editoraId = 0;
        private string nomeEditora = "";
        private byte[] capaBytes = null;
        private string tituloLivro = "";

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

            // Configurar TextBox da sinopse - NÃO INTERAGÍVEL
            txtSinopse.Multiline = true;
            txtSinopse.ScrollBars = ScrollBars.Vertical;
            txtSinopse.ReadOnly = true;
            txtSinopse.BackColor = Color.White;
            txtSinopse.Cursor = Cursors.Arrow; // Mudar cursor
            txtSinopse.TabStop = false; // Não permitir focar com TAB
            // Remover foco ao clicar
            txtSinopse.GotFocus += (s, e) => { this.ActiveControl = null; };
        }

        public void CarregarLivro(int idLivro, int? clienteId = null)
        {
            livroId = idLivro;
            clienteIdLogado = clienteId;

            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
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
                            // ✅ GUARDAR nas variáveis da classe
                            tituloLivro = reader["TITULO"].ToString();
                            nomeEditora = reader["RAZAO_SOCIAL"].ToString();
                            editoraId = reader.GetInt32(11); // Índice 11 = ID_EDITORA
                            precoLivro = reader.GetDecimal(6); // Índice 6 = PRECO

                            // Preencher labels
                            lblTitulo.Text = tituloLivro;
                            lblAutores.Text = "Autor(es): " + CarregarAutores(idLivro);
                            lblEditora.Text = "Editora: " + nomeEditora;
                            lblCategoria.Text = "Categoria: " + reader["NOME_CATEGORIA"].ToString();

                            lblISBN.Text = "ISBN: " + (reader["ISBN"] != DBNull.Value ? reader["ISBN"].ToString() : "Não informado");

                            string ano = reader["ANO_PUBLICACAO"] != DBNull.Value ? reader["ANO_PUBLICACAO"].ToString() : "N/A";
                            string paginas = reader["PAGINAS"] != DBNull.Value ? reader["PAGINAS"].ToString() + " páginas" : "N/A";
                            string acabamento = reader["ACABAMENTO"] != DBNull.Value ? reader["ACABAMENTO"].ToString() : "N/A";
                            lblDetalhes.Text = $"Ano: {ano} | Páginas: {paginas} | {acabamento}";

                            txtSinopse.Text = reader["SINOPSE"] != DBNull.Value ? reader["SINOPSE"].ToString() : "Sem sinopse disponível.";

                            lblPreco.Text = $"R$ {precoLivro:F2}";

                            int estoque = reader.GetInt32(7);
                            lblEstoque.Text = $"Estoque: {estoque} unidade(s) disponível(eis)";
                            lblEstoque.ForeColor = estoque > 0 ? Color.Green : Color.Red;

                            // ✅ Carregar e GUARDAR capa
                            if (reader["CAPA"] != DBNull.Value)
                            {
                                capaBytes = (byte[])reader["CAPA"];
                                picCapa.Image = ConverterBytesParaImagem(capaBytes);
                            }
                            else
                            {
                                capaBytes = null;
                                picCapa.Image = null; // Limpar imagem se não houver
                            }

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

        // Versão segura para evitar erro GDI+ (Generic error occurred in GDI+)
        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            try
            {
                if (bytes == null || bytes.Length == 0)
                    return null;

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    // Criar imagem a partir do stream
                    Image imagemOriginal = Image.FromStream(ms);

                    // Criar uma CÓPIA da imagem. Isso libera o MemoryStream.
                    Image imagemCopia = new Bitmap(imagemOriginal);

                    // Descartar a imagem original (que estava presa ao stream)
                    imagemOriginal.Dispose();

                    return imagemCopia;
                }
            }
            catch (Exception ex)
            {
                // Logar o erro se necessário, mas retornar null
                Console.WriteLine("Erro ao converter bytes para imagem: " + ex.Message);
                return null;
            }
        }

        // ===== BOTÃO ADICIONAR AO CARRINHO =====
        private void btnAdicionarCarrinho_Click(object sender, EventArgs e)
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
                    // ACHO que você quer fechar este e abrir o FormMenu
                    // Se o FormMenu for seu formulário de Login, isso está ok.
                    FormMenu formMenu = new FormMenu();
                    formMenu.Show();
                    this.Close();
                }
                return;
            }

            try
            {
                // ✅ Verificar se os valores da classe estão corretos
                if (precoLivro == 0) // Uma verificação simples
                {
                    MessageBox.Show("Erro: Preço do livro não foi carregado corretamente! Tente novamente.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(tituloLivro))
                {
                    MessageBox.Show("Erro: Título do livro não foi carregado. Tente novamente.", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Criar item do carrinho USANDO AS VARIÁVEIS DA CLASSE
                ItemCarrinho item = new ItemCarrinho(
                    livroId,
                    tituloLivro,
                    precoLivro,
                    1,
                    editoraId,
                    nomeEditora,
                    capaBytes
                );

                // Adicionar ao carrinho
                GerenciadorCarrinho.AdicionarItem(item);

                MessageBox.Show(
                    $"'{tituloLivro}' adicionado ao carrinho!\n\nTotal de itens: {GerenciadorCarrinho.ContarItens()}",
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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}