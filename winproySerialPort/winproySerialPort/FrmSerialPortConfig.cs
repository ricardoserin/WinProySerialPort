using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace winproySerialPort
{
    public partial class FrmSerialPortConfig : Form
    {
        public delegate void enviarDatosConexion(string puerto, int baudios);
        public event enviarDatosConexion enviado;

        public delegate void ConfiguracionTerminada();
        private event ConfiguracionTerminada configurado;
        public event ConfiguracionTerminada Configurado
        {
            add
            {
                configurado += new ConfiguracionTerminada(value);
            }
            remove
            {
                configurado -= value;
            }
        }

        public delegate void cerrarConexion();
        public event cerrarConexion cerrado;

        public bool EstadoConexion;

        public FrmSerialPortConfig(bool estado)
        {
            InitializeComponent();
            EstadoConexion = estado;
        }

        private void FrmSerialPortConfig_Load(object sender, EventArgs e)
        {
            String[] puertos = SerialPort.GetPortNames();
            cbxPuertos.DataSource=puertos;
            //objTxRx = new classTransRecep();
            if (EstadoConexion)
            {
                btnConectar.Enabled = false;
                cbxPuertos.Enabled = false;
                txtBaudios.Enabled = false;
                btnDesconectar.Enabled = true;
            }
            else
            {
                btnConectar.Enabled = true;
                cbxPuertos.Enabled = true;
                txtBaudios.Enabled = true;
                btnDesconectar.Enabled = false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            string puerto = cbxPuertos.SelectedValue.ToString();
            int baudios = int.Parse(txtBaudios.Text.ToString());
            //enviado(puerto, baudios);
            //Dispose(); */
            try
            {
                EventController.Inicializar(puerto, baudios, 1024);
                Dispose();
                configurado();
            } catch
            {
                MessageBox.Show("Error al iniciar el puerto, seleccione uno diferente");
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            cerrado();
            btnConectar.Enabled = true;
            cbxPuertos.Enabled = true;
            txtBaudios.Enabled = true;
            btnDesconectar.Enabled = false;
        }
    }
}
