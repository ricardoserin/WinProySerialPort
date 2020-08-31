using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winproySerialPort
{
    public partial class Form1 : Form
    {
        classTransRecep objTxRx;
        delegate void MostrarOtroProceso(string mensaje);
        //declaramos el delegado
        MostrarOtroProceso delegadoMostrar;
        public bool estado;
        FrmSerialPortConfig frmSPc;

        public Form1()
        {
            InitializeComponent();
            estado = false;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            objTxRx.Enviar(rchMensajes.Text.Trim());

            rchConversacion.SelectionColor = Color.DarkRed;
            rchConversacion.SelectionBackColor = Color.LightGreen;
            rchConversacion.SelectionAlignment = HorizontalAlignment.Right;
            rchConversacion.SelectedText = "\nEnviado: \n" + rchMensajes.Text.Trim() + "\n";
            //rchConversacion.Text+= "\nEnviado: \n" + rchMensajes.Text.Trim()+"\n";

            rchMensajes.Text = "";
            //Barra de progreso
            //pgbarMensaje.Minimum = 1;
            //pgbarMensaje.Maximum = Convert.ToInt32(objTxRx.BytesPorSalir().ToString());
            //pgbarMensaje.Value = 1;
            //while (Convert.ToInt32(objTxRx.BytesPorSalir().ToString()) > 0)
            //{
            //    pgbarMensaje.PerformStep();
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                estado = objTxRx.EstadoConexion();
            }
            catch(NullReferenceException)
            {
                estado = false;
            }
            frmSPc = new FrmSerialPortConfig(estado);
            frmSPc.enviado += new FrmSerialPortConfig.enviarDatosConexion(iniciarConexion);
            
            frmSPc.Show();
            //objTxRx = new classTransRecep();
            //objTxRx.Inicializa("COM1", 57600);
            //objTxRx.LlegoMensaje += new classTransRecep.HandlerTxRx(objTxRx_LlegoMensaje);

            ////instanciamos al delegado
            //delegadoMostrar = new MostrarOtroProceso(MostrandoMensaje);
        }


        private void objTxRx_LlegoMensaje(object o, string mm)
        {
            //MessageBox.Show("En usuario, se disparó: " + mm);
            //invocamos al delegado
            Invoke(delegadoMostrar, mm); //el delegado debe ejecutar MostrandoMensaje
        }

        private void MostrandoMensaje(string textMens)
        {
            rchConversacion.SelectionColor = Color.DarkBlue;
            rchConversacion.SelectionBackColor = Color.LightBlue;
            rchConversacion.SelectionAlignment = HorizontalAlignment.Left;
            rchConversacion.SelectedText = "\nRecibido:\n" + textMens + "\n";
            //rchConversacion.Text += "\nRecibido:\n" + textMens + "\n";
        }

        private void btnRecibir_Click(object sender, EventArgs e)
        {
            //objTxRx.Recibir();
            //Mostramos la cantidad de bytes que faltan por salir del buffer
            MessageBox.Show("Faltan por salir a enviar: " + objTxRx.BytesPorSalir().ToString());
            MessageBox.Show("Tramas recibidas: " + objTxRx.tramasRecibidas);
        }

        private void puertoSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombrePuertoAbierto = objTxRx.nombrePuerto;
            int baudiosPuertoAbierto = objTxRx.baudiosPuerto;
            estado = objTxRx.EstadoConexion();
            frmSPc = new FrmSerialPortConfig(estado);
            frmSPc.enviado += new FrmSerialPortConfig.enviarDatosConexion(iniciarConexion);
            frmSPc.cerrado += new FrmSerialPortConfig.cerrarConexion(cerrarConexion);
            frmSPc.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                objTxRx.CerrarConexion();
            }
            catch(NullReferenceException)
            {

            }
            
        }

        public void iniciarConexion(string p, int b)
        {
            objTxRx = new classTransRecep();
            objTxRx.Inicializa(p, b);

            objTxRx.LlegoMensaje += new classTransRecep.HandlerTxRx(objTxRx_LlegoMensaje);

            //instanciamos al delegado para que ejecute el método MostrandoMensaje 
            delegadoMostrar = new MostrarOtroProceso(MostrandoMensaje);

            this.Enabled = true;
        }

        public void cerrarConexion()
        {
            objTxRx.CerrarConexion();
        }

        private void btnEnviarArchivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            objTxRx.IniciaEnvioArchivo(path);
        }
    }
}
