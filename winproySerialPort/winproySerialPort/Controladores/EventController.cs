
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
    class EventController
    {
        // Delegado y eventos mensaje enviado y recibido

        public Emmiter emisor { get; private set; }
        public Receiver receptor { get; private set; }

        private Thread procesoEnvio;
        private Thread procesoRecepcion;

        public void Inicializar(string nombrePuerto, int tamanoDeTrama = 1024)
        {
            InicializarEmisor(nombrePuerto);
            InicializarReceptor(nombrePuerto);
            Puerto.TamanoDeTrama = tamanoDeTrama;
            Puerto.DataRecibida += receptor_DataReceived;
        }

        public void InicializarEmisor(string nombrePuerto)
        {
            emisor = new Emmiter(nombrePuerto);
        }

        public void InicializarReceptor(string nombrePuerto)
        {
            receptor = new Receiver(nombrePuerto);
        }

        // Inicia un nuevo proceso que se encarga de enviar un mensaje
        public void Enviar(string mensaje)
        {
            emisor.Inicializar(mensaje);
            procesoEnvio = new Thread(emisor.Transmitir);
            procesoEnvio.Start();
        }

        public void Enviar(ArchivoLectura archivoLectura)
        {
            emisor.Inicializar(archivoLectura);
            procesoEnvio = new Thread(emisor.Transmitir);
            procesoEnvio.Start();
        }

        public void Recibir()
        {
            receptor.Inicializar();
            procesoRecepcion = new Thread(receptor.Recibir);
            procesoRecepcion.Start();
        }
        public void Recibir(ArchivoEscritura archivo)
        {
            receptor.Inicializar(archivo);
            procesoRecepcion = new Thread(receptor.Recibir);
            procesoRecepcion.Start();
        }

        // Función que se dispara cuando el receptor recibe datos
        private void receptor_DataReceived(object o, System.IO.Ports.SerialDataReceivedEventArgs args)
        {
            if (Puerto.puerto.BytesToRead >= receptor.TamanoDeTrama)
            {
                Recibir();
            }
        }



    }
}
