using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using winproySerialPort.Modelos;

namespace winproySerialPort.Controladores
{
    static class ReceiverController
    {
        public static Receiver Receptor { get; private set; }

        public delegate void ReceptionHandler(string nombreEmisor, string datosRecepcion);

        public static event ReceptionHandler fileReceptionStarted;
        public static event ReceptionHandler messageReceived;

        public static event ReceptionHandler FileReceptionStarted
        {
            add
            {
                fileReceptionStarted += new ReceptionHandler(value);
            }
            remove
            {
                fileReceptionStarted -= value;
            }
        }
        public static event ReceptionHandler MessageReceived
        {
            add
            {
                messageReceived += new ReceptionHandler(value);
            }
            remove
            {
                messageReceived -= value;
            }
        }

        private static bool Inicializado = false;
        private static Thread procesoRecepcion;
        private static List<TramasRecepcion> ListaRecepcion;
        private static Queue<Trama> TramasPorProcesar;

        public static void Inicializar(string nombrePuerto)
        {
            if (!Inicializado)
            {
                Receptor = new Receiver(nombrePuerto);
                ListaRecepcion = new List<TramasRecepcion>();
                TramasPorProcesar = new Queue<Trama>();
                Receptor.FrameReceived += Reciever_frameReceived;
                procesoRecepcion = new Thread(EnsamblarTramas);
                procesoRecepcion.Start();
                Inicializado = true;
            }
        }

        public static void Recibir(Trama trama)
        {
            var cabecera = new Cabecera(trama);
            switch (cabecera.TipoContenido)
            {
                case ETipoContenido.Cabecera:
                    var receptor = new TramasRecepcion(trama);
                    ListaRecepcion.Add(receptor);
                    if (cabecera.TipoTrama == ETipoTrama.Archivo) OnInicioRecepcionArchivo(receptor);
                    break;
                case ETipoContenido.Informacion:
                    var id = cabecera.IdRecepcion;
                    var receptorEncontrado = ListaRecepcion.Find(tramaRecepcion => tramaRecepcion.IdRecepcion == id);
                    receptorEncontrado.RecibirTrama(trama);
                    if (receptorEncontrado.Completado)
                    {
                        receptorEncontrado.Ensamblar(/* TODO: pasar path */);
                        ListaRecepcion.Remove(receptorEncontrado);
                        OnMensajeRecibido(receptorEncontrado);
                    }
                    break;
            }
        }

        public static void Reciever_frameReceived(string mensaje)
        {
            var trama = Receptor.Recibir();
            if(trama != null) TramasPorProcesar.Enqueue(trama);
        }

        private static void OnInicioRecepcionArchivo(TramasRecepcion receptor)
        {
            fileReceptionStarted?.Invoke(receptor.Emisor, receptor.MetaDatos[2] + receptor.MetaDatos[3]);
        }
        private static void OnMensajeRecibido(TramasRecepcion receptor)
        {
            messageReceived?.Invoke(receptor.Emisor, receptor.DisplayMessage);
        }

        public static void EnsamblarTramas()
        {
            while (true)
            {
                if (TramasPorProcesar.Count > 0)
                {
                    Recibir(TramasPorProcesar.Dequeue());
                }
            }
        }
    }
}
