namespace SistemaLivraria.Forms
{
    partial class FormDetalhesLivro
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
            this.picCapa = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAutores = new System.Windows.Forms.Label();
            this.lblEditora = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblISBN = new System.Windows.Forms.Label();
            this.lblDetalhes = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPreco = new System.Windows.Forms.Label();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.txtSinopse = new System.Windows.Forms.TextBox();
            this.btnAdicionarCarrinho = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).BeginInit();
            this.SuspendLayout();
            // 
            // picCapa
            // 
            this.picCapa.Location = new System.Drawing.Point(22, 13);
            this.picCapa.Name = "picCapa";
            this.picCapa.Size = new System.Drawing.Size(250, 350);
            this.picCapa.TabIndex = 0;
            this.picCapa.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(289, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(76, 13);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Título do Livro";
            // 
            // lblAutores
            // 
            this.lblAutores.AutoSize = true;
            this.lblAutores.Location = new System.Drawing.Point(289, 37);
            this.lblAutores.Name = "lblAutores";
            this.lblAutores.Size = new System.Drawing.Size(52, 13);
            this.lblAutores.TabIndex = 2;
            this.lblAutores.Text = "Autor(es):";
            // 
            // lblEditora
            // 
            this.lblEditora.AutoSize = true;
            this.lblEditora.Location = new System.Drawing.Point(289, 64);
            this.lblEditora.Name = "lblEditora";
            this.lblEditora.Size = new System.Drawing.Size(46, 13);
            this.lblEditora.TabIndex = 3;
            this.lblEditora.Text = "Editora: ";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(289, 86);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(58, 13);
            this.lblCategoria.TabIndex = 4;
            this.lblCategoria.Text = "Categoria: ";
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Location = new System.Drawing.Point(289, 110);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(38, 13);
            this.lblISBN.TabIndex = 5;
            this.lblISBN.Text = "ISBN: ";
            // 
            // lblDetalhes
            // 
            this.lblDetalhes.AutoSize = true;
            this.lblDetalhes.Location = new System.Drawing.Point(289, 133);
            this.lblDetalhes.Name = "lblDetalhes";
            this.lblDetalhes.Size = new System.Drawing.Size(149, 13);
            this.lblDetalhes.TabIndex = 6;
            this.lblDetalhes.Text = "Ano: | Páginas: | Acabamento:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "SINOPSE:";
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.Location = new System.Drawing.Point(289, 217);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(45, 13);
            this.lblPreco.TabIndex = 8;
            this.lblPreco.Text = "R$ 0,00";
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.Location = new System.Drawing.Point(289, 284);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(104, 13);
            this.lblEstoque.TabIndex = 9;
            this.lblEstoque.Text = "Estoque: 0 unidades";
            // 
            // txtSinopse
            // 
            this.txtSinopse.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtSinopse.Location = new System.Drawing.Point(352, 164);
            this.txtSinopse.Multiline = true;
            this.txtSinopse.Name = "txtSinopse";
            this.txtSinopse.ReadOnly = true;
            this.txtSinopse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSinopse.Size = new System.Drawing.Size(425, 107);
            this.txtSinopse.TabIndex = 10;
            this.txtSinopse.TabStop = false;
            // 
            // btnAdicionarCarrinho
            // 
            this.btnAdicionarCarrinho.Location = new System.Drawing.Point(292, 339);
            this.btnAdicionarCarrinho.Name = "btnAdicionarCarrinho";
            this.btnAdicionarCarrinho.Size = new System.Drawing.Size(119, 23);
            this.btnAdicionarCarrinho.TabIndex = 11;
            this.btnAdicionarCarrinho.Text = "Adicionar ao Carrinho";
            this.btnAdicionarCarrinho.UseVisualStyleBackColor = true;
            this.btnAdicionarCarrinho.Click += new System.EventHandler(this.btnAdicionarCarrinho_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Location = new System.Drawing.Point(437, 340);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(75, 23);
            this.btnVoltar.TabIndex = 12;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // FormDetalhesLivro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnAdicionarCarrinho);
            this.Controls.Add(this.txtSinopse);
            this.Controls.Add(this.lblEstoque);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblDetalhes);
            this.Controls.Add(this.lblISBN);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.lblEditora);
            this.Controls.Add(this.lblAutores);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.picCapa);
            this.Name = "FormDetalhesLivro";
            this.Text = "FormDetalhesLivro";
            this.Load += new System.EventHandler(this.FormDetalhesLivro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCapa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCapa;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAutores;
        private System.Windows.Forms.Label lblEditora;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.Label lblDetalhes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.TextBox txtSinopse;
        private System.Windows.Forms.Button btnAdicionarCarrinho;
        private System.Windows.Forms.Button btnVoltar;
    }
}