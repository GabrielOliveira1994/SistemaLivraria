using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormGerenciarAutores : Form
    {
        private int? autorIdSelecionado = null; // Null = modo cadastro, ID = modo edição
        private byte[] imagemFoto = null;

        public FormGerenciarAutores()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            LimparCampos();
            CarregarAutoresGrid();
        }

        // 1. Configurar o DataGridView
        private void ConfigurarDataGridView()
        {
            dgvAutores.AutoGenerateColumns = false;
            dgvAutores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAutores.MultiSelect = false;
            dgvAutores.AllowUserToAddRows = false;

            dgvAutores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID_AUTOR",
                HeaderText = "ID",
                DataPropertyName = "ID_AUTOR",
                Width = 50
            });

            dgvAutores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NOME_AUTOR",
                HeaderText = "Nome do Autor",
                DataPropertyName = "NOME_AUTOR",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        // 2. Carregar todos os autores no DataGridView
        private void CarregarAutoresGrid()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = "SELECT ID_AUTOR, NOME_AUTOR FROM AUTORES ORDER BY NOME_AUTOR";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);
                    dgvAutores.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar autores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Limpar campos para "Modo Cadastro"
        private void LimparCampos()
        {
            autorIdSelecionado = null;
            txtNomeAutor.Clear();
            txtNacionalidade.Clear();
            dtpNascimento.Value = DateTime.Now;
            txtBiografia.Clear();
            picFoto.Image = null;
            imagemFoto = null;

            btnSalvar.Text = "Salvar Novo";
            btnExcluir.Enabled = false;
            txtNomeAutor.Focus();
        }

        // 4. Carregar dados do autor selecionado
        private void CarregarDadosAutor(int idAutor)
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT NOME_AUTOR, DATA_NASCIMENTO, NACIONALIDADE, BIOGRAFIA, FOTO 
                                    FROM AUTORES 
                                    WHERE ID_AUTOR = @Id";
                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Id", idAutor);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            autorIdSelecionado = idAutor;
                            txtNomeAutor.Text = reader["NOME_AUTOR"].ToString();

                            if (reader["NACIONALIDADE"] != DBNull.Value)
                                txtNacionalidade.Text = reader["NACIONALIDADE"].ToString();

                            if (reader["BIOGRAFIA"] != DBNull.Value)
                                txtBiografia.Text = reader["BIOGRAFIA"].ToString();

                            if (reader["DATA_NASCIMENTO"] != DBNull.Value)
                                dtpNascimento.Value = (DateTime)reader["DATA_NASCIMENTO"];
                            else
                                dtpNascimento.Value = DateTime.Now; // Ou um valor padrão

                            if (reader["FOTO"] != DBNull.Value)
                            {
                                imagemFoto = (byte[])reader["FOTO"];
                                picFoto.Image = ConverterBytesParaImagem(imagemFoto);
                            }
                            else
                            {
                                imagemFoto = null;
                                picFoto.Image = null;
                            }

                            btnSalvar.Text = "Atualizar";
                            btnExcluir.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do autor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== BOTÕES DE AÇÃO =====

        // Botão NOVO / LIMPAR
        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // Botão SELECIONAR FOTO
        private void btnSelecionarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
            openFile.Title = "Selecione a foto do autor";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picFoto.Image = Image.FromFile(openFile.FileName);
                    // Usaremos a sugestão de File.ReadAllBytes aqui
                    imagemFoto = File.ReadAllBytes(openFile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar foto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Botão SALVAR (INSERT ou UPDATE)
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomeAutor.Text))
            {
                MessageBox.Show("O nome do autor é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query;
                    // MODO UPDATE
                    if (autorIdSelecionado.HasValue)
                    {
                        query = @"UPDATE AUTORES SET 
                                    NOME_AUTOR = @Nome, 
                                    DATA_NASCIMENTO = @Data, 
                                    NACIONALIDADE = @Nacionalidade, 
                                    BIOGRAFIA = @Biografia, 
                                    FOTO = @Foto 
                                  WHERE ID_AUTOR = @Id";
                    }
                    // MODO INSERT
                    else
                    {
                        query = @"INSERT INTO AUTORES (NOME_AUTOR, DATA_NASCIMENTO, NACIONALIDADE, BIOGRAFIA, FOTO) 
                                  VALUES (@Nome, @Data, @Nacionalidade, @Biografia, @Foto)";
                    }

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Nome", txtNomeAutor.Text);

                    // Tratar valores que podem ser nulos
                    cmd.Parameters.AddWithValue("@Data", (object)dtpNascimento.Value ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Nacionalidade", string.IsNullOrWhiteSpace(txtNacionalidade.Text) ? DBNull.Value : (object)txtNacionalidade.Text);
                    cmd.Parameters.AddWithValue("@Biografia", string.IsNullOrWhiteSpace(txtBiografia.Text) ? DBNull.Value : (object)txtBiografia.Text);
                    cmd.Parameters.AddWithValue("@Foto", (object)imagemFoto ?? DBNull.Value);

                    if (autorIdSelecionado.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@Id", autorIdSelecionado.Value);
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Autor salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregarAutoresGrid(); // Atualiza a lista
                    LimparCampos();      // Limpa os campos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar autor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão EXCLUIR
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!autorIdSelecionado.HasValue) return;

            // 1. Verificar se o autor está vinculado a algum livro
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string queryCheck = "SELECT COUNT(*) FROM LIVRO_AUTOR WHERE ID_AUTOR = @Id";
                    SqlCommand cmdCheck = new SqlCommand(queryCheck, conexao);
                    cmdCheck.Parameters.AddWithValue("@Id", autorIdSelecionado.Value);

                    int vinculos = (int)cmdCheck.ExecuteScalar();
                    if (vinculos > 0)
                    {
                        MessageBox.Show($"Não é possível excluir este autor, pois ele está vinculado a {vinculos} livro(s).", "Ação Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar vínculos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Se não houver vínculos, confirmar e excluir
            DialogResult confirm = MessageBox.Show("Deseja realmente excluir este autor? Esta ação é irreversível.", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexao = Conexao.ObterConexao())
                    {
                        string queryDelete = "DELETE FROM AUTORES WHERE ID_AUTOR = @Id";
                        SqlCommand cmdDelete = new SqlCommand(queryDelete, conexao);
                        cmdDelete.Parameters.AddWithValue("@Id", autorIdSelecionado.Value);
                        cmdDelete.ExecuteNonQuery();

                        MessageBox.Show("Autor excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CarregarAutoresGrid();
                        LimparCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir autor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ===== EVENTOS DO DATAGRIDVIEW =====
        private void dgvAutores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvAutores.SelectedRows[0].Cells["ID_AUTOR"].Value);
                CarregarDadosAutor(id);
            }
        }

        // ===== HELPER FUNCTIONS =====
        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            try
            {
                if (bytes == null || bytes.Length == 0) return null;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image imagemOriginal = Image.FromStream(ms);
                    Image imagemCopia = new Bitmap(imagemOriginal);
                    imagemOriginal.Dispose();
                    return imagemCopia;
                }
            }
            catch { return null; }
        }
    }
}