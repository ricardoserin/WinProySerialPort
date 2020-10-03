using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace winproySerialPort.Controladores
{
    static class EmmitController
    {
        public static Emmiter Emisor { get; private set; }
        public static Queue<TramasEnvio> ColaEnvio { get; set; }
        public static bool Inicializado = false;

        private static Thread procesoEnvio;
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

        public static void Enviar(ArchivoEnvio archivoEnvio)
        {
            var tramas = archivoEnvio.Disassemble();
            ColaEnvio.Enqueue(tramas);
        }

        public static void Enviar(Mensaje mensaje)
        {
            var tramas = mensaje.Disassemble();
            ColaEnvio.Enqueue(tramas);
        }

        private static void EnviarArchivos()
        {
            while(true)
            {
                if (!Emisor.Enviando && ColaEnvio.Count > 0)
                {
                    var tramas = ColaEnvio.Dequeue();
                    Emisor.Transmitir(tramas);
                }
            }
        }
    }
}
