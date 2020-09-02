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
        public int TamanoDeTrama { get; set; }
        public int TamanoDeCabecera { get; set; }
        public byte ByteRelleno { get; set; }

        public delegate void ReceiveHandler(object obj, string mensajeRecibido);
        private event ReceiveHandler messageReceived;
        public event ReceiveHandler MessageReceived
        {
            add
            {
                messageReceived += new ReceiveHandler(value);
            }
            remove
            {
                messageReceived -= value;
            }
        }
        
        private int TamanoMensajeRecibido;
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
        }
        public void Recibir()
        {
            Puerto.Leer(TramaRecibida, TamanoDeTrama);
            TramaRecibida.ObtenerCabeceraDesdeContenido();
            TamanoMensajeRecibido = int.Parse(Encoding.UTF8.GetString(TramaRecibida.Cabecera));
            
            while (TamanoMensajeRecibido > TamanoLeido)
            {
                TramaRecibida.ObtenerCuerpoDesdeContenido();
                var cuerpoValido = TramaRecibida.ObtenerCuerpoValido();
                MensajeRecibido += Encoding.UTF8.GetString(cuerpoValido);
                TamanoLeido += cuerpoValido.Length;
                Puerto.Leer(TramaRecibida, TamanoDeTrama);
            }
            onMensajeRecibido();
        }
        protected virtual void onMensajeRecibido()
        {
            messageReceived?.Invoke(this, MensajeRecibido);
        }

        

    }

}
