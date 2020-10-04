using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.IO.Ports;
using winproySerialPort.Modelos;

namespace winproySerialPort
{
    class Receiver
    {
        public delegate void ReceiveHandler(object obj, string mensaje);
        private event ReceiveHandler frameReceived;
        private event ReceiveHandler receptionStarted;
        public bool Recibiendo { get; set; }
        public event ReceiveHandler FrameReceived
        {
            add
            {
                frameReceived += new ReceiveHandler(value);
            }
            remove
            {
                frameReceived -= value;
            }
        }
        public event ReceiveHandler ReceptionStarted
        {
            add
            {
                receptionStarted += new ReceiveHandler(value);
            }
            remove
            {
                receptionStarted -= value;
            }
        }

        public Receiver(string nombrePuerto = "COM1", int tamanoDeTramas = 1024, int baudios = 57600)
        {
            Puerto.Inicializar(nombrePuerto, tamanoDeTramas);
            
        }
        public Trama Recibir()
        {
            // var bytes = Puerto.Leer(1024);
            if (Puerto.puerto.BytesToRead >= 1024)
            {
                OnRecepcionIniciada();
                var trama = new Trama(Puerto.Leer(1024));
                OnTramaRecibida();
                return trama;
            }
            return null;
        }

        protected virtual void OnTramaRecibida()
        {
            Recibiendo = false;
            frameReceived?.Invoke(this, "Trama recibida");
        }

        protected virtual void OnRecepcionIniciada()
        {
            Recibiendo = true;
            receptionStarted?.Invoke(this, "Inicio recepción");
        }
    }

}
