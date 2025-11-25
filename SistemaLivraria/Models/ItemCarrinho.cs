using System;

namespace SistemaLivraria.Models
{
    public class ItemCarrinho
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public int EditoraId { get; set; }
        public string NomeEditora { get; set; }
        public byte[] Capa { get; set; }

        // Propriedade calculada
        public decimal Subtotal
        {
            get { return PrecoUnitario * Quantidade; }
        }

        public ItemCarrinho(int livroId, string titulo, decimal preco, int quantidade, int editoraId, string nomeEditora, byte[] capa = null)
        {
            LivroId = livroId;
            Titulo = titulo;
            PrecoUnitario = preco;
            Quantidade = quantidade;
            EditoraId = editoraId;
            NomeEditora = nomeEditora;
            Capa = capa;
        }
    }
}