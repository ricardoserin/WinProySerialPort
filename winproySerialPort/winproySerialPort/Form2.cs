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
    public partial class Form2 : Form
    {
        private EventController eventController;
        private delegate void MensajeroDeProceso(string mensaje, string emisor);
        MensajeroDeProceso ObtenerMensajeDeProceso;
        public Form2()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            var archivoLectura = new ArchivoLectura(LeerRutaArchivo());
            eventController.Enviar(archivoLectura);
            // var archivoEscritura = new ArchivoEscritura(ObtenerRutaGuardado(archivoLectura));
            /*
            var arreglo = new byte[archivoLectura.TamanoTrama];
            long restante = archivoLectura.Tamano;

            while(restante > archivoLectura.TamanoTrama)
            {
                archivoLectura.Leer(arreglo);
                archivoEscritura.Escribir(arreglo);
                archivoEscritura.Avance += archivoEscritura.TamanoTrama;
                restante = archivoLectura.Tamano - archivoEscritura.Avance;
            }

            archivoLectura.Leer(arreglo);
            archivoEscritura.Escribir(arreglo, Convert.ToInt32(restante));
            archivoEscritura.DesactivarArchivo();
            archivoLectura.DesactivarArchivo();
            archivoLectura.Leer(arreglo);
            archivoEscritura.Escribir(arreglo);
            MessageBox.Show(Puerto.BytesPorSalir.ToString());
            */
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            eventController.Enviar(rchMensaje.Text.Trim());
            rchMensaje.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string puerto = "COM1";
            int tamanoDeTrama = 1024;
            ObtenerMensajeDeProceso = new MensajeroDeProceso(MensajeExterno);
            try
            {
                eventController = new EventController();
                eventController.Inicializar(puerto, tamanoDeTrama);
                // Se agregan las funciones a los eventos
                eventController.receptor.MessageReceived += EventController_LlegoMensaje;
                eventController.receptor.DataFromFileReceived += EventController_InicioRecepcionArchivo;
                eventController.emisor.MessageEmmited += EventController_EnvioMensaje;
                // Establece el nombre del formulario
                Text = Puerto.NombrePuerto;
                MessageBox.Show($"Conexión en el puerto {Puerto.NombrePuerto}");
            } catch (Exception exc)
            {
                MessageBox.Show($"Error al inicializar el puerto {puerto}\n . Error: {exc.Message}\n Stack: {exc.StackTrace}");
            }
        }

        private void MensajeExterno(string mensaje, string emisor)
        {
            rchConversacion.Text += $"{emisor}: {mensaje}\n";
        }
        private void EventController_LlegoMensaje(object o, string mensajeRecibido)
        {
            Invoke(ObtenerMensajeDeProceso, mensajeRecibido, "El otro puerto");    
        }

        private void EventController_InicioRecepcionArchivo(object o)
        {
            var archivoEscritura = new ArchivoEscritura(ObtenerRutaGuardado());
            eventController.receptor.Inicializar(archivoEscritura);
            //Invoke(ObtenerMensajeDeProceso, mensajeRecibido, "El otro puerto");
        }

        private void EventController_EnvioMensaje(object o, string mensajeEnviado)
        {
            Invoke(ObtenerMensajeDeProceso, mensajeEnviado, Puerto.NombrePuerto);
        }
    }
}
