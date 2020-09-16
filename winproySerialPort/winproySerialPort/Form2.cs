using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace winproySerialPort
{
    public partial class Form2 : Form
    {
        private delegate void MensajeroDeProceso(string mensaje, string emisor);
        MensajeroDeProceso ObtenerMensajeDeProceso;

        public delegate void MostrarProgreso(int value);
        MostrarProgreso MostrarProgresoEnvio;

        private Thread ProcesoMuestraProgreso;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ruta = LeerRutaArchivo();
            var archivoEnvio = new ArchivoEnvio(ruta);
            EventController.Enviar(archivoEnvio);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var mensaje = rchMensaje.Text.Trim();
            EventController.Enviar(mensaje);
            rchMensaje.Clear();
        }
        private void Form2_OnPuertoConfigurado()
        {
            Enabled = true;
            // Se agregan los métodos que se dispararán cuando ocurran los eventos
            EventController.Receptor.MessageReceived += EventController_LlegoMensaje;
            EventController.Receptor.DataFromFileReceived += EventController_InicioRecepcionArchivo;
            EventController.Emisor.MessageEmmited += EventController_EnvioMensaje;
            EventController.Emisor.TransmissionStarted += EventController_InicioEnvio;

            // Establece el nombre del formulario
            Text = Puerto.NombrePuerto;
            MessageBox.Show($"Conexión en el puerto {Puerto.NombrePuerto}");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Enabled = false;
            ObtenerMensajeDeProceso = new MensajeroDeProceso(MensajeExterno);
            MostrarProgresoEnvio = new MostrarProgreso(mostrarProgreso);
            pbEnvio.Maximum = 100;
            pbEnvio.Minimum = 0;
            try
            {
                var formPortConfig = new FrmSerialPortConfig(false);
                formPortConfig.Configurado += Form2_OnPuertoConfigurado;
                formPortConfig.Show();
            } catch (Exception exc)
            {
                MessageBox.Show($"Error no controlado al inicializar el puerto\n . Error: {exc.Message}\n Stack: {exc.StackTrace}");
            }
        }
        // Eventos de envio - recepción
        private void EventController_InicioEnvio(object o, string mensaje)
        {
            ProcesoMuestraProgreso = new Thread(LlenarBarraProgreso);
            ProcesoMuestraProgreso.Start();
        }
        private void EventController_EnvioMensaje(object o, string mensajeEnviado)
        {
            Invoke(ObtenerMensajeDeProceso, mensajeEnviado, Puerto.NombrePuerto);
        }
        private void EventController_InicioRecepcionArchivo(object o)
        {
            var archivoRecepcion = new ArchivoRecepcion(ObtenerRutaGuardado());
            EventController.Receptor.Inicializar(archivoRecepcion);
        }
        private void EventController_LlegoMensaje(object o, string mensajeRecibido)
        {
            Invoke(ObtenerMensajeDeProceso, mensajeRecibido, "El otro puerto");
        }
        // Fin eventos de envio - recepción
        public void LlenarBarraProgreso()
        {
            while (EventController.Emisor.Enviando)
            {
                Invoke(MostrarProgresoEnvio, (int)(EventController.Emisor.ProgresoEnvio * 100), 100);
            }
            Invoke(MostrarProgresoEnvio, 100, 100);
            ProcesoMuestraProgreso.Abort();
        }
        // Modificadores de formulario
        private void MensajeExterno(string mensaje, string emisor)
        {
            rchConversacion.Text += $"{emisor}: {mensaje}\n";
        }
        public void mostrarProgreso(int value)
        {
            pbEnvio.Value = value;
            if (value == 100)
            {
                pbEnvio.Refresh();
                label1.Text = "Envio finalizado";
            }
        }
        private string LeerRutaArchivo()
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            return path;
        }
        private string ObtenerRutaGuardado(Archivo archivoLectura = null)
        {
            if (archivoLectura != null)
            {
                saveFileDialog1.InitialDirectory = archivoLectura.Directorio;
                saveFileDialog1.FileName = $"{archivoLectura.Nombre}-copia";
                saveFileDialog1.DefaultExt = archivoLectura.Extension;
                saveFileDialog1.AddExtension = true;
            }
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            return path;
        }
    }
}
