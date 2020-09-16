
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
        // Delegado y eventos mensaje enviado y recibido
        public static Emmiter Emisor { get; private set; }
        public static Receiver Receptor { get; private set; }

        private static Thread procesoEnvio;
        private static Thread procesoRecepcion;

        public static void Inicializar(string nombrePuerto, int baudios = 57600, int tamanoDeTrama = 1024)
        {
            Emisor = new Emmiter(nombrePuerto);
            Receptor = new Receiver(nombrePuerto);
            Puerto.TamanoDeTrama = tamanoDeTrama;
            Puerto.DataRecibida += receptor_DataReceived;
        }

        // Inicia un nuevo proceso que se encarga de enviar un mensaje
        public static void Enviar(string mensaje)
        {
            Emisor.Inicializar(mensaje);
            // procesoEnvio = new Thread(Emisor.Transmitir);
            // procesoEnvio.Start();
        }

        public static void Enviar(ArchivoEnvio archivoEnvio)
        {
            var tramas = archivoEnvio.Disassemble();
            procesoEnvio = new Thread(() => Emisor.Transmitir(tramas));
            procesoEnvio.Start();
        }

        public static void Recibir()
        {
            Receptor.Inicializar();
            procesoRecepcion = new Thread(Receptor.Recibir);
            procesoRecepcion.Start();
        }
        public static void Recibir(ArchivoRecepcion archivoRecepcion)
        {
            Receptor.Inicializar();
            procesoRecepcion = new Thread(Receptor.Recibir);
            procesoRecepcion.Start();
        }

        // Función que se dispara cuando el receptor recibe datos
        private static void receptor_DataReceived(object o, System.IO.Ports.SerialDataReceivedEventArgs args)
        {
            if (Puerto.puerto.BytesToRead >= Receptor.TamanoDeTrama)
            {
                Recibir();
            }
        }
    }
}
