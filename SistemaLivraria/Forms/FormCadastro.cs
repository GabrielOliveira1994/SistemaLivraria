using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SistemaLivraria.Database;
using SistemaLivraria.Models;
using System.IO; // ← Para FileStream
using System.Drawing; // ← Para Image

namespace SistemaLivraria.Forms
{
    public partial class FormCadastro : Form
    {
        private string tipoUsuario; // "Cliente" ou "Editora"
        private byte[] imagemIcon = null;    // ← NOVA!
        private byte[] imagemCapa = null;    // ← NOVA!

        public FormCadastro()
        {
            InitializeComponent();
        }

        public void DefinirTipo(string tipo)
        {
            tipoUsuario = tipo;
            this.Text = $"Cadastro - {tipo}";

            // Mostrar/Esconder campos específicos
            if (tipo == "Cliente")
            {
                lblNome.Text = "Nome Completo:";
                lblDocumento.Text = "CPF:";
                txtDocumento.MaxLength = 14; // 000.000.000-00

                // Ocultar campos de editora (se existirem)
            }
            else if (tipo == "Editora")
            {
                lblNome.Text = "Razão Social:";
                lblDocumento.Text = "CNPJ:";
                txtDocumento.MaxLength = 18; // 00.000.000/0000-00
                                             // Mostrar campos de capa
                lblCapa.Visible = true;
                picCapa.Visible = true;
                btnSelecionarCapa.Visible = true;
            }
        }

        // Botão SELECIONAR ICON
        private void btnSelecionarIcon_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
            openFile.Title = "Selecione uma foto de perfil";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Carrega a imagem no PictureBox
                    picIcon.Image = Image.FromFile(openFile.FileName);

                    // Converte para byte[] para salvar no banco
                    imagemIcon = ConverterImagemParaBytes(openFile.FileName);

                    MessageBox.Show("Imagem carregada com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar imagem: " + ex.Message, "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Botão SELECIONAR CAPA (só para editora)
        private void btnSelecionarCapa_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
            openFile.Title = "Selecione uma imagem de capa";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Carrega a imagem no PictureBox
                    picCapa.Image = Image.FromFile(openFile.FileName);

                    // Converte para byte[] para salvar no banco
                    imagemCapa = ConverterImagemParaBytes(openFile.FileName);

                    MessageBox.Show("Imagem carregada com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar imagem: " + ex.Message, "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método auxiliar para converter imagem em byte[]
        private byte[] ConverterImagemParaBytes(string caminhoImagem)
        {
            using (FileStream fs = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                return bytes;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validações
            if (!ValidarCampos())
                return;

            if (tipoUsuario == "Cliente")
            {
                if (CadastrarCliente())
                {
                    MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir tela de login
                    FormLogin formLogin = new FormLogin();
                    formLogin.DefinirTipo("Cliente");
                    formLogin.Show();
                    this.Close();
                }
            }
            else if (tipoUsuario == "Editora")
            {
                if (CadastrarEditora())
                {
                    MessageBox.Show("Editora cadastrada com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir tela de login
                    FormLogin formLogin = new FormLogin();
                    formLogin.DefinirTipo("Editora");
                    formLogin.Show();
                    this.Close();
                }
            }
        }

        private bool ValidarCampos()
        {
            // Validar campos vazios
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmarSenha.Text))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar email
            if (!ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("Email inválido!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validar senhas iguais
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("As senhas não coincidem!", "Atenção",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarSenha.Focus();
                return false;
            }

            // Validar documento (CPF ou CNPJ)
            if (tipoUsuario == "Cliente")
            {
                if (!ValidarCPF(txtDocumento.Text))
                {
                    MessageBox.Show("CPF inválido!", "Atenção",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDocumento.Focus();
                    return false;
                }
            }
            else if (tipoUsuario == "Editora")
            {
                if (!ValidarCNPJ(txtDocumento.Text))
                {
                    MessageBox.Show("CNPJ inválido!", "Atenção",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDocumento.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool CadastrarCliente()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"INSERT INTO CLIENTE 
                                    (NOME, CPF, EMAIL, SENHA, TELEFONE, CEP, LOGRADOURO, 
                                     NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO) 
                                    VALUES 
                                    (@Nome, @CPF, @Email, @Senha, @Telefone, @CEP, @Logradouro, 
                                     @Numero, @Complemento, @Bairro, @Cidade, @Estado)";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@CPF", LimparDocumento(txtDocumento.Text));
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Senha", txtSenha.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text ?? "");
                    cmd.Parameters.AddWithValue("@CEP", txtCEP.Text ?? "");
                    cmd.Parameters.AddWithValue("@Logradouro", txtLogradouro.Text ?? "");
                    cmd.Parameters.AddWithValue("@Numero", txtNumero.Text ?? "");
                    cmd.Parameters.AddWithValue("@Complemento", txtComplemento.Text ?? "");
                    cmd.Parameters.AddWithValue("@Bairro", txtBairro.Text ?? "");
                    cmd.Parameters.AddWithValue("@Cidade", txtCidade.Text ?? "");
                    cmd.Parameters.AddWithValue("@Estado", txtEstado.Text ?? "");
                    cmd.Parameters.AddWithValue("@Icon", (object)imagemIcon ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                {
                    MessageBox.Show("Este CPF ou Email já está cadastrado!", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        private bool CadastrarEditora()
        {
            try
            {
                using (SqlConnection conexao = Conexao.ObterConexao())
                {
                    string query = @"INSERT INTO EDITORA 
                                    (RAZAO_SOCIAL, CNPJ, EMAIL, SENHA, TELEFONE, CEP, LOGRADOURO, 
                                     NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO) 
                                    VALUES 
                                    (@RazaoSocial, @CNPJ, @Email, @Senha, @Telefone, @CEP, @Logradouro, 
                                     @Numero, @Complemento, @Bairro, @Cidade, @Estado)";

                    SqlCommand cmd = new SqlCommand(query, conexao);
                    cmd.Parameters.AddWithValue("@RazaoSocial", txtNome.Text);
                    cmd.Parameters.AddWithValue("@CNPJ", LimparDocumento(txtDocumento.Text));
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Senha", txtSenha.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text ?? "");
                    cmd.Parameters.AddWithValue("@CEP", txtCEP.Text ?? "");
                    cmd.Parameters.AddWithValue("@Logradouro", txtLogradouro.Text ?? "");
                    cmd.Parameters.AddWithValue("@Numero", txtNumero.Text ?? "");
                    cmd.Parameters.AddWithValue("@Complemento", txtComplemento.Text ?? "");
                    cmd.Parameters.AddWithValue("@Bairro", txtBairro.Text ?? "");
                    cmd.Parameters.AddWithValue("@Cidade", txtCidade.Text ?? "");
                    cmd.Parameters.AddWithValue("@Estado", txtEstado.Text ?? "");
                    cmd.Parameters.AddWithValue("@Icon", (object)imagemIcon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Capa", (object)imagemCapa ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                {
                    MessageBox.Show("Este CNPJ ou Email já está cadastrado!", "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        // ===== MÉTODOS DE VALIDAÇÃO =====

        private bool ValidarEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool ValidarCPF(string cpf)
        {
            cpf = LimparDocumento(cpf);

            if (cpf.Length != 11)
                return false;

            // CPFs inválidos conhecidos
            if (cpf == "00000000000" || cpf == "11111111111" ||
                cpf == "22222222222" || cpf == "33333333333" ||
                cpf == "44444444444" || cpf == "55555555555" ||
                cpf == "66666666666" || cpf == "77777777777" ||
                cpf == "88888888888" || cpf == "99999999999")
                return false;

            // Validação dos dígitos verificadores
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private bool ValidarCNPJ(string cnpj)
        {
            cnpj = LimparDocumento(cnpj);

            if (cnpj.Length != 14)
                return false;

            // CNPJs inválidos conhecidos
            if (cnpj == "00000000000000" || cnpj == "11111111111111" ||
                cnpj == "22222222222222" || cnpj == "33333333333333" ||
                cnpj == "44444444444444" || cnpj == "55555555555555" ||
                cnpj == "66666666666666" || cnpj == "77777777777777" ||
                cnpj == "88888888888888" || cnpj == "99999999999999")
                return false;

            // Validação simplificada (pode melhorar depois)
            return true;
        }

        private string LimparDocumento(string documento)
        {
            return Regex.Replace(documento, @"[^\d]", "");
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.DefinirTipo(tipoUsuario);
            formLogin.Show();
            this.Close();
        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {

        }
    }
}