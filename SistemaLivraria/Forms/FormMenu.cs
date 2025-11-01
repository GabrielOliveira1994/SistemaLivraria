using System;
using System.Windows.Forms;

namespace SistemaLivraria.Forms
{
    public partial class FormMenu : Form
    {
        // Construtor SEM parâmetros (para o designer funcionar)
        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            // Abre tela de Login como CLIENTE
            FormLogin formLogin = new FormLogin();
            formLogin.DefinirTipo("Cliente");
            formLogin.Show();
            this.Hide();
        }

        private void btnEditora_Click(object sender, EventArgs e)
        {
            // Abre tela de Login como EDITORA
            FormLogin formLogin = new FormLogin();
            formLogin.DefinirTipo("Editora");
            formLogin.Show();
            this.Hide();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }
    }
}