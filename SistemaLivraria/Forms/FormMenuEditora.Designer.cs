namespace SistemaLivraria.Forms
{
    partial class FormMenuEditora
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCadastrarLivro = new System.Windows.Forms.Button();
            this.btnMeusLivros = new System.Windows.Forms.Button();
            this.btnMinhasVendas = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.picCapa = new System.Windows.Forms.PictureBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblBoasVindas = new System.Windows.Forms.Label();
            this.btnGerenciarAutores = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCadastrarLivro
            // 
            this.btnCadastrarLivro.Location = new System.Drawing.Point(38, 343);
            this.btnCadastrarLivro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCadastrarLivro.Name = "btnCadastrarLivro";
            this.btnCadastrarLivro.Size = new System.Drawing.Size(137, 23);
            this.btnCadastrarLivro.TabIndex = 0;
            this.btnCadastrarLivro.Text = "📚 Cadastrar Livro";
            this.btnCadastrarLivro.UseVisualStyleBackColor = true;
            this.btnCadastrarLivro.Click += new System.EventHandler(this.btnCadastrarLivro_Click);
            // 
            // btnMeusLivros
            // 
            this.btnMeusLivros.Location = new System.Drawing.Point(181, 343);
            this.btnMeusLivros.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMeusLivros.Name = "btnMeusLivros";
            this.btnMeusLivros.Size = new System.Drawing.Size(111, 23);
            this.btnMeusLivros.TabIndex = 1;
            this.btnMeusLivros.Text = "📖 Meus Livros";
            this.btnMeusLivros.UseVisualStyleBackColor = true;
            this.btnMeusLivros.Click += new System.EventHandler(this.btnMeusLivros_Click);
            // 
            // btnMinhasVendas
            // 
            this.btnMinhasVendas.Location = new System.Drawing.Point(436, 343);
            this.btnMinhasVendas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMinhasVendas.Name = "btnMinhasVendas";
            this.btnMinhasVendas.Size = new System.Drawing.Size(124, 23);
            this.btnMinhasVendas.TabIndex = 2;
            this.btnMinhasVendas.Text = "📊 Minhas Vendas";
            this.btnMinhasVendas.UseVisualStyleBackColor = true;
            this.btnMinhasVendas.Click += new System.EventHandler(this.btnMinhasVendas_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.Location = new System.Drawing.Point(566, 343);
            this.btnInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(125, 23);
            this.btnInicio.TabIndex = 3;
            this.btnInicio.Text = "🏠 Voltar ao Início";
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(696, 343);
            this.btnSair.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "🚪 Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // picCapa
            // 
            this.picCapa.BackColor = System.Drawing.Color.LightGray;
            this.picCapa.Dock = System.Windows.Forms.DockStyle.Top;
            this.picCapa.Location = new System.Drawing.Point(0, 0);
            this.picCapa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picCapa.Name = "picCapa";
            this.picCapa.Size = new System.Drawing.Size(800, 71);
            this.picCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCapa.TabIndex = 5;
            this.picCapa.TabStop = false;
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.White;
            this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIcon.Location = new System.Drawing.Point(284, 31);
            this.picIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(101, 100);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 6;
            this.picIcon.TabStop = false;
            // 
            // lblBoasVindas
            // 
            this.lblBoasVindas.AutoSize = true;
            this.lblBoasVindas.Location = new System.Drawing.Point(403, 89);
            this.lblBoasVindas.Name = "lblBoasVindas";
            this.lblBoasVindas.Size = new System.Drawing.Size(133, 16);
            this.lblBoasVindas.TabIndex = 7;
            this.lblBoasVindas.Text = "\"Bem-vindo, [Nome]\"";
            // 
            // btnGerenciarAutores
            // 
            this.btnGerenciarAutores.Location = new System.Drawing.Point(298, 343);
            this.btnGerenciarAutores.Name = "btnGerenciarAutores";
            this.btnGerenciarAutores.Size = new System.Drawing.Size(132, 23);
            this.btnGerenciarAutores.TabIndex = 8;
            this.btnGerenciarAutores.Text = "Gerenciar Autores";
            this.btnGerenciarAutores.UseVisualStyleBackColor = true;
            this.btnGerenciarAutores.Click += new System.EventHandler(this.btnGerenciarAutores_Click);
            // 
            // FormMenuEditora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGerenciarAutores);
            this.Controls.Add(this.lblBoasVindas);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.picCapa);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.btnMinhasVendas);
            this.Controls.Add(this.btnMeusLivros);
            this.Controls.Add(this.btnCadastrarLivro);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMenuEditora";
            this.Text = "FormMenuEditora";
            this.Load += new System.EventHandler(this.FormMenuEditora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCadastrarLivro;
        private System.Windows.Forms.Button btnMeusLivros;
        private System.Windows.Forms.Button btnMinhasVendas;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox picCapa;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblBoasVindas;
        private System.Windows.Forms.Button btnGerenciarAutores;
    }
}