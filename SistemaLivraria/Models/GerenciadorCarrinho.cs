using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLivraria.Models
{
    public static class GerenciadorCarrinho
    {
        // Lista estática que persiste enquanto o programa está rodando
        private static List<ItemCarrinho> itens = new List<ItemCarrinho>();

        // Obter todos os itens do carrinho
        public static List<ItemCarrinho> ObterItens()
        {
            return itens;
        }

        // Adicionar item ao carrinho
        public static bool AdicionarItem(ItemCarrinho item)
        {
            // Verifica se o livro já está no carrinho
            var itemExistente = itens.FirstOrDefault(i => i.LivroId == item.LivroId);

            if (itemExistente != null)
            {
                // Se já existe, aumenta a quantidade
                itemExistente.Quantidade += item.Quantidade;
            }
            else
            {
                // Se não existe, adiciona novo
                itens.Add(item);
            }

            return true;
        }

        // Remover item do carrinho
        public static bool RemoverItem(int livroId)
        {
            var item = itens.FirstOrDefault(i => i.LivroId == livroId);
            if (item != null)
            {
                itens.Remove(item);
                return true;
            }
            return false;
        }

        // Atualizar quantidade de um item
        public static bool AtualizarQuantidade(int livroId, int novaQuantidade)
        {
            var item = itens.FirstOrDefault(i => i.LivroId == livroId);
            if (item != null)
            {
                if (novaQuantidade <= 0)
                {
                    itens.Remove(item);
                }
                else
                {
                    item.Quantidade = novaQuantidade;
                }
                return true;
            }
            return false;
        }

        // Limpar carrinho (após finalizar pedido)
        public static void LimparCarrinho()
        {
            itens.Clear();
        }

        // Calcular total do carrinho
        public static decimal ObterTotal()
        {
            return itens.Sum(i => i.Subtotal);
        }

        // Contar itens no carrinho
        public static int ContarItens()
        {
            return itens.Sum(i => i.Quantidade);
        }
    }
}