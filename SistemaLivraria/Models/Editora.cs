using System;

namespace SistemaLivraria.Models
{
    public class Editora
    {
        public int IdEditora { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Capa { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}