namespace winproySerialPort
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.rchMensaje = new System.Windows.Forms.RichTextBox();
            this.rchConversacion = new System.Windows.Forms.RichTextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(596, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Enviar archivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rchMensaje
            // 
            this.rchMensaje.Location = new System.Drawing.Point(12, 333);
            this.rchMensaje.Name = "rchMensaje";
            this.rchMensaje.Size = new System.Drawing.Size(551, 96);
            this.rchMensaje.TabIndex = 1;
            this.rchMensaje.Text = "";
            // 
            // rchConversacion
            // 
            this.rchConversacion.Location = new System.Drawing.Point(12, 12);
            this.rchConversacion.Name = "rchConversacion";
            this.rchConversacion.Size = new System.Drawing.Size(551, 304);
            this.rchConversacion.TabIndex = 2;
            this.rchConversacion.Text = "";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(596, 333);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(180, 42);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Enviar mensaje";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.rchConversacion);
            this.Controls.Add(this.rchMensaje);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RichTextBox rchMensaje;
        private System.Windows.Forms.RichTextBox rchConversacion;
        private System.Windows.Forms.Button btnEnviar;
    }
}