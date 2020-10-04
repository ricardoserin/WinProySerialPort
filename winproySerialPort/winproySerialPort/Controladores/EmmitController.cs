using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using winproySerialPort.Modelos;

namespace winproySerialPort.Controladores
{
    static class EmmitController
    {
        public static Emmiter Emisor { get; private set; }
        public static Queue<TramasEnvio> ColaEnvio { get; set; }
        public static bool Inicializado { get; set; } = false;

        public delegate void EmmisionHandler(string mensaje);

        public static event EmmisionHandler fileTransmissionStarted;
        public static event EmmisionHandler messageEmmited;

        public static event EmmisionHandler FileTransmissionStarted
        {
            add
            {
                fileTransmissionStarted += new EmmisionHandler(value);
            }
            remove
            {
                fileTransmissionStarted -= value;
            }
        }
        public static event EmmisionHandler MessageEmmited
        {
            add
            {
                messageEmmited += new EmmisionHandler(value);
            }
            remove
            {
                messageEmmited -= value;
            }
        }

        private static Thread procesoEnvio;
        private static int CurrentId = 0;
        public static void Inicializar(string nombrePuerto)
        {
            if (!Inicializado)
            {
                Emisor = new Emmiter(nombrePuerto);
                ColaEnvio = new Queue<TramasEnvio>();
                procesoEnvio = new Thread(EnviarArchivos);
                procesoEnvio.Start();
                Inicializado = true;
            }
        }

        public static void Enviar(ArchivoEnvio archivoEnvio, string emisor)
        {
            var tramas = archivoEnvio.Disassemble(emisor, CurrentId++);
            ColaEnvio.Enqueue(tramas);
        }

        public static void Enviar(Mensaje mensaje, string emisor)
        {
            var tramas = mensaje.Disassemble(emisor, CurrentId++);
            ColaEnvio.Enqueue(tramas);
        }

        private static void EnviarArchivos()
        {
            while(true)
            {
                if (!Emisor.Enviando && ColaEnvio.Count > 0)
                {
                    var tramas = ColaEnvio.Dequeue();
                    if (tramas.TipoDatos == ETipoTrama.Archivo) OnInicioEnvioArchivo(tramas);
                    Emisor.Transmitir(tramas);
                    OnMensajeEnviado(tramas);
                }
            }
        }

        private static void OnInicioEnvioArchivo(TramasEnvio tramas)
        {
            fileTransmissionStarted?.Invoke(tramas.DisplayMessage);
        }

        private static void OnMensajeEnviado(TramasEnvio tramas)
        {
            messageEmmited?.Invoke(tramas.DisplayMessage);
        }
    }
}
