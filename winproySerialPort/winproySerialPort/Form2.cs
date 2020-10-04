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
using winproySerialPort.Controladores;

namespace winproySerialPort
{
    public partial class Form2 : Form
    {
        private delegate void MensajeroDeProceso(string mensaje, string emisor);
        MensajeroDeProceso ObtenerMensajeDeProceso;

        public delegate void MostrarProgreso(int value);
        MostrarProgreso MostrarProgresoEnvio;

        private Thread ProcesoMuestraProgresoEnvio;
        private Thread ProcesoMuestraProgresoRecepcion;
        private List<ProgressBar> BarrasRecepcion;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ruta = LeerRutaArchivo();
            var archivoEnvio = new ArchivoEnvio(ruta);
            EmmitController.Enviar(archivoEnvio, Text);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var mensaje = rchMensaje.Text.Trim();
            var mensajeEnvio = new Mensaje(mensaje);
            EmmitController.Enviar(mensajeEnvio, Text);
            rchMensaje.Clear();
        }
        private void Form2_OnPuertoConfigurado()
        {
            Enabled = true;
            // Se agregan los métodos a los eventos del emisor
            EmmitController.FileTransmissionStarted += EventController_InicioEnvioArchivo;
            EmmitController.MessageEmmited += EventController_EnvioMensaje;

            ReceiverController.FileReceptionStarted += EventController_InicioRecepcionArchivo;
            ReceiverController.MessageReceived += EventController_LlegoMensaje;

            // Establece el nombre del formulario
            Text = Puerto.NombrePuerto;
            MessageBox.Show($"Conexión en el puerto {Puerto.NombrePuerto}");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Enabled = false;
            ObtenerMensajeDeProceso = new MensajeroDeProceso(MensajeExterno);
            MostrarProgresoEnvio = new MostrarProgreso(mostrarProgreso);
            BarrasRecepcion = new List<ProgressBar>();
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
        private void EventController_InicioEnvioArchivo(string mensaje)
        {
            if (ProcesoMuestraProgresoEnvio != null && ProcesoMuestraProgresoEnvio.IsAlive) ProcesoMuestraProgresoEnvio.Abort();
            ProcesoMuestraProgresoEnvio = new Thread(LlenarBarraEnvio);
            ProcesoMuestraProgresoEnvio.Start();
        }
        private void EventController_EnvioMensaje(string mensajeEnviado)
        {
            Invoke(ObtenerMensajeDeProceso, mensajeEnviado, Puerto.NombrePuerto);
        }
        private void EventController_InicioRecepcionArchivo(object o, string mensaje)
        {
            ProcesoMuestraProgresoRecepcion = new Thread(LlenarBarraRecepcion);
            ProcesoMuestraProgresoRecepcion.Start();
        }
        private void EventController_LlegoMensaje(object o, string mensajeRecibido)
        {
            Invoke(ObtenerMensajeDeProceso, mensajeRecibido, "El otro puerto");
        }
        // Fin eventos de envio - recepción
        public void LlenarBarraEnvio()
        {
            while (EmmitController.Emisor.Enviando)
            {
                Invoke(MostrarProgresoEnvio, (int)(EmmitController.Emisor.ProgresoEnvio * 100));
            }
            Invoke(MostrarProgresoEnvio, 100);
            ProcesoMuestraProgresoEnvio.Abort();
        }
        public void LlenarBarraRecepcion()
        {
            var pb = new ProgressBar();
            pb.Location = new Point(200, 50 + BarrasRecepcion.Count * 50);
            pb.Visible = true;
            BarrasRecepcion.Add(pb);
            ProcesoMuestraProgresoRecepcion.Abort();
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
        private string ObtenerRutaGuardado(Archivo archivoRecepcion = null)
        {
            if (archivoRecepcion != null)
            {
                saveFileDialog1.InitialDirectory = archivoRecepcion.Directorio;
                saveFileDialog1.FileName = $"{archivoRecepcion.Nombre}-copia";
                saveFileDialog1.DefaultExt = archivoRecepcion.Extension;
                saveFileDialog1.AddExtension = true;
            }
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            return path;
        }
    }
}
