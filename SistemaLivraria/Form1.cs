using System;
using System.Windows.Forms;
using SistemaLivraria.Database;  // ← Importante!

namespace SistemaLivraria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            if (Conexao.TestarConexao())
            {
                MessageBox.Show("Conexão realizada com sucesso! ✅",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Falha ao conectar com o banco de dados! ❌",
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}