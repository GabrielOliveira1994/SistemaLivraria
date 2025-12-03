namespace SistemaLivraria.Forms
{
    partial class FormFinalizarPedido
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
            this.lblResumoItens = new System.Windows.Forms.Label();
            this.btnConfirmarPedido = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTotalPedido = new System.Windows.Forms.Label();
            this.lblEnderecoCompleto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblResumoItens
            // 
            this.lblResumoItens.AutoSize = true;
            this.lblResumoItens.Location = new System.Drawing.Point(48, 27);
            this.lblResumoItens.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResumoItens.Name = "lblResumoItens";
            this.lblResumoItens.Size = new System.Drawing.Size(86, 16);
            this.lblResumoItens.TabIndex = 0;
            this.lblResumoItens.Text = "ResumoItens";
            // 
            // btnConfirmarPedido
            // 
            this.btnConfirmarPedido.Location = new System.Drawing.Point(513, 15);
            this.btnConfirmarPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfirmarPedido.Name = "btnConfirmarPedido";
            this.btnConfirmarPedido.Size = new System.Drawing.Size(161, 28);
            this.btnConfirmarPedido.TabIndex = 1;
            this.btnConfirmarPedido.Text = "Confirmar Pedido";
            this.btnConfirmarPedido.UseVisualStyleBackColor = true;
            this.btnConfirmarPedido.Click += new System.EventHandler(this.btnConfirmarPedido_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(513, 63);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblTotalPedido
            // 
            this.lblTotalPedido.AutoSize = true;
            this.lblTotalPedido.Location = new System.Drawing.Point(48, 63);
            this.lblTotalPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPedido.Name = "lblTotalPedido";
            this.lblTotalPedido.Size = new System.Drawing.Size(82, 16);
            this.lblTotalPedido.TabIndex = 3;
            this.lblTotalPedido.Text = "TotalPedido";
            // 
            // lblEnderecoCompleto
            // 
            this.lblEnderecoCompleto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEnderecoCompleto.Location = new System.Drawing.Point(48, 102);
            this.lblEnderecoCompleto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnderecoCompleto.Name = "lblEnderecoCompleto";
            this.lblEnderecoCompleto.Size = new System.Drawing.Size(293, 241);
            this.lblEnderecoCompleto.TabIndex = 4;
            this.lblEnderecoCompleto.Text = "Endereço";
            // 
            // FormFinalizarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.lblEnderecoCompleto);
            this.Controls.Add(this.lblTotalPedido);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmarPedido);
            this.Controls.Add(this.lblResumoItens);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormFinalizarPedido";
            this.Text = "FormFinalizarPedido";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResumoItens;
        private System.Windows.Forms.Button btnConfirmarPedido;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTotalPedido;
        private System.Windows.Forms.Label lblEnderecoCompleto;
    }
}