﻿namespace winproySerialPort
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
            this.btnRecibir = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.puertoSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pgbarMensaje = new System.Windows.Forms.ProgressBar();
            btnEnviar = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviar
            // 
            btnEnviar.AutoEllipsis = true;
            btnEnviar.BackColor = System.Drawing.Color.SlateGray;
            btnEnviar.BackgroundImage = global::winproySerialPort.Properties.Resources.send_icon_solid_01;
            btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEnviar.ForeColor = System.Drawing.Color.SlateGray;
            btnEnviar.Location = new System.Drawing.Point(290, 388);
            btnEnviar.Margin = new System.Windows.Forms.Padding(0);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            btnEnviar.Size = new System.Drawing.Size(50, 50);
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
            this.rchMensajes.Location = new System.Drawing.Point(12, 388);
            this.rchMensajes.Name = "rchMensajes";
            this.rchMensajes.Size = new System.Drawing.Size(275, 53);
            this.rchMensajes.TabIndex = 1;
            this.rchMensajes.Text = "";
            // 
            // rchConversacion
            // 
            this.rchConversacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rchConversacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchConversacion.Location = new System.Drawing.Point(12, 27);
            this.rchConversacion.Name = "rchConversacion";
            this.rchConversacion.ReadOnly = true;
            this.rchConversacion.Size = new System.Drawing.Size(375, 344);
            this.rchConversacion.TabIndex = 2;
            this.rchConversacion.Text = "\n";
            // 
            // btnRecibir
            // 
            this.btnRecibir.Location = new System.Drawing.Point(343, 405);
            this.btnRecibir.Name = "btnRecibir";
            this.btnRecibir.Size = new System.Drawing.Size(44, 33);
            this.btnRecibir.TabIndex = 3;
            this.btnRecibir.Text = "RECIBIR MENSAJE";
            this.btnRecibir.UseVisualStyleBackColor = true;
            this.btnRecibir.Click += new System.EventHandler(this.btnRecibir_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(399, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.puertoSerialToolStripMenuItem});
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuracionToolStripMenuItem.Text = "Configuración";
            // 
            // puertoSerialToolStripMenuItem
            // 
            this.puertoSerialToolStripMenuItem.Name = "puertoSerialToolStripMenuItem";
            this.puertoSerialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.puertoSerialToolStripMenuItem.Text = "Puerto Serial";
            this.puertoSerialToolStripMenuItem.Click += new System.EventHandler(this.puertoSerialToolStripMenuItem_Click);
            // 
            // pgbarMensaje
            // 
            this.pgbarMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbarMensaje.Location = new System.Drawing.Point(12, 377);
            this.pgbarMensaje.Name = "pgbarMensaje";
            this.pgbarMensaje.Size = new System.Drawing.Size(375, 5);
            this.pgbarMensaje.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(399, 444);
            this.Controls.Add(this.pgbarMensaje);
            this.Controls.Add(this.btnRecibir);
            this.Controls.Add(this.rchConversacion);
            this.Controls.Add(this.rchMensajes);
            this.Controls.Add(btnEnviar);
            this.Controls.Add(this.menuStrip1);
            this.Enabled = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form SERIN NERY ELMER RICARDO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rchMensajes;
        private System.Windows.Forms.RichTextBox rchConversacion;
        private System.Windows.Forms.Button btnRecibir;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem puertoSerialToolStripMenuItem;
        private System.Windows.Forms.ProgressBar pgbarMensaje;
    }
}

