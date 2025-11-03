namespace SistemaLivraria.Forms
{
    partial class FormMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnEditora = new System.Windows.Forms.Button();
            this.btnVoltar2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = " \"Bem-vindo ao Sistema de Livraria\"";
            // 
            // btnCliente
            // 
            this.btnCliente.Location = new System.Drawing.Point(385, 185);
            this.btnCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(271, 28);
            this.btnCliente.TabIndex = 1;
            this.btnCliente.Text = "\"Sou Cliente\"";
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // btnEditora
            // 
            this.btnEditora.Location = new System.Drawing.Point(385, 256);
            this.btnEditora.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditora.Name = "btnEditora";
            this.btnEditora.Size = new System.Drawing.Size(271, 28);
            this.btnEditora.TabIndex = 2;
            this.btnEditora.Text = "\"Sou Editora\"";
            this.btnEditora.UseVisualStyleBackColor = true;
            this.btnEditora.Click += new System.EventHandler(this.btnEditora_Click);
            // 
            // btnVoltar2
            // 
            this.btnVoltar2.Location = new System.Drawing.Point(120, 406);
            this.btnVoltar2.Name = "btnVoltar2";
            this.btnVoltar2.Size = new System.Drawing.Size(75, 23);
            this.btnVoltar2.TabIndex = 3;
            this.btnVoltar2.Text = "Voltar";
            this.btnVoltar2.UseVisualStyleBackColor = true;
            this.btnVoltar2.Click += new System.EventHandler(this.btnVoltar2_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnVoltar2);
            this.Controls.Add(this.btnEditora);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Livraria - Bem-vindo";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnEditora;
        private System.Windows.Forms.Button btnVoltar2;
    }
}