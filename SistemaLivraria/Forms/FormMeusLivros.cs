using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaLivraria.Database;

namespace SistemaLivraria.Forms
{
    public partial class FormMeusLivros : Form
    {
        private int editoraId; // ID da editora logada

        public FormMeusLivros()
        {
            InitializeComponent();
            ConfigurarLayout();
            ConfigurarDataGridView();
        }
        private void ConfigurarLayout()
        {
            // Configurar Panel de Botões
            panelBotoes.Dock = DockStyle.Top;
            panelBotoes.Height = 60;

            // Configurar DataGridView
            dgvLivros.Dock = DockStyle.Fill;  // Preenche o resto da tela
            dgvLivros.BringToFront();         // Traz para frente

            // Garantir que o panel está atrás
            panelBotoes.SendToBack();
        }

        // Método para definir qual editora está vendo os livros
        public void DefinirEditora(int id)
        {
            editoraId = id;
            CarregarLivros(); // Carrega os livros automaticamente
        }

        // Configurar o DataGridView
        private void ConfigurarDataGridView()
        {
            dgvLivros.AutoGenerateColumns = false;
            dgvLivros.AllowUserToAddRows = false;
            dgvLivros.ReadOnly = true;
            dgvLivros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLivros.MultiSelect = false;

            // Definir colunas manualmente
            dgvLivros.Columns.Clear();

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID_LIVRO",
                HeaderText = "ID",
                DataPropertyName = "ID_LIVRO",
                Width = 50
            });

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TITULO",
                HeaderText = "Título",
                DataPropertyName = "TITULO",
                Width = 250
            });

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CATEGORIA",
                HeaderText = "Categoria",
                DataPropertyName = "CATEGORIA",
                Width = 120
            });

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ANO_PUBLICACAO",
                HeaderText = "Ano",
                DataPropertyName = "ANO_PUBLICACAO",
                Width = 60
            });

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PRECO",
                HeaderText = "Preço",
                DataPropertyName = "PRECO",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } // Formato moeda
            });

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QUANTIDADE_ESTOQUE",
                HeaderText = "Estoque",
                DataPropertyName = "QUANTIDADE_ESTOQUE",
                Width = 80
            });

            dgvLivros.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ATIVO",
                HeaderText = "Status",
                DataPropertyName = "ATIVO",
                Width = 80
            });
        }

        // ===== CARREGAR LIVROS DO BANCO =====
        private void CarregarLivros()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT 
                                L.ID_LIVRO,
                                L.TITULO,
                                C.NOME_CATEGORIA AS CATEGORIA,
                                L.ANO_PUBLICACAO,
                                L.PRECO,
                                L.QUANTIDADE_ESTOQUE,
                                CASE WHEN L.ATIVO = 1 THEN 'Ativo' ELSE 'Inativo' END AS ATIVO
                            FROM LIVROS L
                            INNER JOIN CATEGORIAS C ON L.ID_CATEGORIA = C.ID_CATEGORIA
                            WHERE L.ID_EDITORA = @IdEditora
                            ORDER BY L.TITULO";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@IdEditora", editoraId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvLivros.DataSource = dt;

                    // Atualizar label com total de livros (se tiver)
                    this.Text = $"Meus Livros ({dt.Rows.Count} cadastrados)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar livros: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== BOTÃO ATUALIZAR =====
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarLivros();
            MessageBox.Show("Lista atualizada!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===== BOTÃO EDITAR =====
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para editar!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int livroId = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["ID_LIVRO"].Value);

            // Abre FormCadastroLivro em modo de edição
            FormCadastroLivro formEdicao = new FormCadastroLivro();
            formEdicao.DefinirEditora(editoraId);
            formEdicao.CarregarLivro(livroId); // ← Vamos criar esse método
            formEdicao.ShowDialog();

            // Recarrega a lista após fechar
            CarregarLivros();
        }

        // ===== BOTÃO EXCLUIR =====
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para excluir!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int livroId = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["ID_LIVRO"].Value);
            string titulo = dgvLivros.SelectedRows[0].Cells["TITULO"].Value.ToString();

            DialogResult resultado = MessageBox.Show(
                $"Deseja realmente excluir o livro '{titulo}'?\n\nEsta ação não pode ser desfeita!",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                if (ExcluirLivro(livroId))
                {
                    MessageBox.Show("Livro excluído com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarLivros(); // Recarrega a lista
                }
            }
        }

        // Método para excluir livro
        private bool ExcluirLivro(int livroId)
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    // Primeiro, deletar relações com autores
                    string queryAutores = "DELETE FROM LIVRO_AUTOR WHERE ID_LIVRO = @IdLivro";
                    SqlCommand cmdAutores = new SqlCommand(queryAutores, conexao);
                    cmdAutores.Parameters.AddWithValue("@IdLivro", livroId);
                    cmdAutores.ExecuteNonQuery();

                    // Depois, deletar o livro
                    string queryLivro = "DELETE FROM LIVROS WHERE ID_LIVRO = @IdLivro";
                    SqlCommand cmdLivro = new SqlCommand(queryLivro, conexao);
                    cmdLivro.Parameters.AddWithValue("@IdLivro", livroId);
                    cmdLivro.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir livro: " + ex.Message, "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ===== BOTÃO FECHAR =====
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Duplo clique na linha = Editar
        private void dgvLivros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica se clicou em uma linha válida
            {
                btnEditar_Click(sender, e); // Chama o método de editar
            }
        }

        private void panelBotoes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvLivros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}