namespace SistemaLivraria.Forms
{
    partial class FormMenuCliente
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
            this.btnCatalogo = new System.Windows.Forms.Button();
            this.btnCarrinho = new System.Windows.Forms.Button();
            this.btnPedidos = new System.Windows.Forms.Button();
            this.btnEditarPerfil = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.lblBoasVindas = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCatalogo
            // 
            this.btnCatalogo.Location = new System.Drawing.Point(77, 352);
            this.btnCatalogo.Name = "btnCatalogo";
            this.btnCatalogo.Size = new System.Drawing.Size(144, 23);
            this.btnCatalogo.TabIndex = 0;
            this.btnCatalogo.Text = "🏠 Voltar ao Catálogo";
            this.btnCatalogo.UseVisualStyleBackColor = true;
            this.btnCatalogo.Click += new System.EventHandler(this.btnCatalogo_Click);
            // 
            // btnCarrinho
            // 
            this.btnCarrinho.Location = new System.Drawing.Point(236, 352);
            this.btnCarrinho.Name = "btnCarrinho";
            this.btnCarrinho.Size = new System.Drawing.Size(120, 23);
            this.btnCarrinho.TabIndex = 1;
            this.btnCarrinho.Text = "🛒 Meu Carrinho";
            this.btnCarrinho.UseVisualStyleBackColor = true;
            this.btnCarrinho.Click += new System.EventHandler(this.btnCarrinho_Click);
            // 
            // btnPedidos
            // 
            this.btnPedidos.Location = new System.Drawing.Point(362, 352);
            this.btnPedidos.Name = "btnPedidos";
            this.btnPedidos.Size = new System.Drawing.Size(114, 23);
            this.btnPedidos.TabIndex = 2;
            this.btnPedidos.Text = "📦 Meus Pedidos";
            this.btnPedidos.UseVisualStyleBackColor = true;
            this.btnPedidos.Click += new System.EventHandler(this.btnPedidos_Click);
            // 
            // btnEditarPerfil
            // 
            this.btnEditarPerfil.Location = new System.Drawing.Point(482, 352);
            this.btnEditarPerfil.Name = "btnEditarPerfil";
            this.btnEditarPerfil.Size = new System.Drawing.Size(107, 23);
            this.btnEditarPerfil.TabIndex = 3;
            this.btnEditarPerfil.Text = "👤 Editar Perfil";
            this.btnEditarPerfil.UseVisualStyleBackColor = true;
            this.btnEditarPerfil.Click += new System.EventHandler(this.btnEditarPerfil_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(595, 352);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(66, 23);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "🚪 Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // lblBoasVindas
            // 
            this.lblBoasVindas.AutoSize = true;
            this.lblBoasVindas.Location = new System.Drawing.Point(369, 156);
            this.lblBoasVindas.Name = "lblBoasVindas";
            this.lblBoasVindas.Size = new System.Drawing.Size(100, 13);
            this.lblBoasVindas.TabIndex = 5;
            this.lblBoasVindas.Text = "Bem-vindo, [Nome]!";
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(249, 156);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(100, 50);
            this.picIcon.TabIndex = 6;
            this.picIcon.TabStop = false;
            // 
            // FormMenuCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblBoasVindas);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnEditarPerfil);
            this.Controls.Add(this.btnPedidos);
            this.Controls.Add(this.btnCarrinho);
            this.Controls.Add(this.btnCatalogo);
            this.Name = "FormMenuCliente";
            this.Text = "Área do Cliente";
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCatalogo;
        private System.Windows.Forms.Button btnCarrinho;
        private System.Windows.Forms.Button btnPedidos;
        private System.Windows.Forms.Button btnEditarPerfil;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lblBoasVindas;
        private System.Windows.Forms.PictureBox picIcon;
    }
}