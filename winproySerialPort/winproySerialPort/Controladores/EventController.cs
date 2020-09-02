
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
        

        private Thread procesoEnvio;
        public Emmiter emisor { get; private set; }
        public Receiver receptor { get; private set; }

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

        // Función que se dispara cuando el receptor recibe datos
        private void receptor_DataReceived(object o, System.IO.Ports.SerialDataReceivedEventArgs args)
        {
            if (Puerto.puerto.BytesToRead >= receptor.TamanoDeTrama)
            {
                receptor.Recibir();
            }
        }



    }
}
