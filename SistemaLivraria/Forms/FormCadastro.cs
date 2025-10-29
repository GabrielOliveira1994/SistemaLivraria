using System;
using System.Windows.Forms;

namespace SistemaLivraria.Forms
{
    public partial class FormCadastro : Form
    {
        private string tipoUsuario; // "Cliente" ou "Editora"

        // Construtor SEM parâmetros (para o designer funcionar)
        public FormCadastro()
        {
            InitializeComponent();
        }

        // Método público para definir o tipo depois
        public void DefinirTipo(string tipo)
        {
            tipoUsuario = tipo;
            this.Text = $"Cadastro - {tipo}";

            // Você pode mostrar/esconder campos dependendo do tipo
            if (tipo == "Cliente")
            {
                // Mostrar campos de Cliente (Nome, CPF)
                // Esconder campos de Editora (Razão Social, CNPJ)
            }
            else if (tipo == "Editora")
            {
                // Mostrar campos de Editora
                // Esconder campos de Cliente
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar cadastro
            MessageBox.Show($"Cadastro de {tipoUsuario} será implementado!");
        }
    }
}