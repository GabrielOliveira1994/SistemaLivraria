using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormCadastroLivro : Form
    {
        private int editoraId; // ID da editora logada
        private byte[] imagemCapa = null; // Capa do livro
        private List<string> autores = new List<string>(); // Lista de autores

        public FormCadastroLivro()
        {
            InitializeComponent();
            ConfigurarComboBoxes();
        }

        // Método para definir qual editora está cadastrando
        public void DefinirEditora(int id)
        {
            editoraId = id;
        }

        // Configurar os ComboBoxes
        private void ConfigurarComboBoxes()
        {
            // ComboBox de Acabamento (dados fixos)
            cmbAcabamento.Items.Add("Capa Dura");
            cmbAcabamento.Items.Add("Brochura");
            cmbAcabamento.Items.Add("Espiral");
            cmbAcabamento.SelectedIndex = 0; // Seleciona o primeiro

            // ComboBox de Categoria (carrega do banco)
            CarregarCategorias();
        }

        // Carregar categorias do banco de dados
        private void CarregarCategorias()
        {
            try
            {
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
                                Value = reader.GetInt32(0), // ID_CATEGORIA
                                Text = reader.GetString(1)  // NOME_CATEGORIA
                            });
                        }
                    }
                }

                cmbCategoria.DisplayMember = "Text";
                cmbCategoria.ValueMember = "Value";

                if (cmbCategoria.Items.Count > 0)
                    cmbCategoria.SelectedIndex = 0;
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

        // ===== BOTÃO SELECIONAR CAPA =====
        private void btnSelecionarCapa_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
            openFile.Title = "Selecione a capa do livro";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picCapa.Image = Image.FromFile(openFile.FileName);
                    imagemCapa = ConverterImagemParaBytes(openFile.FileName);

                    MessageBox.Show("Capa carregada com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar capa: " + ex.Message, "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private byte[] ConverterImagemParaBytes(string caminhoImagem)
        {
            using (FileStream fs = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                return bytes;
            }
        }

        // ===== ADICIONAR AUTOR =====
        private void btnAdicionarAutor_Click(object sender, EventArgs e)
        {
            string nomeAutor = txtAutor.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeAutor))
            {
                MessageBox.Show("Digite o nome do autor!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAutor.Focus();
                return;
            }

            // Adiciona na lista
            autores.Add(nomeAutor);
            lstAutores.Items.Add(nomeAutor);

            // Limpa o campo
            txtAutor.Clear();
            txtAutor.Focus();
        }

        // ===== BOTÃO SALVAR =====
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            if (SalvarLivro())
            {
                MessageBox.Show("Livro cadastrado com sucesso!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        // Validar campos
        private bool ValidarCampos()
        {
            // Validar campos obrigatórios
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Digite o título do livro!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return false;
            }

            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma categoria!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                MessageBox.Show("Digite o preço!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEstoque.Text))
            {
                MessageBox.Show("Digite a quantidade em estoque!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstoque.Focus();
                return false;
            }

            // Validar preço
            decimal preco;
            if (!decimal.TryParse(txtPreco.Text, out preco) || preco <= 0)
            {
                MessageBox.Show("Preço inválido!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                return false;
            }

            // Validar estoque
            int estoque;
            if (!int.TryParse(txtEstoque.Text, out estoque) || estoque < 0)
            {
                MessageBox.Show("Quantidade em estoque inválida!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstoque.Focus();
                return false;
            }

            // Validar ano (se preenchido)
            if (!string.IsNullOrWhiteSpace(txtAnoPublicacao.Text))
            {
                int ano;
                if (!int.TryParse(txtAnoPublicacao.Text, out ano) || ano < 1000 || ano > DateTime.Now.Year + 1)
                {
                    MessageBox.Show("Ano de publicação inválido!", "Atenção",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAnoPublicacao.Focus();
                    return false;
                }
            }

            // Validar páginas (se preenchido)
            if (!string.IsNullOrWhiteSpace(txtPaginas.Text))
            {
                int paginas;
                if (!int.TryParse(txtPaginas.Text, out paginas) || paginas <= 0)
                {
                    MessageBox.Show("Número de páginas inválido!", "Atenção",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPaginas.Focus();
                    return false;
                }
            }

            return true;
        }

        // Salvar livro no banco
        private bool SalvarLivro()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query;

                    // Modo EDIÇÃO (UPDATE)
                    if (livroId.HasValue)
                    {
                        query = @"UPDATE LIVROS SET 
                            TITULO = @Titulo,
                            ISBN = @ISBN,
                            SINOPSE = @Sinopse,
                            ANO_PUBLICACAO = @AnoPublicacao,
                            ID_CATEGORIA = @IdCategoria,
                            PAGINAS = @Paginas,
                            ACABAMENTO = @Acabamento,
                            PRECO = @Preco,
                            QUANTIDADE_ESTOQUE = @QuantidadeEstoque,
                            CAPA = @Capa
                         WHERE ID_LIVRO = @IdLivro";
                    }
                    // Modo CADASTRO (INSERT)
                    else
                    {
                        query = @"INSERT INTO LIVROS 
                            (ID_EDITORA, TITULO, ISBN, SINOPSE, ANO_PUBLICACAO, 
                             ID_CATEGORIA, PAGINAS, ACABAMENTO, PRECO, 
                             QUANTIDADE_ESTOQUE, CAPA) 
                            OUTPUT INSERTED.ID_LIVRO
                            VALUES 
                            (@IdEditora, @Titulo, @ISBN, @Sinopse, @AnoPublicacao, 
                             @IdCategoria, @Paginas, @Acabamento, @Preco, 
                             @QuantidadeEstoque, @Capa)";
                    }

                    SqlCommand cmd = new SqlCommand(query, conexao);

                    // Parâmetros comuns
                    cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text ?? "");
                    cmd.Parameters.AddWithValue("@Sinopse", txtSinopse.Text ?? "");

                    if (string.IsNullOrWhiteSpace(txtAnoPublicacao.Text))
                        cmd.Parameters.AddWithValue("@AnoPublicacao", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@AnoPublicacao", int.Parse(txtAnoPublicacao.Text));

                    cmd.Parameters.AddWithValue("@IdCategoria", ((ItemComboBox)cmbCategoria.SelectedItem).Value);

                    if (string.IsNullOrWhiteSpace(txtPaginas.Text))
                        cmd.Parameters.AddWithValue("@Paginas", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Paginas", int.Parse(txtPaginas.Text));

                    cmd.Parameters.AddWithValue("@Acabamento", cmbAcabamento.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Preco", decimal.Parse(txtPreco.Text));
                    cmd.Parameters.AddWithValue("@QuantidadeEstoque", int.Parse(txtEstoque.Text));
                    cmd.Parameters.AddWithValue("@Capa", (object)imagemCapa ?? DBNull.Value);

                    int idLivroAtual;

                    // Modo EDIÇÃO
                    if (livroId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@IdLivro", livroId.Value);
                        cmd.ExecuteNonQuery();
                        idLivroAtual = livroId.Value;

                        // Deletar autores antigos
                        string deletarAutores = "DELETE FROM LIVRO_AUTOR WHERE ID_LIVRO = @IdLivro";
                        SqlCommand cmdDeletar = new SqlCommand(deletarAutores, conexao);
                        cmdDeletar.Parameters.AddWithValue("@IdLivro", livroId.Value);
                        cmdDeletar.ExecuteNonQuery();
                    }
                    // Modo CADASTRO
                    else
                    {
                        cmd.Parameters.AddWithValue("@IdEditora", editoraId);
                        idLivroAtual = (int)cmd.ExecuteScalar();
                    }

                    // Inserir autores (novos ou atualizados)
                    if (autores.Count > 0)
                    {
                        SalvarAutores(conexao, idLivroAtual);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar livro: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Salvar autores (simplificado - cria autores se não existirem)
        private void SalvarAutores(SqlConnection conexao, int livroId)
        {
            foreach (string nomeAutor in autores)
            {
                // Verifica se autor já existe
                string queryBuscar = "SELECT ID_AUTOR FROM AUTORES WHERE NOME_AUTOR = @Nome";
                SqlCommand cmdBuscar = new SqlCommand(queryBuscar, conexao);
                cmdBuscar.Parameters.AddWithValue("@Nome", nomeAutor);

                object resultado = cmdBuscar.ExecuteScalar();
                int autorId;

                if (resultado == null)
                {
                    // Autor não existe, cria
                    string queryInserir = "INSERT INTO AUTORES (NOME_AUTOR) OUTPUT INSERTED.ID_AUTOR VALUES (@Nome)";
                    SqlCommand cmdInserir = new SqlCommand(queryInserir, conexao);
                    cmdInserir.Parameters.AddWithValue("@Nome", nomeAutor);
                    autorId = (int)cmdInserir.ExecuteScalar();
                }
                else
                {
                    // Autor já existe
                    autorId = (int)resultado;
                }

                // Relaciona livro com autor
                string queryRelacao = "INSERT INTO LIVRO_AUTOR (ID_LIVRO, ID_AUTOR) VALUES (@IdLivro, @IdAutor)";
                SqlCommand cmdRelacao = new SqlCommand(queryRelacao, conexao);
                cmdRelacao.Parameters.AddWithValue("@IdLivro", livroId);
                cmdRelacao.Parameters.AddWithValue("@IdAutor", autorId);
                cmdRelacao.ExecuteNonQuery();
            }
        }

        // ===== BOTÃO CANCELAR =====
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCadastroLivro_Load(object sender, EventArgs e)
        {

        }
        private int? livroId = null; // Null = modo cadastro, valor = modo edição

        // Método público para carregar livro existente (modo edição)
        public void CarregarLivro(int id)
        {
            livroId = id;
            this.Text = "Editar Livro";
            btnSalvar.Text = "Atualizar";

            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    // Buscar dados do livro
                    string query = @"SELECT 
                                TITULO, ISBN, SINOPSE, ANO_PUBLICACAO, 
                                ID_CATEGORIA, PAGINAS, ACABAMENTO, PRECO, 
                                QUANTIDADE_ESTOQUE, CAPA 
                            FROM LIVROS 
                            WHERE ID_LIVRO = @Id";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Preencher campos
                            txtTitulo.Text = reader["TITULO"].ToString();
                            txtISBN.Text = reader["ISBN"].ToString();
                            txtSinopse.Text = reader["SINOPSE"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("ANO_PUBLICACAO")))
                                txtAnoPublicacao.Text = reader["ANO_PUBLICACAO"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("PAGINAS")))
                                txtPaginas.Text = reader["PAGINAS"].ToString();

                            txtPreco.Text = reader["PRECO"].ToString();
                            txtEstoque.Text = reader["QUANTIDADE_ESTOQUE"].ToString();

                            // Selecionar categoria
                            int categoriaId = reader.GetInt32(reader.GetOrdinal("ID_CATEGORIA"));
                            for (int i = 0; i < cmbCategoria.Items.Count; i++)
                            {
                                ItemComboBox item = (ItemComboBox)cmbCategoria.Items[i];
                                if (item.Value == categoriaId)
                                {
                                    cmbCategoria.SelectedIndex = i;
                                    break;
                                }
                            }

                            // Selecionar acabamento
                            string acabamento = reader["ACABAMENTO"].ToString();
                            cmbAcabamento.SelectedItem = acabamento;

                            // Carregar capa
                            if (!reader.IsDBNull(reader.GetOrdinal("CAPA")))
                            {
                                byte[] capaBytes = (byte[])reader["CAPA"];
                                imagemCapa = capaBytes;
                                picCapa.Image = ConverterBytesParaImagem(capaBytes);
                            }
                        }
                    }

                    // Carregar autores do livro
                    CarregarAutoresDoLivro(conexao, id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar livro: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarAutoresDoLivro(SqlConnection conexao, int idLivro)
        {
            string query = @"SELECT A.NOME_AUTOR 
                     FROM AUTORES A
                     INNER JOIN LIVRO_AUTOR LA ON A.ID_AUTOR = LA.ID_AUTOR
                     WHERE LA.ID_LIVRO = @IdLivro";

            SqlCommand cmd = new SqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@IdLivro", idLivro);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string nomeAutor = reader["NOME_AUTOR"].ToString();
                    autores.Add(nomeAutor);
                    lstAutores.Items.Add(nomeAutor);
                }
            }
        }

        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}