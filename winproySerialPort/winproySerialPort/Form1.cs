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


        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            objTxRx.Enviar(rchMensajes.Text.Trim());
            rchMensajes.Text = "";
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objTxRx = new classTransRecep();
            objTxRx.Inicializa("COM1");
            objTxRx.LlegoMensaje += new classTransRecep.HandlerTxRx(objTxRx_LlegoMensaje);

            //instanciamos al delegado
            delegadoMostrar = new MostrarOtroProceso(MostrandoMensaje);
        }

        private void objTxRx_LlegoMensaje(object o, string mm)
        {
            //MessageBox.Show("En usuario, se disparó: " + mm);
            //invocamos al delegado
            Invoke(delegadoMostrar, mm);
        }

        private void MostrandoMensaje(string textMens)
        {
            rchConversacion.Text += "\n" + textMens;
        }

        private void btnRecibir_Click(object sender, EventArgs e)
        {
            //objTxRx.Recibir();
            MessageBox.Show("Faltan por salir a enviar: " + objTxRx.BytesPorSalir().ToString());
        }

        
    }
}
