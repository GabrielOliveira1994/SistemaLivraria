using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using SistemaLivraria.Database;
using SistemaLivraria.Models;

namespace SistemaLivraria.Forms
{
    public partial class FormFinalizarPedido : Form
    {
        private int? clienteIdLogado;
        private string nomeCliente;
        private decimal totalPedido = 0;

        public FormFinalizarPedido()
        {
            InitializeComponent();
        }

        // Método chamado para iniciar o formulário
        public void DefinirCliente(int id, string nome)
        {
            clienteIdLogado = id;
            nomeCliente = nome;

            // Carregar os dados na tela
            CarregarResumoPedido();
            CarregarEnderecoCliente();
        }

        // 1. Busca o total do GerenciadorCarrinho
        private void CarregarResumoPedido()
        {
            totalPedido = GerenciadorCarrinho.ObterTotal();
            int totalItens = GerenciadorCarrinho.ContarItens();

            // Atualiza as labels no formulário
            // (Você precisará ADICIONAR estas labels no designer)
            if (lblResumoItens != null)
                lblResumoItens.Text = $"Total de itens: {totalItens} unidade(s)";

            if (lblTotalPedido != null)
                lblTotalPedido.Text = $"Valor Total: R$ {totalPedido:F2}";

            // Desabilita o botão se o carrinho estiver vazio
            if (btnConfirmarPedido != null)
                btnConfirmarPedido.Enabled = (totalPedido > 0);
        }

        // 2. Busca o endereço do cliente no banco
        private void CarregarEnderecoCliente()
        {
            if (!clienteIdLogado.HasValue) return;

            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"SELECT LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO, CEP 
                                    FROM CLIENTE 
                                    WHERE IDCLIENTE = @Id";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Id", clienteIdLogado.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StringBuilder endereco = new StringBuilder();
                            endereco.AppendLine("Endereço de Entrega:");
                            endereco.AppendLine($"Rua/Av: {reader["LOGRADOURO"]}, {reader["NUMERO"]}");

                            if (reader["COMPLEMENTO"] != DBNull.Value && !string.IsNullOrEmpty(reader["COMPLEMENTO"].ToString()))
                            {
                                endereco.AppendLine($"Complemento: {reader["COMPLEMENTO"]}");
                            }

                            endereco.AppendLine($"Bairro: {reader["BAIRRO"]}");
                            endereco.AppendLine($"Cidade/Estado: {reader["CIDADE"]} - {reader["ESTADO"]}");
                            endereco.AppendLine($"CEP: {reader["CEP"]}");

                            // Atualiza a label de endereço
                            // (Você precisará ADICIONAR esta label no designer)
                            if (lblEnderecoCompleto != null)
                                lblEnderecoCompleto.Text = endereco.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar endereço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (lblEnderecoCompleto != null)
                    lblEnderecoCompleto.Text = "Não foi possível carregar o endereço.";
            }
        }

        // 3. O Botão CANCELAR (Voltar)
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 4. O Botão CONFIRMAR PEDIDO (A LÓGICA PRINCIPAL)
        private void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            if (!clienteIdLogado.HasValue)
            {
                MessageBox.Show("Erro: Cliente não identificado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (GerenciadorCarrinho.ContarItens() == 0)
            {
                MessageBox.Show("Seu carrinho está vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmação final
            DialogResult confirm = MessageBox.Show(
                $"Confirmar pedido no valor de R$ {totalPedido:F2}?",
                "Finalizar Pedido",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return;
            }

            // Desabilitar botão para evitar clique duplo
            btnConfirmarPedido.Enabled = false;

            // ===== INÍCIO DA TRANSAÇÃO SQL =====
            // Usamos 'using' para garantir que a conexão seja fechada
            using (SqlConnection conexao = Conexao.ObterConexao())
            {
                // 1. Inicia a transação
                SqlTransaction transaction = conexao.BeginTransaction();

                try
                {
                    // 2. INSERE O PEDIDO na tabela PEDIDOS
                    // (Usamos 'OUTPUT INSERTED.ID_PEDIDO' para pegar o ID do pedido que acabamos de criar)
                    string queryPedido = @"INSERT INTO PEDIDOS (ID_CLIENTE, DATA_PEDIDO, VALOR_TOTAL) 
                                           OUTPUT INSERTED.ID_PEDIDO 
                                           VALUES (@IdCliente, @Data, @Total)";

                    SqlCommand cmdPedido = new SqlCommand(queryPedido, conexao, transaction);
                    cmdPedido.Parameters.AddWithValue("@IdCliente", clienteIdLogado.Value);
                    cmdPedido.Parameters.AddWithValue("@Data", DateTime.Now);
                    cmdPedido.Parameters.AddWithValue("@Total", totalPedido);

                    // Executa e pega o ID do novo pedido
                    int novoPedidoId = (int)cmdPedido.ExecuteScalar();

                    var itens = GerenciadorCarrinho.ObterItens();

                    // 3. Loop para salvar cada item do carrinho
                    foreach (var item in itens)
                    {
                        // 3a. INSERE O ITEM na tabela ITENSPEDIDO
                        string queryItem = @"INSERT INTO ITENSPEDIDO (ID_PEDIDO, ID_LIVRO, ID_EDITORA, QUANTIDADE, PRECO_UNITARIO, SUBTOTAL) 
                     VALUES (@IdPedido, @IdLivro, @IdEditora, @Qtd, @Preco, @Subtotal)";

                        SqlCommand cmdItem = new SqlCommand(queryItem, conexao, transaction);
                        cmdItem.Parameters.AddWithValue("@IdPedido", novoPedidoId);
                        cmdItem.Parameters.AddWithValue("@IdLivro", item.LivroId);
                        cmdItem.Parameters.AddWithValue("@IdEditora", item.EditoraId);     // <-- ADICIONADO
                        cmdItem.Parameters.AddWithValue("@Qtd", item.Quantidade);
                        cmdItem.Parameters.AddWithValue("@Preco", item.PrecoUnitario);
                        cmdItem.Parameters.AddWithValue("@Subtotal", item.Subtotal); // <-- ADICIONADO
                        cmdItem.ExecuteNonQuery();

                        // 3b. ATUALIZA O ESTOQUE na tabela LIVROS
                        // (Verificamos 'WHERE QUANTIDADE_ESTOQUE >= @QtdComprada' para segurança)
                        string queryEstoque = @"UPDATE LIVROS SET QUANTIDADE_ESTOQUE = QUANTIDADE_ESTOQUE - @QtdComprada 
                                                WHERE ID_LIVRO = @IdLivro AND QUANTIDADE_ESTOQUE >= @QtdComprada";

                        SqlCommand cmdEstoque = new SqlCommand(queryEstoque, conexao, transaction);
                        cmdEstoque.Parameters.AddWithValue("@QtdComprada", item.Quantidade);
                        cmdEstoque.Parameters.AddWithValue("@IdLivro", item.LivroId);

                        int linhasAfetadas = cmdEstoque.ExecuteNonQuery();

                        // Se 'linhasAfetadas' for 0, o estoque era insuficiente.
                        if (linhasAfetadas == 0)
                        {
                            // Isso força o 'catch' e cancela a transação
                            throw new Exception($"Estoque insuficiente para o livro '{item.Titulo}'.");
                        }
                    }

                    // 4. Se tudo deu certo, COMPLETA a transação
                    transaction.Commit();

                    // 5. Limpa o carrinho e avisa o usuário
                    GerenciadorCarrinho.LimparCarrinho();
                    MessageBox.Show($"Pedido Nº {novoPedidoId} realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close(); // Fecha a tela de finalizar

                    // (O FormCarrinho será atualizado quando reaberto, pois o Gerenciador está limpo)

                }
                catch (Exception ex)
                {
                    // 6. Se algo deu errado, DESFAZ a transação
                    transaction.Rollback();
                    MessageBox.Show("Erro ao finalizar pedido: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Reabilita o botão
                    btnConfirmarPedido.Enabled = true;
                }
            } // 'using' fecha a conexão aqui
            // ===== FIM DA TRANSAÇÃO SQL =====
        }
    }
}