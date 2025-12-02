using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SistemaLivraria.Models;

namespace SistemaLivraria.Forms
{
    public partial class FormCarrinho : Form
    {
        private int? clienteIdLogado;
        private string nomeCliente;

        public FormCarrinho()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            ConfigurarLayout();
        }

        // Método para definir qual cliente está vendo o carrinho
        public void DefinirCliente(int id, string nome)
        {
            clienteIdLogado = id;
            nomeCliente = nome;
            CarregarCarrinho();
        }

        private void ConfigurarLayout()
        {
            // Configurar label do total
            lblTotal.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTotal.ForeColor = Color.Green;
            lblTotal.AutoSize = true;
        }

        // Configurar DataGridView
        private void ConfigurarDataGridView()
        {
            dgvCarrinho.AutoGenerateColumns = false;
            dgvCarrinho.AllowUserToAddRows = false;
            dgvCarrinho.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrinho.MultiSelect = false;

            // Limpar colunas
            dgvCarrinho.Columns.Clear();

            // Coluna da CAPA (Imagem)
            DataGridViewImageColumn colCapa = new DataGridViewImageColumn();
            colCapa.Name = "Capa";
            colCapa.HeaderText = "Capa";
            colCapa.Width = 80;
            colCapa.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvCarrinho.Columns.Add(colCapa);

            // Coluna TÍTULO
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                HeaderText = "Título",
                Width = 250,
                ReadOnly = true
            });

            // Coluna EDITORA
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Editora",
                HeaderText = "Editora",
                Width = 150,
                ReadOnly = true
            });

            // Coluna PREÇO UNITÁRIO
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecoUnitario",
                HeaderText = "Preço Unit.",
                Width = 80,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Coluna QUANTIDADE (editável!)
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantidade",
                HeaderText = "Qtd",
                Width = 60,
                ReadOnly = false // ← Permite editar!
            });

            // Coluna SUBTOTAL
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Coluna REMOVER (botão)
            DataGridViewButtonColumn colRemover = new DataGridViewButtonColumn();
            colRemover.Name = "Remover";
            colRemover.HeaderText = "";
            colRemover.Text = "🗑️ Remover";
            colRemover.UseColumnTextForButtonValue = true;
            colRemover.Width = 100;
            dgvCarrinho.Columns.Add(colRemover);

            // Coluna oculta com ID do livro
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LivroId",
                Visible = false
            });

            // Ajustar altura das linhas para caber as imagens
            dgvCarrinho.RowTemplate.Height = 90;
        }

        // ===== CARREGAR CARRINHO =====
        // ===== CARREGAR CARRINHO =====
        private void CarregarCarrinho()
        {
            // ✅ DESABILITA o evento temporariamente
            dgvCarrinho.CellValueChanged -= dgvCarrinho_CellValueChanged;

            dgvCarrinho.Rows.Clear();

            var itens = GerenciadorCarrinho.ObterItens();

            if (itens.Count == 0)
            {
                MessageBox.Show("Seu carrinho está vazio!", "Carrinho Vazio",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarTotal();

                // ✅ REABILITA o evento antes de sair
                dgvCarrinho.CellValueChanged += dgvCarrinho_CellValueChanged;
                return;
            }

            foreach (var item in itens)
            {
                // Converter capa para Image
                Image imagemCapa = null;
                if (item.Capa != null && item.Capa.Length > 0)
                {
                    imagemCapa = ConverterBytesParaImagem(item.Capa);
                }

                // Adicionar linha
                int index = dgvCarrinho.Rows.Add();

                DataGridViewRow row = dgvCarrinho.Rows[index];

                row.Cells["Capa"].Value = imagemCapa;
                row.Cells["Titulo"].Value = item.Titulo;
                row.Cells["Editora"].Value = item.NomeEditora;
                row.Cells["PrecoUnitario"].Value = item.PrecoUnitario;
                row.Cells["Quantidade"].Value = item.Quantidade;
                row.Cells["Subtotal"].Value = item.Subtotal;
                row.Cells["LivroId"].Value = item.LivroId;
            }

            AtualizarTotal();

            // ✅ REABILITA o evento depois de carregar tudo
            dgvCarrinho.CellValueChanged += dgvCarrinho_CellValueChanged;
        }

        private Image ConverterBytesParaImagem(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        // Atualizar total
        private void AtualizarTotal()
        {
            decimal total = GerenciadorCarrinho.ObterTotal();
            lblTotal.Text = $"Total: R$ {total:F2}";
        }

        // ===== EVENTOS =====

        // Quando clicar em um botão da grid (Remover)
        private void dgvCarrinho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se clicou na coluna "Remover"
            if (e.ColumnIndex == dgvCarrinho.Columns["Remover"].Index && e.RowIndex >= 0)
            {
                int livroId = Convert.ToInt32(dgvCarrinho.Rows[e.RowIndex].Cells["LivroId"].Value);
                string titulo = dgvCarrinho.Rows[e.RowIndex].Cells["Titulo"].Value.ToString();

                DialogResult resultado = MessageBox.Show(
                    $"Remover '{titulo}' do carrinho?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    GerenciadorCarrinho.RemoverItem(livroId);
                    CarregarCarrinho();
                }
            }
        }

        // Quando editar a quantidade
        private void dgvCarrinho_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCarrinho.Columns["Quantidade"].Index && e.RowIndex >= 0)
            {
                try
                {
                    int livroId = Convert.ToInt32(dgvCarrinho.Rows[e.RowIndex].Cells["LivroId"].Value);
                    int novaQuantidade = Convert.ToInt32(dgvCarrinho.Rows[e.RowIndex].Cells["Quantidade"].Value);

                    if (novaQuantidade <= 0)
                    {
                        MessageBox.Show("Quantidade deve ser maior que zero!", "Atenção",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CarregarCarrinho();
                        return;
                    }

                    GerenciadorCarrinho.AtualizarQuantidade(livroId, novaQuantidade);
                    CarregarCarrinho();
                }
                catch
                {
                    MessageBox.Show("Quantidade inválida!", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CarregarCarrinho();
                }
            }
        }

        // Botão CONTINUAR COMPRANDO
        private void btnContinuarComprando_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Botão LIMPAR CARRINHO
        private void btnLimparCarrinho_Click(object sender, EventArgs e)
        {
            if (GerenciadorCarrinho.ContarItens() == 0)
            {
                MessageBox.Show("Carrinho já está vazio!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "Deseja realmente limpar todo o carrinho?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                GerenciadorCarrinho.LimparCarrinho();
                CarregarCarrinho();
                MessageBox.Show("Carrinho limpo!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Botão FINALIZAR PEDIDO
        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (GerenciadorCarrinho.ContarItens() == 0)
            {
                MessageBox.Show("Seu carrinho está vazio!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Abre tela de finalização (Etapa 6.4)
            FormFinalizarPedido formFinalizar = new FormFinalizarPedido();
            formFinalizar.DefinirCliente(clienteIdLogado.Value, nomeCliente);
            formFinalizar.ShowDialog();

            // Recarrega carrinho (caso tenha finalizado)
            CarregarCarrinho();
        }
    }
}