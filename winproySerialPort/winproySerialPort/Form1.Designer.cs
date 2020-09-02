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
            System.Windows.Forms.Button btnEnviar;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rchMensajes = new System.Windows.Forms.RichTextBox();
            this.rchConversacion = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.puertoSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pgbarMensaje = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEnviarArchivo = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            btnEnviar = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviar
            // 
            btnEnviar.AutoEllipsis = true;
            btnEnviar.BackColor = System.Drawing.Color.Transparent;
            btnEnviar.BackgroundImage = global::winproySerialPort.Properties.Resources.send_icon_solid_01;
            btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEnviar.ForeColor = System.Drawing.Color.Transparent;
            btnEnviar.Location = new System.Drawing.Point(497, 16);
            btnEnviar.Margin = new System.Windows.Forms.Padding(0);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            btnEnviar.Size = new System.Drawing.Size(67, 62);
            btnEnviar.TabIndex = 0;
            btnEnviar.TabStop = false;
            btnEnviar.UseMnemonic = false;
            btnEnviar.UseVisualStyleBackColor = false;
            btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // rchMensajes
            // 
            this.rchMensajes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rchMensajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchMensajes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchMensajes.Location = new System.Drawing.Point(11, 22);
            this.rchMensajes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rchMensajes.Name = "rchMensajes";
            this.rchMensajes.Size = new System.Drawing.Size(413, 48);
            this.rchMensajes.TabIndex = 1;
            this.rchMensajes.Text = "Envía un mensaje";
            // 
            // rchConversacion
            // 
            this.rchConversacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rchConversacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.rchConversacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchConversacion.Location = new System.Drawing.Point(0, 0);
            this.rchConversacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rchConversacion.Name = "rchConversacion";
            this.rchConversacion.ReadOnly = true;
            this.rchConversacion.Size = new System.Drawing.Size(577, 475);
            this.rchConversacion.TabIndex = 2;
            this.rchConversacion.Text = "\n";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(577, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.configuracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.puertoSerialToolStripMenuItem});
            this.configuracionToolStripMenuItem.ForeColor = System.Drawing.Color.SlateGray;
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.configuracionToolStripMenuItem.Text = "Configuración";
            // 
            // puertoSerialToolStripMenuItem
            // 
            this.puertoSerialToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.puertoSerialToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.puertoSerialToolStripMenuItem.Name = "puertoSerialToolStripMenuItem";
            this.puertoSerialToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.puertoSerialToolStripMenuItem.Text = "Puerto Serial";
            this.puertoSerialToolStripMenuItem.Click += new System.EventHandler(this.puertoSerialToolStripMenuItem_Click);
            // 
            // pgbarMensaje
            // 
            this.pgbarMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbarMensaje.Location = new System.Drawing.Point(1, 0);
            this.pgbarMensaje.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgbarMensaje.Name = "pgbarMensaje";
            this.pgbarMensaje.Size = new System.Drawing.Size(576, 12);
            this.pgbarMensaje.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnEnviarArchivo);
            this.groupBox1.Controls.Add(this.pgbarMensaje);
            this.groupBox1.Controls.Add(this.rchMensajes);
            this.groupBox1.Controls.Add(btnEnviar);
            this.groupBox1.Location = new System.Drawing.Point(0, 466);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(577, 90);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // btnEnviarArchivo
            // 
            this.btnEnviarArchivo.AutoEllipsis = true;
            this.btnEnviarArchivo.BackColor = System.Drawing.Color.Transparent;
            this.btnEnviarArchivo.BackgroundImage = global::winproySerialPort.Properties.Resources.clip;
            this.btnEnviarArchivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnviarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarArchivo.ForeColor = System.Drawing.Color.Transparent;
            this.btnEnviarArchivo.Location = new System.Drawing.Point(432, 16);
            this.btnEnviarArchivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnviarArchivo.Name = "btnEnviarArchivo";
            this.btnEnviarArchivo.Size = new System.Drawing.Size(61, 62);
            this.btnEnviarArchivo.TabIndex = 0;
            this.btnEnviarArchivo.TabStop = false;
            this.btnEnviarArchivo.UseMnemonic = false;
            this.btnEnviarArchivo.UseVisualStyleBackColor = false;
            this.btnEnviarArchivo.Click += new System.EventHandler(this.btnEnviarArchivo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(577, 549);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.rchConversacion);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rchMensajes;
        private System.Windows.Forms.RichTextBox rchConversacion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem puertoSerialToolStripMenuItem;
        private System.Windows.Forms.ProgressBar pgbarMensaje;
        private System.Windows.Forms.Button btnEnviarArchivo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

