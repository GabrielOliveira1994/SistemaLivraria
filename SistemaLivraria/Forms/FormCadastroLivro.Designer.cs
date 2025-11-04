namespace SistemaLivraria.Forms
{
    partial class FormCadastroLivro
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblISBN = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblAnoPublicacao = new System.Windows.Forms.Label();
            this.lblPaginas = new System.Windows.Forms.Label();
            this.lblAcabamento = new System.Windows.Forms.Label();
            this.lblPreco = new System.Windows.Forms.Label();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.lblSinopse = new System.Windows.Forms.Label();
            this.lblCapa = new System.Windows.Forms.Label();
            this.lblAutores = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.txtAnoPublicacao = new System.Windows.Forms.TextBox();
            this.txtPaginas = new System.Windows.Forms.TextBox();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.txtEstoque = new System.Windows.Forms.TextBox();
            this.txtSinopse = new System.Windows.Forms.TextBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbAcabamento = new System.Windows.Forms.ComboBox();
            this.picCapa = new System.Windows.Forms.PictureBox();
            this.btnSelecionarCapa = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lstAutores = new System.Windows.Forms.ListBox();
            this.txtAutor = new System.Windows.Forms.TextBox();
            this.btnAdicionarAutor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(13, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(38, 13);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Título:";
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(13, 35);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(35, 13);
            this.lblISBN.TabIndex = 1;
            this.lblISBN.Text = "ISBN:";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(247, 10);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(55, 13);
            this.lblCategoria.TabIndex = 2;
            this.lblCategoria.Text = "Categoria:";
            // 
            // lblAnoPublicacao
            // 
            this.lblAnoPublicacao.AutoSize = true;
            this.lblAnoPublicacao.Location = new System.Drawing.Point(247, 42);
            this.lblAnoPublicacao.Name = "lblAnoPublicacao";
            this.lblAnoPublicacao.Size = new System.Drawing.Size(100, 13);
            this.lblAnoPublicacao.TabIndex = 3;
            this.lblAnoPublicacao.Text = "Ano de Publicação:";
            // 
            // lblPaginas
            // 
            this.lblPaginas.AutoSize = true;
            this.lblPaginas.Location = new System.Drawing.Point(507, 13);
            this.lblPaginas.Name = "lblPaginas";
            this.lblPaginas.Size = new System.Drawing.Size(48, 13);
            this.lblPaginas.TabIndex = 4;
            this.lblPaginas.Text = "Páginas:";
            // 
            // lblAcabamento
            // 
            this.lblAcabamento.AutoSize = true;
            this.lblAcabamento.Location = new System.Drawing.Point(507, 35);
            this.lblAcabamento.Name = "lblAcabamento";
            this.lblAcabamento.Size = new System.Drawing.Size(70, 13);
            this.lblAcabamento.TabIndex = 5;
            this.lblAcabamento.Text = "Acabamento:";
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.Location = new System.Drawing.Point(13, 75);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(38, 13);
            this.lblPreco.TabIndex = 6;
            this.lblPreco.Text = "Preço:";
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.Location = new System.Drawing.Point(13, 100);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(124, 13);
            this.lblEstoque.TabIndex = 7;
            this.lblEstoque.Text = "Quantidade em Estoque:";
            // 
            // lblSinopse
            // 
            this.lblSinopse.AutoSize = true;
            this.lblSinopse.Location = new System.Drawing.Point(13, 150);
            this.lblSinopse.Name = "lblSinopse";
            this.lblSinopse.Size = new System.Drawing.Size(48, 13);
            this.lblSinopse.TabIndex = 8;
            this.lblSinopse.Text = "Sinopse:";
            // 
            // lblCapa
            // 
            this.lblCapa.AutoSize = true;
            this.lblCapa.Location = new System.Drawing.Point(546, 171);
            this.lblCapa.Name = "lblCapa";
            this.lblCapa.Size = new System.Drawing.Size(76, 13);
            this.lblCapa.TabIndex = 9;
            this.lblCapa.Text = "Capa do Livro:";
            // 
            // lblAutores
            // 
            this.lblAutores.AutoSize = true;
            this.lblAutores.Location = new System.Drawing.Point(29, 279);
            this.lblAutores.Name = "lblAutores";
            this.lblAutores.Size = new System.Drawing.Size(46, 13);
            this.lblAutores.TabIndex = 10;
            this.lblAutores.Text = "Autores:";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(118, 10);
            this.txtTitulo.MaxLength = 200;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(100, 20);
            this.txtTitulo.TabIndex = 13;
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(118, 35);
            this.txtISBN.MaxLength = 20;
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(100, 20);
            this.txtISBN.TabIndex = 14;
            // 
            // txtAnoPublicacao
            // 
            this.txtAnoPublicacao.Location = new System.Drawing.Point(353, 39);
            this.txtAnoPublicacao.MaxLength = 4;
            this.txtAnoPublicacao.Name = "txtAnoPublicacao";
            this.txtAnoPublicacao.Size = new System.Drawing.Size(100, 20);
            this.txtAnoPublicacao.TabIndex = 15;
            // 
            // txtPaginas
            // 
            this.txtPaginas.Location = new System.Drawing.Point(612, 10);
            this.txtPaginas.MaxLength = 5;
            this.txtPaginas.Name = "txtPaginas";
            this.txtPaginas.Size = new System.Drawing.Size(100, 20);
            this.txtPaginas.TabIndex = 16;
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(119, 72);
            this.txtPreco.MaxLength = 10;
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(100, 20);
            this.txtPreco.TabIndex = 17;
            // 
            // txtEstoque
            // 
            this.txtEstoque.Location = new System.Drawing.Point(143, 98);
            this.txtEstoque.MaxLength = 5;
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Size = new System.Drawing.Size(100, 20);
            this.txtEstoque.TabIndex = 18;
            // 
            // txtSinopse
            // 
            this.txtSinopse.Location = new System.Drawing.Point(143, 150);
            this.txtSinopse.MaxLength = 5000;
            this.txtSinopse.Multiline = true;
            this.txtSinopse.Name = "txtSinopse";
            this.txtSinopse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSinopse.Size = new System.Drawing.Size(100, 20);
            this.txtSinopse.TabIndex = 19;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(308, 7);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(121, 21);
            this.cmbCategoria.TabIndex = 20;
            // 
            // cmbAcabamento
            // 
            this.cmbAcabamento.FormattingEnabled = true;
            this.cmbAcabamento.Location = new System.Drawing.Point(613, 32);
            this.cmbAcabamento.Name = "cmbAcabamento";
            this.cmbAcabamento.Size = new System.Drawing.Size(121, 21);
            this.cmbAcabamento.TabIndex = 21;
            // 
            // picCapa
            // 
            this.picCapa.BackColor = System.Drawing.Color.LightGray;
            this.picCapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCapa.Location = new System.Drawing.Point(638, 171);
            this.picCapa.Name = "picCapa";
            this.picCapa.Size = new System.Drawing.Size(150, 200);
            this.picCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCapa.TabIndex = 22;
            this.picCapa.TabStop = false;
            // 
            // btnSelecionarCapa
            // 
            this.btnSelecionarCapa.Location = new System.Drawing.Point(510, 190);
            this.btnSelecionarCapa.Name = "btnSelecionarCapa";
            this.btnSelecionarCapa.Size = new System.Drawing.Size(112, 23);
            this.btnSelecionarCapa.TabIndex = 23;
            this.btnSelecionarCapa.Text = "Selecionar Capa...";
            this.btnSelecionarCapa.UseVisualStyleBackColor = true;
            this.btnSelecionarCapa.Click += new System.EventHandler(this.btnSelecionarCapa_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(693, 404);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 24;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(598, 404);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 25;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lstAutores
            // 
            this.lstAutores.FormattingEnabled = true;
            this.lstAutores.Location = new System.Drawing.Point(199, 274);
            this.lstAutores.Name = "lstAutores";
            this.lstAutores.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstAutores.Size = new System.Drawing.Size(120, 95);
            this.lstAutores.TabIndex = 26;
            // 
            // txtAutor
            // 
            this.txtAutor.Location = new System.Drawing.Point(23, 297);
            this.txtAutor.Name = "txtAutor";
            this.txtAutor.Size = new System.Drawing.Size(100, 20);
            this.txtAutor.TabIndex = 27;
            this.txtAutor.Text = "Nome do autor:";
            // 
            // btnAdicionarAutor
            // 
            this.btnAdicionarAutor.Location = new System.Drawing.Point(23, 324);
            this.btnAdicionarAutor.Name = "btnAdicionarAutor";
            this.btnAdicionarAutor.Size = new System.Drawing.Size(100, 23);
            this.btnAdicionarAutor.TabIndex = 28;
            this.btnAdicionarAutor.Text = "Adicionar Autor";
            this.btnAdicionarAutor.UseVisualStyleBackColor = true;
            this.btnAdicionarAutor.Click += new System.EventHandler(this.btnAdicionarAutor_Click);
            // 
            // FormCadastroLivro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAdicionarAutor);
            this.Controls.Add(this.txtAutor);
            this.Controls.Add(this.lstAutores);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnSelecionarCapa);
            this.Controls.Add(this.picCapa);
            this.Controls.Add(this.cmbAcabamento);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.txtSinopse);
            this.Controls.Add(this.txtEstoque);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.txtPaginas);
            this.Controls.Add(this.txtAnoPublicacao);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.lblAutores);
            this.Controls.Add(this.lblCapa);
            this.Controls.Add(this.lblSinopse);
            this.Controls.Add(this.lblEstoque);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.lblAcabamento);
            this.Controls.Add(this.lblPaginas);
            this.Controls.Add(this.lblAnoPublicacao);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.lblISBN);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormCadastroLivro";
            this.Text = "FormCadastroLivro";
            this.Load += new System.EventHandler(this.FormCadastroLivro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblAnoPublicacao;
        private System.Windows.Forms.Label lblPaginas;
        private System.Windows.Forms.Label lblAcabamento;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.Label lblSinopse;
        private System.Windows.Forms.Label lblCapa;
        private System.Windows.Forms.Label lblAutores;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.TextBox txtAnoPublicacao;
        private System.Windows.Forms.TextBox txtPaginas;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.TextBox txtEstoque;
        private System.Windows.Forms.TextBox txtSinopse;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.ComboBox cmbAcabamento;
        private System.Windows.Forms.PictureBox picCapa;
        private System.Windows.Forms.Button btnSelecionarCapa;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ListBox lstAutores;
        private System.Windows.Forms.TextBox txtAutor;
        private System.Windows.Forms.Button btnAdicionarAutor;
    }
}