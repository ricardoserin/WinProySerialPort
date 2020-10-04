
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Reflection;

namespace winproySerialPort
{
    static class EventController
    {
        public static Receiver Receptor { get; private set; }
        private static Thread procesoRecepcion;

        public static void Inicializar(string nombrePuerto, int baudios = 57600, int tamanoDeTrama = 1024)
        {
            Receptor = new Receiver(nombrePuerto);
            Puerto.TamanoDeTrama = tamanoDeTrama;
            Puerto.DataRecibida += receptor_DataReceived;
        }

        public static void Recibir()
        {
            // Receptor.Inicializar();
            // procesoRecepcion = new Thread(Receptor.Recibir);
            // procesoRecepcion.Start();
        }
        public static void Recibir(ArchivoRecepcion archivoRecepcion)
        {
            // Receptor.Inicializar();
            // procesoRecepcion = new Thread(Receptor.Recibir);
            // procesoRecepcion.Start();
        }

        // Función que se dispara cuando el receptor recibe datos
        private static void receptor_DataReceived(object o, System.IO.Ports.SerialDataReceivedEventArgs args)
        {
            // if (Puerto.puerto.BytesToRead >= Receptor.TamanoDeTrama)
            //{
             //   Recibir();
            //}
        }
    }
}
