using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.IO.Ports;

namespace winproySerialPort
{
    class Receiver
    {
        public string MensajeRecibido { get; set; }
        public Trama TramaRecibida { get; set; }
        public ArchivoEscritura archivoRecibido { get; set; }
        public int TamanoDeTrama { get; set; }
        public int TamanoDeCabecera { get; set; }
        public byte ByteRelleno { get; set; }
        public struct Cabecera
        {
            public char tipoDeArchivo;
            public long tamanoDeArchivo;
        }
        public Cabecera CabeceraDecodificada { get; set; }

        public delegate void ReceiveMessageHandler(object obj, string mensajeRecibido);
        private event ReceiveMessageHandler messageReceived;
        public event ReceiveMessageHandler MessageReceived
        {
            add
            {
                messageReceived += new ReceiveMessageHandler(value);
            }
            remove
            {
                messageReceived -= value;
            }
        }

        public delegate void ReceiveFileHandler(object obj, ArchivoEscritura archivoRecibido);
        private event ReceiveFileHandler fileReceived;
        public event ReceiveFileHandler FileReceived
        {
            add
            {
                fileReceived += new ReceiveFileHandler(value);
            }
            remove
            {
                fileReceived -= value;
            }
        }

        public delegate void BeforeReceiveHandler(object obj);
        private event BeforeReceiveHandler dataFromFileReceived;
        public event BeforeReceiveHandler DataFromFileReceived
        {
            add
            {
                dataFromFileReceived += new BeforeReceiveHandler(value);
            }
            remove
            {
                dataFromFileReceived -= value;
            }
        }

        private int TamanoLeido;
       
        public Receiver(string nombrePuerto = "COM1", int tamanoTrama = 1024, int tamanoCabecera = 5, byte byteRelleno = 64)
        {
            Puerto.Inicializar(nombrePuerto);
            TamanoDeTrama = tamanoTrama;
            TamanoDeCabecera = tamanoCabecera;
            ByteRelleno = byteRelleno;
            TramaRecibida = new Trama(TamanoDeTrama, TamanoDeCabecera, ByteRelleno);
            MensajeRecibido = "";
            TamanoLeido = 0;
            CabeceraDecodificada = new Cabecera();
        }
        public void Inicializar(string mensaje = "")
        {
            MensajeRecibido = mensaje;
        }
        public void Inicializar(ArchivoEscritura archivo)
        {
            archivoRecibido = archivo;
        }
        public void Recibir()
        {
            LeerTrama();
            switch (CabeceraDecodificada.tipoDeArchivo)
            {
                case 'M':
                    RecibirMensaje();
                    break;
                case 'A':
                    OnDatosDeArchivo();
                    RecibirArchivo();
                    break;
                case 'I':
                    break;
                default:
                    // Trama no reconocida
                    break;
            }
        }
        public void RecibirMensaje()
        {
            while (CabeceraDecodificada.tamanoDeArchivo > TamanoLeido)
            {
                var cuerpoValido = TramaRecibida.ObtenerCuerpoValido();
                MensajeRecibido += Encoding.UTF8.GetString(cuerpoValido);
                TamanoLeido += cuerpoValido.Length;
                LeerTrama();
            }
            OnMensajeRecibido();
        }
        public void RecibirArchivo()
        {
            while (CabeceraDecodificada.tamanoDeArchivo > TamanoLeido)
            {
                var cuerpoValido = TramaRecibida.ObtenerCuerpoValido();
                archivoRecibido.Escribir(cuerpoValido);
                TamanoLeido += cuerpoValido.Length;
                LeerTrama();
            }
            archivoRecibido.DesactivarArchivo();
            OnArchivoRecibido();
        }

        private void LeerTrama()
        {
            Puerto.Leer(TramaRecibida, TamanoDeTrama);
            TramaRecibida.ObtenerCuerpoDesdeContenido();
            TramaRecibida.ObtenerCabeceraDesdeContenido();
            DecodificarCabecera();

        }
        protected virtual void OnMensajeRecibido()
        {
            messageReceived?.Invoke(this, MensajeRecibido);
            MensajeRecibido = "";
            TamanoLeido = 0;
        }
        protected virtual void OnDatosDeArchivo()
        {
            dataFromFileReceived?.Invoke(this);
            TamanoLeido = 0;
        }
        protected virtual void OnArchivoRecibido()
        {
            fileReceived?.Invoke(this, archivoRecibido);
            TamanoLeido = 0;
        }
        public void DecodificarCabecera()
        {
            string cabecera = Encoding.UTF8.GetString(TramaRecibida.Cabecera);
            string tarea = cabecera.Substring(0, 1);
            CabeceraDecodificada = new Cabecera
            {
                tipoDeArchivo = tarea[0],
                tamanoDeArchivo = int.Parse(cabecera.Substring(1))
            };
        }
        

    }

}
