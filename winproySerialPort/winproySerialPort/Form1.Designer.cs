namespace winproySerialPort
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEnviar = new System.Windows.Forms.Button();
            this.rchMensajes = new System.Windows.Forms.RichTextBox();
            this.rchConversacion = new System.Windows.Forms.RichTextBox();
            this.btnRecibir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(334, 300);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(196, 23);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.Text = "ENVIAR MENSAJE";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // rchMensajes
            // 
            this.rchMensajes.Location = new System.Drawing.Point(334, 213);
            this.rchMensajes.Name = "rchMensajes";
            this.rchMensajes.Size = new System.Drawing.Size(196, 81);
            this.rchMensajes.TabIndex = 1;
            this.rchMensajes.Text = "";
            // 
            // rchConversacion
            // 
            this.rchConversacion.Location = new System.Drawing.Point(334, 12);
            this.rchConversacion.Name = "rchConversacion";
            this.rchConversacion.Size = new System.Drawing.Size(196, 195);
            this.rchConversacion.TabIndex = 2;
            this.rchConversacion.Text = "";
            // 
            // btnRecibir
            // 
            this.btnRecibir.Location = new System.Drawing.Point(334, 329);
            this.btnRecibir.Name = "btnRecibir";
            this.btnRecibir.Size = new System.Drawing.Size(196, 23);
            this.btnRecibir.TabIndex = 3;
            this.btnRecibir.Text = "RECIBIR MENSAJE";
            this.btnRecibir.UseVisualStyleBackColor = true;
            this.btnRecibir.Click += new System.EventHandler(this.btnRecibir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 366);
            this.Controls.Add(this.btnRecibir);
            this.Controls.Add(this.rchConversacion);
            this.Controls.Add(this.rchMensajes);
            this.Controls.Add(this.btnEnviar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form SERIN NERY ELMER RICARDO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.RichTextBox rchMensajes;
        private System.Windows.Forms.RichTextBox rchConversacion;
        private System.Windows.Forms.Button btnRecibir;
    }
}

