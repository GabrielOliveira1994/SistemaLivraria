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
            this.lblResumoItens.Location = new System.Drawing.Point(36, 22);
            this.lblResumoItens.Name = "lblResumoItens";
            this.lblResumoItens.Size = new System.Drawing.Size(69, 13);
            this.lblResumoItens.TabIndex = 0;
            this.lblResumoItens.Text = "ResumoItens";
            // 
            // btnConfirmarPedido
            // 
            this.btnConfirmarPedido.Location = new System.Drawing.Point(385, 12);
            this.btnConfirmarPedido.Name = "btnConfirmarPedido";
            this.btnConfirmarPedido.Size = new System.Drawing.Size(121, 23);
            this.btnConfirmarPedido.TabIndex = 1;
            this.btnConfirmarPedido.Text = "Confirmar Pedido";
            this.btnConfirmarPedido.UseVisualStyleBackColor = true;
            this.btnConfirmarPedido.Click += new System.EventHandler(this.btnConfirmarPedido_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(385, 51);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblTotalPedido
            // 
            this.lblTotalPedido.AutoSize = true;
            this.lblTotalPedido.Location = new System.Drawing.Point(36, 51);
            this.lblTotalPedido.Name = "lblTotalPedido";
            this.lblTotalPedido.Size = new System.Drawing.Size(64, 13);
            this.lblTotalPedido.TabIndex = 3;
            this.lblTotalPedido.Text = "TotalPedido";
            // 
            // lblEnderecoCompleto
            // 
            this.lblEnderecoCompleto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEnderecoCompleto.Location = new System.Drawing.Point(36, 83);
            this.lblEnderecoCompleto.Name = "lblEnderecoCompleto";
            this.lblEnderecoCompleto.Size = new System.Drawing.Size(220, 196);
            this.lblEnderecoCompleto.TabIndex = 4;
            this.lblEnderecoCompleto.Text = "label3";
            // 
            // FormFinalizarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblEnderecoCompleto);
            this.Controls.Add(this.lblTotalPedido);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmarPedido);
            this.Controls.Add(this.lblResumoItens);
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