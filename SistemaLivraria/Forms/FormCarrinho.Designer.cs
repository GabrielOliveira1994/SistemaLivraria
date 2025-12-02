namespace SistemaLivraria.Forms
{
    partial class FormCarrinho
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
            this.dgvCarrinho = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnContinuarComprando = new System.Windows.Forms.Button();
            this.btnLimparCarrinho = new System.Windows.Forms.Button();
            this.btnFinalizarPedido = new System.Windows.Forms.Button();
            this.panelRodape = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).BeginInit();
            this.panelRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCarrinho
            // 
            this.dgvCarrinho.AllowUserToAddRows = false;
            this.dgvCarrinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrinho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarrinho.Location = new System.Drawing.Point(0, 0);
            this.dgvCarrinho.MultiSelect = false;
            this.dgvCarrinho.Name = "dgvCarrinho";
            this.dgvCarrinho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrinho.Size = new System.Drawing.Size(800, 450);
            this.dgvCarrinho.TabIndex = 0;
            this.dgvCarrinho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarrinho_CellContentClick);
            this.dgvCarrinho.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarrinho_CellValueChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(42, 38);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 13);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total: R$ 0,00";
            // 
            // btnContinuarComprando
            // 
            this.btnContinuarComprando.Location = new System.Drawing.Point(260, 33);
            this.btnContinuarComprando.Name = "btnContinuarComprando";
            this.btnContinuarComprando.Size = new System.Drawing.Size(143, 23);
            this.btnContinuarComprando.TabIndex = 2;
            this.btnContinuarComprando.Text = "← Continuar Comprando";
            this.btnContinuarComprando.UseVisualStyleBackColor = true;
            this.btnContinuarComprando.Click += new System.EventHandler(this.btnContinuarComprando_Click);
            // 
            // btnLimparCarrinho
            // 
            this.btnLimparCarrinho.Location = new System.Drawing.Point(425, 33);
            this.btnLimparCarrinho.Name = "btnLimparCarrinho";
            this.btnLimparCarrinho.Size = new System.Drawing.Size(120, 23);
            this.btnLimparCarrinho.TabIndex = 3;
            this.btnLimparCarrinho.Text = "🗑️ Limpar Carrinho";
            this.btnLimparCarrinho.UseVisualStyleBackColor = true;
            this.btnLimparCarrinho.Click += new System.EventHandler(this.btnLimparCarrinho_Click);
            // 
            // btnFinalizarPedido
            // 
            this.btnFinalizarPedido.Location = new System.Drawing.Point(560, 33);
            this.btnFinalizarPedido.Name = "btnFinalizarPedido";
            this.btnFinalizarPedido.Size = new System.Drawing.Size(126, 23);
            this.btnFinalizarPedido.TabIndex = 4;
            this.btnFinalizarPedido.Text = "✓ Finalizar Pedido";
            this.btnFinalizarPedido.UseVisualStyleBackColor = true;
            this.btnFinalizarPedido.Click += new System.EventHandler(this.btnFinalizarPedido_Click);
            // 
            // panelRodape
            // 
            this.panelRodape.Controls.Add(this.lblTotal);
            this.panelRodape.Controls.Add(this.btnFinalizarPedido);
            this.panelRodape.Controls.Add(this.btnContinuarComprando);
            this.panelRodape.Controls.Add(this.btnLimparCarrinho);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodape.Location = new System.Drawing.Point(0, 350);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(800, 100);
            this.panelRodape.TabIndex = 5;
            // 
            // FormCarrinho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelRodape);
            this.Controls.Add(this.dgvCarrinho);
            this.Name = "FormCarrinho";
            this.Text = "Carrinho";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).EndInit();
            this.panelRodape.ResumeLayout(false);
            this.panelRodape.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCarrinho;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnContinuarComprando;
        private System.Windows.Forms.Button btnLimparCarrinho;
        private System.Windows.Forms.Button btnFinalizarPedido;
        private System.Windows.Forms.Panel panelRodape;
    }
}